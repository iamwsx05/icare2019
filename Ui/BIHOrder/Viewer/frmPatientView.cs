using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data; 
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmPatientChargeView : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
    {
        /// <summary>
        /// ������ˮ�ǼǺ�
        /// </summary>
        private string m_strRegisterId = "";
        public frmPatientChargeView()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public frmPatientChargeView(string RegisterId)
        {
            m_strRegisterId = RegisterId;
            InitializeComponent();
        }

        private void frmPatientView_Load(object sender, EventArgs e)
        {
            ucPatientInfo1.Status = 1;
            ucPatientInfo1.m_mthFind(m_strRegisterId, 3);
        }

        private void frmPatientChargeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}