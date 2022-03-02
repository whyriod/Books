using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Basket
    {
        //Set up and initalize the thing.
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public virtual void AddItem(Book b, int q) 
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

        public virtual void RemoveItem(Book b)
        {
            Items.RemoveAll(x => x.Book.BookId == b.BookId);
        }

        public virtual void ClearBasket()
        {
            Items.Clear();
        }

        public virtual double CalculateTotal()
        {
            double total = Items.Sum(x => x.Quantity * x.Book.Price); // need to change to actual price later.

            return total;
        }
        //Quantity Price Subtotal
    }


    public class BasketItem
    {
        [Key]
        public int ID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
