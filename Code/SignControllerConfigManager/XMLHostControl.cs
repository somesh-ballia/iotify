using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SignController.ConfigurationManager
{
    public class XMLHostControl
    {
        public XMLHostControl()
        {
            Enabled = true;
            Name = "";
            IP = "";
            Port = 0;
            SerialAddress = 0;
        }

        [XmlElement("Enabled")]
        public bool Enabled { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IP")]
        public string IP { get; set; }

        [XmlElement("Port")]
        public int Port { get; set; }

        [XmlElement("SerialAddress")]
        public int SerialAddress { get; set; }
    }
}
