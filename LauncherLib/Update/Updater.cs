using LauncherLib.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LauncherLib.Net;

namespace LauncherLib.Update
{
    public class Updater : IDisposable
    {
        private bool _disposed;

        private readonly WebClient _client = new WebClient();

        public event EventHandler<FileManifestEventArgs> FileDownloaded;

        protected virtual void OnFileDownloaded(object sender, FileManifestEventArgs e)
        {
            FileDownloaded?.Invoke(sender, e);
        }

        public event EventHandler<DownloadProgressChangedEventArgs> FileDownloadProgressChanged;

        protected virtual void OnFileDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            FileDownloadProgressChanged?.Invoke(sender, e);
        }

        public event EventHandler<FileManifestEventArgs> FileStartDownload;

        protected virtual void OnFileStartDownload(object sender, FileManifestEventArgs e)
        {
            FileStartDownload?.Invoke(sender, e);
        }

        /// <summary>
        ///     The download path where the files from
        ///     the file manifest will be downloaded
        /// </summary>
        private readonly string _downloadPath;

        private readonly Uri _downloadApi;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Updater"/> class.
        /// </summary>
        public Updater(Uri downloadApi, string downloadPath)
        {
            _downloadApi = downloadApi;
            _downloadPath = downloadPath;
        }

        /// <summary>
        ///     Destructs <see cref="Updater"/>
        /// </summary>
        ~Updater()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Patches all game files
        /// </summary>
        public async Task PatchFiles()
        {
            // Get manifest 
            var manifest = await Http.GetFileManifest(_downloadApi);

            // Make sure manifest is not null
            if (manifest == null)
            {
                throw new NullFileManifestException("File manifest was null.");
            }

            // Get the files that haven't been patched yet
            var unpatchedFiles = await GetUnpatchedFiles(manifest);


            // Download the unpatched files
            await DownloadUnpatchedFiles(unpatchedFiles);

        }

        /// <summary>
        ///     Gets all unpatched files from a file collection.
        /// </summary>
        /// <param name="collection">The collection of files</param>
        /// <returns>A <see cref="IFileManifestCollection"/> with unpatched files</returns>
        private async Task<IFileManifestCollection> GetUnpatchedFiles(IFileManifestCollection collection)
        {
            var unpatched = new List<FileManifest>();

            Parallel.ForEach(collection.Files,
                new ParallelOptions() { MaxDegreeOfParallelism = (int)Math.Round(Environment.ProcessorCount / 2.0) },
                file =>
                {
                    string path = GetCorrectFilePath(file.Filename);

                    // First check if the file exists
                    // then check if the hashes are the same
                    if (File.Exists(path) && Cryptography.CalculateSha256(path) == file.Sha256)
                    {
                    }
                    else
                    {
                        unpatched.Add(file);
                    }
                });

            return new FileManifestCollection(unpatched.ToArray());
        }

        /// <summary>
        ///     Downloads all unpatched files
        /// </summary>
        /// <param name="collection">The file manifest collection</param>
        private async Task DownloadUnpatchedFiles(IFileManifestCollection collection)
        {
            // Make sure download folder is created
            Directory.CreateDirectory(Path.Combine(_downloadPath, ""));
            Directory.CreateDirectory(Path.Combine(_downloadPath, "config"));
            Directory.CreateDirectory(Path.Combine(_downloadPath, "resources", "default"));

            foreach (var file in collection.Files)
            {
                // Download started
                OnFileStartDownload(this, new FileManifestEventArgs(file));

                // Begin downloading
                await DownloadFile(file);

                // Download finished
                OnFileDownloaded(this, new FileManifestEventArgs(file));

                // Delay to give webserver some time to rest
                await Task.Delay(3000);
            }
        }

        /// <summary>
        ///     Downloads a file.
        /// </summary>
        /// <param name="file">The file</param>
        private async Task DownloadFile(FileManifest file)
        {
            // Subscribe to the download events
            _client.DownloadProgressChanged += OnDownloadProgressChanged;

            string path = GetCorrectFilePath(file.Filename);

            // Download file
            await _client.DownloadFileTaskAsync(file.Url, path);
        }

        /// <summary>
        ///     Piggybacks off of the web client downloading
        ///     and forward the event to this class event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnFileDownloadProgressChanged(this, e);
        }

        private string GetCorrectFilePath(string file)
        {
            string name = file.ToLower();
            if (name.Contains("phase"))
            {
                return Path.Combine(_downloadPath, "resources", "default", file);
            }

            if (name.EndsWith(".dc"))
            {
                return Path.Combine(_downloadPath, "config", file);
            }

            return Path.Combine(_downloadPath, file);
        }

        /// <summary>
        ///     Frees, releases, and resets unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Dispose of unmanaged resources
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free managed objects
                if (_client != null)
                {
                    // Would of used null propogation,
                    // but code analysis thinks I'm not
                    // properly disposing _client
                    _client.Dispose();
                }
            }

            // Free any unmanaged objects

            _disposed = true;
        }
    }
}
