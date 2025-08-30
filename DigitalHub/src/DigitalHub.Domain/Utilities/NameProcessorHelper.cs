using DigitalHub.Domain.Extensions;

namespace DigitalHub.Domain.Utilities;


/// <summary>
/// Helper class for processing and normalizing first and last names
/// </summary>
public static class NameProcessorHelper
{
    /// <summary>
    /// Processes first and last names and returns normalized values
    /// </summary>
    /// <param name="firstName">Input first name</param>
    /// <param name="lastName">Input last name</param>
    /// <returns>Tuple containing processed (FirstName, LastName)</returns>
    public static (string FirstName, string LastName) ProcessNames(string firstName, string lastName)
    {
        var cleanFirstName = firstName?.Trim() ?? string.Empty;
        var cleanLastName = lastName?.Trim() ?? string.Empty;

        // Case 1: Both first and last names are provided - return as is
        if (!string.IsNullOrWhiteSpace(cleanFirstName) && !string.IsNullOrWhiteSpace(cleanLastName))
        {
            return (cleanFirstName, cleanLastName);
        }

        // Case 2: Only first name is provided - try to split it
        if (!string.IsNullOrWhiteSpace(cleanFirstName) && string.IsNullOrWhiteSpace(cleanLastName))
        {
            var extractedLastName = cleanFirstName.GetLastName();
            if (!string.IsNullOrWhiteSpace(extractedLastName))
            {
                return (cleanFirstName.GetFirstName(), extractedLastName);
            }
            // If no last name can be extracted, keep the full name as first name
            return (cleanFirstName, string.Empty);
        }

        // Case 3: Only last name is provided - try to split it
        if (string.IsNullOrWhiteSpace(cleanFirstName) && !string.IsNullOrWhiteSpace(cleanLastName))
        {
            var extractedFirstName = cleanLastName.GetFirstName();
            var extractedLastName = cleanLastName.GetLastName();

            if (!string.IsNullOrWhiteSpace(extractedLastName))
            {
                return (extractedFirstName, extractedLastName);
            }
            else
            {
                // If no splitting possible, move to first name
                return (cleanLastName, string.Empty);
            }
        }

        // Case 4: Both are empty
        return (string.Empty, string.Empty);
    }

    /// <summary>
    /// Advanced name processing that combines both fields and redistributes intelligently
    /// </summary>
    /// <param name="firstName">Input first name</param>
    /// <param name="lastName">Input last name</param>
    /// <returns>Tuple containing processed (FirstName, LastName)</returns>
    public static (string FirstName, string LastName) ProcessNamesAdvanced(string firstName, string lastName)
    {
        var cleanFirstName = firstName?.Trim();
        var cleanLastName = lastName?.Trim();

        // Combine available name parts
        var fullName = string.Join(" ", new[] { cleanFirstName, cleanLastName }
            .Where(n => !string.IsNullOrWhiteSpace(n)));

        if (string.IsNullOrWhiteSpace(fullName))
        {
            return (string.Empty, string.Empty);
        }

        // Split the combined name properly
        var nameParts = fullName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (nameParts.Length == 1)
        {
            return (nameParts[0], string.Empty);
        }
        else if (nameParts.Length >= 2)
        {
            return (nameParts[0], string.Join(" ", nameParts.Skip(1))); // Handle compound last names
        }

        return (string.Empty, string.Empty);
    }

    /// <summary>
    /// Processes names for any object type using reflection
    /// </summary>
    /// <typeparam name="T">The type of object to process</typeparam>
    /// <param name="entity">The entity object</param>
    /// <param name="firstNameProperty">Name of the first name property</param>
    /// <param name="lastNameProperty">Name of the last name property</param>
    /// <param name="useAdvancedProcessing">Whether to use advanced processing logic</param>
    public static void ProcessEntityNames<T>(T entity, string firstNameProperty = "FirstName", string lastNameProperty = "LastName", bool useAdvancedProcessing = true)
    {
        if (entity == null) return;

        var type = typeof(T);
        var firstNameProp = type.GetProperty(firstNameProperty);
        var lastNameProp = type.GetProperty(lastNameProperty);

        if (firstNameProp == null || lastNameProp == null)
        {
            throw new ArgumentException($"Properties {firstNameProperty} or {lastNameProperty} not found on type {type.Name}");
        }

        var currentFirstName = firstNameProp.GetValue(entity)?.ToString() ?? string.Empty;
        var currentLastName = lastNameProp.GetValue(entity)?.ToString() ?? string.Empty;

        var (processedFirstName, processedLastName) = useAdvancedProcessing
            ? ProcessNamesAdvanced(currentFirstName, currentLastName)
            : ProcessNames(currentFirstName, currentLastName);

        firstNameProp.SetValue(entity, processedFirstName);
        lastNameProp.SetValue(entity, processedLastName);
    }
}
