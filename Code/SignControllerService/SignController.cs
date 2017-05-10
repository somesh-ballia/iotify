using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using SignController;

namespace SignController
{
    public partial class SignControllerService : ServiceBase
    {
        private SerialOverIPMain _SerialOverIPMain = new SerialOverIPMain();

        public SignControllerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int errorCode = _SerialOverIPMain.Start();
            if (errorCode != 0)
            {
                ExitCode = errorCode;
                throw new InvalidOperationException("Error occurred starting service.  See log.");
            }
        }

        protected override void OnStop()
        {
            _SerialOverIPMain.Stop();
            ExitCode = 0;
        }
    }
}
