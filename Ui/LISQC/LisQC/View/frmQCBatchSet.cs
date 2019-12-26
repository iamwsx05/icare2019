using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using weCare.Core.Entity;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;
using PinkieControls;
using com.digitalwave.iCare.gui.LIS.QC.Control;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCBatchSet : frmMDI_Child_Base
    {
        // Fields
        private bool blnCanReturnArr;
        private bool blnMultipleChoice;
        internal Button button1;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewCheckBoxColumn Column2;
        internal DataGridViewCheckBoxColumn Column3;
        private IContainer components;
        internal GroupBox groupBox1;
        internal Label label1;
        internal Label label10;
        internal Label label11;
        internal Label label15;
        internal Label label16;
        internal Label label17;
        internal Label label18;
        internal Label label19;
        internal Label label2;
        internal Label label20;
        internal Label label21;
        internal Label label3;
        internal Label label4;
        internal Label label5;
        internal Label label6;
        internal Label label7;
        internal Label label8;
        internal Label label9;
        internal ctlCheckMethodCombox m_cboCheckMethod;
        internal ctlLISDeviceComboBox m_cboLisDevice;
        internal ctlVendorCombox m_cboQCSampleSource;
        internal ctlVendorCombox m_cboQCSqmpleVendor;
        internal ctlVendorCombox m_cboReagentVendor;
        internal ctlWorkGroupCombox m_cboWorkGroup;
        internal CheckBox m_chkIsMachine;
        internal ButtonXP m_cmdCancel;
        internal ButtonXP m_cmdConfirm;
        internal DataGridView m_dtgQCRules;
        internal DateTimePicker m_dtpBeginDate;
        internal DateTimePicker m_dtpEndDate;
        //private clsDcl_QCBatchManage m_objDomain;
        internal Panel m_pnlBottom;
        internal Panel m_pnlInfo;
        internal ctlEmpTextBox m_txtAppDoct;
        internal TextBox m_txtQCBatchSeq;
        internal TextBox m_txtQCCheckItem;
        internal TextBox m_txtQCSampleLotNO;
        internal TextBox m_txtReagentLotNO;
        internal TextBox m_txtResultUnit;
        internal TextBox m_txtSummary;
        internal TextBox m_txtWaveLength;
        private clsLisQCBatchVO objQCBatch;
        private clsLisQCBatchVO[] objQCBatchArr;
        private frmQCCheckItemSelector QCselector;
        private frmCheckItemSelector selector;

        // Methods
        public frmQCBatchSet()
        {
            this.components = null;
            // this.m_objDomain = new clsDcl_QCBatchManage();
            this.blnCanReturnArr = false;
            this.blnMultipleChoice = false;
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num;
            bool flag;
            this.QCselector = new frmQCCheckItemSelector();
            if (this.m_cboLisDevice.SelectedValue != null && this.m_chkIsMachine.Checked)
            {
                this.QCselector.m_strDeviceId = this.m_cboLisDevice.SelectedValue.ToString();
            }
            if (this.QCselector.ShowDialog() != DialogResult.OK)
            {
                goto Label_0128;
            }
            if (this.QCselector.m_strQCCheckItemName != null)
            {
                goto Label_007F;
            }
            goto Label_0128;
        Label_007F:
            if (((int)this.QCselector.m_strQCCheckItemName.Length) <= 0)
            {
                goto Label_0127;
            }
            num = 0;
            goto Label_00D2;
        Label_009E:
            this.m_txtQCCheckItem.Text = this.m_txtQCCheckItem.Text + this.QCselector.m_strQCCheckItemName[num] + ",";
            num += 1;
        Label_00D2:
            if ((num < (((int)this.QCselector.m_strQCCheckItemName.Length) - 1)))
            {
                goto Label_009E;
            }
            this.m_txtQCCheckItem.Text = this.m_txtQCCheckItem.Text + this.QCselector.m_strQCCheckItemName[((int)this.QCselector.m_strQCCheckItemName.Length) - 1];
            this.blnMultipleChoice = true;
        Label_0127:;
        Label_0128:
            return;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    this.Dispose(disposing);
        //}

        private void frmQCBatch_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            this.m_mthSetKeyTab(e);
        }

        private void frmQCBatchSet_Load(object sender, EventArgs e)
        {
            if (this.objQCBatch == null)
            {
                this.objQCBatch = new clsLisQCBatchVO();
                this.m_mthResetAll();
            }
            else
            {
                m_mthDisplayVO();
            }
        }

        private void InitializeComponent()
        {
            DataGridViewColumn[] columnArray;
            this.m_pnlInfo = new Panel();
            this.m_txtAppDoct = new ctlEmpTextBox();
            this.m_chkIsMachine = new CheckBox();
            this.m_cboLisDevice = new ctlLISDeviceComboBox();
            this.label21 = new Label();
            this.label20 = new Label();
            this.m_dtgQCRules = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewCheckBoxColumn();
            this.Column3 = new DataGridViewCheckBoxColumn();
            this.m_txtSummary = new TextBox();
            this.m_dtpEndDate = new DateTimePicker();
            this.m_dtpBeginDate = new DateTimePicker();
            this.m_txtResultUnit = new TextBox();
            this.m_txtWaveLength = new TextBox();
            this.label19 = new Label();
            this.label18 = new Label();
            this.label17 = new Label();
            this.label16 = new Label();
            this.label15 = new Label();
            this.label11 = new Label();
            this.label10 = new Label();
            this.m_cboCheckMethod = new ctlCheckMethodCombox();
            this.label9 = new Label();
            this.m_cboReagentVendor = new ctlVendorCombox();
            this.label7 = new Label();
            this.m_txtReagentLotNO = new TextBox();
            this.label8 = new Label();
            this.m_cboQCSqmpleVendor = new ctlVendorCombox();
            this.m_cboQCSampleSource = new ctlVendorCombox();
            this.label6 = new Label();
            this.label5 = new Label();
            this.m_txtQCSampleLotNO = new TextBox();
            this.label4 = new Label();
            this.button1 = new Button();
            this.m_txtQCCheckItem = new TextBox();
            this.label3 = new Label();
            this.m_cboWorkGroup = new ctlWorkGroupCombox();
            this.label2 = new Label();
            this.m_txtQCBatchSeq = new TextBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.m_pnlBottom = new Panel();
            this.m_cmdConfirm = new ButtonXP();
            this.m_cmdCancel = new ButtonXP();
            this.m_pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgQCRules)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.m_pnlBottom.SuspendLayout();
            this.SuspendLayout(); 
            this.m_pnlInfo.Controls.Add(this.m_txtAppDoct);
            this.m_pnlInfo.Controls.Add(this.m_chkIsMachine);
            this.m_pnlInfo.Controls.Add(this.m_cboLisDevice);
            this.m_pnlInfo.Controls.Add(this.label21);
            this.m_pnlInfo.Controls.Add(this.label20);
            this.m_pnlInfo.Controls.Add(this.m_dtgQCRules);
            this.m_pnlInfo.Controls.Add(this.m_txtSummary);
            this.m_pnlInfo.Controls.Add(this.m_dtpEndDate);
            this.m_pnlInfo.Controls.Add(this.m_dtpBeginDate);
            this.m_pnlInfo.Controls.Add(this.m_txtResultUnit);
            this.m_pnlInfo.Controls.Add(this.m_txtWaveLength);
            this.m_pnlInfo.Controls.Add(this.label19);
            this.m_pnlInfo.Controls.Add(this.label18);
            this.m_pnlInfo.Controls.Add(this.label17);
            this.m_pnlInfo.Controls.Add(this.label16);
            this.m_pnlInfo.Controls.Add(this.label15);
            this.m_pnlInfo.Controls.Add(this.label11);
            this.m_pnlInfo.Controls.Add(this.label10);
            this.m_pnlInfo.Controls.Add(this.m_cboCheckMethod);
            this.m_pnlInfo.Controls.Add(this.label9);
            this.m_pnlInfo.Controls.Add(this.m_cboReagentVendor);
            this.m_pnlInfo.Controls.Add(this.label7);
            this.m_pnlInfo.Controls.Add(this.m_txtReagentLotNO);
            this.m_pnlInfo.Controls.Add(this.label8);
            this.m_pnlInfo.Controls.Add(this.m_cboQCSqmpleVendor);
            this.m_pnlInfo.Controls.Add(this.m_cboQCSampleSource);
            this.m_pnlInfo.Controls.Add(this.label6);
            this.m_pnlInfo.Controls.Add(this.label5);
            this.m_pnlInfo.Controls.Add(this.m_txtQCSampleLotNO);
            this.m_pnlInfo.Controls.Add(this.label4);
            this.m_pnlInfo.Controls.Add(this.button1);
            this.m_pnlInfo.Controls.Add(this.m_txtQCCheckItem);
            this.m_pnlInfo.Controls.Add(this.label3);
            this.m_pnlInfo.Controls.Add(this.m_cboWorkGroup);
            this.m_pnlInfo.Controls.Add(this.label2);
            this.m_pnlInfo.Controls.Add(this.m_txtQCBatchSeq);
            this.m_pnlInfo.Controls.Add(this.label1);
            this.m_pnlInfo.Dock = DockStyle.Fill;
            this.m_pnlInfo.Location = new Point(3, 0x13);
            this.m_pnlInfo.Name = "m_pnlInfo";
            this.m_pnlInfo.Size = new Size(0x269, 0x197);
            this.m_pnlInfo.TabIndex = 0;
            //this.m_txtAppDoct.EnableAutoValidation = 1;
            //this.m_txtAppDoct.EnableEnterKeyValidate = 1;
            //this.m_txtAppDoct.EnableEscapeKeyUndo = 1;
            //this.m_txtAppDoct.EnableLastValidValue = 1;
            //this.m_txtAppDoct.ErrorProvider = null;
            //this.m_txtAppDoct.ErrorProviderMessage = "Invalid value";
            this.m_txtAppDoct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtAppDoct.ForceFormatText = 1;
            this.m_txtAppDoct.ForeColor = SystemColors.WindowText;
            this.m_txtAppDoct.Location = new Point(0x5c, 0x16c);
            this.m_txtAppDoct.m_intShowOtherEmp = 0;
            this.m_txtAppDoct.m_StrDeptID = "*";
            this.m_txtAppDoct.m_StrEmployeeID = null;
            this.m_txtAppDoct.m_StrEmployeeName = null;
            this.m_txtAppDoct.MaxLength = 20;
            this.m_txtAppDoct.Name = "m_txtAppDoct";
            this.m_txtAppDoct.Size = new Size(120, 0x17);
            this.m_txtAppDoct.TabIndex = 0x11;
            this.m_chkIsMachine.AutoSize = true;
            this.m_chkIsMachine.Location = new Point(0xd7, 80);
            this.m_chkIsMachine.Name = "m_chkIsMachine";
            this.m_chkIsMachine.Size = new Size(15, 14);
            this.m_chkIsMachine.TabIndex = 0x2b;
            this.m_chkIsMachine.UseVisualStyleBackColor = true;
            this.m_chkIsMachine.Visible = false;
            this.m_chkIsMachine.CheckedChanged += new EventHandler(this.m_chkIsMachine_CheckedChanged);
            this.m_cboLisDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboLisDevice.Enabled = false;
            this.m_cboLisDevice.FormattingEnabled = true;
            this.m_cboLisDevice.Location = new Point(0x5c, 0x4c);
            this.m_cboLisDevice.Name = "m_cboLisDevice";
            this.m_cboLisDevice.Size = new Size(120, 0x16);
            this.m_cboLisDevice.TabIndex = 2;
            this.label21.AutoSize = true;
            this.label21.Location = new Point(20, 80);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x4d, 14);
            this.label21.TabIndex = 40;
            this.label21.Text = "检测仪器：";
            this.label20.AutoSize = true;
            this.label20.BackColor = SystemColors.Control;
            this.label20.Location = new Point(0xc0, 0x110);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x15, 14);
            this.label20.TabIndex = 0;
            this.label20.Text = "nm";
            this.m_dtgQCRules.AllowUserToAddRows = false;
            this.m_dtgQCRules.AllowUserToDeleteRows = false;
            this.m_dtgQCRules.BackgroundColor = SystemColors.Window;
            this.m_dtgQCRules.BorderStyle = BorderStyle.Fixed3D;
            this.m_dtgQCRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dtgQCRules.Columns.AddRange(new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3 });
            this.m_dtgQCRules.Location = new Point(0xf4, 0x84);
            this.m_dtgQCRules.MultiSelect = false;
            this.m_dtgQCRules.Name = "m_dtgQCRules";
            this.m_dtgQCRules.RowHeadersVisible = false;
            this.m_dtgQCRules.RowTemplate.Height = 0x17;
            this.m_dtgQCRules.Size = new Size(0x164, 0x100);
            this.m_dtgQCRules.TabIndex = 0x13;
            this.Column1.DataPropertyName = "ruleName";
            this.Column1.HeaderText = "质控规则";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 90;
            this.Column2.DataPropertyName = "ruleChoice";
            this.Column2.HeaderText = "选择";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            this.Column3.DataPropertyName = "ruleWarning";
            this.Column3.HeaderText = "报警";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            this.m_txtSummary.ImeMode = ImeMode.On;
            this.m_txtSummary.Location = new Point(0xf4, 20);
            this.m_txtSummary.MaxLength = 100;
            this.m_txtSummary.Multiline = true;
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new Size(360, 0x5c);
            this.m_txtSummary.TabIndex = 0x12;
            this.m_dtpEndDate.Location = new Point(0x5c, 340);
            this.m_dtpEndDate.Name = "m_dtpEndDate";
            this.m_dtpEndDate.Size = new Size(120, 0x17);
            this.m_dtpEndDate.TabIndex = 0x10;
            this.m_dtpBeginDate.Location = new Point(0x5c, 0x13c);
            this.m_dtpBeginDate.Name = "m_dtpBeginDate";
            this.m_dtpBeginDate.Size = new Size(120, 0x17);
            this.m_dtpBeginDate.TabIndex = 15;
            this.m_txtResultUnit.ImeMode = ImeMode.Off;
            this.m_txtResultUnit.Location = new Point(0x5c, 0x124);
            this.m_txtResultUnit.MaxLength = 10;
            this.m_txtResultUnit.Name = "m_txtResultUnit";
            this.m_txtResultUnit.Size = new Size(120, 0x17);
            this.m_txtResultUnit.TabIndex = 14;
            this.m_txtWaveLength.ImeMode = ImeMode.Off;
            this.m_txtWaveLength.Location = new Point(0x5c, 0x10c);
            this.m_txtWaveLength.MaxLength = 6;
            this.m_txtWaveLength.Name = "m_txtWaveLength";
            this.m_txtWaveLength.Size = new Size(100, 0x17);
            this.m_txtWaveLength.TabIndex = 10;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x22, 0x170);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x3f, 14);
            this.label19.TabIndex = 0x1c;
            this.label19.Text = "操作者：";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(240, 4);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x31, 14);
            this.label18.TabIndex = 0x1b;
            this.label18.Text = "备注：";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(20, 0x158);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x4d, 14);
            this.label17.TabIndex = 0x1a;
            this.label17.Text = "结束日期：";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(20, 320);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x4d, 14);
            this.label16.TabIndex = 0x19;
            this.label16.Text = "开始日期：";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(20, 0x128);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x4d, 14);
            this.label15.TabIndex = 0x18;
            this.label15.Text = "结果单位：";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xf4, 0x74);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x5b, 14);
            this.label11.TabIndex = 20;
            this.label11.Text = "质控规则组：";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(20, 0x110);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x4d, 14);
            this.label10.TabIndex = 0x13;
            this.label10.Text = "检测波长：";
            this.m_cboCheckMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboCheckMethod.FormattingEnabled = true;
            this.m_cboCheckMethod.Location = new Point(0x5c, 0xf4);
            this.m_cboCheckMethod.Name = "m_cboCheckMethod";
            this.m_cboCheckMethod.Size = new Size(120, 0x16);
            this.m_cboCheckMethod.TabIndex = 9;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(20, 0xf8);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4d, 14);
            this.label9.TabIndex = 0x11;
            this.label9.Text = "检测方法：";
            this.m_cboReagentVendor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboReagentVendor.FormattingEnabled = true;
            this.m_cboReagentVendor.Location = new Point(0x5c, 220);
            this.m_cboReagentVendor.Name = "m_cboReagentVendor";
            this.m_cboReagentVendor.Size = new Size(120, 0x16);
            this.m_cboReagentVendor.TabIndex = 8;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(20, 0xe0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4d, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "试剂厂商：";
            this.m_txtReagentLotNO.ImeMode = ImeMode.Off;
            this.m_txtReagentLotNO.Location = new Point(0x5c, 0xc4);
            this.m_txtReagentLotNO.MaxLength = 0x19;
            this.m_txtReagentLotNO.Name = "m_txtReagentLotNO";
            this.m_txtReagentLotNO.Size = new Size(120, 0x17);
            this.m_txtReagentLotNO.TabIndex = 7;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(20, 200);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4d, 14);
            this.label8.TabIndex = 13;
            this.label8.Text = "试剂批号：";
            this.m_cboQCSqmpleVendor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboQCSqmpleVendor.FormattingEnabled = true;
            this.m_cboQCSqmpleVendor.Location = new Point(0x5c, 0xac);
            this.m_cboQCSqmpleVendor.Name = "m_cboQCSqmpleVendor";
            this.m_cboQCSqmpleVendor.Size = new Size(120, 0x16);
            this.m_cboQCSqmpleVendor.TabIndex = 6;
            this.m_cboQCSampleSource.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboQCSampleSource.FormattingEnabled = true;
            this.m_cboQCSampleSource.Location = new Point(0x5c, 0x94);
            this.m_cboQCSampleSource.Name = "m_cboQCSampleSource";
            this.m_cboQCSampleSource.Size = new Size(120, 0x16);
            this.m_cboQCSampleSource.TabIndex = 5;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(6, 0xb0);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x5b, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "质控品厂商：";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 0x98);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x5b, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "质控品来源：";
            this.m_txtQCSampleLotNO.ImeMode = ImeMode.Off;
            this.m_txtQCSampleLotNO.Location = new Point(0x5c, 0x7c);
            this.m_txtQCSampleLotNO.MaxLength = 0x19;
            this.m_txtQCSampleLotNO.Name = "m_txtQCSampleLotNO";
            this.m_txtQCSampleLotNO.Size = new Size(120, 0x17);
            this.m_txtQCSampleLotNO.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 0x80);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x5b, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "质控品批号：";
            this.button1.FlatStyle = FlatStyle.System;
            this.button1.Location = new Point(0xc0, 100);
            this.button1.Name = "button1";
            this.button1.Size = new Size(20, 0x17);
            this.button1.TabIndex = 3;
            this.button1.Text = "┅";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.m_txtQCCheckItem.BackColor = SystemColors.Window;
            this.m_txtQCCheckItem.Location = new Point(0x5c, 100);
            this.m_txtQCCheckItem.Name = "m_txtQCCheckItem";
            this.m_txtQCCheckItem.ReadOnly = true;
            this.m_txtQCCheckItem.Size = new Size(100, 0x17);
            this.m_txtQCCheckItem.TabIndex = 100;
            this.m_txtQCCheckItem.TabStop = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 0x68);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "质控项目：";
            this.m_cboWorkGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            this.m_cboWorkGroup.FormattingEnabled = true;
            this.m_cboWorkGroup.Location = new Point(0x5c, 0x34);
            this.m_cboWorkGroup.Name = "m_cboWorkGroup";
            this.m_cboWorkGroup.Size = new Size(120, 0x16);
            this.m_cboWorkGroup.TabIndex = 1;
            this.m_cboWorkGroup.Value = -2147483648;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x22, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3f, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "工作组：";
            this.m_txtQCBatchSeq.BackColor = SystemColors.Info;
            this.m_txtQCBatchSeq.Location = new Point(0x5c, 0x1c);
            this.m_txtQCBatchSeq.MaxLength = 6;
            this.m_txtQCBatchSeq.Name = "m_txtQCBatchSeq";
            this.m_txtQCBatchSeq.ReadOnly = true;
            this.m_txtQCBatchSeq.Size = new Size(120, 0x17);
            this.m_txtQCBatchSeq.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x30, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x31, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "序号：";
            this.groupBox1.Controls.Add(this.m_pnlInfo);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x26f, 0x1ad);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.m_pnlBottom.Controls.Add(this.m_cmdConfirm);
            this.m_pnlBottom.Controls.Add(this.m_cmdCancel);
            this.m_pnlBottom.Dock = DockStyle.Bottom;
            this.m_pnlBottom.Location = new Point(0, 0x1ad);
            this.m_pnlBottom.Name = "m_pnlBottom";
            this.m_pnlBottom.Size = new Size(0x26f, 60);
            this.m_pnlBottom.TabIndex = 2;
            this.m_cmdConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdConfirm.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdConfirm.DefaultScheme = true;
            this.m_cmdConfirm.DialogResult = 0;
            this.m_cmdConfirm.Hint = "";
            this.m_cmdConfirm.Location = new Point(380, 12);
            this.m_cmdConfirm.Name = "m_cmdConfirm";
            this.m_cmdConfirm.Scheme = 0;
            this.m_cmdConfirm.Size = new Size(0x62, 0x21);
            this.m_cmdConfirm.TabIndex = 0x11;
            this.m_cmdConfirm.Text = "确定";
            this.m_cmdConfirm.Click += new EventHandler(this.m_cmdConfirm_Click);
            this.m_cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCancel.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdCancel.DefaultScheme = true;
            this.m_cmdCancel.DialogResult = DialogResult.Cancel;
            this.m_cmdCancel.Hint = "";
            this.m_cmdCancel.Location = new Point(0x1ec, 12);
            this.m_cmdCancel.Name = "m_cmdCancel";
            this.m_cmdCancel.Scheme = 0;
            this.m_cmdCancel.Size = new Size(0x62, 0x21);
            this.m_cmdCancel.TabIndex = 0x12;
            this.m_cmdCancel.Text = "取消(ESC)";
            this.m_cmdCancel.Click += new EventHandler(this.m_cmdCancel_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.m_cmdCancel;
            this.ClientSize = new Size(0x26f, 0x1e9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_pnlBottom);
            this.Font = new Font("宋体", 10.5f);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQCBatchSet";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "质控批设置";
            this.KeyDown += new KeyEventHandler(this.frmQCBatch_KeyDown);
            this.Load += new EventHandler(this.frmQCBatchSet_Load);
            this.m_pnlInfo.ResumeLayout(false);
            this.m_pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtgQCRules)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.m_pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private List<QualityControlRule> m_arrGetRuelsFromVO()
        {
            clsLisQCBatchVO objBatch = this.objQCBatch;
            QcParserXmlRules parser = new QcParserXmlRules(objBatch.m_strQCRules);
            return parser.RuleList;
        }

        private clsLisQCRuleVO[] m_arrGetRulesFromBase()
        {
            clsLisQCRuleVO[] evoArray = null;
            (new weCare.Proxy.ProxyLis02()).Service.m_lngFindQCRule(out evoArray);
            return evoArray;
        }

        private bool m_blnCheckNum()
        {
            if (this.m_txtWaveLength.Text.Trim() != string.Empty)
            {
                double b;
                if (!double.TryParse(this.m_txtWaveLength.Text.Trim(), out b))
                {
                    MessageBox.Show("请输入数字!", "iCare");
                    this.m_txtWaveLength.Focus();
                    this.m_txtWaveLength.SelectAll();
                    return false;
                }
            }
            return true;
        }

        private void m_chkIsMachine_CheckedChanged(object sender, EventArgs e)
        {
            this.m_cboLisDevice.Enabled = this.m_chkIsMachine.Checked;
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void m_cmdConfirm_Click(object sender, EventArgs e)
        {
            long num;
            int num2;
            clsLISCheckItemNode[] nodeArray;
            clsLisQCBatchVO hvo;
            int num3;
            int[] numArray;
            bool flag;
            clsLisQCBatchVO[] hvoArray;
            num = 0L;
            if (this.m_blnCheckNum())
            {
                goto Label_0016;
            }
            goto Label_037F;
        Label_0016:
            this.m_mthConstructVO();
            if (!string.IsNullOrEmpty(this.objQCBatch.m_strSampleLotNo))
            {
                goto Label_004C;
            }
            MessageBox.Show("请输入质控批号！", "iCare");
            goto Label_037F;
        Label_004C:
            num2 = 1;
            if (!(this.objQCBatch.m_strCheckItemId == null || this.objQCBatch.m_strCheckItemId == ""))
            {
                goto Label_0195;
            }
            if (!string.IsNullOrEmpty(this.objQCBatch.m_strDeviceId))
            {
                goto Label_00AF;
            }
            MessageBox.Show("请选择要做质控的仪器或项目", "iCare");
            goto Label_037F;
        Label_00AF:
            if (!this.M_blnCanReturnArr)
            {
                goto Label_017E;
            }
            nodeArray = null;
            (new weCare.Proxy.ProxyLis02()).Service.m_lngGetDeviceQCCheckItemByID(this.objQCBatch.m_strDeviceId, out nodeArray);
            if (nodeArray == null || nodeArray.Length <= 0)
            {
                MessageBox.Show("请选设置该仪器的质控项目！", "iCare");
                goto Label_037F;
            }
            else
            {
                goto Label_0105;
            }

        Label_0105:
            this.objQCBatchArr = new clsLisQCBatchVO[(int)nodeArray.Length];
            hvo = null;
            num3 = 0;
            goto Label_016C;
        Label_011A:
            hvo = new clsLisQCBatchVO();
            this.objQCBatch.m_mthCopyTo(hvo);
            hvo.m_strCheckItemId = nodeArray[num3].strID;
            hvo.m_strCheckItemName = nodeArray[num3].strName;
            hvo.m_strSortNum = Convert.ToString(num3 + 1);
            this.objQCBatchArr[num3] = hvo;
            num3 += 1;
        Label_016C:
            if (num3 < ((int)nodeArray.Length))
            {
                goto Label_011A;
            }
            num2 = 2;
            goto Label_0194;
        Label_017E:
            MessageBox.Show("请选择要做质控的项目", "iCare");
            goto Label_037F;
        Label_0194:;
        Label_0195:
            if (!this.blnMultipleChoice)
            {
                goto Label_0273;
            }
            if (((int)this.QCselector.m_strQCCheckItemID.Length) <= 0)
            {
                goto Label_025C;
            }
            this.objQCBatchArr = new clsLisQCBatchVO[(int)this.QCselector.m_strQCCheckItemID.Length];
            hvo = null;
            num3 = 0;
            goto Label_0240;
        Label_01E4:
            hvo = new clsLisQCBatchVO();
            this.objQCBatch.m_mthCopyTo(hvo);
            hvo.m_strCheckItemId = this.QCselector.m_strQCCheckItemID[num3];
            hvo.m_strCheckItemName = this.QCselector.m_strQCCheckItemName[num3];
            hvo.m_strSortNum = Convert.ToString(num3 + 1);
            this.objQCBatchArr[num3] = hvo;
            num3 += 1;
        Label_0240:
            if (num3 < this.QCselector.m_strQCCheckItemID.Length)
            {
                goto Label_01E4;
            }
            num2 = 2;
            goto Label_0272;
        Label_025C:
            MessageBox.Show("请选择要做的质控项目", "新增质控批提示");
            goto Label_037F;
        Label_0272:;
        Label_0273:
            if (num2 != 2)
            {
                goto Label_02DE;
            }
            numArray = null;
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCBatchByArr(this.objQCBatchArr, out numArray);
            if (numArray != null)
            {
                num3 = 0;
                goto Label_02CC;
            }
            else
            {
                goto Label_02DB;
            }
        Label_02B1:
            this.objQCBatchArr[num3].m_intSeq = numArray[num3];
            num3 += 1;
        Label_02CC:
            if (num3 < numArray.Length)
            {
                goto Label_02B1;
            }
        Label_02DB:
            goto Label_0333;
        Label_02DE:
            if (this.m_txtQCBatchSeq.Text != string.Empty)
            {
                goto Label_031E;
            }
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCBatch(this.objQCBatch, out this.objQCBatch.m_intSeq);
            goto Label_0332;
        Label_031E:
            num = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCBatch(this.objQCBatch);
        Label_0332:;
        Label_0333:
            if (num > 0L)
            {
                goto Label_0348;
            }
            clsCommonDialog.m_mthShowDBError();
            goto Label_037F;
        Label_0348:
            if (num2 == 2)
            {
                goto Label_036F;
            }
            this.objQCBatchArr = new clsLisQCBatchVO[] { this.objQCBatch };
        Label_036F:
            this.DialogResult = DialogResult.OK;
            this.Close();
        Label_037F:
            return;
        }

        private DataTable m_dtbGetRulesFromBase()
        {
            clsLisQCRuleVO[] rules = m_arrGetRulesFromBase();

            DataTable dtbRules = new DataTable();
            dtbRules.Columns.Add("ruleName", typeof(System.String));
            dtbRules.Columns.Add("ruleChoice", typeof(System.Boolean));
            dtbRules.Columns.Add("ruleWarning", typeof(System.Boolean));

            foreach (clsLisQCRuleVO vo in rules)
            {
                dtbRules.Rows.Add(vo.m_strName, vo.m_enmDefaultflag == enmQCRuleDefault.YEA ? true : false,
                                  vo.m_enmWarnType == enmQCRuleWarnLevel.Warning ? true : false);
            }
            return dtbRules;
        }

        private DataTable m_dtbGetRulesFromVO()
        {
            clsLisQCBatchVO objBatch = this.objQCBatch;

            List<QualityControlRule> rules = m_arrGetRuelsFromVO();

            DataTable dtbRules = new DataTable();
            dtbRules.Columns.Add("ruleName", typeof(System.String));
            dtbRules.Columns.Add("ruleChoice", typeof(System.Boolean));
            dtbRules.Columns.Add("ruleWarning", typeof(System.Boolean));

            Hashtable hasRules = new Hashtable();

            if (rules != null)
            {
                foreach (QualityControlRule vo in rules)
                {
                    dtbRules.Rows.Add(vo.Name, true, vo.IsWarning);
                    hasRules.Add(vo.Name, "");
                }
            }


            foreach (clsLisQCRuleVO vo in m_arrGetRulesFromBase())
            {
                if (!hasRules.Contains(vo.m_strName))
                {
                    dtbRules.Rows.Add(vo.m_strName, false, vo.m_enmWarnType == enmQCRuleWarnLevel.Warning ? true : false);
                }
            }

            return dtbRules;
        }

        private void m_mthConstructVO()
        {
            clsLisQCBatchVO objBatch = objQCBatch;

            objBatch.m_intWorkGroupSeq = m_cboWorkGroup.Value;
            objBatch.m_strWorkGroupName = m_cboWorkGroup.Text;
            if (this.m_cboLisDevice.SelectedValue != null && this.m_chkIsMachine.Checked)
            {
                objBatch.m_strDeviceId = this.m_cboLisDevice.SelectedValue.ToString();
                objBatch.m_strDeviceName = this.m_cboLisDevice.Text;
            }
            else { objQCBatch.m_strDeviceId = string.Empty; }

            objBatch.m_strCheckItemId = m_txtQCCheckItem.Tag as string;
            objBatch.m_strCheckItemName = m_txtQCCheckItem.Text;
            objBatch.m_strSampleLotNo = m_txtQCSampleLotNO.Text.Trim();
            objBatch.m_strSampleSource = m_cboQCSampleSource.Text;
            objBatch.m_strSampleVendor = m_cboQCSqmpleVendor.Text;
            objBatch.m_strReagent = m_cboReagentVendor.Text.Trim();
            objBatch.m_strReagentBatch = m_txtReagentLotNO.Text.Trim();
            objBatch.m_strCheckmethodName = m_cboCheckMethod.Text;

            try { objBatch.m_dblWaveLength = Convert.ToDouble(m_txtWaveLength.Text.Trim()); }
            catch { objBatch.m_dblWaveLength = DBAssist.NullDouble; }


            objBatch.m_strResultUnit = m_txtResultUnit.Text.Trim();
            objBatch.m_dtBegin = DateTime.Parse(m_dtpBeginDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objBatch.m_dtEnd = DateTime.Parse(m_dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objBatch.m_strOperatorId = m_txtAppDoct.m_StrEmployeeID;
            objBatch.m_strSummary = m_txtSummary.Text;
            //质控规则
            objBatch.m_strQCRules = m_strConstructXmlRules();
            objQCBatch.m_enmStatus = enmQCStatus.Natrural;
            this.objQCBatch.m_strSortNum = "1";
        }

        private void m_mthDisplayVO()
        {
            clsLisQCBatchVO objBatch = this.objQCBatch;

            m_txtQCBatchSeq.Text = DBAssist.ToString(objBatch.m_intSeq);
            m_cboWorkGroup.Value = objBatch.m_intWorkGroupSeq;
            if (objQCBatch.m_strDeviceId != string.Empty)
            {
                try { this.m_cboLisDevice.SelectedValue = objBatch.m_strDeviceId; }
                catch { }
                this.m_chkIsMachine.Checked = true;
            }
            else
            {
                this.m_chkIsMachine.Checked = false;
            }
            m_txtQCCheckItem.Tag = objBatch.m_strCheckItemId;
            m_txtQCCheckItem.Text = objBatch.m_strCheckItemName;
            m_txtQCSampleLotNO.Text = objBatch.m_strSampleLotNo;
            m_cboQCSampleSource.Text = objBatch.m_strSampleSource;
            m_cboQCSqmpleVendor.Text = objBatch.m_strSampleVendor;
            m_txtReagentLotNO.Text = objBatch.m_strReagentBatch;
            m_cboReagentVendor.Text = objBatch.m_strReagent;
            m_cboCheckMethod.Text = objBatch.m_strCheckmethodName;
            m_txtWaveLength.Text = DBAssist.ToString(objBatch.m_dblWaveLength);
            m_txtResultUnit.Text = objBatch.m_strResultUnit;
            m_dtpBeginDate.Value = objBatch.m_dtBegin;
            m_dtpEndDate.Value = objBatch.m_dtEnd;
            m_txtAppDoct.m_StrEmployeeID = objBatch.m_strOperatorId;
            m_txtSummary.Text = objBatch.m_strSummary;

            //质控规则
            //...
            m_dtgQCRules.DataSource = m_dtbGetRulesFromVO();
        }

        private void m_mthResetAll()
        {
            m_txtQCBatchSeq.Clear();
            try { m_cboWorkGroup.SelectedIndex = 0; }
            catch { }
            try
            {
                this.m_cboLisDevice.SelectedIndex = 0;
                this.m_cboLisDevice.Enabled = true;
                this.m_chkIsMachine.Checked = true;
            }
            catch { }
            m_txtQCCheckItem.Clear();
            m_txtQCCheckItem.Tag = null;
            m_txtQCSampleLotNO.Clear();
            try { m_cboQCSampleSource.SelectedIndex = 0; }
            catch { }
            try { m_cboQCSqmpleVendor.SelectedIndex = 0; }
            catch { }
            m_txtReagentLotNO.Clear();
            try { m_cboReagentVendor.SelectedIndex = 0; }
            catch { }
            try { m_cboCheckMethod.SelectedIndex = 0; }
            catch { }
            m_txtWaveLength.Clear();
            m_txtResultUnit.Clear();
            m_dtpBeginDate.Value = DateTime.Now;
            m_dtpEndDate.Value = DateTime.Now;
            m_txtAppDoct.m_StrEmployeeID = this.LoginInfo.m_strEmpID;
            m_txtSummary.Clear();
            m_dtgQCRules.DataSource = m_dtbGetRulesFromBase();
        }

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            return;
        }

        private string m_strConstructXmlRules()
        {
            Hashtable hasChoice = new Hashtable();
            clsLisQCRuleVO[] rules = m_arrGetRulesFromBase();
            List<QualityControlRule> qcRules = new List<QualityControlRule>();
            for (int i = 0; i < m_dtgQCRules.Rows.Count; i++)
            {
                if ((bool)m_dtgQCRules.Rows[i].Cells[1].Value)
                {
                    hasChoice.Add(m_dtgQCRules.Rows[i].Cells[0].Value, (bool)m_dtgQCRules.Rows[i].Cells[2].Value);
                }

            }

            foreach (clsLisQCRuleVO vo in rules)
            {
                if (hasChoice.Contains(vo.m_strName))
                {
                    QualityControlRule rule = null;
                    QcParserXmlRules parser = new QcParserXmlRules(vo.m_strFormula);
                    if (parser.Rule != null)
                    {
                        rule = parser.Rule;
                        rule.IsWarning = (bool)hasChoice[vo.m_strName];
                        rule.Name = vo.m_strName;
                        qcRules.Add(rule);
                    }
                }
            }

            return QcParserXmlRules.ParserRuleArrToXmlString(qcRules);
        }

        // Properties
        public bool M_blnCanReturnArr
        {
            get { return blnCanReturnArr; }
            set { blnCanReturnArr = value; }
        }

        public clsLisQCBatchVO QCBatchVO
        {
            get { return objQCBatch; }
            set { objQCBatch = value; }
        }

        public clsLisQCBatchVO[] QCBatchVOArr
        {
            get { return objQCBatchArr; }
        }
    }
}
