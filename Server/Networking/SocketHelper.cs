using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Sockets;

namespace FileShareSystem.Server.Networking {
    class SocketHelper {
        private const int BUFFER_SIZE = 1024;

        private const int HEAD_SIZE = 4;

        public static void SendVariableData(Socket socket, byte[] data) {
            Int32 datagramLength = HEAD_SIZE + data.Length;
            byte[] lengthByte = BitConverter.GetBytes(datagramLength);

            List<byte> list = new List<byte>();
            list.AddRange(lengthByte);
            list.AddRange(data.Take(BUFFER_SIZE - HEAD_SIZE));
            byte[] segment = list.ToArray();
            socket.Send(segment);

            for (int i = BUFFER_SIZE - HEAD_SIZE; i < data.Length; i += BUFFER_SIZE) {
                segment = data.Skip(i).Take(BUFFER_SIZE).ToArray();
                socket.Send(segment);
            }
        }

        public static byte[] ReceiveVariableData(Socket socket) {
            byte[] buffer = new byte[BUFFER_SIZE];
            int size = socket.Receive(buffer) - HEAD_SIZE;
            int datagramSize = BitConverter.ToInt32(buffer.Take(HEAD_SIZE).ToArray());

            var result = new List<byte>();
            result.AddRange(buffer.Skip(HEAD_SIZE).Take(size));
            for (int i = BUFFER_SIZE; i < datagramSize; i += BUFFER_SIZE) {
                size = socket.Receive(buffer);
                result.AddRange(buffer.Take(size));
            }
            return result.ToArray();
        }
    }
}
