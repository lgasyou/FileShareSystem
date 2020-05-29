namespace FileShareSystem.Server {
    public struct Response {
        public string Message;

        public int Status;

        public object Data;
    }

    public class ResponseHelper {
        public static Response Ok(object data) {
            return new Response{
                Message = "Ok",
                Status = 200,
                Data = data
            };
        }

        public static Response Ok() {
            return new Response{
                Message = "Ok",
                Status = 200,
                Data = null
            };
        }

        public static Response Error(string message) {
            return new Response{
                Message = message,
                Status = 400,
                Data = null
            };
        }
    }
}
