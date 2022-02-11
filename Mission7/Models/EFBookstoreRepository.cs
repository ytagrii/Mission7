using System;
using System.Linq;

namespace Mission7.Models
{
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }

        //constructor
        public EFBookstoreRepository(BookstoreContext temp) => context = temp;

        public IQueryable<Book> Books => context.Books;
    }
}
