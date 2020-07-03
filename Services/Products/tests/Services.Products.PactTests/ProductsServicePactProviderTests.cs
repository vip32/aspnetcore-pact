namespace Services.Products.PactTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Pactify;
    using Services.Products.Application.Web;
    using Xunit;

    public class ProductsServicePactProviderTests
    {
        private readonly HttpClient httpClient;

        public ProductsServicePactProviderTests()
        {
            this.httpClient = new TestServer(
                new WebHostBuilder()
                  .UseEnvironment("Development")
                  .UseStartup<Startup>()) // https://visualstudiomagazine.com/articles/2017/07/01/testserver.aspx
            {
                AllowSynchronousIO = true
            }.CreateClient();
        }

        [Fact]
        public async Task Pact_Should_Be_Verified()
        {
            await PactVerifier
                .Create(this.httpClient)
                .Between("orders", "products")
                .RetrievedFromFile("../../../../../../../pacts") // TODO: docker based pact broker
                .VerifyAsync().ConfigureAwait(false);
        }
    }
}
