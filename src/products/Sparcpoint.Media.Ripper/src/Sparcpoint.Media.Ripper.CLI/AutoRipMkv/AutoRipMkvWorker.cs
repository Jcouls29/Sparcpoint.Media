using Microsoft.Extensions.Logging;
using Sparcpoint.Notification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.CLI
{
    public class AutoRipMkvWorker
    {
        private readonly ILoggerFactory _Factory;
        private readonly CancellationToken _Token;

        public AutoRipMkvWorker(ILoggerFactory factory, CancellationToken token)
        {
            _Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            
            _Token = token;
        }

        public async Task Run(AutoRipMakeMkvOptions options)
        {
            var conventions = CreateStandardConventions(options);

            ICommandLineExecutor executor = new CliWrapCommandLineExecutor(options.MakeMKVPath).Wrap(_Factory);
            IAutoRipService service = new DefaultAutoRipService(
                new MakeMKV.MakeMKVDriveQuery(executor).Wrap(_Factory),
                new MakeMKV.MakeMKVDiscTitleReader(executor).Wrap(_Factory),
                new MakeMKV.MakeMKVDiscTitleSaver(executor).Wrap(_Factory),
                new DefaultDiscDriveAccess().Wrap(_Factory),
                new Ripper.AutoRipOptions
                {
                    RootPath = options.TargetFolder,
                    NamingConventions = conventions[options.Mode],
                    StartingDiscNumber = options.StartingDiscNumber,
                }).Wrap(_Factory);

            if (!string.IsNullOrWhiteSpace(options.GoogleUsername) && !string.IsNullOrWhiteSpace(options.GooglePassword))
            {
                if (!string.IsNullOrWhiteSpace(options.MobileNumber))
                {
                    var sms = new SafeSmsNotification(
                        _Factory.CreateLogger<SafeSmsNotification>(), 
                        new EmailSmsNotification(SmtpEmailOptions.Google(options.GoogleUsername, options.GooglePassword, "DVD Ripping"))
                    );

                    service.TitleErrorOccurred += async (sender, e) =>
                    {
                        await sms.SendAsync(options.MobileNumber, options.Provider, $"Error! {e.Exception?.Message}");
                    };
                    service.DiscCompleted += async (sender, e) =>
                    {
                        await sms.SendAsync(options.MobileNumber, options.Provider, "Disc Completed!");
                    };
                }
            }

            Console.WriteLine("Auto-Rip Started...");
            Console.WriteLine($"Title: {options.Title}, Season: {options.Season}");
            await service.AutoRipAsync(_Token);
        }

        static Dictionary<MakeMKVConventionMode, ICollection<IAutoRipNamingConvention>> CreateStandardConventions(AutoRipMakeMkvOptions options)
        {
            var results = new Dictionary<MakeMKVConventionMode, ICollection<IAutoRipNamingConvention>>();

            results.Add(MakeMKVConventionMode.Standard, new List<IAutoRipNamingConvention>
            {
                new RangedNamingConvention(TimeSpan.FromSeconds(30), TimeSpan.MaxValue, $"{options.Title} - {{TitleIndex}}.mkv")
            });

            results.Add(MakeMKVConventionMode.Movie, new List<IAutoRipNamingConvention>
            {
                new RangedNamingConvention(TimeSpan.FromMinutes(90), TimeSpan.MaxValue, $"{options.Title} - {{TitleIndex}}.mkv"),
                new StaticNamingConvention("Extras-{TitleIndex}.mkv"),
            });

            results.Add(MakeMKVConventionMode.PlexTV_20min, new List<IAutoRipNamingConvention>
            {
                new PlexTVSeriesNamingConvention(TimeSpan.FromMinutes(19), TimeSpan.FromMinutes(23), options.Title, options.Season, startingIndex: options.StartingEpisodeNumber - 1),
            });

            results.Add(MakeMKVConventionMode.PlexTV_45min, new List<IAutoRipNamingConvention>
            {
                new PlexTVSeriesNamingConvention(TimeSpan.FromMinutes(42), TimeSpan.FromMinutes(48), options.Title, options.Season, startingIndex: options.StartingEpisodeNumber - 1),
            });

            results.Add(MakeMKVConventionMode.PlexTV_Custom, new List<IAutoRipNamingConvention>
            {
                new PlexTVSeriesNamingConvention(TimeSpan.FromMinutes(options.MinimumTitleLength), TimeSpan.FromMinutes(options.MaximumTitleLength), options.Title, options.Season, startingIndex: options.StartingEpisodeNumber - 1),
            });

            return results;
        }
    }

}
