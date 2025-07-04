using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_ECommerce_Task
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool RequiresShipping { get; set; }

        public Product(string name, double price, int quantity, double weight,
                      DateTime? expiryDate, bool requiresShipping)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Weight = weight;
            ExpiryDate = expiryDate;
            RequiresShipping = requiresShipping;
        }

        public bool IsExpired()
        {
            return ExpiryDate.HasValue && DateTime.Now > ExpiryDate.Value;
        }

        public bool IsOutOfStock()
        {
            return Quantity <= 0;
        }
    }
}
