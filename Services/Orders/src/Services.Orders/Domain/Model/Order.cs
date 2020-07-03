namespace Services.Orders.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Order : IEquatable<Order>
    {
        public Guid Id { get; set; }

        private ISet<OrderItem> items = new HashSet<OrderItem>();

        public Guid CustomerId { get; set; }

        public OrderStatus Status { get; private set; } = OrderStatus.New;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? DeliveryDate { get; private set; } = DateTime.UtcNow.AddDays(2);

        public decimal TotalPrice => this.Items.Select(i => i.Price * i.Quantity).Sum();

        public string CancellationReason { get; private set; }

        public bool CanBeDeleted => this.Status == OrderStatus.New;

        public bool CanAddProducts => this.Status == OrderStatus.New;

        public bool HasProducts => this.Items.Any();

        public IEnumerable<OrderItem> Items
        {
            get => this.items;
            private set => this.items = new HashSet<OrderItem>(value);
        }

        public void AddItem(string title, string ean, decimal price, int quantity)
        {
            var item = this.items.FirstOrDefault(i => i.Ean == ean);
            if (item != null)
            {
                item.UpdateQuantity(quantity);
            }
            else
            {
                item = new OrderItem
                {
                    Title = title,
                    Ean = ean,
                    Price = price,
                    Quantity = quantity,
                };

                this.items.Add(item);
            }
        }

        public bool Equals(Order other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || this.Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((Order)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
