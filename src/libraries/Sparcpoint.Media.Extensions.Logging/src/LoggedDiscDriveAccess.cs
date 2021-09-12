using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Extensions.Logging
{
    public sealed class LoggedDiscDriveAccess : IDiscDriveAccess
    {
        private readonly ILogger<LoggedDiscDriveAccess> _Logger;
        private readonly IDiscDriveAccess _InnerService;

        public LoggedDiscDriveAccess(ILogger<LoggedDiscDriveAccess> logger, IDiscDriveAccess innerService)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _InnerService = innerService ?? throw new ArgumentNullException(nameof(innerService));
        }

        public void EjectDisc(string driveName)
        {
            _Logger.LogInformation("Ejecting {DriveName}...", driveName);
            _InnerService.EjectDisc(driveName);
            _Logger.LogInformation("Successfully ejected {DriveName}", driveName);
        }

        public async Task WaitForDiscDriveAsync(string driveName, CancellationToken cancelToken = default)
        {
            using(_Logger.Measure("Finished waiting for Disc"))
            {
                _Logger.LogInformation("Waiting on disc insertion for drive {DriveName}...");
                await _InnerService.WaitForDiscDriveAsync(driveName, cancelToken);
                _Logger.LogInformation("Disc Inserted!");
            }
        }
    }
}
