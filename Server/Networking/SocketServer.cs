using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;

namespace FileShareSystem.Server.Networking {
    class SocketServer {

        private readonly Socket server;
        private readonly IBroker broker;
        private const int BACKLOG = 10;

        public SocketServer(IBroker broker) {
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
            server.Listen(BACKLOG);
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
