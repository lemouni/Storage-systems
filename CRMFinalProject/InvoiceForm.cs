using DevComponents.AdvTree;
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
using BLL;
using BE;
using Stimulsoft.Report;
using System.Reflection;
using System.Windows;
using DevComponents.Editors.DateTimeAdv;
using Application = System.Windows.Forms.Application;
using CRM_Utility;
using static Stimulsoft.Report.StiOptions;
using System.IO;

namespace CRMFinalProject
{
    public partial class InvoiceForm : Form
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
        public InvoiceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;

        MoshkhasatFactorBLL MFbll = new MoshkhasatFactorBLL();


        CustomerBLL Cbll = new CustomerBLL();
        ProductBLL Pbll = new ProductBLL();
        InvoiceBLL Ibll = new InvoiceBLL();
        UserBLL Ubll = new UserBLL();
        CountProductBLL CPbll = new CountProductBLL();
        CountProductInAnbarBLL CPIAbll = new CountProductInAnbarBLL();
        SellDaryaftBLL SDbll = new SellDaryaftBLL();

        //Invoice i = new Invoice();
        Customer c = new Customer();

        Msgbox m = new Msgbox();
        
        void FillDataGrid1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Products.ToList();
            dataGridViewX1.Columns["id"].Visible = false;
            dataGridViewX1.Columns["Stock"].Visible = false;
            dataGridViewX1.Columns["DeleteStatus"].Visible = false;
            dataGridViewX1.Columns["RegDate"].Visible = false;
            //dataGridViewX1.Columns["Category_id"].Visible = false;
            dataGridViewX1.Columns["Name"].HeaderText = "نام محصول";
            dataGridViewX1.Columns["Price"].HeaderText = "قیمت تک";
            dataGridViewX1.Columns["Price1"].HeaderText = "قیمت عمده";
        }

        void FillDataGrid()
        {
            //dataGridViewX2.DataSource = null;
            //dataGridViewX2.DataSource = Ibll.Read();
            //dataGridViewX2.Columns["id"].Visible = false;



            dataGridViewX3.DataSource = null;
            dataGridViewX3.DataSource = Ibll.ReadViewModel();
            //  dataGridViewX2.Columns["id"].Visible = false;
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX3.Text == "" && textBoxX4.Text == "")
            {
                m.MyShowDialog("اخطار", "لطفا ابتدا شماره تماس مشتری یا نام مشتری مورد نظر را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX3.Focus();
            }
            return isvalid;
        }



        List<Product> Products = new List<Product>();
        public List<CountProduct> CountProducts = new List<CountProduct>();

        private void label15_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault();
            w.RefreshPage();
            this.Close();
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InvoiceForm_KeyUp(object sender, KeyEventArgs e)
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

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            //label7.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
            radioButton2.Checked = true;
            label7.Text = MetodExtations.ToShamsi(DateTime.Now);
            FillDataGrid();

            radioButton3.Enabled = false;

            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhoneNumbers())
            {
                phone.Add(item);
            }
            textBoxX4.AutoCompleteCustomSource = phone;

            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach (var item in Pbll.ReadNmaes())
            {
                Names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = Names;

            AutoCompleteStringCollection CNames = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadCustName())
            {
                CNames.Add(item);
            }
            textBoxX3.AutoCompleteCustomSource = CNames;

            FillDataGrid1();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            c = Cbll.Read(textBoxX4.Text);
            if (c!=null)
            {
                textBoxX4.Enabled = false;
                label1.Text = c.Name;
                label4.Text = c.Phone;
                textBoxX3.Enabled = false;

                t = c.TotalCreditWithDocument;
                cr = c.CreditWithoutDocuments;
            }
            else
            {
                m.MyShowDialog("اخطار", "مشتری با این شماره در بانک اطلاعاتی وجود ندارد", "", false, true);
            }

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CountProduct cc = new CountProduct();
            cc.producC = Pbll.ReadN(textBoxX2.Text);
            Product p = cc.producC;

            cc.counProductInAnbar = CPIAbll.ReadN(comboBoxEx2.Text, p);
            CounProductInAnbar pa = cc.counProductInAnbar;

            if (comboBoxEx2.Text!="")
            {
                cc.anbarname = pa.anbarCategoryP.AnbarName;
                //cc.anbarname = comboBoxEx2.Text;
            }
            else
            {
                m.MyShowDialog("اخطار", "آیتم انبار را پر کنید", "", false, true);
            }
            if (textBoxX6.Text != "")
            {
                cc.count = Convert.ToInt16(textBoxX6.Text);
            }
            else
            {
                m.MyShowDialog("اخطار","فیلد تعداد خالیست","",false, true);
            }
            if (textBoxX7.Text!="")
            {
                cc.Percentage = Convert.ToInt16(textBoxX7.Text);
            }
            else
            {
                cc.Percentage = 0;
            }
            if (comboBoxEx1.Text=="")
            {
                cc.priceselect = p.Price*((100-cc.Percentage)/100);
            }
            else if (comboBoxEx1.SelectedItem.ToString()== "تک فروش")
            {
                cc.priceselect = p.Price * ((100 - cc.Percentage) / 100);
            }
            else if(comboBoxEx1.SelectedItem.ToString()== "عمده فروش")
            {
                cc.priceselect = p.Price1 * ((100 - cc.Percentage) / 100);
            }

            if(p!=null && cc != null)
            {
                Products.Add(p);
                CountProducts.Add(cc);


                FillDataGrid1();
                textBoxX2.Text = "";
                textBoxX6.Text = "";
                textBoxX7.Text = "";
                string s = p.Name + " به ارزش  " + (cc.priceselect).ToString("N0") + " تومان " + " به تعداد  " + cc.count.ToString() + " ارزش کل  " + (cc.priceselect * cc.count).ToString("N0") + " تومان از انبار " + cc.anbarname;
                listBox1.Items.Add(s);

                double sum = 0;
                foreach (var item in CountProducts)
                {
                    sum = sum + (item.priceselect * item.count);
                }
                double SumPercent = 0;
                foreach (var item in CountProducts)
                {
                    SumPercent = SumPercent + item.Percentage * item.count;
                }
                double TedadCount = 0;
                foreach (var item in CountProducts)
                {
                    TedadCount = TedadCount + item.count;
                }
                label12.Text = sum.ToString("N0");
                label13.Text = (SumPercent / TedadCount).ToString();


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
                foreach (var item in CountProducts)
                {
                    sumi = sumi + (item.priceselect * item.count);
                }
                Invoice i = new Invoice();
                i.TotalCost = sumi;
                i.RegDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                if (checkBox4.Checked)
                {
                    i.IsCheckedOut = true;
                    i.CheckoutDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                    i.FeePaid = i.TotalCost;
                    #region create & print
                    if (Ubll.Access(Lu, "بخش فاکتورها", 2))
                    {
                        int idd = Ibll.Create(i, c, Lu, CountProducts).id;
                        DialogResult res = m.MyShowDialog("اطلاعیه", "آیا قصد چاپ فاکتور را دارید؟", "", true, false);
                        if (res == DialogResult.Yes)
                        {
                            List<CountProduct> csc = new List<CountProduct>();
                            csc = CPbll.GetCountProductsByInvoiceId(idd).ToList();

                            List<ProductViewModel> lsds = new List<ProductViewModel>();

                            foreach (var item in csc)
                            {
                                ProductViewModel pvs = new ProductViewModel();
                                pvs.id = item.id;
                                pvs.invoicenumberC = item.invoiceC.InvoiceNumber;
                                pvs.name = item.producC.Name;
                                pvs.price = item.priceselect;
                                pvs.Percentage = item.Percentage;
                                pvs.count = item.count;
                                pvs.total = item.priceselect * item.count;
                                pvs.anbarname = item.anbarname;
                                pvs.masolsabt = "";
                                pvs.date = "";
                                pvs.masolmarjo = "";
                                pvs.datemarjo = "";
                                lsds.Add(pvs);
                            }

                            StiReport sti = new StiReport();
                            //sti.Load(@"C:\Users\pasargad 2\Desktop\Report.mrt");
                            //sti.Load(@"D:\FM CRM\Report.mrt");
                            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                            string path = filePath + @"Report.mrt";//Name File Report
                            sti.Load(path);
                            //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\Report.mrt");

                            sti.Dictionary.Variables["foroshgah"].Value = MFbll.ReadNameForosghah();

                            sti.Dictionary.Variables["InvoiceNum"].Value = Ibll.ReadInvoiceNum();
                            sti.Dictionary.Variables["AdressCustomer"].Value = Ibll.ReadCustomeradrees(idd);
                            sti.Dictionary.Variables["CodePostCustomer"].Value = Ibll.ReadCustomercodpost(idd);
                            sti.Dictionary.Variables["Date"].Value = label7.Text;
                            sti.Dictionary.Variables["CustName"].Value = label1.Text;
                            sti.Dictionary.Variables["CustPhone"].Value = label4.Text;
                            sti.RegBusinessObject("cc", lsds);
                            sti.Render();
                            sti.Show();
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت فاکتور را ندارید", "", false, true);
                    }
                    #endregion
                }
                else if (!checkBox4.Checked && radioButton1.Checked)
                {
                    if (t >= i.TotalCost)
                    {
                        i.IsCheckedOut = false;
                        i.FeePaid = Convert.ToDouble(textBoxX1.Text);
                        #region create & print
                        if (Ubll.Access(Lu, "بخش فاکتورها", 2))
                        {
                            if (i.FeePaid<i.TotalCost)
                            {
                                int idd = Ibll.Create(i, c, Lu, CountProducts).id;
                                DialogResult res = m.MyShowDialog("اطلاعیه", "آیا قصد چاپ فاکتور را دارید؟", "", true, false);
                                if (res == DialogResult.Yes)
                                {
                                    List<CountProduct> csc = new List<CountProduct>();
                                    csc = CPbll.GetCountProductsByInvoiceId(idd).ToList();

                                    List<ProductViewModel> lsds = new List<ProductViewModel>();

                                    foreach (var item in csc)
                                    {
                                        ProductViewModel pvs = new ProductViewModel();
                                        pvs.id = item.id;
                                        pvs.invoicenumberC = item.invoiceC.InvoiceNumber;
                                        pvs.name = item.producC.Name;
                                        pvs.price = item.priceselect;
                                        pvs.Percentage = item.Percentage;
                                        pvs.count = item.count;
                                        pvs.total = item.priceselect * item.count;
                                        pvs.anbarname = item.anbarname;
                                        pvs.masolsabt = "";
                                        pvs.date = "";
                                        pvs.masolmarjo = "";
                                        pvs.datemarjo = "";
                                        lsds.Add(pvs);
                                    }

                                    StiReport sti = new StiReport();
                                    //sti.Load(@"C:\Users\pasargad 2\Desktop\Report.mrt");
                                    //sti.Load(@"D:\FM CRM\Report.mrt");
                                    string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                                    string path = filePath + @"Report.mrt";//Name File Report
                                    sti.Load(path);
                                    //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\Report.mrt");

                                    sti.Dictionary.Variables["foroshgah"].Value = MFbll.ReadNameForosghah();

                                    sti.Dictionary.Variables["InvoiceNum"].Value = Ibll.ReadInvoiceNum();
                                    sti.Dictionary.Variables["AdressCustomer"].Value = Ibll.ReadCustomeradrees(idd);
                                    sti.Dictionary.Variables["CodePostCustomer"].Value = Ibll.ReadCustomercodpost(idd);
                                    sti.Dictionary.Variables["Date"].Value = label7.Text;
                                    sti.Dictionary.Variables["CustName"].Value = label1.Text;
                                    sti.Dictionary.Variables["CustPhone"].Value = label4.Text;
                                    sti.RegBusinessObject("cc", lsds);
                                    sti.Render();
                                    sti.Show();
                                }
                            }
                            else
                            {
                                m.MyShowDialog("اخطار", "مبلغ پرداخت شده بزرگتر از جمع کل است", "", false, true);
                            }
                        }
                        else
                        {
                            m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت فاکتور را ندارید", "", false, true);
                        }
                        #endregion
                    }
                    else
                    {
                        m.MyShowDialog("اخطار", "مجموع مبلغ این فاکتور از اعتبار کل با سند مشتری مذکور بیشتر است", "", false, true);
                    }
                }
                else if (!checkBox4.Checked && radioButton2.Checked)
                {
                    i.FeePaid = Convert.ToDouble(textBoxX1.Text);
                    if (cr >= (i.TotalCost - i.FeePaid))
                    {
                        i.IsCheckedOut = false;
                        #region create & print
                        if (Ubll.Access(Lu, "بخش فاکتورها", 2))
                        {
                            if (i.FeePaid<i.TotalCost)
                            {
                                int idd = Ibll.Create(i, c, Lu, CountProducts).id;
                                DialogResult res = m.MyShowDialog("اطلاعیه", "آیا قصد چاپ فاکتور را دارید؟", "", true, false);
                                if (res == DialogResult.Yes)
                                {
                                    List<CountProduct> csc = new List<CountProduct>();
                                    csc = CPbll.GetCountProductsByInvoiceId(idd).ToList();

                                    List<ProductViewModel> lsds = new List<ProductViewModel>();

                                    foreach (var item in csc)
                                    {
                                        ProductViewModel pvs = new ProductViewModel();
                                        pvs.id = item.id;
                                        pvs.invoicenumberC = item.invoiceC.InvoiceNumber;
                                        pvs.name = item.producC.Name;
                                        pvs.price = item.priceselect;
                                        pvs.Percentage = item.Percentage;
                                        pvs.count = item.count;
                                        pvs.total = item.priceselect * item.count;
                                        pvs.anbarname = item.anbarname;
                                        pvs.masolsabt = "";
                                        pvs.date = "";
                                        pvs.masolmarjo = "";
                                        pvs.datemarjo = "";
                                        lsds.Add(pvs);
                                    }

                                    StiReport sti = new StiReport();
                                    //sti.Load(@"C:\Users\pasargad 2\Desktop\Report.mrt");
                                    //sti.Load(@"D:\FM CRM\Report.mrt");
                                    string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                                    string path = filePath + @"Report.mrt";//Name File Report
                                    sti.Load(path);
                                    //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\Report.mrt");

                                    sti.Dictionary.Variables["foroshgah"].Value = MFbll.ReadNameForosghah();

                                    sti.Dictionary.Variables["InvoiceNum"].Value = Ibll.ReadInvoiceNum();
                                    sti.Dictionary.Variables["AdressCustomer"].Value = Ibll.ReadCustomeradrees(idd);
                                    sti.Dictionary.Variables["CodePostCustomer"].Value = Ibll.ReadCustomercodpost(idd);
                                    sti.Dictionary.Variables["Date"].Value = label7.Text;
                                    sti.Dictionary.Variables["CustName"].Value = label1.Text;
                                    sti.Dictionary.Variables["CustPhone"].Value = label4.Text;
                                    sti.RegBusinessObject("cc", lsds);
                                    sti.Render();
                                    sti.Show();
                                }
                            }
                            else
                            {
                                m.MyShowDialog("اخطار", "مبلغ پرداخت شده بزرگتر از جمع کل است", "", false, true);
                            }
                        }
                        else
                        {
                            m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت فاکتور را ندارید", "", false, true);
                        }
                        #endregion
                    }
                    else
                    {
                        m.MyShowDialog("اخطار", "مجموع مبلغ این فاکتور از اعتبار بدون سند مشتری مذکور بیشتر است", "", false, true);
                    }
                }

                FillDataGrid();
                dataGridViewX1.DataSource = null;
                listBox1.Items.Clear();
                textBoxX3.Enabled = true;
                textBoxX3.Text = "";
                Products.Clear();
                label1.Text = "";
                label4.Text = "";
                label7.Text = "";
                label12.Text = "0";
                label13.Text = "0";
                textBoxX1.Text = "0";
            }
            CountProducts.Clear();
        }
        public double t;
        double cr;
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            c = Cbll.ReadCN(textBoxX3.Text);

            if (c!=null)
            {
                textBoxX3.Enabled = false;
                label1.Text = c.Name;
                label4.Text = c.Phone;
                textBoxX4.Enabled = false;

                t = c.TotalCreditWithDocument;
                cr = c.CreditWithoutDocuments;
            }
            else
            {
                m.MyShowDialog("اخطار","مشتری با این نام در بانک اطلاعاتی وجود ندارد","",false,true);
            }
        }
        

        private void textBoxX5_TextChanged(object sender, EventArgs e)
        {
            //dataGridViewX2.DataSource = null;
            //dataGridViewX2.DataSource = Ibll.Read(textBoxX5.Text);
            //dataGridViewX2.Columns["id"].Visible = false;
            if (textBoxX5.Text=="")
            {
                FillDataGrid();
            }
            else
            {

                dataGridViewX3.DataSource = null;
                dataGridViewX3.DataSource = Ibll.SearchReadViewModel(textBoxX5.Text);
            }

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش فاکتورها", 4))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت حذف فاکتور تمام اطلاعات مربوط به آن فاکتور به غیر از بخش حواله و دریافت حذف خواهند شد\nآیا قصد حذف فاکتور را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", Ibll.Delete(id), "",false,false) ;
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف فاکتور را ندارید", "", false, true);
            }
        }

        private void پرداختشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش فاکتورها", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک پرداخت شد فعال می شود\nآیا این فاکتور را پرداخت شده میدانید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Ibll.Done(id);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت فاکتور را ندارید", "", false, true);
            }
        }

        private void پرداختنشدهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش فاکتورها", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت انجام این رویداد تیک پرداخت شد غیر فعال می شود\nآیا این فاکتور را پرداخت شده نمیدانید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    Ibll.NotDone(id);


                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه آپدیت فاکتور را ندارید", "", false, true);
            }

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

        private void نمایشجزییاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CountProduct> c = new List<CountProduct>();
            Pfactor pf = new Pfactor();

            c = CPbll.GetCountProductsByInvoiceId(id).ToList();

            List<ProductViewModel> lsd = new List<ProductViewModel>();

            foreach (var item in c)
            {
                ProductViewModel pv = new ProductViewModel();
                pv.id = item.id;
                pv.invoicenumberC = item.invoiceC.InvoiceNumber;
                pv.name = item.producC.Name;
                pv.price = item.priceselect;
                pv.Percentage = item.Percentage;
                pv.count = item.count;
                pv.total = item.priceselect * item.count;
                pv.anbarname = item.anbarname;
                pv.masolsabt = item.MasolSabt;
                pv.date = item.Datekhoroj;
                pv.masolmarjo = item.MasolMarjo;
                pv.datemarjo = item.DateMarjo;
                


                lsd.Add(pv);
            }
            double sum = 0;
            foreach (var item in c)
            {
                sum = sum + item.priceselect*item.count;
            }
            pf.label12.Text = sum.ToString("N0");

            pf.dataGridViewX1.DataSource = null;
            pf.dataGridViewX1.DataSource = lsd;
            pf.dataGridViewX1.Columns["id"].Visible = false;
            pf.dataGridViewX1.Columns["invoicenumberC"].HeaderText = "شماره فاکتور";
            pf.dataGridViewX1.Columns["name"].HeaderText = "نام محصول";
            pf.dataGridViewX1.Columns["price"].HeaderText = "قیمت محصول";
            pf.dataGridViewX1.Columns["Percentage"].HeaderText = "درصد تخفیف";
            pf.dataGridViewX1.Columns["count"].HeaderText = "تعداد محصول";
            pf.dataGridViewX1.Columns["total"].HeaderText = "جمع مبلغ کل محصول";
            pf.dataGridViewX1.Columns["anbarname"].HeaderText = "انبار";
            pf.dataGridViewX1.Columns["masolsabt"].HeaderText = "مسئول ثبت";
            pf.dataGridViewX1.Columns["date"].HeaderText = "تاریخ ثبت خروج";
            pf.dataGridViewX1.Columns["masolmarjo"].HeaderText = "مسئول مرجوع";
            pf.dataGridViewX1.Columns["datemarjo"].HeaderText = "تاریخ مرجوع";
            pf.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

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

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                radioButton3.Checked=true;
                textBoxX1.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
            }
            else if (!checkBox4.Checked)
            {
                radioButton2.Checked = true;
                textBoxX1.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = false;
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            List<string> s = new List<string>();
            List<string> d = new List<string>();
            foreach (var item in CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text))
            {
                if (item.count!=0)
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
            s.Distinct();
            d.Distinct();
            if (textBoxX2.Text=="")
            {
                comboBoxEx2.Text="";
                listBox2.DataSource = null;
            }
            else
            {
                if (/*CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text) != null*/CPIAbll.GetAnbarForTextBoxFactorP(textBoxX2.Text).Count()>0)
                {
                    comboBoxEx2.DataSource = s.Distinct().ToList();
                    listBox2.DataSource = d.Distinct().ToList();
                }
                else
                {
                    comboBoxEx2.Text = "";
                    listBox2.DataSource = null;
                }
            }
        }

        private void dataGridViewX3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            id = Convert.ToInt32(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells[0].Value);
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

        private void نمایشدریافتیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SellDaryaft> sss = new List<SellDaryaft>();
            DaryaftiHesab fff = new DaryaftiHesab();
            sss = SDbll.GetCountHesabsByBuyId(id).ToList();
            List<SellDaryaftViewModel> lsddd = new List<SellDaryaftViewModel>();
            foreach (var item in sss)
            {
                SellDaryaftViewModel pvvv = new SellDaryaftViewModel();
                pvvv.id = item.id;
                pvvv.HesabBank = item.HesabBank.ShomareHesab;
                pvvv.Invoice = item.Invoice.InvoiceNumber;
                pvvv.Tozih = item.Tozih;
                pvvv.Daryafti = item.Daryafti;
                pvvv.DateDaryafti = item.DateDaryafti;
                pvvv.DaryaftShod = item.DaryaftShod;
                pvvv.MasolDaryaftShod = item.MasolDaryaftShod;
                pvvv.DateDaryaftShod = item.DateDaryaftShod;
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
            fff.dataGridViewX1.Columns["Invoice"].HeaderText = "شماره فاکتور";
            fff.dataGridViewX1.Columns["Tozih"].HeaderText = "توضیح";
            fff.dataGridViewX1.Columns["Daryafti"].HeaderText = "دریافتی";
            fff.dataGridViewX1.Columns["DateDaryafti"].HeaderText = "تاریخ دریافتی";
            fff.dataGridViewX1.Columns["DaryaftShod"].HeaderText = "وضعیت دریافتی";
            fff.dataGridViewX1.Columns["MasolDaryaftShod"].HeaderText = "مسئول دریافتی";
            fff.dataGridViewX1.Columns["DateDaryaftShod"].HeaderText = "تاریخ دریافت شد";
            fff.dataGridViewX1.Columns["Bargasht"].HeaderText = "برگشتی";
            fff.dataGridViewX1.Columns["MasolBargasht"].HeaderText = "مسئول برگشتی";
            fff.dataGridViewX1.Columns["DateBargasht"].HeaderText = "تاریخ برگشتی";
            fff.dataGridViewX1.Columns["CategoriNoe"].HeaderText = "نوع پرداخت";
            fff.ShowDialog();
        }

        private void چاپفاکتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CountProduct> csc1 = new List<CountProduct>();
            csc1 = CPbll.GetCountProductsByInvoiceId(id).ToList();

            List<ProductViewModel> lsds1 = new List<ProductViewModel>();

            foreach (var item in csc1)
            {
                ProductViewModel pvs1 = new ProductViewModel();
                pvs1.id = item.id;
                pvs1.invoicenumberC = item.invoiceC.InvoiceNumber;
                pvs1.name = item.producC.Name;
                pvs1.price = item.priceselect;
                pvs1.Percentage = item.Percentage;
                pvs1.count = item.count;
                pvs1.total = item.priceselect * item.count;
                pvs1.anbarname = item.anbarname;
                pvs1.masolsabt = item.MasolSabt;
                pvs1.date = item.Datekhoroj;
                pvs1.masolmarjo = item.MasolMarjo;
                pvs1.datemarjo = item.DateMarjo;
                lsds1.Add(pvs1);
            }
            StiReport sti = new StiReport();
            //sti.Load(@"C:\Users\pasargad 2\Desktop\Report.mrt");
            //sti.Load(@"D:\FM CRM\Report.mrt");
            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
            string path = filePath + @"Report.mrt";//Name File Report
            sti.Load(path);
            //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\Report.mrt");

            sti.Dictionary.Variables["foroshgah"].Value = MFbll.ReadNameForosghah();


            sti.Dictionary.Variables["InvoiceNum"].Value = Ibll.ReadInvoiceNumForid(id);
            sti.Dictionary.Variables["AdressCustomer"].Value = Ibll.ReadCustomeradrees(id);
            sti.Dictionary.Variables["CodePostCustomer"].Value = Ibll.ReadCustomercodpost(id);
            sti.Dictionary.Variables["Date"].Value = Ibll.ReadDate(id);
            sti.Dictionary.Variables["CustName"].Value = Ibll.ReadCustomerName(id);
            sti.Dictionary.Variables["CustPhone"].Value = Ibll.ReadCustomerPhone(id);
            sti.RegBusinessObject("cc", lsds1);
            sti.Render();
            sti.Show();
        }
    }
}
