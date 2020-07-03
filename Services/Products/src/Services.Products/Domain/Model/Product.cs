namespace Services.Products.Domain
{
    using System;

    public class Product
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string Ean { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }
    }
}
