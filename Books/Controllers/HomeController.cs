using Books.Models;
using Books.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    public class HomeController : Controller
    {

        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository r) => repo = r;

        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BookViewModel
            {
                Books = repo.BookRepo
                .Where(c => c.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),
                PageInfo = new PageInfo
                {
                    TotalNumBooks =
                        category == null ? repo.BookRepo.Count() : repo.BookRepo.Where(x => x.Category == category).Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }

            };

/*            var books = repo.BookRepo.OrderBy(b => b.Title).Skip((pageNum - 1) * pageSize).Take(pageSize);*/
            return View(x);
        }

/*        => View(repo.BookRepo.OrderBy(b => b.Title).Take(10));*/

    }
}
