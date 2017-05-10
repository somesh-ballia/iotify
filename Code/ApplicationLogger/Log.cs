using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogger
{
    public class Log : ILog
    {
        private string m_name;

        private int m_loggingLevel = LogManager.LogLevels.TRACE;

        private LoggingMechanism[] m_loggingMechanisms = null;

        /// <summary>
        /// Gets/Sets Log name
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        internal Log(string name)
        {
            m_name = name;
            Reset();
        }

        internal void Reset()
        {
            string val = LogManager.GetPropertyHierarchy(m_name, LogManager.PropertyNames.LOGGING_MECHANISM_NAMES, null);
            string[] loggingMechanismNames = (val == null) ? new string[0] : val.Split(new char[] { ',' });
            m_loggingMechanisms = LogManager.GetLoggingMechanisms(loggingMechanismNames);
            val = LogManager.GetPropertyHierarchy(m_name, LogManager.PropertyNames.LOGGING_LEVEL, LogManager.LogLevels.TRACE.ToString());
            if (!int.TryParse(val, out m_loggingLevel))
                throw new ArgumentException("The value of property " + LogManager.PropertyNames.LOGGING_LEVEL + " must be an integer");
        }

        /// <summary>
        /// Log Information level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Information(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.INFORMATION, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Error level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Error(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.ERROR, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Warning level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Warning(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.WARNING, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Detail level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Detail(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.DETAIL, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Trace level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Trace(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.TRACE, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Fatal level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Fatal(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.FATAL, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log Always level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void Always(string message, int errorCode, string stackTrace)
        {
            LogEntry(LogManager.LogLevels.ALWAYS, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="level">Loging level</param>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void LogEntry(int level, string message, int errorCode, string stackTrace)
        {
            if (level <= m_loggingLevel)
                foreach (LoggingMechanism mechanism in m_loggingMechanisms)
                    mechanism.LogMessage(level, m_name, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Gets/Sets max logging level
        /// </summary>
        public int LoggingLevel
        {
            get { return m_loggingLevel; }
            set
            {
                if (m_loggingLevel <= LogManager.LogLevels.ALWAYS || m_loggingLevel > LogManager.LogLevels.TRACE)
                    throw new ArgumentException("Logging level must be greater than " + LogManager.LogLevels.ALWAYS +
                        " and less or equal to " + LogManager.LogLevels.TRACE);
                m_loggingLevel = value;
            }
        }
    }
}
