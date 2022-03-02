using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mission7.Models
{
    public class EFCheckoutRepository : ICheckoutRepository
    {
        private BookstoreContext context;

        public EFCheckoutRepository(BookstoreContext temp) => context = temp;

        //gets the dataset then attackes the books
        public IQueryable<Checkout> checkouts => context.Checkouts.Include(x => x.Items).ThenInclude(x => x.Book);

        //this save a purchase to the database
        public void SavePurchase(Checkout checkout)
        {
            context.AttachRange(checkout.Items.Select(x => x.Book));

            //make sure the purchase does not exist yet hence id is 0
            if(checkout.checkoutId == 0)
            {
                context.Checkouts.Add(checkout);
            }

            context.SaveChanges();
        }

    }
}
