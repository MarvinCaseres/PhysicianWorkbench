using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PhysicianWorkbench
{
    public partial class consult : UserControl
    {
        string connectionString = "server=localhost;database=triage_system;uid=root;pwd=;";

        private PrintDocument printDocument = new PrintDocument();
        private int currentPage = 1;

        public consult()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintPage;
        }
        private void consult_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
                try
                {
                    conn.Open();

                    // Get latest inserted record
                    string query = "SELECT * FROM patient_queue ORDER BY triage_id DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        lblName.Text = reader["fullname"].ToString();
                        lblPatientID.Text = reader["patient_id"].ToString();
                        lblSex.Text = reader["sex"].ToString();
                        lblAge.Text = reader["age"].ToString();

                        BloodPressuretb.Text = reader["blood_pressure"].ToString();
                        HeartRatetb.Text = reader["heart_rate"].ToString();
                        Oxygentb.Text = reader["oxygen_level"].ToString();
                        Temptb.Text = reader["temperature"].ToString();
                        Weighttb.Text = reader["weight"].ToString();
                        Heighttb.Text = reader["height"].ToString();
                        ChiefComptb.Text = reader["chief_complaint"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {

            string query = "DELETE * FROM patient_queue ORDER BY triage_id DESC LIMIT 1";

            currentPage = 1;

            printDocument.DefaultPageSettings.PaperSize =
                new PaperSize("A4", 827, 1169);

            printDocument.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
{
    if (currentPage == 1)
    {
        DrawConsultationPage(e);

        printDocument.DefaultPageSettings.PaperSize =
            new PaperSize("Prescription", 400, 600);

        currentPage++;
        e.HasMorePages = true;
    }
    else if (currentPage == 2)
    {
        DrawPrescriptionPage(e);

        printDocument.DefaultPageSettings.PaperSize =
            new PaperSize("Billing", 400, 500);

        currentPage++;
        e.HasMorePages = true;
    }
    else
    {
        DrawBillingPage(e);

        e.HasMorePages = false;
        currentPage = 1;

        printDocument.DefaultPageSettings.PaperSize =
            new PaperSize("A4", 827, 1169);
    }
}

        private void DrawConsultationPage(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font headerFont = new Font("Segoe UI", 16, FontStyle.Bold);
            Font subHeader = new Font("Segoe UI", 9);
            Font sectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 10);

            float left = 50;
            float y = 30;
            float width = e.MarginBounds.Width;

            Brush accent = new SolidBrush(Color.FromArgb(0, 102, 153));

            try
            {
                Image logo = Properties.Resources.logo;
                g.DrawImage(logo, left, y, 50, 50);
            }
            catch { }

            g.DrawString("NORTH METRO GENERAL HOSPITAL", headerFont, Brushes.Black, left + 65, y);
            g.DrawString("123 Medical Street | 0912-345-6789", subHeader, Brushes.Gray, left + 65, y + 25);

            g.DrawString("Date: " + DateTime.Now.ToString("MMM dd, yyyy"),
                subHeader, Brushes.Black, left + width - 180, y);

            y += 60;
            g.FillRectangle(accent, left, y, width, 3);
            y += 15;

            g.DrawString("PATIENT INFORMATION", sectionFont, Brushes.Black, left, y);
            y += 25;

            g.DrawString("Name: " + lblName.Text, bodyFont, Brushes.Black, left, y);
            g.DrawString("ID: " + lblPatientID.Text, bodyFont, Brushes.Black, left + 300, y);

            y += 25;
            g.DrawString("Sex: " + lblSex.Text, bodyFont, Brushes.Black, left, y);
            g.DrawString("Age: " + lblAge.Text, bodyFont, Brushes.Black, left + 150, y);

            y += 40;

            g.DrawString("VITAL SIGNS", sectionFont, Brushes.Black, left, y);
            y += 25;

            string[] vitals =
            {
                "BP: " + BloodPressuretb.Text,
                "HR: " + HeartRatetb.Text,
                "SpO2: " + Oxygentb.Text,
                "Temp: " + Temptb.Text,
                "Wt: " + Weighttb.Text,
                "Ht: " + Heighttb.Text
            };

            for (int i = 0; i < vitals.Length; i++)
            {
                g.DrawString(vitals[i], bodyFont, Brushes.Black,
                    left + (i % 3) * 180,
                    y + (i / 3) * 25);
            }

            y += 70;

            g.DrawString("CHIEF COMPLAINT", sectionFont, Brushes.Black, left, y);
            y += 25;

            g.DrawString(ChiefComptb.Text, bodyFont, Brushes.Black,
                new RectangleF(left, y, width, 60));

            y += 70;

            g.DrawString("DOCTOR'S NOTE", sectionFont, Brushes.Black, left, y);
            y += 25;

            g.DrawString(Notetb.Text, bodyFont, Brushes.Black,
                new RectangleF(left, y, width, 60));

            y += 70;

            g.DrawString("PRESCRIPTION", sectionFont, Brushes.Black, left, y);
            y += 25;

            g.DrawString(FormatPrescription(Prescriptiontb.Text),
                bodyFont, Brushes.Black,
                new RectangleF(left, y, width, 80));

            y += 100;

            g.DrawString("Physician Signature:", bodyFont, Brushes.Black, left, y);
            g.DrawLine(Pens.Black, left + 150, y + 12, left + 350, y + 12);
        }

        private void DrawBillingPage(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 9);
            Font totalFont = new Font("Segoe UI", 11, FontStyle.Bold);

            float left = 20;
            float y = 20;
            float width = e.MarginBounds.Width;

            // LOGO
            try
            {
                Image logo = Properties.Resources.logo;
                g.DrawImage(logo, left, y, 30, 30);
            }
            catch { }

            // HEADER
            g.DrawString("NORTH METRO GENERAL HOSPITAL", headerFont, Brushes.Black, left + 35, y);
            y += 30;

            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 15;

            // PATIENT INFO
            g.DrawString("Patient: " + lblName.Text, bodyFont, Brushes.Black, left, y);
            y += 15;

            g.DrawString("Date: " + DateTime.Now.ToString("MM/dd/yyyy"), bodyFont, Brushes.Black, left, y);
            y += 25;

            // BILLING TITLE
            g.DrawString("BILLING STATEMENT", headerFont, Brushes.Black, left, y);
            y += 25;

            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 15;

            // TABLE HEADER
            g.DrawString("Description", bodyFont, Brushes.Black, left, y);
            g.DrawString("Amount", bodyFont, Brushes.Black, left + width - 80, y);
            y += 15;

            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 10;

            // ITEM
            g.DrawString("Consultation Fee", bodyFont, Brushes.Black, left, y);
            g.DrawString("₱ 600.00", bodyFont, Brushes.Black, left + width - 80, y);
            y += 25;

            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 15;

            // TOTAL
            g.DrawString("TOTAL:", totalFont, Brushes.Black, left, y);
            g.DrawString("₱ 600.00", totalFont, Brushes.Black, left + width - 80, y);

            y += 40;

            // SIGNATURE
            g.DrawString("Cashier:", bodyFont, Brushes.Black, left, y);
            g.DrawLine(Pens.Black, left + 60, y + 10, left + 180, y + 10);
        }
        private void DrawPrescriptionPage(PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font bodyFont = new Font("Segoe UI", 8);
            Font rxFont = new Font("Segoe UI", 22, FontStyle.Bold);

            float left = 20;
            float y = 20;
            float width = e.MarginBounds.Width;

            try
            {
                Image logo = Properties.Resources.logo;
                g.DrawImage(logo, left, y, 30, 30);
            }
            catch { }

            g.DrawString("NORTH METRO GENERAL HOSPITAL", headerFont, Brushes.Black, left + 35, y);
            y += 30;

            g.DrawLine(Pens.Black, left, y, left + width, y);
            y += 10;

            g.DrawString("Patient: " + lblName.Text, bodyFont, Brushes.Black, left, y);
            y += 15;

            g.DrawString("Age: " + lblAge.Text, bodyFont, Brushes.Black, left, y);
            y += 15;

            g.DrawString("Date: " + DateTime.Now.ToString("MM/dd/yyyy"), bodyFont, Brushes.Black, left, y);

            y += 25;

            g.DrawString("℞", rxFont, Brushes.Black, left, y);
            y += 30;

            g.DrawString(FormatPrescription(Prescriptiontb.Text),
                bodyFont, Brushes.Black,
                new RectangleF(left + 10, y, width - 20, 180));

            y += 160;

            g.DrawString("Signature:", bodyFont, Brushes.Black, left, y);
            g.DrawLine(Pens.Black, left + 70, y + 10, left + 180, y + 10);
        }

        private string FormatPrescription(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return "";

            string[] lines = raw.Split(new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            string formatted = "";

            foreach (var line in lines)
            {
                formatted += "• " + line + Environment.NewLine + Environment.NewLine;
            }

            return formatted;
        }
    }
}