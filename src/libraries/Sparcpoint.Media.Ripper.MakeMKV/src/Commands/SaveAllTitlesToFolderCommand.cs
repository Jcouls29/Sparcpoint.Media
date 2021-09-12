using System;
using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    internal class SaveAllTitlesToFolderCommand : MakeMKVCommandParameters
    {
        private readonly int _DiscId;
        private readonly int _TitleId;
        private readonly string _OutputPath;

        public SaveAllTitlesToFolderCommand(int discId, string outputPath, int minLengthSeconds = 0)
        {
            _DiscId = discId;
            _OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));

            UseRobotMode = true;

            if (minLengthSeconds > 0)
                MinimumLengthInSeconds = minLengthSeconds;
        }

        protected override IEnumerable<string> GetCommandArguments()
            => new[] { "mkv", $"disc:{_DiscId}", "all", _OutputPath };
    }
}
