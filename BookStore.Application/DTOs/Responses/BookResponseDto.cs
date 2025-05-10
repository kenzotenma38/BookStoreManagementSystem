namespace BookStore.Application.DTOs.Responses
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}