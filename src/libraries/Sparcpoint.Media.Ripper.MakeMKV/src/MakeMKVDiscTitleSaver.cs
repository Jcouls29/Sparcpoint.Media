using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    public class MakeMKVDiscTitleSaver : IDiscTitleSaver
    {
        private readonly ICommandLineExecutor _Executor;

        public MakeMKVDiscTitleSaver(ICommandLineExecutor executor)
        {
            _Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public async Task<MediaFileInfo> SaveTitleAsync(DiscTitleRecord record, SaveTitleOptions options, CancellationToken cancelToken = default)
        {
            EnsureValidOptions(options);
            
            var cmd = new SaveTitleToFolderCommand(record.DiscRecord.Number, record.Index, options.DirectoryPath);
            var result = await cmd.RunCommand(_Executor, cancelToken);
            result.EnsureSuccessResult();

            string filePath = Path.Combine(options.DirectoryPath, record.OriginalFileName);
            FileInfo fileInfo = new FileInfo(filePath);

            return new MediaFileInfo
            {
                FilePath = filePath,
                FileSize = fileInfo.Length,
                Length = record.Length,
                TitleIndex = int.Parse(record.Id)
            };
        }

        public async Task<IEnumerable<MediaFileInfo>> SaveTitlesAsync(IEnumerable<DiscTitleRecord> records, SaveTitleOptions options, CancellationToken cancelToken = default)
        {
            EnsureValidOptions(options);

            List<MediaFileInfo> results = new List<MediaFileInfo>();
            foreach(var record in records)
            {
                results.Add(await SaveTitleAsync(record, options, cancelToken));
            }
            return results;
        }

        private void EnsureValidOptions(SaveTitleOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.DirectoryPath))
                throw new ArgumentException("Directory Path must be specified.", nameof(options));

            EnsureDirectoryExists(options.DirectoryPath);
        }

        private void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}
