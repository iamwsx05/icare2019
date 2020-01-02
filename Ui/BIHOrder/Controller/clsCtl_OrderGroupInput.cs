using System;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace com.digitalwave.iCare.BIHOrder
{
    class clsCtl_OrderGroupInput : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        clsDcl_InputOrder m_objManage = null;
        /// <summary>
        /// ��ǰ������Ŀ�۸��ܺ�
        /// </summary>
        decimal m_decItemsAllCout = 0;
        /// <summary>
        /// ��ǰ��Ŀ�۸�
        /// </summary>
        decimal m_decItemsCount = 0;
        /// <summary>
        /// ҽ�����׳�Ա��
        /// </summary>
        private DataTable m_dtOrderGroupDetail = new DataTable();
        /// <summary>
        /// ����Ƶ����Ϣ
        /// </summary>
        public Hashtable m_htTempFreq = null;
        #endregion 

        #region ���캯��
        public clsCtl_OrderGroupInput()
        {
            m_objManage = new clsDcl_InputOrder();
        }
        #endregion

        #region ���ô������
        com.digitalwave.iCare.BIHOrder.frmBIHOrderGroupInput m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBIHOrderGroupInput)frmMDI_Child_Base_in;

        }
        #endregion

        #region �����б�
        /// <summary>
        /// �����б�
        /// </summary>
        internal void bindGroupTree()
        {
            this.m_objViewer.iniControl();

            //SHARETYPE_INT��������{1=˽��;2=����}
            //m_objViewer.arrGroup[i].m_intShareType
            this.m_objViewer.treeView1.Nodes.Add("ģ���б�");
            this.m_objViewer.treeView1.Nodes[0].Nodes.Add("˽��");
            this.m_objViewer.treeView1.Nodes[0].Nodes.Add("����");
            TreeNode selectNode = null;
            ArrayList m_arrPublic = new ArrayList();
            ArrayList m_arrPrivate = new ArrayList();
            for (int i = 0; i < m_objViewer.arrGroup.Length; i++)
            {
                if (m_objViewer.arrGroup[i].m_intShareType == 1)
                {
                    m_arrPrivate.Add(m_objViewer.arrGroup[i]);
                }
                else
                {
                    m_arrPublic.Add(m_objViewer.arrGroup[i]);
                }
            }
            if (m_arrPrivate.Count > 0)
            {
                TreeNode FindNode = this.m_objViewer.treeView1.Nodes[0].Nodes[0];

                for (int i = 0; i < m_arrPrivate.Count; i++)
                {
                    clsBIHOrderGroup m_Group = (clsBIHOrderGroup)m_arrPrivate[i];
                    string[] m_arrSubNodes = m_Group.m_strName.Split("-".ToCharArray());
                    TreeNode[] m_arrTrAdd = new TreeNode[m_arrSubNodes.Length];
                    if (m_arrTrAdd.Length == 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < m_arrSubNodes.Length; j++)
                    {
                        m_arrTrAdd[j] = new TreeNode();
                        m_arrTrAdd[j].Tag = null;
                        m_arrTrAdd[j].ImageIndex = 0;
                        m_arrTrAdd[j].SelectedImageIndex = 1;
                        m_arrTrAdd[j].Text = m_arrSubNodes[j];
                        if (j == m_arrSubNodes.Length - 1)
                        {
                            m_arrTrAdd[j].ImageIndex = 2;
                            m_arrTrAdd[j].SelectedImageIndex = 2;
                            m_arrTrAdd[j].Tag = m_arrPrivate[i];
                        }
                        if (j > 0)
                        {
                            m_arrTrAdd[j - 1].Nodes.Add(m_arrTrAdd[j]);
                        }
                    }
                    FindNode.Nodes.Add(m_arrTrAdd[0]);

                }


            }
            if (m_arrPublic.Count > 0)
            {
                TreeNode FindNode = this.m_objViewer.treeView1.Nodes[0].Nodes[1];

                for (int i = 0; i < m_arrPublic.Count; i++)
                {
                    clsBIHOrderGroup m_Group = (clsBIHOrderGroup)m_arrPublic[i];
                    // TreeNode trAdd = new TreeNode();
                    string[] m_arrSubNodes = m_Group.m_strName.Split("-".ToCharArray());
                    TreeNode[] m_arrTrAdd = new TreeNode[m_arrSubNodes.Length];
                    if (m_arrTrAdd.Length == 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < m_arrSubNodes.Length; j++)
                    {
                        m_arrTrAdd[j] = new TreeNode();
                        m_arrTrAdd[j].Tag = null;
                        m_arrTrAdd[j].ImageIndex = 0;
                        m_arrTrAdd[j].SelectedImageIndex = 1;
                        m_arrTrAdd[j].Text = m_arrSubNodes[j];
                        if (j == m_arrSubNodes.Length - 1)
                        {
                            m_arrTrAdd[j].ImageIndex = 2;
                            m_arrTrAdd[j].SelectedImageIndex = 2;
                            m_arrTrAdd[j].Tag = m_arrPublic[i];
                        }
                        if (j > 0)
                        {
                            m_arrTrAdd[j - 1].Nodes.Add(m_arrTrAdd[j]);
                        }



                    }
                    FindNode.Nodes.Add(m_arrTrAdd[0]);

                }

            }
            sortTheTree(this.m_objViewer.treeView1.Nodes[0]);


            if (m_objViewer.treeView1.Nodes[0].Nodes[0].Nodes.Count > 0)
            {
                TreeNode node1 = m_objViewer.treeView1.Nodes[0].Nodes[0];
                while (node1.Nodes.Count > 0)
                {
                    node1 = node1.Nodes[0];
                }
                m_objViewer.treeView1.SelectedNode = node1;
            }
            else if (m_objViewer.treeView1.Nodes[0].Nodes[1].Nodes.Count > 0)
            {
                TreeNode node1 = m_objViewer.treeView1.Nodes[0].Nodes[1];
                while (node1.Nodes.Count > 0)
                {
                    node1 = node1.Nodes[0];
                }
                m_objViewer.treeView1.SelectedNode = node1;
            }

        }
        private void sortTheTree(TreeNode FindNode)
        {
            if (FindNode.Nodes.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < FindNode.Nodes.Count; i++)
            {
                for (int j = i + 1; j < FindNode.Nodes.Count; j++)
                {
                    if (FindNode.Nodes[i].Text.Trim().Equals(FindNode.Nodes[j].Text.Trim()))
                    {
                        for (int k = 0; k < FindNode.Nodes[j].Nodes.Count; k++)
                        {
                            FindNode.Nodes[i].Nodes.Add((TreeNode)(FindNode.Nodes[j].Nodes[k].Clone()));
                            //FindNode.Nodes.Remove(FindNode.Nodes[j].Nodes[k]);
                            //k--;
                        }
                        FindNode.Nodes.Remove(FindNode.Nodes[j]);
                        j--;
                    }
                }

                sortTheTree(FindNode.Nodes[i]);
            }

        }



        #endregion

        #region ��������ID��ѯ��ص�������
        /// <summary>
        /// ��������ID��ѯ��ص�������
        /// </summary>
        /// <param name="m_strGroupID"></param>
        internal void bindGroupListView(string m_strGroupID)
        {

            m_LoadDataInfo(m_strGroupID);
        }
        #endregion

        #region ��������ҽ��(blnIsSameNO-trueͬ��,false ��ͬ��)
        /// <summary>
        /// ��������ҽ��(blnIsSameNO-trueͬ��,false ��ͬ��)
        /// </summary>
        public bool SaveTheGroup()
        {
            bool m_blSave = false;
            string[] strRecordIDArr;
            this.m_objViewer.m_arrGroupOrder = new List<clsBIHOrder>();
            //if (this.m_objViewer.listView2.CheckedItems.Count > 0)
            if (this.m_objViewer.m_dtvGroupDetail.RowCount > 0)
            {

                for (int i = 0; i < this.m_objViewer.m_dtvGroupDetail.RowCount; i++)
                {
                    //clsBIHOrder order = (clsBIHOrder)this.m_objViewer.listView2.CheckedItems[i].Tag;
                    if (this.m_objViewer.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value.ToString().Trim().Equals("0"))
                    {
                        continue;
                    }
                    clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvGroupDetail.Rows[i].Tag;
                    /*���������Ϣ��ҽ��VO��*/
                    BaseToVo(ref order);
                    this.m_objViewer.m_arrGroupOrder.Add(order);
                }
                if (this.m_objViewer.m_arrGroupOrder.Count > 0)
                {
                    //m_objManage.m_lngAddNewOrderByGroup(out strRecordIDArr, m_arrGroupOrder);
                    m_blSave = true;
                    /*<======================*/
                }
                else
                {
                    MessageBox.Show("��ѡ����Ŀ!", "���棡", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            /*����ͬ������*/

            return m_blSave;
            /*<-----------------------------------------------------*/

        }

        /// <summary>
        /// ����ѡ�е�������Ŀ����
        /// </summary>
        /// <returns></returns>
        public ArrayList GetTheSelectOrderDic()
        {
            ArrayList m_arrOrderDic = new ArrayList();
            if (this.m_objViewer.m_dtvGroupDetail.RowCount > 0)
            {

                for (int i = 0; i < this.m_objViewer.m_dtvGroupDetail.RowCount; i++)
                {
                    //clsBIHOrder order = (clsBIHOrder)this.m_objViewer.listView2.CheckedItems[i].Tag;
                    if (this.m_objViewer.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value.ToString().Trim().Equals("0"))
                    {
                        continue;
                    }
                    clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvGroupDetail.Rows[i].Tag;
                    m_arrOrderDic.Add(order.m_strOrderDicID);
                }

            }
            return m_arrOrderDic;
        }


        /// <summary>
        /// ����Ƶ��id��ȡƵ��VO
        /// </summary>
        /// <param name="m_strExecFreqID"></param>
        /// <returns></returns>
        public clsAIDRecipeFreq GetFreqVoByFreqID(string m_strExecFreqID)
        {
            if (m_htTempFreq == null)
            {
                // clsBIHOrderService m_objService = clsGenerator.CreateObject(typeof(clsBIHOrderService)) as clsBIHOrderService;
                m_htTempFreq = new Hashtable();
                clsAIDRecipeFreq[] Freq;
                m_objManage.m_lngGetRecipeFreq("", out Freq);
                for (int i = 0; i < Freq.Length; i++)
                {
                    m_htTempFreq.Add(Freq[i].m_strFreqID, Freq[i]);
                }

            }
            clsAIDRecipeFreq m_objTemp = (clsAIDRecipeFreq)m_htTempFreq[m_strExecFreqID];
            if (m_objTemp == null)
            {
                m_objTemp = new clsAIDRecipeFreq();
            }
            return m_objTemp;
        }

        private void BaseToVo(ref clsBIHOrder order)
        {

            //order.m_strOrderID = m_objViewer.baseBIHOrder.m_strOrderID;
            //order.m_intRecipenNo = m_objViewer.baseBIHOrder.m_intRecipenNo;
            order.m_strRegisterID = m_objViewer.baseBIHOrder.m_strRegisterID;
            //order.m_strParentID = m_objViewer.baseBIHOrder.m_strParentID;
            order.m_strPatientID = m_objViewer.baseBIHOrder.m_strPatientID;
            //order.m_intExecuteType = m_objViewer.baseBIHOrder.m_intExecuteType;
            order.m_intIsRepare = m_objViewer.baseBIHOrder.m_intIsRepare;
            //order.m_intRateType = m_objViewer.baseBIHOrder.m_intRateType;

            order.m_strCreator = m_objViewer.baseBIHOrder.m_strCreator;
            order.m_strCreatorID = m_objViewer.baseBIHOrder.m_strCreatorID;
            //order.m_strOrderID = m_objViewer.baseBIHOrder.m_strOrderID;
            order.m_dtCreatedate = DateTime.Now;
            // ����������ң���ǰ���ң�
            order.m_strCREATEAREA_ID = m_objViewer.baseBIHOrder.m_strCREATEAREA_ID;
            order.m_strCREATEAREA_Name = m_objViewer.baseBIHOrder.m_strCREATEAREA_Name;

            order.m_strCHARGEDOCTORGROUPID = m_objViewer.baseBIHOrder.m_strCHARGEDOCTORGROUPID;
            order.m_strDOCTORID_CHR = m_objViewer.baseBIHOrder.m_strDOCTORID_CHR;
            order.m_strDOCTOR_VCHR = m_objViewer.baseBIHOrder.m_strDOCTOR_VCHR;
            order.m_strDOCTORGROUPID_CHR = m_objViewer.baseBIHOrder.m_strDOCTORGROUPID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURAREAID_CHR = m_objViewer.baseBIHOrder.m_strCURAREAID_CHR;
            //��ҽ��ʱ�������ڲ���ID
            order.m_strCURBEDID_CHR = m_objViewer.baseBIHOrder.m_strCURBEDID_CHR;
            /*<==============================================*/
            //order.m_intOUTGETMEDDAYS_INT=m_objViewer.baseBIHOrder.m_intOUTGETMEDDAYS_INT;
            //order.m_intATTACHTIMES_INT=m_objViewer.baseBIHOrder.m_intATTACHTIMES_INT;
            order.m_dtStartDate = m_objViewer.baseBIHOrder.m_dtStartDate;
            order.m_dtFinishDate = m_objViewer.baseBIHOrder.m_dtFinishDate;
            //ҽ��ǩ��
            order.SIGN_INT = m_objViewer.baseBIHOrder.SIGN_INT;
            // �������ͼ�Ƶ�ʴ���
            clsAIDRecipeFreq m_objTempFreq = GetFreqVoByFreqID(order.m_strExecFreqID);
            if (m_objTempFreq != null)
            {
                order.m_intFreqTime = m_objTempFreq.m_intTimes;
                order.m_intFreqDays = m_objTempFreq.m_intDays;
            }
            //�������������ε���

        }
        #endregion


        /// <summary>
        /// Ϊ�б��������ݲ��������ݷ��úϼ�
        /// </summary>
        /// <param name="m_strGroupID"></param>
        private void m_LoadDataInfo(string m_strGroupID)
        {

            if (m_strGroupID == string.Empty) return;
            //���������Ϣ
            clsT_aid_bih_ordergroup_VO p_objResult = new clsT_aid_bih_ordergroup_VO();
            long lngRes = m_objManage.m_lngGetOrderGroupByID(m_strGroupID, out p_objResult);

            lngRes = m_objManage.m_lngGetOrderGroupDetailByGroupID(m_strGroupID, out m_dtOrderGroupDetail);
            if (lngRes > 0)
            {
                /*���б�����*/
                ListViewbind(m_strGroupID);
            }
            /**********��ǰ��������ѡ����ú�,����ԭ֮ǰ��ѡ��״̬*************/
            clsBIHOrder order;
            decimal price = 0;
            for (int i = 0; i < m_objViewer.m_dtvGroupDetail.RowCount; i++)
            {
                order = (clsBIHOrder)m_objViewer.m_dtvGroupDetail.Rows[i].Tag;
                ////��ͬ�������б�
                //if (m_objViewer.m_htBIHOrder.Contains(order.m_strOrderID))
                //{
                //    m_objViewer.listView2.Items[i].Checked = true;
                //}
                ///*<--------------------------------------*/
                ////ͬ�������б�
                //if (m_objViewer.m_htBIHOrderGroup.Contains(order.m_strOrderID))
                //{
                //    m_objViewer.listView2.Items[i].Checked = true;
                //}
                ///*<--------------------------------------*/
                price += order.m_dmlPrice * (order.m_dmlGet);
            }
            //this.m_objViewer.label2.Text = price.ToString("0.000");
            /*<=======================================================*/
        }
        /// <summary>
        /// ��ȡ��Ӧ����ϸ����
        /// </summary>
        /// <param name="myDataView"></param>
        /// <param name="m_objGroupVo"></param>
        private void GetGroupDetailData(DataView objRow, out clsT_aid_bih_ordergroup_detail_VO[] m_arrObjItem)
        {


            m_arrObjItem = new clsT_aid_bih_ordergroup_detail_VO[objRow.Count];
            for (int i = 0; i < objRow.Count; i++)
            {
                m_arrObjItem[i] = new clsT_aid_bih_ordergroup_detail_VO();
                //��ˮ��
                m_arrObjItem[i].m_strDETAILID_CHR = Convert.ToString(objRow[i]["DETAILID_CHR"].ToString().Trim());
                //����id	{=ҽ������.Id}
                m_arrObjItem[i].m_strGROUPID_CHR = Convert.ToString(objRow[i]["GROUPID_CHR"].ToString().Trim());
                //������Ŀid	{=������Ŀ.Id}
                m_arrObjItem[i].m_strORDERDICID_CHR = Convert.ToString(objRow[i]["ORDERDICID_CHR"].ToString().Trim());
                //������Ŀ����

                m_arrObjItem[i].m_strOrderdicName = Convert.ToString(objRow[i]["OrderdicName"].ToString().Trim());
                m_arrObjItem[i].m_strOrderName = Convert.ToString(objRow[i]["NAME_VCHR"].ToString().Trim());

                //ִ��Ƶ��id	{=ִ��Ƶ��.Id}
                m_arrObjItem[i].m_strFREQID_CHR = Convert.ToString(objRow[i]["FREQID_CHR"].ToString().Trim());
                //ִ��Ƶ������
                m_arrObjItem[i].m_strExecFreqName = Convert.ToString(objRow[i]["freqname_chr"].ToString().Trim());
                //һ�μ���
                if (!objRow[i]["DOSAGE_DEC"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_fltDOSAGE_DEC = float.Parse(objRow[i]["DOSAGE_DEC"].ToString().Trim());
                //������λ
                m_arrObjItem[i].m_strDOSAGEUNIT_CHR = Convert.ToString(objRow[i]["DOSAGEUNIT_CHR"].ToString().Trim());
                //һ������
                if (!objRow[i]["USE_DEC"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_fltUSE_DEC = float.Parse(objRow[i]["USE_DEC"].ToString().Trim());
                //������λ
                m_arrObjItem[i].m_strUSEUNIT_CHR = Convert.ToString(objRow[i]["USEUNIT_CHR"].ToString().Trim());
                //һ������
                if (!objRow[i]["GET_DEC"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_fltGET_DEC = float.Parse(objRow[i]["GET_DEC"].ToString().Trim());
                //������λ
                m_arrObjItem[i].m_strGETUNIT_CHR = Convert.ToString(objRow[i]["GETUNIT_CHR"].ToString().Trim());
                //��ҩ��ʽid
                m_arrObjItem[i].m_strDOSETYPE_CHR = Convert.ToString(objRow[i]["DOSETYPE_CHR"].ToString().Trim());
                //��ҩ��ʽ����
                m_arrObjItem[i].m_strDosetypeName = Convert.ToString(objRow[i]["usagename_vchr"].ToString().Trim());
                //ҽ������
                m_arrObjItem[i].m_strENTRUST_VCHR = Convert.ToString(objRow[i]["ENTRUST_VCHR"].ToString().Trim());
                //�Ƿ����Ʒ
                if (!objRow[i]["ISRICH_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intISRICH_INT = int.Parse(objRow[i]["ISRICH_INT"].ToString().Trim());
                //��������Ŀid	{=������Ŀ.Id}
                m_arrObjItem[i].m_strPARENTID_CHR = Convert.ToString(objRow[i]["PARENTID_CHR"].ToString().Trim());
                //�Ƿ�������Ŀ
                if (!objRow[i]["IFPARENTID_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intIFPARENTID_INT = int.Parse(objRow[i]["IFPARENTID_INT"].ToString().Trim());
                //ִ������{1=����;2=��ʱ,3=��Ժ��ҩ}
                if (!objRow[i]["EXECUTETYPE_INT"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intEXECUTETYPE_INT = int.Parse(objRow[i]["EXECUTETYPE_INT"].ToString().Trim());
                }
                if (m_arrObjItem[i].m_intEXECUTETYPE_INT == 0)
                {
                    m_arrObjItem[i].m_intEXECUTETYPE_INT = 1;
                }
                //��Ժ��ҩ����
                if (!objRow[i]["OUTGETMEDDAYS_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intOUTGETMEDDAYS_INT = int.Parse(objRow[i]["OUTGETMEDDAYS_INT"].ToString().Trim());
                //���δ���
                if (!objRow[i]["ATTACHTIMES_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intATTACHTIMES_INT = int.Parse(objRow[i]["ATTACHTIMES_INT"].ToString().Trim());
                //��������ID
                m_arrObjItem[i].m_strSAMPLEID_VCHR = Convert.ToString(objRow[i]["SAMPLEID_VCHR"].ToString().Trim());
                //������������
                m_arrObjItem[i].m_strSAMPLEName_VCHR = Convert.ToString(objRow[i]["sample_type_desc_vchr"].ToString().Trim());

                //��鲿λID
                m_arrObjItem[i].m_strPARTID_VCHR = Convert.ToString(objRow[i]["PARTID_VCHR"].ToString().Trim());
                //��鲿λ����
                m_arrObjItem[i].m_strPARTNAME_VCHR = Convert.ToString(objRow[i]["partname"].ToString().Trim());
                //ҽ��
                m_arrObjItem[i].m_strMedicareTypeName = Convert.ToString(objRow[i]["typename_vchr"].ToString().Trim());
                //ҽ������
                m_arrObjItem[i].m_strOrderDicCateID = Convert.ToString(objRow[i]["ordercateid_chr"].ToString().Trim());
                //ҽ����������
                m_arrObjItem[i].m_strOrderDicCateName = Convert.ToString(objRow[i]["viewname_vchr"].ToString().Trim());
                //����	���ڱ�����ʾ
                if (!objRow[i]["RECIPENO_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_intRecipenNo = int.Parse(objRow[i]["RECIPENO_INT"].ToString().Trim());
                //���ñ�־	{1=ȫ�շѣ�2=�÷��շѣ�3=���շ�}
                if (!objRow[i]["RATETYPE_INT"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].RateType = int.Parse(objRow[i]["RATETYPE_INT"].ToString().Trim());
                //����
                if (!objRow[i]["ItemPrice"].ToString().Trim().Equals(""))
                    m_arrObjItem[i].m_dmlPrice = decimal.Parse(objRow[i]["ItemPrice"].ToString().Trim());
                //Ƥ��
                if (!objRow[i]["ISNEEDFEEL"].ToString().Trim().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intISNEEDFEEL = int.Parse(objRow[i]["ISNEEDFEEL"].ToString().Trim());
                }
                // ��һ�ε�����(��������Ŀ����)
                decimal.TryParse(objRow[i]["SINGLEAMOUNT_DEC"].ToString().Trim(), out m_arrObjItem[i].m_dmlOneUse);
                if (!objRow[i]["stopStatus"].ToString().Trim().Equals(""))
                {
                    m_arrObjItem[i].m_intOrderDicStop = int.Parse(objRow[i]["stopStatus"].ToString().Trim());
                }
                if (!objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim().Equals("") && objRow[i]["ITEMSRCTYPE_INT"].ToString().Trim().Equals("1"))
                {
                    m_arrObjItem[i].m_intIPNOQTYFLAG_INT = int.Parse(objRow[i]["IPNOQTYFLAG_INT"].ToString().Trim());
                }
                //��װ��
                if (objRow[i]["packqty_dec"] != DBNull.Value)
                {
                    m_arrObjItem[i].m_dmlPACKQTY_DEC = decimal.Parse(objRow[i]["packqty_dec"].ToString().Trim());
                }

                //סԺ��λ����
                if (objRow[i]["ipchargeflg_int"] != DBNull.Value)
                {
                    m_arrObjItem[i].m_intIPCHARGEFLG_INT = int.Parse(objRow[i]["ipchargeflg_int"].ToString().Trim());

                }
            }
            // m_objGroupVo.m_objGroupVoList = m_arrObjItem;
        }

        /// <summary>
        /// ����Ŀ�б�����
        /// </summary>
        private void ListViewbind(string m_strGroupID)
        {
            m_objViewer.m_dtvGroupDetail.Rows.Clear();
            DataView myDataView = new DataView(m_dtOrderGroupDetail);
            clsT_aid_bih_ordergroup_detail_VO[] m_arrObjItem;
            GetGroupDetailData(myDataView, out m_arrObjItem);
            for (int i = 0; i < m_arrObjItem.Length; i++)
            {
                clsT_aid_bih_ordergroup_detail_VO m_ObjItem = (clsT_aid_bih_ordergroup_detail_VO)m_arrObjItem[i];
                //ҽ������
                clsT_aid_bih_ordercate_VO p_objItem = (clsT_aid_bih_ordercate_VO)this.m_objViewer.m_htOrderCate[m_ObjItem.m_strOrderDicCateID];
                //ת������
                //���ñ�����ˮ��
                clsBIHOrder order = new clsBIHOrder();
                order.m_strOrderID = m_ObjItem.m_strDETAILID_CHR;
                order.m_strOrderDicID = m_ObjItem.m_strORDERDICID_CHR;
                order.m_strOrderDicCateID = m_ObjItem.m_strOrderDicCateID;
                order.m_strOrderDicCateName = m_ObjItem.m_strOrderDicCateName;
                order.m_strRegisterID = null;

                order.m_intRecipenNo = m_ObjItem.m_intRecipenNo;
                if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))//����ҽ��ʱ��ʾ��Ŀ�������������ʾ������Ŀ��
                {
                    //��Ŀ����
                    order.m_strName = m_ObjItem.m_strOrderName;

                }
                else
                {
                    order.m_strName = m_ObjItem.m_strOrderdicName;
                }
                order.m_strSpec = m_ObjItem.m_strItemspec_vchr;
                order.m_strExecFreqID = m_ObjItem.m_strFREQID_CHR;
                order.m_strExecFreqName = m_ObjItem.m_strExecFreqName;
                order.m_dmlDosage = decimal.Parse(m_ObjItem.m_fltDOSAGE_DEC.ToString());

                order.m_strDosageUnit = m_ObjItem.m_strDOSAGEUNIT_CHR;
                order.m_dmlGet = decimal.Parse(m_ObjItem.m_fltGET_DEC.ToString());

                order.m_strGetunit = m_ObjItem.m_strGETUNIT_CHR;
                order.m_dmlUse = decimal.Parse(m_ObjItem.m_fltUSE_DEC.ToString());
                order.m_strUseunit = m_ObjItem.m_strUSEUNIT_CHR;
                order.m_dmlDosageRate = order.m_dmlUse;
                order.m_strDosetypeID = m_ObjItem.m_strDOSETYPE_CHR;
                order.m_strDosetypeName = m_ObjItem.m_strDosetypeName;
                order.m_strEntrust = m_ObjItem.m_strENTRUST_VCHR;
                order.m_dmlPrice = m_ObjItem.m_dmlPrice;
                order.m_intIsRich = m_ObjItem.m_intISRICH_INT;
                order.RateType = m_ObjItem.RateType;
                order.m_strSAMPLEID_VCHR = m_ObjItem.m_strSAMPLEID_VCHR;
                order.m_strPARTID_VCHR = m_ObjItem.m_strPARTID_VCHR;
                order.m_intExecuteType = m_ObjItem.m_intEXECUTETYPE_INT;
                order.m_intOUTGETMEDDAYS_INT = m_ObjItem.m_intOUTGETMEDDAYS_INT;
                order.m_intATTACHTIMES_INT = m_ObjItem.m_intATTACHTIMES_INT;
                order.m_strSAMPLEID_VCHR = m_ObjItem.m_strSAMPLEID_VCHR;
                order.m_strSAMPLEName_VCHR = m_ObjItem.m_strSAMPLEName_VCHR;
                order.m_strPARTID_VCHR = m_ObjItem.m_strPARTID_VCHR;
                order.m_strPARTNAME_VCHR = m_ObjItem.m_strPARTNAME_VCHR;
                order.m_intISNEEDFEEL = m_ObjItem.m_intISNEEDFEEL;
                order.m_dmlOneUse = m_ObjItem.m_dmlOneUse;
                // �ѵ�ǰ����id����ҽ����Ŀ��
                order.m_strGroupId = m_strGroupID.Trim();
                // �жϵ�ǰҽ���Ƿ�Ϊ��ҽ��
                order.m_intIFPARENTID_INT = m_ObjItem.m_intIFPARENTID_INT;
                order.m_intIPCHARGEFLG_INT = m_ObjItem.m_intIPCHARGEFLG_INT;
                order.m_dmlPACKQTY_DEC = m_ObjItem.m_dmlPACKQTY_DEC;
                if (this.m_objViewer.m_strOrderCateList.Contains(m_ObjItem.m_strOrderDicCateID.ToString().Trim()))
                {
                    continue;
                }
                if (order.m_strName.Trim().Equals(""))
                {
                    continue;
                }
                m_objViewer.m_dtvGroupDetail.Rows.Add();

                DataGridViewRow objRow = m_objViewer.m_dtvGroupDetail.Rows[this.m_objViewer.m_dtvGroupDetail.RowCount - 1];
                objRow.Cells["dtv_Check"].Tag = "1";
                if (this.m_objViewer.m_blDeableMedControl == false)//ȱҩ����Ŀ�Ƿ����¼��0-������false��1-����true
                {
                    if (m_ObjItem.m_intIPNOQTYFLAG_INT == 1)
                    {
                        objRow.Cells["dtv_Check"].Tag = "0";
                        objRow.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                    }
                }
                if (this.m_objViewer.m_blStopControl == false)//ͣ����Ŀ�Ƿ��¼�뿪��
                {
                    if (m_ObjItem.m_intOrderDicStop == 0)
                    {
                        objRow.Cells["dtv_Check"].Tag = "0";
                        objRow.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
                    }
                }
                //ѡ��
                if (this.m_objViewer.ra_selectAll.Checked && objRow.Cells["dtv_Check"].Tag.ToString() != "0")
                {
                    objRow.Cells["dtv_Check"].Value = "1";
                }
                else
                {
                    objRow.Cells["dtv_Check"].Value = "0";
                }
                //��
                objRow.Cells["dtv_DetalNo"].Value = i + 1;
                //��
                objRow.Cells["dtv_RecipeNo"].Value = m_ObjItem.m_intRecipenNo.ToString();
                // ҩƷ��Դ: 0 ҩ��(ȫ�Ʒ�,��ҩ); 1 �����Ա�(ֻ�շ��÷�������Ŀ,����ҩ); 2 ���һ���(ȫ�Ʒѣ�����ҩ)
                switch (m_ObjItem.RateType)
                {
                    case 0:
                        objRow.Cells["RATETYPE_INT"].Value = "ҩ��";
                        break;
                    case 1:
                        objRow.Cells["RATETYPE_INT"].Value = "�Ա�";
                        break;
                    case 2:
                        objRow.Cells["RATETYPE_INT"].Value = "����";
                        break;
                    default:
                        break;
                }
                //����
                if (m_ObjItem.m_intEXECUTETYPE_INT == 1)
                {
                    objRow.Cells["dtv_ExecuteType"].Value = "����";

                }
                else if (m_ObjItem.m_intEXECUTETYPE_INT == 2)
                {
                    objRow.Cells["dtv_ExecuteType"].Value = "��ʱ";

                }
                else if (m_ObjItem.m_intEXECUTETYPE_INT == 3)
                {
                    objRow.Cells["dtv_ExecuteType"].Value = "��Ժ��ҩ";

                }
                else
                {
                    objRow.Cells["dtv_ExecuteType"].Value = "";
                }
                //ҽ��
                objRow.Cells["MedicareTypeName"].Value = m_ObjItem.m_strMedicareTypeName;
                //��Ŀ����
                objRow.Cells["dtv_Name"].Value = m_ObjItem.m_strOrderdicName;

                if (p_objItem != null && !p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))
                {
                    //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
                    if (!m_ObjItem.m_strPARTID_VCHR.Trim().Equals(""))
                    {
                        objRow.Cells["dtv_method"].Value = m_ObjItem.m_strPARTNAME_VCHR;
                    }
                    else if (!m_ObjItem.m_strSAMPLEID_VCHR.Trim().Equals(""))
                    {
                        objRow.Cells["dtv_method"].Value = m_ObjItem.m_strSAMPLEName_VCHR;
                    }

                    //����
                    objRow.Cells["dtv_Dosage"].Value = m_ObjItem.m_fltDOSAGE_DEC + m_ObjItem.m_strDOSAGEUNIT_CHR;
                    //�÷�
                    objRow.Cells["dtv_UseType"].Value = m_ObjItem.m_strDosetypeName;
                    //Ƶ��
                    objRow.Cells["dtv_Freq"].Value = m_ObjItem.m_strExecFreqName;
                    //����
                    if (m_ObjItem.m_fltGET_DEC > 0)
                    {
                        objRow.Cells["dtv_Get"].Value = m_ObjItem.m_fltGET_DEC + m_ObjItem.m_strGETUNIT_CHR;
                    }
                    else
                    {
                        objRow.Cells["dtv_Get"].Value = "";
                    }
                    //����  N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
                    if (m_ObjItem.m_intEXECUTETYPE_INT == 3)
                    {
                        objRow.Cells["dtv_sum"].Value = m_ObjItem.m_intOUTGETMEDDAYS_INT.ToString() + "�칲" + Convert.ToString(m_ObjItem.m_fltGET_DEC * m_ObjItem.m_intOUTGETMEDDAYS_INT) + m_ObjItem.m_strGETUNIT_CHR;
                    }
                    else
                    {
                        objRow.Cells["dtv_sum"].Value = "";
                    }
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = m_ObjItem.m_intATTACHTIMES_INT;
                    //����
                    objRow.Cells["dtv_ENTRUST"].Value = m_ObjItem.m_strENTRUST_VCHR;
                }
                else
                {
                    //���������С�������ʾ����ҽ�����������ͣ��ͼ��ҽ���Ĳ�λ��Ϣ
                    objRow.Cells["dtv_method"].Value = "";
                    //����
                    objRow.Cells["dtv_Dosage"].Value = "";
                    //�÷�
                    objRow.Cells["dtv_UseType"].Value = "";
                    //Ƶ��
                    objRow.Cells["dtv_Freq"].Value = "";
                    objRow.Cells["dtv_Get"].Value = "";
                    //����  N�칲MƬ��N-��ʾ��Ժ��ҩ��������M-��ʾ��Ժ��ҩ�ϼƵ�����
                    objRow.Cells["dtv_sum"].Value = "";
                    //����
                    objRow.Cells["ATTACHTIMES_INT"].Value = "";
                    //����
                    objRow.Cells["dtv_ENTRUST"].Value = "";
                }
                if (p_objItem != null && p_objItem.m_strVIEWNAME_VCHR.Trim().Equals("����ҽ��"))//����ҽ��ʱ��ʾ��Ŀ�������������ʾ������Ŀ��
                {
                    //��Ŀ����
                    objRow.Cells["dtv_Name"].Value = m_ObjItem.m_strOrderName;
                }

                objRow.Tag = order;
            }

            if (this.m_objViewer.m_dtvGroupDetail.RowCount > 0)
            {
                this.m_objViewer.m_dtvGroupDetail.CurrentCell = this.m_objViewer.m_dtvGroupDetail.Rows[0].Cells[2];
            }

        }

        internal void m_lngBindChargeListByGroupDetail()
        {
            //lngRes=m_objManage.m_lngGetOrderGroupDetailByGroupID(m_strGroupID, out m_dtOrderGroupDetail);

            /**********��ǰ��������ѡ����ú�,����ԭ֮ǰ��ѡ��״̬*************/
            if (m_objViewer.m_dtvGroupDetail.SelectedRows.Count > 0)
            {
                List<clsORDERCHARGEDEPT_VO> m_arrChargeList = new List<clsORDERCHARGEDEPT_VO>();
                clsBIHOrder order = (clsBIHOrder)m_objViewer.m_dtvGroupDetail.SelectedRows[0].Tag;
                m_objManage.m_lngGetChargeListByGroupDetail(order, out m_arrChargeList);
                ListViewDetailBind(m_arrChargeList.ToArray());
            }


        }

        private void ListViewDetailBind(clsORDERCHARGEDEPT_VO[] m_arrDEPT_VO)
        {
            this.m_objViewer.m_dtvChangeList.Rows.Clear();
            if (m_arrDEPT_VO == null)
            {
                return;
            }
            decimal m_decSum = 0;
            for (int i = 0; i < m_arrDEPT_VO.Length; i++)
            {
                clsORDERCHARGEDEPT_VO m_objDEPTVO = m_arrDEPT_VO[i];
                this.m_objViewer.m_dtvChangeList.Rows.Add();
                DataGridViewRow row1 = this.m_objViewer.m_dtvChangeList.Rows[this.m_objViewer.m_dtvChangeList.RowCount - 1];
                row1.Cells["dtv_NO"].Value = Convert.ToString(i + 1);//��
                row1.Cells["dtv_ItemName"].Value = m_objDEPTVO.m_strChargeitemname_chr;//�շ���
                row1.Cells["dtv_ChargeClass"].Value = "";//�������
                switch (m_objDEPTVO.m_intFLAG_INT)
                {
                    case 0:
                        row1.Cells["dtv_ChargeClass"].Value = "����Ŀ";
                        break;
                    case 1:
                        row1.Cells["dtv_ChargeClass"].Value = "������Ŀ";
                        break;
                    case 2:
                        row1.Cells["dtv_ChargeClass"].Value = "�÷�����";
                        break;
                    case 3:
                        row1.Cells["dtv_ChargeClass"].Value = "����¼����Ŀ";
                        break;
                }
                row1.Cells["dtv_ITEMSPEC_VCHR"].Value = m_objDEPTVO.m_strSpec_vchr;// ���
                row1.Cells["dtv_MinPrice"].Value = m_objDEPTVO.m_decUnitprice_dec;//����
                row1.Cells["dtv_QTY_INT"].Value = m_objDEPTVO.m_decAmount_dec;// ����
                row1.Cells["dtv_priceAdd"].Value = m_objDEPTVO.m_decUnitprice_dec * m_objDEPTVO.m_decAmount_dec;// �ϼ�
                m_decSum += m_objDEPTVO.m_decUnitprice_dec * m_objDEPTVO.m_decAmount_dec;
                row1.Tag = m_objDEPTVO;
            }
            this.m_objViewer.m_lblChargeSum.Text = "���úϼ�:" + m_decSum.ToString("0.00");

        }

        internal void LoadTheGroup(string m_strFindCode, string m_strEmpId, string m_strInpatientAreaID, int m_intClass, out clsBIHOrderGroup[] arrGroup)
        {
            long ret1 = m_objManage.m_lngFindGroup(m_strFindCode, m_strEmpId, m_strInpatientAreaID, m_intClass, out arrGroup);
        }

        //ԭ��ʹ��ArrayList�Ĵ���
        //internal long GetTheHisControl(ArrayList m_arrControl, out DataTable dtbResult)
        //{
        //    long lngRes = m_objManage.GetTheHisControl(m_arrControl, out  dtbResult);
        //    return lngRes;
        //}
        //ʹ��List<T>����
        internal long GetTheHisControl(System.Collections.Generic.List<string> m_arrControl, out DataTable dtbResult)
        {
            long lngRes = m_objManage.GetTheHisControl(m_arrControl, out dtbResult);
            return lngRes;
        }
        internal void GetTheSelectOrderDic(ref ArrayList m_arrOrderDic, ref DataTable m_dtOrderDic)
        {
            m_arrOrderDic = new ArrayList();
            m_dtOrderDic = new DataTable();
            m_dtOrderDic.Columns.Clear();
            m_dtOrderDic.Columns.Add("OrderDicID");
            m_dtOrderDic.Columns.Add("OrderDicCateID");
            if (this.m_objViewer.m_dtvGroupDetail.RowCount > 0)
            {

                for (int i = 0; i < this.m_objViewer.m_dtvGroupDetail.RowCount; i++)
                {
                    //clsBIHOrder order = (clsBIHOrder)this.m_objViewer.listView2.CheckedItems[i].Tag;
                    if (this.m_objViewer.m_dtvGroupDetail.Rows[i].Cells["dtv_Check"].Value.ToString().Trim().Equals("0"))
                    {
                        continue;
                    }
                    clsBIHOrder order = (clsBIHOrder)this.m_objViewer.m_dtvGroupDetail.Rows[i].Tag;
                    m_arrOrderDic.Add(order.m_strOrderDicID);
                    DataRow row1 = m_dtOrderDic.NewRow();
                    row1["OrderDicID"] = order.m_strOrderDicID;
                    row1["OrderDicCateID"] = order.m_strOrderDicCateID;
                    m_dtOrderDic.Rows.Add(row1);

                }

            }

        }

        internal void LoadTheCate(ref Hashtable m_htOrderCate)
        {
            long lngRes = 0;
            clsT_aid_bih_ordercate_VO[] p_objItemArr = null;
            lngRes = m_objManage.m_lngGetAidOrderCate(out p_objItemArr);
            m_htOrderCate.Clear();
            for (int i = 0; i < p_objItemArr.Length; i++)
            {
                if (!m_htOrderCate.Contains(p_objItemArr[i].m_strORDERCATEID_CHR))
                {
                    m_htOrderCate.Add(p_objItemArr[i].m_strORDERCATEID_CHR, p_objItemArr[i]);
                }
            }
        }
    }
}
