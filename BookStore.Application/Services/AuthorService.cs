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
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AuthorRequestDto> _validator;

        public AuthorService(
            IRepository<Author> authorRepository,
            IRepository<Book> bookRepository,
            IMapper mapper,
            IValidator<AuthorRequestDto> validator)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public void AddAuthor(AuthorRequestDto authorDto)
        {
            _validator.ValidateAndThrow(authorDto);
            var author = _mapper.Map<Author>(authorDto);
            _authorRepository.Add(author);
        }

        public void UpdateAuthor(int id, AuthorRequestDto authorDto)
        {
            _validator.ValidateAndThrow(authorDto);
            var existingAuthor = _authorRepository.GetById(id);
            if (existingAuthor == null)
                throw new Exception("Author not found");

            var updatedAuthor = _mapper.Map<Author>(authorDto);
            updatedAuthor.Id = id;
            _authorRepository.Update(updatedAuthor);
        }

        public void DeleteAuthor(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
                throw new Exception("Author not found");
            if (_bookRepository.GetAll().Any(b => b.AuthorId == id))
                throw new Exception("Cannot delete author with books");
            _authorRepository.Delete(id);
        }

        public AuthorResponseDto GetAuthorById(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
                throw new Exception("Author not found");
            return _mapper.Map<AuthorResponseDto>(author);
        }

        public List<AuthorResponseDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAll();
            return _mapper.Map<List<AuthorResponseDto>>(authors);
        }

        public List<BookResponseDto> GetBooksByAuthor(int authorId)
        {
            var author = _authorRepository.GetById(authorId);
            if (author == null)
                throw new Exception("Author not found");
            var books = _bookRepository.GetAll().Where(b => b.AuthorId == authorId).ToList();
            return _mapper.Map<List<BookResponseDto>>(books);
        }
    }
}