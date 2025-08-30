namespace DigitalHub.Domain.Utilities;

public static class ExpressionHelper
{
    public static string PhoneNumber
        => @"^\+?[0-9\s\-\(\)]{7,20}$";

}
