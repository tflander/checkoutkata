using System;
using CheckoutKata;
using Xunit;
using FluentAssertions;

namespace CheckoutKataTests
{
    public class PriceLookupBasicTests
    {
        private readonly PriceLookup _prices = new();
        
        [Fact]
        public void AddSoup()
        {
            _prices.AddProduct("Soup", 189);
            _prices.PriceInCents("Soup", 2).Should().Be(378);
        }
        
        [Fact]
        public void AddPie()
        {
            _prices.AddProduct("Pie", 699);
            _prices.PriceInCents("Pie", 1).Should().Be(699);
        }

        [Fact]
        public void AddBeef()
        {
            _prices.AddProduct("Ground Beef", 599);
            _prices.PriceInCents("Ground Beef", 2).Should().Be(1198);
        }
        
        [Fact]
        public void UnknownProduct()
        {
            void Act() => _prices.PriceInCents("unknown per unit", 1);
            var exception = Assert.Throws<ArgumentException>((Action) Act);
            exception.Message.Should().Be("[unknown per unit] is not a valid product");
        }
        
        [Fact]
        public void PriceUpdate()
        {
            _prices.AddProduct("Soup", 189);
            _prices.AddProduct("Soup", 199);
            _prices.PriceInCents("Soup", 1).Should().Be(199);
        }
        
    }
}