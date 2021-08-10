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
            _prices.AddProduct("Soup", 1.99);
            _prices.AddMarkdown("Soup", 0.50);
            _prices.Price("Soup", 1).Should().Be(1.49);
        }
        
        /*
         * Along with the markdowns, a set of specials are advertised each week.
         * For example, the soup could be advertised  "three cans for $5.00."
         * Sometimes limits are placed on these specials. For example, "Buy two, get one free. Limit 6." 
         */

        [Fact]
        public void PurchaseTwoForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 1.99);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.Price("Soup", 2).Should().Be(1.99);
        }
        
        [Fact]
        public void PurchaseOneForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 1.99);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.Price("Soup", 1).Should().Be(1.99);
        }
        
        [Fact]
        public void PurchaseThreeForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 1.99);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.Price("Soup", 3).Should().BeApproximately(3.98, 0.001);
        }
        
        [Fact]
        public void PurchaseThreeForBuyOneGetOneHalfOff()
        {
            _prices.AddProduct("Soup", 1.99);
            var bogo = new Bogo("Soup", 0.50);
            _prices.AddSpecial(bogo);
            _prices.Price("Soup", 3).Should().BeApproximately(4.98, 0.001);
        }

        [Fact]
        public void PurchaseThreeForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 1.99);
            var bulkPrice = new BulkPrice("Soup", 3, 5.00);
            _prices.AddSpecial(bulkPrice);
            _prices.Price("Soup", 3).Should().BeApproximately(5.00, 0.001);
        }

        [Fact]
        public void PurchaseTwoForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 1.99);
            var bulkPrice = new BulkPrice("Soup", 3, 5.00);
            _prices.AddSpecial(bulkPrice);
            _prices.Price("Soup", 2).Should().BeApproximately(3.98, 0.001);
        }
        
        [Fact]
        public void PurchaseSevenForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 1.99);
            var bulkPrice = new BulkPrice("Soup", 3, 5.00);
            _prices.AddSpecial(bulkPrice);
            _prices.Price("Soup", 7).Should().BeApproximately(11.99, 0.001);
        }
    }
}