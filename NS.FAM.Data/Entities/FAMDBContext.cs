using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NS.FAM.Data.Entities;

namespace NS.FAM.Data.Entities
{
    public partial class FAMDBContext : DbContext
    {
        public FAMDBContext()
        {
        }

        public FAMDBContext(DbContextOptions<FAMDBContext> options)
            : base(options)
        {
        }

        
        public virtual DbSet<City> City { get; set; } = null!;
        public virtual DbSet<Country> Country { get; set; } = null!;
        public virtual DbSet<LogError> LogErrors { get; set; } = null!;
        public virtual DbSet<State> State { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<Products> Products{ get; set; } = null!;   
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Category> Category{ get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategory{ get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.2.29;Database=Testing_JanakRaj;User ID=JanakRaj;Password=AqdAFS3dZBcXbYqq;TrustServerCertificate=True");
            }
        }

       
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
