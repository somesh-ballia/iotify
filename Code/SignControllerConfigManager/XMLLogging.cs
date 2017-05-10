using System;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SignController.ConfigurationManager
{
    public class XMLLogging
    {
        public XMLLogging()
        {
            LogLevel = DefaultValues.LogLevel;
            LogMaxSize = DefaultValues.LogMaxSize;
            LogMaxFiles = DefaultValues.LogMaxFiles;
            Directory = DefaultValues.Directory;
        }

        [XmlElement("Directory")]
        public string Directory { get; set; }

        [XmlElement("LogLevel")]
        public int LogLevel { get; set; }

        [XmlElement("LogMaxSize")]
        public long LogMaxSize { get; set; }

        [XmlElement("LogMaxFiles")]
        public int LogMaxFiles { get; set; }
    }
}
