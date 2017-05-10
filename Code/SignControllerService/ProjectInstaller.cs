using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Diagnostics;
using System.Linq;


namespace SignController
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }       

        private void SerialOverIPService_BeforeInstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == SignControllerService.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

        private void SerialOverIPService_BeforeUninstall(object sender, InstallEventArgs e)
        {
            try
            {
                ServiceController controller = ServiceController.GetServices().Where
                (s => s.ServiceName == SignControllerService.ServiceName).FirstOrDefault();
                if (controller != null)
                {
                    if ((controller.Status != ServiceControllerStatus.Stopped) &&
                    (controller.Status != ServiceControllerStatus.StopPending))
                    {
                        controller.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Configuration.Install.InstallException
                    (ex.Message.ToString());
            }
        }

        private void SerialOverIPService_AfterInstall(object sender, InstallEventArgs e)
        {
            SetRecoveryOptions(SignControllerService.ServiceName);
        }

        private void SetRecoveryOptions(string serviceName)
        {            
            using (var process = new Process())
            {
                var startInfo = process.StartInfo;
                startInfo.FileName = "sc";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                // tell Windows that the service should restart if it fails
                startInfo.Arguments = string.Format("failure \"{0}\" reset= 604800 actions= restart/10000/restart/10000/restart/10000", serviceName);
                process.StartInfo = startInfo;
                process.Start();
            }
        }
    }
}
