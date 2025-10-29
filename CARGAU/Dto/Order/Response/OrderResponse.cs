using Core.Enum;

namespace API.Dto.Order.Response;

/// <summary>
/// Ответ с информацией о заказе для клиента
/// </summary>
public record class OrderResponse
(
    Guid Id,
    OrderStatus Status,
    DateTime CreatedAt,
    decimal TotalPrice
);
