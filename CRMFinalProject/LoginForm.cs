using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class LoginForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse
);
        public LoginForm()
        {
            this.Controls.Add(r);
            this.Controls["RegisterAdmin"].Location = new Point(146, 510);
            this.Controls.Add(l);
            this.Controls["LoginUC"].Location = new Point(146, 510);
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        UserBLL Ubll = new UserBLL();

        List<string> usernames = new List<string>();

        RegisterAdmin r = new RegisterAdmin();
        LoginUC l = new LoginUC();
        public void LoadLoginForm()
        {
            t3.Enabled = true;
            t3.Interval = 1;
            t3.Tick += Timer3_Tick;
            t3.Start();
        }

        bool _IsRegistered;

        private void LoginForm_Load(object sender, EventArgs e)
        {
            label2.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
            


        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >=100)
            {
                t1.Stop();
                progressBarX1.Visible = false;
                label2.Visible=false;
                label1.Visible =true;
                t2.Enabled=true;
                t2.Interval = 1;
                t2.Tick += Timer2_Tick;
                t2.Start();
            }
            else if (progressBarX1.Value == 45)
            {
                _IsRegistered = Ubll.IsRegistered();
                progressBarX1.Value++;
            }
            else
            {
                progressBarX1.Value++;
            }
        }
        int y = 220;
        int y2 = 450;
        int y3 = 450;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (label1.Location.Y >= 45)
            {
                y = y - 15;
                y2 = y2 - 30;
                label1.Location = new Point(168, y);
                if (_IsRegistered)
                {
                    this.Controls["LoginUC"].Location = new Point(146, y2);
                }
                else
                {
                    this.Controls["RegisterAdmin"].Location = new Point(146, y2);

                }
            }
            else
            {
                t2.Stop();
                panel2.Visible = true;
            }
        }
        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (this.Controls["LoginUC"].Location.Y >= 100)
            {
                y3 = y3 - 30;
                this.Controls["LoginUC"].Location = new Point(146, y2); 
            }
            else
            {
                t3.Stop();
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

    }
}
