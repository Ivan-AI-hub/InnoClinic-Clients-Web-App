using System.Net;

namespace ClientsWebApp.Application
{
    public class ManagerResult
    {
        public bool IsComplete => ErrorMessage != null;
        public string? ErrorMessage { get; }
        public HttpStatusCode StatusCode { get; }

        public ManagerResult(string? errorMessage, HttpStatusCode code)
        {
            ErrorMessage = errorMessage;
            StatusCode = code;
        }
    }
}
