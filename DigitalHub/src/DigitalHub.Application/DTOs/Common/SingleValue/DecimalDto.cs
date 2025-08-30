namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record DecimalDto : IDto
{
    public decimal Value { get; set; } //= 0;
}
