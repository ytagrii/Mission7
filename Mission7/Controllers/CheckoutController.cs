using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission7.Models;
using Mission7.Models.ViewModels;

namespace Mission7.Controllers
{
    public class CheckoutController : Controller
    {
        //before using repository pattern make sure to add it to the startup.cs file
        private ICheckoutRepository repo { get; set; }
        private Cart cart { get; set; }

        //constructor 
        public CheckoutController(ICheckoutRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        //checkout get method
        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Checkout());
        }

        //checkout posst method
        [HttpPost]
        public IActionResult Checkout(Checkout checkout)
        {
            //make sure the cart has books in it
            if(cart.Books.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry Your Cart is Empty");
            }

            //check to make sure the form is valid 
            if (ModelState.IsValid)
            {
                checkout.Items = cart.Books.ToArray();
                repo.SavePurchase(checkout);
                cart.ClearCart();
                return RedirectToPage("/PurchaseCompleted");
            }
            else
            {
                return View();
            }
            
        }
    }
}
