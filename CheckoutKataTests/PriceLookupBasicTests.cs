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
            _prices.AddProduct("Soup", 1.89);
            _prices.Price("Soup", 2).Should().Be(3.78);
        }
        
        [Fact]
        public void AddPie()
        {
            _prices.AddProduct("Pie", 6.99);
            _prices.Price("Pie", 1).Should().Be(6.99);
        }

        [Fact]
        public void AddBeef()
        {
            _prices.AddProduct("Ground Beef", 5.99);
            _prices.Price("Ground Beef", 2).Should().Be(11.98);
        }
        
        [Fact]
        public void UnknownProduct()
        {
            void Act() => _prices.Price("unknown per unit", 1);
            var exception = Assert.Throws<ArgumentException>((Action) Act);
            exception.Message.Should().Be("[unknown per unit] is not a valid product");
        }
        
        [Fact]
        public void PriceUpdate()
        {
            _prices.AddProduct("Soup", 1.89);
            _prices.AddProduct("Soup", 1.99);
            _prices.Price("Soup", 1).Should().Be(1.99);
        }
        
    }
}