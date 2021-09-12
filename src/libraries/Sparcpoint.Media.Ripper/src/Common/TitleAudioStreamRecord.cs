namespace Sparcpoint.Media.Ripper
{
    public class TitleAudioStreamRecord
    {
        public string Id { get; set; }
        public string AudioType { get; set; }
        public string Encoding { get; set; }
        public string FriendlyEncodingName { get; set; }
        public ByteRate BitRate { get; set; }
        public string Language { get; set; }
    }
}
