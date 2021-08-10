using System;
using System.Collections.Generic;
using CheckoutKataTests;

namespace CheckoutKata
{
    public class PriceLookup
    {
        private readonly Dictionary<string, double> _prices = new();
        private readonly Dictionary<string, double> _markdowns = new();
        private readonly Dictionary<string, ISpecial> _specials = new();
        public void AddProduct(string productName, double price)
        {
            _prices[productName] = price;
        }

        public double Price(string productName, int unitsOrPounds)
        {
            if (!_prices.ContainsKey(productName))
            {
                throw new ArgumentException($"[{productName}] is not a valid product");
            }

            if (_specials.ContainsKey(productName))
            {
                return _specials[productName].Price(unitsOrPounds, _prices[productName]);
            }

            var markdown = _markdowns.GetValueOrDefault(productName, 0);
            return (_prices[productName] * unitsOrPounds) - markdown;
        }
        
        public void AddMarkdown(string productName, double priceReduction)
        {
            _markdowns[productName] = priceReduction;
        }

        public void AddSpecial(ISpecial special)
        {
            _specials[special.ProductName] = special;
        }
    }
}