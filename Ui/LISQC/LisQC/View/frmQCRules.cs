using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using weCare.Core.Entity;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;
using PinkieControls;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCRules : frmMDI_Child_Base
    {
        // Fields
        private ButtonXP btnExit;
        private IContainer components;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private bool m_blnNewRule;
        private ButtonXP m_btnDelete;
        private ButtonXP m_btnNew;
        private ButtonXP m_btnSave;
        private ComboBox m_cboTypeFlag;
        private ColumnHeader m_chDefaultFlag;
        private CheckBox m_chkDefaultFlag;
        private ColumnHeader m_chRuleAlias;
        private ColumnHeader m_chRuleDesc;
        private ColumnHeader m_chRuleFormula;
        private ColumnHeader m_chRuleName;
        private ColumnHeader m_chsummary;
        private ColumnHeader m_chTypeFlag;
        private ListView m_lsvRules;
        //private clsDcl_QCDataBusiness m_objDomain;
        private TextBox m_txtFormula;
        private TextBox m_txtRuleAliasName;
        private TextBox m_txtRuleDesc;
        private TextBox m_txtRuleName;
        private TextBox m_txtSummary;
        private Panel panel1;

        // Methods
        public frmQCRules()
        {
            //this.m_objDomain = new clsDcl_QCDataBusiness();
            this.m_blnNewRule = false;
            this.components = null;
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    this.Dispose(disposing);
        //}

        private void frmQCRules_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            this.m_mthSetKeyTab(e);
        }

        private void frmQCRules_Load(object sender, EventArgs e)
        {
            Control[] controlArray;
            this.m_mthLoadRules();
            this.m_cboTypeFlag.SelectedIndex = 0;
            this.m_mthSetEnter2Tab(new Control[] { this.m_txtFormula });
        }

        private void InitializeComponent()
        {
            string[] strArray;
            object[] objArray;
            ColumnHeader[] headerArray;
            this.groupBox1 = new GroupBox();
            this.label6 = new Label();
            this.m_cboTypeFlag = new ComboBox();
            this.m_chkDefaultFlag = new CheckBox();
            this.m_txtFormula = new TextBox();
            this.label5 = new Label();
            this.m_txtSummary = new TextBox();
            this.label4 = new Label();
            this.m_txtRuleDesc = new TextBox();
            this.label3 = new Label();
            this.m_txtRuleAliasName = new TextBox();
            this.label2 = new Label();
            this.m_txtRuleName = new TextBox();
            this.label1 = new Label();
            this.m_lsvRules = new ListView();
            this.m_chRuleName = new ColumnHeader();
            this.m_chRuleAlias = new ColumnHeader();
            this.m_chRuleDesc = new ColumnHeader();
            this.m_chRuleFormula = new ColumnHeader();
            this.m_chsummary = new ColumnHeader();
            this.m_chDefaultFlag = new ColumnHeader();
            this.m_chTypeFlag = new ColumnHeader();
            this.m_btnDelete = new ButtonXP();
            this.m_btnSave = new ButtonXP();
            this.m_btnNew = new ButtonXP();
            this.panel1 = new Panel();
            this.btnExit = new ButtonXP();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.m_cboTypeFlag);
            this.groupBox1.Controls.Add(this.m_chkDefaultFlag);
            this.groupBox1.Controls.Add(this.m_txtFormula);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_txtSummary);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.m_txtRuleDesc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_txtRuleAliasName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.m_txtRuleName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x30f, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xf8, 0x100);
            this.label6.Name = "label6";
            this.label6.Size = new Size(70, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "规则类型:";
            this.m_cboTypeFlag.AutoCompleteCustomSource.AddRange(new string[] { "失控", "警告" });
            this.m_cboTypeFlag.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboTypeFlag.FormattingEnabled = true;
            this.m_cboTypeFlag.Items.AddRange(new object[] { "失控", "警告" });
            this.m_cboTypeFlag.Location = new Point(320, 250);
            this.m_cboTypeFlag.Name = "m_cboTypeFlag";
            this.m_cboTypeFlag.Size = new Size(60, 0x16);
            this.m_cboTypeFlag.TabIndex = 9;
            this.m_chkDefaultFlag.AutoSize = true;
            this.m_chkDefaultFlag.Font = new Font("宋体", 10.5f);
            this.m_chkDefaultFlag.Location = new Point(0x59, 0xfe);
            this.m_chkDefaultFlag.Name = "m_chkDefaultFlag";
            this.m_chkDefaultFlag.Size = new Size(0x8a, 0x12);
            this.m_chkDefaultFlag.TabIndex = 6;
            this.m_chkDefaultFlag.Text = "是否是默认组成员";
            this.m_chkDefaultFlag.UseVisualStyleBackColor = true;
            this.m_txtFormula.AcceptsReturn = true;
            this.m_txtFormula.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFormula.Font = new Font("宋体", 10.5f);
            this.m_txtFormula.Location = new Point(0x59, 0x44);
            this.m_txtFormula.MaxLength = 250;
            this.m_txtFormula.Multiline = true;
            this.m_txtFormula.Name = "m_txtFormula";
            this.m_txtFormula.ScrollBars = ScrollBars.Vertical;
            this.m_txtFormula.Size = new Size(0x2ad, 0x98);
            this.m_txtFormula.TabIndex = 4;
            this.m_txtFormula.WordWrap = false;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("宋体", 10.5f);
            this.label5.Location = new Point(0x13, 0xe2);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3f, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "备   注:";
            this.m_txtSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSummary.Location = new Point(0x59, 0xde);
            this.m_txtSummary.MaxLength = 250;
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new Size(0x2ad, 0x17);
            this.m_txtSummary.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("宋体", 10.5f);
            this.label4.Location = new Point(12, 0x7c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(70, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "规则公式:";
            this.m_txtRuleDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRuleDesc.Font = new Font("宋体", 10.5f);
            this.m_txtRuleDesc.Location = new Point(0x59, 0x2c);
            this.m_txtRuleDesc.MaxLength = 250;
            this.m_txtRuleDesc.Name = "m_txtRuleDesc";
            this.m_txtRuleDesc.Size = new Size(0x2ad, 0x17);
            this.m_txtRuleDesc.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("宋体", 10.5f);
            this.label3.Location = new Point(12, 0x30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(70, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "规则描述:";
            this.m_txtRuleAliasName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtRuleAliasName.Font = new Font("宋体", 10.5f);
            this.m_txtRuleAliasName.Location = new Point(0x19b, 20);
            this.m_txtRuleAliasName.MaxLength = 0x19;
            this.m_txtRuleAliasName.Name = "m_txtRuleAliasName";
            this.m_txtRuleAliasName.Size = new Size(0x16b, 0x17);
            this.m_txtRuleAliasName.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 10.5f);
            this.label2.Location = new Point(0x150, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "规则别名:";
            this.m_txtRuleName.Font = new Font("宋体", 10.5f);
            this.m_txtRuleName.Location = new Point(0x59, 20);
            this.m_txtRuleName.MaxLength = 0x19;
            this.m_txtRuleName.Name = "m_txtRuleName";
            this.m_txtRuleName.Size = new Size(0xe9, 0x17);
            this.m_txtRuleName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 10.5f);
            this.label1.Location = new Point(12, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "规则名称:";
            this.m_lsvRules.Columns.AddRange(new ColumnHeader[] { this.m_chRuleName, this.m_chRuleAlias, this.m_chRuleDesc, this.m_chRuleFormula, this.m_chsummary, this.m_chDefaultFlag, this.m_chTypeFlag });
            this.m_lsvRules.Dock = DockStyle.Fill;
            this.m_lsvRules.Font = new Font("宋体", 10.5f);
            this.m_lsvRules.FullRowSelect = true;
            this.m_lsvRules.GridLines = true;
            this.m_lsvRules.HideSelection = false;
            this.m_lsvRules.Location = new Point(0, 280);
            this.m_lsvRules.MultiSelect = false;
            this.m_lsvRules.Name = "m_lsvRules";
            this.m_lsvRules.Size = new Size(0x30f, 0xb8);
            this.m_lsvRules.TabIndex = 1;
            this.m_lsvRules.TabStop = false;
            this.m_lsvRules.UseCompatibleStateImageBehavior = false;
            this.m_lsvRules.View = View.Details;
            this.m_lsvRules.Click += new EventHandler(this.m_lsvRules_Click);
            this.m_chRuleName.Text = "规则名称";
            this.m_chRuleName.Width = 0x74;
            this.m_chRuleAlias.Text = "规则别名";
            this.m_chRuleAlias.Width = 0x55;
            this.m_chRuleDesc.Text = "规则描述";
            this.m_chRuleDesc.Width = 0xf7;
            this.m_chRuleFormula.Text = "规则公式";
            this.m_chRuleFormula.Width = 0x4f;
            this.m_chsummary.Text = "备注";
            this.m_chsummary.Width = 0x4a;
            this.m_chDefaultFlag.Text = "默认规则";
            this.m_chDefaultFlag.Width = 0x48;
            this.m_chTypeFlag.Text = "规则类型";
            this.m_chTypeFlag.Width = 0x6a;
            this.m_btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_btnDelete.DefaultScheme = true;
            this.m_btnDelete.DialogResult = 0;
            this.m_btnDelete.Hint = "";
            this.m_btnDelete.Location = new Point(0x1ef, 0x18);
            this.m_btnDelete.Name = "m_btnDelete";
            this.m_btnDelete.Scheme = 0;
            this.m_btnDelete.Size = new Size(0x56, 0x21);
            this.m_btnDelete.TabIndex = 1;
            this.m_btnDelete.Text = "删除";
            this.m_btnDelete.Click += new EventHandler(this.m_btnDelete_Click);
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_btnSave.DefaultScheme = true;
            this.m_btnSave.DialogResult = 0;
            this.m_btnSave.Hint = "";
            this.m_btnSave.Location = new Point(590, 0x18);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Scheme = 0;
            this.m_btnSave.Size = new Size(0x56, 0x21);
            this.m_btnSave.TabIndex = 2;
            this.m_btnSave.Text = "保存";
            this.m_btnSave.Click += new EventHandler(this.m_btnSave_Click);
            this.m_btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNew.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_btnNew.DefaultScheme = true;
            this.m_btnNew.DialogResult = 0;
            this.m_btnNew.Hint = "";
            this.m_btnNew.Location = new Point(400, 0x18);
            this.m_btnNew.Name = "m_btnNew";
            this.m_btnNew.Scheme = 0;
            this.m_btnNew.Size = new Size(0x56, 0x21);
            this.m_btnNew.TabIndex = 0;
            this.m_btnNew.Text = "新增";
            this.m_btnNew.Click += new EventHandler(this.m_btnNew_Click);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.m_btnDelete);
            this.panel1.Controls.Add(this.m_btnSave);
            this.panel1.Controls.Add(this.m_btnNew);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x1d0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x30f, 80);
            this.panel1.TabIndex = 0x90;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.btnExit.DefaultScheme = true;
            this.btnExit.DialogResult = 0;
            this.btnExit.Hint = "";
            this.btnExit.Location = new Point(0x2ad, 0x18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Scheme = 0;
            this.btnExit.Size = new Size(0x56, 0x21);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "关闭(&C)";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x30f, 0x220);
            this.Controls.Add(this.m_lsvRules);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new Font("宋体", 10.5f);
            this.KeyPreview = true;
            this.Name = "frmQCRules";
            this.Text = "质控规则管理";
            this.KeyDown += new KeyEventHandler(this.frmQCRules_KeyDown);
            this.Load += new EventHandler(this.frmQCRules_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void m_btnDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvRules.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_lsvRules.Enabled = false;

            clsLisQCRuleVO objRule = (clsLisQCRuleVO)this.m_lsvRules.FocusedItem.Tag;

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteQCRule(objRule.m_intSeq);

            if (lngRes > 0)
            {//成功
                int intIdx = this.m_lsvRules.FocusedItem.Index;
                this.m_lsvRules.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvRules.Items.Count)
                {
                    this.m_lsvRules.Items[intIdx].Selected = true;
                    this.m_lsvRules.Items[intIdx].Focused = true;
                    this.m_lsvRules_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvRules.Items[intIdx - 1].Selected = true;
                    this.m_lsvRules.Items[intIdx - 1].Focused = true;
                    this.m_lsvRules_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            //m_mthLoadRules();
            this.m_lsvRules.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_btnNew_Click(object sender, EventArgs e)
        {
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvRules.FocusedItem != null)
            {
                this.m_lsvRules.FocusedItem.Selected = false;
                this.m_lsvRules.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthRulesDetailClear();

            //设置光标焦点
            this.m_txtRuleName.Focus();

            //设置新增标志
            this.m_blnNewRule = true;
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvRules.FocusedItem == null
                && !this.m_blnNewRule)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_btnNew.Enabled = false;

            if (this.m_blnNewRule)
            {//新增的保存
                clsLisQCRuleVO objRule = new clsLisQCRuleVO();
                m_mthBindControlValueToObj(objRule);
                int seqId = 0;
                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertQCRule(objRule, out seqId);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewRule = false;
                    //加入到集合
                    clsLisQCRuleVO[] objRuleArr = (clsLisQCRuleVO[])this.m_lsvRules.Tag;
                    clsLisQCRuleVO[] objRuleNewArr = new clsLisQCRuleVO[objRuleArr.Length + 1];
                    objRuleArr.CopyTo(objRuleNewArr, 0);
                    objRuleNewArr[objRuleNewArr.Length - 1] = objRule;
                    this.m_lsvRules.Tag = objRuleNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objRule.m_strName);
                    //item中展示
                    item = m_mthlsvItemAddObject(objRule);
                    item.Tag = objRule;
                    this.m_lsvRules.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvRules_Click(null, null);
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
                this.m_btnNew.Enabled = true;
            }
            else
            {//修改的保存
                clsLisQCRuleVO objRule = (clsLisQCRuleVO)this.m_lsvRules.FocusedItem.Tag;

                clsLisQCRuleVO objNewRule = new clsLisQCRuleVO();
                objRule.m_mthCopyTo(objNewRule);

                //Vo获取控件的值
                m_mthBindControlValueToObj(objNewRule);

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateQCRule(objNewRule);

                if (lngRes > 0)
                {//成功
                    objNewRule.m_mthCopyTo(objRule);
                    m_mthlsvItemDisplayObject(this.m_lsvRules.FocusedItem, objRule);
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_btnNew.Enabled = true;
            this.m_btnSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvRules_Click(object sender, EventArgs e)
        {
            if (this.m_lsvRules.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewRule = false;

            clsLisQCRuleVO objRule = (clsLisQCRuleVO)this.m_lsvRules.FocusedItem.Tag;

            m_mthControlsDisplayVOValue(objRule);
        }

        private void m_mthBindControlValueToObj(clsLisQCRuleVO objRule)
        {
            objRule.m_strName = this.m_txtRuleName.Text.Trim();
            objRule.m_strAlias = this.m_txtRuleAliasName.Text.Trim();
            objRule.m_strDesc = this.m_txtRuleDesc.Text.Trim();
            objRule.m_strFormula = this.m_txtFormula.Text.Trim();
            objRule.m_strSummary = this.m_txtSummary.Text.Trim();
            objRule.m_enmDefaultflag = (enmQCRuleDefault)Convert.ToInt32(this.m_chkDefaultFlag.Checked);
            objRule.m_enmWarnType = m_cboTypeFlag.SelectedIndex == 0 ? enmQCRuleWarnLevel.Error : enmQCRuleWarnLevel.Warning;
        }

        private void m_mthControlsDisplayVOValue(clsLisQCRuleVO objRule)
        {
            this.m_txtRuleName.Text = objRule.m_strName;
            this.m_txtRuleAliasName.Text = objRule.m_strAlias;
            this.m_txtRuleDesc.Text = objRule.m_strDesc;
            this.m_txtFormula.Text = objRule.m_strFormula;
            this.m_txtSummary.Text = objRule.m_strSummary;
            this.m_chkDefaultFlag.Checked = objRule.m_enmDefaultflag == enmQCRuleDefault.YEA ? true : false;
            this.m_cboTypeFlag.SelectedIndex = objRule.m_enmWarnType == enmQCRuleWarnLevel.Error ? 0 : 1;
        }

        private void m_mthLoadRules()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsLisQCRuleVO[] objRulesArr = null;
            (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCRule(out objRulesArr);
            if (objRulesArr == null)
                objRulesArr = new clsLisQCRuleVO[0];
            m_lsvRules.Tag = objRulesArr;

            //填充列表
            m_mthShowRulesList(objRulesArr);

            Cursor.Current = Cursors.Default;
        }

        private static ListViewItem m_mthlsvItemAddObject(clsLisQCRuleVO rule)
        {
            ListViewItem item = new ListViewItem(rule.m_strName);
            item.SubItems.Add(rule.m_strAlias);
            item.SubItems.Add(rule.m_strDesc);
            item.SubItems.Add(rule.m_strFormula);
            item.SubItems.Add(rule.m_strSummary);
            item.SubItems.Add(rule.m_enmDefaultflag == enmQCRuleDefault.YEA ? "是" : "否");
            item.SubItems.Add(rule.m_enmWarnType == enmQCRuleWarnLevel.Error ? "失控" : "警告");
            return item;
        }

        private void m_mthlsvItemDisplayObject(ListViewItem item, clsLisQCRuleVO objRule)
        {
            item.Text = objRule.m_strName;
            item.SubItems[1].Text = objRule.m_strAlias;
            item.SubItems[2].Text = objRule.m_strDesc;
            item.SubItems[3].Text = objRule.m_strFormula;
            item.SubItems[4].Text = objRule.m_strSummary;
            item.SubItems[5].Text = objRule.m_enmDefaultflag == enmQCRuleDefault.YEA ? "是" : "否";
            item.SubItems[6].Text = objRule.m_enmWarnType == enmQCRuleWarnLevel.Error ? "失控" : "警告";
        }

        private void m_mthRulesDetailClear()
        {
            this.m_txtRuleName.Clear();
            this.m_txtRuleAliasName.Clear();
            this.m_txtRuleDesc.Clear();
            this.m_txtFormula.Clear();
            this.m_txtSummary.Clear();
            this.m_chkDefaultFlag.Checked = false;
            this.m_cboTypeFlag.SelectedIndex = 0;
        }

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            return;
        }

        private void m_mthShowRulesList(clsLisQCRuleVO[] objRulesArr)
        {
            this.m_lsvRules.BeginUpdate();//开始更新列表
            this.m_lsvRules.Items.Clear();

            foreach (clsLisQCRuleVO rule in objRulesArr)
            {
                ListViewItem item = m_mthlsvItemAddObject(rule);
                item.Tag = rule;
                this.m_lsvRules.Items.Add(item);
            }

            //重置状态标志
            this.m_blnNewRule = false;
            //清空明细
            m_mthRulesDetailClear();

            this.m_lsvRules.EndUpdate();//结束更新列表
        }
    }
}
