namespace ScheduleMaker.Models
{
    public class Employee
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<DayOfWeek, bool> Availability { get; set; } = new();
        public List<ShiftType> PreferredShifts { get; set; } = new();
        public HourRange HourPreference { get; set; }
        
        public Employee()
        {
            // Initialize availability for all days as false
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                Availability[day] = false;
            }
        }
    }

    public enum ShiftType
    {
        Opening,
        PostOpening,  // Lunch rush
        Middle,
        PreClosing,   // Supper rush
        Closing
    }

    public enum HourRange
    {
        ZeroToTen,
        TenToTwenty,
        TwentyToThirty,
        ThirtyToForty
    }
}
