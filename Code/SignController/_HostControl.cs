using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SignController.ConfigurationManager;

namespace SignController
{
    public class _HostControl
    {
        public _HostControl()
        {
            Enabled = true;
            Name = "";
            IP = "";
            Port = -1;
            SerialAddress = -1;
        }

        public _HostControl(bool _Enabled, String _Name, string _IP, int _Port, int _SerialAddress)
        {
            Enabled = _Enabled;
            Name = _Name;
            IP = _IP;
            Port = _Port;
            SerialAddress = _SerialAddress;
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public int SerialAddress { get; set; }
    }
}
