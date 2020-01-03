using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
using System.Xml;
using com.digitalwave.Utility.Controls;
using System.Reflection;


namespace iCare
{
    public class frmIntelligentStatistics : iCare.frmPrimaryForm, PublicFunction
    {
        private System.Windows.Forms.TreeView m_trvConditionStructure;
        private System.Windows.Forms.ListView m_lsvQueryResult;
        private System.Windows.Forms.Panel m_pnlConditionDetail;
        private System.Windows.Forms.CheckedListBox m_objCheckLstSelectedField;
        private System.Windows.Forms.Button m_cmdPerformQuery;
        private System.Windows.Forms.ContextMenu m_ctmTrv;
        private System.Windows.Forms.MenuItem m_mniAddItem;
        private System.Windows.Forms.MenuItem m_mniAddRelation;
        private System.Windows.Forms.MenuItem m_mniDelete;
        private System.Windows.Forms.Button m_cmdOpenCondition;
        private System.Windows.Forms.MenuItem m_mniModify;
        private System.Windows.Forms.Button mcmdClear;
        private System.Windows.Forms.Button m_cmdSave;
        private System.Windows.Forms.Label label1;
        protected com.digitalwave.Utility.Controls.ctlBorderTextBox m_txtQueryDesc;
        private System.Windows.Forms.Button m_cmdModifyFixedSystems;
        private System.Windows.Forms.RichTextBox m_txtFixEdit;
        private System.Windows.Forms.Panel m_pnlUnder;
        private System.Windows.Forms.CheckBox m_chkUseEnglish;
        private System.Windows.Forms.TextBox m_txtTreeSQL;
        private System.ComponentModel.IContainer components = null;

        public frmIntelligentStatistics()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            m_mthLoadStaticDefinition();
            m_mthLoadConditionOperator();
            m_mthLoadConditionRelation();
            m_mthLoadInitConditionStructure();
            m_mthSetQuickKeysThis();

            //m_objBorderTool=new clsBorderTool(Color.White);
            //m_objBorderTool.m_mthChangedControlsArrayBorder(new Control[]{m_objCheckLstSelectedField,m_trvConditionStructure,m_lsvQueryResult,m_txtTreeSQL});


            //string strXml="<UserCondiction> <Condiction Symbol=\"or\">  <Item OptionName=\"a.MainDescription\" Symbol=\"like\" DefaultValue=\"\'%10��%\'\" />  <Item OptionName=\"a.CurrentStatus\" Symbol=\"like\" DefaultValue=\"\'%1982%\'\" />  </Condiction>  </UserCondiction>";
            //	m_mthLoadXmlStructionToTreeView(strXml,this.m_trvConditionStructure );
            //			m_objDomain.m_mthLoadTreeViewToXmlStruction (this.m_trvConditionStructure , out strXml);
        }

        private const string c_strSplitSelectedFieldSymbol = "��";


        #region ��Ա����
        clsIntelligentStatisticsDomain m_objDomain = new clsIntelligentStatisticsDomain();
        clsStatisticDefinitionValue[] m_objStatisticDefinitionArr = null;
        clsStatisticSelectedFieldValue[] m_objSelectedFieldArr = null;
        clsStatisticCCOperatorValue[] m_objCCOperatorArr = null;
        clsStatisticCondictionOptionValue[] m_objCondictionOptionArr = null;
        clsStatisticConditionOperatorValue[] m_objConditionOperatorArr = null;
        System.Data.DataTable m_strQueryReslutDArr = null;
        string m_strFixedSystems;
        string m_strStatisticID;
        string m_strQueryID;
        /// <summary>
        /// �򿪶�Ӧ������������ĸ���(�������������򿪴����thisָ��)
        /// </summary>
        int m_intParametersNum = 0;
        /// <summary>
        /// ���Գ����õģ���¼�༭FixedSystems�ı༭���Ƿ��
        /// </summary>
        bool m_blnFixOpened = false;
        /// <summary>
        /// ������������ʾ��ѯ�����õ���Ӣ�Ļ������ġ�
        /// </summary>
        bool m_blnIsUsingEnglishSQL = false;

        //����5�У����ڵݹ���ʾ�ͱ����ѯ������Xml		
        static string s_strXmlStruction = "";
        static string s_strDeepSpace = "";
        static TreeNode m_objTreeNode = null;
        #endregion

        #region ��Ӽ��̿�ݼ�
        private void m_mthSetQuickKeysThis()
        {
            m_mthSetControlEvent(this);
        }

        private void m_mthSetControlEvent(Control p_ctlControl)
        {
            #region ���õݹ���ã���ȡ���������н����¼�	
            string strTypeName = p_ctlControl.GetType().Name;
            if (strTypeName != "Lable" && strTypeName != "Button")
            {
                p_ctlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_mthEvent_KeyDown);

                if (p_ctlControl.HasChildren && strTypeName != "DataGrid" && strTypeName != "DateTimePicker" && strTypeName != "ctlComboBox")
                {
                    m_objHighLight.m_mthAddControlInContainer(p_ctlControl);
                    foreach (Control subcontrol in p_ctlControl.Controls)
                    {
                        string strSubTypeName = subcontrol.GetType().Name;
                        if (strSubTypeName != "Lable")
                            m_mthSetControlEvent(subcontrol);
                    }
                }
            }
            #endregion
        }

        private void m_mthEvent_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyValue)
            {//F1 112  ����, F2 113 Save��F3  114 Del��F4 115 Print��F5 116 Refresh��F6 117 Search
                case 13:// enter	
                    break;
                case 46://Delete Key
                    if (sender.GetType().Name == "TreeView" && ((TreeView)sender).Name == "m_trvConditionStructure")
                    {
                        m_mthDeleteCondition();
                    }
                    break;

                case 113://save
                    Save();
                    break;
                case 114://del
                    break;
                case 115://print

                    break;
                case 116://refresh
                    m_mthClearForm();
                    break;
                case 117://Search					
                    break;
            }
        }

        #endregion

        #region ��������
        private bool m_blnIsEmptyString(string p_str)
        {
            return (p_str == null || p_str.Trim().Length == 0);
        }
        private void m_ShowMessage(string p_str)
        {
            if (m_blnIsEmptyString(p_str)) return;
            clsPublicFunction.ShowInformationMessageBox(p_str);
        }
        #endregion

        #region װ����Ҫ��ʾ��Ŀ
        private void m_mthLoadStaticDefinition()
        {
            long lngRes = m_objDomain.m_lngGetAllStatisticDefinition(out m_objStatisticDefinitionArr);

        }
        private void m_mthLoadDisplayField(string p_strSelectedFields)
        {
            if (m_strStatisticID == null || m_strStatisticID.Trim().Length == 0)
            {
                m_ShowMessage("����ѡ��ͳ�����͡�");
                return;
            }
            m_objCheckLstSelectedField.Items.Clear();
            long lngRes = m_objDomain.m_lngGetAllStatisticSelectedField(m_strStatisticID, out m_objSelectedFieldArr);
            if (lngRes <= 0 || m_objSelectedFieldArr == null) return;
            string[] strTempArr = new string[m_objSelectedFieldArr.Length];
            for (int i = 0; i < m_objSelectedFieldArr.Length; i++)
            {
                strTempArr[i] = m_objSelectedFieldArr[i].m_strFieldDesc;
            }
            m_objCheckLstSelectedField.Items.AddRange(strTempArr);
            if (m_blnIsEmptyString(p_strSelectedFields)) return;
            string[] strSelectedFieldsArr = null;
            if (p_strSelectedFields.IndexOf(c_strSplitSelectedFieldSymbol) < 0)
            {
                strSelectedFieldsArr = new string[1];
                strSelectedFieldsArr[0] = p_strSelectedFields.Trim();
            }
            else
            {
                strSelectedFieldsArr = p_strSelectedFields.Split(c_strSplitSelectedFieldSymbol.ToCharArray());
            }
            for (int i = 0; i < m_objSelectedFieldArr.Length; i++)
            {
                for (int k = 0; k < strSelectedFieldsArr.Length; k++)
                {
                    if (m_objSelectedFieldArr[i].m_strFieldName == strSelectedFieldsArr[k])
                    {
                        m_objCheckLstSelectedField.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }
        private void m_mthLoadConditionOption()
        {
            if (m_strStatisticID == null || m_strStatisticID.Trim().Length == 0)
            {
                m_ShowMessage("����ѡ��ͳ�����͡�");
                return;
            }
            long lngRes = m_objDomain.m_lngGetStatisticCondictionOptionValue(m_strStatisticID, out m_objCondictionOptionArr);

        }
        private void m_mthLoadConditionOperator()
        {
            long lngRes = m_objDomain.lngGetStatisticConditionOperatorValue(out m_objConditionOperatorArr);
        }
        private void m_mthLoadConditionRelation()
        {
            long lngRes = m_objDomain.m_lngGetAllStatisticCCOperator(out m_objCCOperatorArr);
        }
        /// <summary>
        /// �Ӹ��ڵ�
        /// </summary>
        private void m_mthLoadInitConditionStructure()
        {
            m_trvConditionStructure.Nodes.Add(new TreeNode("�û�����"));
            m_trvConditionStructure.Nodes[0].Tag = "9";
            m_trvConditionStructure.ExpandAll();
        }

        /// <summary>
        /// ��ʾ��ѯ���
        /// </summary>
        private void m_mthLoadResult()
        {
            m_lsvQueryResult.Items.Clear();
            m_lsvQueryResult.Columns.Clear();
            if (m_strQueryReslutDArr == null)
            {
                m_ShowMessage("û�з��ϲ�ѯ��������Ŀ��");
                return;
            }
            if (m_objCheckLstSelectedField.CheckedIndices.Count == 0) return;
            int intColumnNum = m_objCheckLstSelectedField.CheckedIndices.Count;
            int intRowNum = m_strQueryReslutDArr.Rows.Count / (m_objCheckLstSelectedField.CheckedIndices.Count + m_intParametersNum);
            ColumnHeader[] objColumnHeaderArr = new ColumnHeader[intColumnNum];
            int[] intMaxArr = new int[intColumnNum];
            for (int i = 0; i < intColumnNum; i++)
            {
                objColumnHeaderArr[i] = new ColumnHeader();
                objColumnHeaderArr[i].Text = m_objCheckLstSelectedField.Items[m_objCheckLstSelectedField.CheckedIndices[i]].ToString();
                intMaxArr[i] = objColumnHeaderArr[i].Text.Length * (m_lsvQueryResult.Font.Height + 0);

            }
            m_lsvQueryResult.Columns.AddRange(objColumnHeaderArr);

            ListViewItem[] objLsvItemArr = new ListViewItem[intRowNum];
            for (int i = 0; i < intRowNum; i++)
            {
                string[] strItemArr = new string[intColumnNum];
                for (int k = 0; k < intColumnNum; k++)
                {
                    strItemArr[k] = m_strQueryReslutDArr.Rows[i][k + m_intParametersNum].ToString();
                    if (m_strQueryReslutDArr.Rows[i][k + m_intParametersNum] != null)
                    {
                        if (intMaxArr[k] < m_strQueryReslutDArr.Rows[i][k + m_intParametersNum].ToString().Length) intMaxArr[k] = m_strQueryReslutDArr.Rows[i][k + m_intParametersNum].ToString().Trim().Length;
                    }
                    else
                    {
                        intMaxArr[k] = 1;
                    }
                }
                objLsvItemArr[i] = new ListViewItem(strItemArr);
            }

            m_lsvQueryResult.Items.AddRange(objLsvItemArr);
            for (int i = 0; i < intColumnNum; i++)
            {
                m_lsvQueryResult.Columns[i].Width = intMaxArr[i] * (m_lsvQueryResult.Font.Height + 4) > 120 ? 120 : intMaxArr[i] * (m_lsvQueryResult.Font.Height + 4);
            }
        }

        #endregion

        #region ��ѯ��������ʾ�뱣��

        private void m_mthSaveConditionStructure(bool p_blnDoNotSave)
        {
            if (!p_blnDoNotSave && !m_blnIsEmptyString(m_strQueryID))
            {
                if (clsPublicFunction.ShowQuestionMessageBox("���Ƿ�Ҫ����ղŵ��޸ģ�") != DialogResult.Yes) return;

            }
            if (m_blnIsEmptyString(m_strStatisticID))
            {
                m_ShowMessage("�㻹û�����û�������ͳ�����ͣ����һ�\"�û�����\"ѡ��\"�޸�\"��");
                return;
            }
            if (!m_blnIsValidTree())
            {
                m_ShowMessage("��Ĳ�ѯ����Ϊ�ջ��ʽ����ȷ��");
                return;
            }
            if (m_objCheckLstSelectedField.CheckedIndices.Count == 0)
            {
                m_ShowMessage("�㻹ûѡ����Ҫ��Ҫͳ�Ƶ���Ŀ��");
                return;
            }
            string strXml;
            long lngRes = 1;
            m_mthLoadTreeViewToXmlStruction(m_trvConditionStructure, out strXml);
            clsStatisticQueryModeValue objQuery = new clsStatisticQueryModeValue();
            if (m_blnIsEmptyString(m_strFixedSystems))
            {
                clsStatisticQueryModeValue[] objQueryArr = null;

                lngRes = m_objDomain.m_lngGetStatisticQueryMode(m_strStatisticID, out objQueryArr);
                if (lngRes <= 0 || objQueryArr == null || objQueryArr.Length == 0 || m_blnIsEmptyString(objQueryArr[0].m_strXMLContent))
                {
                    m_ShowMessage("�޷��������ݿ⡣");
                    return;
                }
                try
                {

                    int intBegin = objQueryArr[0].m_strXMLContent.IndexOf("<FixedSystems>", 0);
                    int intEnd = objQueryArr[0].m_strXMLContent.IndexOf("</FixedSystems>", intBegin);
                    int intLength = intEnd - intBegin + "</FixedSystems>".Length;
                    m_strFixedSystems = objQueryArr[0].m_strXMLContent.Substring(intBegin, intLength);
                }

                catch
                {
                    m_ShowMessage("���ݿ��ʽ����");
                    return;
                }
            }
            objQuery.m_strStatistic_ID = m_strStatisticID;
            objQuery.m_strXMLContent = "<root>" + m_strFixedSystems + strXml + "</root>";
            objQuery.m_strModeDesc = "";
            objQuery.m_strQueryID = "";
            objQuery.m_strModeDesc = m_txtQueryDesc.Text;
            string strSelectedField = "";
            for (int i = 0; i < m_objCheckLstSelectedField.CheckedIndices.Count; i++)
            {
                strSelectedField += m_objSelectedFieldArr[m_objCheckLstSelectedField.CheckedIndices[i]].m_strFieldName + c_strSplitSelectedFieldSymbol;

            }
            if (strSelectedField.Length > 0) strSelectedField = strSelectedField.Substring(0, strSelectedField.Length - 1);
            objQuery.m_strSelectedFieldIndexes = strSelectedField;

            if (!p_blnDoNotSave)
            {
                if (m_blnIsEmptyString(m_strQueryID))
                {
                    lngRes = m_objDomain.m_lngAddNewStatisticMode(objQuery, out m_strQueryID);
                }
                else
                {
                    lngRes = m_objDomain.m_lngUpdateStatisticMode(m_strStatisticID, m_strQueryID, objQuery.m_strXMLContent, objQuery.m_strSelectedFieldIndexes, objQuery.m_strModeDesc);
                }
                if (lngRes <= 0)
                {
                    m_ShowMessage("�޷�����ղ��趨�Ĳ�ѯ������");
                    return;
                }
                else
                {
                    m_ShowMessage("�ѳɹ�����ղ��趨�Ĳ�ѯ������");
                    return;
                }
            }
            else
            {
                string strSQLContent = null;
                string strOpenClassParameters = null;
                for (int i = 0; i < m_objStatisticDefinitionArr.Length; i++)
                {
                    if (m_objStatisticDefinitionArr[i].m_strStatistic_ID == m_strStatisticID)
                    {
                        strSQLContent = m_objStatisticDefinitionArr[i].m_strStatisticSQLContent;
                        strOpenClassParameters = m_objStatisticDefinitionArr[i].m_strOpenClassParameters;
                        break;
                    }
                }
                if (m_blnIsEmptyString(strSQLContent))
                {
                    m_ShowMessage("���ݿ��Ѹ��ģ������´򿪱����ڡ�");
                    return;
                }
                m_strQueryReslutDArr = null;
                lngRes = m_objDomain.m_lngPerformSqlQuery(strSQLContent, strOpenClassParameters, objQuery.m_strSelectedFieldIndexes,
                    objQuery.m_strXMLContent, out m_strQueryReslutDArr);
                if (lngRes <= 0)
                {
                    m_ShowMessage("��ѯ������ȷ����ѯ����������Ĳ�ѯֵ��ȷ��");
                    return;
                }
                m_mthLoadResult();
            }


        }
        private void m_mthLoadConditionStructure()
        {
            frmOpenStatistics frm = new frmOpenStatistics();
            if (frm.ShowDialog() != DialogResult.OK) return;
            string strStaticID, strQueryID;
            frm.m_blnGetQueryToOpen(out strStaticID, out strQueryID);
            m_strStatisticID = strStaticID;
            m_strQueryID = strQueryID;

            clsStatisticQueryModeValue[] objQuery = null;
            long lngRes = m_objDomain.m_lngGetStatisticQueryMode(strStaticID, out objQuery);
            if (lngRes <= 0 || objQuery == null)
            {
                m_ShowMessage("�޷��򿪡�");
                return;
            }
            string strXml = null;
            string strSelectedFields = null;
            int i = 0;
            for (i = 0; i < objQuery.Length; i++)
            {
                if (strQueryID == objQuery[i].m_strQueryID)
                {
                    strXml = objQuery[i].m_strXMLContent;
                    strSelectedFields = objQuery[i].m_strSelectedFieldIndexes;
                    m_txtQueryDesc.Text = objQuery[i].m_strModeDesc;
                    break;
                }

            }
            if (i == objQuery.Length)
            {
                m_ShowMessage("�޷��򿪡�");
                return;
            }
            try
            {

                m_mthLoadConditionOption();
                m_mthLoadDisplayField(strSelectedFields);
                m_mthLoadXmlStructionToTreeView(strXml, this.m_trvConditionStructure);
                int intBegin = strXml.IndexOf("<FixedSystems>", 0);
                int intEnd = strXml.IndexOf("</FixedSystems>", intBegin);
                int intLength = intEnd - intBegin + "</FixedSystems>".Length;
                m_strFixedSystems = strXml.Substring(intBegin, intLength);

            }
            catch
            {
                m_ShowMessage("�����ݿ��ж����Ĳ�ѯ������ʽ����");
                return;

            }
            for (i = 0; i < m_objStatisticDefinitionArr.Length; i++)
            {
                if (m_objStatisticDefinitionArr[i].m_strStatistic_ID == m_strStatisticID)
                {
                    m_trvConditionStructure.Nodes[0].Text = m_trvConditionStructure.Nodes[0].Text.Substring(0, 4);
                    m_trvConditionStructure.Nodes[0].Text += "(" + m_objStatisticDefinitionArr[i].m_strStatisticDesc + ")";
                    if (m_blnIsEmptyString(m_objStatisticDefinitionArr[i].m_strOpenClassParameters))
                    {
                        m_intParametersNum = 0;
                    }
                    else if (m_objStatisticDefinitionArr[i].m_strOpenClassParameters.IndexOf(",") < 0)
                    {
                        m_intParametersNum = 1;
                    }
                    else
                    {
                        m_intParametersNum = m_objStatisticDefinitionArr[i].m_strOpenClassParameters.Split(',').Length;
                    }
                    break;
                }
            }
            m_trvConditionStructure.ExpandAll();
            m_mthTextizeCondition();

        }

        #endregion

        #region ��ѯ����������
        private void m_mthAddConditionItem()
        {
            if (m_blnIsEmptyString(m_strStatisticID))
            {
                m_ShowMessage("�㻹û�����û�������ͳ�����ͣ����һ�\"�û�����\"ѡ��\"�޸�\"��");
                return;
            }
            if (m_trvConditionStructure.SelectedNode != null && !m_blnIsEmptyString(m_strStatisticID)
                 && m_trvConditionStructure.SelectedNode.Tag.ToString().IndexOf("��") < 0)
            {
                TreeNode trnNew = new TreeNode();
                trnNew.Text = " ";
                trnNew.Tag = " �� ��' '";
                frmModifyStatistics frmModify = new frmModifyStatistics(false, m_strStatisticID, trnNew);
                if (frmModify.ShowDialog() != DialogResult.OK) return;

                //				m_trvConditionStructure.SelectedNode.Parent.Nodes.Add(trnNewRelation);
                //				m_trvConditionStructure.SelectedNode.Remove();
                //				trnNewRelation.Nodes.Add(trnOriginalNode);
                //				trnNewRelation.Nodes.Add(new TreeNode("A>1"));
                m_trvConditionStructure.SelectedNode.Nodes.Add(trnNew);
                m_trvConditionStructure.ExpandAll();

            }
            else if (m_trvConditionStructure.SelectedNode != null && m_trvConditionStructure.SelectedNode.Nodes.Count != 0)
            {
                //
            }
            m_mthTextizeCondition();
        }
        private void m_mthAddConditionRelation()
        {
            if (m_blnIsEmptyString(m_strStatisticID))
            {
                m_ShowMessage("�㻹û�����û�������ͳ�����ͣ����һ�\"�û�����\"ѡ��\"�޸�\"��");
                return;
            }
            if (m_trvConditionStructure.SelectedNode != null && !m_blnIsEmptyString(m_strStatisticID)
                && m_trvConditionStructure.SelectedNode.Tag.ToString().IndexOf("��") < 0)
            {
                TreeNode trnNew = new TreeNode();
                trnNew.Text = " ";
                trnNew.Tag = " ";
                frmModifyStatistics frmModify = new frmModifyStatistics(false, m_strStatisticID, trnNew);
                if (frmModify.ShowDialog() != DialogResult.OK) return;

                //				m_trvConditionStructure.SelectedNode.Parent.Nodes.Add(trnNewRelation);
                //				m_trvConditionStructure.SelectedNode.Remove();
                //				trnNewRelation.Nodes.Add(trnOriginalNode);
                //				trnNewRelation.Nodes.Add(new TreeNode("A>1"));
                m_trvConditionStructure.SelectedNode.Nodes.Add(trnNew);
                m_trvConditionStructure.ExpandAll();

            }
            else if (m_trvConditionStructure.SelectedNode != null && m_trvConditionStructure.SelectedNode.Nodes.Count != 0)
            {
                //
            }
            m_mthTextizeCondition();
        }
        private void m_mthDeleteCondition()
        {
            if (m_trvConditionStructure.SelectedNode == null || m_trvConditionStructure.SelectedNode.Tag == null)
            {
                return;
            }
            if (m_trvConditionStructure.SelectedNode.Tag != null && m_trvConditionStructure.SelectedNode.Tag.ToString() == "9")
            {
                clsPublicFunction.ShowInformationMessageBox("����ɾ������Ŀ");
                return;
            }
            if (m_trvConditionStructure.SelectedNode != null && m_trvConditionStructure.SelectedNode.Nodes.Count != 0)
            {
                if (clsPublicFunction.ShowQuestionMessageBox("�ò����Ὣ����Ŀ��������Ŀȫ��ɾ�����Ƿ������") != DialogResult.Yes) return;
            }
            //				TreeNode trnParentNode=m_trvConditionStructure.SelectedNode.Parent;
            m_trvConditionStructure.SelectedNode.Remove();
            m_mthTextizeCondition();

        }

        /// <summary>
        /// ͨ���Ϸ����ƶ����ƽڵ�
        /// </summary>
        /// <param name="p_trnDrag">���϶��Ľڵ�</param>
        /// <param name="p_trnOnTo">���ձ��϶��ڵ�Ľڵ�</param>
        /// <param name="p_blnIsCopy">˵�����ƶ����Ǹ���</param>
        /// <returns></returns>
        private bool m_blnAddDropDrag(TreeNode p_trnDrag, TreeNode p_trnOnTo, bool p_blnIsCopy)
        {
            if (p_trnDrag == null || p_trnDrag.Tag == null || p_trnOnTo == null || p_trnOnTo.Tag == null) return false;
            if (p_trnDrag.Tag.ToString() == "9") return false;
            if (p_trnOnTo.Tag.ToString().IndexOf("��") >= 0) return false;
            if (!p_blnIsCopy)
            {
                p_trnDrag.Remove();
                p_trnOnTo.Nodes.Add(p_trnDrag);
                p_trnOnTo.ExpandAll();
                m_trvConditionStructure.SelectedNode = p_trnDrag;
            }
            else
            {
                TreeNode trnNew = new TreeNode();
                m_mthCopyDragNode(trnNew, p_trnDrag);
                p_trnOnTo.Nodes.Add(trnNew);
                p_trnOnTo.ExpandAll();
                m_trvConditionStructure.SelectedNode = trnNew;
            }
            m_mthTextizeCondition();
            return true;

        }

        /// <summary>
        /// �ݹ鸴�ƽڵ㼰���ӽڵ�
        /// </summary>
        /// <param name="p_trnNew"></param>
        /// <param name="p_trnCopied"></param>
        static private void m_mthCopyDragNode(TreeNode p_trnNew, TreeNode p_trnCopied)
        {
            if (p_trnNew == null || p_trnCopied == null) return;
            p_trnNew.Text = p_trnCopied.Text;
            p_trnNew.Tag = p_trnCopied.Tag;
            for (int i = 0; i < p_trnCopied.Nodes.Count; i++)
            {
                TreeNode trnNew = new TreeNode();
                m_mthCopyDragNode(trnNew, p_trnCopied.Nodes[i]);
                p_trnNew.Nodes.Add(trnNew);
            }


        }
        #endregion

        #region ���ģ��
        private void m_mthClearForm()
        {
            m_trvConditionStructure.Nodes[0].Nodes.Clear();
            m_trvConditionStructure.Nodes[0].Text = "�û�����";
            m_lsvQueryResult.Clear();
            m_objCheckLstSelectedField.Items.Clear();
            m_strStatisticID = null;
            m_strQueryID = null;
            m_strFixedSystems = null;
            m_intParametersNum = 0;
            m_txtQueryDesc.Text = "";
        }
        #endregion


        #region ִ�в�ѯ
        private void m_mthPerformQuery()
        {

        }
        #endregion

        #region ���ı��ķ�ʽ��ʾ��ѯ����
        private void m_mthTextizeCondition()
        {
            string strTemp = "";
            try
            {
                m_mthTextizeConditionSub(m_trvConditionStructure.Nodes[0], m_blnIsUsingEnglishSQL, ref strTemp);
            }
            catch
            {

            }
            if (m_blnIsValidTree()) m_txtTreeSQL.ForeColor = Color.White;
            else m_txtTreeSQL.ForeColor = Color.Red;
            m_txtTreeSQL.Text = strTemp;
        }
        static private void m_mthTextizeConditionSub(TreeNode p_trnNode, bool p_blnUseEnglish, ref string p_strTextizedCondition)
        {
            if (p_trnNode == null) return;
            if (p_trnNode.Tag.ToString().IndexOf("��") >= 0)
            {
                string[] strItemArr = p_trnNode.Tag.ToString().Split('��');
                if (p_blnUseEnglish)
                {
                    for (int i = 0; i < strItemArr.Length; i++)
                    {
                        p_strTextizedCondition += strItemArr[i] + " ";
                    }
                }
                else
                {
                    p_strTextizedCondition += p_trnNode.Text + " ";
                }

            }
            else if (p_trnNode.Tag.ToString() == "9")
            {
                for (int i = 0; i < p_trnNode.Nodes.Count; i++)
                {
                    m_mthTextizeConditionSub(p_trnNode.Nodes[i], p_blnUseEnglish, ref p_strTextizedCondition);
                }
            }
            else
            {
                if (p_trnNode.Nodes.Count == 0)
                {
                    return;
                }
                else if (p_trnNode.Nodes.Count == 1)
                {
                    m_mthTextizeConditionSub(p_trnNode.Nodes[0], p_blnUseEnglish, ref p_strTextizedCondition);
                }
                else
                {
                    p_strTextizedCondition += "(" + " ";
                    for (int i = 0; i < p_trnNode.Nodes.Count; i++)
                    {
                        m_mthTextizeConditionSub(p_trnNode.Nodes[i], p_blnUseEnglish, ref p_strTextizedCondition);
                        if (i == p_trnNode.Nodes.Count - 1) continue;
                        if (!p_blnUseEnglish) p_strTextizedCondition += p_trnNode.Text + " ";
                        else p_strTextizedCondition += p_trnNode.Tag.ToString() + " ";
                    }
                    p_strTextizedCondition += ")" + " ";
                }
            }


        }
        #endregion

        private bool m_blnIsValidTree()
        {
            TreeNode trnRoot = m_trvConditionStructure.Nodes[0];
            if (trnRoot.Nodes.Count != 1) return false;
            if (trnRoot.Nodes.Count > 1)
            {
                for (int i = 0; i < trnRoot.Nodes.Count; i++)
                {
                    if (trnRoot.Nodes[i].Tag.ToString() == "9" || trnRoot.Nodes[i].Tag.ToString().IndexOf("��") >= 0) return false;
                }
            }
            return m_blnIsValidTreeSub(trnRoot);
        }
        static private bool m_blnIsValidTreeSub(TreeNode p_trnNode)
        {

            if (p_trnNode.Tag.ToString().IndexOf("��") < 0 && p_trnNode.Tag.ToString() != "9")
            {
                if (p_trnNode.Nodes.Count <= 1) return false;
            }
            for (int i = 0; i < p_trnNode.Nodes.Count; i++)
            {
                bool bln = m_blnIsValidTreeSub(p_trnNode.Nodes[i]);
                if (!bln) return bln;
            }
            return true;
        }


        public void m_mthLoadXmlStructionToTreeView(string p_strWhereXML, TreeView p_objTree)
        {
            p_objTree.Nodes[0].Nodes.Clear();
            XmlDocument objXmlDocment = new XmlDocument();
            objXmlDocment.LoadXml(p_strWhereXML);

            //			m_objTreeNode = p_objTree.Nodes.Add ("�û�����");
            //			m_objTreeNode.Tag ="9";
            //
            int intBegin = p_strWhereXML.IndexOf("<FixedSystems>", 0);
            int intEnd = p_strWhereXML.IndexOf("</FixedSystems>", intBegin);
            int intLength = intEnd - intBegin + "</FixedSystems>".Length;
            m_strFixedSystems = p_strWhereXML.Substring(intBegin, intLength);
            m_mthReadUserCondictionToTree(objXmlDocment.DocumentElement.ChildNodes[1], p_objTree.Nodes[0],
                m_objCCOperatorArr, m_objCondictionOptionArr, m_objConditionOperatorArr);

        }

        public void m_mthLoadTreeViewToXmlStruction(TreeView p_objTree, out string p_strWhereXML)
        {
            p_strWhereXML = "";
            if (p_objTree.Nodes.Count <= 0) return;
            s_strXmlStruction = "";
            s_strDeepSpace = "";
            m_objTreeNode = p_objTree.Nodes[0];
            m_mthReadTreeToUserCondiction(m_objTreeNode);
            p_strWhereXML = "<UserCondiction>\r\n" + s_strXmlStruction + "</UserCondiction>";
        }


        #region ��XML�ṹװ�뵽���л��Ű������뵽XML��
        #region ���뵽��
        static void m_mthReadUserCondictionToTree(XmlNode p_objNode, TreeNode p_objTreeNode,
            clsStatisticCCOperatorValue[] p_objCCOperatorArr,
            clsStatisticCondictionOptionValue[] p_objCondictionOptionArr,
            clsStatisticConditionOperatorValue[] p_objConditionOperatorArr)
        {
            if (p_objNode == null)
                return;
            if (p_objNode.Name == "UserCondiction")
            {
                if (p_objNode.HasChildNodes)
                {
                    m_mthReadUserCondictionToTree(p_objNode.ChildNodes[0], p_objTreeNode, p_objCCOperatorArr, p_objCondictionOptionArr, p_objConditionOperatorArr);
                }
                return;
            }
            //if is Item
            if (p_objNode.Name == "Item")
            {
                string strOptionDesc = "", strSymbolDesc = "";
                for (int i = 0; i < p_objCondictionOptionArr.Length; i++)
                {
                    if (p_objNode.Attributes["OPTIONNAME"].Value == p_objCondictionOptionArr[i].m_strOptionFieldName)
                    {
                        strOptionDesc = p_objCondictionOptionArr[i].m_strOptionDesc;
                        break;
                    }

                }
                for (int i = 0; i < p_objConditionOperatorArr.Length; i++)
                {
                    if (p_objNode.Attributes["SYMBOL"].Value == p_objConditionOperatorArr[i].m_strOperatorSymbol)
                    {
                        strSymbolDesc = p_objConditionOperatorArr[i].m_strOperatorDesc;
                        break;
                    }

                }

                //strOptionName=
                string strValue = p_objNode.Attributes["DEFAULTVALUE"].Value;
                if (strValue == null || strValue.Trim().Length == 0) throw new Exception("�����ݿ�����Ĳ�ѯ������ʽ����");
                strValue = strValue.Trim();
                strValue = strValue.Substring(1, strValue.Length - 2);
                strValue = strValue.Replace("''", "'");
                TreeNode objTreeNode = p_objTreeNode.Nodes.Add(strOptionDesc + " " + strSymbolDesc + " " + strValue + " ");
                objTreeNode.Tag = p_objNode.Attributes["OPTIONNAME"].Value + "��" + p_objNode.Attributes["SYMBOL"].Value + "��" + p_objNode.Attributes["DEFAULTVALUE"].Value;
                return;
            }

            string strSymbol = "";
            for (int i = 0; i < p_objCCOperatorArr.Length; i++)
            {
                if (p_objNode.Attributes["SYMBOL"].Value == p_objCCOperatorArr[i].m_strOperatorSymbol)
                {
                    strSymbol = p_objCCOperatorArr[i].m_strOperatorDesc;
                    break;
                }

            }
            p_objTreeNode = p_objTreeNode.Nodes.Add(strSymbol);
            p_objTreeNode.Tag = p_objNode.Attributes["SYMBOL"].Value;//��ʾ���ǹ�ϵ(And,Or...)
            for (int i = 0; i < p_objNode.ChildNodes.Count; i++)
            {
                m_mthReadUserCondictionToTree(p_objNode.ChildNodes[i], p_objTreeNode, p_objCCOperatorArr, p_objCondictionOptionArr, p_objConditionOperatorArr);
            }

        }

        #endregion

        static void m_mthReadTreeToUserCondiction(TreeNode p_objTreeNode)
        {
            if (p_objTreeNode == null)
                return;
            if (p_objTreeNode.Tag.ToString() == "9")
            {
                if (p_objTreeNode.Nodes.Count > 0)
                {
                    m_mthReadTreeToUserCondiction(p_objTreeNode.Nodes[0]);
                }
                return;
            }
            //if is Item
            if (p_objTreeNode.Tag.ToString().IndexOf("��") >= 0)
            {
                string strItem = p_objTreeNode.Tag.ToString();
                string[] strItems = strItem.Split('��');
                if (strItems.Length >= 3)
                {
                    s_strDeepSpace += "    ";
                    s_strXmlStruction = s_strXmlStruction + s_strDeepSpace + "<Item OptionName=\"" + strItems[0].Trim() + "\" Symbol=\"" + strItems[1].Trim() + "\" DefaultValue=\"" + strItems[2].Trim() + "\" />\r\n";
                    if (s_strDeepSpace.Length > 4)
                        s_strDeepSpace = s_strDeepSpace.Substring(0, s_strDeepSpace.Length - 4);
                }
                return;
            }

            string strSymbol = p_objTreeNode.Tag.ToString();        //condiction symbo
            s_strDeepSpace += "    ";
            s_strXmlStruction = s_strXmlStruction + s_strDeepSpace + "<Condiction Symbol=\"" + strSymbol + "\">\r\n";
            for (int i = 0; i < p_objTreeNode.Nodes.Count; i++)
            {
                m_mthReadTreeToUserCondiction(p_objTreeNode.Nodes[i]);
            }
            s_strXmlStruction = s_strXmlStruction + s_strDeepSpace + "</Condiction>\r\n";
            if (s_strDeepSpace.Length > 4)
                s_strDeepSpace = s_strDeepSpace.Substring(0, s_strDeepSpace.Length - 4);
        }
        #endregion

        #region �򿪲�ѯ�����Ӧ�ĵ�
        /// <summary>
        /// ��Ҫ�õ������ݿ��ֶΣ�OpenClassName,OpenClassMethod,OpenClassParameters��
        /// OpenClassParameters�ǷŶ��ֵ����","�ָ�����˳����ã����м�¼�������ݿ���ֶ��������Ҹ�OpenClassMethod�Ĳ������Ӧ(p_frmInvoker����)��
        /// ��ѯʱ���صĶ�ά��ֵǰ�����з���OpenClassParameters��Ӧ�����ݿ��е�ֵ��
        /// </summary>
        private void m_mthOpenOrderByReflection(int p_intClickRow)
        {
            if (m_objStatisticDefinitionArr == null) return;
            if (m_blnIsEmptyString(m_strStatisticID))
            {
                m_ShowMessage("�㻹û�����û�������ͳ�����ͣ����һ�\"�û�����\"ѡ��\"�޸�\"��");
                return;
            }
            int intSelectedIndex = 0;
            for (intSelectedIndex = 0; intSelectedIndex < m_objStatisticDefinitionArr.Length; intSelectedIndex++)
            {
                if (m_strStatisticID == m_objStatisticDefinitionArr[intSelectedIndex].m_strStatistic_ID) break;
            }

            if (intSelectedIndex >= m_objStatisticDefinitionArr.Length)
            {
                m_ShowMessage("����ѡ���ͳ�����Ͳ����ڡ�");
                return;
            }

            object[] objArr = new object[m_intParametersNum + 1];
            objArr[0] = this;
            for (int i = 0; i < objArr.Length - 1; i++)
            {
                if ((i + 1) * (p_intClickRow + 1) > m_strQueryReslutDArr.Rows.Count) return;
                objArr[i + 1] = m_strQueryReslutDArr.Rows[p_intClickRow][i].ToString();
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Type typ = Type.GetType(m_objStatisticDefinitionArr[intSelectedIndex].m_strOpenClassName.Trim());
                Object obj = Activator.CreateInstance(typ);
                typ.GetMethod(m_objStatisticDefinitionArr[intSelectedIndex].m_strOpenClassMethod.Trim()).Invoke(obj, objArr);
            }
            catch
            {

                m_ShowMessage("�޷��򿪱�����ȷ�������ѯ����ж�Ӧ�ı���");
                this.Cursor = Cursors.Default;
                return;
            }
            this.Cursor = Cursors.Default;

        }

        #endregion


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
            this.m_trvConditionStructure = new System.Windows.Forms.TreeView();
            this.m_ctmTrv = new System.Windows.Forms.ContextMenu();
            this.m_mniModify = new System.Windows.Forms.MenuItem();
            this.m_mniAddItem = new System.Windows.Forms.MenuItem();
            this.m_mniAddRelation = new System.Windows.Forms.MenuItem();
            this.m_mniDelete = new System.Windows.Forms.MenuItem();
            this.m_lsvQueryResult = new System.Windows.Forms.ListView();
            this.m_pnlConditionDetail = new System.Windows.Forms.Panel();
            this.m_cmdModifyFixedSystems = new System.Windows.Forms.Button();
            this.m_txtQueryDesc = new com.digitalwave.Utility.Controls.ctlBorderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmdSave = new System.Windows.Forms.Button();
            this.mcmdClear = new System.Windows.Forms.Button();
            this.m_cmdOpenCondition = new System.Windows.Forms.Button();
            this.m_cmdPerformQuery = new System.Windows.Forms.Button();
            this.m_objCheckLstSelectedField = new System.Windows.Forms.CheckedListBox();
            this.m_txtFixEdit = new System.Windows.Forms.RichTextBox();
            this.m_pnlUnder = new System.Windows.Forms.Panel();
            this.m_txtTreeSQL = new System.Windows.Forms.TextBox();
            this.m_chkUseEnglish = new System.Windows.Forms.CheckBox();
            this.m_pnlConditionDetail.SuspendLayout();
            this.m_pnlUnder.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trvConditionStructure
            // 
            this.m_trvConditionStructure.AllowDrop = true;
            this.m_trvConditionStructure.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_trvConditionStructure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_trvConditionStructure.ContextMenu = this.m_ctmTrv;
            this.m_trvConditionStructure.Cursor = System.Windows.Forms.Cursors.Default;
            this.m_trvConditionStructure.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_trvConditionStructure.ForeColor = System.Drawing.Color.White;
            this.m_trvConditionStructure.HideSelection = false;
            this.m_trvConditionStructure.HotTracking = true;
            this.m_trvConditionStructure.ImageIndex = -1;
            this.m_trvConditionStructure.Indent = 20;
            this.m_trvConditionStructure.Location = new System.Drawing.Point(0, 84);
            this.m_trvConditionStructure.Name = "m_trvConditionStructure";
            this.m_trvConditionStructure.SelectedImageIndex = -1;
            this.m_trvConditionStructure.ShowRootLines = false;
            this.m_trvConditionStructure.Size = new System.Drawing.Size(348, 512);
            this.m_trvConditionStructure.Sorted = true;
            this.m_trvConditionStructure.TabIndex = 200;
            this.m_trvConditionStructure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_trvConditionStructure_MouseDown);
            this.m_trvConditionStructure.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_trvConditionStructure_MouseUp);
            this.m_trvConditionStructure.DragOver += new System.Windows.Forms.DragEventHandler(this.m_trvConditionStructure_DragOver);
            this.m_trvConditionStructure.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_trvConditionStructure_DragEnter);
            this.m_trvConditionStructure.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_trvConditionStructure_ItemDrag);
            this.m_trvConditionStructure.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_trvConditionStructure_DragDrop);
            // 
            // m_ctmTrv
            // 
            this.m_ctmTrv.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.m_mniModify,
                                                                                     this.m_mniAddItem,
                                                                                     this.m_mniAddRelation,
                                                                                     this.m_mniDelete});
            this.m_ctmTrv.Popup += new System.EventHandler(this.m_ctmTrv_Popup);
            // 
            // m_mniModify
            // 
            this.m_mniModify.Index = 0;
            this.m_mniModify.Text = "�޸�";
            this.m_mniModify.Click += new System.EventHandler(this.m_mniModify_Click);
            // 
            // m_mniAddItem
            // 
            this.m_mniAddItem.Index = 1;
            this.m_mniAddItem.Text = "���������";
            this.m_mniAddItem.Click += new System.EventHandler(this.m_mniAddItem_Click);
            // 
            // m_mniAddRelation
            // 
            this.m_mniAddRelation.Index = 2;
            this.m_mniAddRelation.Text = "��ӹ�ϵ";
            this.m_mniAddRelation.Click += new System.EventHandler(this.m_mniAddRelation_Click);
            // 
            // m_mniDelete
            // 
            this.m_mniDelete.Index = 3;
            this.m_mniDelete.Text = "ɾ��";
            this.m_mniDelete.Click += new System.EventHandler(this.m_mniDelete_Click);
            // 
            // m_lsvQueryResult
            // 
            this.m_lsvQueryResult.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_lsvQueryResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsvQueryResult.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_lsvQueryResult.ForeColor = System.Drawing.Color.White;
            this.m_lsvQueryResult.FullRowSelect = true;
            this.m_lsvQueryResult.GridLines = true;
            this.m_lsvQueryResult.Location = new System.Drawing.Point(352, 112);
            this.m_lsvQueryResult.MultiSelect = false;
            this.m_lsvQueryResult.Name = "m_lsvQueryResult";
            this.m_lsvQueryResult.Size = new System.Drawing.Size(664, 484);
            this.m_lsvQueryResult.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_lsvQueryResult.TabIndex = 170;
            this.m_lsvQueryResult.View = System.Windows.Forms.View.Details;
            this.m_lsvQueryResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvQueryResult_MouseDown);
            // 
            // m_pnlConditionDetail
            // 
            this.m_pnlConditionDetail.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                               this.m_cmdModifyFixedSystems,
                                                                                               this.m_txtQueryDesc,
                                                                                               this.label1,
                                                                                               this.m_cmdSave,
                                                                                               this.mcmdClear,
                                                                                               this.m_cmdOpenCondition,
                                                                                               this.m_cmdPerformQuery});
            this.m_pnlConditionDetail.Location = new System.Drawing.Point(4, 4);
            this.m_pnlConditionDetail.Name = "m_pnlConditionDetail";
            this.m_pnlConditionDetail.Size = new System.Drawing.Size(344, 72);
            this.m_pnlConditionDetail.TabIndex = 4;
            // 
            // m_cmdModifyFixedSystems
            // 
            this.m_cmdModifyFixedSystems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdModifyFixedSystems.Location = new System.Drawing.Point(16, 8);
            this.m_cmdModifyFixedSystems.Name = "m_cmdModifyFixedSystems";
            this.m_cmdModifyFixedSystems.Size = new System.Drawing.Size(56, 28);
            this.m_cmdModifyFixedSystems.TabIndex = 100;
            this.m_cmdModifyFixedSystems.Text = "Fix";
            this.m_cmdModifyFixedSystems.Visible = false;
            this.m_cmdModifyFixedSystems.Click += new System.EventHandler(this.m_cmdModifyFixedSystems_Click);
            // 
            // m_txtQueryDesc
            // 
            this.m_txtQueryDesc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(52)), ((System.Byte)(113)), ((System.Byte)(152)));
            this.m_txtQueryDesc.BorderColor = System.Drawing.Color.White;
            this.m_txtQueryDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtQueryDesc.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.m_txtQueryDesc.ForeColor = System.Drawing.Color.White;
            this.m_txtQueryDesc.Location = new System.Drawing.Point(108, 42);
            this.m_txtQueryDesc.Name = "m_txtQueryDesc";
            this.m_txtQueryDesc.Size = new System.Drawing.Size(224, 26);
            this.m_txtQueryDesc.TabIndex = 150;
            this.m_txtQueryDesc.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 23;
            this.label1.Text = "��ѯ˵��:";
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdSave.Location = new System.Drawing.Point(208, 8);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Size = new System.Drawing.Size(56, 28);
            this.m_cmdSave.TabIndex = 130;
            this.m_cmdSave.Text = "����";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // mcmdClear
            // 
            this.mcmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mcmdClear.Location = new System.Drawing.Point(80, 8);
            this.mcmdClear.Name = "mcmdClear";
            this.mcmdClear.Size = new System.Drawing.Size(56, 28);
            this.mcmdClear.TabIndex = 110;
            this.mcmdClear.Text = "���";
            this.mcmdClear.Click += new System.EventHandler(this.mcmdClear_Click);
            // 
            // m_cmdOpenCondition
            // 
            this.m_cmdOpenCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdOpenCondition.Location = new System.Drawing.Point(144, 8);
            this.m_cmdOpenCondition.Name = "m_cmdOpenCondition";
            this.m_cmdOpenCondition.Size = new System.Drawing.Size(56, 28);
            this.m_cmdOpenCondition.TabIndex = 120;
            this.m_cmdOpenCondition.Text = "��";
            this.m_cmdOpenCondition.Click += new System.EventHandler(this.m_cmdOpenCondition_Click);
            // 
            // m_cmdPerformQuery
            // 
            this.m_cmdPerformQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_cmdPerformQuery.Location = new System.Drawing.Point(272, 8);
            this.m_cmdPerformQuery.Name = "m_cmdPerformQuery";
            this.m_cmdPerformQuery.Size = new System.Drawing.Size(56, 28);
            this.m_cmdPerformQuery.TabIndex = 140;
            this.m_cmdPerformQuery.Text = "��ѯ";
            this.m_cmdPerformQuery.Click += new System.EventHandler(this.m_cmdPerformQuery_Click);
            // 
            // m_objCheckLstSelectedField
            // 
            this.m_objCheckLstSelectedField.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_objCheckLstSelectedField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_objCheckLstSelectedField.ColumnWidth = 180;
            this.m_objCheckLstSelectedField.ForeColor = System.Drawing.Color.White;
            this.m_objCheckLstSelectedField.Location = new System.Drawing.Point(352, 4);
            this.m_objCheckLstSelectedField.MultiColumn = true;
            this.m_objCheckLstSelectedField.Name = "m_objCheckLstSelectedField";
            this.m_objCheckLstSelectedField.Size = new System.Drawing.Size(664, 105);
            this.m_objCheckLstSelectedField.TabIndex = 160;
            // 
            // m_txtFixEdit
            // 
            this.m_txtFixEdit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_txtFixEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtFixEdit.ForeColor = System.Drawing.Color.White;
            this.m_txtFixEdit.Location = new System.Drawing.Point(28, 44);
            this.m_txtFixEdit.Name = "m_txtFixEdit";
            this.m_txtFixEdit.Size = new System.Drawing.Size(628, 244);
            this.m_txtFixEdit.TabIndex = 51;
            this.m_txtFixEdit.Text = "";
            this.m_txtFixEdit.Visible = false;
            // 
            // m_pnlUnder
            // 
            this.m_pnlUnder.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.m_txtTreeSQL,
                                                                                     this.m_chkUseEnglish});
            this.m_pnlUnder.Location = new System.Drawing.Point(8, 600);
            this.m_pnlUnder.Name = "m_pnlUnder";
            this.m_pnlUnder.Size = new System.Drawing.Size(1004, 36);
            this.m_pnlUnder.TabIndex = 52;
            // 
            // m_txtTreeSQL
            // 
            this.m_txtTreeSQL.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(51)), ((System.Byte)(102)), ((System.Byte)(153)));
            this.m_txtTreeSQL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtTreeSQL.ForeColor = System.Drawing.Color.White;
            this.m_txtTreeSQL.Location = new System.Drawing.Point(76, 11);
            this.m_txtTreeSQL.Multiline = true;
            this.m_txtTreeSQL.Name = "m_txtTreeSQL";
            this.m_txtTreeSQL.ReadOnly = true;
            this.m_txtTreeSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtTreeSQL.Size = new System.Drawing.Size(928, 21);
            this.m_txtTreeSQL.TabIndex = 190;
            this.m_txtTreeSQL.TabStop = false;
            this.m_txtTreeSQL.Text = "";
            // 
            // m_chkUseEnglish
            // 
            this.m_chkUseEnglish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_chkUseEnglish.Location = new System.Drawing.Point(8, 8);
            this.m_chkUseEnglish.Name = "m_chkUseEnglish";
            this.m_chkUseEnglish.Size = new System.Drawing.Size(64, 24);
            this.m_chkUseEnglish.TabIndex = 180;
            this.m_chkUseEnglish.Text = "Ӣ��";
            this.m_chkUseEnglish.CheckedChanged += new System.EventHandler(this.m_chkUseEnglish_CheckedChanged);
            // 
            // frmIntelligentStatistics
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1016, 641);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.m_pnlUnder,
                                                                          this.m_objCheckLstSelectedField,
                                                                          this.m_lsvQueryResult,
                                                                          this.m_trvConditionStructure,
                                                                          this.m_pnlConditionDetail,
                                                                          this.m_txtFixEdit});
            this.Name = "frmIntelligentStatistics";
            this.Text = "����ͳ��";
            this.m_pnlConditionDetail.ResumeLayout(false);
            this.m_pnlUnder.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion



        #region Public Function
        public void Copy()
        {
            m_lngCopy();
        }

        public void Cut()
        {
            m_lngCut();
        }

        public void Delete()
        {
            //			m_lngDelete ();
        }
        public void Verify()
        {
            ////long lngRes=m_lngSignVerify(p_strFormID,p_strRecordID);
        }
        public void Display()
        {

        }

        public void Display(string cardno, string sendcheckdate)
        {

        }

        public void Paste()
        {
            m_lngPaste();
        }

        public void Print()
        {

        }

        public void Redo()
        {

        }

        public void Save()
        {
            m_mthSaveConditionStructure(false);
        }

        public void Undo()
        {

        }

        private void m_trvConditionStructure_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //			if(m_trvConditionStructure.SelectedNode!=null)
            //			{
            //				string str="";
            //				if(m_trvConditionStructure.SelectedNode.Parent!=null) str+=m_trvConditionStructure.SelectedNode.Parent.Text + " " ;
            //				clsPublicFunction.ShowInformationMessageBox(str+m_trvConditionStructure.SelectedNode.Text);
            //				m_mthAddConditionItem();
            //			}
        }
        #endregion




        private void m_mniAddItem_Click(object sender, System.EventArgs e)
        {
            m_mthAddConditionItem();
        }

        private void m_mniDelete_Click(object sender, System.EventArgs e)
        {
            m_mthDeleteCondition();
        }


        private void m_cmdOpenCondition_Click(object sender, System.EventArgs e)
        {
            m_mthLoadConditionStructure();
        }

        //		private void m_trvConditionStructure_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        //		{
        //			return;
        //			if(m_trvConditionStructure.SelectedNode.Tag.ToString()=="9")
        //			{
        //				m_mthDisplayRoot();		
        //			}
        //			else if( m_trvConditionStructure.SelectedNode.Tag.ToString().IndexOf("��")>=0)
        //			{
        //				m_mthDisplayOption();
        //			}
        //			else
        //			{
        //				m_mthDisplayConditionRelation();
        //			}
        //		}

        private void m_mniModify_Click(object sender, System.EventArgs e)
        {
            if (m_trvConditionStructure.SelectedNode == null) return;
            frmModifyStatistics frm = new frmModifyStatistics(false, m_strStatisticID, m_trvConditionStructure.SelectedNode);
            frm.ShowDialog();

            if (frm.m_StrStatisticID != m_strStatisticID)
            {
                m_strStatisticID = frm.m_StrStatisticID;
                m_mthLoadDisplayField(null);
                m_trvConditionStructure.Nodes[0].Nodes.Clear();
                int i = 0;
                for (i = 0; i < m_objStatisticDefinitionArr.Length; i++)
                {
                    if (m_objStatisticDefinitionArr[i].m_strStatistic_ID == m_strStatisticID)
                    {
                        m_trvConditionStructure.Nodes[0].Text = m_trvConditionStructure.Nodes[0].Text.Substring(0, 4);
                        m_trvConditionStructure.Nodes[0].Text += "(" + m_objStatisticDefinitionArr[i].m_strStatisticDesc + ")";
                        if (m_blnIsEmptyString(m_objStatisticDefinitionArr[i].m_strOpenClassParameters))
                        {
                            m_intParametersNum = 0;
                        }
                        if (m_objStatisticDefinitionArr[i].m_strOpenClassParameters.IndexOf(",") < 0) m_intParametersNum = 1;
                        else
                        {
                            m_intParametersNum = m_objStatisticDefinitionArr[i].m_strOpenClassParameters.Split(',').Length;
                        }
                        break;
                    }
                }
                if (i == m_objStatisticDefinitionArr.Length)
                {
                    m_ShowMessage("���ݿ��Ѹ��ģ������´򿪴��塣");
                    return;
                }
            }
        }

        private void m_mniAddRelation_Click(object sender, System.EventArgs e)
        {
            m_mthAddConditionRelation();
        }

        private void m_trvConditionStructure_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {

            if (m_trvConditionStructure.SelectedNode == null) return;
            m_trvConditionStructure.DoDragDrop(m_trvConditionStructure.SelectedNode, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void m_trvConditionStructure_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            TreeNode trnDrag = (TreeNode)e.Data.GetData(typeof(TreeNode));
            TreeNode trnOnTo = m_trvConditionStructure.SelectedNode;
            if (trnDrag == null || trnOnTo == null || trnDrag == trnOnTo || trnDrag.Parent == trnOnTo) return;

            m_blnAddDropDrag(trnDrag, trnOnTo, e.Effect == DragDropEffects.Copy);

            //			listView1.SelectedItems[0].Text = strSelectedItemText;
            //dropItem
        }

        private void m_trvConditionStructure_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                if (e.KeyState == 9)
                    e.Effect = DragDropEffects.Copy;
                else e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            TreeNode trnDrag = (TreeNode)e.Data.GetData(typeof(TreeNode));
            Point objPoint = new Point(e.X, e.Y);
            TreeNode trnOnTo = m_trvConditionStructure.GetNodeAt(m_trvConditionStructure.PointToClient(objPoint));
            m_trvConditionStructure.SelectedNode = trnOnTo;
            if (trnOnTo == null || trnOnTo.Tag.ToString().IndexOf("��") > 0 || trnDrag == null || trnDrag.Tag == null || trnDrag.Tag.ToString() == "9" || trnDrag == trnOnTo || trnDrag.Parent == trnOnTo)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

        }

        private void m_trvConditionStructure_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //			if (e.Data.GetDataPresent(typeof(TreeNode)) )
            //				e.Effect = DragDropEffects.Move;
            //			else
            //				e.Effect = DragDropEffects.None;
        }

        private void m_trvConditionStructure_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point objPoint = new Point(e.X, e.Y);
            m_trvConditionStructure.SelectedNode = m_trvConditionStructure.GetNodeAt(objPoint);
        }

        private void m_ctmTrv_Popup(object sender, System.EventArgs e)
        {
            TreeNode trnNode = m_trvConditionStructure.SelectedNode;
            if (trnNode == null)
            {
                m_mniAddItem.Enabled = false;
                m_mniAddRelation.Enabled = false;
                m_mniDelete.Enabled = false;
                m_mniModify.Enabled = false;
                return;
            }
            m_mniAddItem.Enabled = true;
            m_mniAddRelation.Enabled = true;
            m_mniDelete.Enabled = true;
            m_mniModify.Enabled = true;
            if (trnNode.Tag.ToString() == "9")
            {
                m_mniDelete.Enabled = false;
            }
            else if (trnNode.Tag.ToString().IndexOf("��") > 0)
            {
                m_mniAddItem.Enabled = false;
                m_mniAddRelation.Enabled = false;
            }

        }

        private void mcmdClear_Click(object sender, System.EventArgs e)
        {
            m_mthClearForm();
        }

        private void m_cmdPerformQuery_Click(object sender, System.EventArgs e)
        {
            m_mthSaveConditionStructure(true);
        }

        private void m_cmdSave_Click(object sender, System.EventArgs e)
        {
            m_mthSaveConditionStructure(false);
        }

        private void m_lsvQueryResult_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                ListViewItem objLsvItem = m_lsvQueryResult.GetItemAt(e.X, e.Y);
                if (objLsvItem == null) return;
                m_mthOpenOrderByReflection(objLsvItem.Index);

            }
        }

        private void m_cmdModifyFixedSystems_Click(object sender, System.EventArgs e)
        {
            if (!m_blnFixOpened)
            {
                m_txtFixEdit.Text = m_strFixedSystems;
                m_txtFixEdit.Visible = true;
                m_blnFixOpened = true;
            }
            else
            {
                m_strFixedSystems = m_txtFixEdit.Text;
                m_txtFixEdit.Visible = false;
                m_blnFixOpened = false;
            }
        }

        private void m_chkUseEnglish_CheckedChanged(object sender, System.EventArgs e)
        {
            m_blnIsUsingEnglishSQL = m_chkUseEnglish.Checked;
            m_mthTextizeCondition();
        }
    }
}

