using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper
{
    public interface IDiscTitleReader
    {
        Task<IEnumerable<DiscTitleRecord>> ReadTitleMetadata(DriveInformation drive, ReadTitleMetadataOptions options = null, CancellationToken cancelToken = default);
    }
}
