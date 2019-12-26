using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.iCare.gui.MedicineStore;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ��ҩ��λ��ҩ��ϸ
    /// </summary>
    class clsCtl_VendorSupplyDetail : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��ҩ��λ��ҩ��ϸ�м��������
        /// </summary>
        private clsDcl_VendorSupplyDetail m_objDomain = null;
        /// <summary>
        /// ��ҩ��λ��ҩ��ϸ����
        /// </summary>
        private frmVendorSupplyDetail m_objViewer = null;
        /// <summary>
        /// ��ѯ��Ӧ�̿ؼ�
        /// </summary>
        private ctlQueryVendor m_ctlQueryVendor = null;
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        private DataTable m_dtbVendor = null;
        #endregion 

        #region ���캯��
        /// <summary>
        /// ��ҩ��λ��ҩ��ϸ�����߼�����
        /// </summary>
        public clsCtl_VendorSupplyDetail()
        {
            m_objDomain = new clsDcl_VendorSupplyDetail();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmVendorSupplyDetail)frmMDI_Child_Base_in;
        }
        #endregion

        #region ͳ�ƹ�ҩ��λ��ҩ��ϸ
        /// <summary>
        /// ͳ�ƹ�ҩ��λ��ҩ��ϸ
        /// </summary>
        internal void m_mthStatistics()
        {
            DateTime dtmBegin = DateTime.Parse(Convert.ToDateTime(m_objViewer.m_dtpBeginDate.Text).ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = DateTime.Parse(Convert.ToDateTime(m_objViewer.m_dtpEndDate.Text).ToString("yyyy-MM-dd 23:59:59"));
            string strVendor = m_objViewer.m_txtVendor.Text;

            this.m_objViewer.m_dwcData.Modify("begindate.text = '" + dtmBegin.ToString("yyyy-MM-dd") + "'");
            this.m_objViewer.m_dwcData.Modify("enddate.text = '" + dtmEnd.ToString("yyyy-MM-dd") + "'");
            this.m_objViewer.m_dwcData.Modify("vendorname.text = '" + strVendor + "'");
            m_mthSetStorageNameToReport();

            string strVendorID = string.Empty;
            if (m_objViewer.m_txtVendor.Tag != null && !string.IsNullOrEmpty(m_objViewer.m_txtVendor.Text))
            {
                strVendorID = m_objViewer.m_txtVendor.Tag.ToString();
            }

            int intSetID = 0;
            if (m_objViewer.m_cboType.SelectedItem != null)
            {
                clsMS_MedicineTypeSetVO objSet = m_objViewer.m_cboType.SelectedItem as clsMS_MedicineTypeSetVO;
                if (objSet != null)
                {
                    intSetID = objSet.m_intMedicineTypeSetID;
                }
            }

            DataTable dtbData = null;
            long lngRes = m_objDomain.m_lngStatistics(m_objViewer.m_strStorageID, dtmBegin, dtmEnd, strVendorID, intSetID, out dtbData);

            if (dtbData == null || dtbData.Rows.Count == 0)
            {
                m_objViewer.m_dwcData.Reset();
                return;
            }

            DataTable dtbNewdata = null;
            if (dtbData != null)
            {
                this.m_objViewer.m_dwcData.SetRedrawOff();

                //��ҩƷID����
                DataView dtv = new DataView();
                dtv = dtbData.DefaultView;
                dtv.Sort = "assistcode_chr";
                dtbNewdata = dtv.ToTable();

                this.m_objViewer.m_dwcData.Retrieve(dtbNewdata);
                this.m_objViewer.m_dwcData.SetRedrawOn();
                this.m_objViewer.m_dwcData.Refresh();
            }
        }
        #endregion

        #region ��ʾ��Ӧ��
        /// <summary>
        /// ��ʾ��Ӧ��
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowVendor(string p_strSearchCon)
        {
            if (m_ctlQueryVendor == null)
            {
                m_mthGetVendor(out m_dtbVendor);
                m_ctlQueryVendor = new ctlQueryVendor(m_dtbVendor);
                m_objViewer.Controls.Add(m_ctlQueryVendor);

                int X = m_objViewer.m_txtVendor.Location.X;
                int Y = m_objViewer.m_txtVendor.Location.Y + m_objViewer.m_txtVendor.Size.Height;

                m_ctlQueryVendor.Location = new System.Drawing.Point(X, Y);
                m_ctlQueryVendor.ReturnInfo += new ReturnVendorInfo(QueryVendor_ReturnInfo);
            }
            m_ctlQueryVendor.BringToFront();
            m_ctlQueryVendor.m_mthSetSearchText(p_strSearchCon);
            m_ctlQueryVendor.Visible = true;
            m_ctlQueryVendor.Focus();
        }

        /// <summary>
        /// ��ù�Ӧ��
        /// </summary>
        /// <param name="p_dtbVendor">��Ӧ�����ݱ�</param>
        private void m_mthGetVendor(out DataTable p_dtbVendor)
        {
            long lngRes = m_objDomain.m_lngGetVendor(out p_dtbVendor);
        }

        internal void QueryVendor_ReturnInfo( clsMS_Vendor MS_VO)
        {
            m_objViewer.m_txtVendor.Tag = null;
            if (MS_VO == null)
            {
                return;
            }

            m_objViewer.m_txtVendor.Tag = MS_VO.m_strVendorID;
            m_objViewer.m_txtVendor.Text = MS_VO.m_strVendorName;

            m_objViewer.m_btnSearch.Focus();
        }
        #endregion

        #region ��ȡҩƷ��������
        /// <summary>
        /// ��ȡҩƷ��������
        /// </summary>
        internal void m_mthGetMedicineTypeSet()
        {
            clsMS_MedicineTypeSetVO[] objMPVO = null;
            clsDcl_MedicineTypeSet objMTDomain = new clsDcl_MedicineTypeSet();
            long lngRes = objMTDomain.m_lngGetAllMedicinTypeSetInfo(out objMPVO);
            objMTDomain = null;

            if (objMPVO == null || m_objViewer.m_cboType.Items.Count > 0)
            {
                return;
            }

            clsMS_MedicineTypeSetVO objAll = new clsMS_MedicineTypeSetVO();
            objAll.m_intMedicineTypeSetID = 0;
            objAll.m_strMedicineTypeSetName = "ȫ��";

            m_objViewer.m_cboType.Items.Add(objAll);
            m_objViewer.m_cboType.Items.AddRange(objMPVO);

            m_objViewer.m_cboType.SelectedIndex = 0;
        }
        #endregion

        #region ��ʼ������ʹ������
        /// <summary>
        /// �Ѳֿ���д������
        /// </summary>
        internal void m_mthSetStorageNameToReport()
        {
            if (string.IsNullOrEmpty(m_objViewer.m_strStorageName))
            {
                long lngRes = m_objDomain.m_lngGetStoreRoomName(m_objViewer.m_strStorageID, out m_objViewer.m_strStorageName);
            }
            this.m_objViewer.m_dwcData.Modify("storagename.text = '" + m_objViewer.m_strStorageName + "'");
        }

        /// <summary>
        /// ����Ӳֿ���
        /// </summary>
        internal void m_mthSetStorageNameToFrm()
        {
            long lngRes = m_objDomain.m_lngGetStoreRoomName(m_objViewer.m_strStorageID, out m_objViewer.m_strStorageName);
            this.m_objViewer.Text += "(" + m_objViewer.m_strStorageName + ")";
        }
        #endregion
    }
}
