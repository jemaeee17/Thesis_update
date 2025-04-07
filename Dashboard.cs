using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thesis_IncidentMngt
{
    public partial class Dashboard_Main : Form
    {
        public Dashboard_Main()
        {
            InitializeComponent();
        }

        private void LoadForm(Form form)
        {
            panelContainer.Controls.Clear();
            form.TopLevel = false;
            form.AutoScaleMode = AutoScaleMode.None;

            //set a fixed size for the form
            form.Size = new Size(2001, 724);

            //Center the form inside the panel container
            form.Location = new Point(
                (panelContainer.Width - form.Width) / 2,
                (panelContainer.Height - form.Height) / 2
            );

            //Ensures that the form stays centered when panel resizes
            panelContainer.Resize += (s, e) =>
            {
                form.Location = new Point(
                    (panelContainer.Width - form.Width) / 2,
                    (panelContainer.Height - form.Height) / 2
                );
            };

            panelContainer.Controls.Add(form);
            form.Show();
        }
        private void Dashboard_button_Click(object sender, EventArgs e)
        {
            LoadForm(new Form_Dashboard());
        }

        private void Incidentmngt_button_Click(object sender, EventArgs e)
        {
            LoadForm(new IncidentManagement());
        }

        private void Usermngt_Click(object sender, EventArgs e)
        {
            LoadForm(new UserManagement());
        }

        private void EditandVerification_button_Click(object sender, EventArgs e)
        {
            LoadForm(new EditandVerification());
        }

        private void ReportandPrinting_button_Click(object sender, EventArgs e)
        {
            LoadForm(new ReportandPrinting());
        }

        private void LogTrail_button_Click(object sender, EventArgs e)
        {
            LoadForm(new LogTrail());
        }

        private void DataBackup_button_Click(object sender, EventArgs e)
        {
            LoadForm(new DataBackup());
        }

        private void TrainingandSupport_button_Click(object sender, EventArgs e)
        {
            LoadForm(new TrainingandSupport());
        }

        private void Settings_button_Click(object sender, EventArgs e)
        {
            LoadForm(new Settings());
        }

        private void Dashboard_LogOut_button_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Hide();
        }
    }
}
