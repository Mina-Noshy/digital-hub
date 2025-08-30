namespace DigitalHub.Api.Controllers.Base;


internal static class ApiVersionsConfig
{
    internal static class Api
    {
        internal static class Versions
        {
            internal const string V_1_0 = "1.0";
            internal const string V_2_0 = "2.0";
            internal const string V_3_0 = "3.0";
            internal const string V_4_0 = "4.0";
            internal const string V_5_0 = "5.0";
            internal const string V_6_0 = "6.0";
            internal const string V_7_0 = "7.0";
            internal const string V_8_0 = "8.0";
        }

        internal static class Routes
        {
            private const string Base = "api/v{version:apiVersion}/";

            internal const string Auth = Base + "auth/";
            internal const string Master = Base + "master/";
            internal const string Setup = Base + "setup/";
            internal const string Fm = Base + "fm/";
            internal const string Lease = Base + "lease/";
            internal const string Finance = Base + "finance/";
            internal const string Hr = Base + "hr/";
            internal const string Crm = Base + "crm/";
        }
    }
}
