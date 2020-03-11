using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.DataExchangeSystem
{
    public partial class frmAsk : Form
    {
        private int i=0;

        public frmAsk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label2.Text = (30-i).ToString();
            if (i == 30)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}