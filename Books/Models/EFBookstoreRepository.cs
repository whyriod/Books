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

        public void SaveBook(Book b)
        {
            _context.SaveChanges();
        }
        public void CreateBook(Book b)
        {
            _context.Add(b);
            _context.SaveChanges();
        }
        public void DeleteBook(Book b)
        {
            _context.Remove(b);
            _context.SaveChanges();
        }
    }
}
