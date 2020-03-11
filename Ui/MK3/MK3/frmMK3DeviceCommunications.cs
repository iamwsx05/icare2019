using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public partial class frmMK3DeviceCommunications :com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        /// ����ͨѶ���Ʋ�
        /// </summary>
        clsCtl_MK3DeviceCommunications m_objController;
        internal string m_strCheckItemID = null;
        #endregion

        #region �������Ʋ�
        public override void CreateController()
        {
            m_objController = new clsCtl_MK3DeviceCommunications();
            m_objController.Set_GUI_Apperance(this);
            objController = m_objController;
        }
        #endregion

        #region ���캯��
        public frmMK3DeviceCommunications(string strCheckItemID)
        {
            InitializeComponent();
            m_strCheckItemID = strCheckItemID;
        }
        #endregion

        private void frmMK3DeviceCommunications_Load(object sender, EventArgs e)
        {
            m_objController.m_mthGetCheckItemCustomOrder();
        }

        private void m_btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            m_objController.m_mthOperationCheckItemCustomOrder();
        }

        private void m_blnDelete_Click(object sender, EventArgs e)
        {
            m_objController.m_mthDeleteCheckItemCustomOrder();
        }
    }
}