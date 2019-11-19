using ServerBox.Lib;
using ServerBox.Model;
using ServerBox.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ServerBox
{
    public partial class Home : Form
    {
        private DatabaseHelper db;
        private ServerService serverService;
        private List<Server> servers;

        public Home(DatabaseHelper db)
        {
            this.db = db;
            serverService = new ServerService(this.db);
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Initialize();
            dgv1.Columns[0].Visible = false;
            dgv1.Columns[5].Visible = false;

            DataGridViewButtonColumn connectButton = new DataGridViewButtonColumn();
            connectButton.UseColumnTextForButtonValue = true;
            connectButton.Text = "Connect";
            connectButton.Name = "Connect";
            dgv1.Columns.Add(connectButton);
        }

        private void Initialize()
        {
            servers = serverService.GetAll();
            var source = new BindingSource();
            source.DataSource = servers;
            dgv1.AutoGenerateColumns = true;
            dgv1.DataSource = source;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(db);
            addForm.ShowDialog();
            Initialize();
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                Server selectedServer = servers.ElementAt(e.RowIndex);
                bool usePassword = false;
                string command = "";
                if (selectedServer.Password != null && selectedServer.Password != "")
                {
                    usePassword = true;
                }
                switch (selectedServer.Type)
                {
                    case "SSH":
                        command = String.Format(" /K ssh {0}@{1}{2}",
                            selectedServer.Username,
                            selectedServer.Ip, "");
                        Console.WriteLine("Command : " + command);
                        if (usePassword)
                        {
                            copyPassword(selectedServer.Password);
                        }
                        System.Diagnostics.Process.Start("cmd.exe", command);
                        break;
                    case "RDP":
                        string rdpPath = serverService.generateRdpFile(selectedServer);
                        command = String.Format(@" /C mstsc {0}",
                            rdpPath);
                        Console.WriteLine("Command : " + command);
                        if (usePassword)
                        {
                            copyPassword(selectedServer.Password);
                        }
                        System.Diagnostics.Process.Start("cmd.exe", command);
                        break;
                    default:
                        break;
                }
            }
        }

        private void copyPassword(string password)
        {
            // copy password to clipboard
            Clipboard.SetText(password);
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = "Server Box";
            notifyIcon.BalloonTipText = "Password copied to clipboard";
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.ShowBalloonTip(3000);
        }

        private void Home_Shown(object sender, EventArgs e)
        {
            if (servers.Count < 1)
            {
                addToolStripMenuItem.PerformClick();
            }
        }

        private void passcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPasscodeForm addForm = new SetPasscodeForm(db);
            addForm.ShowDialog();
            Initialize();
        }
    }
}
