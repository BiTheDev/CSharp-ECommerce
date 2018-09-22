using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class commentViewModel
    {
        [Required]
        public string comment{get;set;}
        [Required]
        public int rating{get;set;}
    }
}