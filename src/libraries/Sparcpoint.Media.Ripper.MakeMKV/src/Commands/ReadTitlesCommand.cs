using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    internal class ReadTitlesCommand : MakeMKVCommandParameters
    {
        private readonly int _DiscId;

        public ReadTitlesCommand(int discId)
        {
            _DiscId = discId;
            CacheSizeInMB = 512;
            UseRobotMode = true;
        }

        protected override IEnumerable<string> GetCommandArguments()
            => new[] { "info", $"disc:{_DiscId}" };
    }
}
