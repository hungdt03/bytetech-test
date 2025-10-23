using System.Net;

namespace ByteTech.Application.Common;

public class BaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; } = default!;
    public bool Success { get; set; }
}