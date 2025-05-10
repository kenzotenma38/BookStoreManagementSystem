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
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GenreRequestDto> _validator;

        public GenreService(
            IRepository<Genre> genreRepository,
            IRepository<Book> bookRepository,
            IMapper mapper,
            IValidator<GenreRequestDto> validator)
        {
            _genreRepository = genreRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public void AddGenre(GenreRequestDto genreDto)
        {
            _validator.ValidateAndThrow(genreDto);
            var genre = _mapper.Map<Genre>(genreDto);
            _genreRepository.Add(genre);
        }

        public void UpdateGenre(int id, GenreRequestDto genreDto)
        {
            _validator.ValidateAndThrow(genreDto);
            var existingGenre = _genreRepository.GetById(id);
            if (existingGenre == null)
                throw new Exception("Genre not found");

            var updatedGenre = _mapper.Map<Genre>(genreDto);
            updatedGenre.Id = id;
            _genreRepository.Update(updatedGenre);
        }

        public void DeleteGenre(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
                throw new Exception("Genre not found");
            if (_bookRepository.GetAll().Any(b => b.GenreId == id))
                throw new Exception("Cannot delete genre with books");
            _genreRepository.Delete(id);
        }

        public GenreResponseDto GetGenreById(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
                throw new Exception("Genre not found");
            return _mapper.Map<GenreResponseDto>(genre);
        }

        public List<GenreResponseDto> GetAllGenres()
        {
            var genres = _genreRepository.GetAll();
            return _mapper.Map<List<GenreResponseDto>>(genres);
        }

        public List<BookResponseDto> GetBooksByGenre(int genreId)
        {
            var genre = _genreRepository.GetById(genreId);
            if (genre == null)
                throw new Exception("Genre not found");
            var books = _bookRepository.GetAll().Where(b => b.GenreId == genreId).ToList();
            return _mapper.Map<List<BookResponseDto>>(books);
        }
    }
}