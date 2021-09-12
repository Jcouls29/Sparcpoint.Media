using Microsoft.Extensions.Logging;
using Sparcpoint.Media.Ripper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedAutoRipService : IAutoRipService, IDisposable
    {
        private readonly ILogger<LoggedAutoRipService> _Logger;
        private readonly IAutoRipService _InnerService;

        public LoggedAutoRipService(ILogger<LoggedAutoRipService> logger, IAutoRipService innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));

            _InnerService.TitleRipCompleted += _InnerService_TitleRipCompleted;
            _InnerService.TitleErrorOccurred += _InnerService_TitleErrorOccurred;
            _InnerService.DiscCompleted += _InnerService_DiscCompleted;
        }

        private void _InnerService_DiscCompleted(object sender, EventArgs e)
        {
            _Logger.LogInformation("Disc Completed!");
        }

        private void _InnerService_TitleErrorOccurred(object sender, TitleRipErrorEventArgs e)
        {
            _Logger.LogError(e.Exception, "Error Occurred! {Message}", e.Exception.Message);
        }

        private void _InnerService_TitleRipCompleted(object sender, TitleRipEventArgs e)
        {
            _Logger.LogInformation("Title Ripped! File = {FilePath}", e.Record.OriginalFileName);
        }

        public void Dispose()
        {
            _InnerService.TitleRipCompleted -= _InnerService_TitleRipCompleted;
            _InnerService.TitleErrorOccurred -= _InnerService_TitleErrorOccurred;
            _InnerService.DiscCompleted -= _InnerService_DiscCompleted;
        }

        public event EventHandler<TitleRipEventArgs> TitleRipCompleted
        {
            add { _InnerService.TitleRipCompleted += value; }
            remove { _InnerService.TitleRipCompleted -= value; }
        }

        public event EventHandler DiscCompleted
        {
            add { _InnerService.DiscCompleted += value; }
            remove { _InnerService.DiscCompleted -= value; }
        }

        public event EventHandler<TitleRipErrorEventArgs> TitleErrorOccurred
        {
            add { _InnerService.TitleErrorOccurred += value; }
            remove { _InnerService.TitleErrorOccurred -= value; }
        }

        public async Task AutoRipAsync(CancellationToken cancelToken = default)
        {
            using (_Logger.Measure("Auto-Ripped Completed"))
            {
                _Logger.LogInformation("Auto-Rip service starting up...");
                await _InnerService.AutoRipAsync(cancelToken);
                _Logger.LogInformation("Auto-rip service shutting down...");
            }
        }
    }
}
