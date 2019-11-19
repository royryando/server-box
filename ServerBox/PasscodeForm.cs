using ServerBox.Lib;
using ServerBox.Service;
using System;
using System.Windows.Forms;

namespace ServerBox
{
    public partial class PasscodeForm : Form
    {
        private DatabaseHelper db;
        private UserService userService;

        public PasscodeForm(DatabaseHelper db)
        {
            this.db = db;
            this.userService = new UserService(this.db);
            InitializeComponent();
        }

        private void PasscodeForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userService.CheckPasscode(textBox1.Text))
                {
                    label2.Visible = false;
                    // go to main form
                    this.Hide();
                    Home home = new Home(db);
                    home.FormClosed += (s, args) => this.Close();
                    home.Show();
                }
                label2.Visible = true;
                textBox1.Focus();
            }
        }
    }
}
