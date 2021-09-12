using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sparcpoint.Media.Ripper
{
    public sealed class DefaultAutoRipService : IAutoRipService
    {
        public event EventHandler<TitleRipEventArgs> TitleRipCompleted;
        public event EventHandler DiscCompleted;
        public event EventHandler<TitleRipErrorEventArgs> TitleErrorOccurred;

        private readonly IDriveQuery _DriveQuery;
        private readonly IDiscTitleReader _Reader;
        private readonly IDiscTitleSaver _Saver;
        private readonly IDiscDriveAccess _Access;
        private readonly AutoRipOptions _Options;

        public DefaultAutoRipService(
            IDriveQuery driveQuery, 
            IDiscTitleReader reader, 
            IDiscTitleSaver saver, 
            IDiscDriveAccess access, 
            AutoRipOptions options)
        {
            _DriveQuery = driveQuery ?? throw new ArgumentNullException(nameof(driveQuery));
            _Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _Saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _Access = access ?? throw new ArgumentNullException(nameof(access));

            if (options == null)
                throw new ArgumentNullException(nameof(options));
            
            options.EnsureValidOptions();
            _Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task AutoRipAsync(CancellationToken cancelToken = default)
        {
            var foundDrive = (await _DriveQuery.QueryAllDrivesAsync(cancelToken)).FirstOrDefault();
            if (foundDrive == null)
                return;

            int currentDiscNumber = _Options.StartingDiscNumber;
            while (!cancelToken.IsCancellationRequested)
            {
                try
                {
                    // Let's wait for a DVD first
                    await _Access.WaitForDiscDriveAsync(foundDrive.DriveName, cancelToken);
                    if (cancelToken.IsCancellationRequested)
                        return;

                    // Create the Sub-Directory to store the tracks for the disc
                    //var path = _Options.CreateNextDiscPath(currentDiscIndex);
                    var titles = await _Reader.ReadTitleMetadata(foundDrive, cancelToken: cancelToken);
                    if (cancelToken.IsCancellationRequested)
                        return;

                    foreach(var title in titles)
                    {
                        NamingConventionResult convention = _Options.GetNewFileName(title);
                        if (convention == null)
                            continue;

                        string newFileName = convention.FileName;
                        if (newFileName == null)
                            continue;

                        string path = _Options.CreateSubFolder(convention.SubFolder);

                        // Save the title to the directory
                        await _Saver.SaveTitleAsync(title, new SaveTitleOptions { DirectoryPath = path }, cancelToken);
                        if (cancelToken.IsCancellationRequested)
                            return;

                        // Rename the file on completion
                        var oldFilePath = Path.Combine(path, title.OriginalFileName);
                        if (File.Exists(oldFilePath))
                            File.Move(oldFilePath, Path.Combine(path, newFileName));

                        // Run post-rip events
                        title.OriginalFileName = newFileName;
                        TitleRipCompleted?.Invoke(this, new TitleRipEventArgs { Record = title });
                    }

                    currentDiscNumber++;
                } 
                catch (Exception ex)
                {
                    TitleErrorOccurred?.Invoke(this, new TitleRipErrorEventArgs { Exception = ex });
                }
                finally
                {
                    if (!cancelToken.IsCancellationRequested)
                    {
                        // This delay is to allow enough time to close down the connection
                        // to the drive.
                        await Task.Delay(1000, cancelToken);
                    }
                    
                    _Access.EjectDisc(foundDrive.DriveName);
                }

                DiscCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
