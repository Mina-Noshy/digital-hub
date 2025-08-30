namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record Int64Dto : IDto
{
    public long Value { get; set; } //= 0;
}
