using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media
{
    public sealed class DefaultDiscDriveAccess : IDiscDriveAccess
    {
        public void EjectDisc(string driveName)
        {
            var foundDrive = FindDrive(driveName);
            var driveLetter = foundDrive.Name[0];
            DiscEjector.EjectMedia(driveLetter);
        }

        public async Task WaitForDiscDriveAsync(string driveName, CancellationToken cancelToken = default)
        {
            // Used to make sure the drive is available initially
            FindDrive(driveName);

            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    var foundDrive = FindDrive(driveName);
                    if (foundDrive.IsReady)
                        return;
                }
                catch { }
                finally
                {
                    await Task.Delay(500);
                }
            }
        }

        private DriveInfo FindDrive(string driveName)
        {
            var allDrives = DriveInfo.GetDrives();
            var foundDrive = allDrives
                .Where(d => d.DriveType == DriveType.CDRom && d.Name.StartsWith(driveName, System.StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();

            if (foundDrive == null)
                throw new System.Exception($"Could not find drive with name '{driveName}'.");

            return foundDrive;
        }
    }
}
