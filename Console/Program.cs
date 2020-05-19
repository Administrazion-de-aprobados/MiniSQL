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
            Task.Delay(1000).Wait();
            String patternClose = "<Close/>";

            Boolean database = false;

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
                    if (database is false)
                    {
                        Console.WriteLine("Login into Database");
                    }
                    else
                    {
                        Console.WriteLine("Insert query");
                    }


                    Me = Console.ReadLine();

                    if (Regex.IsMatch(Me, patternClose) && database is true)
                    {
                        database = false;

                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(Me);
                        stream.Write(data, 0, data.Length);


                        data = new Byte[256];
                        String responseData = String.Empty;

                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine(responseData);
                        Console.WriteLine("");




                    }
                    else if(Regex.IsMatch(Me, patternClose) && database is false)
                    {
                        stream.Close();
                        client.Close();
                        break;
                    }
                    else
                    {

                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(Me);
                        stream.Write(data, 0, data.Length);

                        data = new Byte[256];
                        String responseData = String.Empty;

                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                        if (responseData.Equals("<Success>Database created</Success>"))
                            database = true;
                        else if (responseData.Equals("<Success/>"))
                            database = true;

                        Console.WriteLine( responseData );
                        Console.WriteLine("");
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
