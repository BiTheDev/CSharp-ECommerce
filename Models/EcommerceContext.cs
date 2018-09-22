using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models{
    public class EcommerceContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<users> users{get;set;}

        public DbSet<categories> categories{get;set;}

        public DbSet<products> products{get;set;}

        public DbSet<productcategory> productcategory{get;set;}

        public DbSet<orders> orders{get;set;}
        public DbSet<comments> comments{get;set;}
        public DbSet<addresses> addresses{get;set;}

        public DbSet<helpful> helpful{get;set;}
        public DbSet<helpless> helpless{get;set;}
        
    }
}