using System;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Domain.Models;

namespace Tutorial.Restful.Data
{
    public class RestfulContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public RestfulContext(DbContextOptions<RestfulContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
