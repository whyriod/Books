using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Infastructure;
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
        public string ReturnUrl { get; set; } //Make it persist.

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            //?? Null Coalesing Operator. If the first isnt null use that, else use the thing on the right.
            var book = repo.BookRepo.FirstOrDefault(x => x.BookId == bookId);
            Basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            Basket.AddItem(book, 1);

            HttpContext.Session.SetJson("basket", Basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
