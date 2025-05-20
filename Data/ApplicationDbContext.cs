using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NaturalCosmeticsECommerce.Models;


namespace NaturalCosmeticsECommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Sıvı Takviyeler",
                    Description = "Doğal içerikli sıvı takviye ürünleri",
                    ImageUrl = "/img/sivi_takviye.png"
                },
        new Category
        {
            CategoryId = 2,
            Name = "Sabunlar",
            Description = "Bitkisel sabun çeşitleri",
            ImageUrl = "/img/sabun.png"
        },
        new Category
        {
            CategoryId = 3,
            Name = "Damlalar",
            Description = "Bitkisel damla ürünleri",
            ImageUrl = "/img/damla.png"

        },
        new Category
        {
            CategoryId = 4,
            Name = "Diğer Ürünler",
            Description = "Diğer doğal kozmetik ürünleri",
            ImageUrl = ""
        },
        new Category
        {
            CategoryId = 5,
            Name = "Cilt Bakımı",
            Description = "Doğal cilt bakım ürünleri",
            ImageUrl = "/img/cilt_bakim.png"
        },
        new Category
        {
            CategoryId = 6,
            Name = "Kremler",
            Description = "Bitkisel krem çeşitleri",
            ImageUrl = "/img/krem.png"
        }
            );

            modelBuilder.Entity<Product>().HasData(

                new Product { ProductId = 1, Name = "Gül Suyu 250ml", Description = "", Price = 250m, ImageUrl = "", CategoryId = 1, StockQuantity = 100 },
                new Product { ProductId = 2, Name = "Çam Çırası Suyu 500g", Description = "", Price = 330m, ImageUrl = "", CategoryId = 1, StockQuantity = 100 },
                new Product { ProductId = 3, Name = "Çam Çırası Suyu 1L", Description = "", Price = 500m, ImageUrl = "", CategoryId = 1, StockQuantity = 100 },
                new Product { ProductId = 4, Name = "Kan Şurubu 1L", Description = "", Price = 500m, ImageUrl = "", CategoryId = 1, StockQuantity = 100 },

                new Product { ProductId = 5, Name = "Keçi Sütlü Sabun", Description = "", Price = 160m, ImageUrl = "", CategoryId = 2, StockQuantity = 100 },
                new Product { ProductId = 6, Name = "Sidr Sabun", Description = "", Price = 160m, ImageUrl = "", CategoryId = 2, StockQuantity = 100 },
                new Product { ProductId = 7, Name = "Bıttım Sabun", Description = "", Price = 160m, ImageUrl = "", CategoryId = 2, StockQuantity = 100 },

                new Product { ProductId = 8, Name = "Kantaron Yağı 250ml", Description = "", Price = 250m, ImageUrl = "", CategoryId = 4, StockQuantity = 100 },
                new Product { ProductId = 9, Name = "Kantaron Yağı 100ml", Description = "", Price = 200m, ImageUrl = "", CategoryId = 4, StockQuantity = 100 },
                new Product { ProductId = 10, Name = "İğde Tozu 200g", Description = "", Price = 250m, ImageUrl = "", CategoryId = 4, StockQuantity = 100 },
                new Product { ProductId = 11, Name = "Ceviz Yağı 100ml", Description = "", Price = 220m, ImageUrl = "", CategoryId = 4, StockQuantity = 100 },
                new Product { ProductId = 12, Name = "Ceviz Yağı 250ml", Description = "", Price = 425m, ImageUrl = "", CategoryId = 4, StockQuantity = 100 },

                new Product { ProductId = 13, Name = "D Vitamini Kompleks (3-6-9)", Description = "", Price = 400m, ImageUrl = "", CategoryId = 3, StockQuantity = 100 },
                new Product { ProductId = 14, Name = "Kulak Burun Damla", Description = "", Price = 400m, ImageUrl = "", CategoryId = 3, StockQuantity = 100 },
                new Product { ProductId = 15, Name = "SinPlus", Description = "", Price = 450m, ImageUrl = "", CategoryId = 3, StockQuantity = 100 },

                new Product { ProductId = 16, Name = "Anti Aging Leke Giderici Serum", Description = "", Price = 600m, ImageUrl = "", CategoryId = 5, StockQuantity = 100 },
                new Product { ProductId = 17, Name = "Sivilce Akne Kremi 50ml", Description = "", Price = 350m, ImageUrl = "", CategoryId = 5, StockQuantity = 100 },
                new Product { ProductId = 18, Name = "Tüy Azaltıcı Losyon", Description = "", Price = 550m, ImageUrl = "", CategoryId = 5, StockQuantity = 100 },

                new Product { ProductId = 19, Name = "Kırmızı Krem 40cc", Description = "Kuşburnu ve menekşe içerikli", Price = 300m, ImageUrl = "", CategoryId = 6, StockQuantity = 100 },
                new Product { ProductId = 20, Name = "Kırmızı Krem 190cc", Description = "", Price = 500m, ImageUrl = "", CategoryId = 6, StockQuantity = 100 },
                new Product { ProductId = 21, Name = "Katran Krem 40cc", Description = "Ardıç ve susam yağı içerikli", Price = 300m, ImageUrl = "", CategoryId = 6, StockQuantity = 100 },
                new Product { ProductId = 22, Name = "Katran Krem 190cc", Description = "", Price = 500m, ImageUrl = "", CategoryId = 6, StockQuantity = 100 },
                new Product { ProductId = 23, Name = "Akıllı Krem 50ml", Description = "Ağrı kesici kas gevşetici masaj merhemi", Price = 300m, ImageUrl = "", CategoryId = 6, StockQuantity = 100 }
            );
        }
    }
}
