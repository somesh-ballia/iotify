using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ApplicationLogger
{
    public abstract class LoggingMechanism
    {
        protected class LogEntry
        {
            private int m_level;
            private string m_logName;
            private string m_message;
            private int m_errorCode;
            private string m_stackTrace;
            private DateTime m_timestamp;
            private string m_thread;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="level">Logging level</param>
            /// <param name="logName">Log name</param>
            /// <param name="message">Log message</param>
            /// <param name="errorCode">Error code</param>
            /// <param name="stackTrace">Stack trace</param>
            /// <param name="timestamp">Timestamp</param>
            /// <param name="thread">Thead name</param>
            public LogEntry(int level, string logName, string message, int errorCode, string stackTrace, DateTime timestamp, string thread)
            {
                m_level = level;
                m_logName = logName;
                m_message = message;
                m_errorCode = errorCode;
                m_stackTrace = stackTrace;
                m_timestamp = timestamp;
                m_thread = thread;
            }

            /// <summary>
            /// Gets logging level
            /// </summary>
            public int Level
            {
                get { return m_level; }
            }

            /// <summary>
            /// Gets logging level name
            /// </summary>
            public string LevelName
            {
                get { return LogManager.GetLoggingLevelName(m_level); }
            }

            /// <summary>
            /// Gets log name
            /// </summary>
            public string LogName
            {
                get { return m_logName; }
            }

            /// <summary>
            /// Gets log message
            /// </summary>
            public string Message
            {
                get { return m_message; }
            }

            /// <summary>
            /// Gets error code
            /// </summary>
            public int ErrorCode
            {
                get { return m_errorCode; }
            }

            /// <summary>
            /// Gets timestamp
            /// </summary>
            public DateTime Timestamp
            {
                get { return m_timestamp; }
            }

            /// <summary>
            /// Gets thread name
            /// </summary>
            public string Thread
            {
                get { return m_thread; }
            }

            /// <summary>
            /// Gets stack trace
            /// </summary>
            public string StackTrace
            {
                get { return m_stackTrace; }
            }
        }

        private Queue<LogEntry> m_queue = new Queue<LogEntry>();
        protected bool m_stopped = false;
        protected bool m_stopping = false;
        private Thread m_queueThread = null;
        private string m_name;
        //        private int m_logLevelFilter = 0x7fffffff;

        /// <summary>
        /// Sets log mechanism name
        /// </summary>
        /// <param name="name">Log mechanism name</param>
        public LoggingMechanism(string name)
        {
            m_name = name;
        }

        /// <summary>
        /// Gets log mechanism name
        /// </summary>
        public string Name
        {
            get { return m_name; }
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="level">Logging level</param>
        /// <param name="logName">Log name</param>
        /// <param name="message">Log message</param>
        /// <param name="errorCode">Error code</param>
        /// <param name="stackTrace">Stack trace</param>
        public void LogMessage(int level, string logName, string message, int errorCode, string stackTrace)
        {
            if (m_stopping)
                return;

            Thread thread = Thread.CurrentThread;
            LogEntry entry = new LogEntry(level, logName, message, errorCode, stackTrace, DateTime.Now,
                thread.Name + "(" + thread.ManagedThreadId + ")");
            lock (m_queue)
            {
                m_queue.Enqueue(entry);
                Monitor.Pulse(m_queue);
            }
        }

        /// <summary>
        /// Start log mechanism
        /// </summary>
        public void Start()
        {
            lock (m_queue)
            {
                if (m_queueThread == null)
                {
                    m_stopped = false;
                    m_stopping = false;
                    OpenLogMechanism();
                    m_queueThread = new Thread(ProcessQueue);
                    m_queueThread.Name = "LoggingMechanism" + Name;
                    m_queueThread.IsBackground = true;
                    m_queueThread.Start();
                }
            }
        }

        /// <summary>
        /// Stop log mechanism
        /// </summary>
        public void Stop()
        {
            if (!m_stopped)
            {
                lock (m_queue)
                {
                    if (m_queueThread != null)
                    {
                        m_stopping = true;
                        Monitor.Pulse(m_queue);
                        if (!m_stopped)
                            Monitor.Wait(m_queue);
                        m_queueThread = null;
                    }
                    else
                        m_stopped = true;
                }
            }
            CloseLogMechanism();
        }

        private void ProcessQueue()
        {
            while (!m_stopped)
            {
                LogEntry entry = null;
                lock (m_queue)
                {
                    if (m_queue.Count == 0)
                    {
                        if (m_stopping)
                        {
                            m_stopped = true;
                            m_queueThread = null;
                            Monitor.Pulse(m_queue);
                        }
                        else
                            Monitor.Wait(m_queue);
                    }
                    else
                        entry = m_queue.Dequeue();
                }

                if (entry != null)
                    try
                    {
                        WriteToLog(entry);
                    }
                    catch (Exception) { }
            }
        }

        protected abstract void WriteToLog(LogEntry entry);
        protected abstract void OpenLogMechanism();
        protected abstract void CloseLogMechanism();
    }
}
