using System;
namespace Mission7.Models.ViewModels
{
    //this class in the info about how many books were retunred. 
    public class PageInfo
    {
        public int TotalBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling((double)TotalBooks / BooksPerPage);
    }
}
