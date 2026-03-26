namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static OrderResponseDto ToOrderResponseDto(this Order order)
        {
            return new OrderResponseDto(
                Id: order.Id.Value,
                CustomerId: order.CustomerId.Value,
                OrderName: order.OrderName.Value,
                ShippingAddress: order.ShippingAddress.ToAddressDto(),
                BillingAddress: order.BillingAddress.ToAddressDto(),
                Payment: order.Payment.ToPaymentDto(),
                Status: order.Status,
                OrderItems: order.OrderItems.Select(oi => oi.ToOrderItemResponseDto()).ToList()
            );
        }

        public static AddressDto ToAddressDto(this Address address)
        {
            return new AddressDto(
                FirstName: address.FirstName,
                LastName: address.LastName,
                EmailAddress: address.EmailAddress ?? string.Empty,
                AddressLine: address.AddressLine,
                Country: address.Country,
                State: address.State,
                ZipCode: address.ZipCode
            );
        }

        public static PaymentDto ToPaymentDto(this Payment payment)
        {
            return new PaymentDto(
                CardName: payment.CardName ?? string.Empty,
                CardNumber: payment.CardNumber,
                Expiration: payment.Expiration,
                Cvv: payment.CVV,
                PaymentMethod: payment.PaymentMethod
            );
        }

        public static OrderItemResponseDto ToOrderItemResponseDto(this OrderItem orderItem)
        {
            return new OrderItemResponseDto(
                OrderId: orderItem.OrderId.Value,
                ProductId: orderItem.ProductId.Value,
                Quantity: orderItem.Quantity,
                Price: orderItem.Price
            );
        }

        public static IEnumerable<OrderResponseDto> ToOrderResponseDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(o => o.ToOrderResponseDto()).ToList();
        }
    }
}
