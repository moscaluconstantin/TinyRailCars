using System.Text.Json;
using TinyRailCars.ImportAndExport;
using TinyRailCars.Models;

namespace TinyRailCars.Services
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

                var perkStats = GetPerkStats(car.Perk);

                if(perkStats != null)
                    stats.Add(perkStats);

                var level = int.Parse(rawCar.Level);

                car.Stats[level - 1] = stats;
            }

            return cars;
        }

        public static TrainCarStats? GetPerkStats(string perk)
        {
            if (string.IsNullOrEmpty(perk) || perk == "None" || perk.Contains(" if ") || perk.Contains(" When ") || perk.Contains(" when ") || perk.Contains("Boost"))
                return null;

            var perkParts = perk.Split(' ');

            if (perkParts.Length < 2)
                return null;

            var stats = new TrainCarStats();

            var statValue = int.Parse(perkParts[0].TrimStart('+', '-'));

            if (perkParts[0][0] == '-')
                statValue *= -1;

            switch (perkParts[1])
            {
                case "Weight":
                    stats.Weight = statValue;
                    break;

                case "Passengers":
                    stats.Passengers = statValue;
                    break;

                case "Cargo":
                    stats.Cargo = statValue;
                    break;

                case "Food":
                    stats.Food = statValue;
                    break;

                case "Comfort":
                    stats.Comfort = statValue;
                    break;

                case "Entertainment":
                    stats.Entertainment = statValue;
                    break;

                case "Facilities":
                    stats.Facilities = statValue;
                    break;

                default: break;
            }

            return stats;
        }
    }
}
