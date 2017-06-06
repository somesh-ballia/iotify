using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using ApplicationLogger;
using SignController.ConfigurationManager;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace SignController
{
    public class TempStorage
    {
        private int m_iValue;
        private string m_StatusMessage;

        public TempStorage(int iValue, String statusMessage)
        {
            m_iValue = iValue;
            m_StatusMessage = statusMessage;
        }

        public String toString(String Name)
        {
            return Name + "," + m_iValue + "," + m_StatusMessage + "|";
        }
    }

    class SignControllerWorker
    {
        /// <summary>
        /// Logger
        /// </summary>
        private ILog m_log;

        /// <summary>
        /// Thread associated with this worker instance
        /// </summary>
        private Thread m_thread;

        /// <summary>
        /// Flag indicating if we should stop processing
        /// </summary>
        private bool m_shouldStop = false;

        private string m_StagingPath;

        object objSyncObj = new object();

        Dictionary<string, TempStorage> m_valueDicionary = new Dictionary<string, TempStorage>();
        
        private string m_id;
        private string m_RuntimeXML;
        private long m_PollInterval;
        List<_HostControl> m_listHostControl = new List<_HostControl>();
        RuntimeXMLHandler m_xmlHandler;
        MemoryMappedFile m_MemoryMapFile = null;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">Tool Connect configuration</param>
        public SignControllerWorker(ConfigurationManager.XMLHostControlList hostControlList,
                                    String RuntimeXML,long PollInterval,string stagingPath,
                                    MemoryMappedFile MemoryMapFile)
        {   
            m_RuntimeXML = RuntimeXML;
            m_StagingPath = stagingPath;
            m_PollInterval = PollInterval;
            m_id = Guid.NewGuid().ToString();
            m_log = AppLogger.GetLog("SignControllerWorker");
            m_MemoryMapFile = MemoryMapFile;

            FileInfo fiRunTimeXML = new FileInfo(RuntimeXML);
            if (fiRunTimeXML.Exists)
            {
                if (0 < hostControlList.HostControl.Count)
                {
                    m_xmlHandler = new RuntimeXMLHandler(fiRunTimeXML,stagingPath);
                    foreach (XMLHostControl xmlHostCtrl in hostControlList.HostControl)
                    { 
                        _HostControl ctrl = new _HostControl(xmlHostCtrl.Enabled,xmlHostCtrl.Name,
                            xmlHostCtrl.IP,xmlHostCtrl.Port,xmlHostCtrl.SerialAddress);
                        m_listHostControl.Add(ctrl);
                    }
                }
                else
                {
                    throw new Exception("No Host Control Entry Found");
                }
            }
            else
            {
                throw new Exception("Runtime XML not found on path :" + RuntimeXML);
            }
        }

        /// <summary>
        /// Gets/sets the Thread associated with this object
        /// </summary>
        public Thread WorkerThread
        {
            get { return m_thread; }
            set { m_thread = value; }
        }

        /// <summary>
        /// Gets the worker Id
        /// </summary>
        public string Id
        {
            get { return m_id; }
        }

        /// <summary>
        /// Run method
        /// </summary>
        public void Run()
        {
            m_log.Information("Starting Run()...", 0, null);
            try
            {  
                bool stop = false;
                while (!stop)
                {
                    for (int iCount = 0; (iCount < m_listHostControl.Count) && (!stop); iCount++)
                    {
                        string StatusMessage = "";
                        _HostControl control = null;
                        int iValue = -1;

                        try
                        {
                            control = m_listHostControl[iCount];
                            if (control.Enabled)
                            {
                                m_log.Information("Fetching Value For :" + control.Name, 0, null);
                                iValue = m_xmlHandler.GetValue(control.Name);
                                m_log.Information("Fetching Complete For :" + control.Name, 0, null);
                            }
                            else
                            {
                                m_log.Information("Blank Mode set for :" + control.Name, 0, null);
                                iValue = 10003; // code for blank mode
                            }
                                                      
                            if (-1 != iValue)
                            {
                                m_log.Information("Creating Socket for  :" + control.Name, 0, null);
                                SocketHandler socketHandler = new SocketHandler(control, iValue);
                                if (socketHandler.SendData(out StatusMessage))
                                {   
                                    m_log.Information("Data Sent Successfully for Name :" + control.Name, 0, null);
                                }
                                else
                                {
                                    m_log.Information("Failure Sending Data for for Name :" + control.Name, 0, null);
                                }
                            }
                            else
                            {
                                m_log.Error("No Value Found in the RuntimeXML for Name :" + control.Name, 0, null);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            string msg = "Exception Ocoured while processing, Message : " + ex.Message;
                            m_log.Error(msg, 0, null);
                        }

                        if (null != control)
                        {
                            TempStorage stroage = new TempStorage(iValue, StatusMessage);
                            if (m_valueDicionary.ContainsKey(control.Name))
                            {
                                m_valueDicionary.Remove(control.Name);
                            }
                            m_valueDicionary.Add(control.Name, stroage);
                            UpdateSharedMemory();
                        }

                        lock (objSyncObj)
                        {
                            if (m_shouldStop)
                            {
                                stop = true;
                            }
                        }
                        
                        if (!stop)
                        {
                            Thread.Sleep((int)m_PollInterval);
                        }

                        if (iCount == (m_listHostControl.Count - 1))
                            iCount = -1;
                    }
                }
            }
            catch (System.Exception ex)
            {
                string msg = "Exiting thread " + m_id + " due to error; " + ex.Message;
                m_log.Error(msg, 0, null);
            }
        }

        /// <summary>
        /// Stop method
        /// </summary>
        public void Stop()
        {
            try
            {
                lock (objSyncObj)
                {
                    m_shouldStop = true;
                }                
            }
            catch (Exception ex)
            {
                m_log.Fatal("Exception occurred while executing STOP of worker thread, Exception :" + ex.Message, 1, ex.StackTrace);
            }
        }

        void UpdateSharedMemory()
        {
            string serialized = "";
            foreach (KeyValuePair<string, TempStorage> pair in m_valueDicionary)
            {
                TempStorage tempStore = pair.Value;
                serialized += tempStore.toString(pair.Key);
            }

            try
            {   
                // Create the memory-mapped file.
                using (MemoryMappedViewAccessor accessor = m_MemoryMapFile.CreateViewAccessor())
                {
                    m_log.Information("Writing Info to File:" + serialized, 1, null);
                    byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(serialized);
                    accessor.Write(54, Buffer.Length);
                    accessor.WriteArray(54 + 4, Buffer, 0, Buffer.Length);
                    m_log.Information("Write Complete", 1, null);
                }
            }
            catch (System.Exception ex)
            {
                m_log.Fatal("Exception occurred while writing memory mapped file, Exception :" + ex.Message, 1, ex.StackTrace);
            }
        }
    }
}
