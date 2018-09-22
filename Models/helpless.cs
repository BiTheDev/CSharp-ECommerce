using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class helpless{

        [Key]
        public int id {get;set;}
        public int usersid{get;set;}
        public int commentsid{get;set;}
        public users user{get;set;}
        public comments comment{get;set;}
    }
}