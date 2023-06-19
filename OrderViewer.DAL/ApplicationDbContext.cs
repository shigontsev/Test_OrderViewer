﻿using Microsoft.EntityFrameworkCore;
using OrderViewer.Common.Entities;

namespace OrderViewer.DAL
{
    public class ApplicationDBContext : DbContext
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test_OrderViewer_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<UserData> UserData { get; set; } = null!;

        public DbSet<Order> Order { get; set; } = null!;

        public DbSet<Product> Product { get; set; } = null!;

        public DbSet<OrderProduct> OrderProduct { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}