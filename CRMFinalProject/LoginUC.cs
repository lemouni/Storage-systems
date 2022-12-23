using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BE;
using BLL;
using HandyControl.Controls;

namespace CRMFinalProject
{
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }
        UserBLL Ubll = new UserBLL();
        DashbordBLL Dbll = new DashbordBLL();
        Msgbox m = new Msgbox();
        User u = new User();
        private void label11_Click(object sender, EventArgs e)
        {
            u =Ubll.Login(textBoxX1.Text, textBoxX3.Text);
            if (u !=null)
            {
                m.MyShowDialog("خوش آمدید", "برای ورود به نرم افزار روی خروج کلیک کنید", "", false, false);
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
                w.LoggedInUser = u;
                w.RefreshPage();
                ((LoginForm)System.Windows.Forms.Application.OpenForms["LoginForm"]).Close();
            }
            else
            {
                m.MyShowDialog("اخطار", "نام کاربری یا رمز عبور اشتباه است", "", false, true);
            }
        }
    }
}
