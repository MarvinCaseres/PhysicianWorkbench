using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ShowControl(UserControl control)
        {
            panel3.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel3.Controls.Add(control);
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dashButton.FillColor = ColorTranslator.FromHtml("#63b4f5");
            Pastbtn.FillColor = Color.Transparent;
            guna2Button3.FillColor = Color.Transparent;
            ShowControl(new Dash());

        }
        private void Pastbtn_Click(object sender, EventArgs e)
        {
            Pastbtn.FillColor = ColorTranslator.FromHtml("#63b4f5");
            dashButton.FillColor = Color.Transparent;
            guna2Button3.FillColor = Color.Transparent;
            ShowControl(new Past());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dashButton.FillColor = ColorTranslator.FromHtml("#63b4f5");
            ShowControl(new Dash());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Button3.FillColor = ColorTranslator.FromHtml("#63b4f5");
            dashButton.FillColor = Color.Transparent;
            Pastbtn.FillColor = Color.Transparent;
            ShowControl(new consult());
        }
    }
}
