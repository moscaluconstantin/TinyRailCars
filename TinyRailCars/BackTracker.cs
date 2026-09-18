namespace TinyRailCars
{
    public class BackTracker
    {
        private int[] _combination;
        private List<int[]> _solutions;
        private List<TrainCar> _cars;

        public BackTracker() =>
            _solutions = new List<int[]>();

        public List<List<TrainCar>> Run(List<TrainCar> cars, int length)
        {
            _combination = new int[length];
            _solutions.Clear();
            _cars = cars;

            BackTrack(0);

            var result = new List<List<TrainCar>>();

            foreach (var combination in _solutions)
            {
                var comb = combination.Select(x => _cars[x]).ToList();
                result.Add(comb);
            }

            return result;
        }

        private void BackTrack(int index)
        {
            if (index >= _combination.Length)
            {
                if (Check())
                    _solutions.Add(_combination);

                return;
            }

            for (int i = 0; i < _cars.Count; i++)
            {
                _combination[index] = i;

                if (Check(index))
                    BackTrack(index + 1);
            }
        }

        private bool Check(int index)
        {
            if (index == 0)
                return true;

            for (int i = 0; i < index; i++)
            {
                if (_combination[i] >= _combination[index])
                    return false;
            }

            return true;
        }

        private bool Check()
        {
            var stats = new TrainCarStats();

            foreach (var index in _combination)
                stats.Add(_cars[index].Stats[0]);

            if (stats.Passengers == 0) return false;
            if (stats.Cargo == 0) return false;
            if (stats.Food == 0) return false;
            if (stats.Comfort == 0) return false;
            if (stats.Entertainment == 0) return false;
            if (stats.Facilities == 0) return false;

            if (stats.Weight > 85) return false;

            var minStat = stats.Avg * 0.7f;

            if (stats.Passengers < minStat) return false;
            if (stats.Cargo < minStat) return false;
            if (stats.Food < minStat) return false;
            if (stats.Comfort < minStat) return false;
            if (stats.Entertainment < minStat) return false;
            if (stats.Facilities < minStat) return false;

            foreach (var solution in _solutions)
            {
                for (var i = 0; i < _combination.Length; i++)
                {
                    if (solution[i] != _combination[i])
                        return true;
                }

                return false;
            }

            return true;
        }
    }
}
