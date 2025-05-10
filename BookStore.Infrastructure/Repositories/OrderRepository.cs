using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
                .ToList();
        }

        public override Order GetById(int id)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Book)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}