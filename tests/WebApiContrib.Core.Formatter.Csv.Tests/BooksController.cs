using Microsoft.AspNetCore.Mvc;

namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        [Route("data.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataAsCsv()
        {
            return Ok(Book.DataArray);
        }
    }
}
