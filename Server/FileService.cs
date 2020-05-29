using System.Text;
using System;

namespace FileShareSystem.Server {
    public class FileService {
        private const string SHARE_DIRECTORY = "files";

        public void Hello() {
            // Do nothing.
        }

        public string ListDirectory(string directory) {
            Console.WriteLine("ListDirectory()");
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; ++i) {
                stringBuilder.Append("0");
            }
            string s = stringBuilder.ToString();
            return s;
        }

        public string ListRootDirectory() {
            return ListDirectory(SHARE_DIRECTORY);
        }

        public string PutFile(byte[] content, string newFilename) {
            Console.WriteLine("PutFile({0}, {1})", content, newFilename);
            return "";
        }

        public byte[] GetFile(string filename) {
            Console.WriteLine("GetFile({0})", filename);
            return null;
        }
    }
}
