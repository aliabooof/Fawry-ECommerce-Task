using Fawry_ECommerce_Task.Models;
using Fawry_ECommerce_Task.Services;

namespace Fawry_ECommerce_Task
{
    internal class Program
    {

        public class ECommerceSystem
        {
            private Dictionary<string, Product> products;
            private ShoppingCart cart;

            public ECommerceSystem()
            {
                products = new Dictionary<string, Product>();
                cart = new ShoppingCart();
                InitializeProducts();
            }

            private void InitializeProducts()
            {

                products["Cheese"] = new Product("Cheese", 200, 10, 400,
                    DateTime.Now.AddDays(30), true);
                products["Biscuits"] = new Product("Biscuits", 150, 5, 700,
                    new DateTime(2025,3,15), true);
                products["TV"] = new Product("TV", 5000, 2, 15000,
                    null, true);
                products["Mobile"] = new Product("Mobile", 3000, 3, 200,
                    null, false);
                products["ScratchCard"] = new Product("ScratchCard", 50, 20, 5,
                    null, false);
            }

            public void AddToCart(string productName, int quantity)
            {
                if (!products.ContainsKey(productName))
                {
                    throw new ArgumentException("Product not found");
                }

                Product product = products[productName];
                cart.Add(product, quantity);
            }

            public void Checkout(Customer customer)
            {

                if (cart.IsEmpty())
                {
                    Console.WriteLine("Error: Cart is empty");
                    return;
                }

                double subtotal = cart.GetSubtotal();
                double shippingFee = cart.GetShippingFee();
                double totalAmount = subtotal + shippingFee;


                if (customer.Balance < totalAmount)
                {
                    Console.WriteLine("Error: Customer's balance is insufficient.");
                    return;
                }


                foreach (var item in cart.GetItems())
                {
                    if (item.Product.IsExpired())
                    {
                        Console.WriteLine("Error: one product is out of stock or expired.");
                        return;
                    }
                    if (item.Product.IsOutOfStock())
                    {
                        Console.WriteLine("Error: one product is out of stock or expired.");
                        return;
                    }
                }


                customer.Balance -= totalAmount;


                foreach (var item in cart.GetItems())
                {
                    item.Product.Quantity -= item.Quantity;
                }


                var shippableItems = cart.GetShippableItems();
                if (shippableItems.Count > 0)
                {
                    Console.WriteLine("** Shipment notice **");
                    foreach (var item in shippableItems)
                    {
                        Console.WriteLine($"{GetQuantityFromCart(item.GetName())}x {item.GetName()}\t\t{(int)item.GetWeight()}g");
                    }
                    double totalWeight = shippableItems.Sum(item => item.GetWeight());
                    Console.WriteLine($"Total package weight {(totalWeight / 1000):F1}kg");
                    Console.WriteLine();
                }


                Console.WriteLine("** Checkout receipt **");
                foreach (var item in cart.GetItems())
                {
                    Console.WriteLine($"{item.Quantity}x {item.Product.Name}\t\t{(int)item.TotalPrice}");
                }
                Console.WriteLine("--------------------");
                Console.WriteLine($"Subtotal\t\t{(int)subtotal}");
                Console.WriteLine($"Shipping\t\t{(int)shippingFee}");
                Console.WriteLine($"Amount\t\t\t{(int)totalAmount}");
                Console.WriteLine();
                Console.WriteLine($"Customer balance after payment: {(int)customer.Balance}");
            }

            private int GetQuantityFromCart(string productName)
            {
                var item = cart.GetItems().FirstOrDefault(item => item.Product.Name == productName);
                return item?.Quantity ?? 0;
            }

            static void Main(string[] args)
            {
                var system = new ECommerceSystem();
                var customer = new Customer("Ali Abooof", 100000);

                try
                {
                    
                    system.AddToCart("Cheese", 3);
                    system.AddToCart("TV", 10);
                    system.AddToCart("ScratchCard", 20);
                    //system.AddToCart("Biscuits", 5);

                   
                    system.Checkout(customer);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
    }
}
