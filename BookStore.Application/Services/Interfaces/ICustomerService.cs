using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using System.Collections.Generic;

namespace BookStore.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerRequestDto customerDto);
        void UpdateCustomer(int id, CustomerRequestDto customerDto);
        void DeleteCustomer(int id);
        CustomerResponseDto GetCustomerById(int id);
        List<CustomerResponseDto> GetAllCustomers();
    }
}