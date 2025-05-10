using BookStore.Application.DTOs.Requests;
using BookStore.Application.Services.Interfaces;
using System;

namespace BookStore.Presentation.Menus
{
    public class BookMenu
    {
        private readonly IBookService _bookService;

        public BookMenu(IBookService bookService)
        {
            _bookService = bookService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Books Menu ***");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. List Books");
                Console.WriteLine("3. Search Books");
                Console.WriteLine("4. Edit Book");
                Console.WriteLine("5. Delete Book");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddBook();
                            break;
                        case "2":
                            ListBooks();
                            break;
                        case "3":
                            SearchBooks();
                            break;
                        case "4":
                            EditBook();
                            break;
                        case "5":
                            DeleteBook();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
        

        private void AddBook()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Book ***");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author ID: ");
            int authorId = int.Parse(Console.ReadLine());
            Console.Write("Genre ID: ");
            int genreId = int.Parse(Console.ReadLine());
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            var bookDto = new BookRequestDto
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                Price = price,
                Stock = stock
            };

            _bookService.AddBook(bookDto);
            Console.WriteLine("Book added successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void ListBooks()
        {
            Console.Clear();
            Console.WriteLine("*** Book List ***");
            var books = _bookService.GetAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.AuthorName}, Genre: {book.GenreName}, Price: {book.Price}, Stock: {book.Stock}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("*** Search Books ***");
            Console.Write("Enter search term (title, author, or genre): ");
            string searchTerm = Console.ReadLine();
            var books = _bookService.SearchBooks(searchTerm);
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.AuthorName}, Genre: {book.GenreName}, Price: {book.Price}, Stock: {book.Stock}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void EditBook()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Book ***");
            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());
            var book = _bookService.GetBookById(id);
            Console.WriteLine($"Current Title: {book.Title}");
            Console.Write("New Title: ");
            string title = Console.ReadLine();
            Console.Write($"Current Author ID: {book.AuthorName}");
            Console.Write("New Author ID: ");
            int authorId = int.Parse(Console.ReadLine());
            Console.Write($"Current Genre ID: {book.GenreName}");
            Console.Write("New Genre ID: ");
            int genreId = int.Parse(Console.ReadLine());
            Console.Write($"Current Price: {book.Price}");
            Console.Write("New Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write($"Current Stock: {book.Stock}");
            Console.Write("New Stock: ");
            int stock = int.Parse(Console.ReadLine());

            var bookDto = new BookRequestDto
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                Price = price,
                Stock = stock
            };

            _bookService.UpdateBook(id, bookDto);
            Console.WriteLine("Book updated successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("*** Delete Book ***");
            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());
            _bookService.DeleteBook(id);
            Console.WriteLine("Book deleted successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}