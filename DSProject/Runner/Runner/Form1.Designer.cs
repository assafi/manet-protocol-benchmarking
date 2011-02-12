using System.ComponentModel;
using ManetProtocolAdaptors;

namespace Runner
{
    partial class Runner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Runner));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.runMenuItem = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            //this.fileNameTextBox = new System.Windows.Forms.TextBox();
            //this.chooseFileButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.argsComboBox = new System.Windows.Forms.ComboBox();
            this.protocolsComboBox = new System.Windows.Forms.ComboBox();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.saveLogButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.runMenuItem);
            this.mainMenu1.MenuItems.Add(this.aboutMenuItem);
            // 
            // runMenuItem
            // 
            this.runMenuItem.Text = "Run";
            this.runMenuItem.Click += new System.EventHandler(this.runMenuItemClicked);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItemClicked);
            //// 
            //// fileNameTextBox
            //// 
            //this.fileNameTextBox.Enabled = false;
            //this.fileNameTextBox.Location = new System.Drawing.Point(0, 31);
            //this.fileNameTextBox.Name = "fileNameTextBox";
            //this.fileNameTextBox.Size = new System.Drawing.Size(240, 21);
            //this.fileNameTextBox.TabIndex = 0;
            //this.fileNameTextBox.Text = "\\Program Files\\iperfWM\\iperfWiMo.exe";
            //// 
            //// chooseFileButton
            //// 
            //this.chooseFileButton.Location = new System.Drawing.Point(161, 3);
            //this.chooseFileButton.Name = "chooseFileButton";
            //this.chooseFileButton.Size = new System.Drawing.Size(79, 22);
            //this.chooseFileButton.TabIndex = 1;
            //this.chooseFileButton.Text = "Choose File";
            //this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButtonClick);
            // 
            // logTextBox
            // 
            this.logTextBox.Font = new System.Drawing.Font("David", 9F, System.Drawing.FontStyle.Regular);
            this.logTextBox.Location = new System.Drawing.Point(0, 126);
            this.logTextBox.MaxLength = 320767;
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(239, 115);
            this.logTextBox.TabIndex = 3;
            this.logTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.Text = "Arguments:";

            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label1";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.Text = "Protocols:";

            // 
            // label2
            // 
            //this.label2.Location = new System.Drawing.Point(3, 5);
            //this.label2.Name = "label2";
            //this.label2.Size = new System.Drawing.Size(65, 20);
            //this.label2.Text = "Run file:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 20);
            this.label3.Text = "Log";
            
            // 
            // protocolsComboBox
            // 
            foreach (var protocolClassName in ProtocolsNames.Protocols)
            {
                int classNameIndex = protocolClassName.Split(new char[] {'.'}).Length;
                string protocolName = protocolClassName.Split(new char[] {'.'})[classNameIndex-1]; // Get the class name without the namespaces
                this.protocolsComboBox.Items.Add(protocolName);         
            }
            this.protocolsComboBox.Location = new System.Drawing.Point(0, 31);
            this.protocolsComboBox.Name = "protocolsComboBox";
            this.protocolsComboBox.Size = new System.Drawing.Size(240, 22);
            this.protocolsComboBox.TabIndex = 9;
            
            // 
            // argsComboBox
            // 
            this.argsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.argsComboBox.Items.Add("-s -ip 192.168.0.1");
            this.argsComboBox.Items.Add("-inter -ip 192.168.0.2");
            //this.argsComboBox.Items.Add("-c 127.0.0.1");
            this.argsComboBox.Items.Add("-c 192.168.0.1 -ip 192.168.0.3");
            //this.argsComboBox.Items.Add("-c 192.168.0.1 -t 60");
            this.argsComboBox.Location = new System.Drawing.Point(0, 78);
            this.argsComboBox.Name = "argsComboBox";
            this.argsComboBox.Size = new System.Drawing.Size(240, 22);
            this.argsComboBox.TabIndex = 9;
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(165, 245);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(72, 20);
            this.clearLogButton.TabIndex = 13;
            this.clearLogButton.Text = "Clear log";
            this.clearLogButton.Click += new System.EventHandler(this.clearLogButtonClick);
            // 
            // saveLogButton
            // 
            this.saveLogButton.Location = new System.Drawing.Point(3, 245);
            this.saveLogButton.Name = "saveLogButton";
            this.saveLogButton.Size = new System.Drawing.Size(72, 20);
            this.saveLogButton.TabIndex = 14;
            this.saveLogButton.Text = "Save log";
            this.saveLogButton.Click += new System.EventHandler(this.saveLogButtonClick);
            // 
            // Runner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.saveLogButton);
            this.Controls.Add(this.clearLogButton);
            this.Controls.Add(this.argsComboBox);
            this.Controls.Add(this.protocolsComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logTextBox);
//            this.Controls.Add(this.chooseFileButton);
//            this.Controls.Add(this.fileNameTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Runner";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuItem runMenuItem;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox argsComboBox;
        private System.Windows.Forms.ComboBox protocolsComboBox;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.Button saveLogButton;
    }
}

