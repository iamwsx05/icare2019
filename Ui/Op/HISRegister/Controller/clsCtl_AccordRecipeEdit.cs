using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Э������ά���߼���
    /// </summary>
    public class clsCtl_AccordRecipeEdit : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����

        #region ��ҩ�ó���
        /// <summary>
        /// ����
        /// </summary>
        internal int c_GroupNo = 0;
        /// <summary>
        /// ��ѯ
        /// </summary>
        internal int c_Find = 1;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Count = 2;
        /// <summary>
        /// ��λ
        /// </summary>
        internal int c_Unit = 3;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Name = 4;
        /// <summary>
        /// ���
        /// </summary>
        internal int c_Spec = 5;
        /// <summary>
        /// �÷�����
        /// </summary>
        internal int c_UsageName = 6;
        /// <summary>
        /// Ƶ������
        /// </summary>
        internal int c_FreName = 7;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Day = 8;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Price = 9;
        /// <summary>
        /// �ܼ�
        /// </summary>
        internal int c_SumMoney = 10;
        /// <summary>
        /// ��ĿID
        /// </summary>
        internal int c_ItemID = 11;
        /// <summary>
        /// ��װ��
        /// </summary>
        internal int c_Packet = 12;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Total = 13;
        /// <summary>
        /// Ƶ������
        /// </summary>
        internal int c_FreDays = 14;
        /// <summary>
        /// Ƶ�ʴ���
        /// </summary>
        internal int c_FreTimes = 15;
        /// <summary>
        /// �÷�ID
        /// </summary>
        internal int c_UsageID = 16;
        /// <summary>
        /// Ƶ��ID
        /// </summary>
        internal int c_FreID = 17;
        /// <summary>
        /// ��λ
        /// </summary>

        internal int c_BigUnit = 18;
        /// <summary>
        /// �к�
        /// </summary>
        internal int c_RowNo = 19;
        /// <summary>
        /// ������
        /// </summary>
        internal int c_DiscountName = 20;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Discount = 21;
        /// <summary>
        /// ��С��λ���
        /// </summary>
        internal int c_UnitFlag = 22;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_Dosage = 23;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_MaxLimit = 24;
        /// <summary>
        /// ����
        /// </summary>
        internal int c_MinLimit = 25;
        /// <summary>
        /// �Ƿ����
        /// </summary>
        internal int c_IsCal = 26;
        /// <summary>
        /// ��Ʊ����
        /// </summary>
        internal int c_InvoiceType = 27;
        /// <summary>
        /// ������ĿID
        /// </summary>
        internal int c_SubItemID = 28;
        /// <summary>
        /// ���־
        /// </summary>
        internal int c_IsMain = 29;
        /// <summary>
        /// ������Ŀԭ����
        /// </summary>
        internal int c_PreCount = 30;
        /// <summary>
        /// ������Ŀ��־
        /// </summary>
        internal int c_Fage = 31;
        /// <summary>
        /// Ӣ����
        /// </summary>
        internal int c_EnglishName = 32;
        /// <summary>
        /// Ƥ��
        /// </summary>
        internal int c_PS = 33;
        /// <summary>
        /// ��ϸ�÷�
        /// </summary>
        internal int c_UsageDetail = 34;
        /// <summary>
        /// Ƥ�Ա�־ Ƥ�Ա�־ 0--�� 1--��
        /// </summary>
        internal int c_PSFlag = 35;
        /// <summary>
        /// ��������Ŀ
        /// </summary>
        internal int c_resubitem = 36;
        /// <summary>
        /// ����ĿĬ������
        /// </summary>
        internal int c_MainItemNum = 37;
        /// <summary>
        /// �Ʊ�ҩ��־����
        /// </summary>
        internal int c_Deptmed = 38;
        /// <summary>
        /// �Ʊ�ҩ��־ID
        /// </summary>
        internal int c_DeptmedID = 39;

        #endregion

        #region ��ҩ����
        /// <summary>
        /// ��������Ŀ
        /// </summary>
        internal int cm_resubitem = 25;
        /// <summary>
        /// ����ĿĬ������
        /// </summary>
        internal int cm_MainItemNum = 26;
        /// <summary>
        /// �Ʊ�ҩ��־����
        /// </summary>
        internal int cm_Deptmed = 27;
        /// <summary>
        /// �Ʊ�ҩ��־ID
        /// </summary>
        internal int cm_DeptmedID = 28;
        /// <summary>
        /// ��ϸ�÷�
        /// </summary>
        internal int cm_UsageDetail = 29;

        #endregion

        #region ��������
        /// <summary>
        /// ��ѯ0
        /// </summary>
        internal int o_Find = 0;
        /// <summary>
        /// ����1
        /// </summary>
        internal int o_Count = 1;
        /// <summary>
        /// ����2
        /// </summary>
        internal int o_Name = 2;
        /// <summary>
        /// ���3
        /// </summary>
        internal int o_Spec = 3;
        /// <summary>
        /// ��λ4
        /// </summary>
        internal int o_Unit = 4;
        /// <summary>
        /// ����5
        /// </summary>
        internal int o_Price = 5;
        /// <summary>
        /// �ܼ�6
        /// </summary>
        internal int o_SumMoney = 6;
        /// <summary>
        /// ��ĿID7
        /// </summary>
        internal int o_ItemID = 7;
        /// <summary>
        /// �Զ���۸�
        /// </summary>
        internal int o_PriceFlag = 8;
        /// <summary>
        /// �к�9
        /// </summary>
        internal int o_RowNo = 9;
        /// <summary>
        /// ������10
        /// </summary>
        internal int o_DiscountName = 10;
        /// <summary>
        /// ����ֵ11
        /// </summary>
        internal int o_Discount = 11;
        /// <summary>
        /// ��Ʊ����12
        /// </summary>
        internal int o_InvoiceType = 12;
        /// <summary>
        /// ������ĿID13
        /// </summary>
        internal int o_OtherItemID = 13;
        /// <summary>
        /// ��������14
        /// </summary>
        internal int o_OtherCount = 14;
        /// <summary>
        /// Ӣ����15
        /// </summary>
        internal int o_EnglishName = 15;
        /// <summary>
        /// Ԥ��16
        /// </summary>
        internal int o_Temp = 16;
        /// <summary>
        ///  ���뵥ID17
        /// </summary>
        internal int o_ApplyId = 17;
        /// <summary>
        /// ��������Ŀ
        /// </summary>
        internal int o_resubitem = 18;
        /// <summary>
        /// ����ĿĬ������
        /// </summary>
        internal int o_MainItemNum = 19;
        /// <summary>
        /// ���뵥��־
        /// </summary>
        internal int o_appflag = 20;
        /// <summary>
        /// ������ϸ�÷�
        /// </summary>
        internal int o_UsageDetail = 21;

        /// <summary>
        /// ������ϸ�÷�
        /// </summary>
        internal int o_UsageDetail2 = 20;
        /// <summary>
        /// �����Ʊ�ҩ��־����
        /// </summary>
        internal int o_Deptmed = 21;
        /// <summary>
        /// �����Ʊ�ҩ��־ID
        /// </summary>
        internal int o_DeptmedID = 22;
        /// <summary>
        /// �������������÷�ID
        /// </summary>
        internal int o_UsageID = 22;

        #endregion

        #region ������
        /// <summary>
        /// ��ѯ0
        /// </summary>
        internal int t_Find = 0;
        /// <summary>
        /// ����1
        /// </summary>
        internal int t_Count = 1;

        /// <summary>
        /// ����2
        /// </summary>
        internal int t_Name = 2;
        /// <summary>
        /// ���3
        /// </summary>
        internal int t_Spec = 3;
        /// <summary>
        /// ���/����������4
        /// </summary>
        internal int t_PartName = 4;
        /// <summary>
        /// ��λ5
        /// </summary>
        internal int t_Unit = 5;

        /// <summary>
        /// ����6
        /// </summary>
        internal int t_Price = 6;
        /// <summary>
        /// �ܼ�7
        /// </summary>
        internal int t_SumMoney = 7;
        /// <summary>
        /// ��ĿID8
        /// </summary>
        internal int t_ItemID = 8;
        /// <summary>
        /// �Զ���۸�9
        /// </summary>
        internal int t_PriceFlag = 9;
        /// <summary>
        /// �к�10
        /// </summary>
        internal int t_RowNo = 10;
        /// <summary>
        /// ������11
        /// </summary>
        internal int t_DiscountName = 11;
        /// <summary>
        /// ����ֵ12
        /// </summary>
        internal int t_Discount = 12;
        /// <summary>
        /// ��Ʊ����13
        /// </summary>
        internal int t_InvoiceType = 13;
        /// <summary>
        /// ������ĿID14
        /// </summary>
        internal int t_OtherItemID = 14;
        /// <summary>
        /// ��������15
        /// </summary>
        internal int t_OtherCount = 15;
        /// <summary>
        /// Ӣ����16
        /// </summary>
        internal int t_EnglishName = 16;
        /// <summary>
        /// ����ID17
        /// </summary>
        internal int t_Temp = 17;
        /// <summary>
        ///  ���뵥ID18
        /// </summary>
        internal int t_ApplyId = 18;
        /// <summary>
        /// ��������Ŀ
        /// </summary>
        internal int t_resubitem = 19;
        /// <summary>
        /// ����ĿĬ������
        /// </summary>
        internal int t_MainItemNum = 20;
        /// <summary>
        /// �����־����
        /// </summary>
        internal int t_quick = 21;
        /// <summary>
        /// �����־ID
        /// </summary>
        internal int t_quickid = 22;
        /// <summary>
        /// ������ϸ�÷�
        /// </summary>
        internal int t_UsageDetail = 23;
        /// <summary>
        /// �����ϸ�÷�
        /// </summary>
        internal int t_UsageDetail2 = 21;
        /// <summary>
        /// ��������÷�ID
        /// </summary>
        internal int t_UsageID = 22;
        /// <summary>
        /// ����������Ŀ������ĿID
        /// </summary>
        internal int t_lis_orderitem = 24;
        /// <summary>
        /// ����������Ŀ������ĿĬ������
        /// </summary>
        internal int t_lis_ordernum = 25;
        /// <summary>
        /// ����������Ŀ���۱���
        /// </summary>
        internal int t_lis_discount = 26;
        /// <summary>
        /// ���������Ŀ������ĿID
        /// </summary>
        internal int t_test_orderitem = 23;
        /// <summary>
        /// ���������Ŀ������ĿĬ������
        /// </summary>
        internal int t_test_ordernum = 24;
        /// <summary>
        /// ��������������Ŀ������ĿID
        /// </summary>
        internal int t_ops_orderitem = 23;
        /// <summary>
        /// ��������������Ŀ������ĿĬ������
        /// </summary>
        internal int t_ops_ordernum = 24;
        #endregion

        /// <summary>
        /// DoctorWorkstation-DoMain��
        /// </summary>
        private clsDcl_DoctorWorkstation objSvc;
        /// <summary>
        /// �ж����ּ��㷽ʽ true ����,false
        /// </summary>
        private bool CalFlag = true;
        /// <summary>
        /// ȫ�ֱ���,��¼datagrid��CellChange����ʱ�ĵ�λ��
        /// </summary>
        private int colNo = 0;
        /// <summary>
        /// ȫ�ֱ���,��¼datagrid��CellChange����ʱ�ĵ�λ��
        /// </summary>
        private int rowNo = 0;
        /// <summary>
        /// Ƭ�����㷽��
        /// </summary>
        private bool Trochecalc = false;
        /// <summary>
        /// ��תҳʱ��ת��
        /// </summary>
        public bool b_IndexChangeFlag = true;
        /// <summary>
        /// ����Ȩ��(0 �� 1 ��)
        /// </summary>
        internal string Recpur = "0";
        /// <summary>
        /// ����ҩ��Ȩ��(0 �� 1 ��)
        /// </summary>
        private string Neurpur = "0";
        /// <summary>
        /// ����ҩȨ��(0 �� 1 ��)
        /// </summary>
        private string Drugpur = "0";
        /// <summary>
        /// �ж��Ƿ���ʾȱҩ true ��ʾ,false����ʾ
        /// </summary>
        internal bool isShowLackMedicine = true;
        /// <summary>
        /// ������ҩ����ʾ��Ϣ
        /// </summary>
        private string Neurpurhintinfo = "��ҩƷΪ����ҩ�\r\n\r\n��һ��(��������)���ܳ���3�죬�벹��д������\r\n\r\n�ڶ��ࣨ�簲���ࣩ���ܳ���7�죬�������������14�죨��ע�����ɣ���";
        /// <summary>
        /// ����ҩ����ʾ��Ϣ
        /// </summary>
        private string Drugpurhintinfo = "��ҩƷΪ����ҩ��벹��д������\r\n\r\n����ҩƷʹ����֪��\r\n\r\n  ��ע���ÿ�Ŵ���һ����������Ժ��ע�䣩��\r\n\r\n  �ڿػ����Ƽ����ܳ���7�죻\r\n\r\n  ���������Ͳ��ܳ���3�졣";

        private List<string> OrderCatLisArr = new List<string>();
        private List<string> OrderCatTestArr = new List<string>();
        private List<string> OrderCatOpsArr = new List<string>();

        Dictionary<string, bool> dicCheckItem = new Dictionary<string, bool>();
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public clsCtl_AccordRecipeEdit()
        {
            objSvc = new clsDcl_DoctorWorkstation();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ����
        /// </summary>
        public com.digitalwave.iCare.gui.HIS.frmAccordRecipeEdit m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmAccordRecipeEdit)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInit()
        {
            CalFlag = objSvc.m_mthIsCanDo("0015");
            Trochecalc = objSvc.m_mthIsCanDo("0054");
            isShowLackMedicine = objSvc.m_mthIsCanDo("0012");

            OrderCatLisArr = this.m_Gettoken(this.objSvc.m_strGetSysparm("0010"), ";");
            OrderCatTestArr = this.m_Gettoken(this.objSvc.m_strGetSysparm("0011"), ";");
            OrderCatOpsArr = this.m_Gettoken(this.objSvc.m_strGetSysparm("0012"), ";");

            this.m_mthGetmedpurview();
            this.objSvc.m_mthGetmedpurview(this.m_objViewer.LoginInfo.m_strEmpID, out Neurpur, out Drugpur, out Recpur);

            //��ȡ��Ʊ���Ͷ�Ӧ�����ݷ�Ʊ��������Ӧ��
            this.m_mthLoadCat();

            //��DataGrid�Ļس���תΪ�ո����
            this.m_mthSetDataGridEnterToSpace();

            this.m_objViewer.ctlDataGrid1.Controls.Add(this.m_objViewer.listView2);
            this.m_objViewer.ctlDataGrid1.Controls.Add(this.m_objViewer.listView3);
            this.m_objViewer.ctlDataGrid2.Controls.Add(this.m_objViewer.listView4);
            this.m_objViewer.listView5.Leave += new EventHandler(listView5_Leave);
            this.m_objViewer.listView5.DoubleClick += new EventHandler(listView5_DoubleClick);
            this.m_objViewer.listView5.KeyDown += new KeyEventHandler(listView5_KeyDown);

            this.m_mthNew();
        }
        #endregion

        #region ���龫��ҩƷ
        /// <summary>
        /// ���龫��ҩƷȨ�� 0-�Ӷ���ҩΪ��ͨҩ���� 1-���޶���Ȩ�޵�ҽ����ҩʱֻ����ʾ 2-�ϸ����ƣ���Ȩ�޵Ĳ��ܿ�����Ȩ�޵Ŀ�ҩʱ��¼�벡�����֤�š�����ҩ����Էִ�����
        /// </summary>
        private int Medpurview = 0;
        /// <summary>
        /// ��ȡ���龫��ҩƷȨ��
        /// </summary>
        private void m_mthGetmedpurview()
        {
            DataTable dt;
            long ret = objSvc.m_lngGetWSParm("0049", out dt);		//0049 ���龫��ҩƷȨ��

            if (ret > 0 && dt.Rows.Count > 0)
            {
                Medpurview = Convert.ToInt32(dt.Rows[0]["SETSTATUS_INT"]);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthCreateTree(string EmpID)
        {
            DataTable dt;

            try
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                long l = this.objSvc.m_lngFindAccordRecipe(EmpID, out dt);
                if (l > 0)
                {
                    this.m_objViewer.tv.BeginUpdate();
                    this.m_objViewer.tv.Nodes.Clear();

                    DataView DV = new DataView(dt);
                    DV.Sort = "recipename_chr asc";

                    ArrayList RecArr = new ArrayList();
                    DataRow dr = null;
                    for (int i = 0; i < DV.Count; i++)
                    {
                        dr = DV[i].Row;

                        AccordRecipeEdit objAR = new AccordRecipeEdit();
                        objAR.RecipeID_Chr = dr["recipeid_chr"].ToString().Trim();
                        objAR.Recipename_Chr = dr["recipename_chr"].ToString().Trim();
                        objAR.UserCode_Chr = dr["usercode_chr"].ToString().Trim();
                        objAR.PyCode_Chr = dr["pycode_chr"].ToString().Trim();
                        objAR.WbCode_Chr = dr["wbcode_chr"].ToString().Trim();
                        objAR.Privilege_Int = dr["privilege_int"].ToString();
                        objAR.Status_Int = dr["status_int"].ToString();
                        objAR.ReMark_Vchr = dr["diseasename_vchr"].ToString();
                        objAR.OrigeName = dr["recipename_chr"].ToString().Trim();
                        RecArr.Add(objAR);
                    }

                    this.m_objViewer.tv.Nodes.Add("Э������ģ���б�");
                    TreeNode FirstNode = null;
                    TreeNode FindNode = this.m_objViewer.tv.Nodes[0];
                    FindNode.Tag = "root";
                    FindNode.Name = "root";
                    FindNode.ImageIndex = 1;
                    FindNode.SelectedImageIndex = 1;

                    int count = 0;
                    bool Exists = false;

                    foreach (AccordRecipeEdit objRecipe in RecArr)
                    {
                        FindNode = this.m_objViewer.tv.Nodes[0];

                        string[] SplitArr = objRecipe.LevelNameArr;

                        for (int i = 0; i < SplitArr.Length; i++)
                        {
                            Exists = false;
                            for (int j = 0; j < FindNode.Nodes.Count; j++)
                            {
                                if (SplitArr[i] == FindNode.Nodes[j].Text)
                                {
                                    Exists = true;
                                    FindNode = FindNode.Nodes[j];
                                    break;
                                }
                            }

                            if (Exists)
                            {
                                if (i == SplitArr.Length - 1)
                                {
                                    TreeNode tnAdd = new TreeNode(SplitArr[i].Trim());
                                    FindNode.Parent.Nodes.Add(tnAdd);
                                    tnAdd.Tag = objRecipe;
                                    tnAdd.ImageIndex = 0;
                                    tnAdd.SelectedImageIndex = 5;
                                    tnAdd.Name = "child->" + SplitArr[i].Trim();
                                }
                            }
                            else
                            {
                                TreeNode tnAdd = new TreeNode(SplitArr[i].Trim());
                                FindNode.Nodes.Add(tnAdd);
                                if (i == SplitArr.Length - 1)
                                {
                                    tnAdd.Tag = objRecipe;
                                    tnAdd.ImageIndex = 0;
                                    tnAdd.SelectedImageIndex = 5;
                                    tnAdd.Name = "child->" + SplitArr[i].Trim();
                                }
                                else
                                {
                                    tnAdd.ImageIndex = 2;
                                    tnAdd.SelectedImageIndex = 5;
                                    tnAdd.Name = "parent->" + SplitArr[i].Trim();
                                }
                                if (count == 0)
                                {
                                    FirstNode = tnAdd;
                                    count++;
                                }

                                FindNode = tnAdd;
                            }
                        }
                    }

                    this.m_objViewer.tv.EndUpdate();
                    this.m_objViewer.tv.SelectedNode = FirstNode;
                }
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="FindStr"></param>
        /// <param name="ChildFlag"></param>
        public void m_mthFindTree(string FindStr, bool ChildFlag)
        {
            bool FindFlag = false;
            TreeNodeCollection nodes = this.m_objViewer.tv.Nodes;
            foreach (TreeNode n in nodes)
            {
                //�ݹ�������ڵ�
                FindRecursive(n, FindStr.ToLower(), ChildFlag, ref FindFlag);
            }
        }

        /// <summary>
        /// �ݹ�������ڵ�
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="FindStr"></param>
        /// <param name="ChildFlag"></param>
        /// <param name="FindFlag"></param>
        private void FindRecursive(TreeNode treeNode, string FindStr, bool ChildFlag, ref bool FindFlag)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                //if (tn.Tag == null)
                //{
                //    continue;
                //}

                if (FindFlag)
                {
                    return;
                }

                if (ChildFlag)
                {
                    if (tn.Name.ToString().ToLower().StartsWith("child"))
                    {
                        AccordRecipeEdit objAR = tn.Tag as AccordRecipeEdit;
                        if (objAR.UserCode_Chr.ToLower().StartsWith(FindStr) || objAR.PyCode_Chr.ToLower().StartsWith(FindStr) ||
                            objAR.WbCode_Chr.ToLower().StartsWith(FindStr) || objAR.Recipename_Chr.ToLower().StartsWith(FindStr))
                        {
                            if (!tn.IsSelected)
                            {
                                this.m_objViewer.tv.SelectedNode = tn;
                            }

                            if (!tn.IsExpanded)
                            {
                                tn.Expand();
                            }

                            FindFlag = true;

                            return;
                        }
                        else
                        {
                            FindRecursive(tn, FindStr, ChildFlag, ref FindFlag);
                        }
                    }
                }
                else
                {
                    string tnName = tn.Name.ToString().ToLower().Replace("parent->", "").Replace("child->", "");
                    if (tnName.StartsWith(FindStr))
                    {
                        if (!tn.IsSelected)
                        {
                            this.m_objViewer.tv.SelectedNode = tn;
                        }

                        if (!tn.IsExpanded)
                        {
                            tn.Expand();
                        }

                        FindFlag = true;

                        return;
                    }
                    else
                    {
                        FindRecursive(tn, FindStr, ChildFlag, ref FindFlag);
                    }
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="RecID"></param>
        public void m_mthFindChild(string RecID)
        {
            bool FindFlag = false;
            TreeNodeCollection nodes = this.m_objViewer.tv.Nodes;
            foreach (TreeNode n in nodes)
            {
                //�ݹ�������ڵ�
                FindRecursiveChild(n, RecID, ref FindFlag);
            }
        }

        /// <summary>
        /// �ݹ�������ڵ�
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="RecID"></param>
        /// <param name="FindFlag"></param>
        private void FindRecursiveChild(TreeNode treeNode, string RecID, ref bool FindFlag)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Tag == null)
                {
                    continue;
                }

                if (FindFlag)
                {
                    return;
                }

                if (((AccordRecipeEdit)tn.Tag).RecipeID_Chr == RecID)
                {
                    if (!tn.IsSelected)
                    {
                        this.m_objViewer.tv.SelectedNode = tn;
                    }

                    if (!tn.IsExpanded)
                    {
                        tn.Expand();
                    }

                    FindFlag = true;

                    return;
                }
                else
                {
                    FindRecursiveChild(tn, RecID, ref FindFlag);
                }
            }
        }
        #endregion

        #region �����ܵ�����
        /// <summary>
        /// ��ҩ���������㷽�����������㷨�����Ը�������ѡ���㷨��ʵ���ϵڶ����㷨��������ʵ��
        /// ������һ���㷨��һ��Ҫȷ����ҩ������Ƶ�ʵ������ı���
        /// ��ҩ��λ֪ʶ���簢Ī�������ҩ��һ��12����һ����0.5�ˡ�����������Ĵ�λ����������С��λ��
        /// 12�����İ�װ�����˾��Ǽ�����λ��0.5���Ǽ���������ҽ��������ʱ���Ὺ����������˵���ģ�
        /// ��ֻ��˵1�ˣ�����2������˵����������Ҫ����������ļ�������������������㷨��Ҫ�ִ�λ
        /// �㷨��С��λ�㷨��
        /// ����һ��
        /// ��λ:������*����*Ƶ�ʴ�����/(Ƶ������*��������*��װ��) 
        /// С��λ: ������*����*Ƶ�ʴ�����/(Ƶ������*��������) 
        /// ��������
        /// ��λ:(����/��������*��װ��) ȡ�ȴ��ڻ���ڽ������С�������ٳ�(����/Ƶ������*Ƶ�ʴ���)
        /// С��λ: (����/��������) ȡ�ȴ��ڻ���ڽ������С�������ٳ�(����/Ƶ������*Ƶ�ʴ���)
        ///  ȡ�ȴ��ڻ���ڽ������С����������1.001�͵���2
        /// </summary>
        public void m_mthCalculateAmount()
        {
            rowNo = this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
            colNo = this.m_objViewer.ctlDataGrid1.CurrentCell.ColumnNumber;
            if (b_IndexChangeFlag && this.m_objViewer.ctlDataGrid1[rowNo, c_IsCal].ToString() == "0")
            {
                return;
            }
            b_IndexChangeFlag = true;
            decimal decSumCountTemp = clsConvertToDecimal.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Total]);
            if (this.m_objViewer.ctlDataGrid1[rowNo, c_ItemID].ToString() == "")//���IDΪ���˳�
            {
                return;
            }
            string ItemId = this.m_objViewer.ctlDataGrid1[rowNo, c_ItemID].ToString();
            int TempDay = (int)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreDays]);//Ƶ������
            int Days = (int)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Day]);//����
            if (TempDay != 0 && Days % TempDay != 0)
            {
                MessageBox.Show("���������Ӧ���� " + TempDay.ToString() + " �ı���", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int i = (int)Math.Ceiling(Convert.ToDouble(Days / TempDay));
                colNo = 0;
                this.m_objViewer.ctlDataGrid1[rowNo, c_Day] = TempDay;
                this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(rowNo, c_Day);
            }
            else
            {
                if (TempDay != 0)
                {
                    Days = Days / TempDay;
                }
                else
                {
                    Days = 0;
                }
                decimal packet = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Packet]);
                if (packet == 0)
                {
                    packet = 1;
                }
                decimal d_Usage = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Dosage]);
                if (d_Usage == 0)
                {
                    d_Usage = 1;
                }
                decimal iii = 0;
                decimal price = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Price]);

                #region �������㷽��
                bool b = false;
                if (dicCheckItem.ContainsKey(ItemId))
                {
                    b = dicCheckItem[ItemId];
                }
                else
                {
                    b = objSvc.m_blnCheckmedicament(ItemId);
                    dicCheckItem.Add(ItemId, b);
                }
                if (b)
                {
                    if (Trochecalc)
                    {//����������������
                        decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]);
                        decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]);
                        iii = (decimal)Math.Ceiling((double)(temp2 / (d_Usage)));//ʲôʱ������С��λ������
                        iii = Days * iii * temp1;
                        if (m_objViewer.ctlDataGrid1[rowNo, c_UnitFlag].ToString().Trim() == "0")//����ô�λ������ʱ��ת�ɴ�λ
                        {
                            double dTemp = (double)(iii / packet);
                            iii = (decimal)Math.Ceiling(dTemp);
                        }
                    }
                    else
                    {
                        iii = (decimal)Math.Ceiling((double)(m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]) * Days * m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]) / (packet * d_Usage)));
                        if (m_objViewer.ctlDataGrid1[rowNo, c_UnitFlag].ToString().Trim() == "1")//С��λ
                        {
                            decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]);
                            decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]);
                            decimal temp3 = temp1 * Days * temp2 / d_Usage;
                            double temp4 = Math.Ceiling((double)temp3);
                            iii = decimal.Parse(temp4.ToString());
                        }
                    }
                }
                else
                {
                    if (CalFlag)
                    {//����������������
                        decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]);
                        decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]);
                        iii = (decimal)Math.Ceiling((double)(temp2 / (d_Usage)));//ʲôʱ������С��λ������
                        iii = Days * iii * temp1;
                        if (m_objViewer.ctlDataGrid1[rowNo, c_UnitFlag].ToString().Trim() == "0")//����ô�λ������ʱ��ת�ɴ�λ
                        {
                            double dTemp = (double)(iii / packet);
                            iii = (decimal)Math.Ceiling(dTemp);
                        }
                    }
                    else
                    {
                        iii = (decimal)Math.Ceiling((double)(m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]) * Days * m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]) / (packet * d_Usage)));
                        if (m_objViewer.ctlDataGrid1[rowNo, c_UnitFlag].ToString().Trim() == "1")//С��λ
                        {
                            decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_FreTimes]);
                            decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Count]);
                            decimal temp3 = temp1 * Days * temp2 / d_Usage;
                            double temp4 = Math.Ceiling((double)temp3);
                            iii = decimal.Parse(temp4.ToString());
                        }
                    }
                }
                #endregion

                if (this.m_objViewer.ctlDataGrid1[rowNo, c_IsCal].ToString().Trim() == "1")
                {
                    this.m_objViewer.ctlDataGrid1[rowNo, c_Total] = iii;
                }
                else
                {
                    iii = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[rowNo, c_Total]);
                }
            }
        }

        public void m_mthCalculateAmount(int p_intRow)
        {

            if (this.m_objViewer.ctlDataGrid1[p_intRow, c_ItemID].ToString() == "")//���IDΪ���˳�
            {
                return;
            }
            int TempDay = (int)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_FreDays]);//Ƶ������

            int Days = (int)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Day]);//����


            if (TempDay != 0)
            {
                Days = Days / TempDay;
            }
            else
            {
                Days = 0;
            }
            decimal packet = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Packet]);
            if (packet == 0)
            {
                packet = 1;
            }
            decimal d_Usage = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Dosage]);
            if (d_Usage == 0)
            {
                d_Usage = 1;
            }
            decimal iii = 0;
            decimal price = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Price]);
            if (CalFlag)
            {
                decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_FreTimes]);
                decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Count]);
                iii = (decimal)Math.Ceiling((double)(temp2 / (d_Usage)));//ʲôʱ������С��λ������
                iii = Days * iii * temp1;
                if (m_objViewer.ctlDataGrid1[rowNo, c_UnitFlag].ToString().Trim() == "0")//����ô�λ������ʱ��ת�ɴ�λ
                {
                    double dTemp = (double)(iii / packet);
                    iii = (decimal)Math.Ceiling(dTemp);

                }
            }
            else
            {
                iii = (decimal)Math.Ceiling((double)(m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, 15]) * Days * m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, 2]) / (packet * d_Usage)));
                if (m_objViewer.ctlDataGrid1[p_intRow, c_UnitFlag].ToString().Trim() == "1")//С��λ
                {
                    decimal temp1 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_FreTimes]);
                    decimal temp2 = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Count]);
                    decimal temp3 = temp1 * Days * temp2 / d_Usage;
                    double temp4 = Math.Ceiling((double)temp3);
                    iii = decimal.Parse(temp4.ToString());
                }
            }
            if (this.m_objViewer.ctlDataGrid1[p_intRow, c_IsCal].ToString().Trim() == "1")
            {
                this.m_objViewer.ctlDataGrid1[p_intRow, c_Total] = iii;
            }
            else
            {
                iii = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[p_intRow, c_Total]);
            }
        }

        /// <summary>
        /// ��������(��ҩ)
        /// </summary>
        /// <param name="p_intRow"></param>
        public void m_mthCalculateAmount2(int p_intRow)
        {

            if (this.m_objViewer.ctlDataGrid2[p_intRow, 8].ToString() == "")//���IDΪ���˳�
            {
                return;
            }
            decimal decSumCountTemp = clsConvertToDecimal.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid2[p_intRow, 15]);
            double packet = (double)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid2[p_intRow, 18]);
            if (packet == 0)
            {
                packet = 1;
            }
            double d_Usage = (double)m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid2[p_intRow, 12]);
            if (d_Usage == 0)
            {
                d_Usage = 1;
            }
            double temp = (double)m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid2[p_intRow, 1]);
            decimal iii = (decimal)Math.Ceiling(temp / (packet * d_Usage));
            this.m_objViewer.ctlDataGrid2[p_intRow, 15] = iii;

            decimal price = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid2[p_intRow, 16]);

            if (m_objViewer.ctlDataGrid2[p_intRow, 17].ToString().Trim() == "1")
            {
                iii = (decimal)Math.Ceiling(temp / d_Usage);
                this.m_objViewer.ctlDataGrid2[p_intRow, 15] = iii;
                price = m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid2[p_intRow, 6]);
            }
            this.m_objViewer.ctlDataGrid2[p_intRow, 7] = iii * price;
        }
        #endregion

        #region ת��������
        /// <summary>
        /// ת��������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public decimal m_mthConvertObjToDecimal(object obj)
        {
            try
            {
                if (obj != null && obj.ToString() != "")
                {
                    return Convert.ToDecimal(obj.ToString());

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public decimal m_mthConvertObjToDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Trim());
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region �����÷�
        public long m_mthFindUsage(string ID, int row)
        {
            string strItemID = this.m_objViewer.ctlDataGrid1[row, c_ItemID].ToString().Trim();
            string strReItemID = this.m_objViewer.ctlDataGrid1[row, c_resubitem].ToString().Trim();
            string strSubItemID = this.m_objViewer.ctlDataGrid1[row, c_SubItemID].ToString().Trim();
            bool blnSub = objSvc.m_blnIsSubChrgItem(strItemID, strReItemID);

            DataTable dt = null;
            long strRet = objSvc.m_mthFindUsage(ID, 1, out dt);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    m_objViewer.ctlDataGrid1[row, c_UsageName] = dt.Rows[0]["USAGENAME_VCHR"].ToString().Trim();

                    m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_FreName);
                    m_objViewer.ctlDataGrid1[row, c_UsageID] = dt.Rows[0]["USAGEID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, c_UsageName] = dt.Rows[0]["USAGENAME_VCHR"].ToString().Trim();

                    return 0;
                }
                else
                {
                    m_objViewer.listView2.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["USAGEID_CHR"].ToString().Trim());//ID
                        lv.SubItems.Add(dt.Rows[i]["USERCODE_CHR"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["USAGENAME_VCHR"].ToString().Trim());//����
                        m_objViewer.listView2.Items.Add(lv);
                    }

                }
                return 1;
            }
            else
            {

                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[6]).DataGridTextBoxColumn.TextBox.SelectAll();
                return -1;

            }
        }
        #endregion

        #region ������ҩ�÷�
        public long m_mthFindUsage2(string ID, int row)
        {
            DataTable dt = null;
            long strRet = objSvc.m_mthFindUsage(ID, 2, out dt);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row + 1, 0);
                    m_objViewer.ctlDataGrid2[row, 5] = dt.Rows[0]["USAGENAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 21] = dt.Rows[0]["USAGEID_CHR"].ToString().Trim();

                    //ֱ�����datagrid
                    return 0;
                }
                else
                {
                    m_objViewer.listView4.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["USAGEID_CHR"].ToString().Trim());//ID
                        lv.SubItems.Add(dt.Rows[i]["USERCODE_CHR"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["USAGENAME_VCHR"].ToString().Trim());//����
                        m_objViewer.listView4.Items.Add(lv);
                    }

                    //���listView

                }
                return 1;
            }
            else
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid2.Columns[5]).DataGridTextBoxColumn.TextBox.SelectAll();
                return -1;
            }
        }
        #endregion

        #region �����س��¼�
        public void m_mthDaysEnter(string str)
        {
            if (str.Trim() == "")
            {
                return;
            }
            else
            {
                int temp = this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
                SendKeys.SendWait("{Tab}");
            }
        }
        #endregion

        #region �����е���ת
        public void m_mthSetColNo1()
        {
            int col = this.m_objViewer.ctlDataGrid1.CurrentCell.ColumnNumber;
            int row = this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
            bool temp = false;
            //if (this.m_mthConvertObjToDecimal(this.m_objViewer.ctlDataGrid1[row, c_IsMain]) > -1)
            //{
            //    temp = true;
            //}
            if (temp)//��Щ����е�����Ŀ����ת������
            {
                if (col > 2 && col < 13)//ֱ����������
                {
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_Total);
                }
                if (col > 13)
                {
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row + 1, c_GroupNo);
                }
            }
            else
            {
                if (col > 2 && col < 6)
                {
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_UsageName);
                }
                if (col > 8 && col < 12)
                {
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_Total);
                }
                if (col > 13)
                {
                    this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row + 1, c_GroupNo);
                }
            }
        }

        public void m_mthSetColNo2()
        {
            int col = this.m_objViewer.ctlDataGrid2.CurrentCell.ColumnNumber;
            int row = this.m_objViewer.ctlDataGrid2.CurrentCell.RowNumber;

            if (col > 1 && col < 5)
            {
                this.m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row, 5);
            }
            if (col > 5 && col < cm_Deptmed)
            {
                this.m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row, cm_Deptmed);
            }
            if (col > cm_Deptmed)
            {
                this.m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row + 1, 0);
            }

            if (this.m_objViewer.ctlDataGrid2.CurrentCell.ColumnNumber != cm_Deptmed)
            {
                //this.m_objViewer.cboDeptmed2.Hide();
            }
        }

        public void m_mthSetColNo6()
        {
            int row = this.m_objViewer.ctlDataGrid6.CurrentCell.RowNumber;
            bool isCouPrice = this.m_objViewer.ctlDataGrid6[row, o_PriceFlag].ToString().Trim() == "0";
            if (isCouPrice)//��������Զ���۸�ľ�ֱ�Ӹ�����һ��
            {
                if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber > 1 && this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber < o_Deptmed)
                {
                    this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row, o_Deptmed);
                }
                if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber > o_Deptmed)
                {
                    this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row + 1, 0);
                }
            }
            else//��������
            {
                if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber > o_Count && this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber < o_Price)
                {
                    this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row, o_Price);
                }
                if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber > o_Price && this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber < o_Deptmed)
                {
                    this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row, o_Deptmed);
                }
                if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber > o_Deptmed)
                {
                    this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row + 1, 0);
                }
            }

            if (this.m_objViewer.ctlDataGrid6.CurrentCell.ColumnNumber != o_Deptmed)
            {
                //this.m_objViewer.cboDeptmed6.Hide();
            }
        }
        public void m_mthSetColNoLis()
        {
            int row = this.m_objViewer.ctlDataGridLis.CurrentCell.RowNumber;

            if (this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber > t_Count && this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber < t_PartName)
            {
                this.m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row, t_PartName);
                return;
            }

            if (this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber > t_PartName && this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber < t_quick)
            {
                this.m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row, t_quick);
            }
            else if (this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber > t_quick)
            {
                this.m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row + 1, t_Find);
            }

            if (this.m_objViewer.ctlDataGridLis.CurrentCell.ColumnNumber != t_quick)
            {
                //this.m_objViewer.cboquick.Hide();
            }
        }
        public void m_mthSetColNoTest()
        {
            int row = this.m_objViewer.ctlDataGridTest.CurrentCell.RowNumber;

            if (this.m_objViewer.ctlDataGridTest.CurrentCell.ColumnNumber > t_Count && this.m_objViewer.ctlDataGridTest.CurrentCell.ColumnNumber < t_PartName)
            {
                this.m_objViewer.ctlDataGridTest.CurrentCell = new DataGridCell(row, t_PartName);
                return;
            }

            if (this.m_objViewer.ctlDataGridTest.CurrentCell.ColumnNumber > t_PartName)
            {
                this.m_objViewer.ctlDataGridTest.CurrentCell = new DataGridCell(row + 1, 0);
            }
        }
        public void m_mthSetColNoOps()
        {
            int row = this.m_objViewer.ctlDataGridOps.CurrentCell.RowNumber;

            if (this.m_objViewer.ctlDataGridOps.CurrentCell.ColumnNumber > 1)
            {
                this.m_objViewer.ctlDataGridOps.CurrentCell = new DataGridCell(row + 1, 0);
            }
        }
        #endregion

        #region �����÷�Ƶ��(��ҩ)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public long m_mthFindFrequency(string ID, int row)
        {
            //�޸�
            DataTable dt = null;
            long strRet = objSvc.m_mthFindFrequency(ID, out dt);
            if (strRet > 0 && dt.Rows.Count > 0)
            {

                if (dt.Rows.Count == 1)
                {
                    m_objViewer.ctlDataGrid1[row, c_IsCal] = 1;
                    m_objViewer.ctlDataGrid1[row, c_FreID] = dt.Rows[0]["FREQID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, c_FreName] = dt.Rows[0]["FREQNAME_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid1[row, c_FreDays] = dt.Rows[0]["DAYS_INT"].ToString().Trim();
                    if (this.m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[row, c_Day]) % this.m_mthConvertObjToDecimal(dt.Rows[0]["DAYS_INT"]) != 0)
                    {
                        m_objViewer.ctlDataGrid1[row, c_Day] = dt.Rows[0]["DAYS_INT"].ToString().Trim();
                    }
                    m_objViewer.ctlDataGrid1[row, c_FreTimes] = dt.Rows[0]["TIMES_INT"].ToString().Trim();
                    m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_Day);
                    m_objViewer.ctlDataGrid1[row, c_FreName] = dt.Rows[0]["FREQNAME_CHR"].ToString().Trim();

                    //ֱ�����datagrid
                    return 0;
                }
                else
                {
                    m_objViewer.listView3.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["FREQID_CHR"].ToString().Trim());//ID
                        lv.SubItems.Add(dt.Rows[i]["USERCODE_CHR"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["FREQNAME_CHR"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["TIMES_INT"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["DAYS_INT"].ToString().Trim());//����
                        m_objViewer.listView3.Items.Add(lv);
                    }

                    //���listView

                }
                return 1;
            }
            else
            {

                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[6]).DataGridTextBoxColumn.TextBox.SelectAll();
                return -1;

            }
        }
        #endregion

        #region ������ҩ������Ŀ
        /// <summary>
        /// ���ݲ�ѯ���ݲ���ҩƷ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindWMedicineByID(string ID, int row)
        {
            //�޸�
            DataTable dt = null;
            long strRet = objSvc.m_lngFindChargeItem(ID, 1, out dt, false);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                m_objViewer.listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Medpurview == 2)
                    {
                        if (dt.Rows[i]["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dt.Rows[i]["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                        {
                            if (Neurpur != "1")
                            {
                                continue;
                            }
                        }

                        if (dt.Rows[i]["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dt.Rows[i]["ispoison_chr"].ToString().ToUpper() == "T")
                        {
                            if (Drugpur != "1")
                            {
                                continue;
                            }
                        }
                    }

                    ListViewItem lv = new ListViewItem(dt.Rows[i]["TYPE"].ToString().Trim());//��ѯ��
                    lv.SubItems.Add(dt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["ITEMCOMMNAME_VCHR"].ToString().Trim());//ͨ������
                    lv.SubItems.Add(dt.Rows[i]["ITEMENGNAME_VCHR"].ToString().Trim());// Ӣ����
                    lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ��������
                    lv.SubItems.Add(dt.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim());//���
                    lv.SubItems.Add(dt.Rows[i]["NMLDOSAGE_DEC"].ToString().Trim());//������
                    lv.SubItems.Add(dt.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim());//��λ
                    if (dt.Rows[0]["opchargeflg_int"].ToString().Trim() == "0")//�жϴ�С��λ
                    {
                        lv.SubItems.Add(dt.Rows[i]["ITEMPRICE_MNY"].ToString().Trim());//��λ
                    }
                    else
                    {
                        lv.SubItems.Add(dt.Rows[i]["SubMoney"].ToString().Trim());//С����
                    }
                    lv.SubItems.Add(dt.Rows[i]["PRECENT_DEC"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                    lv.SubItems.Add("");
                    //if (dt.Rows[i]["NOQTYFLAG_INT"].ToString().Trim() == "0")
                    //{
                    //    lv.SubItems.Add("");//�Ƿ�ȱҩ
                    //}
                    //else
                    //{
                    //    if (!isShowLackMedicine)
                    //    {
                    //        continue;
                    //    }
                    //    if (m_mthRelationInfo(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim()) == "0001")
                    //    {
                    //        lv.SubItems.Add("ȱҩ");
                    //        lv.ForeColor = Color.Red;
                    //    }
                    //    else
                    //    {
                    //        lv.SubItems.Add("��治��");
                    //        lv.ForeColor = Color.MediumBlue;
                    //    }
                    //}
                    lv.SubItems.Add(m_mthRelationInfo(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString().Trim()));//���﷢Ʊ����(����)                        

                    lv.Tag = dt.Rows[i];
                    m_objViewer.listView1.Items.Add(lv);
                }
                if (m_objViewer.listView1.Items.Count > 0)
                {
                    m_objViewer.listView1.Height = 175;
                    m_objViewer.listView1.Visible = true;
                    m_objViewer.listView1.Items[0].Selected = true;
                    m_objViewer.listView1.Select();
                    m_objViewer.listView1.Focus();
                }
                else
                {
                    MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid1.Columns[1]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region ���Ҷ�Ӧ����Ϣ
        private DataTable dt_RelationInfo;
        private string m_mthRelationInfo(string strCatID)
        {
            string str = "0005";//Ĭ������
            for (int i = 0; i < this.dt_RelationInfo.Rows.Count; i++)
            {
                if (strCatID == this.dt_RelationInfo.Rows[i]["CATID_CHR"].ToString().Trim())
                {
                    str = this.dt_RelationInfo.Rows[i]["GROUPID_CHR"].ToString().Trim();
                    break;
                }
            }

            if (str == "0006")
            {
                str = "0005";
            }

            return str;
        }
        /// <summary>
        /// ����IDת�����������
        /// </summary>
        /// <param name="strTypeNo"></param>
        /// <returns></returns>
        private string m_mthConvertToChType(string strTypeNo)
        {
            string strRet = "";
            {
                for (int i = 0; i < objResult.Length; i++)
                {
                    if (strTypeNo == objResult[i].m_strTypeID.Trim())
                    {
                        strRet = objResult[i].m_strTypeName;
                        break;
                    }
                }
            }

            return strRet;
        }
        #endregion

        #region ������Ŀ����
        private clsChargeItemEXType_VO[] objResult = null;
        private void m_mthLoadCat()
        {
            clsDomainControl_ChargeItem clsDomain = new clsDomainControl_ChargeItem();
            long l = clsDomain.m_GetEXType("2", out objResult);
            if (l < 0)
            {
                MessageBox.Show("������Ŀ����ʧ��!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            l = objSvc.m_mthRelationInfo(out this.dt_RelationInfo);
            if (l < 0)
            {
                MessageBox.Show("���ع�ϵ��ʧ��!", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ��������DataGrid���лس�תΪ�ո��
        private void m_mthSetDataGridEnterToSpace()
        {
            //��ҩ
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_GroupNo);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_Find);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_Count);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_UsageName);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_FreName);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_Day);
            m_objViewer.ctlDataGrid1.m_mthAddEnterToSpaceColumn(c_Total);
            //��ҩ
            m_objViewer.ctlDataGrid2.m_mthAddEnterToSpaceColumn(0);
            m_objViewer.ctlDataGrid2.m_mthAddEnterToSpaceColumn(1);
            m_objViewer.ctlDataGrid2.m_mthAddEnterToSpaceColumn(5);
            //����
            m_objViewer.ctlDataGrid6.m_mthAddEnterToSpaceColumn(0);
            m_objViewer.ctlDataGrid6.m_mthAddEnterToSpaceColumn(1);
            m_objViewer.ctlDataGrid6.m_mthAddEnterToSpaceColumn(o_Price);

            //����������Ŀ
            m_objViewer.ctlDataGridLis.m_mthAddEnterToSpaceColumn(0);
            m_objViewer.ctlDataGridLis.m_mthAddEnterToSpaceColumn(1);
            m_objViewer.ctlDataGridLis.m_mthAddEnterToSpaceColumn(t_PartName);
            m_objViewer.ctlDataGridLis.m_mthAddEnterToSpaceColumn(t_quick);
            //���������Ŀ
            m_objViewer.ctlDataGridTest.m_mthAddEnterToSpaceColumn(0);
            m_objViewer.ctlDataGridTest.m_mthAddEnterToSpaceColumn(1);
            m_objViewer.ctlDataGridTest.m_mthAddEnterToSpaceColumn(t_PartName);
            //��������������Ŀ
            m_objViewer.ctlDataGridOps.m_mthAddEnterToSpaceColumn(0);
            m_objViewer.ctlDataGridOps.m_mthAddEnterToSpaceColumn(1);
        }
        #endregion

        #region Ƶ�κ��÷���ListViewKeyDown����
        public void m_mthListViewKeyDown2(System.Windows.Forms.KeyEventArgs e)
        {//�޸�
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick2();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView2.Visible = false;
                this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber, 6);
            }
        }
        public void m_mthListView4KeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick4();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView4.Visible = false;
                this.m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid2.CurrentCell.RowNumber, 5);
            }
        }
        public void m_mthListViewKeyDown3(System.Windows.Forms.KeyEventArgs e)//Ƶ��
        {
            //�޸�
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick3();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView3.Visible = false;
                this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber, 7);
            }
        }
        //˫���¼�
        public void m_mthListViewDoubleClick2()
        {
            int row = m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
            string strItemID = this.m_objViewer.ctlDataGrid1[row, c_ItemID].ToString().Trim();
            string strReItemID = this.m_objViewer.ctlDataGrid1[row, c_resubitem].ToString().Trim();
            string strSubItemID = this.m_objViewer.ctlDataGrid1[row, c_SubItemID].ToString().Trim();
            bool blnSub = objSvc.m_blnIsSubChrgItem(strItemID, strReItemID);
            //�޸�				
            string strUsageID = m_objViewer.ctlDataGrid1[row, c_UsageID].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_UsageID] = m_objViewer.listView2.SelectedItems[0].SubItems[0].Text.Trim();
            m_objViewer.ctlDataGrid1[row, c_UsageName] = m_objViewer.listView2.SelectedItems[0].SubItems[2].Text.Trim();

            m_objViewer.listView2.Hide();
            m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_FreName);
        }
        public void m_mthListViewDoubleClick4()
        {
            int row = m_objViewer.ctlDataGrid2.CurrentCell.RowNumber;
            string strUsageID = m_objViewer.ctlDataGrid2[row, 21].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 21] = m_objViewer.listView4.SelectedItems[0].SubItems[0].Text.Trim();
            m_objViewer.ctlDataGrid2[row, 5] = m_objViewer.listView4.SelectedItems[0].SubItems[2].Text.Trim();
            m_objViewer.listView4.Hide();
            m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row + 1, 0);
        }
        public void m_mthListViewDoubleClick3()//Ƶ��
        {
            //�޸�
            int row = m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
            m_objViewer.ctlDataGrid1[row, c_FreID] = m_objViewer.listView3.SelectedItems[0].SubItems[0].Text.Trim();
            m_objViewer.ctlDataGrid1[row, c_FreName] = m_objViewer.listView3.SelectedItems[0].SubItems[2].Text.Trim();
            m_objViewer.ctlDataGrid1[row, c_FreTimes] = m_objViewer.listView3.SelectedItems[0].SubItems[3].Text.Trim();
            m_objViewer.ctlDataGrid1[row, c_FreDays] = m_objViewer.listView3.SelectedItems[0].SubItems[4].Text.Trim();
            if (this.m_mthConvertObjToDecimal(m_objViewer.ctlDataGrid1[row, c_Day]) % this.m_mthConvertObjToDecimal(m_objViewer.listView3.SelectedItems[0].SubItems[4].Text) != 0)
            {
                m_objViewer.ctlDataGrid1[row, c_Day] = m_objViewer.listView3.SelectedItems[0].SubItems[4].Text.Trim();
            }

            m_objViewer.ctlDataGrid1[row, c_IsCal] = 1;
            m_objViewer.listView3.Hide();
            m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_Day);
        }
        #endregion

        #region listViewKeyDown����
        public void m_mthListViewKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            switch (this.m_objViewer.tabControl1.SelectedIndex)
            {
                case 0:
                    m_mthKeyDown1(e);
                    break;
                case 1:
                    m_mthKeyDown2(e);
                    break;
                case 2:
                    m_mthKeyDown3(e);
                    break;
                case 3:
                    m_mthKeyDown4(e);
                    break;
                case 4:
                    m_mthKeyDown5(e);
                    break;
                case 5:
                    m_mthKeyDown6(e);
                    break;

            }
        }
        private void m_mthKeyDown1(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;
                this.m_objViewer.ctlDataGrid1.Select();
                this.m_objViewer.ctlDataGrid1.Focus();
                this.m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber, c_Find);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        private void m_mthKeyDown2(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;
                this.m_objViewer.ctlDataGrid2.Select();
                this.m_objViewer.ctlDataGrid2.Focus();
                this.m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid2.CurrentCell.RowNumber, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        private void m_mthKeyDown3(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;

                this.m_objViewer.ctlDataGridLis.Select();
                this.m_objViewer.ctlDataGridLis.Focus();
                this.m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGridLis.CurrentCell.RowNumber, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        private void m_mthKeyDown4(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;

                this.m_objViewer.ctlDataGridTest.Select();
                this.m_objViewer.ctlDataGridTest.Focus();
                this.m_objViewer.ctlDataGridTest.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGridTest.CurrentCell.RowNumber, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        private void m_mthKeyDown5(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;
                this.m_objViewer.ctlDataGridOps.Select();

                this.m_objViewer.ctlDataGridOps.Focus();
                this.m_objViewer.ctlDataGridOps.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGridOps.CurrentCell.RowNumber, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        private void m_mthKeyDown6(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView1.Height = 0;
                this.m_objViewer.listView1.Visible = false;
                this.m_objViewer.ctlDataGrid6.Select();
                this.m_objViewer.ctlDataGrid6.Focus();
                this.m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(this.m_objViewer.ctlDataGrid6.CurrentCell.RowNumber, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.m_mthListViewDoubleClick();
            }
        }
        #endregion

        #region listView(��ѯ���)˫���¼�
        /// <summary>
        /// listView(��ѯ���)˫���¼�
        /// </summary>
        public void m_mthListViewDoubleClick()
        {
            if (m_objViewer.listView1.SelectedItems.Count > 0 || m_objViewer.listView1.SelectedItems[0].ForeColor != Color.Red)
            {
                string TabPageName = this.m_objViewer.tabControl1.SelectedTab.Name.ToString();

                if (TabPageName == "tabPage1")
                {
                    string strChrgCode = this.m_objViewer.listView1.SelectedItems[0].SubItems[12].Text;
                    if (strChrgCode != null && strChrgCode != "")
                    {
                        int index = int.Parse(strChrgCode) - 1;
                        this.m_objViewer.tabControl1.SelectedIndex = index;
                    }
                }
                switch (TabPageName)
                {
                    case "tabPage1":
                        m_mthDoubleClick1();
                        break;
                    case "tabPage2":
                        m_mthDoubleClick2();
                        break;
                    case "tabPage3":
                        m_mthDoubleClick3();
                        break;
                    case "tabPage4":
                        m_mthDoubleClick4();
                        break;
                    case "tabPage5":
                        m_mthDoubleClick5();
                        break;
                    case "tabPage6":
                        m_mthDoubleClick6();
                        break;
                }
            }
        }
        private void m_mthDoubleClick1()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;
            m_mthFillDataGridByRow(dr, row);
        }
        private void m_mthFillDataGridByRow(DataRow dr, int row)
        {
            #region �����ԡ�����ҩ����
            if (Medpurview == 1)
            {
                if (dr["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dr["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                {
                    if (Neurpur != "1")
                    {
                        MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (dr["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dr["ispoison_chr"].ToString().ToUpper() == "T")
                {
                    if (Drugpur != "1")
                    {
                        MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (Medpurview == 2)
            {
                if (dr["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dr["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                {
                    if (Neurpur != "1")
                    {
                        MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                if (dr["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dr["ispoison_chr"].ToString().ToUpper() == "T")
                {
                    if (Drugpur != "1")
                    {
                        MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            #endregion

            #region
            string strCurrItem = m_objViewer.ctlDataGrid1[row, c_ItemID].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_Find] = dr["ITEMCODE_VCHR"].ToString().Trim();

            m_objViewer.ctlDataGrid1[row, c_Count] = dr["ADULTDOSAGE_DEC"].ToString().Trim();

            m_objViewer.ctlDataGrid1[row, c_Unit] = dr["DOSAGEUNIT_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_Name] = dr["ITEMNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_Spec] = dr["ITEMSPEC_VCHR"].ToString().Trim();

            m_objViewer.ctlDataGrid1[row, c_Price] = dr["SubMoney"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_BigUnit] = dr["ITEMIPUNIT_CHR"].ToString().Trim();
            if (dr["opchargeflg_int"].ToString().Trim() == "0")//�жϴ�С��λ
            {
                m_objViewer.ctlDataGrid1[row, c_Price] = dr["ITEMPRICE_MNY"].ToString().Trim();//�󵥼�
                m_objViewer.ctlDataGrid1[row, c_BigUnit] = dr["ITEMOPUNIT_CHR"].ToString().Trim();
            }
            m_objViewer.ctlDataGrid1[row, c_ItemID] = dr["ITEMID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_Packet] = dr["PACKQTY_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_DiscountName] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
            m_objViewer.ctlDataGrid1[row, c_Discount] = dr["PRECENT_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_UnitFlag] = dr["opchargeflg_int"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_Dosage] = m_mthConvertObjToDecimal(dr["DOSAGE_DEC"]);
            m_objViewer.ctlDataGrid1[row, c_MaxLimit] = dr["MAXDOSAGE_DEC"].ToString().Trim();//����
            m_objViewer.ctlDataGrid1[row, c_MinLimit] = dr["MINDOSAGE_DEC"].ToString().Trim();//����
            m_objViewer.ctlDataGrid1[row, c_IsCal] = 1;
            m_objViewer.ctlDataGrid1[row, c_InvoiceType] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();//��Ʊ����
            string tempUsageID = m_objViewer.ctlDataGrid1[row, 16].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_UsageName] = dr["USAGENAME_VCHR"].ToString().Trim();//Ĭ���÷�����
            m_objViewer.ctlDataGrid1[row, c_UsageID] = dr["USAGEID_CHR"].ToString().Trim();//Ĭ���÷�ID
            m_objViewer.ctlDataGrid1[row, c_EnglishName] = dr["ITEMENGNAME_VCHR"].ToString().Trim();//Ӣ����			

            this.m_objViewer.ctlDataGrid1[row, c_FreName] = dr["freqname"].ToString();
            this.m_objViewer.ctlDataGrid1[row, c_FreDays] = m_mthConvertObjToDecimal(dr["freqdays"]);
            this.m_objViewer.ctlDataGrid1[row, c_FreTimes] = m_mthConvertObjToDecimal(dr["freqtimes"]);
            this.m_objViewer.ctlDataGrid1[row, c_FreID] = dr["freqid"].ToString();

            try
            {
                m_objViewer.ctlDataGrid1[row, c_PSFlag] = dr["HYPE_INT"].ToString().Trim();//Ƥ�Ա�־
                if (dr["HYPE_INT"].ToString().Trim() == "1")//�����ҩҪƤ����Ĭ��ΪƤ��
                {
                    m_objViewer.ctlDataGrid1[row, c_PS] = 1;
                    this.m_objViewer.ctlDataGrid1.m_mthFormatCell(row, c_UsageName, this.m_objViewer.ctlDataGrid1.Font, Color.White, Color.Red);
                }
                else
                {
                    this.m_objViewer.ctlDataGrid1.m_mthFormatCell(row, c_UsageName, this.m_objViewer.ctlDataGrid1.Font, Color.White, Color.Black);
                }

                this.m_objViewer.ctlDataGrid1[row, c_Deptmed] = "";
                this.m_objViewer.ctlDataGrid1[row, c_DeptmedID] = "";
                if (dr["DEPTPREP_INT"].ToString() != "1")
                {
                    this.m_objViewer.ctlDataGrid1[row, c_DeptmedID] = "*";  //����������Ա�                   
                }
            }
            catch
            {

            }

            m_objViewer.listView1.Height = 0;
            m_objViewer.ctlDataGrid1.CurrentCell = new DataGridCell(row, c_Count);
            m_objViewer.ctlDataGrid1[row, c_Find] = dr["ITEMCODE_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid1[row, c_IsMain] = -4;

            #endregion
        }
        private void m_mthDoubleClick2()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;
            m_mthFillDataGridByRow2(dr, row);

        }
        private void m_mthFillDataGridByRow2(DataRow dr, int row)
        {
            #region �����ԡ�����ҩ����
            if (Medpurview == 1)
            {
                if (dr["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dr["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                {
                    if (Neurpur != "1")
                    {
                        MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (dr["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dr["ispoison_chr"].ToString().ToUpper() == "T")
                {
                    if (Drugpur != "1")
                    {
                        MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (Medpurview == 2)
            {
                if (dr["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dr["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                {
                    if (Neurpur != "1")
                    {
                        MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }

                if (dr["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dr["ispoison_chr"].ToString().ToUpper() == "T")
                {
                    if (Drugpur != "1")
                    {
                        MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }
            #endregion

            m_objViewer.ctlDataGrid2[row, 0] = dr["ITEMCODE_VCHR"].ToString().Trim();

            m_objViewer.ctlDataGrid2[row, 1] = dr["ADULTDOSAGE_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 11] = dr["ADULTDOSAGE_DEC"].ToString().Trim();

            m_objViewer.ctlDataGrid2[row, 2] = dr["DOSAGEUNIT_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 3] = dr["ITEMNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 4] = dr["ITEMSPEC_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 5] = dr["usagename_vchr"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 6] = dr["SubMoney"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 8] = dr["ITEMID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 10] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
            m_objViewer.ctlDataGrid2[row, 11] = dr["PRECENT_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 13] = dr["MAXDOSAGE_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 14] = dr["MINDOSAGE_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 16] = dr["ITEMPRICE_MNY"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 17] = dr["opchargeflg_int"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 18] = dr["PACKQTY_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid2[row, 19] = 0;
            m_objViewer.ctlDataGrid2[row, 20] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();//��Ʊ����
            m_objViewer.ctlDataGrid2[row, 21] = dr["USAGEID_CHR"].ToString().Trim();    //�÷�ID
            m_objViewer.ctlDataGrid2[row, 24] = dr["ITEMENGNAME_VCHR"].ToString().Trim();//Ӣ����

            try
            {
                m_objViewer.listView1.Height = 0;
                m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row, 1);
                m_objViewer.ctlDataGrid2[row, 0] = dr["ITEMCODE_VCHR"].ToString().Trim();
            }
            catch
            {
            }
        }
        private void m_mthDoubleClick3()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;

            m_mthFillDataGridByRowLis(dr, row);
        }
        private void m_mthFillDataGridByRowLis(DataRow dr, int row)
        {
            string strCurrItem = m_objViewer.ctlDataGridLis[row, t_ItemID].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Find] = dr["USERCODE_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Name] = dr["NAME_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Spec] = dr["itemspec_vchr"].ToString().Trim();//���
            m_objViewer.ctlDataGridLis[row, t_PartName] = dr["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Unit] = dr["itemunit"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_PriceFlag] = dr["LISAPPLYUNITID_CHR"].ToString().Trim(); //���Զ��۸��м�¼���뵥ԪID        
            m_objViewer.ctlDataGridLis[row, t_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
            m_objViewer.ctlDataGridLis[row, t_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Temp] = dr["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridLis[row, t_Count] = 1;

            m_objViewer.listView1.Height = 0;
            m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row, t_Count);
        }

        private void m_mthDoubleClick4()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;

            m_mthFillDataGridByRowTest(dr, row);
        }

        private void m_mthFillDataGridByRowTest(DataRow dr, int row)
        {
            string strCurrItem = m_objViewer.ctlDataGridTest[row, t_ItemID].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Find] = dr["USERCODE_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Name] = dr["NAME_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Spec] = dr["itemspec_vchr"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_PartName] = dr["partname"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Unit] = dr["itemunit"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Price] = "";
            m_objViewer.ctlDataGridTest[row, t_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_PriceFlag] = dr["APPLYTYPEID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_DiscountName] = "";//����
            m_objViewer.ctlDataGridTest[row, t_Discount] = "";
            m_objViewer.ctlDataGridTest[row, t_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
            m_objViewer.ctlDataGridTest[row, t_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Temp] = dr["partid_vchr"].ToString().Trim();
            m_objViewer.ctlDataGridTest[row, t_Count] = 1;
            m_objViewer.ctlDataGridTest[row, t_UsageID] = dr["usageid_chr"].ToString().Trim();

            m_objViewer.listView1.Height = 0;
            m_objViewer.ctlDataGridTest.CurrentCell = new DataGridCell(row, t_Count);
        }

        private void m_mthDoubleClick5()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;
            m_mthFillDataGridByRowOps(dr, row);
        }

        private void m_mthFillDataGridByRowOps(DataRow dr, int row)
        {
            string strCurrItem = m_objViewer.ctlDataGridOps[row, o_ItemID].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Find] = dr["USERCODE_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Name] = dr["NAME_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Spec] = dr["itemspec_vchr"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Unit] = dr["itemunit"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Price] = "";
            m_objViewer.ctlDataGridOps[row, o_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_PriceFlag] = dr["APPLYTYPEID_CHR"].ToString().Trim();  //���Զ���۸��д����뵥����
            m_objViewer.ctlDataGridOps[row, o_DiscountName] = "";//����
            m_objViewer.ctlDataGridOps[row, o_Discount] = "";
            m_objViewer.ctlDataGridOps[row, o_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
            m_objViewer.ctlDataGridOps[row, o_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGridOps[row, o_Count] = 1;
            m_objViewer.ctlDataGridOps[row, o_UsageID] = dr["usageid_chr"].ToString().Trim();

            m_objViewer.listView1.Height = 0;
            m_objViewer.ctlDataGridOps.CurrentCell = new DataGridCell(row, o_Count);
        }

        private void m_mthDoubleClick6()
        {
            int row = rowNo;
            DataRow dr = (DataRow)m_objViewer.listView1.SelectedItems[0].Tag;
            m_mthFillDataGridByRow6(dr, row);
        }

        private void m_mthFillDataGridByRow6(DataRow dr, int row)
        {
            string strCurrItem = m_objViewer.ctlDataGrid6[row, o_ItemID].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_Find] = dr["ITEMCODE_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_Name] = dr["ITEMNAME_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_Spec] = dr["ITEMSPEC_VCHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_Unit] = dr["ITEMOPUNIT_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_Price] = dr["ITEMPRICE_MNY"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_ItemID] = dr["ITEMID_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_PriceFlag] = dr["SELFDEFINE_INT"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_DiscountName] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
            m_objViewer.ctlDataGrid6[row, o_Discount] = dr["PRECENT_DEC"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_InvoiceType] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();
            m_objViewer.ctlDataGrid6[row, o_EnglishName] = dr["ITEMENGNAME_VCHR"].ToString().Trim();

            this.m_objViewer.ctlDataGrid6[row, o_Deptmed] = "";
            this.m_objViewer.ctlDataGrid6[row, o_DeptmedID] = "";
            if (dr["DEPTPREP_INT"].ToString() != "1")
            {
                this.m_objViewer.ctlDataGrid6[row, o_DeptmedID] = "*";  //����������Ա�                   
            }

            m_objViewer.listView1.Height = 0;
            m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row, o_Count);
        }
        #endregion

        #region ������ҩ������Ŀ
        /// <summary>
        /// ���ݲ�ѯ���ݲ���ҩƷ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindCMedicineByID(string ID, int row)
        {
            DataTable dt = null;
            long strRet = objSvc.m_lngFindChargeItem(ID, 2, out dt, false);
            if (strRet > 0 && dt.Rows.Count > 0)
            {

                if (dt.Rows.Count == 1)
                {
                    //if (dt.Rows[0]["NOQTYFLAG_INT"].ToString().Trim() != "0")
                    //{
                    //    if (MessageBox.Show("ȱҩ!�Ƿ����?", "ICare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //    {
                    //        return;
                    //    }
                    //}

                    if (Medpurview == 1)
                    {
                        if (dt.Rows[0]["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dt.Rows[0]["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                        {
                            if (Neurpur != "1")
                            {
                                MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }

                        if (dt.Rows[0]["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dt.Rows[0]["ispoison_chr"].ToString().ToUpper() == "T")
                        {
                            if (Drugpur != "1")
                            {
                                MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    else if (Medpurview == 2)
                    {
                        if (dt.Rows[0]["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dt.Rows[0]["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                        {
                            if (Neurpur != "1")
                            {
                                MessageBox.Show(Neurpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }

                        if (dt.Rows[0]["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dt.Rows[0]["ispoison_chr"].ToString().ToUpper() == "T")
                        {
                            if (Drugpur != "1")
                            {
                                MessageBox.Show(Drugpurhintinfo, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }
                    }

                    m_mthFillDataGridByRow2(dt.Rows[0], row);
                    //ֱ�����datagrid
                }
                else
                {
                    m_objViewer.listView1.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Medpurview == 2)
                        {
                            if (dt.Rows[i]["ISCHLORPROMAZINE_CHR"].ToString().ToUpper() == "T" || dt.Rows[i]["ISCHLORPROMAZINE2_CHR"].ToString().ToUpper() == "T")
                            {
                                if (Neurpur != "1")
                                {
                                    continue;
                                }
                            }

                            if (dt.Rows[i]["ISANAESTHESIA_CHR"].ToString().ToUpper() == "T" || dt.Rows[i]["ispoison_chr"].ToString().ToUpper() == "T")
                            {
                                if (Drugpur != "1")
                                {
                                    continue;
                                }
                            }
                        }

                        ListViewItem lv = new ListViewItem(dt.Rows[i]["TYPE"].ToString().Trim());//��ѯ��
                        lv.SubItems.Add(dt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["ITEMCOMMNAME_VCHR"].ToString().Trim());//ͨ������
                        lv.SubItems.Add(dt.Rows[i]["ITEMENGNAME_VCHR"].ToString().Trim());// Ӣ����
                        lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ��������
                        lv.SubItems.Add(dt.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim());//���
                        lv.SubItems.Add(dt.Rows[i]["NMLDOSAGE_DEC"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim());//��λ
                        lv.SubItems.Add(dt.Rows[i]["SubMoney"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["PRECENT_DEC"].ToString().Trim());//����
                        lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                        lv.SubItems.Add("");
                        //if (dt.Rows[i]["NOQTYFLAG_INT"].ToString().Trim() == "0")
                        //{
                        //    lv.SubItems.Add("");//�Ƿ�ȱҩ
                        //}
                        //else
                        //{
                        //    if (!isShowLackMedicine)
                        //    {
                        //        continue;
                        //    }
                        //    else
                        //    {
                        //        lv.SubItems.Add("ȱҩ");
                        //        lv.ForeColor = Color.Red;
                        //    }
                        //}

                        lv.Tag = dt.Rows[i];
                        m_objViewer.listView1.Items.Add(lv);
                    }
                    m_objViewer.listView1.Height = 175;
                    m_objViewer.listView1.Visible = true;
                    m_objViewer.listView1.Items[0].Selected = true;
                    m_objViewer.listView1.Select();
                    m_objViewer.listView1.Focus();
                    //���listView
                }
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid2.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public long m_lngGetLisSampletyType(string ID, int row)
        {
            DataTable dt = null;
            long strRet = objSvc.m_lngGetLisSampletyType(ID, "PYCODE_CHR", out dt);
            if (strRet > 0 && dt.Rows.Count > 0)
            {

                if (dt.Rows.Count == 1)
                {
                    m_objViewer.ctlDataGridLis[row, t_PartName] = dt.Rows[0]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Temp] = dt.Rows[0]["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row + 1, 0);

                    return 0;
                }
                else
                {
                    this.m_objViewer.ctlDataGridLis.Controls.Add(m_objViewer.listView5);
                    m_objViewer.listView5.Items.Clear();
                    m_objViewer.listView5.Columns[2].Text = "ƴ����";
                    this.objDataGrid = this.m_objViewer.ctlDataGridLis;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["SAMPLE_TYPE_ID_CHR"].ToString().Trim());//ID
                        lv.SubItems.Add(dt.Rows[i]["PYCODE_CHR"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim());//����
                        m_objViewer.listView5.Items.Add(lv);
                    }

                }
                return 1;
            }
            else
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGridLis.Columns[t_PartName]).DataGridTextBoxColumn.TextBox.SelectAll();

                return -1;

            }
        }
        #endregion

        #region ���Ҽ�鲿λ
        /// <summary>
        /// ���Ҽ�鲿λ
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public long m_mthLoadCheckPart(string ID, int row)
        {
            long strRet = 0;
            DataTable dt = null;
            com.digitalwave.controls.datagrid.ctlDataGrid dg = new com.digitalwave.controls.datagrid.ctlDataGrid();

            dg = m_objViewer.ctlDataGridTest;
            string applyid = this.m_objViewer.ctlDataGridTest[row, t_PriceFlag].ToString();
            strRet = objSvc.m_mthLoadCheckPartOrder(applyid, ID, out dt);

            if (strRet > 0 && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    dg[row, t_PartName] = dt.Rows[0]["PARTNAME"].ToString().Trim();
                    dg[row, t_Temp] = dt.Rows[0]["PARTID"].ToString().Trim();
                    dg.CurrentCell = new DataGridCell(row + 1, 0);
                    dg[row, t_PartName] = dt.Rows[0]["PARTNAME"].ToString().Trim();
                    //ֱ�����datagrid
                    return 0;
                }
                else
                {
                    dg.Controls.Add(m_objViewer.listView5);
                    m_objViewer.listView5.Items.Clear();
                    m_objViewer.listView5.Columns[2].Text = "������";
                    this.objDataGrid = dg;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem(dt.Rows[i]["PARTID"].ToString().Trim());//ID
                        lv.SubItems.Add(dt.Rows[i]["ASSISTCODE_CHR"].ToString().Trim());//������
                        lv.SubItems.Add(dt.Rows[i]["PARTNAME"].ToString().Trim());//����
                        m_objViewer.listView5.Items.Add(lv);
                    }

                    //���listView

                }
                return 1;
            }
            else
            {
                ((com.digitalwave.controls.datagrid.clsColumnInfo)dg.Columns[t_PartName]).DataGridTextBoxColumn.TextBox.SelectAll();
                return -1;
            }
        }
        #endregion

        #region listView5���¼�����
        private void listView5_Leave(object sender, EventArgs e)
        {
            this.m_objViewer.listView5.Hide();
        }
        private com.digitalwave.controls.datagrid.ctlDataGrid objDataGrid;
        private void listView5_DoubleClick(object sender, EventArgs e)
        {
            int rorTemp = objDataGrid.CurrentCell.RowNumber;
            if (this.m_objViewer.listView5.SelectedItems.Count == 0)
            {
                objDataGrid.CurrentCell = new DataGridCell(rorTemp, t_PartName);
                return;
            }

            objDataGrid[rorTemp, t_PartName] = this.m_objViewer.listView5.SelectedItems[0].SubItems[2].Text;
            objDataGrid[rorTemp, t_Temp] = this.m_objViewer.listView5.SelectedItems[0].Text;
            objDataGrid.CurrentCell = new DataGridCell(rorTemp + 1, 0);
        }

        private void listView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.listView5_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.m_objViewer.listView5.Hide();
                this.objDataGrid.CurrentCell = new DataGridCell(objDataGrid.CurrentCell.RowNumber, t_PartName);
            }
        }
        #endregion

        #region �������Ƽ�����Ŀ
        /// <summary>
        /// �������Ƽ�����Ŀ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindOrderLisByID(string ID, int row)
        {
            DataTable dt = null;

            long l = objSvc.m_lngFindRecipeOrderByID(ID, OrderCatLisArr, out dt, false);
            if (l > 0 && dt.Rows.Count > 0)
            {
                m_objViewer.listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(dt.Rows[i]["usercode_chr"].ToString().Trim()); //�û�����
                    lv.SubItems.Add(dt.Rows[i]["NAME_CHR"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["COMMNAME_VCHR"].ToString().Trim());//ͨ������
                    lv.SubItems.Add(dt.Rows[i]["ENGNAME_VCHR"].ToString().Trim());// Ӣ����
                    lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ�������� 
                    lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());//���
                    lv.SubItems.Add("1");//������
                    lv.SubItems.Add(dt.Rows[i]["itemunit"].ToString().Trim());//��λ
                    lv.SubItems.Add(dt.Rows[i]["totalmny"].ToString().Trim());//����
                    lv.SubItems.Add("");//����
                    lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                    lv.Tag = dt.Rows[i];
                    m_objViewer.listView1.Items.Add(lv);
                }
                m_objViewer.listView1.Height = 175;
                m_objViewer.listView1.Visible = true;
                m_objViewer.listView1.Items[0].Selected = true;
                m_objViewer.listView1.Select();
                m_objViewer.listView1.Focus();
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGridLis.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region ��ȡ�ָ��ַ�����ֵ
        /// <summary>
        /// ��ȡ�ָ��ַ�����ֵ
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public List<string> m_Gettoken(string str, string sign)
        {
            List<string> val = null;
            if (str == null)
                return val;
            if (str.Trim() == "")
            {
                return val;
            }

            int pos = 0;
            val = new List<string>();

            do
            {
                pos = str.IndexOf(sign);
                if (pos > 0)
                {
                    val.Add(str.Substring(0, pos));
                    str = str.Substring(pos + 1);
                }
                else
                {
                    val.Add(str);
                }
            } while (pos > 0);

            return val;
        }
        #endregion

        #region �������Ƽ����Ŀ
        /// <summary>
        /// �������Ƽ����Ŀ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindExamineChargeByOrderID(string ID, int row)
        {
            DataTable dt = null;

            long l = objSvc.m_lngFindRecipeOrderByID(ID, OrderCatTestArr, out dt, false);
            if (l > 0 && dt.Rows.Count > 0)
            {
                m_objViewer.listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(dt.Rows[i]["usercode_chr"].ToString().Trim()); //��ĿID
                    lv.SubItems.Add(dt.Rows[i]["NAME_CHR"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["COMMNAME_VCHR"].ToString().Trim());//ͨ������
                    lv.SubItems.Add(dt.Rows[i]["ENGNAME_VCHR"].ToString().Trim());// Ӣ����
                    lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ�������� 
                    lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());//���
                    lv.SubItems.Add("1");//������
                    lv.SubItems.Add(dt.Rows[i]["itemunit"].ToString().Trim());//��λ
                    lv.SubItems.Add(dt.Rows[i]["totalmny"].ToString().Trim());//����
                    lv.SubItems.Add("");//����
                    lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                    lv.Tag = dt.Rows[i];
                    m_objViewer.listView1.Items.Add(lv);
                }
                m_objViewer.listView1.Height = 175;
                m_objViewer.listView1.Visible = true;
                m_objViewer.listView1.Items[0].Selected = true;
                m_objViewer.listView1.Select();
                m_objViewer.listView1.Focus();
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGridTest.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region ��������������Ŀ
        /// <summary>
        /// ��������������Ŀ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindOPSChargeByOrderID(string ID, int row)
        {
            DataTable dt = null;

            long l = objSvc.m_lngFindRecipeOrderByID(ID, OrderCatOpsArr, out dt, false);
            if (l > 0 && dt.Rows.Count > 0)
            {
                m_objViewer.listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(dt.Rows[i]["usercode_chr"].ToString().Trim());//��ĿID
                    lv.SubItems.Add(dt.Rows[i]["NAME_CHR"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["COMMNAME_VCHR"].ToString().Trim());//ͨ������
                    lv.SubItems.Add(dt.Rows[i]["ENGNAME_VCHR"].ToString().Trim());// Ӣ����
                    lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ�������� 
                    lv.SubItems.Add(dt.Rows[i]["itemspec_vchr"].ToString().Trim());//���
                    lv.SubItems.Add("1");//������
                    lv.SubItems.Add(dt.Rows[i]["itemunit"].ToString().Trim());//��λ
                    lv.SubItems.Add(dt.Rows[i]["totalmny"].ToString().Trim());//����
                    lv.SubItems.Add("");//����
                    lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                    lv.Tag = dt.Rows[i];
                    m_objViewer.listView1.Items.Add(lv);
                }
                m_objViewer.listView1.Height = 175;
                m_objViewer.listView1.Visible = true;
                m_objViewer.listView1.Items[0].Selected = true;
                m_objViewer.listView1.Select();
                m_objViewer.listView1.Focus();
            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGridOps.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region ����������Ŀ
        /// <summary>
        /// ���Ҽ�����Ŀ
        /// </summary>
        /// <param name="ID">��������</param>
        /// <param name="row">�к�</param>
        public void m_mthFindOtherChargeByID(string ID, int row)
        {
            DataTable dt = null;
            long strRet = objSvc.m_lngFindChargeItem(ID, 6, out dt, false);
            if (strRet > 0 && dt.Rows.Count > 0)
            {
                m_objViewer.listView1.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(dt.Rows[i]["TYPE"].ToString().Trim());//��ĿID                    
                    lv.SubItems.Add(dt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["ITEMCOMMNAME_VCHR"].ToString().Trim());//ͨ������
                    lv.SubItems.Add(dt.Rows[i]["ITEMENGNAME_VCHR"].ToString().Trim());// Ӣ����
                    lv.SubItems.Add(this.m_mthConvertToChType(dt.Rows[i]["ITEMOPINVTYPE_CHR"].ToString()));// ��Ʊ��������
                    lv.SubItems.Add(dt.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim());//���
                    lv.SubItems.Add("");//������
                    lv.SubItems.Add(dt.Rows[i]["ITEMOPUNIT_CHR"].ToString().Trim());//��λ
                    lv.SubItems.Add(dt.Rows[i]["ITEMPRICE_MNY"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["PRECENT_DEC"].ToString().Trim());//����
                    lv.SubItems.Add(dt.Rows[i]["ybtypename"].ToString().Trim());//ҽ������
                    lv.SubItems.Add(dt.Rows[i]["SELFDEFINE_INT"].ToString().Trim());//�Ƿ��Զ���۸�
                    lv.Tag = dt.Rows[i];
                    m_objViewer.listView1.Items.Add(lv);
                }
                m_objViewer.listView1.Height = 175;
                m_objViewer.listView1.Visible = true;
                m_objViewer.listView1.Items[0].Selected = true;
                m_objViewer.listView1.Select();
                m_objViewer.listView1.Focus();

            }
            else
            {
                MessageBox.Show("�Բ����Ҳ����κ��շ���Ŀ��", "ICare", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ((com.digitalwave.controls.datagrid.clsColumnInfo)m_objViewer.ctlDataGrid6.Columns[0]).DataGridTextBoxColumn.TextBox.SelectAll();
            }
            rowNo = row;
        }
        #endregion

        #region �½�
        /// <summary>
        /// �½�
        /// </summary>
        public void m_mthNew()
        {
            this.m_objViewer.txtRecName.Text = "";
            this.m_objViewer.txtUserCode.Text = "";
            this.m_objViewer.txtPyCode.Text = "";
            this.m_objViewer.txtWbCode.Text = "";
            this.m_objViewer.txtReMark.Text = "";
            this.m_objViewer.cboUseScope.SelectedIndex = 0;
            this.m_objViewer.cboStatus.SelectedIndex = 0;

            this.m_mthDeleteAllRows();
            this.m_objViewer.tabControl1.SelectedIndex = 0;

            this.m_objViewer.btnSave.Tag = "";

            this.m_objViewer.txtRecName.Focus();
        }

        /// <summary>
        /// ���������
        /// </summary>
        private void m_mthDeleteAllRows()
        {
            this.m_objViewer.ctlDataGrid1.m_mthDeleteAllRow();
            this.m_objViewer.ctlDataGrid2.m_mthDeleteAllRow();
            this.m_objViewer.ctlDataGrid6.m_mthDeleteAllRow();
            this.m_objViewer.ctlDataGridLis.m_mthDeleteAllRow();
            this.m_objViewer.ctlDataGridTest.m_mthDeleteAllRow();
            this.m_objViewer.ctlDataGridOps.m_mthDeleteAllRow();
        }
        #endregion

        #region ɾ����Ч��ϸ��
        /// <summary>
        /// ɾ����Ч��ϸ��
        /// </summary>
        public void m_mthDeleteInvalidRow()
        {
            for (int i = this.m_objViewer.ctlDataGrid1.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGrid1[i, c_ItemID] == null || this.m_objViewer.ctlDataGrid1[i, c_ItemID].ToString() == "")
                {
                    this.m_objViewer.ctlDataGrid1.m_mthDeleteRow(i);
                }
            }

            for (int i = this.m_objViewer.ctlDataGrid2.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGrid2[i, 8] == null || this.m_objViewer.ctlDataGrid2[i, 8].ToString() == "")
                {
                    this.m_objViewer.ctlDataGrid2.m_mthDeleteRow(i);
                }
            }

            for (int i = this.m_objViewer.ctlDataGrid6.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGrid6[i, 7] == null || this.m_objViewer.ctlDataGrid6[i, 7].ToString() == "")
                {
                    this.m_objViewer.ctlDataGrid6.m_mthDeleteRow(i);
                }
            }

            for (int i = this.m_objViewer.ctlDataGridLis.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGridLis[i, t_ItemID] == null || this.m_objViewer.ctlDataGridLis[i, t_ItemID].ToString() == "")
                {
                    this.m_objViewer.ctlDataGridLis.m_mthDeleteRow(i);
                }
            }

            for (int i = this.m_objViewer.ctlDataGridTest.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGridTest[i, t_ItemID] == null || this.m_objViewer.ctlDataGridTest[i, t_ItemID].ToString() == "")
                {
                    this.m_objViewer.ctlDataGridTest.m_mthDeleteRow(i);
                }
            }

            for (int i = this.m_objViewer.ctlDataGridOps.RowCount - 1; i >= 0; i--)
            {
                if (this.m_objViewer.ctlDataGridOps[i, o_ItemID] == null || this.m_objViewer.ctlDataGridOps[i, o_ItemID].ToString() == "")
                {
                    this.m_objViewer.ctlDataGridOps.m_mthDeleteRow(i);
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void m_mthSave()
        {
            #region ��Ч�Լ���
            if (this.m_objViewer.txtRecName.Text.Trim() == "")
            {
                MessageBox.Show("������Э���������ơ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            this.m_mthDeleteInvalidRow();

            List<clsAccordRecipePlus_VO> objRecEntryArr = new List<clsAccordRecipePlus_VO>();

            //��ҩ
            for (int i = 0; i < this.m_objViewer.ctlDataGrid1.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid1[i, c_Count].ToString().Trim() == "" || this.m_objViewer.ctlDataGrid1[i, c_Count].ToString().Trim() == "0")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�еļ������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGrid1[i, c_UsageName].ToString().Trim() == "")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�е��÷����벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGrid1[i, c_FreName].ToString().Trim() == "")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�е�Ƶ�����벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGrid1[i, c_Day].ToString().Trim() == "")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGrid1[i, c_ItemID].ToString();
                AccordRecipePlus_VO.RecNO = this.m_objViewer.ctlDataGrid1[i, c_GroupNo].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGrid1[i, c_Count].ToString();
                AccordRecipePlus_VO.UsageID = this.m_objViewer.ctlDataGrid1[i, c_UsageID].ToString();
                AccordRecipePlus_VO.FreqID = this.m_objViewer.ctlDataGrid1[i, c_FreID].ToString();
                AccordRecipePlus_VO.Days = this.m_objViewer.ctlDataGrid1[i, c_Day].ToString();
                AccordRecipePlus_VO.Type = "0";
                AccordRecipePlus_VO.Class = "1";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }

            //��ҩ
            for (int i = 0; i < this.m_objViewer.ctlDataGrid2.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid2[i, 1].ToString().Trim() == "" || this.m_objViewer.ctlDataGrid2[i, 1].ToString().Trim() == "0")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGrid2[i, 21].ToString().Trim() == "")
                {
                    MessageBox.Show("��ҩ����" + Convert.ToString(i + 1) + "�е��÷����벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGrid2[i, 8].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGrid2[i, 1].ToString();
                AccordRecipePlus_VO.UsageID = this.m_objViewer.ctlDataGrid2[i, 21].ToString();
                AccordRecipePlus_VO.Type = "0";
                AccordRecipePlus_VO.Class = "2";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }

            //����
            for (int i = 0; i < this.m_objViewer.ctlDataGridLis.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGridLis[i, t_Count].ToString().Trim() == "" || this.m_objViewer.ctlDataGridLis[i, t_Count].ToString().Trim() == "0")
                {
                    MessageBox.Show("��������" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGridLis[i, t_PartName].ToString().Trim() == "")
                {
                    MessageBox.Show("��������" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGridLis[i, t_ItemID].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGridLis[i, t_Count].ToString();
                AccordRecipePlus_VO.SampleID = this.m_objViewer.ctlDataGridLis[i, t_Temp].ToString();
                AccordRecipePlus_VO.Type = "1";
                AccordRecipePlus_VO.Class = "3";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }

            //���
            for (int i = 0; i < this.m_objViewer.ctlDataGridTest.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGridTest[i, t_Count].ToString().Trim() == "" || this.m_objViewer.ctlDataGridTest[i, t_Count].ToString().Trim() == "0")
                {
                    MessageBox.Show("�������" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (this.m_objViewer.ctlDataGridTest[i, t_PartName].ToString().Trim() == "")
                {
                    MessageBox.Show("�������" + Convert.ToString(i + 1) + "�еĲ�λ���벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGridTest[i, t_ItemID].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGridTest[i, t_Count].ToString();
                AccordRecipePlus_VO.PartID = this.m_objViewer.ctlDataGridTest[i, t_Temp].ToString();
                AccordRecipePlus_VO.Type = "1";
                AccordRecipePlus_VO.Class = "4";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }

            //����(����)
            for (int i = 0; i < this.m_objViewer.ctlDataGridOps.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGridOps[i, o_Count].ToString().Trim() == "" || this.m_objViewer.ctlDataGridOps[i, o_Count].ToString().Trim() == "0")
                {
                    MessageBox.Show("����(����)����" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGridOps[i, o_ItemID].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGridOps[i, o_Count].ToString();
                AccordRecipePlus_VO.Type = "1";
                AccordRecipePlus_VO.Class = "5";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }

            //����
            for (int i = 0; i < this.m_objViewer.ctlDataGrid6.RowCount; i++)
            {
                if (this.m_objViewer.ctlDataGrid6[i, o_Count].ToString().Trim() == "" || this.m_objViewer.ctlDataGrid6[i, o_Count].ToString().Trim() == "0")
                {
                    MessageBox.Show("��������" + Convert.ToString(i + 1) + "�е��������벻��ȷ�����������롣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsAccordRecipePlus_VO AccordRecipePlus_VO = new clsAccordRecipePlus_VO();
                AccordRecipePlus_VO.ItemID = this.m_objViewer.ctlDataGrid6[i, o_ItemID].ToString();
                AccordRecipePlus_VO.Qty = this.m_objViewer.ctlDataGrid6[i, o_Count].ToString();
                AccordRecipePlus_VO.Type = "0";
                AccordRecipePlus_VO.Class = "6";
                AccordRecipePlus_VO.RowNO = Convert.ToString(i + 1);

                objRecEntryArr.Add(AccordRecipePlus_VO);
            }
            #endregion

            if (objRecEntryArr.Count == 0)
            {
                MessageBox.Show("�����봦����ϸ��Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string PrivilegeFlag = "";
            if (this.m_objViewer.cboUseScope.SelectedIndex == 0)
            {
                PrivilegeFlag = "1";
            }
            else if (this.m_objViewer.cboUseScope.SelectedIndex == 1)
            {
                PrivilegeFlag = "2";
            }
            else if (this.m_objViewer.cboUseScope.SelectedIndex == 2)
            {
                PrivilegeFlag = "0";
            }

            string RecipeID = this.m_objViewer.btnSave.Tag.ToString();
            string RecipeID_Tmp = RecipeID;
            clsAccordRecipe_VO AccordRecipe_VO = new clsAccordRecipe_VO();
            AccordRecipe_VO.RecipeID = RecipeID;
            AccordRecipe_VO.RecipeName = this.m_objViewer.txtRecName.Text.Trim();
            AccordRecipe_VO.Privilege = PrivilegeFlag;
            AccordRecipe_VO.UserCode = this.m_objViewer.txtUserCode.Text.Trim();
            AccordRecipe_VO.PyCode = this.m_objViewer.txtPyCode.Text.Trim();
            AccordRecipe_VO.WbCode = this.m_objViewer.txtWbCode.Text.Trim();
            if (this.m_objViewer.cboStatus.SelectedIndex == 0)
            {
                AccordRecipe_VO.Status = "1";
            }
            else
            {
                AccordRecipe_VO.Status = "0";
            }
            AccordRecipe_VO.DiseaseName = this.m_objViewer.txtReMark.Text.Trim();
            AccordRecipe_VO.CreaterID = this.m_objViewer.LoginInfo.m_strEmpID;
            AccordRecipe_VO.Flag = "0";
            AccordRecipe_VO.DeptID = this.m_objViewer.LoginInfo.m_strDepartmentID;

            this.m_objViewer.Cursor = Cursors.WaitCursor;
            long l = this.objSvc.m_lngSaveAccordRecipe(AccordRecipe_VO, objRecEntryArr, out RecipeID);
            this.m_objViewer.Cursor = Cursors.Default;
            if (l > 0)
            {
                this.m_objViewer.btnSave.Tag = RecipeID;

                if (RecipeID_Tmp.Trim() == "")
                {
                    this.m_mthCreateTree(this.m_objViewer.LoginInfo.m_strEmpID);
                    this.m_mthFindChild(RecipeID);
                }
                else
                {
                    AccordRecipeEdit obj = new AccordRecipeEdit();
                    obj.RecipeID_Chr = AccordRecipe_VO.RecipeID;
                    obj.Recipename_Chr = AccordRecipe_VO.RecipeName;
                    obj.UserCode_Chr = AccordRecipe_VO.UserCode;
                    obj.PyCode_Chr = AccordRecipe_VO.PyCode;
                    obj.WbCode_Chr = AccordRecipe_VO.WbCode;
                    obj.Privilege_Int = AccordRecipe_VO.Privilege;
                    obj.Status_Int = AccordRecipe_VO.Status;
                    obj.ReMark_Vchr = AccordRecipe_VO.DiseaseName;
                    obj.OrigeName = AccordRecipe_VO.RecipeName;
                    this.m_objViewer.CurrTn.Tag = obj;
                }

                #region ��ʾ
                frmFlash flash = new frmFlash();
                flash.Information = "����Э���������ݳɹ���";
                Point p = this.m_objViewer.btnSave.Parent.Parent.PointToScreen(this.m_objViewer.btnSave.Location);
                p.Offset(-50, -(flash.Height - this.m_objViewer.btnSave.Height));
                flash.Location = p;
                flash.Show();
                #endregion

            }
            else
            {
                MessageBox.Show("����Э����������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ɾ��
        /// <summary>
        /// ɾ��
        /// </summary>
        public void m_mthDel(string RecipeID)
        {
            if (RecipeID.Trim() == "")
            {
                return;
            }

            if (MessageBox.Show("ɾ������ǰ���ٴ�ȷ�ϣ��Ƿ�ɾ����", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            long l = this.objSvc.m_lngDelAccordRecipe(RecipeID);
            if (l > 0)
            {
                this.m_mthCreateTree(this.m_objViewer.LoginInfo.m_strEmpID);
                this.m_mthNew();
            }
            else
            {
                MessageBox.Show("ɾ��Э������ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region ��ʾ
        /// <summary>
        /// ��ʾ
        /// </summary>
        public void m_mthShow(string RecipeID)
        {
            if (RecipeID.Trim() == "")
            {
                return;
            }

            this.m_objViewer.btnSave.Tag = RecipeID;
            this.m_mthDeleteAllRows();

            int row = 0;
            int tabindex = 0;
            long l = 0;
            DataTable dt;
            DataRow dr;

            #region ����
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 6, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGrid6.RowCount;
                    this.m_objViewer.ctlDataGrid6.m_mthAppendRow();

                    dr = dt.Rows[i];
                    m_objViewer.ctlDataGrid6[row, o_Find] = dr["ITEMCODE_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_Name] = dr["ITEMNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_Spec] = dr["ITEMSPEC_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_Unit] = dr["ITEMOPUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_Price] = dr["ITEMPRICE_MNY"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_ItemID] = dr["ITEMID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_PriceFlag] = dr["SELFDEFINE_INT"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_DiscountName] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
                    m_objViewer.ctlDataGrid6[row, o_Discount] = dr["PRECENT_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_InvoiceType] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_EnglishName] = dr["ITEMENGNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid6[row, o_Count] = dr["recqty"].ToString().Trim();

                    m_objViewer.ctlDataGrid6.CurrentCell = new DataGridCell(row, o_Count);
                    tabindex = 5;
                }
            }
            #endregion

            #region ����
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 5, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGridOps.RowCount;
                    this.m_objViewer.ctlDataGridOps.m_mthAppendRow();

                    dr = dt.Rows[i];
                    m_objViewer.ctlDataGridOps[row, o_Find] = dr["USERCODE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_Name] = dr["NAME_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_Spec] = dr["itemspec_vchr"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_Unit] = dr["itemunit"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_Price] = "";
                    m_objViewer.ctlDataGridOps[row, o_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_PriceFlag] = dr["APPLYTYPEID_CHR"].ToString().Trim();  //���Զ���۸��д����뵥����
                    m_objViewer.ctlDataGridOps[row, o_DiscountName] = "";//����
                    m_objViewer.ctlDataGridOps[row, o_Discount] = "";
                    m_objViewer.ctlDataGridOps[row, o_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
                    m_objViewer.ctlDataGridOps[row, o_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_Count] = dr["recqty"].ToString().Trim();
                    m_objViewer.ctlDataGridOps[row, o_UsageID] = dr["usageid_chr"].ToString().Trim();

                    m_objViewer.ctlDataGridOps.CurrentCell = new DataGridCell(row, o_Count);
                    tabindex = 4;
                }
            }
            #endregion

            #region ���
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 4, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGridTest.RowCount;
                    this.m_objViewer.ctlDataGridTest.m_mthAppendRow();

                    dr = dt.Rows[i];
                    m_objViewer.ctlDataGridTest[row, t_Find] = dr["USERCODE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Name] = dr["NAME_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Spec] = dr["itemspec_vchr"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_PartName] = dr["partname"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Unit] = dr["itemunit"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Price] = "";
                    m_objViewer.ctlDataGridTest[row, t_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_PriceFlag] = dr["APPLYTYPEID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_DiscountName] = "";//����
                    m_objViewer.ctlDataGridTest[row, t_Discount] = "";
                    m_objViewer.ctlDataGridTest[row, t_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
                    m_objViewer.ctlDataGridTest[row, t_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Temp] = dr["partid_vchr"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_Count] = dr["recqty"].ToString().Trim();
                    m_objViewer.ctlDataGridTest[row, t_UsageID] = dr["usageid_chr"].ToString().Trim();

                    m_objViewer.ctlDataGridTest.CurrentCell = new DataGridCell(row, t_Count);
                    tabindex = 3;
                }
            }
            #endregion

            #region ����
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 3, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGridLis.RowCount;
                    this.m_objViewer.ctlDataGridLis.m_mthAppendRow();

                    dr = dt.Rows[i];
                    m_objViewer.ctlDataGridLis[row, t_Find] = dr["USERCODE_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Name] = dr["NAME_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Spec] = dr["itemspec_vchr"].ToString().Trim();//���
                    m_objViewer.ctlDataGridLis[row, t_PartName] = dr["SAMPLE_TYPE_DESC_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Unit] = dr["itemunit"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_ItemID] = dr["ORDERDICID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_PriceFlag] = dr["LISAPPLYUNITID_CHR"].ToString().Trim(); //���Զ��۸��м�¼���뵥ԪID        
                    m_objViewer.ctlDataGridLis[row, t_InvoiceType] = this.m_mthConvertToChType(dr["ITEMOPINVTYPE_CHR"].ToString());
                    m_objViewer.ctlDataGridLis[row, t_EnglishName] = dr["ENGNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Temp] = dr["SAMPLE_TYPE_ID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGridLis[row, t_Count] = dr["recqty"].ToString().Trim();

                    m_objViewer.ctlDataGridLis.CurrentCell = new DataGridCell(row, t_Count);
                    tabindex = 2;
                }
            }
            #endregion

            #region ��ҩ
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 2, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGrid2.RowCount;
                    this.m_objViewer.ctlDataGrid2.m_mthAppendRow();

                    dr = dt.Rows[i];
                    m_objViewer.ctlDataGrid2[row, 0] = dr["ITEMCODE_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 1] = dr["recqty"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 11] = dr["ADULTDOSAGE_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 2] = dr["DOSAGEUNIT_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 3] = dr["ITEMNAME_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 4] = dr["ITEMSPEC_VCHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 5] = dr["usagename_vchr"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 6] = dr["SubMoney"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 8] = dr["ITEMID_CHR"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 10] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
                    m_objViewer.ctlDataGrid2[row, 11] = dr["PRECENT_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 13] = dr["MAXDOSAGE_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 14] = dr["MINDOSAGE_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 16] = dr["ITEMPRICE_MNY"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 17] = dr["opchargeflg_int"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 18] = dr["PACKQTY_DEC"].ToString().Trim();
                    m_objViewer.ctlDataGrid2[row, 19] = 0;
                    m_objViewer.ctlDataGrid2[row, 20] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();//��Ʊ����
                    m_objViewer.ctlDataGrid2[row, 21] = dr["USAGEID_CHR"].ToString().Trim();    //�÷�ID
                    m_objViewer.ctlDataGrid2[row, 24] = dr["ITEMENGNAME_VCHR"].ToString().Trim();//Ӣ����

                    try
                    {
                        m_objViewer.ctlDataGrid2.CurrentCell = new DataGridCell(row, 1);
                        m_objViewer.ctlDataGrid2[row, 0] = dr["ITEMCODE_VCHR"].ToString().Trim();
                    }
                    catch
                    {
                    }
                    tabindex = 1;
                }
            }
            #endregion

            #region ��ҩ
            l = this.objSvc.m_lngGetAccordRecipe(RecipeID, 1, out dt);
            if (l > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = this.m_objViewer.ctlDataGrid1.RowCount;
                    this.m_objViewer.ctlDataGrid1.m_mthAppendRow();

                    dr = dt.Rows[i];
                    this.m_objViewer.ctlDataGrid1[row, c_GroupNo] = dr["recno"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Find] = dr["ITEMCODE_VCHR"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Count] = dr["recqty"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Unit] = dr["DOSAGEUNIT_CHR"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Name] = dr["ITEMNAME_VCHR"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Spec] = dr["ITEMSPEC_VCHR"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Price] = dr["SubMoney"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_BigUnit] = dr["ITEMIPUNIT_CHR"].ToString().Trim();
                    if (dr["opchargeflg_int"].ToString().Trim() == "0")//�жϴ�С��λ
                    {
                        this.m_objViewer.ctlDataGrid1[row, c_Price] = dr["ITEMPRICE_MNY"].ToString().Trim();//�󵥼�
                        this.m_objViewer.ctlDataGrid1[row, c_BigUnit] = dr["ITEMOPUNIT_CHR"].ToString().Trim();
                    }
                    this.m_objViewer.ctlDataGrid1[row, c_ItemID] = dr["ITEMID_CHR"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Packet] = dr["PACKQTY_DEC"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_DiscountName] = dr["PRECENT_DEC"].ToString().Trim() + "%";//����
                    this.m_objViewer.ctlDataGrid1[row, c_Discount] = dr["PRECENT_DEC"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_UnitFlag] = dr["opchargeflg_int"].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_Dosage] = m_mthConvertObjToDecimal(dr["DOSAGE_DEC"]);
                    this.m_objViewer.ctlDataGrid1[row, c_MaxLimit] = dr["MAXDOSAGE_DEC"].ToString().Trim();//����
                    this.m_objViewer.ctlDataGrid1[row, c_MinLimit] = dr["MINDOSAGE_DEC"].ToString().Trim();//����
                    this.m_objViewer.ctlDataGrid1[row, c_IsCal] = 1;
                    this.m_objViewer.ctlDataGrid1[row, c_InvoiceType] = dr["ITEMOPINVTYPE_CHR"].ToString().Trim();//��Ʊ����
                    string tempUsageID = m_objViewer.ctlDataGrid1[row, 16].ToString().Trim();
                    this.m_objViewer.ctlDataGrid1[row, c_UsageName] = dr["USAGENAME_VCHR"].ToString().Trim();//Ĭ���÷�����
                    this.m_objViewer.ctlDataGrid1[row, c_UsageID] = dr["USAGEID_CHR"].ToString().Trim();//Ĭ���÷�ID
                    this.m_objViewer.ctlDataGrid1[row, c_EnglishName] = dr["ITEMENGNAME_VCHR"].ToString().Trim();//Ӣ����			

                    this.m_objViewer.ctlDataGrid1[row, c_FreName] = dr["freqname"].ToString();
                    this.m_objViewer.ctlDataGrid1[row, c_FreDays] = m_mthConvertObjToDecimal(dr["freqdays"]);
                    this.m_objViewer.ctlDataGrid1[row, c_FreTimes] = m_mthConvertObjToDecimal(dr["freqtimes"]);
                    this.m_objViewer.ctlDataGrid1[row, c_FreID] = dr["freqid"].ToString();
                    this.m_objViewer.ctlDataGrid1[row, c_Day] = dr["recdays"].ToString();

                    try
                    {
                        this.m_objViewer.ctlDataGrid1[row, c_PSFlag] = dr["HYPE_INT"].ToString().Trim();//Ƥ�Ա�־
                        if (dr["HYPE_INT"].ToString().Trim() == "1")//�����ҩҪƤ����Ĭ��ΪƤ��
                        {
                            m_objViewer.ctlDataGrid1[row, c_PS] = 1;
                            this.m_objViewer.ctlDataGrid1.m_mthFormatCell(row, c_UsageName, this.m_objViewer.ctlDataGrid1.Font, Color.White, Color.Red);
                        }
                        else
                        {
                            this.m_objViewer.ctlDataGrid1.m_mthFormatCell(row, c_UsageName, this.m_objViewer.ctlDataGrid1.Font, Color.White, Color.Black);
                        }
                    }
                    catch
                    {

                    }
                    tabindex = 0;
                }
            }
            #endregion

            this.m_objViewer.tabControl1.SelectedIndex = tabindex;
        }
        #endregion

        #region ����Э������
        /// <summary>
        /// ����Э������
        /// </summary>
        public void m_mthCreateAccordRecipe()
        {
            int row = 0;
            #region ��ҩ
            for (int i = 0; i < this.m_objViewer.DataGrid1.RowCount; i++)
            {
                if ((this.m_objViewer.DataGrid1[i, c_SubItemID].ToString().Trim() != "" && !this.m_objViewer.DataGrid1[i, c_SubItemID].ToString().Trim().StartsWith("[PK]"))
                    || (this.m_objViewer.DataGrid1[i, c_resubitem].ToString().Trim() != "" && this.m_objViewer.DataGrid1[i, c_resubitem].ToString().Trim().StartsWith("[PK]")))
                {
                    continue;
                }

                this.m_objViewer.ctlDataGrid1.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGrid1.RowCount - 1;

                this.m_objViewer.ctlDataGrid1[row, c_Find] = this.m_objViewer.DataGrid1[i, c_Find];
                this.m_objViewer.ctlDataGrid1[row, c_Count] = this.m_objViewer.DataGrid1[i, c_Count];
                this.m_objViewer.ctlDataGrid1[row, c_Unit] = this.m_objViewer.DataGrid1[i, c_Unit];
                this.m_objViewer.ctlDataGrid1[row, c_Name] = this.m_objViewer.DataGrid1[i, c_Name];
                this.m_objViewer.ctlDataGrid1[row, c_Spec] = this.m_objViewer.DataGrid1[i, c_Spec];

                this.m_objViewer.ctlDataGrid1[row, c_Price] = this.m_objViewer.DataGrid1[i, c_Price];
                this.m_objViewer.ctlDataGrid1[row, c_BigUnit] = this.m_objViewer.DataGrid1[i, c_BigUnit];

                this.m_objViewer.ctlDataGrid1[row, c_ItemID] = this.m_objViewer.DataGrid1[i, c_ItemID];
                this.m_objViewer.ctlDataGrid1[row, c_Packet] = this.m_objViewer.DataGrid1[i, c_Packet];
                this.m_objViewer.ctlDataGrid1[row, c_DiscountName] = this.m_objViewer.DataGrid1[i, c_DiscountName];//����
                this.m_objViewer.ctlDataGrid1[row, c_Discount] = this.m_objViewer.DataGrid1[i, c_Discount];
                this.m_objViewer.ctlDataGrid1[row, c_UnitFlag] = this.m_objViewer.DataGrid1[i, c_UnitFlag];
                this.m_objViewer.ctlDataGrid1[row, c_Dosage] = this.m_objViewer.DataGrid1[i, c_Dosage];
                this.m_objViewer.ctlDataGrid1[row, c_MaxLimit] = this.m_objViewer.DataGrid1[i, c_MaxLimit];//����
                this.m_objViewer.ctlDataGrid1[row, c_MinLimit] = this.m_objViewer.DataGrid1[i, c_MinLimit];//����
                this.m_objViewer.ctlDataGrid1[row, c_IsCal] = this.m_objViewer.DataGrid1[i, c_IsCal];
                this.m_objViewer.ctlDataGrid1[row, c_InvoiceType] = this.m_objViewer.DataGrid1[i, c_InvoiceType];//��Ʊ����

                this.m_objViewer.ctlDataGrid1[row, c_UsageName] = this.m_objViewer.DataGrid1[i, c_UsageName];//Ĭ���÷�����
                this.m_objViewer.ctlDataGrid1[row, c_UsageID] = this.m_objViewer.DataGrid1[i, c_UsageID];//Ĭ���÷�ID
                this.m_objViewer.ctlDataGrid1[row, c_EnglishName] = this.m_objViewer.DataGrid1[i, c_EnglishName];//Ӣ����			

                this.m_objViewer.ctlDataGrid1[row, c_FreName] = this.m_objViewer.DataGrid1[i, c_FreName];
                this.m_objViewer.ctlDataGrid1[row, c_FreDays] = this.m_objViewer.DataGrid1[i, c_FreDays];
                this.m_objViewer.ctlDataGrid1[row, c_FreTimes] = this.m_objViewer.DataGrid1[i, c_FreTimes];
                this.m_objViewer.ctlDataGrid1[row, c_FreID] = this.m_objViewer.DataGrid1[i, c_FreID];

                this.m_objViewer.ctlDataGrid1[row, c_PSFlag] = this.m_objViewer.DataGrid1[i, c_PSFlag];//Ƥ�Ա�־
                this.m_objViewer.ctlDataGrid1[row, c_PS] = this.m_objViewer.DataGrid1[i, c_PS];

                this.m_objViewer.ctlDataGrid1[row, c_Deptmed] = "";
                this.m_objViewer.ctlDataGrid1[row, c_DeptmedID] = "";

                this.m_objViewer.ctlDataGrid1[row, c_Find] = this.m_objViewer.DataGrid1[i, c_Find];
                this.m_objViewer.ctlDataGrid1[row, c_IsMain] = this.m_objViewer.DataGrid1[i, c_IsMain];

                this.m_objViewer.ctlDataGrid1[row, c_GroupNo] = this.m_objViewer.DataGrid1[i, c_GroupNo];
                this.m_objViewer.ctlDataGrid1[row, c_Day] = this.m_objViewer.DataGrid1[i, c_Day];
                this.m_objViewer.ctlDataGrid1[row, c_SumMoney] = this.m_objViewer.DataGrid1[i, c_SumMoney];
                this.m_objViewer.ctlDataGrid1[row, c_Total] = this.m_objViewer.DataGrid1[i, c_Total];
            }
            #endregion

            #region ��ҩ
            for (int i = 0; i < this.m_objViewer.DataGrid2.RowCount; i++)
            {
                if ((this.m_objViewer.DataGrid2[i, 22].ToString().Trim() != "" && !this.m_objViewer.DataGrid2[i, 22].ToString().Trim().StartsWith("[PK]"))
                    || (this.m_objViewer.DataGrid2[i, cm_resubitem].ToString().Trim() != "" && !this.m_objViewer.DataGrid2[i, cm_resubitem].ToString().Trim().StartsWith("[PK]")))
                {
                    continue;
                }

                this.m_objViewer.ctlDataGrid2.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGrid2.RowCount - 1;

                this.m_objViewer.ctlDataGrid2[row, 0] = this.m_objViewer.DataGrid2[i, 0];

                this.m_objViewer.ctlDataGrid2[row, 1] = this.m_objViewer.DataGrid2[i, 1];
                this.m_objViewer.ctlDataGrid2[row, 11] = this.m_objViewer.DataGrid2[i, 11];

                this.m_objViewer.ctlDataGrid2[row, 2] = this.m_objViewer.DataGrid2[i, 2];
                this.m_objViewer.ctlDataGrid2[row, 3] = this.m_objViewer.DataGrid2[i, 3];
                this.m_objViewer.ctlDataGrid2[row, 4] = this.m_objViewer.DataGrid2[i, 4];
                this.m_objViewer.ctlDataGrid2[row, 5] = this.m_objViewer.DataGrid2[i, 5];
                this.m_objViewer.ctlDataGrid2[row, 6] = this.m_objViewer.DataGrid2[i, 6];
                this.m_objViewer.ctlDataGrid2[row, 7] = this.m_objViewer.DataGrid2[i, 7];
                this.m_objViewer.ctlDataGrid2[row, 8] = this.m_objViewer.DataGrid2[i, 8];
                this.m_objViewer.ctlDataGrid2[row, 10] = this.m_objViewer.DataGrid2[i, 10];//����
                this.m_objViewer.ctlDataGrid2[row, 11] = this.m_objViewer.DataGrid2[i, 11];
                this.m_objViewer.ctlDataGrid2[row, 13] = this.m_objViewer.DataGrid2[i, 13];
                this.m_objViewer.ctlDataGrid2[row, 14] = this.m_objViewer.DataGrid2[i, 14];
                this.m_objViewer.ctlDataGrid2[row, 16] = this.m_objViewer.DataGrid2[i, 16];
                this.m_objViewer.ctlDataGrid2[row, 17] = this.m_objViewer.DataGrid2[i, 17];
                this.m_objViewer.ctlDataGrid2[row, 18] = this.m_objViewer.DataGrid2[i, 18];
                this.m_objViewer.ctlDataGrid2[row, 19] = this.m_objViewer.DataGrid2[i, 19];
                this.m_objViewer.ctlDataGrid2[row, 20] = this.m_objViewer.DataGrid2[i, 20];//��Ʊ����
                this.m_objViewer.ctlDataGrid2[row, 21] = this.m_objViewer.DataGrid2[i, 21];  //�÷�ID
                this.m_objViewer.ctlDataGrid2[row, 24] = this.m_objViewer.DataGrid2[i, 24];//Ӣ����

                this.m_objViewer.ctlDataGrid2[row, 0] = this.m_objViewer.DataGrid2[i, 0];
            }
            #endregion

            #region ����
            for (int i = 0; i < this.m_objViewer.DataGridLis.RowCount; i++)
            {
                this.m_objViewer.ctlDataGridLis.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGridLis.RowCount - 1;

                this.m_objViewer.ctlDataGridLis[row, t_Find] = this.m_objViewer.DataGridLis[i, t_Find];
                this.m_objViewer.ctlDataGridLis[row, t_Name] = this.m_objViewer.DataGridLis[i, t_Name];
                this.m_objViewer.ctlDataGridLis[row, t_Spec] = this.m_objViewer.DataGridLis[i, t_Spec];//���
                this.m_objViewer.ctlDataGridLis[row, t_PartName] = this.m_objViewer.DataGridLis[i, t_PartName];
                this.m_objViewer.ctlDataGridLis[row, t_Unit] = this.m_objViewer.DataGridLis[i, t_Unit];
                this.m_objViewer.ctlDataGridLis[row, t_ItemID] = this.m_objViewer.DataGridLis[i, t_ItemID];
                this.m_objViewer.ctlDataGridLis[row, t_PriceFlag] = this.m_objViewer.DataGridLis[i, t_PriceFlag]; //���Զ��۸��м�¼���뵥ԪID        
                this.m_objViewer.ctlDataGridLis[row, t_InvoiceType] = this.m_objViewer.DataGridLis[i, t_InvoiceType];
                this.m_objViewer.ctlDataGridLis[row, t_EnglishName] = this.m_objViewer.DataGridLis[i, t_EnglishName];
                this.m_objViewer.ctlDataGridLis[row, t_Temp] = this.m_objViewer.DataGridLis[i, t_Temp];
                this.m_objViewer.ctlDataGridLis[row, t_Count] = this.m_objViewer.DataGridLis[i, t_Count];

                this.m_objViewer.ctlDataGridLis[row, t_Price] = this.m_objViewer.DataGridLis[i, t_Price];
                this.m_objViewer.ctlDataGridLis[row, t_SumMoney] = this.m_objViewer.DataGridLis[i, t_SumMoney];
            }
            #endregion

            #region ���
            for (int i = 0; i < this.m_objViewer.DataGridTest.RowCount; i++)
            {
                this.m_objViewer.ctlDataGridTest.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGridTest.RowCount - 1;

                this.m_objViewer.ctlDataGridTest[row, t_Find] = this.m_objViewer.DataGridTest[i, t_Find];
                this.m_objViewer.ctlDataGridTest[row, t_Name] = this.m_objViewer.DataGridTest[i, t_Name];
                this.m_objViewer.ctlDataGridTest[row, t_Spec] = this.m_objViewer.DataGridTest[i, t_Spec];
                this.m_objViewer.ctlDataGridTest[row, t_PartName] = this.m_objViewer.DataGridTest[i, t_PartName];
                this.m_objViewer.ctlDataGridTest[row, t_Unit] = this.m_objViewer.DataGridTest[i, t_Unit];
                this.m_objViewer.ctlDataGridTest[row, t_Price] = this.m_objViewer.DataGridTest[i, t_Price];
                this.m_objViewer.ctlDataGridTest[row, t_ItemID] = this.m_objViewer.DataGridTest[i, t_ItemID];
                this.m_objViewer.ctlDataGridTest[row, t_PriceFlag] = this.m_objViewer.DataGridTest[i, t_PriceFlag];
                this.m_objViewer.ctlDataGridTest[row, t_DiscountName] = this.m_objViewer.DataGridTest[i, t_DiscountName];//����
                this.m_objViewer.ctlDataGridTest[row, t_Discount] = this.m_objViewer.DataGridTest[i, t_Discount];
                this.m_objViewer.ctlDataGridTest[row, t_InvoiceType] = this.m_objViewer.DataGridTest[i, t_InvoiceType];
                this.m_objViewer.ctlDataGridTest[row, t_EnglishName] = this.m_objViewer.DataGridTest[i, t_EnglishName];
                this.m_objViewer.ctlDataGridTest[row, t_Temp] = this.m_objViewer.DataGridTest[i, t_Temp];
                this.m_objViewer.ctlDataGridTest[row, t_Count] = this.m_objViewer.DataGridTest[i, t_Count];
                this.m_objViewer.ctlDataGridTest[row, t_UsageID] = this.m_objViewer.DataGridTest[i, t_UsageID];

                this.m_objViewer.ctlDataGridTest[row, t_SumMoney] = this.m_objViewer.DataGridTest[i, t_SumMoney];
            }
            #endregion

            #region ����(����)
            for (int i = 0; i < this.m_objViewer.DataGridOps.RowCount; i++)
            {
                this.m_objViewer.ctlDataGridOps.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGridOps.RowCount - 1;

                this.m_objViewer.ctlDataGridOps[row, o_Find] = this.m_objViewer.DataGridOps[i, o_Find];
                this.m_objViewer.ctlDataGridOps[row, o_Name] = this.m_objViewer.DataGridOps[i, o_Name];
                this.m_objViewer.ctlDataGridOps[row, o_Spec] = this.m_objViewer.DataGridOps[i, o_Spec];
                this.m_objViewer.ctlDataGridOps[row, o_Unit] = this.m_objViewer.DataGridOps[i, o_Unit];
                this.m_objViewer.ctlDataGridOps[row, o_Price] = this.m_objViewer.DataGridOps[i, o_Price];
                this.m_objViewer.ctlDataGridOps[row, o_ItemID] = this.m_objViewer.DataGridOps[i, o_ItemID];
                this.m_objViewer.ctlDataGridOps[row, o_PriceFlag] = this.m_objViewer.DataGridOps[i, o_PriceFlag]; //���Զ���۸��д����뵥����
                this.m_objViewer.ctlDataGridOps[row, o_DiscountName] = this.m_objViewer.DataGridOps[i, o_DiscountName];//����
                this.m_objViewer.ctlDataGridOps[row, o_Discount] = this.m_objViewer.DataGridOps[i, o_Discount];
                this.m_objViewer.ctlDataGridOps[row, o_InvoiceType] = this.m_objViewer.DataGridOps[i, o_InvoiceType];
                this.m_objViewer.ctlDataGridOps[row, o_EnglishName] = this.m_objViewer.DataGridOps[i, o_EnglishName];
                this.m_objViewer.ctlDataGridOps[row, o_Count] = this.m_objViewer.DataGridOps[i, o_Count];
                this.m_objViewer.ctlDataGridOps[row, o_UsageID] = this.m_objViewer.DataGridOps[i, o_UsageID];

                this.m_objViewer.ctlDataGridOps[row, o_SumMoney] = this.m_objViewer.DataGridOps[i, o_SumMoney];
            }
            #endregion

            #region ����(����)
            for (int i = 0; i < this.m_objViewer.DataGrid6.RowCount; i++)
            {
                if ((this.m_objViewer.DataGrid6[i, o_OtherItemID].ToString().Trim() != "" && !this.m_objViewer.DataGrid6[i, o_OtherItemID].ToString().Trim().StartsWith("[PK]"))
                    || (this.m_objViewer.DataGrid6[i, o_resubitem].ToString().Trim() != "" && !this.m_objViewer.DataGrid6[i, o_resubitem].ToString().Trim().StartsWith("[PK]")))
                {
                    continue;
                }

                this.m_objViewer.ctlDataGrid6.m_mthAppendRow();
                row = this.m_objViewer.ctlDataGrid6.RowCount - 1;

                this.m_objViewer.ctlDataGrid6[row, o_Find] = this.m_objViewer.DataGrid6[i, o_Find];
                this.m_objViewer.ctlDataGrid6[row, o_Name] = this.m_objViewer.DataGrid6[i, o_Name];
                this.m_objViewer.ctlDataGrid6[row, o_Spec] = this.m_objViewer.DataGrid6[i, o_Spec];
                this.m_objViewer.ctlDataGrid6[row, o_Unit] = this.m_objViewer.DataGrid6[i, o_Unit];
                this.m_objViewer.ctlDataGrid6[row, o_Price] = this.m_objViewer.DataGrid6[i, o_Price];
                this.m_objViewer.ctlDataGrid6[row, o_ItemID] = this.m_objViewer.DataGrid6[i, o_ItemID];
                this.m_objViewer.ctlDataGrid6[row, o_PriceFlag] = this.m_objViewer.DataGrid6[i, o_PriceFlag];
                this.m_objViewer.ctlDataGrid6[row, o_DiscountName] = this.m_objViewer.DataGrid6[i, o_DiscountName]; //����
                this.m_objViewer.ctlDataGrid6[row, o_Discount] = this.m_objViewer.DataGrid6[i, o_Discount];
                this.m_objViewer.ctlDataGrid6[row, o_InvoiceType] = this.m_objViewer.DataGrid6[i, o_InvoiceType];
                this.m_objViewer.ctlDataGrid6[row, o_EnglishName] = this.m_objViewer.DataGrid6[i, o_EnglishName];

                this.m_objViewer.ctlDataGrid6[row, o_Deptmed] = this.m_objViewer.DataGrid6[i, o_Deptmed];
                this.m_objViewer.ctlDataGrid6[row, o_DeptmedID] = this.m_objViewer.DataGrid6[i, o_DeptmedID];

                this.m_objViewer.ctlDataGrid6[row, o_Count] = this.m_objViewer.DataGrid6[i, o_Count];
                this.m_objViewer.ctlDataGrid6[row, o_SumMoney] = this.m_objViewer.DataGrid6[i, o_SumMoney];
            }
            #endregion
        }
        #endregion
    }

    #region Э�������Ƚ���
    /// <summary>
    /// Э�������Ƚ���
    /// </summary>
    public class AccordRecipeEdit : IComparable
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public string RecipeID_Chr = "";
        /// <summary>
        /// ����
        /// </summary>
        public string UserCode_Chr = "";
        /// <summary>
        /// ƴ����
        /// </summary>
        public string PyCode_Chr = "";
        /// <summary>
        /// �����
        /// </summary>
        public string WbCode_Chr = "";
        /// <summary>
        /// 1 ���� 2 ���� 0 ����
        /// </summary>
        public string Privilege_Int = "";
        /// <summary>
        /// 0 ͣ�� 1 ����
        /// </summary>
        public string Status_Int = "";
        /// <summary>
        /// ��ע
        /// </summary>
        public string ReMark_Vchr = "";
        /// <summary>
        /// �����
        /// </summary>
        private string OutputName;
        /// <summary>
        /// ԭʼ����
        /// </summary>
        public string OrigeName;
        /// <summary>
        /// ����ID
        /// </summary>
        public string IndexID;
        /// <summary>
        /// ����
        /// </summary>
        public string[] LevelNameArr;
        /// <summary>
        /// ������
        /// </summary>
        public string Recipename_Chr
        {
            set
            {
                OrigeName = value;
                LevelNameArr = value.Replace(">>", "-").Split('-');
                OutputName = LevelNameArr[LevelNameArr.Length - 1];
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < LevelNameArr.Length; i++)
                {
                    sb.Append(LevelNameArr[i]);
                }
                IndexID = sb.ToString();
            }
            get
            {
                return OutputName;
            }
        }

        /// <summary>
        /// ��дToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return OutputName;
        }
        #region IComparable ��Ա
        /// <summary>
        /// IComparable ��Ա
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            AccordRecipeEdit objTemp = obj as AccordRecipeEdit;
            return this.IndexID.CompareTo(objTemp.IndexID);
        }
        #endregion
    }
    #endregion
}
