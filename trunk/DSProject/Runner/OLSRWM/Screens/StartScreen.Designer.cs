namespace OLSR.Screens
{
    partial class StartScreen
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.tbs = new System.Windows.Forms.TabControl();
            this.tbWorker = new System.Windows.Forms.TabPage();
            this.bttLoadConfig = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.bttJoin = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOLSR = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.chckDebug = new System.Windows.Forms.CheckBox();
            this.bttStop = new System.Windows.Forms.Button();
            this.bttStart = new System.Windows.Forms.Button();
            this.lstInterfaces = new System.Windows.Forms.ListBox();
            this.tbParams = new System.Windows.Forms.TabPage();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.bttSave = new System.Windows.Forms.Button();
            this.bttRestore = new System.Windows.Forms.Button();
            this.txtRefresh = new System.Windows.Forms.TextBox();
            this.txtTc = new System.Windows.Forms.TextBox();
            this.txtHello = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNeigh = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lst2HopNeighbors = new System.Windows.Forms.ListBox();
            this.lstNeighbors = new System.Windows.Forms.ListBox();
            this.tbMPR = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.lstMPR = new System.Windows.Forms.ListBox();
            this.tbs.SuspendLayout();
            this.tbWorker.SuspendLayout();
            this.tbOLSR.SuspendLayout();
            this.tbParams.SuspendLayout();
            this.tbNeigh.SuspendLayout();
            this.tbMPR.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // tbs
            // 
            this.tbs.Controls.Add(this.tbWorker);
            this.tbs.Controls.Add(this.tbOLSR);
            this.tbs.Controls.Add(this.tbParams);
            this.tbs.Controls.Add(this.tbNeigh);
            this.tbs.Controls.Add(this.tbMPR);
            this.tbs.Location = new System.Drawing.Point(0, 0);
            this.tbs.Name = "tbs";
            this.tbs.SelectedIndex = 0;
            this.tbs.Size = new System.Drawing.Size(240, 268);
            this.tbs.TabIndex = 0;
            // 
            // tbWorker
            // 
            this.tbWorker.Controls.Add(this.bttLoadConfig);
            this.tbWorker.Controls.Add(this.label24);
            this.tbWorker.Controls.Add(this.bttJoin);
            this.tbWorker.Controls.Add(this.listBox2);
            this.tbWorker.Controls.Add(this.listBox1);
            this.tbWorker.Controls.Add(this.label4);
            this.tbWorker.Controls.Add(this.numericUpDown2);
            this.tbWorker.Controls.Add(this.label5);
            this.tbWorker.Controls.Add(this.numericUpDown1);
            this.tbWorker.Controls.Add(this.label6);
            this.tbWorker.Location = new System.Drawing.Point(0, 0);
            this.tbWorker.Name = "tbWorker";
            this.tbWorker.Size = new System.Drawing.Size(240, 245);
            this.tbWorker.Text = "Worker";
            // 
            // bttLoadConfig
            // 
            this.bttLoadConfig.Enabled = false;
            this.bttLoadConfig.Location = new System.Drawing.Point(138, 210);
            this.bttLoadConfig.Name = "bttLoadConfig";
            this.bttLoadConfig.Size = new System.Drawing.Size(77, 24);
            this.bttLoadConfig.TabIndex = 25;
            this.bttLoadConfig.Text = "Load";
            this.bttLoadConfig.Click += new System.EventHandler(this.bttLoadConfig_Click);
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(117, 6);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(116, 18);
            // 
            // bttJoin
            // 
            this.bttJoin.Location = new System.Drawing.Point(29, 210);
            this.bttJoin.Name = "bttJoin";
            this.bttJoin.Size = new System.Drawing.Size(77, 24);
            this.bttJoin.TabIndex = 24;
            this.bttJoin.Text = "Join";
            this.bttJoin.Click += new System.EventHandler(this.bttJoin_Click);
            // 
            // listBox2
            // 
            this.listBox2.Location = new System.Drawing.Point(29, 146);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(186, 58);
            this.listBox2.TabIndex = 23;
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(29, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(186, 58);
            this.listBox1.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 22);
            this.label4.Text = "Worker ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(156, 120);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown2.TabIndex = 22;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(7, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 22);
            this.label5.Text = "Groups";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(156, 27);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 22);
            this.numericUpDown1.TabIndex = 20;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(5, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 22);
            this.label6.Text = "Team Size";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbOLSR
            // 
            this.tbOLSR.Controls.Add(this.label7);
            this.tbOLSR.Controls.Add(this.chckDebug);
            this.tbOLSR.Controls.Add(this.bttStop);
            this.tbOLSR.Controls.Add(this.bttStart);
            this.tbOLSR.Controls.Add(this.lstInterfaces);
            this.tbOLSR.Location = new System.Drawing.Point(0, 0);
            this.tbOLSR.Name = "tbOLSR";
            this.tbOLSR.Size = new System.Drawing.Size(240, 245);
            this.tbOLSR.Text = "OLSR";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(26, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 22);
            this.label7.Text = "Interface Address list";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chckDebug
            // 
            this.chckDebug.Location = new System.Drawing.Point(26, 163);
            this.chckDebug.Name = "chckDebug";
            this.chckDebug.Size = new System.Drawing.Size(182, 28);
            this.chckDebug.TabIndex = 5;
            this.chckDebug.Text = "Test Mode";
            // 
            // bttStop
            // 
            this.bttStop.Enabled = false;
            this.bttStop.Location = new System.Drawing.Point(126, 197);
            this.bttStop.Name = "bttStop";
            this.bttStop.Size = new System.Drawing.Size(82, 32);
            this.bttStop.TabIndex = 4;
            this.bttStop.Text = "Stop";
            this.bttStop.Click += new System.EventHandler(this.bttStop_Click);
            // 
            // bttStart
            // 
            this.bttStart.Enabled = false;
            this.bttStart.Location = new System.Drawing.Point(26, 197);
            this.bttStart.Name = "bttStart";
            this.bttStart.Size = new System.Drawing.Size(82, 32);
            this.bttStart.TabIndex = 3;
            this.bttStart.Text = "Start";
            this.bttStart.Click += new System.EventHandler(this.bttStart_Click);
            // 
            // lstInterfaces
            // 
            this.lstInterfaces.Location = new System.Drawing.Point(26, 43);
            this.lstInterfaces.Name = "lstInterfaces";
            this.lstInterfaces.Size = new System.Drawing.Size(182, 114);
            this.lstInterfaces.TabIndex = 2;
            this.lstInterfaces.SelectedIndexChanged += new System.EventHandler(this.lstInterfaces_SelectedIndexChanged);
            // 
            // tbParams
            // 
            this.tbParams.Controls.Add(this.cmbLanguage);
            this.tbParams.Controls.Add(this.label12);
            this.tbParams.Controls.Add(this.bttSave);
            this.tbParams.Controls.Add(this.bttRestore);
            this.tbParams.Controls.Add(this.txtRefresh);
            this.tbParams.Controls.Add(this.txtTc);
            this.tbParams.Controls.Add(this.txtHello);
            this.tbParams.Controls.Add(this.label11);
            this.tbParams.Controls.Add(this.label10);
            this.tbParams.Controls.Add(this.label9);
            this.tbParams.Controls.Add(this.label8);
            this.tbParams.Location = new System.Drawing.Point(0, 0);
            this.tbParams.Name = "tbParams";
            this.tbParams.Size = new System.Drawing.Size(240, 245);
            this.tbParams.Text = "OLSR Params";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Items.Add("Ingles");
            this.cmbLanguage.Items.Add("Italiano");
            this.cmbLanguage.Location = new System.Drawing.Point(122, 162);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(110, 22);
            this.cmbLanguage.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(7, 165);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 22);
            this.label12.Text = "Language";
            // 
            // bttSave
            // 
            this.bttSave.Location = new System.Drawing.Point(122, 199);
            this.bttSave.Name = "bttSave";
            this.bttSave.Size = new System.Drawing.Size(110, 27);
            this.bttSave.TabIndex = 12;
            this.bttSave.Text = "Save";
            this.bttSave.Click += new System.EventHandler(this.bttSave_Click);
            // 
            // bttRestore
            // 
            this.bttRestore.Location = new System.Drawing.Point(10, 199);
            this.bttRestore.Name = "bttRestore";
            this.bttRestore.Size = new System.Drawing.Size(110, 27);
            this.bttRestore.TabIndex = 11;
            this.bttRestore.Text = "Restore RFC";
            this.bttRestore.Click += new System.EventHandler(this.bttRestore_Click);
            // 
            // txtRefresh
            // 
            this.txtRefresh.Location = new System.Drawing.Point(122, 125);
            this.txtRefresh.Name = "txtRefresh";
            this.txtRefresh.Size = new System.Drawing.Size(110, 21);
            this.txtRefresh.TabIndex = 10;
            // 
            // txtTc
            // 
            this.txtTc.Location = new System.Drawing.Point(122, 91);
            this.txtTc.Name = "txtTc";
            this.txtTc.Size = new System.Drawing.Size(110, 21);
            this.txtTc.TabIndex = 9;
            // 
            // txtHello
            // 
            this.txtHello.Location = new System.Drawing.Point(122, 54);
            this.txtHello.Name = "txtHello";
            this.txtHello.Size = new System.Drawing.Size(110, 21);
            this.txtHello.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(7, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 32);
            this.label11.Text = "Refresh Interval";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(7, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 37);
            this.label10.Text = "TC Interval";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(7, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 35);
            this.label9.Text = "HELLO Interval";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(226, 22);
            this.label8.Text = "OLSR Configuration Parameters";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbNeigh
            // 
            this.tbNeigh.Controls.Add(this.label2);
            this.tbNeigh.Controls.Add(this.label1);
            this.tbNeigh.Controls.Add(this.lst2HopNeighbors);
            this.tbNeigh.Controls.Add(this.lstNeighbors);
            this.tbNeigh.Location = new System.Drawing.Point(0, 0);
            this.tbNeigh.Name = "tbNeigh";
            this.tbNeigh.Size = new System.Drawing.Size(240, 245);
            this.tbNeigh.Text = "Neighbors";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(133, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 38);
            this.label2.Text = "2 Hop Neighbors";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.Text = "Neighbors";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lst2HopNeighbors
            // 
            this.lst2HopNeighbors.Location = new System.Drawing.Point(133, 65);
            this.lst2HopNeighbors.Name = "lst2HopNeighbors";
            this.lst2HopNeighbors.Size = new System.Drawing.Size(91, 156);
            this.lst2HopNeighbors.TabIndex = 2;
            // 
            // lstNeighbors
            // 
            this.lstNeighbors.Location = new System.Drawing.Point(22, 63);
            this.lstNeighbors.Name = "lstNeighbors";
            this.lstNeighbors.Size = new System.Drawing.Size(91, 156);
            this.lstNeighbors.TabIndex = 1;
            // 
            // tbMPR
            // 
            this.tbMPR.Controls.Add(this.label3);
            this.tbMPR.Controls.Add(this.lstMPR);
            this.tbMPR.Location = new System.Drawing.Point(0, 0);
            this.tbMPR.Name = "tbMPR";
            this.tbMPR.Size = new System.Drawing.Size(240, 245);
            this.tbMPR.Text = "MPR";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(45, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 20);
            this.label3.Text = "MPR\'s";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lstMPR
            // 
            this.lstMPR.Location = new System.Drawing.Point(45, 44);
            this.lstMPR.Name = "lstMPR";
            this.lstMPR.Size = new System.Drawing.Size(151, 170);
            this.lstMPR.TabIndex = 1;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tbs);
            this.Menu = this.mainMenu1;
            this.Name = "StartScreen";
            this.Text = "OLSR";
            this.tbs.ResumeLayout(false);
            this.tbWorker.ResumeLayout(false);
            this.tbOLSR.ResumeLayout(false);
            this.tbParams.ResumeLayout(false);
            this.tbNeigh.ResumeLayout(false);
            this.tbMPR.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbs;
        private System.Windows.Forms.TabPage tbOLSR;
        private System.Windows.Forms.TabPage tbNeigh;
        private System.Windows.Forms.TabPage tbMPR;
        private System.Windows.Forms.ListBox lstNeighbors;
        private System.Windows.Forms.ListBox lstMPR;
        private System.Windows.Forms.ListBox lst2HopNeighbors;
        private System.Windows.Forms.ListBox lstInterfaces;
        private System.Windows.Forms.Button bttStop;
        private System.Windows.Forms.Button bttStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.TabPage tbWorker;
        private System.Windows.Forms.Button bttLoadConfig;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button bttJoin;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chckDebug;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tbParams;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRefresh;
        private System.Windows.Forms.TextBox txtTc;
        private System.Windows.Forms.TextBox txtHello;
        private System.Windows.Forms.Button bttSave;
        private System.Windows.Forms.Button bttRestore;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbLanguage;
    }
}