namespace TinyRailCars
{
    public class TrainCar
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public TrainCarStats[] Stats { get; set; }
        public string Perk { get; set; } = string.Empty;

        public override string ToString() =>
            $"{Number} {Name}: {Stats[0]}";
    }
}
