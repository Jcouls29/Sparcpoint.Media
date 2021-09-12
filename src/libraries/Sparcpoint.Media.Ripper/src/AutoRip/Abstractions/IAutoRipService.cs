using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper
{
    public interface IAutoRipService
    {
        event EventHandler<TitleRipEventArgs> TitleRipCompleted;
        event EventHandler DiscCompleted;
        event EventHandler<TitleRipErrorEventArgs> TitleErrorOccurred;

        Task AutoRipAsync(CancellationToken cancelToken = default);
    }
}
