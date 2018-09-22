using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class CategoryViewModel
    {
        [Required]
        public string Name{get;set;}
    }
}