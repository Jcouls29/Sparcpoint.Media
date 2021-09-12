namespace Sparcpoint.Media.Ripper
{
    public sealed class StaticNamingConvention : IAutoRipNamingConvention
    {
        private readonly string _NamingConvention;
        private int _SessionIndex;

        public StaticNamingConvention(string namingConvention)
        {
            NamingConventionHelpers.EnsureProperNamingConvention(namingConvention);
            _NamingConvention = namingConvention;
            _SessionIndex = 0;
        }

        public NamingConventionResult GetNextFileName(DiscTitleRecord record)
            => new NamingConventionResult
            {
                SubFolder = null,
                FileName = NamingConventionHelpers.ReplaceIndex(_NamingConvention, record.Index, _SessionIndex++, record.DiscRecord.Number)
            };

        public bool ShouldInclude(DiscTitleRecord record)
            => true;
    }
}
