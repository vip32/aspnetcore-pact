namespace Services.Orders.PactTests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Pactify;
    using Services.Orders.Application;
    using Xunit;

    public class ProductsServicePactConsumerTests
    {
        private const string ProductId = "a150e15e-50fa-ea5e-f001-ba5eba11abba";

        [Fact]
        public async Task Given_Valid_Product_Id_Product_Should_Be_Returned()
        {
            await PactMaker
                .Create(new PactDefinitionOptions
                {
                    IgnoreCasing = true,
                    IgnoreContractValues = true,
                })
                .Between("orders", "products")
                .WithHttpInteraction(b => b
                    .Given("Existing product")
                    .UponReceiving("A GET request to retrieve product details")
                    .With(request => request
                        .WithMethod(HttpMethod.Get)
                        .WithPath($"/products/{ProductId}"))
                    .WillRespondWith(response => response
                        .WithHeader("Content-Type", "application/json")
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithBody<ProductDto>()))
                .PublishedAsFile("../../../../../../../pacts") // TODO: docker based pact broker
                .MakeAsync().ConfigureAwait(false);
        }
    }
}
