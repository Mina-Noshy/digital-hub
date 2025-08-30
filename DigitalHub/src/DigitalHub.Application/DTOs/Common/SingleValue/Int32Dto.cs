namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record Int32Dto : IDto
{
    public int Value { get; set; } //= 0;
}
