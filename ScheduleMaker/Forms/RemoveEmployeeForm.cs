using ScheduleMaker.Models;

namespace ScheduleMaker.Forms
{
    public partial class RemoveEmployeeForm : Form
    {
        private ListBox _lstEmployees = null!;
        private List<Employee> _employees;

        public Employee? SelectedEmployee { get; private set; }

        public RemoveEmployeeForm(List<Employee> employees)
        {
            _employees = employees;
            InitializeComponent();
            LoadEmployees();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(400, 400);
            this.Text = "Remove Employee";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblTitle = new Label
            {
                Text = "Select an employee to remove:",
                Location = new Point(20, 20),
                Size = new Size(350, 25)
            };
            this.Controls.Add(lblTitle);

            _lstEmployees = new ListBox
            {
                Location = new Point(20, 50),
                Size = new Size(350, 280)
            };
            this.Controls.Add(_lstEmployees);

            var btnRemove = new Button
            {
                Text = "Remove",
                DialogResult = DialogResult.OK,
                Location = new Point(190, 345),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(180, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRemove.Click += BtnRemove_Click;
            this.Controls.Add(btnRemove);

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new Point(290, 345),
                Size = new Size(90, 35)
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnRemove;
            this.CancelButton = btnCancel;

            this.ResumeLayout(false);
        }

        private void LoadEmployees()
        {
            _lstEmployees.Items.Clear();
            foreach (var emp in _employees)
            {
                _lstEmployees.Items.Add(emp.Name);
            }

            if (_lstEmployees.Items.Count > 0)
            {
                _lstEmployees.SelectedIndex = 0;
            }
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (_lstEmployees.SelectedIndex >= 0)
            {
                var selectedName = _lstEmployees.SelectedItem?.ToString();
                SelectedEmployee = _employees.FirstOrDefault(e => e.Name == selectedName);

                if (SelectedEmployee != null)
                {
                    var result = MessageBox.Show(
                        $"Are you sure you want to remove '{SelectedEmployee.Name}'?",
                        "Confirm Removal",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.None;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an employee to remove.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
