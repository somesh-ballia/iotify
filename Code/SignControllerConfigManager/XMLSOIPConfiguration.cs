using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SignController.ConfigurationManager
{
    public class XMLSOIPConfiguration
    {
        public XMLSOIPConfiguration()
        {
            FileVersion = DefaultValues.FileVersion;
            Logging = new XMLLogging();
            Setting = new XMLSetting();
            HostControlList = new XMLHostControlList();
        }

        [XmlElement("FileVersion")]
        public int FileVersion { get; set; }
        public XMLLogging Logging { get; set; }
        public XMLSetting Setting { get; set; }
        public XMLHostControlList HostControlList { get; set; }

        [XmlIgnore]
        public bool Initailized = false;

        private void SelfPopulate(XMLSOIPConfiguration obj)
        {
            FileVersion = obj.FileVersion;
            Logging = obj.Logging;
            Setting = obj.Setting;
            HostControlList = obj.HostControlList;
            Initailized = true;
        }

        public void DeSerialize(FileInfo FIEmailFile)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(XMLSOIPConfiguration));
            TextReader reader = new StreamReader(FIEmailFile.FullName);
            object obj = deserializer.Deserialize(reader);
            SelfPopulate((XMLSOIPConfiguration)obj);
            reader.Close();
        }

        public void Serialize(FileInfo FIEmailFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLSOIPConfiguration));
            using (TextWriter writer = new StreamWriter(FIEmailFile.FullName))
            {
                serializer.Serialize(writer, this);
            }
        }

        public void Vaidate()      // ToDo
        {
            if (FileVersion != DefaultValues.FileVersion) { throw (new Exception("")); }
            
            // logging
            if (0 == Logging.Directory.Length) { throw (new Exception("No Logging Directory Path Available")); }
            if (0 == Logging.LogMaxSize) { throw (new Exception("Log Max Size Can Not Be 0")); }
            if (0 == Logging.LogMaxFiles) { throw (new Exception("Log Max File Count Can Not Be 0")); }
            if (0 == Setting.RunTimeXMLPath.Length) { throw (new Exception("No RunTime XML Path Available")); }
        }
    }
}
