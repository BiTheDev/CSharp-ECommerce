using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class addressViewModel{
        [Required]
        public string address{get;set;}
        [Required]
        public string apt{get;set;}
        [Required]
        public int zipcode{get;set;}
        [Required]
        public string city{get;set;}
        [Required]
        public string state{get;set;}
        
    }
}