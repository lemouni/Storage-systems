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
using BE;
using BLL;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors.DateTimeAdv;
using System.Reflection;
using System.Xml.Linq;

namespace CRMFinalProject
{
    public partial class ActivityForm : Form
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
        public ActivityForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        CustomerBLL Cbll = new CustomerBLL();
        UserBLL Ubll = new UserBLL();
        ActivityCategorBLL ACbll = new ActivityCategorBLL();
        ActivityBLL Abll =new ActivityBLL();
        ReminderBLL Rbll = new ReminderBLL();

        Msgbox m = new Msgbox();

        int id;

        Customer c = new Customer();
        User u = new User();
        ActivityCategory ac = new ActivityCategory();
        void FillDataGrid()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Abll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            textBoxX3.Text = "";
            textBoxX5.Text = "";
            textBoxX4.Enabled = true;
            textBoxX2.Enabled = true;
            textBoxX5.Enabled = true;
            richTextBox1.Text = "جزییات فعالیت";
            dateTimeInput1.Value = DateTime.Now;
        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX4.Text == "")
            {
                MessageBox.Show("لطفا ابتدا شماره تماس مشتری مورد نظر را وارد کنید");
                isvalid = false;
                textBoxX4.Focus();
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام کاربری یوزر مورد نظر را وارد کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX5.Text == "")
            {
                MessageBox.Show("لطفا دسته بندی فعالیت را مشخص کنید");
                isvalid = false;
                textBoxX5.Focus();
            }
            else if (textBoxX1.Text == "")
            {
                MessageBox.Show("لطفا موضوع فعالیت  را مشخص کنید");
                isvalid = false;
                textBoxX1.Focus();
            }
            else if (richTextBox1.Text == "")
            {
                MessageBox.Show("لطفا جزییات فعالیت را مشخص کنید");
            }
            else if (dateTimeInput1.Text == "")
            {
                MessageBox.Show("لطفا تاریخ مربوط به فعالیت را مشخص کنید");
            }
            return isvalid;
        }
        private void label15_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivityForm_KeyUp(object sender, KeyEventArgs e)
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

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "جزییات فعالیت";
            FillDataGrid();




            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhoneNumbers())
            {
                phone.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = phone;
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadUserNames())
            {
                names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = names;
            AutoCompleteStringCollection ACnames = new AutoCompleteStringCollection();
            foreach (var item in ACbll.ReadNames())
            {
                ACnames.Add(item);
            }
            textBoxX5.AutoCompleteCustomSource = ACnames;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            c = Cbll.Read(textBoxX4.Text);
            textBoxX4.Enabled = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            u = Ubll.ReadU(textBoxX2.Text);
            textBoxX2.Enabled = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ac = ACbll.Read(textBoxX5.Text);
            textBoxX5.Enabled = false;
        }
        User Lu = new User();
        private void label5_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (CHEKed())
            {
                Activity a = new Activity();
                a.Title = textBoxX1.Text;
                a.Info = richTextBox1.Text;
                a.RegDate = DateTime.Now;

                if (label5.Text == "ثبت فاکتور و چاپ")
                {
                    //MessageBox.Show(bll.Create(c));
                    if (Ubll.Access(Lu, "بخش فعالیت", 2))
                    {
                        //نمونه مسیج باکس
                        m.MyShowDialog("", Abll.Create(a, u, c, ac), "", false, false);
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت فعالیت را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(Abll.Update(a, ac, id));
                    label5.Text = "ثبت فاکتور و چاپ";
                }

                if (checkBox1.Checked)
                {
                    Reminder r = new Reminder();
                    r.Title = textBoxX1.Text;
                    r.ReminderInfo = richTextBox1.Text;
                    r.RegDate = DateTime.Now;
                    r.RemindDate = dateTimeInput1.Value;
                    m.MyShowDialog("", Rbll.Create(r, u), "", false, false);
                }
                FillDataGrid();
                ClearTxtBoxs();
            }
            
        }
        int index;
        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked && checkBox2.Checked || (!checkBox2.Checked && !checkBox3.Checked))
            {
                index = 0;
            }
            else if (checkBox3.Checked && !checkBox2.Checked)
            {
                index = 1;
            }
            else if (checkBox2.Checked && !checkBox3.Checked)
            {
                index = 2;
            }
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Abll.Read(textBoxX3.Text, index);
            dataGridViewX2.Columns["id"].Visible = false;
        }

        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells["id"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        //private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //Activity a = new Activity();
        //    //ActivityCategory ac = new ActivityCategory();


        //    Activity a = Abll.Read(id);
        //    ActivityCategory ac= ACbll.Read(id);
        //    textBoxX5.Text = ac.CategoryName;
        //    textBoxX1.Text = a.Title;
        //    richTextBox1.Text = a.Info;
        //    dateTimeInput1.Value = a.RegDate;
        //    label5.Text = "ویرایش اطلاعات";
        //}

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش فعالیت", 4))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف مشتری اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف فعالیت تمام اطلاعات مربوط به آن فعالیت نیز حذف خواهند شد\nآیا قصد حذف فعالیت را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    Abll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف فعالیت را ندارید", "", false, true);
            }
        }
    }
}
