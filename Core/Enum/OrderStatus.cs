using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "Новый")]
    New = 1,

    [EnumMember(Value = "Принятый в обработку")]
    Confirmed = 2,

    [EnumMember(Value = "В обработке...")]
    InProgress = 3,

    [EnumMember(Value = "Ожидание оплаты")]
    WaitingForPayment = 4,

    [EnumMember(Value = "Завершенный")]
    Completed = 5,

    [EnumMember(Value = "Отменный")]
    Cancelled = 6,

    [EnumMember(Value = "")]
    OnHold = 7,
    Default
}