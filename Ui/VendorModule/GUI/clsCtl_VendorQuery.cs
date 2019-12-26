using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace com.digitalwave.iCare.gui.VendorManage
{   
    /// <summary>
    /// ��ѯ������
    /// </summary>
    public class clsCtl_VendorQuery : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// ������
        /// </summary>
        private frmVendorQuery m_objViewer;
        /// <summary>
        /// ����������Ʋ�
        /// </summary>
        private clsDomainControlVendor m_objDomain;
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_VendorQuery()
        {
            m_objDomain = new clsDomainControlVendor();
        }
        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmVendorQuery)frmMDI_Child_Base_in;
        }
        #endregion
                
        /// <summary>
        /// ��������������ѯ��Ϣ
        /// </summary>
        public void m_mthGetVendorInfo()
        {           
            DataTable dtVendorInfo = new DataTable();          
            int m_intStatus = this.m_objViewer.m_cbxType.SelectedIndex + 1;

            try
            {
                long lngRes = this.m_objDomain.m_lngGetVendorInfo(m_objViewer.m_txtUSERCODE.Text,m_objViewer.m_txtVendorName.Text,m_objViewer.m_txtAliasName.Text,
                    m_intStatus,m_objViewer.m_txtPhone.Text,m_objViewer.m_txtAddress.Text,m_objViewer.m_txtContactor.Text,m_objViewer.m_txtContactorPhone.Text,
                    m_objViewer.m_txtFax.Text,m_objViewer.m_txtEmail.Text,m_objViewer.m_txtPyCode.Text,m_objViewer.m_txtWbCode.Text,out dtVendorInfo);               
                if (lngRes > 0)
                {
                    if (dtVendorInfo.Rows.Count > 0)
                    {
                        this.m_objViewer.m_dtVendorInfo = dtVendorInfo;
                        this.m_objViewer.Close();
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("��ѯ�������������������Ϣ���Ƿ������ѯ��", "��ѯ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        if (dr == DialogResult.No)
                        {
                            this.m_objViewer.Close();
                        }
                    }
                }
            }
            finally
            {
                dtVendorInfo.Dispose();
            }            
        }

        /// <summary>
        /// ����Tab��
        /// </summary>
        public void m_mthSendTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        /// <summary>
        /// ����F4��
        /// </summary>
        public void m_mthSendF4()
        {
            System.Windows.Forms.SendKeys.Send("{F4}");
            System.Windows.Forms.SendKeys.Send("{Down}");
        }
    }
}
