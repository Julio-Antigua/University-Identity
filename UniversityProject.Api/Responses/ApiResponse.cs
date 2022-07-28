using UniversityProject.Domain.CustomEntities;

namespace UniversityProject.Api.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Success = true;
        }

        public bool Success { get; set; }
    }
    public class ApiResponse<T> 
    {
        
        public ApiResponse(int StatusCode, string Message, bool HasError, T data)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.HasError = HasError;
            this.Data = data;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool HasError { get; set; }
        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
