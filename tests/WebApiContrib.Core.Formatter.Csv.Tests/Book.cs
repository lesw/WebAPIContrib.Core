namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    public class Book
    {
        public static Book[] DataArray = new[]
        {
            new Book { Title = "Our Mathematical Universe: My Quest for the Ultimate Nature of Reality", Author = "Max Tegmark" },
            new Book { Title = "Hockey Town", Author = "Ron MacLean" },
        };

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
