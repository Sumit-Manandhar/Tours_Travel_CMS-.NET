using System.Net;

namespace Travels.Models.Common
{
    public class ResponseModel<T>
    {
        public ResponseModel() { }
        public ResponseModel(T response)
        {
            Succeeded = true;
            Message = string.Empty;
            Data = response;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
