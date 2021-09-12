namespace Sparcpoint.Media.Ripper
{
    public class TitleSubtitleStreamRecord
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string Encoding { get; set; }
        public string FriendlyEncodingName { get; set; }
        public ByteRate BitRate { get; set; }
    }
}
