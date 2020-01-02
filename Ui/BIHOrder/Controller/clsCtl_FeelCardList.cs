using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// Ƥ��¼���	�߼����Ʋ�
    /// ���ߣ� ����
    /// ����ʱ�䣺 2004-12-23 
    /// </summary>
    public class clsCtl_FeelCardList : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_FeelCardList m_objManage = null;
        clsDcl_InputOrder m_objInputOrder = null;
        public string m_strReportID;
        /// <summary>
        /// ��½�û�ID
        /// </summary>
        public string m_strOperatorID;
        /// <summary>
        /// ��ǰƤ�Խ������ˮ��
        /// </summary>
        internal string m_strOrderFeelID = "";
        /// <summary>
        /// ��ӡ��ҽ����Ŀ
        /// </summary>
        private clsBIHCanExecOrder[] arrExec;

        #endregion
        #region ���캯��
        public clsCtl_FeelCardList()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_FeelCardList();
            m_objInputOrder = new clsDcl_InputOrder();
            m_strReportID = null;
        }
        #endregion
        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmFeelCardList m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmFeelCardList)frmMDI_Child_Base_in;
        }
        #endregion

        #region �����¼�
        public void m_txtAreaInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("�������", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("��������", 90, HorizontalAlignment.Left);
            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 170;
        }
        public void m_txtAreaFindItem(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHArea[] objItemArr;
            long lngRes = m_objInputOrder.m_lngFindArea(strFindCode, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (m_objViewer.LoginInfo != null)
                {
                    System.Collections.IList ilUsableAreaID = m_objViewer.LoginInfo.m_ilUsableAreaID;
                    clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                    objItemArr = (clsBIHArea[])(objInputOrder.GetUsableAreaObject(objItemArr, ilUsableAreaID)).ToArray(typeof(clsBIHArea));
                }
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    /** @update by xzf (05-09-20) 
                     * 
                     */
                    //@ListViewItem lvi=lvwList.Items.Add(objItemArr[i].m_strAreaID);
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].code);
                    lvi.SubItems.Add(objItemArr[i].m_strAreaName);
                    lvi.Tag = objItemArr[i].m_strAreaID;
                    /* <<======================== */
                }
            }
        }
        public void m_txtAreaSelectItem(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_objViewer.m_txtArea.Text = lviSelected.SubItems[1].Text;
                m_objViewer.m_txtArea.Tag = lviSelected.Tag;
                m_cmdSearch();

            }
        }
        #endregion

        internal void m_cmdSearch()
        {
            LoadData();
        }


        internal void LoadData()
        {

            this.m_objViewer.dw_1.Reset();
            int newRow;
            if (m_objViewer.m_txtArea.Tag == null)
            {
                return;
            }
            string m_strAreaId = (string)m_objViewer.m_txtArea.Tag;
            DataTable m_dtExecOrder = null;
            m_objManage.m_lngGetFeelOrderByAreaID(m_strAreaId, out m_dtExecOrder);
            if (m_dtExecOrder != null && m_dtExecOrder.Rows.Count > 0)
            {
                for (int i = m_dtExecOrder.Rows.Count - 1; i >= 0; i--)
                {
                    if (m_dtExecOrder.Rows[i]["cancel_dat"] != DBNull.Value) m_dtExecOrder.Rows.RemoveAt(i);
                }
            }
            DataView myDataView = new DataView(m_dtExecOrder);
            filltheExecOrderTable(myDataView, out arrExec);
            //DateTime executedate_dat, INPATIENT_DAT;

            //dw_1.Modify("execute_dat.text='" + DateTime.Now.ToString("yyyy.MM.dd") + "'");
            if (arrExec != null)
            {
                this.m_objViewer.dw_1.Modify("areatext.text='" + m_objViewer.m_txtArea.Text.Trim() + "'");

                for (int i = 0; i < arrExec.Length; i++)
                {
                    newRow = this.m_objViewer.dw_1.InsertRow();
                    this.m_objViewer.dw_1.SetItemString(newRow, "bedname", arrExec[i].m_strBedName);
                    this.m_objViewer.dw_1.SetItemString(newRow, "patientname", arrExec[i].m_strPatientName);
                    this.m_objViewer.dw_1.SetItemString(newRow, "patientsex", arrExec[i].m_strPatientSex);
                    this.m_objViewer.dw_1.SetItemString(newRow, "ordername", arrExec[i].m_strName.Trim());
                    this.m_objViewer.dw_1.SetItemString(newRow, "inpatientid", arrExec[i].m_strInpatientID.Trim());
                }
            }
            this.m_objViewer.dw_1.AcceptText();

            //dw_1.Sort();
            //dw_1.CalculateGroups();
            //dw_1.Visible = true;
        }

        #region ��ִ��ҽ������ֵ
        /// <summary>
        /// ��ִ��ҽ������ֵ
        /// </summary>
        /// <param name="objDT"></param>
        /// <param name="arrExecOrder"></param>
        private void filltheExecOrderTable(DataView objRow, out clsBIHCanExecOrder[] arrExecOrder)
        {
            //ҽ��ִ�ж�������
            arrExecOrder = new clsBIHCanExecOrder[0];
            if (objRow.Count <= 0)
            {
                return;
            }
            arrExecOrder = new clsBIHCanExecOrder[objRow.Count];
            for (int i = 0; i < objRow.Count; i++)
            {
                arrExecOrder[i] = new clsBIHCanExecOrder();
                arrExecOrder[i].m_strBedID = Convert.ToString(objRow[i]["bedid_chr"].ToString().Trim());
                arrExecOrder[i].m_strBedName = Convert.ToString(objRow[i]["code_chr"].ToString().Trim());
                arrExecOrder[i].m_strCURBEDID_CHR = Convert.ToString(objRow[i]["bedid_chr"].ToString().Trim());
                arrExecOrder[i].m_strCURBEDName = Convert.ToString(objRow[i]["code_chr"].ToString().Trim());
                arrExecOrder[i].m_strRegisterID = Convert.ToString(objRow[i]["registerid_chr"].ToString().Trim());
                arrExecOrder[i].m_strPatientName = Convert.ToString(objRow[i]["LASTNAME_VCHR"].ToString().Trim());//����
                arrExecOrder[i].m_strPatientSex = Convert.ToString(objRow[i]["SEX_CHR"].ToString().Trim());//����

                if (!objRow[i]["RECIPENO_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intRecipenNo = int.Parse(objRow[i]["RECIPENO_INT"].ToString().Trim()); //����
                if (!objRow[i]["RECIPENO2_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intRecipenNo2 = int.Parse(objRow[i]["RECIPENO2_INT"].ToString().Trim()); //����(�����ⲿ��ʾ)

                if (!objRow[i]["EXECUTETYPE_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intExecuteType = int.Parse(objRow[i]["EXECUTETYPE_INT"].ToString().Trim());//ҽ����ҽ����ʽ��
                arrExecOrder[i].m_strOrderDicCateID = Convert.ToString(objRow[i]["ordercateid_chr"].ToString().Trim());//���ID

                arrExecOrder[i].m_strName = Convert.ToString(objRow[i]["NAME_VCHR"].ToString().Trim());//��Ŀ����
                if (!objRow[i]["Dosage_Dec"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_dmlDosage = decimal.Parse(objRow[i]["Dosage_Dec"].ToString().Trim()); ;//����
                arrExecOrder[i].m_strDosageUnit = Convert.ToString(objRow[i]["dosageunit_chr"].ToString().Trim());//������λ
                arrExecOrder[i].m_strExecFreqID = Convert.ToString(objRow[i]["EXECFREQID_CHR"].ToString().Trim());
                arrExecOrder[i].m_strExecFreqName = Convert.ToString(objRow[i]["EXECFREQNAME_CHR"].ToString().Trim());
                if (!objRow[i]["GET_DEC"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_dmlGet = decimal.Parse(objRow[i]["GET_DEC"].ToString().Trim());
                arrExecOrder[i].m_strGetunit = Convert.ToString(objRow[i]["getunit_chr"].ToString().Trim());
                arrExecOrder[i].m_strEntrust = Convert.ToString(objRow[i]["ENTRUST_VCHR"].ToString().Trim());//����
                arrExecOrder[i].m_strDOCTOR_VCHR = Convert.ToString(objRow[i]["DOCTOR_VCHR"].ToString().Trim());//����ҽ��
                if (!objRow[i]["POSTDATE_DAT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_dtPostdate = Convert.ToDateTime(objRow[i]["POSTDATE_DAT"].ToString().Trim());
                arrExecOrder[i].m_strASSESSORFOREXEC_CHR = Convert.ToString(objRow[i]["CONFIRMER_VCHR"].ToString().Trim());//�����-
                arrExecOrder[i].m_strASSESSORFOREXEC_DAT = Convert.ToString(objRow[i]["CONFIRM_DAT"].ToString().Trim());//���ʱ��
                arrExecOrder[i].m_strDosetypeID = Convert.ToString(objRow[i]["DOSETYPEID_CHR"].ToString().Trim());//�÷�ID
                arrExecOrder[i].m_strDosetypeName = Convert.ToString(objRow[i]["DOSETYPENAME_CHR"].ToString().Trim());//�÷�����
                arrExecOrder[i].m_strOrderID = Convert.ToString(objRow[i]["orderid_chr"].ToString().Trim());//ҽ������ˮ��
                if (!objRow[i]["STATUS_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intStatus = int.Parse(objRow[i]["STATUS_INT"].ToString().Trim());//��ǰҽ��״̬
                arrExecOrder[i].m_strCreatorID = Convert.ToString(objRow[i]["CREATORID_CHR"].ToString().Trim());//��ҽ����ҽ��ID
                arrExecOrder[i].m_strCreator = Convert.ToString(objRow[i]["CREATOR_CHR"].ToString().Trim());//��ҽ����ҽ��

                arrExecOrder[i].m_strOrderDicID = Convert.ToString(objRow[i]["ORDERDICID_CHR"].ToString().Trim());//������Ŀ��ˮ��
                arrExecOrder[i].m_strParentID = Convert.ToString(objRow[i]["patientid_chr"].ToString().Trim());//����ID
                arrExecOrder[i].m_strCREATEAREA_ID = Convert.ToString(objRow[i]["createareaid_chr"].ToString().Trim());//��������ID
                arrExecOrder[i].m_strCURAREAID_CHR = Convert.ToString(objRow[i]["CURAREAID_CHR"].ToString().Trim());//��ҽ��ʱ�������ڲ���ID
                arrExecOrder[i].m_strCURAREAName = Convert.ToString(objRow[i]["CURAREAName"].ToString().Trim());//��ҽ��ʱ�������ڲ�������
                if (!objRow[i]["OUTGETMEDDAYS_INT"].ToString().Trim().Equals(""))
                {
                    arrExecOrder[i].m_intOUTGETMEDDAYS_INT = int.Parse(objRow[i]["OUTGETMEDDAYS_INT"].ToString().Trim());//��Ժ��ҩ����(��ִ������Ϊ3=��Ժ��ҩ})
                }
                arrExecOrder[i].m_strSAMPLEID_VCHR = Convert.ToString(objRow[i]["SAMPLEID_VCHR"].ToString().Trim());
                arrExecOrder[i].m_strSAMPLEName_VCHR = Convert.ToString(objRow[i]["sample_type_desc_vchr"].ToString().Trim());
                arrExecOrder[i].m_strPARTID_VCHR = Convert.ToString(objRow[i]["PARTID_VCHR"].ToString().Trim());
                arrExecOrder[i].m_strPARTNAME_VCHR = Convert.ToString(objRow[i]["partname"].ToString().Trim());
                //Ƥ������ֶ�
                arrExecOrder[i].m_intISNEEDFEEL = int.Parse(objRow[i]["ISNEEDFEEL"].ToString().Trim());//�Ƿ���ҪƤ��
                arrExecOrder[i].m_intFEEL_INT = int.Parse(objRow[i]["FEEL_INT"].ToString().Trim());//Ƥ�Խ��
                arrExecOrder[i].m_strFEELRESULT_VCHR = Convert.ToString(objRow[i]["FEELRESULT_VCHR"].ToString().Trim());//Ƥ�Խ����ע
                if (!objRow[i]["CHARGE_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intCHARGE_INT = int.Parse(objRow[i]["CHARGE_INT"].ToString().Trim());
                if (!objRow[i]["ATTACHTIMES_INT"].ToString().Trim().Equals(""))
                    arrExecOrder[i].m_intATTACHTIMES_INT = int.Parse(objRow[i]["ATTACHTIMES_INT"].ToString().Trim());
                arrExecOrder[i].m_strREMARK_VCHR = objRow[i]["REMARK_VCHR"].ToString().Trim();//ҽ��˵��
                arrExecOrder[i].m_strInpatientID = objRow[i]["inpatientid_chr"].ToString().Trim();//סԺ��
            }


        }

        #endregion
    }
}
