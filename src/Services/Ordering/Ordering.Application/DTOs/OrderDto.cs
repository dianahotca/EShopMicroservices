using Ordering.Domain.Enums;

namespace Ordering.Application.DTOs
{
    /// <summary>
    /// Used for Create and Update requests (Id comes from route or is generated)
    /// </summary>
    public record OrderDto(
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> OrderItems
    );

    /// <summary>
    /// Used for Get responses (includes the generated Order Id)
    /// </summary>
    public record OrderResponseDto(
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemResponseDto> OrderItems
    );
}
