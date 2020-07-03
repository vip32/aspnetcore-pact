namespace Services.Orders.Domain
{
    using System;

    public class OrderItem
    {
        public string Title { get; internal set; }

        public string Ean { get; internal set; }

        public DateTime CreatedAt { get; internal set; } = DateTime.UtcNow;

        public decimal Price { get; internal set; }

        public int Quantity { get; internal set; }

        public void UpdateQuantity(int quantity)
        {
            this.Quantity += quantity;
        }
    }
}
