using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Consola
{
    class Program
    {
        public static void Main(string[] args)
        {

            String patternQuery = "<Query>([^<>]+)</Query>";
            String patternClose = "<Close/>";


            try
            {
                String IP, Me = null;

                Console.WriteLine("IP");
                IP = Console.ReadLine();
                Int32 port = 1200;
                TcpClient client = new TcpClient(IP, port);

                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.WriteLine("Mensaje");
                    Me = Console.ReadLine();

                    if (Regex.IsMatch(Me, patternClose))
                    {
                        stream.Close();
                        client.Close();
                        break;
                    }
                    else if (Regex.IsMatch(Me, patternQuery))
                    {
                        Match match = Regex.Match(Me, patternQuery);

                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(match.Groups[1].Value);
                        stream.Write(data, 0, data.Length);

                        data = new Byte[256];
                        String responseData = String.Empty;

                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("<Answer>" + responseData + "</Answer>");

                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();

        }
    }
}
