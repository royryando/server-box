using System;
using System.Windows.Forms;
using ServerBox.Lib;
using ServerBox.Service;

namespace ServerBox
{
    public partial class SetPasscodeForm : Form
    {
        private DatabaseHelper db;
        private UserService userService;

        public SetPasscodeForm(DatabaseHelper db)
        {
            this.db = db;
            this.userService = new UserService(this.db);
            InitializeComponent();
        }

        private void SetPasscodeForm_Load(object sender, EventArgs e)
        {
            if (!userService.HasPasscode())
            {
                lblRemove.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPasscode.TextLength < 1)
            {
                MessageBox.Show("Passcode can't be blank", "Passcode required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            userService.SetPasscode(txtPasscode.Text);
            MessageBox.Show("Passcode successfully set", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            userService.SetPasscode("");
            MessageBox.Show("Passcode successfully removed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
