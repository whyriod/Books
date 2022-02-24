using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public CategoryViewComponent(IBookstoreRepository b)
        {
            repo = b;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Category = RouteData?.Values["category"];

            var types = repo.BookRepo
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);
            return View(types);
        }
    }
}
