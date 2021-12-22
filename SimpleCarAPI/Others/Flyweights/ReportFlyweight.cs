using SimpleCar.Models.Entities;

namespace SimpleCar.Others;

public class ReportFlyweight
{
    public Car Car { get; }
    public string CarCode { get; }

    public ReportFlyweight(Car car)
    {
        Car = car;
        CarCode = $"{car.Brand} {car.Model} {car.Year}".ToSha256Hash();
    }

    public string GetReport(Customer customer, Transaction transaction)
    {
        return
            $"[{customer.Type}] {customer.Title}. {customer.Name} | {Car.Brand} {Car.Model} {Car.Year} | {decimal.Round(transaction.Amount)} {transaction.Currency} | {transaction.PurchasedDate.ToShortDateString()}";
    }
}
