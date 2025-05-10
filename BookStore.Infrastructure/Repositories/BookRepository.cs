using BookStore.Domain.Entities;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToList();
        }

        public override Book GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.Id == id);
        }
    }

}