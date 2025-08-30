namespace DigitalHub.Domain.QueryParams.Common;

public record DropdownQueryParams : IQueryParams
{
    public string? SearchTerm { get; set; } = string.Empty;
    public int Limit { get; set; } = 10;
    public long ParentId { get; set; } = 0;
}
