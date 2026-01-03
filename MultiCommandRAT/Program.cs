using System;
using System.Net.Sockets;

namespace Program
{
    internal class Program
    {
        static TcpListener tcpListener;
        static Socket socketForClient;
        static NetworkStream networkStream;
        static StreamReader streamReader;

        static void Main(string[] args)
        {
            tcpListener = new TcpListener(System.Net.IPAddress.Any, 4444);
            tcpListener.Start();
            RunServer();

        }
        private static void ColoredConsoleOutput(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void RunServer()
        {
            socketForClient = tcpListener.AcceptSocket();
            networkStream = new NetworkStream(socketForClient);
            streamReader = new StreamReader(networkStream);

            try
            {
                string line;
                while (true)
                {
                    line = "";
                    line = streamReader.ReadLine();
                    if(line.Contains("m"))
                        ColoredConsoleOutput("Message: " + line, ConsoleColor.Green);
                    if (line.Contains("b"))
                    {
                        ColoredConsoleOutput("Message: " + line, ConsoleColor.Yellow);
                        Console.Beep(500, 2000);
                    }
                    if(line.Contains("q"))
                    {
                        ColoredConsoleOutput("Message: " + line, ConsoleColor.Red);
                        streamReader.Close();
                        networkStream.Close();
                        socketForClient.Close();
                        tcpListener.Stop();
                        Environment.Exit(0);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                streamReader.Close();
                networkStream.Close();
                socketForClient.Close();
                tcpListener.Stop();
            }
        }
    }
}