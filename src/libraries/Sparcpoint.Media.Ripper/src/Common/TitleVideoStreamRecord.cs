namespace Sparcpoint.Media.Ripper
{
    public class TitleVideoStreamRecord
    {
        public string Id { get; set; }
        public string Encoding { get; set; }
        public string FriendlyEncodingName { get; set; }
        public ByteRate BitRate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Language { get; set; }
    }
}
