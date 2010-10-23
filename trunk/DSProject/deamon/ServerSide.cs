using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Daemon
{
    class ServerSide
    {
        //public static void Main()
        //{
        //    try
        //    {
        //        TcpClient tcpClient = new TcpClient();
        //        Console.WriteLine("Connecting...");

        //        tcpClient.Connect("localhost", 5001);
        //        Console.WriteLine("Connected");
        //        Console.Write("Enter the string to be transmitted : ");
        //        String str = Console.ReadLine();
        //        Stream stm = tcpClient.GetStream();

        //        ASCIIEncoding asen = new ASCIIEncoding();
        //        byte[] ba = asen.GetBytes(str);
        //        Console.WriteLine("Transmitting.....");

        //        stm.Write(ba, 0, ba.Length);

        //        byte[] bb = new byte[100];
        //        int k = stm.Read(bb, 0, 100);

        //        for (int i = 0; i < k; i++)
        //            Console.Write(Convert.ToChar(bb[i]));

        //        tcpClient.Close();
        //    }

        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error..... " + e.StackTrace);
        //    }
        //}

    }
}
