using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext _context { get; set;}
        public EFBookstoreRepository(BookstoreContext b) => _context = b;
        public IQueryable<Book> BookRepo => _context.Books;
    }
}
