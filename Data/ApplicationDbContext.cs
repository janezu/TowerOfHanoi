using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TowerOfHanoi.Models;
using TowerOfHanoi.Data;
using Microsoft.AspNetCore.Identity;


namespace TowerOfHanoi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Variation> Variation { get; set; }
        public DbSet<Optimal> Optimal { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<IdentityUser> IdentityUser { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<Variation>().ToTable("Variation");
            modelBuilder.Entity<Optimal>().ToTable("Optimal");
            modelBuilder.Entity<Configuration>().ToTable("Configuration");
            modelBuilder.Entity<Score>().ToTable("Score");
           



            
        }

    }
}
