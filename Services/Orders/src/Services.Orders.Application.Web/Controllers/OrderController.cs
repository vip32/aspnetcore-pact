namespace Services.Orders.Application.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Orders.Domain;
    using Services.Orders.Infrastructure;

    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IOrderRepository orderRepository;
        private readonly ProductsServiceClient productsServiceClient;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderRepository orderRepository,
            ProductsServiceClient productsServiceClient)
        {
            this.logger = logger;
            this.orderRepository = orderRepository;
            this.productsServiceClient = productsServiceClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await Task.Run(()
                => this.Ok(this.orderRepository.Get())).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<ActionResult<Order>> Get(Guid orderId)
        {
            var order = await Task.Run(() => this.orderRepository.Get(orderId)).ConfigureAwait(false);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order == null)
            {
                return this.BadRequest();
            }

            this.orderRepository.Upsert(order);

            return await Task.Run(()
                => this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{order.Id}"), order)).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("{orderId}/products/{productId}")]
        public async Task<ActionResult<Order>> Post(Guid orderId, Guid productId, int quantity)
        {
            var order = this.orderRepository.Get(orderId);
            if (order == null)
            {
                return this.NotFound();
            }

            // retrieve the product from the product service (=api call)
            var product = await this.productsServiceClient.GetProduct(productId).ConfigureAwait(false);
            if (product == null)
            {
                return this.NotFound();
            }

            if (!order.CanAddProducts)
            {
                return this.BadRequest();
            }

            order.AddItem($"{product.Name} ({product.Brand})", product.Ean, product.Price, quantity);

            return this.Ok(order);
        }
    }
}
