using System.Text.Json.Serialization;

namespace LeaveManagement.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
