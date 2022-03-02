using System;
using Microsoft.AspNetCore.Mvc;
using Mission7.Models;

namespace Mission7.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService) {
            cart = cartService;
        }

        //this sends the info for the cart to the Views/Shared/Components/CartSummary/Default.cshtml
        public IViewComponentResult Invoke() {
            return View(cart);
            //return RedirectToPage("/cart");
        }
    }
}
