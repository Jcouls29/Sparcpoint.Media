using System.Collections.Generic;

namespace Sparcpoint.Media.Ripper
{
    public class DiscRecord
    {
        public string Id { get; set; }
        public int Number { get; set; }

        public string Type { get; set; }
        public string Title { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public string DriveName { get; set; }
    }
}
