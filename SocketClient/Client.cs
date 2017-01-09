using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketClient
{
    class Client
    {
        //Ip address vi tilgår
        private string _serverName;
        //Porten vi tilgår
        private int _port;

        //Writer og reader til at sende og modtage data
        private StreamWriter _writer;
        private StreamReader _reader;
        private string _serverMessage;
        private bool _running = false;




        public Client(string serverName, int port)
        {
            _serverName = serverName;
            _port = port;
        }

        public void Connect()
        {
            //Vi kalder denne klientens forbindelse til serveren
            TcpClient server = new TcpClient(_serverName, _port);
            //Den strøm i hvilken vores data sendes
            NetworkStream stream = server.GetStream();
            //instantialiesering af reader og writer
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream);

            StartRecieveing();
        }

        public void StartRecieveing()
        {
            //Starter en tråd der sørger for at klienten får beskeder tilbage fra serveren.
            Thread recieveThread = new Thread(ReadFromServer);
            //Starter tråden
            recieveThread.Start();

            Thread startRunning = new Thread(Run);
            startRunning.Start();

        }
        public void ReadFromServer()
        {
            _running = true;
            while (_running)
            {
                _serverMessage = _reader.ReadLine();
                //Printer til konsollen hvad vi har fået
                Console.WriteLine(_serverMessage);
            }


        }
        // Run metoden som skal startes fra main.
        public void Run()
        {
            string msg = "";
            while (msg != "quit")
            {
                msg = Console.ReadLine();
                if (msg != "quit")
                {
                    SendRequest(msg);
                }
                else
                {
                    _reader.Close();
                    _writer.Close();
                    Environment.Exit(0);
                }
            }

        }

        // Sender requests til serveren
        public void SendRequest(string msg)
        {

            _writer.WriteLine(msg);
            _writer.Flush();
        }
    }
}
