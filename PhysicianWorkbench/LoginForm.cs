using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicianWorkbench
{
    public partial class LoginForm : Form
    {
        string connectionString = "server=localhost;database=triage_system;uid=root;pwd=;";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = Usernametb.Text.Trim();
            string password = Passwordtb.Text;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
                try
                {
                    conn.Open();

                    string query = "SELECT StaffID, FullName, PasswordHash FROM staffaccounts WHERE Username=@user AND PasswordHash=@pass";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", Usernametb.Text);
                    cmd.Parameters.AddWithValue("@pass", Passwordtb.Text);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // 🔹 SAVE BOTH VALUES
                        Class1.LoggedInUserID = reader["StaffID"].ToString();
                        Class1.LoggedInUser = reader["FullName"].ToString();
                        Class1.LoggedInUserPasword = reader["PasswordHash"].ToString();

                        // 🔹 OPEN MAIN FORM
                        Form1 frm = new Form1();
                        frm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (showpasscb.Checked)
            {
                Passwordtb.UseSystemPasswordChar = false;
            }
            else
            {
                Passwordtb.UseSystemPasswordChar = true;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Passwordtb.UseSystemPasswordChar = true;
        }
    }
}
