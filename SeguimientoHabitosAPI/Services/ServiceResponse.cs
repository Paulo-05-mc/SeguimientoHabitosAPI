// Services/ServiceResponse.cs
namespace SeguimientoHabitosAPI.Services
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ServiceResponse() { }

        public ServiceResponse(T data)
        {
            Data = data;
        }

        public ServiceResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }
    }
}