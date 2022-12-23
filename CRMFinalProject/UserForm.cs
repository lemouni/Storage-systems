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
using System.IO;
using BE;
using BLL;
using HandyControl.Tools.Extension;
using DevComponents.Editors.DateTimeAdv;

namespace CRMFinalProject
{
    public partial class UserForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse);
        public UserForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        int id;

        Msgbox msgbox = new Msgbox();

        UserBLL bll = new UserBLL();
        UserGroupBLL UGbll = new UserGroupBLL();
        UserAccessBLL UAbll = new UserAccessBLL();
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
        void FillDataGrid2()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = UGbll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {

            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            textBoxX3.Text = "";
            
            pictureBox2.Image = null;
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا نام و نام خانوادگی کاربر را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX1.Focus();
            }
            if (textBoxX2.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا نام کاربری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX3.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا کلمه عبور را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX3.Focus();
            }
            else if (textBoxX4.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا تکرار کلمه عبور را وارد کنید", "", false, true);
                isvalid = false;
            }
            else if (comboBoxEx1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا گروه کاربری را وارد کنید", "", false, true);
                isvalid = false;
            }
            return isvalid;
        }

        bool CHEKed1()
        {
            bool isvalid = true;
            if (textBoxX10.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا نام گروه کاربری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX10.Focus();
            }
            return isvalid;
        }

        Msgbox m = new Msgbox();
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate,bool CanUpdate,bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }

            private void pictureBox2_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                pictureBox2.Image = pic;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //UserBLL bll = new UserBLL(); 

        private void label3_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                pictureBox2.Image = pic;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                
            }
            
        }
        User Lu = new User();
        private void label1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (CHEKed())
            {
                User u = new User();
                u.Name = textBoxX1.Text;
                u.UserName = textBoxX2.Text;
                UserGroup ug = UGbll.ReadN(comboBoxEx1.Text);

                if (textBoxX3.Text == textBoxX4.Text)
                {
                    u.Password = textBoxX4.Text;
                }
                else
                {
                    MessageBox.Show("تکرار کلمه عبور درست وارد نشده است");
                }
                if (label1.Text == "ثبت اطلاعات")
                {
                    u.RegDate = DateTime.Now;
                    u.Pic = SavePic(textBoxX2.Text);

                    if (u.Pic != null)
                    {
                        if (bll.Access(Lu, "بخش کاربران", 2))
                        {
                            MessageBox.Show(bll.Create(u, ug));
                        }
                        else
                        {
                            m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت یوزر را ندارید", "", false, true);
                        }

                    }
                    else
                    {
                        MessageBox.Show("تصویر کاربر انتخاب نشده است");
                    }
                }
                else
                {
                    //u.Pic = SavePic(textBoxX2.Text);

                    //if (u.Pic != null)
                    //{
                    //    MessageBox.Show(bll.Update(u, id));
                    //    label3.Enabled = true;
                    //    pictureBox2.Enabled = true;
                    //    label1.Text = "ثبت اطلاعات";
                    //    textBoxX2.Enabled = true;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("تصویر کاربر انتخاب نشده است");
                    //}
                    MessageBox.Show(bll.Update(u,ug, id));
                    label3.Enabled = true;
                    pictureBox2.Enabled = true;
                    label1.Text = "ثبت اطلاعات";
                    textBoxX2.Enabled = true;
                }
                FillDataGrid();
                ClearTxtBoxs();
            }

        }


        private void UserForm_Load(object sender, EventArgs e)
        {
            FillDataGrid2();
            FillDataGrid();
            foreach (var item in UGbll.ReadUGNames())
            {
                comboBoxEx1.Items.Add(item);
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["id"].Value);

                
            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                User u = bll.Read(id);
                textBoxX1.Text = u.Name;
                textBoxX2.Text = u.UserName;
                textBoxX2.Enabled = false;

                label3.Enabled = false;
                pictureBox2.Enabled = false;
                pictureBox2.Image = Image.FromFile(u.Pic);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //if (u.Pic != null)
                //{
                ////چون جایگاه فایل رو بردن درایو d به مشکل خورده که موردی نیست
                //    pictureBox2.Image = Image.FromFile(u.Pic);
                //    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //}
                //else
                //{
                //    u.Pic = SavePic(textBoxX2.Text + "01");
                //}



                label1.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت یوزر را ندارید", "", false, true);
            }   
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف مشتری اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف یوزر را ندارید", "", false, true);
            }
        }

        private void UserForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if
                (
                    MessageBox.Show
                    (
                        "آیا میخواهید فرم را ببندید؟",
                        "بستن فرم",
                        MessageBoxButtons.YesNo,

                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1 // hit Enter == No !
                    )
                    == DialogResult.Yes
                )
                {
                    this.Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox8.Checked = true;
                checkBox12.Checked = true;
                checkBox16.Checked = true;
                checkBox20.Checked = true;
                checkBox24.Checked = true;
                checkBox28.Checked = true;
                checkBox32.Checked = true;
                checkBox36.Checked = true;
                checkBox40.Checked = true;
                checkBox44.Checked = true;
                checkBox48.Checked = true;
                checkBox52.Checked = true;
                checkBox56.Checked = true;

            }
            else
            {
                checkBox8.Checked = false;
                checkBox12.Checked = false;
                checkBox16.Checked = false;
                checkBox20.Checked = false;
                checkBox24.Checked = false;
                checkBox28.Checked = false;
                checkBox32.Checked = false;
                checkBox36.Checked = false;
                checkBox40.Checked = false;
                checkBox44.Checked = false;
                checkBox48.Checked = false;
                checkBox52.Checked = false;
                checkBox56.Checked = false;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox7.Checked = true;
                checkBox11.Checked = true;
                checkBox15.Checked = true;
                checkBox19.Checked = true;
                checkBox23.Checked = true;
                checkBox27.Checked = true;
                checkBox31.Checked = true;
                checkBox35.Checked = true;
                checkBox39.Checked = true;
                checkBox43.Checked = true;
                checkBox47.Checked = true;
                checkBox51.Checked = true;
                checkBox55.Checked = true;

            }
            else
            {
                checkBox7.Checked = false;
                checkBox11.Checked = false;
                checkBox15.Checked = false;
                checkBox19.Checked = false;
                checkBox23.Checked = false;
                checkBox27.Checked = false;
                checkBox31.Checked = false;
                checkBox35.Checked = false;
                checkBox39.Checked = false;
                checkBox43.Checked = false;
                checkBox47.Checked = false;
                checkBox51.Checked = false;
                checkBox55.Checked = false;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox6.Checked = true;
                checkBox10.Checked = true;
                checkBox14.Checked = true;
                checkBox18.Checked = true;
                checkBox22.Checked = true;
                checkBox26.Checked = true;
                checkBox30.Checked = true;
                checkBox34.Checked = true;
                checkBox38.Checked = true;
                checkBox42.Checked = true;
                checkBox46.Checked = true;
                checkBox50.Checked = true;
                checkBox54.Checked = true;

            }
            else
            {
                checkBox6.Checked = false;
                checkBox10.Checked = false;
                checkBox14.Checked = false;
                checkBox18.Checked = false;
                checkBox22.Checked = false;
                checkBox26.Checked = false;
                checkBox30.Checked = false;
                checkBox34.Checked = false;
                checkBox38.Checked = false;
                checkBox42.Checked = false;
                checkBox46.Checked = false;
                checkBox50.Checked = false;
                checkBox54.Checked = false;

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox5.Checked = true;
                checkBox9.Checked = true;
                checkBox13.Checked = true;
                checkBox17.Checked = true;
                checkBox21.Checked = true;
                checkBox25.Checked = true;
                checkBox29.Checked = true;
                checkBox33.Checked = true;
                checkBox37.Checked = true;
                checkBox41.Checked = true;
                checkBox45.Checked = true;
                checkBox49.Checked = true;
                checkBox53.Checked = true;

            }
            else
            {
                checkBox5.Checked = false;
                checkBox9.Checked = false;
                checkBox13.Checked = false;
                checkBox17.Checked = false;
                checkBox21.Checked = false;
                checkBox25.Checked = false;
                checkBox29.Checked = false;
                checkBox33.Checked = false;
                checkBox37.Checked = false;
                checkBox41.Checked = false;
                checkBox45.Checked = false;
                checkBox49.Checked = false;
                checkBox53.Checked = false;

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (CHEKed1())
            {
                UserGroup ug = new UserGroup();
                ug.Title = textBoxX10.Text;
                ug.UserAccessRoles.Add(FillAccessRole(label6.Text, checkBox8.Checked, checkBox7.Checked, checkBox6.Checked, checkBox5.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label7.Text, checkBox12.Checked, checkBox11.Checked, checkBox10.Checked, checkBox9.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label8.Text, checkBox16.Checked, checkBox15.Checked, checkBox14.Checked, checkBox13.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label9.Text, checkBox20.Checked, checkBox19.Checked, checkBox18.Checked, checkBox17.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label10.Text, checkBox24.Checked, checkBox23.Checked, checkBox22.Checked, checkBox21.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label11.Text, checkBox28.Checked, checkBox27.Checked, checkBox26.Checked, checkBox25.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label12.Text, checkBox32.Checked, checkBox31.Checked, checkBox30.Checked, checkBox29.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label13.Text, checkBox36.Checked, checkBox35.Checked, checkBox34.Checked, checkBox33.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label14.Text, checkBox40.Checked, checkBox39.Checked, checkBox38.Checked, checkBox37.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label16.Text, checkBox44.Checked, checkBox43.Checked, checkBox42.Checked, checkBox41.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label17.Text, checkBox48.Checked, checkBox47.Checked, checkBox46.Checked, checkBox45.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label18.Text, checkBox52.Checked, checkBox51.Checked, checkBox50.Checked, checkBox49.Checked));
                ug.UserAccessRoles.Add(FillAccessRole(label19.Text, checkBox56.Checked, checkBox55.Checked, checkBox54.Checked, checkBox53.Checked));

                if (bll.Access(Lu, "بخش کاربران", 2))
                {
                    m.MyShowDialog("نتیجه ثبت اطلاعات", UGbll.Create(ug), "", false, false);
                }
                else
                {
                    m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت سطح کاربری را ندارید", "", false, true);
                }
                FillDataGrid2();
            }
        }

        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells["id"].Value);


            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
            
        }

        private void ورودفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی ورود فعال می شود\nآیا این دسترسی را آزاد میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneEnter(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }
        }

        private void ورودغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی ورود غیر فعال می شود\nآیا این دسترسی را غیر فعال میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneEnter(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }

        private void ویرایشفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی آپدیت فعال می شود\nآیا این دسترسی را آزاد میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneUpdate(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }

        private void ویرایشغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی آپدیت غیر فعال می شود\nآیا این دسترسی را غیر فعال میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneUpdate(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }

        private void آپدیتفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی افزودن فعال می شود\nآیا این دسترسی را آزاد میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneCreate(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }

        private void آپدیتغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی افزودن غیر فعال می شود\nآیا این دسترسی را غیر فعال میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneCreate(id);
                }
                FillDataGrid2();
            }

            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }
        }

        private void حذففعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی حذف فعال می شود\nآیا این دسترسی را آزاد میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.DoneDelete(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }

        private void حذفغیرفعالToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (bll.Access(Lu, "بخش کاربران", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک دسترسی حذف غیر فعال می شود\nآیا این دسترسی را غیر فعال میکنید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    UAbll.NotDoneDelete(id);
                }
                FillDataGrid2();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت سطح کاربری را ندارید", "", false, true);
            }

        }
    }
}
