using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class products{
        public products(){
            productcategory = new List<productcategory>();
            productorder = new List<orders>();
            productCmt = new List<comments>();
        }
        [Key]
        public int id{get;set;}
        public int usersid{get;set;}
        public string name{get;set;}
        public string description{get;set;}
        public string img_name{get;set;}
        public int inventory{get;set;}
        public double price{get;set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get;set;}
        public users user{get;set;}
        public List<productcategory> productcategory{get;set;}
        public List<orders> productorder{get;set;}
        public List<comments> productCmt{get;set;}
    }
}