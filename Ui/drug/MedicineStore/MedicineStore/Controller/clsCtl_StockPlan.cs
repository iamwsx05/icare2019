using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    public class clsCtl_StockPlan : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_StockPlan m_objDomain = null;
        /// <summary>
        /// ����
        /// </summary>
        private com.digitalwave.iCare.gui.MedicineStore.frmStockPlan m_objViewer;
        /// <summary>
        /// ��ѯ��Ӧ�̿ؼ�
        /// </summary>
        private ctlQueryVendor m_ctlQueryVendor = null;
        /// <summary>
        /// ��Ӧ��
        /// </summary>
        private DataTable m_dtbVendor = null;
        /// <summary>
        /// ҩƷ��ѯ�ؼ�
        /// </summary>
        private ctlQueryMedicintLeastElement m_ctlQueryMedicint = null;
        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_StockPlan()
        {
            m_objDomain = new clsDcl_StockPlan();
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
            m_objViewer = (frmStockPlan)frmMDI_Child_Base_in;
        }
        #endregion
        #region ����Ƿ���ҩ�����Ȩ��
        /// <summary>
        /// ����Ƿ���ҩ�����Ȩ��
        /// </summary>
        /// <param name="strEmpID">Ա��ID</param>
        /// <param name="p_blnHasRole">�Ƿ���Ȩ��</param>
        internal void m_mthCheckHasAdminRole(string strEmpID, out bool p_blnHasRole)
        {
            long lngRes = m_objDomain.m_lngCheckEmpHasRole(strEmpID, out p_blnHasRole);
        }
        #endregion

        #region ��ȡ�ֿ���
        /// <summary>
        /// ��ȡ�ֿ���
        /// </summary>
        /// <param name="p_strStoreRoomID">�ֿ�ID</param>
        /// <returns></returns>
        internal string m_strStoreRoomName(string p_strStoreRoomID)
        {
            string strStoreRoomName = string.Empty;
            long lngRes = m_objDomain.m_lngGetStoreRoomName(p_strStoreRoomID, out strStoreRoomName);
            return strStoreRoomName;
        }
        #endregion

        #region �޸�ѡ������ⵥ
        /// <summary>
        /// �޸�ѡ������ⵥ
        /// </summary>
        /// <param name="p_objMain">����</param>
        /// <param name="p_objSubVO">�ӱ�</param>
        internal void m_mthModifySelected(out clsMS_StockPlan_VO p_objMain, out clsMS_StockPlan_Detail_VO[] p_objSubVO)
        {
            p_objMain = null;
            p_objSubVO = null;
            #region ����VO
            clsMS_StockPlan_VO objMain = null;
            if (m_objViewer.m_dgvMainInfo.SelectedRows.Count == 1)
            {
                DataRowView drvMain = m_objViewer.m_dtvMainView[m_objViewer.m_dgvMainInfo.SelectedRows[0].Index];
                //int intState = Convert.ToInt32(drvMain["STATE_INT"]);
                //if (intState != 1)
                //{
                //    MessageBox.Show("�����Ʊ������޸�", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                objMain = new clsMS_StockPlan_VO();
                objMain.m_lngSERIESID_INT = Convert.ToInt64(drvMain["SERIESID_INT"]);
                objMain.m_strSTOCKPLANID_VCHR = drvMain["STOCKPLANID_VCHR"].ToString();
                //objMain.m_strSTORAGENAME_VCHR = drvMain["STORAGENAME_VCHR"].ToString();
                objMain.m_lngSTATE_INT = Convert.ToInt32(drvMain["STATE_INT"]);
                objMain.m_strVENDORID_CHR = drvMain["VENDORID_CHR"].ToString();
                objMain.m_strVENDORNAME_VCHR = drvMain["vendorname_vchr"].ToString();
                objMain.m_strSTORAGEID_CHR = drvMain["STORAGEID_CHR"].ToString();
                objMain.m_datSTOCKPLAN_DAT = Convert.ToDateTime(drvMain["STOCKPLAN_DAT"]);
                objMain.m_datNEWORDER_DAT = Convert.ToDateTime(drvMain["NEWORDER_DAT"]);
                objMain.m_strCOMMENT_VCHR = drvMain["COMMENT_VCHR"].ToString();
                objMain.m_strMAKERID_CHR = drvMain["MAKERID_CHR"].ToString();
                objMain.m_strMAKERNAME_VCHR = drvMain["MAKERNAME"].ToString();
            }

            if (objMain == null)
            {
                return;
            }
            p_objMain = objMain;
            #endregion

            #region �ӱ�VO
            clsMS_StockPlan_Detail_VO[] objSubVO = null;
            DataView dvSub = m_objViewer.m_dgvSubInfo.DataSource as DataView;
            if (dvSub != null && dvSub.Count > 0)
            {
                DataRowView drvCurrent = null;
                objSubVO = new clsMS_StockPlan_Detail_VO[dvSub.Count];
                DateTime dtTemp;
                for (int iRow = 0; iRow < m_objViewer.m_dgvSubInfo.Rows.Count; iRow++)
                {
                    drvCurrent = dvSub[iRow];
                    objSubVO[iRow] = new clsMS_StockPlan_Detail_VO();
                    objSubVO[iRow].m_lngSERIESID_INT = Convert.ToInt64(drvCurrent["SERIESID_INT"]);
                    objSubVO[iRow].m_lngSERIESID2_INT = Convert.ToInt64(drvCurrent["SERIESID2_INT"]);
                    objSubVO[iRow].m_strMEDICINEID_CHR = drvCurrent["MEDICINEID_CHR"].ToString();
                    objSubVO[iRow].m_strMEDICINENAME_VCHR = drvCurrent["MEDICINENAME_VCHR"].ToString();
                    objSubVO[iRow].m_strMEDSPEC_VCHR = drvCurrent["MEDSPEC_VCHR"].ToString();
                    objSubVO[iRow].m_dblAMOUNT = Convert.ToInt32(drvCurrent["AMOUNT"]);
                    objSubVO[iRow].m_strPRODUCTORID_CHR = drvCurrent["PRODUCTORID_CHR"].ToString();
                    objSubVO[iRow].m_strUNIT_VCHR = drvCurrent["UNIT_VCHR"].ToString();
                    objSubVO[iRow].m_strASSISTCODE_CHR = drvCurrent["ASSISTCODE_CHR"].ToString();
                    objSubVO[iRow].m_lngSTATUS = Convert.ToInt16(drvCurrent["STATUS"]);
                    objSubVO[iRow].m_dblCALLPRICE_INT = Convert.ToDouble(drvCurrent["CALLPRICE_INT"]);
                    objSubVO[iRow].m_dblSTOCKSUM = Convert.ToDouble(drvCurrent["STOCKSUM"]);
                    if (DateTime.TryParse(drvCurrent["LASTINSTORAGEDATE_DAT"].ToString(), out dtTemp))
                        objSubVO[iRow].m_datLASTINSTORAGEDATE_DAT = dtTemp;
                    objSubVO[iRow].m_strVENDORID_CHR = drvCurrent["VENDORID_CHR"].ToString();
                    objSubVO[iRow].m_strVENDORNAME_VCHR = drvCurrent["vendorname"].ToString();

                }
            }
            p_objSubVO = objSubVO;
            #endregion
        }
        #endregion

        #region ɾ��ҩƷ��Ϣ
        /// <summary>
        /// ɾ��ҩƷ��Ϣ
        /// </summary>
        internal void m_mthDeleteMedicine()
        {
            List<long> lngCheckRowIndex = new List<long>();
            List<long> lngWrongRowIndex = new List<long>();
            for (int iSe = 0; iSe < m_objViewer.m_dgvMainInfo.Rows.Count; iSe++)
            {
                if (Convert.ToBoolean(m_objViewer.m_dgvMainInfo.Rows[iSe].Cells[0].Value))
                {
                    int intState = Convert.ToInt32(m_objViewer.m_dtvMainView[iSe]["STATE_INT"]);
                    if (intState == 2 || intState == 0)//����˻���ɾ��
                    {
                        lngWrongRowIndex.Add(Convert.ToInt64(m_objViewer.m_dtvMainView[iSe]["SERIESID_INT"]));
                        continue;
                    }
                    lngCheckRowIndex.Add(Convert.ToInt64(m_objViewer.m_dtvMainView[iSe]["SERIESID_INT"]));
                }
            }

            if (lngWrongRowIndex.Count > 0)
            {
                DialogResult drResultQ = MessageBox.Show("������ѡ���¼����˻���ɾ����������ɾ�����Ƿ������", "�ɹ��ƻ�", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (drResultQ == DialogResult.No)
                {
                    return;
                }
            }

            if (lngCheckRowIndex.Count == 0)
            {
                MessageBox.Show("����ѡ��һ�����Ʋɹ��ƻ���Ϣ", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult drResult = MessageBox.Show("�Ƿ�ɾ��ѡ�м�¼��", "�ɹ��ƻ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drResult == DialogResult.No)
            {
                return;
            }

            long lngRes = m_objDomain.m_lngDeleteMainStockPlan(lngCheckRowIndex.ToArray());
            if (lngRes > 0)
            {
                MessageBox.Show("ɾ���ɹ�", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_cmdSearch.PerformClick();
                //StringBuilder stbFilter = new StringBuilder(50);
                //for (int iSer = 0; iSer < lngCheckRowIndex.Count; iSer++)
                //{
                //    stbFilter.Append(" SERIESID_INT = '");
                //    stbFilter.Append(lngCheckRowIndex[iSer]);
                //    stbFilter.Append("'");
                //    if (iSer < lngCheckRowIndex.Count - 1)
                //    {
                //        stbFilter.Append(" or ");
                //    }
                //}
                //DataRow[] drRemove = m_objViewer.m_dtbMainData.Select(stbFilter.ToString());
                //if (drRemove != null && drRemove.Length > 0)
                //{
                //    for (int iRev = 0; iRev < drRemove.Length; iRev++)
                //    {
                //        m_objViewer.m_dtbMainData.Rows.Remove(drRemove[iRev]);
                //    }
                //}

                //DataView dtvSub = m_objViewer.m_dgvSubInfo.DataSource as DataView;
                //if (dtvSub != null)
                //{
                //    dtvSub.Table.Rows.Clear();
                //}
            }
            else
            {
                MessageBox.Show("ɾ��ʧ��", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ���ҩƷ��Ϣ
        /// <summary>
        /// ���ҩƷ��Ϣ
        /// </summary>
        /// <param name="p_drCommit">��˵���</param>
        internal void m_mthCommitMedicine(out DataRow[] p_drCommit)
        {
            p_drCommit = null;
            if (!m_objViewer.m_blnIsAdmin)
            {
                MessageBox.Show("��ǰ�û�û��ҩ�����Ȩ�ޣ��������", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<DataRow> lstCheck = new List<DataRow>();
            for (int iSe = 0; iSe < m_objViewer.m_dgvMainInfo.Rows.Count; iSe++)
            {
                if (Convert.ToBoolean(m_objViewer.m_dgvMainInfo.Rows[iSe].Cells[0].Value))
                {
                    DataRow drCheck = ((DataRowView)m_objViewer.m_dgvMainInfo.Rows[iSe].DataBoundItem).Row;
                    if (drCheck["STATE_INT"].ToString() == "1")
                    {
                        lstCheck.Add(drCheck);
                    }
                }
            }

            DataRow[] drNew = lstCheck.ToArray();

            if (drNew == null || drNew.Length == 0)
            {
                MessageBox.Show("û������˵Ĳɹ��ƻ���Ϣ", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                long lngRes = 0;
                long lngSEQ = 0;
                clsDcl_StockPlan objSTDomain = new clsDcl_StockPlan();

                bool blnSaveComplete = true;
                DateTime dtmNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                for (int iRow = 0; iRow < drNew.Length; iRow++)
                {
                    lngSEQ = Convert.ToInt64(drNew[iRow]["SERIESID_INT"]);
                    lngRes = m_objDomain.m_lngCommitStockPlan(lngSEQ, m_objViewer.LoginInfo.m_strEmpID, dtmNow);

                    if (lngRes <= 0)
                    {
                        blnSaveComplete = false;
                        break;
                    }
                }

                if (blnSaveComplete)
                {
                    p_drCommit = drNew;
                    System.Windows.Forms.MessageBox.Show("������", "�ɹ��ƻ�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("���ʧ��", "�ɹ��ƻ�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
                System.Windows.Forms.MessageBox.Show("���ʧ��", "�ɹ��ƻ�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        ///// <summary>
        ///// ���������
        ///// </summary>
        ///// <param name="p_drCommit">����</param>
        //private void m_mthSetCommitUser(DataRow[] p_drCommit)
        //{
        //    if (p_drCommit == null || p_drCommit.Length == 0)
        //    {
        //        return;
        //    }

        //    long[] lngSEQ = new long[p_drCommit.Length];
        //    for (int iRow = 0; iRow < p_drCommit.Length; iRow++)
        //    {
        //        lngSEQ[iRow] = Convert.ToInt64(p_drCommit[iRow]["SERIESID_INT"]);
        //    }

        //    long lngRes = m_objDomain.m_lngSetCommitUser(m_objViewer.LoginInfo.m_strEmpID, lngSEQ);
        //}

        /// <summary>
        /// ���½���
        /// </summary>
        /// <param name="p_drNew">����</param>
        internal void m_mthUpdateUIAfterCommit(DataRow[] p_drNew)
        {
            if (p_drNew == null || p_drNew.Length == 0)
            {
                return;
            }

            for (int iRow = 0; iRow < p_drNew.Length; iRow++)
            {
                DataRow[] drTemp = m_objViewer.m_dtbMainData.Select("SERIESID_INT = " + Convert.ToInt64(p_drNew[iRow]["SERIESID_INT"]));
                if (drTemp.Length > 0)
                {
                    drTemp[0]["EXAMERID_CHR"] = m_objViewer.LoginInfo.m_strEmpID;
                    drTemp[0]["examername"] = m_objViewer.LoginInfo.m_strEmpName;
                    drTemp[0]["STATE_INT"] = 2;
                    drTemp[0]["EXAM_DAT"] = DateTime.Now;
                    drTemp[0]["statedesc"] = "���";
                }
            }

            if (m_objViewer.m_dtvMainView.Count == 0)
            {
                m_objViewer.m_dtbSubData.Rows.Clear();
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        internal void m_mthUnCommit()
        {
            if (!m_objViewer.m_blnIsAdmin)
            {
                MessageBox.Show("��ǰ�û�û��ҩ�����Ȩ�ޣ���������", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<DataRow> lstCheck = new List<DataRow>();
            for (int iSe = 0; iSe < m_objViewer.m_dgvMainInfo.Rows.Count; iSe++)
            {
                if (Convert.ToBoolean(m_objViewer.m_dgvMainInfo.Rows[iSe].Cells[0].Value))
                {
                    DataRow drCheck = ((DataRowView)m_objViewer.m_dgvMainInfo.Rows[iSe].DataBoundItem).Row;
                    if (drCheck["STATE_INT"].ToString() == "2")
                    {
                        lstCheck.Add(drCheck);
                    }
                }
            }

            DataRow[] drCommit = lstCheck.ToArray();

            if (drCommit == null || drCommit.Length == 0)
            {
                MessageBox.Show("û�з������������Ĳɹ��ƻ���Ϣ", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<long> lstSEQ = new List<long>();//δ������������������к�
            List<string> lstRigthInStorageID = new List<string>();//δ��������������ⵥ��
            List<DataRow> lstCommitRow = new List<DataRow>();//����������������

            Hashtable hstWrongID = new Hashtable();

            for (int iRow = 0; iRow < drCommit.Length; iRow++)
            {
                lstSEQ.Add(Convert.ToInt64(drCommit[iRow]["SERIESID_INT"]));
                lstRigthInStorageID.Add(drCommit[iRow]["stockplanid_vchr"].ToString());
                lstCommitRow.Add(drCommit[iRow]);
            }

            if (lstRigthInStorageID.Count == 0)
            {
                MessageBox.Show("û�з������������Ĳɹ��ƻ���Ϣ", "�ɹ��ƻ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            long[] lngSEQArr = lstSEQ.ToArray();
            string[] strInStorageIDArr = lstRigthInStorageID.ToArray();

            long lngRes = 0;

            lngRes = m_objDomain.m_lngUnCommit(lngSEQArr);
            if (lngRes > 0)
            {
                m_mthUpdateUIAfterUnCommit(lstCommitRow.ToArray());
                if (m_objViewer.m_dgvMainInfo.Rows.Count > 0)
                {
                    m_objViewer.m_dgvMainInfo.Rows[0].Selected = true;
                }
                else
                {
                    if (m_objViewer.m_dtbSubData != null)
                    {
                        m_objViewer.m_dtbSubData.Rows.Clear();
                    }
                }
                System.Windows.Forms.MessageBox.Show("�������", "�ɹ��ƻ�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("����ʧ��", "�ɹ��ƻ�", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// �������½���
        /// </summary>
        /// <param name="drData">����</param>
        private void m_mthUpdateUIAfterUnCommit(DataRow[] drData)
        {
            if (drData == null || drData.Length == 0)
            {
                return;
            }

            for (int iRow = 0; iRow < drData.Length; iRow++)
            {
                drData[iRow]["examername"] = DBNull.Value;
                drData[iRow]["STATE_INT"] = 1;
                drData[iRow]["EXAM_DAT"] = DBNull.Value;
                drData[iRow]["statedesc"] = "����";
            }
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_dtmBeginDate">��ѯ��ʼʱ��</param>
        /// <param name="p_dtmEndDate">��ѯ����ʱ��</param>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strMedicineName">ҩƷ����</param>
        /// <param name="p_strVendorName">��Ӧ������</param>
        /// <param name="p_strStockPlanID">���ݺ�</param>
        /// <param name="p_strMedicinePreptype">ҩƷ����</param>
        /// <param name="p_dtbValue">��������</param>
        internal void m_mthGetMainData(DateTime p_dtmBeginDate, DateTime p_dtmEndDate, string p_strStorageID,
            string p_strMedicineName, string p_strVendorName, string p_strStockPlanID, string p_strMedicinePreptype, out DataTable p_dtbValue)
        {
            long lngRes = m_objDomain.m_lngGetStockPlan(p_dtmBeginDate, p_dtmEndDate, p_strStorageID, p_strMedicineName, p_strVendorName, p_strStockPlanID, p_strMedicinePreptype, out p_dtbValue);


            if (p_dtbValue != null)
            {
                string strUnCommit = "STATE_INT = 1 and EXAMERID_CHR is not null";
                DataRow[] drUnCommit = p_dtbValue.Select(strUnCommit);
                if (drUnCommit != null && drUnCommit.Length > 0)
                {
                    for (int iRow = 0; iRow < drUnCommit.Length; iRow++)
                    {
                        drUnCommit[iRow]["EXAMERID_CHR"] = DBNull.Value;
                        drUnCommit[iRow]["examername"] = DBNull.Value;
                        drUnCommit[iRow]["EXAM_DAT"] = DBNull.Value;
                    }
                }
            }
        }
        #endregion


        #region ��ȡָ���ֿ��ҩƷ����
        /// <summary>
        /// ��ȡָ���ֿ��ҩƷ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objMTVO">ҩƷ�Ƽ�����</param>
        internal void m_mthGetStorageMedicineType(string p_strStorageID, out clsMS_MedicineType_VO[] p_objMTVO)
        {
            long lngRes = m_objDomain.m_lngGetStorageMedicineType(p_strStorageID, out p_objMTVO);
        }
        #endregion

        #region ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// <summary>
        /// ��ȡҩƷ�ֵ���СԪ�ؼ�
        /// </summary>
        internal void m_mthGetMedicineInfo()
        {
            clsDcl_InventoryRecord objIRDomain = new clsDcl_InventoryRecord();
            long lngRes = objIRDomain.m_lngGetBaseMedicine(string.Empty, m_objViewer.m_strStorageID, out m_objViewer.m_dtbMedicinDict);
        }
        #endregion


        #region ����
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        internal void m_mthFilterMainData()
        {
            if (m_objViewer.m_dtbSubData != null)
            {
                m_objViewer.m_dtbSubData.Rows.Clear();
            }

            string strFilterMain = m_strMainFilter();

            m_objViewer.m_dtvMainView = new DataView(m_objViewer.m_dtbMainData);
            m_objViewer.m_dtvMainView.RowFilter = strFilterMain;
            m_objViewer.m_dgvMainInfo.DataSource = m_objViewer.m_dtvMainView;

            m_objViewer.m_dgvMainInfo.Refresh();

            if (m_objViewer.m_dgvMainInfo.Rows.Count > 0)
            {

                //m_objViewer.m_dgvMainInfo.Focus();
                if (m_objViewer.m_dgvMainInfo.CurrentCell == null)
                {
                    m_objViewer.m_dgvMainInfo.Rows[0].Selected = true;
                    m_objViewer.m_dgvMainInfo.CurrentCell = m_objViewer.m_dgvMainInfo.Rows[0].Cells[1];
                }
            }
            else
            {
                if (m_objViewer.m_dtbSubData != null)
                {
                    m_objViewer.m_dtbSubData.Rows.Clear();
                }
            }
        }
        #endregion

        #region ����ҩƷ��Ϣ
        /// <summary>
        /// ��ȡ�����������
        /// </summary>
        /// <returns></returns>
        private string m_strMainFilter()
        {
            string strFilterMain = string.Empty;

            if (m_objViewer.m_cboBillState.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(strFilterMain))
                {
                    strFilterMain += " and ";
                }
                if (m_objViewer.m_cboBillState.SelectedIndex != 3)
                    strFilterMain += " STATE_INT = " + (m_objViewer.m_cboBillState.SelectedIndex).ToString();
                else
                    strFilterMain += " STATE_INT = 0 ";
            }
            return strFilterMain;
        }

        /// <summary>
        /// �����ӱ���Ϣ
        /// </summary>
        internal void m_mthFilterSubData()
        {
            string strFilterSub = string.Empty;
            if (!string.IsNullOrEmpty(m_objViewer.m_txtMedicineName.Text))
            {
                strFilterSub = "MEDICINENAME_VCHR like '" + m_objViewer.m_txtMedicineName.Text + "%'";
            }

            if (m_objViewer.m_cboDoseType.SelectedIndex > 0)
            {
                if (!string.IsNullOrEmpty(strFilterSub))
                {
                    strFilterSub += " and ";
                }
                clsMEDICINEPREPTYPE_VO objVo = m_objViewer.m_cboDoseType.SelectedItem as clsMEDICINEPREPTYPE_VO;
                if (objVo != null)
                {
                    strFilterSub += " MEDICINEPREPTYPE_CHR = '" + objVo.m_strMEDICINEPREPTYPE_CHR + "'";
                }
            }

            if (m_objViewer.m_dtbSubData != null)
            {
                DataView dtvSub = new DataView(m_objViewer.m_dtbSubData);
                dtvSub.RowFilter = strFilterSub;
                m_objViewer.m_dgvSubInfo.DataSource = dtvSub;
            }

            m_objViewer.m_dgvSubInfo.Refresh();
        }
        #endregion

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

        #region ��ȡ��ϸ������
        /// <summary>
        /// ��ȡ��ϸ������
        /// </summary>
        /// <param name="p_lngSeries2ID">��ϸ��������</param>
        /// <param name="p_dtbValue">��ϸ������</param>
        /// <returns></returns>
        internal void m_mthGetStockPlanDetail(long p_lngSeries2ID, out DataTable p_dtbValue)
        {
            long lngRes = m_objDomain.m_mthGetStockPlanDetail(p_lngSeries2ID, out p_dtbValue);
        }
        #endregion

        #region ��ʾ��Ӧ�̲�ѯ
        /// <summary>
        /// ��ȡ��Ӧ����Ϣ
        /// </summary>
        /// <param name="p_dtbVendor"></param>
        internal void m_mthGetVendor(out DataTable p_dtbVendor)
        {
            clsDcl_InventoryRecord objIRDomain = new clsDcl_InventoryRecord();
            long lngRes = objIRDomain.m_lngGetVendor(out p_dtbVendor);
        }
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

                int X = m_objViewer.panel1.Location.X + m_objViewer.m_txtProviderName.Location.X;
                int Y = m_objViewer.panel1.Location.Y + m_objViewer.m_txtProviderName.Location.Y + m_objViewer.m_txtProviderName.Size.Height;

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
        }
        #endregion

        #region ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// <summary>
        /// ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowQueryMedicineForm(string p_strSearchCon)
        {
            if (m_ctlQueryMedicint == null)
            {
                m_ctlQueryMedicint = new ctlQueryMedicintLeastElement(m_objViewer.m_dtbMedicinDict);
                m_objViewer.Controls.Add(m_ctlQueryMedicint);

                int X = m_objViewer.panel1.Location.X + m_objViewer.m_txtMedicineName.Location.X;
                int Y = m_objViewer.panel1.Location.Y + m_objViewer.m_txtMedicineName.Location.Y + m_objViewer.m_txtMedicineName.Size.Height;

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

            m_objViewer.m_txtBillNumber.Focus();
        }
        #endregion
    }
}
