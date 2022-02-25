using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Books.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public CartModel (IBookstoreRepository r)
        {
            repo = r;
        }

        public Basket Basket { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost(int bookId)
        {
            var book = repo.BookRepo.FirstOrDefault(x => x.BookId == bookId);
            Basket = new Basket();
            Basket.AddItem(book, 1);

            return RedirectToPage();
        }
    }
}
