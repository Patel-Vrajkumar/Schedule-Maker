using System.Text.Json;

namespace ScheduleMaker.Models
{
    public class Schedule
    {
        public DateTime WeekStartDate { get; set; }
        public List<Employee> Employees { get; set; } = new();
        public List<Shift> Shifts { get; set; } = new();
        public Dictionary<DayOfWeek, int> ProjectedSales { get; set; } = new();

        public Schedule()
        {
            // Set default projected sales
            ProjectedSales[DayOfWeek.Sunday] = 1200;
            ProjectedSales[DayOfWeek.Monday] = 1200;
            ProjectedSales[DayOfWeek.Tuesday] = 1200;
            ProjectedSales[DayOfWeek.Wednesday] = 1550;
            ProjectedSales[DayOfWeek.Thursday] = 1550;
            ProjectedSales[DayOfWeek.Friday] = 1500;
            ProjectedSales[DayOfWeek.Saturday] = 1250;
        }

        public double GetEmployeeWeeklyHours(string employeeName)
        {
            return Shifts.Where(s => s.EmployeeName == employeeName)
                        .Sum(s => s.TotalHours);
        }

        public void SaveToFile(string filePath)
        {
            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            File.WriteAllText(filePath, json);
        }

        public static Schedule? LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Schedule>(json);
        }
    }
}
