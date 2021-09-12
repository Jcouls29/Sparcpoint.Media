using System;
using NC = Sparcpoint.Media.Ripper.NamingConventionHelpers;

namespace Sparcpoint.Media.Ripper
{
    public sealed class PlexTVSeriesNamingConvention : IAutoRipNamingConvention
    {
        private readonly RangedNamingConvention _RangeConvention;
        private readonly StaticNamingConvention _ExtrasConvention;
        private readonly string _SubPath;

        public PlexTVSeriesNamingConvention(TimeSpan minLength, TimeSpan maxLength, string seriesName, int seasonNumber, int maxTitles = int.MaxValue, int startingIndex = 0)
        {
            _RangeConvention = new RangedNamingConvention(minLength, maxLength, $"{seriesName} - s{seasonNumber.ToString("00")}e{NC.SESSION_INDEX_PLACEHOLDER}.mkv", maxTitles, sessionIndex: startingIndex);
            _ExtrasConvention = new StaticNamingConvention($"{seriesName} - Extras {NC.DISC_NO_PLACEHOLDER} - {NC.SESSION_INDEX_PLACEHOLDER}.mkv");
            _SubPath = $"{seriesName}\\Season {seasonNumber.ToString("00")}";
        }

        public NamingConventionResult GetNextFileName(DiscTitleRecord record)
        {
            var result = _RangeConvention.ShouldInclude(record) ? 
                _RangeConvention.GetNextFileName(record) : 
                _ExtrasConvention.GetNextFileName(record);

            return new NamingConventionResult
            {
                SubFolder = _SubPath,
                FileName = result.FileName
            };
        }

        public bool ShouldInclude(DiscTitleRecord record)
            => _RangeConvention.ShouldInclude(record) || _ExtrasConvention.ShouldInclude(record);
    }
}
