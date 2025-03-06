using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection.Metadata;
using System.Security.Policy;
using WebApi.Domain;

namespace QueueMessage.Consumer.DataBase
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
        //}

        public DbSet<Client> client { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<ItensOrder> item_order { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Cpf).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(10,2)");
            });



            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CodigoOrdem);
                entity.Property(e => e.ClientId);
                entity.Property(e => e.TotalPrice).IsRequired().HasColumnType("decimal(10,2)");

                entity.HasOne(e => e.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.ClientId);
            });



            modelBuilder.Entity<ItensOrder>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();

                entity.HasOne(e => e.Orders)
                        .WithMany(o => o.Itens);


                entity.HasOne(e => e.Product)
                        .WithMany(p => p.Itens)
                        .HasForeignKey(e => e.ProductId);

            });
         }
    }
}
    