using System;
using System.ComponentModel;
using System.Windows.Forms;
using ManetProtocolAdaptors;
using MobilePractices.OpenFileDialogEx;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;
using Timer = System.Threading.Timer;

namespace Runner
{
    public partial class Runner : Form, ILogger
    {
        private Process process;
        private Thread logUpdaterThread;
        private bool logUpdaterExitFlag = false;
        private delegate void AddLineToLogDelegate(string line);
        private delegate void FinishRunDelegate(bool waitForLogger);
        private bool logChanged = false;



        private static bool stop = false;
        private IManetProtocol manetProtocol;
        private bool isServer = false;
        private bool isIntermediate = false;
        private static Timer timerClient;
        private static Timer timerServer;
        private static Timer timerPrintNeighbors;
        private static int timerInvokeCounter = 0;
        private IPAddress ipAd;
        private IPAddress ipServer;

        //Parameters
        private int paccketSize = 8192; // in bytes
        private int sendTime = 60; // in seconds
        private int dataPort = 8888; // in seconds


        public Runner()
        {
            InitializeComponent();
        }

        private void chooseFileButtonClick(object sender, EventArgs e)
        {
            OpenFileDialogEx ofd = new OpenFileDialogEx();
            ofd.Filter = "*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileNameTextBox.Text = ofd.FileName;
            }


        }

        private void runMenuItemClicked(object sender, EventArgs e)
        {
            if (runMenuItem.Text.Equals("Run"))
            {
                if (protocolsComboBox.Text != null)
                {
                    // Create an instance of the chosen protocol
                    manetProtocol = (IManetProtocol)System.Activator.CreateInstance(Type.GetType(ProtocolsNames.Protocols[protocolsComboBox.SelectedIndex]));
                }
                //manetProtocol = new OLSRProtocolAdaptor();

                runMenuItem.Text = "Stop";
                if (argsComboBox.Text != null)
                {
                    //Packet size parameter
                    int packetSizeIndex = argsComboBox.Text.IndexOf("-psz");
                    if (packetSizeIndex != -1)
                    {
                        int packetSizeEnd = argsComboBox.Text.IndexOf(" ", packetSizeIndex + 5);
                        string paccketSizeStr = packetSizeEnd == -1 ? argsComboBox.Text.Substring(packetSizeIndex + 5) : argsComboBox.Text.Substring(packetSizeIndex + 5, packetSizeEnd - packetSizeIndex - 5);
                        paccketSize = System.Convert.ToInt32(paccketSizeStr);
                    }

                    //sending time parameter
                    int sendTimeIndex = argsComboBox.Text.IndexOf("-t");
                    if (sendTimeIndex != -1)
                    {
                        int sendTimeEnd = argsComboBox.Text.IndexOf(" ", sendTimeIndex + 3);
                        string sendTimeStr = sendTimeEnd == -1 ? argsComboBox.Text.Substring(sendTimeIndex + 3) : argsComboBox.Text.Substring(sendTimeIndex + 3, sendTimeEnd - sendTimeIndex - 3);
                        sendTime = System.Convert.ToInt32(sendTimeStr);
                    }

                    //data port parameter
                    int dataPortIndex = argsComboBox.Text.IndexOf("-p");
                    if (dataPortIndex != -1)
                    {
                        int dataPortEnd = argsComboBox.Text.IndexOf(" ", dataPortIndex + 3);
                        string dataPortStr = dataPortEnd == -1 ? argsComboBox.Text.Substring(dataPortIndex + 3) : argsComboBox.Text.Substring(dataPortIndex + 3, dataPortEnd - dataPortIndex - 3);
                        dataPort = System.Convert.ToInt32(dataPortStr);
                    }

                    //local ip parameter
                    int ipIndex = argsComboBox.Text.IndexOf("-ip");
                    if (ipIndex != -1)
                    {
                        int ipEnd = argsComboBox.Text.IndexOf(" ", ipIndex + 4);
                        string ip = ipEnd == -1 ? argsComboBox.Text.Substring(ipIndex + 4) : argsComboBox.Text.Substring(ipIndex + 4, ipEnd - ipIndex - 4);
                        ipAd = IPAddress.Parse(ip);
                    }

                    // Start the protocol
                    manetProtocol.StartProtocol(ipAd, dataPort, paccketSize, this);
                    stop = false;
                    timerInvokeCounter = 0;
                    
                    isServer = argsComboBox.Text.Contains("-s");
                    isIntermediate = argsComboBox.Text.Contains("-inter");
                    if (!isServer)
                    {
                        //server ip parameter
                        int ipServerIndex = argsComboBox.Text.IndexOf("-c");
                        if (ipServerIndex != -1)
                        {
                            int ipServerEnd = argsComboBox.Text.IndexOf(" ", ipServerIndex + 3);
                            string ip = ipServerEnd == -1 ? argsComboBox.Text.Substring(ipServerIndex + 3) : argsComboBox.Text.Substring(ipServerIndex + 3, ipServerEnd - ipServerIndex - 3);
                            ipServer = IPAddress.Parse(ip);
                        }

                        // Initiate a timerClient set first time after 5 sec (warm up) and then run for 60 sec
                        if (!isIntermediate)
                        {
                            timerClient = new Timer(TimerEventHandlerClient, this, 5000, 60000);
                        }

                        //IPAddress ipAd = IPAddress.Parse("192.168.0.3");
                        //manetProtocol.StartProtocol(ipAd);

                        Thread sendThread = new Thread(new ThreadStart(SendRecevieMsg));
                        sendThread.Start();

                    }
                    else if(!isIntermediate)
                    {
                        //IPAddress ipAd = IPAddress.Parse("192.168.0.1");
                        //manetProtocol.StartProtocol(ipAd);

                        Thread sendThread = new Thread(new ThreadStart(SendRecevieMsg));
                        sendThread.Start();
                    }
                }
 
            }
            else
            {
                stop = true;
                stopMenuItemClicked(sender, e);
            }
            
        }

        private void SendRecevieMsg()
        {
            while (!manetProtocol.NeighborsExists() && !stop)
            {
            }

            //manetProtocol.PrintNeighbors();

            //timerPrintNeighbors = new Timer(TimerEventPrintNeighbors, this, 0, 5000); // Print the neighbors every 5 seconds
            timerPrintNeighbors = new Timer(TimerEventPrintRoutes, this, 0, 5000); // Print the neighbors every 5 seconds

            if(isIntermediate) return; // if this is an intermediate node don't do anything

            byte[] msg = new byte[8192-4];// the msg will contain 4 bytes of the dest address
            int counter = 0;

            if (!isServer)
            {
                //IPAddress idestIP = IPAddress.Parse("192.168.0.1");

                while (!stop)
                {
                    if (manetProtocol.SendMessage(ipServer, msg, msg.Length))
                    {
                        // count the sent message only 
                        counter++;
                        //addLineToLog(Convert.ToString(counter));
                    }
                }
            }
            else
            {
                byte[] messege = new byte[] { };
                int size = 0;
                Boolean first = true;
                while (!stop) // when the server runs we don't enable the timerClient so stop = true only when pressed
                {
                    counter++;
                    manetProtocol.RecevieMessage(ref messege, ref size);
                    if (messege != null)
                    {
                        if (first) // the first 4 bytes are the address
                        {
                            first = false;
                            //counter = 0;
                            timerServer = new Timer(TimerEventHandlerServer, this, 60000, 60000);
                        }
                        //addLineToLog(System.Text.Encoding.ASCII.GetString(messege, 0, size));
                        //addLineToLog(Convert.ToString(counter));
                    }
                }
                //addLineToLog(System.Text.Encoding.ASCII.GetString(messege, 0, size));
                //addLineToLog(Convert.ToString(counter));
            }
            addLineToLog("Bandwith:" + Convert.ToString((double)counter * 8192 / 60000) + "Kb/sec");
            //addLineToLog(Convert.ToString(timerInvokeCounter));
            stopMenuItemClicked(null, null);
        }

        private void TimerEventHandlerClient(Object myObject)//, EventArgs myEventArgs)
        {
            if (timerInvokeCounter >= 1)
            {
                stop = true;
                timerClient.Dispose();
            }
            timerInvokeCounter++;
        }

        private void TimerEventHandlerServer(Object myObject)//, EventArgs myEventArgs)
        {
            stop = true;
            timerServer.Dispose();
            ((Runner)myObject).manetProtocol.InteruptRecevieMessageBlock();
        }

        private void TimerEventPrintNeighbors(Object myObject)//, EventArgs myEventArgs)
        {
            ((Runner) myObject).addLineToLog("Neighbors: ");
            ((Runner)myObject).PrintNeighbors((Runner)myObject);
        }

        private void TimerEventPrintRoutes(Object myObject)//, EventArgs myEventArgs)
        {
            ((Runner)myObject).addLineToLog("Routes: ");
            ((Runner)myObject).PrintRoutes((Runner)myObject);
        }

        private void PrintNeighbors(Runner me)
        {
            foreach (var neighbor in me.manetProtocol.AvailableNeighbors())
            {
                addLineToLog(neighbor.ToString());
            }
        }

        private void PrintRoutes(Runner me)
        {
            foreach (var route in me.manetProtocol.AvailableRoutes())
            {
                addLineToLog(route.Key.ToString() + "->" + route.Value.ToString());
            }
        }

        private void stopMenuItemClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Send time is over or stop is pressed.\n Exit?", "runnerWM",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {            
                processExited(null,null);
                addLineToLog("Run was stopped");
            }
        }

        private void aboutMenuItemClicked(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void processExited(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                FinishRunDelegate d = new FinishRunDelegate(finishRun);
                this.Invoke(d, new object[] { false });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                finishRun(true);
            }

        }

        private void finishRun(bool waitForLogger)
        {
            runMenuItem.Text = "Run";
            argsComboBox.Enabled = true;
            //chooseFileButton.Enabled = true;

            logUpdaterExitFlag = true;

            timerPrintNeighbors.Dispose(); // stop the printing of the neighbors
            manetProtocol.EndProtocol();
            
            addLineToLog("************");
        }

        public void addLineToLog(String line)
        {
            if (this.logTextBox.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                AddLineToLogDelegate d = new AddLineToLogDelegate(addLineToLogThreadSafe);
                this.Invoke(d, new object[] { line });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                addLineToLogThreadSafe(line);
            }

        }

        private void addLineToLogThreadSafe(String line)
        {
//            if (logTextBox.Text.Length > 0)
//            {
//                logTextBox.Text += "\r\n";
//            }
            logTextBox.Text += line + "\r\n";
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
            logChanged = true;
        }

        private void updateLog(string outFileName, string errFileName)
        {
            while (logUpdaterExitFlag == false && !File.Exists(outFileName) 
                && !File.Exists(errFileName) )
            {
               // the files are not ready yet
               Thread.Sleep(500);
            }

            DateTime lastStdoutWriteTime = DateTime.MinValue;
            DateTime lastStderrWriteTime = DateTime.MinValue;

            int numStdoutLines = 0;
            int numStderrLines = 0;

            while (logUpdaterExitFlag == false)
            {
                readNewLines(outFileName, "+++ ", ref lastStdoutWriteTime, ref numStdoutLines, false);
                readNewLines(errFileName, "!!! ", ref lastStderrWriteTime, ref numStderrLines, false);
                
                Thread.Sleep(1000);
                // check if process has exited - trigger the event "Exited" if yes
                if (logUpdaterExitFlag == false)// && process.HasExited)
                {
                    // do nothing
                }
            }
            
            // last chance to get update from the logs
            readNewLines(outFileName, "+++ ", ref lastStdoutWriteTime, ref numStdoutLines, true);
            readNewLines(errFileName, "!!! ", ref lastStderrWriteTime, ref numStderrLines, true);
        }

        private void readNewLines(string fileName, string prefix, ref DateTime lastWriteTime, 
            ref int numLines, bool ignoreTimeCheck)
        {
            DateTime tmpWriteTime;
            try
            {
                tmpWriteTime = File.GetLastWriteTime(fileName);
            }
            catch (IOException)
            {
                // log files are not found (probably, there were deleted externally)
                return;
            }
            if (!ignoreTimeCheck && lastWriteTime != null && 
                lastWriteTime.CompareTo(tmpWriteTime) >= 0)
            {
                // no update since last read
                return;
            }

            string tmpFileName = fileName + ".tmp";
            File.Copy(fileName, tmpFileName, true);
            
            String line = "";
            StreamReader reader = File.OpenText(tmpFileName);

            for (int i = 0; i < numLines; i++)
            {
                // skip lines we have already seen
                reader.ReadLine();
            }

            try
            {
                while ((line = reader.ReadLine()) != null)
                {
                    addLineToLog(prefix + line);
                    numLines++;
                }
            }
            catch (IOException)
            {
                // do nothing
            }

            lastWriteTime = tmpWriteTime;
            reader.Dispose();
            File.Delete(tmpFileName);
        }

        private SaveFileDialog createSaveLogDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files|*.txt|All Files|*.*";
            dialog.FileName = "log";
            
            return dialog;
        }

        private void saveLogButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = createSaveLogDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                saveLog(dialog.FileName);
            }
        }

        private void saveLog(string logFileName)
        {
            using (StreamWriter logStream = new StreamWriter(logFileName))
            {
                logStream.Write(logTextBox.Text);
            }
            logChanged = false;
        }

        private void clearLogButtonClick(object sender, EventArgs e)
        {
            logTextBox.Text = "";
        }

        private void formClosing(object sender, CancelEventArgs e)
        {
            bool killProcess = false;
            string logFileName = "";

            if (process != null && !process.HasExited)
            {
                if (MessageBox.Show("Stop the running process?", "runnerWM",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    // cancel the Closing event from closing the form.
                    e.Cancel = true;
                    return;
                }
                killProcess = true;
            }


            // determine if text has changed in the textbox by comparing to original text.
            if (logChanged)
            {
                // display a MsgBox asking the user to save changes or abort.
                DialogResult result = MessageBox.Show("Save log before exit?", "runnerWM",
                   MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (result == DialogResult.Cancel) {
                    e.Cancel = true;
                    return;
                } else if (result == DialogResult.Yes) {
                    SaveFileDialog dialog = createSaveLogDialog();
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        // cancel the Closing event from closing the form.
                        e.Cancel = true;
                        return;
                    }

                    logFileName = dialog.FileName;
                }
            }

            if (killProcess)
            {
                process.Kill();
                finishRun(true);
            }

            if (logFileName.CompareTo("") != 0)
            {
                saveLog(logFileName);
            }
        }
    }


}