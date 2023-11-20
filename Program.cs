using System.Linq;
using System.Net;
using System.Net.WebSockets;

List<Product> products = new List<Product>()
{
  new Product()
  {
    Name= "Football",
    Price = 15.00M,
    SoldOnDate = null,
    StockDate = new DateTime(2022, 11, 20),
    ManufactureYear = 2010,
    Condition = 4.2
  },
  new Product()
  {
    Name = "Hockey Stick",
    Price = 12.00M,
    SoldOnDate = null,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2011,
    Condition = 3.2
  },
  new Product()
  {
    Name = "Baseball Bat",
    Price = 20.00M,
    SoldOnDate = new DateTime(2023, 11, 19, 10, 10, 00),
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2012,
    Condition = 4.5
  },
  new Product()
  {
    Name = "Shin guards",
    Price = 19.00M,
    SoldOnDate = null,
    StockDate = new DateTime(2022, 10, 20),
    ManufactureYear = 2018,
    Condition = 4.9
  }
  ,
  new Product()
  {
    Name = "Athletic socks",
    Price = 6.00M,
    SoldOnDate = null,
    StockDate = new DateTime(2023, 11, 2),
    ManufactureYear = 2019,
    Condition = 5.0
  }
  ,
  new Product()
  {
    Name = "Running shorts",
    Price = 24.99M,
    SoldOnDate = new DateTime(2023, 10, 30, 2, 30, 00),
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
                    3. View Latest Products
                    4. Monthly Sales Report");
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
  else if (choice == "4")
  {
    MonthlySalesReport();
  }
  else if (choice == "5")
  {
    AddProduct();
  }
}




void ViewProductDetails()
{
  Product chosenProduct = null;
  ListProducts();
  int response = -1;
  while (chosenProduct == null)
  {
    Console.WriteLine("Please enter a product number: ");
    try
    {
      response = int.Parse(Console.ReadLine().Trim());
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

  Console.WriteLine(@$"You chose: 
  {chosenProduct.Name}, which costs {chosenProduct.Price} dollars.
  It is {now.Year - chosenProduct.ManufactureYear} years old.
  Its condition is rated {chosenProduct.Condition}.
  It {(chosenProduct.SoldOnDate != null ? "is not available." : $"has been in stock for {chosenProduct.TimeInStock} days.")}");

  Console.WriteLine("Would you like to mark the item as sold? Y/N");
  string userInput = Console.ReadLine();
  if (userInput == "Y")
  {
    MarkAsSold(response);
  }
}

void ListProducts()
{
  decimal totalValue = 0.0M;
  foreach (Product product in products)
  {
    if (product.SoldOnDate != null)
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
  Console.WriteLine($"Average Product Time in Stock: {AverageTimeStocked()} days");
  Console.WriteLine($"Average Product Time on Shelf Before Sale: {AverageTimeBeforeSold()} days");
  BusiestSaleHours();
}

void ViewLatestProducts()
{
  List<Product> latestProducts = new List<Product>();
  DateTime threeMonthsAgo = DateTime.Now - TimeSpan.FromDays(90);
  foreach (Product product in products)
  {
    if (product.StockDate > threeMonthsAgo && product.SoldOnDate != null)
    {
      latestProducts.Add(product);
    }
  }
  for (int i = 0; i < latestProducts.Count; i ++)
  {
    Console.WriteLine($"{i + 1}. {latestProducts[i].Name}");
  }
}

void MonthlySalesReport()
{
  Console.WriteLine("Input the year for the report you want as YYYY");
  int userInputYear = int.Parse(Console.ReadLine());
  Console.WriteLine("Input the month for the report you want as MM");
  int userInputMonth = int.Parse(Console.ReadLine());

  IEnumerable<Product> foundProducts = products.Where(product => 
  {
    if (product.SoldOnDate != null)
    {
      return  product.SoldOnDate.Value.Year == userInputYear &&
              product.SoldOnDate.Value.Month == userInputMonth;
    }
    return false;
  });

  decimal totalPrice = foundProducts.Sum(product => product.Price);
  Console.WriteLine($"Total sales for products sold in {userInputMonth}/{userInputYear}: ${totalPrice}");
}

void AddProduct()
{
  Console.WriteLine("Please enter the details of the product to add:");
  Console.WriteLine("What is the name of the product?");
  string productNameToAdd = Console.ReadLine();
  Console.WriteLine("What is the price of the product?");
  decimal productPriceToAdd = decimal.Parse(Console.ReadLine());
  Console.WriteLine("What is the manufacture year of the product?");
  int productManufactureYearToAdd = int.Parse(Console.ReadLine());
  Console.WriteLine("What is the condition of the product on a scale of 0.0 - 5.0?");
  double productConditionToAdd = double.Parse(Console.ReadLine());

  DateTime now = new DateTime();
  Product productToAdd = new Product()
  {
    Name = productNameToAdd,
    Price = productPriceToAdd,
    ManufactureYear = productManufactureYearToAdd,
    Condition = productConditionToAdd,
    StockDate = now,
    SoldOnDate = null
  };

  products.Add(productToAdd);
  Console.WriteLine($"Your {productNameToAdd} was successfully added.");

  //TODO test add product
}

void MarkAsSold(int userInput)
{
  products[userInput - 1].SoldOnDate = new DateTime();
}

TimeSpan AverageTimeStocked()
{
  // Allow the user to see the average time that currently stocked products have been on the shelf (in days).
  TimeSpan totalTime = new TimeSpan(products.Sum(p => p.TimeInStock.Ticks));
  return totalTime / (double)products.Count;
}

TimeSpan AverageTimeBeforeSold()
{
  // Allow the user to see the average amount of time the sold products were on the shelf before sale.
  var soldProducts = products.Where(p => p.SoldOnDate != null);
  TimeSpan totalTime = new TimeSpan(soldProducts.Sum(p => p.TimeInStock.Ticks));
  int soldProductsCount = soldProducts.Count();
  return totalTime / (double)soldProductsCount;
}

void BusiestSaleHours()
{
  List<Product> soldProducts = products.Where(p => p.SoldOnDate != null).ToList();
  int[] storeHours = { 9, 10, 11, 12, 1, 2, 3, 4 };
  // iterate through store hours 9-4
  foreach (int h in storeHours)
  {
    int numberSold = 0;
    foreach (Product p in soldProducts)
    {
      if (h == p.SoldOnDate.Value.Hour)
      {
        numberSold++;
      }
    }
    Console.WriteLine($"{h}:00 -- {numberSold} items sold");
  }
  // if product was sold in that hours, console it
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