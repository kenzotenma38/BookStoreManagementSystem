using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using System.Collections.Generic;

namespace BookStore.Application.Services.Interfaces
{
    public interface IGenreService
    {
        void AddGenre(GenreRequestDto genreDto);
        void UpdateGenre(int id, GenreRequestDto genreDto);
        void DeleteGenre(int id);
        GenreResponseDto GetGenreById(int id);
        List<GenreResponseDto> GetAllGenres();
        List<BookResponseDto> GetBooksByGenre(int genreId);
    }
}