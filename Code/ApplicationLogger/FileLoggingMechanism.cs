using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ApplicationLogger
{
    internal class FileLoggingMechanism : LoggingMechanism
    {
        private string m_directoryPath;
        private string m_baseFilename;
        private string m_extension;
        private long m_maxFileSize;
        private int m_maxNumberFiles;
        private FileStream m_stream = null;
        private StreamWriter m_writer = null;
        private object m_fileSync = new object();
        private bool m_started = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public FileLoggingMechanism(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Writes entry in log
        /// </summary>
        /// <param name="entry">Log entry</param>
        protected override void WriteToLog(LoggingMechanism.LogEntry entry)
        {
            if (m_writer != null)
            {
                lock (m_fileSync)
                {
                    m_writer.WriteLine(entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" +
                        entry.LogName + "\t" +
                        entry.LevelName + "\t" +
                        entry.Thread + "\t" +
                        entry.ErrorCode.ToString() + "\t" +
                        entry.Message + "\t" +
                        entry.StackTrace);
                    m_writer.Flush();
                    RollFileIfNeeded();
                }
            }
        }

        private void RollFileIfNeeded()
        {
            try
            {
                if ((m_maxFileSize > 0) && (m_stream.Length > m_maxFileSize))
                {
                    CloseFile();
                    OpenFile();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Opens log mechanism
        /// </summary>
        protected override void OpenLogMechanism()
        {
            m_directoryPath = LogManager.GetPropertyHierarchy(Name, LogManager.PropertyNames.LOG_DIRECTORY,
                "C:\\ProgramData\\KLA-Tencor\\");
            if (!m_directoryPath.EndsWith("/") && !m_directoryPath.EndsWith("\\"))
                m_directoryPath += "\\";
            m_baseFilename = LogManager.GetPropertyHierarchy(Name, LogManager.PropertyNames.LOG_FILENAME, "Application");
            m_extension = LogManager.GetPropertyHierarchy(Name, LogManager.PropertyNames.LOG_EXTENSION, "log");
            string val = LogManager.GetPropertyHierarchy(Name, LogManager.PropertyNames.MAX_FILE_SIZE, "10000000");
            if (!long.TryParse(val, out m_maxFileSize))
                throw new ArgumentException("The property " + LogManager.PropertyNames.MAX_FILE_SIZE + " must be an integer value");
            val = LogManager.GetPropertyHierarchy(Name, LogManager.PropertyNames.MAX_NUMBER_FILES, "10");
            if (!int.TryParse(val, out m_maxNumberFiles))
                throw new ArgumentException("The property " + LogManager.PropertyNames.MAX_NUMBER_FILES + " must be an integer value");

            lock (m_fileSync)
            {
                if (!m_started)
                {
                    if (!Directory.Exists(m_directoryPath))
                        Directory.CreateDirectory(m_directoryPath);
                    OpenFile();
                    m_started = true;
                }
            }
        }

        private void OpenFile()
        {
            string fullFilename = m_directoryPath + m_baseFilename + "." + m_extension;
            if (File.Exists(fullFilename))
            {
                DateTime rolloverDate = DateTime.Now;
                string newName = m_directoryPath + m_baseFilename + rolloverDate.ToString("_yyyyMMdd_HHmmss") + "." + m_extension;
                try
                {
                    File.Move(fullFilename, newName);
                }
                catch (Exception)
                {
                }
            }
            if (m_maxNumberFiles > 0)
            {
                try
                {
                    string[] matchingFiles = Directory.GetFiles(m_directoryPath, m_baseFilename + "_*." + m_extension);
                    if (m_maxNumberFiles < matchingFiles.Length)
                    {
                        int deleteCount = matchingFiles.Length - m_maxNumberFiles;
                        Array.Sort(matchingFiles);
                        for (int i = 0; i < deleteCount; ++i)
                            File.Delete(matchingFiles[i]);
                    }
                }
                catch (Exception) { }
            }
            m_stream = new FileStream(fullFilename, FileMode.Append);
            m_writer = new StreamWriter(m_stream);
        }

        private void CloseFile()
        {
            try
            {
                if (m_writer != null)
                    m_writer.Close();
                if (m_stream != null)
                    m_stream.Close();
            }
            catch (Exception)
            {
            }
            m_writer = null;
            m_stream = null;
        }

        /// <summary>
        /// Close log mechanism
        /// </summary>
        protected override void CloseLogMechanism()
        {
            lock (m_fileSync)
            {
                CloseFile();
                m_started = false;
            }
        }
    }
}
