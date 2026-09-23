namespace TinyRailCars.Models
{
    public class TrainCar
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public TrainCarStats[] Stats { get; set; }
        public string Perk { get; set; } = string.Empty;

        public TrainCarStats GetStats(int level = 1) =>
            Stats[level - 1];

        public override string ToString() =>
            $"{Number} {Name}: {Stats[0]}";
    }
}
