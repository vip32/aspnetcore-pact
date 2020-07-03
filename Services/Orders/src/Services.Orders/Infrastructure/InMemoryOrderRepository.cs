namespace Services.Orders.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Services.Orders.Domain;

    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly ISet<Order> entities = new HashSet<Order>();

        public Order Get(Guid id)
        {
            return this.entities.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Order> Get()
        {
            return this.entities;
        }

        public void Upsert(Order entity)
        {
            EnsureArg.IsNotNull(entity);

            var existing = this.Get(entity.Id);
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
