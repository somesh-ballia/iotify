using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using ApplicationLogger;
using SignController.ConfigurationManager;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SignController
{
    public class SerialOverIPMain
    {
        /// <summary>
        /// Log
        /// </summary>
        protected ILog m_log;

        /// <summary>
        /// Service configuration data
        /// </summary>
        protected Configuration m_configuration;

        /// <summary>
        /// Thread list
        /// </summary>
        private List<SignControllerWorker> m_workerList = new List<SignControllerWorker>();

        MemoryMappedFile m_MemoryMapFile = null;
    
        /// <summary>
        /// Returns Configuration File Path
        /// </summary>
        /// <returns>FileInfo</returns>
        private FileInfo GetConfigurationFile()
        {
            FileInfo configFile = new FileInfo("C:\\Windows\\XMLSOIPConfiguration.xml");
            if (!configFile.Exists)
                throw (new Exception("Configuration file not found on C:\\Windows\\XMLSOIPConfiguration.xml"));
            return configFile;
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        /// <returns>0 on success, non-0 if an error occurs</returns>
        public int Start()
        {
            // read logging configuration information
            try
            {
                m_configuration = new Configuration(GetConfigurationFile());
            }
            catch (Exception e)
            {
                // Logging has not been configured yet, so write error to event log
                System.Diagnostics.EventLog.WriteEntry("SerialOverIP", e.Message, 
                    System.Diagnostics.EventLogEntryType.Error);
                return -1;
            }

            // initialize logger
            AppLogger.Configure(m_configuration.XMLSOIPConfiguration.Logging.Directory,
                m_configuration.XMLSOIPConfiguration.Logging.LogLevel,
                (int)m_configuration.XMLSOIPConfiguration.Logging.LogMaxSize,
                m_configuration.XMLSOIPConfiguration.Logging.LogMaxFiles);

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            m_log = AppLogger.GetLog("Service");
            m_log.Always("main thread: Starting Serial Over IP " + version.ToString(), 0, null);

            string MemoryMapFileName = m_configuration.XMLSOIPConfiguration.Setting.StagingDir + "\\memoryMapfile.txt";
            try
            {
                MemoryMappedFileSecurity mSec = new MemoryMappedFileSecurity();
                mSec.AddAccessRule(new AccessRule<MemoryMappedFileRights>(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                    MemoryMappedFileRights.FullControl, AccessControlType.Allow));

                m_MemoryMapFile = MemoryMappedFile.CreateOrOpen("Global\\SerialOverIP", 104857600, MemoryMappedFileAccess.ReadWriteExecute,
                    MemoryMappedFileOptions.None, mSec, HandleInheritability.None);
                m_log.Always("Created Memory Map File", 0, null);

                SignControllerWorker worker = new SignControllerWorker(m_configuration.XMLSOIPConfiguration.HostControlList,
                                                                    m_configuration.XMLSOIPConfiguration.Setting.RunTimeXMLPath,
                                                                    m_configuration.XMLSOIPConfiguration.Setting.PollInterval_MS,
                                                                    m_configuration.XMLSOIPConfiguration.Setting.StagingDir,
                                                                    m_MemoryMapFile);

                System.IO.Directory.CreateDirectory(m_configuration.XMLSOIPConfiguration.Setting.StagingDir);

                Thread workerThread = new Thread(worker.Run);
                m_log.Information("main thread: Trying to Start worker thread for worker " + worker.Id, 0, null);
                // start worker thread
                workerThread.Start();
                m_log.Information("main thread: Starting worker thread for worker " + worker.Id, 0, null);

                // loop until thread activates
                while (!workerThread.IsAlive) ;
                // put main thread to sleep
                Thread.Sleep(10);
                // store thread in worker and add worker to list for stop
                worker.WorkerThread = workerThread;
                m_workerList.Add(worker);
            }
            catch (System.Exception ex)
            {
                m_log.Fatal("Exception occurred while Starting Exception : " + ex.Message, 1, ex.StackTrace);
            }

            return 0;
        }

        /// <summary>
        /// Stops the service
        /// </summary>
        public void Stop()
        {
            m_log.Always("Shutting down Service", 0, null);

            try
            {
                if (null != m_MemoryMapFile)
                {
                    m_MemoryMapFile.Dispose();
                    m_log.Always("Destroyed Memory Map File", 0, null);
                }
            }
            catch (System.Exception ex)
            {
                m_log.Fatal("Exception occurred Disposing Memory Mapped File Exception : " + ex.Message, 1, ex.StackTrace);
            }
            
            foreach (SignControllerWorker worker in m_workerList)
            {
                try
                {
                    m_log.Information("main thread: Trying to Stop worker thread for worker " + worker.Id, 0, null);
                    // request stop
                    worker.Stop();
                    m_log.Information("main thread: Marking STOP " + worker.Id, 0, null);
                    // block current thread until worker thread terminates
                    
                    if(worker.WorkerThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                         m_log.Information("main thread: Aborting! Thread Sleeping :" + worker.Id, 0, null);
                         worker.WorkerThread.Abort();
                    }
                    else
                    {
                        m_log.Information("main thread: Waiting! Thread Sleeping :" + worker.Id, 0, null);
                        worker.WorkerThread.Join();
                    }

                    m_log.Information("main thread: Worker thread has terminated for worker " + worker.Id, 0, null);
                }
                catch (Exception ex)
                {
                    m_log.Fatal("Exception occurred while stopping tool connect, Info : " + worker.Id + " Exception : " + ex.Message, 1, ex.StackTrace);
                }
            }
                       
            try
            {                
                LogManager.Shutdown();
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("SerialOverIP", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }
    }
}
