

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using Goldentech.Models;

namespace Goldentech.Data
{
    public class ApplicationSqlDbContext : DbContext
    {



        public ApplicationSqlDbContext(DbContextOptions<ApplicationSqlDbContext> options) : base(options) { }
        public DbSet<Items> Items { get; set; }
        public DbSet<BusinessPartners> BusinessPartners { get; set; }
        public DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrdersLines> PurchaseOrdersLines { get; set; }
        public DbSet<SaleOrders> SaleOrders { get; set; }
        public DbSet<SaleOrdersLines> SaleOrdersLines { get; set; }
        public DbSet<SaleOrdersLinesComments> SaleOrdersLinesComments { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.Query<AirDoctor_Tokens>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SaleOrders>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.BPCode)
                    .IsRequired();
                entity.Property(e => e.CreatedBy)
                                   .IsRequired();
                entity.Property(e => e.LastUpdatedBy);


            });
            modelBuilder.Entity<SaleOrdersLines>(entity =>
            {
                entity.HasKey(e => e.LineID);
                
                entity.Property(e => e.DocID)
                    .IsRequired();
                entity.Property(e => e.ItemCode)
                                   .IsRequired();
                entity.Property(e => e.Quantity);

            });
            modelBuilder.Entity<SaleOrdersLinesComments>(entity =>
            {
                entity.HasKey(e => e.CommentLineID);

                entity.Property(e => e.DocID)
                    .IsRequired();
                entity.Property(e => e.LineID)
                                   .IsRequired();
                entity.Property(e => e.Comment).IsRequired();




            });
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.FullName)
                    .IsRequired();

                entity.Property(e => e.Active)
                   .IsRequired();



            });
            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.ItemCode);

                entity.Property(e => e.ItemName)
                    .IsRequired();

                entity.Property(e => e.Active)
                   .IsRequired();

            });
            modelBuilder.Entity<BusinessPartners>(entity =>
            {
                entity.HasKey(e => e.BPCode);

                entity.Property(e => e.BPName)
                    .IsRequired();
                entity.Property(e => e.BPType)
                                   .IsRequired();
                entity.Property(e => e.Active)
                   .IsRequired();



            });
            modelBuilder.Entity<PurchaseOrders>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.BPCode)
                    .IsRequired();
                entity.Property(e => e.CreatedBy)
                                   .IsRequired();
                entity.Property(e => e.LastUpdatedBy);
                 



            });
            modelBuilder.Entity<PurchaseOrdersLines>(entity =>
            {
                entity.HasKey(e => e.LineID);

                entity.Property(e => e.DocID)
                    .IsRequired();
                entity.Property(e => e.ItemCode)
                                   .IsRequired();
                entity.Property(e => e.Quantity);

            });




        }


    }

    
}
