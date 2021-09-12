using System;

namespace Sparcpoint.Media.Ripper
{
    public static class ReadTitleMetadataOptionsExtensions
    {
        public static ReadTitleMetadataOptions WithMinimumLength(this ReadTitleMetadataOptions opt, TimeSpan length)
            => new ReadTitleMetadataOptions { MinimumLength = length, MaximumLength = opt.MaximumLength };
        public static ReadTitleMetadataOptions WithMaximumLength(this ReadTitleMetadataOptions opt, TimeSpan length)
            => new ReadTitleMetadataOptions { MinimumLength = opt.MinimumLength, MaximumLength = length };
    }
}
