namespace DigitalHub.Api.Security;

internal class SecurityHeaderSettings
{
    public bool Enable { get; set; }
    public string XFrameOptions { get; set; } = null!;
    public string XContentTypeOptions { get; set; } = null!;
    public string ReferrerPolicy { get; set; } = null!;
    public string PermissionsPolicy { get; set; } = null!;
    public string SameSite { get; set; } = null!;
    public string XXSSProtection { get; set; } = null!;
}
