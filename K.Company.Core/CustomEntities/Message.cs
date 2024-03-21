using System.Net;

namespace K.Company.Core.CustomEntities
{
    public class Message
    {
        public bool IsSuccess { get; set; } = true;
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public string Title { get; set; } = HttpStatusCode.OK.ToString();
        public string Description { get; set; }
    }
}
