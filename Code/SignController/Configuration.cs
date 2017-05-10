using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using SignController.ConfigurationManager;

namespace SignController
{
    public class Configuration
    {
        private FileInfo ConfigurationFile = null;

        /// <summary>
        /// Configuration definitions XMLGEEConfiguration
        /// </summary>
        private XMLSOIPConfiguration m_XMLSOIPConfiguration = null;

        public XMLSOIPConfiguration XMLSOIPConfiguration 
        {
            get
            {
                return m_XMLSOIPConfiguration;
            }

            protected set
            {
                m_XMLSOIPConfiguration = value;
            }
        }

        public Configuration(FileInfo fiConfigurationFile)
        {
            if (!fiConfigurationFile.Exists)
                throw new Exception(fiConfigurationFile.FullName + " initialization file not found");

            ConfigurationFile = fiConfigurationFile;
            LoadLoggingConfiguration();
        }

        private void LoadLoggingConfiguration()
        {
            try
            {
                XMLSOIPConfiguration = new XMLSOIPConfiguration();
                XMLSOIPConfiguration.DeSerialize(ConfigurationFile);
                XMLSOIPConfiguration.Vaidate();                
            }
            catch (Exception ex)
            {
                throw (new Exception("Exception occurred in Reading Configuration File. Exception: "+ ex));
            }
        }
    }
}
