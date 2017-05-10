using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ApplicationLogger
{
    public class LogManager
    {
        public class LogLevels
        {
            /// <summary>
            /// Logging level for a message that is always written to the log.  Logger logging level cannot be set to this value.
            /// </summary>
            public const int ALWAYS = 0;

            /// <summary>
            /// Logging level for a message that represents a fatal error that will prevent further processing
            /// </summary>
            public const int FATAL = 1;

            /// <summary>
            /// Logging level for errors or failures detected
            /// </summary>
            public const int ERROR = 2;

            /// <summary>
            /// Logging level for warnings or conditions that may be incorrect
            /// </summary>
            public const int WARNING = 3;

            /// <summary>
            /// Logging level for general information.  Logging at this level should not generate a large volume of log entries.
            /// </summary>
            public const int INFORMATION = 4;

            /// <summary>
            /// Logging of detail information that aids in understanding flow within the application.
            /// </summary>
            public const int DETAIL = 5;

            /// <summary>
            /// Logging of trace or debugging level of information.  This level should be used when a very large number of log entries are generated.
            /// </summary>
            public const int TRACE = 6;
        }

        public class LoggingLevelNames
        {
            /// <summary>
            /// Name of the Always logging level
            /// </summary>
            public const string ALWAYS = "Always";

            /// <summary>
            /// Name of the Fatal logging level
            /// </summary>
            public const string FATAL = "Fatal";

            /// <summary>
            /// Name of the Error level
            /// </summary>
            public const string ERROR = "Error";

            /// <summary>
            /// Name of the Warning level
            /// </summary>
            public const string WARNING = "Warning";

            /// <summary>
            /// Name of the Information level
            /// </summary>
            public const string INFORMATION = "Info";

            /// <summary>
            /// Name of the Detail level
            /// </summary>
            public const string DETAIL = "Detail";

            /// <summary>
            /// Name of the Trace level
            /// </summary>
            public const string TRACE = "Trace";
        }

        /// <summary>
        /// Gets logging level name
        /// </summary>
        /// <param name="loggingLevel">Logging level</param>
        /// <returns>Associated name</returns>
        public static string GetLoggingLevelName(int loggingLevel)
        {
            string levelName;
            switch (loggingLevel)
            {
                case LogLevels.ALWAYS:
                    levelName = LoggingLevelNames.ALWAYS;
                    break;
                case LogLevels.FATAL:
                    levelName = LoggingLevelNames.FATAL;
                    break;
                case LogLevels.ERROR:
                    levelName = LoggingLevelNames.ERROR;
                    break;
                case LogLevels.WARNING:
                    levelName = LoggingLevelNames.WARNING;
                    break;
                case LogLevels.INFORMATION:
                    levelName = LoggingLevelNames.INFORMATION;
                    break;
                case LogLevels.DETAIL:
                    levelName = LoggingLevelNames.DETAIL;
                    break;
                case LogLevels.TRACE:
                    levelName = LoggingLevelNames.TRACE;
                    break;
                default:
                    levelName = loggingLevel.ToString();
                    break;
            }
            return levelName;
        }

        /// <summary>
        /// Gets logging level from name
        /// </summary>
        /// <param name="name">Logging level name</param>
        /// <returns>Associated logging level</returns>
        public static int GetLoggingLevel(string name)
        {
            int loggingLevel;
            switch (name)
            {
                case LoggingLevelNames.ALWAYS:
                    loggingLevel = LogLevels.ALWAYS;
                    break;
                case LoggingLevelNames.FATAL:
                    loggingLevel = LogLevels.FATAL;
                    break;
                case LoggingLevelNames.ERROR:
                    loggingLevel = LogLevels.ERROR;
                    break;
                case LoggingLevelNames.WARNING:
                    loggingLevel = LogLevels.WARNING;
                    break;
                case LoggingLevelNames.INFORMATION:
                    loggingLevel = LogLevels.INFORMATION;
                    break;
                case LoggingLevelNames.DETAIL:
                    loggingLevel = LogLevels.DETAIL;
                    break;
                case LoggingLevelNames.TRACE:
                    loggingLevel = LogLevels.TRACE;
                    break;
                default:
                    loggingLevel = 0;
                    int.TryParse(name, out loggingLevel);
                    break;
            }
            return loggingLevel;
        }

        public abstract class PropertyNames
        {
            public const string LOG_DIRECTORY = "LogDirectory";
            public const string LOG_FILENAME = "LogFilename";
            public const string LOG_EXTENSION = "LogExtension";
            public const string MAX_FILE_SIZE = "MaxFileSize";
            public const string MAX_NUMBER_FILES = "MaxNumberFiles";
            public const string LOGGING_LEVEL = "LoggingLevel";
            public const string LOGGING_MECHANISM_NAMES = "LoggingMechanismNames";
        }

        public static Log[] Logs
        {
            get 
            { 
                Log[] logArray = new Log[m_logs.Values.Count];
                if (m_logs.Values.Count > 0)
                    m_logs.Values.CopyTo(logArray, 0);
                return logArray; 
            }
            set 
            {
                foreach (Log log in value)
                {
                    m_logs[log.Name] = log;
                }
            }
        }

        private static Dictionary<string, Log> m_logs = new Dictionary<string, Log>();

        private static Dictionary<string, LoggingMechanism> m_loggingMechanisms = new Dictionary<string, LoggingMechanism>();

        private static StringDictionary m_properties = new StringDictionary();

        public static void SetProperty(string propertyName, string propertyValue)
        {
            if (propertyValue == null)
                throw new ArgumentException("Property values cannot be null");
            m_properties[propertyName] = propertyValue;
        }

        public static string GetPropertyExplicit(string propertyName, string defaultValue)
        {
            string propertyValue = m_properties[propertyName];
            if (propertyValue == null)
                propertyValue = defaultValue;

            return propertyValue;
        }

        public static string GetPropertyHierarchy(string hierarchy, string propertyName, string defaultValue)
        {
            string propertyValue = null;
            do
            {
                propertyValue = m_properties[hierarchy + "." + propertyName];
                if (propertyValue == null)
                {
                    int dotSeparator = hierarchy.LastIndexOf('.');
                    if (dotSeparator < 0)
                    {
                        propertyValue = GetPropertyExplicit(propertyName, defaultValue);
                        break;
                    }
                    hierarchy = hierarchy.Substring(0, dotSeparator);
                }
            } while (propertyValue == null);

            return propertyValue;
        }

        public static void DefineLoggingMechanism(string name, string typeName)
        {
            LoggingMechanism mechanism = null;
            if (typeName == typeof(FileLoggingMechanism).Name)
                mechanism = new FileLoggingMechanism(name);
            else
            {
                //TODO: Use reflections to create it
            }

            if (mechanism != null)
            {
                lock (m_loggingMechanisms)
                {
                    if (m_loggingMechanisms.ContainsKey(mechanism.Name))
                        throw new ArgumentException("A logging mechanism with the name " + mechanism.Name + " has already been defined.");
                    m_loggingMechanisms[mechanism.Name] = mechanism;
                }
            }
        }

        internal static LoggingMechanism[] GetLoggingMechanisms(string[] loggingMechanismNames)
        {
            LoggingMechanism[] mechanisms = new LoggingMechanism[loggingMechanismNames.Length];
            lock (m_loggingMechanisms)
            {
                for (int mechanismIndex = 0; mechanismIndex < loggingMechanismNames.Length; ++mechanismIndex)
                {
                    LoggingMechanism mechanism = m_loggingMechanisms[loggingMechanismNames[mechanismIndex].Trim()];
                    if (mechanism == null)
                        throw new ArgumentException("No logging mechanism named " + loggingMechanismNames[mechanismIndex] + " exists");
                    mechanism.Start();
                    mechanisms[mechanismIndex] = mechanism;
                }
            }
            return mechanisms;
        }

        public static ILog GetLogger(string repository, string logName)
        {
            return GetLogger(logName);
        }

        public static ILog GetLogger(string logName)
        {
            Log log;
            if (!m_logs.TryGetValue(logName, out log))
            {
                log = new Log(logName);
                m_logs[logName] = log;
            }

            return log;
        }

        public static void Shutdown()
        {
            m_logs.Clear();
            lock (m_loggingMechanisms)
            {
                foreach (LoggingMechanism mechanism in m_loggingMechanisms.Values)
                    mechanism.Stop();
                m_loggingMechanisms.Clear();
            }
        }

        public static void Reset()
        {
            Shutdown();
            m_loggingMechanisms.Clear();
            m_properties.Clear();
        }
    }
}
