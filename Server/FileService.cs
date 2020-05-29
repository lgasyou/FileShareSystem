using System.Text;
using System;
using System.IO;
using System.Linq;


namespace FileShareSystem.Server {
    public class FileService {
        private const string SHARE_DIRECTORY = "files";

        public void Hello() {
            // Do nothing.
        }

        public string ListDirectory(string directory) {
            object locker = new object();
            lock (locker)
            {
                Console.WriteLine("ListDirectory()");
                StringBuilder stringBuilder = new StringBuilder();
                string[] filenames = FindFile(directory);
                foreach (string filename in filenames)
                {
                    stringBuilder.Append(filename + " ");
                }
                string s = stringBuilder.ToString();
                return s;
            }
                
        }

        public string ListRootDirectory() {
            object locker = new object();
            lock (locker)
            {
                return ListDirectory(SHARE_DIRECTORY);
            }
        }

        public Response PutFile(byte[] content, string newFilename) {
            object locker = new object();
            lock(locker)
            {
                Console.WriteLine("PutFile({0}, {1})", content, newFilename);
                string[] filenames = FindFile(SHARE_DIRECTORY);
                if (filenames.ToList().IndexOf(newFilename) != -1) return ResponseHelper.Error("该文件名已存在");
                FileStream fs = new FileStream(SHARE_DIRECTORY, FileMode.Create);
                fs.Write(content);
                fs.Close();
                return ResponseHelper.Ok();
            }
           
        }

        public byte[] GetFile(string filename) {
            object locker = new object();
            lock (locker)
            {
                Console.WriteLine("GetFile({0})", filename);
                string[] filenames = FindFile(SHARE_DIRECTORY);
                if (filenames.ToList().IndexOf(filename) == -1) return null;
                string text = File.ReadAllText(SHARE_DIRECTORY + "/" + filename);
                byte[] content = Encoding.Default.GetBytes(text);
                return content;
            }
                
        }

        private string[] FindFile(string path)
        {
            string[] filenames = Directory.GetFiles(path);
            return filenames;
        }
    }
}
