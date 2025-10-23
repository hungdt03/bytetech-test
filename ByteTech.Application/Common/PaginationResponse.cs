namespace ByteTech.Application.Common;

public class PaginationResponse<T> : DataResponse<T>
    where T : class
{
    public Pagination Pagination { get; set; } = default!;
}

public class Pagination
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}