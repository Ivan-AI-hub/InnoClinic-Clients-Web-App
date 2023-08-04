namespace ClientsWebApp.Services
{
    public class ErrorModel
    {
        public int StatusCode { get; }
        public string Message { get; }
        public ErrorModel(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
