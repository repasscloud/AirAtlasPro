using AirAtlasPro.Shared.Helper;

namespace AirAtlasPro.Models.Support
{
    public enum IssuePriority
    {
        [EnumStringValue("Low")]
        LOW,

        [EnumStringValue("Medium")]
        MEDIUM,

        [EnumStringValue("High")]
        HIGH,

        [EnumStringValue("Critical")]
        CRITICAL
    }
}

