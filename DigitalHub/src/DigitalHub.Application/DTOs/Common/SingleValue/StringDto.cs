namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record StringDto : IDto
{
    public string Value { get; set; } = null!; //= string.Empty;
}
