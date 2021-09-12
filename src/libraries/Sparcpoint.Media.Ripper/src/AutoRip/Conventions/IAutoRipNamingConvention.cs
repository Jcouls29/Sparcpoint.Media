namespace Sparcpoint.Media.Ripper
{
    public interface IAutoRipNamingConvention
    {
        bool ShouldInclude(DiscTitleRecord record);

        NamingConventionResult GetNextFileName(DiscTitleRecord record);
    }

    public class NamingConventionResult
    {
        public string SubFolder { get; set; }
        public string FileName { get; set; }
    }
}
