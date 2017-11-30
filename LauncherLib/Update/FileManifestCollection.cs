using System.Collections;
using Newtonsoft.Json;

namespace LauncherLib.Update
{
    /// <summary>
    ///     Represents a collection of files with metadata
    /// </summary>
    public class FileManifestCollection : IFileManifestCollection
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestCollection"/> class.
        /// </summary>
        /// <param name="rawManifest">The raw manifest retrieved from the api</param>
        public FileManifestCollection(string rawManifest)
        {
            // Split the array to get each individual line
            string[] rawManifestArray = rawManifest.Split('#');

            // Subtract 1 because last value of the split is always empty
            int totalElements = rawManifestArray.Length - 1;

            // Create a new array to hold FileManifest objects.
            this.Files = new FileManifest[totalElements];

            // Iterate through the individual lines of the manifest
            // and deserialize each line into the Files array
            for (int i = 0; i < totalElements; i++)
            {
                Files[i] = JsonConvert.DeserializeObject<FileManifest>(rawManifestArray[i]);
            }
        }

        /// <summary>
        ///     Initializes a new instace of the <see cref="FileManifestCollection"/> class.
        /// </summary>
        /// <param name="files">The files</param>
        public FileManifestCollection(FileManifest[] files)
        {
            Files = files;
        }

        /// <summary>
        ///     The files
        /// </summary>
        public FileManifest[] Files { get; }

        public IEnumerator GetEnumerator() => Files.GetEnumerator();

    }
}
