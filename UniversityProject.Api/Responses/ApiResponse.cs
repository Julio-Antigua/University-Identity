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
        
        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
