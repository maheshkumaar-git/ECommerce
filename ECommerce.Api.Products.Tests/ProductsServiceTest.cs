using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var productsDbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var productsDbContext = new ProductsDbContext(productsDbContextOptions);
            CreateProducts(productsDbContext);

            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(productsDbContext,null, mapper);

            var product = await productsProvider.GetProductsAsync();
            
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var productsDbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidId))
                .Options;

            var productsDbContext = new ProductsDbContext(productsDbContextOptions);

            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(productsDbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var productsDbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInvalidId))
                .Options;

            var productsDbContext = new ProductsDbContext(productsDbContextOptions);

            var profile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(productsDbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext productsDbContext)
        {

            for (int i = 1; i <= 10; i++)
            {
                productsDbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i*3.14),
                });
            }
            productsDbContext.SaveChanges();
        }

    }
}