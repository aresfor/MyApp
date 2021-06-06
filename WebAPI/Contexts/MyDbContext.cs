using Microsoft.EntityFrameworkCore;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyApp.WebAPI.Configurations;
namespace MyApp.WebAPI.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        { }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        ////不写也没关系
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SongEntityTypeConfiguration().Configure(modelBuilder.Entity<Song>());
            modelBuilder.Entity<Song>().ToTable("Songs");
            modelBuilder.Entity<Account>().ToTable("Accounts");
            modelBuilder.Entity<Collection>().ToTable("Collections");

        }
    }
}
