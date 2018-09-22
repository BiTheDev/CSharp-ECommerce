using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class addresses{
        [Key]
        public int id{get;set;}
        public int usersid{get;set;}
        public string address{get;set;}
        public string apt{get;set;}
        public int zipcode{get;set;}
        public string city{get;set;}
        public string state{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at {get;set;}
        public users user{get;set;}

    }
}