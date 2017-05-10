namespace SignController
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SignControllerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SignControllerService = new System.ServiceProcess.ServiceInstaller();
            // 
            // SignControllerProcessInstaller
            // 
            this.SignControllerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SignControllerProcessInstaller.Password = null;
            this.SignControllerProcessInstaller.Username = null;
            // 
            // SignControllerService
            // 
            this.SignControllerService.Description = "This Service is responsible for Sign Controlling";
            this.SignControllerService.DisplayName = "SignController";
            this.SignControllerService.ServiceName = "SignController";
            this.SignControllerService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.SignControllerService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.SerialOverIPService_AfterInstall);
            this.SignControllerService.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.SerialOverIPService_BeforeInstall);
            this.SignControllerService.BeforeUninstall += new System.Configuration.Install.InstallEventHandler(this.SerialOverIPService_BeforeUninstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SignControllerProcessInstaller,
            this.SignControllerService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SignControllerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SignControllerService;
    }
}