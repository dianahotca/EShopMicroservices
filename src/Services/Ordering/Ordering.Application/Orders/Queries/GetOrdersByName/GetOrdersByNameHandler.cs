namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler
        (IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .Where(order => order.OrderName.Value.Contains(query.Name))
                .OrderBy(order => order.OrderName)
                .ToListAsync(cancellationToken);

            var orderDtos = orders.Select(o => o.ToOrderDto()).ToList();

            return new GetOrdersByNameResult(orderDtos);
        }
    }
}
