using FileShareSystem.Server.Networking;

namespace FileShareSystem.Server {
    class ServerMain {
        static void Main(string[] args) {
            FileController fileController = new FileController();
            IBroker broker = new FileShareBroker(fileController);
            SocketServer socketServer = new SocketServer(broker);
            socketServer.ListenAndProcess();
        }
    }
}
