using System.Dynamic;

class Product
{
    public string name { get; set; }
    public decimal Price { get; set; }
    public int Quantities { get; set; }
    public Product(string productName, decimal productPrice, int productQuantity)
    {
        name = (productName);
        Price = (productPrice);
        Quantities = (productQuantity);
    }
    public decimal GetSubtotal()
    {
        decimal itemTotal = Price * Quantities;
        if (Quantities > 10)
        {
            itemTotal = itemTotal * 0.9m;
            Console.WriteLine($"-> Discount applied on '{name}'! Subtotal: ${itemTotal:F2}");
        }
        else
        {
            Console.WriteLine($"-> '{name}' Subtotal (no discount): ${itemTotal:F2}");
        }
        return itemTotal;
    }
}
class Store
{
    static List<Product> Products = new List<Product>();
    static void Main(string[] args)
    {
        EnterProducts();
    }
    public static void EnterProducts()
    {
        while (true)
        {
            Console.WriteLine("Enter product name (or 'Search' to search in existing Products) (or 'exit' to finish ):");
            string? productName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(productName))
            {
                Console.WriteLine("Product name cannot be empty. Please enter a valid product name.");
                continue;
            }
            if (productName?.ToLower() == "exit")
            {
                DisplayProducts();
                DisplayMostExpensiveProduct();
                CalculateTotal();
                break;
            }
            if (productName?.ToLower() == "search")
            {
                searchProduct();
                continue;
            }
            Console.WriteLine("Enter product price:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal productPrice) || productPrice <= 0)
            {
                Console.WriteLine("Invalid price format. Please enter a valid price.");
                continue;
            }

            Console.WriteLine("Enter product quantity:");
            if (!int.TryParse(Console.ReadLine(), out int productQuantity) || productQuantity <= 0)
            {
                Console.WriteLine("Invalid quantity format. Please enter a valid quantity.");
                continue;
            }
            AddProduct(productName!, productPrice, productQuantity);
        }
    }
    public static void AddProduct(string productName, decimal productPrice, int productQuantity)
    {
        var product = new Product(productName, productPrice, productQuantity);
        Products.Add(product);
        Console.WriteLine($"Product '{productName}' added successfully.");
    }
    public static void CalculateTotal()
    {
        decimal grandTotal = 0;
        for (int i = 0; i < Products.Count; i++)
        {
            grandTotal += Products[i].GetSubtotal();
        }
        Console.WriteLine($"the total is:{grandTotal:f2}");
    }
    public static void DisplayProducts()
    {
        Console.WriteLine("Products in the store:");
        for (int i = 0; i < Products.Count; i++)
        {
            Console.WriteLine($"Product: {Products[i].name}, Price: ${Products[i].Price:F2}, Quantity: {Products[i].Quantities}");
        }
    }
    public static void DisplayMostExpensiveProduct()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        decimal maxPrice = Products.Max(product => product.Price);
        int index = Products.FindIndex(product => product.Price == maxPrice);
        Console.WriteLine($"Most expensive product: {Products[index].name}, Price: ${maxPrice:F2}, Quantity: {Products[index].Quantities}");
    }
    public static void searchProduct()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("no Products found");
            return;
        }
        Console.WriteLine("What Product u want to see enter the name:");
        string? productSearched = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(productSearched))
        {
            Console.WriteLine("enter a valid name");
            return;
        }
        else
        {
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].name.ToLower() == productSearched.ToLower())
                {
                    Console.WriteLine($"1 result fount: {Products[i].name}, Price: ${Products[i].Price:F2}, Quantity: {Products[i].Quantities}");
                    return;
                }
            }
            Console.WriteLine($"Product '{productSearched}' does not exist.");
        }
    }
}