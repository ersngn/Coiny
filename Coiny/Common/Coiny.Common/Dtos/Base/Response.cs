using System.Net;
using System.Text.Json.Serialization;
using Coiny.Common.Constants;
using Coiny.Common.Enumeration;

namespace Coiny.Common.Dtos.Base;

public class Response<T> : IDto
{
    public T Data { get; private set; }
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; private set; }
    [JsonIgnore]
    public bool IsSuccess { get; private set; }
    public List<ErrorDto> Errors { get; set; }

    public static Response<T> Success(T data)
    {
        return new Response<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.OK,
            IsSuccess = true
        };
    }

    public static Response<T?> Success(HttpStatusCode statusCode)
    {
        return new Response<T?>()
        {
            Data = default(T),
            StatusCode = statusCode,
            IsSuccess = true
        };
    }

    public static Response<T> Fail(List<ErrorDto> errors, HttpStatusCode statusCode)
    {
        return new Response<T>()
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccess = false
        };
    }

    public static Response<T> Fail(Error error, string errorMessage)
    {
        var errorDto = new ErrorDto()
        {
            Error = error,
            ErrorCode = (int)error,
            Message = errorMessage
        };
        return new Response<T>()
        {
            Errors = new List<ErrorDto>() { errorDto },
            StatusCode = HttpStatusCode.BadRequest,
            IsSuccess = false
        };
    }

    public static Response<T> Fail(string exceptionMessage)
    {
        return new Response<T>()
        {
            Errors = new List<ErrorDto>()
            {
                new ErrorDto()
                {
                    Error = Error.UnknownError,
                    ErrorCode = (int)Error.UnknownError,
                    Message = ErrorConstants.UnknownError
                },
                new ErrorDto()
                {
                    Error = Error.UnknownError,
                    ErrorCode = (int)Error.UnknownError,
                    Message = exceptionMessage
                }
            },
            StatusCode = HttpStatusCode.BadRequest,
            IsSuccess = false
        };
    }

}