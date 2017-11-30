using System.Collections;

namespace LauncherLib.Update
{
    public interface IFileManifestCollection : IEnumerable
    {
        /// <summary>
        ///     The files
        /// </summary>
        FileManifest[] Files { get; }
    }
}