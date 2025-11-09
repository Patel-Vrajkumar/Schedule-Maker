# Schedule Maker - Feature Documentation

## Core Features

### 1. Employee Management System

#### Add Employee
- **Purpose**: Add new employees to the scheduling system
- **Inputs**:
  - Employee name (text field)
  - Weekly hour preference (dropdown)
  - Day availability (checkboxes for all 7 days)
  - Shift type preferences (checkboxes for 5 shift types)
- **Validation**:
  - Name must not be empty
  - At least one day must be selected
  - Duplicate names are allowed (for multiple employees with same name)
- **Output**: Employee added to the system and visible in schedule grid

#### Remove Employee
- **Purpose**: Remove employees from the system
- **Process**:
  1. Select employee from list
  2. Confirm removal
  3. Employee and all their shifts are removed
- **Impact**: Schedule grid updates immediately

### 2. Schedule Generation Engine

#### Automatic Assignment Algorithm
- **Method**: Random assignment with constraints
- **Constraints Enforced**:
  1. Employee availability (day-based)
  2. Weekly hour limits (based on preference)
  3. Shift preferences (prioritized but not required)
  4. Store operating hours
  5. Minimum 3-hour shift duration
  6. Break allocation (automatic based on shift length)

#### Shift Distribution
- **Opening Shifts**: ~5.5 hours with 30-minute break
- **Post-Opening**: ~4 hours with 15-minute break
- **Middle Shifts**: ~5 hours with 30-minute break
- **Pre-Closing**: Variable length with 30-minute break
- **Closing Shifts**: ~5 hours with 15-minute break

#### Break Allocation
- Automatically assigned based on shift type and duration
- Ranges from 15 to 30 minutes
- Deducted from total hours calculation

### 3. Schedule Visualization

#### Grid Display
- **Rows**: One per employee
- **Columns**:
  - Employee name
  - 7 days (Wednesday through Tuesday)
  - Total weekly hours

#### Cell Content Types
- **Scheduled**: Shows time range and break duration
- **Available**: "Available" text with green background
- **Off Day**: "OFF" text with gray background

#### Auto-sizing
- Rows automatically expand to show multi-line shift information
- Columns size to fit content
- Scrollable for large numbers of employees

### 4. Data Persistence

#### Save Functionality
- **Format**: JSON
- **File Name**: schedule.json
- **Location**: Application directory
- **Contents**:
  - Week start date
  - All employee profiles (name, availability, preferences)
  - All scheduled shifts
  - Projected sales data

#### Load Functionality
- **Source**: schedule.json file
- **Behavior**: Replaces current schedule completely
- **Error Handling**: Shows message if file not found

### 5. User Interface

#### Color Scheme
- **Header**: Subway green (#007846)
- **Action Buttons**: 
  - Green for employee management
  - Orange for schedule generation
  - Blue for save/load operations
- **Grid**:
  - White background for scheduled cells
  - Light green for available cells
  - Light gray for off days

#### Button Layout
Left to right:
1. Add Employee
2. Remove Employee
3. Generate Schedule
4. Save Schedule
5. Load Schedule

### 6. Store Hours Configuration

#### Pre-configured Hours
- **Sunday**: 8:00 AM - 10:00 PM (14 hours)
- **Monday**: 8:00 AM - 10:00 PM (14 hours)
- **Tuesday**: 8:00 AM - 10:00 PM (14 hours)
- **Wednesday**: 8:00 AM - 10:00 PM (14 hours)
- **Thursday**: 7:30 AM - 10:00 PM (14.5 hours)
- **Friday**: 7:30 AM - 10:00 PM (14.5 hours)
- **Saturday**: 8:00 AM - 10:00 PM (14 hours)

#### Shift Generation Boundaries
- No shifts scheduled before opening time
- No shifts extending beyond closing time
- Shifts automatically constrained to these windows

### 7. Weekly Hour Management

#### Hour Range Options
1. **0-10 hours**: Very part-time, minimal shifts
2. **10-20 hours**: Part-time, 2-3 shifts per week
3. **20-30 hours**: Full-time, 4-5 shifts per week
4. **30-40 hours**: Full-time, 5-6 shifts per week

#### Hour Tracking
- Cumulative across all shifts in the week
- Displayed in "Total Hours" column
- Break time is excluded from totals
- Algorithm stops assigning when max reached

### 8. Shift Type System

#### Opening
- **Time**: Starts at store opening
- **Duration**: ~5.5 hours
- **Break**: 30 minutes
- **Responsibilities**: Store opening procedures

#### Post-Opening (Lunch Rush)
- **Time**: Typically 11:30 AM start
- **Duration**: ~4 hours
- **Break**: 15 minutes
- **Responsibilities**: Lunch rush coverage

#### Middle
- **Time**: Afternoon (2:00 PM start)
- **Duration**: ~5 hours
- **Break**: 30 minutes
- **Responsibilities**: Bridge between rushes

#### Pre-Closing (Supper Rush)
- **Time**: Evening (5:00 PM start)
- **Duration**: Until close
- **Break**: 30 minutes
- **Responsibilities**: Dinner rush coverage

#### Closing
- **Time**: Last ~5 hours before close
- **Duration**: ~5 hours
- **Break**: 15 minutes
- **Responsibilities**: Store closing procedures

## Technical Features

### 1. Data Models

#### Employee Model
```
- Name: string
- Availability: Dictionary<DayOfWeek, bool>
- PreferredShifts: List<ShiftType>
- HourPreference: HourRange
```

#### Shift Model
```
- EmployeeName: string
- Day: DayOfWeek
- StartTime: TimeSpan
- EndTime: TimeSpan
- BreakDuration: TimeSpan?
- Type: ShiftType
- TotalHours: double (calculated)
```

#### Schedule Model
```
- WeekStartDate: DateTime
- Employees: List<Employee>
- Shifts: List<Shift>
- ProjectedSales: Dictionary<DayOfWeek, int>
```

### 2. Week Calculation

#### Week Start Logic
- Week starts on Wednesday
- Current week start calculated based on today's date
- Days to subtract: `(dayOfWeek - Wednesday + 7) % 7`

### 3. Randomization

#### Shuffle Algorithm
- Uses `Random` class with system-generated seed
- Employees shuffled for fair distribution
- Re-shuffle for each shift assignment

#### Preference Matching
- First pass: Try to match shift preferences
- Second pass: Assign to any available employee
- Both passes respect hour limits

### 4. JSON Serialization

#### Configuration
- Pretty-printed (indented) for readability
- All public properties serialized
- Enums serialized as strings
- DateTime values in ISO format

## Limitations and Known Behaviors

### Current Limitations
1. No conflict detection between overlapping shifts
2. No manual shift editing after generation
3. Week always starts on Wednesday (not configurable)
4. Store hours are hardcoded (not editable in UI)
5. Cannot save multiple different schedules (single file)
6. No export to Excel or PDF
7. No print functionality
8. No undo/redo functionality

### Expected Behaviors
1. Regenerating creates completely new schedule
2. Multiple employees can be scheduled same time
3. Some employees may not get shifts if hours are full
4. Randomization means different results each generation
5. Availability without preferences means random shift assignment

## Future Enhancement Possibilities

### Potential Additions
- Multiple week planning
- Schedule conflict detection
- Manual shift editing with drag-and-drop
- Export to various formats (Excel, PDF, CSV)
- Print preview and printing
- Undo/redo system
- Schedule templates
- Employee swap functionality
- Time-off request management
- Configurable store hours
- Multiple location support
- Labor cost calculation
- Schedule comparison tool
- Email schedule distribution
- Mobile app version

### Technical Improvements
- Database integration instead of JSON
- Real-time collaboration
- Cloud backup
- Audit logging
- User authentication
- Permission levels
- Schedule approval workflow
- Integration with payroll systems
