namespace Domain.Models
{
    public class BaseServiceResponse
    {
        public string Message { get; set; }

        public BaseServiceResponse() { }

        public BaseServiceResponse(string message)
        {
            Message = message;
        }
    }


    public class BaseServiceResponse<T> : BaseServiceResponse
    {
        public T Data { get; set; }

        public BaseServiceResponse(string message) : base(message)
        {
        }

        public BaseServiceResponse(T data, string message) : base(message)
        {
            Data = data;
        }

        public BaseServiceResponse()
        {
        }
    }
}
