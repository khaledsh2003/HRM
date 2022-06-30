namespace HRM.Responses
{

    public class Response<T>
    {
        public T Data { get; set; }
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}


