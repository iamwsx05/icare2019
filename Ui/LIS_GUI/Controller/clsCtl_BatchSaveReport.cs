using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// ������������ſ��Ʋ�
    /// </summary>
    class clsCtl_BatchSaveReport:com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// �����
        /// </summary>
        frmBatchSaveReport m_objViewer;
        /// <summary>
        /// Domain��
        /// </summary>
        clsDcl_BatchSaveReport m_objDomain;
        private clsCheckNODecoder m_objDecoder = new clsCheckNODecoder(); //���� �����Ž����õĽ�����
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_BatchSaveReport()
        {
            m_objDomain = new clsDcl_BatchSaveReport();
        }
        #endregion

        #region ���ý����
        /// <summary>
        /// ���ý����
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmBatchSaveReport)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ѯ���뵥��Ϣ
        /// <summary>
        /// ��ѯ���뵥��Ϣ
        /// </summary>
        internal void m_mthQueryAppInfo()
        {
            string m_strBarcode = m_objViewer.m_txtBarcode.Text;
            if (string.IsNullOrEmpty(m_strBarcode))
            {
                return;
            }
            clsLisApplMainVO m_objMainVO = null;
            long lngRes = m_objDomain.m_lngQuerySampleInfo(m_strBarcode, out m_objMainVO);
            if (m_objMainVO != null)
            {
                string strCheckNO = null;
                if (m_objViewer.m_dgAppList.Rows.Count > 0)
                {
                    
                    if (m_objViewer.m_dgAppList.Rows[m_objViewer.m_dgAppList.Rows.Count - 1].Cells["colCheckNum"].Value != null)
                    {
                        this.m_objDecoder.m_mthGetNextCheckNO(m_objViewer.m_dgAppList.Rows[m_objViewer.m_dgAppList.Rows.Count - 1].Cells["colCheckNum"].Value.ToString(), out strCheckNO);
                        m_objViewer.m_txtCheckNo.Text = strCheckNO;
                    }
                }
                m_objViewer.m_dgAppList.Rows.Add(new object[] { m_objMainVO.m_strApplication_Form_NO, m_objMainVO.m_strPatient_Name, m_objMainVO.m_strCheckContent, m_objMainVO.m_strAPPLICATION_ID });
                m_objViewer.m_dgAppList.Rows[m_objViewer.m_dgAppList.Rows.Count - 1].Selected = true;
            }
        }
        #endregion

        #region ��Ӽ�����
        /// <summary>
        /// ��Ӽ�����
        /// </summary>
        internal void m_mthAddCheckNO()
        {
            if (m_objViewer.m_dgAppList.SelectedRows.Count <= 0)
            {
                return;
            }
            m_objViewer.m_dgAppList.SelectedRows[0].Cells["colCheckNum"].Value = m_objViewer.m_txtCheckNo.Text;
        }
        #endregion

        #region �������������
        /// <summary>
        /// �������������
        /// </summary>
        internal void m_mthBatchSave()
        {
            List<clsLisApplMainVO> m_lst = new List<clsLisApplMainVO>();
            clsLisApplMainVO objTemp = null;
            long lngRes = 0;
            for (int i = 0; i < m_objViewer.m_dgAppList.Rows.Count; i++)
            {
                objTemp = new clsLisApplMainVO();
                objTemp.m_strAPPLICATION_ID = m_objViewer.m_dgAppList.Rows[i].Cells["colApplicationID"].Value.ToString();
                if (m_objViewer.m_dgAppList.Rows[i].Cells["colCheckNum"].Value != null)
                {
                    objTemp.m_strApplication_Form_NO = m_objViewer.m_dgAppList.Rows[i].Cells["colCheckNum"].Value.ToString().Trim();
                }
                m_lst.Add(objTemp);
            }
            if (m_lst.Count <= 0)
            {
                return;
            }
            lngRes = m_objDomain.m_lngUpdateCheckNUM(m_lst.ToArray(), m_objViewer.LoginInfo.m_strEmpID);
            if (lngRes > 0)
            {
                System.Windows.Forms.MessageBox.Show("����ɹ�");
            }
        }
        #endregion
    }
}
