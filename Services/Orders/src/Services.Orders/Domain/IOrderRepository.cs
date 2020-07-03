namespace Services.Orders.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IOrderRepository
    {
        Order Get(Guid id);

        IEnumerable<Order> Get();

        void Upsert(Order entity);
    }
}
