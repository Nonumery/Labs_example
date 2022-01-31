using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ISRPO_TCP
{

    internal class Program
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in AuthenticationManager system!");
        }
        const int ECHO_PORT = 8080;
        static void Main(string[] args)
        {
            Console.WriteLine("Your Name:");
            string name = Console.ReadLine();
            Console.WriteLine("---Logged In---");
            try
            {
                TcpClient eClient = new TcpClient(/*"127.0.0.1"*/ GetLocalIPAddress(), ECHO_PORT);
                StreamReader readerStream = new StreamReader(eClient.GetStream());
                NetworkStream writerStream = eClient.GetStream();

                string dataToSend;

                dataToSend = name;
                dataToSend += "\r\n";

                byte[] data = Encoding.ASCII.GetBytes(dataToSend);

                writerStream.Write(data,0, data.Length);

                while (true)
                {
                    Console.Write(name + ":");

                    dataToSend = Console.ReadLine();
                    dataToSend += "\r\n";

                    data = Encoding.ASCII.GetBytes(dataToSend);
                    writerStream.Write(data, 0, data.Length);

                    if (dataToSend.IndexOf("QUIT") > -1) break;
                    string returnData;
                    returnData = readerStream.ReadLine();
                    Console.WriteLine("Server: " + returnData);
                }
                eClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            
        }
    }
}
