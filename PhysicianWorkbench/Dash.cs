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
        string connectionString = "server=localhost;database=triage_system;uid=root;pwd=;";
        public Dash()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            LoadGenderChart();
            LoadChart();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }
        private void LoadGenderChart()
        {
            int maleCount = 0;
            int femaleCount = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT gender, COUNT(*) as count FROM patient_registration GROUP BY gender";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string gender = reader["gender"].ToString().Trim();
                        int count = Convert.ToInt32(reader["count"]);

                        if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                            maleCount = count;
                        else if (gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                            femaleCount = count;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            // Clear existing data
            gunaPieDataset1.DataPoints.Clear();

            // ✅ Correct way (NO DataPoint class)
            gunaPieDataset1.DataPoints.Add("Male", maleCount);
            gunaPieDataset1.DataPoints.Add("Female", femaleCount);

            // Refresh chart
            gunaChart1.Update();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongDateString();
            Timelbl.Text = DateTime.Now.ToLongTimeString();
        }
        public static int CalculateAge(DateTime birthdate)
        {
            int age = DateTime.Now.Year - birthdate.Year;

            // If birthday hasn't occurred yet this year, subtract 1
            if (DateTime.Now < birthdate.AddYears(age))
            {
                age--;
            }

            return age;
        }
        private void LoadChart()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM patient_registration WHERE status= 'Waiting'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                label1.Text = result.ToString();

                query = "SELECT COUNT(*) FROM patient_registration WHERE priority_level= 'High Risk'";
                cmd = new MySqlCommand(query, conn);
                result = cmd.ExecuteScalar();
                label4.Text = result.ToString();

                chart1.Series.Clear();
                chart1.Titles.Clear();

                int age1_17 = 0;
                int age18_59 = 0;
                int age60_plus = 0;

                {
                    conn.Open();

                    query = "SELECT birthdate FROM patient_registration";
                    cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // 🔹 Get birthdate from DB
                        DateTime birthdate = Convert.ToDateTime(reader["birthdate"]);

                        // 🔹 Calculate age using your function
                        int age = CalculateAge(birthdate);

                        // 🔹 Categorize
                        if (age >= 1 && age <= 17)
                            age1_17++;
                        else if (age >= 18 && age <= 59)
                            age18_59++;
                        else if (age >= 60)
                            age60_plus++;
                    }
                }

                Series series = new Series("Patients");
                series.ChartType = SeriesChartType.Column;
                series.IsValueShownAsLabel = true;

                series.Points.AddXY("1-17", age1_17);
                series.Points.AddXY("18-59", age18_59);
                series.Points.AddXY("60+", age60_plus);

                chart1.Series.Add(series);
                chart1.Titles.Add("Patient Age Distribution");
            }
        }
        private void LoadEmergencyCount()
        {
            
        }
        private void Dash_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
