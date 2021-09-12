using Microsoft.Extensions.Logging;
using Sparcpoint.Media.Ripper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedDiscTitleSaver : IDiscTitleSaver
    {
        private readonly ILogger<LoggedDiscTitleSaver> _Logger;
        private readonly IDiscTitleSaver _InnerService;

        public LoggedDiscTitleSaver(ILogger<LoggedDiscTitleSaver> logger, IDiscTitleSaver innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        }

        public async Task<MediaFileInfo> SaveTitleAsync(DiscTitleRecord record, SaveTitleOptions options, CancellationToken cancelToken = default)
        {
            using (_Logger.Measure("Title Saved"))
            {
                _Logger.LogInformation("Saving Title... {Title} ({Id}) => {Length}", record.Title ?? "Unknown", record.Id, record.Length);
                var result = await _InnerService.SaveTitleAsync(record, options, cancelToken);
                _Logger.LogInformation("Saved Title! Location = {FilePath} (Byte Size: {FileSize})", result.FilePath, result.FileSize);

                return result;
            }
        }

        public async Task<IEnumerable<MediaFileInfo>> SaveTitlesAsync(IEnumerable<DiscTitleRecord> records, SaveTitleOptions options, CancellationToken cancelToken = default)
        {
            // TODO: Perform Logging
            var results = await _InnerService.SaveTitlesAsync(records, options, cancelToken);
            return results;
        }
    }
}
