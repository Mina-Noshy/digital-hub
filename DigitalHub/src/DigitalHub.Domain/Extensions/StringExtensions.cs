namespace DigitalHub.Domain.Extensions;

/// <summary>
/// Extension methods for string and collection operations involving delimited strings.
/// </summary>
public static class StringExtensions
{
    private const string DefaultDelimiter = "###";

    /// <summary>
    /// Converts a collection of strings into a single delimited string, filtering out null, empty, or whitespace-only values.
    /// </summary>
    /// <param name="source">The collection of strings to join.</param>
    /// <param name="delimiter">The delimiter to use for joining. Defaults to "###".</param>
    /// <param name="removeEmptyEntries">Whether to filter out null, empty, or whitespace-only entries. Defaults to true.</param>
    /// <returns>A delimited string containing the filtered values, or an empty string if the source is null or contains no valid entries.</returns>
    /// <exception cref="ArgumentNullException">Thrown when delimiter is null.</exception>
    public static string ToDelimitedString(this IEnumerable<string>? source, string delimiter = DefaultDelimiter, bool removeEmptyEntries = true)
    {
        if (delimiter == null)
            throw new ArgumentNullException(nameof(delimiter));

        if (source == null)
            return string.Empty;

        var filteredSource = removeEmptyEntries
            ? source.Where(s => !string.IsNullOrWhiteSpace(s))
            : source;

        return string.Join(delimiter, filteredSource);
    }

    /// <summary>
    /// Converts a delimited string back into a list of strings.
    /// </summary>
    /// <param name="delimitedString">The delimited string to split.</param>
    /// <param name="delimiter">The delimiter used for splitting. Defaults to "###".</param>
    /// <param name="removeEmptyEntries">Whether to remove empty entries from the result. Defaults to false to maintain symmetry with ToDelimitedString.</param>
    /// <returns>A list of strings split by the delimiter, or an empty list if the input is null or empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when delimiter is null.</exception>
    public static List<string> ToStringList(this string? delimitedString, string delimiter = DefaultDelimiter, bool removeEmptyEntries = false)
    {
        if (delimiter == null)
            throw new ArgumentNullException(nameof(delimiter));

        if (string.IsNullOrEmpty(delimitedString))
            return new List<string>();

        var splitOptions = removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
        return delimitedString.Split(new[] { delimiter }, splitOptions).ToList();
    }

    /// <summary>
    /// Converts a delimited string back into a list of strings, filtering out null, empty, or whitespace-only entries.
    /// </summary>
    /// <param name="delimitedString">The delimited string to split.</param>
    /// <param name="delimiter">The delimiter used for splitting. Defaults to "###".</param>
    /// <returns>A list of non-empty strings split by the delimiter.</returns>
    /// <exception cref="ArgumentNullException">Thrown when delimiter is null.</exception>
    public static List<string> ToStringListFiltered(this string? delimitedString, string delimiter = DefaultDelimiter)
    {
        if (delimiter == null)
            throw new ArgumentNullException(nameof(delimiter));

        if (string.IsNullOrEmpty(delimitedString))
            return new List<string>();

        return delimitedString
            .Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();
    }





    /// <summary>
    /// Gets the first name from a full name string
    /// </summary>
    /// <param name="fullName">The full name string</param>
    /// <returns>The first name, or empty string if null/empty input</returns>
    public static string GetFirstName(this string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;

        var names = fullName.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return names.Length > 0 ? names[0] : string.Empty;
    }

    /// <summary>
    /// Gets the last name from a full name string
    /// </summary>
    /// <param name="fullName">The full name string</param>
    /// <returns>The last name, or empty string if null/empty input or no last name</returns>
    public static string GetLastName(this string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return string.Empty;

        var names = fullName.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return names.Length > 1 ? names[names.Length - 1] : string.Empty;
    }

}
