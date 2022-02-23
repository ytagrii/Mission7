using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mission7.Models;

namespace Mission7.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public TypesViewComponent(IBookstoreRepository temp) => repo = temp;

        //this function here gets all the book categories and does a distinct for all of them
        public IViewComponentResult Invoke()
        {
            ViewBag.BookCategory = RouteData.Values["bookCategory"];
            var types = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}
