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
using System.Windows.Controls;

namespace CRMFinalProject
{
    public partial class SettingForm : Form
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
        public SettingForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        int id;
        ApiSmsBLL ASbll = new ApiSmsBLL();
        MoshkhasatFactorBLL MFbll = new MoshkhasatFactorBLL();
        CategoriNoeBLL CNbll = new CategoriNoeBLL();
        CountProductInAnbarBLL CPIAbll = new CountProductInAnbarBLL();
        ActivityCategorBLL  bll = new ActivityCategorBLL();
        ProductCategoryBLL PCbll = new ProductCategoryBLL();
        AnbarCategoryBLL ANCbll = new AnbarCategoryBLL();
        UserBLL Ubll = new UserBLL();

        /// <summary>
        /// برای مسیج باکس شخصی سازی شده
        /// </summary>
        Msgbox msgbox = new Msgbox();

        void FillDataGrid()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = bll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs()
        {
            
            textBoxX4.Text = "";
        }

        void FillDataGrid1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = PCbll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs1()
        {

            textBoxX1.Text = "";
        }

        void FillDataGrid2()
        {
            dataGridViewX3.DataSource = null;
            dataGridViewX3.DataSource = ANCbll.Read();
            dataGridViewX3.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs2()
        {

            textBoxX2.Text = "";
            textBoxX3.Text = "";
        }

        void FillDataGrid3()
        {
            dataGridViewX4.DataSource = null;
            dataGridViewX4.DataSource = CNbll.Read();
            dataGridViewX4.Columns["id"].Visible = false;
        }
        void ClearTxtBoxs3()
        {

            textBoxX5.Text = "";
        }

        bool CHEKed()
        {
            bool isvalid = true;
            if (textBoxX4.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا دسته فعالیت را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX4.Focus();
            }
            return isvalid;
        }
        bool CHEKed1()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا دسته محصولات را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX1.Focus();
            }
            return isvalid;
        }

        bool CHEKed2()
        {
            bool isvalid = true;
            if (textBoxX2.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا نام انبار را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX2.Focus();
            }
            else if (textBoxX3.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا آدرس انبار را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX3.Focus();
            }
            return isvalid;
        }

        bool CHEKed3()
        {
            bool isvalid = true;
            if (textBoxX5.Text == "")
            {
                msgbox.MyShowDialog("اخطار", "لطفا ابتدا نوع پرداختی را وارد کنید", "", false, true);
                isvalid = false;
                textBoxX5.Focus();
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

        private void SettingForm_KeyUp(object sender, KeyEventArgs e)
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
        User Lu = new User();
        private void label1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed())
            {
                ActivityCategory ac = new ActivityCategory();
                ac.CategoryName = textBoxX4.Text;

                if (label1.Text == "ثبت نوع فعالیت جدید")
                {
                    if (Ubll.Access(Lu, "بخش تنظیمات", 2))
                    {
                        MessageBox.Show(bll.Create(ac));
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت دسته بندی فعالیت را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(bll.Update(ac, id));
                    label1.Text = "ثبت نوع فعالیت جدید";
                }
                FillDataGrid();
                ClearTxtBoxs();
            }

        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            FillDataGrid1();
            FillDataGrid2();
            FillDataGrid3();
            label5.Text = MFbll.ReadNameForosghah();
            label6.Text = ASbll.ReadApiKay();
            label7.Text = ASbll.ReadSecretKay();
            label8.Text = ASbll.ReadKhat();
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

        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 3))
            {
                ActivityCategory ac = bll.Read(id);
                textBoxX4.Text = ac.CategoryName;

                label1.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش دسته بندی فعالیت را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 4))
            {
                //DialogResult dr = MessageBox.Show("آیا از حذف مشتری اطمینان دارید؟", "اخطار", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //کد بالا برای مسیج باکس معمولی و کد پایین برای مسیج باکس شخصی سازی شده است

                DialogResult dr = msgbox.MyShowDialog("اخطار", "آیا قصد حذف نوع فعالیت را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    bll.Delete(id);
                }
                FillDataGrid();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف دسته بندی فعالیت را ندارید", "", false, true);
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["id"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed1())
            {
                ProductCategory pc = new ProductCategory();
                pc.CategoryNameP = textBoxX1.Text;

                if (label2.Text == "ثبت نوع دسته بندی جدید")
                {
                    if (Ubll.Access(Lu, "بخش تنظیمات", 2))
                    {
                        MessageBox.Show(PCbll.Create(pc));
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت دسته بندی محصولات را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(PCbll.Update(pc, id));
                    label2.Text = "ثبت نوع دسته بندی جدید";
                }
                FillDataGrid1();
                ClearTxtBoxs1();
            }
        }

        private void ویرایشToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 3))
            {
                ProductCategory pc = PCbll.Read(id);
                textBoxX1.Text = pc.CategoryNameP;

                label2.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش دسته بندی محصولات را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 4))
            {
                DialogResult dr = msgbox.MyShowDialog("اخطار", "آیا قصد حذف نوع دسته بندی را دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    PCbll.Delete(id);
                }
                FillDataGrid1();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف دسته بندی محصولات را ندارید", "", false, true);
            }
        }

        private void dataGridViewX3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip3.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX3.Rows[dataGridViewX3.CurrentRow.Index].Cells["id"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed2())
            {
                AnbarCategory anc = new AnbarCategory();
                anc.AnbarName = textBoxX2.Text;
                anc.AnbarAdress = textBoxX3.Text;

                if (label3.Text == "ثبت نام انبار")
                {
                    if (Ubll.Access(Lu, "بخش تنظیمات", 2))
                    {
                        MessageBox.Show(ANCbll.Create(anc));
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت انبار را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(ANCbll.Update(anc, id));
                    label3.Text = "ثبت نام انبار";
                }
                FillDataGrid2();
                ClearTxtBoxs2();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 3))
            {
                AnbarCategory anc = ANCbll.Read(id);
                textBoxX2.Text = anc.AnbarName;
                textBoxX3.Text = anc.AnbarAdress;

                label3.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش انبار محصولات را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 4))
            {
                DialogResult dr = msgbox.MyShowDialog("اخطار", "آیا قصد حذف انبار را دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    ANCbll.Delete(id);
                }
                FillDataGrid2();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف انبار محصولات را ندارید", "", false, true);
            }
        }

        private void نمایشمحصولاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CounProductInAnbar> cpin = new List<CounProductInAnbar>();
            ProductsInAnbarForm psif = new ProductsInAnbarForm();
            cpin = CPIAbll.GetProductsInAnbarByanbarcategorId(id).ToList();
            List<ProducthaieAnbarViewModel> phavm = new List<ProducthaieAnbarViewModel>();
            foreach (var item in cpin)
            {
                ProducthaieAnbarViewModel pvm = new ProducthaieAnbarViewModel();
                pvm.id = item.id;
                pvm.nameanbar = item.anbarCategoryP.AnbarName;
                pvm.nameproduct = item.productP.Name;
                pvm.count = item.count;
                pvm.totalstock = item.productP.Stock;

                phavm.Add(pvm);
            }
            psif.dataGridViewX1.DataSource = null;
            psif.dataGridViewX1.DataSource = phavm;
            psif.dataGridViewX1.Columns["id"].Visible = false;
            psif.dataGridViewX1.Columns["nameanbar"].HeaderText = "نام انبار";
            psif.dataGridViewX1.Columns["nameproduct"].HeaderText = "نام محصول";
            psif.dataGridViewX1.Columns["count"].HeaderText = "تعداد در این انبار";
            psif.dataGridViewX1.Columns["totalstock"].HeaderText = "تعداد در کل انبار ها";
            psif.ShowDialog();
        }

        private void dataGridViewX4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip4.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX4.Rows[dataGridViewX4.CurrentRow.Index].Cells["id"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("این سطر خالی می باشد");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (CHEKed3())
            {
                CategoriNoe cn = new CategoriNoe();
                cn.name = textBoxX5.Text;

                if (label4.Text == "ثبت نوع پرداخت جدید")
                {
                    if (Ubll.Access(Lu, "بخش تنظیمات", 2))
                    {
                        MessageBox.Show(CNbll.Create(cn));
                    }
                    else
                    {
                        msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت نوع پرداخت را ندارید", "", false, true);
                    }
                }
                else
                {
                    MessageBox.Show(CNbll.Update(cn, id));
                    label4.Text = "ثبت نوع پرداخت جدید";
                }
                FillDataGrid3();
                ClearTxtBoxs3();
            }
        }

        private void آپدیتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 3))
            {
                CategoriNoe cn = CNbll.Read(id);
                textBoxX5.Text = cn.name;

                label4.Text = "ویرایش اطلاعات";
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه ویرایش نوع پرداخت را ندارید", "", false, true);
            }
        }

        private void حذفToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "بخش تنظیمات", 4))
            {
                DialogResult dr = msgbox.MyShowDialog("اخطار", "آیا قصد حذف نوع پرداخت را دارید؟", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    CNbll.Delete(id);
                }
                FillDataGrid3();
            }
            else
            {
                msgbox.MyShowDialog("محدودیت دسترسی", "شما اجازه حذف نوع پرداخت را ندارید", "", false, true);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX6.Text!="")
            {
                MoshkhasatFactor m = new MoshkhasatFactor();
                m.NameForoshgah = textBoxX6.Text;
                MessageBox.Show(MFbll.Create(m));
            }
            else
            {
                MessageBox.Show("فیلد خالی میباشد");
            }
            textBoxX6.Text = "";
            label5.Text = MFbll.ReadNameForosghah();

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (textBoxX7.Text != "" && textBoxX8.Text != "" && textBoxX9.Text != "")
            {
                ApiSms a = new ApiSms();
                a.UserApiKey = textBoxX7.Text;
                a.SecretKey = textBoxX8.Text;
                a.Khat = textBoxX9.Text;
                MessageBox.Show(ASbll.Create(a));
            }
            else
            {
                MessageBox.Show("فیلد خالی میباشد");
            }
            textBoxX7.Text = "";
            textBoxX8.Text = "";
            textBoxX9.Text = "";

            label6.Text = ASbll.ReadApiKay();
            label7.Text = ASbll.ReadSecretKay();
            label8.Text = ASbll.ReadKhat();
        }
    }
}
