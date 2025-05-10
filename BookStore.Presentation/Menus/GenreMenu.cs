using BookStore.Application.DTOs.Requests;
using BookStore.Application.Services.Interfaces;
using System;

namespace BookStore.Presentation.Menus
{
    public class GenreMenu
    {
        private readonly IGenreService _genreService;

        public GenreMenu(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Genres Menu ***");
                Console.WriteLine("1. Add New Genre");
                Console.WriteLine("2. List Genres");
                Console.WriteLine("3. View Genre's Books");
                Console.WriteLine("4. Edit Genre");
                Console.WriteLine("5. Delete Genre");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddGenre();
                            break;
                        case "2":
                            ListGenres();
                            break;
                        case "3":
                            ViewGenreBooks();
                            break;
                        case "4":
                            EditGenre();
                            break;
                        case "5":
                            DeleteGenre();
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

        private void AddGenre()
        {
            Console.Clear();
            Console.WriteLine("*** Add New Genre ***");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            var genreDto = new GenreRequestDto { Name = name };
            _genreService.AddGenre(genreDto);
            Console.WriteLine("Genre added successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void ListGenres()
        {
            Console.Clear();
            Console.WriteLine("*** Genre List ***");
            var genres = _genreService.GetAllGenres();
            foreach (var genre in genres)
            {
                Console.WriteLine($"ID: {genre.Id}, Name: {genre.Name}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewGenreBooks()
        {
            Console.Clear();
            Console.WriteLine("*** View Genre's Books ***");
            Console.Write("Enter Genre ID: ");
            int id = int.Parse(Console.ReadLine());
            var books = _genreService.GetBooksByGenre(id);
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.AuthorName}, Price: {book.Price}, Stock: {book.Stock}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void EditGenre()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Genre ***");
            Console.Write("Enter Genre ID: ");
            int id = int.Parse(Console.ReadLine());
            var genre = _genreService.GetGenreById(id);
            Console.WriteLine($"Current Name: {genre.Name}");
            Console.Write("New Name: ");
            string name = Console.ReadLine();

            var genreDto = new GenreRequestDto { Name = name };
            _genreService.UpdateGenre(id, genreDto);
            Console.WriteLine("Genre updated successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteGenre()
        {
            Console.Clear();
            Console.WriteLine("*** Delete Genre ***");
            Console.Write("Enter Genre ID: ");
            int id = int.Parse(Console.ReadLine());
            _genreService.DeleteGenre(id);
            Console.WriteLine("Genre deleted successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}