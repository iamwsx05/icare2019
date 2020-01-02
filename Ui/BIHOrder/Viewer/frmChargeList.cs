using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmChargeList : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
    {
        public ListViewItem[] arrItem = new ListViewItem[0];
        public ListViewItem m_lviSItem = new ListViewItem();
        /// <summary>
        /// ֱ������ҽ����������Ŀ����
        /// </summary>
        public ListViewItem[] m_lviSItemArr = null;
        public DataSet m_dsDicChargeSet;
        /// <summary>
        /// ������Ŀ����ID
        /// </summary>
        private string m_strORDERCATEID_CHR = "";
        /// <summary>
        /// Ƿ�ѿ���
        /// </summary>
        private bool m_blLessMedControl = false;
        /// <summary>
        ///  ��ѯ����
        /// </summary>
        private int m_intClass = 0;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_strOrderName = "";
        /// <summary>
        /// ҽ�������б�
        /// </summary>
        public Hashtable m_htOrderCate = new Hashtable();
        /// <summary>
        /// ͣ��ҽ���Ƿ��¼�뿪��
        /// </summary>
        private bool m_blStopControl = true;
        /// <summary>
        /// ȱҩ����Ŀ�Ƿ����¼��0-������false��1-����true
        /// </summary>
        private bool m_blDeableMedControl = false;
        /// <summary>
        /// ҩƷ�����
        /// </summary>
        public float m_dmlOpcurrentgross_num = 0;
        /// <summary>
        /// ��¼�Ƿ���ҩƷ 1��ҩƷ  2Ϊ����
        /// </summary>
        public int m_intITEMSRCTYPE_INT = 0;
        /// <summary>
        /// ҩ��ID
        /// </summary>
        public string m_strMedDeptId = "";
        /// <summary>
        /// ���ID
        /// </summary>
        public string strPayType = string.Empty;
        /// <summary>
        /// ��Ӧ֢.��ע
        /// </summary>
        public string syzRemark { get; set; }

        /// <summary>
        /// �Ƿ��ͯ�۸�
        /// </summary>
        internal bool isChildPrice { get; set; }

        public frmChargeList()
        {
            InitializeComponent();
            arrItem = new ListViewItem[0];

        }

        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_ChargeList();
            objController.Set_GUI_Apperance(this);
        }

        public frmChargeList(string m_strFindText, string ORDERCATEID_CHR, bool LessMedControl, int SelectClass, Hashtable m_htCate, bool StopControl, bool DeableMedControl, bool _isChildPrice)
        {
            m_htOrderCate = m_htCate;
            m_strORDERCATEID_CHR = ORDERCATEID_CHR;
            m_blLessMedControl = LessMedControl;
            m_intClass = SelectClass;
            m_strOrderName = m_strFindText;
            InitializeComponent();
            arrItem = new ListViewItem[0];
            m_blStopControl = StopControl;
            m_blDeableMedControl = DeableMedControl;
            this.isChildPrice = _isChildPrice;
        }

        private void frmChargeList_Load(object sender, EventArgs e)
        {
            m_txtFind.Text = m_strOrderName.TrimStart(@"/".ToCharArray());
            seachClass.SelectedIndex = m_intClass;

            clsT_aid_bih_ordercate_VO p_objItem;
            p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[m_strORDERCATEID_CHR];
            if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))
            {
                this.m_plTextOrder.Visible = true;
                this.m_cmdTextOrder.Visible = true;
            }


            LoadTheData();

        }

        private void LoadTheData()
        {
            m_lvwList.Items.Clear();
            m_lvwList.Items.AddRange(arrItem);
            if (m_lvwList.Items.Count > 0)
            {
                m_lvwList.Items[0].Selected = true;
                m_lvwList.Focus();
                if (m_dsDicChargeSet != null && m_dsDicChargeSet.Tables.Count > 0)
                {
                    DataView myDataView = m_dsDicChargeSet.Tables[0].DefaultView;
                    myDataView.RowFilter = "orderdicid_chr='" + ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderDicID + "'";
                    //m_dtvOrderdicCharge.DataSource = myDataView;
                    bintheChargeList(myDataView);
                    ClearTheBlank();
                }
                if (this.m_plTextOrder.Visible)
                {
                    SendKeys.Send("{F2}");
                }
                /*<=====================================*/
            }
            else if (this.m_plTextOrder.Visible)
            {
                this.txtInfo.Text = m_txtFind.Text;
                SendKeys.Send("{F2}");
            }
            else
            {

                SendKeys.Send("{Tab}");
                SendKeys.Send("{Tab}");
            }

        }


        private void m_lvwList_DoubleClick(object sender, EventArgs e)
        {
            if (m_lvwList.SelectedItems.Count <= 0)
            {
                return;
            }
            else
            {
                if (((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strDesc.Trim().Equals("*"))
                {
                    return;
                }
                if (m_blDeableMedControl == false)
                {
                    if (((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_intIPNOQTYFLAG_INT == 1)
                    {
                        MessageBox.Show("����ѡ��ȱҩ����!", "��ʾ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                }
                //��¼�Ƿ���ҩƷ
                this.m_intITEMSRCTYPE_INT = ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_intITEMSRCTYPE_INT;

                //�����(��ͺ����)
                int intOrderdicChargeCount = m_dtvOrderdicCharge.RowCount;
                for (int intI1 = 0; intI1 < intOrderdicChargeCount; intI1++)
                {
                    this.m_dmlOpcurrentgross_num += Convert.ToSingle(m_dtvOrderdicCharge.Rows[intI1].Cells["QTY_INT"].Value.ToString());
                }
                if (m_blStopControl == false)
                {

                    bool m_blCheck = false;
                    if (m_dsDicChargeSet != null && m_dsDicChargeSet.Tables.Count > 0)
                    {
                        DataView myDataView = m_dsDicChargeSet.Tables[0].DefaultView;
                        myDataView.RowFilter = "orderdicid_chr='" + ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderDicID + "'";
                        m_blCheck = CheckTheStopCharge(myDataView);

                    }
                    if (m_blCheck)
                    {
                        MessageBox.Show("����ѡ����ͣ���շ���Ŀ!", "��ʾ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                m_lviSItem = m_lvwList.SelectedItems[0];
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
                if (str.Contains(strPayType))
                {
                    string strRemark = string.Empty;
                    string strItemName = string.Empty;
                    bool blnRes = m_objDomain.m_blnShiying(((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderDicID, out strRemark, out strItemName);
                    if (blnRes)
                    {
                        // �޶�������������ҽԺ
                        if (strRemark.Trim() == "�޶�������������ҽԺ" || strRemark.Trim() == "�޶�������ҽԺ")
                            MessageBox.Show("����Ŀ�ޡ�" + strRemark + "��ʹ�á�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        else
                            MessageBox.Show("����Ŀ�ޡ�" + strRemark + "��ʹ�á�" + Environment.NewLine + "�жϱ������Ƿ������������������������ѡ�񡾷��ϡ����������ϡ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        this.syzRemark = strRemark;
                    }
                }
                this.DialogResult = DialogResult.OK;

            }
        }

        private bool CheckTheStopCharge(DataView myDataView)
        {
            if (myDataView.Count > 0)
            {

                for (int i = 0; i < myDataView.Count; i++)
                {
                    DataRowView row = myDataView[i];
                    if (row["IFSTOP_INT"].ToString().Trim().Equals("1"))//1ͣ�ã�0-����
                    {
                        return true; ;
                    }

                }
            }
            return false;

        }

        private void m_lvwList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    m_lvwList_DoubleClick(null, null);
                    break;
                case Keys.Escape:

                    this.DialogResult = DialogResult.No;
                    break;
            }
        }

        private void m_lvwList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lvwList.SelectedItems.Count > 0)
            {
                this.m_cmdAddOrder.Enabled = true;
                if (((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strDesc.Trim().Equals("*"))
                {
                    this.m_cmdAddOrder.Enabled = false;
                }

                if (m_dsDicChargeSet != null && m_dsDicChargeSet.Tables.Count > 0)
                {
                    DataView myDataView = m_dsDicChargeSet.Tables[0].DefaultView;
                    myDataView.RowFilter = "orderdicid_chr='" + ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderDicID + "'";
                    //m_dtvOrderdicCharge.DataSource = myDataView;
                    //m_dtvOrderdicCharge.DataMember = "chargeList";
                    bintheChargeList(myDataView);

                    ClearTheBlank();
                }

                clsT_aid_bih_ordercate_VO p_objItem;
                p_objItem = (clsT_aid_bih_ordercate_VO)m_htOrderCate[((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderCateID];
                if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))
                {
                    this.m_plTextOrder.Visible = true;
                    this.m_cmdTextOrder.Visible = true;

                }
                else
                {
                    this.m_plTextOrder.Visible = false;
                    this.m_cmdTextOrder.Visible = false;
                }
                //���µ�����ҽ�������
                txtInfo.Text = ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strName;
                txtInfo.Tag = (clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag;
                /*<=====================================*/


            }
        }

        private void bintheChargeList(DataView myDataView)
        {
            m_dtvOrderdicCharge.Rows.Clear();
            double m_intChargeSum = 0;
            double QTY_INT = 0, MinPrice = 0;
            if (myDataView.Count > 0)
            {
                QTY_INT = 0;
                MinPrice = 0;
                for (int i = 0; i < myDataView.Count; i++)
                {
                    DataRowView row = myDataView[i];

                    m_dtvOrderdicCharge.Rows.Add();
                    DataGridViewRow m_dgvRow = m_dtvOrderdicCharge.Rows[m_dtvOrderdicCharge.RowCount - 1];
                    m_dgvRow.Cells["itemCode"].Value = row["ITEMCODE_VCHR"].ToString();
                    m_dgvRow.Cells["ItemName"].Value = row["ItemName"].ToString();
                    if (row["IsChiefItem"].ToString().Trim().Equals("1"))
                    {
                        m_dgvRow.Cells["IsChiefItem"].Value = "��";
                    }
                    else
                    {
                        m_dgvRow.Cells["IsChiefItem"].Value = "";
                    }
                    m_dgvRow.Cells["QTY_INT"].Value = row["QTY_INT"].ToString();
                    if (row["ipcurrentgross_num"].ToString().Trim() == "")
                    {
                        m_dgvRow.Cells["QTY_INT"].Value = 0;
                    }
                    else
                    {
                        m_dgvRow.Cells["QTY_INT"].Value = row["ipcurrentgross_num"].ToString().Trim();
                    }
                    double.TryParse(row["QTY_INT"].ToString(), out QTY_INT);
                    double.TryParse(row["MinPrice"].ToString(), out MinPrice);
                    m_dgvRow.Cells["DOSAGE_DEC"].Value = row["DOSAGE_DEC"].ToString();
                    m_dgvRow.Cells["DOSAGEUNIT_CHR"].Value = row["DOSAGEUNIT_CHR"].ToString();
                    m_dgvRow.Cells["ITEMIPUNIT_CHR"].Value = row["ITEMIPUNIT_CHR"].ToString();
                    m_dgvRow.Cells["ITEMSPEC_VCHR"].Value = row["ITEMSPEC_VCHR"].ToString().Trim();
                    m_dgvRow.Cells["MinPrice"].Value = row["MinPrice"].ToString();
                    if (row["itemsrctype_int"].ToString().Trim().Equals("1") && row["IPNOQTYFLAG_INT"].ToString().Trim().Equals("1"))
                    {
                        m_dgvRow.Cells["IPNOQTYFLAG_INT"].Value = "ȱҩ";
                        m_dgvRow.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;

                    }
                    m_dgvRow.Cells["MinPrice"].Value = row["MinPrice"].ToString();
                    m_dgvRow.Cells["MedicareTypeName"].Value = row["MedicareTypeName"].ToString();
                    m_dgvRow.Cells["orderdicid_chr"].Value = row["orderdicid_chr"].ToString();
                    m_dgvRow.Cells["MEDICINEPREPTYPENAME_VCHR"].Value = row["MEDICINEPREPTYPENAME_VCHR"].ToString();

                    if (row["IFSTOP_INT"].ToString().Trim().Equals("1"))//1ͣ�ã�0-����
                    {
                        m_dgvRow.Cells["IFSTOP_INT"].Value = "ͣ";
                        m_dgvRow.DefaultCellStyle.ForeColor = SystemColors.Control;
                    }
                    else
                    {
                        m_dgvRow.Cells["IFSTOP_INT"].Value = "";
                    }

                    m_dgvRow.Cells["expenselimit_mny"].Value = row["expenselimit_mny"].ToString();
                    //
                    string m_strPrecent_dec = "";
                    //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                    (new weCare.Proxy.ProxyIP()).Service.m_lngGetChargeItemPrecent(row["itemid_chr"].ToString(), strPayType, out m_strPrecent_dec);
                    m_dgvRow.Cells["precent_dec"].Value = m_strPrecent_dec + "%";

                    /*
                    ItemName  �շ���
                    IsChiefItem ��
                    QTY_INT  ����
                    DOSAGE_DEC ����
                    DOSAGEUNIT_CHR ������λ
                    ITEMIPUNIT_CHR סԺ��λ
                    ITEMSPEC_VCHR  ���
                    MinPrice       ����
                    IPNOQTYFLAG_INT  ҩ��
                    orderdicid_chr
                     */

                    m_intChargeSum += QTY_INT * MinPrice;

                }
                this.m_lblChargeSum.Text = m_intChargeSum.ToString("0.00");
            }
        }

        public void ClearTheBlank()
        {
            for (int i = 0; i < m_dtvOrderdicCharge.RowCount; i++)
            {
                for (int j = 0; j < m_dtvOrderdicCharge.ColumnCount; j++)
                {
                    if (m_dtvOrderdicCharge[j, i].Value != null)
                        m_dtvOrderdicCharge[j, i].Value = m_dtvOrderdicCharge[j, i].Value.ToString().Trim();
                }
            }
        }

        private void m_dtvOrderdicCharge_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            string strFindCode = this.m_txtFind.Text.Trim();
            //��ѯ����
            int m_intClass = seachClass.SelectedIndex;
            clsBIHOrderDic[] arrDic = null;
            ((clsCtl_ChargeList)this.objController).m_lngGetOrderDicChargeByCode(strFindCode, m_intClass, m_strORDERCATEID_CHR, m_blLessMedControl, this.m_strMedDeptId, out arrDic, out m_dsDicChargeSet);
            arrItem = (ListViewItem[])getTheChargeItem(arrDic).ToArray(typeof(ListViewItem));
            LoadTheData();


        }

        private ArrayList getTheChargeItem(clsBIHOrderDic[] arrDic)
        {
            ArrayList m_arlItems = new ArrayList();
            if ((arrDic != null) && (arrDic.Length > 0))
            {
                for (int i = 0; i < arrDic.Length; i++)
                {
                    //string strDicCode = arrDic[i].m_strUserCode;
                    //switch (m_intClass)
                    //{
                    //    case -1:
                    //        strDicCode = arrDic[i].m_strPYCode;
                    //        break;
                    //    case 0:
                    //        strDicCode = arrDic[i].m_strPYCode;
                    //        break;
                    //    case 1:
                    //        strDicCode = arrDic[i].m_strWBCode;
                    //        break;
                    //    case 2:
                    //        strDicCode = arrDic[i].m_strName;
                    //        break;
                    //    case 3:
                    //        strDicCode = arrDic[i].m_strUserCode;
                    //        break;
                    //}
                    ////�û�����
                    //ListViewItem objItem = new ListViewItem((i + 1).ToString());
                    ////objItem.SubItems.Add(arrDic[i].m_strUserCode);
                    //objItem.SubItems.Add(strDicCode);
                    ////��Ŀ����(��������
                    //objItem.SubItems.Add(arrDic[i].m_strName);
                    ////��Ŀ����(ҩƷ��������)
                    //objItem.SubItems.Add(arrDic[i].m_strITEMCOMMNAME_VCHR);
                    ////��Ŀ���
                    //objItem.SubItems.Add(arrDic[i].m_strSpec);
                    ////��װ
                    ////objItem.SubItems.Add(arrDic[i].m_StrPackage);
                    ////סԺ����
                    //objItem.SubItems.Add(arrDic[i].m_dmlPrice.ToString("0.0000"));
                    ////��λ
                    //objItem.SubItems.Add(arrDic[i].m_strDosageUnit);
                    ////ҩ����ʾ
                    //if (arrDic[i].m_intITEMSRCTYPE_INT == 1 && arrDic[i].m_intIPNOQTYFLAG_INT == 1)
                    //{
                    //    objItem.SubItems.Add("ȱҩ");
                    //    objItem.ForeColor = Color.Red;
                    //}
                    //else
                    //{
                    //    objItem.SubItems.Add("");
                    //}

                    //objItem.Tag = arrDic[i];
                    //m_arlItems.Add(objItem);

                    string strDicCode = arrDic[i].m_strUserCode;
                    switch (m_intClass)
                    {
                        case -1:
                            strDicCode = arrDic[i].m_strPYCode;
                            break;
                        case 0:
                            strDicCode = arrDic[i].m_strPYCode;
                            break;
                        case 1:
                            strDicCode = arrDic[i].m_strWBCode;
                            break;
                        case 2:
                            strDicCode = arrDic[i].m_strName;
                            break;
                        case 3:
                            strDicCode = arrDic[i].m_strUserCode;
                            break;
                    }
                    //�û�����
                    ListViewItem objItem = new ListViewItem((i + 1).ToString());
                    objItem.SubItems.Add(arrDic[i].m_strUserCode);
                    //objItem.SubItems.Add(arrDic[i].m_strUserCode);
                    objItem.SubItems.Add(strDicCode);
                    //��Ŀ����(��������
                    objItem.SubItems.Add(arrDic[i].m_strName);
                    //��Ŀ����(ҩƷ��������)
                    objItem.SubItems.Add(arrDic[i].m_strITEMCOMMNAME_VCHR);
                    //��Ŀ���
                    objItem.SubItems.Add(arrDic[i].m_strSpec);
                    //��װ
                    //objItem.SubItems.Add(arrDic[i].m_StrPackage);
                    //סԺ����
                    //objItem.SubItems.Add(arrDic[i].m_dmlPrice.ToString("0.0000"));
                    //��λ
                    //objItem.SubItems.Add(arrDic[i].m_strDosageUnit);
                    //ҩ����ʾ
                    if (arrDic[i].m_intITEMSRCTYPE_INT == 1 && arrDic[i].m_intIPNOQTYFLAG_INT == 1)
                    {
                        objItem.SubItems.Add("ȱҩ");
                        objItem.ForeColor = Color.Red;
                    }
                    else
                    {
                        //ҽ����Ŀ->��ɫ
                        if (arrDic[i].m_strYBTypeID.Trim() != "")
                        {
                            objItem.SubItems.Add("ҽ����");
                            objItem.ForeColor = Color.Green;
                        }
                        else
                        {
                            objItem.SubItems.Add("");
                        }
                    }

                    objItem.Tag = arrDic[i];
                    m_arlItems.Add(objItem);
                }
            }
            return m_arlItems;
        }

        private void seachClass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_txtFind.Focus();
            }
        }

        private void m_txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btFind_Click(null, null);
            }
        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            if (m_lvwList.SelectedItems.Count <= 0 && this.m_plTextOrder.Visible == false)
            {

                return;
            }
            else
            {
                if (m_lvwList.SelectedItems.Count > 0)
                {
                    if (((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strDesc.Trim().Equals("*"))
                    {
                        return;
                    }
                    if (m_blDeableMedControl == false)
                    {
                        if (((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_intIPNOQTYFLAG_INT == 1)
                        {
                            MessageBox.Show("����ѡ��ȱҩ����!", "��ʾ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                    }
                    //��¼�Ƿ���ҩƷ
                    this.m_intITEMSRCTYPE_INT = ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_intITEMSRCTYPE_INT;

                    //�����(��ͺ����)
                    int intOrderdicChargeCount = m_dtvOrderdicCharge.RowCount;
                    for (int intI1 = 0; intI1 < intOrderdicChargeCount; intI1++)
                    {
                        this.m_dmlOpcurrentgross_num += Convert.ToSingle(m_dtvOrderdicCharge.Rows[intI1].Cells["QTY_INT"].Value.ToString());
                    }
                    if (m_blStopControl == false)
                    {
                        bool m_blCheck = false;
                        if (m_dsDicChargeSet != null && m_dsDicChargeSet.Tables.Count > 0)
                        {
                            DataView myDataView = m_dsDicChargeSet.Tables[0].DefaultView;
                            myDataView.RowFilter = "orderdicid_chr='" + ((clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag).m_strOrderDicID + "'";
                            m_blCheck = CheckTheStopCharge(myDataView);

                        }
                        if (m_blCheck)
                        {
                            MessageBox.Show("����ѡ����ͣ���շ���Ŀ!", "��ʾ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                //���µ�����ҽ�������
                if (this.m_plTextOrder.Visible == true)
                {
                    //if (this.txtInfo.Text.Trim().Equals(""))
                    //{
                    //    return;
                    //}
                    //m_lviSItem = new ListViewItem();
                    //clsBIHOrderDic orderDic = new clsBIHOrderDic();
                    //if (m_lvwList.SelectedItems.Count > 0)
                    //{
                    //    orderDic = (clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag;
                    //}
                    //else
                    //{
                    //    orderDic.m_strOrderCateID = m_strORDERCATEID_CHR;
                    //}
                    //orderDic.m_strName = txtInfo.Text;
                    //m_lviSItem.Tag = orderDic;

                    //m_lviSItemArr = new ListViewItem[1];
                    //m_lviSItemArr[0] = m_lviSItem;
                    clsBIHOrderDic orderDic = new clsBIHOrderDic();
                    if (GetTheTextOrder(ref orderDic) == false)
                    {
                        return;
                    }
                    m_lviSItem = new ListViewItem();
                    m_lviSItem.Tag = orderDic;
                    m_lviSItemArr = new ListViewItem[1];
                    m_lviSItemArr[0] = m_lviSItem;
                }
                else
                {
                    m_lviSItemArr = new ListViewItem[m_lvwList.SelectedItems.Count];
                    for (int i = 0; i < m_lvwList.SelectedItems.Count; i++)
                    {
                        m_lviSItemArr[i] = m_lvwList.SelectedItems[i];
                    }
                }
                this.DialogResult = DialogResult.Yes;

            }
        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {

            m_lviSItem = new ListViewItem();
            clsBIHOrderDic orderDic = new clsBIHOrderDic();
            if (GetTheTextOrder(ref orderDic) == false)
            {
                return;
            }
            //if (m_lvwList.SelectedItems.Count > 0)
            //{
            //    orderDic = (clsBIHOrderDic)m_lvwList.SelectedItems[0].Tag;
            //}
            //else
            //{
            //   orderDic.m_strOrderCateID=m_strORDERCATEID_CHR;
            //}

            m_lviSItem.Tag = orderDic;
            this.DialogResult = DialogResult.OK;


        }

        private bool GetTheTextOrder(ref clsBIHOrderDic orderDic)
        {

            if (txtInfo.Text.Trim().Equals(""))
            {
                MessageBox.Show("���ݲ���Ϊ��!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInfo.Focus();
                return false;

            }
            if (txtInfo.Tag is clsBIHOrderDic)
            {
                orderDic = (clsBIHOrderDic)txtInfo.Tag;
            }
            else
            {
                if (m_strORDERCATEID_CHR.Trim().Equals(""))
                {
                    MessageBox.Show("����ѡ������ҽ�����!", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //clsBIHOrderService m_objService = com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsBIHOrderService)) as clsBIHOrderService;
                //clsBIHOrderService m_objService = new clsDcl_GetSvcObject().m_GetOrderSvcObject();
                clsBIHOrderDic[] arrDic;
                (new weCare.Proxy.ProxyIP()).Service.m_lngGetOrderDicChargeByCode("", m_intClass, m_strORDERCATEID_CHR, false, this.m_strMedDeptId, out arrDic, out m_dsDicChargeSet, this.isChildPrice);
                //m_objService.m_lngGetOrderDicChargeByCode("", m_intClass, m_strORDERCATEID_CHR, false, out arrDic, out m_dsDicChargeSet);
                if (arrDic != null && arrDic.Length > 0)
                {
                    orderDic = arrDic[0];
                }

            }

            orderDic.m_strName = txtInfo.Text;
            return true;
        }

        private void frmChargeList_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:

                    this.DialogResult = DialogResult.No;
                    break;
                case Keys.F1:
                    btFind_Click(null, null);
                    break;
                case Keys.F2:
                    if (m_plTextOrder.Visible == true)
                    {
                        txtInfo.Focus();
                        txtInfo.SelectAll();
                    }
                    break;
                case Keys.F3:
                    if (m_cmdTextOrder.Visible == true)
                    {
                        buttonXP2_Click(null, null);
                    }
                    break;
                case Keys.F4:
                    buttonXP1_Click(null, null);
                    break;
            }
        }





    }
}