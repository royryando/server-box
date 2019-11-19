using ServerBox.Lib;
using ServerBox.Model;
using ServerBox.Service;
using System;
using System.Windows.Forms;

namespace ServerBox
{
    public partial class AddForm : Form
    {
        DatabaseHelper db;
        ServerService serverService;
        public AddForm(DatabaseHelper db)
        {
            this.db = db;
            this.serverService = new ServerService(this.db);
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                showMessage("Name is required");
                txtName.Focus();
            }
            else if (txtIP.Text.Equals(""))
            {
                showMessage("Domain/IP is required");
                txtIP.Focus();
            }
            else if (txtUsername.Text.Equals(""))
            {
                showMessage("Username is required");
                txtUsername.Focus();
            }
            else if (txtPort.TextLength < 1)
            {
                showMessage("Port is required");
            }
            else
            {
                // save 
                string type = "";
                if (rdRDP.Checked)
                {
                    type = "RDP";
                }
                else if (rdSSH.Checked)
                {
                    type = "SSH";
                }
                Server server = new Server { Ip = txtIP.Text, Port = int.Parse(txtPort.Text), Name = txtName.Text, Username = txtUsername.Text, Password = txtPassword.Text, Type = type };
                serverService.Save(server);
                MessageBox.Show("Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                txtName.Focus();
            }
        }

        private void rdRDP_CheckedChanged(object sender, EventArgs e)
        {
            onRadioChange();
        }

        private void rdSSH_CheckedChanged(object sender, EventArgs e)
        {
            onRadioChange();
        }

        private void onRadioChange()
        {
            if (rdSSH.Checked)
            {
                txtUsername.Text = "root";
                txtPort.Text = "22";
            }
            else if (rdRDP.Checked)
            {
                txtUsername.Text = "Administrator";
                txtPort.Text = "3389";
            }
        }

        private void Clear()
        {
            txtName.Text = "";
            rdSSH.Checked = true;
            txtIP.Text = "";
            txtUsername.Text = "root";
            txtPassword.Text = "";
        }

        private void showMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void showMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
