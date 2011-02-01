﻿using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using OLSR.OLSR;
using OLSR.OLSR.Packets.Messages.Neighbors;
using OLSR.OLSR.RoutingTable;
using OLSR.Screens;

namespace ManetProtocolAdaptors
{
    public class OLSRProtocolAdaptor : IManetProtocol
    {
        private const int _dataPort = 8888;
        private const int BufferSize = 8192;
        private Thread _olsrDataThread;

        IPEndPoint ipep;
        private UdpClient _dataSocket;
        
        
        private IPAddress _localIp;
        private AutoResetEvent _dataRecived;
        private bool _finish = false;
        private byte[] _msg;
        private ILogger _logger;

        public OLSRProtocolAdaptor()
        {

        }

        public void StartProtocol(IPAddress localIp, ILogger logger)
        {
            this._logger = logger;
            this._localIp = localIp;
            StartScreen.GetInstance().ConfigureLocalIface(localIp);
            StartScreen.GetInstance().bttStart_Click(null, null);
            _dataRecived = new AutoResetEvent(false);

            ipep = new IPEndPoint(_localIp, _dataPort);
            _dataSocket = new UdpClient(ipep);
            
            _olsrDataThread = new Thread(new ThreadStart(farwardData));
            _logger.addLineToLog("StartProtocol: OLSR Started");
            _olsrDataThread.Start();
        }

        private void farwardData()
        {
            _logger.addLineToLog("farwardData: Start");
            IPEndPoint receivedIp = new IPEndPoint(IPAddress.Any, _dataPort);
            try
            {
                while (!_finish)
                {
                    var destIpStr = new byte[4]; // the destination address is the first 4 bytes
                    var message = new byte[BufferSize];

                    message = _dataSocket.Receive(ref receivedIp);
                    //_logger.addLineToLog("farwardData: received from" + receivedIp.ToString());

                    System.Buffer.BlockCopy(message, 0, destIpStr, 0, 4);

                    var destIp = new IPAddress(destIpStr);
                    //_logger.addLineToLog("farwardData: to " + destIp.ToString());
                    if (_localIp.Equals(destIp))
                    {
                        _msg = message;
                        _dataRecived.Set();
                    }

                    SendMessage(destIp, message, message.Length);
                }
            }
            catch (SocketException e)
            {
                _logger.addLineToLog(e.ErrorCode.ToString());
            }
            _logger.addLineToLog("farwardData: End");
        }

        public void EndProtocol()
        {
            _finish = true;
            if (_dataSocket != null)
                _dataSocket.Close();

            if(_olsrDataThread != null)
                _olsrDataThread.Join(1000);

            StartScreen.GetInstance().bttStop_Click(null, null);
            _logger.addLineToLog("EndProtocol: Protocol stopped");
        }

        public bool SendMessage(IPAddress destIP, byte[] message, int messageSize)
        {
            /*
             * 1.Get the routes
             * 2.Check if a rout exists to the destIP
             * 3.Find the next IP(hop) to send him the data
             * 4.Send the data over the opend socket
             */

            bool routFound = false;
            //_logger.addLineToLog("SendMessage: Start");
            ArrayList newRoutes = RoutingTableCalculation.GetInstance().CalculateTableRoute();
            foreach (Route route in newRoutes)
            {
                //_logger.addLineToLog("SendMessage: rout dest " + route.R_dest_addr_.ToString());
                if (route.R_dest_addr_.Equals(destIP))
                {
                    routFound = true;
                    var targetEndPoint = new IPEndPoint(route.R_dest_addr_, _dataPort);

                    int sendSize = 0;

                    // Append the destination address to the begging of the message
                    byte[] sendMsg = new byte[destIP.GetAddressBytes().Length + message.Length];
                    System.Buffer.BlockCopy(destIP.GetAddressBytes(), 0, sendMsg, 0, destIP.GetAddressBytes().Length);
                    System.Buffer.BlockCopy(message, 0, sendMsg, destIP.GetAddressBytes().Length, 1);

                    do
                    {
                        //_logger.addLineToLog("SendMessage: sending data");
                        sendSize = _dataSocket.Send(sendMsg, sendMsg.Length, targetEndPoint);

                    } while (sendSize < sendMsg.Length);
                }
            }

            //_logger.addLineToLog("SendMessage: end");
            return routFound;
        }

        public void PrintNeighbors()
        {
            lock (OLSRParameters.NeighborList)
            {
                for (int i = OLSRParameters.NeighborList.Count - 1; i > -1; i--)
                {
                    Neighbor neig = (Neighbor) OLSRParameters.NeighborList[i];
                    _logger.addLineToLog(neig.GetN_neighbor_iface_addr().ToString());
                }

            }
        }

        public bool NeighborsExists()
        {
            lock (OLSRParameters.NeighborList)
            {
                return OLSRParameters.NeighborList.Count > 0;

            }
        }

        public void RecevieMessage(ref byte[] message, ref int messageSize)
        {
            /*
             * 1.check if there is any data in the data socket. 
             * 2.receive the data.
             * 3.put the data inside message
             */
            //_logger.addLineToLog("RecevieMessage: Start");
            IPEndPoint endPoint = null;
            //_logger.addLineToLog("RecevieMessage: waiting data");
            _dataRecived.WaitOne();
            //_logger.addLineToLog("RecevieMessage:Data received");
            message = _msg;
            messageSize = message.Length;
            //_logger.addLineToLog("RecevieMessage: end");
        }

        public void InteruptRecevieMessageBlock()
        {
            _dataRecived.Set();
        }
    }
}
