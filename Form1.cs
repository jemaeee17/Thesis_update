using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Thesis_IncidentMngt
{
    public partial class Create_Account : Form
    {
        bool isPasswordShown = false;
        bool isConfirmPasswordShown = false;
        public Create_Account()
        {
            InitializeComponent();
            this.Resize += Create_Account_Resize;
            create_account_panel.Resize += Create_account_panel_Resize;

            this.ShowPassword_picturebox.Click += new System.EventHandler(this.ShowPassword_picturebox_Click);
            this.ConfirmPass_picturebox.Click += new System.EventHandler(this.ConfirmPass_picturebox_Click);


        }

        private void Create_account_panel_Resize(object sender, EventArgs e)
        {
            label_createacc.Left = (create_account_panel.Width - label_createacc.Width) / 2;
        }

        private void Create_Account_Resize(object sender, EventArgs e)
        {
            create_account_panel.Width = (int)(this.Width * 0.50);
            FirstName_LastName_panel.Width = (int)(this.Width * 0.40);
            Bottompart_createacc_panel.Width = (int)(this.Width * 0.40);
            Username_textbox.Width = (int)(this.Width * 0.40);
            Password_textbox.Width = (int)(this.Width * 0.40);
            ConfirmPassword_textbox.Width = (int)(this.Width * 0.40);
           
        }

        private void Login_createacc_label_Click(object sender, EventArgs e)
        {
            Login loginform = new Login();
            loginform.Show();

            this.Hide();
        }

        private void Create_button_Click(object sender, EventArgs e)
        {
            Dashboard_Main dashboard = new Dashboard_Main();
            dashboard.Show();

            this.Hide();

            //password and confirm password validation
            if(Password_textbox.Text != ConfirmPassword_textbox.Text )
            {
                MessageBox.Show("Passwords do not match!");
                return; // exit if passwords don't match
            }

            string firstname = FirstName_textbox.Text;
            string lastname = LastName_textbox.Text;
            string username = Username_textbox.Text;
            string password = Password_textbox.Text;
            

            string position = "";
            if (DeskOfficer_radiobutton.Checked)
            {
                position = "Desk Officer";
            }
            else if (Admin_radiobutton.Checked)
            {
                position = "Admin";
            }
            else
            {
                MessageBox.Show("Please Select a Position!");
                return;
            }

            string connectionString = "Server=localhost;Database=incidentlog_db;Uid=root;Pwd=;";
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                conn.Open();

                //Hash password before inserting
                string hashedPassword = HashPassword(password);

                //SQL query to insert the user into the user's table
                string query = "INSERT INTO users (firstname, lastname, username, password, position) VALUES(@firstname, @lastname, @username, @password, @position)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                //use parameters to prevent sqli
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@position", position);

                //execute the query
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Account Created Successfully");
                }
                else
                {
                    MessageBox.Show("Error Creating Account. Please try again!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //Password Hashing
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        // To show/hide the password when the eye icon is clicked.
        private void ShowPassword_picturebox_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the password.
            isPasswordShown = !isPasswordShown;

            // Toggle the password visibility by changing 'UseSystemPasswordChar'.
            Password_textbox.UseSystemPasswordChar = !isPasswordShown;

            // Toggle the eye icon based on the visibility of the password.
            ShowPassword_picturebox.Image = isPasswordShown
                ? Properties.Resources.show  // Open eye icon when password is visible.
                : Properties.Resources.hide; // Eye with slash icon when password is hidden.
        }

        // To show/hide the confirm password when the eye icon is clicked.
        private void ConfirmPass_picturebox_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of the confirm password.
            isConfirmPasswordShown = !isConfirmPasswordShown;

            // Toggle the password visibility by changing 'UseSystemPasswordChar'.
            ConfirmPassword_textbox.UseSystemPasswordChar = !isConfirmPasswordShown;

            // Toggle the eye icon based on the visibility of the confirm password.
            ConfirmPass_picturebox.Image = isConfirmPasswordShown
                ? Properties.Resources.show  // Open eye icon when confirm password is visible.
                : Properties.Resources.hide; // Eye with slash icon when confirm password is hidden.
        }

    }
}
