using Microsoft.EntityFrameworkCore;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.WebAPI.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
        { }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        //不写也没关系
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Account>().ToTable("Account");
        }
    }
}
