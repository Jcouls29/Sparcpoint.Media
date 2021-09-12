using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper
{
    public interface IDiscTitleSaver
    {
        Task<MediaFileInfo> SaveTitleAsync(DiscTitleRecord record, SaveTitleOptions options, CancellationToken cancelToken = default);
        Task<IEnumerable<MediaFileInfo>> SaveTitlesAsync(IEnumerable<DiscTitleRecord> records, SaveTitleOptions options, CancellationToken cancelToken = default);
    }

    public class MediaFileInfo
    {
        public int TitleIndex { get; set; }
        public TimeSpan Length { get; set; }

        public string FilePath { get; set; }
        public FileSize FileSize { get; set; }
    }
}
