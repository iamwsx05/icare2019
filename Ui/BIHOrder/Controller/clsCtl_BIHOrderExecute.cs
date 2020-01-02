using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms; 
using com.digitalwave.iCare.BIHOrder.Control;
using System.Collections;
using com.digitalwave.iCare.gui.LIS;
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ��ʿ����վ:		�鿴���ҽ��	�߼����Ʋ� 
    /// </summary>
    public class clsCtl_BIHOrderExecute : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����

        /// <summary>
        /// ��ʱ��,���ڶ�ʱˢ������ 
        /// </summary>
        Timer m_timer;

        clsDcl_ExecuteOrder m_objManage = null;
        clsDcl_InputOrder m_objInputOrder = null;
        public string m_strReportID;
        /// <summary>
        /// ������ID
        /// </summary>
        public string m_strOperatorID;
        /// <summary>
        /// ������
        /// </summary>
        public string m_strOperatorName;
        /// <summary>
        /// ҽ������ID	{ҩƷ}
        /// </summary>
        private string m_strMedicineOrderTypeID = "";
        /// <summary>
        /// ҽ��������Ϣ����	[����]
        /// </summary>
        public clsOrderBaseInfo[] m_objOrderBaseInfoArr = null;
        /// <summary>
        /// ҽ��������Ϣ����	[����]
        /// </summary>
        public clsPatientChargeInfo[] m_objPatientChargeInfoArr = null;
        /// <summary>
        /// ��ִ��ҽ������
        /// </summary>
        public clsBIHCanExecOrder[] m_objBIHCanExecOrderArr;
        /// <summary>
        /// ҽ������Vo	[����]
        /// </summary>
        private clsT_aid_bih_ordercate_VO[] m_objOrdercateArr = null;

        /// <summary>
        /// �Ƿ�ʹ�ö�ͯ�۸� 9015
        /// </summary>
        bool isUseChildPrice { get; set; }

        #endregion
        #region ���캯��
        public clsCtl_BIHOrderExecute()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_ExecuteOrder();
            m_objInputOrder = new clsDcl_InputOrder();
            m_strReportID = null;
        }
        #endregion
        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmBIHOrderExecute m_objViewer;
        com.digitalwave.iCare.BIHOrder.frmEditOnceAgain m_objViewer2 = new frmEditOnceAgain();
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBIHOrderExecute)frmMDI_Child_Base_in;

        }
        #endregion
        #region ���
        /// <summary>
        /// ���ListView��Ϣ�������
        /// </summary>
        public void EmptyListView()
        {
            m_objOrderBaseInfoArr = null;
            m_objPatientChargeInfoArr = null;
            m_objBIHCanExecOrderArr = null;
            ClearListView();
        }
        /// <summary>
        /// ���ListView
        /// </summary>
        private void ClearListView()
        {
            m_objViewer.m_lsvOrderBaseInfo.Items.Clear();
            m_objViewer.m_lsvPatientChargeInfo.Items.Clear();
            m_objViewer.m_lsvToolTip.Items.Clear();
            m_objViewer.chkSelectAll.Checked = false;
        }
        #endregion
        #region ����
        public void m_LoadInitialization()
        {
            this.isUseChildPrice = (new clsDcl_ExecuteOrder()).IsUseChildPrice();
            SetOrderColor();
            FillComboBoxOrderCate(m_objViewer.m_cobOrderCate);
            if (m_objViewer.m_cobOrderCate.Items.Count > 0) m_objViewer.m_cobOrderCate.SelectedIndex = 0;//Ĭ��ȫ��
        }
        /// <summary>
        /// ���FindComboBox	{������Ŀ����}
        /// </summary>
        /// <param name="cbControl"></param>
        private void FillComboBoxOrderCate(ComboBox cbControl)
        {
            cbControl.Items.Clear();
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngGetAidOrderCate(out m_objOrdercateArr);
            if (lngRes > 0 && m_objOrdercateArr != null && m_objOrdercateArr.Length > 0)
            {
                //����ҽ�����Ͷ���	ViewName ��ͬ��ֻ����һ��
                ArrayList alViewName = new ArrayList();
                clsOrderCate objItem = new clsOrderCate();
                for (int i1 = 0; i1 < m_objOrdercateArr.Length; i1++)
                {
                    if (!alViewName.Contains(m_objOrdercateArr[i1].m_strVIEWNAME_VCHR.Trim()))
                    {
                        objItem = new clsOrderCate();
                        objItem.m_objOrderCate = m_objOrdercateArr[i1];
                        cbControl.Items.Add(objItem);
                        alViewName.Add(m_objOrdercateArr[i1].m_strVIEWNAME_VCHR.Trim());
                    }
                }
                objItem = new clsOrderCate();
                objItem.m_objOrderCate.m_strVIEWNAME_VCHR = "ȫ��";
                objItem.m_objOrderCate.m_strORDERCATEID_CHR = "";
                cbControl.Items.Insert(0, objItem);
            }
        }
        #endregion
        #region ���ListView
        /// <summary>
        /// ���ListView
        /// </summary>
        private void FillListView()
        {
            m_objViewer.m_lsvOrderBaseInfo.Items.Clear();
            m_objViewer.m_lsvPatientChargeInfo.Items.Clear();
            //��������Ϣ
            #region ��������Ϣ
            if (m_objOrderBaseInfoArr == null || m_objOrderBaseInfoArr.Length <= 0) return;
            ListViewItem lviTemp = null;
            clsT_Opr_Bih_OrderFeel_VO objTem;
            for (int i1 = 0; i1 < m_objOrderBaseInfoArr.Length; i1++)
            {
                clsBIHCanExecOrder objItem = m_objOrderBaseInfoArr[i1].m_objBIHCanExecOrder;
                //
                lviTemp = new ListViewItem((i1 + 1).ToString());
                if (i1 > 0 && objItem.m_strRegisterID.Trim() == m_objOrderBaseInfoArr[i1 - 1].m_objBIHCanExecOrder.m_strRegisterID.Trim())
                {
                    //����
                    lviTemp.SubItems.Add("");
                    //����
                    lviTemp.SubItems.Add("");
                }
                else
                {
                    //����
                    lviTemp.SubItems.Add(objItem.m_strBedName);
                    //����
                    lviTemp.SubItems.Add(objItem.m_strPatientName);
                }
                //����
                lviTemp.SubItems.Add(objItem.m_intRecipenNo.ToString());
                //��/��
                if (objItem.m_intExecuteType == 1)
                {
                    lviTemp.SubItems.Add("��");
                }
                else
                {
                    if (objItem.m_intExecuteType == 2)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");
                }
                //ҽ������
                lviTemp.SubItems.Add(objItem.m_strName);
                //��������λ��
                if (objItem.m_dmlDosage > 0)
                    lviTemp.SubItems.Add(objItem.m_dmlDosage.ToString() + " " + objItem.m_strDosageUnit);
                else
                    lviTemp.SubItems.Add("");
                //��������λ��
                if (objItem.m_dmlUse > 0)
                    lviTemp.SubItems.Add(objItem.m_dmlUse.ToString() + " " + objItem.m_strUseunit);
                else
                    lviTemp.SubItems.Add("");
                //����
                if (objItem.m_dmlGet > 0)
                    lviTemp.SubItems.Add(objItem.m_dmlGet.ToString() + " " + objItem.m_strGetunit);
                else
                    lviTemp.SubItems.Add("");
                //����
                lviTemp.SubItems.Add(objItem.m_dmlPrice.ToString("0.0000"));
                //ִ��Ƶ��
                lviTemp.SubItems.Add(objItem.m_strExecFreqName);
                //��ҩ��ʽ
                lviTemp.SubItems.Add(objItem.m_strDosetypeName);
                //����	{if(�״�ִ�� && ״̬Ϊͨ����� && ����ΪҩƷ) then Ĭ��Ϊ����}
                lviTemp.SubItems.Add((m_objOrderBaseInfoArr[i1].m_intIsRecruit == 1) ? "��" : "");
                //Ƥ��	Ƥ�Խ��
                if (objItem.m_intISNEEDFEEL == 1)
                {
                    //�Ƿ�Ƥ��
                    lviTemp.SubItems.Add("��");
                    //Ƥ�Խ��
                    objTem = null;
                    m_objManage.m_lngGetOrderFeelByOrderID(objItem.m_strOrderID, out objTem);
                    if (objTem != null && objTem.m_strORDERFEELID_CHR != null)
                    {
                        lviTemp.SubItems.Add(objTem.m_strResultTypeName);
                    }
                    else
                    {
                        lviTemp.SubItems.Add("");
                    }
                }
                else
                {
                    //�Ƿ�Ƥ��
                    lviTemp.SubItems.Add("");
                    //Ƥ�Խ��
                    lviTemp.SubItems.Add("");
                }
                //����ҽ��
                lviTemp.SubItems.Add(objItem.m_strParentName);
                //��ʼʱ��
                lviTemp.SubItems.Add(DateTimeToString(objItem.m_dtStartDate));
                //ֹͣ��
                lviTemp.SubItems.Add(objItem.m_strStoper);
                //ֹͣʱ��
                lviTemp.SubItems.Add(DateTimeToString(objItem.m_dtStopdate));
                //��ɫ����(���ڱ���ɫ�����ֳ�����)
                #region ��ɫ����
                System.Drawing.Color clrForeColor = System.Drawing.Color.Black, clrBackColor = System.Drawing.Color.White;
                switch (objItem.m_intStatus)
                {
                    case 1://�Ѿ��ύ
                        clrForeColor = m_objViewer.m_lblNotExecute.BackColor;
                        break;
                    case 3://��ֹͣ
                        clrForeColor = m_objViewer.m_lblStopExecute.BackColor;
                        break;
                    default://ִ�й�
                        clrForeColor = m_objViewer.m_lblExecute.BackColor;
                        break;
                }
                if (objItem.m_intExecuteType == 1)
                    clrBackColor = m_objViewer.m_lblLong.BackColor;
                else
                    clrBackColor = m_objViewer.m_lblTemp.BackColor;
                lviTemp.BackColor = clrBackColor;
                lviTemp.ForeColor = clrForeColor;
                #endregion
                lviTemp.Tag = m_objOrderBaseInfoArr[i1];
                m_objViewer.m_lsvOrderBaseInfo.Items.Add(lviTemp);
            }
            #endregion
            //���ҽ��������ϢVo
            //��������Ϣ
            #region ��������Ϣ
            if (m_objPatientChargeInfoArr == null || m_objPatientChargeInfoArr.Length <= 0) return;
            lviTemp = null;
            for (int i1 = 0; i1 < m_objPatientChargeInfoArr.Length; i1++)
            {
                //����
                lviTemp = new ListViewItem(m_objPatientChargeInfoArr[i1].m_strBedName);
                //����
                lviTemp.SubItems.Add(m_objPatientChargeInfoArr[i1].m_strPatientName);
                //��������
                lviTemp.SubItems.Add(m_objPatientChargeInfoArr[i1].m_dblLowerLimitMoney.ToString("0.00"));
                //�ۼƷ���
                lviTemp.SubItems.Add(m_objPatientChargeInfoArr[i1].m_dblSumMoney.ToString("0.00"));
                //��  ��
                lviTemp.SubItems.Add(m_objPatientChargeInfoArr[i1].m_dblBalanceMoney.ToString("0.00"));
                //�����ɫ	{ҵ��˵���������������޵ľ�ͻ����ʾ}
                #region �����ɫ
                if (m_objPatientChargeInfoArr[i1].m_dblBalanceMoney <= m_objPatientChargeInfoArr[i1].m_dblLowerLimitMoney)
                {
                    lviTemp.BackColor = System.Drawing.Color.Pink;
                    lviTemp.ForeColor = System.Drawing.Color.Blue;
                }
                #endregion
                lviTemp.Tag = m_objPatientChargeInfoArr[i1];
                m_objViewer.m_lsvPatientChargeInfo.Items.Add(lviTemp);
            }
            #endregion
        }
        /// <summary>
        /// ���ҽ��������Ϣ�����ҽ��������Ϣ����
        /// </summary>
        /// <param name="p_objBIHCanExecOrderArr">��ִ��ҽ��Vo	[����]</param>
        private void FillObject(clsBIHCanExecOrder[] p_objBIHCanExecOrderArr)
        {
            if (p_objBIHCanExecOrderArr == null || p_objBIHCanExecOrderArr.Length <= 0) return;
            #region ��䡰������Ϣ����
            int i1 = 0, i2 = 0;
            m_objOrderBaseInfoArr = new clsOrderBaseInfo[p_objBIHCanExecOrderArr.Length];
            for (i1 = 0; i1 < p_objBIHCanExecOrderArr.Length; i1++)
            {
                m_objOrderBaseInfoArr[i1] = new clsOrderBaseInfo();
                //ҵ��˵����	{if(�״�ִ�� && ״̬Ϊ�ύ && ����ΪҩƷ) then Ĭ��Ϊ����}
                if (m_strMedicineOrderTypeID.Trim() == "")
                {
                    m_strMedicineOrderTypeID = m_objManage.m_strGetMedicineOrderTypeID();
                }
                if (p_objBIHCanExecOrderArr[i1].m_intExecuteType == 1 && p_objBIHCanExecOrderArr[i1].m_intStatus == 1 && p_objBIHCanExecOrderArr[i1].m_strOrderDicCateID.Trim() == m_strMedicineOrderTypeID)
                {
                    m_objOrderBaseInfoArr[i1].m_intIsRecruit = 1;
                }
                else
                {
                    m_objOrderBaseInfoArr[i1].m_intIsRecruit = 0;
                }
                m_objOrderBaseInfoArr[i1].m_objBIHCanExecOrder = p_objBIHCanExecOrderArr[i1];
            }
            #endregion
            #region ��䡰������Ϣ����
            ArrayList alDifferentRegisterID = new ArrayList();
            #region ��ȡ��ͬ��Ժ�Ǽ�ID�ġ���ִ��Vo����
            for (i1 = 0; i1 < p_objBIHCanExecOrderArr.Length; i1++)
            {
                for (i2 = 0; i2 < alDifferentRegisterID.Count; i2++)
                {
                    if (((clsBIHCanExecOrder)alDifferentRegisterID[i2]).m_strRegisterID.Trim() == p_objBIHCanExecOrderArr[i1].m_strRegisterID.Trim())
                    {
                        break;
                    }
                }
                if (i2 >= alDifferentRegisterID.Count)
                {
                    alDifferentRegisterID.Add(p_objBIHCanExecOrderArr[i1]);
                }
            }
            #endregion
            //���ҽ��������ϢVo
            #region ���
            m_objPatientChargeInfoArr = new clsPatientChargeInfo[alDifferentRegisterID.Count];
            for (i1 = 0; i1 < alDifferentRegisterID.Count; i1++)
            {
                m_objPatientChargeInfoArr[i1] = new clsPatientChargeInfo();
                clsBIHCanExecOrder objItem = (clsBIHCanExecOrder)alDifferentRegisterID[i1];
                double dblSumMoney = 0;			//�ۼƷ���
                double dblBalanceMoney = 0;		//Ԥ�������
                double dblLowerLimitMoney = 0;	//��������
                try
                {
                    dblSumMoney = m_objManage.m_dblGetSumMoneyByRegisterID(objItem.m_strRegisterID);
                    dblBalanceMoney = m_objManage.m_dblGetBalanceMoneyByRegisterID(objItem.m_strRegisterID);
                    dblLowerLimitMoney = m_objManage.m_dblGetLowerLimitMoneyByRegisterID(objItem.m_strRegisterID);
                }
                catch { }
                //���
                m_objPatientChargeInfoArr[i1].m_strRegisterID = objItem.m_strRegisterID;
                m_objPatientChargeInfoArr[i1].m_strPatientName = objItem.m_strPatientName;
                m_objPatientChargeInfoArr[i1].m_strBedName = objItem.m_strBedName;
                try
                {
                    m_objPatientChargeInfoArr[i1].m_dblSumMoney = m_objManage.m_dblGetSumMoneyByRegisterID(objItem.m_strRegisterID);
                    m_objPatientChargeInfoArr[i1].m_dblBalanceMoney = m_objManage.m_dblGetBalanceMoneyByRegisterID(objItem.m_strRegisterID);
                    m_objPatientChargeInfoArr[i1].m_dblLowerLimitMoney = m_objManage.m_dblGetLowerLimitMoneyByRegisterID(objItem.m_strRegisterID);
                }
                catch
                { }
            }
            #endregion
            #endregion
        }
        #endregion

        #region �¼�
        #region ��ѯҽ��
        #region ֱ�Ӳ��ҿ�
        //		/// <summary>
        //		/// ����ҽ��(��ִ��|ȫ��)
        //		/// </summary>
        //		public void m_FindOrder()
        //		{
        //			#region ��ѯ����
        //			EmptyListView();
        //			//����������
        //			string strAreaID ="",strBedIDs ="",strCreatorID="";
        //			if(m_objViewer.m_txtArea.Tag!=null && m_objViewer.m_txtArea.Tag.ToString().Trim()!="" && m_objViewer.m_txtArea.Text.Trim()!="")
        //			{
        //				strAreaID =clsConverter.ToString(m_objViewer.m_txtArea.Tag).Trim();
        //				strBedIDs =m_objViewer.m_BedIDs;
        //			}
        //			else
        //			{
        //				m_objViewer.m_txtArea.Text ="";
        //				m_objViewer.m_txtArea.Tag ="";
        //				m_objViewer.m_BedIDs ="";
        //			}
        //			if(strAreaID.Trim()=="")
        //			{
        //				MessageBox.Show("��������ѡ��","��ʾ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
        //				m_objViewer.m_txtArea.Focus();
        //				m_objViewer.m_txtArea.SelectAll();
        //				return;
        //			}
        //			//¼��ҽ��
        //			if(m_objViewer.m_txtDoctor.Tag!=null && m_objViewer.m_txtDoctor.Tag.ToString().Trim()!="" && m_objViewer.m_txtDoctor.Text.Trim()!="")
        //			{
        //				strCreatorID=clsConverter.ToString(m_objViewer.m_txtDoctor.Tag).Trim();
        //			}
        //			//����Ƥ��
        //			bool blnNeedFeel =m_objViewer.m_chkNeedFeel.Checked;
        //			//����ʾ����
        //			bool blnOnlyToday =m_objViewer.m_chkOnlyToday.Checked;
        //			//��Ժ��ҩ
        //			bool blnTakeMedicine =m_objViewer.m_chktakeMedicine.Checked;
        //			//ִ������
        //			DateTime dtStartExecute =m_objViewer.m_dtpStartExecute.Value;
        //			DateTime dtEndExecute =m_objViewer.m_dtpEndExecute.Value;
        //			//ִ��״̬
        //			string strOrderStatus =m_objViewer.GetOrderStatus;
        //			//ҽ������
        //			string strOrderType =m_objViewer.GetOrderType;
        //			//������Ŀ����
        //			string strOrderCate ="";
        //			if(m_objViewer.m_cobOrderCate.SelectedIndex>0)
        //			{
        //				clsOrderCate objOrderCate =(clsOrderCate)m_objViewer.m_cobOrderCate.SelectedItem;
        //				strOrderCate =objOrderCate.m_objOrderCate.m_strORDERCATEID_CHR.Trim();
        //			}
        //			#endregion
        //
        //			long lngRes =-1;
        //			clsBIHCanExecOrder[] objBIHCanExecOrderArr=new clsBIHCanExecOrder[0];
        //			switch(m_objViewer.Work)
        //			{	//{1=�鿴ҽ����2=����ύ��3=ִ��ҽ����4=���ֹͣ��}
        //				case 1:
        //					lngRes =m_objManage.m_lngGetCanExecuteOrder(strAreaID,strBedIDs,strOrderType,strOrderStatus,strOrderCate,blnNeedFeel,blnOnlyToday,blnTakeMedicine,strCreatorID,dtStartExecute,out objBIHCanExecOrderArr);
        //					break;
        //				case 2:
        //					break;
        //				case 3:
        //					lngRes =m_objManage.m_lngGetCanExecuteOrderOnlyCan(strAreaID,strBedIDs,strOrderType,strOrderStatus,strOrderCate,blnNeedFeel,blnOnlyToday,blnTakeMedicine,strCreatorID,dtStartExecute,out objBIHCanExecOrderArr);
        //					break;
        //				case 4:
        //					break;
        //			}			
        //			//���ҽ��������Ϣ�����ҽ��������Ϣ����
        //			FillObject(objBIHCanExecOrderArr);
        //			//���LisvtView			
        //			FillListView();
        //		}
        //		public void m_FindOrder(string AreaName,string AreaID,string strBedIDs,string strCreatorID)
        //		{
        //			#region ��ѯ����
        //			EmptyListView();
        //			m_objViewer.m_txtArea.Text = AreaName;
        //			m_objViewer.m_txtArea.Tag = AreaID;
        //
        //			//����Ƥ��
        //			bool blnNeedFeel =m_objViewer.m_chkNeedFeel.Checked;
        //			//����ʾ����
        //			bool blnOnlyToday =m_objViewer.m_chkOnlyToday.Checked;
        //			//��Ժ��ҩ
        //			bool blnTakeMedicine =m_objViewer.m_chktakeMedicine.Checked;
        //			//ִ������
        //			DateTime dtStartExecute =m_objViewer.m_dtpStartExecute.Value;
        //			DateTime dtEndExecute =m_objViewer.m_dtpEndExecute.Value;
        //			//ִ��״̬
        //			string strOrderStatus =m_objViewer.GetOrderStatus;
        //			//ҽ������
        //			string strOrderType =m_objViewer.GetOrderType;
        //			//������Ŀ����
        //			string strOrderCate ="";
        //			if(m_objViewer.m_cobOrderCate.SelectedIndex>0)
        //			{
        //				clsOrderCate objOrderCate =(clsOrderCate)m_objViewer.m_cobOrderCate.SelectedItem;
        //				strOrderCate =objOrderCate.m_objOrderCate.m_strORDERCATEID_CHR.Trim();
        //			}
        //			#endregion
        //
        //			long lngRes =-1;
        //			clsBIHCanExecOrder[] objBIHCanExecOrderArr=new clsBIHCanExecOrder[0];
        //			switch(m_objViewer.Work)
        //			{	//{1=�鿴ҽ����2=����ύ��3=ִ��ҽ����4=���ֹͣ��}
        //				case 1:
        //					lngRes =m_objManage.m_lngGetCanExecuteOrder(AreaID,strBedIDs,strOrderType,strOrderStatus,strOrderCate,blnNeedFeel,blnOnlyToday,blnTakeMedicine,strCreatorID,dtStartExecute,out objBIHCanExecOrderArr);
        //					break;
        //				case 2:
        //					break;
        //				case 3:
        //					lngRes =m_objManage.m_lngGetCanExecuteOrderOnlyCan(AreaID,strBedIDs,strOrderType,strOrderStatus,strOrderCate,blnNeedFeel,blnOnlyToday,blnTakeMedicine,strCreatorID,dtStartExecute,out objBIHCanExecOrderArr);
        //					break;
        //				case 4:
        //					break;
        //			}			
        //			//���ҽ��������Ϣ�����ҽ��������Ϣ����
        //			FillObject(objBIHCanExecOrderArr);
        //			//���LisvtView			
        //			FillListView();
        //		}
        #endregion
        #region ����ģʽ
        public void m_FindOrder()
        {
            #region ��ѯ����
            EmptyListView();
            //����������
            string strAreaID = "", strBedIDs = "";
            if (m_objViewer.m_txtArea.Tag != null && m_objViewer.m_txtArea.Tag.ToString().Trim() != "" && m_objViewer.m_txtArea.Text.Trim() != "")
            {
                strAreaID = clsConverter.ToString(m_objViewer.m_txtArea.Tag).Trim();
                strBedIDs = m_objViewer.m_BedIDs;
            }
            else
            {
                m_objViewer.m_txtArea.Text = "";
                m_objViewer.m_txtArea.Tag = "";
                m_objViewer.m_BedIDs = "";
            }
            if (strAreaID.Trim() == "")
            {
                MessageBox.Show("��������ѡ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtArea.Focus();
                m_objViewer.m_txtArea.SelectAll();
                return;
            }
            //ִ������
            DateTime dtStartExecute = m_objViewer.m_dtpStartExecute.Value;
            DateTime dtEndExecute = m_objViewer.m_dtpEndExecute.Value;
            #endregion

            long lngRes = -1;
            switch (m_objViewer.Work)
            {	//{1=�鿴ҽ����2=����ύ��3=ִ��ҽ����4=���ֹͣ��}
                case 1:
                    lngRes = m_objManage.m_lngGetCanExecuteOrder(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 2:
                    lngRes = m_objManage.m_lngGetOrderForAuditingExecute(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 3:
                    lngRes = m_objManage.m_lngGetCanExecuteOrderOnlyCan(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 4:
                    lngRes = m_objManage.m_lngGetOrderForAuditingStop(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
            }
            //���ListView
            DataBindListView();
        }
        public void m_FindOrder(string strAreaName, string strAreaID, string strBedIDs, string strCreatorID)
        {
            EmptyListView();
            m_objViewer.m_txtArea.Text = strAreaName;
            m_objViewer.m_txtArea.Tag = strAreaID;
            //ִ������
            DateTime dtStartExecute = m_objViewer.m_dtpStartExecute.Value;
            DateTime dtEndExecute = m_objViewer.m_dtpEndExecute.Value;

            long lngRes = -1;
            switch (m_objViewer.Work)
            {	//{1=�鿴ҽ����2=����ύ��3=ִ��ҽ����4=���ֹͣ��}
                case 1:
                    lngRes = m_objManage.m_lngGetCanExecuteOrder(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 2:
                    lngRes = m_objManage.m_lngGetOrderForAuditingExecute(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 3:
                    lngRes = m_objManage.m_lngGetCanExecuteOrderOnlyCan(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
                case 4:
                    lngRes = m_objManage.m_lngGetOrderForAuditingStop(strAreaID, strBedIDs, dtStartExecute, out m_objBIHCanExecOrderArr );
                    break;
            }
            //���ListView
            DataBindListView();
        }
        #endregion
        #endregion
        #region �鿴ҽ����
        /// <summary>
        /// �鿴ҽ����
        /// </summary>
        public void m_ViewExecOrder()
        {
            m_mthShowChargeItemList(null);
        }
        #endregion
        #region ִ   �С�����ύ�����ֹͣ
        /// <summary>
        /// �����¼�	{2=����ύ��3=ִ��ҽ����4=���ֹͣ��}
        /// ҵ��˵��:	��ٵĲ��˿������,������ִ��ҽ��.
        /// </summary>
        public void m_WorkExecute()
        {
            switch (m_objViewer.Work)
            {	//{1=�鿴ҽ����2=����ύ��3=ִ��ҽ����4=���ֹͣ��}				
                case 2:
                    AuditingForExecute();
                    break;
                case 3:
                    m_ExecuteOrder();
                    break;
                case 4:
                    AuditingForStop();
                    break;
            }
        }
        #region ִ    ��
        /// <summary>
        /// ִ��ҽ��
        /// </summary>
        private void m_ExecuteOrder()
        {
            if (m_objViewer.Work != 3) return;
            long lngRes = -1;
            #region	��ȡ��ִ����
            ArrayList alCanExecItem = GetOrderBaseInfoByListView(1);
            if (alCanExecItem.Count <= 0)
            {
                MessageBox.Show(m_objViewer, "��ѡҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            #region ������
            //ҽ��ID [����]		
            string[] strOrderIDArr = new string[alCanExecItem.Count];
            //��Ժ�Ǽ�ID [����]		
            string[] strRegisterIDArr = new string[alCanExecItem.Count];
            //����	[����]	
            int[] intRecipenNoArr = new int[alCanExecItem.Count];
            //ִ�е���¼ID [����] [out ������������ض��ID���ж��š������ָΪ��ִ��ʧ�ܡ�]
            string[] strOrderExecIDArr = new string[alCanExecItem.Count];
            //ָ���Ƿ񲹴�(��ִ������) [����]
            bool[] blnIsRecruitArr = new bool[alCanExecItem.Count];
            //ִ��ҽ����ˮ��
            string strEmpID = m_strOperatorID;
            //ִ��ҽ������
            string strEmpName = m_strOperatorName;
            //ִ������
            DateTime dtExecDate = m_objViewer.m_dtpStartExecute.Value;
            //����ҽ��ID [����]
            string[] strParentIDArr = new string[alCanExecItem.Count];
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                clsOrderBaseInfo objItem = (clsOrderBaseInfo)alCanExecItem[i1];
                strOrderIDArr[i1] = objItem.m_objBIHCanExecOrder.m_strOrderID;
                strRegisterIDArr[i1] = objItem.m_objBIHCanExecOrder.m_strRegisterID;
                intRecipenNoArr[i1] = objItem.m_objBIHCanExecOrder.m_intRecipenNo;
                blnIsRecruitArr[i1] = (objItem.m_intIsRecruit == 1) ? true : false;
                strParentIDArr[i1] = objItem.m_objBIHCanExecOrder.m_strParentID;
            }
            #endregion

            #region �����֤
            if (!PassCheckPatientIsLeave(alCanExecItem))
            {
                MessageBox.Show(m_objViewer, "������ٵĲ���;\r\n��ٲ��˲���ִ��ҽ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            #region ������֤
            if (!PassExecuteRecipeNOValidate(alCanExecItem))
            {
                MessageBox.Show("ͬ���ŵ�ҽ������һ��ִ�У�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            #region ��ҽ����֤
            if (!PassValidateFatherSonOrder(alCanExecItem))
            {
                MessageBox.Show("�Ӽ�ҽ�����ܵ���ִ�У�������丸��ҽ��һ��ִ�У�", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion
            #region �ų���֤
            string[] strExcludeOrderIDArr, strExcludeOrderNameArr;
            string[] strCanExecuteOrderIDArr = null;
            int intExcludeType = 0;
            lngRes = m_objManage.m_lngGetCanExecuteOrderByOrderID(strOrderIDArr[0], out strCanExecuteOrderIDArr);
            lngRes = m_objManage.m_lngJudgeExcludeOrder(strOrderIDArr, strCanExecuteOrderIDArr, 2, out intExcludeType, out strExcludeOrderIDArr, out strExcludeOrderNameArr);
            if (lngRes > 0 && intExcludeType != 0)
            {
                string strMessageBox = "";
                //{0=û�ų⣻1=ȫ�ų���ʱҽ����2=ȫ�ųⳤ��ҽ����3=ȫ�ų��ٳ�ҽ����4=��ͨ�ų⣻}
                switch (intExcludeType)
                {
                    case 1:
                        strMessageBox += "����ҽ������ȫ�ų�[���ڡ���ʱ]��\r\n";
                        break;
                    case 2:
                        strMessageBox += "����ҽ������ȫ�ų�[����]��\r\n";
                        break;
                    case 3:
                        strMessageBox += "����ҽ������ȫ�ų�[��ʱ]��\r\n";
                        break;
                    case 4:
                        strMessageBox += "����ҽ�������ų⣺\r\n";
                        break;
                }
                for (int i1 = 0; i1 < strExcludeOrderNameArr.Length; i1++)
                {
                    strMessageBox += strExcludeOrderNameArr[i1] + "\r\n";
                }
                MessageBox.Show(strMessageBox, "ִ��ʧ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            #region ��ʾȷ��
            if (strOrderIDArr == null || strOrderIDArr.Length <= 0)
            {
                MessageBox.Show(m_objViewer, "û��Ҫִ�е�ҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(m_objViewer, "ȷ��ִ��ѡ�е�ҽ����?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            #endregion

            #region ִ��ҽ��
            try
            {
                lngRes = m_objManage.m_lngExecuteOrder(strOrderIDArr, strRegisterIDArr, intRecipenNoArr, out strOrderExecIDArr, blnIsRecruitArr, strEmpID, strEmpName, dtExecDate, strParentIDArr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #region �ύ���ӵ���
            string strMessage = "ҽ��ִ�гɹ�";
            #endregion
            #region ������
            if (lngRes <= 0)
            {
                if (strMessage.Trim() == "") strMessage = "ҽ��ִ��ʧ�ܣ�";
                MessageBox.Show(strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_FindOrder();
                return;
            }

            //��ʾ������Ϣ
            //��ȡ���ɵ�ִ�е�
            string[] strArr;
            int j;
            //���ڱ���ִ�й���ִ�е�ID
            ArrayList arlExecOrderID = new ArrayList();
            for (int i1 = 0; i1 < strOrderExecIDArr.Length; i1++)
            {
                strArr = strOrderExecIDArr[i1].Split(new char[] { ',' });
                for (j = 0; j < strArr.Length; j++)
                {
                    if (strArr[j] != string.Empty) arlExecOrderID.Add(strArr[j].Trim());
                }
            }
            string[] arrID = (string[])(arlExecOrderID.ToArray(typeof(string)));
            m_mthShowChargeItemList(arrID);
            m_FindOrder();//ˢ������
            #endregion
        }
        #endregion
        #region ����ύ
        /// <summary>
        /// ����ύ
        /// </summary>
        private void AuditingForExecute()
        {
            if (m_objViewer.Work != 2) return;
            long lngRes = -1;
            //��ȡѡ����
            ArrayList alCanExecItem = GetOrderBaseInfoByListView(1);
            if (alCanExecItem.Count <= 0)
            {
                MessageBox.Show(m_objViewer, "��ѡҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region ������֤
            if (!PassExecuteRecipeNOValidate(alCanExecItem))
            {
                MessageBox.Show("ͬ���ŵ�ҽ������ͬ��������", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            string[] strOrderIDArr = new string[alCanExecItem.Count];
            clsOrderBaseInfo objItem;
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                objItem = (clsOrderBaseInfo)alCanExecItem[i1];
                strOrderIDArr[i1] = objItem.m_objBIHCanExecOrder.m_strOrderID;
            }
            lngRes = m_objManage.m_lngAuditingForExecute(strOrderIDArr, m_strOperatorID, m_strOperatorName);

            //������
            if (lngRes <= 0)
            {
                MessageBox.Show("����ʧ�ܣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_FindOrder();
        }
        #endregion
        #region ���ֹͣ
        /// <summary>
        /// ���ֹͣ
        /// </summary>
        private void AuditingForStop()
        {
            if (m_objViewer.Work != 4) return;
            long lngRes = -1;
            //��ȡѡ����
            ArrayList alCanExecItem = GetOrderBaseInfoByListView(1);
            if (alCanExecItem.Count <= 0)
            {
                MessageBox.Show(m_objViewer, "��ѡҽ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //��֤����
            #region ������֤
            if (!PassExecuteRecipeNOValidate(alCanExecItem))
            {
                MessageBox.Show("ͬ���ŵ�ҽ������ͬ��������", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            string[] strOrderIDArr = new string[alCanExecItem.Count];
            clsOrderBaseInfo objItem;
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                objItem = (clsOrderBaseInfo)alCanExecItem[i1];
                strOrderIDArr[i1] = objItem.m_objBIHCanExecOrder.m_strOrderID;
            }
            lngRes = m_objManage.m_lngAuditingForStop(strOrderIDArr, m_strOperatorID, m_strOperatorName);

            //������
            if (lngRes <= 0)
            {
                MessageBox.Show("����ʧ�ܣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_FindOrder();
        }
        #endregion
        #region �ų�ҽ������
        /// <summary>
        /// ��ȡ�ų�ҽ��������
        /// </summary>
        /// <param name="strExcludeOrderIDArr">�ų�ҽ��ID</param>
        /// <returns></returns>
        private string GetExcludeOrderName(string[] strExcludeOrderIDArr)
        {
            string strExcludeOrderName = "";
            for (int i1 = 0; i1 < strExcludeOrderIDArr.Length; i1++)
            {
                if (strExcludeOrderIDArr[i1] == null || strExcludeOrderIDArr[i1].Trim() == "") continue;
                for (int i2 = 0; i2 < m_objOrderBaseInfoArr.Length; i2++)
                {
                    if (((clsOrderBaseInfo)m_objOrderBaseInfoArr[i2]).m_objBIHCanExecOrder.m_strOrderID.Trim() == strExcludeOrderIDArr[i1].Trim())
                    {
                        strExcludeOrderName += "\n����" + ((clsOrderBaseInfo)m_objOrderBaseInfoArr[i2]).m_objBIHCanExecOrder.m_intRecipenNo.ToString().Trim() + "-" + ((clsOrderBaseInfo)m_objOrderBaseInfoArr[i2]).m_objBIHCanExecOrder.m_strName.Trim();
                    }
                }
            }
            return strExcludeOrderName;
        }
        #endregion
        #region ������֤
        /// <summary>
        /// ������֤
        /// </summary>
        /// <param name="alCanExecItem">ҽ��������Ϣ����</param>
        /// <returns></returns>
        private bool PassExecuteRecipeNOValidate(ArrayList alCanExecItem)
        {
            clsBIHCanExecOrder[] objTempCanExecOrderArr = null;
            objTempCanExecOrderArr = m_objBIHCanExecOrderArr;
            #region ��ȡȫ���Ŀ�ִ�м�¼	����ֱ�Ӻͷ������ݿ�
            //			string strAreaID ="",strBedIDs ="",strCreatorID="";
            //			if(m_objViewer.m_txtArea.Tag!=null && m_objViewer.m_txtArea.Tag.ToString().Trim()!="" && m_objViewer.m_txtArea.Text.Trim()!="")
            //			{
            //				strAreaID =clsConverter.ToString(m_objViewer.m_txtArea.Tag).Trim();
            //				strBedIDs =m_objViewer.m_BedIDs;
            //			}				
            //			DateTime dtStartExecute=m_objViewer.m_dtpStartExecute.Value;
            //			m_objManage.m_lngGetCanExecuteOrderOnlyCan(strAreaID,strBedIDs,dtStartExecute,out objTempCanExecOrderArr);
            #endregion
            if (objTempCanExecOrderArr == null || objTempCanExecOrderArr.Length <= 0) return false;

            bool lbnSame = false;
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                clsOrderBaseInfo objItem = ((clsOrderBaseInfo)alCanExecItem[i1]);
                for (int i2 = 0; i2 < objTempCanExecOrderArr.Length; i2++)
                {
                    //if(סԺ�Ǽ�ID��ͬ && ������ͬ && û��ѡ��) return false;
                    if (objItem.m_objBIHCanExecOrder.m_strRegisterID.Trim() == objTempCanExecOrderArr[i2].m_strRegisterID.Trim()
                        && objItem.m_objBIHCanExecOrder.m_intRecipenNo == objTempCanExecOrderArr[i2].m_intRecipenNo
                        && objItem.m_objBIHCanExecOrder.m_strOrderID != objTempCanExecOrderArr[i2].m_strOrderID)
                    {
                        lbnSame = false;
                        for (int i3 = 0; i3 < alCanExecItem.Count; i3++)
                        {
                            if (((clsOrderBaseInfo)alCanExecItem[i3]).m_objBIHCanExecOrder.m_strOrderID == objTempCanExecOrderArr[i2].m_strOrderID)
                            {
                                lbnSame = true;
                                break;
                            }
                        }
                        if (!lbnSame) return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region �����֤
        /// <summary>
        /// �����֤	{��ٵ��˲���ִ��ҽ��}
        /// ˵��: �����,��ͨ����֤;
        /// </summary>
        /// <param name="alCanExecItem">ҽ��������Ϣ����{clsOrderBaseInfo}</param>
        /// <returns></returns>
        private bool PassCheckPatientIsLeave(ArrayList alCanExecItem)
        {
            bool blnRes = false;
            ArrayList alItem = new ArrayList();//�洢�����˵Ļ���
            string strRegisterIDs = "";
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                clsOrderBaseInfo objItem = ((clsOrderBaseInfo)alCanExecItem[i1]);
                string strRes = objItem.m_objBIHCanExecOrder.m_strRegisterID.Trim();
                if (!alItem.Contains(strRes))
                {
                    alItem.Add(strRes);
                    strRegisterIDs = (strRegisterIDs == "") ? (strRegisterIDs) : (strRegisterIDs + ",");
                    strRegisterIDs += "'" + strRes + "'";
                }
            }
            if (strRegisterIDs != "")
            {
                bool blnIsLeave = false;
                long lngRes = m_objManage.m_lngCheckPatientIsLeave(strRegisterIDs, out blnIsLeave);
                blnRes = (!blnIsLeave);
            }
            return blnRes;
        }
        #endregion
        #region ����ҽ����֤
        /// <summary>
        /// ����ҽ����֤
        /// </summary>
        /// <param name="alCanExecItem">ҽ��������Ϣ����</param>
        /// <returns></returns>
        private bool PassValidateFatherSonOrder(ArrayList alCanExecItem)
        {
            if (alCanExecItem.Count <= 0) return true;
            string strParentID = "", strOrderID = "";
            bool blnSame = false;
            for (int i1 = 0; i1 < alCanExecItem.Count; i1++)
            {
                strParentID = ((clsOrderBaseInfo)alCanExecItem[i1]).m_objBIHCanExecOrder.m_strParentID.Trim();//����ҽ��ID 
                if (strParentID == null || strParentID == "") continue;
                //if(����ҽ��û��ѡ��) return false;
                blnSame = false;
                for (int i2 = 0; i2 < alCanExecItem.Count; i2++)
                {
                    strOrderID = ((clsOrderBaseInfo)alCanExecItem[i2]).m_objBIHCanExecOrder.m_strOrderID.Trim();
                    if (strParentID == strOrderID)
                    {
                        blnSame = true;
                        break;
                    }
                }
                if (!blnSame) return false;
            }
            return true;
        }
        #endregion
        #endregion
        #region ��ӡҽ��
        /// <summary>
        /// ��ӡҽ��
        /// </summary>
        public void m_PrintOrder()
        {
            //��ȡ��Ժ�Ǽ�ID
            #region ��ȡ��Ժ�Ǽ�ID
            string strRegisterID = "";
            ArrayList alSelectItem = new ArrayList();
            if (m_objViewer.m_tabMain.SelectedIndex == 0)//������Ϣ
            {
                if (m_objViewer.m_lsvOrderBaseInfo.CheckBoxes)
                {
                    alSelectItem = GetSelectListView(m_objViewer.m_lsvOrderBaseInfo);
                    if (alSelectItem.Count <= 0 || alSelectItem.Count > 1)
                    {
                        MessageBox.Show("��ӡ������ȷ������ѡ��һ����¼��", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    strRegisterID = ((clsOrderBaseInfo)alSelectItem[0]).m_objBIHCanExecOrder.m_strRegisterID;
                }
                else
                {
                    if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count != 1)
                    {
                        MessageBox.Show("��ӡ������ȷ������ѡ��һ����¼��", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    strRegisterID = ((clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag).m_objBIHCanExecOrder.m_strRegisterID;
                }
            }
            else//������Ϣ
            {
                if (m_objViewer.m_lsvPatientChargeInfo.CheckBoxes)
                {
                    alSelectItem = GetSelectListView(m_objViewer.m_lsvPatientChargeInfo);
                    if (alSelectItem.Count <= 0 || alSelectItem.Count > 1)
                    {
                        MessageBox.Show("��ӡ������ȷ������ѡ��һ����¼��", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    strRegisterID = ((clsPatientChargeInfo)alSelectItem[0]).m_strRegisterID;
                }
                else
                {
                    if (m_objViewer.m_lsvPatientChargeInfo.SelectedItems.Count != 1)
                    {
                        MessageBox.Show("��ӡ������ȷ������ѡ��һ����¼��", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    strRegisterID = ((clsPatientChargeInfo)m_objViewer.m_lsvPatientChargeInfo.SelectedItems[0].Tag).m_strRegisterID;
                }
            }
            #endregion

            if (strRegisterID.Trim() == "") return;
            //��ʾ��ӡ����ҳ��
            frmPrintOrder objPrintOrder = new frmPrintOrder(strRegisterID);
            objPrintOrder.ShowDialog();
        }
        #endregion
        #region �˵��¼�
        public void m_funPopup()
        {
            m_objViewer.ctuMenu.MenuItems[0].Enabled = false;
            if (m_objViewer.Work != 2 && m_objViewer.Work != 3) return;
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count == 1 && m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag != null)
            {
                clsOrderBaseInfo objItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;
                if (objItem.m_objBIHCanExecOrder.m_intStatus == 1 || objItem.m_objBIHCanExecOrder.m_intStatus == 5)
                {
                    m_objViewer.ctuMenu.MenuItems[0].Enabled = true;
                    m_objViewer.ctuMenu.MenuItems[0].DefaultItem = true;
                }
            }
        }
        /// <summary>
        /// �˻�ҽ��
        /// </summary>
        public void m_funSendBackOrder()
        {
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count != 1) return;
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag == null || (!(m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag is clsOrderBaseInfo))) return;

            clsOrderBaseInfo objItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;
            if (objItem.m_objBIHCanExecOrder.m_intStatus != 1 && objItem.m_objBIHCanExecOrder.m_intStatus != 5)
            {
                return;
            }

            bool showBack = false;
            if (objItem.m_objBIHCanExecOrder.m_strParentID == null)
            {
                showBack = true;
            }
            else if (objItem.m_objBIHCanExecOrder.m_strParentID.ToString().Trim().Equals(""))
            {
                showBack = true;
            }
            else
            {
                ArrayList alCanExecItem = GetOrderBaseInfoByListView(2);
                for (int i = 0; i < alCanExecItem.Count; i++)
                {
                    if (((clsOrderBaseInfo)alCanExecItem[i]).m_objBIHCanExecOrder.m_strParentID.ToString().Trim().Equals(objItem.m_objBIHCanExecOrder.m_strOrderID.ToString().Trim()))
                    {
                        showBack = true;
                        break;
                    }
                }

            }
            //��鵱ǰ״̬�Ƿ�����ҽ��
            if (showBack)
            {
                int count = 0;
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i++)
                {
                    if (((clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i].Tag).m_objBIHCanExecOrder.m_strParentID.ToString().Trim().Equals(objItem.m_objBIHCanExecOrder.m_strOrderID.ToString().Trim()))
                    {
                        count = 1;
                        break;
                    }
                }
                if (count == 1)
                {
                    showBack = true;
                }
                else
                {
                    showBack = false;
                }
            }



            if (showBack)
            {
                com.digitalwave.iCare.BIHOrder.frmConfirmOrderBack objfrmConfirmOrderBack = new frmConfirmOrderBack();
                objfrmConfirmOrderBack.m_strOrderID = objItem.m_objBIHCanExecOrder.m_strOrderID.ToString().Trim();
                objfrmConfirmOrderBack.m_strOrderName = objItem.m_objBIHCanExecOrder.m_strName.ToString().Trim();
                objfrmConfirmOrderBack.m_strPatientName = objItem.m_objBIHCanExecOrder.m_strPatientName.ToString().Trim();
                objfrmConfirmOrderBack.IsChildPrice = this.IsChildPrice(objfrmConfirmOrderBack.m_strOrderID);
                if (objfrmConfirmOrderBack.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            /*<=======================================================================*/
            com.digitalwave.iCare.BIHOrder.frmSendBackReason objfrmSendBackReason = new com.digitalwave.iCare.BIHOrder.frmSendBackReason();
            objfrmSendBackReason.m_txbOrderName.Text = objItem.m_objBIHCanExecOrder.m_strName.Trim();
            if (objfrmSendBackReason.ShowDialog() != DialogResult.OK)
                return;

            long lngRes = 0;

            if (showBack)
            {
                lngRes = m_objManage.m_lngReturnOrder(objItem.m_objBIHCanExecOrder.m_strOrderID, objfrmSendBackReason.m_txtReason.Text.Replace(",", "").Trim(), m_strOperatorID, m_strOperatorName, true, this.IsChildPrice(objItem.m_objBIHCanExecOrder.m_strOrderID));

            }
            else
            {
                lngRes = m_objManage.m_lngReturnOrder(objItem.m_objBIHCanExecOrder.m_strOrderID, objfrmSendBackReason.m_txtReason.Text.Replace(",", "").Trim(), m_strOperatorID, m_strOperatorName);

            }
            /*<=============================================================================================*/
            //������
            if (lngRes <= 0)
            {
                MessageBox.Show("����ʧ�ܣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // ��ѡ�е���Ŀ����������
            ArrayList checkList = new ArrayList();

            for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.CheckedItems.Count; i++)
            {
                checkList.Add(((clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].Tag).m_objBIHCanExecOrder.m_strOrderID.ToString().Trim());
            }
            /*<===============================================*/
            m_FindOrder();
            // ����ԭ�еķ��˻���Ŀ������
            for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i++)
            {
                if (checkList.Contains(((clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i].Tag).m_objBIHCanExecOrder.m_strOrderID.ToString().Trim()))
                {
                    m_objViewer.m_lsvOrderBaseInfo.Items[i].Checked = true; ;
                }
            }
            /*<===============================================*/

        }
        #endregion
        #region ListView�¼�
        /// <summary>
        /// ҵ��˵����ֻ���ύ״̬�ҳ���ҽ����������Ϊ�����Ρ�
        /// </summary>
        public void m_ItemActivateOrderBaseInfo()
        {
            if (m_objViewer.Work != 3) return;
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count <= 0 || m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count > 1) return;
            clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;

            //����	[λ�ڵ�11��]
            #region ����
            //			if(objSelectItem.m_objBIHCanExecOrder.m_intExecuteType==1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus==1)
            //			{
            //				if(objSelectItem.m_intIsRecruit==1)
            //				{
            //					if(MessageBox.Show(m_objViewer,"��ǰΪ�����Ρ�״̬��ȷ���������Ρ���","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            //					{
            //						objSelectItem.m_intIsRecruit=0;
            //						m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[11].Text ="";
            //						m_objViewer.m_lsvOrderBaseInfo.Refresh();
            //					}
            //				}
            //				else
            //				{
            //					if(MessageBox.Show(m_objViewer,"��ǰΪ�������Ρ�״̬��ȷ�������Ρ���","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            //					{
            //						objSelectItem.m_intIsRecruit=1;
            //						m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[11].Text ="��";	
            //						m_objViewer.m_lsvOrderBaseInfo.Refresh();
            //					}
            //				}
            //			}
            #endregion

            #region Ƥ��	[Ƥ�Խ��λ�ڵ�13��]
            //			//���ҪƤ������ʾƤ��
            //			if(objSelectItem.m_objBIHCanExecOrder.m_intISNEEDFEEL==1)
            //			{
            //				//��ʾƤ��¼��ҳ��
            //				clsFeelEdit objFellEdit =new clsFeelEdit();
            //				objFellEdit.m_strOrderID =objSelectItem.m_objBIHCanExecOrder.m_strOrderID;
            //				objFellEdit.m_strPatientName =objSelectItem.m_objBIHCanExecOrder.m_strPatientName;
            //				objFellEdit.m_strOrderName =objSelectItem.m_objBIHCanExecOrder.m_strName;
            //				frmNeedFeel objfrmNeedFeel =new frmNeedFeel(objFellEdit);
            //				objfrmNeedFeel.ShowDialog();
            //
            //				//ˢ��ҳ��
            //				if(objFellEdit.m_intExitState>0)
            //				{
            //					m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[13].Text =objFellEdit.m_strFeelResult;
            //					m_objViewer.m_lsvOrderBaseInfo.Refresh();
            //				}
            //			}
            #endregion
        }
        public void m_ItemCheckPatientChargeInfo(System.Windows.Forms.ItemCheckEventArgs e)
        {
            clsPatientChargeInfo objCur = (clsPatientChargeInfo)m_objViewer.m_lsvPatientChargeInfo.Items[e.Index].Tag;
            if (m_objViewer.m_lsvPatientChargeInfo.Items[e.Index].Checked)
            {
                for (int i1 = 0; i1 < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i1++)
                {
                    clsOrderBaseInfo objItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i1].Tag;
                    //if(��Ժ�Ǽ�ID��ͬ && ѡ��) ��ѡ��
                    if (objItem.m_objBIHCanExecOrder.m_strRegisterID.Trim() == objCur.m_strRegisterID.Trim() && m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked)
                        m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked = false;
                }
            }
            else
            {
                for (int i1 = 0; i1 < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i1++)
                {
                    clsOrderBaseInfo objItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i1].Tag;
                    //if(��Ժ�Ǽ�ID��ͬ && ûѡ��) ѡ��
                    if (objItem.m_objBIHCanExecOrder.m_strRegisterID.Trim() == objCur.m_strRegisterID.Trim() && (!m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked))
                        m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked = true;
                }
            }
        }
        /// <summary>
        /// ѡ����ı���¼�
        /// </summary>
        public void m_SelectedIndexChangedOrderBaseInfo()
        {
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count != 1)
            {
                m_objViewer.m_lsvToolTip.Items.Clear();
                return;
            }
            clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;
            //��ʾ������Ϣ
            m_DisPlayToolTipListView(objSelectItem, m_objViewer.m_lsvToolTip);

            /*
            if(m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count<=0 || m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count>1)
            {
                m_objViewer.cmdEditFeel.Enabled =false;
                m_objViewer.cmdOnceAgain.Enabled =false;
                m_objViewer.cmdOnceAgain.Text ="����(F3)";
                return;
            }
            //����	[λ�ڵ�10��]	{ҵ��˵��:���֮��,ִ��֮����ܱ༭}	{if(�״�ִ�� && ״̬Ϊͨ����� && ����ΪҩƷ) then Ĭ��Ϊ����}
            if(m_objViewer.Work==3 && objSelectItem.m_objBIHCanExecOrder.m_intExecuteType==1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus==5)
            {
                m_objViewer.cmdOnceAgain.Enabled =true;
                if(objSelectItem.m_intIsRecruit==1)
                    m_objViewer.cmdOnceAgain.Text ="������(F3)";
                else
                    m_objViewer.cmdOnceAgain.Text ="����(F3)";
            }
            else
            {
                m_objViewer.cmdOnceAgain.Enabled =false;
            }
             */
            //Ƥ��	[Ƥ�Խ��λ�ڵ�12��]	{ҵ��˵��:���֮��,ִ��֮����ܱ༭}
            if (m_objViewer.Work == 3 && objSelectItem.m_objBIHCanExecOrder.m_intISNEEDFEEL == 1)
                m_objViewer.cmdEditFeel.Enabled = true;
            else
                m_objViewer.cmdEditFeel.Enabled = false;

            //���ֹͣ	{ҵ��˵��: ֻ�г���ҽ������Ҫֹͣҽ������}
            if (m_objViewer.Work == 4)
            {
                if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 3)
                {
                    m_objViewer.m_cmdExecute.Enabled = true;
                }
                else
                {
                    m_objViewer.m_cmdExecute.Enabled = false;
                }
            }
        }
        #endregion
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_OnceAgain()
        {
            //if (m_objViewer.Work != 3) return;
            //if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count <= 0 || m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count > 1) return; 
            //clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;
            //if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
            //{
            //    if (objSelectItem.m_intIsRecruit == 1)
            //    {
            //        //if(MessageBox.Show(m_objViewer,"��ǰΪ�����Ρ�״̬��ȷ���������Ρ���","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            //        {
            //            objSelectItem.m_intIsRecruit = 0; 
            //            /*---------------------------------------------------->
            //            m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[12].Text ="";
            //             */

            //            m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[13].Text = "";

            //            /*<========================================================*/
            //            m_objViewer.m_lsvOrderBaseInfo.Refresh();
            //        }
            //    }
            //    else
            //    {
            //        //if(MessageBox.Show(m_objViewer,"��ǰΪ�������Ρ�״̬��ȷ�������Ρ���","��ʾ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            //        {
            //            objSelectItem.m_intIsRecruit = 1; 
            //            /*
            //            m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[12].Text ="��";	
            //             */
            //            m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[13].Text = "��";

            //            /*<===================================================================*/
            //            m_objViewer.m_lsvOrderBaseInfo.Refresh();
            //        }
            //    }
            //}

            if (m_objViewer.Work != 3) return;
            if (m_objViewer.m_lsvOrderBaseInfo.CheckedItems.Count <= 0)
            {
                MessageBox.Show("û��ѡ�пɽ��в��ε��");
                return;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.CheckedItems.Count; i++)
                {

                    clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].Tag;
                    if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
                    {
                        count++;
                        break;
                    }
                }
                if (count < 0)
                {
                    MessageBox.Show("û��ѡ�пɽ��в��ε��");
                    return;
                }
            }
            //���б���Ӧ���������С������Է��㸸��ҽ������ͬ��
            ArrayList m_arrtemp = new ArrayList();

            /*<========================================================*/
            DialogResult m_dlgResult = m_objViewer2.ShowDialog();
            if (m_dlgResult == DialogResult.Cancel)
            {
                return;
            }
            else if (m_dlgResult == DialogResult.Yes)
            {
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.CheckedItems.Count; i++)
                {
                    clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].Tag;
                    if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
                    {
                        objSelectItem.m_intIsRecruit = 1;
                        m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].SubItems[13].Text = "��";
                        if (objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim() != "")
                        {
                            m_arrtemp.Add(objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim());
                        }
                        else
                        {
                            m_arrtemp.Add(objSelectItem.m_objBIHCanExecOrder.m_strOrderID.Trim());
                        }

                    }
                }
                //����ҽ������ͬ��
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i++)
                {
                    clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i].Tag;
                    if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
                    {

                        if (m_arrtemp.Contains(objSelectItem.m_objBIHCanExecOrder.m_strOrderID.Trim()))
                        {
                            m_objViewer.m_lsvOrderBaseInfo.Items[i].SubItems[13].Text = "��";
                            objSelectItem.m_intIsRecruit = 1;
                        }
                        else
                        {
                            if (m_arrtemp.Contains(objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim()))
                            {
                                m_objViewer.m_lsvOrderBaseInfo.Items[i].SubItems[13].Text = "��";
                                objSelectItem.m_intIsRecruit = 1;
                            }
                        }
                    }
                }

                m_objViewer.m_lsvOrderBaseInfo.Refresh();
            }
            else if (m_dlgResult == DialogResult.No)
            {
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.CheckedItems.Count; i++)
                {
                    clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].Tag;
                    if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
                    {
                        objSelectItem.m_intIsRecruit = 0;
                        m_objViewer.m_lsvOrderBaseInfo.CheckedItems[i].SubItems[13].Text = "";
                        if (objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim() != "")
                        {
                            m_arrtemp.Add(objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim());
                        }
                        else
                        {
                            m_arrtemp.Add(objSelectItem.m_objBIHCanExecOrder.m_strOrderID.Trim());
                        }
                    }
                }
                //����ҽ������ͬ��
                for (int i = 0; i < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i++)
                {
                    clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i].Tag;
                    if (objSelectItem.m_objBIHCanExecOrder.m_intExecuteType == 1 && objSelectItem.m_objBIHCanExecOrder.m_intStatus == 5)
                    {

                        if (m_arrtemp.Contains(objSelectItem.m_objBIHCanExecOrder.m_strOrderID.Trim()))
                        {
                            m_objViewer.m_lsvOrderBaseInfo.Items[i].SubItems[13].Text = "";
                            objSelectItem.m_intIsRecruit = 0;
                        }
                        else
                        {
                            if (m_arrtemp.Contains(objSelectItem.m_objBIHCanExecOrder.m_strParentID.Trim()))
                            {
                                m_objViewer.m_lsvOrderBaseInfo.Items[i].SubItems[13].Text = "";
                                objSelectItem.m_intIsRecruit = 0;
                            }
                        }
                    }
                }
                /*<========================================================*/
                m_objViewer.m_lsvOrderBaseInfo.Refresh();
            }
            else
            {
                return;
            }

            /*<======================================================================*/
        }
        /// <summary>
        /// �༭Ƥ��
        /// </summary>
        public void m_EditFeel()
        {
            //if(m_objViewer.Work!=3) return;
            if (m_objViewer.m_lsvOrderBaseInfo.SelectedItems.Count != 1) return;
            clsOrderBaseInfo objSelectItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].Tag;
            //���ҪƤ������ʾƤ��
            if (objSelectItem.m_objBIHCanExecOrder.m_intISNEEDFEEL == 1)
            {
                //��ʾƤ��¼��ҳ��
                clsFeelEdit objFellEdit = new clsFeelEdit();
                objFellEdit.m_strOrderID = objSelectItem.m_objBIHCanExecOrder.m_strOrderID;
                objFellEdit.m_strPatientName = objSelectItem.m_objBIHCanExecOrder.m_strPatientName;
                objFellEdit.m_strOrderName = objSelectItem.m_objBIHCanExecOrder.m_strName;
                frmNeedFeel objfrmNeedFeel = new frmNeedFeel(objFellEdit);
                objfrmNeedFeel.ShowDialog();

                //ˢ��ҳ��
                if (objFellEdit.m_intExitState > 0)
                {
                    m_objViewer.m_lsvOrderBaseInfo.SelectedItems[0].SubItems[14].Text = objFellEdit.m_strFeelResult;
                    m_objViewer.m_lsvOrderBaseInfo.Refresh();
                    //���û�����
                    GetOrderFeelFromBuffer(objSelectItem.m_objBIHCanExecOrder.m_strOrderID, true);
                }
            }
        }
        #endregion
        #endregion
        #region ˽�з���
        #region �ListView
        /// <summary>
        /// �ListView	{ҽ��������Ϣ��ҽ��������Ϣ}
        /// </summary>
        /// <param name="p_objItemArr">��ִ��ҽ������</param>
        public void DataBindListView()
        {
            ClearListView();
            if (m_objBIHCanExecOrderArr == null || m_objBIHCanExecOrderArr.Length <= 0) return;

            ArrayList alOrderBaseInfo = new ArrayList();
            alOrderBaseInfo = GetOrderBaseInfo(m_objBIHCanExecOrderArr);
            DataBind_OrderBaseInfo(alOrderBaseInfo);

            ArrayList alItem = new ArrayList();
            alItem = GetPatientChargeInfo(alOrderBaseInfo);
            DataBind_PatientChargeInfo(alItem);
        }
        /// <summary>
        /// �ҽ��������Ϣ	LisView
        /// clsOrderBaseInfo
        /// </summary>
        /// <param name="p_alItem">ҽ��������Ϣ����</param>
        private void DataBind_OrderBaseInfo(ArrayList p_alItem)
        {
            m_objViewer.m_btnAddBills.Enabled = false;
            m_objViewer.m_lsvOrderBaseInfo.Items.Clear();
            if (p_alItem == null || p_alItem.Count <= 0) return;
            #region �����Ϣ
            ListViewItem lviTemp = null;
            clsT_Opr_Bih_OrderFeel_VO objTem;
            clsBIHCanExecOrder objItem = new clsBIHCanExecOrder();
            for (int i1 = 0; i1 < p_alItem.Count; i1++)
            {
                objItem = ((clsOrderBaseInfo)p_alItem[i1]).m_objBIHCanExecOrder;
                //���
                lviTemp = new ListViewItem((i1 + 1).ToString());
                if (i1 > 0 && objItem.m_strRegisterID.Trim() == ((clsOrderBaseInfo)p_alItem[i1 - 1]).m_objBIHCanExecOrder.m_strRegisterID.Trim())
                {
                    //����
                    lviTemp.SubItems.Add("");
                    //����
                    lviTemp.SubItems.Add("");
                }
                else
                {
                    //����
                    lviTemp.SubItems.Add(objItem.m_strBedName);
                    //����
                    lviTemp.SubItems.Add(objItem.m_strPatientName);
                }
                //����
                lviTemp.SubItems.Add(objItem.m_intRecipenNo.ToString());
                //��/��
                if (objItem.m_intExecuteType == 1)
                {
                    lviTemp.SubItems.Add("��");
                }
                else
                {
                    if (objItem.m_intExecuteType == 2)
                        lviTemp.SubItems.Add("��");
                    else
                        lviTemp.SubItems.Add("");
                }

                //Add by jli in 2005-04-20

                //DataTable dtbAddBills=new DataTable();
                //long lngRet=this.m_objViewer.m_lngGetAddBillByOrderID(objItem.m_strOrderID.Trim(),out dtbAddBills);
                if (objItem.m_strATTARELAID_CHR != null && objItem.m_strATTARELAID_CHR != "")
                {
                    lviTemp.SubItems.Add("��");
                    m_objViewer.m_btnAddBills.Enabled = true;
                }
                else
                {
                    lviTemp.SubItems.Add("");
                }

                //Add End

                //ҽ������
                lviTemp.SubItems.Add(objItem.m_strName);
                //����
                lviTemp.SubItems.Add(objItem.m_strEntrust);
                // 
                lviTemp.SubItems.Add(objItem.m_strSpec.ToString().Trim());
                /*<===========================================================*/
                //��������λ��
                if (objItem.m_dmlDosage > 0)
                    lviTemp.SubItems.Add(objItem.m_dmlDosage.ToString() + " " + objItem.m_strDosageUnit);
                else
                    lviTemp.SubItems.Add("");
                //��������λ��
                //if(objItem.m_dmlUse>0)
                //	lviTemp.SubItems.Add(objItem.m_dmlUse.ToString() + " " + objItem.m_strUseunit);
                //else
                //	lviTemp.SubItems.Add("");
                //����
                if (objItem.m_dmlGet > 0)
                    lviTemp.SubItems.Add(objItem.m_dmlGet.ToString() + " " + objItem.m_strGetunit);
                else
                    lviTemp.SubItems.Add("");
                //����
                //lviTemp.SubItems.Add(objItem.m_dmlPrice.ToString("0.0000"));
                //ִ��Ƶ��
                lviTemp.SubItems.Add(objItem.m_strExecFreqName);
                //��ҩ��ʽ
                lviTemp.SubItems.Add(objItem.m_strDosetypeName);
                //����	{if(�״�ִ�� && ״̬Ϊ�ύ && ����ΪҩƷ) then Ĭ��Ϊ����}
                lviTemp.SubItems.Add((((clsOrderBaseInfo)p_alItem[i1]).m_intIsRecruit == 1) ? "��" : "");
                //Ƥ��	Ƥ�Խ��
                if (objItem.m_intISNEEDFEEL == 1)
                {
                    //�Ƿ�Ƥ��
                    lviTemp.SubItems.Add("��");
                    //Ƥ�Խ��
                    objTem = GetOrderFeelFromBuffer(objItem.m_strOrderID, false);
                    //					objTem =null;
                    //					m_objManage.m_lngGetOrderFeelByOrderID(objItem.m_strOrderID,out objTem);
                    if (objTem != null && objTem.m_strORDERFEELID_CHR != null)
                    {
                        lviTemp.SubItems.Add(objTem.m_strResultTypeName);
                    }
                    else
                    {
                        lviTemp.SubItems.Add("");
                    }
                }
                else
                {
                    //�Ƿ�Ƥ��
                    lviTemp.SubItems.Add("");
                    //Ƥ�Խ��
                    lviTemp.SubItems.Add("");
                }
                //����ҽ��
                lviTemp.SubItems.Add(objItem.m_strParentName);
                //��ʼʱ��
                lviTemp.SubItems.Add(DateTimeToString(objItem.m_dtStartDate));
                //ֹͣ��
                lviTemp.SubItems.Add(objItem.m_strStoper);
                //ֹͣʱ��
                lviTemp.SubItems.Add(DateTimeToString(objItem.m_dtStopdate));
                //��ɫ����(���ڱ���ɫ�����ֳ�����)
                #region ��ɫ����
                System.Drawing.Color clrForeColor = System.Drawing.Color.Black, clrBackColor = System.Drawing.Color.White;
                switch (objItem.m_intStatus)//ִ��״̬{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5- ����ύ;6-���ֹͣ;}
                {
                    case 1://�Ѿ��ύ
                        clrForeColor = m_objViewer.m_lblNotExecute.BackColor;
                        break;
                    case 2://ִ�й�
                        clrForeColor = m_objViewer.m_lblExecute.BackColor;
                        break;
                    case 3://��ֹͣ
                        clrForeColor = m_objViewer.m_lblStopExecute.BackColor;
                        break;
                    case 5://������ύ
                        clrForeColor = m_objViewer.m_lblAuditingExecute.BackColor;
                        break;
                    case 6://�����ֹͣ
                        clrForeColor = m_objViewer.m_lblAuditingStop.BackColor;
                        break;
                }
                if (objItem.m_intExecuteType == 1)
                    clrBackColor = m_objViewer.m_lblLong.BackColor;
                else
                    clrBackColor = m_objViewer.m_lblTemp.BackColor;
                lviTemp.BackColor = clrBackColor;
                lviTemp.ForeColor = clrForeColor;
                #endregion
                lviTemp.Tag = (clsOrderBaseInfo)p_alItem[i1];
                m_objViewer.m_lsvOrderBaseInfo.Items.Add(lviTemp);
            }
            #endregion
        }
        /// <summary>
        /// �ҽ��������Ϣ	LisView
        /// clsPatientChargeInfo
        /// </summary>
        /// <param name="p_alItem">ҽ��������Ϣ����</param>
        private void DataBind_PatientChargeInfo(ArrayList p_alItem)
        {
            m_objViewer.m_lsvPatientChargeInfo.Items.Clear();
            if (p_alItem == null || p_alItem.Count <= 0) return;
            #region �����Ϣ
            ListViewItem lviTemp = null;
            clsPatientChargeInfo objItem = new clsPatientChargeInfo();
            for (int i1 = 0; i1 < p_alItem.Count; i1++)
            {
                objItem = ((clsPatientChargeInfo)p_alItem[i1]);
                //����
                lviTemp = new ListViewItem(objItem.m_strBedName);
                //����
                lviTemp.SubItems.Add(objItem.m_strPatientName);
                //��������
                lviTemp.SubItems.Add(objItem.m_dblLowerLimitMoney.ToString("0.00"));
                //�ۼƷ���
                lviTemp.SubItems.Add(objItem.m_dblSumMoney.ToString("0.00"));
                //��  ��
                lviTemp.SubItems.Add(objItem.m_dblBalanceMoney.ToString("0.00"));
                //�����ɫ	{ҵ��˵���������������޵ľ�ͻ����ʾ}
                #region �����ɫ
                if (objItem.m_dblBalanceMoney <= objItem.m_dblLowerLimitMoney)
                {
                    lviTemp.BackColor = System.Drawing.Color.Red;
                    lviTemp.ForeColor = System.Drawing.Color.Blue;
                }
                #endregion
                lviTemp.Tag = objItem;
                m_objViewer.m_lsvPatientChargeInfo.Items.Add(lviTemp);
            }
            #endregion
        }
        #endregion
        #region ��ȡ����	���ݿ�ִ��ҽ������
        /// <summary>
        /// ��ȡҽ��������Ϣ����	[ArrayList]	��ӦListView��ʾ
        /// clsOrderBaseInfo
        /// </summary>
        /// <param name="p_objItemArr">��ִ��ҽ������	[����]</param>
        /// <returns></returns>
        private ArrayList GetOrderBaseInfo(clsBIHCanExecOrder[] p_objItemArr)
        {
            ArrayList alResult = new ArrayList();
            if (p_objItemArr == null || p_objItemArr.Length <= 0) return alResult;
            clsOrderBaseInfo objItem;
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                if (!PassFilter(p_objItemArr[i1])) continue;
                objItem = new clsOrderBaseInfo();
                objItem.m_objBIHCanExecOrder = p_objItemArr[i1];
                //����Ĭ�ϲ���	
                #region ����Ĭ�ϲ���
                //ҵ��˵����	{if(�״�ִ�� && ״̬Ϊͨ����� && ����ΪҩƷ) then Ĭ��Ϊ����}
                if (m_strMedicineOrderTypeID.Trim() == "")
                {
                    m_strMedicineOrderTypeID = m_objManage.m_strGetMedicineOrderTypeID();
                }
                if (p_objItemArr[i1].m_intExecuteType == 1 && p_objItemArr[i1].m_intStatus == 5 && p_objItemArr[i1].m_strOrderDicCateID.Trim() == m_strMedicineOrderTypeID)
                {
                    objItem.m_intIsRecruit = 1;
                }
                else
                {
                    objItem.m_intIsRecruit = 0;
                }
                #endregion
                alResult.Add(objItem);
            }
            return alResult;
        }
        /// <summary>
        /// ��ȡҽ��������Ϣ����	[ArrayList]
        /// clsPatientChargeInfo
        /// </summary>
        /// <param name="p_alItem">ҽ��������Ϣ����	[ArrayList]</param>
        /// <returns></returns>
        private ArrayList GetPatientChargeInfo(ArrayList p_alItem)
        {
            clsOrderBaseInfo[] p_objItemArr;
            p_objItemArr = (clsOrderBaseInfo[])(p_alItem.ToArray(typeof(clsOrderBaseInfo)));

            ArrayList alResult = new ArrayList();
            if (p_objItemArr == null || p_objItemArr.Length <= 0) return alResult;
            clsPatientChargeInfo objItem;
            bool blnExist = false;
            for (int i1 = 0; i1 < p_objItemArr.Length; i1++)
            {
                blnExist = false;
                for (int i2 = 0; i2 < alResult.Count; i2++)
                {
                    objItem = (clsPatientChargeInfo)alResult[i2];
                    if (objItem.m_strRegisterID.Trim() == p_objItemArr[i1].m_objBIHCanExecOrder.m_strRegisterID.Trim())
                    {
                        blnExist = true;
                        break;
                    }
                }
                if (!blnExist)
                {
                    objItem = new clsPatientChargeInfo();
                    #region ���
                    double dblSumMoney = 0;			//�ۼƷ���
                    double dblBalanceMoney = 0;		//Ԥ�������
                    double dblLowerLimitMoney = 0;	//��������
                    try
                    {
                        dblSumMoney = m_objManage.m_dblGetSumMoneyByRegisterID(p_objItemArr[i1].m_objBIHCanExecOrder.m_strRegisterID);
                        dblBalanceMoney = m_objManage.m_dblGetBalanceMoneyByRegisterID(p_objItemArr[i1].m_objBIHCanExecOrder.m_strRegisterID);
                        dblLowerLimitMoney = m_objManage.m_dblGetLowerLimitMoneyByRegisterID(p_objItemArr[i1].m_objBIHCanExecOrder.m_strRegisterID);
                    }
                    catch { }
                    //��Ժ�Ǽ�ID
                    objItem.m_strRegisterID = p_objItemArr[i1].m_objBIHCanExecOrder.m_strRegisterID;
                    //������
                    objItem.m_strBedName = p_objItemArr[i1].m_objBIHCanExecOrder.m_strBedName;
                    //��������
                    objItem.m_strPatientName = p_objItemArr[i1].m_objBIHCanExecOrder.m_strPatientName;
                    //�ۼƷ���
                    objItem.m_dblSumMoney = dblSumMoney;
                    //Ԥ�������
                    objItem.m_dblBalanceMoney = dblBalanceMoney;
                    //��������
                    objItem.m_dblLowerLimitMoney = dblLowerLimitMoney;
                    #endregion
                    alResult.Add(objItem);
                }
            }
            return alResult;
        }
        #endregion
        #region ������
        /// <summary>
        /// ������	���˿�ִ��ҽ�������Թ�ListView��ʾ
        /// </summary>
        /// <param name="p_objItem">��ִ��ҽ������</param>
        /// <returns></returns>
        private bool PassFilter(clsBIHCanExecOrder p_objItem)
        {
            if (p_objItem == null || p_objItem.m_strOrderID == null || p_objItem.m_strOrderID.Trim() == "") return false;
            //ҽ������	{1=����;2=��ʱ}
            #region ҽ������
            if (!m_objViewer.m_chkLong.Checked)
            {
                if (p_objItem.m_intExecuteType == 1) return false;
            }
            if (!m_objViewer.m_chkTemp.Checked)
            {
                if (p_objItem.m_intExecuteType == 2) return false;
            }
            #endregion
            //ִ��״̬	{-1����ҽ��;0-����;1-�ύ;2-ִ��;3-ֹͣ;4-����;5-������ύ;6-�����ֹͣ;}
            #region ִ��״̬
            if (!m_objViewer.m_chkStatus1.Checked)
            {
                if (p_objItem.m_intStatus == 1) return false;
            }
            if ((!m_objViewer.m_chkStatus2.Checked))
            {
                if (p_objItem.m_intStatus == 2) return false;
            }
            if ((!m_objViewer.m_chkStatus3.Checked))
            {
                if (p_objItem.m_intStatus == 3) return false;
            }
            if ((!m_objViewer.m_chkStatus5.Checked))
            {
                if (p_objItem.m_intStatus == 5) return false;
            }
            if ((!m_objViewer.m_chkStatus6.Checked))
            {
                if (p_objItem.m_intStatus == 6) return false;
            }
            #endregion
            //��Ժ��ҩ
            #region ��Ժ��ҩ
            if ((m_objViewer.m_chktakeMedicine.Checked))
            {
                if (!(p_objItem.RateType == 4)) return false;
            }
            #endregion
            //���Ե���
            #region ���Ե���
            if ((m_objViewer.m_chkOnlyToday.Checked))
            {
                if (p_objItem.m_dtPostdate.ToString("yyyy-MM-dd") != System.DateTime.Now.ToString("yyyy-MM-dd")) return false;
            }
            #endregion
            //����Ƥ��
            #region ����Ƥ��
            if ((m_objViewer.m_chkNeedFeel.Checked))
            {
                if (p_objItem.m_intISNEEDFEEL != 1) return false;
            }
            #endregion
            //������Ŀ����
            #region ������Ŀ����
            if (m_objViewer.m_cobOrderCate.SelectedIndex > 0)
            {
                clsOrderCate objItem = (clsOrderCate)m_objViewer.m_cobOrderCate.SelectedItem;
                if (objItem.m_objOrderCate.m_strORDERCATEID_CHR != null && objItem.m_objOrderCate.m_strORDERCATEID_CHR.Trim() != "")
                {
                    //��ȡ��ͬ��ʾ���Ƶ� ������ĿID�ۺ�
                    ArrayList alOrdercateID = new ArrayList();
                    for (int i1 = 0; i1 < m_objOrdercateArr.Length; i1++)
                    {
                        if (m_objOrdercateArr[i1].m_strVIEWNAME_VCHR.Trim() == objItem.m_objOrderCate.m_strVIEWNAME_VCHR.Trim())
                        {
                            if (!alOrdercateID.Contains(objItem.m_objOrderCate.m_strORDERCATEID_CHR.Trim()))
                            {
                                alOrdercateID.Add(objItem.m_objOrderCate.m_strORDERCATEID_CHR.Trim());
                            }
                        }
                    }

                    if (!alOrdercateID.Contains(p_objItem.m_strOrderDicCateID.Trim())) return false;
                }
            }
            #endregion
            //¼��ҽ��
            #region ¼��ҽ��
            if (m_objViewer.m_txtDoctor.Tag != null && m_objViewer.m_txtDoctor.Tag.ToString().Trim() != "" && m_objViewer.m_txtDoctor.Text.Trim() != "")
            {
                string strCreatorID = clsConverter.ToString(m_objViewer.m_txtDoctor.Tag).Trim();
                if (strCreatorID != "")
                {
                    if (p_objItem.m_strCreatorID.Trim() != strCreatorID) return false;
                }
            }
            #endregion
            return true;
        }
        #endregion
        #region ��ȡ����	����ListView
        /// <summary>
        /// ��ȡҽ��������Ϣ����	[ArrayList]	����ListView
        /// clsOrderBaseInfo
        /// </summary>
        /// <param name="intCheckedState">�Ƿ�ѡ��{0=δѡ��;1=ѡ��;2ȫ��(Ĭ��)}</param>
        /// <returns></returns>
        private ArrayList GetOrderBaseInfoByListView(int intCheckedState)
        {
            ArrayList alResult = new ArrayList();
            for (int i1 = 0; i1 < m_objViewer.m_lsvOrderBaseInfo.Items.Count; i1++)
            {
                if (m_objViewer.m_lsvOrderBaseInfo.Items[i1].Tag != null)
                {
                    clsOrderBaseInfo objItem = (clsOrderBaseInfo)m_objViewer.m_lsvOrderBaseInfo.Items[i1].Tag;
                    switch (intCheckedState)
                    {
                        case 0://δѡ��
                            if (!m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked)
                            {
                                alResult.Add(objItem);
                            }
                            break;
                        case 1://ѡ��
                            if (m_objViewer.m_lsvOrderBaseInfo.Items[i1].Checked)
                            {
                                alResult.Add(objItem);
                            }
                            break;
                        default://ȫ��(Ĭ��)
                            alResult.Add(objItem);
                            break;
                    }
                }
            }
            return alResult;
        }
        #endregion
        #region ����
        public DateTime m_dtLastValue = DateTime.MinValue;
        /// <summary>
        /// ת��ʱ��To�ַ���
        /// </summary>
        /// <param name="dtValue">DataTime��</param>
        /// <returns></returns>
        private string DateTimeToString(DateTime dtValue)
        {
            if (dtValue.Date == DateTime.MinValue.Date)
                return "";
            else
                return dtValue.ToString("yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// ������ɫ
        /// </summary>
        private void SetOrderColor()
        {
            m_objViewer.m_lblTemp.BackColor = clsOrderColor.BackColorTemOrder;
            m_objViewer.m_lblLong.BackColor = clsOrderColor.BackColorLongOrder;
            m_objViewer.m_lblExecute.BackColor = clsOrderColor.ForeColorOrderStatus2;
            m_objViewer.m_lblNotExecute.BackColor = clsOrderColor.ForeColorOrderStatus1;
            m_objViewer.m_lblStopExecute.BackColor = clsOrderColor.ForeColorOrderStatus3;
            m_objViewer.m_lblCanNotExecute.BackColor = clsOrderColor.ForeColorCanNotExecute;
            /** @update by xzf (05-09-20) */
            //@m_objViewer.m_lblLowerLimitBackColor.BackColor =clsOrderColor.BackColorChargeUnderLowerLimit;
            /* m_objViewer.m_lblLowerLimitBackColor.BackColor =clsOrderColor.BackColorChargeUnderLowerLimit; */
            /* <<================================== */
            m_objViewer.m_lblAuditingExecute.BackColor = clsOrderColor.ForeColorOrderStatus5;
            m_objViewer.m_lblAuditingStop.BackColor = clsOrderColor.ForeColorOrderStatus6;
        }
        /// <summary>
        /// ��ȡListView�е�ѡ����	[��ȡ��Ӧ�Ķ���]
        /// </summary>
        /// <param name="lsvItem">ListView�ؼ�</param>
        /// <returns></returns>
        private ArrayList GetSelectListView(ListView lsvItem)
        {
            ArrayList alSeletcItem = new ArrayList();
            for (int i1 = 0; i1 < lsvItem.Items.Count; i1++)
            {
                if (lsvItem.Items[i1].Checked)
                {
                    alSeletcItem.Add(lsvItem.Items[i1].Tag);
                }
            }
            return alSeletcItem;
        }
        /// <summary>
        /// �ύҽ��ִ�е�	[���䷽��]
        /// </summary>
        /// <param name="strDllName">Dll��</param>
        /// <param name="strClassName">Class��</param>
        /// <param name="strCommitAttachOrder">������</param>
        /// <param name="strOrderID">ҽ��ID</param>
        /// <returns></returns>
        private long lngCommitAttachOrder(string strDllName, string strClassName, string strCommitAttachOrder, string strOrderID)
        {
            if (strDllName.Trim() == "" || strClassName.Trim() == "" || strCommitAttachOrder.Trim() == "" || strOrderID.Trim() == "") return -1;
            long lngRes = 1;
            try
            {
                System.Reflection.Assembly objAsm = System.Reflection.Assembly.LoadFrom(strDllName);
                if (objAsm == null) return -1;
                //���ò���
                object[] objParams = new object[1];
                objParams[0] = strOrderID;
                object obj = objAsm.CreateInstance(strClassName, true);
                if (obj == null) return -1;
                int intIndex = strCommitAttachOrder.IndexOf("(");
                string strMethodName = strCommitAttachOrder.Substring(0, intIndex);
                Type objType = obj.GetType();
                System.Reflection.MethodInfo objMi = objType.GetMethod(strMethodName);
                if (objMi == null) return -1;
                objMi.Invoke(obj, objParams);
            }
            catch
            {
                lngRes = -1;
            }
            return lngRes;
        }
        /// <summary>
        /// ��ʾ������ϸ
        /// </summary>
        /// <param name="arrOrderExecID">ִ�е�	[����]</param>
        public void m_mthShowChargeItemList(string[] arrOrderExecID)
        {
            frmBIHChargeItemList objfrmBIHChargeItemList = new frmBIHChargeItemList();

            objfrmBIHChargeItemList.m_mthSetCurrentArea(clsConverter.ToString(m_objViewer.m_txtArea.Tag), m_objViewer.m_txtArea.Text);
            if (m_objViewer.m_txtBed.Tag == null) m_objViewer.m_txtBed.Tag = "";
            objfrmBIHChargeItemList.m_mthSetBed(m_objViewer.m_txtBed.Tag.ToString());
            objfrmBIHChargeItemList.m_mthSetDoctor(clsConverter.ToString(m_objViewer.m_txtDoctor.Tag), m_objViewer.m_txtDoctor.Text);
            objfrmBIHChargeItemList.m_mthSetExecuteDate(m_objViewer.m_dtpStartExecute.Value);
            objfrmBIHChargeItemList.m_mthSetCurrentDoctor(m_strOperatorID, m_strOperatorName);

            objfrmBIHChargeItemList.m_mthSetOrderList(arrOrderExecID);
            if (m_objViewer.MdiParent != null) objfrmBIHChargeItemList.MdiParent = m_objViewer.MdiParent;
            objfrmBIHChargeItemList.Show();
            objfrmBIHChargeItemList.BringToFront();
        }
        #endregion
        #endregion
        #region ����������
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
                    IList ilUsableAreaID = m_objViewer.LoginInfo.m_ilUsableAreaID;
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
                //LoadBedListView();
            }
        }
        public void m_txtBedKeyDown()
        {
            LoadBedListView();

            //��������	
            #region ��������
            if (m_objViewer.m_txtBed.Tag == null) m_objViewer.m_txtBed.Tag = "";
            string strID = m_objViewer.m_txtBed.Tag.ToString().Trim();
            string[] strIDArr = strID.Split(new char[] { ',' });
            if (strIDArr != null && strIDArr.Length > 0)
            {
                for (int i = 0; i < m_objViewer.m_lsvSelectBed.Items.Count; i++)
                {
                    clsT_Bse_Bed_VO objItem = (m_objViewer.m_lsvSelectBed.Items[i].Tag as clsT_Bse_Bed_VO);
                    if (objItem == null) continue;
                    strID = objItem.m_strBEDID_CHR.Trim();
                    m_objViewer.m_lsvSelectBed.Items[i].Checked = false;
                    for (int j = 0; j < strIDArr.Length; j++)
                    {
                        if (strID == strIDArr[j].Trim())
                        {
                            m_objViewer.m_lsvSelectBed.Items[i].Checked = true;
                            break;
                        }
                    }
                }
            }
            #endregion
            m_objViewer.m_lsvSelectBed.Visible = true;
            m_objViewer.m_lsvSelectBed.Focus();
        }
        /// <summary>
        /// ���벡����Ϣ
        /// </summary>
        public void LoadBedListView()
        {
            m_objViewer.m_txtBed.Text = "";
            m_objViewer.m_txtBed.Tag = "";
            m_objViewer.m_lsvSelectBed.Items.Clear();
            if (m_objViewer.m_txtArea.Tag == null) m_objViewer.m_txtArea.Tag = "";
            string strAreaID = m_objViewer.m_txtArea.Tag.ToString().Trim();
            if (strAreaID.Trim() == "") return;
            clsT_Bse_Bed_VO[] objItemArr;
            long lngRes = m_objInputOrder.m_lngGetBedInfoByAreaID(strAreaID, out objItemArr);
            if (lngRes > 0 && objItemArr != null && objItemArr.Length > 0)
            {
                #region ���ListView
                ListViewItem lviTemp = null;
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    //���
                    //lviTemp = new ListViewItem((i1+1).ToString());
                    //����
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strCODE_CHR);
                    //���
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strSexName);
                    //ռ��״̬
                    //lviTemp.SubItems.Add(objItemArr[i1].m_strStatusName);		
                    lviTemp = new ListViewItem(objItemArr[i1].m_strCODE_CHR);
                    lviTemp.SubItems.Add(objItemArr[i1].m_strPatientName.Trim());
                    lviTemp.SubItems.Add(objItemArr[i1].m_strPatientSex.Trim());

                    lviTemp.Tag = objItemArr[i1];
                    m_objViewer.m_lsvSelectBed.Items.Add(lviTemp);
                }
                #endregion
            }
        }
        /// <summary>
        /// �ָ���","��	Text���洲��	Tag����ID
        /// </summary>
        public void m_lsvSelectBedLeave()
        {
            string strText = "";
            string strID = "";
            clsT_Bse_Bed_VO objItem = new clsT_Bse_Bed_VO();
            for (int i1 = 0; i1 < m_objViewer.m_lsvSelectBed.Items.Count; i1++)
            {
                objItem = ((m_objViewer.m_lsvSelectBed.Items[i1].Tag) as clsT_Bse_Bed_VO);
                if (m_objViewer.m_lsvSelectBed.Items[i1].Checked)
                {
                    if (strText.Length > 0)
                    {
                        strText += ",";
                        strID += ",";
                    }
                    strText += objItem.m_strCODE_CHR.Trim();
                    strID += objItem.m_strBEDID_CHR.Trim();
                }
            }
            m_objViewer.m_txtBed.Text = strText;
            m_objViewer.m_txtBed.Tag = strID;
            m_objViewer.m_lsvSelectBed.Visible = false;
        }
        #endregion
        #region ��ʾ������Ϣ
        /// <summary>
        /// �洢������Ϣ	[��������] {ҽ��ID[�ؼ���],���ö���(ArrayList)}
        /// </summary>
        public System.Collections.Hashtable m_htbToolTip = new Hashtable();
        /// <summary>
        /// ��ListView����ϢToolTip
        /// </summary>
        /// <param name="p_objItem">ҽ����¼����</param>
        /// <param name="p_lsvToolTip">ListView �ؼ�</param>
        /// <returns></returns>
        /// ҵ��˵����
        ///		1���Ա��Ȳ���ȡ���˷��õ�ҽ������ҽ����ʿ���鿴�շ���ϸʱ������Ӧ����ʾ������
        /// </remarks>
        public void m_DisPlayToolTipListView(clsOrderBaseInfo p_objItem, System.Windows.Forms.ListView p_lsvToolTip)
        {
            p_lsvToolTip.Items.Clear();
            string strOrderID = p_objItem.m_objBIHCanExecOrder.m_strOrderID.Trim();
            if ((p_objItem.m_objBIHCanExecOrder.m_intExecuteType == 2 && p_objItem.m_objBIHCanExecOrder.m_intIsRepare >= 3 && p_objItem.m_objBIHCanExecOrder.m_intIsRepare <= 4))
            {
                if (m_htbToolTip.ContainsKey(strOrderID))
                {
                    m_htbToolTip.Remove(strOrderID);
                }
                return;
            }
            else
            {
                if (!m_htbToolTip.ContainsKey(strOrderID))
                {
                    FillToolTipHashtable(p_objItem);
                }
            }
            if (m_htbToolTip.ContainsKey(strOrderID))
            {
                ArrayList alItem = new ArrayList();
                alItem = (m_htbToolTip[strOrderID] as ArrayList);
                if (alItem != null && alItem.Count > 0)
                {
                    clsChargeForDisplay[] objItemArr = (clsChargeForDisplay[])(alItem.ToArray(typeof(clsChargeForDisplay)));
                    //��ʾListView
                    m_objInputOrder.DisplayCharge(objItemArr, p_lsvToolTip);
                }
            }
        }

        bool IsChildPrice(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                return false;
            if (this.isUseChildPrice)
            {
                //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                //{
                    DateTime birthday = (new weCare.Proxy.ProxyIP()).Service.GetBirthdayByOrderId(orderId);
                    return new clsBrithdayToAge().IsChild(birthday);
                //}
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ����ϣ����ֵ ToopTip
        /// </summary>
        /// <param name="p_objItem">ҽ����¼����</param>
        /// <returns></returns>
        private void FillToolTipHashtable(clsOrderBaseInfo p_objItem)
        {
            long lngRes = 0;
            if (p_objItem == null || p_objItem.m_objBIHCanExecOrder.m_strOrderID == null || p_objItem.m_objBIHCanExecOrder.m_strOrderID.Trim() == "") return;
            //��ȡҽ��ID
            string strOrderID = p_objItem.m_objBIHCanExecOrder.m_strOrderID.Trim();
            //���շѵ�����
            double dblNumber = System.Convert.ToDouble(p_objItem.m_objBIHCanExecOrder.m_dmlGet);
            clsT_aid_bih_ordercate_VO objOrdercate;
            lngRes = m_objInputOrder.m_lngGetAidOrderCateByID(p_objItem.m_objBIHCanExecOrder.m_strOrderDicCateID, out objOrdercate);
            if (lngRes > 0 && objOrdercate != null && objOrdercate.m_intDOSAGEVIEWTYPE == 2) dblNumber = 1;
            //ִ��Ƶ��ID
            string strFreqID = p_objItem.m_objBIHCanExecOrder.m_strExecFreqID;
            //�÷�ID
            string strUsageID = p_objItem.m_objBIHCanExecOrder.m_strDosetypeID;
            //�Ƿ��Ӽ�ҽ��	{0=���Ӽ�ҽ��;1=�Ӽ�ҽ��}
            int intIsSonOrder = 0;
            if (p_objItem.m_objBIHCanExecOrder != null && p_objItem.m_objBIHCanExecOrder.m_strParentID != null && p_objItem.m_objBIHCanExecOrder.m_strParentID.Trim() != "")
                intIsSonOrder = 1;

            clsChargeForDisplay[] objItemArr;
            lngRes = m_objInputOrder.m_lngGetBIHCharge(strOrderID, intIsSonOrder, dblNumber, strFreqID, strUsageID, out objItemArr, this.IsChildPrice(strOrderID));
            if (objItemArr != null && objItemArr.Length > 0)
            {
                ArrayList alItem = new ArrayList();
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    alItem.Add(objItemArr[i1]);
                }
                if (alItem != null && alItem.Count > 0 && (!m_htbToolTip.ContainsKey(strOrderID)))
                {
                    m_htbToolTip.Add(strOrderID, alItem);
                }
            }
        }
        /// <summary>
        /// ��ȡҽ��Ƥ�Խ������	[�ӻ�����]
        /// </summary>
        /// <param name="p_strOrderID">ҽ��ID</param>
        /// <param name="p_blnRefurbish">�Ƿ�ˢ�»������еĶ�Ӧ��������	{�����������,������ˢ��}</param>
        private clsT_Opr_Bih_OrderFeel_VO GetOrderFeelFromBuffer(string p_strOrderID, bool p_blnRefurbish)
        {
            clsT_Opr_Bih_OrderFeel_VO objItem = new clsT_Opr_Bih_OrderFeel_VO();
            if (!m_htbToolTip.ContainsKey("ORDERFEEL" + p_strOrderID.Trim()))//{Key="ORDERFEEL"+ҽ��ID}
            {
                long lngRes = m_objManage.m_lngGetOrderFeelByOrderID(p_strOrderID, out objItem);
                if (lngRes > 0 && objItem != null && objItem.m_strORDERID_CHR != null && objItem.m_strORDERID_CHR.Trim() != "")
                {
                    m_htbToolTip.Add("ORDERFEEL" + p_strOrderID.Trim(), objItem);
                }
            }
            else if (p_blnRefurbish)
            {
                long lngRes = m_objManage.m_lngGetOrderFeelByOrderID(p_strOrderID, out objItem);
                if (lngRes > 0 && objItem != null && objItem.m_strORDERID_CHR != null && objItem.m_strORDERID_CHR.Trim() != "")
                {
                    m_htbToolTip["ORDERFEEL" + p_strOrderID.Trim()] = objItem;
                }
            }
            else
            {
                objItem = (m_htbToolTip["ORDERFEEL" + p_strOrderID.Trim()] as clsT_Opr_Bih_OrderFeel_VO);
            }
            return objItem;
        }
        /// <summary>
        /// ��ջ�������
        /// </summary>
        public void m_ClearBuffer()
        {
            m_htbToolTip.Clear();
        }
        #endregion
        #region �ڲ��õ���
        /// <summary>
        /// ҽ��������Ϣ����
        /// </summary>
        public class clsPatientChargeInfo
        {
            public clsPatientChargeInfo()
            { }
            /// <summary>
            /// ��Ժ�Ǽ�ID
            /// </summary>
            public string m_strRegisterID = "";
            /// <summary>
            /// ������
            /// </summary>
            public string m_strBedName = "";
            /// <summary>
            /// ��������
            /// </summary>
            public string m_strPatientName = "";
            /// <summary>
            /// �ۼƷ���
            /// </summary>
            public double m_dblSumMoney = 0;
            /// <summary>
            /// Ԥ�������
            /// </summary>
            public double m_dblBalanceMoney = 0;
            /// <summary>
            /// ��������
            /// </summary>
            public double m_dblLowerLimitMoney = 0;
        }
        /// <summary>
        /// ҽ��������Ϣ����
        /// </summary>
        public class clsOrderBaseInfo
        {
            public clsOrderBaseInfo()
            { }
            /// <summary>
            /// �Ƿ񲹴�	{1=����;0=������}
            /// </summary>
            public int m_intIsRecruit = 0;
            /// <summary>
            /// ִ��ҽ��Vo����
            /// </summary>
            public clsBIHCanExecOrder m_objBIHCanExecOrder = new clsBIHCanExecOrder();
        }
        #endregion

        #region	��ʼ����ʱ��	glzhang 2005.08.1
        /// <summary>
        /// ��ʼ����ʱ��	glzhang 2005.08.1
        /// </summary>
        public void m_mthInitAlert()
        {
            m_timer = new Timer();
            m_timer.Interval = 10 * 60 * 1000;//����ˢ�����ݼ��ʱ��
            m_timer.Tick += new System.EventHandler(m_mthAlert);
            m_timer.Start();
        }
        #endregion

        #region	������ʾ�ƿ���״̬	glzhang	205.08.1
        /// <summary>
        /// ������ʾ��״̬	glzhang	205.08.1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void m_mthAlert(object sender, System.EventArgs e)
        {
            /*  �жϵ�ǰ�����Ƿ�������ύ��ҽ��*/
            /*
			m_objBIHCanExecOrderArr = null;
			//����������
			string strAreaID ="",strBedIDs ="";
			//ִ������
			DateTime dtStartExecute =m_objViewer.m_dtpStartExecute.Value;

			if(m_objViewer.m_txtArea.Tag!=null && m_objViewer.m_txtArea.Tag.ToString().Trim()!="" && m_objViewer.m_txtArea.Text.Trim()!="")
			{
				strAreaID =clsConverter.ToString(m_objViewer.m_txtArea.Tag).Trim();
				strBedIDs =m_objViewer.m_BedIDs;
			}
			else
			{
				return;
			}

			long lngRes=0;

			//��ȡ����ύҽ��
			lngRes =m_objManage.m_lngGetOrderForAuditingExecute(strAreaID,strBedIDs,dtStartExecute,out m_objBIHCanExecOrderArr);
			if(lngRes >0)
			{
				if(m_objBIHCanExecOrderArr==null || m_objBIHCanExecOrderArr.Length<=0)
				{
					m_objViewer.pictureBox3.Visible=false;
					m_objViewer.pictureBox6.Visible=false;
				}
				else
				{
					m_objViewer.pictureBox3.Visible=true;
					m_objViewer.pictureBox6.Visible=true;
				}
				
			}
			else
			{
				MessageBox.Show("��ʱˢ������ʧ�ܡ�����","������ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
             */
            //����������
            string strAreaID = "";

            if (m_objViewer.m_txtArea.Tag != null && m_objViewer.m_txtArea.Tag.ToString().Trim() != "" && m_objViewer.m_txtArea.Text.Trim() != "")
            {
                strAreaID = clsConverter.ToString(m_objViewer.m_txtArea.Tag).Trim();
            }
            long lngRes = m_objManage.m_lngGetOrderForAuditingExecute(strAreaID);
            if (lngRes > 0)
            {
                m_objViewer.pictureBox3.Visible = true;
                m_objViewer.pictureBox6.Visible = true;

            }
            else
            {
                m_objViewer.pictureBox3.Visible = false;
                m_objViewer.pictureBox6.Visible = false;
            }


            /*<==========================================================================*/
        }
        #endregion


        public long m_lngDellORDERCHARGEDEPT(string m_strSeq_int)
        {
            clsDcl_InputOrder objInputOrder = new clsDcl_InputOrder();
            long m_lngref = objInputOrder.m_lngDellORDERCHARGEDEPT(m_strSeq_int);
            return m_lngref;
        }
    }
}
