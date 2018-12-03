using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace RoverTransmitter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string fileName = args[0];
            Console.WriteLine(fileName);

            try
            {
                string[] lines = System.IO.File.ReadAllLines(fileName);
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 9999);

                Console.WriteLine("Connected");

                foreach (string line in lines)
                {

                    Stream stm = tcpclnt.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(line);
                    Console.WriteLine("Transmitting.....");

                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[256];
                    int k = stm.Read(bb, 0, 256);

                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(bb[i]));
                    }
                }
                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}
