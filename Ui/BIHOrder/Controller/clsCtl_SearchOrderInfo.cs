using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using System.Collections;
using com.digitalwave.iCare.BIHOrder.Control;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// �鿴ҽ��	�߼����Ʋ� 
    /// </summary>
    public class clsCtl_SearchOrderInfo : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_InputOrder m_objInputOrder = null;
        clsDcl_CommitOrder m_objCommitOrder = null;
        clsDcl_ExecuteOrder m_objExecuteOrder = null;
        clsDcl_SearchOrderInfo m_objSearchOrder = null;
        public string m_strReportID;
        /// <summary>
        /// ��½�û�ID
        /// </summary>
        public string m_strOperatorID;
        /// <summary>
        /// ��ǰ������Ϣ����
        /// </summary>
        private clsBIHPatientInfo m_objCurPatientInfo = null;
        private clsBIHOrder m_objCurBIHOrder = null;
        #endregion
        #region ���캯��
        public clsCtl_SearchOrderInfo()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objInputOrder = new clsDcl_InputOrder();
            m_objCommitOrder = new clsDcl_CommitOrder();
            m_objExecuteOrder = new clsDcl_ExecuteOrder();
            m_objSearchOrder = new clsDcl_SearchOrderInfo();
            m_strReportID = null;
        }
        #endregion
        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmSearchOrderInfo m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmSearchOrderInfo)frmMDI_Child_Base_in;
        }
        #endregion
        #region ��ʼ��
        /// <summary>
        /// ��ʼ����ѯ����
        /// </summary>
        public void m_InitializtionFindOrderCondition()
        {
            m_objCurPatientInfo = null;
            m_objCurBIHOrder = null;
            m_objViewer.m_lsvOrder.Items.Clear();
            m_objViewer.m_lsvExecOrderInfo.Items.Clear();
            m_objViewer.m_lsvChargeInfo.Items.Clear();
            m_objViewer.m_lsvPutMedicineInfo.Items.Clear();
            //			m_objViewer.m_txtAddOrderInfo.Text ="";
            //			m_objViewer.m_txtAddOrderInfo.Tag =null;
            //��ѯ����
            m_objViewer.m_cobFindTpye.Items.Clear();
            m_objViewer.m_cobFindTpye.Items.Add("����|����|ƴ����|�����");
            m_objViewer.m_cobFindTpye.Items.Add("������Ŀ����");
            m_objViewer.m_cobFindTpye.Items.Add("��ʼʱ��");
            m_objViewer.m_cobFindTpye.Items.Add("����ʱ��");
            m_objViewer.m_cobFindTpye.Items.Add("������");
            m_objViewer.m_cobFindTpye.Items.Add("�ύ��");
            m_objViewer.m_cobFindTpye.Items.Add("ִ����");
            m_objViewer.m_cobFindTpye.Items.Add("ֹͣ��");
            m_objViewer.m_cobFindTpye.Items.Add("ҽ����ˮ��");
            m_objViewer.m_cobFindTpye.SelectedIndex = 0;//Ĭ�� {"����|����|ƴ����|�����"}
            //FindMessage
            m_objViewer.m_lblFindMessage1.Text = "��ѯ����";
            //FindText
            //@ m_objViewer.m_txbFindText.Visible =true; /** @update by xzf
            //FindComboBox
            m_objViewer.m_cobFindComboBox.Visible = false;
            //FindDataTime
            m_objViewer.m_dtStartTime.Visible = false;
            m_objViewer.m_dtEndTime.Visible = false;
            m_objViewer.m_lblFindMessage2.Visible = false;
            //CheckBox
            m_objViewer.m_chkLongOrder.Checked = true;
            m_objViewer.m_chkTempOrder.Checked = true;
            m_objViewer.m_chkStatus0.Checked = true;
            m_objViewer.m_chkStatus1.Checked = true;
            m_objViewer.m_chkStatus2.Checked = true;
            m_objViewer.m_chkStatus3.Checked = true;
            m_objViewer.m_chkStatus4.Checked = true;
            m_objViewer.m_chkNeedFeel.Checked = false;
            m_objViewer.m_txtInHospitalNo.Focus();
        }
        public void m_SetFindOrderCondition()
        {
            switch (m_objViewer.m_cobFindTpye.SelectedIndex)
            {
                case 1://FindComboBox	{������Ŀ����}
                    //FindMessage
                    m_objViewer.m_lblFindMessage1.Text = "��ѯ����";
                    //FindText
                    m_objViewer.m_txbFindText.Visible = false;
                    //FindComboBox
                    m_objViewer.m_cobFindComboBox.Visible = true;
                    //FindDataTime
                    m_objViewer.m_dtStartTime.Visible = false;
                    m_objViewer.m_dtEndTime.Visible = false;
                    m_objViewer.m_lblFindMessage2.Visible = false;
                    //���ֵ
                    FillComboBoxOrderCate(m_objViewer.m_cobFindComboBox);
                    if (m_objViewer.m_cobFindComboBox.Items.Count > 0)
                    {
                        m_objViewer.m_cobFindComboBox.SelectedIndex = 0;
                    }
                    break;
                case 2://FindDataTime	{��ʼʱ��}
                    //FindMessage
                    m_objViewer.m_lblFindMessage1.Text = "��";
                    //FindText
                    m_objViewer.m_txbFindText.Visible = false;
                    //FindComboBox
                    m_objViewer.m_cobFindComboBox.Visible = false;
                    //FindDataTime
                    m_objViewer.m_dtStartTime.Visible = true;
                    m_objViewer.m_dtEndTime.Visible = true;
                    m_objViewer.m_lblFindMessage2.Visible = true;
                    break;
                case 3://FindDataTime	{����ʱ��}
                    //FindMessage
                    m_objViewer.m_lblFindMessage1.Text = "��";
                    //FindText
                    m_objViewer.m_txbFindText.Visible = false;
                    //FindComboBox
                    m_objViewer.m_cobFindComboBox.Visible = false;
                    //FindDataTime
                    m_objViewer.m_dtStartTime.Visible = true;
                    m_objViewer.m_dtEndTime.Visible = true;
                    m_objViewer.m_lblFindMessage2.Visible = true;
                    break;
                default: //FindText
                    //FindMessage
                    m_objViewer.m_lblFindMessage1.Text = "��ѯ����";
                    //FindText
                    //@ m_objViewer.m_txbFindText.Visible =true; /** @update by xzf
                    //FindComboBox
                    m_objViewer.m_cobFindComboBox.Visible = false;
                    //FindDataTime
                    m_objViewer.m_dtStartTime.Visible = false;
                    m_objViewer.m_dtEndTime.Visible = false;
                    m_objViewer.m_lblFindMessage2.Visible = false;
                    break;
            }
        }
        /// <summary>
        /// ���FindComboBox	{������Ŀ����}
        /// </summary>
        /// <param name="cbControl"></param>
        private void FillComboBoxOrderCate(ComboBox cbControl)
        {
            cbControl.Items.Clear();
            long lngRes = 0;
            clsT_aid_bih_ordercate_VO[] p_objItemArr = null;
            lngRes = m_objInputOrder.m_lngGetAidOrderCate(out p_objItemArr);
            if (lngRes > 0 && p_objItemArr != null && p_objItemArr.Length > 0)
            {
                //����ҽ�����Ͷ���
                clsOrderCate objItem = new clsOrderCate();
                for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
                {
                    objItem = new clsOrderCate();
                    objItem.m_objOrderCate = p_objItemArr[i1];
                    cbControl.Items.Add(objItem);
                }
                objItem = new clsOrderCate();
                objItem.m_objOrderCate.m_strVIEWNAME_VCHR = "ȫ��";
                objItem.m_objOrderCate.m_strORDERCATEID_CHR = "";
                cbControl.Items.Insert(0, objItem);
            }
        }
        #endregion

        #region  ������Ϣ
        /// <summary>
        /// ����ִ�е���Ϣ
        /// </summary>
        private void LoadExecOrderInfo()
        {
            m_objViewer.m_lsvExecOrderInfo.Items.Clear();
            if (m_objCurBIHOrder == null) return;
            string strOrderID = m_objCurBIHOrder.m_strOrderID;
            clsBIHExecOrder[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetExecuteOrderByOrderID(new string[] { strOrderID }, out objItemArr );
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                ListViewItem lviTemp = null;
                m_objViewer.m_lblRecordExecOrder.Text = objItemArr.Length.ToString();
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    #region ���ֵ
                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //ִ�е�ID
                    lviTemp.SubItems.Add(objItemArr[i1].m_strEOrderExecID);
                    //������
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCreator);
                    //����ʱ��
                    lviTemp.SubItems.Add(DateTimeToString(objItemArr[i1].m_dtECreateDate));
                    //ִ������
                    lviTemp.SubItems.Add(objItemArr[i1].m_intEExecuteDays.ToString());
                    //ִ�д���
                    lviTemp.SubItems.Add(objItemArr[i1].m_intEExecuteTimes.ToString());
                    //ִ��ʱ��
                    lviTemp.SubItems.Add(objItemArr[i1].m_strEExecuteDate);
                    //�ѽ���
                    if (objItemArr[i1].m_intEIsIncept == 1)
                        lviTemp.SubItems.Add("�ѽ���");
                    else
                        lviTemp.SubItems.Add("δ����");
                    //�״�ִ��
                    if (objItemArr[i1].m_intISFIRST_INT == 1)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");
                    //����
                    if (objItemArr[i1].m_intEIsFirst == 1)
                        lviTemp.SubItems.Add("����");
                    else if (objItemArr[i1].m_intEIsFirst == 2)
                        lviTemp.SubItems.Add("����");
                    else if (objItemArr[i1].m_intEIsFirst == 3)
                        lviTemp.SubItems.Add("�¿���");
                    //�޸���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strEOperator);
                    //ɾ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strEDeactivator);

                    lviTemp.Tag = objItemArr[i1];
                    m_objViewer.m_lsvExecOrderInfo.Items.Add(lviTemp);
                    #endregion
                }
            }
        }
        /// <summary>
        /// �����շ���Ϣ
        /// </summary>
        private void LoadChargeInfo()
        {
            m_objViewer.m_lsvChargeInfo.Items.Clear();
            if (m_objCurBIHOrder == null) return;
            string strOrderID = m_objCurBIHOrder.m_strOrderID;
            clsT_opr_bih_patientcharge_VO[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetPatientChargeByOrderID(new string[] { strOrderID }, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    ListViewItem lviTemp = null;
                    m_objViewer.m_lblRecordCharge.Text = objItemArr.Length.ToString();
                    #region ���ֵ
                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //��Ч����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strACTIVE_DAT);
                    //���㲡��
                    lviTemp.SubItems.Add(objItemArr[i1].m_strClacAreaName);
                    //�����ص�
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCreateAreaName);
                    //���ú������
                    lviTemp.SubItems.Add(objItemArr[i1].m_strItemIpcalcTypeName);
                    //���÷�Ʊ���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strItemIpinvTypeName);
                    //�շ���Ŀ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCHARGEITEMNAME_CHR);
                    //סԺ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_dblUNITPRICE_DEC.ToString("0.0000"));
                    //����
                    lviTemp.SubItems.Add(objItemArr[i1].m_fltAMOUNT_DEC.ToString() + " " + objItemArr[i1].m_strUNIT_VCHR);
                    //�ۿ۱���
                    lviTemp.SubItems.Add(objItemArr[i1].m_fltDISCOUNT_DEC.ToString());
                    //�Ƿ��Է���Ŀ
                    lviTemp.SubItems.Add(objItemArr[i1].m_strIsmePayName);
                    //¼������
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCreateTypeName);
                    //¼����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCreatorName);
                    //¼��ʱ��
                    lviTemp.SubItems.Add(objItemArr[i1].m_strCREATE_DAT);

                    lviTemp.Tag = objItemArr[i1];
                    m_objViewer.m_lsvChargeInfo.Items.Add(lviTemp);
                    #endregion
                }
            }
        }
        /// <summary>
        /// �����ҩ��Ϣ
        /// </summary>
        private void LoadPutMedicineInfo()
        {
            m_objViewer.m_lsvPutMedicineInfo.Items.Clear();
            if (m_objCurBIHOrder == null) return;
            string strOrderID = m_objCurBIHOrder.m_strOrderID;
            clsT_Bih_Opr_Putmeddetail_VO[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetPutMedDetailByOrderID(new string[] { strOrderID }, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                ListViewItem lviTemp = null;
                m_objViewer.m_lblRecordPutMedicine.Text = objItemArr.Length.ToString();
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    #region ���ֵ
                    //���
                    lviTemp = new ListViewItem((i1 + 1).ToString());
                    //��ҩ
                    if (objItemArr[i1].m_intISPUT_INT == 1)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");
                    //ִ������
                    lviTemp.SubItems.Add(objItemArr[i1].m_strOrderExecTypeName);
                    //ҩƷ���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strASSISTCODE_CHR);
                    //ҩƷ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strMEDNAME_VCHR);
                    //ҩƷ���
                    lviTemp.SubItems.Add(objItemArr[i1].m_strMedSpec);
                    //ִ��Ƶ��
                    lviTemp.SubItems.Add(objItemArr[i1].m_strFreqName);
                    //�÷�
                    lviTemp.SubItems.Add(objItemArr[i1].m_strUsageName);
                    //����
                    lviTemp.SubItems.Add(objItemArr[i1].m_dblGET_DEC.ToString() + " " + objItemArr[i1].m_strUNIT_VCHR);
                    //����
                    lviTemp.SubItems.Add(objItemArr[i1].m_dblUNITPRICE_MNY.ToString("0.0000"));
                    //����
                    if (objItemArr[i1].m_intISRICH_INT == 1)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");
                    //��ҩ����
                    lviTemp.SubItems.Add(objItemArr[i1].m_strPutTypeName);

                    lviTemp.Tag = objItemArr[i1];
                    m_objViewer.m_lsvPutMedicineInfo.Items.Add(lviTemp);
                    #endregion
                }
            }
        }
        /// <summary>
        /// ���븽�ӵ���Ϣ
        /// </summary>
        private void LoadAddOrderInfo()
        {
            //			m_objViewer.m_txtAddOrderInfo.Text ="";
            if (m_objCurBIHOrder == null) return;
            string strOrderID = m_objCurBIHOrder.m_strOrderID;
            clsOrderAttach[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetAttachOrderByOrderID(new string[] { strOrderID }, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                m_objViewer.m_lblReordAddOrder.Text = objItemArr.Length.ToString();
                this.m_objViewer.m_lsvApplyInfo.Items.Clear();
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    ListViewItem lv = new ListViewItem(m_objCurBIHOrder.m_strName, 0);
                    lv.Tag = objItemArr[i1].m_strID;
                    this.m_objViewer.m_lsvApplyInfo.Items.Add(lv);
                    //					strTem +="ҽ�����ƣ�" + m_objCurBIHOrder.m_strName + "\r\n";//objItemArr[i1].m_strOrderID
                    //					strTem +="���ӵ��ݱ�ţ�" + objItemArr[i1].m_strID + "\r\n";
                    //					strTem +="�����ˣ�" + objItemArr[i1].m_strCreatorID + "\t";
                    //					strTem +="����ʱ�䣺" + objItemArr[i1].m_strCreatorID + "\t\r\n";
                    //					strTem +="״̬��" + objItemArr[i1].m_strStatusName + "\r\n";
                    //					strTem +="����" + objItemArr[i1].m_strContent + "\r\n";
                    //					strTem +="\r\n";
                }
                //				m_objViewer.m_txtAddOrderInfo.Text =strTem;
            }
        }
        #endregion
        public void m_mthShowApplyInfo()
        {
            if (this.m_objViewer.m_lsvApplyInfo.SelectedItems.Count == 0)
            {
                return;
            }
            //			clsBIHCanExecOrder objOrders=(clsBIHCanExecOrder)(trvAddBills.SelectedNode.Tag);
            //ҽ������ID
            string strOrderCateID = m_objCurBIHOrder.m_strOrderDicCateID.Trim();
            //ҽ��ID
            string strOrderID = m_objCurBIHOrder.m_strOrderID.Trim();
            //���ӵ���ID
            string strAttachID = this.m_objViewer.m_lsvApplyInfo.SelectedItems[0].Tag.ToString(); ;
            //��ϵ��ID
            //			clsRelation_VOArr objRelation=new clsRelation_VOArr();
            //			long lngR=objRelation.m_lngGetRelation("sourceitemid_vchr='"+strOrderID.Trim()+"'");
            //			string strRelationID=objRelation.m_objValues[0].m_strATTARELAID_CHR.Trim();
            //			DataTable dtbAddBills=null;
            //			long lngRes=m_lngGetAddBillByOrderID(strOrderID.Trim(),out dtbAddBills);

            clsT_aid_bih_ordercate_VO objResult = null;
            clsDcl_InputOrder objTem = new clsDcl_InputOrder();
            long lngRes = objTem.m_lngGetAidOrderCateByID(strOrderCateID, out objResult);
            if (lngRes <= 0 || objResult == null) return;

            string strDllName = objResult.m_strDLLNAME_VCHR;
            string strClassName = objResult.m_strCLASSNAME_VCHR;
            string strInsertName = objResult.m_strOPRADD_VCHR;
            string strUpdateName = objResult.m_strOPRUPD_VCHR;

            System.Reflection.Assembly objAsm = System.Reflection.Assembly.LoadFrom(strDllName);

            if (objAsm == null) return;
            object[] objParams = new object[1];

            objParams[0] = strAttachID.Trim();

            object obj;
            try
            {

                obj = objAsm.CreateInstance(strClassName, true, System.Reflection.BindingFlags.Default, null, objParams, null, new object[0] { });
            }
            catch (System.Exception err)
            {
                string strMsg = err.Message.ToString();
                MessageBox.Show(strMsg);
                return;
            }
            if (obj == null) return;
            //�򿪴���
            ((Form)obj).ShowDialog();
            //			Type objType=obj.GetType();	
            //			System.Reflection.PropertyInfo objMi=objType.GetProperty("m_StrRecordID");
            //			string strAddBillRecordID=objMi.GetValue(obj,null).ToString();

        }
        #region ������Ϣ
        #region סԺ��
        public void m_FindItemInHospitalNo(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsT_Opr_Bih_Register_VO[] objItemArr;
            long ret = new clsDcl_SearchOrderInfo().m_lngFindHospitalNo(strFindCode, out objItemArr);
            if ((ret > 0) && (objItemArr != null))
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (m_objViewer.LoginInfo != null)
                {
                    IList ilUsableAreaID = m_objViewer.LoginInfo.m_ilUsableAreaID;
                    clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                    objItemArr = (clsT_Opr_Bih_Register_VO[])(objInputOrder.GetUsableRegisterObject(objItemArr, ilUsableAreaID)).ToArray(typeof(clsT_Opr_Bih_Register_VO));
                }
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].m_strINPATIENTID_CHR);
                }
            }
        }

        public void m_InitListViewInHospitalNo(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("סԺ��", 100, HorizontalAlignment.Left);
            lvwList.Width = 120;
        }

        public void m_SelectItemInHospitalNo(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_objViewer.m_txtInHospitalNo.Text = lviSelected.Text;
                m_objViewer.m_txtInHospitalNo.Tag = lviSelected.Text;
                clsBIHPatientInfo objPatient = null;
                long lngRes = m_objSearchOrder.m_lngGetPatientByInHospitalNo(lviSelected.Text, out objPatient);
                m_objCurPatientInfo = objPatient;
                FillPatientInfo();
            }
        }
        #endregion
        public void SetpatientInfo(string InpatientId)
        {
            clsBIHPatientInfo objPatient = null;
            long lngRes = m_objSearchOrder.m_lngGetPatientByInHospitalNo(InpatientId, out objPatient);
            m_objCurPatientInfo = objPatient;
            FillPatientInfo();
            m_FindOrder(false);
        }
        #region ����
        public void m_FindItemArea(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            clsBIHArea[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngFindArea(strFindCode, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                //��ȡ��Ȩ�޷��ʵĲ���ID����
                if (m_objViewer.LoginInfo != null)
                {
                    IList ilUsableAreaID = m_objViewer.LoginInfo.m_ilUsableAreaID;
                    clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
                    objItemArr = (clsBIHArea[])(objInputOrder.GetUsableAreaObject(objItemArr, ilUsableAreaID)).ToArray(typeof(clsBIHArea));
                }
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].m_strAreaName);
                    lvi.Tag = objItemArr[i];
                }
            }
        }

        public void m_InitListViewArea(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("��������", 120, HorizontalAlignment.Left);
            lvwList.Width = 140;
        }

        public void m_SelectItemArea(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_objViewer.m_txtArea.Text = lviSelected.Text;
                m_objViewer.m_txtArea.Tag = lviSelected.Tag;

                m_objViewer.m_txtBedNo.Text = "";
                m_objViewer.m_txtBedNo.Tag = "";
                m_objViewer.m_txtBedNo.Focus();
            }
        }
        #endregion
        #region ����
        public void m_FindItemBed(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            //��ȡ����ID
            string strAreaID = "";
            if (m_objViewer.m_txtArea.Text.Trim() == "" || m_objViewer.m_txtArea.Tag == null)
            {
                MessageBox.Show("����ָ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtArea.Focus();
                return;
            }
            /** update by xzf (05-09-29) */
            try
            {
                strAreaID = ((clsBIHArea)m_objViewer.m_txtArea.Tag).m_strAreaID;
            }
            catch
            {
                strAreaID = m_objViewer.m_txtArea.Tag.ToString().Trim();
            }
            /* <<============================ */
            weCare.Core.Entity.clsBIHBed[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetBedByArea(strAreaID, strFindCode, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                for (int i = 0; i < objItemArr.Length; i++)
                {
                    ListViewItem lvi = lvwList.Items.Add(objItemArr[i].m_strBedName);
                    lvi.Tag = objItemArr[i];
                }
            }
        }

        public void m_InitListViewBed(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Columns.Add("����", 100, HorizontalAlignment.Left);
            lvwList.Width = 100;
        }

        public void m_SelectItemBed(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                m_objViewer.m_txtBedNo.Text = lviSelected.Text;
                m_objViewer.m_txtBedNo.Tag = lviSelected.Tag;
                /** update by xzf (05-09-29) */
                string strAreaID = "";
                try
                {
                    strAreaID = (m_objViewer.m_txtArea.Tag as clsBIHArea).m_strAreaID;
                }
                catch
                {
                    strAreaID = m_objViewer.m_txtArea.Tag.ToString().Trim();
                }
                /** <<============================= */
                string strBedID = (m_objViewer.m_txtBedNo.Tag as weCare.Core.Entity.clsBIHBed).m_strBedID;
                clsBIHPatientInfo objPatient = null;
                long lngRes = m_objSearchOrder.m_lngGetPatientByAreaBed(strAreaID, strBedID, out objPatient);
                m_objCurPatientInfo = objPatient;
                FillPatientInfo();
            }
        }
        #endregion
        #region ����
        /// <summary>
        /// ��ղ�����Ϣ
        /// </summary>
        private void EmptyPatientInfo()
        {
            m_objViewer.m_txtInHospitalNo.Text = "";
            m_objViewer.m_txtInHospitalNo.Tag = null;
            m_objViewer.m_txtArea.Text = "";
            m_objViewer.m_txtArea.Tag = null;
            m_objViewer.m_txtBedNo.Text = "";
            m_objViewer.m_txtBedNo.Tag = null;
            m_objViewer.m_txtName.Text = "";
            m_objViewer.m_txtSex.Text = "";
            m_objViewer.m_txtPayType.Text = "";
            m_objViewer.m_txtPrePayMoney.Text = "";
            m_objViewer.m_txtInHospitalDate.Text = "";
            m_objViewer.m_txtInDays.Text = "";
            m_objViewer.m_txtDiagnose.Text = "";
        }
        /// <summary>
        /// ��䲡����Ϣ
        /// </summary>
        /// <param name="objItem"></param>
        private void FillPatientInfo()
        {
            EmptyPatientInfo();
            /** @add by xzf (05-09-30)
             * ��ʶ�л��˲���
             */
            this.isCurrPatient = false;
            this.m_objViewer.cbo_bihHistory.Visible = false;
            /* <<========================== */
            if (m_objCurPatientInfo == null) return;

            clsBIHPatientInfo objItem = new clsBIHPatientInfo();
            objItem = m_objCurPatientInfo;
            m_objViewer.m_txtInHospitalNo.Text = objItem.m_strInHospitalNo;
            m_objViewer.m_txtInHospitalNo.Tag = objItem.m_strInHospitalNo;
            m_objViewer.m_txtArea.Text = objItem.m_strAreaName;
            m_objViewer.m_txtArea.Tag = objItem.m_strAreaID;
            m_objViewer.m_txtBedNo.Text = objItem.m_strBedName;
            m_objViewer.m_txtBedNo.Tag = objItem.m_strBedID;
            m_objViewer.m_txtName.Text = objItem.m_strPatientName;
            m_objViewer.m_txtSex.Text = objItem.m_strSex;
            m_objViewer.m_txtPayType.Text = objItem.m_strPayTypeName;
            m_objViewer.m_txtPrePayMoney.Text = m_dmlGetMoney(objItem.m_strRegisterID).ToString("0.00");
            try
            {
                m_objViewer.m_txtInHospitalDate.Text = objItem.m_dtInHospital.ToString("yyyy��MM��dd��");
                TimeSpan tsTimeSpan = DateTime.Now.Date - objItem.m_dtInHospital.Date;
                m_objViewer.m_txtInDays.Text = tsTimeSpan.Days.ToString();
            }
            catch { }
            m_objViewer.m_txtDiagnose.Text = objItem.m_strDiagnose;

            #region �����ʾ	glzhang	2005.10.24
            string strDiagnose = "\n   ������ϣ�" + objItem.m_strMzdiagnose_vchr + "   \n";
            strDiagnose += "\n   ��Ժ��ϣ�ICD10����" + objItem.m_strDiagnose + "   \n";
            if (objItem.m_strDiagnose_vchr.Length > 0)
            {
                strDiagnose += "\n   ��Ժ��ϣ�ҽ������" + objItem.m_strDiagnose_vchr + "   \n";
            }
            else
            {
                strDiagnose += "\n   ��Ժ��ϣ�ҽ��������  \n";
            }
            m_objViewer.toolTip1.SetToolTip(m_objViewer.m_txtDiagnose, strDiagnose);
            #endregion
        }
        /// <summary>
        /// ��ȡ���	����סԺID
        /// </summary>
        /// <param name="strRegisterID">סԺID</param>
        /// <returns></returns>
        private double m_dmlGetMoney(string strRegisterID)
        {
            double dblBalanceMoney = 0;
            long lngRes = m_objSearchOrder.m_lngGetBalanceMoneyByRegisterID(strRegisterID, out dblBalanceMoney);
            return dblBalanceMoney;
        }
        #endregion
        #endregion
        #region ��ѯҽ��
        /// <summary>
        /// ����ҽ��
        /// </summary>
        public void m_FindOrder(bool blnWarning)
        {
            if (m_objCurPatientInfo == null || m_objCurPatientInfo.m_strRegisterID.Trim() == "")
            {
                if (blnWarning)
                {
                    MessageBox.Show("����ѡ����", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_objViewer.m_txtInHospitalNo.Focus();
                }
                return;
            }
            m_objViewer.m_lsvOrder.Items.Clear();
            string strSqlWhere = strGetSQLWhere();
            clsBIHOrder[] objItemArr;
            long lngRes = m_objSearchOrder.m_lngGetOrderByCondition(strSqlWhere, out objItemArr );
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                FillOrder(objItemArr);
            }
            /** add by xzf (05-09-30)
             * �����ʷסԺ��¼
             */
            if (isCurrPatient) return;
            string registerId = m_objCurPatientInfo.m_strRegisterID;
            DataTable dt = m_objSearchOrder.getBihHistory(registerId);
            this.m_objViewer.cbo_bihHistory.Visible = (dt.Rows.Count > 1);
            if (dt.Rows.Count > 1)
            {
                dt.DefaultView.Sort = dt.Columns[1].ToString() + " desc";
                this.m_objViewer.cbo_bihHistory.DataSource = dt.DefaultView;
                this.m_objViewer.cbo_bihHistory.DisplayMember = dt.Columns[1].ToString();
                this.m_objViewer.cbo_bihHistory.ValueMember = dt.Columns[0].ToString();
                this.m_objViewer.cbo_bihHistory.Text = "ѡ��סԺ����";
            }
            /* <<=================================================== */
        }

        /** @add by xzf (05-09-30) */
        private bool isCurrPatient = false;  //�Ƿ�������

        public void cbo_bihHistorySelectedIndexChanged()
        {
            this.isCurrPatient = true;
            string registerId = this.m_objViewer.cbo_bihHistory.SelectedValue.ToString();
            m_objCurPatientInfo.m_strRegisterID = registerId;
            this.m_objViewer.cmdFindOrder_Click(null, null);
        }

        public void m_SelectedIndexChangedOrder()
        {
            if (m_objViewer.m_lsvOrder.SelectedItems.Count != 1) return;
            m_objCurBIHOrder = ((clsBIHOrder)m_objViewer.m_lsvOrder.SelectedItems[0].Tag);
            //����{ִ�е���Ϣ���շ���Ϣ����ҩ��Ϣ�����ӵ���Ϣ}
            LoadExecOrderInfo();
            LoadChargeInfo();
            LoadPutMedicineInfo();
            LoadAddOrderInfo();
        }
        #region ����
        /// <summary>
        /// ��ȡ��ѯҽ����SQL��Where����	������Where
        /// </summary>
        /// <returns></returns>
        private string strGetSQLWhere()
        {
            string strSqlWhere = "", strTem = "";
            //��Ժ�Ǽ�ID
            #region ��Ժ�Ǽ�ID
            if (m_objCurPatientInfo == null || m_objCurPatientInfo.m_strRegisterID.Trim() == "") return " 1=2 ";
            if (strSqlWhere.Trim() == "")
            {
                strSqlWhere += " Trim(REGISTERID_CHR)='" + m_objCurPatientInfo.m_strRegisterID.Trim() + "' ";
            }
            else
            {
                strSqlWhere += " and Trim(REGISTERID_CHR)='" + m_objCurPatientInfo.m_strRegisterID.Trim() + "' ";
            }
            #endregion
            //��ѯ��
            #region ��ѯ��
            switch (m_objViewer.m_cobFindTpye.SelectedIndex)
            {
                case 0://{"����|����|ƴ����|�����"}
                    #region {"����|����|ƴ����|�����"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " NAME_VCHR like '%" + strTem + "% '";
                        }
                        else
                        {
                            strSqlWhere += " and NAME_VCHR like '%" + strTem + "%' ";
                        }
                    }
                    #endregion
                    break;
                case 1://{"������Ŀ����"}
                    #region {"������Ŀ����"}
                    try
                    {
                        strTem = ((clsOrderCate)(m_objViewer.m_cobFindComboBox.SelectedItem)).m_objOrderCate.m_strORDERCATEID_CHR.Trim();
                    }
                    catch { }
                    if (strTem.Trim() != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " Trim(OrderCateID_Chr)='" + strTem + "' ";
                        }
                        else
                        {
                            strSqlWhere += " and Trim(OrderCateID_Chr)='" + strTem + "' ";
                        }
                    }
                    #endregion
                    break;
                case 2://{"��ʼʱ��"}
                    #region {"��ʼʱ��"}
                    strTem = m_objViewer.m_dtStartTime.Value.ToString("yyyy-MM-dd");
                    strTem += " 00:00:00";
                    if (strSqlWhere.Trim() == "")
                    {
                        strSqlWhere += " STARTDATE_DAT>=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    else
                    {
                        strSqlWhere += " and  STARTDATE_DAT>=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    strTem = m_objViewer.m_dtEndTime.Value.ToShortDateString();
                    strTem += " 23:59:59";
                    if (strSqlWhere.Trim() == "")
                    {
                        strSqlWhere += " STARTDATE_DAT<=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    else
                    {
                        strSqlWhere += " and  STARTDATE_DAT<=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    #endregion
                    break;
                case 3://{"����ʱ��"}
                    #region {"����ʱ��"}
                    strTem = m_objViewer.m_dtStartTime.Value.ToString("yyyy-MM-dd");
                    strTem += " 00:00:00";
                    if (strSqlWhere.Trim() == "")
                    {
                        strSqlWhere += " FINISHDATE_DAT>=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    else
                    {
                        strSqlWhere += " and  FINISHDATE_DAT>=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    strTem = m_objViewer.m_dtEndTime.Value.ToShortDateString();
                    strTem += " 23:59:59";
                    if (strSqlWhere.Trim() == "")
                    {
                        strSqlWhere += " FINISHDATE_DAT<=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    else
                    {
                        strSqlWhere += " and  FINISHDATE_DAT<=To_Date('" + strTem + "','YYYY-MM-DD hh24:mi:ss') ";
                    }
                    #endregion
                    break;
                case 4://{"������"}
                    #region {"������"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " CREATOR_CHR like '%" + strTem + "%' ";
                        }
                        else
                        {
                            strSqlWhere += " and CREATOR_CHR like '%" + strTem + "%' ";
                        }
                    }
                    #endregion
                    break;
                case 5://{"�ύ��"}
                    #region {"�ύ��"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " POSTER_CHR like '%" + strTem + "%' ";
                        }
                        else
                        {
                            strSqlWhere += " and POSTER_CHR like '%" + strTem + "%' ";
                        }
                    }
                    #endregion
                    break;
                case 6://{"ִ����"}
                    #region {"ִ����"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " EXECUTOR_CHR like '%" + strTem + "%' ";
                        }
                        else
                        {
                            strSqlWhere += " and EXECUTOR_CHR like '%" + strTem + "%' ";
                        }
                    }
                    #endregion
                    break;
                case 7://{"ֹͣ��"}
                    #region {"ֹͣ��"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem != "")
                    {
                        if (strSqlWhere.Trim() == "")
                        {
                            strSqlWhere += " STOPER_CHR like '%" + strTem + "%' ";
                        }
                        else
                        {
                            strSqlWhere += " and STOPER_CHR like '%" + strTem + "%' ";
                        }
                    }
                    #endregion
                    break;
                case 8://{"ҽ����ˮ��"}
                    #region {"ҽ����ˮ��"}
                    strTem = m_objViewer.m_txbFindText.Text.Trim();
                    if (strTem == null) strTem = "";
                    if (strSqlWhere.Trim() == "")
                    {
                        strSqlWhere += " ORDERID_CHR ='" + strTem.Trim() + "' ";
                    }
                    else
                    {
                        strSqlWhere += " and ORDERID_CHR ='" + strTem.Trim() + "' ";
                    }
                    #endregion
                    break;
            }
            #endregion
            //ҽ������	{1=����;2=��ʱ}
            #region ҽ������
            if (!(m_objViewer.m_chkLongOrder.Checked && m_objViewer.m_chkTempOrder.Checked))
            {
                strTem = "";
                if (m_objViewer.m_chkLongOrder.Checked)
                {
                    if (strTem.Trim() == "")
                        strTem += " ExecuteType_Int in (1";
                    else
                        strTem += ",1";
                }
                if (m_objViewer.m_chkTempOrder.Checked)
                {
                    if (strTem.Trim() == "")
                        strTem += " ExecuteType_Int in (2";
                    else
                        strTem += ",2";
                }
                if (strTem.Trim() != "")
                {
                    if (strSqlWhere.Trim() == "")
                        strSqlWhere += strTem + ") ";
                    else
                        strSqlWhere += " and " + strTem + ") ";
                }
                else
                {
                    strSqlWhere = "1=2";
                }
            }
            #endregion
            //ִ��״̬��	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}
            #region ִ��״̬
            strTem = "";
            if (m_objViewer.m_chkStatus0.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (0";
                else
                    strTem += ",0";
            }
            if (m_objViewer.m_chkStatus1.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (1,5";
                else
                    strTem += ",1,5";
            }
            if (m_objViewer.m_chkStatus2.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (2";
                else
                    strTem += ",2";
            }
            if (m_objViewer.m_chkStatus3.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (3,6";
                else
                    strTem += ",3,6";
            }
            if (m_objViewer.m_chkStatus4.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (4";
                else
                    strTem += ",4";
            }
            if (m_objViewer.m_chkStatus5.Checked)
            {
                if (strTem.Trim() == "")
                    strTem += " Status_Int in (7";
                else
                    strTem += ",7";
            }
            if (strTem.Trim() != "")
            {
                if (strSqlWhere.Trim() == "")
                    strSqlWhere += strTem + ") ";
                else
                    strSqlWhere += " and " + strTem + ") ";
            }
            else
            {
                strSqlWhere = "1=2";
            }
            #endregion
            //Ƥ��	
            #region Ƥ��
            if (m_objViewer.m_chkNeedFeel.Checked)
            {
                if (strSqlWhere.Trim() == "")
                    strSqlWhere += " IsNeedFeeL=1 ";
                else
                    strSqlWhere += " and IsNeedFeeL=1 ";
            }
            #endregion
            return strSqlWhere;
        }
        /// <summary>
        /// ���ҽ��ListView
        /// </summary>
        /// <param name="p_objItemArr">ҽ����¼����</param>
        private void FillOrder(clsBIHOrder[] p_objItemArr)
        {
            if (p_objItemArr == null || p_objItemArr.Length <= 0) return;
            m_objViewer.m_lsvOrder.Items.Clear();
            ListViewItem lviTemp = null;
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                #region ���ֵ
                //���
                lviTemp = new ListViewItem((i1 + 1).ToString());
                //����
                lviTemp.SubItems.Add(p_objItemArr[i1].m_intRecipenNo.ToString());
                //����
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strName);
                //��|��
                if (p_objItemArr[i1].m_intExecuteType == 1)
                    lviTemp.SubItems.Add("��");
                else if (p_objItemArr[i1].m_intExecuteType == 2)
                    lviTemp.SubItems.Add("��");
                else
                    lviTemp.SubItems.Add("");
                //ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;7-�˻�;}
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strStatusName);
                //����|��λ		
                if (p_objItemArr[i1].m_dmlDosage > 0)
                    lviTemp.SubItems.Add(p_objItemArr[i1].m_dmlDosage.ToString() + " " + p_objItemArr[i1].m_strDosageUnit);
                else
                    lviTemp.SubItems.Add("");
                //����|��λ	
                if (p_objItemArr[i1].m_dmlUse > 0)
                    lviTemp.SubItems.Add(p_objItemArr[i1].m_dmlUse.ToString() + " " + p_objItemArr[i1].m_strUseunit);
                else
                    lviTemp.SubItems.Add("");
                //����|��λ
                if (p_objItemArr[i1].m_dmlGet > 0)
                    lviTemp.SubItems.Add(p_objItemArr[i1].m_dmlGet.ToString() + " " + p_objItemArr[i1].m_strGetunit);
                else
                    lviTemp.SubItems.Add("");
                //����
                lviTemp.SubItems.Add(p_objItemArr[i1].m_dmlPrice.ToString());
                //ִ��Ƶ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strExecFreqName);
                //�÷�
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strDosetypeName);
                //��ҽ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strParentName);
                //��ʼʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtStartDate));
                //����ʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtFinishDate));
                //������
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strCreator);
                //����ʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtCreatedate));
                //�ύ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strPoster);
                //�ύʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtPostdate));
                //����ύ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strASSESSORFOREXEC_CHR);
                //����ύʱ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strASSESSORFOREXEC_DAT);
                //ִ����
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strExecutor);
                //ִ��ʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtExecutedate));
                //ֹͣ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strStoper);
                //ֹͣʱ��
                lviTemp.SubItems.Add(DateTimeToString(p_objItemArr[i1].m_dtStopdate));
                //���ֹͣ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strASSESSORFORSTOP_CHR);
                //���ֹͣʱ��
                lviTemp.SubItems.Add(p_objItemArr[i1].m_strASSESSORFORSTOP_DAT);
                //�����ɫ			
                System.Drawing.Color back;
                System.Drawing.Color fore;
                clsOrderStatus.s_mthGetColorByStatus(
                    p_objItemArr[i1].m_intExecuteType,
                    p_objItemArr[i1].m_intStatus,
                    out back,
                    out fore);
                lviTemp.BackColor = back;
                lviTemp.ForeColor = fore;

                lviTemp.Tag = p_objItemArr[i1];
                m_objViewer.m_lsvOrder.Items.Add(lviTemp);
                #endregion
            }
        }
        /// <summary>
        /// ʱ������ת��
        /// </summary>
        /// <param name="dtValue"></param>
        /// <returns></returns>
        private string DateTimeToString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue.Date)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion
        #endregion
        #region ��ӡ
        /// <summary>
        /// ��ӡҽ��
        /// </summary>
        public void m_PrintOrder()
        {
            if (m_objCurPatientInfo == null)
            {
                MessageBox.Show("����ѡ����", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtInHospitalNo.Focus();
                return;
            }
            string strRegisterID = m_objCurPatientInfo.m_strRegisterID;
            if (strRegisterID.Trim() == "") return;
            //��ʾ��ӡ����ҳ��
            frmPrintOrder objPrintOrder = new frmPrintOrder(strRegisterID);
            objPrintOrder.ShowDialog();
        }
        #endregion
    }
}
