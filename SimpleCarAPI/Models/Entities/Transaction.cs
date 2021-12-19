namespace SimpleCar.Models.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTime PurchasedDate { get; set; }
}