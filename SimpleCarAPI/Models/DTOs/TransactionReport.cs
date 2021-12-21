using SimpleCar.Models.Entities;

namespace SimpleCar.Models.DTOs;

public class TransactionReport
{
    public string CustomerType { get; set; }
    public string CustomerName { get; set; }
    public string CustomerTitle { get; set; }
    public short CarYear { get; set; }
    public string CarBrand { get; set; }
    public string CarModel { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTime PurchasedDate { get; set; }

    public string GetReport()
    {
        return $"[{CustomerType}] {CustomerTitle}. {CustomerName} - {CarBrand} {CarModel} {CarYear} - {decimal.Round(Amount)} {Currency} - {PurchasedDate.ToShortDateString()}";
    }
}