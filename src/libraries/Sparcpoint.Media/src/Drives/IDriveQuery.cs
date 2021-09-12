using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media
{
    public interface IDriveQuery
    {
        Task<IEnumerable<DriveInformation>> QueryAllDrivesAsync(CancellationToken cancelToken = default);
    }
}
