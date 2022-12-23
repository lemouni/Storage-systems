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
using System.Windows;
using DevComponents.Editors.DateTimeAdv;
using CRM_Utility;

namespace CRMFinalProject
{
    public partial class CustomersForm : Form
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
        public CustomersForm()
        {
            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        UserBLL Ubll = new UserBLL();
        CustomerBLL bll = new CustomerBLL();

        /// <summary>
        /// برای مسیج باکس شخصی سازی شده
        /// </summary>
        Msgbox msgbox = new Msgbox();
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
            textBoxX5.Text = "";
            textBoxX6.Text = "";
            textBoxX7.Text = "";
            textBoxX8.Text = "";
            textBoxX9.Text = "";
            comboBoxEx1.Text = "";
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX4.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا نام مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX4.Focus();
            }
            if (textBoxX2.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا شماره تماس مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (comboBoxEx1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا گروه حساب مشتری را وارد کنید", "", false, true);
                isvalid = false;
                comboBoxEx1.Focus();
            }
            else if (textBoxX3.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا کد ملی مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX3.Focus();
            }
            else if (textBoxX5.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا کد اقتصادی مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX5.Focus();
            }
            else if (textBoxX6.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا آدرس مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX6.Focus();
            }
            else if (textBoxX7.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا کد پستی مشتری را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX7.Focus();
            }
            return isvalid;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            w.RefreshPage();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        User Lu = new User();
        private void label5_Click(object sender, EventArgs e)
        {
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
                Lu = w.LoggedInUser;
            if (CHEKed())
            {
                Customer c = new Customer();
                c.Name = textBoxX4.Text;
                c.Phone = textBoxX2.Text;
                c.AccountGroup = comboBoxEx1.Text;
                c.CodeMeli = textBoxX3.Text;
                c.CodeEghtesadi = textBoxX5.Text;
                c.Adress = textBoxX6.Text;
                c.CodePost = textBoxX7.Text;
                c.CreditWithoutDocuments = Convert.ToDouble(textBoxX8.Text);
                c.TotalCreditWithDocument = Convert.ToDouble(textBoxX9.Text);
                c.RegDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                if (label5.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "بخش مشتریان", 2))
                    {
                        //MessageBox.Show(bll.Create(c));

                        //نمونه مسیج باکس
                        msgbox.MyShowDialog("اطلاعیه", bll.Create(c), "", false, false);
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت مشتری را ندارید", "", false, true);
                    }
                }
                else
                {
                    msgbox.MyShowDialog("اطلاعیه", bll.Update(c, id), "", false, false);
                    label5.Text = "ثبت اطلاعات";
                    //textBoxX2.Enabled = true;
                }
                FillDataGrid();
                ClearTxtBoxs();
            }
        }

        int index;
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked && checkBox3.Checked && checkBox1.Checked || (!checkBox3.Checked && !checkBox4.Checked && !checkBox1.Checked))
            {
                index = 0;
            }
            else if (checkBox4.Checked && !checkBox3.Checked && !checkBox1.Checked)
            {
                 index = 1;
            }
            else if (checkBox3.Checked && !checkBox4.Checked && !checkBox1.Checked)
            {
                 index = 2;
            }
            else if (checkBox1.Checked && !checkBox4.Checked && !checkBox3.Checked)
            {
                index = 3;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX1.Text , index);
            dataGridViewX1.Columns["id"].Visible = false;
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            
            FillDataGrid();
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

            if (Ubll.Access(Lu, "بخش مشتریان", 3))
            {
                Customer c = bll.Read(id);
                textBoxX4.Text = c.Name;
                textBoxX2.Text = c.Phone;
                comboBoxEx1.Text = c.AccountGroup;
                textBoxX3.Text = c.CodeMeli;
                textBoxX5.Text = c.CodeEghtesadi;
                textBoxX6.Text = c.Adress;
                textBoxX7.Text = c.CodePost;
                textBoxX8.Text = c.CreditWithoutDocuments.ToString();
                textBoxX9.Text = c.TotalCreditWithDocument.ToString();
                //textBoxX2.Enabled = false;
                label5.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش مشتری را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش مشتریان", 4))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف مشتری اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = msgbox.MyShowDialog("اخطار", "در صورت حذف مشتری تمام اطلاعات مربوط به آن مشتری نیز حذف خواهند شد\nآیا قصد حذف مشتری را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف مشتری را ندارید", "", false, true);
            }

        }

        private void CustomersForm_KeyUp(object sender, KeyEventArgs e)
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

        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void textBoxX3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void textBoxX8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void textBoxX9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }
    }
}
