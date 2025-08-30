using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.DTOs.Common;

public record CreateDocumentDto
{
    public IFormFile File { get; set; } = null!;
    public string? Description { get; set; }
}
