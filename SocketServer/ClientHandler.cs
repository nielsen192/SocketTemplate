using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    internal class ClientHandler
    {
        // Vores forbindelse til den enkelte klient
        private Socket requests;

        private NetworkStream stream;
        private bool _stopped = false;

        // Det vi bruger til at læse og skrive til klienten med
        private StreamReader reader;
        private StreamWriter writer;

        public ClientHandler(Socket requests)
        {
            this.requests = requests;
            stream = new NetworkStream(requests);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        // Køres sammen med klientens tråd
        public void RunClient()
        {
            Console.WriteLine(">> Client connected");

            while (!_stopped)
            {
                try
                {
                    string clientText = reader.ReadLine();
                    Console.WriteLine(clientText);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
