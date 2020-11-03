using System.Net;


namespace Students.Core.Common
{
    public class ApiView<TPayload>
    {
        public string Message { get; set; }

        public TPayload Payload { get; set; }

        public int Status { get; private set; }

        public ApiView(HttpStatusCode statusCode)
        {
            Status = (int) statusCode;
        }
    }
}