using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }
    }
}