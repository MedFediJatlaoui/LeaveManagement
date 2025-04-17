using System.Text.Json.Serialization;

namespace LeaveManagement.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveType
    {
        Annual,
        Sick,
        Other
    }
}
