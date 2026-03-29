using MySql.Data.MySqlClient;
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
    public partial class EditProfile : Form
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        private void EditProfile_Load(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = true;

            string[] parts = Class1.LoggedInUser.Split(' ');
            string initials = parts.Length >= 2
                ? $"{parts[0][0]}{parts[parts.Length - 1][0]}"
                : Class1.LoggedInUser.Substring(0, Math.Min(2, Class1.LoggedInUser.Length));

            guna2CircleButton1.Text = initials.ToUpper(); // "JD"

            txtFullName.Text = Class1.LoggedInUser;
            txtNurseID.Text = Class1.LoggedInUserID;
            txtCurrentPassword.Text = Class1.LoggedInUserPasword;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = !txtCurrentPassword.UseSystemPasswordChar;
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = !txtNewPassword.UseSystemPasswordChar;
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string currentPassword = txtCurrentPassword.Text.Trim();

            // Check if fields are empty
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all password fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if new password and confirm password match
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if new password is same as current
            if (newPassword == currentPassword)
            {
                MessageBox.Show("New password must be different from current password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update the database
            string connectionString = "server=localhost;database=triage_system;uid=root;pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE staffaccounts SET PasswordHash = @NewPassword WHERE StaffID = @StaffID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    cmd.Parameters.AddWithValue("@StaffID", Class1.LoggedInUserID);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        Class1.LoggedInUserPasword = newPassword; // update in memory
                        MessageBox.Show("Password updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCurrentPassword.Text = newPassword;
                        txtNewPassword.Clear();
                        txtConfirmPassword.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Update failed. User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
