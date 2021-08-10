using System;
using CheckoutKata;

namespace CheckoutKataTests
{
    public interface ISpecial
    {
        public string ProductName { get; }
        int Price(int quantityRequested, int basePrice);
    }

    public class Bogo : ISpecial
    {
        private readonly int _requiredQuantity;
        public double Discount { get; }
        public string ProductName { get; }

        public Bogo(string productName, double discount = 1, int requiredQuantity=1)
        {
            _requiredQuantity = requiredQuantity;
            ProductName = productName;
            Discount = discount;
        }
        
        public int Price(int quantityRequested, int basePriceInCents)
        {
            var baseTotal = basePriceInCents * quantityRequested;
            var discount = (int)Math.Round(Math.Floor((double)quantityRequested / (_requiredQuantity + 1)) * basePriceInCents * Discount);
            return baseTotal - discount;
        }
    }
    
    public class BulkPrice : ISpecial
    {
        private readonly int _quantity;
        private readonly int _priceInCents;

        public BulkPrice(string productName, int quantity, int priceInCents)
        {
            _quantity = quantity;
            _priceInCents = priceInCents;
            ProductName = productName;
        }

        public string ProductName { get; }
        public int Price(int quantityRequested, int basePrice)
        {
            var bulkUnitsRequested = Math.Floor((double)quantityRequested / _quantity);
            var nonBulkUnits = quantityRequested - bulkUnitsRequested * _quantity;
            return (int)(_priceInCents * bulkUnitsRequested + nonBulkUnits * basePrice);
        }
    }
}