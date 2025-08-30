namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record DateOnlyDto : IDto
{
    public DateTime Value { get; set; } //= new DateOnly(1900, 1, 1);
}
