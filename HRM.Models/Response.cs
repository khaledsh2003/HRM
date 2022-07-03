
namespace HRM.Models
{
    //in models
    public class Response<T>
    {
        public Response(ErrorCodes errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
        }
        public Response(T data)
        {
            Data = data;

        }
        public T Data { get; set; }
        public ErrorCodes ErrorCode { get; set; } = 0;
        public string Description { get; set; }
    }
}


