using Microsoft.Extensions.Logging;
using Sparcpoint.Media.Ripper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedDiscTitleReader : IDiscTitleReader
    {
        private readonly ILogger<LoggedDiscTitleReader> _Logger;
        private readonly IDiscTitleReader _InnerService;

        public LoggedDiscTitleReader(ILogger<LoggedDiscTitleReader> logger, IDiscTitleReader innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        }

        public async Task<IEnumerable<DiscTitleRecord>> ReadTitleMetadata(DriveInformation drive, ReadTitleMetadataOptions options = null, CancellationToken cancelToken = default)
        {
            using (_Logger.Measure("Read Titles"))
            {
                _Logger.LogInformation("Reading Title Metadata... (Drive: {DriveName} ({DriveId})", drive.DriveName, drive.DriveId);
                var results = await _InnerService.ReadTitleMetadata(drive, options, cancelToken);
                foreach (var result in results)
                    _Logger.LogInformation("\tFound Title: {Title} ({Id}) => {Length} (Byte Count: {FileSize})", result.Title ?? "Unknown", result.Id, result.Length.ToString(), result.RawFileSize);

                return results;
            }
        }
    }
}
