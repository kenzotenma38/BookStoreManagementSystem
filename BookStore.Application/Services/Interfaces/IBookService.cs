using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using System.Collections.Generic;

namespace BookStore.Application.Services.Interfaces
{
    public interface IBookService
    {
        void AddBook(BookRequestDto bookDto);
        void UpdateBook(int id, BookRequestDto bookDto);
        void DeleteBook(int id);
        BookResponseDto GetBookById(int id);
        List<BookResponseDto> GetAllBooks();
        List<BookResponseDto> SearchBooks(string searchTerm);
    }
}