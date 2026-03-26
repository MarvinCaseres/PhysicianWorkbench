using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace PhysicianWorkbench
{
    public partial class consult : UserControl
    {
        string connStr = "server=localhost;user id=root;password=;database=triage_system;";
        public consult()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT patient_id, fullname, assessed_at, priority_level FROM" +
                    " triage_records ORDER BY CASE " +
                    "WHEN priority_level = 'Emergency' THEN 1 " +
                    "WHEN priority_level = 'Priority' THEN 2 " +
                    "WHEN priority_level = 'Regular' " +
                    "THEN 3 ELSE 4 END";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2DataGridView1.DataSource = dt;
            }
        }

        private void consult_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void consultbtn_Click(object sender, EventArgs e)
        {
            consultation form = new consultation();
            form.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
