using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignController.ConfigurationManager
{
    public class DefaultValues
    {
        #region XMLGEEConfiguration

        public const int FileVersion = 1;

        #endregion

        #region XMLLogging

        public const string Directory = "";
        public const int LogLevel = 6;
        public const long LogMaxSize = 10000000;
        public const int LogMaxFiles = 20;

        #endregion

        #region XMLSetting

        public const string HostControlPath = "";
        public const string RunTimeXMLPath = "";
        public const long PollInterval_MS = 10000;
        public const string StagingDir = "";
        
        #endregion
    }
}
