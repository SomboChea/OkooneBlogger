using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OkooneBlogger.Data
{
    public class OkooneDbContext : DbContext
    {
        public OkooneDbContext(DbContextOptions<OkooneDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasMany(a => a.Articles).WithOne(u => u.Author);
        }

        public DbSet<Models.Article> Articles { get; set; } 
        public DbSet<Models.User> Users { get; set; }
    }
}
