namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(createOrderCommand => createOrderCommand.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(createOrderCommand => createOrderCommand.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(createOrderCommand => createOrderCommand.Order.ShippingAddress).NotNull().WithMessage("ShippingAddress is required");
            RuleFor(createOrderCommand => createOrderCommand.Order.BillingAddress).NotNull().WithMessage("BillingAddress is required");
            RuleFor(createOrderCommand => createOrderCommand.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
