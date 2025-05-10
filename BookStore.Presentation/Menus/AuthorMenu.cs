using BookStore.Application.DTOs.Requests;
using BookStore.Application.Services.Interfaces;
using System;

namespace BookStore.Presentation.Menus
{
    public class AuthorMenu
    {
        private readonly IAuthorService _authorService;

        public AuthorMenu(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Authors Menu ***");
                Console.WriteLine("1. Add New Author");
                Console.WriteLine("2. List Authors");
                Console.WriteLine("3. View Author's Books");
                Console.WriteLine("4. Edit Author");
                Console.WriteLine("5. Delete Author");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddAuthor();
                            break;
                        case "2":
                            ListAuthors();
                            break;
                        case "3":
                            ViewAuthorBooks();
                            break;
                        case "4":
                            EditAuthor();
                            break;
                        case "5":
                            DeleteAuthor();
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

        private void AddAuthor()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Author ***");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            var authorDto = new AuthorRequestDto { Name = name };
            _authorService.AddAuthor(authorDto);
            Console.WriteLine("Author added successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void ListAuthors()
        {
            Console.Clear();
            Console.WriteLine("*** Author List ***");
            var authors = _authorService.GetAllAuthors();
            foreach (var author in authors)
            {
                Console.WriteLine($"ID: {author.Id}, Name: {author.Name}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewAuthorBooks()
        {
            Console.Clear();
            Console.WriteLine("*** View Author's Books ***");
            Console.Write("Enter Author ID: ");
            int id = int.Parse(Console.ReadLine());
            var books = _authorService.GetBooksByAuthor(id);
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Genre: {book.GenreName}, Price: {book.Price}, Stock: {book.Stock}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void EditAuthor()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Author ***");
            Console.Write("Enter Author ID: ");
            int id = int.Parse(Console.ReadLine());
            var author = _authorService.GetAuthorById(id);
            Console.WriteLine($"Current Name: {author.Name}");
            Console.Write("New Name: ");
            string name = Console.ReadLine();

            var authorDto = new AuthorRequestDto { Name = name };
            _authorService.UpdateAuthor(id, authorDto);
            Console.WriteLine("Author updated successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteAuthor()
        {
            Console.Clear();
            Console.WriteLine("*** Delete Author ***");
            Console.Write("Enter Author ID: ");
            int id = int.Parse(Console.ReadLine());
            _authorService.DeleteAuthor(id);
            Console.WriteLine("Author deleted successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}