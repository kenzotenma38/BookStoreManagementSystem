using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using System.Collections.Generic;

namespace BookStore.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        void AddAuthor(AuthorRequestDto authorDto);
        void UpdateAuthor(int id, AuthorRequestDto authorDto);
        void DeleteAuthor(int id);
        AuthorResponseDto GetAuthorById(int id);
        List<AuthorResponseDto> GetAllAuthors();
        List<BookResponseDto> GetBooksByAuthor(int authorId);
    }
}