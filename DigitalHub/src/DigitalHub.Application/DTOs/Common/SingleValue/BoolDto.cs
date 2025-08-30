namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record BoolDto : IDto
{
    public bool Value { get; set; } //= false;
}
