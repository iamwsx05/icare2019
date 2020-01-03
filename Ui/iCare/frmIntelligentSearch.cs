using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using weCare.Core.Entity;

using System.Windows.Forms;


namespace iCare
{
    public class frmIntelligentSearch : iCare.frmPrimaryForm
    {
        private System.Windows.Forms.TreeView trvSearchType;
        protected System.Windows.Forms.Label m_lblForTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button m_cmdPerformQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView m_lsvQueryResult;
        private System.Windows.Forms.Label m_lblSearchDesc;
        private System.Data.DataSet dtsTemp;
        private System.Data.DataTable m_dtbSearchContent;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.DataGridBoolColumn dataGridBoolColumn1;
        private System.ComponentModel.IContainer components = null;

        public frmIntelligentSearch()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            //m_objBorderTool.m_mthChangedSubControlsBorder(this);

            trvSearchType.Nodes.Add(m_trnRoot);
        }

        private const string c_strSplitSelectedFieldSymbol = "��";

        #region Xml�ڵ����ƶ���
        /// <summary>
        /// ϵͳ��������
        /// </summary>
        private const string c_strFixedSystems = "FixedSystems";
        /// <summary>
        /// �û���������
        /// </summary>
        private const string c_strUserCondiction = "UserCondiction";
        /// <summary>
        /// ��ѯ����
        /// </summary>
        private const string c_strCondiction = "Condiction";
        /// <summary>
        /// ��ѯ�������������
        /// </summary>
        private const string c_strCondictionSymbol = "Symbol";
        /// <summary>
        /// ��ѯ������Ŀ
        /// </summary>
        private const string c_strItem = "Item";
        /// <summary>
        /// ��ѯ������Ŀ���ֶ�����
        /// </summary>
        private const string c_strItemOptionName = "OptionName";
        /// <summary>
        /// ��ѯ������Ŀ�Ĳ���
        /// </summary>
        private const string c_strItemSymbol = "Symbol";
        /// <summary>
        /// ��ѯ������Ŀ��Ĭ��ֵ
        /// </summary>
        private const string c_strItemDefaultValue = "DefaultValue";
        /// <summary>
        /// ��ѯ������Ŀ��Ĭ��ֵ
        /// </summary>
        private const string c_strItemIsUse = "IsUse";

        /// <summary>
        /// Like������
        /// </summary>
        private const string c_strLikeOperator = "like";
        #endregion Xml�ڵ����ƶ���

        /// <summary>
        /// ��ѯģ���б�ĸ��ڵ�
        /// </summary>
        private TreeNode m_trnRoot = new TreeNode("��ѯ����");

        /// <summary>
        /// ��ѯ�����Ĳ��������� != like ...
        /// </summary>
        private clsStatisticConditionOperatorValue[] m_objConditionOperatorArr = null;

        /// <summary>
        /// ��ѯ��������Ϲ�ϵ��and or
        /// </summary>
        private clsStatisticCCOperatorValue[] m_objCCOperatorArr = null;

        /// <summary>
        /// ����ͳ�Ƶ�Domain
        /// </summary>
        private clsIntelligentStatisticsDomain m_objDomain = new clsIntelligentStatisticsDomain();

        /// <summary>
        /// ��Ų�ѯ�����ж���Ŀ�����Ϊ��ʾ������ֶ���Ϣ��m_strStatistic_IDΪKey��clsStatisticSelectedFieldValue[]ΪValue��
        /// </summary>
        private Hashtable m_hasSelectedField = new Hashtable();
        /// <summary>
        /// ��Ų�ѯ�����ж���Ŀ�����Ϊ�������ֶ���Ϣ��m_strStatistic_IDΪKey��clsStatisticCondictionOptionValue[]ΪValue��
        /// </summary>
        private Hashtable m_hasConditionField = new Hashtable();

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmIntelligentSearch));
            this.trvSearchType = new System.Windows.Forms.TreeView();
            this.m_lblForTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_lblSearchDesc = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.m_dtbSearchContent = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtsTemp = new System.Data.DataSet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lsvQueryResult = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmdPerformQuery = new System.Windows.Forms.Button();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataGridBoolColumn1 = new System.Windows.Forms.DataGridBoolColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbSearchContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsTemp)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvSearchType
            // 
            this.trvSearchType.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.trvSearchType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvSearchType.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.trvSearchType.ForeColor = System.Drawing.SystemColors.Window;
            this.trvSearchType.HideSelection = false;
            this.trvSearchType.ImageIndex = -1;
            this.trvSearchType.ItemHeight = 18;
            this.trvSearchType.Location = new System.Drawing.Point(40, 68);
            this.trvSearchType.Name = "trvSearchType";
            this.trvSearchType.SelectedImageIndex = -1;
            this.trvSearchType.ShowRootLines = false;
            this.trvSearchType.Size = new System.Drawing.Size(260, 240);
            this.trvSearchType.TabIndex = 502;
            this.trvSearchType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSearchType_AfterSelect);
            this.trvSearchType.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvSearchType_BeforeExpand);
            // 
            // m_lblForTitle
            // 
            this.m_lblForTitle.Font = new System.Drawing.Font("SimSun", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_lblForTitle.Location = new System.Drawing.Point(324, 8);
            this.m_lblForTitle.Name = "m_lblForTitle";
            this.m_lblForTitle.Size = new System.Drawing.Size(416, 48);
            this.m_lblForTitle.TabIndex = 503;
            this.m_lblForTitle.Text = "�� �� �� ѯ";
            this.m_lblForTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.m_lblSearchDesc,
                                                                                    this.dataGrid1});
            this.groupBox1.Location = new System.Drawing.Point(316, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 252);
            this.groupBox1.TabIndex = 504;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "��ѯ����";
            // 
            // m_lblSearchDesc
            // 
            this.m_lblSearchDesc.Location = new System.Drawing.Point(16, 28);
            this.m_lblSearchDesc.Name = "m_lblSearchDesc";
            this.m_lblSearchDesc.Size = new System.Drawing.Size(632, 40);
            this.m_lblSearchDesc.TabIndex = 2;
            this.m_lblSearchDesc.Text = "��ѯ˵����";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.DataSource = this.m_dtbSearchContent;
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(16, 76);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(632, 160);
            this.dataGrid1.TabIndex = 1;
            this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
                                                                                                  this.dataGridTableStyle1});
            // 
            // m_dtbSearchContent
            // 
            this.m_dtbSearchContent.Columns.AddRange(new System.Data.DataColumn[] {
                                                                                      this.dataColumn1,
                                                                                      this.dataColumn2,
                                                                                      this.dataColumn3,
                                                                                      this.dataColumn4,
                                                                                      this.dataColumn5});
            this.m_dtbSearchContent.TableName = "SearchContent";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "����";
            this.dataColumn1.ColumnName = "ConditionName";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "����ֵ";
            this.dataColumn2.ColumnName = "ConditionValue";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "˵��";
            this.dataColumn3.ColumnName = "ConditionExplain";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "XmlNode";
            this.dataColumn4.DataType = typeof(object);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dataGrid1;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
                                                                                                                  this.dataGridTextBoxColumn1,
                                                                                                                  this.dataGridTextBoxColumn2,
                                                                                                                  this.dataGridBoolColumn1,
                                                                                                                  this.dataGridTextBoxColumn3});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "SearchContent";
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "����";
            this.dataGridTextBoxColumn1.MappingName = "ConditionName";
            this.dataGridTextBoxColumn1.NullText = "";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 120;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "����ֵ";
            this.dataGridTextBoxColumn2.MappingName = "ConditionValue";
            this.dataGridTextBoxColumn2.NullText = "";
            this.dataGridTextBoxColumn2.Width = 120;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "˵��";
            this.dataGridTextBoxColumn3.MappingName = "ConditionExplain";
            this.dataGridTextBoxColumn3.NullText = "";
            this.dataGridTextBoxColumn3.ReadOnly = true;
            this.dataGridTextBoxColumn3.Width = 300;
            // 
            // dtsTemp
            // 
            this.dtsTemp.DataSetName = "TempDTS";
            this.dtsTemp.Locale = new System.Globalization.CultureInfo("zh-CN");
            this.dtsTemp.Tables.AddRange(new System.Data.DataTable[] {
                                                                         this.m_dtbSearchContent});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                    this.m_lsvQueryResult,
                                                                                    this.label1,
                                                                                    this.m_cmdPerformQuery});
            this.groupBox2.Location = new System.Drawing.Point(40, 324);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 304);
            this.groupBox2.TabIndex = 505;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "��ѯ���";
            // 
            // m_lsvQueryResult
            // 
            this.m_lsvQueryResult.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_lsvQueryResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsvQueryResult.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_lsvQueryResult.ForeColor = System.Drawing.Color.White;
            this.m_lsvQueryResult.FullRowSelect = true;
            this.m_lsvQueryResult.GridLines = true;
            this.m_lsvQueryResult.Location = new System.Drawing.Point(16, 76);
            this.m_lsvQueryResult.MultiSelect = false;
            this.m_lsvQueryResult.Name = "m_lsvQueryResult";
            this.m_lsvQueryResult.Size = new System.Drawing.Size(904, 208);
            this.m_lsvQueryResult.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lsvQueryResult.TabIndex = 171;
            this.m_lsvQueryResult.View = System.Windows.Forms.View.Details;
            this.m_lsvQueryResult.DoubleClick += new System.EventHandler(this.m_lsvQueryResult_DoubleClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(88, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 23);
            this.label1.TabIndex = 142;
            this.label1.Text = "��ʾ��˫��������鿴��������";
            // 
            // m_cmdPerformQuery
            // 
            this.m_cmdPerformQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdPerformQuery.Location = new System.Drawing.Point(16, 32);
            this.m_cmdPerformQuery.Name = "m_cmdPerformQuery";
            this.m_cmdPerformQuery.Size = new System.Drawing.Size(64, 32);
            this.m_cmdPerformQuery.TabIndex = 141;
            this.m_cmdPerformQuery.Text = "��ѯ";
            this.m_cmdPerformQuery.Click += new System.EventHandler(this.m_cmdPerformQuery_Click);
            // 
            // dataColumn5
            // 
            this.dataColumn5.AllowDBNull = false;
            this.dataColumn5.ColumnName = "IsUse";
            this.dataColumn5.DataType = typeof(bool);
            this.dataColumn5.DefaultValue = false;
            // 
            // dataGridBoolColumn1
            // 
            this.dataGridBoolColumn1.AllowNull = false;
            this.dataGridBoolColumn1.FalseValue = false;
            this.dataGridBoolColumn1.HeaderText = "ʹ��";
            this.dataGridBoolColumn1.MappingName = "IsUse";
            this.dataGridBoolColumn1.NullText = "";
            this.dataGridBoolColumn1.NullValue = ((System.DBNull)(resources.GetObject("dataGridBoolColumn1.NullValue")));
            this.dataGridBoolColumn1.TrueValue = true;
            this.dataGridBoolColumn1.Width = 60;
            // 
            // frmIntelligentSearch
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(1016, 733);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.groupBox1,
                                                                          this.groupBox2,
                                                                          this.m_lblForTitle,
                                                                          this.trvSearchType});
            this.Name = "frmIntelligentSearch";
            this.Text = "���ܲ�ѯ";
            this.Load += new System.EventHandler(this.frmIntelligentSearch_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtbSearchContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtsTemp)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region �ؼ��¼�
        private void frmIntelligentSearch_Load(object sender, System.EventArgs e)
        {
            m_mthInitStatisticDefinitionNode();
            m_mthInitConditionOperator();
            m_mthInitConditionRelation();
        }

        private void trvSearchType_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            m_mthInitQueryModeInfo(e.Node);
        }

        private void trvSearchType_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            m_mthInitStatisticQueryNode(e.Node);
        }

        private void m_cmdPerformQuery_Click(object sender, System.EventArgs e)
        {
            m_mthSearch();
        }

        private void m_lsvQueryResult_DoubleClick(object sender, System.EventArgs e)
        {
            if (m_lsvQueryResult.SelectedIndices == null ||
                m_lsvQueryResult.SelectedIndices.Count == 0)
                return;

            TreeNode trnCurrentQueryMode = trvSearchType.SelectedNode;

            //�ж��Ƿ��ѯģ��Ľڵ�
            if (!m_blnIsQueryModeTreeNode(trnCurrentQueryMode))
                return;

            clsStatisticDefinitionValue objDefinition = (clsStatisticDefinitionValue)trnCurrentQueryMode.Parent.Tag;


            m_mthOpenOrderByReflection(objDefinition, (string[,])m_lsvQueryResult.Tag, m_lsvQueryResult.SelectedIndices[0]);
        }
        #endregion �ؼ��¼�

        #region ģ���б���Ϣ
        /// <summary>
        /// ��ʼ����ѯ����
        /// </summary>
        private void m_mthInitStatisticDefinitionNode()
        {
            clsStatisticDefinitionValue[] objStatisticDefinitionArr = null;

            long lngRes = m_objDomain.m_lngGetAllStatisticDefinition(out objStatisticDefinitionArr);

            m_trnRoot.Nodes.Clear();

            if (lngRes <= 0 || objStatisticDefinitionArr == null) return;

            for (int i = 0; i < objStatisticDefinitionArr.Length; i++)
            {
                TreeNode trnType = new TreeNode(objStatisticDefinitionArr[i].m_strStatisticDesc);
                trnType.Tag = objStatisticDefinitionArr[i];
                trnType.Nodes.Add(".");

                m_trnRoot.Nodes.Add(trnType);
            }

            m_trnRoot.Expand();
        }
        /// <summary>
        /// ��ʼ���Ѿ����ɵĲ�ѯģ��
        /// </summary>
        /// <param name="p_trnDefinition">��ѯģ�����Ӧ�Ĳ�ѯ���ͽڵ㡣����ڵ㲻�ǲ�ѯ���ͽڵ㣬�����κβ���</param>
        private void m_mthInitStatisticQueryNode(TreeNode p_trnDefinition)
        {
            if (!m_blnIsDefinitionTreeNode(p_trnDefinition))
                return;

            //�ж��Ƿ��Ѿ���ʼ��
            if (!(p_trnDefinition.Nodes.Count == 1 && p_trnDefinition.Nodes[0].Tag == null))
                return;

            clsStatisticDefinitionValue objStatisDefValue = (clsStatisticDefinitionValue)p_trnDefinition.Tag;

            p_trnDefinition.Nodes.Clear();

            clsStatisticQueryModeValue[] objStaticQueryMode;

            long lngRes = m_objDomain.m_lngGetStatisticQueryMode(objStatisDefValue.m_strStatistic_ID, out objStaticQueryMode);

            if (lngRes <= 0 || objStaticQueryMode == null || objStaticQueryMode.Length == 0) return;

            for (int i = 0; i < objStaticQueryMode.Length; i++)
            {
                TreeNode trnType = new TreeNode(objStaticQueryMode[i].m_strQueryName);
                trnType.Tag = objStaticQueryMode[i];

                p_trnDefinition.Nodes.Add(trnType);
            }

            p_trnDefinition.Expand();
        }
        #endregion ģ���б���Ϣ

        #region Statistic��Ϣ
        /// <summary>
        /// ��ʼ����ѯ�����Ĳ�����
        /// </summary>
        private void m_mthInitConditionOperator()
        {
            long lngRes = m_objDomain.lngGetStatisticConditionOperatorValue(out m_objConditionOperatorArr);

            if (lngRes <= 0)
                m_objConditionOperatorArr = new clsStatisticConditionOperatorValue[0];
        }
        /// <summary>
        /// ��ʼ����ѯ��������Ϲ�ϵ
        /// </summary>
        private void m_mthInitConditionRelation()
        {
            long lngRes = m_objDomain.m_lngGetAllStatisticCCOperator(out m_objCCOperatorArr);

            if (lngRes <= 0)
                m_objCCOperatorArr = new clsStatisticCCOperatorValue[0];
        }
        #endregion Statistic��Ϣ

        #region ��ѯģ��
        /// <summary>
        /// ��ʼ����ѯģ����Ϣ
        /// </summary>
        /// <param name="p_trnQueryMode">��ѯģ������ڵ㡣����ڵ㲻�ǲ�ѯģ��ڵ㣬�����κβ���</param>
        private void m_mthInitQueryModeInfo(TreeNode p_trnQueryMode)
        {
            m_mthClearQueryInfo();

            if (!m_blnIsQueryModeTreeNode(p_trnQueryMode))
                return;

            clsStatisticQueryModeValue objQueryMode = (clsStatisticQueryModeValue)p_trnQueryMode.Tag;
            clsStatisticDefinitionValue objDefinition = (clsStatisticDefinitionValue)p_trnQueryMode.Parent.Tag;

            m_mthInitQueryDisplayField(objDefinition, objQueryMode);
            m_mthInitQueryDetail(objDefinition, objQueryMode);
        }
        /// <summary>
        /// ��ʼ����ѯ����ֶ�
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_objQueryMode">��ѯģ��</param>
        private void m_mthInitQueryDisplayField(clsStatisticDefinitionValue p_objDefinition, clsStatisticQueryModeValue p_objQueryMode)
        {
            if (p_objDefinition == null || m_blnIsEmptyString(p_objDefinition.m_strStatistic_ID) || p_objQueryMode == null || m_blnIsEmptyString(p_objQueryMode.m_strSelectedFieldIndexes))
            {
                return;
            }

            clsStatisticSelectedFieldValue[] objSelectedFieldArr = (clsStatisticSelectedFieldValue[])m_hasSelectedField[p_objDefinition.m_strStatistic_ID];

            if (objSelectedFieldArr == null)
            {
                long lngRes = m_objDomain.m_lngGetAllStatisticSelectedField(p_objDefinition.m_strStatistic_ID, out objSelectedFieldArr);

                if (lngRes <= 0 || objSelectedFieldArr == null || objSelectedFieldArr.Length == 0)
                    return;

                m_hasSelectedField[p_objDefinition.m_strStatistic_ID] = objSelectedFieldArr;
            }

            string[] strSelectedFieldsArr = null;
            if (p_objQueryMode.m_strSelectedFieldIndexes.IndexOf(c_strSplitSelectedFieldSymbol) < 0)
            {
                strSelectedFieldsArr = new string[1];
                strSelectedFieldsArr[0] = p_objQueryMode.m_strSelectedFieldIndexes.Trim();
            }
            else
            {
                strSelectedFieldsArr = p_objQueryMode.m_strSelectedFieldIndexes.Split(c_strSplitSelectedFieldSymbol.ToCharArray());
            }

            for (int i = 0; i < strSelectedFieldsArr.Length; i++)
            {
                for (int j2 = 0; j2 < objSelectedFieldArr.Length; j2++)
                {
                    if (objSelectedFieldArr[j2].m_strFieldName == strSelectedFieldsArr[i])
                    {
                        m_lsvQueryResult.Columns.Add(objSelectedFieldArr[j2].m_strFieldDesc, 120, HorizontalAlignment.Left);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// ��ʼ����ѯģ��ϸ����Ϣ
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_objQueryMode">��ѯģ��</param>
        private void m_mthInitQueryDetail(clsStatisticDefinitionValue p_objDefinition, clsStatisticQueryModeValue p_objQueryMode)
        {
            if (p_objDefinition == null || m_blnIsEmptyString(p_objDefinition.m_strStatistic_ID) || p_objQueryMode == null || m_blnIsEmptyString(p_objQueryMode.m_strModeDesc))
            {
                return;
            }

            m_lblSearchDesc.Text = "��ѯ˵����" + p_objQueryMode.m_strModeDesc;

            m_mthLoadXmlToDataGrid(p_objDefinition, p_objQueryMode);
        }
        /// <summary>
        /// ��ղ�ѯģ���������
        /// </summary>
        private void m_mthClearQueryInfo()
        {
            m_lblSearchDesc.Text = "��ѯ˵����";

            m_dtbSearchContent.Rows.Clear();

            m_lsvQueryResult.Clear();
            m_lsvQueryResult.Tag = null;
        }
        #endregion ��ѯģ��

        #region ִ�в�ѯ
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void m_mthSearch()
        {
            m_lsvQueryResult.Items.Clear();

            TreeNode trnCurrentQueryMode = trvSearchType.SelectedNode;

            //�ж��Ƿ��ѯģ��Ľڵ�
            if (!m_blnIsQueryModeTreeNode(trnCurrentQueryMode))
                return;

            clsStatisticQueryModeValue objQueryMode = (clsStatisticQueryModeValue)trnCurrentQueryMode.Tag;
            clsStatisticDefinitionValue objDefinition = (clsStatisticDefinitionValue)trnCurrentQueryMode.Parent.Tag;

            m_mthMakeXmlContentValue();

            System.Data.DataTable strQueryReslutDArr = m_strGetResultArr(objDefinition, objQueryMode);

            m_mthDisplayResult(objDefinition, strQueryReslutDArr);
        }
        /// <summary>
        /// ��ȡ��ѯ����ֵ
        /// </summary>
        /// <returns></returns>
        private void m_mthMakeXmlContentValue()
        {
            XmlDocument objXmlDoc = (XmlDocument)dataGrid1.Tag;

            if (objXmlDoc == null)
                return;

            for (int i = 0; i < m_dtbSearchContent.Rows.Count; i++)
            {
                object[] objValueArr = m_dtbSearchContent.Rows[i].ItemArray;
                XmlNode xndItem = (XmlNode)objValueArr[3];

                string strSymbol = xndItem.Attributes[c_strItemSymbol].Value;
                if (strSymbol.ToLower() == c_strLikeOperator)
                {
                    //ȥ��Like�е�%%
                    objValueArr[1] = "%" + objValueArr[1].ToString() + "%";
                }

                xndItem.Attributes[c_strItemDefaultValue].Value = "'" + objValueArr[1].ToString() + "'";

                XmlAttribute xmlIsUse = objXmlDoc.CreateAttribute(c_strItemIsUse);
                xmlIsUse.Value = objValueArr[4].ToString();
                xndItem.Attributes.Append(xmlIsUse);
            }
        }
        /// <summary>
        /// ��ȡ��ѯ���
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_objQueryMode">��ѯģ��</param>
        /// <returns></returns>
        private System.Data.DataTable m_strGetResultArr(clsStatisticDefinitionValue p_objDefinition, clsStatisticQueryModeValue p_objQueryMode)
        {
            if (p_objDefinition == null || m_blnIsEmptyString(p_objDefinition.m_strStatisticSQLContent) || m_blnIsEmptyString(p_objDefinition.m_strOpenClassParameters))
                return null;

            if (p_objQueryMode == null || m_blnIsEmptyString(p_objQueryMode.m_strSelectedFieldIndexes))
                return null;

            System.Data.DataTable strQueryReslutDArr = null;
            long lngRes = m_objDomain.m_lngPerformSqlQuery(p_objDefinition.m_strStatisticSQLContent, p_objDefinition.m_strOpenClassParameters, p_objQueryMode.m_strSelectedFieldIndexes.Replace(c_strSplitSelectedFieldSymbol, ","),
                (XmlDocument)dataGrid1.Tag, out strQueryReslutDArr);

            if (lngRes <= 0 || strQueryReslutDArr == null)
            {
                return null;
            }
            else
            {
                return strQueryReslutDArr;
            }
        }
        /// <summary>
        /// ��ʾ��ѯ���
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_strQueryReslutDArr">��ѯ���</param>
        private void m_mthDisplayResult(clsStatisticDefinitionValue p_objDefinition, System.Data.DataTable p_strQueryReslutDArr)
        {
            if (p_objDefinition == null || p_strQueryReslutDArr == null)
                return;

            int intParametersNum = m_intGetDefinitionParametersNum(p_objDefinition);

            int intColumnNum = m_lsvQueryResult.Columns.Count;
            int intRowNum = p_strQueryReslutDArr.Rows.Count / (intColumnNum + intParametersNum);

            ListViewItem[] objLsvItemArr = new ListViewItem[intRowNum];
            for (int i = 0; i < intRowNum; i++)
            {
                string[] strItemArr = new string[intColumnNum];
                for (int k2 = 0; k2 < intColumnNum; k2++)
                {
                    strItemArr[k2] = p_strQueryReslutDArr.Rows[i][k2 + intParametersNum].ToString();
                }
                objLsvItemArr[i] = new ListViewItem(strItemArr);
            }
            m_lsvQueryResult.Items.AddRange(objLsvItemArr);
            m_lsvQueryResult.Tag = p_strQueryReslutDArr;
        }
        #endregion ִ�в�ѯ

        #region Xml���ݴ�����
        /// <summary>
        /// ��Xml���û�����ֵ������DataGrid
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_objQueryMode">��ѯģ��</param>
        private void m_mthLoadXmlToDataGrid(clsStatisticDefinitionValue p_objDefinition, clsStatisticQueryModeValue p_objQueryMode)
        {
            if (p_objDefinition == null || p_objQueryMode == null || m_blnIsEmptyString(p_objQueryMode.m_strXMLContent))
                return;

            XmlDocument objXmlDoc = new XmlDocument();
            try
            {
                objXmlDoc.LoadXml(p_objQueryMode.m_strXMLContent);
            }
            catch
            {
                return;
            }

            XmlNodeList xnlUserCondition = objXmlDoc.GetElementsByTagName(c_strUserCondiction);

            if (xnlUserCondition.Count <= 0)
                return;

            clsStatisticCondictionOptionValue[] objCondictionOptionArr = (clsStatisticCondictionOptionValue[])m_hasConditionField[p_objDefinition.m_strStatistic_ID];

            if (objCondictionOptionArr == null)
            {
                long lngRes = m_objDomain.m_lngGetStatisticCondictionOptionValue(p_objDefinition.m_strStatistic_ID, out objCondictionOptionArr);

                if (lngRes <= 0 || objCondictionOptionArr == null)
                    return;

                m_hasConditionField[p_objDefinition.m_strStatistic_ID] = objCondictionOptionArr;
            }

            foreach (XmlNode xndCondition in xnlUserCondition)
            {
                XmlNode xndParentCondition = xndCondition;

                m_mthAddUserConditionToDataGrid(xndCondition, objCondictionOptionArr);
            }

            dataGrid1.Tag = objXmlDoc;
        }
        /// <summary>
        /// ��Xml���û�����ֵ������DataGrid
        /// </summary>
        /// <param name="p_xndCondition">�û������Xml�ڵ�</param>
        /// <param name="p_objCondictionOptionArr">��ѯ������������Ϊ�������ֶ���Ϣ</param>
        private void m_mthAddUserConditionToDataGrid(XmlNode p_xndCondition, clsStatisticCondictionOptionValue[] p_objCondictionOptionArr)
        {
            foreach (XmlNode xndChild in p_xndCondition.ChildNodes)
            {
                if (xndChild.Name == c_strItem)
                {
                    string strOptionName = xndChild.Attributes[c_strItemOptionName].Value;
                    for (int i = 0; i < p_objCondictionOptionArr.Length; i++)
                    {
                        if (strOptionName == p_objCondictionOptionArr[i].m_strOptionFieldName)
                        {
                            strOptionName = p_objCondictionOptionArr[i].m_strOptionDesc;
                            break;
                        }
                    }

                    string strValue = xndChild.Attributes[c_strItemDefaultValue].Value;
                    strValue = strValue.Substring(1, strValue.Length - 2);

                    string strSymbol = xndChild.Attributes[c_strItemSymbol].Value;
                    if (strSymbol.ToLower() == c_strLikeOperator)
                    {
                        //ȥ��Like�е�%%
                        strValue = strValue.Substring(1, strValue.Length - 2);
                    }

                    for (int i = 0; i < m_objConditionOperatorArr.Length; i++)
                    {
                        if (strSymbol == m_objConditionOperatorArr[i].m_strOperatorSymbol)
                        {
                            strSymbol = m_objConditionOperatorArr[i].m_strOperatorDesc;
                            break;
                        }
                    }

                    //��ӵ�DataGrid
                    object[] objCondition = new object[4];
                    objCondition[0] = strOptionName;
                    objCondition[1] = strValue;
                    objCondition[2] = strSymbol;
                    objCondition[3] = xndChild;

                    m_dtbSearchContent.Rows.Add(objCondition);

                }
                else
                {
                    //�ݹ�
                    m_mthAddUserConditionToDataGrid(xndChild, p_objCondictionOptionArr);
                }
            }
        }
        #endregion Xml���ݴ�����

        #region �򿪲�ѯ�����Ӧ�ĵ�
        /// <summary>
        /// ��Ҫ�õ������ݿ��ֶΣ�OpenClassName,OpenClassMethod,OpenClassParameters��
        /// OpenClassParameters�ǷŶ��ֵ����","�ָ�����˳����ã����м�¼�������ݿ���ֶ��������Ҹ�OpenClassMethod�Ĳ������Ӧ(p_frmInvoker����)��
        /// ��ѯʱ���صĶ�ά��ֵǰ�����з���OpenClassParameters��Ӧ�����ݿ��е�ֵ��
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <param name="p_strResArr">��ѯ���������</param>
        /// <param name="p_intClickRow">�û�����ļ�¼��</param>
        private void m_mthOpenOrderByReflection(clsStatisticDefinitionValue p_objDefinition, string[,] p_strResArr, int p_intClickRow)
        {
            int intParametersNum = m_intGetDefinitionParametersNum(p_objDefinition);

            object[] objArr = new object[intParametersNum + 1];
            objArr[0] = this;
            for (int i = 0; i < objArr.Length - 1; i++)
            {
                objArr[i + 1] = p_strResArr[p_intClickRow, i];
            }

            this.Cursor = Cursors.WaitCursor;
            try
            {
                Type typ = Type.GetType(p_objDefinition.m_strOpenClassName.Trim());
                Object obj = Activator.CreateInstance(typ);
                typ.GetMethod(p_objDefinition.m_strOpenClassMethod.Trim()).Invoke(obj, objArr);
            }
            catch
            {
                this.Cursor = Cursors.Default;
                return;
            }
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region ��������
        /// <summary>
        /// �ж��ַ����Ƿ�Ϊ��
        /// </summary>
        /// <param name="p_str">�ַ���</param>
        /// <returns></returns>
        private bool m_blnIsEmptyString(string p_str)
        {
            return (p_str == null || p_str.Trim().Length == 0);
        }

        /// <summary>
        /// �ж��Ƿ�ģ�����͵Ľڵ�
        /// </summary>
        /// <param name="p_trnNode">���ڵ�</param>
        /// <returns></returns>
        private bool m_blnIsDefinitionTreeNode(TreeNode p_trnNode)
        {
            if (p_trnNode == null || p_trnNode.Tag == null || p_trnNode.Tag.GetType().Name != "clsStatisticDefinitionValue")
                return false;
            else
                return true;
        }

        /// <summary>
        /// �ж��Ƿ��ѯģ��Ľڵ�
        /// </summary>
        /// <param name="p_trnNode">���ڵ�</param>
        /// <returns></returns>
        private bool m_blnIsQueryModeTreeNode(TreeNode p_trnNode)
        {
            if (p_trnNode == null || p_trnNode.Tag == null || p_trnNode.Tag.GetType().Name != "clsStatisticQueryModeValue")
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��ȡ��ѯ�����ж���Ĳ�������
        /// </summary>
        /// <param name="p_objDefinition">��ѯ����</param>
        /// <returns></returns>
        private int m_intGetDefinitionParametersNum(clsStatisticDefinitionValue p_objDefinition)
        {
            int intParametersNum = 0;

            if (m_blnIsEmptyString(p_objDefinition.m_strOpenClassParameters))
            {
                intParametersNum = 0;
            }
            else if (p_objDefinition.m_strOpenClassParameters.IndexOf(",") < 0)
            {
                intParametersNum = 1;
            }
            else
            {
                intParametersNum = p_objDefinition.m_strOpenClassParameters.Split(',').Length;
            }

            return intParametersNum;
        }
        #endregion ��������
    }
}

