using ScheduleMaker.Models;

namespace ScheduleMaker.Forms
{
    public partial class MainForm : Form
    {
        private Schedule _currentSchedule;
        private ScheduleGenerator _generator;
        private const string ScheduleFileName = "schedule.json";

        public MainForm()
        {
            InitializeComponent();
            _currentSchedule = new Schedule();
            _generator = new ScheduleGenerator();
            _currentSchedule.WeekStartDate = ScheduleGenerator.GetCurrentWeekStart();
            UpdateScheduleDisplay();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 700);
            this.Name = "MainForm";
            this.Text = "Schedule Maker - Subway";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create header panel
            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(0, 120, 70) // Subway green
            };

            var titleLabel = new Label
            {
                Text = "SUBWAY SCHEDULE MAKER",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);

            // Create button panel
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10)
            };

            int buttonX = 10;
            var btnAddEmployee = new Button
            {
                Text = "Add Employee",
                Location = new Point(buttonX, 10),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(0, 120, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAddEmployee.Click += BtnAddEmployee_Click;
            buttonPanel.Controls.Add(btnAddEmployee);
            buttonX += 130;

            var btnRemoveEmployee = new Button
            {
                Text = "Remove Employee",
                Location = new Point(buttonX, 10),
                Size = new Size(140, 40),
                BackColor = Color.FromArgb(0, 120, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemoveEmployee.Click += BtnRemoveEmployee_Click;
            buttonPanel.Controls.Add(btnRemoveEmployee);
            buttonX += 150;

            var btnGenerateSchedule = new Button
            {
                Text = "Generate Schedule",
                Location = new Point(buttonX, 10),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(200, 100, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerateSchedule.Click += BtnGenerateSchedule_Click;
            buttonPanel.Controls.Add(btnGenerateSchedule);
            buttonX += 160;

            var btnSaveSchedule = new Button
            {
                Text = "Save Schedule",
                Location = new Point(buttonX, 10),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(50, 50, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveSchedule.Click += BtnSaveSchedule_Click;
            buttonPanel.Controls.Add(btnSaveSchedule);
            buttonX += 130;

            var btnLoadSchedule = new Button
            {
                Text = "Load Schedule",
                Location = new Point(buttonX, 10),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(50, 50, 150),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLoadSchedule.Click += BtnLoadSchedule_Click;
            buttonPanel.Controls.Add(btnLoadSchedule);

            this.Controls.Add(buttonPanel);

            // Create schedule display area
            var schedulePanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            _scheduleDataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };

            schedulePanel.Controls.Add(_scheduleDataGridView);
            this.Controls.Add(schedulePanel);

            this.ResumeLayout(false);
        }

        private DataGridView _scheduleDataGridView = null!;

        private void BtnAddEmployee_Click(object? sender, EventArgs e)
        {
            var addEmployeeForm = new AddEmployeeForm();
            if (addEmployeeForm.ShowDialog() == DialogResult.OK)
            {
                if (addEmployeeForm.Employee != null)
                {
                    _currentSchedule.Employees.Add(addEmployeeForm.Employee);
                    UpdateScheduleDisplay();
                    MessageBox.Show($"Employee '{addEmployeeForm.Employee.Name}' added successfully!", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnRemoveEmployee_Click(object? sender, EventArgs e)
        {
            if (_currentSchedule.Employees.Count == 0)
            {
                MessageBox.Show("No employees to remove.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var removeForm = new RemoveEmployeeForm(_currentSchedule.Employees);
            if (removeForm.ShowDialog() == DialogResult.OK && removeForm.SelectedEmployee != null)
            {
                _currentSchedule.Employees.Remove(removeForm.SelectedEmployee);
                _currentSchedule.Shifts.RemoveAll(s => s.EmployeeName == removeForm.SelectedEmployee.Name);
                UpdateScheduleDisplay();
                MessageBox.Show($"Employee '{removeForm.SelectedEmployee.Name}' removed successfully!", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnGenerateSchedule_Click(object? sender, EventArgs e)
        {
            if (_currentSchedule.Employees.Count == 0)
            {
                MessageBox.Show("Please add employees before generating a schedule.", 
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _generator.GenerateSchedule(_currentSchedule);
            UpdateScheduleDisplay();
            MessageBox.Show("Schedule generated successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSaveSchedule_Click(object? sender, EventArgs e)
        {
            try
            {
                _currentSchedule.SaveToFile(ScheduleFileName);
                MessageBox.Show("Schedule saved successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving schedule: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoadSchedule_Click(object? sender, EventArgs e)
        {
            try
            {
                var loadedSchedule = Schedule.LoadFromFile(ScheduleFileName);
                if (loadedSchedule != null)
                {
                    _currentSchedule = loadedSchedule;
                    UpdateScheduleDisplay();
                    MessageBox.Show("Schedule loaded successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No saved schedule found.", "Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading schedule: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateScheduleDisplay()
        {
            _scheduleDataGridView.Columns.Clear();
            _scheduleDataGridView.Rows.Clear();

            // Add employee column
            _scheduleDataGridView.Columns.Add("Employee", "Employee");

            // Add day columns (Wednesday to Tuesday)
            var weekStart = _currentSchedule.WeekStartDate;
            var days = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, 
                              DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday };

            for (int i = 0; i < 7; i++)
            {
                var date = weekStart.AddDays(i);
                var dayName = date.ToString("ddd M/d");
                _scheduleDataGridView.Columns.Add($"Day{i}", dayName);
            }

            // Add total hours column
            _scheduleDataGridView.Columns.Add("TotalHours", "Total Hours");

            // Add rows for each employee
            foreach (var employee in _currentSchedule.Employees)
            {
                var rowIndex = _scheduleDataGridView.Rows.Add();
                var row = _scheduleDataGridView.Rows[rowIndex];
                row.Cells[0].Value = employee.Name;

                // Fill in shifts for each day
                for (int i = 0; i < 7; i++)
                {
                    var date = weekStart.AddDays(i);
                    var dayShifts = _currentSchedule.Shifts
                        .Where(s => s.EmployeeName == employee.Name && s.Day == date.DayOfWeek)
                        .ToList();

                    if (dayShifts.Any())
                    {
                        row.Cells[i + 1].Value = string.Join("\n", dayShifts.Select(s => s.ToString()));
                    }
                    else if (employee.Availability[date.DayOfWeek])
                    {
                        row.Cells[i + 1].Value = "Available";
                        row.Cells[i + 1].Style.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        row.Cells[i + 1].Value = "OFF";
                        row.Cells[i + 1].Style.BackColor = Color.LightGray;
                    }
                }

                // Calculate total hours
                var totalHours = _currentSchedule.GetEmployeeWeeklyHours(employee.Name);
                row.Cells[8].Value = totalHours.ToString("F1");
            }

            // Auto-size rows for better visibility
            _scheduleDataGridView.AutoResizeRows();
        }
    }
}
