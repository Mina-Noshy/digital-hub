namespace DigitalHub.Application.DTOs.Common;

public record IdsBodyDto : IDto
{
    public long[] IDs { get; set; } = [];
}
