﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 22-Nov-18 11:47:15 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using OkooneBlogger.Models;
using User = OkooneBlogger.Models.User;
using Article = OkooneBlogger.Models.Article;

namespace Data
{

    public partial class OkooneBlogModel : DbContext
    {

        public OkooneBlogModel() :
            base()
        {
            OnCreated();
        }

        public OkooneBlogModel(DbContextOptions<OkooneBlogModel> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=OkooneBlog;Integrated Security=True;Persist Security Info=True");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<Article> Articles
        {
            get;
            set;
        }

        public virtual DbSet<User> Users
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this.ArticleMapping(modelBuilder);
            //this.CustomizeArticleMapping(modelBuilder);

            //this.UserMapping(modelBuilder);
            //this.CustomizeUserMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            //CustomizeMapping(ref modelBuilder);
        }
    
        #region Article Mapping

        private void ArticleMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable(@"Articles", @"dbo");
            modelBuilder.Entity<Article>().Property<int>(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Article>().Property<string>(x => x.Title).HasColumnName(@"Title").HasColumnType(@"nvarchar(255)").ValueGeneratedNever().HasMaxLength(255);
            modelBuilder.Entity<Article>().Property<string>(x => x.Description).HasColumnName(@"Description").HasColumnType(@"nvarchar(max)").ValueGeneratedNever();
            modelBuilder.Entity<Article>().Property<System.DateTime?>(x => x.Date).HasColumnName(@"Date").HasColumnType(@"datetime2").ValueGeneratedNever();
            modelBuilder.Entity<Article>().Property<int?>(x => x.AuthorId).HasColumnName(@"AuthorId").HasColumnType(@"int").ValueGeneratedNever();
            modelBuilder.Entity<OkooneBlogger.Data.Article>().HasKey(@"Id");
        }
	
        partial void CustomizeArticleMapping(ModelBuilder modelBuilder);

        #endregion
    
        #region User Mapping

        private void UserMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(@"Users", @"dbo");
            modelBuilder.Entity<User>().Property<int>(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property<string>(x => x.FullName).HasColumnName(@"FullName").HasColumnType(@"nvarchar(50)").ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<User>().Property<string>(x => x.Username).HasColumnName(@"Username").HasColumnType(@"varchar(30)").ValueGeneratedNever().HasMaxLength(30);
            modelBuilder.Entity<User>().Property<string>(x => x.Email).HasColumnName(@"Email").HasColumnType(@"varchar(50)").ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<User>().Property<string>(x => x.Password).HasColumnName(@"Password").HasColumnType(@"varchar(100)").ValueGeneratedNever().HasMaxLength(100);
            modelBuilder.Entity<User>().Property<System.DateTime?>(x => x.Date).HasColumnName(@"Date").HasColumnType(@"datetime2").ValueGeneratedNever();
            modelBuilder.Entity<User>().HasKey(@"Id");
        }
	
        partial void CustomizeUserMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {

        #region Article Navigation properties

            modelBuilder.Entity<Article>().HasOne(x => x.Author).WithMany(op => op.Articles).IsRequired(false).HasForeignKey(@"AuthorId");

        #endregion

        #region User Navigation properties

            modelBuilder.Entity<User>().HasMany(x => x.Articles).WithOne(op => op.Author).IsRequired(false).HasForeignKey(@"AuthorId");

        #endregion
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
