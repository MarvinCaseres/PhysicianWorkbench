using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PhysicianWorkbench;

namespace PhysicianWorkbench
{
    public partial class Form1 : Form
    {

        public static UserControl ActiveTriageSession = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void MoveIndicator(Control btn)
        {

            img_slide.Top = btn.Top;
            img_slide.Height = btn.Height;

            ResetShadows();

            if (btn is Guna2Button currentBtn)
            {
                currentBtn.ShadowDecoration.Enabled = true;
            }
        }

        private void ResetShadows()
        {
            dashboardBtn.ShadowDecoration.Enabled = false;
            Consultbtn.ShadowDecoration.Enabled = false;
            PatientRecordbtn.ShadowDecoration.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MoveIndicator(dashboardBtn);
            dashboardBtn.Checked = true;

            Dash uc = new Dash();
            addUserControl(uc);

            string[] parts = Class1.LoggedInUser.Split(' ');
            string initials = parts.Length >= 2
                ? $"{parts[0][0]}{parts[parts.Length - 1][0]}"
                : Class1.LoggedInUser.Substring(0, Math.Min(2, Class1.LoggedInUser.Length));

            guna2CircleButton1.Text = initials.ToUpper(); // "JD"

            namelb.Text = Class1.LoggedInUser;
            staffidlb.Text = Class1.LoggedInUserID;


        }

        private void guna2VSeparator1_Click(object sender, EventArgs e)
        {
            // Unused event
        }

        private void dashboardBtn_Click_1(object sender, EventArgs e)
        {
            MoveIndicator(sender as Control);

            Dash uc = new Dash();
            addUserControl(uc);
        }

        private void Consultbtn_Click_1(object sender, EventArgs e)
        {
            MoveIndicator(sender as Control);

            consult uc = new consult();
            addUserControl(uc);
        }

        private void PatientRecordbtn_Click_1(object sender, EventArgs e)
        {
            MoveIndicator(sender as Control);

            mainPanel.Controls.Clear();

            if (ActiveTriageSession != null)
            {
                ActiveTriageSession.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(ActiveTriageSession);
                ActiveTriageSession.BringToFront();
            }
            else
            {

                Past myQueue = new Past();
                myQueue.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(myQueue);
            }
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Form login = new LoginForm(); // change to your main form
            login.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            EditProfile frm = new EditProfile();
            frm.Show();
        }
    }
}