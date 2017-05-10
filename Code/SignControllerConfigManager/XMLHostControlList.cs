using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SignController.ConfigurationManager
{
    public class XMLHostControlList
    {
        public XMLHostControlList()
        {
            HostControl = new List<XMLHostControl>();
        }

        [XmlElement("HostControl")]
        public List<XMLHostControl> HostControl { get; set; }
    }
}
