
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository
        (IBasketRepository repository, IDistributedCache cache) 
        : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(username, cancellationToken);
            await cache.RemoveAsync(username, cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(username, cancellationToken);

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }

            var basket = await repository.GetBasket(username, cancellationToken);
            await cache.SetStringAsync(username, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(cart, cancellationToken);
            await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(cart), cancellationToken);

            return cart;
        }
    }
}
