namespace ByteTech.Application.Common;

public class DataResponse<T> : BaseResponse
{
    public T Data { get; set; } = default!;
}
