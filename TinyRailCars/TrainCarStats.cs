namespace TinyRailCars
{
    public class TrainCarStats
    {
        public float Weight { get; set; } = 0;
        public int Passengers { get; set; } = 0;
        public int Cargo { get; set; } = 0;
        public int Food { get; set; } = 0;
        public int Comfort { get; set; } = 0;
        public int Entertainment { get; set; } = 0;
        public int Facilities { get; set; } = 0;

        public int Total => Passengers + Cargo + Food + Comfort + Entertainment + Facilities;
        public float Avg => (float)Total / 6;

        public void Add(TrainCarStats stats)
        {
            Weight += stats.Weight;
            Passengers += stats.Passengers;
            Cargo += stats.Cargo;
            Food += stats.Food;
            Comfort += stats.Comfort;
            Entertainment += stats.Entertainment;
            Facilities += stats.Facilities;
        }

        public override string ToString() =>
            $"We{Weight}, Pa{Passengers}, Ca{Cargo}, Fo{Food}, Co{Comfort}, En{Entertainment}, Fa{Facilities}";
    }
}
