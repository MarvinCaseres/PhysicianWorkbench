using Org.BouncyCastle.Asn1.Ocsp;
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
    public partial class Order : Form
    {
        public Order(string id, string name, string gender, string age,
             string bloodpressure, string heartrate, string oxygen,
             string temp, string weight, string height,
             string chiefcomplaint, string prescription, string docNotes)
        {
            InitializeComponent();

            lblPatientID.Text = id;
            lblName.Text = name;
            lblSex.Text = gender;
            lblAge.Text = age;

            BloodPressuretb.Text = bloodpressure;
            guna2TextBox4.Text = heartrate;
            guna2TextBox6.Text = oxygen;
            guna2TextBox3.Text = temp;
            guna2TextBox5.Text = weight;
            guna2TextBox7.Text = height;

            guna2TextBox1.Text = chiefcomplaint;
            ChiefComptb.Text = prescription;
            guna2TextBox2.Text = docNotes;
        }

        private void Order_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
