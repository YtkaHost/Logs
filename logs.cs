using System;
using System.Collections.Generic;
using Serilog;

class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("operation.log")
                    .CreateLogger();
        Log.Information("Open");

        Log.Information("User authentication attempt");
        bool isAuthenticated = AuthenticateUser("user", "password123");
        if (!isAuthenticated)
        {
            Log.Error("Authentication failed");
            return;
        }
        Log.Information("User successfully authenticated");

        List<string> foundItems = SearchProducts("A");
        if (foundItems.Count > 0)
        {
            Log.Information("Products found: {Products}", string.Join(", ", foundItems));
        }
        else
        {
            Log.Warning("No products found for the given query");
        }

        List<string> cart = new List<string>();
        foreach (var item in foundItems)
        {
            AddToCart(cart, item);
        }
    }

    static bool AuthenticateUser(string username, string password)
    {
        return username == "user" && password == "password123";
    }

    static List<string> SearchProducts(string query)
    {
        Log.Information("Searching for products with query: {Query}", query);

        List<string> productDatabase = new List<string>
        {
            "AAA", "AA", "A",
            "A b", "d A", "a a a"
        };

        List<string> foundItems = productDatabase.FindAll(product => product.Contains(query, StringComparison.OrdinalIgnoreCase));
        return foundItems;
    }

    static void AddToCart(List<string> cart, string item)
    {
        cart.Add(item);
        Log.Information("Item added to cart: {Item}", item);
    }
}
