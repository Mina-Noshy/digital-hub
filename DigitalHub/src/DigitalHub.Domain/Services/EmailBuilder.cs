using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using MailKit.Security;
using MimeKit;
using Serilog;

namespace DigitalHub.Domain.Services;

public class EmailBuilder
{
    private readonly ICompanyProfileSettingsRepository _settingsRepository;
    private readonly ICurrentUser _currentUser;

    private readonly List<string> _recipients = new();
    private readonly List<string> _ccRecipients = new();
    private string _subject = string.Empty;
    private string _body = string.Empty;
    private bool _isHtml = true;
    private readonly List<string> _attachments = new();

    public EmailBuilder(ICompanyProfileSettingsRepository settingsRepository, ICurrentUser currentUser)
    {
        _settingsRepository = settingsRepository;
        _currentUser = currentUser;
    }

    public EmailBuilder To(params string[] emails)
    {
        _recipients.AddRange(emails.Where(e => !string.IsNullOrWhiteSpace(e)));
        return this;
    }

    public EmailBuilder Cc(params string[] emails)
    {
        _ccRecipients.AddRange(emails.Where(e => !string.IsNullOrWhiteSpace(e)));
        return this;
    }

    public EmailBuilder Subject(string subject)
    {
        _subject = subject;
        return this;
    }

    public EmailBuilder Body(string body, bool isHtml = true)
    {
        _body = body;
        _isHtml = isHtml;
        return this;
    }

    public EmailBuilder Attach(params string[] files)
    {
        _attachments.AddRange(files.Where(f => !string.IsNullOrWhiteSpace(f)));
        return this;
    }

    public async Task<bool> SendAsync(CancellationToken cancellationToken = default)
    {
        var settings = await _settingsRepository.GetSettingsAsync(cancellationToken);
        if (settings == null || !_recipients.Any())
        {
            Log.Error("Email not sent. Missing settings or recipients.");
            return false;
        }

        if (_isHtml)
        {
            var formattedHeader = GetEmailFormattedHeader(settings.HTMLEmailHeader!);
            _body = $"{formattedHeader}{_body}{settings.HTMLEmailFooter}";
        }

        var builder = new BodyBuilder
        {
            HtmlBody = _isHtml ? _body : null,
            TextBody = !_isHtml ? _body : null
        };

        foreach (var path in _attachments)
        {
            try
            {
                builder.Attachments.Add(path);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Failed to attach file: {File}", path);
            }
        }

        bool allSent = true;

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        try
        {
            await smtp.ConnectAsync(settings.SmtpHost, settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await smtp.AuthenticateAsync(settings.SmtpUsername, settings.SmtpPassword, cancellationToken);

            foreach (var recipient in _recipients)
            {
                try
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(settings.SmtpUsername));
                    email.To.Add(MailboxAddress.Parse(recipient));

                    foreach (var cc in _ccRecipients)
                        email.Cc.Add(MailboxAddress.Parse(cc));

                    email.Subject = _subject;
                    email.Body = builder.ToMessageBody();

                    await smtp.SendAsync(email, cancellationToken);
                    Log.Information("Email successfully sent to {Recipient}", recipient);
                }
                catch (Exception ex)
                {
                    allSent = false;
                    Log.Error(ex, "Failed to send email to {Recipient}", recipient);
                }
            }

            await smtp.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception ex)
        {
            allSent = false;
            Log.Error(ex, "SMTP connection failed: {Message}", ex.Message);
        }

        return allSent;
    }

    private string GetEmailFormattedHeader(string header)
    {
        var direction = _currentUser.ContentDirection;
        var lang = _currentUser.Language;

        return header
            .Replace("lang=\"en\"", $"lang=\"{lang}\"")
            .Replace("lang=\"ar\"", $"lang=\"{lang}\"")
            .Replace("dir=\"ltr\"", $"dir=\"{direction}\"")
            .Replace("dir=\"rtl\"", $"dir=\"{direction}\"")
            .Replace("<body>", $"<body dir=\"{direction}\"")
            .Replace("<html>", $"<html dir=\"{direction}\" lang=\"{lang}\">");
    }
}