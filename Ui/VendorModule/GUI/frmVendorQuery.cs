using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.VendorManage
{   
    /// <summary>
    /// 查询界面
    /// </summary>
    public partial class frmVendorQuery : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {   
         /// <summary>
         /// 构造函数
         /// </summary>
        public frmVendorQuery()
        {
            InitializeComponent();
          
        }

        private void m_btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        /// <summary>
        /// 主界面
        /// </summary>
        public frmVendorManage frmMain;     
        /// <summary>
        /// 信息
        /// </summary>
        public DataTable m_dtVendorInfo = null;        
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtl_VendorQuery();
            objController.Set_GUI_Apperance(this);
        }
        private void frmVendorQuery_Load(object sender, EventArgs e)
        {
            this.m_cbxType.SelectedIndex = 2;            
        }
        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ((clsCtl_VendorQuery)this.objController).m_mthGetVendorInfo();
            this.Cursor = Cursors.Default;
        }

        private void m_btnReSet_Click(object sender, EventArgs e)
        {
            TextBox tbxTemp = null;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType().FullName == "SourceLibrary.Windows.Forms.TextBoxTyped")
                {
                    tbxTemp = ctrl as TextBox;
                    tbxTemp.Clear();
                }
            }
            m_txtVendorName.Focus();
        }

        private void m_txtBillId_KeyDown(object sender, KeyEventArgs e)
        {
            ((clsCtl_VendorQuery)objController).m_mthSendTab(sender, e);
        }

        private void m_cboStatus_Enter(object sender, EventArgs e)
        {
            ((clsCtl_VendorQuery)objController).m_mthSendF4();
        }
    }
}