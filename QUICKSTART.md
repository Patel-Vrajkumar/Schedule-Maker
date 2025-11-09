# Quick Start Guide

Get your Schedule Maker up and running in 5 minutes!

## Prerequisites

- Windows 10 or later
- .NET 8.0 SDK installed ([Download here](https://dotnet.microsoft.com/download))

## Installation Steps

### Option 1: Visual Studio (Recommended)

1. **Clone the repository**
   ```bash
   git clone https://github.com/Patel-Vrajkumar/Schedule-Maker.git
   cd Schedule-Maker
   ```

2. **Open in Visual Studio**
   - Double-click `ScheduleMaker.sln`
   - Visual Studio 2022 will open

3. **Build the project**
   - Press `Ctrl+Shift+B` or select `Build > Build Solution`

4. **Run the application**
   - Press `F5` or click the green "Start" button
   - The Schedule Maker application will launch

### Option 2: Command Line

1. **Clone the repository**
   ```bash
   git clone https://github.com/Patel-Vrajkumar/Schedule-Maker.git
   cd Schedule-Maker
   ```

2. **Build the project**
   ```bash
   dotnet build
   ```

3. **Run the application**
   ```bash
   dotnet run --project ScheduleMaker
   ```

## First Time Setup

### Step 1: Add Employees (2 minutes)

1. Click **"Add Employee"**
2. Fill in:
   - **Name**: "John Smith"
   - **Weekly Hours**: "20-30 hours"
   - **Availability**: Check Monday, Wednesday, Friday
   - **Shift Preferences**: Check "Opening" and "Middle Shift"
3. Click **"Add"**

Repeat this 3-4 times to add more employees with different availabilities.

### Step 2: Generate Schedule (30 seconds)

1. Click **"Generate Schedule"** (orange button)
2. Watch as the schedule populates automatically
3. Review the schedule in the grid

### Step 3: Save Your Work (10 seconds)

1. Click **"Save Schedule"** (blue button)
2. Your schedule is now saved to `schedule.json`

## What You'll See

### Main Window
```
┌─────────────────────────────────────────────────────────────┐
│              SUBWAY SCHEDULE MAKER                           │
├─────────────────────────────────────────────────────────────┤
│ [Add Employee] [Remove Employee] [Generate] [Save] [Load]   │
├─────────────────────────────────────────────────────────────┤
│ Employee │ Wed │ Thu │ Fri │ Sat │ Sun │ Mon │ Tue │ Hours │
│──────────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┼───────│
│ John     │ 8-2 │ OFF │ 8-2 │ 11-4│ OFF │ 7-1 │ OFF │ 18.0  │
│ Sarah    │ 2-9 │ 5-10│ OFF │ 5-10│ 8-2 │ OFF │ 11-4│ 22.5  │
│ Mike     │ OFF │ 8-2 │ 5-10│ OFF │ 11-4│ 2-9 │ 8-1 │ 25.0  │
└─────────────────────────────────────────────────────────────┘
```

### Color Coding
- **Green**: Available but not scheduled
- **Gray**: Day off / Not available
- **White**: Scheduled shift with times

## Common First Tasks

### Add More Employees
Keep adding employees until you have adequate coverage for all days.

### Regenerate for Different Options
Don't like the schedule? Click "Generate Schedule" again for a different random arrangement.

### Update Employee Availability
1. Remove the employee
2. Re-add with new availability
3. Regenerate the schedule

### Load Previous Schedule
Click "Load Schedule" to restore your last saved schedule.

## Tips for Success

1. **Add 5-8 employees** for best coverage
2. **Vary availability** - not everyone on the same days
3. **Mix shift preferences** - some openers, some closers
4. **Save regularly** - don't lose your work
5. **Regenerate if needed** - it's quick and easy

## Troubleshooting

### App Won't Start
- Verify .NET 8.0 SDK is installed: `dotnet --version`
- Re-run: `dotnet build`

### Can't Add Employee
- Make sure to enter a name
- Select at least one available day

### No Shifts Generated
- Check employee availability
- Ensure hour preferences aren't all 0-10
- Add more employees

### Changes Don't Save
- Make sure you click "Save Schedule" before closing
- Check file permissions in the application directory

## Next Steps

1. Read the [USAGE_GUIDE.md](USAGE_GUIDE.md) for detailed instructions
2. Check [FEATURES.md](FEATURES.md) for complete feature list
3. Review [README.md](README.md) for technical details

## Need Help?

- Review the documentation files
- Check the code comments in the source files
- Open an issue on GitHub

## Ready to Go!

You now have a working Schedule Maker application. Start adding your team and generate your first schedule!
