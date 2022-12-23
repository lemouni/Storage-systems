using BE;
using BLL;
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
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class HavaleKhorojForm : Form
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
        public HavaleKhorojForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        Msgbox m = new Msgbox();

        CountProductBLL CPbll = new CountProductBLL();
        Buy_CountProductBLL BCPbll = new Buy_CountProductBLL();
        UserBLL Ubll = new UserBLL();

        int id;
        User Lu = new User();

        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = CPbll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        void FillDataGrid1()
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = BCPbll.Read();
            dataGridViewX2.Columns["id"].Visible = false;
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells[0].Value);
            }
            catch (Exception)
            {

                m.MyShowDialog("", "این سطر خالی می باشد", "", false, true);
            }
        }
        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX2.Rows[dataGridViewX2.CurrentRow.Index].Cells[0].Value);
            }
            catch (Exception)
            {

                m.MyShowDialog("", "این سطر خالی می باشد", "", false, true);
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = CPbll.Read(textBoxX1.Text);
            dataGridViewX1.Columns["id"].Visible = false;
        }
        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource = null;
            dataGridViewX2.DataSource = BCPbll.Read(textBoxX2.Text);
            dataGridViewX2.Columns["id"].Visible = false;
        }

        private void ثبتمرجوعیToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حواله خروج", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت خروج تمام تعداد از انبار مربوطه کاسته می شود\nآیا قصد ثبت خروج را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", CPbll.SabtKhorojoDone(Lu, id), "", false, false);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت خروج را ندارید", "", false, true);
            }
        }

        private void HavaleKhorojForm_Load(object sender, EventArgs e)
        {
            FillDataGrid();
            FillDataGrid1();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ثبتمرجوعیToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حواله خروج", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت مرجوعی تمام تعداد به انبار مربوطه اضافه می شود\nآیا قصد مرجوع را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", CPbll.MarjoDone(Lu, id), "",false,false);
                }
                FillDataGrid();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت مرجوع را ندارید", "", false, true);
            }
        }

        private void ثبتورودکالاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حواله خروج", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت ورود تمام تعداد به انبار مربوطه اضافه می شود\nآیا قصد ثبت ورود را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", BCPbll.SabtVorodDone(Lu, id), "", false, false);
                }
                FillDataGrid1();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت ورود را ندارید", "", false, true);
            }
        }

        private void ثبتمرجوعیToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().FirstOrDefault();
            Lu = w.LoggedInUser;

            if (Ubll.Access(Lu, "حواله خروج", 3))
            {
                DialogResult dr = m.MyShowDialog("اخطار", "در صورت ثبت مرجوعی تمام تعداد از انبار مربوطه کاسته می شود\nآیا قصد مرجوع را دارید؟", "", true, false);



                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("اطلاعیه", BCPbll.MarjoDone(Lu, id), "", false, false);
                }
                FillDataGrid1();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ثبت مرجوع را ندارید", "", false, true);
            }
        }

        private void HavaleKhorojForm_KeyUp(object sender, KeyEventArgs e)
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
    }
}
