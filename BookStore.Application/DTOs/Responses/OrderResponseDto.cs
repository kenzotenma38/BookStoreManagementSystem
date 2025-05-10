namespace BookStore.Application.DTOs.Responses
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailResponseDto> OrderDetails { get; set; }
    }

    public class OrderDetailResponseDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}