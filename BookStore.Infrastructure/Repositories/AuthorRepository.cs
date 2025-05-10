using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}