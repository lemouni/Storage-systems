using BE;
using BLL;
using CRM_Utility;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class BuyForm : Form
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
        public BuyForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        Msgbox m = new Msgbox();

        List<Product> Products = new List<Product>();
        public List<Buy_CountProduct> Buy_CountProducts = new List<Buy_CountProduct>();

        ProductBLL Pbll = new ProductBLL();
        BuyBLL Ibll = new BuyBLL();
        UserBLL Ubll = new UserBLL();
        Buy_CountProductBLL CPbll = new Buy_CountProductBLL();
        CountProductInAnbarBLL CPIAbll = new CountProductInAnbarBLL();
        BuyBLL Bbll = new BuyBLL();
        BuyPardakhtBLL BPbll = new BuyPardakhtBLL();
        void FillDataGrid1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Products.ToList();
            dataGridViewX1.Columns["id"].Visible = false;
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
            dataGridViewX1.Columns["RegDate"].Visible = false;
            //dataGridViewX1.Columns["Category_id"].Visible = false;
            dataGridViewX1.Columns["Name"].HeaderText = "نام محصول";
            dataGridViewX1.Columns["Stock"].HeaderText = "موجودی واقعی";
        }
        void FillDataGrid()
        {
            dataGridViewX3.DataSource = null;
            dataGridViewX3.DataSource = Ibll.ReadViewModel();
            //  dataGridViewX2.Columns["id"].Visible = false;
        }
        void FillComboBoxAnbar()
        {
            List<string> s = new List<string>();
            foreach (var item in CPIAbll.GetAnbarForTextBoxBuyP())
            {
                if (item.count != 0)
                {
                    s.Add(item.anbarCategoryP.AnbarName);

                }
            }

            s.Distinct();
            if (/*CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text) != null*/CPIAbll.GetAnbarForTextBoxBuyP().Count() > 0)
            {
                comboBoxEx2.DataSource = s.Distinct().ToList();
                comboBoxEx2.Text = "";
            }
            comboBoxEx2.Text=null;
        }

        private void textBoxX6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }
        bool CHEKed()
        {
            bool isvalid = true;
            if (comboBoxEx3.Text == "")
            {
                m.MyShowDialog("اخطار", "لطفا نوع خرید را وارد کنید", "", false, true);
                isvalid = false;
                comboBoxEx3.Focus();
            }
            else if (textBoxX3.Text=="")
            {
                m.MyShowDialog("اخطار", "لطفا موضوع خرید را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX3.Focus();
            }
            else if (textBoxX4.Text == "")
            {
                m.MyShowDialog("اخطار", "لطفا موضوع خرید را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX4.Focus();
            }

            return isvalid;
        }

        private void textBoxX7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            w.RefreshPage();
            this.Close();
        }

        private void BuyForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dr = m.MyShowDialog("بستن فرم", "آیا میخواهید فرم را ببندید", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void BuyForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            //FillComboBoxAnbar();
            textBoxX2.Enabled = false;
            textBoxX7.Enabled = false;
            comboBoxEx2.Enabled = false;
            comboBoxEx2.Text = "";
            textBoxX6.Enabled = false;
            pictureBox3.Enabled = false;
            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach (var item in Pbll.ReadNmaes())
            {
                Names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = Names;
            FillDataGrid1();
        }
        /// <summary>
        /// فکر کنم فهمیدم مشکل دیت تایم؟بله م
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Buy_CountProduct bcc = new Buy_CountProduct();
            bcc.productB = Pbll.ReadN(textBoxX2.Text);
            Product p = bcc.productB;

            bcc.counProductInAnbarB = CPIAbll.ReadN(comboBoxEx2.Text, p);
            CounProductInAnbar pa = bcc.counProductInAnbarB;

            if (comboBoxEx2.Text != "")
            {
                bcc.anbarname = pa.anbarCategoryP.AnbarName;
            }
            else
            {
                m.MyShowDialog("اخطار", "آیتم انبار را پر کنید", "", false, true);
            }
            if (textBoxX6.Text != "")
            {
                bcc.count = Convert.ToInt16(textBoxX6.Text);
            }
            else
            {
                m.MyShowDialog("اخطار", "فیلد تعداد خالیست", "", false, true);
            }
            bcc.price = Convert.ToInt16(textBoxX7.Text);
            if (p != null && bcc != null)
            {
                Products.Add(p);
                Buy_CountProducts.Add(bcc);
                FillDataGrid1();
                textBoxX2.Text = "";
                textBoxX6.Text = "";
                textBoxX7.Text = "";
                string s = p.Name + " به ارزش  " + (bcc.price).ToString("N0") + " تومان " + " به تعداد  " + bcc.count.ToString() + " ارزش کل  " + (bcc.price * bcc.count).ToString("N0") + " تومان به انبار " + bcc.anbarname;
                listBox1.Items.Add(s);
                double sum = 0;
                foreach (var item in Buy_CountProducts)
                {
                    sum = sum + (item.price * item.count);
                }
                label12.Text = sum.ToString("N0");
            }
            else
            {
                m.MyShowDialog("اخطار", "محصول با این نام در بانک اطلاعاتی وجود ندارد", "", false, true);
            }
        }
        User Lu = new User();

        private void label5_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed())
            {
                double sumi = 0;
                foreach (var item in Buy_CountProducts)
                {
                    sumi = sumi + (item.price * item.count);
                }
                Buy bi = new Buy();
                bi.Type = comboBoxEx3.Text;
                bi.Title = textBoxX3.Text;
                bi.Tozih = textBoxX4.Text;
                bi.RegDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                if (checkBox4.Checked && comboBoxEx3.Text== "کالا")
                {
                    bi.IsCheckOut = true;
                    bi.CheckOutDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                    bi.TotalCostB = sumi;
                    bi.FeepaidB = bi.TotalCostB;
                    if (Ubll.Access(Lu, "خرید", 2))
                    {
                        //m.MyShowDialog("اطلاعیه", Bbll.Create(bi, Lu, Buy_CountProducts), "", false, false);
                        int idd = Ibll.Create(bi, Lu, Buy_CountProducts).id;
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت خرید را ندارید", "", false, true);
                    }
                }
                else if(checkBox4.Checked && comboBoxEx3.Text != "کالا")
                {
                    bi.IsCheckOut = true;
                    bi.CheckOutDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                    bi.FeepaidB = Convert.ToInt32(textBoxX1.Text);
                    bi.TotalCostB= bi.FeepaidB;

                    if (Ubll.Access(Lu, "خرید", 2))
                    {
                        //m.MyShowDialog("اطلاعیه", Bbll.Create(bi, Lu, Buy_CountProducts), "", false, false);
                        int idd = Ibll.Create(bi, Lu, Buy_CountProducts).id;
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت خرید را ندارید", "", false, true);
                    }
                }
                else if (!checkBox4.Checked && comboBoxEx3.Text == "کالا")
                {
                    bi.IsCheckOut = false;
                    bi.CheckOutDate = null;
                    bi.FeepaidB = Convert.ToInt32(textBoxX1.Text);
                    bi.TotalCostB = sumi;
                    if (Ubll.Access(Lu, "خرید", 2))
                    {
                        if (bi.FeepaidB<bi.TotalCostB)
                        {
                            //m.MyShowDialog("اطلاعیه", Bbll.Create(bi, Lu, Buy_CountProducts), "", false, false);
                            int idd = Ibll.Create(bi, Lu, Buy_CountProducts).id;
                        }
                        else
                        {
                            m.MyShowDialog("اخطار","جمع پرداخت شده از جمع کل بزرگتر است","",false, true);
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت خرید را ندارید", "", false, true);
                    }
                }
                else if (!checkBox4.Checked && comboBoxEx3.Text != "کالا")
                {
                    bi.IsCheckOut = false;
                    bi.CheckOutDate = null;
                    bi.FeepaidB = Convert.ToInt32(textBoxX1.Text);
                    bi.TotalCostB = Convert.ToInt32(textBoxX8.Text);
                    if (Ubll.Access(Lu, "خرید", 2))
                    {
                        if (bi.FeepaidB < bi.TotalCostB)
                        {
                            //m.MyShowDialog("اطلاعیه", Bbll.Create(bi, Lu, Buy_CountProducts), "", false, false);
                            int idd = Ibll.Create(bi, Lu, Buy_CountProducts).id;
                        }
                        else
                        {
                            m.MyShowDialog("اخطار", "جمع پرداخت شده از جمع کل بزرگتر است", "", false, true);
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت خرید را ندارید", "", false, true);
                    }
                }
                FillDataGrid();
                dataGridViewX1.DataSource = null;
                listBox1.Items.Clear();
                textBoxX3.Text = "";
                textBoxX4.Text = "";
                comboBoxEx3.Text = "";
                textBoxX2.Enabled=false;
                textBoxX7.Enabled = false;
                comboBoxEx2.Enabled = false;
                textBoxX6.Enabled = false;
                pictureBox3.Enabled = false;
                Products.Clear();
                label12.Text = "0";
                textBoxX1.Text = "0";
                checkBox4.Checked=false;
            }
            Buy_CountProducts.Clear();
        }

        private void comboBoxEx3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx3.Text== "کالا")
            {
                textBoxX2.Enabled = true;
                textBoxX7.Enabled = true;
                comboBoxEx2.Enabled = true;
                textBoxX6.Enabled = true;
                pictureBox3.Enabled = true;
                comboBoxEx2.Text = null;
                textBoxX8.Enabled = false;

            }
            else
            {
                textBoxX2.Enabled = false;
                textBoxX7.Enabled = false;
                comboBoxEx2.Enabled = false;
                textBoxX6.Enabled = false;
                pictureBox3.Enabled = false;
                comboBoxEx2.Text = null;
                textBoxX8.Enabled = true;

            }
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBoxX8.Enabled = false;
            }
            else if (!checkBox4.Checked)
            {
                textBoxX8.Enabled = true;

            }
        }

        private void dataGridViewX3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[0].Value);
        }

        private void textBoxX5_TextChanged(object sender, EventArgs e)
        {
            if (textBoxX5.Text == "")
            {
                FillDataGrid();
            }
            else
            {

                dataGridViewX3.DataSource = null;
                dataGridViewX3.DataSource = Ibll.SearchReadViewModel(textBoxX5.Text);
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            List<string> s = new List<string>();
            List<string> d = new List<string>();
            foreach (var item in CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text))
            {
                if (item.count != 0)
                {
                    s.Add(item.anbarCategoryP.AnbarName);

                }
            }
            foreach (var item in CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text))
            {
                if (item.count != 0)
                {
                    d.Add(item.anbarCategoryP.AnbarName + "/" + item.count + "عدد به عمده" + item.productP.Price1 + "و تکی" + item.productP.Price + " موجودی اصلی " + item.productP.Stock);
                }
            }
            d.Distinct();
            s.Distinct();

            if (textBoxX2.Text == "")
            {
                listBox2.DataSource = null;
                comboBoxEx2.Text = "";

            }
            else
            {
                if (/*CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text) != null*/CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text).Count() > 0)
                {
                    listBox2.DataSource = d.Distinct().ToList();
                    comboBoxEx2.DataSource = s.Distinct().ToList();

                }
                else
                {
                    listBox2.DataSource = null;
                    comboBoxEx2.Text = "";

                }
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "خرید", 4))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف خرید تمام اطلاعات مربوط به آن خرید به غیر از بخش حواله و دریافت حذف خواهند شد\nآیا قصد حذف خرید را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", Ibll.Delete(id), "", false, false);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف خرید را ندارید", "", false, true);
            }
        }

        private void پرداختشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "خرید", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک پرداخت شد فعال می شود\nآیا این خرید را پرداخت شده میدانید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Ibll.Done(id);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت خرید را ندارید", "", false, true);
            }
        }

        private void نمایشجزییاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Buy_CountProduct> cb = new List<Buy_CountProduct>();
            PBuyForm pff = new PBuyForm();

            cb = CPbll.GetCountProductsByBuyId(id).ToList();

            List<Buy_ProductViewModel> lsdd = new List<Buy_ProductViewModel>();

            foreach (var item in cb)
            {
                Buy_ProductViewModel pvv = new Buy_ProductViewModel();
                pvv.id = item.id;
                pvv.BuynumberC = item.buyC.BuyNumber;
                pvv.nameProduct = item.productB.Name;
                pvv.priceProduct = item.price;
                pvv.count = item.count;
                pvv.total = item.price * item.count;
                pvv.anbarname = item.anbarname;
                pvv.masolsabt = item.MasolSabt;
                pvv.date = item.DateSabt;
                pvv.masolmarjo = item.MasolMarjo;
                pvv.datemarjo = item.DateMarjo;



                lsdd.Add(pvv);
            }
            double sum = 0;
            foreach (var item in cb)
            {
                sum = sum + item.price * item.count;
            }
            pff.label12.Text = sum.ToString("N0");

            pff.dataGridViewX1.DataSource = null;
            pff.dataGridViewX1.DataSource = lsdd;
            pff.dataGridViewX1.Columns["id"].Visible = false;
            pff.dataGridViewX1.Columns["BuynumberC"].HeaderText = "شماره خرید";
            pff.dataGridViewX1.Columns["nameProduct"].HeaderText = "نام محصول";
            pff.dataGridViewX1.Columns["priceProduct"].HeaderText = "قیمت محصول";
            pff.dataGridViewX1.Columns["count"].HeaderText = "تعداد محصول";
            pff.dataGridViewX1.Columns["total"].HeaderText = "جمع مبلغ کل محصول";
            pff.dataGridViewX1.Columns["anbarname"].HeaderText = "انبار";
            pff.dataGridViewX1.Columns["masolsabt"].HeaderText = "مسئول ثبت";
            pff.dataGridViewX1.Columns["date"].HeaderText = "تاریخ ثبت ورود";
            pff.dataGridViewX1.Columns["masolmarjo"].HeaderText = "مسئول مرجوع";
            pff.dataGridViewX1.Columns["datemarjo"].HeaderText = "تاریخ مرجوع";
            pff.ShowDialog();
        }
        Timer t1 = new Timer();

        private void buttonX1_Click(object sender, EventArgs e)
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();



        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            progressBarX1.Visible = true;

            if (progressBarX1.Value >= 100)
            {
                progressBarX1.Visible = false;
            }
            else if (progressBarX1.Value == 45)
            {
                Ibll.CheckPardakht();
                progressBarX1.Value++;
                FillDataGrid();
            }
            else
            {
                progressBarX1.Value++;
            }
        }

        private void نمایشپرداختیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<BuyPardakht> sss = new List<BuyPardakht>();
            PardakhtHesab fff = new PardakhtHesab();
            sss = BPbll.GetCountHesabsByBuyId(id).ToList();
            List<BuyPardakhtViewModel> lsddd = new List<BuyPardakhtViewModel>();
            foreach (var item in sss)
            {
                BuyPardakhtViewModel pvvv = new BuyPardakhtViewModel();
                pvvv.id = item.id;
                pvvv.HesabBank = item.HesabBank.ShomareHesab;
                pvvv.Buy = item.Buy.BuyNumber;
                pvvv.Tozih = item.Tozih;
                pvvv.Pardakhti = item.Pardakhti;
                pvvv.DatePardakht = item.DatePardakht;
                pvvv.PardakhtShod = item.PardakhtShod;
                pvvv.MasolPardakhtShod = item.MasolPardakhtShod;
                pvvv.DatePardakhtShod = item.DatePardakhtShod;
                pvvv.Bargasht = item.Bargasht;
                pvvv.MasolBargasht = item.MasolBargasht;
                pvvv.DateBargasht = item.DateBargasht;
                pvvv.CategoriNoe = item.CategoriNoe.name;
                lsddd.Add(pvvv);
            }
            fff.dataGridViewX1.DataSource = null;
            fff.dataGridViewX1.DataSource = lsddd;
            fff.dataGridViewX1.Columns["id"].Visible = false;
            fff.dataGridViewX1.Columns["HesabBank"].HeaderText = "شماره حساب";
            fff.dataGridViewX1.Columns["Buy"].HeaderText = "شماره خرید";
            fff.dataGridViewX1.Columns["Tozih"].HeaderText = "توضیح";
            fff.dataGridViewX1.Columns["Pardakhti"].HeaderText = "پرداختی";
            fff.dataGridViewX1.Columns["DatePardakht"].HeaderText = "تاریخ پرداختی";
            fff.dataGridViewX1.Columns["PardakhtShod"].HeaderText = "وضعیت پرداختی";
            fff.dataGridViewX1.Columns["MasolPardakhtShod"].HeaderText = "مسئول پرداختی";
            fff.dataGridViewX1.Columns["DatePardakhtShod"].HeaderText = "تاریخ پرداخت شد";
            fff.dataGridViewX1.Columns["Bargasht"].HeaderText = "برگشتی";
            fff.dataGridViewX1.Columns["MasolBargasht"].HeaderText = "مسئول برگشتی";
            fff.dataGridViewX1.Columns["DateBargasht"].HeaderText = "تاریخ برگشتی";
            fff.dataGridViewX1.Columns["CategoriNoe"].HeaderText = "نوع پرداخت";
            fff.ShowDialog();
        }
    }
}
