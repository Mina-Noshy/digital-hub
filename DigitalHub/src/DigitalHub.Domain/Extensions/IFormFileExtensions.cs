﻿using DigitalHub.Domain.Utilities;
using Microsoft.AspNetCore.Http;

namespace DigitalHub.Domain.Extensions;

public static class IFormFileExtensions
{
    private const decimal MAX_SIZE = 15; // Megabytes.
    private static readonly HashSet<string> WHITE_LIST = new HashSet<string>
    {
        ".jpg", ".jpeg", ".txt", ".mkv", ".flv", ".doc", ".docx",
        ".png", ".gif", ".bmp", ".tiff", ".tif", ".webp", ".pdf",
        ".rtf", ".csv", ".xls", ".xlsx", ".ppt", ".pptx",
        ".mp3", ".wav", ".ogg", ".mp4", ".mov", ".avi"
    };
    private static readonly HashSet<string> BLACK_LIST = new HashSet<string>
    {
        ".exe", ".bat", ".com", ".dll", ".java", ".class", ".jar", ".war", ".scr", ".msi",
        ".cab", ".c", ".h", ".cs", ".cpp", ".hpp", ".py", ".pyc", ".pyd", ".pyo", ".pyw", ".php",
        ".rb", ".go", ".js", ".css", ".html", ".htm", ".sql", ".cmd", ".swift", ".rs", ".ts", ".pl",
        ".sh", ".json", ".xml", ".md", ".yaml", ".yml"
    };


    /// <summary>
    /// Checks if the file size is valid by comparing it against a predefined maximum size (15 MB)
    /// </summary>
    /// <param name="file"></param>
    /// <returns>true if the file size exceeds the defined maximum, otherwise false</returns>
    public static bool IsFileSizeValid(this IFormFile? file)
    {
        return file != null && file.Length <= (MAX_SIZE * 1024 * 1024);
    }
    /// <summary>
    /// Validates the file extension by ensuring it is on the whitelist and not on the blacklist of allowed/disallowed file types.
    /// </summary>
    /// <param name="file"></param>
    /// <returns>true if the file extension is valid, otherwise false</returns>
    public static bool IsFileExtensionValid(this IFormFile? file)
    {
        if (file == null) return false;
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return !BLACK_LIST.Contains(fileExtension) && WHITE_LIST.Contains(fileExtension);
    }
    /// <summary>
    /// Generates a unique file name by appending the current timestamp (in ticks) to the original file name without extension. If the file is null, it returns only the timestamp as the file name.
    /// </summary>
    /// <param name="file"></param>
    /// <returns>A unique file name as a string</returns>
    public static string GenerateUniqueFileName(this IFormFile? file)
    {
        string uniqueName = DateTimeProvider.UtcNow.Ticks.ToString();
        if (file == null) return uniqueName;

        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
        string fileExtension = Path.GetExtension(file.FileName);

        return $"{fileName}_{uniqueName}{fileExtension}";
    }
    /// <summary>
    /// Uploads the file to a specified directory with a given unique file name. Validates file size and extension before uploading. Throws an exception if validation fails.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="uploadTo"></param>
    /// <param name="uniqueFileName"></param>
    /// <returns>The full path of the uploaded file.</returns>
    public static async Task<string> UploadToAsync(this IFormFile? file, string uploadTo, string? uniqueFileName = null)
    {
        if (file == null) throw new ArgumentNullException(nameof(file), "File is null.");
        if (!IsFileSizeValid(file)) throw new ArgumentException($"File size exceeds the maximum allowed size of {MAX_SIZE} MB.", nameof(file));
        if (!IsFileExtensionValid(file)) throw new ArgumentException($"Invalid file type. Only the following file types are allowed: {string.Join(",", WHITE_LIST)}", nameof(file));

        uniqueFileName ??= GenerateUniqueFileName(file);
        Directory.CreateDirectory(uploadTo);
        string filePath = Path.Combine(uploadTo, uniqueFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filePath;
    }

}
