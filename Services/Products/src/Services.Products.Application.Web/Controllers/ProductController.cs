namespace Services.Products.Application.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Services.Products.Domain;

    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductRepository productRepository;

        public ProductController(
            ILogger<ProductController> logger,
            IProductRepository productRepository)
        {
            this.logger = logger;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await Task.Run(()
                => this.Ok(this.productRepository.Get())).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult<Product>> Get(Guid productId)
        {
            var product = await Task.Run(()
                => this.productRepository.Get(productId)).ConfigureAwait(false);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.Ok(product);
        }
    }
}
