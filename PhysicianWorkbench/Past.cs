using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicianWorkbench
{
    public partial class Past : UserControl
    {
        public Past()
        {
            InitializeComponent();
        }
        private void LoadDummyData()
        {
            // 1. Prevent auto-generation
            guna2DataGridView1.AutoGenerateColumns = false;

            // 2. Define Columns (if not in designer)
            if (guna2DataGridView1.Columns.Count == 0)
            {
                guna2DataGridView1.Columns.Add("PatientID", "Patient ID");
                guna2DataGridView1.Columns.Add("Name", "Full Name");
                guna2DataGridView1.Columns.Add("DateOfBirth", "Date Of Birth");
                guna2DataGridView1.Columns.Add("ContactNo", "Contact No.");
                guna2DataGridView1.Columns.Add("LastVisit", "Last Visit");
                guna2DataGridView1.Columns.Add("Status", "Status");
            }

            // 3. Clear old rows (optional safety)
            guna2DataGridView1.Rows.Clear();

            // 4. Add Rows
            guna2DataGridView1.Rows.Add("C-030", "Juan dela Cruz", "11/12/13", "09123456789", "11/12/13", "Completed");
            guna2DataGridView1.Rows.Add("D-015", "Maria Santos", "11/12/13", "09123456789", "11/12/13", "Completed");
            guna2DataGridView1.Rows.Add("C-012", "Pedro Penduko", "11/12/13", "09123456789", "11/12/13", "Completed");
            guna2DataGridView1.Rows.Add("C-036", "Ana Reyes", "11/12/13", "09123456789", "11/12/13", "Completed");
            guna2DataGridView1.Rows.Add("C-023", "Jose Mario", "11/12/13", "09123456789", "11/12/13", "Completed");
        }

        private void Past_Load(object sender, EventArgs e)
        {
            LoadDummyData();
        }
    }

}
