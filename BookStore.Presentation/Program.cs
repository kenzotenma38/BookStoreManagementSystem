using AutoMapper;
using BookStore.Application.Mappings;
using BookStore.Application.Services;
using BookStore.Application.Validations;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repositories;
using BookStore.Presentation.Menus;

namespace BookStore.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new AppDbContext();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperConfig.CreateMapper();

            var bookRepository = new BookRepository(dbContext);
            var authorRepository = new AuthorRepository(dbContext);
            var genreRepository = new GenreRepository(dbContext);
            var customerRepository = new CustomerRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);

            var bookService = new BookService(bookRepository, authorRepository, genreRepository, mapper, new BookRequestValidator());
            var authorService = new AuthorService(authorRepository, bookRepository, mapper, new AuthorRequestValidator());
            var genreService = new GenreService(genreRepository, bookRepository, mapper, new GenreRequestValidator());
            var customerService = new CustomerService(customerRepository, orderRepository, mapper, new CustomerRequestValidator());
            var orderService = new OrderService(orderRepository, customerRepository, bookRepository, mapper, new OrderRequestValidator());

            var mainMenu = new MainMenu(bookService, authorService, genreService, customerService, orderService);
            mainMenu.Show();
        }
    }
}