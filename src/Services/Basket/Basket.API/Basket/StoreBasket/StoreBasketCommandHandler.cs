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
        (IBasketRepository repository) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            //store basket in database using Marten upsert - if exists -> update, otherwise -> create)
            var storedCart = await repository.StoreBasket(cart, cancellationToken);

            return new StoreBasketResult(storedCart.Username);
        }
    }
}
