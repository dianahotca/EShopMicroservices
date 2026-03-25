namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = MapOrderDtoToOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Order MapOrderDtoToOrder(OrderDto orderDto)
        {
            var shippingAddressFromDto = orderDto.ShippingAddress;
            var shippingAddress = Address.Of(shippingAddressFromDto.FirstName, shippingAddressFromDto.LastName, shippingAddressFromDto.EmailAddress, shippingAddressFromDto.AddressLine, shippingAddressFromDto.Country, shippingAddressFromDto.State, shippingAddressFromDto.ZipCode);

            var billingAddressFromDto = orderDto.BillingAddress;
            var billingAddress = Address.Of(billingAddressFromDto.FirstName, billingAddressFromDto.LastName, billingAddressFromDto.EmailAddress, billingAddressFromDto.AddressLine, billingAddressFromDto.Country, billingAddressFromDto.State, billingAddressFromDto.ZipCode);

            var paymentFromDto = orderDto.Payment;
            var payment = Payment.Of(paymentFromDto.CardName, paymentFromDto.CardNumber, paymentFromDto.Expiration, paymentFromDto.Cvv, paymentFromDto.PaymentMethod);

            var newOrder = Order.Create(
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress,
                billingAddress,
                payment
                );

            foreach(var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }

            return newOrder;
        }
    }
}
