using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MobilePractices.OpenFileDialogEx;

namespace IconViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialogEx ofd = new OpenFileDialogEx();
            ofd.Filter = "*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
                MessageBox.Show("User selected: " + ofd.FileName, "Status");
            else
                MessageBox.Show("User canceled the dialog", "Status");
        }
    }
}