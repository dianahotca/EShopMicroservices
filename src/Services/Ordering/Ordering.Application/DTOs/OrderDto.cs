using Ordering.Domain.Enums;

namespace Ordering.Application.DTOs
{
    public record OrderDto(
        Guid Id, 
        Guid CustomerId,
        string OrderName,
        AddressDto ShippingAddress,
        AddressDto BilolingAddress,
        PaymentDto Payment,
        OrderStatus Status,
        List<OrderItemDto> OrderItems
        );
}
