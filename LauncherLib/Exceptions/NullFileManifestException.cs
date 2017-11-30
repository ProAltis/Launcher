using System;
using System.Runtime.Serialization;

namespace LauncherLib.Exceptions
{
    [Serializable]
    public class NullFileManifestException : NullReferenceException
    {
        public NullFileManifestException()
        {
        }

        public NullFileManifestException(string message) : base(message)
        {
        }

        public NullFileManifestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullFileManifestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
