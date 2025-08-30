namespace DigitalHub.Domain.Enums;

public enum TransactionTypes
{
    None = 0,

    // 💰 Income
    RentRevenue = 1,                // e.g. Rental income
    ServiceChargeRevenue = 2,       // e.g. Shared facility charges
    LateFeeRevenue = 3,             // e.g. Fines from tenants
    OtherIncome = 4,                // e.g. Advertising, sublease fees

    // 📤 Liabilities
    SecurityDepositPayable = 5,     // Amount received to return later
    PrepaidRentLiability = 6,       // Rent received in advance
    WithholdingPayable = 7,         // Tenant-deducted withholding tax
    VATPayable = 8,                 // VAT collected on rent or services

    // 🧾 Receivables
    TenantReceivable = 9,           // Tenant owes rent
    OtherReceivable = 10,           // Maintenance or penalties

    // 💸 Expenses
    PropertyMaintenanceExpense = 11, // Maintenance expenses
    UtilitiesExpense = 12,           // Water, Electricity etc.
    ManagementFeeExpense = 13,       // Fees paid to management company
    PropertyTaxExpense = 14,         // Government taxes
    InsuranceExpense = 15,           // Property insurance
    DepreciationExpense = 16,        // Depreciation on assets
    LegalOrProfessionalFees = 17,    // Legal and audit fees
    OtherExpenses = 18,              // Catch-all

    // 💼 Assets
    BankAccount = 19,                // Cash/Bank account used for payments
    CashOnHand = 20,                 // Petty cash
    FixedAssets = 21,                // Buildings, Furniture, etc.

    // 🧾 Revenue Adjustments
    RentDiscount = 22,               // Rent waived or discounted
    RentWriteOff = 23,               // Rent written off (uncollectible)

    // 🔄 Internal Adjustments
    IntercompanyReceivable = 24,
    IntercompanyPayable = 25,

    // 📊 Equity/Profit
    RetainedEarnings = 26,
    OwnerDrawings = 27
}
