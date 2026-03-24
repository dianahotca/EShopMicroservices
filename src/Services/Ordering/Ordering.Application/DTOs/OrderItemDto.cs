namespace Ordering.Application.DTOs
{
    public record OrderItemDto(
        Guid ProductId,
        int Quantity,
        decimal Price
        );

    public record OrderItemResponseDto(
     Guid OrderId,
     Guid ProductId,
     int Quantity,
     decimal Price
     );
}
