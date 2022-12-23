using BE;
using BLL;
using HandyControl.Controls;
using SmsIrRestful;
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
    public partial class SMSPanelForm : Form
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
        public SMSPanelForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        Msgbox m = new Msgbox();
        ApiSmsBLL APbll = new ApiSmsBLL();
        SmsBLL Sbll = new SmsBLL();
        private void label11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        CustomerBLL Cbll = new CustomerBLL();
        void Fill1()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Sbll.Read();
            dataGridViewX1.Columns["id"].Visible = false;
        }
        private void SMSPanelForm_Load(object sender, EventArgs e)
        {
            Fill1();



            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadPhoneNumbers())
            {
                phone.Add(item);
            }
            textBoxX3.AutoCompleteCustomSource = phone;
        }
        public List<string> Numberss = new List<string>();
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Numberss.Add(textBoxX3.Text);
            listBox1.DataSource = null;
            listBox1.DataSource = Numberss;
            textBoxX3.Text = "";
        }
        string payam;
        string UserApiKey;
        string SecretKey;
        string Khat;
        private void label2_Click(object sender, EventArgs e)
        {
            payam = richTextBox2.Text;
            UserApiKey = APbll.ReadApiKay();
            SecretKey = APbll.ReadSecretKay();
            Khat = APbll.ReadKhat();

            var token = new Token().GetToken(UserApiKey, SecretKey);

            var messageSendObject = new MessageSendObject()
            {
                Messages = new List<string> { payam }.ToArray(),
                MobileNumbers =Numberss.ToArray(),
                LineNumber = Khat,
                SendDateTime = null,
                CanContinueInCaseOfError = true
            };
            MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, messageSendObject);
            if (messageSendResponseObject.IsSuccessful)
            {
                Sms s = new Sms();
                s.Payam = richTextBox2.Text;
                m.MyShowDialog("تاییدیه", "ارسال پیامک با موفقیت انجام شد", "", false, true);
                m.MyShowDialog("تاییدیه", Sbll.Create(s), "", false, true);
                Fill1();

            }
            else
            {
                m.MyShowDialog("اخطاریه", messageSendResponseObject.Message, "", false, true);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Numberss.Clear();
            listBox1.DataSource = null;
            richTextBox2.Text = "";
        }
    }
}
