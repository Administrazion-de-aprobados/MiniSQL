using System;
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
    public class MyTcpListener
    {

        public static DataBase database = null;
        public static User user = null;



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


                        string respuesta = serverParser(data);

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(respuesta);


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




        public static string serverParser (string sentence){


            String patternQuery = "<Query>(.+)</Query>";
            String patternClose = "<Close/>";
            String patternOpen = "<Open Database=\\\"(\\w+)\\\"\\sUser=\\\"(\\w+)\\\"\\sPassword=\\\"(\\w+)\\\"\\/>";



            if (Regex.IsMatch(sentence, patternOpen))
            {
                Match match = Regex.Match(sentence, patternOpen);
                string DatabaseName = match.Groups[1].Value;
                string userName = match.Groups[2].Value;
                string password = match.Groups[3].Value;


                if (database == null)
                {
                    database = DataBase.load(DatabaseName);
                    if (database != null)
                    {
                        database.loadSecurity();
                        database.loadUsers();
                        if (database.existUser(userName, password))
                        {
                            user = database.Users[userName];
                            string success = "<Success/>";
                            return success;
                        }
                        else
                        {
                            database = null;
                            string errorUsuario = "<Error>"+Constants.SecurityIncorrectLogin+"</Error>";
                            return errorUsuario;
                        }
                    }
                    else
                    {
                        DataBase nuevadb = new DataBase(DatabaseName, userName, password);
                        database = nuevadb;
                        user = database.admin;
                        string dbcreated = "<Success>" + Constants.CreateDatabaseSuccess + "</Success>";
                        return dbcreated;
                    }
                }
            }
            else if (Regex.IsMatch(sentence, patternQuery))
            {
                if (database != null && user != null)
                {
                    try
                    {
                        Match match = Regex.Match(sentence, patternQuery);
                        string querysentence = match.Groups[1].Value;
                        string output = database.output(querysentence, user);
                        string querysent = "<Answer>" + output + "</Answer>";
                        return querysent;
                    }
                    catch (Exception e)
                    {
                        string error = "<Answer><Error>" + e.Message + "</Error></Answer>";
                        return error;
                    }
                }
                else
                {
                    string error = "<Error>Login first to be able to send any query</Error>";
                    return error;
                }
            }
            else if (Regex.IsMatch(sentence, patternClose)) {
                database.write();
                database.writeSecurity();
                database.writeUsers();
                database = null;
                user = null;
                string closesentence = "<Close/>";
                return closesentence;
            }

            return "<Error>"+Constants.WrongSyntax+"</Error>";
        }





        }
}
