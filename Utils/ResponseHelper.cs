using System.Data;

public class CustomResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

public static class ResponseHelper
{
    public static CustomResponse<T> CreateSuccessResponse<T>(T data, string message = "Data successfully.")
    {
        return new CustomResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static CustomResponse<T> CreateErrorResponse<T>(string message = "An error occurred.")
    {
        return new CustomResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }
}
