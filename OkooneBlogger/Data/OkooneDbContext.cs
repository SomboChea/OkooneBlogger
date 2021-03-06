﻿using Microsoft.EntityFrameworkCore;

namespace OkooneBlogger.Data
{
    public class OkooneDbContext : DbContext
    {
        public OkooneDbContext(DbContextOptions<OkooneDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasMany(a => a.Articles).WithOne(u => u.Author);
            modelBuilder.Entity<Models.User>().HasIndex(a => a.Username).IsUnique();
        }

        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}