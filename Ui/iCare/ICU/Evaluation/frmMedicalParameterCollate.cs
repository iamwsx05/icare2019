using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using com.digitalwave.iCare.middletier.ICU;
using weCare.Core.Entity;
using com.digitalwave.iCare.common;
//using com.digitalwave.ApplyReportServer;
//using iCare.ICU.Espial;

namespace iCare.ICU.Evaluation
{
    /// <summary>
    /// frmMedicalParameterCollate 的摘要说明。
    /// </summary>
    public class frmMedicalParameterCollate : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button m_cmdOk;
        private System.Windows.Forms.Button m_cmdCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button m_cmdAddItem;
        private System.Windows.Forms.ListView m_lsvItem;
        private System.Windows.Forms.Button m_cmdDelItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView m_trvItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TreeView m_trvForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_cmdAdd;
        private System.Windows.Forms.Button m_cmdDel;
        private System.Windows.Forms.TextBox m_txtItemName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmMedicalParameterCollate()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        public frmMedicalParameterCollate(Form p_frmParent)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            m_frmParent = p_frmParent;
        }

        private Form m_frmParent;

        #region 变量
        private System.Drawing.Color m_Add_Color = System.Drawing.Color.Red;
        private System.Drawing.Color m_Edit_Color = System.Drawing.Color.Blue;

        //		string[,] m_strFormarry = new string[,] {{"CT智能评分","frmCTEvaluation"},{"SIRSE诊断评分","SIRSEvaluation"},
        //											   {"改良Glasgow昏迷评分","ImproveGlasgowComaEvaluation"},
        //											   {"急性肺损伤评分","LungInjuryEvaluation"},
        //											   {"新生儿危重病例评分法","NewBabyInjuryCaseEvaluation"},
        //											   {"小儿危重病例评分","BabyInjuryCaseEvaluation"},
        //											   {"APACHEII评分","APACHEIIValuation"},{"APACHEIII评分","APACHEIIIValuation"},
        //											   {"TISS-28评分","frmTISSValuation"},{"MODS智能评分","frmMODSEvalution"},
        //											   {"SOFA智能评分","frmSOFAEvaluation"},
        //											   {"Ranson智能评分","frmRansonEvaluation"},
        //											   {"MortalityRate智能评分","frmMortalityRateEvaluation"}};
        private ArrayList m_altDeleteSQLArry;
        #endregion 变量

        /// <summary>
        /// 清理所有正在使用的资源。
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

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedicalParameterCollate));
            this.m_cmdOk = new System.Windows.Forms.Button();
            this.m_cmdCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_cmdDelItems = new System.Windows.Forms.Button();
            this.m_cmdAddItem = new System.Windows.Forms.Button();
            this.m_lsvItem = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_trvItem = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_trvForm = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtItemName = new System.Windows.Forms.TextBox();
            this.m_cmdAdd = new System.Windows.Forms.Button();
            this.m_cmdDel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmdOk
            // 
            this.m_cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdOk.Location = new System.Drawing.Point(512, 6);
            this.m_cmdOk.Name = "m_cmdOk";
            this.m_cmdOk.Size = new System.Drawing.Size(84, 28);
            this.m_cmdOk.TabIndex = 12;
            this.m_cmdOk.Text = "确定";
            this.m_cmdOk.Click += new System.EventHandler(this.m_cmdOk_Click);
            // 
            // m_cmdCancel
            // 
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.Location = new System.Drawing.Point(632, 6);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Size = new System.Drawing.Size(84, 28);
            this.m_cmdCancel.TabIndex = 13;
            this.m_cmdCancel.Text = "取消";
            this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.m_lsvItem);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 432);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检验项目";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_cmdDelItems);
            this.panel2.Controls.Add(this.m_cmdAddItem);
            this.panel2.Location = new System.Drawing.Point(4, 383);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 44);
            this.panel2.TabIndex = 2;
            // 
            // m_cmdDelItems
            // 
            this.m_cmdDelItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelItems.Location = new System.Drawing.Point(362, 8);
            this.m_cmdDelItems.Name = "m_cmdDelItems";
            this.m_cmdDelItems.Size = new System.Drawing.Size(100, 32);
            this.m_cmdDelItems.TabIndex = 1;
            this.m_cmdDelItems.Text = "删除项目";
            this.m_cmdDelItems.Click += new System.EventHandler(this.m_cmdDelItems_Click);
            // 
            // m_cmdAddItem
            // 
            this.m_cmdAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAddItem.Location = new System.Drawing.Point(250, 8);
            this.m_cmdAddItem.Name = "m_cmdAddItem";
            this.m_cmdAddItem.Size = new System.Drawing.Size(104, 32);
            this.m_cmdAddItem.TabIndex = 1;
            this.m_cmdAddItem.Text = "添加项目";
            this.m_cmdAddItem.Click += new System.EventHandler(this.m_cmdAddItem_Click);
            // 
            // m_lsvItem
            // 
            this.m_lsvItem.AllowDrop = true;
            this.m_lsvItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_lsvItem.GridLines = true;
            this.m_lsvItem.HideSelection = false;
            this.m_lsvItem.LabelEdit = true;
            this.m_lsvItem.Location = new System.Drawing.Point(4, 32);
            this.m_lsvItem.MultiSelect = false;
            this.m_lsvItem.Name = "m_lsvItem";
            this.m_lsvItem.Size = new System.Drawing.Size(474, 347);
            this.m_lsvItem.TabIndex = 0;
            this.m_lsvItem.UseCompatibleStateImageBehavior = false;
            this.m_lsvItem.View = System.Windows.Forms.View.Details;
            this.m_lsvItem.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvItem_DragDrop);
            this.m_lsvItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_lsvItem_KeyDown);
            this.m_lsvItem.DragOver += new System.Windows.Forms.DragEventHandler(this.m_lsvItem_DragOver);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "检验项目ID";
            this.columnHeader1.Width = 114;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "绑定控件名称";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "检验项目名称";
            this.columnHeader3.Width = 114;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "项目模块";
            this.columnHeader4.Width = 254;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.m_trvItem);
            this.groupBox2.Location = new System.Drawing.Point(480, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 432);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "现有检验项目";
            // 
            // m_trvItem
            // 
            this.m_trvItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trvItem.Location = new System.Drawing.Point(3, 19);
            this.m_trvItem.Name = "m_trvItem";
            this.m_trvItem.Size = new System.Drawing.Size(246, 410);
            this.m_trvItem.TabIndex = 0;
            this.m_trvItem.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_trvItem_ItemDrag);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.m_trvForm);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.m_txtItemName);
            this.groupBox3.Controls.Add(this.m_cmdAdd);
            this.groupBox3.Controls.Add(this.m_cmdDel);
            this.groupBox3.Location = new System.Drawing.Point(480, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(252, 432);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // m_trvForm
            // 
            this.m_trvForm.AllowDrop = true;
            this.m_trvForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trvForm.CheckBoxes = true;
            this.m_trvForm.Location = new System.Drawing.Point(4, 44);
            this.m_trvForm.Name = "m_trvForm";
            this.m_trvForm.Size = new System.Drawing.Size(244, 348);
            this.m_trvForm.TabIndex = 5;
            this.m_trvForm.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.m_trvForm_AfterCheck);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "项目名称";
            // 
            // m_txtItemName
            // 
            this.m_txtItemName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtItemName.Location = new System.Drawing.Point(72, 20);
            this.m_txtItemName.Name = "m_txtItemName";
            this.m_txtItemName.Size = new System.Drawing.Size(176, 23);
            this.m_txtItemName.TabIndex = 1;
            this.m_txtItemName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtItemName_KeyDown);
            // 
            // m_cmdAdd
            // 
            this.m_cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAdd.Location = new System.Drawing.Point(88, 396);
            this.m_cmdAdd.Name = "m_cmdAdd";
            this.m_cmdAdd.Size = new System.Drawing.Size(76, 28);
            this.m_cmdAdd.TabIndex = 3;
            this.m_cmdAdd.Text = "添加";
            this.m_cmdAdd.Click += new System.EventHandler(this.m_cmdAdd_Click);
            // 
            // m_cmdDel
            // 
            this.m_cmdDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDel.Location = new System.Drawing.Point(172, 396);
            this.m_cmdDel.Name = "m_cmdDel";
            this.m_cmdDel.Size = new System.Drawing.Size(76, 28);
            this.m_cmdDel.TabIndex = 3;
            this.m_cmdDel.Text = "取消";
            this.m_cmdDel.Click += new System.EventHandler(this.m_cmdDel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmdOk);
            this.panel1.Controls.Add(this.m_cmdCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 433);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 36);
            this.panel1.TabIndex = 15;
            // 
            // frmMedicalParameterCollate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(736, 469);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMedicalParameterCollate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检验项目设置";
            this.Load += new System.EventHandler(this.frmMedicalParameterCollate_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 得到检验系统中所有的检验信息
        /// </summary>
        private void m_mthGetCheckInf()
        {
            string strTemp = "";

            string SQL = @"select (select SAMPLE_GROUP_NAME_CHR from T_AID_LIS_SAMPLE_GROUP where SAMPLE_GROUP_ID_CHR=a.sample_group_id_chr) group_name,
					(select CHECK_ITEM_NAME_VCHR || decode(CHECK_ITEM_ENGLISH_NAME_VCHR,null,'','(') || CHECK_ITEM_ENGLISH_NAME_VCHR || decode(CHECK_ITEM_ENGLISH_NAME_VCHR,null,'',')') from T_BSE_LIS_CHECK_ITEM where CHECK_ITEM_ID_CHR=a.check_item_id_chr) item_name,
					check_item_id_chr,
					sample_group_id_chr	
					from v_lis_bse_sample_group_items a order by sample_group_id_chr ";

            DataTable dtRecord = null;

            TreeNode trvNode = null;
            //clsApplyReportServer objSvc =
            //    (clsApplyReportServer)clsObjectGenerator.objCreatorObjectByType(typeof(clsApplyReportServer));
            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecord);

            if (dtRecord == null)
                return;

            for (int i = 0; i < dtRecord.Rows.Count; i++)
            {
                if (strTemp != dtRecord.Rows[i]["group_name"].ToString().Trim())
                {
                    strTemp = dtRecord.Rows[i]["group_name"].ToString().Trim();
                    trvNode = m_trvItem.Nodes.Add(dtRecord.Rows[i]["group_name"].ToString().Trim());
                    TreeNode trvNodeTemp = trvNode.Nodes.Add(dtRecord.Rows[i]["item_name"].ToString().Trim());
                    trvNodeTemp.Tag = dtRecord.Rows[i];
                }
                else
                {
                    TreeNode trvNodeTemp = trvNode.Nodes.Add(dtRecord.Rows[i]["item_name"].ToString().Trim());
                    trvNodeTemp.Tag = dtRecord.Rows[i];
                }
            }
        }

        /// <summary>
        /// 初始化窗体项目
        /// </summary>
        private void m_mthIniFormItem()
        {
            //clsICU_QuerySvc objSvc = (clsICU_QuerySvc)clsObjectGenerator.objCreatorObjectByType(typeof(clsICU_QuerySvc));
            List<clsICUFormCheckItem> lstCheckItem = null;
            //objSvc.m_lngGetFormCheckItem(m_frmParent.Name, out lstCheckItem);
            if (lstCheckItem != null)
            {
                ListViewItem viewItem = null;
                foreach (clsICUFormCheckItem item in lstCheckItem)
                {
                    viewItem = new ListViewItem(item.m_strCheckItemID);
                    viewItem.SubItems.Add(item.m_strControlName);
                    viewItem.SubItems.Add(item.m_strCheckItemName);
                    viewItem.SubItems.Add(item.m_strFormName);
                    viewItem.SubItems.Add("0");
                    viewItem.Tag = item;
                    m_lsvItem.Items.Add(viewItem);
                }
            }
        }

        private void m_mthInitControl()
        {
        }

        private void frmMedicalParameterCollate_Load(object sender, System.EventArgs e)
        {
            m_mthGetCheckInf();
            m_mthIniFormItem();

            //m_mthGetData();
            m_altDeleteSQLArry = new ArrayList();
        }

        private void m_cmdAddItem_Click(object sender, System.EventArgs e)
        {
            //ListViewItem viewItem = new ListViewItem("000000");
            //viewItem.SubItems.Add("");
            //viewItem.SubItems.Add("");
            //viewItem.SubItems.Add(m_frmParent.Name);
            //viewItem.SubItems.Add("1");            
            //m_lsvItem.Items.Add(viewItem);
        }

        private void m_cmdAdd_Click(object sender, System.EventArgs e)
        {
            string strItemName = m_txtItemName.Text;

            if (m_txtItemName.Text.Trim().Length == 0 && m_txtItemName.Enabled)
                return;

            if (strItemName.Trim().Length != 0)
            {
                ListViewItem lviTemp = m_lsvItem.Items.Add(strItemName);
                lviTemp.SubItems.Add("");
                lviTemp.SubItems.Add("");
                clsCheckItemTagInfo objCheckItemTagInfo = new clsCheckItemTagInfo();
                lviTemp.SubItems.Add(m_strGetApplyForm(ref objCheckItemTagInfo.objCheckItemByFormOrControlArry));
                lviTemp.Tag = objCheckItemTagInfo;
                lviTemp.ForeColor = m_Add_Color;
            }
            else
            {
                if (m_lsvItem.SelectedItems[0] != null)
                {
                    clsCheckItemTagInfo objCheckItemTagInfo = (clsCheckItemTagInfo)m_lsvItem.SelectedItems[0].Tag;
                    m_lsvItem.SelectedItems[0].SubItems[3].Text = m_strGetApplyForm(ref objCheckItemTagInfo.objCheckItemByFormOrControlArry);
                    if (m_lsvItem.SelectedItems[0].ForeColor != m_Add_Color)
                        m_lsvItem.SelectedItems[0].ForeColor = m_Edit_Color;
                }
            }
            m_txtItemName.Text = "";
            m_cmdDel_Click(null, null);
        }

        /// <summary>
        /// 得到检验项目所应用的窗体。
        /// </summary>
        /// <returns></returns>
        private string m_strGetApplyForm(ref clsCheckItemByFormOrControl[] p_objCheckItemByFormOrControlArry)
        {
            ArrayList alttemp = new ArrayList();
            string str = "";

            for (int i = 0; i < m_trvForm.Nodes.Count; i++)
            {
                m_mthGetCheckFromItem(m_trvForm.Nodes[i], ref alttemp);
            }

            if (alttemp != null)
            {
                //clsCheckItemByFormOrControl[] objCheckItemByFormOrControlArry = new clsCheckItemByFormOrControl[alttemp.Count];
                p_objCheckItemByFormOrControlArry = new clsCheckItemByFormOrControl[alttemp.Count];
                string strold = "";
                for (int i = 0; i < alttemp.Count; i++)
                {
                    p_objCheckItemByFormOrControlArry[i] = new clsCheckItemByFormOrControl();
                    p_objCheckItemByFormOrControlArry[i] = (clsCheckItemByFormOrControl)alttemp[i];
                    if (strold != p_objCheckItemByFormOrControlArry[i].m_strFromCName)
                    {
                        str = str + p_objCheckItemByFormOrControlArry[i].m_strFromCName + "、";
                        strold = p_objCheckItemByFormOrControlArry[i].m_strFromCName;
                    }
                }
            }

            return str;
        }

        private void m_mthGetCheckFromItem(TreeNode p_TNode, ref ArrayList p_alt)
        {
            if (p_TNode.Checked && p_TNode.Tag != null)
            {
                clsCheckItemByFormOrControl objCheckItemByFormOrControl = new clsCheckItemByFormOrControl();
                objCheckItemByFormOrControl.m_strFromName = ((DataRow)p_TNode.Tag)["FORMNAME"].ToString();
                objCheckItemByFormOrControl.m_strFromCName = ((DataRow)p_TNode.Tag)["FORMCNAME"].ToString();
                objCheckItemByFormOrControl.m_strControlName = ((DataRow)p_TNode.Tag)["CONTROLNAME"].ToString();
                p_alt.Add(objCheckItemByFormOrControl);
            }

            for (int i = 0; i < p_TNode.Nodes.Count; i++)
            {
                m_mthGetCheckFromItem(p_TNode.Nodes[i], ref p_alt);
            }
        }

        private void m_mthClearFormItem(TreeNode p_Node)
        {
            if (p_Node != null)
            {
                p_Node.Checked = false;
                p_Node.Collapse();

                for (int i = 0; i < p_Node.Nodes.Count; i++)
                {
                    m_mthClearFormItem(p_Node.Nodes[i]);
                }
            }

        }

        private void m_mthSelectNodeByForm(string p_strFormName, string p_strControl, TreeNode p_Node)
        {
            if (p_strFormName != null && p_strControl != null)
            {
                if (p_Node != null)
                {
                    if (p_Node.Tag != null)
                    {
                        DataRow dtRow = (DataRow)p_Node.Tag;
                        if (p_strFormName == dtRow["FORMNAME"].ToString() && p_strControl == dtRow["CONTROLNAME"].ToString())
                        {
                            p_Node.Checked = true;
                            p_Node.Parent.ExpandAll();
                        }
                    }
                }

                for (int i = 0; i < p_Node.Nodes.Count; i++)
                {
                    m_mthSelectNodeByForm(p_strFormName, p_strControl, p_Node.Nodes[i]);
                }

            }
        }

        private void m_cmdDel_Click(object sender, System.EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            m_cmdAddItem.Enabled = true;
            m_lsvItem.Focus();
        }

        private void m_trvItem_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            if (m_trvItem.SelectedNode != null && m_trvItem.SelectedNode.Tag != null)
                m_trvItem.DoDragDrop(m_trvItem.SelectedNode, System.Windows.Forms.DragDropEffects.Copy);
        }

        private void m_lsvItem_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point clientPoint = m_lsvItem.PointToClient(new Point(e.X, e.Y));
            if (m_lsvItem.GetItemAt(clientPoint.X, clientPoint.Y) != null)
            {
                m_lsvItem.GetItemAt(clientPoint.X, clientPoint.Y).Selected = true;
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void m_lsvItem_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (m_lsvItem.SelectedItems[0] == null || m_trvItem.SelectedNode == null)
                return;

            if (m_lsvItem.SelectedItems[0].ForeColor != m_Add_Color)
                m_lsvItem.SelectedItems[0].ForeColor = m_Edit_Color;

            m_lsvItem.SelectedItems[0].SubItems[0].Text = ((DataRow)m_trvItem.SelectedNode.Tag)["check_item_id_chr"].ToString();
            m_lsvItem.SelectedItems[0].SubItems[2].Text = (((DataRow)m_trvItem.SelectedNode.Tag)["item_name"].ToString());
            m_lsvItem.SelectedItems[0].SubItems[4].Text = "1";
        }

        private void m_txtItemName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                m_cmdAdd_Click(null, null);
        }

        private void m_lsvItem_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                m_cmdAddItem_Click(null, null);
        }

        private void m_lsvItem_DoubleClick(object sender, System.EventArgs e)
        {
            if (m_lsvItem.SelectedItems[0] != null)
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
                m_cmdAddItem.Enabled = false;
                m_txtItemName.Enabled = false;
            }
        }

        private void m_cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void m_cmdOk_Click(object sender, System.EventArgs e)
        {
            ListViewItem viewItem = null;
            clsICUFormCheckItem objItem = null;
            //clsMedicalParameterCollateServ objSvc = (clsMedicalParameterCollateServ)clsObjectGenerator.objCreatorObjectByType(typeof(clsMedicalParameterCollateServ));
            for (int i = 0; i < m_lsvItem.Items.Count; i++)
            {
                viewItem = m_lsvItem.Items[i];
                if (viewItem.SubItems[4].Text == "0")
                {
                    continue;
                }
                if (viewItem.Tag == null)
                {
                    objItem = new clsICUFormCheckItem();
                    objItem.m_strCheckItemID = viewItem.SubItems[0].Text;
                    objItem.m_strControlName = viewItem.SubItems[1].Text;
                    objItem.m_strFormName = viewItem.SubItems[3].Text;
                    //(new weCare.Proxy.ProxyEmr()).Service.m_lngAddItem(objItem);
                }
                else
                {
                    objItem = viewItem.Tag as clsICUFormCheckItem;
                    objItem.m_strCheckItemID = viewItem.SubItems[0].Text;
                    objItem.m_strControlName = viewItem.SubItems[1].Text;
                    objItem.m_strFormName = viewItem.SubItems[3].Text;
                    //objSvc.m_lngModifyItem(objItem);
                }
            }
            foreach (clsICUFormCheckItem item in m_lstDeletedItem)
            {
                //objSvc.m_lngDeleteItem(objItem);
            }
            this.Close();
        }

        private void m_mthGetGroupNameAndCheckNameByID(string p_strGroupID, string p_strCheckID, ref string p_strGroupName, ref string p_strCheckName)
        {
            string SQL = @"select (select SAMPLE_GROUP_NAME_CHR from T_AID_LIS_SAMPLE_GROUP where SAMPLE_GROUP_ID_CHR=a.sample_group_id_chr) group_name,
					(select CHECK_ITEM_NAME_VCHR || decode(CHECK_ITEM_ENGLISH_NAME_VCHR,null,'','(') || CHECK_ITEM_ENGLISH_NAME_VCHR || decode(CHECK_ITEM_ENGLISH_NAME_VCHR,null,'',')') from T_BSE_LIS_CHECK_ITEM where CHECK_ITEM_ID_CHR=a.check_item_id_chr) item_name,
					check_item_id_chr,
					sample_group_id_chr	
					from v_lis_bse_sample_group_items a where a.sample_group_id_chr = '" + p_strGroupID + "' and a.check_item_id_chr='" + p_strCheckID + "' order by sample_group_id_chr ";

            DataTable dtRecord = null;
            //clsApplyReportServer objSvc =
            //    (clsApplyReportServer)clsObjectGenerator.objCreatorObjectByType(typeof(clsApplyReportServer));
            (new weCare.Proxy.ProxyEmr03()).Service.clsApplyReportServer_m_lngGetData(SQL, out dtRecord);

            if (dtRecord != null && dtRecord.Rows.Count > 0)
            {
                p_strGroupName = dtRecord.Rows[0]["group_name"].ToString();
                p_strCheckName = dtRecord.Rows[0]["item_name"].ToString();
            }
        }

        private void m_mthGetForName()
        {

        }

        private void m_cmdDelItems_Click(object sender, System.EventArgs e)
        {
            if (m_lsvItem.SelectedItems.Count > 0)
            {
                if (m_lsvItem.SelectedItems[0].Tag != null)
                {
                    m_lstDeletedItem.Add((clsICUFormCheckItem)m_lsvItem.SelectedItems[0].Tag);
                }
                m_lsvItem.SelectedItems[0].Remove();
            }
        }

        private List<clsICUFormCheckItem> m_lstDeletedItem = new List<clsICUFormCheckItem>();

        /// <summary>
        /// 所有控件对应的检验结果
        /// </summary>
        public static Hashtable m_hasControlByChedkedResult = new Hashtable();

        /// <summary>
        ///按照指定病人和住院日期查找指定检查项目的检查信息
        /// </summary>
        /// <param name="p_strInPatientID">病人ID</param>
        /// <param name="p_strInPatientDate">病人住院日期</param>
        /// <param name="p_strCheckItemIDArry">检查项目ID数组</param>
        /// <param name="p_strCheckInfoArry">返回的检查项目的结果数组，顺序与检查项目ID数组的顺序相同</param>
        /// <param name="p_ctlArry">显示内容的控件数据</param>
        public static void m_mthGetCheckInfo(string p_strInPatientID, string p_strInPatientDate, string[] p_strCheckItemIDArry, ref string[] p_strCheckInfoArry, Control[] p_ctlArry)
        {

            m_hasControlByChedkedResult.Clear();
            p_strCheckInfoArry = new string[p_strCheckItemIDArry.Length]; //初始化检查项目的结果数组

            //==========================查找出所有检查项目结果
            //com.digitalwave.iCare.middletier.LIS.clsApplicationSvc objA = (com.digitalwave.iCare.middletier.LIS.clsApplicationSvc)clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.LIS.clsApplicationSvc));
            clsLISPatientCheckResultInfoVO[] LisVO = null;
            (new weCare.Proxy.ProxyLis()).Service.m_lngGetResultInfo(p_strInPatientID, p_strInPatientDate, null, out LisVO);
            //==========================END 查找出所有检查项目结果

            for (int i = 0; i < p_strCheckItemIDArry.Length; i++) //循环检查项目ID数组
            {
                p_strCheckInfoArry[i] = "";
                Control ctlTemp = null;
                if (p_ctlArry != null)
                    ctlTemp = p_ctlArry[i];
                if (ctlTemp.Text.Trim().Length > 0)
                {
                    continue;
                }
                if (LisVO != null)
                {
                    for (int j = 0; j < LisVO.Length; j++) //循环检查项目ID数组
                    {
                        clsCheckResult_VO[] CRVO = LisVO[j].m_objResults;

                        if (CRVO == null)
                            continue;

                        for (int p = 0; p < CRVO.Length; p++)
                        {
                            if (CRVO[p].m_strCheck_Item_ID == p_strCheckItemIDArry[i]) //判断当前项是否是需要的项
                            {
                                p_strCheckInfoArry[i] = CRVO[p].m_strResult;
                                if (ctlTemp != null)
                                {
                                    ctlTemp.Text = CRVO[p].m_strResult;
                                    if (m_hasControlByChedkedResult.ContainsKey(ctlTemp.Name))
                                    {
                                        m_hasControlByChedkedResult.Remove(ctlTemp.Name);
                                    }
                                    m_hasControlByChedkedResult.Add(ctlTemp.Name, CRVO[p].m_strResult);
                                }
                                break;
                            }
                        }
                        //if (blnMatching)
                        //    break;
                    }
                }

            }
        }

        private void m_trvForm_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if (e.Node.Parent == null)
                    return;
                e.Node.Parent.Checked = true;
            }
            else
            {
                TreeNode tnode = e.Node.Parent;
                if (tnode == null)
                    return;
                bool blnCheckTrue = false;
                for (int i = 0; i < tnode.Nodes.Count; i++)
                {
                    if (tnode.Nodes[i].Checked)
                    {
                        blnCheckTrue = true;
                        break;
                    }
                }

                tnode.Checked = blnCheckTrue;
            }
        }

        private class clsCheckItemTagInfo
        {
            public string m_strID = "";
            public string m_strGroupID = "";
            public string m_strItemID = "";
            public clsCheckItemByFormOrControl[] objCheckItemByFormOrControlArry = null;
        }

        private class clsCheckItemByFormOrControl
        {
            public string m_strFromName = "";
            public string m_strFromCName = "";
            public string m_strControlName = "";
        }
    }
}
