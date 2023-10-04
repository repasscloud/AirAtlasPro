using AirAtlasPro.Shared.Helper;

namespace AirAtlasPro.Models.Support
{
    public enum IssueType
    {
        [EnumStringValue("General Support")]
        SUPPORT,

        [EnumStringValue("Account Enquiry")]
        ACCOUNT,

        [EnumStringValue("Billing Question")]
        BILLING
    }
}

