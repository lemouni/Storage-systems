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
using DevComponents.DotNetBar.Controls;

namespace CRMFinalProject
{
    public partial class HesabAsnad : Form
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
        public HesabAsnad()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        Msgbox m = new Msgbox();
        HesabBankBLL bll = new HesabBankBLL();
        SellDaryaftBLL Sbll = new SellDaryaftBLL();
        BuyPardakhtBLL Bbll = new BuyPardakhtBLL();
        UserBLL Ubll = new UserBLL();
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

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HesabAsnad_KeyUp(object sender, KeyEventArgs e)
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
            textBoxX2.Text = "";
            textBoxX4.Text = "";
            textBoxX3.Text = "";
            textBoxX5.Text = "";
        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX4.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام بانک را وارد کنید");
                isvalid = false;
                textBoxX4.Focus();
            }
            if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا شماره حساب را وارد کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX3.Text == "")
            {
                MessageBox.Show("لطفا شماره شبا را مشخص کنید");
                isvalid = false;
                textBoxX3.Focus();
            }
            else if (textBoxX5.Text == "")
            {
                MessageBox.Show("لطفا موجودی حساب را مشخص کنید");
                isvalid = false;
                textBoxX5.Focus();
            }
            return isvalid;
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX1.Text);
            dataGridViewX1.Columns["id"].Visible = false;
        }

        private void HesabAsnad_Load(object sender, EventArgs e)
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

                MessageBox.Show("این سطر خالی می باشد");
            }
        }
        User Lu = new User();

        private void label5_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed())
            {

                HesabBank hb = new HesabBank();
                hb.name = textBoxX4.Text;
                hb.ShomareHesab = textBoxX2.Text;
                hb.Sheba = textBoxX3.Text;
                hb.Stock = Convert.ToInt16(textBoxX5.Text);
                if (label5.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "حساب ها و اسناد", 2))
                    {
                        //int idd = bll.Create(hb).id;
                        MessageBox.Show(bll.Create(hb));
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت حساب را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(bll.Update(hb, id));
                    label5.Text = "ثبت اطلاعات";
                }
                FillDataGrid();
                ClearTxtBoxs();
            }
        }

        private void آپدیتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حساب ها و اسناد", 3))
            {
                HesabBank hb = bll.Read(id);
                textBoxX4.Text = hb.name;
                textBoxX2.Text = hb.ShomareHesab;
                textBoxX3.Text = hb.Sheba;
                textBoxX5.Text = Convert.ToString(hb.Stock);
                label5.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش حساب را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حساب ها و اسناد", 4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف حساب اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show(bll.Delete(id));
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف حساب را ندارید", "", false, true);
            }
        }

        private void دریافتیهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SellDaryaft> ss = new List<SellDaryaft>();
            DaryaftiHesab f = new DaryaftiHesab();
            ss = Sbll.GetCountDaryaftssByHesabId(id).ToList();
            List<SellDaryaftViewModel> lsdd = new List<SellDaryaftViewModel>();
            foreach (var item in ss)
            {
                SellDaryaftViewModel pvv = new SellDaryaftViewModel();
                pvv.id = item.id;
                pvv.HesabBank = item.HesabBank.ShomareHesab;
                pvv.Invoice = item.Invoice.InvoiceNumber;
                pvv.Tozih = item.Tozih;
                pvv.Daryafti = item.Daryafti;
                pvv.DateDaryafti = item.DateDaryafti;
                pvv.DaryaftShod = item.DaryaftShod;
                pvv.MasolDaryaftShod = item.MasolDaryaftShod;
                pvv.DateDaryaftShod = item.DateDaryaftShod;
                pvv.Bargasht = item.Bargasht;
                pvv.MasolBargasht = item.MasolBargasht;
                pvv.DateBargasht = item.DateBargasht;
                pvv.CategoriNoe = item.CategoriNoe.name;
                lsdd.Add(pvv);
            }
            f.dataGridViewX1.DataSource = null;
            f.dataGridViewX1.DataSource = lsdd;
            f.dataGridViewX1.Columns["id"].Visible = false;
            f.dataGridViewX1.Columns["HesabBank"].HeaderText = "شماره حساب";
            f.dataGridViewX1.Columns["Invoice"].HeaderText = "شماره فاکتور";
            f.dataGridViewX1.Columns["Tozih"].HeaderText = "توضیح";
            f.dataGridViewX1.Columns["Daryafti"].HeaderText = "دریافتی";
            f.dataGridViewX1.Columns["DateDaryafti"].HeaderText = "تاریخ دریافتی";
            f.dataGridViewX1.Columns["DaryaftShod"].HeaderText = "وضعیت دریافتی";
            f.dataGridViewX1.Columns["MasolDaryaftShod"].HeaderText = "مسئول دریافتی";
            f.dataGridViewX1.Columns["DateDaryaftShod"].HeaderText = "تاریخ دریافت شد";
            f.dataGridViewX1.Columns["Bargasht"].HeaderText = "برگشتی";
            f.dataGridViewX1.Columns["MasolBargasht"].HeaderText = "مسئول برگشتی";
            f.dataGridViewX1.Columns["DateBargasht"].HeaderText = "تاریخ برگشتی";
            f.dataGridViewX1.Columns["CategoriNoe"].HeaderText = "نوع پرداخت";
            f.ShowDialog();

        }

        private void پرداختیهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BuyPardakht> ss = new List<BuyPardakht>();
            PardakhtHesab f = new PardakhtHesab();
            ss = Bbll.GetCountPardakhtssByHesabId(id).ToList();
            List<BuyPardakhtViewModel> lsdd = new List<BuyPardakhtViewModel>();
            foreach (var item in ss)
            {
                BuyPardakhtViewModel pvv = new BuyPardakhtViewModel();
                pvv.id = item.id;
                pvv.HesabBank = item.HesabBank.ShomareHesab;
                pvv.Buy = item.Buy.BuyNumber;
                pvv.Tozih = item.Tozih;
                pvv.Pardakhti = item.Pardakhti;
                pvv.DatePardakht = item.DatePardakht;
                pvv.PardakhtShod = item.PardakhtShod;
                pvv.MasolPardakhtShod = item.MasolPardakhtShod;
                pvv.DatePardakhtShod = item.DatePardakhtShod;
                pvv.Bargasht = item.Bargasht;
                pvv.MasolBargasht = item.MasolBargasht;
                pvv.DateBargasht = item.DateBargasht;
                pvv.CategoriNoe = item.CategoriNoe.name;
                lsdd.Add(pvv);
            }
            f.dataGridViewX1.DataSource = null;
            f.dataGridViewX1.DataSource = lsdd;
            f.dataGridViewX1.Columns["id"].Visible = false;
            f.dataGridViewX1.Columns["HesabBank"].HeaderText = "شماره حساب";
            f.dataGridViewX1.Columns["Buy"].HeaderText = "شماره خرید";
            f.dataGridViewX1.Columns["Tozih"].HeaderText = "توضیح";
            f.dataGridViewX1.Columns["Pardakhti"].HeaderText = "پرداختی";
            f.dataGridViewX1.Columns["DatePardakht"].HeaderText = "تاریخ پرداختی";
            f.dataGridViewX1.Columns["PardakhtShod"].HeaderText = "وضعیت پرداختی";
            f.dataGridViewX1.Columns["MasolPardakhtShod"].HeaderText = "مسئول پرداختی";
            f.dataGridViewX1.Columns["DatePardakhtShod"].HeaderText = "تاریخ پرداخت شد";
            f.dataGridViewX1.Columns["Bargasht"].HeaderText = "برگشتی";
            f.dataGridViewX1.Columns["MasolBargasht"].HeaderText = "مسئول برگشتی";
            f.dataGridViewX1.Columns["DateBargasht"].HeaderText = "تاریخ برگشتی";
            f.dataGridViewX1.Columns["CategoriNoe"].HeaderText = "نوع پرداخت";
            f.ShowDialog();
        }
    }
}
