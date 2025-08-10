using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Inventory
{
    private List<Product> products = new();
    private int nextId = 1;

    public void AddProduct(string name, int quantity, decimal price)
    {
        products.Add(new Product { Id = nextId++, Name = name, Quantity = quantity, Price = price });
    }

    public bool UpdateProduct(int id, int quantity, decimal price)
    {
        var product = products.Find(p => p.Id == id);
        if (product == null) return false;
        product.Quantity = quantity;
        product.Price = price;
        return true;
    }

    public bool RemoveProduct(int id)
    {
        var product = products.Find(p => p.Id == id);
        if (product == null) return false;
        products.Remove(product);
        return true;
    }

    public List<Product> ListProducts() => new(products);
}

public class Program
{
    public static void Main(string[] args)
    {
        Inventory inventory = new();
        while (true)
        {
            Console.WriteLine("\nInventory App");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Remove Product");
            Console.WriteLine("4. List Products");
            Console.WriteLine("5. Exit");
            Console.Write("Select option: ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Write("Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Quantity: ");
                    int.TryParse(Console.ReadLine(), out int qty);
                    Console.Write("Price: ");
                    decimal.TryParse(Console.ReadLine(), out decimal price);
                    inventory.AddProduct(name, qty, price);
                    Console.WriteLine("Product added.");
                    break;
                case "2":
                    Console.Write("Product ID: ");
                    int.TryParse(Console.ReadLine(), out int upId);
                    Console.Write("New Quantity: ");
                    int.TryParse(Console.ReadLine(), out int upQty);
                    Console.Write("New Price: ");
                    decimal.TryParse(Console.ReadLine(), out decimal upPrice);
                    if (inventory.UpdateProduct(upId, upQty, upPrice))
                        Console.WriteLine("Product updated.");
                    else
                        Console.WriteLine("Product not found.");
                    break;
                case "3":
                    Console.Write("Product ID: ");
                    int.TryParse(Console.ReadLine(), out int rmId);
                    if (inventory.RemoveProduct(rmId))
                        Console.WriteLine("Product removed.");
                    else
                        Console.WriteLine("Product not found.");
                    break;
                case "4":
                    var products = inventory.ListProducts();
                    Console.WriteLine("ID\tName\tQuantity\tPrice");
                    foreach (var p in products)
                        Console.WriteLine($"{p.Id}\t{p.Name}\t{p.Quantity}\t{p.Price:C}");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
