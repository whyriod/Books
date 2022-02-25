using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public interface IBookstoreRepository
    {
        IQueryable<Book> BookRepo { get; }
    }
}
