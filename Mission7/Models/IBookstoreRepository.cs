using System;
using System.Linq;

namespace Mission7.Models
{
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; }

    }
}
