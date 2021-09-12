using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    internal class DriveQueryAllCommand : MakeMKVCommandParameters
    {
        public DriveQueryAllCommand()
        {
            CacheSizeInMB = 1;
            UseRobotMode = true;
        }

        protected override IEnumerable<string> GetCommandArguments()
            => new string[] { "info", "disc:9999" };
    }
}
