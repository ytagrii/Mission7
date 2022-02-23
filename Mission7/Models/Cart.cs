using System;
using System.Collections.Generic;
using System.Linq;

namespace Mission7.Models
{
    public class Cart
    {
        public List<CartItem> Books { get; set; } = new List<CartItem>();

        public void AddBook(Book bk, int qty)
        {
            CartItem line = Books
                .Where(b => b.Book.BookId == bk.BookId)
                .FirstOrDefault();

            //make sure that if book is already in cart it doest add it again. Just updates quantity 
            if(line == null)
            {
                Books.Add(new CartItem
                {
                    Book = bk,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        //this function gets the total amount for all the books in the cart
        public double GetTotal()
        {
            double sum = Books.Sum(b => b.Quantity * b.Book.Price);
            return sum;
        }
    }


    //this class is the info we need to store about
    //each book in the cart
    public class CartItem
    {
        public int ItemID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        
    }
}
