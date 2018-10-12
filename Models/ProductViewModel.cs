using System;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Name{get;set;}
        [Required]
        public string description{get;set;}
        [Required]
        public double price{get;set;}

        [Required]
        public int inventory{get;set;} 

        public string img{get;set;}
    }
}