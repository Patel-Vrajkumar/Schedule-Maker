namespace ScheduleMaker.Models
{
    public class Shift
    {
        public string EmployeeName { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan? BreakDuration { get; set; }
        public ShiftType Type { get; set; }

        public double TotalHours
        {
            get
            {
                var totalTime = EndTime - StartTime;
                if (BreakDuration.HasValue)
                {
                    totalTime -= BreakDuration.Value;
                }
                return totalTime.TotalHours;
            }
        }

        public override string ToString()
        {
            string timeStr = $"{FormatTime(StartTime)}-{FormatTime(EndTime)}";
            if (BreakDuration.HasValue)
            {
                timeStr += $"\n({BreakDuration.Value.TotalMinutes}min break)";
            }
            return timeStr;
        }

        private string FormatTime(TimeSpan time)
        {
            int hour = time.Hours;
            int minute = time.Minutes;
            return $"{hour}:{minute:D2}";
        }
    }
}
