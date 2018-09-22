using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class comments{
        public comments(){
            helpful = new List<helpful>();
            helpless = new List<helpless>();
        }
        [Key]
        public int id{get;set;}
        public int usersid{get;set;}
        public int productsid{get;set;}
        public string comment{get;set;}
        public int rating{get;set;}
        public DateTime created_at{get;set;}
        public users user{get;set;}
        public products protcmt{get;set;}
        public List<helpful> helpful{get;set;}
        public List<helpless> helpless{get;set;}
    }
}