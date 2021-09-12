using System;

namespace Sparcpoint.Media.Ripper
{
    public class TitleRipErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
