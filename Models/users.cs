using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class users{

        public users(){
            Product = new List<products>();
            Categories = new List<categories>();
            UserOrder = new List<orders>();
            UserAddress = new List<addresses>();
            UserCmt = new List<comments>();
            helpful = new List<helpful>();
            helpless = new List<helpless>();
        }
        
        [Key]
        public int id{get;set;}
        

        [Required]
        public string first_name{get; set;}

        [Required]
        public string last_name{get; set;}


        [Required]
        public string email {get; set;}

        [Required]
        public string password {get; set;}

        public string level{get;set;}

        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}

        public List<products> Product{get;set;}

        public List<categories> Categories{get;set;}

        public List<orders> UserOrder{get;set;}
        public List<comments> UserCmt{get;set;}
        public List<addresses> UserAddress{get;set;}

        public List<helpful> helpful{get;set;}
        public List<helpless> helpless{get;set;}


    }
}