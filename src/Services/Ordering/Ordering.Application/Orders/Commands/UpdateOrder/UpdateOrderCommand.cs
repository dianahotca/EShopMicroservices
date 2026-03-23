namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(updateOrderCommand => updateOrderCommand.Order.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(updateOrderCommand => updateOrderCommand.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(updateOrderCommand => updateOrderCommand.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        }
    }
}
