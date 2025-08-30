namespace DigitalHub.Application.DTOs.Common;

public record DropdownItemDto : IDto
{
    public long Value { get; set; }
    public string Description { get; set; } = null!;
}