using BookStore.Application.Services.Interfaces;
using System;

namespace BookStore.Presentation.Menus
{
    public class MainMenu
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public MainMenu(
            IBookService bookService,
            IAuthorService authorService,
            IGenreService genreService,
            ICustomerService customerService,
            IOrderService orderService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _customerService = customerService;
            _orderService = orderService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** BOOKSTORE SYSTEM ***");
                Console.WriteLine("1. Books");
                Console.WriteLine("2. Authors");
                Console.WriteLine("3. Genres");
                Console.WriteLine("4. Customers and Orders");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        new BookMenu(_bookService).Show();
                        break;
                    case "2":
                        new AuthorMenu(_authorService).Show();
                        break;
                    case "3":
                        new GenreMenu(_genreService).Show();
                        break;
                    case "4":
                        new CustomerOrderMenu(_customerService, _orderService).Show();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}