using AutoMapper;
using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderRequestDto> _validator;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Customer> customerRepository,
            IRepository<Book> bookRepository,
            IMapper mapper,
            IValidator<OrderRequestDto> validator)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public void CreateOrder(OrderRequestDto orderDto)
        {
            _validator.ValidateAndThrow(orderDto);
            if (_customerRepository.GetById(orderDto.CustomerId) == null)
                throw new Exception("Customer not found");

            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.Now;
            order.TotalAmount = 0;

            foreach (var detailDto in orderDto.OrderDetails)
            {
                var book = _bookRepository.GetById(detailDto.BookId);
                if (book == null)
                    throw new Exception($"Book with ID {detailDto.BookId} not found");
                if (book.Stock < detailDto.Quantity)
                    throw new Exception($"Not enough stock for book: {book.Title}");

                var detail = new OrderDetail
                {
                    BookId = detailDto.BookId,
                    Quantity = detailDto.Quantity,
                    UnitPrice = book.Price
                };
                order.OrderDetails.Add(detail);
                order.TotalAmount += detail.Quantity * detail.UnitPrice;

                book.Stock -= detailDto.Quantity;
                _bookRepository.Update(book);
            }

            _orderRepository.Add(order);
        }

        public OrderResponseDto GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                throw new Exception("Order not found");
            return _mapper.Map<OrderResponseDto>(order);
        }

        public List<OrderResponseDto> GetOrdersByCustomer(int customerId)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                throw new Exception("Customer not found");
            var orders = _orderRepository.GetAll().Where(o => o.CustomerId == customerId).ToList();
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public List<OrderResponseDto> GetAllOrders()
        {
            var orders = _orderRepository.GetAll();
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }
    }
}