using System;

namespace Sparcpoint.Media.Ripper
{
    public sealed class RangedNamingConvention : IAutoRipNamingConvention
    {
        private readonly TimeSpan _MinLength;
        private readonly TimeSpan _MaxLength;
        private readonly string _NamingConvention;
        private int _SessionIndex;
        private readonly int _MaxTitles;

        public RangedNamingConvention(TimeSpan minLength, TimeSpan maxLength, string namingConvention, int maxTitles = int.MaxValue, int sessionIndex = 0)
        {
            NamingConventionHelpers.EnsureProperNamingConvention(namingConvention);

            if (maxLength < minLength)
                throw new ArgumentException("Max Length must be longer than min length");

            _MinLength = minLength;
            _MaxLength = maxLength;
            _NamingConvention = namingConvention ?? throw new ArgumentNullException(nameof(namingConvention));
            _SessionIndex = sessionIndex;
            _MaxTitles = maxTitles;
        }

        public NamingConventionResult GetNextFileName(DiscTitleRecord record)
            => new NamingConventionResult
            {
                SubFolder = null,
                FileName = NamingConventionHelpers.ReplaceIndex(_NamingConvention, record.Index, _SessionIndex++, record.DiscRecord.Number)
            };

        public bool ShouldInclude(DiscTitleRecord record)
            => _MinLength < record.Length && record.Length < _MaxLength && _SessionIndex < _MaxTitles;
    }
}
