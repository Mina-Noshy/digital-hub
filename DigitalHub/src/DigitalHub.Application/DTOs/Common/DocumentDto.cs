namespace DigitalHub.Application.DTOs.Common;

public record DocumentDto
{
    public long Id { get; set; }
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
}
