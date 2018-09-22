using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class productcategory{
        public int id{get;set;}
        public int productsid{get;set;}
        public int categoriesid{get;set;}
        public products product{get;set;}
        public categories category{get;set;}
    }
}