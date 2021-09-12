using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper.MakeMKV
{
    public class MakeMKVDriveQuery : IDriveQuery
    {
        private readonly ICommandLineExecutor _Executor;

        public MakeMKVDriveQuery(ICommandLineExecutor executor)
        {
            _Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public async Task<IEnumerable<DriveInformation>> QueryAllDrivesAsync(CancellationToken cancelToken = default)
        {
            var cmdResult = await (new DriveQueryAllCommand()).RunCommand(_Executor, cancelToken);
            cmdResult.EnsureSuccessResult();

            List<DriveInformation> results = new List<DriveInformation>();
            foreach(var line in cmdResult.Messages)
            {
                var match = DRIVE_PATTERN.Match(line);
                if (match.Success)
                {
                    if (!int.TryParse(match.Groups["DeviceId"].Value, out int deviceId))
                        continue;

                    if (!int.TryParse(match.Groups["IsEnabled"].Value, out int intIsEnabled))
                        continue;

                    //bool isEnabled = intIsEnabled > 0 ? true : false;
                    //if (!isEnabled)
                    //    continue;

                    string deviceName = match.Groups["DeviceName"].Value;
                    string discName = match.Groups["DiscName"].Value;
                    string driveName = match.Groups["DriveName"].Value;

                    if (string.IsNullOrWhiteSpace(driveName))
                        continue;

                    results.Add(new DriveInformation
                    {
                        DriveId = deviceId,
                        DeviceName = deviceName,
                        DiscName = discName,
                        DriveName = driveName
                    });
                }
            }

            return results;
        }

        private static Regex DRIVE_PATTERN = new Regex(@"^\s*DRV:(?<DeviceId>\d+),(\d+),(\d+),(?<IsEnabled>\d),\""(?<DeviceName>.*?)\"",\""(?<DiscName>.*?)\"",\""(?<DriveName>.*?)\""\s*$");
    }
}
