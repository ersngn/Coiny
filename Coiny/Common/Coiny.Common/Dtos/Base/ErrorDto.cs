using Coiny.Common.Enumeration;

namespace Coiny.Common.Dtos.Base;

public class ErrorDto : IDto
{
    public Error Error { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; }
}