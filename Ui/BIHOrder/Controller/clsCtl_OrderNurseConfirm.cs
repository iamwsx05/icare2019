using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.iCare.gui.HIS;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_OrderNurseConfirm : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ��������
        clsDcl_ExecuteOrder m_objManage = null;
        clsDcl_InputOrder m_objInputOrder = null;
        DataTable m_dtChargeList;
        DataTable m_dtExecOrder;
        ArrayList m_arrBedIdList = null;//Ϊ��ݼ����°�����ѡ��ҽ����
        public int m_intBedIndex = -1;//Ϊ��ݼ����°�����ѡ��ҽ���� -1Ϊ���ǿ�ݼ���ʽ������Ϊ��ݼ���ʽ
        public bool m_blBedIdKey = false;//Ϊ��ݼ����°�����ѡ��ҽ����(false-���ǿ�ݼ���ʽ,true-��ݼ���ʽ)
        /// <summary>
        /// ���״̬ѡ����0-ȫ��,1-δ���,2-�����
        /// </summary>
        int m_intState = -1;
        /// <summary>
        /// ҽ�������б�
        /// </summary>
        public Hashtable m_htOrderCate = new Hashtable();
        /// <summary>
        /// סԺ�������ñ�VO
        /// </summary>
        public clsSPECORDERCATE m_objSpecateVo;
        /// <summary>
        /// �ύ�Ƿ���Ҫ���
        /// </summary>
        bool m_blNeedComfirm = false;
        /// <summary>
        /// ִ���Ƿ���Ҫ���
        /// </summary>
        bool m_blExeConfirm = false;
        /// <summary>
        /// '1038', 'סԺת�������Ƿ���ʾ�������', '0-��1-��'
        /// </summary>
        internal bool m_blNeedMessageAlert = false;
        /// <summary>
        /// '1039', 'סԺת���������������ʾ���ʱ��', '��λ:��', 10,
        /// </summary>
        int m_intMessageOpenTime = 0;
        /// <summary>
        /// '1040', 'סԺת������������Ѵ�����ʾͣ��ʱ��', '��λ:��', 5, 
        /// </summary>
        int m_intMessageCloseTime = 0;
        /// <summary>
        /// 1018Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        /// </summary>
        decimal m_dmlMedOCMin = 0;
        /// <summary>
        /// 1019Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        /// </summary>
        decimal m_dmlNoMedOCMin = 0;
        /// <summary>
        /// 1020��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        /// </summary>
        decimal m_dmlMedICMin = 0;
        /// <summary>
        /// 1021��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
        /// </summary>
        decimal m_dmlNoMedICMin = 0;
        /// <summary>
        /// '1030', '���ƻ�ʿִ��ģ���Ƿ�����Ƿ�Ѳ���ִ��ҽ��', '0-������ 1-����'
        /// </summary>
        bool m_blMoneyControl = false;
        /// <summary>
        /// '1046', '����Ƿ��ִ��ʱ�Ҳ��˽�Ƿ��ʱ�Ĳ��˷�����ʾ����', '0-����ʾ 1-��ʾ'
        /// </summary>
        bool m_blLessExecuteAlert = false;
        /// <summary>
        /// '1047', 'ҽ��ִ�н����Ƿ���������ѡ��ҽ��ִ�п���', '0-������ 1-����'
        /// </summary>
        bool m_blCanSelectOrder = false;
        /// <summary>
        /// '1049', '����������Ŀ��Ӧ�����շ���Ŀ��һ�Զࣩ�Ƿ��ҩ',  '0-����ҩ 1-��ҩ' 
        /// </summary>
        public bool m_blPutMedicineFormDic = false;
        /// <summary>
        /// "4006"����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
        /// </summary>
        public int m_intLisDiscountNum = 0;
        /// <summary>
        /// 4007�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
        /// </summary>
        public decimal m_decLisDiscountMount = 100;
        /// <summary>
        /// 4008  0-false������ 1-true �������
        /// </summary>
        public bool m_blLisDiscount = false;
        /// <summary>
        /// ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037
        /// </summary>
        public bool m_blStopControl = false;
        /// <summary>
        /// ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
        /// </summary>
        public bool m_blDeableMedControl = false;
        /// <summary>
        ///'1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1, '0010' 
        /// </summary>
        public bool m_blAutoStopAlert = false;
        /// <summary>
        /// ���ͼ���ҽ�����أ� false=ִ��ʱ���ͣ� true=�ύʱ����
        /// '1050' ����ҽ����ִ�л������ύʱ���ͼ������뵥��  0-ִ��ʱ���� 1-�ύʱ����
        /// 2010/8/26 shichun.chen
        /// </summary>
        public bool m_blSendLisBill = false;
        /// <summary>
        /// ҩƷ��������,��Ӧϵͳ����,9003 
        /// </summary>
        public string m_strDiffMedicineType = string.Empty;
        /// <summary>
        /// ��ǰ�����б����
        /// </summary>
        public Hashtable m_htPatientVos = new Hashtable();
        System.Drawing.Color FeedColor = System.Drawing.Color.Green; //��ҪƤ�Ե���Ŀ��ɫ

        /// <summary>
        /// �Ƿ�����Ԥ��ҩĬ��1��
        /// </summary>
        bool isUsePretestMed { get; set; }

        /// <summary>
        /// �Ƿ�ʹ�ö�ͯ�۸� 9015
        /// </summary>
        bool isUseChildPrice { get; set; }

        #endregion

        #region ���캯��
        public clsCtl_OrderNurseConfirm()
        {
            m_objManage = new clsDcl_ExecuteOrder();
            m_objInputOrder = new clsDcl_InputOrder();

        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmOrderNurseConfirm m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOrderNurseConfirm)frmMDI_Child_Base_in;

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
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                LoadTheDate();
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ���浼�뵱ǰ��������
        /// </summary>
        internal void LoadTheDate()
        {
            if (m_objViewer.m_txtArea.Tag == null || ((string)m_objViewer.m_txtArea.Tag).Trim().Equals(""))
            {
                MessageBox.Show("��������ѡ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtArea.Focus();
                return;
            }
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            this.m_objViewer.m_txtChargeSum.Text = "";
            this.m_objViewer.m_txtSameCharge.Text = "";
            m_htPatientVos.Clear();
            m_intState = getState();
            long lngRes = m_objManage.m_lngGetOrderByArea((string)m_objViewer.m_txtArea.Tag, m_intState, out m_dtExecOrder, out m_dtChargeList);

            bool isBed = (this.m_objViewer.m_cboCode.SelectedIndex == 1 ? true : false);
            refreshTheDataByBed(isBed);
            refreshTheDataByFeel();
            ControlTheButton();
        }

        /// <summary>
        /// Ƥ�Թ��� ��ǰ����ֻ��ʾƤ����Ŀ
        /// </summary>
        public void refreshTheDataByFeel()
        {
            if (this.m_objViewer.m_chkNeedFeel.Checked == true)
            {
                ArrayList m_arrFeelOrder = new ArrayList();//����Ƥ����ķ���
                for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                {
                    if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intISNEEDFEEL == 1)
                    {
                        m_arrFeelOrder.Add((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag);
                    }
                }
                //Ƥ�Թ���
                if (m_arrFeelOrder.Count > 0)
                {
                    SetTheNeelSelect(m_arrFeelOrder);

                }
                else
                {
                    this.m_objViewer.m_dtvOrderList.Rows.Clear();
                }

            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// ��ǰ�¿�ҽ������ͳ��
        /// </summary>
        /// <returns></returns>
        private int GetWaitCfPersonCout()
        {
            int cout = 0;
            ArrayList arr = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                string m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intStatus == 1)
                {
                    if (!arr.Contains(m_strRegisterID))
                    {
                        arr.Add(m_strRegisterID);
                    }
                }
            }
            cout = arr.Count;
            return cout;
        }

        /// <summary>
        /// ���浼�뵱ǰ��������
        /// </summary>
        internal void LoadTheDate2(int ColumnIndex)
        {
            int RowIndex = 0;
            if (this.m_objViewer.m_dtvOrderList.SelectedRows.Count > 0)
            {
                RowIndex = this.m_objViewer.m_dtvOrderList.SelectedRows[0].Index;
            }
            DataView myDataView = new DataView(m_dtExecOrder);
            string m_strColumn = GetTheSortColumnName(ColumnIndex);

            myDataView.Sort = m_strColumn;
            if (myDataView.Count <= 0)
            {
                return;
            }
            clsBIHCanExecOrder[] arrExecOrder;
            filltheExecOrderTable(myDataView, out arrExecOrder);
            ResortTheDataGridView(arrExecOrder);

            if (this.m_objViewer.m_dtvOrderList.RowCount > RowIndex)
            {
                this.m_objViewer.m_dtvOrderList.CurrentCell = this.m_objViewer.m_dtvOrderList[ColumnIndex, RowIndex];
            }
            // SelectAll();

        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="arrExecOrder"></param>
        public void ResortTheDataGridView(clsBIHCanExecOrder[] arrExecOrder)
        {
            clsBIHCanExecOrder oldExecOrder;
            int j = 0;
            for (int i = 0; i < arrExecOrder.Length; i++)
            {
                j = findTheExecOrder(arrExecOrder[i]);
                ChangeThePosition(i, j);
            }
            //���ر���ֶν���������ʾ�հ׿���
            ArrayList m_arrSep = new ArrayList();
            m_arrSep.Add("dtv_CURAREAName");
            m_arrSep.Add("dtv_bedcode");
            m_arrSep.Add("m_dtvLastName");
            RefreshTheSPColumn(m_arrSep);
        }

        /// <summary>
        ///  //���ر���ֶν���������ʾ�հ׿���
        /// </summary>
        /// <param name="m_arrSep"></param>
        public void RefreshTheSPColumn(ArrayList m_arrSep)
        {
            string m_strUpRegisterID = "", m_strDownRegisterID = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {

                m_strDownRegisterID = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (!m_strUpRegisterID.Equals(m_strDownRegisterID))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["dtv_CURAREAName"].Value = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strCURAREAName;
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["dtv_bedcode"].Value = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strBedName;
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvLastName"].Value = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strPatientName;
                }
                else
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["dtv_CURAREAName"].Value = "";
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["dtv_bedcode"].Value = "";
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvLastName"].Value = "";
                }
                m_strUpRegisterID = m_strDownRegisterID;
            }
        }

        /// <summary>
        /// �ҵ���Ӧ��ҽ��ִ�ж����е�DATAGRIDVIEW��λ��
        /// </summary>
        /// <param name="clsBIHCanExecOrder"></param>
        /// <returns></returns>
        public int findTheExecOrder(clsBIHCanExecOrder clsBIHCanExecOrder)
        {
            string m_strOrderID = clsBIHCanExecOrder.m_strOrderID;
            int j = 0;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID.Equals(m_strOrderID))
                {
                    j = i;
                    break;
                }
            }
            return j;
        }

        private void ChangeThePosition(int i, int j)
        {
            string m_strValue1, m_strValue2;
            for (int a = 0; a < this.m_objViewer.m_dtvOrderList.ColumnCount; a++)
            {
                m_strValue1 = this.m_objViewer.m_dtvOrderList.Rows[i].Cells[a].Value.ToString().Trim();
                m_strValue2 = this.m_objViewer.m_dtvOrderList.Rows[j].Cells[a].Value.ToString().Trim();
                this.m_objViewer.m_dtvOrderList.Rows[i].Cells[a].Value = m_strValue2;
                this.m_objViewer.m_dtvOrderList.Rows[j].Cells[a].Value = m_strValue1;
            }
            clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
            this.m_objViewer.m_dtvOrderList.Rows[i].Tag = this.m_objViewer.m_dtvOrderList.Rows[j].Tag;
            this.m_objViewer.m_dtvOrderList.Rows[j].Tag = order;
        }

        /// <summary>
        /// �����������õ���Ӧ�����ֶ���
        /// </summary>
        /// <param name="ColumnIndex"></param>
        /// <returns></returns>
        public string GetTheSortColumnName(int ColumnIndex)
        {
            string m_strColumnBegin = "CURAREAName,code_chr,LASTNAME_VCHR";//����ͨ�ֶν�������
            //string m_strColumnID = "";//�Թؼ����ֶν�������(�紲λ�ż�����)
            string m_strColumn = "";//���صĹ��������ֶ�
            m_strColumn = this.m_objViewer.m_dtvOrderList.Columns[ColumnIndex].Name;
            switch (m_strColumn)
            {
                case "dtv_bedcode":
                    m_strColumnBegin = "CURAREAName,code_chr";

                    if (this.m_objViewer.m_dtvOrderList.SortOrder == SortOrder.Ascending)
                    {
                        m_strColumnBegin = m_strColumnBegin + " asc";
                    }
                    else
                    {
                        m_strColumnBegin = m_strColumnBegin + " desc";
                    }
                    m_strColumnBegin = m_strColumnBegin + "," + "LASTNAME_VCHR";
                    m_strColumn = "";
                    break;
                case "m_dtvLastName":
                    m_strColumn = "";
                    break;
                case "dtv_RecipeNo":
                    m_strColumn = "recipeno_int";
                    break;
                case "dtv_ExecuteType":
                    m_strColumn = "executetype_int";
                    break;
                case "viewname_vchr":
                    m_strColumn = "viewname_vchr";
                    break;
                case "dtv_Name":
                    m_strColumn = "NAME_VCHR";
                    break;
                case "dtv_Dosage":
                    m_strColumn = "Dosage_Dec";
                    break;
                case "dtv_UseType":
                    m_strColumn = "DOSETYPENAME_CHR";
                    break;
                case "dtv_Freq":
                    m_strColumn = "EXECFREQNAME_CHR";
                    break;
                case "dtv_Get":
                    m_strColumn = "GET_DEC";
                    break;
                case "dtv_ENTRUST":
                    m_strColumn = "ENTRUST_VCHR";
                    break;
                case "DOCTOR_VCHR":
                    m_strColumn = "DOCTOR_VCHR";
                    break;
                case "m_dtvPOSTDATE_DAT":
                    m_strColumn = "POSTDATE_DAT";
                    break;
                case "m_dtvCONFIRMER_VCHR":
                    m_strColumn = "CONFIRMER_VCHR";
                    break;
                case "m_dtvCONFIRM_DAT":
                    m_strColumn = "CONFIRM_DAT";
                    break;
                case "CREATOR_CHR":
                    m_strColumn = "CREATOR_CHR";
                    break;
                case "dtv_NO":
                    m_strColumn = "";
                    break;
                case "dtv_CURAREAName":
                    m_strColumnBegin = "CURAREAName";

                    if (this.m_objViewer.m_dtvOrderList.SortOrder == SortOrder.Ascending)
                    {
                        m_strColumnBegin = m_strColumnBegin + " asc";
                    }
                    else
                    {
                        m_strColumnBegin = m_strColumnBegin + " desc";
                    }

                    m_strColumnBegin = m_strColumnBegin + "," + "code_chr,LASTNAME_VCHR";
                    m_strColumn = "";
                    break;
            }
            if (m_strColumn.Trim().Equals(""))
            {
                m_strColumn = m_strColumnBegin + m_strColumn;
            }
            else
            {
                m_strColumn = m_strColumnBegin + "," + m_strColumn;
            }
            if (this.m_objViewer.m_dtvOrderList.SortOrder == SortOrder.Ascending)
            {
                m_strColumn = m_strColumn + " asc";
            }
            else
            {
                m_strColumn = m_strColumn + " desc";
            }
            return m_strColumn;
        }

        /// <summary>
        /// �������޸��շ���Ŀʱ�����ύ�շ�����
        /// </summary>
        internal void refreshTheChargeDate()
        {
            if (m_objViewer.m_txtArea.Tag == null || ((string)m_objViewer.m_txtArea.Tag).Trim().Equals(""))
            {

                return;
            }
            m_intState = getState();
            long lngRes = m_objManage.m_lngrefreshTheChargeDate((string)m_objViewer.m_txtArea.Tag, m_intState, out m_dtChargeList);

        }

        /// <summary>
        /// ���ص�ǰ���״̬ѡ����
        /// </summary>
        /// <returns></returns>
        private int getState()
        {
            int i = -1;
            if (m_objViewer.m_rdoAll.Checked)
            {
                i = 0;
            }
            else if (m_objViewer.m_rdoNOT.Checked)
            {
                i = 1;
            }
            else if (m_objViewer.m_rdoYET.Checked)
            {
                i = 2;
            }

            return i;
        }
        #endregion

        #region ��ǰҽ��ִ�м�¼ѡ���¼�����
        /// <summary>
        /// ��ǰҽ��ִ�м�¼ѡ���¼�����
        /// </summary>
        internal void OrderListSelect()
        {
            this.m_objViewer.m_txtSameCharge.Text = "";
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            if (this.m_objViewer.m_dtvOrderList.CurrentCell != null && this.m_objViewer.m_dtvOrderList.RowCount > 0)
            {

                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[this.m_objViewer.m_dtvOrderList.CurrentCell.RowIndex].Tag;

                if (order != null)
                {
                    //if (m_dtPatients != null && m_dtPatients.Rows.Count > 0)
                    //{
                    //fillthePatient(order);
                    filltheChargeList(order);
                    decimal m_decSum = m_objInputOrder.GettheChargeSum(order, m_dtChargeList);
                    this.m_objViewer.m_txtChargeSum.Text = "�����ܼƣ�" + m_decSum.ToString();
                    m_decSum = m_objInputOrder.GetTheSameChargeSum(order, m_dtChargeList);
                    this.m_objViewer.m_txtSameCharge.Text = "ͬ�������ܼƣ�" + m_decSum.ToString();

                    //}
                    ControlTheButton();
                    TheSameNoRowSelect(order);
                }
            }

        }



        /// <summary>
        /// ͬ��ҽ��һ��ѡ��
        /// </summary>
        /// <param name="order"></param>
        private void TheSameNoRowSelect(clsBIHCanExecOrder order)
        {
            string m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
            string m_strTemp = "";

            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                clsBIHCanExecOrder Exeorder = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strTemp = Exeorder.m_strRegisterID + "," + Exeorder.m_intRecipenNo.ToString() + ";";
                if (m_strTemp.Equals(m_strID))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Selected = true;
                }

            }
            /*<====================================*/
        }


        #endregion

        #region ��ǰҽ����¼�ı�ʱ���˸ı��¼�
        /// <summary>
        /// ��ǰҽ����¼�ı�ʱ���˸ı��¼�
        /// </summary>
        /// <param name="order"></param>
        private void BindthePatient(clsBIHCanExecOrder order)
        {
            clsBIHPatientInfo m_objPatient = null;
            if (m_htPatientVos.Contains(order.m_strRegisterID))
            {
                m_objPatient = (clsBIHPatientInfo)m_htPatientVos[order.m_strRegisterID];
            }
            else
            {
                DataTable m_dtPatient = null;
                long lngRes = m_objManage.m_lngGetPatientInfoVo(order.m_strRegisterID, out m_dtPatient);
                if (m_dtPatient != null && m_dtPatient.Rows.Count > 0)
                {
                    string m_strInHospitalNoOld = "";
                    string m_strInHospitalNoNow = "";
                    m_strInHospitalNoOld = this.m_objViewer.m_ctlPatient.m_txtRegisterid.Text.Trim();
                    m_strInHospitalNoNow = order.m_strRegisterID.Trim();
                    if (m_strInHospitalNoOld.Equals(m_strInHospitalNoNow))
                    {
                        return;
                    }

                    DataView myDataView = new DataView(m_dtPatient);
                    myDataView.RowFilter = "registerid_chr='" + order.m_strRegisterID + "'";
                    if (myDataView.Count <= 0)
                    {
                        return;
                    }
                    m_mthGetPatientInfoFromDateTable(myDataView, out m_objPatient);
                    m_htPatientVos.Add(order.m_strRegisterID, m_objPatient);
                }
            }
            if (m_objPatient != null)
            {
                this.m_objViewer.m_ctlPatient.m_objPatient = m_objPatient;
                this.m_objViewer.m_ctlPatient.m_mthShowPatient();
            }
        }
        #endregion

        #region ��ҽ��ִ��DATAGRIDVIEW
        /// <summary>
        /// ��ҽ��ִ��DATAGRIDVIEW
        /// </summary>
        /// <param name="arrExecOrder"></param>
        private void BindTheBihOrderList(clsBIHCanExecOrder[] arrExecOrder)
        {
            m_objViewer.m_dtvOrderList.Rows.Clear();
            if (arrExecOrder != null)
            {
                // ��ţ���CheckBox��dtv_NO\����dtv_bedcode\����m_dtvLastName\����dtv_RecipeNo\ҽ����ҽ����ʽ��dtv_ExecuteType\���viewname_vchr
                // ��Ŀ����dtv_Name\����dtv_Dosage\�÷�dtv_UseType\Ƶ��dtv_Freq\����dtv_Get\����dtv_ENTRUST\����ҽ��DOCTOR_VCHR\�ύʱ��m_dtvPOSTDATE_DAT\�����m_dtvCONFIRMER_VCHR\���ʱ�� m_dtvCONFIRM_DAT
                DataGridViewRow row = null;
                foreach (clsBIHCanExecOrder item in arrExecOrder)
                {
                    m_objViewer.m_dtvOrderList.Rows.Add();
                    row = m_objViewer.m_dtvOrderList.Rows[m_objViewer.m_dtvOrderList.RowCount - 1];
                    BindTheBihOrderListDetail(row, item);
                }
            }
            refreshTheDataGridView();
            if (m_blAutoStopAlert)
            {
                GetTheStopOrder();
            }
        }

        /// <summary>
        /// ˢ�½�����������
        /// </summary>
        public void refreshTheDataGridView()
        {
            //ˢ����ͬ����ҽ��������ͬ���ʵ��ֶ�
            m_mthRefreshSamePersionColumn();
            //ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�
            m_mthRefreshSameReqNoColor();
            //��̴����¼�
            if (this.m_objViewer.m_dtvOrderList.RowCount > 0)
            {
                this.m_objViewer.m_dtvOrderList.CurrentCell = this.m_objViewer.m_dtvOrderList.Rows[0].Cells[1];
            }
            //��ǰ�¿�ҽ������ͳ��
            int cout = GetWaitCfPersonCout();
            this.m_objViewer.m_lblNewOrderCount.Text = "���� " + cout.ToString() + " �������¿���ҽ��";
        }

        /// <summary>
        /// ˢ����ͬ����ҽ��������ͬ���ʵ��ֶ�
        /// </summary>
        internal void m_mthRefreshSamePersionColumn()
        {
            string m_strPreRegister = "";
            for (int i = 0; i < m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                DataGridViewRow row1 = m_objViewer.m_dtvOrderList.Rows[i];
                clsBIHCanExecOrder m_objExecOrder = (clsBIHCanExecOrder)row1.Tag;
                if (m_strPreRegister.Trim().Equals(m_objExecOrder.m_strRegisterID.Trim()))
                {
                    row1.Cells["dtv_bedcode"].Value = "";
                    row1.Cells["m_dtvLastName"].Value = "";
                    row1.Cells["inpatientid"].Value = "";
                    //row1.Cells["dtv_CURAREAName"].Value = "";
                }
                m_strPreRegister = m_objExecOrder.m_strRegisterID;
            }
        }

        /// <summary>
        /// ��ҽ��ִ����ϸ����
        /// </summary>
        internal void BindTheBihOrderListDetail(DataGridViewRow row, clsBIHCanExecOrder objExecOrder)
        {
            row.Cells["dtv_NO"].Value = Convert.ToString((row.Index + 1));
            //  row.ReadOnly = true;
            decimal m_dmlOneUse = 0;    // ��һ�ε�����
            if (m_objViewer.m_chkSelectAll.Checked == true)
            {
                row.Cells["m_dtvselectCheck"].Value = "1";
            }
            else
            {
                row.Cells["m_dtvselectCheck"].Value = "0";
            }

            row.Cells["dtv_bedcode"].Value = objExecOrder.m_strBedName;
            row.Cells["m_dtvLastName"].Value = objExecOrder.m_strPatientName;
            row.Cells["inpatientid"].Value = objExecOrder.m_strInpatientID;

            // ҽ������
            clsT_aid_bih_ordercate_VO p_objItem = null;
            if (m_htOrderCate.Contains(objExecOrder.m_strOrderDicCateID))
            {
                p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[objExecOrder.m_strOrderDicCateID];
            }
            if (p_objItem == null)
            {
                if (objExecOrder.m_intTYPE_INT > 0)
                {
                    row.Cells["dtv_Name"].Value = objExecOrder.m_strName.ToString();
                    row.Cells["dtv_Name"].Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                    row.Tag = objExecOrder;
                    return;
                }
            }

            if (objExecOrder.m_intExecuteType == 1)
            {
                // ��
                row.Cells["dtv_RecipeNo"].Value = " " + objExecOrder.m_intRecipenNo2.ToString();
            }
            row.Cells["dtv_ExecuteType"].Value = objExecOrder.m_strExecuteTypeName;
            row.Cells["viewname_vchr"].Value = objExecOrder.m_strOrderDicCateName;
            row.Cells["dtv_Name"].Value = objExecOrder.m_strName;

            row.Cells["dtv_Dosage"].Value = objExecOrder.m_dmlDosageRate.ToString() + objExecOrder.m_strDosageUnit.ToString().Trim();   // ����
            row.Cells["dtv_UseType"].Value = objExecOrder.m_strDosetypeName;    // ��ҩ��ʽ
            row.Cells["dtv_Freq"].Value = objExecOrder.m_strExecFreqName;       // Ƶ��
            row.Cells["dtv_Get"].Value = objExecOrder.m_dmlGet.ToString() + objExecOrder.m_strGetunit;
            row.Cells["dtv_ENTRUST"].Value = objExecOrder.m_strEntrust;
            row.Cells["DOCTOR_VCHR"].Value = objExecOrder.m_strDOCTOR_VCHR;
            row.Cells["m_dtvPOSTDATE_DAT"].Value = objExecOrder.m_dtPostdate.ToString();
            row.Cells["m_dtvCONFIRMER_VCHR"].Value = objExecOrder.m_strASSESSORFOREXEC_CHR;
            row.Cells["m_dtvCONFIRM_DAT"].Value = objExecOrder.m_strASSESSORFOREXEC_DAT;
            row.Cells["CREATOR_CHR"].Value = objExecOrder.m_strCreator;
            string m_strCase = "";
            if (objExecOrder.m_intStatus == 1 || objExecOrder.m_intStatus == 5)
            {
                m_strCase = "��";
                row.Cells["dtv_Case"].Style.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                m_strCase = "ͣ";
            }
            row.Cells["dtv_Case"].Value = m_strCase;

            // ���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
            if (!objExecOrder.m_strPARTID_VCHR.Trim().Equals(""))
            {
                row.Cells["dtv_method"].Value = objExecOrder.m_strPARTNAME_VCHR;
            }
            else if (!objExecOrder.m_strSAMPLEID_VCHR.Trim().Equals(""))
            {
                row.Cells["dtv_method"].Value = objExecOrder.m_strSAMPLEName_VCHR;
            }
            else
            {
                row.Cells["dtv_method"].Value = "";
            }
            // ҽ��˵��
            row.Cells["dtv_REMARK"].Value = objExecOrder.m_strREMARK_VCHR.Trim();

            #region ҽ�����ͽ��洦��
            p_objItem = null;
            if (m_htOrderCate.Contains(objExecOrder.m_strOrderDicCateID))
            {
                p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[objExecOrder.m_strOrderDicCateID];
            }
            if (p_objItem != null)
            {
                if (!objExecOrder.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))  // ������ҽ������ʾ����
                {
                    if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
                    {
                        // ����
                        if (objExecOrder.m_dmlDosage > 0)
                        {
                            row.Cells["dtv_Dosage"].Value = objExecOrder.m_dmlDosage.ToString() + "" + objExecOrder.m_strDosageUnit;
                        }
                        else
                        {
                            row.Cells["dtv_Dosage"].Value = "";
                        }
                    }
                    else
                    {
                        row.Cells["dtv_Dosage"].Value = "";
                    }
                }
                else
                {
                    row.Cells["dtv_Dosage"].Value = "";
                }

                if (!objExecOrder.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))  // ������ҽ������ʾ����
                {
                    if (p_objItem.m_intUSAGEVIEWTYPE == 1)
                    {
                        // �÷�
                        row.Cells["dtv_UseType"].Value = objExecOrder.m_strDosetypeName;
                    }
                    else
                    {
                        // �÷�
                        row.Cells["dtv_UseType"].Value = "";
                    }
                }
                else
                {
                    // �÷�
                    row.Cells["dtv_UseType"].Value = "";
                }

                if (p_objItem.m_intExecuFrenquenceType == 1)
                {
                    // Ƶ��
                    row.Cells["dtv_Freq"].Value = objExecOrder.m_strExecFreqName;
                }
                else
                {
                    // ������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                    if (objExecOrder.m_intCHARGE_INT == 1)
                    {
                        row.Cells["dtv_Freq"].Value = objExecOrder.m_strExecFreqName;   // Ƶ��
                    }
                    else
                    {
                        row.Cells["dtv_Freq"].Value = "";   // Ƶ��
                    }
                }

                if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1)
                {
                    // ����
                    row.Cells["ATTACHTIMES_INT"].Value = objExecOrder.m_intATTACHTIMES_INT;
                    m_dmlOneUse = objExecOrder.m_dmlOneUse * objExecOrder.m_intATTACHTIMES_INT;
                }
                else
                {
                    // ����
                    row.Cells["ATTACHTIMES_INT"].Value = "";
                    m_dmlOneUse = 0;
                }
                // ����
                if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
                {
                    if (objExecOrder.m_dmlGet > 0)
                    {
                        row.Cells["dtv_Get"].Value = objExecOrder.m_dmlGet.ToString() + " " + objExecOrder.m_strGetunit;
                    }
                    else
                    {
                        row.Cells["dtv_Get"].Value = "";
                    }
                }
                else
                {
                    // ����
                    row.Cells["dtv_Get"].Value = "";
                }
            }
            else
            {
                // ����
                row.Cells["dtv_Dosage"].Value = "";
                // Ƶ��
                row.Cells["dtv_Freq"].Value = "";
                // �÷�
                row.Cells["dtv_UseType"].Value = "";
                // ����
                row.Cells["ATTACHTIMES_INT"].Value = "";
                // ����
                row.Cells["dtv_Get"].Value = "";
            }
            #endregion
            string m_strFeel = "";
            if (objExecOrder.m_intISNEEDFEEL == 1)
            {

                switch (objExecOrder.m_intFEEL_INT)
                {
                    case 0:
                        m_strFeel = " AST( ) ";
                        break;
                    case 1:
                        m_strFeel = " AST(-) ";
                        break;
                    case 2:
                        m_strFeel = " AST(+) ";
                        break;
                }
                row.Cells["dtv_Name"].Value = objExecOrder.m_strName + m_strFeel;
            }
            // ��Ժ��ҩ����
            string m_strOUTGETMEDDAYS_INT = "";
            // �����ֶεĿ���
            if (objExecOrder.m_strOrderDicCateID.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))      // ��ҩ�����߼�
            {
                row.Cells["dtv_sum"].Value = objExecOrder.m_intOUTGETMEDDAYS_INT.ToString() + "����" + Convert.ToString(objExecOrder.m_dmlGet + m_dmlOneUse) + objExecOrder.m_strGetunit;
                m_strOUTGETMEDDAYS_INT = objExecOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
            }
            else
            {
                // ���� N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
                if (objExecOrder.m_intExecuteType == 3)
                {
                    row.Cells["dtv_sum"].Value = objExecOrder.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(objExecOrder.m_dmlGet + m_dmlOneUse) + objExecOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = objExecOrder.m_intOUTGETMEDDAYS_INT.ToString() + "��";
                }
                else
                {
                    row.Cells["dtv_sum"].Value = "��" + Convert.ToString(objExecOrder.m_dmlGet + m_dmlOneUse) + objExecOrder.m_strGetunit;
                    m_strOUTGETMEDDAYS_INT = "";
                }

            }
            row.Cells["m_dtvASSESSORFORSTOP_CHR"].Value = objExecOrder.m_strASSESSORFORSTOP_CHR;
            row.Cells["m_dtvASSESSORFORSTOP_DAT"].Value = objExecOrder.m_strASSESSORFORSTOP_DAT;
            // ����
            row.Cells["dtv_Name"].Value = objExecOrder.m_strName + " " + row.Cells["dtv_Dosage"].Value.ToString() + " " + row.Cells["dtv_UseType"].Value.ToString() + " " + row.Cells["dtv_Freq"].Value.ToString() + m_strFeel + " " + m_strOUTGETMEDDAYS_INT;

            #region Ԥ������.Ĭ��1��
            if (this.isUsePretestMed)
            {
                if (objExecOrder.m_intStatus == 1)
                {
                    if (objExecOrder.m_strExecuteTypeName.Contains("����"))
                    {
                        if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(objExecOrder.m_dtPostdate.ToShortDateString())).Days == 0)   // ����
                        {
                            //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                            //{
                            if ((new weCare.Proxy.ProxyIP()).Service.IsMedInjection(objExecOrder.m_strOrderDicID, 1) == true)    // ���
                            {
                                row.Cells["pretestdays"].Value = "1";
                            }
                            //}
                        }
                    }
                }
            }
            #endregion

            row.Tag = objExecOrder;
        }
        #endregion

        /// <summary>
        /// ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�,Ƥ�Խ����ɫ��ʾ
        /// </summary>
        public void m_mthRefreshSameReqNoColor()
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                DataGridViewRow objRow = this.m_objViewer.m_dtvOrderList.Rows[i];
                clsBIHCanExecOrder ExecOrder = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;

                if (ExecOrder.m_intISNEEDFEEL == 1 && (ExecOrder.m_intStatus == 1 || ExecOrder.m_intStatus == 5))
                {
                    objRow.Cells["dtv_Name"].Style.ForeColor = FeedColor;       // ��ҪƤ�Ե���Ŀ��ɫ
                }

                // ��ͣ������ͣ��ҽ��,��ִ�й��������ú�ɫ��ʾ
                if (ExecOrder.m_intStatus == 3 || ExecOrder.m_intStatus == 6 || (ExecOrder.m_intExecuteType == 2 && ExecOrder.m_intStatus == 2))
                {
                    objRow.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                }
                if (i < this.m_objViewer.m_dtvOrderList.RowCount - 1)
                {
                    clsBIHCanExecOrder ExecOrder2 = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i + 1].Tag;
                    if ((ExecOrder.m_strRegisterID == ExecOrder2.m_strRegisterID) && (ExecOrder.m_intRecipenNo == ExecOrder2.m_intRecipenNo))
                    {
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_RecipeNo"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_ExecuteType"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_ENTRUST"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_CASE"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["ATTACHTIMES_INT"].Value = "";
                        if (ExecOrder.m_strOrderDicCateID.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))     // ��ҩ�����߼�
                        {
                            this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_REMARK"].Value = "";
                        }
                    }
                }
            }
        }

        #region Ϊ����datagridview��ֵ
        /// <summary>
        /// Ϊ����datagridview��ֵ
        /// </summary>
        /// <param name="order"></param>
        private void filltheChargeList(clsBIHCanExecOrder order)
        {
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            if (m_dtChargeList != null && m_dtChargeList.Rows.Count > 0)
            {

                DataView myDataView = new DataView(m_dtChargeList);
                myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
                myDataView.Sort = "FLAG_INT";
                if (myDataView.Count <= 0)
                {
                    return;
                }
                clsChargeForDisplay[] m_arrObjItem;
                m_mthGetChargeListFromDateTable(myDataView, out m_arrObjItem);
                int k = 0;
                for (int i = 0; i < m_arrObjItem.Length; i++)
                {

                    k++;
                    this.m_objViewer.m_dtvChangeList.Rows.Add();
                    DataGridViewRow row1 = this.m_objViewer.m_dtvChangeList.Rows[this.m_objViewer.m_dtvChangeList.RowCount - 1];
                    row1.Cells["seq"].Value = Convert.ToString(k);
                    row1.Cells["chargeName"].Value = m_arrObjItem[i].m_strName;
                    row1.Cells["ITEMSPEC_VCHR"].Value = m_arrObjItem[i].m_strSPEC_VCHR;
                    row1.Cells["ChargeClass"].Value = "";
                    switch (m_arrObjItem[i].m_intType)
                    {
                        case 0:
                            row1.Cells["ChargeClass"].Value = "����Ŀ";
                            break;
                        case 1:
                            row1.Cells["ChargeClass"].Value = "������Ŀ";
                            break;
                        case 2:
                            row1.Cells["ChargeClass"].Value = "�÷�����";
                            break;
                        case 3:
                            row1.Cells["ChargeClass"].Value = "����¼��";
                            break;
                    }

                    row1.Cells["ChargePrice"].Value = m_arrObjItem[i].m_dblPrice.ToString();
                    row1.Cells["get_count"].Value = m_arrObjItem[i].m_dblDrawAmount.ToString() + " " + m_arrObjItem[i].m_strUNIT_VCHR;
                    row1.Cells["countSum"].Value = m_arrObjItem[i].m_dblMoney.ToString();
                    row1.Cells["TotalDiffCost"].Value = m_arrObjItem[i].m_dblDiffCostMoney.ToString("0.0000");// ���������
                    switch (m_arrObjItem[i].m_intCONTINUEUSETYPE_INT)
                    {
                        case 1:
                            row1.Cells["xuClass"].Value = "�״���";
                            break;
                        case 0:
                            row1.Cells["xuClass"].Value = "������";
                            break;
                        default:
                            row1.Cells["xuClass"].Value = " -- ";
                            break;
                    }

                    row1.Cells["excuteDept"].Value = m_arrObjItem[i].m_strClacareaName_chr;
                    row1.Cells["YBClass"].Value = m_arrObjItem[i].m_strYBClass;
                    row1.Cells["IPNOQTYFLAG_INT"].Value = "";
                    if (m_arrObjItem[i].m_intITEMSRCTYPE_INT == 1)
                    {
                        if (m_arrObjItem[i].m_intIPNOQTYFLAG_INT == 1)
                        {
                            row1.Cells["IPNOQTYFLAG_INT"].Value = "ȱҩ";
                            row1.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    row1.Tag = m_arrObjItem[i];

                    //��� seq
                    //��Ŀ���� chargeName
                    //�������� ChargeClass
                    //���� ChargePrice
                    //���� get_count
                    //�ܽ�� countSum
                    //�������� xuClass
                    //ִ�п��� excuteDept
                    //ҽ������ YBClass
                }
            }

        }
        #endregion

        #region ��ִ��ҽ������ֵ
        /// <summary>
        /// ��ִ��ҽ������ֵ
        /// </summary>
        /// <param name="objDT"></param>
        /// <param name="arrExecOrder"></param>
        private void filltheExecOrderTable(DataView objRow, out clsBIHCanExecOrder[] arrExecOrder)
        {
            //ҽ��ִ�ж�������
            //arrExecOrder = new clsBIHCanExecOrder[0];//�ɴ���
            int intRowsCount = objRow.Count;

            if (intRowsCount <= 0)
            {
                arrExecOrder = new clsBIHCanExecOrder[0];//new code
                //return; //�ɴ���
            }
            else
            {
                //arrExecOrder = new clsBIHCanExecOrder[objRow.Count];//�ɴ���
                arrExecOrder = new clsBIHCanExecOrder[intRowsCount];//new code
                m_arrBedIdList = new ArrayList();
                System.Data.DataRowView objOneDataRowView = null;

                for (int i = 0; i < intRowsCount; i++)
                {
                    objOneDataRowView = objRow[i];//new code

                    arrExecOrder[i] = new clsBIHCanExecOrder();

                    arrExecOrder[i].m_strInpatientID = objOneDataRowView["inpatientid_chr"].ToString().Trim();

                    //arrExecOrder[i].m_strBedID = Convert.ToString(objRow[i]["bedid_chr"].ToString().Trim());//�ɴ���
                    arrExecOrder[i].m_strBedID = objOneDataRowView["bedid_chr"].ToString().Trim();//�´���

                    //arrExecOrder[i].m_strBedName = Convert.ToString(objRow[i]["code_chr"].ToString().Trim()); //old code
                    arrExecOrder[i].m_strBedName = objOneDataRowView["code_chr"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strCURBEDID_CHR = Convert.ToString(objRow[i]["CURBEDID_CHR"].ToString().Trim());//old code
                    arrExecOrder[i].m_strCURBEDID_CHR = objOneDataRowView["CURBEDID_CHR"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strCURBEDName = Convert.ToString(objRow[i]["code_chr"].ToString().Trim());//ԭ�����Ѿ�û��ʹ����

                    //arrExecOrder[i].m_strRegisterID = Convert.ToString(objRow[i]["registerid_chr"].ToString().Trim());//old code
                    arrExecOrder[i].m_strRegisterID = objOneDataRowView["registerid_chr"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strPatientName = Convert.ToString(objRow[i]["LASTNAME_VCHR"].ToString().Trim());//���� old code
                    arrExecOrder[i].m_strPatientName = objOneDataRowView["LASTNAME_VCHR"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strPatientSex = Convert.ToString(objRow[i]["SEX_CHR"].ToString().Trim());//���� old code
                    arrExecOrder[i].m_strPatientSex = objOneDataRowView["SEX_CHR"].ToString().Trim();//new code

                    //old code**************
                    //if (!objRow[i]["RECIPENO_INT"].ToString().Trim().Equals(""))
                    //    arrExecOrder[i].m_intRecipenNo = int.Parse(objRow[i]["RECIPENO_INT"].ToString().Trim()); //����
                    //if (!objRow[i]["RECIPENO2_INT"].ToString().Trim().Equals(""))
                    //    arrExecOrder[i].m_intRecipenNo2 = int.Parse(objRow[i]["RECIPENO2_INT"].ToString().Trim()); //����(�����ⲿ��ʾ)
                    //********************

                    //new code**********************
                    if (!objOneDataRowView["RECIPENO_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intRecipenNo = int.Parse(objOneDataRowView["RECIPENO_INT"].ToString().Trim()); //����
                    if (!objOneDataRowView["RECIPENO2_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intRecipenNo2 = int.Parse(objOneDataRowView["RECIPENO2_INT"].ToString().Trim()); //����(�����ⲿ��ʾ)
                    //****************************

                    //old code*****************************
                    //if (!objRow[i]["EXECUTETYPE_INT"].ToString().Trim().Equals(""))
                    //    arrExecOrder[i].m_intExecuteType = int.Parse(objRow[i]["EXECUTETYPE_INT"].ToString().Trim());//ҽ����ҽ����ʽ��
                    //**************************************

                    //new code********************
                    if (!objOneDataRowView["EXECUTETYPE_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intExecuteType = int.Parse(objOneDataRowView["EXECUTETYPE_INT"].ToString().Trim());//ҽ����ҽ����ʽ��
                    //********************************

                    //arrExecOrder[i].m_strOrderDicCateID = Convert.ToString(objRow[i]["ordercateid_chr"].ToString().Trim());//���ID old code
                    arrExecOrder[i].m_strOrderDicCateID = objOneDataRowView["ordercateid_chr"].ToString().Trim();//new code

                    clsT_aid_bih_ordercate_VO p_objItem = null;
                    if (m_htOrderCate.Contains(arrExecOrder[i].m_strOrderDicCateID))
                    {
                        p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[arrExecOrder[i].m_strOrderDicCateID];
                    }
                    if (p_objItem != null)
                    {
                        arrExecOrder[i].m_strOrderDicCateName = p_objItem.m_strVIEWNAME_VCHR;//�������

                    }

                    //arrExecOrder[i].m_strName = Convert.ToString(objRow[i]["NAME_VCHR"].ToString().Trim());//old code ��Ŀ����
                    arrExecOrder[i].m_strName = objOneDataRowView["NAME_VCHR"].ToString().Trim();//new code

                    //old code******************************
                    //if (!objRow[i]["Dosage_Dec"].ToString().Trim().Equals(""))
                    //    arrExecOrder[i].m_dmlDosage = decimal.Parse(objRow[i]["Dosage_Dec"].ToString().Trim()); ;//����
                    //*****************************************

                    //new code*************************
                    if (!objOneDataRowView["Dosage_Dec"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_dmlDosage = decimal.Parse(objOneDataRowView["Dosage_Dec"].ToString().Trim()); ;//����
                    //***************

                    //arrExecOrder[i].m_strDosageUnit = Convert.ToString(objRow[i]["dosageunit_chr"].ToString().Trim());//old code ������λ
                    arrExecOrder[i].m_strDosageUnit = objOneDataRowView["dosageunit_chr"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strExecFreqID = Convert.ToString(objRow[i]["EXECFREQID_CHR"].ToString().Trim());//old code
                    arrExecOrder[i].m_strExecFreqID = objOneDataRowView["EXECFREQID_CHR"].ToString().Trim();//new code

                    //arrExecOrder[i].m_strExecFreqName = Convert.ToString(objRow[i]["EXECFREQNAME_CHR"].ToString().Trim());//old code
                    arrExecOrder[i].m_strExecFreqName = objOneDataRowView["EXECFREQNAME_CHR"].ToString().Trim();//new code

                    if (!objOneDataRowView["GET_DEC"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_dmlGet = decimal.Parse(objOneDataRowView["GET_DEC"].ToString().Trim());


                    arrExecOrder[i].m_strGetunit = objOneDataRowView["getunit_chr"].ToString().Trim();
                    arrExecOrder[i].m_strEntrust = objOneDataRowView["ENTRUST_VCHR"].ToString().Trim();//����
                    arrExecOrder[i].m_strDOCTOR_VCHR = objOneDataRowView["DOCTOR_VCHR"].ToString().Trim();//����ҽ��
                    if (!objOneDataRowView["POSTDATE_DAT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_dtPostdate = Convert.ToDateTime(objOneDataRowView["POSTDATE_DAT"].ToString().Trim());
                    arrExecOrder[i].m_strASSESSORFOREXEC_CHR = objOneDataRowView["CONFIRMER_VCHR"].ToString().Trim();//�����-
                    arrExecOrder[i].m_strASSESSORFOREXEC_DAT = objOneDataRowView["CONFIRM_DAT"].ToString().Trim();//���ʱ��
                    arrExecOrder[i].m_strDosetypeID = objOneDataRowView["DOSETYPEID_CHR"].ToString().Trim();//�÷�ID
                    arrExecOrder[i].m_strDosetypeName = objOneDataRowView["DOSETYPENAME_CHR"].ToString().Trim();//�÷�����
                    arrExecOrder[i].m_strOrderID = objOneDataRowView["orderid_chr"].ToString().Trim();//ҽ������ˮ��
                    if (!objOneDataRowView["STATUS_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intStatus = int.Parse(objOneDataRowView["STATUS_INT"].ToString().Trim());//��ǰҽ��״̬
                    arrExecOrder[i].m_strCreatorID = objOneDataRowView["CREATORID_CHR"].ToString().Trim();//��ҽ����ҽ��ID
                    arrExecOrder[i].m_strCreator = objOneDataRowView["CREATOR_CHR"].ToString().Trim();//��ҽ����ҽ��

                    arrExecOrder[i].m_strOrderDicID = objOneDataRowView["ORDERDICID_CHR"].ToString().Trim();//������Ŀ��ˮ��
                    arrExecOrder[i].m_strParentID = objOneDataRowView["patientid_chr"].ToString().Trim();//����ID
                    arrExecOrder[i].m_strCREATEAREA_ID = objOneDataRowView["createareaid_chr"].ToString().Trim();//��������ID
                    arrExecOrder[i].m_strCURAREAID_CHR = objOneDataRowView["CURAREAID_CHR"].ToString().Trim();//��ҽ��ʱ�������ڲ���ID
                    //arrExecOrder[i].m_strCURAREAName = Convert.ToString(objRow[i]["CURAREAName"].ToString().Trim());//��ҽ��ʱ�������ڲ�������
                    if (!objOneDataRowView["OUTGETMEDDAYS_INT"].ToString().Trim().Equals(""))
                    {
                        arrExecOrder[i].m_intOUTGETMEDDAYS_INT = int.Parse(objOneDataRowView["OUTGETMEDDAYS_INT"].ToString().Trim());//��Ժ��ҩ����(��ִ������Ϊ3=��Ժ��ҩ})
                    }
                    arrExecOrder[i].m_strSAMPLEID_VCHR = objOneDataRowView["SAMPLEID_VCHR"].ToString().Trim();
                    arrExecOrder[i].m_strSAMPLEName_VCHR = objOneDataRowView["sample_type_desc_vchr"].ToString().Trim();
                    arrExecOrder[i].m_strPARTID_VCHR = objOneDataRowView["PARTID_VCHR"].ToString().Trim();
                    arrExecOrder[i].m_strPARTNAME_VCHR = objOneDataRowView["partname"].ToString().Trim();
                    //Ƥ������ֶ�
                    arrExecOrder[i].m_intISNEEDFEEL = int.Parse(objOneDataRowView["ISNEEDFEEL"].ToString().Trim());//�Ƿ���ҪƤ��
                    arrExecOrder[i].m_intFEEL_INT = int.Parse(objOneDataRowView["FEEL_INT"].ToString().Trim());//Ƥ�Խ��
                    arrExecOrder[i].m_strFEELRESULT_VCHR = objOneDataRowView["FEELRESULT_VCHR"].ToString().Trim();//Ƥ�Խ����ע
                    if (!objOneDataRowView["CHARGE_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intCHARGE_INT = int.Parse(objOneDataRowView["CHARGE_INT"].ToString().Trim());
                    if (!objOneDataRowView["ATTACHTIMES_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intATTACHTIMES_INT = int.Parse(objOneDataRowView["ATTACHTIMES_INT"].ToString().Trim());
                    arrExecOrder[i].m_strREMARK_VCHR = objOneDataRowView["REMARK_VCHR"].ToString().Trim();//ҽ��˵��
                    arrExecOrder[i].m_intTYPE_INT = int.Parse(objOneDataRowView["TYPE_INT"].ToString().Trim());//ҽ������(0-��ͨ,1-����ҽ��,2-ת��ҽ��,3-��Ժ(����),4-��Ժ(����))
                    arrExecOrder[i].m_dmlOneUse = decimal.Parse(objOneDataRowView["SINGLEAMOUNT_DEC"].ToString().Trim());//��һ�ε�����
                    arrExecOrder[i].m_strASSESSORFORSTOP_CHR = objOneDataRowView["ASSESSORFORSTOP_CHR"].ToString().Trim();//���ֹͣ��
                    DateTime m_dtDate = DateTime.MinValue;
                    DateTime.TryParse(objOneDataRowView["ASSESSORFORSTOP_DAT"].ToString().Trim(), out m_dtDate);
                    if (m_dtDate != null && m_dtDate != DateTime.MinValue)
                    {
                        arrExecOrder[i].m_strASSESSORFORSTOP_DAT = m_dtDate.ToString();//���ֹͣʱ��
                    }
                    //Ϊ��ǰҽ����¼�����˴��ű��� Ϊ�˰�����f8,f9��������ת��
                    if (m_blBedIdKey)
                    {
                        if (!m_arrBedIdList.Contains(arrExecOrder[i].m_strBedID))
                        {
                            m_arrBedIdList.Add(arrExecOrder[i].m_strBedID);
                        }
                    }
                    /*<===================================*/
                }
            }

            //                    ����
            //\
            //����
            //\���� (code_chr)
            //����-RECIPENO_INT
            //ҽ����ҽ����ʽ��-EXECUTETYPE_INT
            //���
            //��Ŀ����--NAME_VCHR
            //����--DOSAGE_DEC ������λ--DOSAGEUNIT_CHR
            //�÷�ID--DOSETYPEID_CHR ��ҩ��ʽDOSETYPENAME_CHR
            //Ƶ��--EXECFREQID_CHR
            //Ƶ������--EXECFREQNAME_CHR
            //����--GET_DEC
            //������λ--GETUNIT_CHR
            //����--ENTRUST_VCHR
            //����ҽ��--DOCTOR_VCHR
            //�ύʱ��--POSTDATE_DAT
            //�����--CONFIRMER_VCHR
            //���ʱ��--CONFIRM_DAT
        }

        #endregion

        #region ���˱�ת���ɲ�����Ϣ����
        /// <summary>
        /// ���˱�ת���ɲ�����Ϣ����
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="objPatient"></param>
        private void m_mthGetPatientInfoFromDateTable(DataView objRow, out clsBIHPatientInfo objPatient)
        {
            objPatient = null;
            if (objRow == null) return;

            objPatient = new clsBIHPatientInfo();
            objPatient.m_strRegisterID = clsConverter.ToString(objRow[0]["RegisterID_Chr"]).Trim();
            objPatient.m_strPatientID = clsConverter.ToString(objRow[0]["PatientID_Chr"]).Trim();
            objPatient.m_strInHospitalNo = clsConverter.ToString(objRow[0]["InPatientID_Chr"]).Trim();
            objPatient.m_dtInHospital = clsConverter.ToDateTime(objRow[0]["InPatient_Dat"]);
            objPatient.m_strDeptID = clsConverter.ToString(objRow[0]["DeptID_Chr"]).Trim();
            objPatient.m_strAreaID = clsConverter.ToString(objRow[0]["AreaID_Chr"]).Trim();

            objPatient.m_strAreaName = clsConverter.ToString(objRow[0]["AreaName"]).Trim();
            objPatient.m_strBedID = clsConverter.ToString(objRow[0]["BedID_Chr"]).Trim();
            objPatient.m_strBedName = clsConverter.ToString(objRow[0]["BedName"]).Trim();
            /** update by xzf (05-09-29) */
            //@ objPatient.m_strDiagnose=clsConverter.ToString(objRow["Diagnose_Vchr"]).Trim();
            objPatient.m_strDiagnose = clsConverter.ToString(objRow[0]["ICD10DIAGTEXT_VCHR"]).Trim();
            /* <<============================= */
            objPatient.m_intInTimes = clsConverter.ToInt(objRow[0]["InPatientCount_Int"]);
            objPatient.m_strPatientName = clsConverter.ToString(objRow[0]["Name_VChr"]).Trim();

            objPatient.m_strSex = clsConverter.ToString(objRow[0]["Sex_Chr"]).Trim();
            objPatient.m_dtBorn = clsConverter.ToDateTime(objRow[0]["Birth_Dat"]);
            objPatient.m_strPayTypeID = clsConverter.ToString(objRow[0]["PayTypeID_Chr"]).Trim();
            objPatient.m_strPayTypeName = clsConverter.ToString(objRow[0]["PayTypeName_VChr"]).Trim();
            objPatient.m_strInpatientState = clsConverter.ToString(objRow[0]["state"]).Trim();
            objPatient.m_strMzdiagnose_vchr = clsConverter.ToString(objRow[0]["mzdiagnose_vchr"]).Trim();
            objPatient.m_strDiagnose_vchr = clsConverter.ToString(objRow[0]["diagnose_vchr"]).Trim();
            if (objRow[0]["limitrate_mny"] != System.DBNull.Value)
            {
                objPatient.m_dblLIMITRATE_MNY = double.Parse(objRow[0]["limitrate_mny"].ToString());
            }
            try
            {
                TimeSpan span1 = clsConverter.ToDateTime(objRow[0]["today"].ToString().Trim()) - objPatient.m_dtBorn;
                objPatient.m_intAge = span1.Days / 365;
            }
            catch
            {
            }
            try
            {
                objPatient.m_strREMARKNAME_VCHR = objRow[0]["REMARKNAME_VCHR"].ToString().Trim();
            }
            catch
            {
            }
            try
            {
                objPatient.m_strDES_VCHR = objRow[0]["DES_VCHR"].ToString().Trim();
            }
            catch
            {
            }
            //���˵ĺϼƽ�Ԥ����
            //if (m_dtChargeMoney.Rows.Count > 0)
            //{
            //    DataView myDataView = new DataView(m_dtChargeMoney);
            //    myDataView.RowFilter = "registerid_chr='" + objPatient.m_strRegisterID + "'";
            //    if (myDataView.Count <= 0)
            //    {
            //        return;
            //    }
            //    decimal PreMoney = 0, PreUseMoney = 0, NotUsePreMoney = 0, ClearMoney = 0, WaitMoney = 0, WaitClearMoney = 0, VerticalMoney=0;

            //    if (!myDataView[0]["clearmoney"].ToString().Trim().Equals(""))
            //    {
            //        ClearMoney = decimal.Parse(myDataView[0]["clearmoney"].ToString().Trim());
            //    }
            //    if (!myDataView[0]["preusemoney"].ToString().Trim().Equals(""))
            //    {
            //        PreUseMoney = decimal.Parse(myDataView[0]["preusemoney"].ToString().Trim());
            //    }

            //    if (!myDataView[0]["NotUsePreMoney"].ToString().Trim().Equals(""))
            //    {
            //        NotUsePreMoney = decimal.Parse(myDataView[0]["NotUsePreMoney"].ToString().Trim());
            //    }
            //    if (!myDataView[0]["WaitMoney"].ToString().Trim().Equals(""))
            //    {
            //        WaitMoney = decimal.Parse(myDataView[0]["WaitMoney"].ToString().Trim());
            //    }
            //    if (!myDataView[0]["WaitClearMoney"].ToString().Trim().Equals(""))
            //    {
            //        WaitClearMoney = decimal.Parse(myDataView[0]["WaitClearMoney"].ToString().Trim());
            //    }
            //    if (!myDataView[0]["VerticalMoney"].ToString().Trim().Equals(""))
            //    {
            //        VerticalMoney = decimal.Parse(myDataView[0]["VerticalMoney"].ToString().Trim());
            //    }

            //    objPatient.m_decPreMoney = NotUsePreMoney;
            //    objPatient.m_decPreUseMoney = PreUseMoney;
            //    objPatient.m_decClearMoney = ClearMoney;
            //    objPatient.m_decVerticalMoney = VerticalMoney;
            //    objPatient.m_decPrePayMoney = NotUsePreMoney - WaitMoney - WaitClearMoney;

            //}
        }
        #endregion

        #region ���ñ�ת��Ϊ������ϸ����
        /// <summary>
        /// ���ñ�ת��Ϊ������ϸ����
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="m_arrObjItem"></param>
        private void m_mthGetChargeListFromDateTable(DataView objRow, out clsChargeForDisplay[] m_arrObjItem)
        {


            m_arrObjItem = new clsChargeForDisplay[objRow.Count];
            decimal UNITPRICE_DEC = 0, ItemPrice_Mny = 0, PackQty_Dec = 0, decItemTradePrice = 0;
            int IPCHARGEFLG_INT = 0;
            for (int i = 0; i < objRow.Count; i++)
            {
                m_arrObjItem[i] = new clsChargeForDisplay();
                m_arrObjItem[i].strOrderID = clsConverter.ToString(objRow[i]["orderid_chr"]).Trim();
                m_arrObjItem[i].m_strChargeID = clsConverter.ToString(objRow[i]["CHARGEITEMID_CHR"]).Trim();
                //�շ���Ŀ����
                m_arrObjItem[i].m_strName = clsConverter.ToString(objRow[i]["CHARGEITEMNAME_CHR"]).Trim();
                double dblNum = 0;
                //if (objMedicineItemArr[i1].m_objChargeItem.m_strITEMID_CHR.Trim() == objMedicineItemArr[i1].m_strChiefItemID.Trim())//�Ƿ����շ���Ŀ
                //{
                //    dblNum = p_dblDraw;
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 2;
                //}
                //else
                //{
                //    dblNum = System.Convert.ToDouble(m_dmlGetChargeNotMainItem(objRecipeFreq, objMedicineItemArr[i1]));
                //    //�շ����	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
                //    m_arrObjItem[i].m_intType = 1;
                //}
                //����
                //if (!objRow[i]["UNITPRICE_DEC"].ToString().Trim().Equals(""))
                //{
                //    m_arrObjItem[i].m_dblPrice = double.Parse(clsConverter.ToString(objRow[i]["UNITPRICE_DEC"]).Trim());
                //}
                //ȡ�շ���Ŀ�еĵ���
                /*decode(b.IPCHARGEFLG_INT,1,Round(B.ItemPrice_Mny/B.PackQty_Dec,4),0,B.ItemPrice_Mny,Round(B.ItemPrice_Mny/B.PackQty_Dec,4)) ItemPriceA*/
                UNITPRICE_DEC = 0;
                ItemPrice_Mny = 0;
                PackQty_Dec = 0;
                int.TryParse(objRow[i]["IPCHARGEFLG_INT"].ToString(), out IPCHARGEFLG_INT);
                decimal.TryParse(objRow[i]["ItemPrice_Mny"].ToString(), out ItemPrice_Mny);
                decimal.TryParse(objRow[i]["PackQty_Dec"].ToString(), out PackQty_Dec);
                if (objRow.Table != null && objRow.Table.Columns.Contains("tradeprice_mny"))
                    decimal.TryParse(objRow[i]["tradeprice_mny"].ToString(), out decItemTradePrice);// ��������
                if (PackQty_Dec <= 0)
                {
                    PackQty_Dec = 1;
                }
                switch (IPCHARGEFLG_INT)
                {
                    case 1:
                        UNITPRICE_DEC = decimal.Round(ItemPrice_Mny / PackQty_Dec, 4);
                        decItemTradePrice = decimal.Round(decItemTradePrice / PackQty_Dec, 4);
                        break;
                    case 0:
                        UNITPRICE_DEC = decimal.Round(ItemPrice_Mny, 4);
                        break;
                    default:
                        UNITPRICE_DEC = decimal.Round(ItemPrice_Mny / PackQty_Dec, 4);
                        decItemTradePrice = decimal.Round(decItemTradePrice / PackQty_Dec, 4);
                        break;

                }
                //orderChargeVo.m_decUnitprice_dec = clsConverter.ToDecimal(row["UNITPRICE_DEC"]);
                m_arrObjItem[i].m_dblPrice = double.Parse(UNITPRICE_DEC.ToString());
                /*<===============*/

                if (!objRow[i]["AMOUNT_DEC"].ToString().Trim().Equals(""))
                {
                    dblNum = double.Parse(clsConverter.ToString(objRow[i]["AMOUNT_DEC"]).Trim());
                }
                /*<---------------------------------*/
                //����
                m_arrObjItem[i].m_dblDrawAmount = dblNum;

                //�ϼƽ��
                m_arrObjItem[i].m_dblMoney = m_arrObjItem[i].m_dblPrice * dblNum;

                m_arrObjItem[i].m_dblTradePrice = double.Parse(decItemTradePrice.ToString());

                //com.digitalwave.Utility.clsLogText jianjunlog = new com.digitalwave.Utility.clsLogText();
                //jianjunlog.LogError("medicnetype_int : " + objRow[i]["medicnetype_int"].ToString());
                //jianjunlog.LogError("m_dblTradePrice :" + m_arrObjItem[i].m_dblTradePrice.ToString());
                //jianjunlog.LogError("m_dblPrice = " + m_arrObjItem[i].m_dblPrice.ToString());
                if (this.m_blnIsDiffMed(objRow[i]["medicinetypeid_chr"].ToString()))//ҩƷ������
                    // ���������,ȡ����
                    m_arrObjItem[i].m_dblDiffCostMoney = Math.Round((m_arrObjItem[i].m_dblTradePrice - m_arrObjItem[i].m_dblPrice) * dblNum, 4);
                //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                if (!objRow[i]["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intCONTINUEUSETYPE_INT = int.Parse(objRow[i]["CONTINUEUSETYPE_INT"].ToString().Trim());
                }

                //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                // m_arrObjItem[i].m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                //�Ƿ�ȱҩ
                // m_arrObjItem[i].m_strNoqtyFLag = objMedicineItemArr[i1].m_strNoqtyFLag;
                // ���Ͽ�������
                m_arrObjItem[i].m_strClacarea_chr = clsConverter.ToString(objRow[i]["CLACAREA_CHR"]).Trim();
                m_arrObjItem[i].m_strClacareaName_chr = clsConverter.ToString(objRow[i]["deptname_vchr"]).Trim();
                //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                m_arrObjItem[i].m_strSeq_int = clsConverter.ToString(objRow[i]["SEQ_INT"]).Trim(); ;
                m_arrObjItem[i].m_strYBClass = clsConverter.ToString(objRow[i]["INSURACEDESC_VCHR"]).Trim();
                m_arrObjItem[i].m_strUNIT_VCHR = clsConverter.ToString(objRow[i]["UNIT_VCHR"]).Trim();
                //�շ�����Դ�� 0-��������Ŀ��1-������Ŀ,2���������÷���3���Զ����¿�
                if (!objRow[i]["FLAG_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intType = clsConverter.ToInt(objRow[i]["FLAG_INT"].ToString().Trim());
                // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                //objItem.m_objORDERCHARGEDEPT_VO = objMedicineItemArr[i1].m_objORDERCHARGEDEPT_VO;
                if (!objRow[i]["ITEMSRCTYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intITEMSRCTYPE_INT = int.Parse(objRow[i]["ITEMSRCTYPE_INT"].ToString().Trim());
                }
                if (!objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intIPNOQTYFLAG_INT = int.Parse(objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim());
                }
                m_arrObjItem[i].m_strSPEC_VCHR = clsConverter.ToString(objRow[i]["ITEMSPEC_VCHR"].ToString().Trim());
            }
        }
        #endregion

        #region ѡ����¼�����
        /// <summary>
        /// ѡ����¼�����
        /// </summary>
        internal void SelectAll()
        {
            if (m_objViewer.m_chkSelectAll.Checked == true)
            {
                for (int i = 0; i < m_objViewer.m_dtvOrderList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";
                }
            }
            else
            {
                for (int i = 0; i < m_objViewer.m_dtvOrderList.Rows.Count; i++)
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                }
            }
        }
        #endregion

        #region ��˼�������ť����
        /// <summary>
        /// ��˼�������ť����
        /// </summary>
        public void ControlTheButton()
        {
            this.m_objViewer.m_cmdChargeAdd.Enabled = true;
            this.m_objViewer.m_cmdChargeDele.Enabled = true;
            this.m_objViewer.m_cmdChargeModify.Enabled = true;
            this.m_objViewer.m_cmdEditFeel.Enabled = true;
            this.m_objViewer.m_cmdBack.Enabled = true;
            this.m_objViewer.m_cmdPrintFeel.Enabled = true;
            this.m_objViewer.m_cmdRedraw.Enabled = true;
            this.m_objViewer.m_cmdToCommit.Enabled = true;
            //���ύ����Ŀ
            int m_intCount1 = 0;
            //���ύ����Ŀ
            int m_intCount2 = 0;
            //Ƥ��ѡ��
            int m_intCount3 = 0;
            //Ҫ���ͣĿ����Ŀ
            int m_intNotStop = 0;
            //�����ͣĿ����Ŀ
            int m_intYetStop = 0;
            if (this.m_objViewer.m_dtvOrderList.Rows.Count > 0)
            {
                for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                {
                    if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value == null)
                    {
                        return;
                    }
                    if (this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim().Equals("1"))
                    {
                        clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                        if (order.m_intStatus == 1)
                        {
                            m_intCount1++;
                        }
                        else if (order.m_intStatus == 5)
                        {
                            m_intCount2++;
                        }
                        else if (order.m_intStatus == 3)
                        {
                            m_intNotStop++;
                        }
                        else if (order.m_intStatus == 6)
                        {
                            m_intYetStop++;
                        }
                    }
                }

            }
            if (this.m_objViewer.m_dtvOrderList.SelectedRows.Count > 0)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.SelectedRows[0].Tag;
                if (order.m_intStatus == 1 && order.m_intISNEEDFEEL == 1)
                {
                    m_intCount3++;
                }
            }
            if (m_intCount1 > 0 && m_intCount2 > 0)
            {
                this.m_objViewer.m_cmdToCommit.Enabled = false;
                this.m_objViewer.m_cmdRedraw.Enabled = false;

            }
            else if (m_intCount1 > 0 && m_intCount2 == 0)
            {

                this.m_objViewer.m_cmdRedraw.Enabled = false;
            }
            else if (m_intCount1 == 0 && m_intCount2 > 0)
            {
                this.m_objViewer.m_cmdToCommit.Enabled = false;

            }
            else
            {
                this.m_objViewer.m_cmdToCommit.Enabled = false;
                this.m_objViewer.m_cmdRedraw.Enabled = false;

            }
            if (m_intCount3 > 0)
            {

            }
            else
            {
                this.m_objViewer.m_cmdEditFeel.Enabled = false;
            }
            if (m_intNotStop > 0)
            {
                this.m_objViewer.m_cmdToCommit.Enabled = true;
            }
            //if(m_intNotStop>0)
            //{
            //    this.m_objViewer.m_cmdChargeAdd.Enabled=false;
            //    this.m_objViewer.m_cmdChargeDele.Enabled=false;
            //    this.m_objViewer.m_cmdChargeModify.Enabled=false;
            //    this.m_objViewer.m_cmdEditFeel.Enabled=false;
            //    this.m_objViewer.m_cmdBack.Enabled=false;
            //    this.m_objViewer.m_cmdPrintFeel.Enabled=false;
            //    this.m_objViewer.m_cmdRedraw.Enabled=false;

            //}
            //if(m_intYetStop>0)
            //{
            //    this.m_objViewer.m_cmdChargeAdd.Enabled=false;
            //    this.m_objViewer.m_cmdChargeDele.Enabled=false;
            //    this.m_objViewer.m_cmdChargeModify.Enabled=false;
            //    this.m_objViewer.m_cmdEditFeel.Enabled=false;
            //    this.m_objViewer.m_cmdBack.Enabled=false;
            //    this.m_objViewer.m_cmdPrintFeel.Enabled=false;
            //    this.m_objViewer.m_cmdRedraw.Enabled=false;
            //    this.m_objViewer.m_cmdToCommit.Enabled=false;
            //}
            if (this.m_objViewer.m_rdoNOT.Checked == true)
            {
                this.m_objViewer.m_plChargeControl.Enabled = true;
            }
            else
            {
                this.m_objViewer.m_plChargeControl.Enabled = false;
            }
            ControlTheBackButton();
        }
        #endregion

        #region �˻ذ�ť�Ŀ���
        /// <summary>
        /// �˻ذ�ť�Ŀ���
        /// </summary>
        private void ControlTheBackButton()
        {


            if (this.m_objViewer.m_dtvOrderList.SelectedRows.Count > 0)
            {
                if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.SelectedRows[0].Tag).m_intStatus == 1)
                {
                    this.m_objViewer.m_cmdBack.Enabled = true;
                }
                else
                {
                    this.m_objViewer.m_cmdBack.Enabled = false;
                }
            }

        }
        #endregion

        /// <summary>
        /// ��ʼ������
        /// </summary>
        internal void IniTheForm()
        {
            string preTestMed1day = clsPublic.m_strGetSysparm("9014");
            int p1day = 0;
            int.TryParse(preTestMed1day, out p1day);
            this.isUsePretestMed = p1day == 1 ? true : false;
            this.isUseChildPrice = (new clsDcl_ExecuteOrder()).IsUseChildPrice();

            this.m_objViewer.m_txtSameCharge.Text = "";
            this.m_objViewer.m_chkSelectAll.Checked = true;
            this.m_objViewer.m_txtArea.Text = this.m_objViewer.LoginInfo.m_strInpatientAreaName;
            this.m_objViewer.m_txtArea.Tag = this.m_objViewer.LoginInfo.m_strInpatientAreaID;
            this.m_objViewer.m_txtArea.Focus();
            this.m_objViewer.m_cboCode.SelectedIndex = 0;
            this.m_objViewer.Cursor = Cursors.WaitCursor;
            LoadTheDate();
            this.m_objViewer.Cursor = Cursors.Default;
            this.m_strDiffMedicineType = clsPublic.m_strGetSysparm("9003");
        }

        #region �ж��Ƿ�������ҩƷ����

        /// <summary>
        /// �ж��Ƿ�������ҩƷ
        /// </summary>
        /// <param name="p_strMedicineType">ҩƷ����Id</param>
        /// <returns></returns>
        private bool m_blnIsDiffMed(string p_strMedicineType)
        {
            bool blnIsDiff = false;
            string[] strDiffTypeIds = this.m_strDiffMedicineType.Split(new char[] { ';' });
            if (strDiffTypeIds == null || strDiffTypeIds.Length == 0)
                return blnIsDiff;
            foreach (string strTypeId in strDiffTypeIds)
            {
                if (string.Compare(strTypeId, p_strMedicineType) == 0)
                {
                    blnIsDiff = true;
                    break;
                }
            }
            return blnIsDiff;
        }
        #endregion

        #region ���ö�ʱ��
        /// <summary>
        /// ���ö�ʱ��
        /// </summary>
        /// <param name="Status"></param>
        public void m_mthReSetTimer(bool Status)
        {
            if (Status)
            {
                this.m_objViewer.m_timerMessage.Enabled = true;
                if (m_intMessageOpenTime == 0)
                {
                    m_intMessageOpenTime = 1;
                }
                this.m_objViewer.m_timerMessage.Interval = m_intMessageOpenTime * 1000;
            }
            else
            {
                this.m_objViewer.m_timerMessage.Enabled = false;
            }
        }
        #endregion

        internal void ChangeTheSelectState(int rowNum)
        {
            if (this.m_objViewer.m_dtvOrderList.Rows.Count > 0 && rowNum >= 0)
            {
                if (this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_dtvselectCheck"].Value.ToString().Trim().Equals("0"))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_dtvselectCheck"].Value = "1";
                }
                else if (this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_dtvselectCheck"].Value.ToString().Trim().Equals("1"))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_dtvselectCheck"].Value = "0";
                }

                ControlTheButton();
            }
            //ͬ������
            TheSamerecipeno(rowNum);
        }

        internal void SetTheNeelSelect(ArrayList m_arrNeel)
        {
            //ҪƤ�ԵĲ����б�
            ArrayList m_arrRegister = new ArrayList();
            //ҪƤ�Ե�ҽ������
            ArrayList m_arrRecipenNo = new ArrayList();
            for (int i = 0; i < m_arrNeel.Count; i++)
            {
                if (((clsBIHCanExecOrder)m_arrNeel[i]).m_intISNEEDFEEL == 1)
                {
                    m_arrRegister.Add(((clsBIHCanExecOrder)m_arrNeel[i]).m_strRegisterID);
                    m_arrRecipenNo.Add(((clsBIHCanExecOrder)m_arrNeel[i]).m_intRecipenNo);
                }
            }
            //����Ƥ�Ե�ɾ��
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                if (m_arrRecipenNo.Contains(order.m_intRecipenNo) && m_arrRegister.Contains(order.m_strRegisterID))
                {
                }
                else
                {
                    this.m_objViewer.m_dtvOrderList.Rows.Remove(this.m_objViewer.m_dtvOrderList.Rows[i]);
                    i--;
                }
            }
        }

        #region ͬ������
        /// <summary>
        /// ͬ������
        /// </summary>
        /// <param name="rowNum"></param>
        private void TheSamerecipeno(int rowNum)
        {

            if (this.m_objViewer.m_dtvOrderList.Rows.Count > 0 && rowNum >= 0)
            {

                string m_strValue = this.m_objViewer.m_dtvOrderList.Rows[rowNum].Cells["m_dtvselectCheck"].Value.ToString().Trim();
                int m_intRecipenNo = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[rowNum].Tag).m_intRecipenNo;
                string m_strRegisterID = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[rowNum].Tag).m_strRegisterID;
                decimal m_decSum = 0;
                for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                {
                    if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intRecipenNo == m_intRecipenNo && ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID.Equals(m_strRegisterID))
                    {
                        this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = m_strValue;

                    }
                }


            }
        }
        #endregion

        #region ͬ������
        /// <summary>
        /// ͬ������
        /// </summary>
        /// <param name="rowNum"></param>
        private void TheSamerecipeno(clsBIHCanExecOrder Order, string m_strValue)
        {

            int m_intRecipenNo = Order.m_intRecipenNo;
            string m_strRegisterID = Order.m_strRegisterID;
            decimal m_decSum = 0;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intRecipenNo == m_intRecipenNo && ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID.Equals(m_strRegisterID))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = m_strValue;

                }
            }

        }
        #endregion

        internal void UpdateBihOrderConfirmer()
        {
            long lngRes = 0;
            List<EntityConfirmOrder> lstOrder = null;
            List<string> m_strStopORDERID_Arr = null;
            string strLeaveHospital = "";
            lstOrder = GetTheSelectItem();
            m_strStopORDERID_Arr = GetTheStopSelectItem(ref strLeaveHospital);
            if (lstOrder.Count == 0 && m_strStopORDERID_Arr.Count == 0)
            {
                MessageBox.Show("û�п���˵�ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Collections.Generic.List<string> m_glstNonExecutableOrder = null;
            System.Collections.Generic.List<string> m_glstSelectORDERID = new System.Collections.Generic.List<string>();

            if (lstOrder.Count > 0)
            {
                foreach (EntityConfirmOrder item in lstOrder)
                {
                    m_glstSelectORDERID.Add(item.OrderId);
                }
            }
            if (m_strStopORDERID_Arr.Count > 0)
            {
                m_glstSelectORDERID.AddRange(m_strStopORDERID_Arr.ToArray());
            }
            if (!ConfirmCurrentOrder(m_glstSelectORDERID, out m_glstNonExecutableOrder))
            {
                MessageBox.Show("��ǰ��ҽ��״̬�ѱ仯,��ȷ�����ٳ���!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DelTheOrderFromDTView(m_glstNonExecutableOrder);
                m_mthRefreshSameReqNoColor();
                return;
            }
            if (m_blNeedComfirm == true)
            {
                string empid = "";
                string empname = "";

                DialogResult dlg = clsPublic.m_dlgConfirm(out empid, out empname);
                if (dlg == DialogResult.Yes)
                {
                    this.m_objViewer.Cursor = Cursors.WaitCursor;

                    if (this.m_objViewer.m_rdoNOT.Checked)
                    {
                        lngRes = m_objManage.m_lngUpdateBihOrderConfirmer(lstOrder, empid, empname);
                        lngRes = m_objManage.m_lngStopBihOrderConfirmer(m_strStopORDERID_Arr, empid, empname);
                        if (strLeaveHospital != "")
                        {
                            lngRes = m_objManage.m_lngUpdateLeaveConfiemer(strLeaveHospital, empid, empname);
                        }
                    }
                    m_glstNonExecutableOrder = null;
                    if (!ConfirmCurrentOrder(m_glstSelectORDERID, out m_glstNonExecutableOrder))
                    {
                        DelTheOrderFromDTView(m_glstNonExecutableOrder);
                        m_mthRefreshSameReqNoColor();
                    }
                    this.m_objViewer.Cursor = Cursors.Default;
                    MessageBox.Show("�˶Գɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #region
                //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
                //if (comfirmBox1.ShowDialog() == DialogResult.OK)
                //{
                //    this.m_objViewer.Cursor = Cursors.WaitCursor;
                //    //if (this.m_objViewer.m_rdoNOTStop.Checked == true)
                //    //{
                //    //    if (MessageBox.Show("���ֹͣ�󽫲��ܽ��г���������Ҫ������!", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    //    {
                //    //        lngRes = m_objManage.m_lngStopBihOrderConfirmer(m_strORDERID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
                //    //    }

                //    //}
                //    //else 
                //    if (this.m_objViewer.m_rdoNOT.Checked)
                //    {
                //        lngRes = m_objManage.m_lngUpdateBihOrderConfirmer(m_strORDERID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
                //        lngRes = m_objManage.m_lngStopBihOrderConfirmer(m_strStopORDERID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);

                //    }

                //    m_arrCanNoOrder = null;
                //    if (!ConfirmCurrentOrder(m_SelectORDERID_Arr, out m_arrCanNoOrder))
                //    {
                //        DelTheOrderFromDTView(m_arrCanNoOrder);
                //        m_mthRefreshSameReqNoColor();

                //    }
                //    MessageBox.Show("�˶Գɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //LoadTheDate();



                //    this.m_objViewer.Cursor = Cursors.Default;
                //}
                #endregion
            }
            else
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                if (this.m_objViewer.m_rdoNOT.Checked)
                {
                    lngRes = m_objManage.m_lngUpdateBihOrderConfirmer(lstOrder, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                    lngRes = m_objManage.m_lngStopBihOrderConfirmer(m_strStopORDERID_Arr, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                    if (strLeaveHospital != "")
                    {
                        lngRes = m_objManage.m_lngUpdateLeaveConfiemer(strLeaveHospital, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                    }
                }
                m_glstNonExecutableOrder = null;
                if (!ConfirmCurrentOrder(m_glstSelectORDERID, out m_glstNonExecutableOrder))
                {
                    DelTheOrderFromDTView(m_glstNonExecutableOrder);
                    m_mthRefreshSameReqNoColor();
                }
                MessageBox.Show("�˶Գɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.Cursor = Cursors.Default;
            }
            if (this.m_objViewer.m_dtvOrderList.RowCount == 0)
            {
                this.m_objViewer.m_dtvChangeList.Rows.Clear();
            }
        }

        internal bool ConfirmCurrentOrder(System.Collections.Generic.List<string> m_glstORDERID, out System.Collections.Generic.List<string> m_glstNonExcutableOrder)
        {
            string m_strStatus1 = "", m_strStatus2 = "";
            if (this.m_objViewer.m_rdoNOT.Checked)
            {
                m_strStatus1 = "1";
                m_strStatus2 = "3";
            }
            else
            {
                m_strStatus1 = "5";
                m_strStatus2 = "6";
            }
            DataTable m_dtOrder = null;
            //ԭ���Ĵ���m_arrCanNoOrder = new ArrayList();

            ArrayList strOrderIDArr = new ArrayList();
            for (int i1 = 0; i1 < m_glstORDERID.Count; i1++)
            {
                strOrderIDArr.Add(m_glstORDERID[i1]);
            }
            string[] m_arrorder = (string[])strOrderIDArr.ToArray(typeof(string));

            m_glstNonExcutableOrder = new System.Collections.Generic.List<string>(m_glstORDERID.Count);
            long lngRef = m_objManage.m_lngConfirmCurrentOrder(m_arrorder, out m_dtOrder);
            if (lngRef > 0 && m_dtOrder != null)
            {
                string orderid_chr = "", status_int = "";
                DateTime executedate_dat, today;
                bool m_blCan = false;
                int intOrderRowsCount = m_dtOrder.Rows.Count;
                System.Data.DataRow objRow = null;

                for (int i = 0; i < intOrderRowsCount; i++)
                {
                    m_blCan = false;
                    objRow = m_dtOrder.Rows[i];
                    orderid_chr = objRow["orderid_chr"].ToString().Trim();
                    status_int = objRow["status_int"].ToString().Trim();
                    DateTime.TryParse(objRow["executedate_dat"].ToString().Trim(), out executedate_dat);
                    DateTime.TryParse(objRow["sysdate"].ToString().Trim(), out today);
                    if (status_int.Equals(m_strStatus1) || status_int.Equals(m_strStatus2))
                    {
                        m_blCan = true;
                    }
                    else
                    {
                        m_blCan = false;
                    }

                    if (!m_blCan)
                    {
                        //ԭ���Ĵ���m_arrCanNoOrder.Add(orderid_chr);
                        m_glstNonExcutableOrder.Add(orderid_chr);
                    }
                }

            }
            if (m_glstNonExcutableOrder.Count > 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// ������ˮ��ɾ����
        /// </summary>
        /// <param name="m_arrCanNoOrder"></param>
        //ԭ���Ĵ���private void DelTheOrderFromDTView(ArrayList m_arrCanNoOrder)
        private void DelTheOrderFromDTView(System.Collections.Generic.List<string> m_glstNonExecutableOrder)
        {
            int intOrderListRowsCount = this.m_objViewer.m_dtvOrderList.Rows.Count;
            DataGridViewRow row1 = null;
            DataGridViewRow row2 = null;

            //for (int i = 0; i < intOrderListRowsCount; i++)//����ģ�������ѭ����DataGridView������Remove�Ĳ�����Countֵ�Ǹı�ġ�
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                row1 = this.m_objViewer.m_dtvOrderList.Rows[i];
                if (m_glstNonExecutableOrder.Contains(((clsBIHCanExecOrder)row1.Tag).m_strOrderID))
                {
                    if ((row1.Cells["dtv_bedcode"].Value != null && !row1.Cells["dtv_bedcode"].Value.ToString().Equals("")) || (row1.Cells["m_dtvLastName"].Value != null && !row1.Cells["m_dtvLastName"].Value.ToString().Equals("")))
                    {
                        if (i < this.m_objViewer.m_dtvOrderList.Rows.Count - 1)
                        {
                            row2 = this.m_objViewer.m_dtvOrderList.Rows[i + 1];
                            if (((clsBIHCanExecOrder)row1.Tag).m_strRegisterID.Equals(((clsBIHCanExecOrder)row2.Tag).m_strRegisterID))
                            {
                                row2.Cells["dtv_bedcode"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strBedName;
                                row2.Cells["m_dtvLastName"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strPatientName;
                                //row2.Cells["dtv_DEPTNAME_VCHR"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strCURAREAName;
                            }
                        }
                    }

                    this.m_objViewer.m_dtvOrderList.Rows.Remove(this.m_objViewer.m_dtvOrderList.Rows[i]);
                    i--;
                }
            }
            SelectAll();
            ControlTheButton();
        }

        /// <summary>
        /// �õ��ύ״̬��ҽ��
        /// </summary>
        /// <returns></returns>
        private List<EntityConfirmOrder> GetTheSelectItem()
        {
            string orderDicId = string.Empty;
            string orderTypeName = string.Empty;
            EntityConfirmOrder vo = null;
            List<EntityConfirmOrder> lstOrder = new List<EntityConfirmOrder>();
            //clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    if (((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intStatus == 1)
                    {
                        vo = new EntityConfirmOrder();
                        orderDicId = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderDicID;
                        vo.OrderId = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID.Trim();
                        vo.PretestDays = Convert.ToInt32(m_objViewer.m_dtvOrderList.Rows[i].Cells["pretestdays"].Value);
                        orderTypeName = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strExecuteTypeName;
                        if (vo.PretestDays > 0)
                        {
                            if (orderTypeName.Contains("����") == false)
                            {
                                MessageBox.Show("Ԥ��ҩֻ����:����ҽ��");
                                lstOrder.Clear();
                                return lstOrder;
                            }
                            if ((Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_dtPostdate.ToShortDateString())).Days > 0)
                            {
                                MessageBox.Show("Ԥ��ҩֻ����:�����¿�ҽ��");
                                lstOrder.Clear();
                                return lstOrder;
                            }
                            if ((new weCare.Proxy.ProxyIP()).Service.IsMedInjection(orderDicId, 1) == false)
                            {
                                MessageBox.Show("Ԥ��ҩֻ����:���ҩƷҽ��");
                                lstOrder.Clear();
                                return lstOrder;
                            }
                        }
                        lstOrder.Add(vo);
                    }
                }
            }
            //svc = null;
            return lstOrder;
        }

        /// <summary>
        /// �õ������ֹͣ״̬��ҽ��
        /// </summary>
        /// <returns></returns>
        private List<string> GetTheStopSelectItem(ref string strLeaveHospital)
        {
            List<string> orderArr = new List<string>();
            strLeaveHospital = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    clsBIHCanExecOrder objTmp = this.m_objViewer.m_dtvOrderList.Rows[i].Tag as clsBIHCanExecOrder;
                    if (objTmp.m_intStatus != 1)
                    {
                        orderArr.Add(((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID.Trim());

                        if (objTmp.m_intTYPE_INT == 3 || objTmp.m_intTYPE_INT == 4)
                        {
                            strLeaveHospital = objTmp.m_strOrderID;
                        }
                    }
                }
            }
            return orderArr;
        }

        /// <summary>
        /// �õ�������״̬��ҽ��
        /// </summary>
        /// <returns></returns>
        private List<string> GetTheRedrawSelectItem()
        {
            List<string> orderArr = new List<string>();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    if (((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intStatus == 5)
                    {
                        orderArr.Add(((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID.Trim());
                    }
                }
            }

            return orderArr;
        }

        /// <summary>
        /// �����ǰѡ�е������п��Գ�����˵����ΪTRUE,����ΪFALSE
        /// </summary>
        /// <returns></returns>
        internal bool GetTheRedrawSelectItemCount()
        {
            bool m_blRedraw = false;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    if (((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intStatus == 5)
                    {
                        m_blRedraw = true;
                        break;

                    }
                }
            }
            return m_blRedraw;
        }

        /// <summary>
        /// �����ǰѡ�е������п�����˵����ΪTRUE,����ΪFALSE
        /// </summary>
        /// <returns></returns>
        internal bool GetTheCommitSelectItemCount()
        {
            bool m_blRedraw = false;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    m_blRedraw = true;
                    break;
                }
            }
            return m_blRedraw;
        }

        internal void UpdateBihOrderRedraw()
        {
            List<string> m_strORDERID_Arr = null;
            m_strORDERID_Arr = GetTheRedrawSelectItem();
            if (m_strORDERID_Arr.Count == 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            //ԭ���Ĵ���ArrayList m_arrCanNoOrder = null;
            //ԭ���Ĵ���ArrayList m_SelectORDERID_Arr = new ArrayList();
            System.Collections.Generic.List<string> m_glstNonExecutableOrder = null;//״̬�����˱仯,������ִ�е�ҽ��
            System.Collections.Generic.List<string> m_glstSelectOrder = new System.Collections.Generic.List<string>();//���п�����Ҫִ�е�ҽ��

            if (m_strORDERID_Arr.Count > 0)
            {
                //ԭ���Ĵ���m_SelectORDERID_Arr.AddRange((string[])m_strORDERID_Arr.ToArray(typeof(string)));
                m_glstSelectOrder.AddRange(m_strORDERID_Arr.ToArray());
            }

            //m_strORDERID_Arr.ToArray(
            //ԭ���Ĵ���if (!ConfirmCurrentOrder(m_SelectORDERID_Arr, out m_arrCanNoOrder))


            if (!ConfirmCurrentOrder(m_glstSelectOrder, out m_glstNonExecutableOrder))
            {
                MessageBox.Show("��ǰ��ҽ��״̬�ѱ仯,��ȷ�����ٳ���!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DelTheOrderFromDTView(m_glstNonExecutableOrder);
                m_mthRefreshSameReqNoColor();
                return;
            }
            if (m_blNeedComfirm == true)
            {
                string empid = "";
                string empname = "";

                DialogResult dlg = clsPublic.m_dlgConfirm(out empid, out empname);
                if (dlg == DialogResult.Yes)
                {
                    long lngRes = m_objManage.m_lngUpdateBihOrderRedraw(m_strORDERID_Arr, empid, empname);
                    //ԭ���Ĵ���m_arrCanNoOrder = null;
                    m_glstNonExecutableOrder = null;
                    //ԭ���Ĵ���if (!ConfirmCurrentOrder(m_SelectORDERID_Arr, out m_arrCanNoOrder))
                    if (!ConfirmCurrentOrder(m_glstSelectOrder, out m_glstNonExecutableOrder))
                    {
                        //ԭ���Ĵ���DelTheOrderFromDTView(m_arrCanNoOrder);
                        DelTheOrderFromDTView(m_glstNonExecutableOrder);
                        m_mthRefreshSameReqNoColor();

                    }
                    MessageBox.Show("������˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }

                #region
                //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
                //comfirmBox1.m_txtName.Focus();
                //if (comfirmBox1.ShowDialog() == DialogResult.OK)
                //{

                //    long lngRes = m_objManage.m_lngUpdateBihOrderRedraw(m_strORDERID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr);
                //    m_arrCanNoOrder = null;
                //    if (!ConfirmCurrentOrder(m_SelectORDERID_Arr, out m_arrCanNoOrder))
                //    {
                //        DelTheOrderFromDTView(m_arrCanNoOrder);
                //        m_mthRefreshSameReqNoColor();

                //    }
                //    MessageBox.Show("������˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //comfirmBox1.Close();
                #endregion
            }
            else
            {

                long lngRes = m_objManage.m_lngUpdateBihOrderRedraw(m_strORDERID_Arr, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.LoginInfo.m_strEmpName);
                //ԭ���Ĵ���m_arrCanNoOrder = null;
                m_glstNonExecutableOrder = null;
                //ԭ���Ĵ���if (!ConfirmCurrentOrder(m_SelectORDERID_Arr, out m_arrCanNoOrder))
                if (!ConfirmCurrentOrder(m_glstSelectOrder, out m_glstNonExecutableOrder))
                {
                    //ԭ���Ĵ���DelTheOrderFromDTView(m_arrCanNoOrder);
                    DelTheOrderFromDTView(m_glstNonExecutableOrder);
                    m_mthRefreshSameReqNoColor();

                }
                MessageBox.Show("������˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            if (this.m_objViewer.m_dtvOrderList.RowCount == 0)
            {
                this.m_objViewer.m_dtvChangeList.Rows.Clear();
            }

        }

        internal void UpdateBihOrderBack()
        {
            if (m_objViewer.m_dtvOrderList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            string empid = "";
            string empname = "";

            DialogResult dlg = clsPublic.m_dlgConfirm(out empid, out empname);
            if (dlg == DialogResult.Yes)
            {
                string m_strORDERID_Arr = "";
                clsBIHCanExecOrder order = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.SelectedRows[0].Tag);
                frmSendBackReason BackReason = new frmSendBackReason();
                BackReason.m_txbOrderName.Text = order.m_strName;

                if (BackReason.ShowDialog() == DialogResult.OK)
                {
                    m_strORDERID_Arr = getTheORDERIDWithSon(order);
                    long lngRes = m_objManage.m_lngUpdateBihOrderBack(m_strORDERID_Arr, empid, empname, BackReason.m_txtReason.Text.Trim());
                    if (lngRes > 0)
                    {
                        MessageBox.Show("�˻���˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_objViewer.Cursor = Cursors.WaitCursor;
                        LoadTheDate();
                        this.m_objViewer.Cursor = Cursors.Default;


                    }
                }
            }

            #region
            //DotorComfirmBox comfirmBox1 = new DotorComfirmBox();
            //comfirmBox1.m_txtName.Focus();
            //if (comfirmBox1.ShowDialog() == DialogResult.OK)
            //{
            //    string m_strORDERID_Arr = "";
            //    clsBIHCanExecOrder order = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.SelectedRows[0].Tag);
            //    frmSendBackReason BackReason = new frmSendBackReason();
            //    BackReason.m_txbOrderName.Text = order.m_strName;

            //    if (BackReason.ShowDialog() == DialogResult.OK)
            //    {
            //        m_strORDERID_Arr = getTheORDERIDWithSon(order);
            //        //m_strORDERID_Arr = "'" +order.m_strOrderID +"'";
            //        long lngRes = m_objManage.m_lngUpdateBihOrderBack(m_strORDERID_Arr, comfirmBox1.empid_chr, comfirmBox1.lastname_vchr, BackReason.m_txtReason.Text.Trim());
            //        if (lngRes > 0)
            //        {
            //            MessageBox.Show("�˻���˳ɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.m_objViewer.Cursor = Cursors.WaitCursor;
            //            LoadTheDate();
            //            this.m_objViewer.Cursor = Cursors.Default;


            //        }
            //    }
            //}
            //comfirmBox1.Close();
            #endregion
        }

        #region ��ͬ��ҽ����һ�𷵻�
        /// <summary>
        /// ��ͬ��ҽ����һ�𷵻�
        /// </summary>
        /// <param name="order"></param>
        private string getTheORDERIDWithSon(clsBIHCanExecOrder order)
        {
            string m_strOrderID = "";
            int m_intRecipenNo = order.m_intRecipenNo;
            string m_strRegisterID = order.m_strRegisterID;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intRecipenNo == m_intRecipenNo && ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID.Equals(m_strRegisterID))
                {
                    m_strOrderID += ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID + ",";
                }
            }
            m_strOrderID = m_strOrderID.TrimEnd(',');
            return m_strOrderID;

        }
        #endregion

        #region �жϵ�ǰ�Ƿ�Ϊ��ҽ��
        /// <summary>
        /// ��ͬ��ҽ����һ�𷵻�
        /// </summary>
        /// <param name="order"></param>
        internal void m_TestORDERIDIsSon(clsBIHCanExecOrder order, out bool m_blSon)
        {
            m_blSon = false;
            string m_strOrderID = "";
            int m_intRecipenNo = order.m_intRecipenNo;
            string m_strRegisterID = order.m_strRegisterID;
            int SameCount = 0;
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                if (((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_intRecipenNo == m_intRecipenNo && ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID.Equals(m_strRegisterID))
                {
                    m_strOrderID = ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID;
                    if (m_strOrderID.Equals(order.m_strOrderID))
                    {

                        if (SameCount == 0)//��ǰ������ҽ���Ǹ�ҽ��
                        {
                            m_blSon = false;
                            break; ;
                        }
                        else
                        {
                            m_blSon = true;
                            break;
                        }
                    }
                    else
                    {
                        SameCount++;
                    }
                }
            }

        }
        #endregion

        internal void AddNewChargeItem()
        {
            if (this.m_objViewer.m_dtvOrderList.CurrentRow == null || this.m_objViewer.m_dtvOrderList.CurrentRow.Tag == null)
            {
                return;
            }

            clsBIHCanExecOrder objItem = (clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.CurrentRow.Tag;
            frmChargeItem frmCharge = new frmChargeItem(objItem);
            if (frmCharge.ShowDialog() == DialogResult.OK)
            {
                refreshTheChargeDate();
                OrderListSelect();

            };
        }

        internal void ChargeItemChanged()
        {
            if (this.m_objViewer.m_dtvOrderList.CurrentRow == null || this.m_objViewer.m_dtvOrderList.CurrentRow.Tag == null)
            {
                return;
            }
            if (this.m_objViewer.m_dtvChangeList.CurrentRow == null || this.m_objViewer.m_dtvChangeList.CurrentRow.Tag == null)
            {
                return;
            }
            clsChargeForDisplay objItem = (clsChargeForDisplay)this.m_objViewer.m_dtvChangeList.CurrentRow.Tag;

            //���շ���Ŀ�������޸� �շ����m_intType	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
            string m_intType = objItem.m_intType.ToString().Trim();
            string m_strSeq_int = objItem.m_strSeq_int;
            //string m_strGet_DEC = m_dtvOrderdicCharge["SumNumber", m_dtvOrderdicCharge.CurrentRow.Index].Value.ToString().Trim();

            //MessageBox.Show(strOrderID);
            frmChargeItem frmCharge = new frmChargeItem(m_strSeq_int);
            //��ʼ��������Ϣ
            try
            {
                frmCharge.m_intType = int.Parse(m_intType);
            }
            catch
            {
            }
            /*<========================*/
            if (frmCharge.ShowDialog() == DialogResult.OK)
            {

                refreshTheChargeDate();
                OrderListSelect();


            }
        }

        internal void DeleItemChanged()
        {
            if (this.m_objViewer.m_dtvOrderList.CurrentRow == null || this.m_objViewer.m_dtvOrderList.CurrentRow.Tag == null)
            {
                return;
            }
            if (this.m_objViewer.m_dtvChangeList.CurrentRow == null || this.m_objViewer.m_dtvChangeList.CurrentRow.Tag == null)
            {
                return;
            }
            clsChargeForDisplay objItem = (clsChargeForDisplay)this.m_objViewer.m_dtvChangeList.CurrentRow.Tag;

            //���շ���Ŀ�������޸�
            string m_intType = objItem.m_intType.ToString().Trim();
            if (m_intType.Trim().Equals("0"))
            {
                MessageBox.Show("���շ���Ŀ������ɾ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            /*<---------------------------*/
            if (MessageBox.Show("�Ƿ�ɾ������Ŀ?", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

            }
            else
            {
                return;
            }

            string m_strSeq_int = objItem.m_strSeq_int;
            long reg = m_objInputOrder.m_lngDellORDERCHARGEDEPT(m_strSeq_int);
            if (reg <= 0)
            {
                MessageBox.Show("ɾ��ʧ��!");
            }
            else
            {
                refreshTheChargeDate();
                OrderListSelect();

            }
        }

        /// <summary>
        /// ��鵱ǰ�Ƿ��������ݵı䶯
        /// </summary>
        internal void CheckTheChanged()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(4000);

                DataTable m_dtExecOrder2 = new DataTable();
                long lngRes = m_objManage.m_lngCheckTheChanged((string)m_objViewer.m_txtArea.Tag, m_intState, out m_dtExecOrder2);
                if (CompareTheTable(m_dtExecOrder, m_dtExecOrder2) == false)
                {
                    this.m_objViewer.Cursor = Cursors.WaitCursor;
                    LoadTheDate();
                    this.m_objViewer.Cursor = Cursors.Default;
                }
            }
        }

        private bool CompareTheTable(DataTable m_dtExecOrder, DataTable m_dtExecOrder2)
        {
            bool m_blSame = false;
            if (m_dtExecOrder != null || m_dtExecOrder2 != null)
            {
                if (m_dtExecOrder.Rows.Count != m_dtExecOrder2.Rows.Count)
                {
                    m_blSame = false;

                }
                else
                {
                    for (int i = 0; i < this.m_dtExecOrder.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.m_dtChargeList.Columns.Count; j++)
                        {
                            if (this.m_dtExecOrder.Rows[i][j].ToString().Trim().Equals(m_dtExecOrder2.Rows[i][j].ToString().Trim()))
                            {

                            }
                            else
                            {
                                m_blSame = false;
                                break;
                            }
                        }
                    }
                    m_blSame = true;
                }

            }
            return m_blSame;
        }

        /// <summary>
        /// �򿪴���ʱ����ҽ������
        /// </summary>
        internal void LoadTheOrderCate()
        {
            clsT_aid_bih_ordercate_VO[] p_objItemArr = null;
            long lngRes = m_objInputOrder.m_lngGetAidOrderCate(out p_objItemArr);
            m_htOrderCate.Clear();
            int intLen = p_objItemArr.Length;
            for (int i = 0; i < intLen; i++)
            {
                if (!m_htOrderCate.Contains(p_objItemArr[i].m_strORDERCATEID_CHR))
                {
                    m_htOrderCate.Add(p_objItemArr[i].m_strORDERCATEID_CHR, p_objItemArr[i]);
                }
            }

        }

        internal void GetTheControl()
        {
            int m_intNeedConfirm = -1;
            //long lngRes = m_objManage.GetTheComfirmControl(out m_intNeedConfirm, out m_intExeConfirm);

            /*ԭ������ȷ����,��Ҫ����
            ArrayList m_arrControl = new ArrayList();
            m_arrControl.Add("1028");//�ύ��ִ���Ƿ���Ҫ����Ա���ſ��� 0-����Ҫ 1-��Ҫ
            m_arrControl.Add("1029");//ִ���Ƿ���Ҫ����Ա���ſ��� 0-����Ҫ 1-��Ҫ
            m_arrControl.Add("1036");//ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
            m_arrControl.Add("1037");//ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037           
            m_arrControl.Add("1038");//'1038', 'סԺת�������Ƿ���ʾ�������', '0-��1-��', 1,
            m_arrControl.Add("1039");//'1039', 'סԺת���������������ʾ���ʱ��', '��λ:��', 10, 
            m_arrControl.Add("1040");//'1040', 'סԺת������������Ѵ�����ʾͣ��ʱ��', '��λ:��', 5, 
            m_arrControl.Add("1018");//m_dmlMedOCMin = 0;//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_arrControl.Add("1019");//m_dmlNoMedOCMin = 0;//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_arrControl.Add("1020");//m_dmlMedICMin = 0;//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_arrControl.Add("1021");//m_dmlNoMedICMin = 0;//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_arrControl.Add("1030");//m_intMoneyControl = 0;//'1030', '���ƻ�ʿִ��ģ���Ƿ�����Ƿ�Ѳ���ִ��ҽ��', '0-������ 1-����'
            m_arrControl.Add("1046");//'1046', '����Ƿ��ִ��ʱ�Ҳ��˽�Ƿ��ʱ�Ĳ��˷�����ʾ����', '0-����ʾ 1-��ʾ'
            m_arrControl.Add("1047");//'1047', 'ҽ��ִ�н����Ƿ���������ѡ��ҽ��ִ�п���', '0-������ 1-����'
            m_arrControl.Add("1049");//'1049', '����������Ŀ��Ӧ�����շ���Ŀ��һ�Զࣩ�Ƿ��ҩ',  '0-����ҩ 1-��ҩ'           
            m_arrControl.Add("4006");//����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
            m_arrControl.Add("4007");//�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
            m_arrControl.Add("4008");//0 ������ 1 �������
            m_arrControl.Add("1053");//'1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1
            */


            //�������´���
            System.Collections.Generic.List<string> m_glstControl = new System.Collections.Generic.List<string>();
            m_glstControl.Add("1028");//�ύ��ִ���Ƿ���Ҫ����Ա���ſ��� 0-����Ҫ 1-��Ҫ
            m_glstControl.Add("1029");//ִ���Ƿ���Ҫ����Ա���ſ��� 0-����Ҫ 1-��Ҫ
            m_glstControl.Add("1036");//ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
            m_glstControl.Add("1037");//ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037           
            m_glstControl.Add("1038");//'1038', 'סԺת�������Ƿ���ʾ�������', '0-��1-��', 1,
            m_glstControl.Add("1039");//'1039', 'סԺת���������������ʾ���ʱ��', '��λ:��', 10, 
            m_glstControl.Add("1040");//'1040', 'סԺת������������Ѵ�����ʾͣ��ʱ��', '��λ:��', 5, 
            m_glstControl.Add("1018");//m_dmlMedOCMin = 0;//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_glstControl.Add("1019");//m_dmlNoMedOCMin = 0;//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_glstControl.Add("1020");//m_dmlMedICMin = 0;//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_glstControl.Add("1021");//m_dmlNoMedICMin = 0;//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
            m_glstControl.Add("1030");//m_intMoneyControl = 0;//'1030', '���ƻ�ʿִ��ģ���Ƿ�����Ƿ�Ѳ���ִ��ҽ��', '0-������ 1-����'
            m_glstControl.Add("1046");//'1046', '����Ƿ��ִ��ʱ�Ҳ��˽�Ƿ��ʱ�Ĳ��˷�����ʾ����', '0-����ʾ 1-��ʾ'
            m_glstControl.Add("1047");//'1047', 'ҽ��ִ�н����Ƿ���������ѡ��ҽ��ִ�п���', '0-������ 1-����'
            m_glstControl.Add("1049");//'1049', '����������Ŀ��Ӧ�����շ���Ŀ��һ�Զࣩ�Ƿ��ҩ',  '0-����ҩ 1-��ҩ'           
            m_glstControl.Add("4006");//����Ϊ8��������м��飨��Ʊ����Ϊ���飩�շ���Ŀ>8��ʱ���ô��۹���
            m_glstControl.Add("4007");//�������ô��۹���ʱ�������շ���Ŀ�Ĵ��۱�����80��������
            m_glstControl.Add("4008");//0 ������ 1 �������
            m_glstControl.Add("1053");//'1053', 'סԺҽ��¼������Ƿ��Զ���ʾ��ǰ���˴���ͣ�û�ȱҩ��δͣҽ��', '0-��1-��', 1
            m_glstControl.Add("1050"); // '1050' ����ҽ����ִ�л������ύʱ���ͼ������뵥��  0-ִ��ʱ���� 1-�ύʱ����

            //�������´���

            DataTable dtbResult = null;
            long lngRes = m_objManage.GetTheHisControl(m_glstControl, out dtbResult);
            int intRowCount = dtbResult.Rows.Count; //new code

            if (lngRes > 0 && dtbResult != null && intRowCount > 0)
            {
                System.Data.DataRow objRow = null;//new code

                for (int i = 0; i < intRowCount; i++)
                {
                    objRow = dtbResult.Rows[i];//new code
                    string strSetID = objRow["setid_chr"].ToString().TrimEnd();//new code
                    string strSetStatus = objRow["setstatus_int"].ToString().TrimEnd();//new code
                    switch (strSetID)
                    {
                        case "1028":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blNeedComfirm = false;
                            }
                            else
                            {
                                m_blNeedComfirm = true;
                            }
                            break;
                        case "1029":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blExeConfirm = false;
                            }
                            else
                            {
                                m_blExeConfirm = true;
                            }
                            break;
                        case "1038"://'1038', 'סԺת�������Ƿ���ʾ�������', '0-��1-��', 1,
                            if (strSetStatus.Equals("0"))
                            {
                                m_blNeedMessageAlert = false;
                            }
                            else
                            {
                                m_blNeedMessageAlert = true;
                            }
                            break;
                        case "1039"://'1039', 'סԺת���������������ʾ���ʱ��', '��λ:��', 10, 
                            if (!strSetStatus.Equals(""))
                            {
                                m_intMessageOpenTime = int.Parse(strSetStatus);
                            }
                            break;
                        case "1040"://'1040', 'סԺת������������Ѵ�����ʾͣ��ʱ��', '��λ:��', 5, 
                            if (!strSetStatus.Equals(""))
                            {
                                m_intMessageCloseTime = int.Parse(strSetStatus);
                            }
                            break;
                        case "1018":
                            m_dmlMedOCMin = decimal.Parse(strSetStatus);//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1019":
                            m_dmlNoMedOCMin = decimal.Parse(strSetStatus);//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1020":
                            m_dmlMedICMin = decimal.Parse(strSetStatus);//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1021":
                            m_dmlNoMedICMin = decimal.Parse(strSetStatus);//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1030":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blMoneyControl = false;
                            }
                            else
                            {
                                m_blMoneyControl = true;
                            }

                            break;
                        case "1046":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blLessExecuteAlert = false;
                            }
                            else
                            {
                                m_blLessExecuteAlert = true;
                            }
                            break;
                        case "1047":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blCanSelectOrder = false;
                            }
                            else
                            {
                                m_blCanSelectOrder = true;
                            }
                            break;
                        case "1049":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blPutMedicineFormDic = false;
                            }
                            else
                            {
                                m_blPutMedicineFormDic = true;
                            }
                            break;
                        case "4006":
                            int.TryParse(strSetStatus, out m_intLisDiscountNum);
                            break;
                        case "4007":
                            decimal.TryParse(strSetStatus, out m_decLisDiscountMount);
                            break;
                        case "4008":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blLisDiscount = false;
                            }
                            else
                            {
                                m_blLisDiscount = true;
                            }
                            break;
                        case "1036":
                            if (strSetStatus.Equals("1"))
                            {
                                m_blDeableMedControl = true;
                            }
                            else
                            {
                                m_blDeableMedControl = false;
                            }
                            break;
                        case "1037":
                            if (strSetStatus.Equals("1"))
                            {
                                m_blStopControl = true;
                            }
                            else
                            {
                                m_blStopControl = false;
                            }
                            break;
                        case "1053":
                            if (strSetStatus.Equals("1"))
                            {
                                m_blAutoStopAlert = true;
                            }
                            else
                            {
                                m_blAutoStopAlert = false;
                            }
                            break;
                        case "1050":
                            if (strSetStatus.Equals("0"))
                            {
                                m_blSendLisBill = false;
                            }
                            else
                            {
                                m_blSendLisBill = true;
                            }
                            break;
                    }
                }
            }

        }

        #region ��λ���¼�
        internal void m_txtBedNo2FindItem(string strFindCode, ListView lvwList)
        {

            this.m_objViewer.m_txtBedNo2.Tag = null;
            /*<----------------------------------------*/

            if (this.m_objViewer.m_txtArea.Tag == null)
            {
                //if (m_blnPrompt) MessageBox.Show("����ָ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("����ָ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.m_txtArea.Text = "";
                this.m_objViewer.m_txtArea.Tag = null;
                this.m_objViewer.m_txtArea.Focus();
                return;
            }
            string strAreaID = (string)this.m_objViewer.m_txtArea.Tag;
            clsBIHBed[] arrBed = null;
            string strBedNo = this.m_objViewer.m_txtBedNo2.Text.Trim();
            ArrayList m_arrBedList = GetTheBed(strFindCode);

            if (m_arrBedList.Count > 0)
            {
                arrBed = (clsBIHBed[])m_arrBedList.ToArray(typeof(clsBIHBed));
            }
            //long ret = m_objService.m_lngGetBihBedByArea(strAreaID, strBedNo, out arrBed);
            if (arrBed != null)
            {
                if (arrBed.Length == 0)
                {
                    MessageBox.Show("��ǰ����û����Ӧ��ǰ���������Ĵ�λ��������ѡ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.m_objViewer.m_txtBedNo2.Focus();
                    return;
                }
                string upName = "";
                for (int i = 0; i < arrBed.Length; i++)
                {
                    //Ϊ�����б�����������ձ� 
                    // ListViewItem objItem = m_lvwBed.Items.Add(arrBed[i].m_strBedName);
                    /*------------------------------------>*/
                    //if (i > 0)
                    //{
                    //    upName = arrBed[i - 1].m_objPatient.m_strAreaName;
                    //}
                    //if (arrBed[i].m_objPatient.m_strAreaName.Trim().Equals(upName.Trim()))
                    //{
                    //    upName = "";
                    //}
                    //else
                    //{
                    //    upName = arrBed[i].m_objPatient.m_strAreaName;
                    //}

                    //ListViewItem objItem = new ListViewItem(upName);
                    ListViewItem objItem = new ListViewItem(arrBed[i].m_strBedName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strPatientName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strSex);
                    /*<----------------------*/
                    //objItem.Tag = arrBed[i].m_strBedID;
                    objItem.Tag = arrBed[i];
                    lvwList.Items.Add(objItem);

                }

            }
            else
            {
                MessageBox.Show("��ǰ����û����Ӧ�Ĵ�λ��������ѡ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
        }

        /// <summary>
        /// �õ���ǰ�����б�
        /// </summary>
        /// <returns></returns>
        private ArrayList GetTheBed(String m_strBedNo)
        {
            ArrayList m_arrBeds = new ArrayList();
            ArrayList m_arrBedList = new ArrayList();
            clsBIHCanExecOrder[] arrExecOrder;
            DataView myDataView = new DataView(m_dtExecOrder);
            filltheExecOrderTable(myDataView, out arrExecOrder);
            string m_strBedIdSelect = "";//��ѡ���Ĵ��ţ���ݼ�ѡ��)
            //�Ƿ��ǿ�ݼ���ʽѡ���Ź���
            if (m_blBedIdKey)
            {
                if (m_intBedIndex != -1 && m_arrBedIdList != null && m_arrBedIdList.Count > 0)
                {
                    if (m_intBedIndex >= m_arrBedIdList.Count || m_intBedIndex <= -1)
                    {
                        m_intBedIndex = 0;
                    }
                    m_strBedIdSelect = Convert.ToString(m_arrBedIdList[m_intBedIndex]);
                }
                m_blBedIdKey = false;
            }
            /*<============================*/
            for (int i = 0; i < arrExecOrder.Length; i++)
            {
                clsBIHCanExecOrder order = arrExecOrder[i];
                if (m_strBedIdSelect.Equals(""))
                {
                    if (!order.m_strBedName.Contains(m_strBedNo))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!order.m_strBedID.Equals(m_strBedIdSelect))
                    {
                        continue;
                    }
                }

                if (!m_arrBeds.Contains(order.m_strBedID))
                {
                    m_arrBeds.Add(order.m_strBedID);
                    clsBIHBed m_objBed = new clsBIHBed();
                    //m_objBed.m_strAreaID = order.m_strCURAREAID_CHR;
                    m_objBed.m_strBedID = order.m_strBedID;
                    //m_objBed.m_strBedName = order.m_strCURBEDName;
                    m_objBed.m_strBedName = order.m_strBedName;
                    m_objBed.m_objPatient = new clsBIHPatientInfo();
                    m_objBed.m_objPatient.m_strPatientName = order.m_strPatientName;
                    //m_objBed.m_objPatient.m_strAreaName = order.m_strCURAREAName;
                    m_objBed.m_objPatient.m_strSex = order.m_strPatientSex;
                    m_arrBedList.Add(m_objBed);

                }
            }
            return m_arrBedList;
        }

        internal void m_txtBedNo2InitListView(ListView lvwList)
        {
            // lvwList.Columns.Add("��  ��", 100, HorizontalAlignment.Left);
            lvwList.Columns.Add("������", 40, HorizontalAlignment.Left);
            lvwList.Columns.Add("�ա���", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("�ԡ���", 40, HorizontalAlignment.Left);
            lvwList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvwList.Width = 170;
            /* <<================================= */
        }

        internal void m_txtBedNo2SelectItem(ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                this.m_objViewer.m_txtBedNo2.Text = lviSelected.SubItems[0].Text;
                this.m_objViewer.m_txtBedNo2.Tag = lviSelected.Tag;
                this.m_objViewer.m_txtBedNo2.SelectAll();
                this.m_objViewer.m_cboCode.SelectedIndex = 1;
                refreshTheDataByBed(true);
            }
        }

        internal void refreshTheDataByBed(bool isBed)
        {
            if (isBed)
            {
                if (this.m_objViewer.m_txtBedNo2.Tag is clsBIHBed)
                {
                    if (m_dtExecOrder == null) return;
                    DataView myDataView = new DataView(m_dtExecOrder);
                    myDataView.RowFilter = "bedid_chr = '" + ((clsBIHBed)this.m_objViewer.m_txtBedNo2.Tag).m_strBedID + "'";
                    myDataView.Sort = "code_chr, registerid_chr, recipeno_int, orderid_chr asc";

                    clsBIHCanExecOrder[] arrExecOrder;
                    filltheExecOrderTable(myDataView, out arrExecOrder);
                    BindTheBihOrderList(arrExecOrder);
                }
            }
            else if (!isBed)
            {
                if (m_dtExecOrder == null) return;
                DataView myDataView = new DataView(m_dtExecOrder);
                myDataView.Sort = "code_chr, registerid_chr, recipeno_int, orderid_chr asc";
                clsBIHCanExecOrder[] arrExecOrder;
                filltheExecOrderTable(myDataView, out arrExecOrder);
                BindTheBihOrderList(arrExecOrder);
            }
        }
        #endregion

        internal void m_cboCode_SelectedValueChanged()
        {
            if (this.m_objViewer.m_cboCode.SelectedIndex == 0)
            {
                refreshTheDataByBed(false);
            }
            else
            {
                refreshTheDataByBed(true);
            }
        }

        public bool SelectTheOrderFromPerson()
        {
            clsBIHCanExecOrder[] arr = (clsBIHCanExecOrder[])getThePersonFromView().ToArray(typeof(clsBIHCanExecOrder));
            if (arr.Length == 0)
            {
                return false;
            }
            ArrayList m_arrRegisterID = new ArrayList();
            clsBIHCanExecOrder[] arrBed = (clsBIHCanExecOrder[])getThePersonFromView().ToArray(typeof(clsBIHCanExecOrder)); ;
            // ��ҪƤ�Եĵ��������ԵĽ�Ҫ�ύ��ҽ���Ĳ���id
            //ArrayList m_arrFeelTest = GetTheFeelFaulList();
            ArrayList m_arrFeelTest = new ArrayList();
            frmBedPatientList objForm = new frmBedPatientList(m_arrFeelTest, 0);
            objForm.arrBed = arrBed;
            if (objForm.ShowDialog() == DialogResult.OK)
            {

                m_arrRegisterID = objForm.m_arrPersionID;
                if (m_arrRegisterID.Count > 0)
                {
                    //this.m_objViewer.cmdRefurbish_Click(null, null);
                }
                else
                {
                    return false;
                }
                SetTheCheckFromPersionList(m_arrRegisterID);
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ���Ƥ��û�����Ϊ���ԵĲ����б�
        /// </summary>
        /// <param name="clsBIHCanExecOrder"></param>
        /// <returns></returns>
        private ArrayList GetTheFeelFaulList()
        {
            ArrayList m_arr = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                if (order.m_intISNEEDFEEL == 1 && (order.m_intFEEL_INT == 2 || order.m_intFEEL_INT == 0))
                {
                    if (!m_arr.Contains(order.m_strRegisterID))
                    {
                        m_arr.Add(order.m_strRegisterID);
                    }
                }

            }
            return m_arr;
        }


        private void SetTheCheckFromPersionList(ArrayList arr)
        {
            clsBIHCanExecOrder order = null;
            string m_strRegisterID = "";
            ArrayList m_arrFellArr = new ArrayList();//����ͨ���˶Ե�Ƥ����
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strRegisterID = order.m_strRegisterID;
                if (arr.Contains(m_strRegisterID))
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";
                    if (order.m_intISNEEDFEEL == 1 && order.m_intFEEL_INT != 0 && order.m_strDosetypeID.Trim().Equals(m_objSpecateVo.m_strUSAGEID_CHR.Trim()))//��Ҫ�����շ�Ƥ��Ƶ��
                    {

                    }
                    else if (order.m_intISNEEDFEEL == 1 && (order.m_intFEEL_INT == 2 || order.m_intFEEL_INT == 0))//��ҪƤ�Ե���û��Ƥ�Խ����Ϊ����
                    {
                        m_arrFellArr.Add(order);
                        //m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                    }
                }
                else
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                }
            }
            SetTheFeelSameNoSelect(m_arrFellArr);
        }

        /// <summary>
        /// ���ܺ˶Ե�Ƥ��ͬ���Ĳ�ѡ��
        /// </summary>
        /// <param name="m_arrFellArr"></param>
        private void SetTheFeelSameNoSelect(ArrayList m_arrFellArr)
        {
            clsBIHCanExecOrder order1, order2;
            for (int i = 0; i < m_arrFellArr.Count; i++)
            {
                order1 = (clsBIHCanExecOrder)m_arrFellArr[i];
                for (int j = 0; j < this.m_objViewer.m_dtvOrderList.RowCount; j++)
                {
                    order2 = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[j].Tag;
                    if (order1.m_strRegisterID == order2.m_strRegisterID && order1.m_intRecipenNo == order2.m_intRecipenNo)
                    {
                        m_objViewer.m_dtvOrderList.Rows[j].Cells["m_dtvselectCheck"].Value = "0";
                    }
                }
            }
        }

        private ArrayList getThePersonFromView()
        {
            int cout = 0;
            ArrayList arr = new ArrayList();
            ArrayList arr2 = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {

                string m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (!arr.Contains(m_strRegisterID))
                {
                    arr.Add(m_strRegisterID);
                    arr2.Add((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag);
                }

            }

            return arr2;
        }

        /// <summary>
        /// �༭Ƥ��
        /// </summary>
        internal void EditFeel()
        {
            if (m_objViewer.m_dtvOrderList.SelectedRows.Count > 0)
            {
                DataGridViewRow row1 = m_objViewer.m_dtvOrderList.SelectedRows[0];
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.SelectedRows[0].Tag;
                //���ҪƤ������ʾƤ��
                if (order.m_intISNEEDFEEL == 0)
                {
                    MessageBox.Show("�뵥��ѡ��һ����AST()Ƥ�Ա�־��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //��ʾƤ��¼��ҳ��
                clsT_Opr_Bih_OrderFeel_VO m_objFell = new clsT_Opr_Bih_OrderFeel_VO();
                m_objFell.m_strORDERID_CHR = order.m_strOrderID;
                m_objFell.m_strParentName = order.m_strPatientName;
                m_objFell.m_strOrderName = order.m_strName;
                m_objFell.m_intRESULTTYPE_INT = order.m_intFEEL_INT;
                m_objFell.m_strDES_VCHR = order.m_strFEELRESULT_VCHR;
                frmNeedFeel objfrmNeedFeel = new frmNeedFeel(m_objFell);
                if (objfrmNeedFeel.ShowDialog() == DialogResult.OK)
                {

                    int intDeleteFeelFlag = objfrmNeedFeel.m_intFeelFlag;

                    if ((objfrmNeedFeel.p_objRecord.m_intRESULTTYPE_INT == 1 || objfrmNeedFeel.p_objRecord.m_intRESULTTYPE_INT == 0) && intDeleteFeelFlag == 1)
                    {
                        long l = this.m_objManage.m_lngDeleteFeelCharge(order.m_strOrderID);
                    }
                    else if (objfrmNeedFeel.p_objRecord.m_intRESULTTYPE_INT == 2)
                    {
                        DataView myDataView = new DataView(m_dtChargeList);
                        myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
                        myDataView.Sort = "FLAG_INT";
                        if (myDataView.Count <= 0)
                        {
                            return;
                        }
                        clsChargeForDisplay[] m_arrObjItem;
                        m_mthGetChargeListFromDateTable(myDataView, out m_arrObjItem);
                        frmFeelCharge objCharge = new frmFeelCharge(m_arrObjItem);
                        if (objCharge.ShowDialog() == DialogResult.OK)
                        {
                            if (objCharge.listFeelCharge == null || objCharge.listFeelCharge.Count == 0)
                            {
                            }
                            else
                            {
                                clsBIHPatientInfo m_objPatient;

                                DataTable m_dtPatient = null;
                                long lngRes = m_objManage.m_lngGetPatientInfoVo(order.m_strRegisterID, out m_dtPatient);
                                if (m_dtPatient != null && m_dtPatient.Rows.Count > 0)
                                {
                                    string m_strInHospitalNoOld = "";
                                    string m_strInHospitalNoNow = "";
                                    m_strInHospitalNoOld = this.m_objViewer.m_ctlPatient.m_txtRegisterid.Text.Trim();
                                    m_strInHospitalNoNow = order.m_strRegisterID.Trim();
                                    if (m_strInHospitalNoOld.Equals(m_strInHospitalNoNow))
                                    {
                                        return;
                                    }

                                    DataView dtPatient = new DataView(m_dtPatient);
                                    dtPatient.RowFilter = "registerid_chr='" + order.m_strRegisterID + "'";
                                    if (dtPatient.Count <= 0)
                                    {
                                        return;
                                    }
                                    m_mthGetPatientInfoFromDateTable(dtPatient, out m_objPatient);

                                    string strItemid = "";
                                    for (int i1 = 0; i1 < objCharge.listFeelCharge.Count; i1++)
                                    {
                                        strItemid += "'" + objCharge.listFeelCharge[i1].m_strChargeID + "',";
                                    }

                                    strItemid = strItemid.TrimEnd(',');

                                    if (intDeleteFeelFlag == 2)
                                    {
                                        if (MessageBox.Show(this.m_objViewer, "��ҽ����Ƥ���Ѿ����գ�Ҫ��ε�Ƥ�Է���Ϊ׼�� \r\n[��] �Ըôε�Ƥ�Է�Ϊ׼��[��] �ٴμ���Ƥ�Է� ��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            long lngDelete = this.m_objManage.m_lngDeleteFeelCharge(order.m_strOrderID);
                                        }
                                    }

                                    long l = this.m_objManage.m_lngFeelCharge(order.m_strOrderID, strItemid, order.m_intExecuteType, m_objPatient.m_strAreaID,
                                        m_objPatient.m_strBedID, this.m_objViewer.LoginInfo.m_strEmpID, IsChildPrice(order.m_strOrderID));
                                    if (l > 0)
                                    {
                                        MessageBox.Show(this.m_objViewer, "�Ѿ��ɹ���ȡƤ�Է��á�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                    else
                                    {
                                        MessageBox.Show(this.m_objViewer, "Ƥ�Է���ȡʧ�ܡ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }

                    order.m_intFEEL_INT = objfrmNeedFeel.p_objRecord.m_intRESULTTYPE_INT;
                    order.m_strFEELRESULT_VCHR = objfrmNeedFeel.p_objRecord.m_strDES_VCHR;
                    //if (order.m_intISNEEDFEEL == 1)
                    //{
                    //    string m_strFeel = "";
                    //    switch (order.m_intFEEL_INT)
                    //    {
                    //        case 0:
                    //            m_strFeel = " AST( ) ";
                    //            break;
                    //        case 1:
                    //            m_strFeel = " AST(-) ";
                    //            break;
                    //        case 2:
                    //            m_strFeel = " AST(+) ";
                    //            break;
                    //    }

                    //    //row1.Cells["dtv_Name"].Value = order.m_strName + m_strFeel;

                    //}
                    m_SaveTheATTACHTIMES_INT(order);
                    BindTheBihOrderListDetail(row1, order);
                    refreshTheDataGridView();
                }

                // }
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

        public void PrintFeel()
        {
            ArrayList m_arrExecOrder = new ArrayList();
            for (int i = 0; i < m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                DataGridViewRow row1 = m_objViewer.m_dtvOrderList.Rows[i];
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)row1.Tag;
                if (order.m_intISNEEDFEEL == 1 && order.m_intFEEL_INT > 0)
                {
                    m_arrExecOrder.Add(order);
                }
            }
            if (m_arrExecOrder.Count > 0)
            {
                frmFeelCard m_freCard = new frmFeelCard((clsBIHCanExecOrder[])m_arrExecOrder.ToArray(typeof(clsBIHCanExecOrder)));
                m_freCard.ShowDialog();
            }
        }

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        internal void MessageList()
        {
            if (m_objViewer.m_txtArea.Tag == null || ((string)m_objViewer.m_txtArea.Tag).Trim().Equals(""))
            {
                return;
            }
            DataTable m_dtNewOrder = null;
            //��ȡ��ǰ������˵�����
            long lngRes = m_objManage.m_lngGetOrderMessageByTimer((string)m_objViewer.m_txtArea.Tag, out m_dtNewOrder);
            if (m_dtNewOrder != null && m_dtNewOrder.Rows.Count > 0)
            {
            }
            else
            {
                return;
            }
            //��Ҫ��˵��ύ״̬��ҽ��
            int m_intExeNewSum = 0;
            //��Ҫ��˵�ֹͣ״̬��ҽ��
            int m_intExeStopSum = 0;
            //�����б�
            ArrayList m_arrPatient = new ArrayList();
            for (int i = 0; i < m_dtNewOrder.Rows.Count; i++)
            {
                if (m_dtNewOrder.Rows[i]["status_int"].ToString().Equals("1"))
                {
                    m_intExeNewSum++;
                }
                else
                {
                    if (m_dtNewOrder.Rows[i]["status_int"].ToString().Equals("3"))
                    {
                        if (m_dtNewOrder.Rows[i]["EXECUTETYPE_INT"].ToString().Equals("1"))
                        {
                            m_intExeStopSum++;
                        }
                    }
                }
                if (!m_arrPatient.Contains(m_dtNewOrder.Rows[i]["REGISTERID_CHR"].ToString()))
                {
                    m_arrPatient.Add(m_dtNewOrder.Rows[i]["REGISTERID_CHR"].ToString());
                }

            }
            string Remark = "";
            if (m_arrPatient.Count > 0)
            {
                Remark = " ���� " + m_arrPatient.Count.ToString() + " �����˵�ҽ����Ҫ���";
            }
            if (Remark.Trim().Length > 0)
            {
                Remark += " \r\n" + " ";
                if (m_intExeNewSum > 0)
                {
                    Remark += " ������ύ��ҽ�� " + m_intExeNewSum.ToString() + " ��";
                }
                if (m_intExeStopSum > 0)
                {
                    Remark += " \r\n" + " ";
                    Remark += " �����ֹͣ��ҽ�� " + m_intExeStopSum.ToString() + " ��";
                }
            }
            if (!Remark.Equals(""))
            {
                //int ShowCodexRemarkFrmTimerinterval = ((frmBIHOrderInput)this.ParentForm).ShowCodexRemarkFrmTimerinterval;
                frmMessageRemark frmRemark = new frmMessageRemark(Remark, m_intMessageCloseTime);
                //frmRemark.StartPosition = FormStartPosition.CenterScreen;
                frmRemark.StartPosition = FormStartPosition.Manual;
                frmRemark.Location = new System.Drawing.Point(500, 10);
                frmRemark.m_freNurse = this.m_objViewer;
                frmRemark.Show();
            }



        }

        /// <summary>
        /// ��ȡסԺ�������ñ�
        /// </summary>
        internal void SetSPECORDERCATE()
        {
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngAddGetSPECORDERCATE(out m_objSpecateVo);
        }

        /// <summary>
        /// ת��ִ�н���
        /// </summary>
        internal void m_cmdTurnToExecute()
        {
            frmOrderExecute OrderExecute = new frmOrderExecute(m_objSpecateVo, m_htOrderCate, true);
            //���ش���

            OrderExecute.m_blNeedComfirm = m_blNeedComfirm;
            OrderExecute.m_blExeConfirm = m_blExeConfirm;
            OrderExecute.m_blNeedMessageAlert = m_blNeedMessageAlert;
            OrderExecute.m_intMessageOpenTime = m_intMessageOpenTime;
            OrderExecute.m_intMessageCloseTime = m_intMessageCloseTime;
            OrderExecute.m_dmlMedOCMin = m_dmlMedOCMin;
            OrderExecute.m_dmlNoMedOCMin = m_dmlNoMedOCMin;
            OrderExecute.m_dmlMedICMin = m_dmlMedICMin;
            OrderExecute.m_dmlNoMedICMin = m_dmlNoMedICMin;
            OrderExecute.m_blMoneyControl = m_blMoneyControl;
            OrderExecute.m_blLessExecuteAlert = m_blLessExecuteAlert;
            OrderExecute.m_blCanSelectOrder = m_blCanSelectOrder;
            OrderExecute.m_blPutMedicineFormDic = m_blPutMedicineFormDic;
            OrderExecute.m_intLisDiscountNum = m_intLisDiscountNum;
            OrderExecute.m_decLisDiscountMount = m_decLisDiscountMount;
            OrderExecute.m_blLisDiscount = m_blLisDiscount;
            OrderExecute.m_blAutoStopAlert = m_blAutoStopAlert;
            OrderExecute.m_blDeableMedControl = m_blDeableMedControl;
            OrderExecute.m_blStopControl = m_blStopControl;
            OrderExecute.m_blSendLisBill = m_blSendLisBill;//--
            /*<====================================*/

            OrderExecute.m_txtArea.Tag = this.m_objViewer.m_txtArea.Tag;
            OrderExecute.m_txtArea.Text = this.m_objViewer.m_txtArea.Text;

            this.m_objViewer.m_timerMessage.Enabled = false;
            OrderExecute.ShowDialog();
            this.m_objViewer.m_timerMessage.Enabled = true;
        }



        internal void m_SaveTheEntrust(clsBIHCanExecOrder order1)
        {
            //�޸�ҽ������
            long lngRes = m_objManage.m_lngSaveTheEntrust(order1.m_strRegisterID, order1.m_intRecipenNo, order1.m_strEntrust);

        }

        internal void m_SaveTheATTACHTIMES_INT(clsBIHCanExecOrder order1)
        {
            //�޸�ҽ������
            long lngRes = m_objManage.m_lngSaveTheATTACHTIMES_INT(order1.m_strRegisterID, order1.m_intRecipenNo, order1.m_intATTACHTIMES_INT);

        }

        internal void TheSelectedItemForDrawBack()
        {
            ClearSelect();

            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.SelectedRows.Count; i++)
            {
                DataGridViewRow row1 = this.m_objViewer.m_dtvOrderList.SelectedRows[i];
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)row1.Tag;
                string m_strValue = "1";
                //ͬ������
                TheSamerecipeno(order, m_strValue);
            }

        }

        private void ClearSelect()
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                DataGridViewRow row1 = this.m_objViewer.m_dtvOrderList.Rows[i];
                row1.Cells["m_dtvselectCheck"].Value = "0";
            }
        }

        /// <summary>
        /// ѡ�е���Ŀת��ΪCHECKBOX��Ҳѡ��
        /// </summary>
        internal void SelectItemToChecked()
        {
            //��Ч��ѡ����(Ƥ��û�н����Ϊ���Ե�Ϊ��Ч)
            ArrayList m_arrActive = new ArrayList();
            //���ܹ��˶Ե�Ƥ������
            ArrayList m_arrFeelDeable = new ArrayList();
            string m_strID = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
                if (order.m_intISNEEDFEEL == 1)//��Ҫ�����շ�Ƥ��Ƶ��
                {
                    if (order.m_intISNEEDFEEL == 1 && order.m_intFEEL_INT != 0 && order.m_strDosetypeID.Trim().Equals(m_objSpecateVo.m_strUSAGEID_CHR.Trim()))//��Ҫ�����շ�Ƥ��Ƶ��
                    {
                    }
                    else if (order.m_intISNEEDFEEL == 1 && (order.m_intFEEL_INT == 2 || order.m_intFEEL_INT == 0))//��ҪƤ�Ե���û��Ƥ�Խ����Ϊ����
                    {
                        //��ҪƤ�Ե���û��Ƥ�Խ����Ϊ���Բ�ѡ��
                        m_arrFeelDeable.Add(m_strID);
                    }
                }
            }
            m_strID = "";//������ˮ�ǼǺż��������
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.SelectedRows.Count; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.SelectedRows[i].Tag;
                m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
                m_arrActive.Add(m_strID);

            }
            m_strID = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
                if (m_arrActive.Contains(m_strID) && !m_arrFeelDeable.Contains(m_strID))
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";
                }
                else
                {
                    this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                }

            }

        }

        /// <summary>
        /// ��յ�ǰ������ѡ��
        /// </summary>
        internal void ClearTheChecked()
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                this.m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
            }
        }

        /// <summary>
        /// ˢ�µ�ǰҽ����Ŀ����Ϣ������ͬ��ҽ�� �Ĳ��λ�������Ϣ
        /// </summary>
        /// <param name="order1"></param>
        internal void RefreshTheOrderListData(clsBIHCanExecOrder order1)
        {
            string m_strID = order1.m_strRegisterID + "," + order1.m_intRecipenNo.ToString();
            string m_strTemp = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strTemp = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString();
                if (m_strID.Equals(m_strTemp))
                {
                    order.m_intATTACHTIMES_INT = order1.m_intATTACHTIMES_INT;
                    order.m_strEntrust = order1.m_strEntrust;

                    DataGridViewRow row1 = this.m_objViewer.m_dtvOrderList.Rows[i];
                    BindTheBihOrderListDetail(row1, order);
                }
            }
            refreshTheDataGridView();
        }

        internal void m_cmdBedIdKeySelect()
        {

            m_blBedIdKey = true;
            if (m_arrBedIdList != null && m_arrBedIdList.Count > 0)
            {
                if (this.m_objViewer.m_txtBedNo2.Tag is clsBIHBed)
                {
                    string m_strBedID = ((clsBIHBed)this.m_objViewer.m_txtBedNo2.Tag).m_strBedID;
                    for (int i = 0; i < m_arrBedIdList.Count; i++)
                    {
                        if (m_arrBedIdList[i].ToString().Equals(m_strBedID))
                        {
                            m_intBedIndex = i++;
                            break;
                        }

                    }
                }
            }
            else
            {
                m_intBedIndex++;
            }
            this.m_objViewer.m_txtBedNo2.Text = "";
            this.m_objViewer.m_txtBedNo2.Focus();
            SendKeys.Send("{ENTER}");

        }

        internal void GetTheStopOrder()
        {

            //���ҽ���Ƿ�ͣ��
            ArrayList m_arrOrderIDs = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                if ((order.m_intStatus == 1 || order.m_intStatus == 5) && !m_arrOrderIDs.Contains(order.m_strOrderID))
                {
                    m_arrOrderIDs.Add(order.m_strOrderID);
                }
            }
            ArrayList m_strOrders = GetTheStopOrders(m_arrOrderIDs);
            if (m_strOrders != null && m_strOrders.Count > 0)
            {
                string m_strErrMessage = "";
                for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
                {
                    clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                    if (m_strOrders.Contains(order.m_strOrderID))
                    {
                        if (!m_strErrMessage.Contains(order.m_strName))
                        {
                            m_strErrMessage += order.m_strName + "\r\n";
                        }
                        this.m_objViewer.m_dtvOrderList.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Brown;
                    }
                }
                if (!m_strErrMessage.Trim().Equals(""))
                {
                    m_strErrMessage = "����ҽ��������շ���Ŀͣ�û�ҩƷͣҩ!" + "\r\n" + m_strErrMessage;
                    MessageBox.Show(m_strErrMessage, "ͣ����ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            /*<==================================*/
        }

        /// <summary>
        /// ���ص�ǰͣ�û�ͣҩ��ҽ����ˮ����
        /// </summary>
        /// <param name="m_arrOrderIDs"></param>
        /// <returns></returns>
        private ArrayList GetTheStopOrders(ArrayList m_arrOrderIDs)
        {
            ArrayList m_arrStopOrderIds = new ArrayList();
            if (m_arrOrderIDs.Count <= 0)
            {
                return m_arrStopOrderIds;
            }
            string[] m_strOrders = (string[])m_arrOrderIDs.ToArray(typeof(string));
            DataTable m_dtOrderSign = null;
            this.m_objManage.m_lngGetOrderStopSign(m_strOrders, out m_dtOrderSign);
            if (m_dtOrderSign != null)
            {
                string orderid_chr = "";
                string STATUS_INT = "";//(������Ŀ״̬ 0-ͣ�� 1-����)
                string IFSTOP_INT = "";//ͣ�ñ�־ 1-ͣ�� 0-����
                string ITEMSRCTYPE_INT = "";//��Ŀ��Դ����1��ҩƷ��
                string IPNOQTYFLAG_INT = "";//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                bool m_blStop = false;
                for (int i = 0; i < m_dtOrderSign.Rows.Count; i++)
                {
                    m_blStop = false;
                    DataRow row = m_dtOrderSign.Rows[i];
                    orderid_chr = row["orderid_chr"].ToString().Trim();
                    STATUS_INT = row["STATUS_INT"].ToString().Trim();//(������Ŀ״̬ 0-ͣ�� 1-����)
                    IFSTOP_INT = row["IFSTOP_INT"].ToString().Trim();//ͣ�ñ�־ 1-ͣ�� 0-����
                    ITEMSRCTYPE_INT = row["ITEMSRCTYPE_INT"].ToString().Trim();//��Ŀ��Դ����1��ҩƷ��
                    IPNOQTYFLAG_INT = row["IPNOQTYFLAG_INT"].ToString().Trim();//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                    if ((STATUS_INT.Equals("0") || IFSTOP_INT.Equals("1")))
                    {
                        if (!m_blStopControl)
                        {
                            m_blStop = true;
                        }
                    }

                    if (!m_blDeableMedControl)
                    {
                        if (ITEMSRCTYPE_INT.Equals("1") && IPNOQTYFLAG_INT.Equals("1"))
                        {
                            m_blStop = true;
                        }
                    }


                    if (m_blStop)
                    {
                        if (!m_arrStopOrderIds.Contains(orderid_chr))
                        {
                            m_arrStopOrderIds.Add(orderid_chr);
                        }
                    }

                }

            }
            return m_arrStopOrderIds;

        }


        internal void setTheCurrentPatient()
        {
            if (this.m_objViewer.m_dtvOrderList.SelectedRows.Count <= 0)
            {
                return;
            }
            clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.SelectedRows[0].Tag;
            BindthePatient(order);
        }
    }
}
