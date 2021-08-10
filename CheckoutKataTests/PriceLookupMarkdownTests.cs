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
            _prices.AddProduct("Soup", 199);
            _prices.AddMarkdown("Soup", 50);
            _prices.PriceInCents("Soup", 1).Should().Be(149);
        }
        
        /*
         * Along with the markdowns, a set of specials are advertised each week.
         * Sometimes limits are placed on these specials. For example, "Buy two, get one free. Limit 6." 
         */

        [Fact]
        public void PurchaseTwoForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 199);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.PriceInCents("Soup", 2).Should().Be(199);
        }
        
        [Fact]
        public void PurchaseOneForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 199);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.PriceInCents("Soup", 1).Should().Be(199);
        }
        
        [Fact]
        public void PurchaseThreeForBuyOneGetOneFree()
        {
            _prices.AddProduct("Soup", 199);
            var bogo = new Bogo("Soup");
            _prices.AddSpecial(bogo);
            _prices.PriceInCents("Soup", 3).Should().Be(398);
        }

        [Fact]
        public void PurchaseFourForBuyThreeGetOneFree()
        {
            _prices.AddProduct("Soup", 199);
            var bogo = new Bogo("Soup", requiredQuantity: 3);
            _prices.AddSpecial(bogo);
            _prices.PriceInCents("Soup", 4).Should().Be(597);
        }
        
        [Fact]
        public void PurchaseThreeForBuyOneGetOneHalfOff()
        {
            _prices.AddProduct("Soup", 199);
            var bogo = new Bogo("Soup", discount: 0.50);
            _prices.AddSpecial(bogo);
            _prices.PriceInCents("Soup", 3).Should().Be(497);
        }

        [Fact]
        public void PurchaseThreeForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 199);
            var bulkPrice = new BulkPrice("Soup", 3, 500);
            _prices.AddSpecial(bulkPrice);
            _prices.PriceInCents("Soup", 3).Should().Be(500);
        }

        [Fact]
        public void PurchaseTwoForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 199);
            var bulkPrice = new BulkPrice("Soup", 3, 500);
            _prices.AddSpecial(bulkPrice);
            _prices.PriceInCents("Soup", 2).Should().Be(398);
        }
        
        [Fact]
        public void PurchaseSevenForSpecialThreeForFive()
        {
            _prices.AddProduct("Soup", 199);
            var bulkPrice = new BulkPrice("Soup", 3, 500);
            _prices.AddSpecial(bulkPrice);
            _prices.PriceInCents("Soup", 7).Should().Be(1199);
        }
    }
}