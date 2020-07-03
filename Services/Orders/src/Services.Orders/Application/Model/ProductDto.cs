namespace Services.Orders.Application
{
    public class ProductDto
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        public string Ean { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }
    }
}
