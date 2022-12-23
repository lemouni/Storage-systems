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
using System.Windows.Controls;
using System.Windows.Forms;
using BE;
using BLL;
using DevComponents.AdvTree;

namespace CRMFinalProject
{
    public partial class DaryaftPardakht : Form
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
        public DaryaftPardakht()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        Buy b = new Buy();
        HesabBank hb = new HesabBank();
        CategoriNoe cn = new CategoriNoe();
        User Lu = new User();

        InvoiceBLL Ibll = new InvoiceBLL();

        BuyBLL Bbll = new BuyBLL();
        HesabBankBLL HBbll = new HesabBankBLL();
        CategoriNoeBLL CNbll = new CategoriNoeBLL();
        UserBLL Ubll = new UserBLL();

        BuyPardakhtBLL bll = new BuyPardakhtBLL();
        SellDaryaftBLL Sbll = new SellDaryaftBLL();

        Msgbox m = new Msgbox();
        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            RasGiri r = new RasGiri();
            r.ShowDialog();
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

        private void textBoxX5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void DaryaftPardakht_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = m.MyShowDialog("بستن فرم", "آیا میخواهید فرم را ببندید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            textBoxX8.Text = "";
            textBoxX4.Text = "";
            textBoxX9.Text = "";
            textBoxX2.Text = "";
            textBoxX11.Text = "";
            textBoxX8.Enabled = true;
            textBoxX4.Enabled = true;
            textBoxX9.Enabled = true;
            maskedTextBox1.Text = null;
        }
        void FillDataGrid1()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Sbll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs1()
        {
            textBoxX7.Text = "";
            textBoxX6.Text = "";
            textBoxX10.Text = "";
            textBoxX5.Text = "";
            textBoxX12.Text = "";
            textBoxX7.Enabled = true;
            textBoxX6.Enabled = true;
            textBoxX10.Enabled = true;
            maskedTextBox2.Text = null;
        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX8.Text == "")
            {
                MessageBox.Show("لطفا ابتدا شماره خرید را وارد کنید");
                isvalid = false;
                textBoxX8.Focus();
            }
            if (textBoxX4.Text == "")
            {
                MessageBox.Show("لطفا شماره حساب را وارد کنید");
                isvalid = false;
                textBoxX4.Focus();
            }
            else if (textBoxX9.Text == "")
            {
                MessageBox.Show("لطفا نوع پرداخت را مشخص کنید");
                isvalid = false;
                textBoxX9.Focus();
            }
            else if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا میزان پرداختی را مشخص کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX11.Text == "")
            {
                MessageBox.Show("لطفا توضیحات یا شماره چک را مشخص کنید");
                isvalid = false;
                textBoxX11.Focus();
            }
            else if (maskedTextBox1.Text == null)
            {
                MessageBox.Show("لطفا تاریخ پرداخت شدن را مشخص کنید");
                isvalid = false;
                maskedTextBox1.Focus();
            }
            return isvalid;
        }
        bool CHEKed1()
        {
            bool isvalid = true;
            if (textBoxX7.Text == "")
            {
                MessageBox.Show("لطفا ابتدا شماره فاکتور را وارد کنید");
                isvalid = false;
                textBoxX7.Focus();
            }
            if (textBoxX6.Text == "")
            {
                MessageBox.Show("لطفا شماره حساب را وارد کنید");
                isvalid = false;
                textBoxX6.Focus();
            }
            else if (textBoxX10.Text == "")
            {
                MessageBox.Show("لطفا نوع پرداخت را مشخص کنید");
                isvalid = false;
                textBoxX10.Focus();
            }
            else if (textBoxX5.Text == "")
            {
                MessageBox.Show("لطفا میزان دریافتی را مشخص کنید");
                isvalid = false;
                textBoxX5.Focus();
            }
            else if (textBoxX12.Text == "")
            {
                MessageBox.Show("لطفا توضیح را مشخص کنید");
                isvalid = false;
                textBoxX12.Focus();
            }
            else if (maskedTextBox2.Text == null)
            {
                MessageBox.Show("لطفا تاریخ دریافت شدن را مشخص کنید");
                isvalid = false;
                maskedTextBox2.Focus();
            }
            return isvalid;
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            b = Bbll.Readd(textBoxX8.Text);
            if (b != null)
            {
                textBoxX8.Enabled = false;
            }
            else
            {
                textBoxX8.Text = "";
                m.MyShowDialog("اخطار", "خرید با این شماره خرید در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            hb = HBbll.Readdd(textBoxX4.Text);
            if (hb != null)
            {
                textBoxX4.Enabled = false;
            }
            else
            {
                textBoxX4.Text = "";
                m.MyShowDialog("اخطار", "حساب با این شماره حساب در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            cn = CNbll.Read(textBoxX9.Text);
            if (cn != null)
            {
                textBoxX9.Enabled = false;
            }
            else
            {
                textBoxX9.Text = "";
                m.MyShowDialog("اخطار", "نوع پرداخت با این نام در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX1.Text);
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

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            FillDataGrid();
            AutoCompleteStringCollection Bnames = new AutoCompleteStringCollection();
            foreach (var item in Bbll.ReadNamesANC())
            {
                Bnames.Add(item);
            }
            textBoxX8.AutoCompleteCustomSource = Bnames;

            AutoCompleteStringCollection HBnames = new AutoCompleteStringCollection();
            foreach (var item in HBbll.ReadNmaes())
            {
                HBnames.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = HBnames;

            AutoCompleteStringCollection CNnames = new AutoCompleteStringCollection();
            foreach (var item in CNbll.ReadNamesANC())
            {
                CNnames.Add(item);
            }
            textBoxX9.AutoCompleteCustomSource = CNnames;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed())
            {
                double sum = 0;
                foreach (var item in bll.GetCountHesabsByBuyId(b.id))
                {
                    sum=sum+item.Pardakhti;
                }
                BuyPardakht bp = new BuyPardakht();
                bp.Pardakhti = Convert.ToDouble(textBoxX2.Text);
                bp.DatePardakht = maskedTextBox1.Text;
                bp.Tozih = textBoxX11.Text;
                if (label5.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "دریافت و پرداخت", 2))
                    {
                        if (sum + bp.Pardakhti <= b.TotalCostB)
                        {
                            //int idd = bll.Create(bp, b, hb, cn).id;
                            MessageBox.Show(bll.Create(bp, b, hb, cn));
                        }
                        else
                        {
                            MessageBox.Show("مجموع کل پرداختی از جمع کل مبلغ فاکتور بیشتر می باشد");
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت پرداختی را ندارید", "", false, true);
                    }
                }
                else
                {
                    b = Bbll.Readd(textBoxX8.Text);
                    hb = HBbll.Readdd(textBoxX4.Text);
                    cn = CNbll.Read(textBoxX9.Text);

                    if (b != null && hb !=null && cn!=null)
                    {
                        MessageBox.Show(bll.Update(bp, b,hb,cn, id));
                        label5.Text = "ثبت اطلاعات";
                    }
                    else
                    {
                        textBoxX8.Text = "";
                        textBoxX4.Text = "";
                        textBoxX9.Text = "";
                        MessageBox.Show("شماره خرید و شماره حساب و نوع پرداخت را به درستی انتخاب نمایید");
                    }
                }
                FillDataGrid();
                ClearTxtBoxs();
            }
        }

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                BuyPardakht bp = bll.Read(id);
                textBoxX8.Text = bp.Buy.BuyNumber;
                textBoxX4.Text = bp.HesabBank.ShomareHesab;
                textBoxX9.Text = bp.CategoriNoe.name;
                textBoxX2.Text = Convert.ToString(bp.Pardakhti);
                maskedTextBox1.Text = bp.DatePardakht;
                label5.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش پرداختی را ندارید", "", false, true);
            }
        }

        private void پرداختوصولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت پرداخت تمام پرداختی از بانک مربوطه کم می شود\nآیا قصد ثبت پرداخت را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", bll.SabtPardakhtShod(Lu, id), "", false, false);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت پرداخت را ندارید", "", false, true);
            }
        }

        private void برگشتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "آیا قصد برگشت زدن را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", bll.BargashtDone(Lu, id), "", false, false);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت برگشت را ندارید", "", false, true);
            }
        }

        private void DaryaftPardakht_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            FillDataGrid1();
            AutoCompleteStringCollection Bnames = new AutoCompleteStringCollection();
            foreach (var item in Bbll.ReadNamesANC())
            {
                Bnames.Add(item);
            }
            textBoxX8.AutoCompleteCustomSource = Bnames;

            AutoCompleteStringCollection HBnames = new AutoCompleteStringCollection();
            foreach (var item in HBbll.ReadNmaes())
            {
                HBnames.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = HBnames;

            AutoCompleteStringCollection CNnames = new AutoCompleteStringCollection();
            foreach (var item in CNbll.ReadNamesANC())
            {
                CNnames.Add(item);
            }
            textBoxX9.AutoCompleteCustomSource = CNnames;

            AutoCompleteStringCollection BSnames = new AutoCompleteStringCollection();
            foreach (var item in Ibll.ReadNamesANC())
            {
                BSnames.Add(item);
            }
            textBoxX7.AutoCompleteCustomSource = BSnames;

            AutoCompleteStringCollection HBSnames = new AutoCompleteStringCollection();
            foreach (var item in HBbll.ReadNmaes())
            {
                HBSnames.Add(item);
            }
            textBoxX6.AutoCompleteCustomSource = HBSnames;

            AutoCompleteStringCollection CNSnames = new AutoCompleteStringCollection();
            foreach (var item in CNbll.ReadNamesANC())
            {
                CNSnames.Add(item);
            }
            textBoxX10.AutoCompleteCustomSource = CNSnames;
        }

        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = Sbll.Read(textBoxX3.Text);
            dataGridViewX2.Columns["id"].Visible = false;
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
        Invoice i = new Invoice();
        HesabBank hbs = new HesabBank();
        CategoriNoe cns = new CategoriNoe();

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            i = Ibll.Readd(textBoxX7.Text);
            if (i != null)
            {
                textBoxX7.Enabled = false;
            }
            else
            {
                textBoxX7.Text = "";
                m.MyShowDialog("اخطار", "فاکتوری با این شماره در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            hbs = HBbll.Readdd(textBoxX6.Text);
            if (hb != null)
            {
                textBoxX6.Enabled = false;
            }
            else
            {
                textBoxX6.Text = "";
                m.MyShowDialog("اخطار", "حساب با این شماره حساب در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            cns = CNbll.Read(textBoxX10.Text);
            if (cn != null)
            {
                textBoxX10.Enabled = false;
            }
            else
            {
                textBoxX10.Text = "";
                m.MyShowDialog("اخطار", "نوع پرداخت با این نام در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed1())
            {
                double sum = 0;
                foreach (var item in Sbll.GetCountHesabsByBuyId(i.id))
                {
                    sum = sum + item.Daryafti;
                }
                SellDaryaft sd = new SellDaryaft();
                sd.Daryafti = Convert.ToDouble(textBoxX5.Text);
                sd.DateDaryafti = maskedTextBox2.Text;
                sd.Tozih = textBoxX12.Text;
                if (label7.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "دریافت و پرداخت", 2))
                    {
                        if (sum + sd.Daryafti <= i.TotalCost)
                        {
                            //int idd = bll.Create(bp, b, hb, cn).id;
                            MessageBox.Show(Sbll.Create(sd, i, hbs, cns));
                        }
                        else
                        {
                            MessageBox.Show("مجموع کل دریافتی از جمع کل مبلغ فاکتور بیشتر می باشد");
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت دریافتی را ندارید", "", false, true);
                    }
                }
                else
                {
                    i = Ibll.Readd(textBoxX7.Text);
                    hbs = HBbll.Readdd(textBoxX6.Text);
                    cns = CNbll.Read(textBoxX10.Text);

                    if (i != null && hbs != null && cns != null)
                    {
                        MessageBox.Show(Sbll.Update(sd, i, hbs, cns, id));
                        label7.Text = "ثبت اطلاعات";
                    }
                    else
                    {
                        textBoxX7.Text = "";
                        textBoxX6.Text = "";
                        textBoxX10.Text = "";
                        MessageBox.Show("شماره فاکتور و شماره حساب و نوع پرداخت را به درستی انتخاب نمایید");
                    }
                }
                FillDataGrid1();
                ClearTxtBoxs1();
            }
        }

        private void دریافتوصولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت دریافت تمام دریافتی به بانک مربوطه اضافه می شود\nآیا قصد ثبت دریافت را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", Sbll.SabtDaryaftShod(Lu, id), "", false, false);
                }
                FillDataGrid1();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت دریافت را ندارید", "", false, true);
            }
        }

        private void برگشتToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "آیا قصد برگشت زدن را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", Sbll.BargashtDone(Lu, id), "", false, false);
                }
                FillDataGrid1();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت برگشت را ندارید", "", false, true);
            }
        }

        private void ویرایشToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 3))
            {
                SellDaryaft sd = Sbll.Read(id);
                textBoxX7.Text = sd.Invoice.InvoiceNumber;
                textBoxX6.Text = sd.HesabBank.ShomareHesab;
                textBoxX10.Text = sd.CategoriNoe.name;
                textBoxX5.Text = Convert.ToString(sd.Daryafti);
                maskedTextBox2.Text = sd.DateDaryafti;
                label5.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش دریافتی را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 4))
            {

                DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف پرداختی تمام اطلاعات مربوط به آن نیز حذف خواهند شد\nآیا قصد حذف پرداختی را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف پرداختی را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "دریافت و پرداخت", 4))
            {

                DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف پرداختی تمام اطلاعات مربوط به آن نیز حذف خواهند شد\nآیا قصد حذف دریافتی را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    Sbll.Delete(id);
                }
                FillDataGrid1();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف دریافتی را ندارید", "", false, true);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///


    }

}
