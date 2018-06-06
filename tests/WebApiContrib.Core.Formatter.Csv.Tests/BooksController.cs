using Microsoft.AspNetCore.Mvc;

namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [Route("dataWithQuotes.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataWithQuotesAsCsv()
        {
            return Ok(Book.Data);
        }

        [HttpGet]
        [Route("dataWithComma.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataWithCommaAsCsv()
        {
            return Ok(Book.Data);
        }

        [HttpGet]
        [Route("dataWithLineBreaks.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataWithLineBreaksAsCsv()
        {
            return Ok(Book.Data);
        }
    }
}
