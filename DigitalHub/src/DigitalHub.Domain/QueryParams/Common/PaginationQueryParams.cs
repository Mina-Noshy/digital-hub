namespace DigitalHub.Domain.QueryParams.Common;

public record PaginationQueryParams : IQueryParams
{
    public string? SearchTerm { get; set; } = string.Empty;
    public string SortColumn { get; set; } = "Id";
    public bool Ascending { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public long ParentId { get; set; } = 0;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
