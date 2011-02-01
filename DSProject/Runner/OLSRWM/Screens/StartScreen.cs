using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
using OLSR.Communication;
using OLSR.Configuration;
using OLSR.OLSR;
using OLSR.OLSR.RoutingTable;
using OpenNETCF.Net.NetworkInformation;

namespace OLSR.Screens
{
    /// <summary>
    /// 
    /// Created by  APIF Moviquity S.A.
    /// 
    /// http://www.moviquity.com/
    /// 
    /// Developers:
    /// 
    ///     Alberto Martinez Garcia
    ///     Francisco Abril Bucero 
    ///     Jose Manuel Lopez Garcia
    /// 
    ///
    /// Version: 1.2
    ///
    /// </summary>
    public partial class StartScreen : Form
    {
        # region  Variables
        
        [DllImport("routewin.dll", EntryPoint = "resetDevice")]
        private static extern bool resetDevice(int num);

        private readonly ControlInvoker controlInvoker = null;

        public static StartScreen instance = null;

        private readonly string DIRECTORY = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

        private readonly string PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\blockedIPs.txt";

        #endregion

        /// <summary>
        /// Metodo encargado de controlar la instancia de la clase
        /// </summary>
        /// <returns>Nueva StartScreen o la instancia existente</returns>
        public static StartScreen GetInstance()
        {
            if (instance == null)
                instance = new StartScreen();
            return instance;
        }

        /// <summary>
        /// Metodo que devuelve el control de la clase para que la pantalla sea modificada
        /// </summary>
        /// <returns>ControlInvoker</returns>
        public ControlInvoker GetControlInvoker()
        {
            return controlInvoker;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public StartScreen()
        {
            controlInvoker = new ControlInvoker();
            controlInvoker.SetControl(this);

            //Variable de objeto que contiene el socket
            OLSRParameters.SocketControler = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //Separamos el puerto 698 para usarlo en nuestra aplicación
            OLSRParameters.SocketControler.Bind(new IPEndPoint(IPAddress.Any, OLSRConstants.OLRS_PORT));

            //Habilitamos la opción Broadcast para el socket
            OLSRParameters.SocketControler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);

            InitializeComponent();

            //Recargamos listado de interfaces con las encontradas en el equipo
            LoadInterfaces();

            //Cargamos parametros de configuracion
            txtHello.Text = PropertiesReader.getInstance().getCurrentHelloInterval();
            txtTc.Text = PropertiesReader.getInstance().getCurrentTCInterval();
            txtRefresh.Text = PropertiesReader.getInstance().getCurrentRefreshInterval();

            OLSRParameters.Language = PropertiesReader.getInstance().getCurrentLanguage();

            LoadLanguage();

        }

        private void LoadLanguage()
        {
            if (OLSRParameters.Language.Equals("Italian")||OLSRParameters.Language.Equals("Italiano"))
            {
                OLSRParameters.Language = "Italian";
                RelaodLanguage();   
            }
            else if(OLSRParameters.Language.Equals("English")||OLSRParameters.Language.Equals("Inglese") )
            {
                OLSRParameters.Language = "English";
                RelaodLanguage();
            }
        }

        private void RelaodLanguage()
        {
            tbs.TabPages[0].Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workertab");
            label4.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workerid");
            label6.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workerteam");
            label5.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workergroups");
            bttJoin.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workerjoin");
            bttLoadConfig.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "workerload");

            tbs.TabPages[1].Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "olsrtab");
            label7.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "olsrifaces");
            chckDebug.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "olsrtest");
            bttStart.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "olsrstart");
            bttStop.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "olsrstop");

            tbs.TabPages[2].Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramstab");
            label8.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramstitle");
            label9.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramshello");
            label10.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramstc");
            label11.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramsrefresh");
            label12.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramslanguage");

            String textLenguages = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramslanguages");
            String[] languages = textLenguages.Split(',');
            cmbLanguage.Items.Clear();
            for (int x = 0; x < languages.Length; x++)
            {
                cmbLanguage.Items.Add(languages[x]);
            }

            if (OLSRParameters.Language.Equals("English"))
                cmbLanguage.SelectedIndex = 0;
            else
                cmbLanguage.SelectedIndex = 1;

            bttRestore.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramsrestore");
            bttSave.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "paramssave");

            tbs.TabPages[3].Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "neighbortab");
            label1.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "neighborlink");
            label2.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "neighborsecond");

            tbs.TabPages[4].Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "mprtab");
            label3.Text = LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "mprlist");
        }

        /// <summary>
        /// Metodo que se encarga de parar la aplicacion por completo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, EventArgs e)
        {
            RoutingTableCalculation.GetInstance().DeleteExistRoutingTable();

            InfoSender.GetInstance().StopThread();

            InfoReceiver.GetInstance().StopThread();

            Application.Exit();
        }

        # region OLSR

        /// <summary>
        /// Metodo encargado de rellenar el listado de interfaces con las interfaces 
        /// disponibles en nuestra máquina
        /// </summary>
        private void LoadInterfaces()
        {
            //borramos el listado de interfaces
            lstInterfaces.Items.Clear();
            //leemos todas las interfaces de red de nuestra máquina

            NetworkInterface[] netInterfaces = (NetworkInterface[]) NetworkInterface.GetAllNetworkInterfaces();

            //recorremos las interfaces y las tratamos, omitiendo 127.0.0.1
            foreach (NetworkInterface net in netInterfaces)
            {
                try
                {
                    IPAddress ip = net.CurrentIpAddress;
                    if (!ip.Equals(IPAddress.Loopback))
                        lstInterfaces.Items.Add(ip);
                }
                catch
                { }
            }
        }

        public void ConfigureLocalIface(IPAddress localIp)
        {
            lstInterfaces.Items.Clear();
            lstInterfaces.Items.Add(localIp);
            lstInterfaces.SelectedIndex = 0;
        }

        /// <summary>
        /// Metodo que se ejecuta cada vez que se pulsa el boton "START" pestaña OLSR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bttStart_Click(object sender, EventArgs e)
        {
            if (lstInterfaces.SelectedIndex != -1)
            {

                OLSRConstants.HELLO_INTERVAL = Convert.ToInt32(txtHello.Text);
                OLSRConstants.TC_INTERVAL = Convert.ToInt32(txtTc.Text);
                OLSRConstants.REFRESH_INTERVAL = Convert.ToInt32(txtRefresh.Text);

                OLSRConstants.UpdateParameters();

                //if(chckDebug.Checked)
                //{
                    try
                    {
                        //Leemos IPs que queremos bloquear
                        var sr = new StreamReader(PATH); //CARGAR IPS BLOQUEADAS
                        string texto = sr.ReadToEnd();
                        string[] values = texto.Split(',');
                        for (int x = 0; x < values.Length; x++)
                            OLSRParameters.BlockedIPs.Add(IPAddress.Parse(values[x]));

                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "errortestfile"));
                    }
                //}

                //bttStart.Enabled = false;
                //bttStop.Enabled = true;

                InfoSender.GetInstance().StartThread();

                InfoReceiver.GetInstance().StartThread();
            }

        }

        /// <summary>
        /// Metodo que se ejecuta cada vez que se pulsa el boton "STOP" pestaña OLSR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bttStop_Click(object sender, EventArgs e)
        {

            RoutingTableCalculation.GetInstance().DeleteExistRoutingTable();

            //bttStart.Enabled = true;
            //bttStop.Enabled = false;

            InfoSender.GetInstance().StopThread();

            InfoReceiver.GetInstance().StopThread();

            //TODO - BLOCK

            OLSRParameters.LinksList = new ArrayList();
            OLSRParameters.MPRSet = new ArrayList();
            OLSRParameters.NeighborList = new ArrayList();
            OLSRParameters.SecondHopNeighborList = new ArrayList();
            OLSRParameters.TopologySet = new ArrayList();

            OLSRParameters.SocketControler.Close();

            instance = null;

            //lstNeighbors.Items.Clear();
            //lst2HopNeighbors.Items.Clear();
            //lstMPR.Items.Clear();
        }

        /// <summary>
        /// Metodo que comprueba si se ha seleccionado una IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstInterfaces.SelectedIndex != -1)
            {
                OLSRParameters.Originator_Addr = IPAddress.Parse(lstInterfaces.SelectedItem.ToString());
                bttStart.Enabled = true;
            }
            else
                MessageBox.Show(LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "selectiface"));
        }

        
        #endregion

        # region OLSR - Neighbosr

        public void PrintNeighbors(object[] arguments)
        {
            lstNeighbors.Items.Clear();
            foreach (string arg in arguments)
            {
                lstNeighbors.Items.Add(arg);
            }

        }

        public void Print2HopNeighbors(object[] arguments)
        {
            lst2HopNeighbors.Items.Clear();
            foreach (string arg in arguments)
            {
                lst2HopNeighbors.Items.Add(arg);
            }
        }

        #endregion

        # region OLSR - MPR

        public void PrintMPRSet(object[] arguments)
        {
            lstMPR.Items.Clear();
            foreach (string arg in arguments)
            {
                lstMPR.Items.Add(arg);
            }
        }

        #endregion

        #region Worker

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 2; i < numericUpDown1.Value + 2; i++)
            {
                listBox1.Items.Add("Worker " + i);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int i = 1; i < numericUpDown2.Value + 1; i++)
            {
                listBox2.Items.Add("Group " + i);
            }
        }

        private void bttJoin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(LanguageConfiguration.getInstance().getText(OLSRParameters.Language, "reboot"), "Alert");

            string id2 = null;
            string id1 = null;
            int tmpId = listBox1.SelectedIndex;
            id2 = (string)listBox1.SelectedItem;
            if (tmpId != -1)
            {
                id2 = id2.Split(' ')[1];
            }
            tmpId = listBox2.SelectedIndex;
            id1 = (string)listBox2.SelectedItem;
            if (tmpId != -1)
            {
                id1 = id1.Split(' ')[1];
            }

            string texto = "192.168." + id1 + "." + id2 + "*" + (numericUpDown1.Value) + "*" + (numericUpDown2.Value);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(DIRECTORY + "\\LoadParams.cfg");
            sw.WriteLine(texto);
            sw.Close();

            #region Accediendo al Registro y Reseteando
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rkWifi, rkComm;
            string[] tmp, values;
            string name = null;
            string keyWifi = null;
            rkComm = rk1.OpenSubKey(@"Comm");
            tmp = rkComm.GetSubKeyNames();
            for (int i = 0; i < rkComm.SubKeyCount; i++)
            {
                rkWifi = rkComm.OpenSubKey(tmp[i], false);
                values = rkWifi.GetValueNames();
                for (int x = 0; x < values.Length; x++)
                {
                    if (values[x].Equals("Wireless"))
                    {
                        keyWifi = tmp[i];
                        name = (string)rkWifi.GetValue("DisplayName");
                    }
                }
            }
            rkWifi = rk1.OpenSubKey(@"Comm\\" + keyWifi + "\\Parms\\TCPIP", true);
            values = rkWifi.GetValueNames();
            for (int x = 0; x < values.Length; x++)
            {
                if (values[x].Equals("IpAddress"))
                {
                    rkWifi.SetValue("IpAddress", "192.168." + id1 + "." + id2);
                }
                else if (values[x].Equals("DefaultGateway"))
                {
                    rkWifi.SetValue("DefaultGateway", "0.0.0.0");
                }
                else if (values[x].Equals("Subnetmask"))
                {
                    rkWifi.SetValue("Subnetmask", "255.255.255.0");
                }
            }
            resetDevice(1256895683);

            #endregion

        }

        private void bttLoadConfig_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(DIRECTORY + "\\LoadParams.cfg", System.Text.Encoding.Default);
            string texto = sr.ReadToEnd();
            sr.Close();

            string[] data = texto.Split('*');

            int team = Int32.Parse(data[1]);
            int groups = Int32.Parse(data[2]);

            numericUpDown1.Value = team;
            numericUpDown1_ValueChanged(sender, e);

            numericUpDown2.Value = groups;
            numericUpDown2_ValueChanged(sender, e);
        }

        #endregion

        # region  Languaje

        private void bttRestore_Click(object sender, EventArgs e)
        {
            txtHello.Text = PropertiesReader.getInstance().getDefaultHelloInterval();
            txtTc.Text = PropertiesReader.getInstance().getDefaultTCInterval();
            txtRefresh.Text = PropertiesReader.getInstance().getDefaultRefreshInterval();
            cmbLanguage.SelectedIndex = 1;
        }

        private void bttSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                PropertiesReader.getInstance().setHelloInterval(txtHello.Text);
            }
            catch (Exception)
            {
                PropertiesReader.getInstance().setHelloInterval(PropertiesReader.getInstance().getDefaultHelloInterval());
            }

            try
            {
                PropertiesReader.getInstance().setTCInterval(txtTc.Text);
            }
            catch (Exception)
            {
                PropertiesReader.getInstance().setTCInterval(PropertiesReader.getInstance().getDefaultTCInterval());
            }

            try
            {
                PropertiesReader.getInstance().setRefreshInterval(txtRefresh.Text);
            }
            catch (Exception)
            {
                PropertiesReader.getInstance().setRefreshInterval(PropertiesReader.getInstance().getDefaultRefreshInterval());
            }

            try
            {
                PropertiesReader.getInstance().setLanguage(cmbLanguage.Text);
            }
            catch (Exception)
            {
                PropertiesReader.getInstance().setLanguage(PropertiesReader.getInstance().getDefaultLanguage());
            }

            PropertiesReader.getInstance().saveFile();

            if (!OLSRParameters.Language.Equals(PropertiesReader.getInstance().getCurrentLanguage()))
            {
                OLSRParameters.Language = PropertiesReader.getInstance().getCurrentLanguage();
                LoadLanguage();
            }
            Cursor.Current = Cursors.Default;
        }

        # endregion

    }
}