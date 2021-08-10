using CheckoutKata;
using Xunit;
using FluentAssertions;

namespace CheckoutKataTests
{
    public class PriceLookupMarkdownTests
    {
        private readonly PriceLookup _prices = new();

        [Fact]
        public void PerUnitProductMarkdown()
        {
            _prices.AddPerUnitProduct("Soup", 1.99);
            _prices.AddMarkdown("Soup", 0.50);
            _prices.PricePerUnit("Soup", 1).Should().Be(1.49);
        }

        [Fact]
        public void PerPoundProductMarkdown()
        {
            _prices.AddPerPoundProduct("Ground Beef", 5.99);
            _prices.AddMarkdown("Ground Beef", 1.00);
            _prices.PricePerPound("Ground Beef", 2).Should().Be(9.98);
        }
        
    }
}