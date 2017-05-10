using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using ApplicationLogger;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using SignController.ConfigurationManager;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SignController.ConfigurationUI
{
    public partial class SignControlUI : Form
    {
        private ILog m_log;
        private const string RECIPIENT_CONFIG_TABLE_NAME = "RecipientTable";
        private const string RECIPIENT_CONFIG_DATASET_NAME = "RecipientDataSet";
        private const string SERVICE_NAME = "SignController";
        private bool IsServiceStarted = false;
        private bool IsSmall = false;
        private Dictionary<String, XMLHostControl> m_mapHostControl = new Dictionary<String, XMLHostControl>();
        XMLSOIPConfiguration m_Configuration;

        public SignControlUI()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConfigurationFile();

                // load logger
                try
                {
                    AppLogger.Configure(m_Configuration.Logging.Directory, m_Configuration.Logging.LogLevel,
                        (int)m_Configuration.Logging.LogMaxSize, m_Configuration.Logging.LogMaxFiles);
                    m_log = m_log = AppLogger.GetLog("UI");
                    m_log.Always("Starting Serial Over IP Configuration User Interface", 0, null);
                }
                catch (Exception ex)
                {
                    String Message = "Unable to load logger on the same path as Application Startup Path, Application will exit. Exception :" + ex.Message;
                    MessageBox.Show(Message, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.EventLog.WriteEntry("SingControlUI", Message, System.Diagnostics.EventLogEntryType.Error);
                    Environment.Exit(0);
                }

                LoadServiceInformation();
            }
            catch (System.Exception ex)
            {
                String Message = "Exception Ocoured While Loading the Configuration file. Exception :" + ex.Message;
                MessageBox.Show(Message, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.EventLog.WriteEntry("SingControlUI", Message, System.Diagnostics.EventLogEntryType.Error);
                Environment.Exit(0);
            }
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            bool bContinueSave = true;
            XMLSOIPConfiguration XMLConfiguration = new XMLSOIPConfiguration();
            XMLConfiguration.Logging = m_Configuration.Logging;
            XMLConfiguration.Setting.StagingDir = m_Configuration.Setting.StagingDir;

            try
            {
                if (bContinueSave)
                {
                    if (0 < textBoxRunTimeXMLPath.Text.Length)
                    {
                        FileInfo ifTest = new FileInfo(textBoxRunTimeXMLPath.Text.Trim());
                        if (ifTest.Exists)
                        {
                            XMLConfiguration.Setting.RunTimeXMLPath = textBoxRunTimeXMLPath.Text.Trim();
                        }
                        else
                        {
                            bContinueSave = false;
                            throw new Exception("File Can Not Be Found : " + textBoxRunTimeXMLPath.Text.Trim());
                        }
                    }
                    else
                    {
                        bContinueSave = false;
                        throw new Exception("No Runtime XML Path Found");
                    }
                }

                if (bContinueSave)
                {
                    if (0 < comboBoxTimeInterval.Text.Length)
                    {
                        long lVal = -1;
                        try
                        {
                            lVal = Convert.ToInt32(comboBoxTimeInterval.Text);
                            XMLConfiguration.Setting.PollInterval_MS = (lVal * 1000);
                        }
                        catch (System.Exception)
                        {
                            bContinueSave = false;
                            throw new Exception("Invalid Poll Interval Value Found");
                        }
                    }
                    else
                    {
                        bContinueSave = false;
                        throw new Exception("No Poll Interval Value Found");
                    }
                }

                if (bContinueSave)
                {
                    foreach (string name in m_mapHostControl.Keys)
                    {
                        XMLHostControl ctrl = m_mapHostControl[name];
                        XMLConfiguration.HostControlList.HostControl.Add(ctrl);
                    }
                }

                if (bContinueSave)
                {
                    try
                    {
                        FileInfo fiConfigFile = new FileInfo(textBoxConfigurationFilePath.Text.Trim());
                        XMLConfiguration.Serialize(fiConfigFile);
                        m_Configuration = XMLConfiguration;
                        MessageBox.Show("Configuration Saved Successfully. Please Restart the Service.", "Success");
                    }
                    catch (System.Exception ex)
                    {
                        String logMessage = "Failure Saving the Configuration. Message: : " + ex.Message;
                        m_log.Error(logMessage, 1, ex.StackTrace);
                        MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                String logMessage = "Exception occurred while Saving the Configuration File, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
                MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GEEConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                m_log.Always("Closing Application", 0, null);
            }
            catch (Exception ex)
            {
                String logMessage = "Exception occurred while Closing Application, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
                MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadConfigurationFile()
        {
            FileInfo ficonfigPath = new FileInfo(textBoxConfigurationFilePath.Text.Trim());
            m_Configuration = new XMLSOIPConfiguration();
            m_Configuration.DeSerialize(ficonfigPath);

            if (m_Configuration.Initailized)
            {
                if (0 < m_Configuration.Setting.RunTimeXMLPath.Length)
                {
                    textBoxRunTimeXMLPath.Text = m_Configuration.Setting.RunTimeXMLPath;
                }

                if (0 < m_Configuration.Setting.PollInterval_MS)
                {
                    long Value = (long)Math.Ceiling(((double)(m_Configuration.Setting.PollInterval_MS) / 1000));
                    comboBoxTimeInterval.Text = Value.ToString();
                }

                if (0 < m_Configuration.HostControlList.HostControl.Count)
                {
                    foreach (XMLHostControl hostControl in m_Configuration.HostControlList.HostControl)
                    {
                        listBoxHostController.Items.Add(hostControl.Name);
                        m_mapHostControl.Add(hostControl.Name, hostControl);
                    }
                }
            }
        }

        private void Clear()
        {
            checkBoxEnableHostController.Checked = true;
            textBoxName.Clear();
            textBoxIP.Clear();
            textBoxPort.Clear();
            textBoxSerialAddress.Clear();
            textBoxStatus.Clear();
            textBoxValue.Clear();
        }
       
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (-1 < listBoxHostController.SelectedIndex)
            {
                try
                {
                    string Name = listBoxHostController.Items[listBoxHostController.SelectedIndex].ToString();
                    if (0 < Name.Length)
                    {
                        if (m_mapHostControl.ContainsKey(Name))
                        {
                            if (m_mapHostControl.Remove(Name))
                            {
                                listBoxHostController.Items.RemoveAt(listBoxHostController.SelectedIndex);
                                MessageBox.Show("Item Removed Successfully", "Success");

                                if (0 < listBoxHostController.Items.Count)
                                {
                                    listBoxHostController.SelectedIndex = 0;
                                }
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Exception Occurs while removing item : Message :" + ex.Message, "Error");	
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                XMLHostControl xmlHostCtrl = ValidateInput();
                if (m_mapHostControl.ContainsKey(xmlHostCtrl.Name))
                    m_mapHostControl.Remove(xmlHostCtrl.Name);

                m_mapHostControl.Add(xmlHostCtrl.Name, xmlHostCtrl);
                if (!listBoxHostController.Items.Contains(xmlHostCtrl.Name))
                {
                    listBoxHostController.Items.Add(xmlHostCtrl.Name);
                    Clear();
                }
                buttonSaveConfig.PerformClick();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Invalid Input : Message :" + ex.Message, "Error");	
            }
        }

        private void FetchDataFromService(string Name, out string Value, out string StatusMessage)
        {
            Value = "N/A";
            StatusMessage = "N/A";

            try
            {   
                // Create the memory-mapped file.
                
                using (var mmf = MemoryMappedFile.OpenExisting("Global\\SerialOverIP",MemoryMappedFileRights.FullControl,HandleInheritability.None))
                {
                    using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {   
                        int Size = accessor.ReadInt32(54);
                        byte[] buffer = new byte[Size];
                        accessor.ReadArray(54 + 4, buffer, 0, buffer.Length);
                        String serialized = ASCIIEncoding.ASCII.GetString(buffer);
                        m_log.Information("ReadingFrom File:" + serialized, 1, null);

                        if (0 < serialized.Length)
                        {
                            string[] atoms = serialized.Split('|');
                            if ((null != atoms) && (0 < atoms.Length))
                            {
                                foreach (string str in atoms)
                                {
                                    string[] chunk = str.Split(',');
                                    if ((null != chunk) && (3 == chunk.Length))
                                    {
                                        if (chunk[0].Equals(Name, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            Value = chunk[1];
                                            StatusMessage = chunk[2];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                m_log.Fatal("Exception occurred while reading memory mapped file, Exception :" + ex.Message, 1, ex.StackTrace);
            }
        }

        private void listBoxHostController_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSelectedIndex = listBoxHostController.SelectedIndex;
            if (-1 < iSelectedIndex)
            {
                try
                {
                    string Name = listBoxHostController.Items[iSelectedIndex].ToString();
                    if (0 < Name.Length)
                    {
                        XMLHostControl ctrl;
                        if(m_mapHostControl.TryGetValue(Name, out ctrl))
                        {
                            if (ctrl.Enabled)
                            {
                                labelNote.ForeColor = System.Drawing.Color.Black;
                                labelNote.Text = "This Area Identifier is Enabled";
                            }
                            else
                            {
                                labelNote.ForeColor = System.Drawing.Color.Red;
                                labelNote.Text = "This Area Identifier is Disabled, It will send blank.";
                            }

                            string Value, statusMessage;
                            FetchDataFromService(ctrl.Name, out Value, out statusMessage);
                            textBoxStatus.Text = statusMessage;
                            checkBoxEnableHostController.Checked = ctrl.Enabled;
                            textBoxName.Text = ctrl.Name;
                            textBoxIP.Text = ctrl.IP;
                            textBoxPort.Text = ctrl.Port.ToString();
                            textBoxSerialAddress.Text = ctrl.SerialAddress.ToString();

                            if (Value.Trim().Equals("-1"))
                            {
                                textBoxValue.Text = "N/A";
                                textBoxStatus.Text = "Record Not Found";
                            }
                            else if (Value.Trim().Equals("0"))
                            {
                                textBoxValue.Text = "FULL";
                            }
                            else if (Value.Trim().Equals("10003"))
                            {
                                textBoxValue.Text = "BLANK";
                            }
                            else
                            {
                                textBoxValue.Text = Value;
                            }
                        }
                        else
                        {
                            throw new Exception("Data Present in List but not internally. Please Restart the Application.");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Exception Occurs while fetching item : Message :" + ex.Message, "Error");
                }
            }
        }

        XMLHostControl ValidateInput()
        {
            XMLHostControl xmlHostCtrl = new XMLHostControl();
            bool Enabled = checkBoxEnableHostController.Checked;
            string Name = textBoxName.Text.Trim();
            string IP = textBoxIP.Text.Trim();
            string Port = textBoxPort.Text.Trim();
            string SerialAddress = textBoxSerialAddress.Text.Trim();

            if (0 < Name.Length)
            {
                if (0 < IP.Length)
                {
                    if (0 < Port.Length)
                    {
                        if (0 < SerialAddress.Length)
                        {
                            try
                            {
                                int iPort = Convert.ToInt32(Port);
                                try
                                {
                                    int iSerialAddress = Convert.ToInt32(SerialAddress);
                                    xmlHostCtrl.Enabled = Enabled;
                                    xmlHostCtrl.Name = Name;
                                    xmlHostCtrl.IP = IP;
                                    xmlHostCtrl.Port = iPort;
                                    xmlHostCtrl.SerialAddress = iSerialAddress;
                                }
                                catch (System.Exception)
                                {
                                    throw new Exception("Serial Address found to be invalid");
                                }
                            }
                            catch (System.Exception)
                            {
                                throw new Exception("Port found to be invalid");
                            }
                        }
                        else
                        {
                            throw new Exception("Serial Address Can Not Be Empty.");
                        }
                    }
                    else
                    {
                        throw new Exception("Port Can Not Be Empty.");
                    }
                }
                else
                {
                    throw new Exception("IP Can Not Be Empty.");
                }
            }
            else
            {
                throw new Exception("Area Identifier Can Not Be Empty.");
            }

            return xmlHostCtrl;
        }

        #region INTERNAL_FUNCTIONS

        private DataSet GetEmptyRecipentDataSet()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable(RECIPIENT_CONFIG_TABLE_NAME);
            dataTable.Columns.Add("RecipientID", typeof(int));
            dataTable.Columns.Add("GroupName", typeof(string));
            dataTable.Columns.Add("RecipientEmail", typeof(string));
            dataTable.Columns["RecipientID"].Unique = true;
            dataSet.Tables.Add(dataTable);
            dataSet.DataSetName = RECIPIENT_CONFIG_DATASET_NAME;
            return dataSet;
        }

        private long BytesToMB(long Bytes)
        {
            long MB = 0;
            MB = ((Bytes / 1024) / 1024);
            return MB;
        }

        private long MBToBytes(long MB)
        {
            long Bytes = 0;
            Bytes = ((MB * 1024) * 1024);
            return Bytes;
        }

        private long MBToBytes(String stringMB)
        {
            long MB = 0;
            long Bytes = 0;
            if (Int64.TryParse(stringMB, out MB))
            {
                Bytes = ((MB * 1024) * 1024);
            }
            else
            {
                throw (new Exception("Unable to Convert to Number : " + stringMB));
            }

            return Bytes;
        }


        private int MsToSec(int ms)
        {
            int sec = 0;
            sec = ms / 1000;
            return sec;
        }

        private long MsToSec(long ms)
        {
            long sec = 0;
            sec = ms / 1000;
            return sec;
        }

        private int SecToMSInt(string Second)
        {
            int ms = 0;
            int Sec = 0;

            if (Int32.TryParse(Second, out Sec))
            {
                ms = Sec * 1000;
            }
            else
            {
                throw (new Exception("Unable to Convert to Number : " + Second));
            }

            return ms;

        }

        private long SecToMSLong(string Second)
        {
            long ms = 0;
            long Sec = 0;

            if (Int64.TryParse(Second, out Sec))
            {
                ms = Sec * 1000;
            }
            else
            {
                throw (new Exception("Unable to Convert to Number : " + Second));
            }

            return ms;
        }

        #endregion

        #region SERVICE_CONTROL

        private void buttonRefreshServiceStatus_Click(object sender, EventArgs e)
        {
            LoadServiceInformation();
        }

        private void buttonServiceStart_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController(SERVICE_NAME);
            try
            {

                TimeSpan timeout = TimeSpan.FromMilliseconds(SecToMSInt("300"));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                LoadServiceInformation();
            }
            catch (Exception ex)
            {
                String logMessage = "Exception occurred while Starting Service, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
                MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonServiceStop_Click(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController(SERVICE_NAME);
            try
            {
                Process.Start("taskkill", "/F /FI \"SERVICES eq SignController\"");
                LoadServiceInformation();
            }
            catch (Exception ex)
            {
                String logMessage = "Exception occurred while Stopping Service, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
                MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServiceInformation()
        {
            textBoxServiceStatus.Text = GetServiceStatus();

            if (IsServiceStarted)
            {
                buttonServiceStart.Enabled = false;
                buttonServiceKill.Enabled = true;
                buttonServiceStop.Enabled = true;
            }
            else
            {
                buttonServiceStart.Enabled = true;
                buttonServiceKill.Enabled = false;
                buttonServiceStop.Enabled = false;
            }
        }

        private string GetServiceStatus()
        {
            try
            {
                ServiceController sc = new ServiceController(SERVICE_NAME);
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        IsServiceStarted = true;
                        return "Running";
                    case ServiceControllerStatus.Stopped:
                        IsServiceStarted = false;
                        return "Stopped";
                    case ServiceControllerStatus.Paused:
                        IsServiceStarted = false;
                        return "Paused";
                    case ServiceControllerStatus.StopPending:
                        IsServiceStarted = true;
                        return "Stopping";
                    case ServiceControllerStatus.StartPending:
                        IsServiceStarted = false;
                        return "Starting";
                    default:
                        return "Status Changing";
                }
            }
            catch (Exception ex)
            {
                m_log.Error("Exception occurred while Fetching Service Status, Exception : " + ex.Message, 1, ex.StackTrace);
                return "Unknown";
            }
        }

        #endregion

        private void buttonOpenXMLFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog1.ShowDialog();
            // Process input if the user clicked OK.
            if (result == DialogResult.OK)
            {
                if (0 < openFileDialog1.FileName.Length)
                {
                    textBoxRunTimeXMLPath.Text = openFileDialog1.FileName;
                }
            }
        }

        private void buttonServiceStop_Click_1(object sender, EventArgs e)
        {
            ServiceController service = new ServiceController(SERVICE_NAME);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(SecToMSInt("300"));
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                LoadServiceInformation();
            }
            catch (Exception ex)
            {
                String logMessage = "Exception occurred while Stopping Service, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
                MessageBox.Show(logMessage, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() {
                                                FileName = m_Configuration.Logging.Directory,
                                                UseShellExecute = true,
                                                Verb = "open"
                                            });
            }
            catch (System.Exception ex)
            {
                String logMessage = "Exception occurred while Opening Log Directory, Exception : " + ex.Message;
                m_log.Error(logMessage, 1, ex.StackTrace);
            }
        }
        
        private void buttonToggle_Click(object sender, EventArgs e)
        {
            if (IsSmall)
            {
                this.Width = 970;
                this.Height = 467;
                IsSmall = false;
            }
            else
            {
                this.Width = 662;
                this.Height = 467;
                IsSmall = true;
            }
        }
    }
}
