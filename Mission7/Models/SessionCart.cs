using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission7.Infastructure;

namespace Mission7.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //seeing if a session cart already exisits. If there is none it creates a new session cart. 
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }


        //overriding AddBook method in Cart.cs
        public override void AddBook(Book bk, int qty)
        {
            base.AddBook(bk, qty);
            Session.SetJson("Cart", this);
        }

        public override void RemoveItem(Book bk)
        {
            base.RemoveItem(bk);
            Session.SetJson("Cart", this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }

    }
}
