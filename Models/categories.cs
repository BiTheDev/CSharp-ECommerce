using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class categories{
        public categories(){
           CatPro = new List<productcategory>();
        }
    

    [Key]
    public int id {get;set;}
    public int usersid{get;set;}

    public string name{get;set;}
    public users user{get;set;}
    public List<productcategory> CatPro{get;set;}
    public DateTime created_at{get;set;}
    public DateTime updated_at{get;set;}
    }
}