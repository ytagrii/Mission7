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
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        //this gets the info about the Books 
        public CartModel(IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        

        //get method for a cart
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        //post method on a cart
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(b => b.BookId == bookId);
           
            cart.AddBook(b, 1);
           
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            //pass the book to the removeitem;
            cart.RemoveItem(cart.Books.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
