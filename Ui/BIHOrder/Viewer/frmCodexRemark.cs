using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmCodexRemark : Form
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_CENTER = 0x0010;
        const int AW_ACTIVATE = 0x20000;

        private int interval = 0;       
        private string remark = "";
        
        public frmCodexRemark(string Remark, int Interval)
        {
            InitializeComponent();

            remark = Remark;
            interval = Interval;
        }

        private void frmCodexRemark_Load(object sender, EventArgs e)
        {
            AnimateWindow(this.Handle, 50, AW_CENTER | AW_ACTIVATE);

            this.txtInfo.Text = remark;

            this.timer1.Interval = interval * 1000;
            this.timer1.Enabled = true;            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}