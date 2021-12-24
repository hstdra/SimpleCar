using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others.Composites
{
    public interface ICarComponent
    {
        void ShowInfomation();
    }

    public class CarComposite : ICarComponent
    {
        private readonly ICollection<ICarComponent> components = new HashSet<ICarComponent>();
        private readonly string note;
        private readonly int level;

        public CarComposite(string note, int level = 0)
        {
            this.level = level;
            this.note = note;
        }

        public void Add(ICarComponent component)
        {
            components.Add(component);
        }

        public void Remove(ICarComponent component)
        {
            components.Remove(component);
        }

        public ICollection<ICarComponent> GetChildren()
        {
            return components;
        }

        public void ShowInfomation()
        {
            Console.WriteLine($"{new string(' ', level * 2)}- {note}");
            foreach (var child in GetChildren())
            {
                child.ShowInfomation();
            }
        }
    }

    public class CarLeaf : ICarComponent
    {
        private readonly string note;
        private readonly int level;

        public CarLeaf(string note, int level = 0)
        {
            this.level = level;
            this.note = note;
        }

        public void ShowInfomation()
        {
            Console.WriteLine($"{new string(' ', level * 2)}- {note}");
        }
    }

    public class CarCompositeTests
    {
        private readonly ICarService _carService;

        public CarCompositeTests(ICarService carService)
        {
            _carService = carService;
        }

        public async Task ShowInfomation()
        {
            var cars = await _carService.GetAll();
            var rootComposite = new CarComposite("Cars:");
            
            foreach (var yearGroup in cars.GroupBy(x => x.Year))
            {
                var yearComposite = new CarComposite($"Year: {yearGroup.Key}", 1);
                rootComposite.Add(yearComposite);

                foreach (var brandGroup in yearGroup.GroupBy(x => x.Brand))
                {
                    var brandComposite = new CarComposite($"Brand: {brandGroup.Key}", 2);
                    yearComposite.Add(brandComposite);

                    foreach (var model in brandGroup)
                    {
                        var modelLeaf = new CarLeaf($"Model: {model.Model}", 3);
                        brandComposite.Add(modelLeaf);
                    }
                }
            }

            rootComposite.ShowInfomation();
        }
    }
}
