using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService.Context
{
    public class ServiceDBContext : DbContext
    {
        public ServiceDBContext() : base()
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //set Connectionstring
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebAPI;User ID=sa;Password=kensindy;TrustServerCertificate=true;");

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
