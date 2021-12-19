namespace SimpleCar.Models.DTOs;

public class CustomerReport
{
    public string Name { get; set; }
    public List<TransactionReport?> CarReports { get; set; }
}