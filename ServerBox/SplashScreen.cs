using ServerBox.Lib;
using ServerBox.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerBox
{
    public partial class SplashScreen : Form
    {
        private DatabaseHelper db;

        public SplashScreen(DatabaseHelper db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            Task.Delay(2000).Wait();
            UserService userService = new UserService(db);
            if (userService.HasPasscode())
            {
                this.Hide();
                PasscodeForm passcodeForm = new PasscodeForm(db);
                passcodeForm.FormClosed += (s, args) => this.Close();
                passcodeForm.Show();
            }
            else
            {
                this.Hide();
                Home home = new Home(db);
                home.FormClosed += (s, args) => this.Close();
                home.Show();
            }
        }

    }
}
