using CommandLine;
using Sparcpoint.Notification;

namespace Sparcpoint.Media.Ripper.CLI
{
    [Verb("auto-rip-mkv", HelpText = "Auto Rips Blu-Ray(s) / DVD(s) as they are inserted into the drive.")]
    public class AutoRipMakeMkvOptions
    {
        [Option('e', "exe", Default = @"C:\Program Files (x86)\MakeMKV\makemkvcon.exe", HelpText = "Path to the MKV Command Line EXE")]
        public string MakeMKVPath { get; set; }

        [Option('m', "mode", Default = MakeMKVConventionMode.Standard, HelpText = "Naming convention for the output titles", Required = true)]
        public MakeMKVConventionMode Mode { get; set; } = MakeMKVConventionMode.Standard;

        [Option('t', "title", Default = "Title", HelpText = "Title to use during naming")]
        public string Title { get; set; } = "Title";

        [Option("season", Default = 0, HelpText = "Season number for the current rip session. Only used when a TV mode is selected.")]
        public int Season { get; set; } = 0;

        [Option('d', "disc-number", Default = 1, HelpText = "The starting disc number.")]
        public int StartingDiscNumber { get; set; }

        [Option("episode", Default = 1, HelpText = "The starting episode number for this session.")]
        public int StartingEpisodeNumber { get; set; }

        [Option("min-length", Default = 0, HelpText = "Minimum title length to record. Typically used with custom conventions")]
        public int MinimumTitleLength { get; set; }

        [Option("max-length", Default = int.MaxValue, HelpText = "Maximum title length to record. Typically used with custom conventions")]
        public int MaximumTitleLength { get; set; }

        [Option('o', "output", HelpText = "Output folder to store ripped files.", Required = true)]
        public string TargetFolder { get; set; }

        [Option("google-username", HelpText = "Google Username to send notifications.")]
        public string GoogleUsername { get; set; }

        [Option("google-password", HelpText = "Google Password to send notifications.")]
        public string GooglePassword { get; set; }

        [Option("mobile", HelpText = "Mobile number for MMS text sending.")]
        public string MobileNumber { get; set; }

        [Option("mobile-provider", Default = SmsProvider.Verizon, HelpText = "Mobile provider for MMS text sending.")]
        public SmsProvider Provider { get; set; } = SmsProvider.Verizon;
    }
}
