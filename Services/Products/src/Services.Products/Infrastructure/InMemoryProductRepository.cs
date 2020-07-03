namespace Services.Products.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Services.Products.Domain;

    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ISet<Product> entities = new HashSet<Product>(
            new[]
            {
                new Product { Id = Guid.Parse("a150e15e-50fa-ea5e-f001-ba5eba11abba"), Brand = "BrandX", Name = "Product01", Ean = "7501031311301", Price = 99.99m, InStock = true },
                new Product { Id = Guid.Parse("b0b510c0-5ac5-ab1e-b055-deface551ade"), Brand = "BrandX", Name = "Product02", Ean = "7501031311302", Price = 99.99m, InStock = true },
                new Product { Id = Guid.Parse("c01055a1-105e-0dd5-5ea5-5c01d5ce55e5"), Brand = "BrandX", Name = "Product03", Ean = "7501031311303", Price = 99.99m, InStock = true },
                new Product { Id = Guid.Parse("dec1a55e-fad5-bee5-b0de-1eaf1e55faff"), Brand = "BrandX", Name = "Product04", Ean = "7501031311304", Price = 99.99m, InStock = true },
            });

        public Product Get(Guid id)
        {
            return this.entities.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Product> Get()
        {
            return this.entities;
        }

        public void Upsert(Product entity)
        {
            EnsureArg.IsNotNull(entity);

            var existing = this.Get(id: entity.Id);

            if (existing != null)
            {
                this.entities.Remove(existing);
            }

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }

            this.entities.Add(entity);
        }
    }
}
