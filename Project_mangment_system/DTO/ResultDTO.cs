using Project_management_system.Enums;
using Project_management_system.ViewModels;

namespace Project_management_system.DTO
{
    public class ResultDTO<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }


        public static ResultDTO<T> Sucess<T>(T data, string message = "")
        {
            return new ResultDTO<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
            };
        }

        public static ResultDTO<T> Faliure(string message)
        {
            return new ResultDTO<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
            };
        }
    }
}

