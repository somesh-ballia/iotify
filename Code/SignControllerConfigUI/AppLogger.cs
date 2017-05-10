using System;
using System.Collections.Generic;
using ApplicationLogger;
using System.Linq;
using System.Text;

namespace SignController.ConfigurationUI
{
    public class AppLogger
    {
        protected const string BASE_LOG_NAME = "SignControllerUI";
        protected const string PRIMARY_LOG_DEVICE = "Primary";

        /// <summary>
        /// Gets specified logger 
        /// </summary>
        /// <param name="name">Logger name</param>
        /// <returns>Logger</returns>
        public static ILog GetLog(string name)
        {
            return LogManager.GetLogger(BASE_LOG_NAME + "." + name);
        }

        /// <summary>
        /// Configure logger
        /// </summary>
        /// <param name="loggingDirectory">Logging directory</param>
        /// <param name="loggingLevel">Logging level</param>
        /// <param name="maxFileSize">Maximum file size in bytes</param>
        /// <param name="maxFiles">Maximum number of log files</param>
        public static void Configure(string loggingDirectory, int loggingLevel, int maxFileSize, int maxFiles)
        {
            // Setup Primary File Logging Mechanism
            LogManager.SetProperty(LogManager.PropertyNames.LOG_DIRECTORY, loggingDirectory);
            LogManager.SetProperty(LogManager.PropertyNames.LOG_FILENAME, "SignControllerUI");
            LogManager.SetProperty(LogManager.PropertyNames.LOG_EXTENSION, "log");
            LogManager.SetProperty(LogManager.PropertyNames.MAX_FILE_SIZE, maxFileSize.ToString());
            LogManager.SetProperty(LogManager.PropertyNames.MAX_NUMBER_FILES, maxFiles.ToString());
            LogManager.DefineLoggingMechanism(PRIMARY_LOG_DEVICE, "FileLoggingMechanism");

            // Setup base Log properties
            LogManager.SetProperty(BASE_LOG_NAME + "." + LogManager.PropertyNames.LOGGING_MECHANISM_NAMES, PRIMARY_LOG_DEVICE);
            LogManager.SetProperty(BASE_LOG_NAME + "." + LogManager.PropertyNames.LOGGING_LEVEL, loggingLevel.ToString());
        }
    }
}
