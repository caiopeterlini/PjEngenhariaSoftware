using Microsoft.EntityFrameworkCore;
using QueueMessage.Consumer.Models;
using System.Configuration;
using System.Reflection.Metadata;
using System.Security.Policy;

namespace QueueMessage.Consumer.DataBase
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ItemOrder> ItemOrder { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cpf).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CpfId).IsRequired();
                entity.Property(d => d.TotalPrice);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CpfId).IsRequired();
                entity.Property(d => d.TotalPrice);
            });


        }
    }
}