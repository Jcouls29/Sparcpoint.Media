using System;
using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper
{
    public class DiscTitleRecord
    {
        public string Id { get; set; }
        public int Index { get; set; }

        public DiscRecord DiscRecord { get; set; }
        
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public FileSize FriendlyFileSize { get; set; }
        public FileSize RawFileSize { get; set; }
        public string OriginalFileName { get; set; }

        public IEnumerable<TitleVideoStreamRecord> VideoStreams { get; set; }
        public IEnumerable<TitleAudioStreamRecord> AudioStreams { get; set; }
        public IEnumerable<TitleSubtitleStreamRecord> SubtitleStreams { get; set; }

        public override string ToString()
        {
            return $"{Title} ({Index})";
        }
    }
}
