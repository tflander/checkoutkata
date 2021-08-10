using System;
using System.Collections.Generic;

namespace CheckoutKata
{
    public class PriceLookup
    {
        private Dictionary<string, double> _perUnitProducts = new Dictionary<string, double>();
        private Dictionary<string, double> _perPoundProducts = new Dictionary<string, double>();
        public void AddPerUnitProduct(string productName, double price)
        {
            if (_perPoundProducts.ContainsKey(productName))
            {
                throw new ArgumentException($"{productName} is already priced per pound.  Cannot price per unit.");
            }
            _perUnitProducts[productName] = price;
        }

        public double PricePerUnit(string productName)
        {
            if (!_perUnitProducts.ContainsKey(productName))
            {
                if (_perPoundProducts.ContainsKey(productName))
                {
                    throw new ArgumentException($"{productName} is priced per pound, not per unit");
                }
                throw new ArgumentException($"[{productName}] is not a valid product");
            }
            return _perUnitProducts[productName];
        }

        public void AddPerPoundProduct(string productName, double price)
        {
            if (_perUnitProducts.ContainsKey(productName))
            {
                throw new ArgumentException($"{productName} is already priced per unit.  Cannot price per pound.");
            }
            _perPoundProducts[productName] = price;
        }

        public double PricePerPound(string productName, int pounds)
        {
            if (!_perPoundProducts.ContainsKey(productName))
            {
                if (_perUnitProducts.ContainsKey(productName))
                {
                    throw new ArgumentException($"{productName} is priced per unit, not per pound");
                }
                throw new ArgumentException($"[{productName}] is not a valid product");
            }
            
            return _perPoundProducts[productName] * pounds;
        }
    }
}