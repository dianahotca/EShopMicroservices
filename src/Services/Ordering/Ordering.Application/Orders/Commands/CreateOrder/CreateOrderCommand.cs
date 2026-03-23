using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.DTOs;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
    public record CreateOrderResult(Guid OrderId);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(orderDto => orderDto.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(orderDto => orderDto.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(orderDto => orderDto.Order.ShippingAddress).NotNull().WithMessage("ShippingAddress is required");
            RuleFor(orderDto => orderDto.Order.BillingAddress).NotNull().WithMessage("BillingAddress is required");
            RuleFor(orderDto => orderDto.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
