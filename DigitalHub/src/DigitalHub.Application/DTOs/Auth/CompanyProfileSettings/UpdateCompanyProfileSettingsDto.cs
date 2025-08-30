using Microsoft.AspNetCore.Http;

namespace DigitalHub.Application.DTOs.Auth.CompanyProfileSettings;


public record UpdateCompanyProfileSettingsDto : IDto
{
    public long Id { get; set; }
    // ------------------- Company Info -------------------
    public string CompanyName { get; set; } = null!;
    public string CompanyDescription { get; set; } = null!;
    public IFormFile? Logo { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }
    public string? Address { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? WorkingHours { get; set; }
    public string? SupportPhoneNumber { get; set; }
    public string SupportEmail { get; set; } = null!;

    // ------------------- SMTP Settings -------------------
    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string? SmtpUsername { get; set; }
    public string? SmtpPassword { get; set; }
    public bool SmtpEnableSsl { get; set; }
    public string? SenderEmail { get; set; }
    public string? SenderDisplayName { get; set; }

    // ------------------- Email Template -------------------
    public string? HTMLEmailHeader { get; set; }
    public string? HTMLEmailFooter { get; set; }
    // ------------------- WhatsApp API Configuration -------------------
    public string? WhatsappApiUrl { get; set; }
    public string? WhatsappApiKey { get; set; }
    public string? WhatsappSenderNumber { get; set; }
    public string? WhatsappDefaultMessageTemplate { get; set; }

    // ------------------- Finance Settings -------------------
    public decimal TaxPercentage { get; set; }
    public decimal CommissionPercentage { get; set; }
    public string? PaymentGatewayApiKey { get; set; }
    public string? InvoiceFooterNote { get; set; }

    // ------------------- Real Estate Specific Settings -------------------
    public string? DefaultCountry { get; set; }
    public string? DefaultLanguage { get; set; }

    // ------------------- SEO Settings -------------------
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? GoogleAnalyticsTrackingId { get; set; }

    // ------------------- Social Media Links -------------------
    public string? FacebookPageUrl { get; set; }
    public string? InstagramProfileUrl { get; set; }
    public string? LinkedInProfileUrl { get; set; }
    public string? YouTubeChannelUrl { get; set; }

    // ------------------- Mobile App Settings -------------------
    public string? AndroidAppDownloadLink { get; set; }
    public string? IOSAppDownloadLink { get; set; }
}