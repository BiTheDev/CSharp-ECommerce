using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class orders{
        public int id{get;set;}
        public int productsid{get;set;}
        public int usersid{get;set;}
        public int amount{get;set;}
        public double price{get;set;}
        public products product{get;set;}
        public users user{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}
    }
}