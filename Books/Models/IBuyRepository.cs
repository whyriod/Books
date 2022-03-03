using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public interface IBuyRepository
    {
        IQueryable<Purchase> Purchases { get; }

        public void FinishPurchase(Purchase purchase)
        {

        }
    }
}
