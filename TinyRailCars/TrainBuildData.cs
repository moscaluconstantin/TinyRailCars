namespace TinyRailCars
{
    public class TrainBuildData
    {
        public string Numbers { get; private set; }
        public string Names { get; private set; }

        public float Weight { get; set; } = 0;
        public int Passengers { get; set; } = 0;
        public int Cargo { get; set; } = 0;
        public int Food { get; set; } = 0;
        public int Comfort { get; set; } = 0;
        public int Entertainment { get; set; } = 0;
        public int Facilities { get; set; } = 0;

        public TrainBuildData(List<TrainCar> cars)
        {
            Numbers = string.Join(" | ", cars.Select(x => x.Number));
            Names = string.Join(" | ", cars.Select(x => x.Name));

            var stats = new TrainCarStats();

            foreach (var car in cars)
                stats.Add(car.Stats[0]);

            Weight = stats.Weight;
            Passengers = stats.Passengers;
            Cargo = stats.Cargo;
            Food = stats.Food;
            Comfort = stats.Comfort;
            Entertainment = stats.Entertainment;
            Facilities = stats.Facilities;
        }
    }
}
