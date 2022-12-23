using BE;
using BLL;
using DevComponents.AdvTree;
using DevComponents.Editors.DateTimeAdv;
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

namespace CRMFinalProject
{
    public partial class ProductForm : Form
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
        public ProductForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        ProductCategory pc = new ProductCategory();
        ProductCategoryBLL PCbll = new ProductCategoryBLL();
        CountProductInAnbarBLL CPIAbll = new CountProductInAnbarBLL();
        AnbarCategoryBLL ANCbll = new AnbarCategoryBLL();
        ProductBLL bll = new ProductBLL();
        UserBLL Ubll = new UserBLL();
        Msgbox m = new Msgbox();

        List<AnbarCategory> AnbarCategories = new List<AnbarCategory>();
        List<CounProductInAnbar> CounProductInAnbars = new List<CounProductInAnbar>();
        void FillDataGrid1()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = AnbarCategories.ToList();
            dataGridViewX2.Columns["id"].Visible = false;
            dataGridViewX2.Columns["DeleteStatus"].Visible = false;
            dataGridViewX2.Columns["AnbarName"].HeaderText = "نام انبار";
            dataGridViewX2.Columns["AnbarAdress"].HeaderText = "آدرس انبار";
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
            textBoxX5.Text = "";
            textBoxX6.Text = "";
            textBoxX7.Text = "";
            textBoxX8.Text = "";
            dataGridViewX2.DataSource = null;
            textBoxX5.Enabled = true;
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX4.Text == "")
            {
                MessageBox.Show("لطفا ابتدا نام محصول را وارد کنید");
                isvalid = false;
                textBoxX4.Focus();
            }
            if (textBoxX5.Text == "")
            {
                MessageBox.Show("لطفا دسته بندی محصول را وارد کنید");
                isvalid = false;
                textBoxX5.Focus();
            }
            else if (textBoxX1.Text == "")
            {
                MessageBox.Show("لطفا موجودی محصول را مشخص کنید");
                isvalid = false;
                textBoxX1.Focus();
            }
            else if (textBoxX2.Text == "")
            {
                MessageBox.Show("لطفا قیمت تکی محصول را مشخص کنید");
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX6.Text == "")
            {
                MessageBox.Show("لطفا قیمت عمده محصول را مشخص کنید");
                isvalid = false;
                textBoxX6.Focus();
            }
            return isvalid;
        }

        private void label11_Click(object sender, EventArgs e)
        {
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
                double sumi = 0;
                foreach (var item in CounProductInAnbars)
                {
                    sumi = sumi + item.count;
                }
                Product p = new Product();
                p.Name = textBoxX4.Text;
                p.Price = Convert.ToDouble(textBoxX2.Text);
                p.Price1 = Convert.ToDouble(textBoxX6.Text);
                p.Stock = Convert.ToInt16(textBoxX1.Text);
                if (label5.Text == "ثبت اطلاعات")
                {
                    if (Ubll.Access(Lu, "بخش کالاها", 2))
                    {
                        if (sumi == p.Stock)
                        {
                            int idd = bll.Create(p, pc, CounProductInAnbars).id;
                            //MessageBox.Show(bll.Create(p, pc,CounProductInAnbars));
                        }
                        else
                        {
                            MessageBox.Show("تعداد کل موجودی با مجموع تعداد موجودی در هر انبار برابر نمیباشد");
                        }
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت کالا را ندارید", "", false, true);
                    }
                }
                else
                {
                    pc = PCbll.Read(textBoxX5.Text);
                    if (pc != null)
                    {
                        MessageBox.Show(bll.Update(p, pc, id));
                        textBoxX7.Enabled = true;
                        textBoxX8.Enabled = true;
                        textBoxX1.Enabled = true;
                        pictureBox3.Enabled = true;
                        label5.Text = "ثبت اطلاعات";
                    }
                    else
                    {
                        textBoxX5.Text = "";
                        MessageBox.Show("دسته بندی رو انتخاب نمایید");
                    }
                }
                FillDataGrid();
                ClearTxtBoxs();
                AnbarCategories.Clear();
            }
            CounProductInAnbars.Clear();
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            dataGridViewX2.DataSource = null;
            textBoxX7.Text = "";
            textBoxX8.Text = "";
        }

        int index;
        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked && checkBox3.Checked || (!checkBox3.Checked && !checkBox4.Checked))
            {
                index = 0;
            }
            else if (checkBox4.Checked && !checkBox3.Checked)
            {
                index = 1;
            }
            else if (checkBox3.Checked && !checkBox4.Checked)
            {
                index = 2;
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX3.Text, index);
            dataGridViewX1.Columns["id"].Visible = false;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            FillDataGrid1();
            AutoCompleteStringCollection PCnames = new AutoCompleteStringCollection();
            foreach (var item in PCbll.ReadNamesPC())
            {
                PCnames.Add(item);
            }
            textBoxX5.AutoCompleteCustomSource = PCnames;

            AutoCompleteStringCollection ANCNames = new AutoCompleteStringCollection();
            foreach (var item in ANCbll.ReadNamesANC())
            {
                ANCNames.Add(item);
            }
            textBoxX7.AutoCompleteCustomSource = ANCNames;
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

            if (Ubll.Access(Lu, "بخش کالاها", 3))
            {
                Product p = bll.Read(id);
                textBoxX4.Text = p.Name;
                //textBoxX5.Text = p.Category;
                textBoxX2.Text = Convert.ToString(p.Price);
                textBoxX6.Text = Convert.ToString(p.Price1);
                textBoxX1.Text = Convert.ToString(p.Stock);
                textBoxX5.Text = p.Category.CategoryNameP;
                textBoxX7.Enabled = false;
                textBoxX8.Enabled = false;
                textBoxX1.Enabled = false;
                pictureBox3.Enabled = false;
                label5.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش کالا را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش کالاها", 4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف کالا اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show(bll.Delete(id));
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف کالا را ندارید", "", false, true);
            }

        }

        private void ProductForm_KeyUp(object sender, KeyEventArgs e)
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pc = PCbll.Read(textBoxX5.Text);
            if (pc!=null)
            {
                textBoxX5.Enabled = false;
            }
            else
            {
                textBoxX5.Text = "";
                m.MyShowDialog("اخطار", "مشتری با این نام در بانک اطلاعاتی وجود ندارد", "", false, true);
            }

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CounProductInAnbar cpa = new CounProductInAnbar();
            cpa.anbarCategoryP = ANCbll.Read(textBoxX7.Text);
            AnbarCategory an = cpa.anbarCategoryP;
            if (textBoxX8.Text != "")
            {
                cpa.count = Convert.ToInt16(textBoxX8.Text);
            }
            else
            {
                m.MyShowDialog("اخطار", "فیلد تعداد خالیست", "", false, true);
            }
            if (an != null && cpa != null && !AnbarCategories.Contains(an))
            {
                AnbarCategories.Add(an);
                CounProductInAnbars.Add(cpa);
                FillDataGrid1();
                textBoxX7.Text = "";
                textBoxX8.Text = "";
                string s = cpa.count + " عدد از کالا در انبار " + an.AnbarName;
                listBox1.Items.Add(s);
            }
            else
            {
                m.MyShowDialog("اخطار", "انبار با این نام در بانک اطلاعاتی وجود ندارد", "", false, true);
                textBoxX7.Text = "";
                textBoxX8.Text = "";
            }
        }

        private void نمایشجزییاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CounProductInAnbar> cpin = new List<CounProductInAnbar>();
            CountProductInAnbarForm cpinf = new CountProductInAnbarForm();
            cpin = CPIAbll.GetCountProductInAnbarByProductId(id).ToList();
            List<ProductInAnbarViewModel> piavml = new List<ProductInAnbarViewModel>();
            foreach (var item in cpin)
            {
                ProductInAnbarViewModel piavm = new ProductInAnbarViewModel();
                piavm.id = item.id;
                piavm.NameProduct = item.productP.Name;
                piavm.NameAnbar = item.anbarCategoryP.AnbarName;
                piavm.AdressAnbar = item.anbarCategoryP.AnbarAdress;
                piavm.count = item.count;

                piavml.Add(piavm);
            }
            cpinf.dataGridViewX1.DataSource = null;
            cpinf.dataGridViewX1.DataSource = piavml;
            cpinf.dataGridViewX1.Columns["id"].Visible = false;
            cpinf.dataGridViewX1.Columns["NameProduct"].HeaderText = "نام محصول";
            cpinf.dataGridViewX1.Columns["NameAnbar"].HeaderText = "نام انبار";
            cpinf.dataGridViewX1.Columns["AdressAnbar"].HeaderText = "آدرس انبار";
            cpinf.dataGridViewX1.Columns["count"].HeaderText = "تعداد محصول";
            cpinf.ShowDialog();
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
    }
}

