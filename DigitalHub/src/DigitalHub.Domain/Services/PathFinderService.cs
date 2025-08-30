using DigitalHub.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DigitalHub.Domain.Services;

public class PathFinderService(ICurrentUser _currentUser, IHttpContextAccessor _contextAccessor, IWebHostEnvironment env) : IPathFinderService
{
    // Common *******************************************************************************************************
    public string GetFullURL(string folderPath, string fileName)
        => GetFullURL(Path.Combine(folderPath, fileName));
    public string GetFullURL(string fullPath)
    {
        if (string.IsNullOrWhiteSpace(fullPath) || fullPath.Length < 2)
        {
            throw new ArgumentException("The full path provided is invalid or too short.", nameof(fullPath));
        }

        var httpContext = _contextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available.");

        string apiURL = $"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value?.TrimEnd('/')}";

        // Ensure path normalization
        fullPath = Path.GetFullPath(fullPath);

        if (!fullPath.StartsWith(env.WebRootPath))
        {
            throw new InvalidOperationException("The file is outside the allowed root directory.");
        }

        string relativePath = fullPath.Replace(env.WebRootPath, "").Replace("\\", "/").TrimStart('/');
        return $"{apiURL}/{relativePath}";
    }




    // Kemet *******************************************************************************************************
    public string UserProfileFolderPath
        => CombineCompanyInfo("user-profile");

    public string CompanyLogoFolderPath
        => CombineCompanyInfo("company-logo");




    // FM *******************************************************************************************************
    public string FMAttachmentsFolderPath
        => CombineCompanyInfo("fm-attachments");

    public string EnquiryDocumentsFolderPath
        => CombineCompanyInfo("enquiry-documents");




    // Setup *******************************************************************************************************
    public string UnitImagesFolderPath
        => CombineCompanyInfo("unit-images");
    public string PropertyImagesFolderPath
        => CombineCompanyInfo("property-images");



    // Finance *******************************************************************************************************
    public string JournalVoucherAttachmentsFolderPath
        => CombineCompanyInfo("journal-voucher-attachments");


    // Lease *******************************************************************************************************
    public string LeaseAgreementTemplatePath
        => CombineCompanyInfo("lease-agreement-templates", "lease-agreement.docx");
    public string ExportedLeaseAgreementFolderPath
        => CombineCompanyInfo("exported-lease-agreements");
    public string LeaseAgreementAttachmentsFolderPath
        => CombineCompanyInfo("lease-agreement-attachments");
















    // Private *******************************************************************************************************
    private string ROOT_FOLDER_PATH
        => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app-files");
    private string CombineCompanyInfo(string folderPath, string? fileName = null)
    {
        string companyId = _currentUser.DatabaseNo;

        if (string.IsNullOrWhiteSpace(companyId))
            companyId = "common";

        return string.IsNullOrWhiteSpace(fileName)
        ? Path.Combine(ROOT_FOLDER_PATH, folderPath, companyId)  // Return folder path
        : Path.Combine(ROOT_FOLDER_PATH, folderPath, companyId, fileName);  // Return file path
    }

}
