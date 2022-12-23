using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Media.Effects;
using BE;
using BLL;

namespace CRMFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginForm f = new LoginForm();
            InitializeComponent();
            OpenWinForm(f);
        }

        UserBLL Ubll = new UserBLL();
        public User LoggedInUser = new User();
        DashbordBLL Dbll = new DashbordBLL();
        Msgbox m = new Msgbox();
        //void OpenWinForm(Form f)
        //{
        //    BlurEffect bme = new BlurEffect();
        //    this.Effect = bme;
        //    bme.Radius = 15;
        //    f.ShowDialog();
        //    Effect = null;
        //}



        void OpenWinForm(Form f)
        {

            Window g = this.FindName("Main") as Window;
            BlurBitmapEffect blurBitmapEffect = new BlurBitmapEffect();
            blurBitmapEffect.Radius = 20;

            g.BitmapEffect = blurBitmapEffect;

            f.ShowDialog();
            blurBitmapEffect.Radius = 0;
            g.BitmapEffect = blurBitmapEffect;
        }
        public void RefreshPage()
        {
            UserNameTxt.Text = LoggedInUser.UserName;
            PersonNameTxt.Text = LoggedInUser.Name;
            RemindersCountTxt.Text = Dbll.UserRemindersCount(LoggedInUser);
            CustCountTxt.Text = Dbll.CustomersCount();
            SellCountTxt.Text = Dbll.SellsCount();
            int a = 0;
            foreach (var item in Dbll.GetUserReminders(LoggedInUser))
            {
                if (a<7)
                {
                    ReminderUC r = new ReminderUC();
                    r.ReminderTitleTxt.Text = item.Title;
                    r.ReminderInfoTxt.Text = item.ReminderInfo;
                    Grid.SetRow(r, 5 + a);
                    Grid.SetColumnSpan(r, 6);
                    MainGrid.Children.Add(r);
                    a++;
                } 
            }
            
        }
        

        private void WrapPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش یادآوریها", 1))
            {
                ReminderForm f = new ReminderForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "پنل پیامکی", 1))
            {
                SMSPanelForm f = new SMSPanelForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"بخش مشتریان",1))
            {
                CustomersForm f = new CustomersForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void WrapPanel_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش کالاها", 1))
            {
                ProductForm f = new ProductForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش فاکتورها", 1))
            {
                InvoiceForm f = new InvoiceForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش فعالیت", 1))
            {
                ActivityForm f = new ActivityForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش کاربران", 1))
            {
                UserForm f = new UserForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        

        private void WrapPanel_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش تنظیمات", 1))
            {
                SettingForm f = new SettingForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();

        }

        private void WrapPanel_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش گزارشات", 1))
            {
                ReportForm f = new ReportForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //RefreshPage();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoggedInUser = null;
            LoginForm f = new LoginForm();
            OpenWinForm(f);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش مشتریان", 1))
            {
                CustomersForm f = new CustomersForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "بخش فاکتورها", 1))
            {
                InvoiceForm f = new InvoiceForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void WrapPanel_MouseLeftButtonDown_9(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "حواله خروج", 1))
            {
                HavaleKhorojForm f = new HavaleKhorojForm();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void WrapPanel_MouseLeftButtonDown_10(object sender, MouseButtonEventArgs e)
        {
            {
                if (Ubll.Access(LoggedInUser, "خرید", 1))
                {
                    BuyForm f = new BuyForm();
                    OpenWinForm(f);
                }
                else
                {
                    m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
                }
                RefreshPage();
            }
        }

        private void WrapPanel_MouseLeftButtonDown_11(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "دریافت و پرداخت", 1))
            {
                DaryaftPardakht f = new DaryaftPardakht();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void WrapPanel_MouseLeftButtonDown_12(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser, "حساب ها و اسناد", 1))
            {
                HesabAsnad f = new HesabAsnad();
                OpenWinForm(f);
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این قسمت را ندارید", "", false, true);
            }
            RefreshPage();
        }

        private void WrapPanel_MouseLeftButtonDown_13(object sender, MouseButtonEventArgs e)
        {
            DarbareMA f = new DarbareMA();
            OpenWinForm(f);
        }
    }
}
