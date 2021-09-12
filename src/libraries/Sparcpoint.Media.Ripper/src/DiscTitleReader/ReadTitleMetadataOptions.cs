using System;

namespace Sparcpoint.Media.Ripper
{
    public sealed class ReadTitleMetadataOptions
    {
        internal ReadTitleMetadataOptions() { }

        public TimeSpan MinimumLength { get; internal set; } = TimeSpan.MinValue;
        public TimeSpan MaximumLength { get; internal set; } = TimeSpan.MaxValue;

        public static ReadTitleMetadataOptions WithMinimumLength(TimeSpan length)
            => new ReadTitleMetadataOptions { MinimumLength = length };
        public static ReadTitleMetadataOptions WithMaximumLength(TimeSpan length)
            => new ReadTitleMetadataOptions { MaximumLength = length };
        public static ReadTitleMetadataOptions Default()
            => new ReadTitleMetadataOptions { };
    }
}
