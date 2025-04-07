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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Resize += Login_Resize;
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            if(login_panel != null)
            {
                login_panel.Width = (int)(this.Width * 0.50);
                login_panel.Left = this.Width - login_panel.Width - 25;
            }
        }

        private void Login_Createacc_label_Click(object sender, EventArgs e)
        {
            Create_Account create_Account = new Create_Account();
            create_Account.Show();

            this.Hide();
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            Dashboard_Main dashboard = new Dashboard_Main();
            dashboard.Show();

            this.Hide();
        }
    }
}
