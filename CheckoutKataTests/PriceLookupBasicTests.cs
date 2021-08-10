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
            _prices.AddPerUnitProduct("Soup", 1.89);
            _prices.PricePerUnit("Soup", 2).Should().Be(3.78);
        }
        
        [Fact]
        public void AddPie()
        {
            _prices.AddPerUnitProduct("Pie", 6.99);
            _prices.PricePerUnit("Pie", 1).Should().Be(6.99);
        }

        [Fact]
        public void AddBeef()
        {
            _prices.AddPerPoundProduct("Ground Beef", 5.99);
            _prices.PricePerPound("Ground Beef", 2).Should().Be(11.98);
        }

        [Fact]
        public void AddBananas()
        {
            _prices.AddPerPoundProduct("Bananas", 2.38);
            _prices.PricePerPound("Bananas", 3).Should().Be(7.14);
        }

        [Fact]
        public void UnknownPerUnitProduct()
        {
            void Act() => _prices.PricePerUnit("unknown per unit", 1);
            var exception = Assert.Throws<ArgumentException>((Action) Act);
            exception.Message.Should().Be("[unknown per unit] is not a valid product");
        }
        
        [Fact]
        public void UnknownPerPoundProduct()
        {
            void Act() => _prices.PricePerPound("unknown per pound", 3);
            var exception = Assert.Throws<ArgumentException>((Action) Act);
            exception.Message.Should().Be("[unknown per pound] is not a valid product");
        }

        [Fact]
        public void PerUnitPriceUpdate()
        {
            _prices.AddPerUnitProduct("Soup", 1.89);
            _prices.AddPerUnitProduct("Soup", 1.99);
            _prices.PricePerUnit("Soup", 1).Should().Be(1.99);
        }

        [Fact]
        public void PerPoundPriceUpdate()
        {
            _prices.AddPerPoundProduct("Ground Beef", 5.99);
            _prices.AddPerPoundProduct("Ground Beef", 6.49);
            _prices.PricePerPound("Ground Beef", 2).Should().Be(12.98);
        }
        
        [Fact]
        public void WrongApiForPerUnit()
        {
            _prices.AddPerUnitProduct("Soup", 1.89);
            void Act() => _prices.PricePerPound("Soup", 2);
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Soup is priced per unit, not per pound");
        }
        
        [Fact]
        public void WrongApiForPerPound()
        {
            _prices.AddPerPoundProduct("Ground Beef", 5.99);
            void Act() => _prices.PricePerUnit("Ground Beef", 1);
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Ground Beef is priced per pound, not per unit");
        }
        
        [Fact]
        public void PerUnitProductIsAlreadyPerPound()
        {
            _prices.AddPerPoundProduct("Ground Beef", 5.99);
            void Act() => _prices.AddPerUnitProduct("Ground Beef", 6.49);
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Ground Beef is already priced per pound.  Cannot price per unit.");
        }
        
        [Fact]
        public void PerPoundProductIsAlreadyPerUnit()
        {
            _prices.AddPerUnitProduct("Soup", 1.89);
            void Act() => _prices.AddPerPoundProduct("Soup", 1.99);
            var exception = Assert.Throws<ArgumentException>(Act);
            exception.Message.Should().Be("Soup is already priced per unit.  Cannot price per pound.");
        }
        
    }
}