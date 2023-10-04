using AirAtlasPro.Shared.Helper;

namespace AirAtlasPro.Models.Support
{
    public enum IssueStatus
    {
        [EnumStringValue("Open")]
        OPEN,

        [EnumStringValue("In Progress")]
        ASSIGNED,

        [EnumStringValue("Waiting for Response")]
        PENDING,

        [EnumStringValue("Closed")]
        CLOSED
    }
}

