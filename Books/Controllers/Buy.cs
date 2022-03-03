using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Controllers
{
    public class Buy : Controller
    {
        private IBuyRepository repo { get; set; }
        private Basket basket { get; set; }
        public Buy(IBuyRepository ib, Basket b)
        {
            repo = ib;
            basket = b;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }

        [HttpPost]
        public IActionResult Checkout(Purchase purchase)
        {
            if (basket.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, You're basket is empty!");
            }

            if (ModelState.IsValid)
            {
                purchase.Items = basket.Items.ToArray();
                repo.FinishPurchase(purchase);
                basket.ClearBasket();

                return RedirectToPage("/CheckoutCompleted");
            }
            else
            {
                return View();
            }
        }
    }
}
