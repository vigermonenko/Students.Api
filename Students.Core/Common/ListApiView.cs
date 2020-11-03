using System.Net;


namespace Students.Core.Common
{
    public class ListApiView<TPayload> : ApiView<TPayload>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalResults { get; set; }

        public int TotalPages => TotalResults / PageSize;


        public ListApiView(HttpStatusCode statusCode) : base(statusCode)
        {
        }
    }
}