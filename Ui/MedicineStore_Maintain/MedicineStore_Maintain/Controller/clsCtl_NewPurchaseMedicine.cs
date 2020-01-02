using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using com.digitalwave.iCare.gui.MedicineStore;
using System.Windows.Forms; 
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ��ȡ��ҩ��ϸģ�������
    /// </summary>
    public class clsCtl_NewPurchaseMedicine : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_NewPurchaseMedicine m_objDomain = null;
        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore_Maintain.frmNewPurchaseMedicine m_objViewer;
        /// <summary>
        /// ҩƷ��ϸ���ݱ�
        /// </summary>  
        internal DataTable dtbResult = null;
        /// <summary>
        /// ҩƷ��ѯ�ؼ�
        /// </summary>
        private ctlQueryMedicintLeastElement m_ctlQueryMedicint = null;
        /// <summary>
        /// ��ѯ��Ӧ�̿ؼ�
        /// </summary>
        private ctlQueryVendor m_ctlQueryVendor = null;
        /// <summary>
        /// ��Ӧ�̣������̣�
        /// </summary>
        DataTable m_dtbVendor = null;
        #endregion  
      
        #region ���캯��.

        /// <summary>
        /// ҩƷ��������
        /// </summary>
        public clsCtl_NewPurchaseMedicine()
        {
            m_objDomain = new clsDcl_NewPurchaseMedicine();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������.
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmNewPurchaseMedicine)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡҩƷ��ϸ����
        /// <summary>
        /// ��ȡ�¹�ҩƷ��ϸ����
        /// ʵ��ͳ�Ʋ�ѯ����ϸ��ѯ���ܡ�
        /// �ɰ�ҩƷ�������롢ƴ���롢����롢ҩƷ��ID��ҩƷ���ƽ���ģ����ѯ
        /// </summary>
        internal void m_mthQuery()
        {
            if (m_objViewer.m_strStorageID == string.Empty)
            {
                MessageBox.Show("����ѡ��ҩ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_objViewer.m_dtpSearchBeginDate.Text.Trim().Length < 11)
                m_objViewer.m_dtpSearchBeginDate.Text = "";
            if (m_objViewer.m_dtpSearchEndDate.Text.Trim().Length < 11)
                m_objViewer.m_dtpSearchEndDate.Text = "";

            if ((m_objViewer.m_dtpSearchBeginDate.Text.Trim().Length == 11) && (m_objViewer.m_dtpSearchEndDate.Text.Trim().Length == 11))
            {
                if ((Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text)) > (Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text)))
                {
                    DialogResult tmpResult = MessageBox.Show("��ʼ���ڱ���С�ڽ������ڣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    m_objViewer.m_dtpSearchBeginDate.Focus();
                    return;
                }
            }

            long lngRes = 0;
            m_objViewer.m_dgvNewMedicine.DataSource = null;

            if (dtbResult != null)
            {
                dtbResult.Clear();
                dtbResult.Dispose();
                dtbResult = null;
            }

            m_objViewer.m_mthInitDataTable();
            System.Collections.Generic.List<string> al = new System.Collections.Generic.List<string>();
            al.Add(m_objViewer.m_strStorageID);
            DateTime m_datBeginTime = Convert.ToDateTime((Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text)).ToString("yyyy-MM-dd 00:00:00"));
            DateTime m_datEndTime = Convert.ToDateTime((Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text)).ToString("yyyy-MM-dd 23:59:59"));            
            al.Add(m_datBeginTime.ToString());
            al.Add(m_datEndTime.ToString());            
            al.Add(m_objViewer.m_txtMedicineName.Text.Trim());
            al.Add(m_objViewer.m_txtProviderName.Text);
            al.Add(m_objViewer.m_txtBillNumber.Text);
            if (m_objViewer.m_cboDoseType.SelectedIndex < 0 || m_objViewer.m_cboDoseType.SelectedIndex == 0)
            {
                al.Add("0");
            }
            else
            {
                clsMS_MedicineType_VO objVo = m_objViewer.m_cboDoseType.SelectedItem as clsMS_MedicineType_VO;
                al.Add(objVo.m_strMedicineTypeID_CHR);
            }

            dtbResult = new DataTable();

            lngRes = ((clsDcl_NewPurchaseMedicine)this.m_objDomain).m_lngGetNewPurchaseMedicine(al, out dtbResult);
            if ((lngRes > 0) && (dtbResult != null))
            {
                m_objViewer.m_dgvNewMedicine.DataSource = dtbResult;
            }           
        }
        #endregion

        #region ��ӡ
        internal void m_mthPrint()
        {
            if (dtbResult == null || dtbResult.Rows.Count <= 0)
            {
                MessageBox.Show("û�пɴ�ӡ���ݣ�", "��ӡ��ҩ֪ͨ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            frmNewPurchaseMedicineReport frmReport = new frmNewPurchaseMedicineReport();
            frmReport.p_dtbVal = dtbResult;
            clsDcl_RejectStorageReport dclTemp = new clsDcl_RejectStorageReport();
            string strStorageName = string.Empty;
            dclTemp.m_lngGetStoreRoomName(m_objViewer.m_strStorageID, out strStorageName);
            frmReport.p_strStorageName = strStorageName;
            string m_strBeginDate = Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text).ToString("yyyy��MM��dd��");
            string m_strEndDate = Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text).ToString("yyyy��MM��dd��");
            if (m_strBeginDate == m_strEndDate)
            {
                frmReport.p_strDate = "������ڣ�" + m_strBeginDate ;
            }
            else
            {
                frmReport.p_strDate = "������ڴӣ�" + m_strBeginDate + "��" + m_strEndDate;
            }
            frmReport.ShowDialog();
        }
        #endregion ��ӡ

        #region ����ҩƷ����
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        /// <param name="p_objMPVO"></param>
        internal void m_mthSetMedicineType(clsMS_MedicineType_VO[] p_objMPVO)
        {
            if (p_objMPVO == null || m_objViewer.m_cboDoseType.Items.Count > 0)
            {
                return;
            }

            clsMS_MedicineType_VO objAll = new clsMS_MedicineType_VO();
            objAll.m_strMedicineTypeID_CHR = "0";
            objAll.m_strMedicineTypeName_VCHR = "ȫ��";

            m_objViewer.m_cboDoseType.Items.Add(objAll);
            m_objViewer.m_cboDoseType.Items.AddRange(p_objMPVO);

            m_objViewer.m_cboDoseType.SelectedIndex = 0;
        }
        #endregion

        #region ��ȡָ���ֿ��ҩƷ����
        /// <summary>
        /// ��ȡָ���ֿ��ҩƷ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objMTVO">ҩƷ�Ƽ�����</param>
        internal void m_mthGetMedicineType(string p_strStorageID, out clsMS_MedicineType_VO[] p_objMTVO)
        {
            long lngRes = m_objDomain.m_mthGetMedicineType(p_strStorageID, out p_objMTVO);
        }
        #endregion

        #region ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// <summary>
        /// ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// </summary>
        internal DataTable m_mthGetMedicineInfo()
        {
            DataTable dtbResult;
            //clsInventoryRecordSVC objIRDomain = new clsInventoryRecordSVC();
            long lngRes = (new weCare.Proxy.ProxyDrug()).Service.m_lngGetBaseMedicine( string.Empty, m_objViewer.m_strStorageID, out dtbResult);
            return dtbResult;
        }
        #endregion

        #region ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// <summary>
        /// ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        /// <param name="m_dtMedicineInfo">��ѯ���</param>
        internal void m_mthShowQueryMedicineForm(string p_strSearchCon, DataTable m_dtMedicineInfo)
        {
            if (m_ctlQueryMedicint == null)
            {
                m_ctlQueryMedicint = new ctlQueryMedicintLeastElement(m_dtMedicineInfo);
                m_objViewer.Controls.Add(m_ctlQueryMedicint);

                int X = 0;
                int Y = 0;
                X = m_objViewer.m_txtMedicineName.Location.X;
                Y = m_objViewer.m_txtMedicineName.Location.Y + m_objViewer.m_txtMedicineName.Size.Height*2;

                m_ctlQueryMedicint.Location = new System.Drawing.Point(X, Y);

                m_ctlQueryMedicint.ReturnInfo += new ReturnMedicineInfo(frmQueryForm_ReturnInfo);
            }

            m_ctlQueryMedicint.Visible = true;
            m_ctlQueryMedicint.BringToFront();
            m_ctlQueryMedicint.Focus();
            m_ctlQueryMedicint.m_mthSetSearchText(p_strSearchCon);
        }

        internal void frmQueryForm_ReturnInfo( clsMS_MedicintLeastElement_VO MS_VO)
        {
            if (MS_VO == null)
            {
                return;
            }
            m_objViewer.m_txtMedicineName.Tag = MS_VO.m_strMedicineID;
            m_objViewer.m_txtMedicineName.Text = MS_VO.m_strMedicineName;
        }
        #endregion

        #region ��Ӧ�̲�ѯ
        /// <summary>
        /// ��ʾ��Ӧ�̲�ѯ
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowVendor(string p_strSearchCon)
        {
            if (m_ctlQueryVendor == null)
            {
                m_mthGetVendor(out m_dtbVendor);

                m_ctlQueryVendor = new ctlQueryVendor(m_dtbVendor);
                m_objViewer.Controls.Add(m_ctlQueryVendor);

                int X = m_objViewer.m_txtProviderName.Location.X;
                int Y = m_objViewer.m_txtProviderName.Location.Y + m_objViewer.m_txtProviderName.Size.Height*2;

                m_ctlQueryVendor.Location = new System.Drawing.Point(X, Y);
                m_ctlQueryVendor.ReturnInfo += new ReturnVendorInfo(QueryVendor_ReturnInfo);
            }
            m_ctlQueryVendor.BringToFront();
            m_ctlQueryVendor.m_mthSetSearchText(p_strSearchCon);
            m_ctlQueryVendor.Visible = true;
            m_ctlQueryVendor.Focus();
        }

        internal void QueryVendor_ReturnInfo( clsMS_Vendor MS_VO)
        {
            m_objViewer.m_txtProviderName.Tag = null;
            if (MS_VO == null)
            {
                return;
            }

            m_objViewer.m_txtProviderName.Tag = MS_VO.m_strVendorID;
            m_objViewer.m_txtProviderName.Text = MS_VO.m_strVendorName;
            m_objViewer.m_txtMedicineName.Focus();
        }

        /// <summary>
        /// ��ȡ��Ӧ����Ϣ
        /// </summary>
        /// <param name="p_dtbVendor"></param>
        internal void m_mthGetVendor(out DataTable p_dtbVendor)
        {
            clsDcl_InStorageStatisticsReport objIRDomain = new clsDcl_InStorageStatisticsReport();
            long lngRes = objIRDomain.m_lngGetVendor(out p_dtbVendor);
        }
        #endregion
    }
}
