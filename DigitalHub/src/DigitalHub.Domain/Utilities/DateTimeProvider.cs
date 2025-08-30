namespace DigitalHub.Domain.Utilities;

public static class DateTimeProvider
{
    public static DateTime UtcNow => DateTime.UtcNow;
    public static DateTime Now => DateTime.Now;
    public static DateTime Today => DateTime.UtcNow.Date;
}
