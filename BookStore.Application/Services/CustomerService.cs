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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerRequestDto> _validator;

        public CustomerService(
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            IMapper mapper,
            IValidator<CustomerRequestDto> validator)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public void AddCustomer(CustomerRequestDto customerDto)
        {
            _validator.ValidateAndThrow(customerDto);
            var customer = _mapper.Map<Customer>(customerDto);
            _customerRepository.Add(customer);
        }

        public void UpdateCustomer(int id, CustomerRequestDto customerDto)
        {
            _validator.ValidateAndThrow(customerDto);
            var existingCustomer = _customerRepository.GetById(id);
            if (existingCustomer == null)
                throw new Exception("Customer not found");

            var updatedCustomer = _mapper.Map<Customer>(customerDto);
            updatedCustomer.Id = id;
            _customerRepository.Update(updatedCustomer);
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                throw new Exception("Customer not found");
            if (_orderRepository.GetAll().Any(o => o.CustomerId == id))
                throw new Exception("Cannot delete customer with orders");
            _customerRepository.Delete(id);
        }

        public CustomerResponseDto GetCustomerById(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                throw new Exception("Customer not found");
            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public List<CustomerResponseDto> GetAllCustomers()
        {
            var customers = _customerRepository.GetAll();
            return _mapper.Map<List<CustomerResponseDto>>(customers);
        }
    }
}