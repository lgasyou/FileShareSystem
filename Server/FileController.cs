using System;
using System.Collections.Generic;

namespace FileShareSystem.Server {
    public class FileController {
        private readonly Dictionary<string, Func<Request, Response>> functions = new Dictionary<string, Func<Request, Response>>();

        private readonly FileService fileService = new FileService();

        public FileController() {
            functions.Add(RequestEnum.Get.ToString(), GetFile);
            functions.Add(RequestEnum.Hello.ToString(), Hello);
            functions.Add(RequestEnum.Put.ToString(), PutFile);
            functions.Add(RequestEnum.ListDirectory.ToString(), ListDirectory);
        }

        public Response Handle(Request request) {
            string operation = request.Operation;
            Func<Request, Response> func;
            if (functions.TryGetValue(operation, out func)) {
                try {
                    return func.Invoke(request);
                } catch (Exception e) {
                    return ResponseHelper.Error(e.ToString());
                }
            }
            return ResponseHelper.Error(operation + " unknown.");
        }

        public Response Hello(Request request) {
            fileService.Hello();
            return ResponseHelper.Ok();
        }

        public Response ListDirectory(Request request) {
            string result;
            if (request.Arguments.Length > 0) {
                result = fileService.ListDirectory(request.Arguments[0]);
            } else {
                result = fileService.ListRootDirectory();
            }
            return ResponseHelper.Ok(result);
        }

        public Response PutFile(Request request) {
            return fileService.PutFile(request.Data as byte[], request.Arguments[0]);
        }

        public Response GetFile(Request request) {
            byte[] result = fileService.GetFile(request.Arguments[0]);
            return ResponseHelper.Ok(result);
        }
    }
}
