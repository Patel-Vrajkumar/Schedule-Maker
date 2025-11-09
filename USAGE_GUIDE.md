# Schedule Maker - User Guide

## Getting Started

When you first launch the Schedule Maker application, you'll see the main window with a green header displaying "SUBWAY SCHEDULE MAKER" and several action buttons below.

## Step-by-Step Tutorial

### 1. Adding Your First Employee

1. Click the **"Add Employee"** button (green button on the left)
2. A new window will appear with the following fields:
   - **Employee Name**: Enter the employee's full name
   - **Weekly Hours**: Select their desired weekly hours from the dropdown:
     - 0-10 hours (part-time)
     - 10-20 hours (part-time)
     - 20-30 hours (full-time)
     - 30-40 hours (full-time)
   
3. **Set Availability**: Check the boxes for days the employee can work:
   - Wednesday
   - Thursday
   - Friday
   - Saturday
   - Sunday
   - Monday
   - Tuesday
   
4. **Select Shift Preferences**: Check which shifts the employee prefers:
   - **Opening**: Early morning shifts when the store opens
   - **Post-Opening (Lunch Rush)**: Mid-morning to afternoon, covers lunch time
   - **Middle Shift**: Afternoon shifts
   - **Pre-Closing (Supper Rush)**: Evening shifts during dinner rush
   - **Closing**: Late shifts until store closes

5. Click **"Add"** to save the employee

**Tip**: Add at least 3-5 employees before generating a schedule to ensure adequate coverage.

### 2. Building Your Team

Repeat the "Add Employee" process for each member of your team. Consider:
- Having a mix of employees with different hour preferences
- Ensuring coverage for all days of the week
- Having employees who prefer different shift types

### 3. Generating a Schedule

Once you have added all your employees:

1. Click the **"Generate Schedule"** button (orange button)
2. The application will automatically:
   - Assign shifts based on availability
   - Respect hour preferences
   - Prioritize shift preferences
   - Ensure minimum 3-hour shifts
   - Distribute hours fairly

3. The schedule will appear in the grid showing:
   - Employee names in the first column
   - Each day of the week (Wednesday through Tuesday)
   - Shift times with breaks
   - Total weekly hours in the last column

### 4. Reading the Schedule

The schedule grid uses color coding:
- **White with shift times**: Employee is scheduled for that day
- **Light Green with "Available"**: Employee is available but not scheduled
- **Light Gray with "OFF"**: Employee is not available that day

Each scheduled shift shows:
- Start time and end time (e.g., "7:45-1:30")
- Break duration if applicable (e.g., "(15mint break)")
- Hours worked below the times

### 5. Saving Your Schedule

To save the current schedule:
1. Click the **"Save Schedule"** button (blue button)
2. The schedule will be saved to a file named `schedule.json`
3. You'll see a confirmation message

**Important**: Save your schedule regularly to avoid losing work!

### 6. Loading a Previous Schedule

To load a previously saved schedule:
1. Click the **"Load Schedule"** button (blue button)
2. If a saved schedule exists, it will be loaded
3. You'll see a confirmation message
4. The schedule grid will update with the loaded data

### 7. Removing an Employee

If an employee leaves or you need to remove someone:
1. Click the **"Remove Employee"** button (green button)
2. Select the employee from the list
3. Click **"Remove"**
4. Confirm the removal when prompted
5. The employee and all their shifts will be removed from the schedule

**Note**: After removing an employee, you may want to regenerate the schedule to fill in the gaps.

## Understanding Store Hours

The application is pre-configured with these store hours:
- **Sunday-Wednesday**: 8:00 AM - 10:00 PM (14 hours)
- **Thursday-Friday**: 7:30 AM - 10:00 PM (14.5 hours)
- **Saturday**: 8:00 AM - 10:00 PM (14 hours)

The schedule generator will only create shifts within these hours.

## Understanding Shift Types

### Opening Shift
- Starts when the store opens
- Responsible for opening procedures
- Usually 5-6 hours with a break

### Post-Opening (Lunch Rush)
- Typically starts around 11:30 AM
- Covers the busy lunch period
- 4-5 hours with a short break

### Middle Shift
- Afternoon coverage
- Bridges lunch and dinner rushes
- 5 hours with a break

### Pre-Closing (Supper Rush)
- Starts around 5:00 PM
- Covers dinner rush period
- Continues until closing

### Closing Shift
- Final hours of operation
- Responsible for closing procedures
- Typically 5 hours

## Tips for Best Results

1. **Balanced Availability**: Ensure you have employees available each day
2. **Diverse Preferences**: Having employees with different shift preferences helps with coverage
3. **Regular Saves**: Save your schedule after making significant changes
4. **Regenerate When Needed**: If the schedule doesn't look right, you can regenerate it
5. **Plan Ahead**: Add all employees before generating to get the best distribution

## Troubleshooting

### "Please add employees before generating a schedule"
- You need at least one employee added before generating
- Click "Add Employee" to add team members

### "No saved schedule found"
- You haven't saved a schedule yet
- Generate and save a schedule first

### Employee has no shifts after generation
- Check if they're marked as available for any days
- Verify their hour preference allows for shifts
- They may have been at their hour limit when shifts were assigned

### Shifts seem unbalanced
- Try regenerating the schedule (randomized algorithm)
- Adjust employee availability or preferences
- Add more employees for better coverage

## Week Definition

The scheduling week runs from **Wednesday to Tuesday**:
- Wednesday is the start of the week
- Tuesday is the end of the week

This is reflected in both the schedule display and the generation algorithm.

## Data Storage

- Schedules are saved in JSON format to `schedule.json`
- The file is stored in the same directory as the application
- You can back up this file to preserve schedules
- The file contains all employee data and shifts

## Support

For issues or questions about the application, please refer to the README.md file or contact the development team.
