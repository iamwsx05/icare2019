using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using com.digitalwave.iCare.gui.HIS;
using com.digitalwave.iCare.gui.LIS;
using weCare.Core.Entity;
using iCare;
using System.Xml;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// ҽ��ִ����
    /// </summary>
    class clsCtl_OrderExecute : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ��������
        internal com.digitalwave.iCare.BIHOrder.frmExecuteOrdersProgress objFrmExecuteOrdersProgress = null;
        clsDcl_ExecuteOrder m_objManage = null;
        clsDcl_InputOrder m_objInputOrder = null;
        DataTable m_dtPatients;
        DataTable m_dtChargeList;
        DataTable m_dtExecOrder;
        DataTable m_dtChargeMoney;
        DataTable m_dtPrepay;
        /// <summary>
        /// ���״̬ѡ����0-ȫ��,1-δ���,2-�����
        /// </summary>
        int m_intState = -1;
        /// <summary>
        /// ҽ��LOAD״̬
        /// </summary>
        public bool EOstate = false;
        /// <summary>
        /// ����LOAD״̬
        /// </summary>
        public bool EPstate = false;
        /// <summary>
        /// �����շ�״̬
        /// </summary>
        public bool ECstate = false;
        /// <summary>
        /// ҽ�������б�
        /// </summary>
        public Hashtable m_htOrderCate = new Hashtable();
        /// <summary>
        /// סԺ�������ñ�VO
        /// </summary>
        public clsSPECORDERCATE m_objSpecateVo = null;
        /// <summary>
        /// ִ���Ƿ���Ҫ���
        /// </summary>
        bool m_blExeConfirm = false;
        /// <summary>
        /// ��ǰѡ��ִ��ҽ���Ĳ����б��ǼǺţ�
        /// </summary>
        public ArrayList m_arrRegisterID = new ArrayList();

        /// <summary>
        /// ִ�е���ִ���ֶ�(0-�ǲ�ִ��,1-��ִ��)
        /// </summary>
        public int m_intRepairEveVo = 0;
        /// <summary>
        /// ִ�е���ִ�д���(0-�ǲ�ִ��,1-��ִ��)
        /// </summary>
        public int m_intRepairEveVoCount = 0;
        public Hashtable m_htReExecute = new Hashtable();
        /// <summary>
        /// ϵͳ������(ICARE����) 1008 סԺȷ�ϼ������̶�Ӧ�����ID ������������ݸ���
        /// </summary>
        public string m_strPARMVALUE_VCHR = "";
        /// <summary>
        /// ϵͳ������(ICARE����) 0013 ������ϴ��۷�Ʊ���� ������������ݸ���
        /// </summary>
        public string m_strLisPARMVALUE_VCHR = "";
        /// <summary>
        /// �Ƿ���������,1 ����,0 ������ 
        /// </summary>
        internal int intDiffPriceOn = 0;
        /// <summary>
        /// ҩƷ��������,��Ӧϵͳ����,9003
        /// </summary>
        public string m_strDiffMedicineType = string.Empty;
        public bool m_blFirstLoad = false;
        /// <summary>
        /// ҩƷ�����VO(ִ��ǰ����ȡ�������ϸ���и�����ҩƷ���ۿ�棬ִ�к����ڸ�����Ӧ���ε����ۿ��) 
        /// </summary>
        private clsDsStorageVO[] m_objDsStorageVOArr = null;

        /// <summary>
        /// ������뵥�Ķ�Ӧ��ϵ��TypeID����Ӧ������
        /// </summary>
        private Dictionary<string, string> m_gdctApplyRlt = null;

        /// <summary>
        /// �Ƿ�ʹ�ö�ͯ�۸� 9015
        /// </summary>
        bool isUseChildPrice { get; set; }

        System.Drawing.Color FeedColor = System.Drawing.Color.Green; //��ҪƤ�Ե���Ŀ��ɫ
        ArrayList m_arrBedIdList = null;//Ϊ��ݼ����°�����ѡ��ҽ����
        public int m_intBedIndex = -1;//Ϊ��ݼ����°�����ѡ��ҽ���� -1Ϊ���ǿ�ݼ���ʽ������Ϊ��ݼ���ʽ
        public bool m_blBedIdKey = false;//Ϊ��ݼ����°�����ѡ��ҽ����(false-���ǿ�ݼ���ʽ,true-��ݼ���ʽ)
        public System.Collections.Generic.List<clsBIHCanExecOrder> m_glstExecOrderList = null; //������ִ��ҽ����ʱ�򣬰������ܹ�ִ�е�ҽ�������������ArrayList���棬������clsBIHCanExecOrder
        //�������´���
        #endregion
        /// <summary>
        /// ί��
        /// </summary>
        delegate void filltheDatagridview();

        #region ���캯��
        public clsCtl_OrderExecute()
        {
            m_objManage = new clsDcl_ExecuteOrder();
            m_objInputOrder = new clsDcl_InputOrder();

        }
        #endregion
        // private frmOrderExecute objOrderExecute = new frmOrderExecute();
        #region ���캯�� 2010/8/19
        //public clsCtl_OrderExecute(ref frmOrderExecute p_objOrderExecute)
        //{
        //    objOrderExecute = p_objOrderExecute;
        //}
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmOrderExecute m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmOrderExecute)frmMDI_Child_Base_in;

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
                refreshTheData();
                this.m_objViewer.m_cboCode.Focus();
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

            m_intState = getState();

            #region LOAD ����
            m_lngGetCanExecuteOrderByArea();//ͨ�������ҳ��ÿ������п�����Ҫִ�е�ҽ����������ȫ�ֱ���m_dtExecOrder

            System.Collections.Generic.List<string> m_glstRegisterid_chr;

            //�ٶȲ�ѯ Ҫ���� 
            GetTheExecuteRegisterID(out m_glstRegisterid_chr);//ɾ���ظ��Ĳ�����ˮ�ţ��������㣬���漰���ݿ�
            m_lngGetPatientDTByArea(m_glstRegisterid_chr);//������һ��������õ�û���ظ�������ˮ�ŵĲ��˺����飬��ȡ���������Ϣ����Щ���˾���ҽ��������Ҫִ��
            m_lngGetChargeByArea(m_glstRegisterid_chr);

            //��ִ�е�ִ��ҽ����ͳ��
            if (this.m_objViewer.m_chkReExcute.Checked == true)
            {
                m_lngGetReExecute();
            }
            #endregion

            bool m_blBed = false;
            if (this.m_objViewer.m_cboCode.SelectedIndex == 1)//Ĭ��ѡ����ʾȫ����ֵΪ0�����ˣ�ĳ�Ų�������ֵΪ1
            {
                m_blBed = true;
            }
            else
            {
                m_blBed = false;
            }

            refreshTheDataByBed(m_blBed);
            ControlTheButton();
        }

        private void GetTheExecuteRegisterID(out System.Collections.Generic.List<string> m_glstRegisterid_chr)
        {
            m_glstRegisterid_chr = new System.Collections.Generic.List<string>();
            if (m_dtExecOrder != null && m_dtExecOrder.Rows.Count > 0)
            {
                string strREGISTERID_CHR = "";
                int intExecOrderRowsCount = m_dtExecOrder.Rows.Count;
                for (int i = 0; i < intExecOrderRowsCount; i++)
                {
                    strREGISTERID_CHR = m_dtExecOrder.Rows[i]["REGISTERID_CHR"].ToString().Trim();
                    if (!m_glstRegisterid_chr.Contains(strREGISTERID_CHR))
                    {
                        m_glstRegisterid_chr.Add(strREGISTERID_CHR);
                    }
                }
            }
        }

        /// <summary>
        /// �̻߳�ȡҽ����Ϣ
        /// </summary>
        public void m_lngGetCanExecuteOrderByArea()
        {
            long lngRes = m_objManage.m_lngGetExecOrderDTByArea((string)m_objViewer.m_txtArea.Tag, out m_dtExecOrder);
            EOstate = true;
        }

        /// <summary>
        /// �̻߳�ȡҽ����Ϣ
        /// </summary>
        public void m_lngGetCanExecuteOrderByBed()
        {
            long lngRes = m_objManage.m_lngGetExecOrderDTByAreaBed((string)m_objViewer.m_txtArea.Tag, ((clsBIHBed)m_objViewer.m_txtBedNo2.Tag).m_objPatient.m_strRegisterID, out m_dtExecOrder);
            EOstate = true;
            clsBIHCanExecOrder[] arrExecOrder;
            DataView myDataView = GetExecDataView(m_dtExecOrder, m_intState);
            filltheExecOrderTable(myDataView, out arrExecOrder);
            BindTheBihOrderList(arrExecOrder);
        }

        /// <summary>
        /// �̻߳�ȡ������Ϣ
        /// </summary>
        public void m_lngGetPatientDTByArea()
        {
            long lngRes = m_objManage.m_lngGetPatientDTByArea((string)m_objViewer.m_txtArea.Tag, out m_dtPatients);
            EPstate = true;

        }
        /// <summary>
        /// �̻߳�ȡ������Ϣ��������һ��������õ�û���ظ�������ˮ�ŵĲ��˺����飬��ȡ���������Ϣ����Щ���˾���ҽ��������Ҫִ��
        /// </summary>
        public void m_lngGetPatientDTByArea(System.Collections.Generic.List<string> m_glstRegisterid_chr)
        {
            long lngRes = m_objManage.m_lngGetPatientDTByArea(m_glstRegisterid_chr, out m_dtPatients);
            EPstate = true;

        }



        /// <summary>
        /// �̻߳�ȡ������Ϣ
        /// </summary>
        public void m_lngGetPatientDTByAreaBed()
        {
            long lngRes = m_objManage.m_lngGetPatientDTByAreaBed((string)m_objViewer.m_txtArea.Tag, ((clsBIHBed)m_objViewer.m_txtBedNo2.Tag).m_objPatient.m_strRegisterID, out m_dtPatients);
            EPstate = true;

        }

        /// <summary>
        /// �̻߳�ȡ������Ϣ
        /// </summary>
        public void m_lngGetChargeByArea()
        {
            long lngRes = m_objManage.m_lngGetChargeByArea((string)m_objViewer.m_txtArea.Tag, out m_dtChargeList, out m_dtChargeMoney, out m_dtPrepay);
            ECstate = true;

        }

        /// <summary>
        /// �̻߳�ȡ������Ϣ
        /// </summary> 
        public void m_lngGetChargeByArea(System.Collections.Generic.List<string> m_glstRegisterid_chr)
        {
            long lngRes = m_objManage.m_lngGetChargeByArea((string)m_objViewer.m_txtArea.Tag, m_glstRegisterid_chr, out m_dtChargeList, out m_dtChargeMoney, out m_dtPrepay);
            ECstate = true;

        }

        /// <summary>
        /// �̻߳�ȡ������Ϣ
        /// </summary>
        public void m_lngGetChargeByAreaBed()
        {
            long lngRes = m_objManage.m_lngGetChargeByAreaBed((string)m_objViewer.m_txtArea.Tag, ((clsBIHBed)m_objViewer.m_txtBedNo2.Tag).m_objPatient.m_strRegisterID, out m_dtChargeList, out m_dtChargeMoney);
            ECstate = true;

        }
        /// <summary>
        /// ����߳�״̬ �жϵ�ǰ���˱�,ҽ����,���ñ��Ƿ���LOAD�ɹ�
        /// </summary>
        public void TestTheTableLoadState()
        {

            while (!ECstate || !EPstate || !EOstate)
            {
            }
            ECstate = false;
            EPstate = false;
            EOstate = false;
            filltheDatagridview mygate = new filltheDatagridview(OrderListSelect);
            this.m_objViewer.Invoke(mygate);
        }

        private DataView GetExecDataView(DataTable m_dtExecOrder, int m_intState)
        {
            DataView myDataView = new DataView(m_dtExecOrder);
            switch (m_intState)
            {
                case 0:
                    myDataView = m_dtExecOrder.DefaultView;
                    break;
                    //case 1:
                    //    myDataView.RowFilter = " execCount=0 ";
                    //    break;
                    //case 2:
                    //    myDataView.RowFilter = " execCount>0";
                    //    break;
            }
            return myDataView;
        }

        /// <summary>
        /// ���浼�뵱ǰ��������λ ��Ӧ������
        /// </summary>
        internal void LoadTheDate2()
        {
            if (m_objViewer.m_txtArea.Tag == null || ((string)m_objViewer.m_txtArea.Tag).Trim().Equals(""))
            {
                MessageBox.Show("��������ѡ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtArea.Focus();
                return;
            }
            if (m_objViewer.m_txtBedNo2.Tag == null || ((clsBIHBed)m_objViewer.m_txtBedNo2.Tag).m_strBedID.Trim().Equals(""))
            {
                MessageBox.Show("��λ����ѡ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtBedNo2.Focus();
                return;
            }
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            clsBIHCanExecOrder[] arrExecOrder;
            m_intState = getState();
            #region ��̨�����߳�

            //this.m_objViewer.backgroundWorker1.RunWorkerAsync();
            #endregion
            #region LOAD ����
            DoWork();
            m_lngGetCanExecuteOrderByBed();
            #endregion
            ControlTheButton();
            int cout = GetWaitCfPersonCout();
            this.m_objViewer.m_lblNewOrderCount.Text = "���� " + cout.ToString() + " ������ҽ����Ҫִ��";
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

        #region ��λ���¼�
        internal void m_txtBedNo2FindItem(string strFindCode, ListView lvwList)
        {


            this.m_objViewer.m_txtBedNo2.Tag = null;
            //clsBIHOrderService m_objService = new clsBIHOrderService();
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
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
            //long ret = m_objService.m_lngGetBihBedByArea(strAreaID, strBedNo, out arrBed);
            ArrayList m_arrBedList = GetTheBed(strFindCode);

            if (m_arrBedList.Count > 0)
            {
                arrBed = (clsBIHBed[])m_arrBedList.ToArray(typeof(clsBIHBed));
            }
            if (arrBed != null)
            {
                if (arrBed.Length == 0)
                {
                    MessageBox.Show("��ǰ����û�д�λ��������ѡ����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    //    upName=arrBed[i].m_objPatient.m_strAreaName;
                    //}

                    //ListViewItem objItem = new ListViewItem(upName);

                    //objItem.SubItems.Add(arrBed[i].m_strBedName);
                    ListViewItem objItem = new ListViewItem(arrBed[i].m_strBedName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strPatientName);
                    objItem.SubItems.Add(arrBed[i].m_objPatient.m_strSex);
                    /*<----------------------*/
                    //objItem.Tag = arrBed[i].m_strBedID;
                    objItem.Tag = arrBed[i];
                    lvwList.Items.Add(objItem);

                }

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
                    m_objBed.m_strAreaID = order.m_strCURAREAID_CHR;
                    m_objBed.m_strBedID = order.m_strBedID;
                    //m_objBed.m_strBedName = order.m_strCURBEDName;
                    m_objBed.m_strBedName = order.m_strBedName;
                    m_objBed.m_objPatient = new clsBIHPatientInfo();
                    m_objBed.m_objPatient.m_strPatientName = order.m_strPatientName;
                    m_objBed.m_objPatient.m_strAreaName = order.m_strCURAREAName;
                    m_objBed.m_objPatient.m_strSex = order.m_strPatientSex;
                    m_arrBedList.Add(m_objBed);

                }
            }
            return m_arrBedList;
        }

        internal void m_txtBedNo2InitListView(ListView lvwList)
        {
            //lvwList.Columns.Add("��  ��", 100, HorizontalAlignment.Left);
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
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                refreshTheDataByBed(true);
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }

        internal void refreshTheDataByBed(bool m_blBed)
        {
            if (this.m_objViewer.m_cboCode.SelectedIndex == 1 && m_blBed == true)//��ʾ�������˵�ҽ��
            {
                if (this.m_objViewer.m_txtBedNo2.Tag is clsBIHBed)
                {
                    DataView myDataView = GetExecDataView(m_dtExecOrder, m_intState);
                    DataTable m_dtData = myDataView.ToTable();
                    myDataView = new DataView(m_dtData);
                    myDataView.RowFilter = "bedid_chr='" + ((clsBIHBed)this.m_objViewer.m_txtBedNo2.Tag).m_strBedID + "'";
                    // myDataView.Sort = "FLAG_INT";
                    myDataView.Sort = "code_chr,recipeno_int,orderid_chr asc";
                    clsBIHCanExecOrder[] arrExecOrder;
                    filltheExecOrderTable(myDataView, out arrExecOrder);
                    BindTheBihOrderList(arrExecOrder);
                }
            }
            else if (m_blBed == false)//��ʾȫ�����˵�ҽ��
            {
                DataView myDataView = GetExecDataView(m_dtExecOrder, m_intState);//m_dtExecOrder��¼�ò���������Ҫִ�е�ȫ��ҽ��
                myDataView.Sort = "code_chr,recipeno_int,orderid_chr asc";
                clsBIHCanExecOrder[] arrExecOrder;
                filltheExecOrderTable(myDataView, out arrExecOrder);//�Ѹò�����Ҫִ�е�ҽ����ר��������arrExecOrder
                BindTheBihOrderList(arrExecOrder);
            }

            if (this.m_objViewer.m_blAutoStopAlert)
            {
                GetTheStopOrder();
            }
        }

        internal void GetTheStopOrder()
        {

            //���ҽ���Ƿ�ͣ��
            ArrayList m_arrOrderIDs = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                if (!m_arrOrderIDs.Contains(order.m_strOrderID))
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
                        if (!this.m_objViewer.m_blStopControl)
                        {
                            m_blStop = true;
                        }
                    }

                    if (!this.m_objViewer.m_blDeableMedControl)
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

        #endregion

        #region ��ǰҽ��ִ�м�¼ѡ���¼�����
        /// <summary>
        /// ��ǰҽ��ִ�м�¼ѡ���¼�����
        /// </summary>
        internal void OrderListSelect()
        {
            if (this.m_objViewer.blnFormLoad == false)
            {
                if (this.m_objViewer.m_dtvOrderList.CurrentCell != null && this.m_objViewer.m_dtvOrderList.RowCount > 0)
                {
                    clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[this.m_objViewer.m_dtvOrderList.CurrentCell.RowIndex].Tag;
                    //******testing**********************
                    //if (order != null)
                    //{
                    //    string strRegisterID = order.m_strRegisterID;
                    //    System.Collections.ArrayList arrRegisterID = new ArrayList();
                    //    arrRegisterID.Add(strRegisterID);
                    //    m_lngGetChargeByArea(arrRegisterID);
                    //}

                    //************************************
                    if (order != null)
                    {
                        if (m_dtPatients != null && m_dtPatients.Rows.Count > 0)
                        {
                            fillthePatient(order);
                            filltheChargeList(order);
                            decimal m_decSum = m_objInputOrder.GettheChargeSum(order, m_dtChargeList);
                            this.m_objViewer.m_txtChargeSum.Text = "�����ܼƣ�" + m_decSum.ToString();
                            m_decSum = m_objInputOrder.GetTheSameChargeSum(order, m_dtChargeList);
                            this.m_objViewer.m_txtSameCharge.Text = "ͬ�������ܼƣ�" + m_decSum.ToString();

                        }
                        ControlTheButton();
                        TheSameNoRowSelect(order);
                    }
                }
                else
                {
                    clsBIHPatientInfo m_objPatient = new clsBIHPatientInfo();
                    BindTheObjPatinet(m_objPatient);
                    this.m_objViewer.m_txtChargeSum.Text = "�����ܼƣ�";
                    this.m_objViewer.m_txtSameCharge.Text = "ͬ�������ܼƣ�";
                    //this.m_objViewer.m_lblNewOrderCount.Text = "���� ������ҽ����Ҫִ��";
                }
            }
        }




        #endregion

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

        #region ��˼�������ť����
        /// <summary>
        /// ��˼�������ť����
        /// </summary>
        private void ControlTheButton()
        {
            bool m_blState = false;
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
                        if (CanExecOrder((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag) == false)
                        {
                            m_blState = false;
                            break;
                        }
                        else
                        {
                            m_blState = true;

                        }
                    }
                }

            }
            //this.m_objViewer.m_cmdToCommit.Enabled = m_blState;

        }
        #endregion

        #region ��ǰҽ����¼�ı�ʱ���˸ı��¼�
        /// <summary>
        /// ��ǰҽ����¼�ı�ʱ���˸ı��¼�
        /// </summary>
        /// <param name="order"></param>
        private void fillthePatient(clsBIHCanExecOrder order)
        {
            if (m_dtPatients != null && m_dtPatients.Rows.Count > 0)
            {
                string m_strInHospitalNoOld = "";
                string m_strInHospitalNoNow = "";
                m_strInHospitalNoOld = this.m_objViewer.m_ctlPatient.m_txtRegisterid.Text.Trim();
                m_strInHospitalNoNow = order.m_strRegisterID.Trim();
                if (m_strInHospitalNoOld.Equals(m_strInHospitalNoNow))
                {
                    return;
                }

                DataView myDataView = new DataView(m_dtPatients);
                myDataView.RowFilter = "registerid_chr='" + order.m_strRegisterID + "'";
                if (myDataView.Count <= 0)
                {
                    return;
                }
                clsBIHPatientInfo m_objPatient;
                m_mthGetPatientInfoFromDateTable(myDataView, out m_objPatient);
                this.m_objViewer.m_ctlPatient.m_objPatient = m_objPatient;
                this.m_objViewer.m_ctlPatient.m_mthShowPatient();

                //BindTheObjPatinet(m_objPatient);


            }
        }

        public bool m_IsInsPatient(string p_strPayTypeID)
        {
            string str;
            string str2;
            XmlDocument document;
            XmlNode node;
            bool flag;
            if (p_strPayTypeID == null || p_strPayTypeID.Trim() == "")
            {
                return false;
            }
            try
            {
                str = "/��Ա���/��¼[@PAYTYPEID_CHR='" + p_strPayTypeID + "']";
                str2 = "";
                document = new XmlDocument();
                document.Load("��Ա���.XML");
                flag = document.SelectSingleNode(str).Attributes["ISYB"].Value.Trim() == "1";
            }
            catch
            {
                flag = false;
            }
            return false;
        }

        /// <summary>
        /// �𶨲��˽�����Ϣ
        /// </summary>
        /// <param name="m_objPatient"></param>
        private void BindTheObjPatinet(clsBIHPatientInfo m_objPatient)
        {
            this.m_objViewer.m_ctlPatient.m_txtRegisterid.Text = m_objPatient.m_strRegisterID;
            this.m_objViewer.m_ctlPatient.m_txtInHospitalNo.Text = m_objPatient.m_strInHospitalNo;
            this.m_objViewer.m_ctlPatient.m_txtName.Text = m_objPatient.m_strPatientName;
            this.m_objViewer.m_ctlPatient.m_txtSex.Text = m_objPatient.m_strSex;
            this.m_objViewer.m_ctlPatient.m_txtPayType.Text = m_objPatient.m_strPayTypeName;
            if (m_objPatient.m_dtInHospital != DateTime.MinValue)
            {
                this.m_objViewer.m_ctlPatient.m_txtInHospitalDate.Text = m_objPatient.m_dtInHospital.ToString("yyyy-MM-dd");
                TimeSpan ts = DateTime.Now.Date - m_objPatient.m_dtInHospital.Date;
                this.m_objViewer.m_ctlPatient.m_txtInDays.Text = clsConverter.ToString(ts.Days);

            }
            else
            {
                this.m_objViewer.m_ctlPatient.m_txtInHospitalDate.Text = "";
                this.m_objViewer.m_ctlPatient.m_txtInDays.Text = "";

            }
            this.m_objViewer.m_ctlPatient.m_txtState.Text = m_objPatient.m_strInpatientState;


            this.m_objViewer.m_ctlPatient.m_txtPreMoney.Text = m_objPatient.m_decPreMoney.ToString("0.00");
            this.m_objViewer.m_ctlPatient.m_txtUseMoney.Text = m_objPatient.m_decPreUseMoney.ToString("0.00");
            this.m_objViewer.m_ctlPatient.m_txtPrePayMoney.Text = m_objPatient.m_decPrePayMoney.ToString("0.00");
            this.m_objViewer.m_ctlPatient.m_txtClearMoney.Text = Convert.ToDecimal(m_objPatient.m_decClearMoney + m_objPatient.m_decVerticalMoney).ToString("0.00");
            this.m_objViewer.m_ctlPatient.m_txtREMARKNAME.Text = m_objPatient.m_strREMARKNAME_VCHR + m_objPatient.m_strDES_VCHR.Trim();
            /*<=======================================================*/
            this.m_objViewer.m_ctlPatient.m_txtLIMITRATE_MNY.Text = m_objPatient.m_dblLIMITRATE_MNY.ToString("0.00");
            this.m_objViewer.m_ctlPatient.m_txtDiagnose.Text = m_objPatient.m_strDiagnose;

            //#region	�����ʾ	glzhang	2005.10.24
            //string strDiagnose = "\n   ������ϣ�" + m_objPatient.m_strMzdiagnose_vchr + "   \n";
            //strDiagnose += "\n   ��Ժ��ϣ�ICD10����" + m_objPatient.m_strDiagnose + "   \n";
            //if (m_objPatient.m_strDiagnose_vchr.Length > 0)
            //{
            //    strDiagnose += "\n   ��Ժ��ϣ�ҽ������" + m_objPatient.m_strDiagnose_vchr + "   \n";
            //}
            //else
            //{
            //    strDiagnose += "\n   ��Ժ��ϣ�ҽ��������   \n";
            //}
            //toolTipDiagnose.SetToolTip(m_txtDiagnose, strDiagnose);
            //#endregion

            //�����︳ֵ�Ƿ�ҽ������
            try
            {
                this.m_objViewer.m_ctlPatient.m_chkIsMedicareMan.Checked = this.m_IsInsPatient(m_objPatient.m_strPayTypeID.Trim());  //new com.digitalwave.iCare.middletier.HIS.clsBIH_INS_Compute().m_IsInsPatient(m_objPatient.m_strPayTypeID.Trim());
            }
            catch
            {

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
            m_objViewer.m_dtvChangeList.Rows.Clear();
            int k = 0;
            if (arrExecOrder != null)
            {
                //��һ����¼�Ĳ�����ˮ��
                string m_strPreRegister = "";
                decimal m_dmlOneUse = 0;//��һ�ε�����
                //���ϲ�ִ�д���
                string m_strRecute = "";
                bool m_blCheck = false;     // ��,��,��, 
                int m_intState = 0;         // 0-ȫ��ҽ����1-��ҽ���ڷ���2-��ҽ���ǿڷ���3-��ҽ���ڷ���4-��ҽ���ǿڷ�
                m_intState = this.m_objViewer.m_cboState.SelectedIndex;
                for (int i = 0; i < arrExecOrder.Length; i++)
                {
                    m_strRecute = "";
                    //�жϵ�ǰҽ���Ƿ��ִ��
                    if (this.m_objViewer.m_rdoNOT.Checked)//û��ִ�е�ҽ��
                    {
                        if (CanExecOrder(arrExecOrder[i]) == false)
                        {
                            continue;
                        }
                    }
                    else if (this.m_objViewer.m_rdoAll.Checked)
                    {
                        if (CanListOrder(arrExecOrder[i]) == false)
                        {
                            continue;
                        }
                    }
                    m_blCheck = false;
                    if (this.m_objViewer.m_chkLong.Checked == true)//���ѡ��ִ�г�����������ҽ�������ǳ���������ִ��
                    {
                        if (arrExecOrder[i].m_intExecuteType == 1)
                        {
                            m_blCheck = true;
                        }
                    }
                    if (this.m_objViewer.m_chkShort.Checked == true)////���ѡ��ִ��������������ҽ������������������ִ��
                    {
                        if (arrExecOrder[i].m_intExecuteType == 2)
                        {
                            m_blCheck = true;
                        }
                    }
                    if (this.m_objViewer.m_chkOut.Checked == true)//���ѡ��ִ�г�Ժ��ҩ��������ҽ�������ǳ�Ժ��ҩ������ִ��
                    {
                        if (arrExecOrder[i].m_intExecuteType == 3)
                        {
                            m_blCheck = true;
                        }
                    }
                    if (m_blCheck == false)
                    {
                        continue;
                    }
                    // 0-ȫ��ҽ����1-��ҽ���ڷ���2-��ҽ���ǿڷ���3-��ҽ���ڷ���4-��ҽ���ǿڷ�
                    switch (m_intState)
                    {
                        case 0:
                            m_blCheck = true;
                            break;
                        case 1:
                            if (arrExecOrder[i].m_intStatus == 5 && arrExecOrder[i].MedProperty == 1)
                            { }
                            else
                            {
                                continue;
                            }
                            break;
                        case 2:
                            if (arrExecOrder[i].m_intStatus == 5 && arrExecOrder[i].MedProperty != 1)
                            { }
                            else
                            {
                                continue;
                            }
                            break;
                        case 3:
                            if (arrExecOrder[i].m_intStatus != 5 && arrExecOrder[i].MedProperty == 1)
                            { }
                            else
                            {
                                continue;
                            }
                            break;
                        case 4:
                            if (arrExecOrder[i].m_intStatus != 5 && arrExecOrder[i].MedProperty != 1)
                            { }
                            else
                            {
                                continue;
                            }
                            break;
                    }
                    if (arrExecOrder[i].m_intRepair > 0 && arrExecOrder[i].m_intRepairCount > 0)
                    {
                        m_strRecute = " ��©ִ��" + arrExecOrder[i].m_intRepairCount + "��";
                    }
                    /*<=========================*/

                    // ��ţ���CheckBox��dtv_NO\����dtv_bedcode\����m_dtvLastName\����dtv_RecipeNo\ҽ����ҽ����ʽ��dtv_ExecuteType\���viewname_vchr
                    //\��Ŀ����dtv_Name\����dtv_Dosage\�÷�dtv_UseType\Ƶ��dtv_Freq\����dtv_Get\����dtv_ENTRUST\����ҽ��DOCTOR_VCHR\�ύʱ��m_dtvPOSTDATE_DAT\�����m_dtvCONFIRMER_VCHR\���ʱ�� m_dtvCONFIRM_DAT
                    m_objViewer.m_dtvOrderList.Rows.Add();
                    k = m_objViewer.m_dtvOrderList.RowCount - 1;
                    DataGridViewRow row1 = m_objViewer.m_dtvOrderList.Rows[m_objViewer.m_dtvOrderList.RowCount - 1];
                    row1.Cells["dtv_NO"].Value = Convert.ToString((k + 1));
                    if (m_objViewer.m_chkSelectAll.Checked == true)
                    {
                        row1.Cells["m_dtvselectCheck"].Value = "1";//ִ��ҽ��ʱ����Ҫ���������ֵ
                    }
                    else
                    {
                        row1.Cells["m_dtvselectCheck"].Value = "0";
                    }

                    if (m_strPreRegister.Trim().Equals(arrExecOrder[i].m_strRegisterID.Trim()))//��ǰ���˺���һ��������ͬһ���ˣ�����ʾ������
                    {
                        row1.Cells["dtv_bedcode"].Value = "";
                        row1.Cells["m_dtvLastName"].Value = "";
                        row1.Cells["dtv_DEPTNAME_VCHR"].Value = "";
                    }
                    else
                    {
                        row1.Cells["dtv_bedcode"].Value = arrExecOrder[i].m_strCURBEDName;
                        row1.Cells["m_dtvLastName"].Value = arrExecOrder[i].m_strPatientName;
                        row1.Cells["dtv_DEPTNAME_VCHR"].Value = arrExecOrder[i].m_strCURAREAName;
                        m_strPreRegister = arrExecOrder[i].m_strRegisterID;
                    }
                    //ҽ������
                    clsT_aid_bih_ordercate_VO p_objItem = null;
                    if (m_htOrderCate.Contains(arrExecOrder[i].m_strOrderDicCateID))
                    {
                        p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[arrExecOrder[i].m_strOrderDicCateID];
                    }
                    string m_strCase = "";
                    if (arrExecOrder[i].m_intStatus == 5)//����ˣ���ִ�е�ҽ��
                    {
                        m_strCase = "��";
                        row1.Cells["dtv_Case"].Style.ForeColor = System.Drawing.Color.Red;//��ҪƤ�Ե���Ŀ��ɫ
                    }
                    else if (arrExecOrder[i].m_intStatus == 2)//��ִ�й���ҽ��
                    {
                        m_strCase = "";
                    }
                    row1.Cells["dtv_Case"].Value = m_strCase;
                    if (arrExecOrder[i].m_intExecuteType == 1)//��������Ҫ��ʾ����
                    {
                        //��
                        row1.Cells["dtv_RecipeNo"].Value = " " + arrExecOrder[i].m_intRecipenNo2.ToString();
                    }
                    row1.Cells["dtv_ExecuteType"].Value = arrExecOrder[i].m_strExecuteTypeName;
                    row1.Cells["viewname_vchr"].Value = arrExecOrder[i].m_strOrderDicCateName;
                    row1.Cells["dtv_Name"].Value = arrExecOrder[i].m_strName;
                    row1.Cells["dtv_Dosage"].Value = arrExecOrder[i].m_dmlDosage.ToString() + arrExecOrder[i].m_strDosageUnit.ToString().Trim();
                    row1.Cells["dtv_UseType"].Value = arrExecOrder[i].m_strDosetypeName;
                    row1.Cells["dtv_Freq"].Value = arrExecOrder[i].m_strExecFreqName;
                    row1.Cells["dtv_Get"].Value = arrExecOrder[i].m_dmlGet.ToString() + arrExecOrder[i].m_strGetunit;
                    row1.Cells["dtv_ENTRUST"].Value = arrExecOrder[i].m_strEntrust;
                    row1.Cells["DOCTOR_VCHR"].Value = arrExecOrder[i].m_strDOCTOR_VCHR;
                    row1.Cells["m_dtvPOSTDATE_DAT"].Value = arrExecOrder[i].m_dtPostdate.ToString();
                    row1.Cells["m_dtvCONFIRMER_VCHR"].Value = arrExecOrder[i].m_strASSESSORFOREXEC_CHR;
                    row1.Cells["m_dtvCONFIRM_DAT"].Value = arrExecOrder[i].m_strASSESSORFOREXEC_DAT;
                    row1.Cells["CREATOR_CHR"].Value = arrExecOrder[i].m_strCreator;
                    if (arrExecOrder[i].PretestDays != 0) row1.Cells["pretestdays"].Value = arrExecOrder[i].PretestDays;

                    //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
                    if (!arrExecOrder[i].m_strPARTID_VCHR.Trim().Equals(""))
                    {
                        row1.Cells["dtv_method"].Value = arrExecOrder[i].m_strPARTNAME_VCHR;
                    }
                    else if (!arrExecOrder[i].m_strSAMPLEID_VCHR.Trim().Equals(""))
                    {
                        row1.Cells["dtv_method"].Value = arrExecOrder[i].m_strSAMPLEName_VCHR;
                    }
                    else
                    {
                        row1.Cells["dtv_method"].Value = "";
                    }
                    //˵��
                    row1.Cells["dtv_REMARK"].Value = arrExecOrder[i].m_strREMARK_VCHR.Trim();
                    #region ҽ�����ͽ��洦��
                    p_objItem = null;
                    //������ظ�������Ҫɾ��*************************
                    if (m_htOrderCate.Contains(arrExecOrder[i].m_strOrderDicCateID))
                    {
                        p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[arrExecOrder[i].m_strOrderDicCateID];
                    }
                    //*******************************
                    if (p_objItem != null)
                    {
                        if (!arrExecOrder[i].m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ����
                        {
                            if (p_objItem.m_intDOSAGEVIEWTYPE == 1)
                            {
                                //����
                                if (arrExecOrder[i].m_dmlDosage > 0)
                                {
                                    row1.Cells["dtv_Dosage"].Value = arrExecOrder[i].m_dmlDosage.ToString() + "" + arrExecOrder[i].m_strDosageUnit;
                                }
                                else
                                {
                                    row1.Cells["dtv_Dosage"].Value = "";

                                }
                            }
                            else
                            {
                                row1.Cells["dtv_Dosage"].Value = "";
                            }
                        }
                        else
                        {
                            row1.Cells["dtv_Dosage"].Value = "";
                        }
                        if (!arrExecOrder[i].m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))//������ҽ������ʾ����
                        {
                            if (p_objItem.m_intUSAGEVIEWTYPE == 1)
                            {
                                //�÷�
                                row1.Cells["dtv_UseType"].Value = arrExecOrder[i].m_strDosetypeName;
                            }
                            else
                            {
                                //�÷�
                                row1.Cells["dtv_UseType"].Value = "";
                            }
                        }
                        else
                        {
                            //�÷�
                            row1.Cells["dtv_UseType"].Value = "";
                        }
                        if (p_objItem.m_intExecuFrenquenceType == 1)
                        {
                            //Ƶ��
                            row1.Cells["dtv_Freq"].Value = arrExecOrder[i].m_strExecFreqName;
                        }
                        else
                        {
                            //������ʾʱ��ҽ�����е�Ϊ�޸ı�־=1ʱҲ��ʾ���� (0-��ͨ״̬,1-Ƶ���޸�)
                            if (arrExecOrder[i].m_intCHARGE_INT == 1)
                            {
                                row1.Cells["dtv_Freq"].Value = arrExecOrder[i].m_strExecFreqName;//Ƶ��
                            }
                            else
                            {
                                row1.Cells["dtv_Freq"].Value = "";//Ƶ��
                            }
                        }
                        if (p_objItem.m_intAPPENDVIEWTYPE_INT == 1 && (arrExecOrder[i].m_intStatus == 5 || (arrExecOrder[i].m_intStatus == 2 && arrExecOrder[i].m_dtStartDate == arrExecOrder[i].m_dtExecutedate)))//����˵�ĩִ�У����״�ִ�еĲ���ʾ���μ����εĺϼ���
                        {
                            //���Σ������ҽ����ҽ��ʱ��ָ���Ĳ��δ��������߻�ʿִ��ʱ��ָ���Ĳ��δ�����������������һ��ִ��ҽ����ʱ�򿪻���֣���ͬ������ִ��ҽ����ɵĲ�©
                            row1.Cells["ATTACHTIMES_INT"].Value = arrExecOrder[i].m_intATTACHTIMES_INT;
                            m_dmlOneUse = arrExecOrder[i].m_dmlOneUse * arrExecOrder[i].m_intATTACHTIMES_INT;
                        }
                        else
                        {
                            //����
                            row1.Cells["ATTACHTIMES_INT"].Value = "";
                            m_dmlOneUse = 0;
                        }

                        //����
                        if (p_objItem.m_intQTYVIEWTYPE_INT == 1)
                        {
                            if (arrExecOrder[i].m_dmlGet > 0)
                            {
                                row1.Cells["dtv_Get"].Value = arrExecOrder[i].m_dmlGet.ToString() + " " + arrExecOrder[i].m_strGetunit;
                            }
                            else
                            {
                                row1.Cells["dtv_Get"].Value = "";
                            }
                        }
                        else
                        {
                            //����
                            row1.Cells["dtv_Get"].Value = "";
                        }
                    }
                    else
                    {
                        //����
                        row1.Cells["dtv_Dosage"].Value = "";
                        //Ƶ��
                        row1.Cells["dtv_Freq"].Value = "";
                        //�÷�
                        row1.Cells["dtv_UseType"].Value = "";
                        //����
                        row1.Cells["ATTACHTIMES_INT"].Value = "";
                        //����
                        row1.Cells["dtv_Get"].Value = "";

                    }
                    #endregion
                    string m_strFeel = "";
                    if (arrExecOrder[i].m_intISNEEDFEEL == 1)
                    {

                        switch (arrExecOrder[i].m_intFEEL_INT)
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
                    }
                    //��Ժ��ҩ����
                    string m_strOUTGETMEDDAYS_INT = "";
                    //�����ֶεĿ���
                    if (arrExecOrder[i].m_strOrderDicCateID.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))//��ҩ�����߼�
                    {
                        row1.Cells["dtv_sum"].Value = arrExecOrder[i].m_intOUTGETMEDDAYS_INT.ToString() + "����" + Convert.ToString(arrExecOrder[i].m_dmlGet + m_dmlOneUse) + arrExecOrder[i].m_strGetunit;
                        m_strOUTGETMEDDAYS_INT = arrExecOrder[i].m_intOUTGETMEDDAYS_INT.ToString() + "��";
                    }
                    else
                    {
                        //����  N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
                        if (arrExecOrder[i].m_intExecuteType == 3)
                        {
                            row1.Cells["dtv_sum"].Value = arrExecOrder[i].m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(arrExecOrder[i].m_dmlGet + m_dmlOneUse) + arrExecOrder[i].m_strGetunit;
                            m_strOUTGETMEDDAYS_INT = arrExecOrder[i].m_intOUTGETMEDDAYS_INT.ToString() + "��";
                        }
                        else
                        {
                            row1.Cells["dtv_sum"].Value = "��" + Convert.ToString(arrExecOrder[i].m_dmlGet + m_dmlOneUse) + arrExecOrder[i].m_strGetunit;
                            m_strOUTGETMEDDAYS_INT = "";
                        }
                    }
                    //����
                    row1.Cells["dtv_Name"].Value = arrExecOrder[i].m_strName + " " + row1.Cells["dtv_Dosage"].Value.ToString() + " " + row1.Cells["dtv_UseType"].Value.ToString() + " " + row1.Cells["dtv_Freq"].Value.ToString() + m_strFeel + " " + m_strOUTGETMEDDAYS_INT + m_strRecute;

                    row1.Tag = arrExecOrder[i];
                }
            }
            //ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�
            m_mthRefreshSameReqNoColor();
            if (this.m_objViewer.m_dtvOrderList.RowCount > 0)
            {
                this.m_objViewer.m_dtvOrderList.CurrentCell = this.m_objViewer.m_dtvOrderList.Rows[0].Cells[3];
            }

        }

        /// <summary>
        /// ��ǰ�Ƿ���Ҫִ�е�ҽ��(true-����δִ�е�,false-δ����ִ�е�)
        /// </summary>
        /// <param name="arrExecOrder"></param>
        internal bool m_blBihOrderCanExecute()
        {
            clsBIHCanExecOrder[] arrExecOrder;
            DataView myDataView = GetExecDataView(m_dtExecOrder, m_intState);
            filltheExecOrderTable(myDataView, out arrExecOrder);

            int k = 0;
            bool m_blHave = false;
            if (arrExecOrder != null)
            {
                //��һ����¼�Ĳ�����ˮ��
                for (int i = 0; i < arrExecOrder.Length; i++)
                {
                    //�жϵ�ǰҽ���Ƿ��ִ��
                    if (CanListOrder(arrExecOrder[i]) == false)
                    {
                        continue;
                    }
                    m_blHave = true;
                    break;
                }
            }
            return m_blHave;
        }

        private void BindTheExecData(clsBIHCanExecOrder ExecOrder)
        {
            //m_intRepair��0-���ǲ�©��ҽ��������ҽ����,1-ֻ�ǲ�©��ҽ��,2-��©���ǲ�©��ҽ������������©ҽ����

            if (m_htReExecute.Contains(ExecOrder.m_strOrderID))//��������ִ�й��ģ����в��ν��в���
            {
                //������ҽ��ִ�й�һ�κ󣬲���ִ��
                if (ExecOrder.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
                {
                    return;
                }
                /*<=================================*/
                int m_intExecAllCount = int.Parse(m_htReExecute[ExecOrder.m_strOrderID].ToString());//����ҽ����ִ�еĴ������Ѵ��ڵ�ִ�е�����
                int ALLCount = 0;//��Ӧ�е�ִ����Ŀ
                int m_intTemp = 0;
                TimeSpan span;
                if (ExecOrder.m_intDays_Int <= 0)
                {
                    m_intTemp = 1;
                }
                else
                {
                    m_intTemp = ExecOrder.m_intDays_Int;
                }
                if (ExecOrder.m_dtStartDate != DateTime.MinValue)
                {
                    span = ExecOrder.m_dtToday - ExecOrder.m_dtStartDate;
                    ALLCount = span.Days / m_intTemp;
                    ALLCount += ExecOrder.m_intATTACHTIMES_INT + 1;//��һ��ִ�еĴ�����m_intATTACHTIMES_INT��ҽ����ҽ�����߻�ʿת��ҽ����ʱ���ֹ����Ĵ�����
                    //��ִ��Ƶ��ÿ2��ִ��һ��Ϊ���ӿ���
                    //�������3�죬Ӧ��ִ��2�Σ��������4�죬Ӧ��ִ��3��
                    if (span.Days % m_intTemp == 0)
                    {
                        ExecOrder.m_intRepairCount = ALLCount - m_intExecAllCount - 1;
                        if (ExecOrder.m_intRepairCount <= 0)
                        {
                            return;
                        }
                        ExecOrder.m_intRepair = 2;
                    }
                    else
                    {
                        ExecOrder.m_intRepairCount = ALLCount - m_intExecAllCount;
                        if (ExecOrder.m_intRepairCount <= 0)
                        {
                            return;
                        }
                        ExecOrder.m_intRepair = 1;
                    }
                }
            }
        }

        /// <summary>
        /// ˢ��ͬ��ҽ���ķ�����ɫ��������ͬ���ʵ��ֶ�,Ƥ�Խ����ɫ��ʾ
        /// </summary>
        public void m_mthRefreshSameReqNoColor()
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                DataGridViewRow objRow = this.m_objViewer.m_dtvOrderList.Rows[i];
                clsBIHCanExecOrder ExecOrder = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;

                if (ExecOrder.m_intISNEEDFEEL == 1)
                {
                    objRow.Cells["dtv_Name"].Style.ForeColor = FeedColor;//��ҪƤ�Ե���Ŀ��ɫ
                }
                if (i < this.m_objViewer.m_dtvOrderList.RowCount - 1)
                {
                    clsBIHCanExecOrder ExecOrder2 = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i + 1].Tag;
                    if ((ExecOrder.m_strRegisterID == ExecOrder2.m_strRegisterID) && (ExecOrder.m_intRecipenNo == ExecOrder2.m_intRecipenNo))
                    {
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_RecipeNo"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_ExecuteType"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_ENTRUST"].Value = "";
                        this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["ATTACHTIMES_INT"].Value = "";
                        if (ExecOrder.m_strOrderDicCateID.Equals(m_objSpecateVo.m_strMID_MEDICINE_CHR))//��ҩ�����߼�
                        {
                            this.m_objViewer.m_dtvOrderList.Rows[i + 1].Cells["dtv_REMARK"].Value = "";
                        }
                    }
                }

            }
            int cout = GetWaitCfPersonCout();
            this.m_objViewer.m_lblNewOrderCount.Text = "���� " + cout.ToString() + " ������ҽ����Ҫִ��";



        }

        private bool CanListOrder(clsBIHCanExecOrder clsBIHCanExecOrder)
        {


            switch (clsBIHCanExecOrder.m_intExecuteType)
            {
                case 1:
                    return CanExecOrder(clsBIHCanExecOrder);
                    break;
                case 2:
                    if (clsBIHCanExecOrder.m_intStatus == 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3:
                    if (clsBIHCanExecOrder.m_intStatus == 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
            }

            return false;
        }


        #endregion

        #region �жϵ�ǰҽ���Ƿ��ִ��
        /// <summary>
        /// �жϵ�ǰҽ���Ƿ��ִ��
        /// </summary>
        /// <param name="clsBIHCanExecOrder"></param>
        private bool CanExecOrder(clsBIHCanExecOrder m_BIHCanExecOrder)//m_BIHCanExecOrder��¼һ��ҽ��
        {
            //���ϲ�ִ�д���
            if (m_objViewer.m_chkReExcute.Checked == true)//�����©ִ��
            {
                BindTheExecData(m_BIHCanExecOrder);
                if (m_BIHCanExecOrder.m_intRepair > 0)
                {
                    return true;
                }
            }
            /*<==================================*/
            bool m_blCanExec = false;//�Ƿ����ִ��
            int m_intTemp;
            //״̬��Ϊ���ִ�л�ִ�е�Ϊ����ִ��
            if (m_BIHCanExecOrder.m_intStatus != 2 && m_BIHCanExecOrder.m_intStatus != 5)//2��5�������ҽ������ִ�У�������2��5������ʾ������ִ��
            {
                return false;
            }
            TimeSpan span;
            switch (m_BIHCanExecOrder.m_intExecuteType)
            {
                case 1://����
                    //�鿴�����Ƿ���ִ�й�
                    span = m_BIHCanExecOrder.m_dtExecutedate - m_BIHCanExecOrder.m_dtToday;
                    if (span.Days == 0)//��ʾ�����Ѿ�ִ����
                    {
                        return false;
                    }
                    /*<==========================*/
                    span = m_BIHCanExecOrder.m_dtToday - m_BIHCanExecOrder.m_dtStartDate;
                    if (span.Days < 0)//��ʾ��δ��Ҫ��ʼִ�и���ҽ������Ϊ��ǰ����С�ڿ�ʼִ��ʱ��
                    {
                        return false;
                    }
                    if (m_BIHCanExecOrder.m_intStatus == 5)//��ʾ����ִ��
                    {
                        return true;
                    }
                    else if (m_BIHCanExecOrder.m_intStatus == 2)
                    {
                        //������ҽ��ִ�й�һ�κ󣬲���ִ��
                        if (m_BIHCanExecOrder.m_strExecFreqID.Trim().Equals(m_objSpecateVo.m_strCONFREQID_CHR.Trim()))
                        {
                            return false;
                        }
                        /*<=================================*/
                    }
                    span = m_BIHCanExecOrder.m_dtFinishDate - m_BIHCanExecOrder.m_dtToday;//��Ϊҽ��ͣ��ʱ���ڽ���֮ǰ���ʴ��Ѿ�ֹͣҽ��������Ҫִ��
                    if ((m_BIHCanExecOrder.m_dtFinishDate != DateTime.MinValue) && (span.Days < 0))
                    {
                        return false;
                    }
                    //���ҽ��ִ��Ƶ�ʵĹ涨����x����ִ��y�Σ���ʿִ��ҽ����ʱ��һ�ι���x���ڵ�y�ζ�ִ���ˣ���y�εķ��ø������ˣ�������t_aid_recipefreq
                    if (m_BIHCanExecOrder.m_intDays_Int <= 0)//ҽ��û��ҽ��ִ��Ƶ�ʶ�Ӧ��һ��Ƶ��ʱ��εĻ�������Ĭ��ֵ��1�졣
                    {
                        m_intTemp = 1;
                    }
                    else
                    {
                        m_intTemp = m_BIHCanExecOrder.m_intDays_Int;
                    }
                    if (m_BIHCanExecOrder.m_dtStartDate != DateTime.MinValue)//������Ч��ҽ����ʼʱ��
                    {
                        span = m_BIHCanExecOrder.m_dtToday - m_BIHCanExecOrder.m_dtStartDate;
                        if (span.Days % m_intTemp == 0)//�������µ�ִ��ʱ���
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                case 2://����
                    if (m_BIHCanExecOrder.m_intStatus == 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 3://��ҩ
                    if (m_BIHCanExecOrder.m_intStatus == 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

            }
            return m_blCanExec;
        }
        #endregion

        #region �жϵ�ǰҽ���Ƿ�ɳ���ִ��
        /// <summary>
        /// �жϵ�ǰҽ���Ƿ�ɳ���ִ��
        /// </summary>
        /// <param name="clsBIHCanExecOrder"></param>
        private bool CanExecDrawOrder(clsBIHCanExecOrder m_BIHCanExecOrder)
        {

            bool m_blCanExecDraw = true;

            //if (m_BIHCanExecOrder.m_intExecCount <= 0)
            //{
            //    return false;
            //}

            return m_blCanExecDraw;
        }
        #endregion

        #region Ϊ����datagridview��ֵ
        /// <summary>
        /// Ϊ����datagridview��ֵ
        /// </summary>
        /// <param name="order"></param>
        private void filltheChargeList(clsBIHCanExecOrder order)
        {
            if (m_dtChargeList != null && m_dtChargeList.Rows.Count > 0)
            {
                this.m_objViewer.m_dtvChangeList.Rows.Clear();
                DataView myDataView = new DataView(m_dtChargeList);
                myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
                myDataView.Sort = "FLAG_INT";
                if (myDataView.Count <= 0)
                {
                    return;
                }
                clsChargeForDisplay[] m_arrObjItem;
                GetChargeListFromDateTable(myDataView, out m_arrObjItem);

                int k = 0;
                for (int i = 0; i < m_arrObjItem.Length; i++)
                {
                    if (m_arrObjItem[i].m_intCONTINUEUSETYPE_INT == 1 && order.m_intStatus == 2)//���ô���
                    {
                        continue;
                    }

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
                        // ���������
                        if (this.intDiffPriceOn == 1)
                            row1.Cells["TotalDiffCost"].Value = m_arrObjItem[i].m_dblDiffCostMoney;
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
            //objRow��¼�ò�����Ҫִ�е�����ҽ����ԭʼ��������m_dtExecorder
            //arrExecOrder��¼�ò�����Ҫִ�е�����ҽ��
            //ҽ��ִ�ж�������

            int intRowsCount = objRow.Count;
            if (intRowsCount <= 0)
            {
                arrExecOrder = new clsBIHCanExecOrder[0];
            }
            else
            {
                m_arrBedIdList = new ArrayList();
                arrExecOrder = new clsBIHCanExecOrder[intRowsCount];//�ò�����Ҫִ�е�ҽ��������
                DataRowView row = null;
                for (int i = 0; i < intRowsCount; i++)
                {
                    row = objRow[i];//������Ҫִ�е�ҽ��
                    arrExecOrder[i] = new clsBIHCanExecOrder();
                    arrExecOrder[i].m_strAREAID_CHR = row["AREAID_CHR"].ToString();//��ǰ�������ڲ���id
                    arrExecOrder[i].m_strBedID = row["bedid_chr"].ToString();//��ǰ�������ڴ�λID
                    arrExecOrder[i].m_strBedName = row["code_chr"].ToString().Trim();

                    arrExecOrder[i].m_strRegisterID = row["registerid_chr"].ToString().Trim();
                    arrExecOrder[i].m_strPatientName = row["LASTNAME_VCHR"].ToString().Trim();//����
                    arrExecOrder[i].m_strPatientSex = row["SEX_CHR"].ToString().Trim();//�Ա�

                    if (!row["RECIPENO_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intRecipenNo = int.Parse(row["RECIPENO_INT"].ToString().Trim()); //����
                    if (!row["RECIPENO2_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intRecipenNo2 = int.Parse(row["RECIPENO2_INT"].ToString().Trim()); //����

                    if (!row["EXECUTETYPE_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intExecuteType = int.Parse(row["EXECUTETYPE_INT"].ToString().Trim());//ҽ����ҽ����ʽ��
                    arrExecOrder[i].m_strOrderDicCateID = row["ordercateid_chr"].ToString().Trim();//���ID
                    arrExecOrder[i].m_strOrderDicCateName = row["viewname_vchr"].ToString().Trim();//�������
                    arrExecOrder[i].m_strName = row["NAME_VCHR"].ToString().Trim();//��Ŀ����
                    if (!row["Dosage_Dec"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_dmlDosage = decimal.Parse(row["Dosage_Dec"].ToString().Trim()); ;//����
                    arrExecOrder[i].m_strDosageUnit = row["dosageunit_chr"].ToString().Trim();//������λ
                    arrExecOrder[i].m_strExecFreqID = row["EXECFREQID_CHR"].ToString().Trim();//Ƶ��ID
                    arrExecOrder[i].m_strExecFreqName = row["EXECFREQNAME_CHR"].ToString().Trim();////Ƶ������
                    if (!row["GET_DEC"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_dmlGet = decimal.Parse(row["GET_DEC"].ToString().Trim());//����
                    arrExecOrder[i].m_strGetunit = row["getunit_chr"].ToString().Trim();//������λ
                    arrExecOrder[i].m_strEntrust = row["ENTRUST_VCHR"].ToString().Trim();//����
                    arrExecOrder[i].m_strDOCTOR_VCHR = row["DOCTOR_VCHR"].ToString().Trim();//����ҽ��
                    if (!row["POSTDATE_DAT"].ToString().Trim().Equals(""))//�ύʱ��
                        arrExecOrder[i].m_dtPostdate = Convert.ToDateTime(row["POSTDATE_DAT"].ToString().Trim());
                    arrExecOrder[i].m_strASSESSORFOREXEC_CHR = row["CONFIRMER_VCHR"].ToString().Trim();//�����-
                    arrExecOrder[i].m_strASSESSORFOREXEC_DAT = row["CONFIRM_DAT"].ToString().Trim();//���ʱ��
                    arrExecOrder[i].m_strDosetypeID = row["DOSETYPEID_CHR"].ToString().Trim();//�÷�����
                    arrExecOrder[i].m_strDosetypeName = row["DOSETYPENAME_CHR"].ToString().Trim();//�÷�����
                    arrExecOrder[i].m_strOrderID = row["orderid_chr"].ToString().Trim();//ҽ������ˮ��
                    if (!row["STATUS_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intStatus = int.Parse(row["STATUS_INT"].ToString().Trim());//��ǰҽ��״̬
                    arrExecOrder[i].m_strCreatorID = row["CREATORID_CHR"].ToString().Trim();//��ҽ����ҽ��ID
                    arrExecOrder[i].m_strCreator = row["CREATOR_CHR"].ToString().Trim();//��ҽ����ҽ��
                    arrExecOrder[i].m_strCHARGEDOCTORGROUPID = row["chargedoctorgroupid_chr"].ToString().Trim();//��ҽ����ҽ������רҵ��

                    arrExecOrder[i].m_dtToday = Convert.ToDateTime(Convert.ToDateTime(row["today"].ToString().Trim()).ToString("yyyy-MM-dd"));//�����ݿ�ȡ��ҽ��ʱ��ʱ��,Ϊ�жϵ�ǰҽ���Ƿ��ִ��ʹ��
                    if (!row["Days_Int"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intDays_Int = int.Parse(row["Days_Int"].ToString().Trim());//���� ҽ��Ƶ�ʶ�Ӧ�� ���� Ϊ�жϵ�ǰҽ���Ƿ��ִ��ʹ�� 
                    if (!row["TIMES_INT"].ToString().Trim().Equals(""))
                        arrExecOrder[i].m_intTIMES_INT = int.Parse(row["TIMES_INT"].ToString().Trim());//���� ҽ��Ƶ�ʶ�Ӧ�� ����  

                    if (!row["FINISHDATE_DAT"].ToString().Trim().Equals(""))//����ʱ��
                    {
                        arrExecOrder[i].m_dtFinishDate = Convert.ToDateTime(row["FINISHDATE_DAT"].ToString().Trim());//FINISHDATE_DAT
                    }
                    else
                    {
                        arrExecOrder[i].m_dtFinishDate = DateTime.MinValue;
                    }
                    if (!row["STARTDATE_DAT"].ToString().Trim().Equals(""))//��ʼʱ��
                    {
                        arrExecOrder[i].m_dtStartDate = Convert.ToDateTime(Convert.ToDateTime(row["STARTDATE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        arrExecOrder[i].m_dtStartDate = DateTime.MinValue;
                    }
                    if (!row["EXECUTEDATE_DAT"].ToString().Trim().Equals(""))//ִ��ʱ��
                    {
                        arrExecOrder[i].m_dtExecutedate = Convert.ToDateTime(Convert.ToDateTime(row["EXECUTEDATE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        arrExecOrder[i].m_dtExecutedate = DateTime.MinValue;
                    }
                    arrExecOrder[i].m_strOrderDicID = row["ORDERDICID_CHR"].ToString().Trim();//������Ŀ��ˮ��
                    arrExecOrder[i].m_strParentID = row["patientid_chr"].ToString().Trim();//����ID
                    arrExecOrder[i].m_strCREATEAREA_ID = row["createareaid_chr"].ToString().Trim();//��������ID
                    arrExecOrder[i].m_strCREATEAREA_Name = row["CREATEAREANAME_VCHR"].ToString().Trim();//��������
                    if (!row["OUTGETMEDDAYS_INT"].ToString().Trim().Equals(""))
                    {
                        arrExecOrder[i].m_intOUTGETMEDDAYS_INT = int.Parse(row["OUTGETMEDDAYS_INT"].ToString().Trim());//��Ժ��ҩ����(��ִ������Ϊ3=��Ժ��ҩ})
                    }
                    arrExecOrder[i].m_strSAMPLEID_VCHR = row["SAMPLEID_VCHR"].ToString().Trim();//����������
                    arrExecOrder[i].m_strSAMPLEName_VCHR = row["sample_type_desc_vchr"].ToString().Trim();//������������
                    arrExecOrder[i].m_strPARTID_VCHR = row["PARTID_VCHR"].ToString().Trim();//��鲿λID
                    arrExecOrder[i].m_strPARTNAME_VCHR = row["partname"].ToString().Trim();//��鲿λ����

                    arrExecOrder[i].m_strDOCTORID_CHR = row["DOCTORID_CHR"].ToString().Trim();//�ܴ�ҽ��
                    arrExecOrder[i].m_strCURAREAID_CHR = row["CURAREAID_CHR"].ToString().Trim();//��ҽ��ʱ�������ڲ���ID
                    arrExecOrder[i].m_strCURBEDID_CHR = row["CURBEDID_CHR"].ToString().Trim();//��ҽ��ʱ�������ڲ���ID
                    arrExecOrder[i].m_strCURAREAName = row["DEPTNAME_VCHR"].ToString().Trim();//������������
                    arrExecOrder[i].m_strCURBEDName = row["code_chr"].ToString().Trim();//������������

                    if (arrExecOrder[i].m_intExecuteType == 1)//���ǳ���
                    {
                        arrExecOrder[i].m_strEXECTIME_VCHR = row["LEXECTIME_VCHR"].ToString().Trim();//����ִ��ʱ��
                    }
                    else
                    {
                        arrExecOrder[i].m_strEXECTIME_VCHR = row["TEXECTIME_VCHR"].ToString().Trim();//����ִ��ʱ��
                    }
                    arrExecOrder[i].m_intATTACHTIMES_INT = clsConverter.ToInt(row["ATTACHTIMES_INT"].ToString().Trim());//���δ���
                    arrExecOrder[i].m_strDOCTORGROUPID_CHR = row["DOCTORGROUPID_CHR"].ToString().Trim();//ҽ��רҵ��ID
                    //Ƥ������ֶ�
                    arrExecOrder[i].m_intISNEEDFEEL = int.Parse(row["ISNEEDFEEL"].ToString().Trim());//�Ƿ���ҪƤ��
                    arrExecOrder[i].m_intFEEL_INT = int.Parse(row["FEEL_INT"].ToString().Trim());//Ƥ�Խ��
                    arrExecOrder[i].m_strFEELRESULT_VCHR = Convert.ToString(row["FEELRESULT_VCHR"].ToString().Trim());//Ƥ�Խ����ע
                    if (!row["CHARGE_INT"].ToString().Trim().Equals("")) //     �޸ı�־(0-��ͨ״̬,1-Ƶ���޸�)�������ǲ�������ҽ����ʱ���޸���Ĭ�ϵ�Ƶ�ʣ����飡����
                        arrExecOrder[i].m_intCHARGE_INT = int.Parse(row["CHARGE_INT"].ToString().Trim());
                    //˵��
                    arrExecOrder[i].m_strREMARK_VCHR = row["REMARK_VCHR"].ToString().Trim();
                    //���뵥���ID(AR_APPLY_TYPELIST)
                    arrExecOrder[i].m_strAPPLYTYPEID_CHR = row["APPLYTYPEID_CHR"].ToString().Trim();
                    //�������뵥ԪID(t_aid_lis_apply_unit)
                    arrExecOrder[i].m_strLISAPPLYUNITID_CHR = row["LISAPPLYUNITID_CHR"].ToString().Trim();
                    //��һ�ε�����
                    arrExecOrder[i].m_dmlOneUse = Convert.ToDecimal(row["SINGLEAMOUNT_DEC"].ToString().Trim());
                    // Ԥ������
                    arrExecOrder[i].PretestDays = row["pretestdays"] == DBNull.Value ? 0 : Convert.ToInt32(row["pretestdays"]);
                    // �Ƴ�����.Ԥ������2(Ԥ����ʣ��)--����
                    arrExecOrder[i].PreAmount2 = row["preamount2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["preamount2"]);
                    // �Ƴ�����
                    arrExecOrder[i].CureDays = row["curedays"] == DBNull.Value ? 0 : Convert.ToInt32(row["curedays"]);
                    // �Ƴ�-���״̬: -9 ��; -1 ҩ����˲�ͨ��; 0 Ĭ��; 1 ���ͨ��
                    arrExecOrder[i].CheckState = row["checkstate"] == DBNull.Value ? -9 : Convert.ToInt32(row["checkstate"]);
                    // ���ͷ��� 1-�ڷ��� 2-�����
                    arrExecOrder[i].MedProperty = row["medproperty"] == DBNull.Value ? 0 : Convert.ToInt32(row["medproperty"]);

                    //Ϊ��ǰҽ����¼�����˴��ű��� Ϊ�˰�����f8,f9��������ת��
                    //m_arrBedIdList��¼�˴���ҽ�������в�����
                    if (m_blBedIdKey)
                    {
                        if (!m_arrBedIdList.Contains(arrExecOrder[i].m_strBedID))
                        {
                            m_arrBedIdList.Add(arrExecOrder[i].m_strBedID);
                        }
                    }
                }
            }
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
            if (objRow.Count <= 0) return;
            objPatient = new clsBIHPatientInfo();
            DataRowView row2 = objRow[0];
            objPatient.m_strRegisterID = clsConverter.ToString(row2["RegisterID_Chr"]).Trim();
            objPatient.m_strPatientID = clsConverter.ToString(row2["PatientID_Chr"]).Trim();
            objPatient.m_strInHospitalNo = clsConverter.ToString(row2["InPatientID_Chr"]).Trim();
            objPatient.m_dtInHospital = clsConverter.ToDateTime(row2["InPatient_Dat"]);
            objPatient.m_strDeptID = clsConverter.ToString(row2["DeptID_Chr"]).Trim();
            objPatient.m_strDeptName = clsConverter.ToString(row2["deptname"]).Trim();
            objPatient.m_strPATIENTCARDID_CHR = row2["patientcardid_chr"].ToString().Trim();

            objPatient.m_strAreaID = clsConverter.ToString(row2["AreaID_Chr"]).Trim();
            objPatient.m_strAreaName = clsConverter.ToString(row2["AreaName"]).Trim();
            objPatient.m_strBedID = clsConverter.ToString(row2["BedID_Chr"]).Trim();
            objPatient.m_strBedName = clsConverter.ToString(row2["BedName"]).Trim();
            /** update by xzf (05-09-29) */
            //@ objPatient.m_strDiagnose=clsConverter.ToString(objRow["Diagnose_Vchr"]).Trim();
            objPatient.m_strDiagnose = clsConverter.ToString(row2["ICD10DIAGTEXT_VCHR"]).Trim();
            /* <<============================= */
            objPatient.m_intInTimes = clsConverter.ToInt(row2["InPatientCount_Int"]);
            objPatient.m_strPatientName = clsConverter.ToString(row2["Name_VChr"]).Trim();

            objPatient.m_strSex = clsConverter.ToString(row2["Sex_Chr"]).Trim();
            objPatient.m_dtBorn = clsConverter.ToDateTime(row2["Birth_Dat"]);
            objPatient.m_strPayTypeID = clsConverter.ToString(row2["PayTypeID_Chr"]).Trim();
            objPatient.m_strPayTypeName = clsConverter.ToString(row2["PayTypeName_VChr"]).Trim();
            objPatient.m_strInpatientState = clsConverter.ToString(row2["state"]).Trim();
            objPatient.m_strMzdiagnose_vchr = clsConverter.ToString(row2["mzdiagnose_vchr"]).Trim();
            objPatient.m_strDiagnose_vchr = clsConverter.ToString(row2["diagnose_vchr"]).Trim();
            objPatient.m_intREMARK_INT = clsConverter.ToInt(row2["REMARK_INT"]);
            objPatient.m_intCHARGECTL_INT = clsConverter.ToInt(row2["CHARGECTL_INT"]);
            objPatient.m_decinsuredsum_mny = clsConverter.ToDecimal(row2["insuredsum_mny"]);
            if (row2["limitrate_mny"] != System.DBNull.Value)
            {
                objPatient.m_dblLIMITRATE_MNY = double.Parse(row2["limitrate_mny"].ToString());
            }
            try
            {
                // TimeSpan span1 = clsConverter.ToDateTime(row2["today"].ToString().Trim()) - objPatient.m_dtBorn;
                //objPatient.m_intAge = span1.Days / 365;
                ////ͳһ�����㷨
                objPatient.m_strAge = (new clsBrithdayToAge().m_strGetAge(objPatient.m_dtBorn)).Trim();
                //objPatient.m_strAge = com.digitalwave.Emr.StaticObject.clsConsts.s_strCalAge(objPatient.m_dtBorn.ToString("yyyy-MM-dd hh:mm:ss"), objPatient.m_dtInHospital, out objPatient.m_intAge);

            }
            catch
            {
            }
            try
            {
                objPatient.m_strREMARKNAME_VCHR = row2["REMARKNAME_VCHR"].ToString().Trim();
            }
            catch
            {
            }
            try
            {
                objPatient.m_strDES_VCHR = row2["DES_VCHR"].ToString().Trim();
            }
            catch
            {
            }
            decimal PreMoney = 0, PreUseMoney = 0, NotUsePreMoney = 0, ClearMoney = 0, WaitMoney = 0, WaitClearMoney = 0, VerticalMoney = 0;
            //decimal unitprice_dec = 0, amount_dec = 0, m_decSum = 0, m_decPre = 0;
            decimal m_decSum = 0, m_decPre = 0;
            int pstatus_int = -1;

            //���˵ĺϼƽ�Ԥ����
            if (m_dtChargeMoney != null && m_dtChargeMoney.Rows.Count > 0)
            {
                DataView myDataView = new DataView(m_dtChargeMoney);
                myDataView.RowFilter = "registerid_chr='" + objPatient.m_strRegisterID + "'";

                for (int i = 0; i < myDataView.Count; i++)
                {
                    DataRowView row = myDataView[i];
                    //unitprice_dec = 0;
                    //amount_dec = 0;
                    pstatus_int = -1;
                    //if (!row["unitprice_dec"].ToString().Trim().Equals(""))
                    //{
                    //    unitprice_dec = Convert.ToDecimal(row["unitprice_dec"].ToString().Trim());
                    //}
                    //if (!row["amount_dec"].ToString().Trim().Equals(""))
                    //{
                    //    amount_dec = Convert.ToDecimal(row["amount_dec"].ToString().Trim());
                    //}
                    //if (!row["pstatus_int"].ToString().Trim().Equals(""))
                    //{
                    //    pstatus_int = int.Parse(row["pstatus_int"].ToString().Trim());
                    //}
                    //m_decSum = decimal.Round(unitprice_dec * amount_dec, 2);
                    decimal.TryParse(row["Money"].ToString().Trim(), out m_decSum);
                    int.TryParse(row["pstatus_int"].ToString().Trim(), out pstatus_int);
                    switch (pstatus_int)
                    {
                        case 1:
                            WaitMoney += m_decSum;
                            break;
                        case 2:
                            WaitClearMoney += m_decSum;
                            break;
                        case 3:
                            ClearMoney += m_decSum;
                            break;
                        case 4:
                            VerticalMoney += m_decSum;
                            break;

                    }
                    if (pstatus_int != 0)
                    {
                        PreUseMoney += m_decSum;
                    }


                }
            }
            if (m_dtPrepay != null && m_dtPrepay.Rows.Count > 0)
            {
                DataView myDataView2 = new DataView(m_dtPrepay);
                myDataView2.RowFilter = "registerid_chr='" + objPatient.m_strRegisterID + "'";
                for (int i = 0; i < myDataView2.Count; i++)
                {
                    DataRowView row = myDataView2[i];
                    if (!row["money_dec"].ToString().Trim().Equals(""))
                    {
                        m_decPre += decimal.Round(Convert.ToDecimal(row["money_dec"].ToString().Trim()), 2);
                    }

                }
            }
            NotUsePreMoney = m_decPre;

            objPatient.m_decPreMoney = NotUsePreMoney;//ʣ���Ԥ�����
            objPatient.m_decPreUseMoney = PreUseMoney;//���ý��
            objPatient.m_decClearMoney = ClearMoney;//������
            objPatient.m_decVerticalMoney = VerticalMoney;//ֱ�ս�ĳЩҩƷ����������Ҫ���Ͻ���
            objPatient.m_decPrePayMoney = NotUsePreMoney - WaitMoney - WaitClearMoney;//������
            //WaitMoney --- �������
            //WaitClearMoney --- �ѽ��㣬��δ����

        }
        #endregion

        #region ���ñ�ת��Ϊ������ϸ����  -- ҽ��ִ�йؼ����� 20180403 (ҽ����Ϣ -> ������Ϣ)
        /// <summary>
        /// ����ǰ������Ϣת��Ϊ������ϸ�����Ϣ  -- ҽ��ִ�йؼ����� 20180403 (ҽ����Ϣ -> ������Ϣ)
        /// </summary>
        /// <param name="objRow"></param>
        /// <param name="m_arrObjItem"></param>
        void GetChargeListFromDateTable(DataView objRow, out clsChargeForDisplay[] m_arrObjItem)
        {
            m_arrObjItem = new clsChargeForDisplay[objRow.Count];
            decimal UNITPRICE_DEC = 0, ItemPrice_Mny = 0, PackQty_Dec = 0, decItemTradePrice = 0;
            int IPCHARGEFLG_INT = 0;
            for (int i = 0; i < objRow.Count; i++)
            {
                m_arrObjItem[i] = new clsChargeForDisplay();

                #region ���ӵ��շ���Ŀ��Ϣ
                // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                clsORDERCHARGEDEPT_VO orderChargeVo = new clsORDERCHARGEDEPT_VO();
                DataRowView row = objRow[i];
                orderChargeVo.m_strSeq_int = clsConverter.ToString(row["SEQ_INT"]).Trim();
                orderChargeVo.m_strOrderid_chr = clsConverter.ToString(row["ORDERID_CHR"]).Trim();
                orderChargeVo.m_strOrderdicid_chr = clsConverter.ToString(row["ORDERDICID_CHR"]).Trim();
                orderChargeVo.m_strChargeitemid_chr = clsConverter.ToString(row["CHARGEITEMID_CHR"]).Trim();
                orderChargeVo.m_strClacarea_chr = clsConverter.ToString(row["CLACAREA_CHR"]).Trim();
                orderChargeVo.m_strCreatearea_chr = clsConverter.ToString(row["CREATEAREA_CHR"]).Trim();
                orderChargeVo.m_intFLAG_INT = clsConverter.ToInt(row["FLAG_INT"]);
                orderChargeVo.m_strChargeitemname_chr = clsConverter.ToString(row["CHARGEITEMNAME_CHR"]).Trim();
                orderChargeVo.m_strSpec_vchr = clsConverter.ToString(row["ITEMSPEC_VCHR"]).Trim();  // ȡ�շ���Ŀ�Ĺ��
                orderChargeVo.m_strUnit_vchr = clsConverter.ToString(row["UNIT_VCHR"]).Trim();
                orderChargeVo.m_decAmount_dec = clsConverter.ToDecimal(row["AMOUNT_DEC"]);          // 20181009: ����������ͬʱҲ�ǰ�ҩ������ 
                string strMedicinetypeid = row["medicinetypeid_chr"].ToString();
                //ȡ�շ���Ŀ�еĵ���
                /*decode(b.IPCHARGEFLG_INT,1,Round(B.ItemPrice_Mny/B.PackQty_Dec,4),0,B.ItemPrice_Mny,Round(B.ItemPrice_Mny/B.PackQty_Dec,4)) ItemPriceA*/
                UNITPRICE_DEC = 0;
                ItemPrice_Mny = 0;
                PackQty_Dec = 0;
                int.TryParse(row["IPCHARGEFLG_INT"].ToString(), out IPCHARGEFLG_INT);
                decimal.TryParse(row["ItemPrice_Mny"].ToString(), out ItemPrice_Mny);
                decimal.TryParse(row["PackQty_Dec"].ToString(), out PackQty_Dec);
                decimal.TryParse(row["tradeprice_mny"].ToString(), out decItemTradePrice);// ��������
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
                orderChargeVo.m_decUnitprice_dec = UNITPRICE_DEC;
                orderChargeVo.m_decUnitTradePrice_dec = decItemTradePrice;

                orderChargeVo.m_strCreatorid_chr = clsConverter.ToString(row["CREATORID_CHR"]).Trim();
                orderChargeVo.m_strCreator_vchr = clsConverter.ToString(row["CREATOR_VCHR"]).Trim();
                orderChargeVo.m_strCreatedate_dat = clsConverter.ToDateTime(row["CREATEDATE_DAT"]);
                orderChargeVo.REMARK = clsConverter.ToString(row["REMARK"]).Trim();
                orderChargeVo.m_strINSURACEDESC_VCHR = clsConverter.ToString(row["INSURACEDESC_VCHR"]).Trim();
                orderChargeVo.m_intRATETYPE_INT = clsConverter.ToInt(row["RATETYPE_INT"]);//�Ƿ�Ʒ� 
                orderChargeVo.m_intCONTINUEUSETYPE_INT = clsConverter.ToInt(row["CONTINUEUSETYPE_INT"]);
                if (!row["SINGLEAMOUNT_DEC"].ToString().Trim().Equals(""))
                    orderChargeVo.m_decSINGLEAMOUNT_DEC = clsConverter.ToDecimal(row["SINGLEAMOUNT_DEC"]);
                //���ӵ��շ���Ϣ
                orderChargeVo.m_intISRICH_INT = clsConverter.ToInt(row["ISRICH_INT"]);
                orderChargeVo.m_strISSELFPAY_CHR = clsConverter.ToString(row["ISSELFPAY_CHR"]).Trim();
                orderChargeVo.m_strItemIPCalcType_Chr = clsConverter.ToString(row["ItemIPCalcType_Chr"]).Trim();
                orderChargeVo.m_strItemIpInvType_Chr = clsConverter.ToString(row["ItemIpInvType_Chr"]).Trim();//�������
                orderChargeVo.m_intITEMSRCTYPE_INT = clsConverter.ToInt(row["ITEMSRCTYPE_INT"]);
                orderChargeVo.m_strITEMSRCID_VCHR = clsConverter.ToString(row["ITEMSRCID_VCHR"]);
                orderChargeVo.m_intPUTMEDTYPE_INT = clsConverter.ToInt(row["PUTMEDTYPE_INT"]);
                orderChargeVo.m_intMEDICNETYPE_INT = clsConverter.ToInt(row["MEDICNETYPE_INT"]);
                orderChargeVo.m_decDOSAGE_DEC = clsConverter.ToDecimal(row["DOSAGE_DEC"]);
                orderChargeVo.m_strDOSAGEUNIT_CHR = clsConverter.ToString(row["DOSAGEUNIT_CHR"]);
                #region  ��ҩ���� ҩƷ�Ķ��� -- ��ҩ��ʶ(�ж�)
                if (orderChargeVo.m_intITEMSRCTYPE_INT == 1 && orderChargeVo.m_intPUTMEDTYPE_INT == 1 && orderChargeVo.m_intFLAG_INT != 3)
                {
                    // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ) 20180404
                    if (clsConverter.ToInt(row["medSource"]) == 1 || clsConverter.ToInt(row["medSource"]) == 2)
                    {
                        orderChargeVo.m_intPOFLAG_INT = 0;
                    }
                    else
                    {
                        orderChargeVo.m_intPOFLAG_INT = 1;
                    }
                }
                #endregion
                m_arrObjItem[i].m_objORDERCHARGEDEPT_VO = orderChargeVo;//סԺ������Ŀ�շ���Ŀִ�пͻ���
                #endregion

                m_arrObjItem[i].m_strChargeID = clsConverter.ToString(row["CHARGEITEMID_CHR"]).Trim();
                //�շ���Ŀ����
                m_arrObjItem[i].m_strName = clsConverter.ToString(row["CHARGEITEMNAME_CHR"]).Trim();
                double dblNum = 0;
                //���� 
                m_arrObjItem[i].m_dblPrice = double.Parse(orderChargeVo.m_decUnitprice_dec.ToString());

                if (!row["AMOUNT_DEC"].ToString().Trim().Equals(""))
                {
                    dblNum = double.Parse(clsConverter.ToString(row["AMOUNT_DEC"]).Trim());
                }
                //����
                m_arrObjItem[i].m_dblDrawAmount = dblNum;
                m_arrObjItem[i].m_dblTradePrice = double.Parse(decItemTradePrice.ToString());
                // ���������,ȡ����
                if (this.m_blnIsDiffMed(strMedicinetypeid))//������ҩƷ����ʾ
                {
                    m_arrObjItem[i].m_dblDiffCostMoney = Math.Round((m_arrObjItem[i].m_dblTradePrice - m_arrObjItem[i].m_dblPrice) * dblNum, 4);
                }
                //�ϼƽ��
                m_arrObjItem[i].m_dblMoney = m_arrObjItem[i].m_dblPrice * dblNum;
                //�������� {-1=���÷��շѣ�ҩƷ�շѵȣ�;0=������;1=ȫ������;2-��������}
                if (!row["CONTINUEUSETYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intCONTINUEUSETYPE_INT = int.Parse(row["CONTINUEUSETYPE_INT"].ToString().Trim());
                }

                //�Ƿ�������ҽ��	{0=��1=��} ������ҽ������ʾҩƷ������Ϣ��
                // m_arrObjItem[i].m_intIsContinueOrder = (blnIsConOrder) ? (1) : (0);
                //�Ƿ�ȱҩ
                // m_arrObjItem[i].m_strNoqtyFLag = objMedicineItemArr[i1].m_strNoqtyFLag;
                // ���Ͽ�������
                m_arrObjItem[i].m_strClacarea_chr = clsConverter.ToString(row["CLACAREA_CHR"]).Trim();
                m_arrObjItem[i].m_strClacareaName_chr = clsConverter.ToString(row["deptname_vchr"]).Trim();
                //�ݴ�סԺ������Ŀ�շ���Ŀִ�пͻ������ˮ��
                m_arrObjItem[i].m_strSeq_int = clsConverter.ToString(row["SEQ_INT"]).Trim(); ;
                m_arrObjItem[i].m_strYBClass = clsConverter.ToString(row["INSURACEDESC_VCHR"]).Trim();
                m_arrObjItem[i].m_strSPEC_VCHR = clsConverter.ToString(row["ITEMSPEC_VCHR"]).Trim();
                m_arrObjItem[i].m_strUNIT_VCHR = clsConverter.ToString(row["UNIT_VCHR"]).Trim();
                //�շ�����Դ�� 0-��������Ŀ��1-������Ŀ,2���������÷���3���Զ����¿�
                if (!row["FLAG_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intType = clsConverter.ToInt(row["FLAG_INT"].ToString().Trim());
                // סԺ������Ŀ�շ���Ŀִ�пͻ���VO
                if (!row["ITEMSRCTYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intITEMSRCTYPE_INT = int.Parse(row["ITEMSRCTYPE_INT"].ToString().Trim());
                }
                if (!row["IPNOQTYFLAG_INT"].ToString().Trim().Equals(""))//����ҩ��ȱҩ��־ 0-��ҩ 1��ȱҩ
                {
                    m_arrObjItem[i].m_intIPNOQTYFLAG_INT = int.Parse(row["IPNOQTYFLAG_INT"].ToString().Trim());
                }
            }
            #region ���ӵ���Ϣ ��ҩ���� ���ڿ���ʱ
            if (this.m_objViewer.m_blPutMedicineFormDic == false)
            {
                for (int i = 0; i < m_arrObjItem.Length; i++)
                {
                    //һ�Զ�����������Ŀ��Ҫ��ҩ��������Ŀ����ҩ
                    if (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_intFLAG_INT == 1)//����������Ŀ�������շ���Ŀʱ������ҩ�������Ա��ˣ�һ�Զ��������Ŀ
                    {
                        for (int j = 0; j < m_arrObjItem.Length; j++)
                        {
                            if (m_arrObjItem[j].m_objORDERCHARGEDEPT_VO.m_intFLAG_INT == 1)
                            {
                                m_arrObjItem[j].m_objORDERCHARGEDEPT_VO.m_intPOFLAG_INT = 0;
                            }
                        }
                        return;
                    }
                }
            }
            #endregion

        }
        #endregion

        // ��ӻ�ʿִ�м���ҽ�����ͼ��鵥������ɼ�����
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

        internal bool UpdateBihOrderConfirmer()
        {
            string m_strORDERID_Arr = "";
            System.Collections.Generic.List<string> p_glstAllPhysicianOrderID = GetTheSelectArrItem();//�����п�����Ҫִ�е�ҽ������ѡ�еĲ��˺���Щ���˵�ҽ��״̬��ɸѡ����
            if (p_glstAllPhysicianOrderID.Count <= 0)
            {
                MessageBox.Show("����ѡ��ҽ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            //ԭ���Ĵ���
            string[] m_arrCanNoOrder = null;
            System.Collections.Generic.List<string> p_glstNonExcutablePhysicianOrderID = null;
            string[] m_arrORDERID = p_glstAllPhysicianOrderID.ToArray();

            bool blnOK_To_Execute_PhysicianOrder = ConfirmCurrentOrder(m_arrORDERID, out m_arrCanNoOrder);
            if (blnOK_To_Execute_PhysicianOrder == false)//��ҽ��״̬�ı�
            {
                MessageBox.Show("��ǰ��ִ��ҽ��״̬�ѱ仯,��ȷ�����ٳ���!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DelTheOrderFromDTView(m_arrCanNoOrder);
                m_mthRefreshSameReqNoColor();
                return false;
            }
            if (m_blExeConfirm == true)//ִ���Ƿ���Ҫ��ˣ�ȷ��ִ�����Ƿ���Ȩ��
            {
                string doctid = "";
                if (clsPublic.m_dlgConfirm(out doctid) == DialogResult.Yes)
                {

                }
                else
                {
                    return false;
                }
            }
            else
            {

            }
            #region ���ɼ�����뵥������ҽ��ִ��ԤԼ���¼(������뵥)
            // ���ɼ�����뵥
            System.Collections.Generic.List<clsCommitOrder> p_glstCommitPhysicianOrderList = null;
            System.Collections.Generic.List<clsExecOrderVO> p_glstExecOrderVO_List = null;
            p_glstCommitPhysicianOrderList = GetTheSelectExecOrderArrItem(ref p_glstExecOrderVO_List);

            this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڼ��Ƿ�����������������";
            this.objFrmExecuteOrdersProgress.Refresh();

            // Ƿ����ʾ
            if (this.m_objViewer.m_blCanSelectOrder == true && this.m_objViewer.m_blLessExecuteAlert == true)
            {
                p_glstExecOrderVO_List = LessMoneyControl(p_glstExecOrderVO_List);
            }
            if (p_glstExecOrderVO_List.Count <= 0)
            {
                return false;
            }

            #region ���ҩƷ��������Կ�治��ҩƷ������ʾ
            Application.DoEvents();

            this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڼ��ҩƷ��档����������";
            this.objFrmExecuteOrdersProgress.Refresh();

            p_glstExecOrderVO_List = CheckMedicineKC(ref p_glstExecOrderVO_List);
            if (p_glstExecOrderVO_List == null || p_glstExecOrderVO_List.Count <= 0)
            {
                return false;
            }
            #endregion

            // ��ʳ���ȼ������ͬ������
            this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڴ����˻������ʳ������������";
            this.objFrmExecuteOrdersProgress.Refresh();

            List<clsPatientNurseVO> m_arrNurseVO = new List<clsPatientNurseVO>(); // �ȼ�������/��ʳ����
            // ���²��˷��û����־
            ArrayList m_arrNurOrders = new ArrayList();
            int intCommitPhysicianOrderListCount = p_glstCommitPhysicianOrderList.Count;
            clsCommitOrder objOneCommitPhysicianOrder = null;

            for (int i = 0; i < intCommitPhysicianOrderListCount; i++)
            {
                objOneCommitPhysicianOrder = p_glstCommitPhysicianOrderList[i];
                //�ȼ�����
                if (objOneCommitPhysicianOrder.m_strOrderDicCateID.Trim().Equals(m_objSpecateVo.m_strNURSECATE.Trim()))
                {
                    clsPatientNurseVO m_objCare = GetThePatientNurseVO(objOneCommitPhysicianOrder);
                    m_objCare.m_intTYPE_INT = 1;
                    m_objCare.m_strORDERID_CHR = objOneCommitPhysicianOrder.m_strOrderID;
                    switch (m_objCare.m_strOrderDicName)
                    {
                        case "��ͨ����":
                            m_objCare.m_intNURSING_CLASS = -1;
                            break;
                        case "�ؼ�����":
                            m_objCare.m_intNURSING_CLASS = 0;
                            break;
                        case "һ������":
                            m_objCare.m_intNURSING_CLASS = 1;
                            break;
                        case "��������":
                            m_objCare.m_intNURSING_CLASS = 2;
                            break;
                        case "��������":
                            m_objCare.m_intNURSING_CLASS = 3;
                            break;
                        case "I������":
                            m_objCare.m_intNURSING_CLASS = 1;
                            break;
                        case "II������":
                            m_objCare.m_intNURSING_CLASS = 2;
                            break;
                        case "III������":
                            m_objCare.m_intNURSING_CLASS = 3;
                            break;
                        default:
                            m_objCare.m_intNURSING_CLASS = -1;
                            break;
                    }
                    m_arrNurseVO.Add(m_objCare);
                    m_arrNurOrders.Add(objOneCommitPhysicianOrder.m_strOrderID);
                }

                //��ʳ����
                if (objOneCommitPhysicianOrder.m_strOrderDicCateID.Trim().Equals(m_objSpecateVo.m_strEATDICCATE.Trim()))
                {
                    clsPatientNurseVO m_objEat = GetThePatientNurseVO(objOneCommitPhysicianOrder);
                    m_objEat.m_strORDERID_CHR = objOneCommitPhysicianOrder.m_strOrderID;
                    m_objEat.m_intTYPE_INT = 2;
                    m_arrNurseVO.Add(m_objEat);
                }
            }
            //���²��˷��û����־
            //ԭ������ȷ���� SetTheNurseCharge(m_arrNurOrders, m_arrExecOrder);
            //�ر��ע���´���,ȷ���ܹ�����m_glstExecOrderList
            SetTheNurseCharge(m_arrNurOrders, p_glstExecOrderVO_List);
            /*<=======================================*/
            //Ϊÿ������ÿ�����ֻ�������һ��Ϊ��Ч,����Ϊ��Ч
            string m_strRegisterid = "";
            //��¼���ĵȼ�����
            Hashtable m_htActive_int = new Hashtable();
            ArrayList m_ArrRegisterid = new ArrayList();
            for (int i = 0; i < m_arrNurseVO.Count; i++)//�ȼ�����
            {
                if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intTYPE_INT == 1)//���� 1-����ȼ� 2-��ʳ״̬
                {
                    m_strRegisterid = ((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR;
                    ((clsPatientNurseVO)m_arrNurseVO[i]).m_intACTIVE_INT = 0;// ��Ч״̬ 0-��Ч 1-��Ч
                    if (m_htActive_int.Contains(m_strRegisterid))
                    {
                        m_htActive_int[m_strRegisterid] = (clsPatientNurseVO)m_arrNurseVO[i];//�����µĻ�����Ŀ����m_htActive_int
                    }
                    else
                    {
                        m_htActive_int.Add(m_strRegisterid, (clsPatientNurseVO)m_arrNurseVO[i]);
                    }
                    if (!m_ArrRegisterid.Contains(m_strRegisterid))
                    {
                        m_ArrRegisterid.Add(m_strRegisterid);
                    }
                }
            }
            //���ĵȼ�������и���Ϊ��Ч
            for (int i = 0; i < m_ArrRegisterid.Count; i++)
            {
                if (m_htActive_int.Contains(m_ArrRegisterid[i].ToString()))
                {
                    ((clsPatientNurseVO)m_htActive_int[m_ArrRegisterid[i].ToString()]).m_intACTIVE_INT = 1;//�����µĻ�����Ŀ
                }
            }
            /*<=========================*/
            for (int i = 0; i < m_arrNurseVO.Count; i++)//��ʳ����
            {
                if (((clsPatientNurseVO)m_arrNurseVO[i]).m_intTYPE_INT == 2)//���� 1-����ȼ� 2-��ʳ״̬
                {
                    m_strRegisterid = ((clsPatientNurseVO)m_arrNurseVO[i]).m_strREGISTERID_CHR;
                    ((clsPatientNurseVO)m_arrNurseVO[i]).m_intACTIVE_INT = 1;
                    for (int j = i + 1; j < m_arrNurseVO.Count; j++)
                    {
                        if (((clsPatientNurseVO)m_arrNurseVO[j]).m_intTYPE_INT == 2)
                        {
                            if (m_strRegisterid.Equals(((clsPatientNurseVO)m_arrNurseVO[j]).m_strREGISTERID_CHR))
                            {
                                ((clsPatientNurseVO)m_arrNurseVO[i]).m_intACTIVE_INT = 0;
                                ((clsPatientNurseVO)m_arrNurseVO[j]).m_intACTIVE_INT = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            this.objFrmExecuteOrdersProgress.lblPatient_Lbl.Visible = false;
            this.objFrmExecuteOrdersProgress.lblPatientName.Visible = false;
            this.objFrmExecuteOrdersProgress.lblPhysicianOrder_Lbl.Visible = false;
            this.objFrmExecuteOrdersProgress.lblPhysicianOrder.Visible = false;
            this.objFrmExecuteOrdersProgress.lblOrderCount.Visible = false;
            this.objFrmExecuteOrdersProgress.lblOrderNum.Visible = false;
            this.objFrmExecuteOrdersProgress.lblOrderSuffix.Visible = false;
            this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Visible = true;

            #region ����Ƴ���ҩ.�Զ�ȷ��
            List<string> lstOrderId = new List<string>();
            List<EntityCureMed> lstCureMed = null;
            List<EntityCureSubStock> lstSubStock = null;
            foreach (clsExecOrderVO item in p_glstExecOrderVO_List)
            {
                if (item.CureDays > 0 && (item.CheckState == 0 || item.CheckState == -9))
                {
                    if (lstOrderId.IndexOf(item.ORDERID_CHR) < 0) lstOrderId.Add(item.ORDERID_CHR);
                }
            }
            if (lstOrderId.Count > 0)
            {
                Application.DoEvents();
                this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڼ���Ƴ���ҩ������������";
                this.objFrmExecuteOrdersProgress.Refresh();

                string orderIdArr = string.Empty;
                foreach (string orderid in lstOrderId)
                {
                    orderIdArr += "'" + orderid + "',";
                }
                orderIdArr = orderIdArr.TrimEnd(',');
                //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                //{
                lstCureMed = (new weCare.Proxy.ProxyIP()).Service.GetCureMed2(orderIdArr);
                if (lstCureMed != null && lstCureMed.Count > 0)
                {
                    if (this.CheckCureMed(ref lstCureMed, out lstSubStock) == false)
                    {
                        return false;
                    }
                }
                //}
            }

            #endregion

            Application.DoEvents();
            this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڸ������ݿ⡣����������";
            this.objFrmExecuteOrdersProgress.Refresh();

            try
            {
                string error = string.Empty;
                List<clsT_Bih_Opr_Putmeddetail_VO> lstPutMedCfkl = new List<clsT_Bih_Opr_Putmeddetail_VO>();
                long lngRes = m_objManage.m_lngUpdateBihOrderExecConfirmer(p_glstExecOrderVO_List, m_arrNurseVO, lstCureMed, lstSubStock, out lstPutMedCfkl, out error);
                if (error != string.Empty)
                {
                    MessageBox.Show(error, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (lstPutMedCfkl != null && lstPutMedCfkl.Count > 0)
                {
                    #region ��ҩ��.�䷽����.����

                    string putMedIds = string.Empty;
                    foreach (clsT_Bih_Opr_Putmeddetail_VO item in lstPutMedCfkl)
                    {
                        putMedIds += "'" + item.m_strPUTMEDDETAILID_CHR + "',";
                    }
                    string xmlIn = string.Empty;
                    xmlIn += "<req>" + Environment.NewLine;
                    xmlIn += string.Format("<recipeId>{0}</recipeId>", "") + Environment.NewLine;
                    xmlIn += string.Format("<putMedId>{0}</putMedId>", putMedIds.TrimEnd(',')) + Environment.NewLine;
                    xmlIn += string.Format("<opIp>{0}</opIp>", "2") + Environment.NewLine;
                    xmlIn += "</req>" + Environment.NewLine;
                    try
                    {
                        using (MedService ms = new MedService())
                        {
                            string outXml = ms.FireBird(xmlIn);
                            //Log.Output(outXml);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Output(ex.Message);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.ToString());
                MessageBox.Show("���ܶ�ͬһҽ������ͬʱ����ִ�в���!��ˢ�º��ٲ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            // ����Ÿո�ִ����,û�б�Ҫ��������ִ�а�?�����Ǻ������ȥ��,û���ظ��������߼�
            m_arrCanNoOrder = null;
            blnOK_To_Execute_PhysicianOrder = ConfirmCurrentOrder(m_arrORDERID, out m_arrCanNoOrder);
            p_glstNonExcutablePhysicianOrderID = new List<string>();
            for (int k = 0; k < m_arrCanNoOrder.Length; k++)
            {
                p_glstNonExcutablePhysicianOrderID.Add(m_arrCanNoOrder[k].ToString());
            }
            ArrayList SendCheckArr2 = new ArrayList();

            if (blnOK_To_Execute_PhysicianOrder == false)
            {
                #region ���ͼ�����뵥����ԤԼ��
                Application.DoEvents();
                this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڴ����顢�������뵥��ԤԼ��������������";
                this.objFrmExecuteOrdersProgress.Refresh();

                Dictionary<string, List<clsCommitOrder>> gdctTestApply = new Dictionary<string, List<clsCommitOrder>>();
                List<string> arrRegID = new List<string>();//���
                List<string> arrCheckRegID = new List<string>();//����

                for (int i = 0; i < intCommitPhysicianOrderListCount; i++)
                {
                    objOneCommitPhysicianOrder = p_glstCommitPhysicianOrderList[i];
                    if (!objOneCommitPhysicianOrder.m_strAPPLYTYPEID_CHR.Trim().Equals("") && p_glstNonExcutablePhysicianOrderID.Contains(objOneCommitPhysicianOrder.m_strOrderID))
                    {
                        if (!arrRegID.Contains(objOneCommitPhysicianOrder.m_strRegisterID))
                        {
                            arrRegID.Add(objOneCommitPhysicianOrder.m_strRegisterID);
                            gdctTestApply.Add(objOneCommitPhysicianOrder.m_strRegisterID, new List<clsCommitOrder>());
                            gdctTestApply[objOneCommitPhysicianOrder.m_strRegisterID].Add(objOneCommitPhysicianOrder);
                        }
                        else
                        {
                            gdctTestApply[objOneCommitPhysicianOrder.m_strRegisterID].Add(objOneCommitPhysicianOrder);
                        }
                    }
                }

                if (gdctTestApply.Count > 0)
                {
                    weCare.Core.Entity.clsOrderBooking[] m_arrOrderBooking;
                    clsATTACHRELATION_VO[] m_arATTACHRELATION;
                    if (m_mthSendCheckApplyBill(gdctTestApply, out m_arrOrderBooking, out m_arATTACHRELATION))
                    {
                        m_objManage.m_lngUpdateOrderBookingArr(m_arrOrderBooking);
                        m_objManage.m_lngUpdateOrderAttachRelation(m_arATTACHRELATION);
                    }
                    else
                    {
                        MessageBox.Show("��鷢��ʧ�ܡ�", "��ʾ");
                        return false;
                    }
                }
                #endregion
                #region ���ͼ������뵥
                if (m_objViewer.m_blSendLisBill == false)
                {
                    Application.DoEvents();
                    this.objFrmExecuteOrdersProgress.lblExecuteOrderNote.Text = "���ڴ����顢�������뵥��ԤԼ��������������";
                    this.objFrmExecuteOrdersProgress.Refresh();

                    Dictionary<string, List<clsCommitOrder>> gdctCheckApply = new Dictionary<string, List<clsCommitOrder>>();
                    for (int i = 0; i < intCommitPhysicianOrderListCount; i++)
                    {
                        objOneCommitPhysicianOrder = p_glstCommitPhysicianOrderList[i];
                        if (!objOneCommitPhysicianOrder.m_strLISAPPLYUNITID_CHR.Trim().Equals("") && p_glstNonExcutablePhysicianOrderID.Contains(objOneCommitPhysicianOrder.m_strOrderID))
                        {
                            if (!arrCheckRegID.Contains(objOneCommitPhysicianOrder.m_strRegisterID))
                            {
                                arrCheckRegID.Add(objOneCommitPhysicianOrder.m_strRegisterID);
                                gdctCheckApply.Add(objOneCommitPhysicianOrder.m_strRegisterID, new List<clsCommitOrder>());
                                gdctCheckApply[objOneCommitPhysicianOrder.m_strRegisterID].Add(objOneCommitPhysicianOrder);
                            }
                            else
                            {
                                gdctCheckApply[objOneCommitPhysicianOrder.m_strRegisterID].Add(objOneCommitPhysicianOrder);
                            }
                        }
                    }

                    // ���� 
                    if (gdctCheckApply.Count > 0)
                    {
                        List<string> arrLisError = null;
                        List<string> objListAll = new List<string>();
                        foreach (string strKey in gdctCheckApply.Keys)
                        {
                            List<clsCommitOrder> objLis = gdctCheckApply[strKey];
                            arrLisError = new List<string>();
                            for (int intJ = 0; intJ < objLis.Count; intJ++)
                            {
                                clsCommitOrder objCommitOrderVO = objLis[intJ];
                                //���Ǽ������δִ�гɹ�  
                                if (!p_glstNonExcutablePhysicianOrderID.Contains(objCommitOrderVO.m_strOrderID) || objCommitOrderVO.m_strOrderDicCateID != this.m_objSpecateVo.m_strORDERCATEID_LIS_CHR)
                                {
                                    objLis.Remove(objCommitOrderVO);
                                    intJ--;
                                }
                                else
                                {
                                    arrLisError.Add(objCommitOrderVO.m_strOrderID);//û�з��ͳɹ���
                                }
                            }
                            arrLisError = new List<string>();
                            //Ϊҽ�����õ��ۣ��ϼƽ���
                            this.SetThePrice(ref objLis);
                            this.sendTheCheck(ref objLis, ref arrLisError);//����ʱarrLisError��������ҽ��ID������ʱ����û�з��ͳɹ������뵥��ҽ��ID
                            objListAll.AddRange(arrLisError);
                            arrLisError = null;
                        }

                        if (objListAll.Count > 0)
                        {
                            string strMessage = "";
                            foreach (clsCommitOrder order in p_glstCommitPhysicianOrderList)
                            {
                                if (objListAll.Contains(order.m_strOrderID))
                                {
                                    strMessage += order.m_strName + "\r\n";
                                }
                            }
                            strMessage += "����ҽ�����ͼ������뵥ʧ�ܡ�";
                            MessageBox.Show(this.m_objViewer, strMessage, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                #endregion

                DelTheOrderFromDTView(m_arrCanNoOrder);
                m_mthRefreshSameReqNoColor();
                refreshThePatientCharge();
            }
            return true;
        }
        /// <summary>
        /// ��������
        /// </summary>
        public bool sendTheCheck(ref List<clsCommitOrder> SendCheckArr, ref List<string> m_arrLisOrders)
        {
            clsBIHLis obj = new clsBIHLis();
            //frmLisAppl obj = new frmLisAppl();
            m_arrLisOrders = new List<string>();
            //m_arrLisOrders = new ArrayList();
            clsLisApplMainVO objLMVO;
            clsTestApplyItme_VO[] itemArr_VO;
            // List<string> personArr;

            //Ϊÿһ��������һ���������뵥
            //int intPersonCount = personArr.Count;

            //for(int i = 0; i < intPersonCount; i++)
            //{
            List<clsCommitOrder> commitArr = SendCheckArr;
            //for(int k = 0; k < SendCheckArr.Count; k++)
            //{
            //    if(SendCheckArr[k].m_strPatientID == personArr[i])
            //    {
            //        commitArr.Add(SendCheckArr[k]);
            //    }
            //}
            //if(commitArr.Count <= 0)
            //{
            //    continue;
            //}

            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();

            long m_lngRef = this.m_mthSendTestApplyBillByCommit(commitArr, out objLMVO, out itemArr_VO);
            //m_objService.Dispose();
            //m_objService = null;


            if (m_lngRef <= 0)
            {
                return false;
            }
            if (obj.m_mthNewApp(objLMVO, itemArr_VO, true))
            {
                clsLISAppResults[] objAppResult = obj.m_objGetMutiResults();
                string strOrderID = string.Empty;

                if (objAppResult != null)
                {
                    int intResultLen = objAppResult.Length;
                    int intOrderLen = 0;
                    for (int i2 = 0; i2 < intResultLen; i2++)
                    {
                        if (objAppResult[i2].m_arrOrderId != null && objAppResult[i2].m_arrOrderId.Length > 0)
                        {
                            intOrderLen = objAppResult[i2].m_arrOrderId.Length;
                            for (int j = 0; j < intOrderLen; j++)
                            {
                                strOrderID = objAppResult[i2].m_arrOrderId[j];

                                if (m_arrLisOrders.Contains(strOrderID))//�ѷ��ͳɹ������������
                                {
                                    m_arrLisOrders.Remove(strOrderID);
                                }
                            }
                        }
                    }
                }

                if (objAppResult.Length > 0)
                {
                    //foreach (string strKey in obj.M_dicLISKeyValue.Keys)
                    //{
                    //    m_objOrderIDDefApplyID.Add(strKey, obj.M_dicLISKeyValue[strKey].ToString());
                    //}
                    //objOrder.m_strLISAPPID_VCHR = objAppResult[0].m_StrApplicationID.ToString().Trim();
                    //objOrder.m_strSAMPLEID_VCHR = m_strSampleid;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }
            //  }
            return true;

        }
        //2010/8/26
        internal void SetThePrice(ref List<clsCommitOrder> SendCheckArr)
        {
            ArrayList m_strOrderIds = new ArrayList();
            Hashtable m_htOrders = new Hashtable();
            for (int i = 0; i < SendCheckArr.Count; i++)
            {
                clsCommitOrder order = (clsCommitOrder)SendCheckArr[i];
                if (!m_strOrderIds.Contains(order.m_strOrderID))
                {
                    m_strOrderIds.Add(order.m_strOrderID);

                }
                if (!m_htOrders.Contains(order.m_strOrderID))
                {
                    m_htOrders.Add(order.m_strOrderID, order);
                }
                //order.m_decTotalPrice
            }
            if (m_strOrderIds.Count <= 0)
            {
                return;
            }
            string[] m_arrOrders = (string[])m_strOrderIds.ToArray(typeof(string));
            DataTable m_dtOrderSign = null;
            m_objManage.m_lngGetOrderLisSign(m_arrOrders, out m_dtOrderSign);
            if (m_dtOrderSign != null && m_dtOrderSign.Rows.Count > 0)
            {
                string m_strOrderID = "", m_OrderID = "";
                int m_intDisCount = 0;
                string InvCateID = "";//���÷�Ʊ���id
                decimal AMOUNT_DEC = 0, UNITPRICE_DEC = 0, m_dmlGet = 0, m_dmlPrice = 0;
                decimal m_decTotalPrice = 0;
                for (int i = 0; i < m_strOrderIds.Count; i++)
                {
                    m_dmlGet = 0;
                    m_dmlPrice = 0;
                    m_intDisCount = 0;
                    m_decTotalPrice = 0;
                    m_strOrderID = (string)m_strOrderIds[i];
                    //frmBIHOrderCommit objBIHOrderCommit = new frmBIHOrderCommit();//2010/8/26
                    for (int j = 0; j < m_dtOrderSign.Rows.Count; j++)
                    {

                        m_OrderID = m_dtOrderSign.Rows[j]["orderid_chr"].ToString();
                        if (!m_strOrderID.Equals(m_OrderID))
                        {
                            continue;
                        }
                        InvCateID = m_dtOrderSign.Rows[j]["ItemIpInvType_Chr"].ToString();
                        decimal.TryParse(m_dtOrderSign.Rows[j]["AMOUNT_DEC"].ToString(), out AMOUNT_DEC);
                        decimal.TryParse(m_dtOrderSign.Rows[j]["UNITPRICE_DEC"].ToString(), out UNITPRICE_DEC);
                        if (m_dtOrderSign.Rows[j]["FLAG_INT"].ToString().Trim().Equals("0"))//���շ���Ŀ
                        {
                            m_dmlGet = AMOUNT_DEC;
                            m_dmlPrice = UNITPRICE_DEC;
                        }
                        m_decTotalPrice += AMOUNT_DEC * UNITPRICE_DEC;

                        if (this.m_objViewer.m_blLisDiscount == true && m_strLisPARMVALUE_VCHR.Contains(InvCateID) && !InvCateID.Equals(""))
                        {
                            m_intDisCount++;
                        }
                    }
                    //������۵��߼�
                    clsCommitOrder order = (clsCommitOrder)m_htOrders[m_strOrderID];
                    order.m_decTotalPrice = m_decTotalPrice;
                    order.m_dmlGet = m_dmlGet;
                    order.m_dmlPrice = m_dmlPrice;
                    if (m_intDisCount > this.m_objViewer.m_decLisDiscountMount)
                    {
                        order.m_decDiscount = this.m_objViewer.m_decLisDiscountMount;
                    }
                    else
                    {
                        order.m_decDiscount = 100;
                    }
                }
            }
        }
        /// <summary>
        /// �������뵥������� 
        /// </summary>
        /// <param name="CommitArr"></param>
        /// <param name="objLMVO"></param>
        /// <param name="itemArr_VO"></param>
        /// <returns></returns>
        private long m_mthSendTestApplyBillByCommit(List<clsCommitOrder> CommitArr, out clsLisApplMainVO objLMVO, out clsTestApplyItme_VO[] itemArr_VO)
        {
            List<clsTestApplyItme_VO> objTemp = new List<clsTestApplyItme_VO>();
            objLMVO = new clsLisApplMainVO();
            itemArr_VO = null;
            clsCommitOrder[] p_objCommitOrder = CommitArr.ToArray();
            int intLen = p_objCommitOrder.Length;
            clsTestApplyItme_VO item_VO = null;
            DataTable dt = null;
            //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
            for (int i = 0; i < intLen; i++)
            {
                #region �ӿ�����
                item_VO = new clsTestApplyItme_VO();
                item_VO.m_decPrice = p_objCommitOrder[i].m_dmlPrice;
                item_VO.m_decQty = p_objCommitOrder[i].m_dmlGet;
                item_VO.m_decTolPrice = p_objCommitOrder[i].m_decTotalPrice;
                // ����������Ŀ�������뵥ԪId
                item_VO.m_strItemID = p_objCommitOrder[i].m_strLISAPPLYUNITID_CHR;
                item_VO.m_strUsageID = p_objCommitOrder[i].m_strSAMPLEID_VCHR;
                item_VO.m_strItemName = p_objCommitOrder[i].m_strName;
                item_VO.m_strSpec = p_objCommitOrder[i].m_strSpec;
                item_VO.m_strSampleId = p_objCommitOrder[i].m_strSAMPLEID_VCHR;
                item_VO.m_strUnit = p_objCommitOrder[i].m_strUseunit;
                item_VO.m_strOutpatRecipeID = "";
                item_VO.m_strRowNo = i.ToString();
                item_VO.m_strOprDeptID = "";
                item_VO.strPartID = "";
                item_VO.m_strOutpatRecipeDeID = p_objCommitOrder[i].m_strChargeITEMID_CHR;//������ñ�����ĿID
                item_VO.m_strOrderID = p_objCommitOrder[i].m_strOrderID;
                item_VO.m_decDiscount = p_objCommitOrder[i].m_decDiscount;
                objTemp.Add(item_VO);
                #endregion
            }
            if (objTemp.Count <= 0)
            {
                objTemp.Add(new clsTestApplyItme_VO());
            }
            itemArr_VO = objTemp.ToArray();

            #region �շѲ��˻�������

            if (p_objCommitOrder.Length <= 0)
            {
                return -1;
            }
            //��VO����������
            objLMVO.m_intForm_int = 0;
            objLMVO.m_strAge = p_objCommitOrder[0].m_strAge + " ��";
            if (p_objCommitOrder[0].m_intSOURCETYPE_INT == 1)
            {
                objLMVO.m_strAppl_DeptID = p_objCommitOrder[0].m_strCREATEAREA_ID;
            }
            else
            {
                objLMVO.m_strAppl_DeptID = p_objCommitOrder[0].m_strCURAREAID_CHR;
            }
            string strEmployeeID = p_objCommitOrder[0].m_strCreatorID;
            objLMVO.m_strAppl_EmpID = p_objCommitOrder[0].m_strCreatorID;
            objLMVO.m_strDiagnose = p_objCommitOrder[0].m_strDIAGNOSE_VCHR;
            objLMVO.m_strOperator_ID = strEmployeeID;
            objLMVO.m_strPatient_Name = p_objCommitOrder[0].m_strPatientName;
            objLMVO.m_strPatientID = p_objCommitOrder[0].m_strPatientID;
            objLMVO.m_strPatientType = "1";
            objLMVO.m_strSex = p_objCommitOrder[0].m_strsex_chr;
            //�����־
            objLMVO.m_intEmergency = p_objCommitOrder[0].IsEmer;
            //�շ�״̬
            objLMVO.m_intChargeState = 1;
            //סԺ��
            objLMVO.m_strPatient_inhospitalno_chr = p_objCommitOrder[0].m_strINPATIENTID_CHR;
            //����
            objLMVO.m_strBedNO = p_objCommitOrder[0].m_strBedName;
            //��ʱ�������ڵĲ���(���õ��ֶ�)
            //objLMVO.m_strSummary = p_objCommitOrder[0].m_strCURAREAID_CHR;

            objLMVO.m_strOrderunitrelation = p_objCommitOrder[0].m_strOrderID;
            #endregion

            #region ���շѲ��˻�������
            //if (p_objCommitOrder.Length <= 0)
            //{
            //    return -1;
            //}

            //objLMVO.m_strInPatientID = p_objCommitOrder[0].m_strINPATIENTID_CHR;//סԺ��
            //objLMVO.m_strPatientCardID = p_objCommitOrder[0].m_strINPATIENTID_CHR;//סԺ��


            //objLMVO.m_enuPatientType = com.digitalwave.iCare.ValueObject.LIS.LisPatientType.Inpatient;
            //objLMVO.m_strRegisterID = p_objCommitOrder[0].m_strRegisterID;//�ǼǺ�
            //objLMVO.m_strPatientName = p_objCommitOrder[0].m_strPatientName;//��������
            //objLMVO.m_strSex = p_objCommitOrder[0].m_strsex_chr;//�Ա�
            //objLMVO.m_strBirthDate = p_objCommitOrder[0].m_dtmBirthDay.ToString("yyyy-MM-dd HH:mm:ss");
            //objLMVO.m_strAgeReferenceDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //objLMVO.m_strAppDoctorID = p_objCommitOrder[0].m_strCreatorID;//����ҽ��
            //if (p_objCommitOrder[0].m_intSOURCETYPE_INT == 1)//�������
            //{
            //    objLMVO.m_strAppDeptID = p_objCommitOrder[0].m_strCREATEAREA_ID;
            //}
            //else
            //{
            //    objLMVO.m_strAppDeptID = p_objCommitOrder[0].m_strCURAREAID_CHR;
            //}
            ////��������
            //objLMVO.m_strAppDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //objLMVO.m_strPATIENTID = p_objCommitOrder[0].m_strPatientID;//���˱��
            //objLMVO.m_strOperatorID = p_objCommitOrder[0].m_strCreatorID;//������


            #endregion
            if (itemArr_VO.Length > 0)
            {
                objLMVO.m_strSampleTypeID = p_objCommitOrder[0].m_strSAMPLEID_VCHR;//�����ȡ�������ʹ���
            }
            else
            {
                return 0;
            }


            return 1;
        }

        #region �������

        /// <summary>
        /// �����������ύ�����б� 2010/8/23
        /// </summary>
        /// <param name="SendCheckArr"></param>
        /// <param name="p_dtResultArr"></param>
        private void m_getPersonGroupList(List<clsCommitOrder> SendCheckArr, out List<string> personArr)
        {
            int intSendCount = SendCheckArr.Count;
            personArr = new List<string>(intSendCount);

            for (int i = 0; i < intSendCount; i++)
            {
                string m_strperID = SendCheckArr[i].m_strPatientID.ToString().Trim();
                if (!personArr.Contains(m_strperID))
                {
                    personArr.Add(m_strperID);
                }
            }
            personArr.TrimExcess();
        }
        #endregion

        #region ���ҩƷ����� -- ҽ��ִ�йؼ�����20180403
        /// <summary>
        /// ���ҩƷ����� -- ҽ��ִ�йؼ�����20180403
        /// </summary>
        /// <param name="p_lstExecOrderList"></param>
        /// <returns></returns>
        List<clsExecOrderVO> CheckMedicineKC(ref List<clsExecOrderVO> p_lstExecOrderList)
        {
            int intLstCount = p_lstExecOrderList.Count;
            List<clsExecOrderVO> lstExecOrderResult = new List<clsExecOrderVO>(intLstCount);
            //ҩƷID <ҩ��ID,<ҩƷID>>
            Dictionary<string, List<string>> dicMedicineID = new Dictionary<string, List<string>>();
            List<string> lstMedicineID = null;              // dtnMedicineID�е�value
            Dictionary<string, double> dicKC = null;        // <ҩ��ID*ҩID�������>
            Dictionary<string, double> dicXYL = new Dictionary<string, double>();       // <ҩ��ID*ҩID��������>
            Dictionary<string, List<clsExecOrderVO>> dicYYPO = new Dictionary<string, List<clsExecOrderVO>>();  // ��ҩҽ��<ҩ��ID*ҩID��<��ҩҽ������>>
            clsT_Bih_Opr_Putmeddetail_VO[] objPutmedVOArr = null;
            List<clsExecOrderVO> lstExceVO = null;
            long res = -1;
            string medId = string.Empty;                    // ҩƷID
            string pharmacyId = string.Empty;               // ҩ��ID
            string portfolioKey = string.Empty;             // ���������ҩ��ID*ҩƷID��
            //
            List<string> lstOrderId = new List<string>();
            Dictionary<string, decimal> dicGet = new Dictionary<string, decimal>();                 // key: ҩ��ID+OrderID
            Dictionary<string, decimal> dicGet2 = new Dictionary<string, decimal>();                // key: ҩ��ID+ҩƷID

            foreach (clsExecOrderVO ordervo in p_lstExecOrderList)
            {
                objPutmedVOArr = ordervo.m_arrPutmeddetail_VO;
                if (objPutmedVOArr != null)
                {
                    for (int i = 0; i < objPutmedVOArr.Length; i++)     // �ҳ�������ִ�е�ҩƷID
                    {
                        if (objPutmedVOArr[i].m_intMEDICNETYPE_INT == 3 && objPutmedVOArr[i].m_intPUTMEDTYPE_INT == 1)
                        {
                            continue;
                        }

                        medId = objPutmedVOArr[i].m_strMEDID_CHR;               // ҩƷID
                        pharmacyId = objPutmedVOArr[i].m_strMEDSTOREID_CHR;     // ҩ��ID
                        portfolioKey = pharmacyId + "*" + medId;                // �м��*�Ժ󷽱���
                        if (!dicMedicineID.ContainsKey(pharmacyId))
                        {
                            lstMedicineID = new List<string>() { medId };
                            dicMedicineID.Add(pharmacyId, lstMedicineID);
                        }
                        else
                        {
                            if (!dicMedicineID[pharmacyId].Contains(medId))
                            {
                                dicMedicineID[pharmacyId].Add(medId);
                            }
                        }

                        // ������ҩҽ��
                        if (!dicYYPO.ContainsKey(portfolioKey))
                        {
                            lstExceVO = new List<clsExecOrderVO>();
                            lstExceVO.Add(ordervo);
                            dicYYPO.Add(portfolioKey, lstExceVO);
                        }
                        else
                        {
                            lstExceVO = dicYYPO[portfolioKey];
                            if (!lstExceVO.Contains(ordervo))
                            {
                                dicYYPO[portfolioKey].Add(ordervo);
                            }
                        }
                        // Ԥ��ҩ���� 20180320
                        objPutmedVOArr[i].PretestDays = ordervo.PretestDays;
                        objPutmedVOArr[i].m_dblGET_DEC = objPutmedVOArr[i].m_dblGET_DEC * (ordervo.PretestDays == 0 ? 1 : ordervo.PretestDays + 1); // ���� = ���� * (Ԥ������+1)

                        // ����ҩƷ������
                        if (!dicXYL.ContainsKey(portfolioKey))
                        {
                            dicXYL.Add(portfolioKey, objPutmedVOArr[i].m_dblGET_DEC);
                        }
                        else
                        {
                            dicXYL[portfolioKey] += objPutmedVOArr[i].m_dblGET_DEC;
                        }
                    }
                    // �Ƴ���ҩ
                    if (!dicGet.ContainsKey(portfolioKey) && ordervo.PreAmount2 != 0)
                    {
                        dicGet.Add(portfolioKey, ordervo.PreAmount2);
                        lstOrderId.Add(ordervo.ORDERID_CHR);
                    }
                }
            }
            #region �Ƴ���ҩ
            if (lstOrderId.Count > 0)
            {
                Dictionary<string, string> dicMedId = new Dictionary<string, string>();
                //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                //{
                dicMedId = (new weCare.Proxy.ProxyIP()).Service.GetMedIdByOrderId(lstOrderId);
                //}
                foreach (string key in dicGet.Keys)
                {
                    if (dicMedId.ContainsKey(key.Split('*')[1]))
                    {
                        dicGet2.Add(key.Split('*')[0] + "*" + dicMedId[key.Split('*')[1]], dicGet[key]);
                    }
                }
                if (dicGet2.Count > 0)
                {
                    foreach (string key in dicGet2.Keys)
                    {
                        if (dicXYL.ContainsKey(key))
                        {
                            dicXYL[key] = dicXYL[key] - Convert.ToDouble(dicGet2[key]);     // ������ = ������ - Ԥ����
                        }
                    }
                }
            }
            #endregion

            if (dicMedicineID.Count != 0)
            {
                res = m_objManage.m_lngGetMedicineKC(dicMedicineID, out dicKC, out m_objDsStorageVOArr);
            }
            else
            {
                return p_lstExecOrderList;      // ����ҩƷ
            }
            if (res > 0 && dicKC != null)
            {
                #region ����������λת������С��λ
                if (m_objDsStorageVOArr != null)
                {
                    clsDsStorageVO objCurrentVO = null;
                    string strKey = string.Empty;
                    for (int i = 0; i < m_objDsStorageVOArr.Length; i++)
                    {
                        objCurrentVO = m_objDsStorageVOArr[i];
                        strKey = objCurrentVO.m_strPharmacyID + "*" + objCurrentVO.m_strMedicineID;
                        if (objCurrentVO.m_intIpChargeFlg == 0 && dicXYL.ContainsKey(strKey))       // ������λ
                        {
                            dicXYL[strKey] = dicXYL[strKey] * objCurrentVO.m_dblPackqty;            // ��С��λ��������λ*��װ��
                        }
                    }
                }
                #endregion

                List<string> lstMedID = new List<string>();         // ��¼�п�浫����ȫ�������ID
                #region ���˵�������
                foreach (string strKey in dicMedicineID.Keys)
                {
                    lstMedicineID = dicMedicineID[strKey];
                    for (int i = 0; i < lstMedicineID.Count; i++)   // ������lstMedicineID.Count����������
                    {
                        medId = lstMedicineID[i];
                        portfolioKey = strKey + "*" + medId;
                        if (dicXYL.ContainsKey(portfolioKey) && dicKC.ContainsKey(portfolioKey))
                        {
                            if (dicXYL[portfolioKey] <= dicKC[portfolioKey])    // ������<=�����
                            {
                                lstMedicineID.Remove(medId);
                                i--;
                            }
                            else if (dicKC[portfolioKey] > 0)                   // ���п��ֻ�ǲ���ȫ��
                            {
                                lstMedID.Add(portfolioKey);
                            }
                        }
                    }
                }
                #endregion

                #region ��dtnMedicineID��List<>.Count==0�ļ�¼�Ƴ�
                List<string> lstKeyForRemove = new List<string>(dicMedicineID.Count);
                foreach (string strKey in dicMedicineID.Keys)
                {
                    if (dicMedicineID[strKey].Count == 0)
                    {
                        lstKeyForRemove.Add(strKey);
                    }
                }
                lstKeyForRemove.TrimExcess();

                foreach (string strKey in lstKeyForRemove)
                {
                    dicMedicineID.Remove(strKey);
                }
                #endregion

                if (dicMedicineID.Count == 0)//��涼��
                {
                    lstExecOrderResult = p_lstExecOrderList;
                }
                else if (dicMedicineID.Count > 0)//�п�治��
                {
                    #region ��治��ʱ��֪��ѯ��
                    List<clsExecOrderVO> lstNonExcutablePO = new List<clsExecOrderVO>();//����ִ�л���ѡ��ִ�е�ҽ��
                    foreach (string strKey1 in dicMedicineID.Keys)//����ҩ��ID
                    {
                        lstMedicineID = dicMedicineID[strKey1];
                        foreach (string strKey2 in lstMedicineID)//����ҩƷID
                        {
                            portfolioKey = strKey1 + "*" + strKey2;//���ID
                            if (dicYYPO.ContainsKey(portfolioKey))
                            {
                                List<clsExecOrderVO> lstCurrent = dicYYPO[portfolioKey];
                                foreach (clsExecOrderVO objCurrentVO in lstCurrent)
                                {
                                    if (!lstNonExcutablePO.Contains(objCurrentVO))
                                    {
                                        lstNonExcutablePO.Add(objCurrentVO);
                                    }
                                }
                            }
                        }
                    }

                    #region �Ƴ���治���ҽ��
                    IEnumerator objEnuPO = null;
                    clsExecOrderVO objCurrentPO = null;
                    List<clsExecOrderVO> lstChooseVO = new List<clsExecOrderVO>();//��Ҫѡ���Ƿ�ִ�е�ҽ��

                    string strPortfolioKey2 = string.Empty;
                    for (int i1 = 0; i1 < lstNonExcutablePO.Count; i1++)
                    {
                        objCurrentPO = lstNonExcutablePO[i1];

                        if (objCurrentPO != null)
                        {
                            foreach (clsT_Bih_Opr_Putmeddetail_VO objCurrentVO in objCurrentPO.m_arrPutmeddetail_VO)
                            {
                                strPortfolioKey2 = objCurrentVO.m_strMEDSTOREID_CHR + "*" + objCurrentVO.m_strMEDID_CHR;
                                if (lstMedID.Contains(strPortfolioKey2) && dicKC.ContainsKey(strPortfolioKey2))
                                {
                                    if (objCurrentVO.m_dblGET_DEC <= dicKC[strPortfolioKey2])//����С�ڿ����
                                    {
                                        if (!lstChooseVO.Contains(objCurrentPO))
                                        {
                                            dicKC[strPortfolioKey2] = dicKC[strPortfolioKey2] - objCurrentVO.m_dblGET_DEC;//�ڿ������ȥ��ѡ���ҩ��
                                            lstChooseVO.Add(objCurrentPO);
                                            lstNonExcutablePO.Remove(objCurrentPO);
                                            i1--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //��¼����ִ�е�ҽ���Ĳ���registerID�ͷ��ţ�ȥ��ͬ������������
                    List<string> m_lstNoExeRecipenNo = new List<string>();
                    int intLstnoExeCount = lstNonExcutablePO.Count;
                    for (int i2 = 0; i2 < intLstnoExeCount; i2++)
                    {
                        if (!m_lstNoExeRecipenNo.Contains(lstNonExcutablePO[i2].m_strRegisterRecipenNo))
                        {
                            m_lstNoExeRecipenNo.Add(lstNonExcutablePO[i2].m_strRegisterRecipenNo);

                        }
                    }

                    string hint = string.Empty;
                    List<string> lstHint = new List<string>();
                    string strNoExecuOrder = "";
                    objEnuPO = p_lstExecOrderList.GetEnumerator();
                    while (objEnuPO.MoveNext())//�����Ƴ���治����ҽ��
                    {
                        clsExecOrderVO objCurrentOrder = objEnuPO.Current as clsExecOrderVO;

                        if (objCurrentOrder != null)
                        {
                            if (!m_lstNoExeRecipenNo.Contains(objCurrentOrder.m_strRegisterRecipenNo))
                            {
                                lstExecOrderResult.Add(objCurrentOrder);

                            }
                            else
                            {
                                hint = objCurrentOrder.m_objPatient.m_strBedName + "������:" + objCurrentOrder.m_objPatient.m_strPatientName + "\nҽ��:" + objCurrentOrder.m_strOrderName + "\n";
                                if (lstHint.IndexOf(hint) < 0)
                                {
                                    lstHint.Add(hint);
                                    //��ʾ��Ϣ����
                                    strNoExecuOrder += hint;
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(strNoExecuOrder))
                    {
                        if (MessageBox.Show(strNoExecuOrder, "��治��,����ִ��", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        {
                            lstExecOrderResult.Clear();
                        }
                    }
                    #endregion

                    #endregion
                }
            }
            if (lstExecOrderResult != null)
            {
                lstExecOrderResult.TrimExcess();
            }
            return lstExecOrderResult;
        }
        #endregion

        private void refreshThePatientCharge()
        {
            //ԭ����ȷ�Ĵ��� ArrayList m_arrRegisterid_chr = null;
            System.Collections.Generic.List<string> m_glstRegisterid_chr = null;
            GetTheExecuteRegisterID(out m_glstRegisterid_chr);
            this.m_objManage.m_lngGetChargeByRegisterids(m_glstRegisterid_chr, out m_dtChargeMoney, out m_dtPrepay);
        }

        /// <summary>
        /// ������ˮ��ɾ����
        /// </summary>
        /// <param name="m_arrCanNoOrder"></param>
        //ԭ���Ĵ���
        private void DelTheOrderFromDTView(string[] m_arrCanNoOrder)
        {
            int intOrderListRowsCount = this.m_objViewer.m_dtvOrderList.Rows.Count;
            DataGridViewRow row1 = null;
            DataGridViewRow row2 = null;
            for (int i = 0; i < intOrderListRowsCount; i++)
            {
                row1 = this.m_objViewer.m_dtvOrderList.Rows[i];
                for (int k = 0; k < m_arrCanNoOrder.Length; k++)
                {
                    if (m_arrCanNoOrder[k] == ((clsBIHCanExecOrder)row1.Tag).m_strOrderID)
                    {
                        if ((row1.Cells["dtv_bedcode"].Value != null && !row1.Cells["dtv_bedcode"].Value.ToString().Equals("")) || (row1.Cells["m_dtvLastName"].Value != null && !row1.Cells["m_dtvLastName"].Value.ToString().Equals("")))
                        {
                            if (i < this.m_objViewer.m_dtvOrderList.Rows.Count - 1)
                            {
                                row2 = this.m_objViewer.m_dtvOrderList.Rows[i + 1];
                                if (((clsBIHCanExecOrder)row1.Tag).m_strRegisterID.Equals(((clsBIHCanExecOrder)row2.Tag).m_strRegisterID))
                                {
                                    row2.Cells["dtv_bedcode"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strCURBEDName;
                                    row2.Cells["m_dtvLastName"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strPatientName;
                                    row2.Cells["dtv_DEPTNAME_VCHR"].Value = ((clsBIHCanExecOrder)row1.Tag).m_strCURAREAName;
                                }
                            }
                        }

                        this.m_objViewer.m_dtvOrderList.Rows.Remove(this.m_objViewer.m_dtvOrderList.Rows[i]);
                        i--;
                        intOrderListRowsCount = this.m_objViewer.m_dtvOrderList.Rows.Count;
                    }
                }
            }
        }

        internal bool ConfirmCurrentOrder(string[] m_arrORDERID, out string[] m_arrCanNoOrder)
        {
            //����ѡ�е�ҽ���ţ����´����ݿ��ҽ���ó�����������ǵ�����״̬��ȷ����Щҽ���ѱ�����״̬�������Ѿ���������ִ��
            DataTable m_dtOrder = null;
            //ԭ������ȷ���� m_arrCanNoOrder = new ArrayList();
            m_arrCanNoOrder = null;
            //p_glstNonExcutablePhysicianOrderID = new System.Collections.Generic.List<string>(p_glstORDERID.Count);//�Ѳ���ִ�е�ҽ�����봢��������
            long lngRef = m_objManage.m_lngConfirmCurrentOrder(m_arrORDERID, out m_dtOrder);//���ڿ��ܶ���ͬʱ����ҽ��ִ�У��ʴˣ�ÿ��ִ��ǰ��Ҫ�ڲ������ݿ������״̬���������������
            //List<string> lsstrOrderID = new List<string>();
            ArrayList strOrderIDArr = null;
            ArrayList arrCanNoOrder = new ArrayList();
            if (lngRef > 0 && m_dtOrder != null)
            {
                strOrderIDArr = new ArrayList();
                string orderid_chr = "", status_int = "";
                DateTime executedate_dat, today;
                bool m_blCan = false;
                int intRowsCount = m_dtOrder.Rows.Count;
                System.Data.DataRow objRow = null;
                for (int i = 0; i < intRowsCount; i++)
                {
                    m_blCan = false;
                    objRow = m_dtOrder.Rows[i];
                    orderid_chr = objRow["orderid_chr"].ToString().Trim();
                    status_int = objRow["status_int"].ToString().Trim();
                    DateTime.TryParse(objRow["executedate_dat"].ToString().Trim(), out executedate_dat);
                    DateTime.TryParse(objRow["sysdate"].ToString().Trim(), out today);
                    if (objRow["ordercateid_chr"].ToString().Trim() == "03")
                    {
                        strOrderIDArr.Add(orderid_chr);
                    }
                    if (status_int.Equals("5"))//��ʾ������ύ
                    {
                        m_blCan = true;
                    }
                    else if (status_int.Equals("2"))//��ʾ��ִ��
                    {
                        if (executedate_dat.Date == today.Date)//��ʾ������ִ��
                        {
                            m_blCan = false;
                        }
                        else
                        {
                            m_blCan = true;//��ʾ���컹û��ִ�У�����ִ��
                        }
                    }
                    if (!m_blCan)
                    {
                        arrCanNoOrder.Add(orderid_chr);
                        //p_glstNonExcutablePhysicianOrderID.Add(orderid_chr);
                    }
                }
                m_arrCanNoOrder = (string[])arrCanNoOrder.ToArray(typeof(string));
                //�޸ļ�����Ŀ״̬�������뵥������1��ʾסԺ
                if (strOrderIDArr.Count > 0)
                {
                    string[] strOrderID = (string[])strOrderIDArr.ToArray(typeof(string));
                    //com.digitalwave.iCare.middletier.LIS.clsLIS_Svc objSvc = (com.digitalwave.iCare.middletier.LIS.clsLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsLIS_Svc));
                    (new weCare.Proxy.ProxyLis()).Service.m_lngUpdateStatus(strOrderID, 1);
                }
            }
            if (m_arrCanNoOrder.Length > 0)
            {
                return false;
            }
            return true;
        }

        //ԭ������ȷ����
        private System.Collections.Generic.List<clsExecOrderVO> LessMoneyControl(System.Collections.Generic.List<clsExecOrderVO> m_glstAllExecutablePhysicianOrderList)
        {
            int intAllExecutablePhysicianOrderListCount = m_glstAllExecutablePhysicianOrderList.Count;

            //ԭ������ȷ���� ArrayList m_arrPatientList = new ArrayList();
            //Ԥ�ȷ�����ڴ�,�����Ͳ���Ҫ��������,���з�ʱ���ڴ����
            System.Collections.Generic.List<string> glstPatientList = new System.Collections.Generic.List<string>(intAllExecutablePhysicianOrderListCount);
            //ԭ������ȷ���� ArrayList m_arrCanNOPatientList = new ArrayList();
            System.Collections.Generic.List<string> glstPatientListWithNonexecutablePhysicainOrder = new System.Collections.Generic.List<string>(intAllExecutablePhysicianOrderListCount);
            //ԭ������ȷ���� ArrayList m_arrCanExeOrder = new ArrayList();
            System.Collections.Generic.List<clsExecOrderVO> glstFinalExecutablePhysicianOrderList = new System.Collections.Generic.List<clsExecOrderVO>(intAllExecutablePhysicianOrderListCount);

            clsBIHPatientInfo m_objPatient = null;
            decimal m_decSum = 0;
            clsExecOrderVO objExecutablePhysicianOrderVO = null;
            string strRegisterID = null;

            for (int i = 0; i < intAllExecutablePhysicianOrderListCount; i++)
            {
                objExecutablePhysicianOrderVO = m_glstAllExecutablePhysicianOrderList[i];
                strRegisterID = objExecutablePhysicianOrderVO.m_objPatient.m_strRegisterID;

                if (!glstPatientList.Contains(strRegisterID))
                {
                    glstPatientList.Add(strRegisterID);
                    //ԭ������ȷ���� m_arrPatientList.Add(((clsExecOrderVO)m_arrExecOrder[i]).m_objPatient.m_strRegisterID);
                }
            }

            int intPatientListCount = glstPatientList.Count;

            for (int i = 0; i < intPatientListCount; i++)
            {
                m_decSum = 0;
                m_objPatient = null;
                strRegisterID = glstPatientList[i];
                for (int j = 0; j < intAllExecutablePhysicianOrderListCount; j++)
                {
                    objExecutablePhysicianOrderVO = m_glstAllExecutablePhysicianOrderList[j];

                    if (strRegisterID.Equals(objExecutablePhysicianOrderVO.m_objPatient.m_strRegisterID))
                    {
                        m_objPatient = objExecutablePhysicianOrderVO.m_objPatient;
                        m_decSum += objExecutablePhysicianOrderVO.m_decChargeMedItemMoney + objExecutablePhysicianOrderVO.m_decChargeNoMedItemMoney;
                    }
                }
                if (m_objPatient != null)
                {
                    if (m_objPatient.m_decPrePayMoney < m_decSum)
                    {
                        string m_strAlert = "���ˣ�" + m_objPatient.m_strPatientName + "\r\n"; ;
                        m_strAlert += "����ִ��ҽ�����ʼ��ʽ��Ϊ" + m_decSum.ToString("0.00") + "Ԫ��������Ϊ" + m_objPatient.m_decPrePayMoney.ToString("0.00") + "Ԫ���Ƿ�ȷ��ִ��";
                        if (MessageBox.Show(m_strAlert, "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                        }
                        else
                        {
                            if (!glstPatientListWithNonexecutablePhysicainOrder.Contains(m_objPatient.m_strRegisterID))
                            //ԭ������ȷ���� if (!m_arrCanNOPatientList.Contains(m_objPatient.m_strRegisterID))
                            {
                                //ԭ������ȷ���� m_arrCanNOPatientList.Add(m_objPatient.m_strRegisterID);
                                glstPatientListWithNonexecutablePhysicainOrder.Add(m_objPatient.m_strRegisterID);
                            }
                        }

                    }
                }
            }
            //�����Ч�Ĳ���ִ��VO
            for (int j = 0; j < intAllExecutablePhysicianOrderListCount; j++)
            {
                objExecutablePhysicianOrderVO = m_glstAllExecutablePhysicianOrderList[j];
                if (!glstPatientListWithNonexecutablePhysicainOrder.Contains(objExecutablePhysicianOrderVO.m_objPatient.m_strRegisterID))
                {
                    //ԭ������ȷ���� m_arrCanExeOrder.Add(m_arrExecOrder[j]);
                    glstFinalExecutablePhysicianOrderList.Add(objExecutablePhysicianOrderVO);
                }
            }
            //ԭ������ȷ���� return m_arrCanExeOrder;
            return glstFinalExecutablePhysicianOrderList;
        }

        /// <summary>
        /// ҽ��ִ�йؼ����� 20180403 (��Ҫ��ȡ��ҩ��Ϣ)
        /// </summary>
        /// <param name="order"></param>
        /// <param name="ExecVO"></param>
        void CreateTheMedicineVos(clsBIHCanExecOrder order, clsExecOrderVO ExecVO)
        {
            ArrayList m_arrPatientChareVO = new ArrayList();
            if (ExecVO.ISINCEPT_INT == 1)
            {
                if (ExecVO.m_arrPatientChareVO != null && ExecVO.m_arrPatientChareVO.Length > 0)
                {
                    for (int j = 0; j < ExecVO.m_arrPatientChareVO.Length; j++)
                    {
                        clsBihPatientCharge_VO PatientChareVO = (clsBihPatientCharge_VO)ExecVO.m_arrPatientChareVO[j];
                        if (PatientChareVO.m_intITEMSRCTYPE_INT == 1 && ExecVO.m_arrPatientChareVO[j].m_intPUTMEDTYPE_INT != 0)//ҩƷ������Ҫ��ҩ
                        {
                            #region ��ҩVO
                            clsT_Bih_Opr_Putmeddetail_VO m_objPutmeddetail_VO = new clsT_Bih_Opr_Putmeddetail_VO();
                            m_objPutmeddetail_VO.m_strPUTMEDDETAILID_CHR = ExecVO.m_arrPatientChareVO[j].m_strPUTMEDREQID_CHR;
                            m_objPutmeddetail_VO.m_strAREAID_CHR = (string)this.m_objViewer.m_txtArea.Tag;
                            m_objPutmeddetail_VO.m_strBedID = ExecVO.m_strEXEBEDID_CHR;//����ʱ�������ڵĲ���ID
                            m_objPutmeddetail_VO.m_strPAIENTID_CHR = ExecVO.m_arrPatientChareVO[j].PatientID;
                            m_objPutmeddetail_VO.m_strREGISTERID_CHR = ExecVO.m_arrPatientChareVO[j].RegisterID;
                            m_objPutmeddetail_VO.m_strORDERID_CHR = ExecVO.m_arrPatientChareVO[j].OrderID;
                            m_objPutmeddetail_VO.m_strORDEREXECID_CHR = "";
                            m_objPutmeddetail_VO.m_intORDEREXECTYPE_INT = ExecVO.m_arrPatientChareVO[j].OrderExecType;
                            m_objPutmeddetail_VO.m_intRECIPENO_INT = order.m_intRecipenNo2;
                            m_objPutmeddetail_VO.m_dblDOSAGE_DEC = (double)ExecVO.m_arrPatientChareVO[j].m_decDOSAGE_DEC;
                            m_objPutmeddetail_VO.m_strDOSAGEUNIT_VCHR = ExecVO.m_arrPatientChareVO[j].m_strDOSAGEUNIT_CHR;
                            m_objPutmeddetail_VO.m_strCHARGEITEMID_CHR = ExecVO.m_arrPatientChareVO[j].ChargeItemID;
                            m_objPutmeddetail_VO.m_strMEDID_CHR = ExecVO.m_arrPatientChareVO[j].m_strITEMSRCID_VCHR;
                            m_objPutmeddetail_VO.m_strMEDNAME_VCHR = ExecVO.m_arrPatientChareVO[j].ChargeItemName;
                            m_objPutmeddetail_VO.m_intISRICH_INT = ExecVO.m_arrPatientChareVO[j].IsRich;

                            m_objPutmeddetail_VO.m_strDOSETYPEID_CHR = order.m_strDosetypeID;
                            m_objPutmeddetail_VO.m_strEXECFREQID_CHR = order.m_strExecFreqID;
                            m_objPutmeddetail_VO.m_intEXECTIMES_INT = ExecVO.EXECUTETIME_INT;
                            m_objPutmeddetail_VO.m_intEXECDAYS_INT = ExecVO.EXECUTEDAYS_INT;
                            m_objPutmeddetail_VO.m_dblUNITPRICE_MNY = (double)ExecVO.m_arrPatientChareVO[j].UnitPrice;

                            m_objPutmeddetail_VO.m_strUNIT_VCHR = ExecVO.m_arrPatientChareVO[j].Unit;
                            m_objPutmeddetail_VO.m_dblGET_DEC = (double)ExecVO.m_arrPatientChareVO[j].Amount;
                            m_objPutmeddetail_VO.m_strCREATOR_CHR = this.m_objViewer.LoginInfo.m_strEmpID;

                            m_objPutmeddetail_VO.m_strPCHARGEID_CHR = ExecVO.m_arrPatientChareVO[j].PchargeID;
                            m_objPutmeddetail_VO.m_strEXECTIME_VCHR = ExecVO.EXECUTEDATE_VCHR;
                            m_objPutmeddetail_VO.m_intNEEDCONFIRM_INT = ExecVO.NEEDCONFIRM_INT;
                            m_objPutmeddetail_VO.m_intACTIVATETYPE_INT = ExecVO.m_arrPatientChareVO[j].ActivateType;

                            m_objPutmeddetail_VO.m_intITEMSRCTYPE_INT = ExecVO.m_arrPatientChareVO[j].m_intITEMSRCTYPE_INT;
                            m_objPutmeddetail_VO.m_intISRECRUIT_INT = ExecVO.ISRECRUIT_INT;
                            m_objPutmeddetail_VO.m_intOUTGETMEDDAYS_INT = order.m_intOUTGETMEDDAYS_INT;
                            m_objPutmeddetail_VO.m_intPUTMEDTYPE_INT = ExecVO.m_arrPatientChareVO[j].m_intPUTMEDTYPE_INT;
                            m_objPutmeddetail_VO.m_intMEDICNETYPE_INT = ExecVO.m_arrPatientChareVO[j].m_intMEDICNETYPE_INT;
                            m_objPutmeddetail_VO.m_strMEDSTOREID_CHR = ExecVO.m_arrPatientChareVO[j].ClacArea; // ҩ��ID
                            m_arrPatientChareVO.Add(m_objPutmeddetail_VO);
                            #endregion
                        }
                    }
                }
            }
            clsT_Bih_Opr_Putmeddetail_VO[] Putmeddetail_VOs = null;
            if (m_arrPatientChareVO.Count > 0)
            {
                Putmeddetail_VOs = (clsT_Bih_Opr_Putmeddetail_VO[])m_arrPatientChareVO.ToArray(typeof(clsT_Bih_Opr_Putmeddetail_VO));
            }
            ExecVO.m_arrPutmeddetail_VO = Putmeddetail_VOs;

        }

        //ԭ������ȷ���� private void SetTheNurseCharge(ArrayList m_arrNurOrders, ArrayList m_arrExecOrder)
        private void SetTheNurseCharge(ArrayList m_arrNurOrders, System.Collections.Generic.List<clsExecOrderVO> p_glstExecutablePhysicianOrderList)
        {
            int intNurOrdersCount = m_arrNurOrders.Count;
            int intExecutablePhysicianOrderListCount = p_glstExecutablePhysicianOrderList.Count;
            clsExecOrderVO objOneExecutablePhysicianOrder = null;

            if (intNurOrdersCount > 0 && intExecutablePhysicianOrderListCount > 0)
            {
                for (int i = 0; i < intExecutablePhysicianOrderListCount; i++)
                {
                    objOneExecutablePhysicianOrder = p_glstExecutablePhysicianOrderList[i];

                    //ԭ����ȷ�Ĵ��� if (((clsExecOrderVO)m_arrExecOrder[i]).m_arrPatientChareVO != null && ((clsExecOrderVO)m_arrExecOrder[i]).m_arrPatientChareVO.Length > 0)
                    if (objOneExecutablePhysicianOrder.m_arrPatientChareVO != null && (objOneExecutablePhysicianOrder.m_arrPatientChareVO.Length > 0))
                    {
                        //ԭ����ȷ�Ĵ��� m_arrNurOrders.Contains(((clsExecOrderVO)m_arrExecOrder[i]).ORDERID_CHR.Trim()))//�����ǰҽ�����ڻ���ҽ������Ҫ�޸ķ��ñ���ı�־PATIENTNURSE_INT
                        if (m_arrNurOrders.Contains(objOneExecutablePhysicianOrder.ORDERID_CHR.Trim()))//�����ǰҽ�����ڻ���ҽ������Ҫ�޸ķ��ñ���ı�־PATIENTNURSE_INT
                        {
                            //ԭ������ȷ���� for (int j = 0; j < ((clsExecOrderVO)m_arrExecOrder[i]).m_arrPatientChareVO.Length; j++)
                            for (int j = 0; j < objOneExecutablePhysicianOrder.m_arrPatientChareVO.Length; j++)
                            {
                                //ԭ������ȷ���� ((clsExecOrderVO)m_arrExecOrder[i]).m_arrPatientChareVO[j].PATIENTNURSE_INT = 1;

                                //objOneExecutablePhysicianOrder�������ݵ��޸��Ƿ�ᷴӳ��ԭ��m_glstExecutablePhysicianOrderList����������,��Ҫ��֤!!!
                                p_glstExecutablePhysicianOrderList[i].m_arrPatientChareVO[j].PATIENTNURSE_INT = 1;
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// ������ʳ������ʷ��¼��VO
        /// </summary>
        /// <param name="clsCommitOrder"></param>
        /// <returns></returns>
        private clsPatientNurseVO GetThePatientNurseVO(clsCommitOrder clsCommitOrder)
        {
            clsPatientNurseVO m_objVo = new clsPatientNurseVO();
            m_objVo.m_dtACTIVE_DAT = DateTime.Now;
            m_objVo.m_intACTIVE_INT = 1;
            m_objVo.m_strOPERATORID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
            m_objVo.m_strORDERDICID_CHR = clsCommitOrder.m_strOrderDicID;
            m_objVo.m_strOrderDicName = clsCommitOrder.m_strName.Trim();
            m_objVo.m_strREGISTERID_CHR = clsCommitOrder.m_strRegisterID;
            return m_objVo;
        }

        /// <summary>
        /// ѡ���ִ�еĲ����б�
        /// </summary>
        /// <returns></returns>
        public bool SelectTheOrderFromPerson()
        {
            clsBIHCanExecOrder[] arrBed = (clsBIHCanExecOrder[])getThePersonFromView(0).ToArray(typeof(clsBIHCanExecOrder));
            if (arrBed.Length == 0)
            {
                return false;
            }
            m_arrRegisterID = new ArrayList();
            //Ƿ�Ѳ����б�
            ArrayList m_arrPatient = null;
            frmBedPatientList objForm = null;

            if (this.m_objViewer.m_blMoneyControl == false)//
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                //objForm.m_arrPatient = GetTheOverChargeList(objForm.arrBed);
                SetTheAllCheck();
                ArrayList arrSelected = GetTheSelectArrItemList();
                decimal m_decSum = 0;
                Hashtable m_htChargeSum = new Hashtable();
                for (int j = 0; j < arrSelected.Count; j++)
                {
                    m_decSum = 0;
                    clsBIHCanExecOrder order = (clsBIHCanExecOrder)arrSelected[j];
                    if (!m_htChargeSum.ContainsKey(order.m_strRegisterID))
                    {
                        m_htChargeSum.Add(order.m_strRegisterID, m_decSum);
                    }
                    m_decSum = (decimal)m_htChargeSum[order.m_strRegisterID];
                    m_decSum += m_objInputOrder.GettheChargeSum(order, m_dtChargeList);
                    m_htChargeSum[order.m_strRegisterID] = m_decSum;
                }
                m_arrPatient = GetTheOverChargeList(arrBed, m_htChargeSum);
            }
            else
            {
                m_arrPatient = new ArrayList();
            }
            objForm = new frmBedPatientList(m_arrPatient, 1);
            objForm.arrBed = arrBed;
            this.m_objViewer.Cursor = Cursors.Default;
            if (objForm.ShowDialog() == DialogResult.OK)
            {

                m_arrRegisterID = objForm.m_arrPersionID;
                if (m_arrRegisterID.Count > 0)
                {
                    // this.m_objViewer.cmdRefurbish_Click(null, null);
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
        /// ѡ�������ִ�еĲ����б�
        /// </summary>
        /// <returns></returns>
        public bool SelectTheRedrawOrderFromPerson()
        {
            clsBIHCanExecOrder[] arrBed = (clsBIHCanExecOrder[])getThePersonFromView(1).ToArray(typeof(clsBIHCanExecOrder));
            if (arrBed.Length == 0)
            {
                return false;
            }
            m_arrRegisterID = new ArrayList();
            //Ƿ�Ѳ����б�
            ArrayList m_arrPatient = null;
            frmBedPatientList objForm = null;
            m_arrPatient = new ArrayList();
            objForm = new frmBedPatientList(m_arrPatient, 1);
            objForm.arrBed = arrBed;
            if (objForm.ShowDialog() == DialogResult.OK)
            {

                m_arrRegisterID = objForm.m_arrPersionID;
                if (m_arrRegisterID.Count > 0)
                {
                    this.m_objViewer.cmdRefurbish_Click(null, null);
                }
                else
                {
                    return false;
                }
                SetTheDrawCheckFromPersionList(m_arrRegisterID);
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// ���Ƿ�ѵĲ����б�
        /// </summary>
        /// <param name="clsBIHCanExecOrder"></param>
        /// <returns></returns>
        private ArrayList GetTheOverChargeList(clsBIHCanExecOrder[] clsBIHCanExecOrder, Hashtable m_htChargeSum)
        {
            ArrayList m_arr = new ArrayList();
            for (int i = 0; i < clsBIHCanExecOrder.Length; i++)
            {
                DataView myDataView = new DataView(m_dtPatients);
                myDataView.RowFilter = "registerid_chr='" + clsBIHCanExecOrder[i].m_strRegisterID + "'";
                myDataView.Sort = "REMARK_NO desc";
                if (myDataView.Count <= 0)
                {
                    continue;
                }
                clsBIHPatientInfo m_objPatient;
                m_mthGetPatientInfoFromDateTable(myDataView, out m_objPatient);
                if (m_objPatient.m_intREMARK_INT == 1 && m_objPatient.m_intCHARGECTL_INT == 1)
                {
                    continue;
                }
                decimal m_decSum = 0;
                if (m_htChargeSum.ContainsKey(m_objPatient.m_strRegisterID))
                {
                    m_decSum = (decimal)m_htChargeSum[m_objPatient.m_strRegisterID];
                }
                //for (int j = 0; j < clsBIHCanExecOrder.Length; j++)
                //{
                //    clsBIHCanExecOrder order = (clsBIHCanExecOrder)clsBIHCanExecOrder[j];
                //    if (order.m_strRegisterID.Equals(m_objPatient.m_strRegisterID))
                //    {
                //        m_decSum += m_objInputOrder.GettheChargeSum(order, m_dtChargeList);
                //    }
                //}
                if (m_objPatient.m_decPrePayMoney + m_objPatient.m_decinsuredsum_mny - m_decSum < 0)
                {
                    if (!m_arr.Contains(clsBIHCanExecOrder[i].m_strRegisterID))
                    {
                        m_arr.Add(clsBIHCanExecOrder[i].m_strRegisterID);
                    }
                }
            }
            return m_arr;
        }

        /// <summary>
        /// ѡ�в��˴�ִ�е�ҽ��
        /// </summary>
        /// <param name="arr"></param>
        private void SetTheCheckFromPersionList(ArrayList arr)
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                string m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (arr.Contains(m_strRegisterID))
                {
                    if (CanExecOrder((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag))
                    {
                        m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";
                    }
                    else
                    {
                        m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                    }
                }
                else
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                }
            }
        }
        /// <summary>
        /// ѡ�в��˴�ִ�е�ҽ��
        /// </summary>
        /// <param name="arr"></param>
        private void SetTheAllCheck()
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {

                m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";

            }
        }
        /// <summary>
        /// ѡ�в��˴�����ִ�е�ҽ��
        /// </summary>
        /// <param name="arr"></param>
        private void SetTheDrawCheckFromPersionList(ArrayList arr)
        {
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.RowCount; i++)
            {
                string m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (arr.Contains(m_strRegisterID))
                {
                    if (CanExecDrawOrder((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag))
                    {
                        m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "1";
                    }
                    else
                    {
                        m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                    }
                }
                else
                {
                    m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value = "0";
                }
            }
        }


        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="m_intCase">0-ִ��ҽ��������1-����ҽ��ִ�в���</param>
        /// <returns></returns>
        private ArrayList getThePersonFromView(int m_intCase)
        {
            int cout = 0;
            ArrayList arr = new ArrayList();
            ArrayList arr2 = new ArrayList();
            string m_strRegisterID = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_intCase == 0)
                {
                    if (CanExecOrder((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag) == false)
                    {
                        continue;
                    }
                }
                else if (m_intCase == 1)
                {
                    if (CanExecDrawOrder((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag) == false)
                    {
                        continue;
                    }
                }
                m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (!arr.Contains(m_strRegisterID))
                {
                    arr.Add(m_strRegisterID);
                    arr2.Add((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag);
                }

            }

            return arr2;
        }

        //ˢ�µ�ǰ����
        public void refreshTheData()
        {
            this.m_objViewer.Cursor = Cursors.WaitCursor;
            LoadTheDate();
            this.m_objViewer.Cursor = Cursors.Default;
            //��ǰ�¿�ҽ������ͳ��


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
                if (CanExecOrder((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag) == false)
                {
                    continue;
                }
                string m_strRegisterID = ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strRegisterID;
                if (!arr.Contains(m_strRegisterID))
                {
                    arr.Add(m_strRegisterID);
                }

            }
            cout = arr.Count;
            return cout;
        }


        private string GetTheSelectItem()
        {
            string orderArr = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Trim() == "1")
                {
                    orderArr += "'" + ((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID + "',";

                }
            }
            orderArr = orderArr.TrimEnd(',');
            return orderArr;
        }

        /// <summary>
        /// ��õ�ǰѡ�еĿ�ִ�е�ҽ����Ŀ
        /// </summary>
        /// <returns></returns>
        private System.Collections.Generic.List<string> GetTheSelectArrItem()
        {
            m_objViewer.m_dtvOrderList.RefreshEdit();
            int intOrderListRowsCount = this.m_objViewer.m_dtvOrderList.Rows.Count;
            DataGridViewRow objDataGridViewRow = null;
            m_glstExecOrderList = new System.Collections.Generic.List<clsBIHCanExecOrder>(intOrderListRowsCount);
            List<string> glstExcutablePhysicianOrderID = new List<string>(intOrderListRowsCount);

            for (int i = 0; i < intOrderListRowsCount; i++)
            {
                objDataGridViewRow = m_objViewer.m_dtvOrderList.Rows[i];
                clsBIHCanExecOrder objBIHCanExecOrder = (clsBIHCanExecOrder)objDataGridViewRow.Tag;

                if (objDataGridViewRow.Cells["m_dtvselectCheck"].Value.Equals("1"))
                {
                    if (CanExecOrder(objBIHCanExecOrder))
                    {
                        glstExcutablePhysicianOrderID.Add(objBIHCanExecOrder.m_strOrderID);
                        m_glstExecOrderList.Add(objBIHCanExecOrder);
                    }
                }
            }
            glstExcutablePhysicianOrderID.TrimExcess();
            m_glstExecOrderList.TrimExcess();
            return glstExcutablePhysicianOrderID;
        }

        /// <summary>
        /// ��õ�ǰѡ�еĿ�ִ�е�ҽ����ĿVO����
        /// </summary>
        /// <returns></returns>
        private ArrayList GetTheSelectArrItemList()
        {
            ArrayList orderArr = new ArrayList();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Equals("1"))
                {
                    orderArr.Add(((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag));
                }
            }

            return orderArr;
        }

        /// <summary>
        /// ��õ�ǰѡ�еĿɳ���ִ�е�ҽ����Ŀ
        /// </summary>
        /// <returns></returns>
        private List<clsBIHCanExecOrder> GetTheSelectDrawArrItem()
        {
            List<clsBIHCanExecOrder> orderArr = new List<clsBIHCanExecOrder>();
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                if (m_objViewer.m_dtvOrderList.Rows[i].Cells["m_dtvselectCheck"].Value.ToString().Equals("1"))
                {
                    orderArr.Add((clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.Rows[i].Tag);
                }
            }

            return orderArr;
        }
        /// <summary>
        /// �õ���ѡ�е�ҽ����������  -- ҽ��ִ�йؼ����� 20180403
        /// </summary>
        /// <returns></returns>
        List<clsCommitOrder> GetTheSelectExecOrderArrItem(ref List<clsExecOrderVO> p_glstExecOrderVOList)
        {
            int intExecOrderListCount = m_glstExecOrderList.Count;
            List<clsCommitOrder> glstCommitOrderList = new List<clsCommitOrder>(intExecOrderListCount);
            p_glstExecOrderVOList = new List<clsExecOrderVO>(intExecOrderListCount);

            clsBIHCanExecOrder order = null;
            this.objFrmExecuteOrdersProgress.Show();
            this.objFrmExecuteOrdersProgress.lblOrderCount.Text = "��" + intExecOrderListCount.ToString() + "��ҽ��";
            for (int i = 0; i < intExecOrderListCount; i++)
            {
                order = m_glstExecOrderList[i];
                this.objFrmExecuteOrdersProgress.lblOrderNum.Text = (i + 1).ToString();
                this.objFrmExecuteOrdersProgress.lblPatientName.Text = order.m_strPatientName;
                this.objFrmExecuteOrdersProgress.lblPhysicianOrder.Text = order.m_strName;

                this.objFrmExecuteOrdersProgress.Refresh();
                Application.DoEvents();
                if (order.m_intRepair == 0)
                {
                    m_intRepairEveVo = 0;
                    clsCommitOrder CommitOrder = new clsCommitOrder();
                    GetCommitOrderFromExecOrder(order, ref CommitOrder, ref p_glstExecOrderVOList);

                    glstCommitOrderList.Add(CommitOrder);
                }
                else if (order.m_intRepair == 1)
                {
                    m_intRepairEveVo = 1;
                    for (int k = 0; k < order.m_intRepairCount; k++)
                    {
                        m_intRepairEveVoCount = k;
                        clsCommitOrder CommitOrder = new clsCommitOrder();
                        GetCommitOrderFromExecOrder(order, ref CommitOrder, ref p_glstExecOrderVOList);

                        glstCommitOrderList.Add(CommitOrder);
                    }
                }
                else if (order.m_intRepair == 2)
                {
                    m_intRepairEveVo = 0;
                    for (int k = 0; k < order.m_intRepairCount + 1; k++)//order.m_intRepairCount + 1��ԭ������Ϊ������Ҫ�����ҲҪִ��һ�Σ����Լ�һ��
                    {
                        m_intRepairEveVoCount = k;//ִ�е���ִ�д���(0-�ǲ�ִ��,1-��ִ��)������
                        clsCommitOrder CommitOrder = new clsCommitOrder();
                        GetCommitOrderFromExecOrder(order, ref CommitOrder, ref p_glstExecOrderVOList);
                        glstCommitOrderList.Add(CommitOrder);
                        m_intRepairEveVo = 1;
                    }
                }
            }
            return glstCommitOrderList;
        }
        /// <summary>
        /// ҽ��ִ�йؼ����� 20180403
        /// </summary>
        /// <param name="order"></param>
        /// <param name="p_objCommitOrder"></param>
        /// <param name="p_glstExecOrderVO_List"></param>
        void GetCommitOrderFromExecOrder(clsBIHCanExecOrder order, ref clsCommitOrder p_objCommitOrder, ref System.Collections.Generic.List<clsExecOrderVO> p_glstExecOrderVO_List)
        {
            DataView myDataView = new DataView(m_dtPatients);

            if (myDataView.Count == 0)
                return;
            myDataView.RowFilter = "registerid_chr='" + order.m_strRegisterID + "'";
            myDataView.Sort = "REMARK_NO desc";
            clsBIHPatientInfo m_objPatient = null;
            if (myDataView.Count > 0)
            {
                m_mthGetPatientInfoFromDateTable(myDataView, out m_objPatient);//���ز�����Ϣ
            }
            myDataView = new DataView(m_dtChargeList);
            myDataView.RowFilter = "orderid_chr='" + order.m_strOrderID + "'";
            myDataView.Sort = "FLAG_INT";
            clsChargeForDisplay[] m_arrObjItem = null;
            if (myDataView.Count > 0)
            {
                GetChargeListFromDateTable(myDataView, out m_arrObjItem);//���ظ���ҽ��ִ�е������з�����Ϣ�����Ƿ��ҩ��Ϣ�ı�־

            }
            p_objCommitOrder.m_strCreatorID = order.m_strCreatorID;
            p_objCommitOrder.m_strCreator = order.m_strCreator;
            p_objCommitOrder.m_strCREATEAREA_ID = order.m_strCREATEAREA_ID;
            p_objCommitOrder.m_strCREATEAREA_Name = order.m_strCREATEAREA_Name;
            p_objCommitOrder.m_strCURAREAID_CHR = order.m_strCURAREAID_CHR;
            p_objCommitOrder.m_strCURAREAName = order.m_strCURAREAName;
            p_objCommitOrder.m_strCURBEDID_CHR = order.m_strCURBEDID_CHR;
            p_objCommitOrder.m_strCURBEDName = order.m_strCURBEDName;
            p_objCommitOrder.m_strDOCTORID_CHR = order.m_strDOCTORID_CHR;
            p_objCommitOrder.m_strDOCTOR_VCHR = order.m_strDOCTOR_VCHR;

            p_objCommitOrder.m_strOrderID = order.m_strOrderID;
            p_objCommitOrder.m_strName = order.m_strName;
            p_objCommitOrder.m_strOrderDicID = order.m_strOrderDicID;//������ĿID
            p_objCommitOrder.m_strPARTID_VCHR = order.m_strPARTID_VCHR;
            p_objCommitOrder.m_strSpec = order.m_strSpec;//���{=This.Getҽ����Ŀ.GetҩƷ.���}
            p_objCommitOrder.m_strUseunit = order.m_strDosageUnit;
            p_objCommitOrder.m_strPARTNAME_VCHR = order.m_strPARTNAME_VCHR;
            p_objCommitOrder.m_strOrderDicCateID = order.m_strOrderDicCateID;
            p_objCommitOrder.m_strOrderDicCateName = order.m_strOrderDicCateName;
            p_objCommitOrder.m_strAPPLYTYPEID_CHR = order.m_strAPPLYTYPEID_CHR; // ���뵥���ID(AR_APPLY_TYPELIST)(���)
            p_objCommitOrder.m_strLISAPPLYUNITID_CHR = order.m_strLISAPPLYUNITID_CHR;//�������뵥ԪID(t_aid_lis_apply_unit)
            p_objCommitOrder.m_strDEPTID_CHR = order.m_strCREATEAREA_ID;
            p_objCommitOrder.m_strDEPTNAME_VCHR = order.m_strCREATEAREA_Name;
            p_objCommitOrder.m_strRegisterID = order.m_strRegisterID;
            p_objCommitOrder.m_strCHARGEDOCTORGROUPID = order.m_strCHARGEDOCTORGROUPID;//

            p_objCommitOrder.m_strSAMPLEID_VCHR = order.m_strSAMPLEID_VCHR;//��������ID
            p_objCommitOrder.m_strSAMPLEName_VCHR = order.m_strSAMPLEName_VCHR;//����������

            if (m_objPatient != null)
            {
                p_objCommitOrder.m_strINPATIENTID_CHR = m_objPatient.m_strInHospitalNo;
                p_objCommitOrder.m_strAge = m_objPatient.m_strAge;
                p_objCommitOrder.m_strPATIENTCARDID_CHR = m_objPatient.m_strPATIENTCARDID_CHR;
                p_objCommitOrder.m_strDIAGNOSE_VCHR = m_objPatient.m_strDiagnose;
                p_objCommitOrder.m_strPatientName = m_objPatient.m_strPatientName;

                p_objCommitOrder.m_strsex_chr = m_objPatient.m_strSex;
                p_objCommitOrder.m_strBedName = m_objPatient.m_strBedName;//ɾ��
                p_objCommitOrder.m_strParentID = m_objPatient.m_strPatientID;
                p_objCommitOrder.m_strAreaID = m_objPatient.m_strAreaID;
                p_objCommitOrder.m_strAreaName = m_objPatient.m_strAreaName;
                p_objCommitOrder.m_strBedID = m_objPatient.m_strBedID;
                p_objCommitOrder.m_strBedName = m_objPatient.m_strBedName;
                p_objCommitOrder.Birthday = m_objPatient.m_dtBorn.ToString("yyyy-MM-dd");
            }

            if (m_arrObjItem != null && m_arrObjItem.Length > 0)
            {
                decimal m_dmlPrice = 0;//�������ܼ�
                clsChargeForDisplay m_arrLipMainItem = null;//������շ���
                clsORDERCHARGEDEPT_VO objOrderChargeDept_VO = null;//new code

                for (int i = 0; i < m_arrObjItem.Length; i++)
                {
                    objOrderChargeDept_VO = m_arrObjItem[i].m_objORDERCHARGEDEPT_VO;//new code

                    if (objOrderChargeDept_VO.m_intFLAG_INT == 0)//��������Ŀ
                    {
                        m_arrLipMainItem = m_arrObjItem[i];
                        m_dmlPrice += objOrderChargeDept_VO.m_decAmount_dec * objOrderChargeDept_VO.m_decUnitprice_dec; //new code
                    }
                    else if (objOrderChargeDept_VO.m_intFLAG_INT == 1)
                    {
                        m_dmlPrice += objOrderChargeDept_VO.m_decAmount_dec * objOrderChargeDept_VO.m_decUnitprice_dec;//new code
                    }
                    objOrderChargeDept_VO = null;
                }

                if (m_arrLipMainItem != null)
                {
                    p_objCommitOrder.m_dmlPrice = decimal.Parse(m_arrLipMainItem.m_dblPrice.ToString());
                    p_objCommitOrder.m_decTotalPrice = m_dmlPrice;//��Ϊ������Ŀ���ܼ�
                    p_objCommitOrder.m_dmlGet = decimal.Parse(m_arrLipMainItem.m_dblDrawAmount.ToString());

                    p_objCommitOrder.m_strChargeITEMID_CHR = m_arrLipMainItem.m_strChargeID;
                    p_objCommitOrder.m_strCharegITEMName = m_arrLipMainItem.m_strName;
                }
            }
            ArrayList m_ObjItemList = new ArrayList();
            if (m_arrObjItem != null && m_arrObjItem.Length > 0)
            {
                for (int i = 0; i < m_arrObjItem.Length; i++)
                {
                    if (m_arrObjItem[i].m_intCONTINUEUSETYPE_INT == 1 && order.m_intStatus == 2)//���ô��������Ѿ�ִ�й��ˣ�Ҳ����˵�������շ���
                    {
                        continue;
                    }
                    else
                    {
                        m_ObjItemList.Add(m_arrObjItem[i]);
                    }
                }
            }
            clsChargeForDisplay[] m_arrObjItem2 = null;
            if (m_ObjItemList.Count > 0)
            {
                m_arrObjItem2 = (clsChargeForDisplay[])m_ObjItemList.ToArray(typeof(clsChargeForDisplay));//��¼����Ҫ�շѵ��շ���Ŀ
            }

            BindTheDateToExecVo(m_objPatient, order, m_arrObjItem2, ref p_glstExecOrderVO_List);
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
        /// ҽ��ִ�йؼ����� 20180403  (��Ҫ��ҽ��������Ϣ)
        /// </summary>
        /// <param name="m_objPatient"></param>
        /// <param name="order"></param>
        /// <param name="m_arrObjItem"></param>
        /// <param name="p_glstExecOrderVO_List"></param>
        void BindTheDateToExecVo(clsBIHPatientInfo m_objPatient, clsBIHCanExecOrder order, clsChargeForDisplay[] m_arrObjItem, ref System.Collections.Generic.List<clsExecOrderVO> p_glstExecOrderVO_List)
        {
            int intIsFirst = 0;//�Ƿ��״�ִ��{1-��/0-��}
            int ISRECRUIT_INT = 0;//�Ƿ񲹴�{1/0}
            int m_intNEEDCONFIRM_INT = 0;//�Ƿ���Ҫ������� 0-�� 1-��
            int intChargeItemStatus = 0;        //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
            int ACTIVATETYPE_INT = 1;//��Ч����{1=����;2=������;3=ȷ�ϼ���;4=ȷ���շ�;5=ֱ���շ�}
            int intIsRich = 0;                  //*--�շ���Ŀ�Ĺ��ر�־
            string ISSELFPAY_CHR = "";//�Ƿ��Է���Ŀ("T","F")                                                                      
            int NEEDCONFIRM_INT = 0;//�Ƿ���Ҫ������� 0-�� 1-��;
            int m_intDisCount = 0;//�����Ƿ���۱�־���շ���Ŀ���� 
            /*
            ����1018-1021ȷ�ϲ����ڵ�ǰ����ݺ���ע״̬�£��Ƿ���Ҫ������ˣ�
            ������־д��(ҽ��ִ�е�)��NEEDCONFIRM_INT���ֶΡ������Ҫ������ˣ�
            ����Ҫͨ��������ȷ�ϼ��ʽ�����ɲ�������Ϣ���������͵�����ҩ����ҩ��
           ������Ҫ��˵�ҽ����������Ϣд��סԺ������ϸ��Ϣ��ʱ��PSTATUS_INT=0����־Ϊδȷ���շ���Ŀ�� 
             */
            // reMainMoney ûǷ��(>0)/Ƿ��(<0)
            int REMARK_INT = m_objPatient.m_intREMARK_INT;
            int CHARGECTL_INT = m_objPatient.m_intCHARGECTL_INT;

            decimal ChargeMedItemMoney = 0;
            decimal ChargeNoMedItemMoney = 0;
            decimal reMainMoney = m_objPatient.m_decPrePayMoney + m_objPatient.m_decinsuredsum_mny;
            decimal decTotalDiffCost = 0;//�������ȡ��ֵ
            //�������շ���Ŀʱֱ�ӷ���
            if (m_arrObjItem != null && m_arrObjItem.Length > 0)
            {
                SumTheCharge(m_arrObjItem, out ChargeMedItemMoney, out ChargeNoMedItemMoney, out intIsRich, out ISSELFPAY_CHR, out decTotalDiffCost);
                if (REMARK_INT == 1 && CHARGECTL_INT == 1)//��עǷ�ѿ��ƵĲ���
                {
                    if (ChargeMedItemMoney > 0 && this.m_objViewer.m_dmlMedOCMin != 0 && this.m_objViewer.m_dmlMedOCMin + reMainMoney < ChargeMedItemMoney)//ҩƷ��Ŀ��Ǯ��>0&&Ƿ�Ѳ���ҩƷ��Ŀȷ����С�������<ҩƷ��Ŀ��Ǯ��
                        m_intNEEDCONFIRM_INT = 1;//Ҫ���
                    if (ChargeNoMedItemMoney > 0 && this.m_objViewer.m_dmlNoMedOCMin != 0 && this.m_objViewer.m_dmlNoMedOCMin + reMainMoney < ChargeNoMedItemMoney)//��ҩƷ��Ŀ��Ǯ��>0&&Ƿ�Ѳ���ҩƷ��Ŀȷ����С�������<��ҩƷ��Ŀ��Ǯ��
                        m_intNEEDCONFIRM_INT = 1;//Ҫ���
                }
                else//����עǷ�ѿ��ƵĲ���(��ͨ����)
                {
                    if (ChargeMedItemMoney > 0 && this.m_objViewer.m_dmlMedICMin != 0 && this.m_objViewer.m_dmlMedICMin + reMainMoney < ChargeMedItemMoney)//ҩƷ��Ŀ��Ǯ��>0&&��ͨ����ҩƷ��Ŀȷ����С�������<ҩƷ��Ŀ��Ǯ��
                        m_intNEEDCONFIRM_INT = 1;//Ҫ���
                    if (ChargeNoMedItemMoney > 0 && this.m_objViewer.m_dmlNoMedICMin != 0 && this.m_objViewer.m_dmlNoMedICMin + reMainMoney < ChargeNoMedItemMoney)//��ҩƷ��Ŀ��Ǯ��>0&&��ͨ���˷�ҩƷ��Ŀȷ����С�������<��ҩƷ��Ŀ��Ǯ��
                        m_intNEEDCONFIRM_INT = 1;//Ҫ���
                }
                if (this.intDiffPriceOn == 1)
                    ChargeMedItemMoney = ChargeMedItemMoney + decTotalDiffCost;// ����ҩƷ�����ܽ��
            }

            bool isChildPrice = this.IsChildPrice(order.m_strOrderID);

            for (int i = 0; i <= order.m_intATTACHTIMES_INT; i++)
            {
                if (i > 0)
                {
                    ISRECRUIT_INT = 1;
                }
                if (order.m_intStatus == 5)
                {
                    intIsFirst = 1;
                }
                clsBihPatientCharge_VO[] m_arrPatientChargeVO = null;//��������
                if (m_arrObjItem != null && m_arrObjItem.Length > 0)
                {
                    ArrayList m_arrObjItemList = new ArrayList();
                    for (int k = 0; k < m_arrObjItem.Length; k++)
                    {
                        if (m_arrObjItem[k].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec != 0)//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����} -- 20180403: ����=0 ���շѣ�����ҩ
                        {
                            m_arrObjItemList.Add(m_arrObjItem[k]);
                        }
                    }
                    m_arrPatientChargeVO = new clsBihPatientCharge_VO[m_arrObjItemList.Count];
                    for (int i2 = 0; i2 < m_arrObjItemList.Count; i2++)
                    {
                        m_arrPatientChargeVO[i2] = new clsBihPatientCharge_VO();
                        m_arrPatientChargeVO[i2].PchargeID = "";//PChargeID_Chr ��ˮ��
                        m_arrPatientChargeVO[i2].RegisterID = order.m_strRegisterID;    // m_objPatient.m_strRegisterID;//RegisterID_Chr
                        m_arrPatientChargeVO[i2].ActiveDat = "";//CHARGEACTIVE_DAT ������Ч����
                        m_arrPatientChargeVO[i2].OrderID = order.m_strOrderID;//OrderID_Chr
                        m_arrPatientChargeVO[i2].OrderExecID = "";//OrderExecID_Chr
                        m_arrPatientChargeVO[i2].CalcCateID = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strItemIPCalcType_Chr;//CalCCateID_Chr  ���ú������id {=�շ���Ŀ����.id}
                        m_arrPatientChargeVO[i2].InvCateID = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strItemIpInvType_Chr;//InvCateID_Chr    ���÷�Ʊ���id {=�շ���Ŀ����.id}
                        m_arrPatientChargeVO[i2].ChargeItemID = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strChargeitemid_chr;//ChargeItemID_Chr
                        m_arrPatientChargeVO[i2].ChargeItemName = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strChargeitemname_chr;//ChargeItemName_Chr
                        m_arrPatientChargeVO[i2].Unit = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strUnit_vchr;//Unit_Vchr סԺ��λ{=�շ���Ŀ.סԺ��λ}
                        m_arrPatientChargeVO[i2].UnitPrice = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec;//UnitPrice_Dec  סԺ����{=�շ���Ŀ.סԺ����}
                        m_arrPatientChargeVO[i2].Amount = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_decAmount_dec;//AMount_Dec    ����
                        m_arrPatientChargeVO[i2].m_decSINGLEAMOUNT_DEC = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_decSINGLEAMOUNT_DEC;//AMount_Dec    ����
                        m_arrPatientChargeVO[i2].Discount = 100;//DisCount_Dec=1���ۿ۱���
                        if (((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strISSELFPAY_CHR.Trim().Equals("T"))
                        {
                            m_arrPatientChargeVO[i2].Ismepay = 1;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                        }
                        else
                        {
                            m_arrPatientChargeVO[i2].Ismepay = 0;//IsMepay_Int=1�� �Ƿ��Է���Ŀ {=�շ���Ŀ.�Ƿ��Է���Ŀ}
                        }
                        m_arrPatientChargeVO[i2].CreateType = 1;//CreateType_Int ¼������ {1=�Զ�(ҽ��);2=�Զ�(�մ���);3=����(ҽ��);4=����(��ҽ��)}
                        m_arrPatientChargeVO[i2].Creator = this.m_objViewer.LoginInfo.m_strEmpID;//Creator_Chr
                        m_arrPatientChargeVO[i2].CreateDat = "";//Create_Dat
                        m_arrPatientChargeVO[i2].CHARGEDOCTORGROUPID_CHR = order.m_strCHARGEDOCTORGROUPID;

                        m_arrPatientChargeVO[i2].Status = 1;// Status_Int��Ч״̬{1=��Ч;0=��Ч;-1=��ʷ}
                        m_arrPatientChargeVO[i2].ClacArea = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strClacarea_chr;//ClacArea_Chr
                        m_arrPatientChargeVO[i2].CreateArea = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strCreatearea_chr;//CreateArea_Chr
                        m_arrPatientChargeVO[i2].IsRich = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_intISRICH_INT;//ISRICH_INT
                        m_arrPatientChargeVO[i2].CurAreaID = order.m_strAREAID_CHR;//ִ��ʱ�������ڲ���ID
                        m_arrPatientChargeVO[i2].CurBedID = order.m_strBedID;//ִ��ʱ�������ڲ���ID
                        m_arrPatientChargeVO[i2].PatientID = order.m_strParentID;   // m_objPatient.m_strPatientID;
                        m_arrPatientChargeVO[i2].INSURACEDESC_VCHR = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_strYBClass.Trim();//ҽ����Ϣ
                        m_arrPatientChargeVO[i2].SPEC_VCHR = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_strSPEC_VCHR.Trim();//���{=this.get������Ŀ.get�շ���Ŀ.���}
                        m_arrPatientChargeVO[i2].DoctorID = order.m_strDOCTORID_CHR;
                        m_arrPatientChargeVO[i2].Doctor = order.m_strDOCTOR_VCHR;
                        m_arrPatientChargeVO[i2].DoctorGroupID = order.m_strDOCTORGROUPID_CHR;
                        m_arrPatientChargeVO[i2].m_intITEMSRCTYPE_INT = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_intITEMSRCTYPE_INT;//�շ���Ŀ��Դ
                        m_arrPatientChargeVO[i2].m_strPUTMEDREQID_CHR = i2.ToString();
                        m_arrPatientChargeVO[i2].m_strITEMSRCID_VCHR = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strITEMSRCID_VCHR;
                        m_arrPatientChargeVO[i2].m_intMEDICNETYPE_INT = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_intMEDICNETYPE_INT;
                        m_arrPatientChargeVO[i2].TotalDiffCostMoney_dec = (decimal)((clsChargeForDisplay)m_arrObjItemList[i2]).m_dblDiffCostMoney;// ���������
                        m_arrPatientChargeVO[i2].BuyPrice = (decimal)((clsChargeForDisplay)m_arrObjItemList[i2]).m_dblTradePrice;
                        m_arrPatientChargeVO[i2].m_intPUTMEDTYPE_INT = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_intPOFLAG_INT;
                        if (((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_intFLAG_INT == 0)
                        {
                            m_arrPatientChargeVO[i2].m_decDOSAGE_DEC = order.m_dmlDosage;
                            m_arrPatientChargeVO[i2].m_strDOSAGEUNIT_CHR = order.m_strDosageUnit;
                        }
                        else
                        {
                            m_arrPatientChargeVO[i2].m_decDOSAGE_DEC = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_decDOSAGE_DEC;
                            m_arrPatientChargeVO[i2].m_strDOSAGEUNIT_CHR = ((clsChargeForDisplay)m_arrObjItemList[i2]).m_objORDERCHARGEDEPT_VO.m_strDOSAGEUNIT_CHR;
                        }

                        //���������Ŀͳ��
                        if (this.m_objViewer.m_blLisDiscount == true && order.m_strOrderDicCateID.Trim().Equals(m_objSpecateVo.m_strORDERCATEID_LIS_CHR.Trim()) && m_strLisPARMVALUE_VCHR.Contains(m_arrPatientChargeVO[i2].InvCateID) && !m_arrPatientChargeVO[i2].InvCateID.Equals(""))
                        {
                            m_intDisCount++;
                        }
                        m_arrPatientChargeVO[i2].CHARGEDOCTORID_CHR = order.m_strCreatorID;
                        m_arrPatientChargeVO[i2].CHARGEDOCTOR_VCHR = order.m_strCreator;
                        m_arrPatientChargeVO[i2].IsChildPrice = isChildPrice ? 1 : 0;       // ��ͯ�۸�
                    }
                }
                clsExecOrderVO ExecVo = new clsExecOrderVO();//ִ�е�VO
                ExecVo.ORDEREXECID_CHR = "";//��ˮ��
                ExecVo.ORDERID_CHR = order.m_strOrderID;//ҽ����id	{=ҽ����.id}
                ExecVo.m_strOrderName = order.m_strName;//ҽ����
                ExecVo.CREATORID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;//����ִ�е���{=��Ա.Id}
                ExecVo.CREATOR_CHR = this.m_objViewer.LoginInfo.m_strEmpName;//����ִ�е���{=��Ա.name}
                ExecVo.EXECUTETIME_INT = order.m_intTIMES_INT;//ִ�д���  Ƶ�ʱ�Ĵ���
                ExecVo.EXECUTEDAYS_INT = order.m_intDays_Int;//ִ������  ���� ������Ƶ�ʱ�
                if (ISRECRUIT_INT > 0)
                {
                    ExecVo.EXECUTEDATE_VCHR = "����";
                }
                else
                {
                    ExecVo.EXECUTEDATE_VCHR = order.m_strEntrust;//ִ��ʱ�� ��: 08:00-14:00-��20:00 ������ҽ������
                }
                ExecVo.ISCHARGE_INT = 0;// �Ƿ��ѼƷ�{1-��/0-��}
                ExecVo.ISINCEPT_INT = 0;// �Ƿ��ѷ���{1-��/0-��}
                ExecVo.ISFIRST_INT = intIsFirst;//IsFirst_int �Ƿ��״�ִ��{1-��/0-��}
                ExecVo.ISRECRUIT_INT = ISRECRUIT_INT;//�Ƿ񲹴�{1/0}
                ExecVo.STATUS_INT = 1;//Status_Int=1    STATUS_INT ��Ч��־	{1=��Ч;0=ɾ��;-1=��ʷ}
                ExecVo.m_strEXEAREAID_CHR = order.m_strAREAID_CHR;//ִ��ʱ�������ڲ���ID
                ExecVo.m_strEXEBEDID_CHR = order.m_strBedID;//ִ��ʱ�������ڴ���ID
                ExecVo.m_objPatient = m_objPatient;
                ExecVo.m_strRegisterRecipenNo = order.m_strRegisterID + "*" + order.m_intRecipenNo.ToString();
                //��������ֶ�
                ExecVo.m_decChargeMedItemMoney = ChargeMedItemMoney;//ҩƷ
                ExecVo.m_decChargeNoMedItemMoney = ChargeNoMedItemMoney;//��ҩƷ 
                /*����һЩ��־λ����*/
                if (m_intNEEDCONFIRM_INT == 1)//ҽ��ִ�е����Ƿ���Ҫȷ�ϱ�־
                {
                    intChargeItemStatus = 0;
                }
                //*--����״̬��0-��ȷ�ϣ�1-���ᣩ
                if (intIsRich == 1 || ISSELFPAY_CHR.ToUpper().Equals("T"))
                {
                    intChargeItemStatus = 0;

                }
                if (m_intNEEDCONFIRM_INT == 0 && intIsRich == 0 && !ISSELFPAY_CHR.ToUpper().Equals("T"))
                {
                    intChargeItemStatus = 1;
                }
                if (intChargeItemStatus == 0)
                {
                    ACTIVATETYPE_INT = 3;
                }
                else if (intChargeItemStatus == 1)
                {
                    ACTIVATETYPE_INT = 1;
                }
                //����ҽ��ִ�к���Ч����Ϊ3=ȷ�ϼ���
                if (intChargeItemStatus == 0 && intIsRich == 1)
                {
                    ACTIVATETYPE_INT = 3;
                }

                //�Է�ҽ��ִ�к���Ч����Ҳ��4=ȷ���շ�
                if (intChargeItemStatus == 0 && ISSELFPAY_CHR.Trim().Equals("T"))
                {
                    ACTIVATETYPE_INT = 4;
                }
                //������� ϵͳ������(ICARE����) 1008 סԺȷ�ϼ������̶�Ӧ�����ID ������������ݸ��� �Ͳ���ȷ���շѣ���ȷ���ϼ�������
                if (ACTIVATETYPE_INT == 4 && !m_strPARMVALUE_VCHR.Trim().Equals("") && m_strPARMVALUE_VCHR.Contains(m_objPatient.m_strPayTypeID))
                {
                    ACTIVATETYPE_INT = 3;
                }
                if (intChargeItemStatus == 0)
                {
                    NEEDCONFIRM_INT = 1;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
                }
                else
                {
                    NEEDCONFIRM_INT = 0;//NEEDCONFIRM_INT�Ƿ���Ҫ������� 0-�� 1-��
                }
                if (m_arrPatientChargeVO != null && m_arrPatientChargeVO.Length > 0)
                {
                    for (int j = 0; j < m_arrPatientChargeVO.Length; j++)
                    {
                        m_arrPatientChargeVO[j].NeedConfirm = NEEDCONFIRM_INT;
                        m_arrPatientChargeVO[j].PStatus = intChargeItemStatus;
                        m_arrPatientChargeVO[j].ActivateType = ACTIVATETYPE_INT;
                        if (m_arrPatientChargeVO[j].NeedConfirm == 0)//�������ʱֱ�ӷ�ҩ
                        {
                            m_arrPatientChargeVO[j].Activator = this.m_objViewer.LoginInfo.m_strEmpID;//����ִ�е���{=��Ա.Id}
                        }
                        if (order.m_intExecuteType == 1 && ISRECRUIT_INT == 1 && intIsFirst == 1)//�����¿���
                        {
                            m_arrPatientChargeVO[j].OrderExecType = 3;//OrderExecType_Int ҽ��ִ������{1=����;2=����;3=�����¿���;4=��Ժ��ҩ}
                            m_arrPatientChargeVO[j].Amount = m_arrPatientChargeVO[j].m_decSINGLEAMOUNT_DEC;
                        }
                        else if (order.m_intExecuteType == 1)
                        {
                            m_arrPatientChargeVO[j].OrderExecType = 1;//����
                        }
                        else if (order.m_intExecuteType == 2)
                        {
                            m_arrPatientChargeVO[j].OrderExecType = 2;//����
                        }
                        else if (order.m_intExecuteType == 3)
                        {
                            m_arrPatientChargeVO[j].OrderExecType = 4;//��Ժ��ҩ
                        }
                        //������۵��߼�
                        if (m_intDisCount > this.m_objViewer.m_intLisDiscountNum)
                        {
                            m_arrPatientChargeVO[j].NEWDISCOUNT_DEC = this.m_objViewer.m_decLisDiscountMount;
                        }
                    }
                }
                ExecVo.m_arrPatientChareVO = m_arrPatientChargeVO;//�Ѳ��˵�ǰҽ���ķ��õ��ŵ�ҽ��ִ�е�VO
                ExecVo.NEEDCONFIRM_INT = NEEDCONFIRM_INT;//�Ƿ���Ҫ������� 0-�� 1-��
                //����������ҽ�������⴦��(1�������շѣ�2ִ�е�״̬Ϊ�ѷ���)
                if (order.m_intExecuteType == 1 && m_objSpecateVo.m_strCONFREQID_CHR.Trim().Equals(order.m_strExecFreqID.Trim()))
                {
                    ExecVo.m_arrPatientChareVO = null;
                    ExecVo.NEEDCONFIRM_INT = 0;
                    ExecVo.ISINCEPT_INT = 1;
                    ExecVo.m_intOrderType = 1;//������ִ�е���ʶ
                    ExecVo.CREATEDATE_DAT = order.m_dtPostdate;//ִ��ʱ����������Ч�Ŀ�ʼʱ�����ҽ���ύ��ʱ��
                }
                if (ExecVo.NEEDCONFIRM_INT == 0)//����ȷ��ʱ��ֱ�ӷ�ҩ
                {
                    ExecVo.ISINCEPT_INT = 1;// �Ƿ��ѷ���{1-��/0-��}
                }
                //��©ִ�б�־
                ExecVo.m_intRepair = m_intRepairEveVo;
                //ִ�е��ű�ʶID Ϊ�˱���ͬһʱ��Ĳ�������ִ�б�Լ�����ʹ��
                ExecVo.m_strAUTOID_VCHR = i.ToString() + m_intRepairEveVoCount.ToString();
                // Ԥ��ҩֻ���ǵ�һ�Σ��Ժ��Ϊ0
                ExecVo.PretestDays = order.m_dtExecutedate > Convert.ToDateTime("2000-01-01") ? 0 : order.PretestDays;
                //
                ExecVo.PreAmount2 = order.PreAmount2;
                //
                ExecVo.RegisterId = order.m_strRegisterID;

                ExecVo.CureDays = order.CureDays;
                ExecVo.CheckState = order.CheckState;

                CreateTheMedicineVos(order, ExecVo);//�ѵ�ǰҽ����Ӧ�ķ�ҩ���ŵ�ִ�е�VO��               
                p_glstExecOrderVO_List.Add(ExecVo);
                //���ǳ������״�ִ�е�,�����в��β���
                if (order.m_intExecuteType != 1 || intIsFirst != 1)
                {
                    break;
                }
            }
        }

        private void SumTheCharge(clsChargeForDisplay[] m_arrObjItem, out decimal ChargeMedItemMoney, out decimal ChargeNoMedItemMoney, out int intIsRich, out string ISSELFPAY_CHR, out decimal p_decTotalDiffCost)
        {
            ChargeMedItemMoney = 0;
            ChargeNoMedItemMoney = 0;
            intIsRich = 0;
            ISSELFPAY_CHR = "F";
            p_decTotalDiffCost = 0;// ���������
            for (int i = 0; i < m_arrObjItem.Length; i++)
            {
                if (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_intISRICH_INT == 1)
                {
                    intIsRich = 1;
                }
                if (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_strISSELFPAY_CHR.Trim().Equals("T"))
                {
                    ISSELFPAY_CHR = "T";
                }
                if (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_intRATETYPE_INT == 1)
                {
                    if (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_intITEMSRCTYPE_INT == 1)
                    {
                        ChargeMedItemMoney += m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decAmount_dec * m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec;
                        p_decTotalDiffCost += m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decAmount_dec * (m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decUnitTradePrice_dec - m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec);// ���������
                    }
                    else
                    {
                        ChargeNoMedItemMoney += m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decAmount_dec * m_arrObjItem[i].m_objORDERCHARGEDEPT_VO.m_decUnitprice_dec;
                    }

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
                    m_strOrderID += "'" + ((clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag).m_strOrderID + "',";
                }
            }
            m_strOrderID = m_strOrderID.TrimEnd(',');
            return m_strOrderID;

        }
        #endregion

        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        internal void IniTheForm()
        {
            long l = this.m_objManage.m_lngGetAPPLY_RLT(out this.m_gdctApplyRlt);
            if (l < 0)
            {
                MessageBox.Show(this.m_objViewer, "������뵥���ձ��д����������Ա��ϵ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            m_blFirstLoad = true;
            this.m_objViewer.m_chkSelectAll.Checked = true;
            if (this.m_objViewer.m_txtArea.Text.Trim().Equals(""))
            {
                this.m_objViewer.m_txtArea.Text = this.m_objViewer.LoginInfo.m_strInpatientAreaName;
                this.m_objViewer.m_txtArea.Tag = this.m_objViewer.LoginInfo.m_strInpatientAreaID;
            }
            this.m_objViewer.m_txtArea.Focus();
            this.m_objViewer.m_cboCode.SelectedIndex = 0;
            this.m_objViewer.m_cboState.Text = "ȫ��ҽ��";
            if (this.m_objViewer.m_blCanSelectOrder == true)
            {
                this.m_objViewer.m_dtvOrderList.Columns["m_dtvselectCheck"].Visible = true;
                this.m_objViewer.m_cmdToCommit.Visible = true;
                this.m_objViewer.m_chkSelectAll.Visible = true;
            }
            this.intDiffPriceOn = clsPublic.m_intGetSysParm("9002");// �������ÿ���
            this.m_strDiffMedicineType = clsPublic.m_strGetSysparm("9003");// ����ҩƷ����
            refreshTheData();
            m_blFirstLoad = false;
            this.isUseChildPrice = (new clsDcl_ExecuteOrder()).IsUseChildPrice();
        }
        #endregion

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
            ControlTheButton();
        }
        #endregion

        internal void AddNewChargeItem()
        {
            if (this.m_objViewer.m_dtvOrderList.CurrentRow == null || this.m_objViewer.m_dtvOrderList.CurrentRow.Tag == null)
            {
                return;
            }

            clsBIHCanExecOrder m_objOrderItem = (clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.CurrentRow.Tag;
            if (m_objOrderItem.m_intStatus == 2)
            {
                MessageBox.Show("��ִ�й�����Ŀ����������շ���Ŀ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmChargeItem frmCharge = new frmChargeItem(m_objOrderItem);
            if (frmCharge.ShowDialog() == DialogResult.OK)
            {
                refreshTheChargeDate();
                OrderListSelect();

            };
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

            clsBIHCanExecOrder m_objOrderItem = (clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.CurrentRow.Tag;
            if (m_objOrderItem.m_intStatus == 2)
            {
                MessageBox.Show("��ִ�й�����Ŀ����������շ���Ŀ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clsChargeForDisplay objItem = (clsChargeForDisplay)this.m_objViewer.m_dtvChangeList.CurrentRow.Tag;

            //���շ���Ŀ�������޸� �շ����m_intType	{1=��ͨҩƷ�շѣ�2=���շѣ�3=�÷��շ�}
            string m_intType = objItem.m_intType.ToString().Trim();
            string m_strSeq_int = objItem.m_strSeq_int;
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
            clsBIHCanExecOrder m_objOrderItem = (clsBIHCanExecOrder)m_objViewer.m_dtvOrderList.CurrentRow.Tag;
            if (m_objOrderItem.m_intStatus == 2)
            {
                MessageBox.Show("��ִ�й�����Ŀ����������շ���Ŀ!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region �������

        /// <summary>
        /// �ύ���ͼ�����뵥
        /// </summary>

        #region ���е��㷨
        /*
        private bool m_mthSendCheckApplyBill(ref ArrayList SendCheckArr, out com.digitalwave.iCare.ValueObject.clsOrderBooking[] m_arrOrderBooking, out clsATTACHRELATION_VO[] m_arATTACHRELATION)
        {
            m_arrOrderBooking = new com.digitalwave.iCare.ValueObject.clsOrderBooking[0];
            m_arATTACHRELATION = new clsATTACHRELATION_VO[0];
            clsApplyRecord objApplyVO;

            com.digitalwave.GLS_WS.clsApplyForm objfrm = new com.digitalwave.GLS_WS.clsApplyForm();
            //������� ���뵥ӳ���VO
            ArrayList m_ATTACHRELATION=new ArrayList();
            ArrayList m_OrderBooking = new ArrayList();
            for (int i = 0; i < SendCheckArr.Count; i++)
            {
                
                clsTestApplyItme_VO item_VO = new clsTestApplyItme_VO();
                clsCommitOrder p_objCommitOrder = (clsCommitOrder)SendCheckArr[i];
                m_mthFillApplyInfo(p_objCommitOrder, out objApplyVO);
                item_VO.m_decDiscount = 0;
                //item_VO.m_decPrice = p_objCommitOrder.m_dmlPrice;
                if (p_objCommitOrder.m_dmlGet == 0)
                {
                    item_VO.m_decPrice = p_objCommitOrder.m_decTotalPrice;
                }
                else
                {
                    item_VO.m_decPrice = p_objCommitOrder.m_decTotalPrice / p_objCommitOrder.m_dmlGet;
                }
                item_VO.m_decQty = p_objCommitOrder.m_dmlGet; 
                item_VO.m_decTolPrice = p_objCommitOrder.m_decTotalPrice; 
                item_VO.m_strItemID = p_objCommitOrder.m_strOrderID; 
                item_VO.m_strItemName = p_objCommitOrder.m_strName;
                item_VO.m_strSpec = p_objCommitOrder.m_strSpec;
                item_VO.m_strUnit = p_objCommitOrder.m_strUseunit;
                item_VO.m_strOutpatRecipeID = "";
                item_VO.m_strRowNo = i.ToString();
                item_VO.m_strOprDeptID = "";

                if (item_VO.m_strItemID.Trim().Equals(""))
                {
                    continue;
                }
                string strTypeID = ""; 
                strTypeID = p_objCommitOrder.m_strAPPLYTYPEID_CHR;
                objApplyVO.m_strDiagnosePart = p_objCommitOrder.m_strPARTNAME_VCHR;
         
                //m_arrOrderBooking[i].m_decBOOKID_INT = -1;
                if (strTypeID == "" || strTypeID == "-1")
                {
                    
                    continue;
                }
                //��ȡ��Ӧ���ҽ������ϼ�����ժҪ��Ϣ
                clsEMR_HIS_CheckRequisition CheckRequest = new clsEMR_HIS_CheckRequisition(p_objCommitOrder.m_strRegisterID, p_objCommitOrder.m_strOrderID);
                clsEMR_HIS_CheckRequisitionValue CheckMessage = CheckRequest.m_objGetCheckRequisition();
                //����ժҪ
                if (CheckMessage != null)
                {
                    objApplyVO.m_strSummary = CheckMessage.m_strCASESUMMARY_VCHR+"\r\n"+CheckMessage.m_strPHYSEXAM_VCHR;
                    objApplyVO.m_strDiagnose = CheckMessage.m_strADMISSIONDIAGNOSIS_VCHR;
                }
                objApplyVO.m_strTypeID = strTypeID;
                objApplyVO.m_objChargeItem = item_VO;
                objApplyVO.m_intChargeStatus = 2;
                objApplyVO.m_objAttachRelation.m_strSourceItemID = p_objCommitOrder.m_strOrderID; 
                objApplyVO.m_strAreaID = p_objCommitOrder.m_strAreaID;
                objApplyVO.m_strArea = p_objCommitOrder.m_strAreaName; 
                objApplyVO.m_strBedNO = p_objCommitOrder.m_strBedName;
                objApplyVO.m_strBIHNO = p_objCommitOrder.m_strINPATIENTID_CHR; 
                objApplyVO.m_strDeptID = p_objCommitOrder.m_strDEPTID_CHR;
                objApplyVO.m_strDepartment = p_objCommitOrder.m_strDEPTNAME_VCHR;
                objApplyVO.m_strDiagnosePart = p_objCommitOrder.m_strPARTNAME_VCHR;
                objApplyVO.m_strDoctorID = p_objCommitOrder.m_strCreatorID;
                objApplyVO.m_strDoctorName = p_objCommitOrder.m_strCreator;
                objApplyVO.m_strName = p_objCommitOrder.m_strPatientName;
                objApplyVO.m_strSex = p_objCommitOrder.m_strsex_chr;
                objApplyVO.m_intDeleted = 0;
                
                clsCheckType objCTArr = objfrm.GetIDWithVO(objApplyVO);
                if (objCTArr != null)
                {
                    com.digitalwave.iCare.ValueObject.clsOrderBooking m_objOrderBooking = new com.digitalwave.iCare.ValueObject.clsOrderBooking();
                    m_objOrderBooking.m_decBOOKID_INT = 0;
                    m_objOrderBooking.m_decUNITPRICE_DEC = objApplyVO.m_objChargeItem.m_decPrice;
                    m_objOrderBooking.m_decAMOUNT_DEC = p_objCommitOrder.m_dmlGet;
                    m_objOrderBooking.m_intBOOKSTATUS_INT = 0;
                    m_objOrderBooking.m_strAPPLY_TYPE_INT = objCTArr.m_strTypeID;
                    m_objOrderBooking.m_strCHARGEITEMID_CHR = p_objCommitOrder.m_strChargeITEMID_CHR;
                    m_objOrderBooking.m_strCHARGEITEMNAME_VCHR = p_objCommitOrder.m_strCharegITEMName;
                    m_objOrderBooking.m_strCREATERID_CHR = p_objCommitOrder.m_strCREATEAREA_ID;
                    m_objOrderBooking.m_strCREATEAREA_CHR = p_objCommitOrder.m_strCREATEAREA_ID;
                    m_objOrderBooking.m_strCURAREAID_CHR = p_objCommitOrder.m_strCURAREAID_CHR;
                    m_objOrderBooking.m_strCURBEDID_CHR = p_objCommitOrder.m_strCURBEDID_CHR;
                    m_objOrderBooking.m_strDOCTORID_CHR = p_objCommitOrder.m_strDOCTORID_CHR;
                    m_objOrderBooking.m_strORDERID_CHR = p_objCommitOrder.m_strOrderID;
                    m_objOrderBooking.m_strORDERNAME_VCHR = p_objCommitOrder.m_strName;
                    m_objOrderBooking.m_strPATIENTID_CHR = p_objCommitOrder.m_strParentID;
                    m_objOrderBooking.m_strREGISTERID_CHR = p_objCommitOrder.m_strRegisterID;
                    m_objOrderBooking.m_strUNIT_VCHR = p_objCommitOrder.m_strUseunit;
                    m_objOrderBooking.m_strSENDERID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
                    m_objOrderBooking.m_strATTACHID_VCHR = objCTArr.m_strApplyID;

                    m_OrderBooking.Add(m_objOrderBooking);

                    clsATTACHRELATION_VO ATTACHRELATION_VO = new clsATTACHRELATION_VO();
                    ATTACHRELATION_VO.strATTACHID_VCHR = objCTArr.m_strApplyID;
                    ATTACHRELATION_VO.strATTACHTYPE_INT = "1";
                    ATTACHRELATION_VO.strATTARELAID_CHR = "";
                    ATTACHRELATION_VO.strSOURCEITEMID_VCHR = p_objCommitOrder.m_strOrderID;
                    ATTACHRELATION_VO.strSYSFROM_INT = "2";
                    ATTACHRELATION_VO.strURGENCY_INT = "0";
                    
                    m_ATTACHRELATION.Add(ATTACHRELATION_VO);
                }
                if (m_ATTACHRELATION.Count > 0)
                {
                    m_arATTACHRELATION = (clsATTACHRELATION_VO[])m_ATTACHRELATION.ToArray(typeof(clsATTACHRELATION_VO));
                }
                if (m_OrderBooking.Count > 0)
                {
                    m_arrOrderBooking = (com.digitalwave.iCare.ValueObject.clsOrderBooking[])m_OrderBooking.ToArray(typeof(com.digitalwave.iCare.ValueObject.clsOrderBooking));
                }
            }
            return true;
        }
        */
        #endregion

        private void m_mthFillApplyInfo(clsCommitOrder p_objCommitOrder, out clsApplyRecord objApplyVO)
        {
            objApplyVO = new clsApplyRecord();
            objApplyVO.m_datApplyDate = DateTime.Now;
            objApplyVO.m_strAge = p_objCommitOrder.m_strAge;
            objApplyVO.m_strCardNO = p_objCommitOrder.m_strPATIENTCARDID_CHR;
            objApplyVO.m_strDiagnose = p_objCommitOrder.m_strDIAGNOSE_VCHR;
            objApplyVO.m_strDoctorName = p_objCommitOrder.m_strCreator;

            objApplyVO.m_strDoctorID = p_objCommitOrder.m_strCreatorID;
            objApplyVO.m_strName = p_objCommitOrder.m_strPatientName;
            objApplyVO.m_strSex = p_objCommitOrder.m_strsex_chr;
            objApplyVO.m_objAttachRelation.m_intSysFrom = 2;
            objApplyVO.m_strDeptID = p_objCommitOrder.m_strDEPTID_CHR;
            objApplyVO.m_strDepartment = p_objCommitOrder.m_strDEPTNAME_VCHR;
            objApplyVO.m_strBedNO = p_objCommitOrder.m_strBedName;
            objApplyVO.m_strBIHNO = p_objCommitOrder.m_strINPATIENTID_CHR;
            objApplyVO.m_strDiagnosePart = p_objCommitOrder.m_strPARTID_VCHR;
            objApplyVO.m_intSubmitted = 1;
        }

        private bool m_mthSendCheckApplyBill(Dictionary<string, List<clsCommitOrder>> p_gdctOrder,
                     out weCare.Core.Entity.clsOrderBooking[] m_arrOrderBooking, out clsATTACHRELATION_VO[] m_arATTACHRELATION)
        {
            m_arrOrderBooking = null;
            m_arATTACHRELATION = null;

            com.digitalwave.GLS_WS.clsApplyForm objfrm = new com.digitalwave.GLS_WS.clsApplyForm();
            //������� ���뵥ӳ���VO
            List<weCare.Core.Entity.clsOrderBooking> glstBooking = new List<weCare.Core.Entity.clsOrderBooking>();
            List<clsATTACHRELATION_VO> glstRelation = new List<clsATTACHRELATION_VO>();

            clsApplyRecord objApplyVO;
            clsTestApplyItme_VO item_VO;
            clsEMR_HIS_CheckRequisition CheckRequest;
            clsEMR_HIS_CheckRequisitionValue CheckMessage;
            System.Collections.Generic.List<clsApplyRecord> glstApplyVO;

            com.digitalwave.GLS_WS.clsApplyInterface objRisSend = new com.digitalwave.GLS_WS.clsApplyInterface();
            foreach (string Register in p_gdctOrder.Keys)
            {
                glstApplyVO = new List<clsApplyRecord>();
                List<clsCommitOrder> glstCommitOrder = p_gdctOrder[Register];

                List<weCare.Core.Entity.clsOrderBooking> bookingTmp = new List<weCare.Core.Entity.clsOrderBooking>();

                List<clsATTACHRELATION_VO> RelationTmp = new List<clsATTACHRELATION_VO>();
                Hashtable hasTmp = new Hashtable();
                for (int i1 = 0; i1 < glstCommitOrder.Count; i1++)
                {
                    if (glstCommitOrder[i1].m_strOrderID == "")
                    {
                        continue;
                    }
                    objApplyVO = new clsApplyRecord();
                    string strTypeID = "";
                    strTypeID = glstCommitOrder[i1].m_strAPPLYTYPEID_CHR;
                    objApplyVO.m_strDiagnosePart = glstCommitOrder[i1].m_strPARTNAME_VCHR;
                    if (strTypeID == "" || strTypeID == "-1")
                    {
                        continue;
                    }

                    if (!hasTmp.ContainsKey(strTypeID))
                    {
                        hasTmp.Add(strTypeID, this.m_gdctApplyRlt[strTypeID].ToString());
                    }

                    m_mthFillApplyInfo(glstCommitOrder[i1], out objApplyVO);
                    item_VO = new clsTestApplyItme_VO();
                    item_VO.m_decDiscount = 0;
                    if (glstCommitOrder[i1].m_dmlGet == 0)
                    {
                        item_VO.m_decPrice = glstCommitOrder[i1].m_decTotalPrice;
                    }
                    else
                    {
                        item_VO.m_decPrice = glstCommitOrder[i1].m_decTotalPrice / glstCommitOrder[i1].m_dmlGet;
                    }
                    item_VO.m_decQty = glstCommitOrder[i1].m_dmlGet;
                    item_VO.m_decTolPrice = glstCommitOrder[i1].m_decTotalPrice;
                    item_VO.m_strItemID = glstCommitOrder[i1].m_strOrderID;
                    item_VO.m_strItemName = glstCommitOrder[i1].m_strName;
                    item_VO.m_strSpec = glstCommitOrder[i1].m_strSpec;
                    item_VO.m_strUnit = glstCommitOrder[i1].m_strUseunit;
                    item_VO.m_strOutpatRecipeID = "";
                    item_VO.m_strRowNo = i1.ToString();
                    item_VO.m_strOprDeptID = "";

                    CheckRequest = new clsEMR_HIS_CheckRequisition(Register, glstCommitOrder[i1].m_strOrderID);
                    CheckMessage = CheckRequest.m_objGetCheckRequisition();
                    if (CheckMessage != null)
                    {
                        objApplyVO.m_strSummary = CheckMessage.m_strCASESUMMARY_VCHR + "\r\n" + CheckMessage.m_strPHYSEXAM_VCHR;
                        objApplyVO.m_strDiagnose = CheckMessage.m_strADMISSIONDIAGNOSIS_VCHR;
                    }

                    objApplyVO.m_strTypeID = strTypeID;
                    objApplyVO.m_objChargeItem = item_VO;
                    objApplyVO.m_intChargeStatus = 2;
                    objApplyVO.m_objAttachRelation.m_strSourceItemID = glstCommitOrder[i1].m_strOrderID;
                    objApplyVO.m_strAreaID = glstCommitOrder[i1].m_strAreaID;
                    objApplyVO.m_strArea = glstCommitOrder[i1].m_strAreaName;
                    objApplyVO.m_strBedNO = glstCommitOrder[i1].m_strBedName;
                    objApplyVO.m_strBIHNO = glstCommitOrder[i1].m_strINPATIENTID_CHR;
                    objApplyVO.m_strDeptID = glstCommitOrder[i1].m_strDEPTID_CHR;
                    objApplyVO.m_strDepartment = glstCommitOrder[i1].m_strDEPTNAME_VCHR;
                    objApplyVO.m_strDiagnosePart = glstCommitOrder[i1].m_strPARTNAME_VCHR;
                    objApplyVO.m_strDoctorID = glstCommitOrder[i1].m_strCreatorID;
                    objApplyVO.m_strDoctorName = glstCommitOrder[i1].m_strCreator;
                    objApplyVO.m_strName = glstCommitOrder[i1].m_strPatientName;
                    objApplyVO.m_strSex = glstCommitOrder[i1].m_strsex_chr;
                    objApplyVO.m_intDeleted = 0;
                    objApplyVO.BirthDay = glstCommitOrder[i1].Birthday;
                    glstApplyVO.Add(objApplyVO);// ������뵥������

                    //��VO

                    weCare.Core.Entity.clsOrderBooking m_objOrderBooking = new weCare.Core.Entity.clsOrderBooking();
                    m_objOrderBooking.m_decBOOKID_INT = 0;
                    m_objOrderBooking.m_decUNITPRICE_DEC = objApplyVO.m_objChargeItem.m_decPrice;
                    m_objOrderBooking.m_decAMOUNT_DEC = glstCommitOrder[i1].m_dmlGet;
                    m_objOrderBooking.m_intBOOKSTATUS_INT = 0;
                    m_objOrderBooking.m_strAPPLY_TYPE_INT = strTypeID;
                    m_objOrderBooking.m_strCHARGEITEMID_CHR = glstCommitOrder[i1].m_strChargeITEMID_CHR;
                    m_objOrderBooking.m_strCHARGEITEMNAME_VCHR = glstCommitOrder[i1].m_strCharegITEMName;
                    m_objOrderBooking.m_strCREATERID_CHR = glstCommitOrder[i1].m_strCREATEAREA_ID;
                    m_objOrderBooking.m_strCREATEAREA_CHR = glstCommitOrder[i1].m_strCREATEAREA_ID;
                    m_objOrderBooking.m_strCURAREAID_CHR = glstCommitOrder[i1].m_strCURAREAID_CHR;
                    m_objOrderBooking.m_strCURBEDID_CHR = glstCommitOrder[i1].m_strCURBEDID_CHR;
                    m_objOrderBooking.m_strDOCTORID_CHR = glstCommitOrder[i1].m_strDOCTORID_CHR;
                    m_objOrderBooking.m_strORDERID_CHR = glstCommitOrder[i1].m_strOrderID;
                    m_objOrderBooking.m_strORDERNAME_VCHR = glstCommitOrder[i1].m_strName;
                    m_objOrderBooking.m_strPATIENTID_CHR = glstCommitOrder[i1].m_strParentID;
                    m_objOrderBooking.m_strREGISTERID_CHR = glstCommitOrder[i1].m_strRegisterID;
                    m_objOrderBooking.m_strUNIT_VCHR = glstCommitOrder[i1].m_strUseunit;
                    m_objOrderBooking.m_strSENDERID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;

                    bookingTmp.Add(m_objOrderBooking);

                    clsATTACHRELATION_VO ATTACHRELATION_VO = new clsATTACHRELATION_VO();
                    ATTACHRELATION_VO.strATTACHTYPE_INT = "1";
                    ATTACHRELATION_VO.strATTARELAID_CHR = "";
                    ATTACHRELATION_VO.strSOURCEITEMID_VCHR = glstCommitOrder[i1].m_strOrderID;
                    ATTACHRELATION_VO.strSYSFROM_INT = "2";
                    ATTACHRELATION_VO.strURGENCY_INT = "0";
                    ATTACHRELATION_VO.strDiagnosePart = objApplyVO.m_strDiagnosePart;
                    ATTACHRELATION_VO.strChargeDetail = objApplyVO.m_strChargeDetail;
                    ATTACHRELATION_VO.strDiagnosePartID = glstCommitOrder[i1].m_strPARTID_VCHR;
                    RelationTmp.Add(ATTACHRELATION_VO);

                }
                clsCheckType[] objCTArr = objRisSend.opGetIDWithVO(glstApplyVO.ToArray(), hasTmp);

                if (objCTArr != null)
                {
                    for (int i1 = 0; i1 < objCTArr.Length; i1++)
                    {
                        for (int j1 = 0; j1 < bookingTmp.Count; j1++)
                        {
                            if (objCTArr[i1].objItem_VO.m_strItemID == bookingTmp[j1].m_strORDERID_CHR)
                            {
                                bookingTmp[j1].m_strATTACHID_VCHR = objCTArr[i1].m_strApplyID;
                                break;
                            }
                        }
                        for (int j1 = 0; j1 < RelationTmp.Count; j1++)
                        {
                            if (objCTArr[i1].objItem_VO.m_strItemID == RelationTmp[j1].strSOURCEITEMID_VCHR)
                            {
                                RelationTmp[j1].strATTACHID_VCHR = objCTArr[i1].m_strApplyID;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                glstBooking.AddRange(bookingTmp);
                glstRelation.AddRange(RelationTmp);
                bookingTmp.Clear();
                RelationTmp.Clear();
                objCTArr = null;
            }

            m_arrOrderBooking = glstBooking.ToArray();
            m_arATTACHRELATION = glstRelation.ToArray();
            objfrm = null;
            glstBooking = null;
            glstRelation = null;
            objApplyVO = null;
            item_VO = null;
            CheckRequest = null;
            glstApplyVO = null;
            return true;
        }

        #endregion
        internal void m_lngGetBihExecOrderControls(out decimal m_dmlMedOCMin, out decimal m_dmlNoMedOCMin, out decimal m_dmlMedICMin, out decimal m_dmlNoMedICMin, out int m_intMoneyControl)
        {
            long lngRes = m_objManage.m_lngGetBihExecOrderControls(out m_dmlMedOCMin, out m_dmlNoMedOCMin, out m_dmlMedICMin, out m_dmlNoMedICMin, out m_intMoneyControl);
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

        internal void DoWork()
        {
            m_lngGetPatientDTByArea();
            m_lngGetChargeByArea();
            if (this.m_objViewer.m_chkReExcute.Checked == true)
                //��ִ�е�ִ��ҽ����ͳ��
                if (this.m_objViewer.m_chkReExcute.Checked == true)
                {
                    m_lngGetReExecute();
                }
        }

        /// <summary>
        /// ��ִ�е�ִ��ҽ����ͳ��
        /// ����ҽ��ִ�б�t_opr_bih_orderexecute�����¼��ÿ��ҽ������ִ�д���
        /// </summary>
        private void m_lngGetReExecute()
        {
            DataTable m_dtReExecute;
            m_htReExecute = new Hashtable();
            long lngRes = m_objManage.m_lngGetReExecute((string)m_objViewer.m_txtArea.Tag, out m_dtReExecute);
            //�˷������Կ����Ż�����m_dtReExecute����¼ҽ����ÿ��ҽ��ִ�еĴ���
            if (lngRes > 0 && m_dtReExecute.Rows.Count > 0)
            {
                int intRowsCount = m_dtReExecute.Rows.Count;
                System.Data.DataRow objRow = null;

                for (int i = 0; i < intRowsCount; i++)
                {
                    objRow = m_dtReExecute.Rows[i];
                    m_htReExecute.Add(objRow["orderid_chr"].ToString(), objRow["exeCounts"].ToString());
                }
            }
        }

        internal void RunWorkerCompleted()
        {
            while (true)
            {
                if (ECstate == true)
                {
                    break;
                }
            }
            ECstate = false;
            EPstate = false;
            EOstate = false;
            OrderListSelect();
            this.m_objViewer.m_plControls.Enabled = true;
        }

        internal void GetTheControl()
        {
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
            m_glstControl.Add("1046");//1046', '����Ƿ��ִ��ʱ�Ҳ��˽�Ƿ��ʱ�Ĳ��˷�����ʾ����', '0-����ʾ 1-��ʾ'
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
            if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
            {
                int intRowsCount = dtbResult.Rows.Count;
                System.Data.DataRow objOneDataRow = null;
                string strSetID = null;
                string strSetStatus = null;

                for (int i = 0; i < intRowsCount; i++)
                {
                    objOneDataRow = dtbResult.Rows[i];
                    strSetID = objOneDataRow["setid_chr"].ToString().Trim();
                    strSetStatus = objOneDataRow["setstatus_int"].ToString().Trim();

                    switch (strSetID)
                    {
                        case "1028":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blNeedComfirm = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blNeedComfirm = true;
                            }
                            break;
                        case "1029":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blExeConfirm = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blExeConfirm = true;
                            }
                            break;
                        case "1038"://'1038', 'סԺת�������Ƿ���ʾ�������', '0-��1-��', 1,
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blNeedMessageAlert = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blNeedMessageAlert = true;
                            }
                            break;
                        case "1039"://'1039', 'סԺת���������������ʾ���ʱ��', '��λ:��', 10, 
                            if (!strSetStatus.Equals(""))
                            {
                                this.m_objViewer.m_intMessageOpenTime = int.Parse(strSetStatus);
                            }
                            break;
                        case "1040"://'1040', 'סԺת������������Ѵ�����ʾͣ��ʱ��', '��λ:��', 5, 
                            if (!strSetStatus.Equals(""))
                            {
                                this.m_objViewer.m_intMessageCloseTime = int.Parse(strSetStatus);
                            }
                            break;
                        case "1018":
                            this.m_objViewer.m_dmlMedOCMin = decimal.Parse(strSetStatus);//Ƿ�Ѳ���ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1019":
                            this.m_objViewer.m_dmlNoMedOCMin = decimal.Parse(strSetStatus);//Ƿ�Ѳ��˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1020":
                            this.m_objViewer.m_dmlMedICMin = decimal.Parse(strSetStatus);//��ͨ����ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1021":
                            this.m_objViewer.m_dmlNoMedICMin = decimal.Parse(strSetStatus);//��ͨ���˷�ҩƷ��Ŀȷ����С������� 0-�������ƣ�����0��������Ƶ��޶�
                            break;
                        case "1030":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blMoneyControl = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blMoneyControl = true;
                            }

                            break;
                        case "1046":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blLessExecuteAlert = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blLessExecuteAlert = true;
                            }
                            break;
                        case "1047":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blCanSelectOrder = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blCanSelectOrder = true;
                            }
                            break;
                        case "1049":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blPutMedicineFormDic = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blPutMedicineFormDic = true;
                            }
                            break;
                        case "4006":
                            int.TryParse(strSetStatus, out this.m_objViewer.m_intLisDiscountNum);
                            break;
                        case "4007":
                            decimal.TryParse(strSetStatus, out this.m_objViewer.m_decLisDiscountMount);
                            break;
                        case "4008":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blLisDiscount = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blLisDiscount = true;
                            }
                            break;
                        case "1036":
                            if (strSetStatus.Equals("1"))
                            {
                                this.m_objViewer.m_blDeableMedControl = true;
                            }
                            else
                            {
                                this.m_objViewer.m_blDeableMedControl = false;
                            }
                            break;
                        case "1037":
                            if (strSetStatus.Equals("1"))
                            {
                                this.m_objViewer.m_blStopControl = true;
                            }
                            else
                            {
                                this.m_objViewer.m_blStopControl = false;
                            }
                            break;
                        case "1053":
                            if (strSetStatus.Equals("1"))
                            {
                                this.m_objViewer.m_blAutoStopAlert = true;
                            }
                            else
                            {
                                this.m_objViewer.m_blAutoStopAlert = false;
                            }
                            break;
                        case "1050":
                            if (strSetStatus.Equals("0"))
                            {
                                this.m_objViewer.m_blSendLisBill = false;
                            }
                            else
                            {
                                this.m_objViewer.m_blSendLisBill = true;
                            }
                            break;
                    }
                }
            }

        }


        /// <summary>
        /// ˢ�����е�����
        /// </summary>
        internal void RefreshHadDate()
        {
            this.m_objViewer.Cursor = Cursors.WaitCursor;
            if (this.m_objViewer.m_cboCode.SelectedIndex == 0)
            {
                refreshTheDataByBed(false);
            }
            else
            {
                refreshTheDataByBed(true);
            }
            this.m_objViewer.Cursor = Cursors.Default;
        }

        //��������
        internal bool Redraw()
        {
            List<clsBIHCanExecOrder> m_arrExecOrder = GetTheSelectDrawArrItem();
            long lngRes = m_objManage.m_lngExecDrawOrderByOrderID(m_arrExecOrder);
            if (lngRes > 0)
            {
                MessageBox.Show("����ִ�гɹ�!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshTheData();


            }
            this.m_objViewer.Cursor = Cursors.Default;

            return true;

        }

        /// <summary>
        /// ��ȡסԺ�������ñ�
        /// </summary>
        internal void SetSPECORDERCATE()
        {
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngAddGetSPECORDERCATE(out m_objSpecateVo);
        }

        internal long m_lngFindSendArea(string m_strAreaID, out DataTable m_dtItem)
        {
            long lngRes = 0;
            lngRes = m_objInputOrder.m_lngFindSendArea(m_strAreaID, out m_dtItem);
            return lngRes;
        }



        internal void TheSelectedItemForComfirmRun()
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

        internal bool GetTheComfirmRunSelectItemCount()
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

        internal long IsAllPatSend(string m_strAreaId, out bool ifAll)
        {
            long lngRes = m_objManage.IsAllPatSend(m_strAreaId, out ifAll);
            return lngRes;

        }

        /// <summary>
        /// �Ҽ�ѡ��ִ��ʱ��Ƿ������
        /// </summary>
        /// <returns></returns>
        internal bool m_blLessMoneyControl()
        {
            //Ƿ�Ѳ����б�
            ArrayList m_arrPatient = null;
            ArrayList arrSelected = null;
            if (this.m_objViewer.m_blMoneyControl == false)//
            {
                arrSelected = GetTheSelectArrItemList();
                decimal m_decSum = 0;
                Hashtable m_htChargeSum = new Hashtable();
                for (int j = 0; j < arrSelected.Count; j++)
                {
                    m_decSum = 0;
                    clsBIHCanExecOrder order = (clsBIHCanExecOrder)arrSelected[j];
                    if (!m_htChargeSum.ContainsKey(order.m_strRegisterID))
                    {
                        m_htChargeSum.Add(order.m_strRegisterID, m_decSum);
                    }
                    m_decSum = (decimal)m_htChargeSum[order.m_strRegisterID];
                    m_decSum += m_objInputOrder.GettheChargeSum(order, m_dtChargeList);
                    m_htChargeSum[order.m_strRegisterID] = m_decSum;
                }
                clsBIHCanExecOrder[] arrBed = (clsBIHCanExecOrder[])arrSelected.ToArray(typeof(clsBIHCanExecOrder));
                m_arrPatient = GetTheOverChargeList(arrBed, m_htChargeSum);
            }
            if (m_arrPatient != null && m_arrPatient.Count > 0)
            {
                if (arrSelected != null && arrSelected.Count > 0)
                {
                    ArrayList m_arrPatient2 = new ArrayList();
                    for (int i = 0; i < arrSelected.Count; i++)
                    {
                        if (m_arrPatient.Contains(((clsBIHCanExecOrder)arrSelected[i]).m_strRegisterID))
                        {
                            m_arrPatient2.Add((clsBIHCanExecOrder)arrSelected[i]);
                            m_arrPatient.Remove(((clsBIHCanExecOrder)arrSelected[i]).m_strRegisterID);
                        }
                    }
                    if (m_arrPatient2.Count > 0)
                    {
                        LessMoneyAlert(m_arrPatient2);
                        return false;
                    }

                }

            }
            return true;
        }

        /// <summary>
        /// Ƿ�Ѳ��˲���ִ����ʾ��
        /// </summary>
        /// <param name="m_arrLessMoney"></param>
        private void LessMoneyAlert(ArrayList m_arrLessMoney)
        {
            string m_strMessage = "";
            for (int i = 0; i < m_arrLessMoney.Count; i++)
            {
                m_strMessage += ((clsBIHCanExecOrder)m_arrLessMoney[i]).m_strBedName + "������" + ((clsBIHCanExecOrder)m_arrLessMoney[i]).m_strPatientName + "\r\n";
            }
            if (!m_strMessage.Equals(""))
            {
                m_strMessage += "ΪǷ�Ѳ���,ҽ������ִ��!";
                MessageBox.Show(m_strMessage, "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            /*<======================================*/
        }

        /// <summary>
        /// ѡ�е���Ŀת��ΪCHECKBOX��Ҳѡ��
        /// </summary>
        internal void SelectItemToChecked()
        {

            //��Ч��ѡ����(Ƥ��û�н����Ϊ���Ե�Ϊ��Ч)
            ArrayList m_arrActive = new ArrayList();
            string m_strID = "";//������ˮ�ǼǺż��������
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.SelectedRows.Count; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.SelectedRows[i].Tag;
                m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
                m_arrActive.Add(m_strID);

            }
            /*<================================*/
            m_strID = "";
            for (int i = 0; i < this.m_objViewer.m_dtvOrderList.Rows.Count; i++)
            {
                clsBIHCanExecOrder order = (clsBIHCanExecOrder)this.m_objViewer.m_dtvOrderList.Rows[i].Tag;
                m_strID = order.m_strRegisterID + "," + order.m_intRecipenNo.ToString() + ";";
                if (m_arrActive.Contains(m_strID))
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
        /// ϵͳ������(ICARE����)
        /// </summary>
        internal long LoadThePARMVALUE()
        {
            List<string> PARMCODE_CHR = new List<string>();
            DataTable m_dtPARMVALUE_VCHR = new DataTable();
            PARMCODE_CHR.Add("1008");//1008 סԺȷ�ϼ������̶�Ӧ�����ID ������������ݸ���
            PARMCODE_CHR.Add("0013");//0013 ������ϴ��۷�Ʊ���� ������������ݸ���
            //long lngRes = m_objManage.LoadThePARMVALUE(PARMCODE_CHR, out m_strPARMVALUE_VCHR);
            long lngRes = m_objManage.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
            if (lngRes > 0 && m_dtPARMVALUE_VCHR != null)
            {
                string m_strPARMCODE = "";
                string m_strPARMVALUE = "";
                for (int i = 0; i < m_dtPARMVALUE_VCHR.Rows.Count; i++)
                {
                    m_strPARMCODE = m_dtPARMVALUE_VCHR.Rows[i]["PARMCODE_CHR"].ToString().Trim();
                    m_strPARMVALUE = m_dtPARMVALUE_VCHR.Rows[i]["PARMVALUE_VCHR"].ToString().Trim();

                    switch (m_strPARMCODE)
                    {
                        case "1008":
                            m_strPARMVALUE_VCHR = m_strPARMVALUE;
                            break;
                        case "0013":
                            m_strLisPARMVALUE_VCHR = m_strPARMVALUE;
                            break;
                    }

                }
            }
            return lngRes;
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

        #region CheckCureMed
        /// <summary>
        /// CheckCureMed
        /// </summary>
        /// <param name="lstOrder"></param>
        /// <param name="lstSubStock"></param>
        /// <returns></returns>
        bool CheckCureMed(ref List<EntityCureMed> lstOrder, out List<EntityCureSubStock> lstSubStock)
        {
            lstSubStock = new List<EntityCureSubStock>();
            List<string> lstOrderId = new List<string>();
            Dictionary<string, double> dicGet = new Dictionary<string, double>();                   // key: ҩ��ID+OrderID
            Dictionary<string, double> dicGet3 = new Dictionary<string, double>();                  // key: RegisterId+OrderID+ҩ��ID+ҩƷID
            Dictionary<string, double> dicTmp = new Dictionary<string, double>();
            Dictionary<string, List<string>> dicOrder = new Dictionary<string, List<string>>();
            string key = string.Empty;
            foreach (EntityCureMed item in lstOrder)
            {
                lstOrderId.Add(item.orderId);
                if (dicOrder.ContainsKey(item.execDeptId))
                {
                    dicOrder[item.execDeptId].Add(item.orderId);
                }
                else
                {
                    dicOrder.Add(item.execDeptId, new List<string>() { item.orderId });
                }
                dicGet.Add(item.execDeptId + "*" + item.orderId, Convert.ToDouble(item.preAmount));
                dicTmp.Add(item.registerId + "*" + item.orderId + "*" + item.execDeptId, Convert.ToDouble(item.preAmount));
            }
            Dictionary<string, string> dicMedId = new Dictionary<string, string>();
            //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
            //{
            dicMedId = (new weCare.Proxy.ProxyIP()).Service.GetMedIdByOrderId(lstOrderId);
            //}
            string orderId = string.Empty;
            List<string> lstKey = new List<string>();
            lstKey.AddRange(dicTmp.Keys);
            foreach (string item in lstKey)
            {
                orderId = item.Split('*')[1];
                if (dicMedId.ContainsKey(orderId))
                {
                    dicGet3.Add(item + "*" + dicMedId[orderId], dicTmp[item]);
                }
            }

            #region ������
            List<string> lstOrderId_NoInDic = new List<string>();
            Dictionary<string, double> dicGet2 = new Dictionary<string, double>();                  // key: ҩ��ID+ҩƷID            
            Dictionary<string, List<string>> dicOrder2 = new Dictionary<string, List<string>>();    // key: ҩ��ID+ҩƷID
            foreach (string storeid in dicOrder.Keys)
            {
                foreach (string orderid in dicOrder[storeid])
                {
                    if (!dicMedId.ContainsKey(orderid))
                    {
                        lstOrderId_NoInDic.Add(orderid);     // ����ҩƷ�ֵ��е�orderId
                    }
                    else
                    {
                        if (dicOrder2.ContainsKey(storeid))
                        {
                            dicOrder2[storeid].Add(dicMedId[orderid]);
                        }
                        else
                        {
                            dicOrder2.Add(storeid, new List<string>() { dicMedId[orderid] });
                        }
                        key = storeid + "*" + dicMedId[orderid];
                        if (dicGet2.ContainsKey(key))
                        {
                            dicGet2[key] += dicGet[storeid + "*" + orderid];
                        }
                        else
                        {
                            dicGet2.Add(key, dicGet[storeid + "*" + orderid]);
                        }
                    }
                }
            }
            #endregion

            if (dicOrder2.Keys.Count > 0)
            {
                Dictionary<string, double> dicKc = null;    // <ҩ��ID*ҩID, �����>
                clsDsStorageVO[] dsStorageVOArr = null;
                clsDcl_ExecuteOrder execDcl = new clsDcl_ExecuteOrder();
                execDcl.m_lngGetMedicineKC(dicOrder2, out dicKc, out dsStorageVOArr);
                if (dsStorageVOArr != null && dsStorageVOArr.Length > 0)
                {
                    #region ����
                    // ����������λת������С��λ
                    Dictionary<string, string> dicMedName = new Dictionary<string, string>();
                    Dictionary<string, double> dicMedPack = new Dictionary<string, double>();
                    foreach (clsDsStorageVO vo in dsStorageVOArr)
                    {
                        key = vo.m_strPharmacyID + "*" + vo.m_strMedicineID;
                        if (!dicMedPack.ContainsKey(key))
                        {
                            if (vo.m_intIpChargeFlg == 0)
                            {
                                dicMedPack.Add(key, vo.m_dblPackqty);
                            }
                            else
                            {
                                dicMedPack.Add(key, 1);
                            }
                        }
                        if (!dicMedName.ContainsKey(vo.m_strMedicineID))
                        {
                            dicMedName.Add(vo.m_strMedicineID, vo.medName);
                        }
                    }
                    #endregion

                    #region ����ж�
                    lstKey.Clear();
                    lstKey.AddRange(dicGet2.Keys);
                    foreach (string key2 in lstKey)
                    {
                        if (dicMedPack.ContainsKey(key2))                           // ������λ
                        {
                            dicGet2[key2] = dicGet2[key2] * dicMedPack[key2];       // ��С��λ=������λ*��װ��
                        }
                        if (dicKc.ContainsKey(key2))
                        {
                            if (dicGet2[key2] > dicKc[key2])
                            {
                                MessageBox.Show("ҩƷ:" + dicMedName[key2.Split('*')[1]] + " ��治��\r\n\r\n�����:" + dicKc[key2] + " ������:" + dicGet2[key2], "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                            else
                            {
                                // ����ж�ͨ��
                            }
                        }
                        else
                        {
                            string medid = key2.Split('*')[1];
                            if (dicMedName.ContainsKey(medid))
                            {
                                MessageBox.Show("ҩƷ:" + dicMedName[medid] + " �޿��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("ҩƷ����:" + medid + " �޿��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            return false;
                        }
                    }
                    #endregion

                    #region ���ɿ��ۼ���Ϣ
                    double ipAmount = 0;
                    double opAmount = 0;
                    EntityCureSubStock subVo = null;
                    foreach (string key3 in dicGet3.Keys)
                    {
                        key = key3.Split('*')[2] + "*" + key3.Split('*')[3];
                        if (dicMedPack.ContainsKey(key))                       // ������λ
                        {
                            opAmount = dicGet3[key3];
                            ipAmount = dicGet3[key3] * dicMedPack[key];        // ��С��λ=������λ*��װ��
                        }
                        else
                        {
                            opAmount = Convert.ToDouble(clsPublic.Round(Convert.ToDecimal(dicGet3[key3]) / Convert.ToDecimal(dicMedPack[key]), 4));  // ������λ=��С��λ/��װ��
                            ipAmount = dicGet3[key3];
                        }
                        for (int k = 0; k < dsStorageVOArr.Length; k++)
                        {
                            clsDsStorageVO dsVo = dsStorageVOArr[k];
                            if (dsVo.m_dbIprealgross == 0) continue;
                            if (dsVo.m_strPharmacyID == key.Split('*')[0] && dsVo.m_strMedicineID == key.Split('*')[1])
                            {
                                subVo = new EntityCureSubStock();
                                subVo.serSid = dsVo.m_intSeriesID;
                                subVo.storeId = dsVo.m_strPharmacyID;
                                subVo.medId = dsVo.m_strMedicineID;
                                if (dsVo.m_dbIprealgross <= ipAmount)
                                {
                                    subVo.ipAmountReal = dsVo.m_dbIprealgross;
                                    subVo.opAmountReal = dsVo.m_dbOprealgross;
                                    ipAmount = ipAmount - subVo.ipAmountReal;
                                    opAmount = opAmount - subVo.opAmountReal;
                                    dsVo.m_dbIprealgross = 0;
                                    dsVo.m_dbOprealgross = 0;
                                }
                                else
                                {
                                    subVo.ipAmountReal = ipAmount;
                                    subVo.opAmountReal = opAmount;
                                    subVo.ipAmountVir = subVo.ipAmountReal;
                                    subVo.opAmountVir = subVo.opAmountReal;
                                    ipAmount = 0;
                                    dsVo.m_dbIprealgross -= ipAmount;
                                    dsVo.m_dbOprealgross -= opAmount;
                                }
                                subVo.registerId = key3.Split('*')[0];
                                subVo.orderId = key3.Split('*')[1];
                                lstSubStock.Add(subVo);
                                if (ipAmount == 0) break;
                            }
                        }
                    }
                    #endregion

                    #region ���
                    foreach (EntityCureMed item in lstOrder)
                    {
                        item.checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        item.checkState = "1";
                        item.checkOperName = this.m_objViewer.LoginInfo.m_strEmpID;
                    }
                    //using (clsBIHOrderService svc = new clsDcl_GetSvcObject().m_GetOrderSvcObject())
                    //{
                    //    if (svc.SaveCureMedConfirm(lstOrder, lstSubStock) > 0)
                    //    {
                    //        MessageBox.Show("��˳ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //}
                    #endregion
                }
            }
            return true;
        }
        #endregion
    }

    #region Log

    public class Log
    {
        public static void OutputXml(string xml)
        {
            bool isWrite = true;
            if (isWrite) Output(xml);
        }

        public static void OutputXml(string xml, bool isWrite)
        {
            if (isWrite) Output(xml);
        }

        public static void Output(string txt)
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd");
            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            string strFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + ".txt";
            bool blnAllWaysNew = false;
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(strFile);
                if (fi.Exists)
                {
                    if (fi.Length >= 2000000)
                    {
                        fi.CopyTo(System.AppDomain.CurrentDomain.BaseDirectory + @"\log\" + strDate + "-" + DateTime.Now.ToString("HHmm") + ".txt", true);
                        sw = fi.CreateText();
                    }
                    else
                    {
                        if (blnAllWaysNew)
                        {
                            sw = fi.CreateText();
                        }
                        else
                        {
                            sw = fi.AppendText();
                        }
                    }
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine("-->>>>> " + strTime);
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public static void Output(string fileName, string txt)
        {
            StreamWriter sw = null;
            try
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    sw = fi.AppendText();
                }
                else
                {
                    if (!Directory.Exists(fi.DirectoryName))
                    {
                        Directory.CreateDirectory(fi.DirectoryName);
                    }
                    sw = fi.CreateText();
                }
                sw.WriteLine(txt);
                sw.WriteLine();
            }
            catch (Exception e)
            {
                //throw e;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
    #endregion
}
