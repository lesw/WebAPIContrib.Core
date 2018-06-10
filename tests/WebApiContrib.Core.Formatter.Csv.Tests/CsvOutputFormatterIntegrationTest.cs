using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    public class CsvOutputFormatterIntegrationTest
    {
        private TestServer _server;
        private HttpClient _client;

        public CsvOutputFormatterIntegrationTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureServices(services => services.AddMvcCore()
                    .AddJsonFormatters()
                    .AddCsvSerializerFormatters(new CsvFormatterOptions()
                    {
                        UseSingleLineHeaderInCsv = true,
                        CsvDelimiter = ",",
                        Encoding = "utf-8"
                    })
                ));

            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client?.Dispose();
            _server?.Dispose();
        }

        [Fact]
        public async Task GetDataAsCsv()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/api/books/data.csv"))
            {
                var result = await _client.SendAsync(request);
                var response = await result.Content.ReadAsStringAsync();

                var expectedResponse = $"Title,Author{Environment.NewLine}" +
                    $"Our Mathematical Universe: My Quest for the Ultimate Nature of Reality,Max Tegmark{Environment.NewLine}" +
                    $"Hockey Town,Ron MacLean{Environment.NewLine}";

                Assert.Equal(expectedResponse, response);
            }
        }
    }
}
