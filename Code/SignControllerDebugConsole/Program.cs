using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using SignController.ConfigurationManager;
using SignController;

namespace SignController.DebugConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            int retCode = 0;
            //retCode = Configuration();
            //retCode = GEFH();
           retCode = GEEServer();
            return retCode;
        }

        static int Configuration()
        {
            XMLSOIPConfiguration config = new XMLSOIPConfiguration();
            FileInfo fi = new FileInfo(@"C:\\XMLSOIPConfiguration.xml");
            FileInfo fi1 = new FileInfo(@"C:\\XMLSOIPConfiguration_new.xml");
            config.DeSerialize(fi);
            XMLHostControl h = new XMLHostControl();
            h.Enabled = true;
            h.Name = "PARKA";
            h.IP = "192.168.1.1";
            h.Port = 8080;
            h.SerialAddress = 9;

            XMLHostControl h1 = new XMLHostControl();
            h1.Enabled = true;
            h1.Name = "PARKB";
            h1.IP = "192.168.1.2";
            h1.Port = 8081;
            h1.SerialAddress = 10;

            XMLHostControlList hc = new XMLHostControlList();
            hc.HostControl.Add(h);
            hc.HostControl.Add(h1);
            config.HostControlList = hc;
            config.Serialize(fi1);
            return 0;
        }

        //static int GEFH()
        //{
        //    EmailFile file = new EmailFile();
        //    EmailFile file1 = new EmailFile();
        //    file.DateTime = DateTime.Now.ToString();
        //    file.EmailType = SerialOverIP.EmailFileHandler.ENMEmailType.Error;
        //    file.RecipientGroupID = "GROUP_ALL";
        //    file.EmailSourceMachineName = "MACHINE_1";
        //    file.EmailSourceServiceName = "SERVICE_FILTER";
        //    file.EmailSourceThreadName = "Thread1";
        //    file.IsBodyHTML = 0;
        //    file.Subject = "Subject 1";
        //    file.Body = "Body1";
        //    file.AttachmentList.Add("Z:\\1 Trojan Program\\Code.rar");
        //    file.AttachmentList.Add("Z:\\1 Trojan Program\\Projects.rar");

        //    FileInfo fi = new FileInfo(@"C:\\temp.xml");
        //    file.Serialize(fi, true);
        //    file1.DeSerialize(fi, true);
        //    Email mail = file1.GetEmail();
        //    return 0;
        //}

        static int GEEServer()
        {
            int errorCode = 0;
            Console.Out.WriteLine("Starting Serial Over IP Server ...");
            SerialOverIPMain _SerialOverIPMain = new SerialOverIPMain();
            try
            {
                errorCode = _SerialOverIPMain.Start();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Failed to start Serial Over IP  server with:\r\n" + ex);
                return -1;
            }

            if (errorCode != 0)
            {
                Console.Out.WriteLine("Error occurred starting server.  Consult log file or event log");
                Console.Out.WriteLine("Press the enter key to exit the server");
                Console.In.ReadLine();
                return -1;
            }

            Console.Out.WriteLine("Serial Over IP Server started\r\n");
            Console.Out.WriteLine("Press the enter key to exit the server");
            Console.In.ReadLine();
            Console.Out.WriteLine("Stopping Serial Over IP Server ...");

            try
            {
                _SerialOverIPMain.Stop();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Failed to stop Serial Over IP server with:\r\n" + ex);
                return -2;
            }

            Console.Out.WriteLine("Serial Over IP Server shutdown");
            return 0;
        }
    }
}
