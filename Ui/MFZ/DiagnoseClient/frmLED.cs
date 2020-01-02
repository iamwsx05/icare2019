using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LEDInterface.LianCheng;

namespace DiagnoseClient
{
    public partial class frmLED : Form
    {
        private static frmLED ledForm=null;

        public static frmLED LEDForm(string p_text)
        {
            if (ledForm == null)
            {
                return new frmLED(p_text);
            }
            else
            {
                ledForm.text = p_text;
            }
            return ledForm;
        }

        private frmLED(string text)
        {
            InitializeComponent();
            this.text = text;
        }

        private string text;

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString("12345678", new System.Drawing.Font("ËÎÌו", 10.5F), Brushes.Red, 0, 0);
            g.Dispose();
            Image im = this.pictureBox1.Image;
            this.pictureBox1.Image = bmp;

            if (im != null)
            {
                im.Dispose();
            }
            this.pictureBox1.Refresh();
            g = Graphics.FromHwnd(this.pictureBox1.Handle);
            IntPtr hdc = g.GetHdc();
            byte res = Introp.sendtwp(hdc, (byte)1, (byte)1, 96, 16, (byte)5, (byte)50, (byte)5, (byte)0, (byte)0);
            g.ReleaseHdc();
            g.Dispose();

        }

        public void ShowText()
        {
            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawString(this.text, new System.Drawing.Font("ËÎÌו", 10.5F), Brushes.Red, 0, 0);
            g.Dispose();
            Image im = this.pictureBox1.Image;
            this.pictureBox1.Image = bmp;
            if (im != null)
            {
                im.Dispose();
            }
            this.pictureBox1.Refresh();
            g = Graphics.FromHwnd(this.pictureBox1.Handle);
            IntPtr hdc = g.GetHdc();
            byte res = Introp.sendtwp(hdc, (byte)1, (byte)1, 96, 16, (byte)5, (byte)50, (byte)5, (byte)0, (byte)0);
            g.ReleaseHdc();
            g.Dispose();
        }

        private void frmLED_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Size.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Size.Height);
            ShowText();
        }
    }
}