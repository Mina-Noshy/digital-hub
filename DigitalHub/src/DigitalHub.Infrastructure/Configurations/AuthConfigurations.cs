using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHub.Infrastructure.Configurations;

internal sealed class RoleMasterConfigurations : IEntityTypeConfiguration<RoleMaster>
{
    public void Configure(EntityTypeBuilder<RoleMaster> entity)
    {
        var items = new List<RoleMaster>()
        {
            new RoleMaster{Id = 1, Name = AuthenticationRoles.ADMIN, NormalizedName = AuthenticationRoles.ADMIN.ToUpper()},
            new RoleMaster{Id = 2, Name = AuthenticationRoles.USER, NormalizedName = AuthenticationRoles.USER.ToUpper()},
            new RoleMaster{Id = 3, Name = AuthenticationRoles.NONE, NormalizedName = AuthenticationRoles.NONE.ToUpper()},
        };

        entity.HasData(items);
    }
}

internal sealed class UserMasterConfigurations : IEntityTypeConfiguration<UserMaster>
{
    public void Configure(EntityTypeBuilder<UserMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(u => u.PhoneNumber).IsUnique();

        var user = new UserMaster
        {
            Id = 1,
            FirstName = "Digital",
            LastName = "Hub",

            UserName = "digitalhub",
            NormalizedUserName = "digitalhub".ToUpper(),

            Email = "ceo@digitalhub.com",
            NormalizedEmail = "ceo@digitalhub.com".ToUpper(),
            EmailConfirmed = true,

            PhoneNumber = "+201210165412",
            PhoneNumberConfirmed = true,

            SecurityStamp = "c446b9dc-2ff7-4812-8929-59403f4cbcef",

            IsActive = true,
            IsBlocked = false,
        };

        user.PasswordHash = PasswordGeneratorHelper.HashPassword(user, "digitalhub123");

        entity.HasData(user);
    }
}

internal sealed class UserRoleMasterConfigurations : IEntityTypeConfiguration<UserRoleMaster>
{
    public void Configure(EntityTypeBuilder<UserRoleMaster> entity)
    {
        var userRole = new UserRoleMaster { UserId = 1, RoleId = 1 };

        entity.HasData(userRole);
    }
}

internal sealed class CompanyDatabaseMasterConfigurations : IEntityTypeConfiguration<CompanyDatabaseMaster>
{
    public void Configure(EntityTypeBuilder<CompanyDatabaseMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(u => u.DatabaseNo).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");

        var items = new List<CompanyDatabaseMaster>() {
            new CompanyDatabaseMaster
            {
                Id = 1,
                CompanyKey = "CDB-0000",
                DatabaseNo = "1000",
                CompanyName = "Rescova 1",
                Country = "Egypt",
                City = "Cairo",
                Addrss = "Nasr City",
                ContactNo = "00201210165412",
                Email = "",
                LogoUrl = "https://digitalhub.com/wp-content/themes/digitalhub/images/logo.png",
                IsActive = true,
                ForCustomers = true,
                IsDefault = true,
            },
            new CompanyDatabaseMaster
            {
                Id = 2,
                CompanyKey = "CDB-0000",
                DatabaseNo = "2000",
                CompanyName = "Rescova 2",
                Country = "Egypt",
                City = "Cairo",
                Addrss = "Nasr City",
                ContactNo = "00201210165412",
                Email = "",
                LogoUrl = "https://digitalhub.com/wp-content/themes/digitalhub/images/logo.png",
                IsActive = true,
                ForCustomers = true,
                IsDefault = true,
            },
            new CompanyDatabaseMaster
            {
                Id = 3,
                CompanyKey = "CDB-0000",
                DatabaseNo = "3000",
                CompanyName = "Rescova 3",
                Country = "Egypt",
                City = "Cairo",
                Addrss = "Nasr City",
                ContactNo = "00201210165412",
                Email = "",
                LogoUrl = "https://digitalhub.com/wp-content/themes/digitalhub/images/logo.png",
                IsActive = true,
                ForCustomers = true,
                IsDefault = true,
            }
        };

        entity.HasData(items);
    }
}

internal sealed class RolePageMasterConfigurations : IEntityTypeConfiguration<RolePageMaster>
{
    public void Configure(EntityTypeBuilder<RolePageMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => new { x.RoleId, x.PageId }).IsUnique().HasFilter("[IsDeleted] = CAST(0 AS BIT)");
    }
}

internal sealed class UserLoginHistoryMasterConfigurations : IEntityTypeConfiguration<UserLoginHistoryMaster>
{
    public void Configure(EntityTypeBuilder<UserLoginHistoryMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.UserId);
    }
}

internal sealed class EntityChangeLogConfigurations : IEntityTypeConfiguration<EntityChangeLog>
{
    public void Configure(EntityTypeBuilder<EntityChangeLog> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.Timestamp);
        entity.HasIndex(x => x.ChangedBy);
        entity.HasIndex(x => x.Operation);

        entity.Property(e => e.Operation).HasColumnType("varchar(10)");
        entity.Property(e => e.IPAddress).HasColumnType("varchar(30)");
        entity.Property(e => e.EntityName).HasColumnType("varchar(40)");

        entity.Property(e => e.ChangedBy).HasColumnType("varchar(50)");
        entity.Property(e => e.UserAgent).HasColumnType("varchar(200)");

        entity
            .Ignore(x => x.CreatedBy)
            .Ignore(x => x.CreatedAt)
            .Ignore(x => x.ModifiedBy)
            .Ignore(x => x.ModifiedAt)
            .Ignore(x => x.DeletedBy)
            .Ignore(x => x.DeletedAt)
            .Ignore(x => x.IsDeleted);
    }
}

internal sealed class NotificationMasterConfigurations : IEntityTypeConfiguration<NotificationMaster>
{
    public void Configure(EntityTypeBuilder<NotificationMaster> entity)
    {
        // Enforce unique columns and indexes
        entity.HasIndex(x => x.UserId);

        entity.Property(e => e.Title).HasColumnType("nvarchar(1000)");
        entity.Property(e => e.Message).HasColumnType("nvarchar(max)");
        entity.Property(e => e.ActionUrl).HasColumnType("nvarchar(500)");

        entity
            .Ignore(x => x.CreatedBy)
            .Ignore(x => x.ModifiedBy)
            .Ignore(x => x.ModifiedAt)
            .Ignore(x => x.DeletedBy)
            .Ignore(x => x.DeletedAt)
            .Ignore(x => x.IsDeleted);
    }
}


internal sealed class CompanyProfileSettingsConfigurations : IEntityTypeConfiguration<CompanyProfileSettings>
{
    public void Configure(EntityTypeBuilder<CompanyProfileSettings> entity)
    {
        var item = new CompanyProfileSettings()
        {
            Id = 1,
            // ------------------- Company Info -------------------
            CompanyName = "Digital Hub",
            CompanyDescription = "Leading real estate company providing property buying, selling, and rental services.",
            Logo = string.Empty,
            ContactNumber = "+1 234 567 890",
            ContactEmail = "info@digitalhub.com",
            Address = "123 Main Street, New York, NY, USA",
            WebsiteUrl = "https://www.digitalhub.com",
            WorkingHours = "Mon-Fri 9:00 AM - 6:00 PM",
            SupportPhoneNumber = "+1 234 567 891",
            SupportEmail = "support@digitalhub.com",

            // ------------------- SMTP Settings -------------------
            SmtpHost = "smtp.gmail.com",
            SmtpPort = 587,
            SmtpUsername = "info.digitalhub@gmail.com",
            SmtpPassword = "xxxxxxxxxxxxxx",
            SmtpEnableSsl = true,
            SenderEmail = "info.digitalhub@gmail.com",
            SenderDisplayName = "Digital Hub",

            // ------------------- Email Template -------------------
            HTMLEmailHeader = @"<html lang=""en"" dir=""ltr""><head><meta charset=""UTF-8"" /><meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" /></head><body dir=""ltr"" style=""font-family: Tahoma, sans-serif;"">
                <div style=""background-color: #f5f5f5; padding: 20px; text-align: center;"">
                    <img src=""https://uhf.microsoft.com/images/microsoft/RE1Mu3b.png"" alt=""Digital Hub"" style=""height: 60px;""/>
                </div>
                <div style=""padding: 20px; font-family: Tahoma, sans-serif; font-size: 14px; color: #333;""> ".Trim(),

            HTMLEmailFooter = @"
                </div>
                <div style=""background-color: #f5f5f5; padding: 20px; text-align: center; font-family: Tahoma, sans-serif; font-size: 12px; color: #777;"">
                    &copy; 2025 Digital Hub. All rights reserved.<br/>
                    <a href=""https://www.digitalhub.com/Home/Terms"" style=""color: #777;"">Terms & Conditions</a> | 
                    <a href=""https://www.digitalhub.com/Home/Privacy"" style=""color: #777;"">Privacy Policy</a>
                </div> </body></html>".Trim(),

            // ------------------- WhatsApp API Configuration -------------------
            WhatsappApiUrl = "https://api.whatsapp.com/send",
            WhatsappApiKey = "your-whatsapp-api-key",
            WhatsappSenderNumber = "+1234567890",
            WhatsappDefaultMessageTemplate = "Hello! How can we assist you with your property needs?",

            // ------------------- Finance Settings -------------------
            TaxPercentage = 5.00m,
            CommissionPercentage = 2.50m,
            PaymentGatewayApiKey = "your-payment-gateway-api-key",
            InvoiceFooterNote = "Thank you for choosing Digital Hub!",

            // ------------------- Real Estate Specific Settings -------------------
            DefaultCountry = "United States",
            DefaultLanguage = "en",

            // ------------------- SEO Settings -------------------
            MetaTitle = "Digital Hub - Buy, Sell, Rent Properties",
            MetaDescription = "Best real estate company to buy, sell, or rent properties easily and quickly.",
            MetaKeywords = "real estate, buy house, sell property, rent apartment",
            GoogleAnalyticsTrackingId = "UA-12345678-1",

            // ------------------- Social Media Links -------------------
            FacebookPageUrl = "https://www.facebook.com/digitalhub1",
            InstagramProfileUrl = "https://www.instagram.com/digitalhub",
            LinkedInProfileUrl = "https://www.linkedin.com/company/digitalhub/",
            YouTubeChannelUrl = "https://www.youtube.com/digitalhub",

            // ------------------- Mobile App Settings -------------------
            AndroidAppDownloadLink = "https://play.google.com/store/apps/details?id=com.digitalhub",
            IOSAppDownloadLink = "https://apps.apple.com/us/app/digitalhub/id1234567890",

        };

        entity.HasData(item);
    }
}
