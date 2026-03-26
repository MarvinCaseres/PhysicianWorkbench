using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace PhysicianWorkbench
{
    public partial class Dash : UserControl
    {
        string connStr = "server=localhost;user id=root;password=;database=triage_system;";

        public Dash()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            chart1.Series.Clear();

            Series series = new Series("Monthly Visits");
            series.ChartType = SeriesChartType.Column;

            series.Points.AddXY("Jan", 50);
            series.Points.AddXY("Feb", 70);
            series.Points.AddXY("Mar", 40);
            series.Points.AddXY("Apr", 90);

            chart1.Series.Add(series);
        }
        private void LoadPatientCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT COUNT(patient_id) FROM triage_records";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int total = Convert.ToInt32(cmd.ExecuteScalar());
                    guna2HtmlLabel4.Text = total.ToString();
                }
            }
        }
        private void LoadEmergencyCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM triage_records WHERE priority_level = 'Emergency'";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int total = Convert.ToInt32(cmd.ExecuteScalar());
                    guna2HtmlLabel5.Text = total.ToString();
                }
            }
        }
        private void Dash_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadPatientCount();
            LoadEmergencyCount();
        }
    }
    
}
