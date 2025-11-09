# Schedule-Maker
A C# Windows Forms application for managing employee schedules at a restaurant (Subway).

## Features

### Employee Management
- **Add Employee**: Create new employee profiles with:
  - Name
  - Weekly hour preferences (0-10, 10-20, 20-30, 30-40 hours)
  - Availability for each day of the week (Wednesday through Tuesday)
  - Shift preferences (Opening, Post-Opening/Lunch Rush, Middle, Pre-Closing/Supper Rush, Closing)

- **Remove Employee**: Select and remove employees from the schedule

### Schedule Generation
- **Automatic Schedule Creation**: Generates shifts automatically based on:
  - Employee availability
  - Hour preferences
  - Shift preferences
  - Store operating hours
  - Minimum 3-hour shift requirement

### Schedule Management
- **Save Schedule**: Save the current schedule to a JSON file
- **Load Schedule**: Load a previously saved schedule
- **Visual Schedule Display**: View the entire week's schedule in a grid format showing:
  - Employee names
  - Daily shifts with times and breaks
  - Total weekly hours per employee
  - Available days vs. scheduled days

### Store Hours
The application is configured with the following store hours:
- **Sunday**: 8:00 AM - 10:00 PM
- **Monday**: 8:00 AM - 10:00 PM
- **Tuesday**: 8:00 AM - 10:00 PM
- **Wednesday**: 8:00 AM - 10:00 PM (Week Start)
- **Thursday**: 7:30 AM - 10:00 PM
- **Friday**: 7:30 AM - 10:00 PM
- **Saturday**: 8:00 AM - 10:00 PM

### Shift Types
1. **Opening** - Early morning shift starting when store opens
2. **Post-Opening (Lunch Rush)** - Mid-morning to afternoon shift covering lunch
3. **Middle** - Afternoon shift
4. **Pre-Closing (Supper Rush)** - Evening shift covering dinner rush
5. **Closing** - Late shift until store closes

## Requirements

- Windows OS (Windows 10 or later recommended)
- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code (recommended for development)

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/Patel-Vrajkumar/Schedule-Maker.git
   cd Schedule-Maker
   ```

2. Open the solution in Visual Studio:
   ```bash
   ScheduleMaker.sln
   ```

3. Build the project:
   - In Visual Studio: Press `Ctrl+Shift+B` or select `Build > Build Solution`
   - Or from command line: `dotnet build`

4. Run the application:
   - In Visual Studio: Press `F5` or select `Debug > Start Debugging`
   - Or from command line: `dotnet run --project ScheduleMaker`

## Usage

### Adding Employees
1. Click the **"Add Employee"** button
2. Enter the employee's name
3. Select their weekly hour preference
4. Check the days they are available
5. Select their preferred shift types
6. Click **"Add"** to save

### Generating a Schedule
1. Ensure you have added at least one employee
2. Click the **"Generate Schedule"** button
3. The application will automatically create shifts based on:
   - Employee availability
   - Hour preferences
   - Shift preferences
   - Store hours
   - Minimum 3-hour shift requirement

### Saving and Loading
- **Save**: Click **"Save Schedule"** to save the current schedule to `schedule.json`
- **Load**: Click **"Load Schedule"** to load the last saved schedule

### Removing Employees
1. Click the **"Remove Employee"** button
2. Select the employee from the list
3. Click **"Remove"** and confirm
4. All shifts for that employee will be removed from the schedule

## Project Structure

```
ScheduleMaker/
├── Forms/
│   ├── MainForm.cs           # Main schedule display and management
│   ├── AddEmployeeForm.cs    # Form for adding new employees
│   └── RemoveEmployeeForm.cs # Form for removing employees
├── Models/
│   ├── Employee.cs           # Employee data model
│   ├── Shift.cs              # Shift data model
│   ├── Schedule.cs           # Schedule data model with save/load
│   └── ScheduleGenerator.cs  # Automatic schedule generation logic
└── Program.cs                # Application entry point
```

## Technical Details

- **Framework**: .NET 8.0 (Windows Forms)
- **Language**: C#
- **Data Persistence**: JSON serialization
- **Week Definition**: Wednesday through Tuesday
- **Minimum Shift Duration**: 3 hours
- **Break Handling**: Automatic break allocation based on shift length

## License

This project is open source and available under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
