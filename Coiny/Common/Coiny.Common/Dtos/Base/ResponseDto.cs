using System.Text.Json.Serialization;

namespace Coiny.Common.Dtos.Base;

public class ResponseDto<T> : IDto
{
    public T Data { get; private set; }
    [JsonIgnore]
    public int StatusCode { get; private set; }
    [JsonIgnore]
    public bool IsSuccess { get; private set; }
    public List<ErrorDto> Errors { get; set; }

    public static ResponseDto<T> Success(T data, int statusCode)
    {
        return new ResponseDto<T>()
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static ResponseDto<T?> Success(int statusCode)
    {
        return new ResponseDto<T?>()
        {
            Data = default(T),
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static ResponseDto<T> Fail(List<ErrorDto> errors, int statusCode)
    {
        return new ResponseDto<T>()
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccess = false
        };
    }

    public static ResponseDto<T> Fail(ErrorDto error, int statusCode)
    {
        return new ResponseDto<T>()
        {
            Errors = new List<ErrorDto>() { error },
            StatusCode = statusCode,
            IsSuccess = false
        };
    }

}