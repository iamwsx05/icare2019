using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �����ֻ����ŷ����ײͶ���
    /// </summary>
    public partial class frmMobileServiceOrder : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// �����ֻ����ŷ����ײͶ���
        /// </summary>
        public frmMobileServiceOrder()
        {
            InitializeComponent();
        }                

        private void frmMobileServiceOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}