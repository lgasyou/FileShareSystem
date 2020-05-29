using System.Text;
using Newtonsoft.Json;

namespace FileShareSystem.Server {
    public class FileShareBroker : IBroker {
        private readonly FileController fileController;

        public FileShareBroker(FileController fileController) {
            this.fileController = fileController;
        }

        public byte[] ProcessData(byte[] data) {
            string requestString = Encoding.UTF8.GetString(data, 0, data.Length);
            Request request = JsonConvert.DeserializeObject<Request>(requestString);
            Response response = fileController.Handle(request);
            string responseString = JsonConvert.SerializeObject(response);
            return Encoding.UTF8.GetBytes(responseString);
        }
    }
}
