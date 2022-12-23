using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class DarbareMA : Form
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
        public DarbareMA()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Process.Start("https://webyarseo.com/%d9%86%d8%b1%d9%85-%d8%a7%d9%81%d8%b2%d8%a7%d8%b1-%d8%ad%d8%b3%d8%a7%d8%a8%d8%af%d8%a7%d8%b1%db%8c-%d9%88-crm-%d9%85%d8%af%db%8c%d8%b1%db%8c%d8%aa-%d9%85%d8%b4%d8%aa%d8%b1%db%8c%d8%a7%d9%86/");
        }
    }
}
