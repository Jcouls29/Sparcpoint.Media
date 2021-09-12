using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media
{
    public interface IDiscDriveAccess
    {
        Task WaitForDiscDriveAsync(string driveName, CancellationToken cancelToken = default);
        void EjectDisc(string driveName);
    }
}
