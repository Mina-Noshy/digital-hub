namespace DigitalHub.Application.Common;

public static class ApiMessage
{
    // CURD Successful
    public const string SuccessfulCreate = "msg-successful-create";
    public const string SuccessfulUpdate = "msg-successful-update";
    public const string SuccessfulDelete = "msg-successful-delete";

    // CURD Failure
    public const string FailedCreate = "msg-failed-create";
    public const string FailedUpdate = "msg-failed-update";
    public const string FailedDelete = "msg-failed-delete";

    // Authentication & Authorization
    public const string InvalidCredentials = "msg-invalid-credentials";
    public const string AccountInactive = "msg-account-inactive";
    public const string AccountBlocked = "msg-account-blocked";
    public const string ConfirmationRequired = "msg-confirmation-required";
    public const string Unauthorized = "msg-unauthorized";
    public const string Forbidden = "msg-forbidden";

    // Token & Sessions
    public const string TokenExpired = "msg-token-expired";
    public const string TokenRevoked = "msg-token-revoked";
    public const string RefreshTokenInvalid = "msg-refresh-token-invalid";

    // Activation and toggle
    public const string PasswordUpdated = "msg-password-updated";
    public const string EmailConfirmed = "msg-email-confirmed";
    public const string ContactNumberConfirmed = "msg-contact-confirmed";
    public const string EmailConfirmationToggled = "msg-email-confirmation-toggled";
    public const string ContactNumberConfirmationToggled = "msg-contact-confirmation-toggled";
    public const string AccountActivationToggled = "msg-account-activation-toggled";
    public const string AccountBlockationToggled = "msg-account-blockation-toggled";

    // Common
    public const string EntityIdMismatch = "msg-entity-id-mismatch";
    public const string ItemNotFound = "msg-item-not-found";
    public const string ItemAlreadyExist = "msg-item-already-exist";
    public const string ServerError = "msg-server-error";
    public const string SomethingWrong = "msg-something-wrong";
    public const string RateLimitExceeded = "msg-rate-limit-exceeded";

    // Leasing & Finance
    public const string UnitUnAvailableOrLeased = "msg-unit-unavailable-or-leased";
    public const string InstallmentPaymentReceived = "msg-installment-payment-received";
    public const string LeaseIncomeRecognized = "msg-lease-income-recognized";
    public const string FinanceInstallmentConfigMissing = "msg-finance-installment-config";
    public const string FinanceVatConfigMissing = "msg-finance-vat-config";
    public const string FinancePaymentMethodNoAccount = "msg-finance-payment-method-no-account";
    public const string SecurityDepositReceived = "msg-security-deposit-received";
    public const string SecurityDepositIncreased = "msg-security-deposit-increased";
    public const string SecurityDepositDebited = "msg-security-deposit-debited";
    public const string DepositLiabilityCreated = "msg-deposit-liability-created";
    public const string FinanceSecurityDepositConfigMissing = "msg-finance-security-deposit-config";
    public const string FinanceLateFeeConfigMissing = "msg-finance-late-fee-config";


    // Email & SMS & Notification
    public const string SuccessfulSent = "msg-successful-sent";
    public const string FailedSend = "msg-failed-send";
}

