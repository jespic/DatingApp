namespace API.errors
{
    public class ApiException
    {            //if we don't provide any message/detail, these properties are going to be set to null
        public ApiException(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}