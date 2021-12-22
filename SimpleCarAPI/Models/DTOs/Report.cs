using SimpleCar.Models.Entities;

namespace SimpleCar.Models.DTOs;

public class Report
{
    public Car Car { get; }
    public Customer Customer { get; }
    public Transaction Transaction { get; }
    public string CarCode { get; }

    public Report(Car car, Customer customer, Transaction transaction)
    {
        Car = car;
        Customer = customer;
        Transaction = transaction;
        CarCode = $"{car.Brand} {car.Model} {car.Year}".ToSha256Hash();
    }

    public string GetReport()
    {
        return
            $"[{Customer.Type}] {Customer.Title}. {Customer.Name} | {Car.Brand} {Car.Model} {Car.Year} | {decimal.Round(Transaction.Amount)} {Transaction.Currency} | {Transaction.PurchasedDate.ToShortDateString()}";
    }
}