namespace DigitalHub.Domain.Utilities;

public static class DateTimeHelper
{
    /// <summary>
    /// Formats the duration between now and the given creation time as a human-readable "time ago" string.
    /// </summary>
    /// <param name="creationTime">The point in time to calculate the difference from.</param>
    /// <returns>A string like "3 minutes ago", "1 day ago", etc.</returns>
    public static string FormatTimeAgo(DateTime creationTime)
    {
        var now = DateTimeProvider.UtcNow;
        var timeSpan = now - creationTime;

        return timeSpan switch
        {
            _ when timeSpan.TotalSeconds < 1 => string.Empty,
            _ when timeSpan.TotalSeconds < 60 => $"{Math.Max(1, Math.Floor(timeSpan.TotalSeconds))} seconds ago",
            _ when timeSpan.TotalMinutes < 60 => $"{Math.Floor(timeSpan.TotalMinutes)} minute{(Math.Floor(timeSpan.TotalMinutes) == 1 ? " ago" : "s ago")}",
            _ when timeSpan.TotalHours < 24 => $"{Math.Floor(timeSpan.TotalHours)} hour{(Math.Floor(timeSpan.TotalHours) == 1 ? " ago" : "s ago")}",
            _ when timeSpan.TotalDays < 30 => $"{Math.Floor(timeSpan.TotalDays)} day{(Math.Floor(timeSpan.TotalDays) == 1 ? " ago" : "s ago")}",
            _ when timeSpan.TotalDays < 365 => $"{Math.Floor(timeSpan.TotalDays / 30)} month{(Math.Floor(timeSpan.TotalDays / 30) == 1 ? " ago" : "s ago")}",
            _ => $"{Math.Floor(timeSpan.TotalDays / 365)} year{(Math.Floor(timeSpan.TotalDays / 365) == 1 ? " ago" : "s ago")}"
        };
    }
}
