namespace Services.Products.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IProductRepository
    {
        Product Get(Guid id);

        IEnumerable<Product> Get();

        void Upsert(Product entity);
    }
}
