using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.iCare.BIHOrder.Control; 
using com.digitalwave.iCare.gui.HIS;	//���뵥�õ�
using System.Collections.Generic;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmBIHOrderGroupInput : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
    {
        /// <summary>
        /// �����б�
        /// </summary>
        public clsBIHOrderGroup[] arrGroup;
        /// <summary>
        /// ��ѡ�еķ�ͬ�������б���
        /// </summary>
        public Hashtable m_htBIHOrder = new Hashtable();
        /// <summary>
        /// ��ѡ�еķ�ͬ�������б���KEY
        /// </summary>
        public ArrayList m_arrBIHOrderKey = new ArrayList();
        /// <summary>
        /// ��ѡ�е�ͬ�������б���
        /// </summary>
        public Hashtable m_htBIHOrderGroup = new Hashtable();
        /// <summary>
        /// ��ѡ�е�ͬ�������б���KEY
        /// </summary>
        public ArrayList m_arrBIHOrderGroupKey = new ArrayList();
        /// <summary>
        /// ����ѡ��ı��¼���־
        /// </summary>
        public bool m_blGpChange = false;
        /// <summary>
        /// ��ǰ����ѡ�е���Ŀ�ܼ�
        /// </summary>
        public decimal m_dlCheckItem = 0;
        /// <summary>
        /// ��ǰԱ��
        /// </summary>
        //public string m_strDoctorID = "";
        /// <summary>
        /// ͬ��ҽ����������
        /// </summary>
        public ArrayList m_arrSameNo = new ArrayList();
        /// <summary>
        /// �����ɸ����崫���Ļ�����Ϣ�磺����ID�����ñ�־���Ƿ�Ƥ��
        /// </summary>
        public clsBIHOrder baseBIHOrder = new clsBIHOrder();
        /// <summary>
        /// ҽ���������datagridview�ؼ�
        /// </summary>
        public DataGridView m_dgvOrder = null;
        /// <summary>
        /// ҽ�������б�
        /// </summary>
        public Hashtable m_htOrderCate = new Hashtable();
        /// <summary>
        /// סԺ�������ñ�VO
        /// </summary>
        public clsSPECORDERCATE m_objSpecateVo;
        /// <summary>
        /// ��Դ(0-ҽ������,1-�շѵ���)
        /// </summary>
        public int m_intSourceType = 0;
        /// <summary>
        /// ��Դ=1-�շѵ���ʱ���ص�ѡ�е�������Ŀ����
        /// </summary>
        public ArrayList m_arrOrderDic = new ArrayList();
        /// <summary>
        /// ��Դ=1-�շѵ���ʱ���ص� ��ֵ
        /// </summary>
        public decimal m_decMount = 0;
        /// <summary>
        /// ͣ����Ŀ�Ƿ��¼�뿪��
        /// </summary>
        public bool m_blStopControl = true;
        /// <summary>
        /// ȱҩ����Ŀ�Ƿ����¼��0-������false��1-����true
        /// </summary>
        public bool m_blDeableMedControl = false;
        /// <summary>
        /// ���ص�������Ŀ��(������Ŀ����-1�У�����-2��)
        /// </summary>
        public DataTable m_dtOrderDic = new DataTable();
        public ArrayList m_strOrderCateList = new ArrayList();
        /// <summary>
        /// ҽ�����ý��淵�ص�ҽ������
        /// </summary>
        public List<clsBIHOrder> m_arrGroupOrder = null;
        /// <summary>
        /// ���ID
        /// </summary>
        public string strPayType = string.Empty;

        internal bool IsChildPrice { get; set; }

        public ctlBIHOrderDetail m_ctlOrder = null;
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_OrderGroupInput();
            objController.Set_GUI_Apperance(this);
        }

        public frmBIHOrderGroupInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="m_strFindCode">��ѯ����</param>
        /// <param name="OrderCateList">���˵���������</param>
        /// <param name="m_strEmpId">Ա����ID</param>
        /// <param name="m_strInpatientAreaID">Ա������ID</param>
        /// <param name="m_intClass">��ѯ����(-1Ĭ��ƴ��,0 ƴ��,1 ���,2 ����,3 �û�����,4 ���)</param>
        /// <param name="m_intType">��Դ(0-ҽ������,1-�շѵ���)</param>
        public frmBIHOrderGroupInput(string m_strFindCode, ArrayList OrderCateList, string m_strEmpId, string m_strInpatientAreaID, int m_intClass, int m_intType)
        {
            InitializeComponent();
            m_strOrderCateList = OrderCateList;
            m_strEmpId = this.LoginInfo.m_strEmpID;
            m_strInpatientAreaID = this.LoginInfo.m_strInpatientAreaID;
            this.Cursor = Cursors.WaitCursor;
            //����ͬ������ҽ��
            ((clsCtl_OrderGroupInput)this.objController).LoadTheGroup(m_strFindCode, m_strEmpId, m_strInpatientAreaID, m_intClass, out arrGroup);
            /*<-------------------------------------------------*/
            m_intSourceType = m_intType;
            this.Cursor = Cursors.Default;
            this.txtFind.Text = m_strFindCode;
            if (m_intClass > 3)
            {
                this.seachClass.SelectedIndex = 0;
            }
            else
            {
                this.seachClass.SelectedIndex = m_intClass;
            }
            ((clsCtl_OrderGroupInput)this.objController).LoadTheCate(ref m_htOrderCate);
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="m_strFindCode">��ѯ����</param>
        /// <param name="m_strEmpId">Ա����ID</param>
        /// <param name="m_strInpatientAreaID">Ա������ID</param>
        /// <param name="m_intClass">��ѯ����(-1Ĭ��ƴ��,0 ƴ��,1 ���,2 ����,3 �û�����,4 ���)</param>
        /// <param name="m_intType">��Դ(0-ҽ������,1-�շѵ���)</param>
        public frmBIHOrderGroupInput(string m_strFindCode, string m_strEmpId, string m_strInpatientAreaID, int m_intClass, int m_intType)
        {
            InitializeComponent();
            m_strEmpId = this.LoginInfo.m_strEmpID;
            m_strInpatientAreaID = this.LoginInfo.m_strInpatientAreaID;
            this.Cursor = Cursors.WaitCursor;
            //����ͬ������ҽ��
            ((clsCtl_OrderGroupInput)this.objController).LoadTheGroup(m_strFindCode, m_strEmpId, m_strInpatientAreaID, m_intClass, out arrGroup);
            /*<-------------------------------------------------*/
            m_intSourceType = m_intType;
            this.Cursor = Cursors.Default;
            this.txtFind.Text = m_strFindCode;
            if (m_intClass > 3)
            {
                this.seachClass.SelectedIndex = 0;
            }
            else
            {
                this.seachClass.SelectedIndex = m_intClass;
            }
            ((clsCtl_OrderGroupInput)this.objController).LoadTheCate(ref m_htOrderCate);
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="m_arrGroup"></param>
        /// <param name="m_BIHOrder"></param>
        /// <param name="m_OrderCate"></param>
        /// <param name="m_dgOrder"></param>
        /// <param name="m_SpecateVo"></param>
        /// <param name="strFindCode">��ѯ����</param>
        /// <param name="m_intClass">��ѯ����(-1Ĭ��ƴ��,0 ƴ��,1 ���,2 ����,3 �û�����,4 ���)</param>
        /// <param name="m_intType">��Դ(0-ҽ������,1-�շѵ���)</param>
        /// <param name="StopControl"></param>
        /// <param name="DeableMedControl"></param>
        public frmBIHOrderGroupInput(clsBIHOrderGroup[] m_arrGroup, clsBIHOrder m_BIHOrder, Hashtable m_OrderCate, DataGridView m_dgOrder, clsSPECORDERCATE m_SpecateVo, string strFindCode, int m_intClass, int m_intType, bool StopControl, bool DeableMedControl)
        {
            InitializeComponent();

            arrGroup = m_arrGroup;
            baseBIHOrder = m_BIHOrder;
            m_htOrderCate = m_OrderCate;
            m_dgvOrder = m_dgOrder;
            m_objSpecateVo = m_SpecateVo;
            this.txtFind.Text = strFindCode;
            this.seachClass.SelectedIndex = m_intClass;
            m_intSourceType = m_intType;
            m_blStopControl = StopControl;
            m_blDeableMedControl = DeableMedControl;
            this.TopMost = false;

        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="m_arrGroup"></param>
        /// <param name="m_BIHOrder"></param>
        /// <param name="m_OrderCate"></param>
        /// <param name="m_dgOrder"></param>
        /// <param name="m_SpecateVo"></param>
        /// <param name="strFindCode">��ѯ����</param>
        /// <param name="m_intClass">��ѯ����(-1Ĭ��ƴ��,0 ƴ��,1 ���,2 ����,3 �û�����,4 ���)</param>
        /// <param name="m_intType">��Դ(0-ҽ������,1-�շѵ���)</param>
        /// <param name="StopControl"></param>
        /// <param name="DeableMedControl"></param>
        public frmBIHOrderGroupInput(clsBIHOrderGroup[] m_arrGroup, clsBIHOrder m_BIHOrder, Hashtable m_OrderCate, DataGridView m_dgOrder, clsSPECORDERCATE m_SpecateVo, string strFindCode, int m_intClass, int m_intType, bool StopControl, bool DeableMedControl, ctlBIHOrderDetail p_ctlOrder, bool _isChildPrice)
        {
            InitializeComponent();

            arrGroup = m_arrGroup;
            baseBIHOrder = m_BIHOrder;
            m_htOrderCate = m_OrderCate;
            m_dgvOrder = m_dgOrder;
            m_objSpecateVo = m_SpecateVo;
            this.txtFind.Text = strFindCode;
            this.seachClass.SelectedIndex = m_intClass;
            m_intSourceType = m_intType;
            m_blStopControl = StopControl;
            m_blDeableMedControl = DeableMedControl;
            this.TopMost = false;
            this.m_ctlOrder = p_ctlOrder;
            this.IsChildPrice = _isChildPrice;
        }

        private void frmBIHOrderGroupInput_Load(object sender, EventArgs e)
        {
            //��ʼ������
            iniTheView();
            ((clsCtl_OrderGroupInput)this.objController).bindGroupTree();
            //this.ra_selectAll.Checked = true;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns></returns>
        private void iniTheView()
        {
            if (m_intSourceType == 0)
            {
                this.m_txtMount.Text = "1";
                this.m_txtMount.Visible = false;
                this.m_lblMount.Visible = false;
                this.m_cmdAddNewTemp.Visible = true;
                this.m_cmdChangeTemp.Visible = true;
            }
            else
            {
                this.m_txtMount.Text = "1";
                this.m_txtMount.Visible = true;
                this.m_lblMount.Visible = true;
                this.m_cmdAddNewTemp.Visible = false;
                this.m_cmdChangeTemp.Visible = false;

                GetTheControl();
            }
        }

        internal void GetTheControl()
        {
            int m_intNeedConfirm = -1;
            //long lngRes = m_objManage.GetTheComfirmControl(out m_intNeedConfirm, out m_intExeConfirm);
            System.Collections.Generic.List<string> m_arrControl = new System.Collections.Generic.List<string>();
            //ArrayList m_arrControl = new ArrayList();
            m_arrControl.Add("1036");//m_blDeableMedControl ҽ��¼���Ƿ����¼��ȱҩ���շ���Ŀ 0-��1-�� 1036
            m_arrControl.Add("1037");//m_blStopControl ҽ��¼���Ƿ����¼����ͣ�õ��շ���Ŀ 0-��,1-�� 1037

            DataTable dtbResult = null;
            long lngRes = ((clsCtl_OrderGroupInput)this.objController).GetTheHisControl(m_arrControl, out  dtbResult);
            if (lngRes > 0 && dtbResult != null && dtbResult.Rows.Count > 0)
            {
                for (int i = 0; i < dtbResult.Rows.Count; i++)
                {
                    switch (dtbResult.Rows[i]["setid_chr"].ToString().TrimEnd())
                    {
                        case "1036":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_blDeableMedControl = false;
                            }
                            else
                            {
                                m_blDeableMedControl = true;
                            }
                            break;
                        case "1037":
                            if (dtbResult.Rows[i]["setstatus_int"].ToString().Equals("0"))
                            {
                                m_blStopControl = false;
                            }
                            else
                            {
                                m_blStopControl = true;
                            }
                            break;
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_cmdChangeTemp.Enabled = false;
            this.Text = "����ģ��";
            if (e.Node.Tag is clsBIHOrderGroup)
            {
                clsBIHOrderGroup group1 = (clsBIHOrderGroup)e.Node.Tag;
                if (group1.m_strCreatorID.Trim().Equals(this.LoginInfo.m_strEmpID))
                {
                    m_cmdChangeTemp.Enabled = true;
                }
                m_blGpChange = true;
                m_dlCheckItem = 0;
                // ��������ID��ѯ��ص�������
                ((clsCtl_OrderGroupInput)this.objController).bindGroupListView(((clsBIHOrderGroup)e.Node.Tag).m_strGroupID.Trim());
                /*<------------------------------------------------*/
                //�Ƿ�����
                if (((clsBIHOrderGroup)e.Node.Tag).m_intISSAMERECIPENO_INT > 0)
                {
                    this.m_chkRECIPENO_SAME.Checked = true;
                }
                else
                {
                    this.m_chkRECIPENO_SAME.Checked = false;
                }
                /*<-------------------------------------------------*/
                //txtRemark.Text = ((clsBIHOrderGroup)e.Node.Tag).m_strDes.Trim();
                m_blGpChange = false;
                //ra_selectAll.Checked = false;
                //ra_selectAll.Checked = true;
                //ra_selectBack.Checked = false;
                this.Text = "����ģ��" + " " + group1.m_strName + "  " + group1.m_strUSERCODE_VCHR + "  " + group1.m_strPYCode + "  " + group1.m_strWBCode + " " + "ͣ�û�ȱҩ��Ŀ����ѡ��";
            }
            else
            {
                m_dtvGroupDetail.Rows.Clear();
                m_dtvChangeList.Rows.Clear();
            }
        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {


            //if(e.Item.Tag is clsBIHOrder&&!m_blGpChange&&m_chkRECIPENO_SAME.Checked)
            //{
            //    if (e.Item.Checked)
            //    {
            //        for (int i = 0; i < this.listView2.Items.Count; i++)
            //        {
            //            listView2.Items[i].Checked = true;


            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < this.listView2.Items.Count; i++)
            //        {
            //            listView2.Items[i].Checked = false; 

            //        }
            //    }

            //}
            ///*<===========================================================*/
            //clsBIHOrder order = new clsBIHOrder();
            //m_dlCheckItem = 0;
            //for (int i = 0; i < this.listView2.CheckedItems.Count; i++)
            //{
            //    order = (clsBIHOrder)this.listView2.CheckedItems[i].Tag;
            //    m_dlCheckItem += order.m_dmlPrice * (order.m_dmlGet);
            //}
            //this.label3.Text = m_dlCheckItem.ToString("0.000");

        }

        private void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (ra_selectAll.Checked)
            //{
            //    for (int i = 0; i < listView2.Items.Count; i++)
            //    {
            //        listView2.Items[i].Checked = true;
            //    }
            //}
            if (ra_selectAll.Checked == true)
            {
                bool m_blCan = true;
                string m_strCheck = "1";
                for (int i = 0; i < this.m_dtvGroupDetail.RowCount; i++)
                {
                    m_strCheck = "1";
                    m_blCan = true;

                    clsBIHOrder order = (clsBIHOrder)this.m_dtvGroupDetail.Rows[i].Tag;
                    //if (this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Tag.ToString().Equals("0"))
                    //{
                    //    m_strCheck = "0";
                    //    m_blCan = false;
                    //}
                    //this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value = m_strCheck;
                    //if (m_blCan == false)
                    //{
                    //    selectRowWithSon(order.m_intRecipenNo, m_strCheck, out  m_blCan);
                    //}
                    //ͬ��ѡ����ҽ��
                    selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);

                    /*<===============================*/
                    if (m_blCan == false)
                    {
                        m_strCheck = "0";
                        //ͬ��ѡ����ҽ��
                        selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);
                    }


                }
            }

            theCheckItemCout();
        }

        private void listView2_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            //MessageBox.Show("bad");
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void selectBack_CheckedChanged(object sender, EventArgs e)
        {
            //if (ra_selectBack.Checked)
            //{
            //    for (int i = 0; i < listView2.Items.Count; i++)
            //    {
            //        listView2.Items[i].Checked = !listView2.Items[i].Checked;
            //    }
            //}

        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (m_intSourceType == 0)
            {
                this.Cursor = Cursors.WaitCursor;

                clsBIHOrderInputDomain m_objDomain = new clsBIHOrderInputDomain();
                List<string> PARMCODE_CHR = new List<string>();
                PARMCODE_CHR.Add("0008");
                DataTable m_dtPARMVALUE_VCHR = new DataTable();
                string str = string.Empty;
                long lngRes = m_objDomain.LoadThePARMVALUE(PARMCODE_CHR, out m_dtPARMVALUE_VCHR);
                if (m_dtPARMVALUE_VCHR.Rows.Count > 0)
                {
                    str = m_dtPARMVALUE_VCHR.Rows[0]["PARMVALUE_VCHR"].ToString();
                }
                //����ͬ������ҽ��
                if (((clsCtl_OrderGroupInput)this.objController).SaveTheGroup() == true)
                {
                    for (int i = 0; i < this.m_arrGroupOrder.Count; i++)
                    {
                        clsBIHOrder order = (clsBIHOrder)this.m_arrGroupOrder[i];
                        if (str.Contains(strPayType))
                        {
                            string strRemark = string.Empty;
                            string strItemName = string.Empty;
                            bool blnRes = m_objDomain.m_blnShiying(order.m_strOrderDicID, out strRemark, out strItemName);
                            if (blnRes)
                            {
                                MessageBox.Show("��ģ������Ŀ��" + strItemName + "���ޡ�" + strRemark + "��ʹ�ã��жϱ������Ƿ��������������\n����������ѡ�񡾷��ϡ����������ϡ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);

                            }
                        }
                        if (((frmBIHOrderInput)this.m_ctlOrder.ParentForm).m_objDomain.m_blChcekOpcurrentgross(ref order) == false)
                        {
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    this.Cursor = Cursors.Default;
                    this.DialogResult = DialogResult.OK;
                    //this.Close();
                }
                this.Cursor = Cursors.Default;
            }
            else if (m_intSourceType == 1)
            {
                ((clsCtl_OrderGroupInput)this.objController).GetTheSelectOrderDic(ref m_arrOrderDic, ref m_dtOrderDic);

                m_decMount = clsConverter.ToDecimal(this.m_txtMount.Text.Trim());

                if (m_arrOrderDic.Count == 0)
                {
                    MessageBox.Show("��ѡ����Ŀ!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (m_decMount == 0)
                {
                    MessageBox.Show("�����������Ҳ���Ϊ0 ", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.m_txtMount.Focus();
                    this.m_txtMount.SelectAll();
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            string strFindCode = txtFind.Text.Trim();

            int m_intClass = seachClass.SelectedIndex;
            clsBIHOrderGroup[] arrGroup;
            //clsBIHOrderGroupService m_objService2 = new clsDcl_GetSvcObject().m_GetOrderGroupSvcObject();
            long ret1 = (new weCare.Proxy.ProxyIP()).Service.m_lngFindGroup(strFindCode, this.baseBIHOrder.m_strCreatorID, "", m_intClass, out arrGroup);
            if ((ret1 > 0) && (arrGroup != null))
            {
                this.arrGroup = arrGroup;
                ((clsCtl_OrderGroupInput)this.objController).bindGroupTree();
            }
        }

        #region ��ʼ������ؼ�������
        /// <summary>
        /// ��ʼ������ؼ�������
        /// </summary>
        public void iniControl()
        {
            this.treeView1.Nodes.Clear();
            //this.listView2.Items.Clear();
            this.m_dtvGroupDetail.Rows.Clear();
            this.m_htBIHOrder.Clear();
        }
        #endregion

        private void frmBIHOrderGroupInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                #region ��ݼ�
                case Keys.Escape:
                    btExit_Click(null, null);
                    break;
                case Keys.F1:
                    if (m_cmdAddNewTemp.Visible == true)
                    {
                        m_cmdAddNewTemp_Click(null, null);
                    }
                    break;
                case Keys.F2:
                    if (m_cmdChangeTemp.Visible == true)
                    {
                        m_cmdChangeTemp_Click(null, null);
                    }
                    break;
                case Keys.F3://
                    btOK_Click(null, null);
                    break;
                #endregion
            }

            // ��Ͽ�ݼ�
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.F)    // ��ӡҽ�� [Ctrl + F] 
                {
                    btFind_Click(null, null);
                }
            }
        }

        private void m_dtvGroupDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                int m_intRecipenNo = -1;    // ����
                string m_strCheck = "0";
                bool m_blCan = true;        // �Ƿ��ѡ��־(ͣ�û�ȱҩ��ĵ�ǰѡ��ֻ��״̬�����ж�)
                if (this.m_dtvGroupDetail.Rows.Count > 0)
                {
                    clsBIHOrder bihorder = (clsBIHOrder)this.m_dtvGroupDetail.Rows[e.RowIndex].Tag;
                    m_intRecipenNo = bihorder.m_intRecipenNo;
                    if (!this.m_dtvGroupDetail.Rows[e.RowIndex].Cells["dtv_Check"].Value.Equals("1"))
                    {
                        this.m_dtvGroupDetail.Rows[e.RowIndex].Cells["dtv_Check"].Value = "1";
                        m_strCheck = "1";
                    }
                    else
                    {
                        this.m_dtvGroupDetail.Rows[e.RowIndex].Cells["dtv_Check"].Value = "0";
                        m_strCheck = "0";
                    }
                    // ͬ��ѡ����ҽ��
                    selectRowWithSon(m_intRecipenNo, m_strCheck, out m_blCan);

                    /*<===============================*/
                    if (m_blCan == false)
                    {
                        m_strCheck = "0";
                        // ͬ��ѡ����ҽ��
                        selectRowWithSon(m_intRecipenNo, m_strCheck, out m_blCan);
                    }
                    theCheckItemCout();
                }

            }
        }

        private void selectRowWithSon(int m_intRecipenNo, string m_strCheck, out bool m_blCan)
        {
            m_blCan = true;
            for (int i = 0; i < this.m_dtvGroupDetail.RowCount; i++)
            {
                clsBIHOrder m_objOrder = (clsBIHOrder)this.m_dtvGroupDetail.Rows[i].Tag;
                if (m_objOrder.m_intRecipenNo == m_intRecipenNo)
                {
                    this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value = m_strCheck;
                    if (this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Tag.ToString().Equals("0"))
                    {
                        m_blCan = false;
                    }
                }
            }
        }

        /// <summary>
        /// ��ǰѡ����Ŀ�ϼ�
        /// </summary>
        private void theCheckItemCout()
        {

            m_dlCheckItem = 0;
            for (int i = 0; i < this.m_dtvGroupDetail.RowCount; i++)
            {
                if (this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value.ToString().Equals("0"))
                {
                    continue;
                }
                clsBIHOrder m_objOrder = (clsBIHOrder)this.m_dtvGroupDetail.Rows[i].Tag;
                m_dlCheckItem += m_objOrder.m_dmlPrice * (m_objOrder.m_dmlGet);
            }
            //this.label3.Text = m_dlCheckItem.ToString("0.000");
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btOK_Click(null, null);
            }
        }

        private void m_cmdAddNewTemp_Click(object sender, EventArgs e)
        {
            if (m_dgvOrder == null)
            {
                return;
            }
            //clsBIHOrder[] m_arrBihOrder = null;
            //if (m_dgvOrder.RowCount > 0)
            //{
            //    m_arrBihOrder = new clsBIHOrder[m_dgvOrder.RowCount];
            //    for (int i = 0; i < m_dgvOrder.RowCount; i++)
            //    {
            //        m_arrBihOrder[i] = (clsBIHOrder)m_dgvOrder.Rows[i].Tag;
            //    }

            //}
            //frmOrderTemplate_Group_Add m_frmGroupAdd = new frmOrderTemplate_Group_Add(m_arrBihOrder,m_htOrderCate,m_objSpecateVo);
            //m_frmGroupAdd.ShowDialog();
            ArrayList m_arrBihOrder = new ArrayList();

            if (this.m_dgvOrder.RowCount > 0)
            {
                for (int i = 0; i < m_dgvOrder.RowCount; i++)
                {
                    //ҽ������Ϊ����ҽ����Ϊ����ҽ���Ĳ���������
                    clsBIHOrder order = (clsBIHOrder)this.m_dgvOrder.Rows[i].Tag;
                    if (!CanCopyToGroup(order))
                    {
                        continue;
                    }
                    m_arrBihOrder.Add(order);
                }

            }
            if (m_arrBihOrder.Count > 0)
            {
                frmOrderTemplate_Group_Add m_frmGroupAdd = new frmOrderTemplate_Group_Add((clsBIHOrder[])(m_arrBihOrder.ToArray(typeof(clsBIHOrder))), m_htOrderCate, m_objSpecateVo);
                m_frmGroupAdd.ShowDialog();
            }
        }

        public bool CanCopyToGroup(clsBIHOrder order)
        {
            bool m_blCan = true;
            //ҽ������Ϊ����ҽ����Ϊ����ҽ���Ĳ���������
            if (order.m_intTYPE_INT != 0)
            {
                m_blCan = false;
            }
            /*<==============================*/
            return m_blCan;
        }

        private void ra_selectBack_CheckedChanged(object sender, EventArgs e)
        {
            if (ra_selectBack.Checked == true)
            {
                ArrayList m_hander = new ArrayList();
                for (int i = 0; i < this.m_dtvGroupDetail.RowCount; i++)
                {
                    string m_strCheck = "0";
                    bool m_blCan = true;
                    if (this.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value.ToString().Trim().Equals("1"))
                    {
                        clsBIHOrder order = (clsBIHOrder)this.m_dtvGroupDetail.Rows[i].Tag;
                        if (m_hander.Contains(order.m_intRecipenNo))
                        {
                            continue;
                        }
                        else
                        {
                            m_hander.Add(order.m_intRecipenNo);
                        }
                        m_strCheck = "0";
                        //ͬ��ѡ����ҽ��
                        selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);

                        /*<===============================*/
                        if (m_blCan == false)
                        {
                            m_strCheck = "0";
                            //ͬ��ѡ����ҽ��
                            selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);
                        }
                    }
                    else
                    {
                        clsBIHOrder order = (clsBIHOrder)this.m_dtvGroupDetail.Rows[i].Tag;
                        if (m_hander.Contains(order.m_intRecipenNo))
                        {
                            continue;
                        }
                        else
                        {
                            m_hander.Add(order.m_intRecipenNo);
                        }
                        m_strCheck = "1";
                        //ͬ��ѡ����ҽ��
                        selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);

                        /*<===============================*/
                        if (m_blCan == false)
                        {
                            m_strCheck = "0";
                            //ͬ��ѡ����ҽ��
                            selectRowWithSon(order.m_intRecipenNo, m_strCheck, out m_blCan);
                        }
                    }
                }
            }
        }

        private void m_dtvGroupDetail_CurrentCellChanged(object sender, EventArgs e)
        {
            ((clsCtl_OrderGroupInput)this.objController).m_lngBindChargeListByGroupDetail();
        }

        private void m_cmdChangeTemp_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is clsBIHOrderGroup)
            {
                com.digitalwave.iCare.gui.HIS.frmGroupManager groupManager = new frmGroupManager(((clsBIHOrderGroup)treeView1.SelectedNode.Tag).m_strGroupID);
                if (groupManager.ShowDialog() == DialogResult.OK)
                {
                    frmBIHOrderGroupInput_Load(null, null);

                }
            }
        }

        private void m_mthOnlyNumber(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = false;

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            { }
            else if ((e.KeyChar == 8) || (e.KeyChar == 13))
            { }
            else if ((e.KeyChar == '.'))
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btFind_Click(null, null);
            }
        }

    }
}