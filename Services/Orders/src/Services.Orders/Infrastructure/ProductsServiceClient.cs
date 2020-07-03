namespace Services.Orders.Infrastructure
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Services.Orders.Application;

    public class ProductsServiceClient
    {
        public ProductsServiceClient(HttpClient client)
        {
            this.Client = client;
            this.Client.BaseAddress = new Uri("https://services.products.application.web/");
        }

        public HttpClient Client { get; }

        public async Task<ProductDto> GetProduct(Guid id)
        {
            // https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json
            return await this.Client
                .GetFromJsonAsync<ProductDto>($"/products/{id}").ConfigureAwait(false);
        }
    }
}
