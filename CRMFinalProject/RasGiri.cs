using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class RasGiri : Form
    {
        public RasGiri()
        {
            InitializeComponent();
        }
        Msgbox m = new Msgbox();
        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int Subtract(string Date2, string Date1)

        {

            System.Globalization.PersianCalendar date = new

           System.Globalization.PersianCalendar();

            //------------------------------------------------------------------------------------------------------

            int year2 = int.Parse(Date2[0].ToString() + Date2[1].ToString() +

            Date2[2].ToString() + Date2[3].ToString());

            int mount2 = int.Parse(Date2[5].ToString() + Date2[6].ToString());

            int day2 = int.Parse(Date2[8].ToString() + Date2[9].ToString());

            //------------------------------------------------------------------------------------------------------

            int year1 = int.Parse(Date1[0].ToString() + Date1[1].ToString() +

            Date1[2].ToString() + Date1[3].ToString());

            int mount1 = int.Parse(Date1[5].ToString() + Date1[6].ToString());

            int day1 = int.Parse(Date1[8].ToString() + Date1[9].ToString());

            //------------------------------------------------------------------------------------------------------

            DateTime dat2 = date.ToDateTime(year2, mount2, day2, 0, 0, 0, 0);

            DateTime dat1 = date.ToDateTime(year1, mount1, day1, 0, 0, 0, 0);

            TimeSpan ts = dat2.Subtract(dat1);

            return ts.Days;

        }
        List<double> dd = new List<double>();
        List<double> d = new List<double>();

        private void btnOk_Click(object sender, EventArgs e)
        {
            int SubDay = Subtract(tbDate2.Text, tbDate1.Text);
            double Mablagh = Convert.ToDouble(textBox1.Text);
            dd.Add(Mablagh);
            double cc = dd.Sum();
            string s = " تعداد روز " + SubDay.ToString() + " به مبلغ " + Mablagh.ToString();
            double b = (SubDay * Mablagh);
            listBox1.Items.Add(s);
            d.Add(b);
            
            listBox2.Items.Add(b);
            double c = d.Sum();
            label5.Text = c.ToString();
            label6.Text = (c / cc).ToString();
            tbDate1.Text = null;
            tbDate2.Text = null;
            textBox1.Text = "";

        }

        private void label4_Click(object sender, EventArgs e)
        {
            label5.Text = null;
            label6.Text = null;
            tbDate1.Text = null;
            tbDate2.Text = null;
            textBox1.Text = "";
            d.Clear();
            dd.Clear();
            listBox1.DataSource = null;
            listBox2.DataSource = null;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar < '0') || (e.KeyChar > '9'))
                e.Handled = true;
        }

        private void RasGiri_KeyUp(object sender, KeyEventArgs e)
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
