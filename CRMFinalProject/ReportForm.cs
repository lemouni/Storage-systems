using BE;
using BLL;
using DevComponents.DotNetBar.Charts;
using DevComponents.WinForms.Drawing;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CRMFinalProject
{
    public partial class ReportForm : Form
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
        public ReportForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        CustomerBLL Cbll = new CustomerBLL();
        InvoiceBLL Ibll = new InvoiceBLL();
        BuyBLL Bbll = new BuyBLL();
        BuyPardakhtBLL BPbll = new BuyPardakhtBLL();
        SellDaryaftBLL SDbll = new SellDaryaftBLL();

        UserBLL Ubll = new UserBLL();

        private void label4_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = SeriesChartType.Column;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = SeriesChartType.Point;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].ChartType = SeriesChartType.Area;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    if (maskedTextBox4.Text != "    /  /" && maskedTextBox3.Text != "    /  /")
                    {
                        List<Customer> C = new List<Customer>();
                        C = Cbll.ReadCust(Convert.ToDateTime(maskedTextBox4.Text), Convert.ToDateTime(maskedTextBox3.Text));
                        List<ProductViewModel> lsds = new List<ProductViewModel>();

                        foreach (var item in C)
                        {
                            ProductViewModel pvs = new ProductViewModel();
                            pvs.id = item.id;
                            pvs.invoicenumberC = item.Name;
                            pvs.name = item.CodeMeli;
                            pvs.anbarname = item.Phone;
                            pvs.date = item.AccountGroup;
                            pvs.masolmarjo = item.CodePost;
                            lsds.Add(pvs);
                        }
                        StiReport sti = new StiReport();
                        string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                        string path = filePath + @"ReportCustomer.mrt";//Name File Report
                        sti.Load(path);
                        //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportCustomer.mrt");
                        sti.RegBusinessObject("c", lsds);
                        sti.Render();
                        sti.Show();
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ رو پر کنید");
                    }
                }
                else if (radioButton2.Checked)
                {
                    if (maskedTextBox4.Text != "    /  /" && maskedTextBox3.Text != "    /  /")
                    {
                        List<Invoice> I = new List<Invoice>();
                        I = Ibll.ReadInv(Convert.ToDateTime(maskedTextBox4.Text), Convert.ToDateTime(maskedTextBox3.Text));
                        List<InvoiceViewModel> lsds = new List<InvoiceViewModel>();

                        foreach (var item in I)
                        {
                            InvoiceViewModel pvs = new InvoiceViewModel();
                            pvs.id = item.id;
                            pvs.InvoiceNumber = item.InvoiceNumber;
                            pvs.RegDate = item.RegDate;
                            pvs.IsCheckedOut = item.IsCheckedOut;
                            pvs.CheckoutDate = item.CheckoutDate.ToString();
                            pvs.CustomerName = item.Customer.Name;
                            pvs.totalcost = item.TotalCost;
                            lsds.Add(pvs);
                        }
                        StiReport sti = new StiReport();
                        string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                        string path = filePath + @"ReportInvoice.mrt";//Name File Report
                        sti.Load(path);
                        //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportInvoice.mrt");
                        sti.RegBusinessObject("c", lsds);
                        sti.Render();
                        sti.Show();
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ رو پر کنید");
                    }

                }
                else if (radioButton11.Checked)
                {
                    if (maskedTextBox4.Text != "    /  /" && maskedTextBox3.Text != "    /  /")
                    {
                        List<Invoice> I1 = new List<Invoice>();
                        I1 = Ibll.ReadInvNotChekedOut(Convert.ToDateTime(maskedTextBox4.Text), Convert.ToDateTime(maskedTextBox3.Text));
                        List<InvoiceViewModel> lsds1 = new List<InvoiceViewModel>();

                        foreach (var item in I1)
                        {
                            InvoiceViewModel pvs1 = new InvoiceViewModel();
                            pvs1.id = item.id;
                            pvs1.InvoiceNumber = item.InvoiceNumber;
                            pvs1.RegDate = item.RegDate;
                            pvs1.IsCheckedOut = item.IsCheckedOut;
                            pvs1.CheckoutDate = item.CheckoutDate.ToString();
                            pvs1.CustomerName = item.Customer.Name;
                            pvs1.totalcost = item.TotalCost;
                            lsds1.Add(pvs1);
                        }
                        StiReport sti = new StiReport();
                        string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                        string path = filePath + @"ReportInvoice.mrt";//Name File Report
                        sti.Load(path);
                        //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportInvoice.mrt");
                        sti.RegBusinessObject("c", lsds1);
                        sti.Render();
                        sti.Show();
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ رو پر کنید");
                    }
                }
                else if (radioButton4.Checked)
                {
                    if (maskedTextBox4.Text != "    /  /" && maskedTextBox3.Text != "    /  /")
                    {
                        List<Buy> B = new List<Buy>();
                        B = Bbll.ReadBuy(Convert.ToDateTime(maskedTextBox4.Text), Convert.ToDateTime(maskedTextBox3.Text));
                        List<InvoiceViewModel> lsds2 = new List<InvoiceViewModel>();
                        foreach (var item in B)
                        {
                            InvoiceViewModel pvs2 = new InvoiceViewModel();
                            pvs2.id = item.id;
                            pvs2.InvoiceNumber = item.BuyNumber;
                            pvs2.RegDate = item.RegDate;
                            pvs2.IsCheckedOut = item.IsCheckOut;
                            pvs2.CheckoutDate = item.CheckOutDate.ToString();
                            pvs2.totalcost = item.TotalCostB;
                            lsds2.Add(pvs2);
                        }
                        StiReport sti = new StiReport();
                        string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                        string path = filePath + @"ReportBuy.mrt";//Name File Report
                        sti.Load(path);
                        //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportBuy.mrt");
                        sti.RegBusinessObject("c", lsds2);
                        sti.Render();
                        sti.Show();
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ رو پر کنید");
                    }
                }
                else if (radioButton7.Checked)
                {
                    List<BuyPardakht> BP = new List<BuyPardakht>();
                    BP = BPbll.ReadBuyPadakhtNotChecked();
                    List<InvoiceViewModel> lsd3 = new List<InvoiceViewModel>();
                    foreach (var item in BP)
                    {
                        InvoiceViewModel pvs3 = new InvoiceViewModel();
                        pvs3.id = item.id;
                        pvs3.InvoiceNumber = item.Buy.BuyNumber;
                        pvs3.CheckoutDate = item.HesabBank.ShomareHesab;
                        pvs3.CustomerName = item.CategoriNoe.name;
                        pvs3.totalcost = item.Pardakhti;
                        lsd3.Add(pvs3);
                    }
                    StiReport sti = new StiReport();
                    string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                    string path = filePath + @"ReportPardakhtiNotChecked.mrt";//Name File Report
                    sti.Load(path);
                    //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportPardakhtiNotChecked.mrt");
                    sti.RegBusinessObject("c", lsd3);
                    sti.Render();
                    sti.Show();
                }
                else if (radioButton8.Checked)
                {
                    List<SellDaryaft> SD = new List<SellDaryaft>();
                    SD = SDbll.ReadBuyDaryaftNotChecked();
                    List<InvoiceViewModel> lsd4 = new List<InvoiceViewModel>();
                    foreach (var item in SD)
                    {
                        InvoiceViewModel pvs4 = new InvoiceViewModel();
                        pvs4.id = item.id;
                        pvs4.InvoiceNumber = item.Invoice.InvoiceNumber;
                        pvs4.CheckoutDate = item.HesabBank.ShomareHesab;
                        pvs4.CustomerName = item.CategoriNoe.name;
                        pvs4.totalcost = item.Daryafti;
                        lsd4.Add(pvs4);
                    }
                    StiReport sti = new StiReport();
                    string filePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";//Name Folder
                    string path = filePath + @"ReportPardakhtiNotChecked.mrt";//Name File Report
                    sti.Load(path);
                    //sti.Load(@"D:\فایل اصلی نرم افزار اف دی\CRMFinalProject\ReportPardakhtiNotChecked.mrt");
                    sti.RegBusinessObject("c", lsd4);
                    sti.Render();
                    sti.Show();
                }
                else
                {
                    MessageBox.Show("یک گزینه را انتخاب نمایید");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("ایرادی وجود دارد"); ;
            }
            maskedTextBox4.Text = null;
            maskedTextBox3.Text = null;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            try
            {
                if (radioButton24.Checked)
                {
                    if (maskedTextBox1.Text != "    /  /" && maskedTextBox2.Text != "    /  /")
                    {
                        foreach (var item in Ubll.ReadInvoiceByUseer())
                        {
                            int x = 0;
                            foreach (var q in item.Invoices)
                            {
                                if (q.RegDate > Convert.ToDateTime(maskedTextBox1.Text) && q.RegDate < Convert.ToDateTime(maskedTextBox2.Text))
                                {
                                    x++;
                                }
                            }
                            chart1.Series["Series1"].Points.AddXY(item.Name, x);
                        }
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ خالیست");
                    }
                }
                else if (radioButton23.Checked)
                {
                    if (maskedTextBox1.Text != "    /  /" && maskedTextBox2.Text != "    /  /")
                    {
                        foreach (var item in Cbll.ReadInvoiceByCustomer())
                        {
                            int y = 0;
                            foreach (var q in item.Invoices)
                            {
                                if (q.RegDate > Convert.ToDateTime(maskedTextBox1.Text) && q.RegDate < Convert.ToDateTime(maskedTextBox2.Text))
                                {
                                    y++;
                                }
                            }
                            chart1.Series["Series1"].Points.AddXY(item.Name, y);
                        }
                    }
                    else
                    {
                        MessageBox.Show("فیلد های تاریخ خالیست");
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("ایرادی در نگارش تاریخ یا موارد دیگر وجود دارد");
            }

        }
    }
}

