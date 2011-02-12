using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ManetProtocolAdaptors
{
    public interface IManetProtocol
    {
        /// <summary>
        /// Starts the protocol
        /// </summary>
        /// <param name="localIp">Local ip of the device</param>
        /// <param name="dataPort">Port on which the data is send</param>
        /// <param name="bufferSize">The size of the inner buffer, equals to the largest size of the sending packet</param>
        /// <param name="logger">An instance of a ILogger interface that is used for logging</param>
        /// <returns></returns>
        void StartProtocol(IPAddress localIp, int dataPort, int bufferSize, ILogger logger);

        /// <summary>
        /// End the run of the protocol
        /// </summary>
        /// <returns></returns>
        void EndProtocol();

        /// <summary>
        /// Sends a message to the specified destination ip
        /// </summary>
        /// <param name="destIp">destination ip</param>
        /// <param name="message">message  of size messageSize</param>
        /// <param name="messageSize">message size</param>
        /// <returns></returns>
        bool SendMessage(IPAddress destIp, byte[] message, int messageSize);

        /// <summary>
        /// Receive a message
        /// </summary>
        /// <param name="message">Message received</param>
        /// <param name="messageSize">Message size</param>
        /// <returns></returns>
        void RecevieMessage(ref byte[] message, ref int messageSize);

        /// <summary>
        /// If RecevieMessage is blocking - unblocks the last invocation 
        /// </summary>
        /// <returns></returns>
        void InteruptRecevieMessageBlock();

        /* Service function of the protocol */

        /// <summary>
        /// Checks if there are neighbors.
        /// </summary>
        /// <returns>true if neighbors exists, false otherwise</returns>
        bool NeighborsExists();

        /// <summary>
        /// Returns the available neighbors.
        /// </summary>
        /// <returns></returns>
        List<IPAddress> AvailableNeighbors();

        /// <summary>
        /// Returns the available routes
        /// </summary>
        /// <returns>Map between the destination and the next hop</returns>
        Dictionary<IPAddress, IPAddress> AvailableRoutes();
    }
}
