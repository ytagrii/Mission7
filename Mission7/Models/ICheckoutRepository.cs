using System;
using System.Linq;

namespace Mission7.Models
{
    public interface ICheckoutRepository
    {
        public IQueryable<Checkout> checkouts { get; }

        public void SavePurchase(Checkout checkout)
        {

        }
    }
}
