using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ApplicationLogger;
using System.Threading;

namespace SignController
{
    public class RuntimeXMLHandler
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILog m_log;

        private FileInfo m_fiRuntimeXML;
        private const int MAX_FILE_OPEN_RETRIES = 50;
        private const int MAX_FILE_COPY_RETRIES = 5;
        private const int RETRY_SLEEP_TIME = 500; // .5 seconds
        private string m_StaginingDir = "";


        public RuntimeXMLHandler(FileInfo fiRuntimeXML,string strStaginingDir)
        {
            if (fiRuntimeXML.Exists)
            {
                m_fiRuntimeXML = fiRuntimeXML;
                m_log = AppLogger.GetLog("xx_RuntimeXMLHandler");
                m_StaginingDir = strStaginingDir;
            }
            else
            {
                throw new Exception("Runtime XML File not found at path :" + fiRuntimeXML.FullName);
            }
        }

        public int GetValue(string resourceName)
        {
            int retValue = -1;

            string tmpPath = m_StaginingDir + "\\" + m_fiRuntimeXML.Name;
            CopyFile(m_fiRuntimeXML, m_StaginingDir);
            bool bMarkerFound = false;
            
            try
            {
                foreach (var line in File.ReadAllLines(tmpPath))
                {
                    if (line.Contains("<Name>") && line.Contains(resourceName) && line.Contains("</Name"))
                    {
                        bMarkerFound = true;
                    }

                    if (bMarkerFound)
                    {
                        if (line.Contains("Free"))
                        {
                            line.Trim();
                            int iStart = line.IndexOf('>');
                            int iEnd = line.LastIndexOf('<');
                            if ((iEnd - iStart) >= 2)
                            {
                                iStart++;
                                string val = line.Substring(iStart, (iEnd - iStart));
                                retValue = Convert.ToInt32(val);
                                break;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                string Message = "Error Ocoured while Parsing the XML file, Message :" + ex.Message;
                m_log.Error(Message, 0, ex.StackTrace);
                throw new Exception(Message);
            }

            try
            {
                File.Delete(tmpPath);
                m_log.Information("Deleted temp file :" + tmpPath,0,null);
            }
            catch (System.Exception)
            {
            }

            return retValue;
        }

        /// <summary>
        /// Copy file to destination directory
        /// </summary>
        /// <param name="fi">FileInfo object</param>
        /// <param name="destFolder">Destination directory</param>
        void CopyFile(FileInfo fi, string destFolder)
        {
            string destPath = destFolder + "\\" + fi.Name;
            int retries = 0;
            bool success = false;
            Exception fileException = null;
            while (retries++ < MAX_FILE_COPY_RETRIES)
            {
                try
                {
                    fi.CopyTo(destPath, true);
                    m_log.Information("Successfully copied " + destPath, 0, null);
                    success = true;
                    break;
                }
                catch (Exception ex)
                {
                    // if error then re-attempt after sleeping
                    fileException = ex;
                    Thread.Sleep(RETRY_SLEEP_TIME);
                }
            }
            if (!success)
            {
                // give up after N failures
                m_log.Error("Could not copy file to " + destPath + "; " + fileException.Message, 0, null);
                throw new Exception("Could not copy file to " + destPath, fileException);
            }
        }
    }
}
