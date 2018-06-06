using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    public class CsvOutputFormatterTests : IDisposable
    {
        private TestServer _server;
        private HttpClient _client;

        public CsvOutputFormatterTests()
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
        public async Task Quotes_Are_Escaped()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/books/data.csv");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
            var result = await _client.SendAsync(request);

            var books = new Deserializer().Deserialize<Book[]>(new StreamReader(await result.Content.ReadAsStreamAsync()));
        }
    }
}
