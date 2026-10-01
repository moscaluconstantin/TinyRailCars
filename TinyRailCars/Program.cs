using System.Text;
using TinyRailCars.ImportAndExport;
using TinyRailCars.Services;

namespace TinyRailCars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ownedCars = [2, 3, 4, 5, 6, 7, 8, 10, 11, 12, 14, 15, 16, 17, 18, 19, 21, 22, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 37, 38, 39, 41, 42, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 62, 63, 64, 65, 66, 68, 70, 71, 73, 75, 81, 84, 86, 87, 90, 93, 95, 96, 98, 104, 105, 123, 131, 133, 135, 137, 274];
            var cars = TrainCarFactory.BuildCars()
                .Where(x => ownedCars.Contains(x.Number) && x.GetStats().ScoreEffect >= 0 && x.GetStats().ScoreStats > 0)
                .OrderBy(x => x.Number)
                .ToList();

            //foreach (var car in cars)
            //{
            //    Console.WriteLine(car);
            //}

            var back = new BackTracker();

            Console.WriteLine("Starting");

            var combinations = back.Run(cars, 16)
                .Where(x=>x.Score > 0.9f)
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x=>x.Stats.Cargo)
                .Take(100)
                .ToList();

            Console.WriteLine($"Found {combinations.Count} combinations");

            Console.WriteLine("Exporting");

            var data = combinations.Select(x => new TrainBuildData(x)).ToList();
            ExportToCsv(data, "builds.csv");

            Console.WriteLine("Done");

            Console.ReadKey();
        }

        public static void ExportToCsv<T>(List<T> items, string filePath)
        {
            var properties = typeof(T).GetProperties();
            var sb = new StringBuilder();

            // Header
            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            // Rows
            foreach (var item in items)
            {
                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item)?.ToString() ?? "";
                    // Escape values containing commas, quotes, or newlines
                    if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                        value = $"\"{value.Replace("\"", "\"\"")}\"";
                    return value;
                });
                sb.AppendLine(string.Join(",", values));
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
