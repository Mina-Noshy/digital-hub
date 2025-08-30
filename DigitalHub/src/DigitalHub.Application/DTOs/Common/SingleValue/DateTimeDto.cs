namespace DigitalHub.Application.DTOs.Common.SingleValue;

public record DateTimeDto : IDto
{
    public DateTime Value { get; set; } //= new DateTime(1900, 1, 1);
}
