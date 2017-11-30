using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.Update
{
    public class FileManifestEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileManifestEventArgs"/> class.
        /// </summary>
        /// <param name="file"></param>
        public FileManifestEventArgs(FileManifest file)
        {
            File = file;
        }

        /// <summary>
        ///     The file manifest
        /// </summary>
        public FileManifest File;
    }
}
