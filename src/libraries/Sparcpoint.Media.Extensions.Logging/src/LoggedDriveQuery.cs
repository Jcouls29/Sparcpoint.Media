using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedDriveQuery : IDriveQuery
    {
        private readonly ILogger<LoggedDriveQuery> _Logger;
        private readonly IDriveQuery _InnerService;

        public LoggedDriveQuery(ILogger<LoggedDriveQuery> logger, IDriveQuery innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        }

        public async Task<IEnumerable<DriveInformation>> QueryAllDrivesAsync(CancellationToken cancelToken = default)
        {
            using (_Logger.Measure("Queried Drives"))
            {
                _Logger.LogDebug("Querying All Drives...");
                var results = await _InnerService.QueryAllDrivesAsync(cancelToken);
                foreach (var result in results)
                    _Logger.LogInformation("\tFound Drive: {DriveName} ({DriveId})", result.DriveName, result.DriveId);

                return results;
            }
        }
    }
}
