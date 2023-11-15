using System.Net.WebSockets;

List<Product> products = new List<Product>()
{
  new Product()
  {
    Name= "Football",
    Price = 15.00M,
    Sold = false,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2010,
    Condition = 4.2
  },
  new Product()
  {
    Name = "Hockey Stick",
    Price = 12.00M,
    Sold = false,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2011,
    Condition = 3.2
  },
  new Product()
  {
    Name = "Baseball Bat",
    Price = 20.00M,
    Sold = true,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2012,
    Condition = 4.5
  },
  new Product()
  {
    Name = "Shin guards",
    Price = 19.00M,
    Sold = false,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2018,
    Condition = 4.9
  }
  ,
  new Product()
  {
    Name = "Athletic socks",
    Price = 6.00M,
    Sold = false,
    StockDate = new DateTime(2023, 11, 2),
    ManufactureYear = 2019,
    Condition = 5.0
  }
  ,
  new Product()
  {
    Name = "Running shorts",
    Price = 24.99M,
    Sold = true,
    StockDate = new DateTime(2023, 10, 22),
    ManufactureYear = 2022,
    Condition = 4.8
  }
};

string greeting = @"Welcome to Thrown for a Loop!
Your one-stop shop for used sporting equipment";
Console.WriteLine(greeting);

string choice = null;
while (choice != "0")
{
  Console.WriteLine(@"Choose an option:
                    0. Exit
                    1. View All Products
                    2. View Product Details
                    3. View Latest Products");
  choice = Console.ReadLine();
  if (choice == "0")
  {
    Console.WriteLine("Goodbye!");
  }
  else if (choice == "1")
  {
    ListProducts();
  }
  else if (choice == "2")
  {
    ViewProductDetails();
  }
  else if (choice == "3")
  {
    ViewLatestProducts();
  }
}






void ViewProductDetails()
{
  // ListProducts();

  Product chosenProduct = null;

  while (chosenProduct == null)
  {
    Console.WriteLine("Please enter a product number: ");
    try
    {
      int response = int.Parse(Console.ReadLine().Trim());
      chosenProduct = products[response - 1];
    }
    catch (FormatException)
    {
      Console.WriteLine("Please type only integers!");
    }
    catch (ArgumentOutOfRangeException)
    {
      Console.WriteLine("Please choose an existing item only!");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      Console.WriteLine("Do better!");
    }
  }

  DateTime now = DateTime.Now;
  TimeSpan timeInStock = now - chosenProduct.StockDate;

  Console.WriteLine(@$"You chose: 
  {chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
  It is {now.Year - chosenProduct.ManufactureYear} years old.
  Its condition is rated {chosenProduct.Condition}.
  It {(chosenProduct.Sold ? "is not available." : $"has been in stock for {timeInStock.Days} days.")}");
}

void ListProducts()
{
  decimal totalValue = 0.0M;
  foreach (Product product in products)
  {
    if (!product.Sold)
    {
      totalValue += product.Price;
    }
  }
  Console.WriteLine($"Total inventory value: ${totalValue}");
  Console.WriteLine("Products:");
  for (int i = 0; i < products.Count; i++)
  {
    Console.WriteLine($"{i + 1}. {products[i].Name}");
  }
}


void ViewLatestProducts()
{
  List<Product> latestProducts = new List<Product>();
  DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);
  foreach (Product product in products)
  {
    if (product.StockDate > threeMonthsAgo && !product.Sold)
    {
      latestProducts.Add(product);
    }
  }
  for (int i = 0; i < latestProducts.Count; i ++)
  {
    Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
  }
}


// Console.WriteLine("Please enter a product number: ");
// int response = int.Parse(Console.ReadLine().Trim());
// while (response > products.Count || response < 1)
// {
//   Console.WriteLine("You didn't choose anything!");
//   Console.WriteLine("Please enter a product name:");
//   response = int.Parse(Console.ReadLine().Trim());
// }

// Product chosenProduct = products[response - 1];


  // decimal totalValue = 0.0M;
  // foreach (Product product in products)
  // {
  //   if (!product.Sold)
  //   {
  //     totalValue += product.Price;
  //   }
  // }
  // Console.WriteLine($"Total inventory value: ${totalValue}");


  // Console.WriteLine("Products:");
  // for (int i = 0; i < products.Count; i++)
  // {
  //   Console.WriteLine($"{i + 1}. {products[i].Name}");
  // }