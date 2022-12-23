using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BE;
using BLL;

namespace CRMFinalProject
{
    public partial class ReminderForm : Form
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
        public ReminderForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;

        ReminderBLL Rbll = new ReminderBLL();
        UserBLL Ubll = new UserBLL();
        User u = new User();

        /// <summary>
        /// برای مسیج باکس شخصی سازی شده
        /// </summary>
        Msgbox msgbox = new Msgbox();
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Rbll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            richTextBox1.Text = "جزییات یادآور";
            dateTimeInput2.Value = DateTime.Now;
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا کاربر مورد نظر را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX1.Focus();
            }
            if (textBoxX2.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا موضوع یادآور را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX2.Focus();
            }
            if (richTextBox1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا جزییات یادآور را وارد کنید", "", false, true);
                isvalid = false;
                richTextBox1.Focus();
            }
            if (dateTimeInput2.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا تاریخ یادآور را وارد کنید", "", false, true);
                isvalid = false;
            }
            return isvalid;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            w.RefreshPage();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Reminder r = new Reminder();
            
            r.Title = textBoxX2.Text;
            r.ReminderInfo = richTextBox1.Text;
            r.RegDate = DateTime.Now;
            r.RemindDate = dateTimeInput2.Value;
            msgbox.MyShowDialog("",Rbll.Create(r ,u),"",false,false);
        }

        private void ReminderForm_Load(object sender, EventArgs e)
        {
            //Reminder r = new Reminder();
            FillDataGrid();
            //if (r.Title!=null)
            //{
            //     dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["Title"].Value
            //}


            ///لیست پایین برای اتوکامپلیت سورس
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadUserNames())
            {
                names.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = names;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            u = Ubll.ReadU(textBoxX1.Text);
        }
        User Lu = new User();
        private void label1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed())
            {
                Reminder r = new Reminder();

                r.Title = textBoxX2.Text;
                r.ReminderInfo = richTextBox1.Text;
                r.RegDate = DateTime.Now;
                r.RemindDate = dateTimeInput2.Value;

                //MessageBox.Show(Rbll.Create(r, u));
                if (label1.Text == "ثبت یادآور")
                {
                    //MessageBox.Show(bll.Create(c));
                    if (Ubll.Access(Lu, "بخش یادآوریها", 2))
                    {
                        //نمونه مسیج باکس
                        msgbox.MyShowDialog("اطلاعیه", Rbll.Create(r, u), "", false, false);
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت یادآور را ندارید", "", false, true);
                    }
                }
                else
                {
                    msgbox.MyShowDialog("", Rbll.Update(r, id), "", false, false);
                    label1.Text = "ثبت یادآور";
                }
                FillDataGrid();
                ClearTxtBoxs();
            }

        }

        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Rbll.Read(textBoxX4.Text);
            dataGridViewX1.Columns["id"].Visible = false;
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

                 msgbox.MyShowDialog("","این سطر خالی می باشد","",false,true);
            }
            
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش یادآوریها", 3))
            {
                Reminder r = Rbll.Read(id);
                textBoxX2.Text = r.Title;
                richTextBox1.Text = r.ReminderInfo;
                dateTimeInput2.Value = r.RemindDate;

                label1.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش یادآور را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش یادآوریها", 4))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف یادآور اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = msgbox.MyShowDialog("اخطار", "در صورت حذف یادآور تمام اطلاعات مربوط به آن مشتری نیز حذف خواهند شد\nآیا قصد حذف یادآور را دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Rbll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف یادآور را ندارید", "", false, true);
            }

        }

        private void انجامشدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش یادآوریها", 3))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف یادآور اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = msgbox.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک انجام شد فعال می شود\nآیا این یادآور را انجام شده میدانید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Rbll.Done(id);
                }
                FillDataGrid();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت یادآور را ندارید", "", false, true);
            }

        }

        private void ReminderForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = msgbox.MyShowDialog("بستن فرم", "آیا میخواهید فرم را ببندید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void انجامنشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش یادآوریها", 3))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف یادآور اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = msgbox.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک انجام شد غیر فعال می شود\nآیا این یادآور را انجام شده نمیدانید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Rbll.NotDone(id);


                }
                FillDataGrid();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت یادآور را ندارید", "", false, true);
            }

        }
    }
}
