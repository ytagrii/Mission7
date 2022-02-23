using System;
using System.Linq;
namespace Mission7.Models.ViewModels
{
    //this class was set up so we can pass it to the index page and it'll have
    //    all the page info it needs to build the tag helpers and also build the book info stuff
    public class ProjectViewModel
    {
        public IQueryable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
