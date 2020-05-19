﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Library;
using System.Text.RegularExpressions;

namespace ServerTCP
{
    class MyTcpListener
    {







        public static void Main()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 1200.
                Int32 port = 1200;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client. AQUI ABAJO LO DE QUE HACIAMOS ANTES








                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);


                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }




        public string serverParser (string sentence){

            String patternQuery = "<Query>([^<>]+)</Query>";
            String patternClose = "<Close/>";
            String patternOpen = "<Open\\sDatabase=”(\\w+)”\\sUser=”(\\w+)”\\sPassword=”(\\w+)”/>";



            if (Regex.IsMatch(sentence, patternOpen))
            {
                Match match = Regex.Match(sentence, patternOpen);
                string DatabaseName = match.Groups[1].Value;
                string userName = match.Groups[2].Value;
                string password = match.Groups[3].Value;




            }








            return null;
        }





        }
}
