using System.Text;

namespace TinyRailCars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] ownedCars = [2, 4, 5, 6, 7, 12, 14, 15, 16, 17, 18, 21, 25, 27, 30, 31, 32, 33, 35, 37, 42, 44, 63, 64, 71, 73, 135];
            var cars = TrainCarFactory.BuildCars()
                .Where(x => ownedCars.Contains(x.Number))
                .OrderBy(x => x.Number)
                .ToList();

            var back = new BackTracker();

            Console.WriteLine("Starting");

            var combinations = back.Run(cars, 11);

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
