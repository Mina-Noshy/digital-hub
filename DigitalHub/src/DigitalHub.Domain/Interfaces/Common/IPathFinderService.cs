namespace DigitalHub.Domain.Interfaces.Common;

public interface IPathFinderService
{
    // Common *******************************************************************************************************
    string GetFullURL(string fullPath);
    string GetFullURL(string folderPath, string fileName);


    // Kemet *******************************************************************************************************
    string UserProfileFolderPath { get; }
    string CompanyLogoFolderPath { get; }



    // FM *******************************************************************************************************
    string FMAttachmentsFolderPath { get; }
    string EnquiryDocumentsFolderPath { get; }


    // Setup *******************************************************************************************************
    string UnitImagesFolderPath { get; }
    string PropertyImagesFolderPath { get; }



    // Finance *******************************************************************************************************
    string JournalVoucherAttachmentsFolderPath { get; }


    // Lease *******************************************************************************************************
    string LeaseAgreementTemplatePath { get; }
    string ExportedLeaseAgreementFolderPath { get; }
    string LeaseAgreementAttachmentsFolderPath { get; }

}
