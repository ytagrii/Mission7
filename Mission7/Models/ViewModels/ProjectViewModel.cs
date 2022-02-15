using System;
using System.Linq;
namespace Mission7.Models.ViewModels
{
    public class ProjectViewModel
    {
        public IQueryable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
