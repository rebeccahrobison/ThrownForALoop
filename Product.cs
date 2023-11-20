public class Product
{
  public string Name { get; set; }
  public decimal Price { get; set; }
  public DateTime? SoldOnDate { get; set; }
  public DateTime StockDate { get; set; }
  public TimeSpan TimeInStock
  {
    get
    {
      DateTime lastDay = SoldOnDate != null ? (DateTime)SoldOnDate : DateTime.Now;
      return lastDay - StockDate;
    }
    // get
    // {
    //   DateTime lastDay;
    //   if (SoldOnDate != null)
    //   {
    //     lastDay = (DateTime)SoldOnDate;
    //   }
    //   else
    //   {
    //     lastDay = DateTime.Now;
    //   }
    //   return lastDay - StockDate;
    // }
  }
  public int ManufactureYear { get; set; }
  public double Condition { get; set; }
}