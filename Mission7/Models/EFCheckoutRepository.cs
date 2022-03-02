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

        public void SavePurchase(Checkout checkout)
        {
            context.AttachRange(checkout.Items.Select(x => x.Book));

            if(checkout.checkoutId == 0)
            {
                context.Checkouts.Add(checkout);
            }

            context.SaveChanges();
        }

    }
}
