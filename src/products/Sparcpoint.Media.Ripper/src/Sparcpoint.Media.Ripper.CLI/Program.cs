using CommandLine;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.CLI
{
    class Program
    {
        static CancellationTokenSource _CancelSource;
        static ILoggerFactory _LoggerFactory;

        static async Task Main(string[] args)
        {
            _CancelSource = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) =>
            {
                Console.WriteLine("Canceling...");
                _CancelSource.Cancel();
                e.Cancel = true;
            };

            _LoggerFactory = BuildLoggerFactory();

            try
            {
                await Parser.Default.ParseArguments<AutoRipMakeMkvOptions>(args)
                    .WithParsedAsync(new AutoRipMkvWorker(_LoggerFactory, _CancelSource.Token).Run)
                ;
            } catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
        }

        static ILoggerFactory BuildLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Sparcpoint", LogLevel.Debug)
                    .AddConsole();
            });
        }
    }
}
