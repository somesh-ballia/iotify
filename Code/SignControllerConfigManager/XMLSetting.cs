using System;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SignController.ConfigurationManager
{
    public class XMLSetting
    {
        public XMLSetting()
        {   
            RunTimeXMLPath = DefaultValues.RunTimeXMLPath;
            PollInterval_MS = DefaultValues.PollInterval_MS;
            StagingDir = DefaultValues.StagingDir;
        }

        [XmlElement("RunTimeXMLPath")]
        public string RunTimeXMLPath { get; set; }

        [XmlElement("PollInterval_MS")]
        public long PollInterval_MS { get; set; }

        [XmlElement("StagingDir")]
        public string StagingDir { get; set; }
    }
}
