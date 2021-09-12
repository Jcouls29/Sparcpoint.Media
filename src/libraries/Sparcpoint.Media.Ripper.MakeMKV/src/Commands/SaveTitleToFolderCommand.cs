using System;
using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    internal class SaveTitleToFolderCommand : MakeMKVCommandParameters
    {
        private readonly int _DiscId;
        private readonly int _TitleId;
        private readonly string _OutputPath;

        public SaveTitleToFolderCommand(int discId, int titleId, string outputPath)
        {
            _DiscId = discId;
            _TitleId = titleId;
            _OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));

            UseRobotMode = true;
        }

        protected override IEnumerable<string> GetCommandArguments()
            => new[] { "mkv", $"disc:{_DiscId}", _TitleId.ToString(), _OutputPath };
    }
}
