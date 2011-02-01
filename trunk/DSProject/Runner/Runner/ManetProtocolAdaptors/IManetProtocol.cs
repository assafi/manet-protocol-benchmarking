using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ManetProtocolAdaptors
{
    public interface IManetProtocol
    {
        void StartProtocol(IPAddress localIp, ILogger logger);
        void EndProtocol();
        bool SendMessage(IPAddress destIP, byte[] message, int messageSize);
        void RecevieMessage(ref byte[] message, ref int messageSize);
        void InteruptRecevieMessageBlock();

        // Service function of the protocol
        bool NeighborsExists();
        void PrintNeighbors();
    }
}
