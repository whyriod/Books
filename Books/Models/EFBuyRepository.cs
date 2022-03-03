using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class EFBuyRepository : IBuyRepository
    {
        private BookstoreContext context;
        public EFBuyRepository(BookstoreContext b)
        {
            context = b;
        }

        public IQueryable<Purchase> Purchases => context.Purchases.Include(x => x.Items).ThenInclude(x => x.Book);

        public void FinishPurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Items.Select(x => x.Book));

            if(purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
