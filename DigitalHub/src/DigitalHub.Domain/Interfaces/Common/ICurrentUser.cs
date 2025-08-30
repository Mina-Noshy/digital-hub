namespace DigitalHub.Domain.Interfaces.Common;

public interface ICurrentUser
{
    long UserId { get; }
    string IpAddress { get; }
    string Username { get; }
    string UserFullname { get; }
    string UserEmail { get; }
    string[] UserRoles { get; }
    string DatabaseNo { get; }
    string UserAgent { get; }
    string FinancialPeriod { get; }
    string Language { get; }
    string ContentDirection { get; } // ltr, rtl
}
