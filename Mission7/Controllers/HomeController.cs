using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission7.Models;
using Mission7.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mission7.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;
        }
        //Index page contorller for get methods 
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {
            int numberOfBooks = 10;

            var books = new ProjectViewModel
            {
                Books = repo.Books.
                Where(b => b.Category == bookCategory || bookCategory == null).
                OrderBy(b => b.Title).
                Skip((pageNum - 1) * numberOfBooks).
                Take(numberOfBooks),
                PageInfo = new PageInfo
                {
                    //this is where the total pages needed comes into play
                    TotalBooks = (bookCategory == null ?repo.Books.Count() :
                        repo.Books.Where(b => b.Category == bookCategory).Count()
                    ),
                    BooksPerPage = numberOfBooks,
                    CurrentPage = pageNum
                }
            };
            return View(books);
        }
    }
}
