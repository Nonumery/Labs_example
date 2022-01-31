using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ISRPO_TCP_S
{
    internal class ClientHandler
    {
        public TcpClient clientSocket;

        public void RunClient()
        {
            StreamReader readerStream = new StreamReader(clientSocket.GetStream());
            NetworkStream writerStream = clientSocket.GetStream();
            string returnData = readerStream.ReadLine();
            string name = returnData;

            Console.WriteLine("Welcome to the server, " + name);

            while (true)
            {
                returnData = readerStream.ReadLine();

                if (returnData.IndexOf("QUIT")>-1)
                {
                    Console.WriteLine("Goodbye, " + name);
                    break;
                }

                Console.WriteLine(name + " : " + returnData);
                returnData += "\r\n";

                byte[] dataWrite = Encoding.ASCII.GetBytes(returnData);
                writerStream.Write(dataWrite, 0, dataWrite.Length);
            }
            clientSocket.Close();
        }
    }
}
