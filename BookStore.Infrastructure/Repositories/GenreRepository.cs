using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }
    }
}