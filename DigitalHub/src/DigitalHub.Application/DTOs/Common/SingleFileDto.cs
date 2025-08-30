using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.DTOs.Common;

public record SingleFileDto : IDto
{
    public IFormFile File { get; set; } = null!;
}
