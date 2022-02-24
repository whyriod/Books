using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Basket
    {
        public List<BasketItem> Items = new List<BasketItem>();

        public void AddItem(Books b, int q) 
        {
            BasketItem bi = Items.Where(p => p.Book.BookId == b.BookId).First();

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
            double total = Items.Sum(x => x.Quantity * 25) // need to change to actual price later.

            return total;
        }
        //Quantity Price Subtotal
    }

    public class BasketItem
    {
        public int ID { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
