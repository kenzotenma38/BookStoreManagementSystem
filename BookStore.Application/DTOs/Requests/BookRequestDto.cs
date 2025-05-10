namespace BookStore.Application.DTOs.Requests
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}