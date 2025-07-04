using Fawry_ECommerce_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_ECommerce_Task.Services
{
    public class ShoppingCart
    {
        private List<CartItem> items;

        public ShoppingCart()
        {
            items = new List<CartItem>();
        }

        public void Add(Product product, int quantity)
        {
            // Check if product is available
            if (product.IsOutOfStock())
            {
                throw new ArgumentException("Product is out of stock");
            }

            if (product.IsExpired())
            {
                throw new ArgumentException($"{product.Name} has expired");
            }

            if (quantity > product.Quantity)
            {
                throw new ArgumentException($"Requested quantity of {product.Name} exceeds available stock");
            }

            // Check if product already exists in cart
            var existingItem = items.FirstOrDefault(item => item.Product.Name == product.Name);
            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + quantity;
                if (newQuantity > product.Quantity)
                {
                    throw new ArgumentException("Total quantity exceeds available stock");
                }
                items.Remove(existingItem);
                items.Add(new CartItem(product, newQuantity));
                return;
            }

            items.Add(new CartItem(product, quantity));
        }

        public List<CartItem> GetItems() => new List<CartItem>(items);

        public bool IsEmpty() => items.Count == 0;

        public double GetSubtotal()
        {
            return items.Sum(item => item.TotalPrice);
        }

        public double GetShippingFee()
        {
            
            double totalWeight = items
                .Where(item => item.Product.RequiresShipping)
                .Sum(item => item.TotalWeight);

            
            return totalWeight > 1000 ? 30.0 : 0.0;
        }

        public List<ShippableItem> GetShippableItems()
        {
            var shippableItems = new List<ShippableItem>();
            foreach (var item in items)
            {
                if (item.Product.RequiresShipping)
                {
                    shippableItems.Add(new ShippableItem(
                        item.Product.Name,
                        item.TotalWeight
                    ));
                }
            }
            return shippableItems;
        }
    }
}
