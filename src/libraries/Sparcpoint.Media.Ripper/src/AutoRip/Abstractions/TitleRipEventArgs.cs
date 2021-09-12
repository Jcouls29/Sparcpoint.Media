using System;

namespace Sparcpoint.Media.Ripper
{
    public class TitleRipEventArgs : EventArgs
    {
        public DiscTitleRecord Record { get; set; }
    }
}
