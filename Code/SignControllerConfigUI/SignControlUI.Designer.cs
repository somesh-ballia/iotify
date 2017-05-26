namespace SignController.ConfigurationUI
{
    partial class SignControlUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignControlUI));
            this.textBoxConfigurationFilePath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPageGeneralConfiguration = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonToggle = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonOpenXMLFile = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxTimeInterval = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxRunTimeXMLPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBoxHostController = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxSerialAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNew = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.checkBoxEnableHostController = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxServiceStatus = new System.Windows.Forms.TextBox();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonRefreshServiceStatus = new System.Windows.Forms.Button();
            this.buttonServiceKill = new System.Windows.Forms.Button();
            this.buttonServiceStart = new System.Windows.Forms.Button();
            this.tabControlConfiguration = new System.Windows.Forms.TabControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonServiceStop = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelNote = new System.Windows.Forms.Label();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.rightClickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPageGeneralConfiguration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControlConfiguration.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.rightClickMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxConfigurationFilePath
            // 
            this.textBoxConfigurationFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConfigurationFilePath.Location = new System.Drawing.Point(139, 9);
            this.textBoxConfigurationFilePath.Name = "textBoxConfigurationFilePath";
            this.textBoxConfigurationFilePath.ReadOnly = true;
            this.textBoxConfigurationFilePath.Size = new System.Drawing.Size(516, 24);
            this.textBoxConfigurationFilePath.TabIndex = 6;
            this.textBoxConfigurationFilePath.TabStop = false;
            this.textBoxConfigurationFilePath.Text = "C:\\Windows\\XMLSOIPConfiguration.xml";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 18);
            this.label12.TabIndex = 5;
            this.label12.Text = "Configuration File";
            // 
            // tabPageGeneralConfiguration
            // 
            this.tabPageGeneralConfiguration.Controls.Add(this.groupBox1);
            this.tabPageGeneralConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneralConfiguration.Name = "tabPageGeneralConfiguration";
            this.tabPageGeneralConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneralConfiguration.Size = new System.Drawing.Size(648, 353);
            this.tabPageGeneralConfiguration.TabIndex = 0;
            this.tabPageGeneralConfiguration.Text = "Configuration";
            this.tabPageGeneralConfiguration.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonToggle);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.buttonOpenXMLFile);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBoxTimeInterval);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxRunTimeXMLPath);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 341);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Host Controller Configuration";
            // 
            // buttonToggle
            // 
            this.buttonToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToggle.Location = new System.Drawing.Point(12, 83);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(600, 19);
            this.buttonToggle.TabIndex = 19;
            this.buttonToggle.UseVisualStyleBackColor = true;
            this.buttonToggle.Click += new System.EventHandler(this.buttonToggle_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(296, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Note : ";
            // 
            // buttonOpenXMLFile
            // 
            this.buttonOpenXMLFile.Location = new System.Drawing.Point(572, 22);
            this.buttonOpenXMLFile.Name = "buttonOpenXMLFile";
            this.buttonOpenXMLFile.Size = new System.Drawing.Size(31, 23);
            this.buttonOpenXMLFile.TabIndex = 18;
            this.buttonOpenXMLFile.TabStop = false;
            this.buttonOpenXMLFile.Text = "..";
            this.buttonOpenXMLFile.UseVisualStyleBackColor = true;
            this.buttonOpenXMLFile.Click += new System.EventHandler(this.buttonOpenXMLFile_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(569, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "ms";
            // 
            // comboBoxTimeInterval
            // 
            this.comboBoxTimeInterval.FormattingEnabled = true;
            this.comboBoxTimeInterval.Items.AddRange(new object[] {
            "10",
            "50",
            "100",
            "500",
            "1000",
            "2000",
            "3000",
            "4000",
            "5000",
            "10000",
            "20000",
            "50000"});
            this.comboBoxTimeInterval.Location = new System.Drawing.Point(114, 53);
            this.comboBoxTimeInterval.Name = "comboBoxTimeInterval";
            this.comboBoxTimeInterval.Size = new System.Drawing.Size(449, 21);
            this.comboBoxTimeInterval.TabIndex = 17;
            this.comboBoxTimeInterval.TabStop = false;
            this.comboBoxTimeInterval.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Poll Interval";
            // 
            // textBoxRunTimeXMLPath
            // 
            this.textBoxRunTimeXMLPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRunTimeXMLPath.Location = new System.Drawing.Point(114, 22);
            this.textBoxRunTimeXMLPath.Name = "textBoxRunTimeXMLPath";
            this.textBoxRunTimeXMLPath.Size = new System.Drawing.Size(449, 24);
            this.textBoxRunTimeXMLPath.TabIndex = 15;
            this.textBoxRunTimeXMLPath.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Runtime XML Path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxHostController);
            this.groupBox2.Location = new System.Drawing.Point(6, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 227);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Host Controller Selector";
            // 
            // listBoxHostController
            // 
            this.listBoxHostController.FormattingEnabled = true;
            this.listBoxHostController.Location = new System.Drawing.Point(6, 18);
            this.listBoxHostController.Name = "listBoxHostController";
            this.listBoxHostController.Size = new System.Drawing.Size(187, 199);
            this.listBoxHostController.Sorted = true;
            this.listBoxHostController.TabIndex = 0;
            this.listBoxHostController.TabStop = false;
            this.listBoxHostController.SelectedIndexChanged += new System.EventHandler(this.listBoxHostController_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxSerialAddress);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.textBoxStatus);
            this.groupBox3.Controls.Add(this.textBoxValue);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttonDelete);
            this.groupBox3.Controls.Add(this.textBoxPort);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.buttonNew);
            this.groupBox3.Controls.Add(this.textBoxName);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonSave);
            this.groupBox3.Controls.Add(this.textBoxIP);
            this.groupBox3.Controls.Add(this.checkBoxEnableHostController);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(213, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(399, 204);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Host Controller Editor";
            // 
            // textBoxSerialAddress
            // 
            this.textBoxSerialAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSerialAddress.Location = new System.Drawing.Point(125, 142);
            this.textBoxSerialAddress.Name = "textBoxSerialAddress";
            this.textBoxSerialAddress.Size = new System.Drawing.Size(75, 24);
            this.textBoxSerialAddress.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Status";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStatus.Location = new System.Drawing.Point(125, 16);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(168, 24);
            this.textBoxStatus.TabIndex = 12;
            this.textBoxStatus.TabStop = false;
            this.textBoxStatus.Text = "Inactive";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValue.Location = new System.Drawing.Point(247, 142);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(136, 24);
            this.textBoxValue.TabIndex = 11;
            this.textBoxValue.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Value";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(216, 171);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(77, 23);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPort.Location = new System.Drawing.Point(125, 112);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(258, 24);
            this.textBoxPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(125, 171);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(77, 23);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.TabStop = false;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(125, 49);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(258, 24);
            this.textBoxName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Area Identifier";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(306, 171);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(77, 23);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.Location = new System.Drawing.Point(125, 83);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(258, 24);
            this.textBoxIP.TabIndex = 2;
            // 
            // checkBoxEnableHostController
            // 
            this.checkBoxEnableHostController.AutoSize = true;
            this.checkBoxEnableHostController.Checked = true;
            this.checkBoxEnableHostController.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableHostController.Location = new System.Drawing.Point(318, 19);
            this.checkBoxEnableHostController.Name = "checkBoxEnableHostController";
            this.checkBoxEnableHostController.Size = new System.Drawing.Size(65, 17);
            this.checkBoxEnableHostController.TabIndex = 0;
            this.checkBoxEnableHostController.Text = "Enabled";
            this.checkBoxEnableHostController.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Serial Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "IP";
            // 
            // textBoxServiceStatus
            // 
            this.textBoxServiceStatus.Location = new System.Drawing.Point(88, 31);
            this.textBoxServiceStatus.Name = "textBoxServiceStatus";
            this.textBoxServiceStatus.ReadOnly = true;
            this.textBoxServiceStatus.Size = new System.Drawing.Size(188, 20);
            this.textBoxServiceStatus.TabIndex = 4;
            this.textBoxServiceStatus.TabStop = false;
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Location = new System.Drawing.Point(9, 113);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(267, 27);
            this.buttonSaveConfig.TabIndex = 1;
            this.buttonSaveConfig.TabStop = false;
            this.buttonSaveConfig.Text = "Save";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 35);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(76, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "Service Status";
            // 
            // buttonRefreshServiceStatus
            // 
            this.buttonRefreshServiceStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefreshServiceStatus.Location = new System.Drawing.Point(9, 67);
            this.buttonRefreshServiceStatus.Name = "buttonRefreshServiceStatus";
            this.buttonRefreshServiceStatus.Size = new System.Drawing.Size(73, 27);
            this.buttonRefreshServiceStatus.TabIndex = 2;
            this.buttonRefreshServiceStatus.TabStop = false;
            this.buttonRefreshServiceStatus.Text = "Refresh";
            this.buttonRefreshServiceStatus.UseVisualStyleBackColor = true;
            this.buttonRefreshServiceStatus.Click += new System.EventHandler(this.buttonRefreshServiceStatus_Click);
            // 
            // buttonServiceKill
            // 
            this.buttonServiceKill.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonServiceKill.Location = new System.Drawing.Point(212, 67);
            this.buttonServiceKill.Name = "buttonServiceKill";
            this.buttonServiceKill.Size = new System.Drawing.Size(64, 27);
            this.buttonServiceKill.TabIndex = 1;
            this.buttonServiceKill.TabStop = false;
            this.buttonServiceKill.Text = "Restart";
            this.buttonServiceKill.UseVisualStyleBackColor = true;
            this.buttonServiceKill.Click += new System.EventHandler(this.buttonServiceStop_Click);
            // 
            // buttonServiceStart
            // 
            this.buttonServiceStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonServiceStart.Location = new System.Drawing.Point(88, 67);
            this.buttonServiceStart.Name = "buttonServiceStart";
            this.buttonServiceStart.Size = new System.Drawing.Size(58, 27);
            this.buttonServiceStart.TabIndex = 0;
            this.buttonServiceStart.TabStop = false;
            this.buttonServiceStart.Text = "Start";
            this.buttonServiceStart.UseVisualStyleBackColor = true;
            this.buttonServiceStart.Click += new System.EventHandler(this.buttonServiceStart_Click);
            // 
            // tabControlConfiguration
            // 
            this.tabControlConfiguration.Controls.Add(this.tabPageGeneralConfiguration);
            this.tabControlConfiguration.Location = new System.Drawing.Point(3, 38);
            this.tabControlConfiguration.Name = "tabControlConfiguration";
            this.tabControlConfiguration.SelectedIndex = 0;
            this.tabControlConfiguration.Size = new System.Drawing.Size(656, 379);
            this.tabControlConfiguration.TabIndex = 0;
            this.tabControlConfiguration.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.buttonServiceStop);
            this.groupBox4.Controls.Add(this.buttonSaveConfig);
            this.groupBox4.Controls.Add(this.buttonServiceKill);
            this.groupBox4.Controls.Add(this.textBoxServiceStatus);
            this.groupBox4.Controls.Add(this.buttonServiceStart);
            this.groupBox4.Controls.Add(this.buttonRefreshServiceStatus);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Location = new System.Drawing.Point(665, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(287, 160);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Service Settings";
            // 
            // buttonServiceStop
            // 
            this.buttonServiceStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonServiceStop.Location = new System.Drawing.Point(150, 67);
            this.buttonServiceStop.Name = "buttonServiceStop";
            this.buttonServiceStop.Size = new System.Drawing.Size(58, 27);
            this.buttonServiceStop.TabIndex = 8;
            this.buttonServiceStop.TabStop = false;
            this.buttonServiceStop.Text = "Stop";
            this.buttonServiceStop.UseVisualStyleBackColor = true;
            this.buttonServiceStop.Click += new System.EventHandler(this.buttonServiceStop_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SignController.ConfigurationUI.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(665, 335);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 82);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(353, 382);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(0, 13);
            this.labelNote.TabIndex = 10;
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Location = new System.Drawing.Point(675, 301);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(267, 23);
            this.buttonOpenLog.TabIndex = 11;
            this.buttonOpenLog.Text = "OpenLog";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // rightClickMenuStrip
            // 
            this.rightClickMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem});
            this.rightClickMenuStrip.Name = "contextMenuStrip1";
            this.rightClickMenuStrip.Size = new System.Drawing.Size(112, 26);
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.toggleToolStripMenuItem.Text = "Toggle";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(230, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "V 2.0.1";
            // 
            // SignControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 429);
            this.Controls.Add(this.buttonOpenLog);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.textBoxConfigurationFilePath);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tabControlConfiguration);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(662, 467);
            this.Name = "SignControlUI";
            this.Text = "Precise Group Technologies - S&B Access Control Systems Integration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GEEConfiguration_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPageGeneralConfiguration.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControlConfiguration.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.rightClickMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxConfigurationFilePath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPageGeneralConfiguration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxHostController;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.CheckBox checkBoxEnableHostController;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxServiceStatus;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonRefreshServiceStatus;
        private System.Windows.Forms.Button buttonServiceKill;
        private System.Windows.Forms.Button buttonServiceStart;
        private System.Windows.Forms.TabControl tabControlConfiguration;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.TextBox textBoxSerialAddress;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxRunTimeXMLPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxTimeInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonOpenXMLFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonServiceStop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonOpenLog;
        private System.Windows.Forms.ContextMenuStrip rightClickMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
        private System.Windows.Forms.Button buttonToggle;
        private System.Windows.Forms.Label label11;

    }
}

