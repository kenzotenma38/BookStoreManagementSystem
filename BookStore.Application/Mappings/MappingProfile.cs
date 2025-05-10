using AutoMapper;
using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book
            CreateMap<BookRequestDto, Book>();
            CreateMap<Book, BookResponseDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            // Author
            CreateMap<AuthorRequestDto, Author>();
            CreateMap<Author, AuthorResponseDto>();

            // Genre
            CreateMap<GenreRequestDto, Genre>();
            CreateMap<Genre, GenreResponseDto>();

            // Customer
            CreateMap<CustomerRequestDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();

            // Order
            CreateMap<OrderRequestDto, Order>();
            CreateMap<OrderDetailRequestDto, OrderDetail>();
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<OrderDetail, OrderDetailResponseDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}