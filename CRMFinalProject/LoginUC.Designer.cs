namespace CRMFinalProject
{
    partial class LoginUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.symbolBox2 = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // symbolBox2
            // 
            // 
            // 
            // 
            this.symbolBox2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.symbolBox2.Location = new System.Drawing.Point(215, 3);
            this.symbolBox2.Name = "symbolBox2";
            this.symbolBox2.Size = new System.Drawing.Size(91, 95);
            this.symbolBox2.Symbol = "";
            this.symbolBox2.TabIndex = 5;
            this.symbolBox2.Text = "symbolBox2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("W_yekan Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(122, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "برای ورود به حساب اطلاعات خود را وارد نمایید";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.textBoxX3);
            this.panel1.Controls.Add(this.textBoxX1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Font = new System.Drawing.Font("W_yekan Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 146);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 198);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(78, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 36);
            this.panel2.TabIndex = 9;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CRMFinalProject.Properties.Resources.account;
            this.pictureBox2.Location = new System.Drawing.Point(3, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(35, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 21);
            this.label11.TabIndex = 4;
            this.label11.Text = "ورود به حساب";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // textBoxX3
            // 
            this.textBoxX3.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.textBoxX3.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX3.Location = new System.Drawing.Point(78, 92);
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.PasswordChar = '*';
            this.textBoxX3.PreventEnterBeep = true;
            this.textBoxX3.Size = new System.Drawing.Size(343, 36);
            this.textBoxX3.TabIndex = 7;
            this.textBoxX3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX3.WatermarkText = "کلمه عبور";
            // 
            // textBoxX1
            // 
            this.textBoxX1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.textBoxX1.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX1.Location = new System.Drawing.Point(78, 53);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(343, 36);
            this.textBoxX1.TabIndex = 6;
            this.textBoxX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX1.WatermarkText = "نام کاربری";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("IRANSansWeb(FaNum) Medium", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.Location = new System.Drawing.Point(189, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "فرم اطلاعات کاربری برای ورود";
            // 
            // LoginUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.symbolBox2);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Name = "LoginUC";
            this.Size = new System.Drawing.Size(500, 462);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SymbolBox symbolBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label11;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private System.Windows.Forms.Label label3;
    }
}
