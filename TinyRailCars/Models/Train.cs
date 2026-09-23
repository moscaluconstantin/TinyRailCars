namespace TinyRailCars.Models
{
    public class Train
    {
        private int _passengers;

        public List<TrainCar> Cars { get; set; }
        public TrainCarStats Stats { get; set; }
        public float Score => (Stats.Food + Stats.Comfort + Stats.Entertainment + Stats.Facilities) / (4.0f * Stats.Passengers);

        public Train(int passengers, List<TrainCar> cars)
        {
            _passengers = passengers;
            Cars = cars;
            Stats = new TrainCarStats();
        }

        public Train(List<TrainCar> cars) : this(36, cars) { }

        public void RefreshStats()
        {
            Stats = new TrainCarStats() { Passengers = _passengers };

            foreach (var car in Cars)
                Stats.Add(car.GetStats());
        }
    }
}
