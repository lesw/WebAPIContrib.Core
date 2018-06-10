using System;
using Xunit;

namespace WebApiContrib.Core.Formatter.Csv.Tests
{
    public class CsvOutputFormatterTests
    {
        private CsvOutputFormatter _sut;
        private static string _delimiter = ",";

        public CsvOutputFormatterTests()
        {
            _sut = new CsvOutputFormatter(new CsvFormatterOptions()
                    {
                        UseSingleLineHeaderInCsv = true,
                        CsvDelimiter = _delimiter,
                        Encoding = "utf-8"
                    });
        }

        [Fact]
        public void GenerateFieldString_ReturnsEmpty_WhenNull()
        {
            var result = _sut.GenerateFieldString(null);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GenerateFieldString_EscapesDelimiter()
        {
            var result = _sut.GenerateFieldString($"Super{_delimiter} luxurious truck");
            Assert.Equal($"\"Super{_delimiter} luxurious truck\"", result);
        }

        [Fact]
        public void GenerateFieldString_EscapesQuotes()
        {
            var result = _sut.GenerateFieldString("Super \"luxurious\" truck");
            Assert.Equal("\"Super \"\"luxurious\"\" truck\"", result);
        }

        [Fact]
        public void GenerateFieldString_ReplacesReturnCarretWithSpace()
        {
            var result = _sut.GenerateFieldString("Super\rtruck");
            Assert.Equal("Super truck", result);
        }
        
        [Fact]
        public void GenerateFieldString_ReplacesNewLineWithSpace()
        {
            var result = _sut.GenerateFieldString($"Super{Environment.NewLine}truck");
            Assert.Equal("Super truck", result);
        }
    }
}
