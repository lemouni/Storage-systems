using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CRMFinalProject
{
    public class Msgbox
    {
        
        public DialogResult MyShowDialog(string title, string FaInfo , string EngInfo , bool buttons, bool type)
        {
            MyMsgbox m = new MyMsgbox();
            m.label1.Text = title;
            m.label2.Text = FaInfo;
            m.label3.Text = EngInfo;

            if (buttons)
            {
                m.buttonX2.Text = "خیر";
            }
            else
            {
                m.buttonX1.Visible = false;
            }

            if (type)
            {
                m.BackColor = Color.FromArgb(242, 69, 29);
                m.pictureBox1.Image = Properties.Resources.Danger;
                //در کد بالا عکسی که میخایم بزاریم باید عکس ارور باشد ولی فعلا bell
;            }

            m.ShowDialog();
            return m.DialogResult;
;        }
    }
}
