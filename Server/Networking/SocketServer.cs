using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;

namespace FileShareSystem.Server.Networking {
    class SocketServer {
        private Socket server;
        private Broker broker;

        public SocketServer(Broker broker) {
            this.broker = broker;
            server = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp
            );
            IPEndPoint iPEndPoint = new IPEndPoint(
                IPAddress.Any,
                0
            );
            server.Bind(iPEndPoint);
        }

        public void ListenAndProcess() {
            server.Listen(10);
            using (server) {
                var point = server.LocalEndPoint as IPEndPoint;
                Console.WriteLine("Listening port {0}.", point.Port);
                while (true) {
                    ProcessOnceAsync();
                }
            }
        }

        private async void ProcessOnceAsync() {
            try {
                Socket socket = server.Accept();
                await Transition(socket);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private Task Transition(Socket socket) {
            return Task.Run(() => {
                using (socket) {
                    byte[] received = SocketHelper.ReceiveVariableData(socket);
                    byte[] toBeSent = broker.ProcessData(received);
                    SocketHelper.SendVariableData(socket, toBeSent);
                }
            });
        }
    }
}
