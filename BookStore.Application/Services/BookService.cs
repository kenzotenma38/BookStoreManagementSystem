using AutoMapper;
using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<BookRequestDto> _validator;

        public BookService(
            IRepository<Book> bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Genre> genreRepository,
            IMapper mapper,
            IValidator<BookRequestDto> validator)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public void AddBook(BookRequestDto bookDto)
        {
            _validator.ValidateAndThrow(bookDto);
            if (_authorRepository.GetById(bookDto.AuthorId) == null || _genreRepository.GetById(bookDto.GenreId) == null)
                throw new Exception("Invalid Author or Genre");

            var book = _mapper.Map<Book>(bookDto);
            _bookRepository.Add(book);
        }

        public void UpdateBook(int id, BookRequestDto bookDto)
        {
            _validator.ValidateAndThrow(bookDto);
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
                throw new Exception("Book not found");
            if (_authorRepository.GetById(bookDto.AuthorId) == null || _genreRepository.GetById(bookDto.GenreId) == null)
                throw new Exception("Invalid Author or Genre");

            var updatedBook = _mapper.Map<Book>(bookDto);
            updatedBook.Id = id;
            _bookRepository.Update(updatedBook);
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                throw new Exception("Book not found");
            _bookRepository.Delete(id);
        }

        public BookResponseDto GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
                throw new Exception("Book not found");
            return _mapper.Map<BookResponseDto>(book);
        }

        public List<BookResponseDto> GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            return _mapper.Map<List<BookResponseDto>>(books);
        }

        public List<BookResponseDto> SearchBooks(string searchTerm)
        {
            var books = _bookRepository.GetAll()
                .Where(b => b.Title.Contains(searchTerm) ||
                            b.Author.Name.Contains(searchTerm) ||
                            b.Genre.Name.Contains(searchTerm))
                .ToList();
            return _mapper.Map<List<BookResponseDto>>(books);
        }
    }
}