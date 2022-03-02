using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mission7.Models
{
    public class Checkout
    {
        [Key]
        [BindNever]
        public int checkoutId { get; set; }

        [BindNever]
        public ICollection<CartItem> Items { get; set; }

        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter an Address")]
        public string AddressLineOne { get; set; }

        public string AddressLineTwo { get; set; }

        [Required(ErrorMessage = "Please Enter a City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter a State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please Enter a Zip Code")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Enter a Country")]
        public string Country { get; set; }

    }
}
