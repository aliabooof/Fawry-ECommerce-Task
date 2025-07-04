using Fawry_ECommerce_Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_ECommerce_Task.Services
{
    public class ShippableItem : IShippingService
    {
        private string name;
        private double weight;

        public ShippableItem(string name, double weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public string GetName() => name;
        public double GetWeight() => weight;
    }
}
