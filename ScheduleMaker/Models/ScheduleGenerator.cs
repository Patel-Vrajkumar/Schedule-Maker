namespace ScheduleMaker.Models
{
    public class ScheduleGenerator
    {
        private readonly Random _random = new();
        private readonly Dictionary<DayOfWeek, (TimeSpan Open, TimeSpan Close)> _storeHours;

        public ScheduleGenerator()
        {
            _storeHours = new Dictionary<DayOfWeek, (TimeSpan, TimeSpan)>
            {
                { DayOfWeek.Sunday, (new TimeSpan(8, 0, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Monday, (new TimeSpan(8, 0, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Tuesday, (new TimeSpan(8, 0, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Wednesday, (new TimeSpan(8, 0, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Thursday, (new TimeSpan(7, 30, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Friday, (new TimeSpan(7, 30, 0), new TimeSpan(22, 0, 0)) },
                { DayOfWeek.Saturday, (new TimeSpan(8, 0, 0), new TimeSpan(22, 0, 0)) }
            };
        }

        public void GenerateSchedule(Schedule schedule)
        {
            schedule.Shifts.Clear();
            var weekDays = GetWeekDays(schedule.WeekStartDate);

            // Track hours assigned to each employee
            var employeeHours = new Dictionary<string, double>();
            foreach (var emp in schedule.Employees)
            {
                employeeHours[emp.Name] = 0;
            }

            // Get target hours for each employee
            var targetHours = new Dictionary<string, (double min, double max)>();
            foreach (var emp in schedule.Employees)
            {
                targetHours[emp.Name] = GetHourRange(emp.HourPreference);
            }

            // Generate shifts for each day
            foreach (var day in weekDays)
            {
                var availableEmployees = schedule.Employees
                    .Where(e => e.Availability[day.DayOfWeek] && 
                               employeeHours[e.Name] < targetHours[e.Name].max)
                    .ToList();

                if (availableEmployees.Count == 0)
                    continue;

                // Generate shifts based on store hours and shift types
                var shifts = GenerateDayShifts(day, availableEmployees, employeeHours, targetHours);
                schedule.Shifts.AddRange(shifts);
            }
        }

        private List<Shift> GenerateDayShifts(
            DateTime date, 
            List<Employee> availableEmployees, 
            Dictionary<string, double> employeeHours,
            Dictionary<string, (double min, double max)> targetHours)
        {
            var shifts = new List<Shift>();
            var dayOfWeek = date.DayOfWeek;
            var (openTime, closeTime) = _storeHours[dayOfWeek];

            // Define shift time windows
            var shiftWindows = new List<(ShiftType type, TimeSpan start, TimeSpan end, TimeSpan? breakDuration)>();

            // Opening shift
            shiftWindows.Add((ShiftType.Opening, openTime, openTime.Add(TimeSpan.FromHours(5.5)), TimeSpan.FromMinutes(30)));
            
            // Post-opening (lunch rush) - around 11:30 AM
            if (openTime.Hours <= 11)
            {
                shiftWindows.Add((ShiftType.PostOpening, new TimeSpan(11, 30, 0), new TimeSpan(15, 30, 0), TimeSpan.FromMinutes(15)));
            }

            // Middle shift
            shiftWindows.Add((ShiftType.Middle, new TimeSpan(14, 0, 0), new TimeSpan(19, 0, 0), TimeSpan.FromMinutes(30)));

            // Pre-closing (supper rush) - around 5 PM
            shiftWindows.Add((ShiftType.PreClosing, new TimeSpan(17, 0, 0), closeTime, TimeSpan.FromMinutes(30)));

            // Closing shift
            shiftWindows.Add((ShiftType.Closing, closeTime.Subtract(TimeSpan.FromHours(5)), closeTime, TimeSpan.FromMinutes(15)));

            // Assign employees to shifts randomly
            var shuffledEmployees = availableEmployees.OrderBy(x => _random.Next()).ToList();
            int empIndex = 0;

            foreach (var window in shiftWindows)
            {
                // Find an employee who prefers this shift type or any employee
                Employee? selectedEmployee = null;
                
                // First try to find someone who prefers this shift
                for (int i = 0; i < shuffledEmployees.Count; i++)
                {
                    var emp = shuffledEmployees[(empIndex + i) % shuffledEmployees.Count];
                    if (emp.PreferredShifts.Contains(window.type) && 
                        employeeHours[emp.Name] < targetHours[emp.Name].max)
                    {
                        selectedEmployee = emp;
                        empIndex = (empIndex + i + 1) % shuffledEmployees.Count;
                        break;
                    }
                }

                // If no one prefers it, assign to any available employee
                if (selectedEmployee == null)
                {
                    for (int i = 0; i < shuffledEmployees.Count; i++)
                    {
                        var emp = shuffledEmployees[(empIndex + i) % shuffledEmployees.Count];
                        if (employeeHours[emp.Name] < targetHours[emp.Name].max)
                        {
                            selectedEmployee = emp;
                            empIndex = (empIndex + i + 1) % shuffledEmployees.Count;
                            break;
                        }
                    }
                }

                if (selectedEmployee != null)
                {
                    var shift = new Shift
                    {
                        EmployeeName = selectedEmployee.Name,
                        Day = dayOfWeek,
                        StartTime = window.start,
                        EndTime = window.end,
                        BreakDuration = window.breakDuration,
                        Type = window.type
                    };

                    // Ensure minimum 3 hours
                    if (shift.TotalHours >= 3)
                    {
                        shifts.Add(shift);
                        employeeHours[selectedEmployee.Name] += shift.TotalHours;
                    }
                }
            }

            return shifts;
        }

        private (double min, double max) GetHourRange(HourRange range)
        {
            return range switch
            {
                HourRange.ZeroToTen => (0, 10),
                HourRange.TenToTwenty => (10, 20),
                HourRange.TwentyToThirty => (20, 30),
                HourRange.ThirtyToForty => (30, 40),
                _ => (0, 10)
            };
        }

        private List<DateTime> GetWeekDays(DateTime weekStart)
        {
            var days = new List<DateTime>();
            // Week starts on Wednesday
            var current = weekStart;
            for (int i = 0; i < 7; i++)
            {
                days.Add(current);
                current = current.AddDays(1);
            }
            return days;
        }

        public static DateTime GetCurrentWeekStart()
        {
            var today = DateTime.Today;
            var dayOfWeek = (int)today.DayOfWeek;
            
            // Wednesday is day 3, calculate days to subtract to get to Wednesday
            int daysToSubtract = (dayOfWeek - (int)DayOfWeek.Wednesday + 7) % 7;
            return today.AddDays(-daysToSubtract);
        }
    }
}
