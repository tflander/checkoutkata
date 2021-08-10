using System;
using CheckoutKata;

namespace CheckoutKataTests
{
    public interface ISpecial
    {
        public string ProductName { get; }
        double Price(int quantityRequested, double basePrice);
    }

    public class Bogo : ISpecial
    {
        public double Discount { get; }
        public string ProductName { get; }

        public Bogo(string productName, double discount = 1)
        {
            ProductName = productName;
            Discount = discount;
        }
        
        public double Price(int quantityRequested, double basePrice)
        {
            var baseTotal = basePrice * quantityRequested;
            var discount = Math.Floor((double)quantityRequested / 2) * basePrice * Discount;
            return baseTotal - discount;
        }
    }
    
    public class BulkPrice : ISpecial
    {
        private readonly int _quantity;
        private readonly double _price;

        public BulkPrice(string productName, int quantity, double price)
        {
            _quantity = quantity;
            _price = price;
            ProductName = productName;
        }

        public string ProductName { get; }
        public double Price(int quantityRequested, double basePrice)
        {
            var bulkUnitsRequested = Math.Floor((double)quantityRequested / _quantity);
            var nonBulkUnits = quantityRequested - bulkUnitsRequested * _quantity;
            return _price * bulkUnitsRequested + nonBulkUnits * basePrice;
        }
    }
}