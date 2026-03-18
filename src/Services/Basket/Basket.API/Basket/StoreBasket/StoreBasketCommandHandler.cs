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
            //TODO: communicate with Dsicount.Grpc and calculate latest price of product after applying the discount


            //store basket in database using Marten upsert - if exists -> update, otherwise -> create
            var storedCart = await repository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(storedCart.Username);
        }
    }
}
