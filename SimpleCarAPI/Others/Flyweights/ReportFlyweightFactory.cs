using System.Collections.Concurrent;
using SimpleCar.Models.Entities;

namespace SimpleCar.Others.Flyweights;

public class ReportFlyweightFactory
{
    private readonly IDictionary<string, ReportFlyweight> _flyweights =
        new ConcurrentDictionary<string, ReportFlyweight>();

    public ReportFlyweight GetReportFlyweight(Car car)
    {
        var key = $"{car.Brand} {car.Model} {car.Year}";
        if (_flyweights.TryGetValue(key, out var existedFlyWeight))
        {
            return existedFlyWeight;
        }

        var flyWeight = new ReportFlyweight(car);
        _flyweights.Add(key, flyWeight);
        return flyWeight;
    }
}
