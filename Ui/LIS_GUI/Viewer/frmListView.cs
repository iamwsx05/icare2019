using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// �����ؼ�����������
    /// </summary>
    public partial class frmListView : Form
    {
        public frmListView()
        {
            InitializeComponent();
        }

        private void frmListView_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}