using Admin_microservice_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Admin_microservice_v2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<ClothesSize> Sizes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Item> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subcategory>()
                .HasKey(sc => sc.SubcategoryId);

            modelBuilder.Entity<Item>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Gender>()
                .HasKey(g => g.GenderId);

            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);

            modelBuilder.Entity<ClothesSize>()
                .HasKey(s => s.SizeId);

            modelBuilder.Entity<Subcategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(sc => sc.FK_CategoryId);

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Subcategory)
                .WithMany(sc => sc.Items)
                .HasForeignKey(p => p.FK_SubcategoryId);

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Gender)
                .WithMany()
                .HasForeignKey(p => p.FK_GenderId); // Указываем внешний ключ явно

            modelBuilder.Entity<Image>()
            .HasOne(i => i.Item)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId);


            modelBuilder.Entity<ItemSize>()
                .HasKey(isz => new { isz.ProductId, isz.SizeId });

            modelBuilder.Entity<ItemSize>()
                .HasOne<Item>(isz => isz.Product)
                .WithMany(p => (IEnumerable<ItemSize>)p.ItemSizes)
                .HasForeignKey(isz => isz.ProductId);


            modelBuilder.Entity<ItemSize>()
                .HasOne<ClothesSize>(isz => isz.Size)
                .WithMany(s => (IEnumerable<ItemSize>)s.Items)
                .HasForeignKey(isz => isz.SizeId);

        }

    }
}
