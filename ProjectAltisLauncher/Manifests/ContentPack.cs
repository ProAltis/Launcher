// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global
using System.Collections.Generic;

namespace ProjectAltis.Manifests
{
    public class ContentPackResponse
    {
        public bool valid { get; set; }

        public List<ContentPackDetail> data { get; set; }
    }
    
    
    public class ContentPackDetail
    {
        public string name { get; set; }

        public string author { get; set; }
        
        public string description { get; set; }

        public string filename { get; set; }

        public List<string> screenshots { get; set; }

        public List<ContentPackVersion> versions { get; set; }
    }

    public class ContentPackVersion
    {
        public int version { get; set; }

        public string displayversion { get; set; }

        public string updates { get; set; }

        public string size { get; set; }

        public string download { get; set; }
    }
}