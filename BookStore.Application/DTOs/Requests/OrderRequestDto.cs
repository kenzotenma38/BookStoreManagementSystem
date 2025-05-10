namespace BookStore.Application.DTOs.Requests
{
    public class OrderRequestDto
    {
        public int CustomerId { get; set; }
        public List<OrderDetailRequestDto> OrderDetails { get; set; }
    }

    public class OrderDetailRequestDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}