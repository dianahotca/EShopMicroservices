namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler 
        (IApplicationDbContext dbContext)
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto newOrderValue)
        {
            var shippingAddressFromDto = newOrderValue.ShippingAddress;
            var updatedShippingAddress = Address.Of(shippingAddressFromDto.FirstName, shippingAddressFromDto.LastName, shippingAddressFromDto.EmailAddress, shippingAddressFromDto.AddressLine, shippingAddressFromDto.Country, shippingAddressFromDto.State, shippingAddressFromDto.ZipCode);

            var billingAddressFromDto = newOrderValue.BillingAddress;
            var updatedBillingAddress = Address.Of(billingAddressFromDto.FirstName, billingAddressFromDto.LastName, billingAddressFromDto.EmailAddress, billingAddressFromDto.AddressLine, billingAddressFromDto.Country, billingAddressFromDto.State, billingAddressFromDto.ZipCode);

            var paymentFromDto = newOrderValue.Payment;
            var updatedPayment = Payment.Of(paymentFromDto.CardName, paymentFromDto.CardNumber, paymentFromDto.Expiration, paymentFromDto.Cvv, paymentFromDto.PaymentMethod);

            order.Update(OrderName.Of(newOrderValue.OrderName), updatedShippingAddress, updatedBillingAddress, updatedPayment, newOrderValue.Status);
        }
    }
}
