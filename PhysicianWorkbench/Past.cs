using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace PhysicianWorkbench
{
    public partial class Past : UserControl
    {
        string connectionString = "server=localhost;database=triage_system;uid=root;pwd=;";

        // 🔥 Make DataTable global
        DataTable dt = new DataTable();

        public Past()
        {
            InitializeComponent();
        }

        private void Past_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                    SELECT 
                    patientid AS 'Patient ID',
                    fullname AS 'Full Name',
                    sex AS 'Gender',
                    birthofdate AS 'Birthdate'
                    FROM patient_record";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    dt.Clear();
                    adapter.Fill(dt);

                    guna2DataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = guna2TextBox1.Text.Trim().Replace("'", "''");

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                dt.DefaultView.RowFilter =
                    "[Patient ID] LIKE '%" + searchValue + "%' OR " +
                    "[Full Name] LIKE '%" + searchValue + "%' OR " +
                    "[Gender] LIKE '%" + searchValue + "%' OR " +
                    "Convert([Birthdate], 'System.String') LIKE '%" + searchValue + "%'";
            }
        }

        private void Viewbtn_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            string id = guna2DataGridView1.CurrentRow.Cells["Patient ID"].Value.ToString();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM patient_record WHERE patientid = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Pass FULL reader to new form
                        Order vp = new Order(
                            reader["patientid"].ToString(),
                            reader["fullname"].ToString(),
                            reader["sex"].ToString(),
                            reader["age"].ToString(),
                            reader["bloodpressure"].ToString(),
                            reader["heartrate"].ToString(),
                            reader["oxygen"].ToString(),
                            reader["temp"].ToString(),
                            reader["weight"].ToString(),
                            reader["height"].ToString(),
                            reader["chiefcomplaint"].ToString(),
                            reader["prescription"].ToString(),
                            reader["docNotes"].ToString()

                        );

                        vp.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}