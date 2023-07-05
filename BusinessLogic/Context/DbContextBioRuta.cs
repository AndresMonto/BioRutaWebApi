﻿using Microsoft.EntityFrameworkCore;
using ProyectoReciclaje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Context
{
    public class DbContextBioRuta : DbContext
    {   
        public DbContextBioRuta(DbContextOptions<DbContextBioRuta> options): base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Collect> Collect { get; set; }
        public DbSet<Collect_State> Collects_States { get; set; }
        public DbSet<ProductCollect> Product_Collect { get; set; }
        public DbSet<Measure> Measure { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(b => b.Role);

            modelBuilder.Entity<Collect>()
                .HasOne(b => b.Client);
            modelBuilder.Entity<Collect>()
                .HasOne(b => b.State);
            modelBuilder.Entity<Collect>()
                .HasOne(b => b.Collecter);

            //modelBuilder.Entity<ProductCollect>()
            //    .HasOne(b => b.Collect);
            modelBuilder.Entity<ProductCollect>()
                .HasOne(b => b.Product);
            modelBuilder.Entity<ProductCollect>()
                .HasOne(b => b.Measure);
        }
    }
}
