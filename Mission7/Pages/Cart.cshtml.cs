using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission7.Infastructure;
using Mission7.Models;

namespace Mission7.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        //this gets the info about the Books 
        public CartModel(IBookstoreRepository temp) => repo = temp;

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        //get method for a cart
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        //post method on a cart
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(b => b.BookId == bookId);
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddBook(b, 1);
            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
