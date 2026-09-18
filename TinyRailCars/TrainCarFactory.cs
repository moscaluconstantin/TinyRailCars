using System.Text.Json;

namespace TinyRailCars
{
    public static class TrainCarFactory
    {
        public static List<TrainCar> BuildCars()
        {
            var rawCars = JsonSerializer.Deserialize<List<TrainCarImport>>(RawData.Json)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Level)
                .ToList();

            var cars = new List<TrainCar>();

            foreach (var rawCar in rawCars)
            {
                var car = cars.FirstOrDefault(x => x.Name == rawCar.Name);

                if (car is null)
                {
                    car = new TrainCar
                    {
                        Number = int.Parse(rawCar.Number),
                        Name = rawCar.Name,
                        Stats = new TrainCarStats[3],
                        Perk = rawCar.Perk == "None" ? string.Empty : rawCar.Perk,
                    };

                    cars.Add(car);
                }

                var stats = new TrainCarStats
                {
                    Weight = float.Parse(rawCar.Weight),
                    Passengers = int.Parse(rawCar.Passengers),
                    Cargo = int.Parse(rawCar.Cargo),
                    Food = int.Parse(rawCar.Food),
                    Comfort = int.Parse(rawCar.Comfort),
                    Entertainment = int.Parse(rawCar.Entertainment),
                    Facilities = int.Parse(rawCar.Facilities)
                };

                var level = int.Parse(rawCar.Level);

                car.Stats[level - 1] = stats;
            }

            return cars;
        }
    }
}
