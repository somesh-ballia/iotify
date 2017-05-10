using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogger
{
    public interface ILog
    {
        /// <summary>
        /// Log Information level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Information(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Error level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Error(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Wrning level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Warning(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Detail level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Detail(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Trace level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Trace(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Fatal level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Fatal(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log Always level message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void Always(string message, int errorCode, string stackTrace);

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="level">Loging level</param>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        void LogEntry(int level, string message, int errorCode, string stackTrace);

        /// <summary>
        /// Gets/Sets max logging level
        /// </summary>
        int LoggingLevel
        {
            get;
            set;
        }
    }
}
