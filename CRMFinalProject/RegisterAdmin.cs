using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using System.IO;

namespace CRMFinalProject
{
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }

        Msgbox m = new Msgbox();
        Timer t1 = new Timer();
        void SwitchPanels()
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
        }
        UserGroupBLL UGbll = new UserGroupBLL();
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }

        void CreateAdminGroup()
        {
            UserGroup ug = new UserGroup();
            ug.Title = "مدیریت";
            ug.UserAccessRoles.Add(FillAccessRole("بخش مشتریان", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش کالاها", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش فاکتورها", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش فعالیت", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش یادآوریها", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش کاربران", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("پنل پیامکی", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش گزارشات", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("بخش تنظیمات", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("حواله خروج", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("دریافت و پرداخت", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("حساب ها و اسناد", true, true, true, true));
            ug.UserAccessRoles.Add(FillAccessRole("خرید", true, true, true, true));

            UGbll.Create(ug);
        }
        OpenFileDialog ofd = new OpenFileDialog();
        Image pic;
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPics\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".JPG";
            try
            {
                string picPath = ofd.FileName;
                if (!Directory.Exists(path + PicName))
                {
                    File.Copy(picPath, path + PicName);
                }
                return path + PicName;

            }
            catch (Exception e)
            {
                MessageBox.Show("سیستم قادر به ذخیره عکس نمی باشد" + e.Message);

            }
            return null;
        }

        UserBLL Ubll = new UserBLL();

        int y = 153;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panel4.Location.Y < 462)
            {
                y = y + 15;
                panel4.Location = new Point(3, y);
            }
            else
            {
                t1.Stop();
                panel1.Visible = true;
            }
        }

        private void RegisterAdmin_Load(object sender, EventArgs e)
        {
            textBoxX8.Text = ComputerInfo.GetComputerId();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(textBoxX8.Text);
            string productKey = textBoxX7.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    m.MyShowDialog("تبریک میگم", "نرم افزار با موفقیت فعال شد", "", false, false);
                    SwitchPanels();
                    //Form1.login = true;
                    //this.Close();
                }
            }
            else
            {
                //Form1.login = false;
                m.MyShowDialog("اخطار", "لایسنس وارد شده نادرست است", "", false, true);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            User u = new User();
            CreateAdminGroup();
            u.UserName = textBoxX1.Text;
            u.Name = textBoxX2.Text;
            if (textBoxX3.Text == textBoxX4.Text)
            {
                u.Password = textBoxX3.Text;
            }
            else
            {
                m.MyShowDialog("اخطار", "کلمه عبور و تکرار آن با یکدیگر همخوانی ندارند", "", false, true);
            }
            u.RegDate = DateTime.Now;
            u.Pic = SavePic(textBoxX1.Text);
            m.MyShowDialog("نتیجه ثبت نام", Ubll.Create(u, UGbll.ReadN("مدیریت")), "", false, false);
            this.Visible = false;
            ((LoginForm)Application.OpenForms["LoginForm"]).LoadLoginForm();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                //pictureBox2.Image = pic;
                //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}

