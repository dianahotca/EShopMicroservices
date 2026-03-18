using Discount.GRPC;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);
    
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(command => command.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(command => command.Cart.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreBasketCommandHandler
        (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);
            //store basket in database using Marten upsert - if exists -> update, otherwise -> create
            var storedCart = await repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(storedCart.Username);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                //communicate with Discount.Grpc and calculate latest price of product after applying the discount
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }

}
