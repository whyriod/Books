using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Basket
    {
        //Set up and initalize the thing.
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public void AddItem(Book b, int q) 
        {
            BasketItem bi = Items.Where(p => p.Book.BookId == b.BookId).FirstOrDefault();

            if(bi == null)
            {
                Items.Add(new BasketItem 
                { 
                    Book = b,
                    Quantity = q
                });
            }
            else
            {
                bi.Quantity += q;
            }
        }

        public double CalculateTotal()
        {
            double total = Items.Sum(x => x.Quantity * x.Book.Price); // need to change to actual price later.

            return total;
        }
        //Quantity Price Subtotal
    }

    public class BasketItem
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
