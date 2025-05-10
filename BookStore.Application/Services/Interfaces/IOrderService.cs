using BookStore.Application.DTOs.Requests;
using BookStore.Application.DTOs.Responses;
using System.Collections.Generic;

namespace BookStore.Application.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(OrderRequestDto orderDto);
        OrderResponseDto GetOrderById(int id);
        List<OrderResponseDto> GetOrdersByCustomer(int customerId);
        List<OrderResponseDto> GetAllOrders();
    }
}