using ScheduleMaker.Models;

namespace ScheduleMaker.Forms
{
    public partial class AddEmployeeForm : Form
    {
        private TextBox _txtName = null!;
        private ComboBox _cmbHourRange = null!;
        private Dictionary<DayOfWeek, CheckBox> _availabilityCheckboxes = new();
        private Dictionary<ShiftType, CheckBox> _shiftTypeCheckboxes = new();

        public Employee? Employee { get; private set; }

        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(500, 650);
            this.Text = "Add Employee";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int yPos = 20;

            // Name
            var lblName = new Label
            {
                Text = "Employee Name:",
                Location = new Point(20, yPos),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblName);

            _txtName = new TextBox
            {
                Location = new Point(180, yPos),
                Size = new Size(280, 25)
            };
            this.Controls.Add(_txtName);
            yPos += 40;

            // Hour Range
            var lblHourRange = new Label
            {
                Text = "Weekly Hours:",
                Location = new Point(20, yPos),
                Size = new Size(150, 25)
            };
            this.Controls.Add(lblHourRange);

            _cmbHourRange = new ComboBox
            {
                Location = new Point(180, yPos),
                Size = new Size(280, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbHourRange.Items.AddRange(new object[] 
            { 
                "0-10 hours", 
                "10-20 hours", 
                "20-30 hours", 
                "30-40 hours" 
            });
            _cmbHourRange.SelectedIndex = 0;
            this.Controls.Add(_cmbHourRange);
            yPos += 40;

            // Availability
            var lblAvailability = new Label
            {
                Text = "Availability:",
                Location = new Point(20, yPos),
                Size = new Size(150, 25),
                Font = new Font(this.Font, FontStyle.Bold)
            };
            this.Controls.Add(lblAvailability);
            yPos += 30;

            var days = new[] { DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, 
                              DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday };
            
            foreach (var day in days)
            {
                var chk = new CheckBox
                {
                    Text = day.ToString(),
                    Location = new Point(40, yPos),
                    Size = new Size(200, 25)
                };
                _availabilityCheckboxes[day] = chk;
                this.Controls.Add(chk);
                yPos += 30;
            }

            yPos += 10;

            // Shift Preferences
            var lblShiftPrefs = new Label
            {
                Text = "Shift Preferences:",
                Location = new Point(20, yPos),
                Size = new Size(150, 25),
                Font = new Font(this.Font, FontStyle.Bold)
            };
            this.Controls.Add(lblShiftPrefs);
            yPos += 30;

            var shiftTypes = new[] 
            { 
                (ShiftType.Opening, "Opening"),
                (ShiftType.PostOpening, "Post-Opening (Lunch Rush)"),
                (ShiftType.Middle, "Middle Shift"),
                (ShiftType.PreClosing, "Pre-Closing (Supper Rush)"),
                (ShiftType.Closing, "Closing")
            };

            foreach (var (type, name) in shiftTypes)
            {
                var chk = new CheckBox
                {
                    Text = name,
                    Location = new Point(40, yPos),
                    Size = new Size(250, 25)
                };
                _shiftTypeCheckboxes[type] = chk;
                this.Controls.Add(chk);
                yPos += 30;
            }

            yPos += 10;

            // Buttons
            var btnOK = new Button
            {
                Text = "Add",
                DialogResult = DialogResult.OK,
                Location = new Point(280, yPos),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(0, 120, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOK.Click += BtnOK_Click;
            this.Controls.Add(btnOK);

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(380, yPos),
                Size = new Size(90, 35)
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            this.ResumeLayout(false);
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("Please enter an employee name.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            var employee = new Employee
            {
                Name = _txtName.Text.Trim()
            };

            // Set availability
            foreach (var kvp in _availabilityCheckboxes)
            {
                employee.Availability[kvp.Key] = kvp.Value.Checked;
            }

            // Set shift preferences
            foreach (var kvp in _shiftTypeCheckboxes)
            {
                if (kvp.Value.Checked)
                {
                    employee.PreferredShifts.Add(kvp.Key);
                }
            }

            // Set hour range
            employee.HourPreference = _cmbHourRange.SelectedIndex switch
            {
                0 => HourRange.ZeroToTen,
                1 => HourRange.TenToTwenty,
                2 => HourRange.TwentyToThirty,
                3 => HourRange.ThirtyToForty,
                _ => HourRange.ZeroToTen
            };

            // Validate that at least one day is selected
            if (!employee.Availability.Values.Any(v => v))
            {
                MessageBox.Show("Please select at least one available day.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            Employee = employee;
        }
    }
}
