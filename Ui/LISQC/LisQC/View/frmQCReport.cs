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
using com.digitalwave.iCare.gui.LIS.QC.Control;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmQCReport : frmMDI_Child_Base
    {
        // Fields
        private IContainer components;
        internal GroupBox groupBox1;
        internal Label label3;
        internal Label label4;
        internal Label label5;
        internal Label label6;
        internal Label label7;
        internal Label label8;
        internal bool m_blIsDate;
        private bool m_blnNew;
        private bool m_blnOK;
        internal ButtonXP m_btnCancel;
        internal ButtonXP m_btnSave;
        internal CheckBox m_chkLost;
        internal ButtonXP m_cmdAddBrokenRules;
        internal DateTime m_dtMonth;
        internal DateTimePicker m_dtpReport;
        private int m_intQCBatchSeq;
        internal Label m_lblBatchNO;
        internal Label m_lblSeq;
        //private clsDcl_QCDataBusiness m_objDomain;
        private clsLisQCReportVO m_objReport;
        private string m_strBrokenRules;
        internal ctlEmpTextBox m_txtAppDoct;
        internal TextBox m_txtProcess;
        internal TextBox m_txtReason;
        internal TextBox m_txtSummary;
        internal TextBox m_txtUnmatchedRule;
        internal Panel panel1;
        internal Panel panel2;

        // Methods
        public frmQCReport()
        {
            this.components = null;
            this.m_blnNew = false;
            this.m_blnOK = false;
            this.m_blIsDate = true;
            //this.m_objDomain = new clsDcl_QCDataBusiness(); 
            this.InitializeComponent();
        }

        public frmQCReport(clsLisQCReportVO report) : this()
        {
            this.m_objReport = report;
            this.m_dtMonth = report.m_dtReport;
            this.m_intQCBatchSeq = report.m_intQCBatchSeq;
        }

        public frmQCReport(int p_intQCBatchSeq) : this()
        {
            this.m_intQCBatchSeq = p_intQCBatchSeq;
            this.m_blnNew = true;
        }

        public frmQCReport(int p_intQCBatchSeq, DateTime p_dtMonth) : this()
        {
            this.m_intQCBatchSeq = p_intQCBatchSeq;
            this.m_dtMonth = p_dtMonth;
            this.m_blnNew = true;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    this.Dispose(disposing);
        //}

        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            this.m_mthSetKeyTab(e);
            return;
        }

        private void frmQCReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.m_blnOK)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void frmQCReport_Load(object sender, EventArgs e)
        {
            if (!this.m_blIsDate)
            {
                this.Text = "质控月报告";
                this.label8.Text = "报告月份";
                this.m_dtpReport.CustomFormat = "yyyy-MM";
                this.m_dtpReport.Value = Convert.ToDateTime(this.m_dtMonth.ToString("yyyy-MM"));
                this.m_dtpReport.Enabled = false;
            }
            else
            {
                this.Text = "质控报告";
                this.label8.Text = "报告日期";
                this.m_dtpReport.CustomFormat = "yyyy-MM-dd";
                this.m_dtpReport.Enabled = true;
            }
            this.m_mthControlsDisplayVOValue(this.m_objReport);
            if (this.m_blnNew)
            {
                this.m_txtUnmatchedRule.Text = this.m_strBrokenRules;
            }
        }

        private void InitializeComponent()
        {
            this.panel2 = new Panel();
            this.groupBox1 = new GroupBox();
            this.m_cmdAddBrokenRules = new ButtonXP();
            this.m_lblSeq = new Label();
            this.m_dtpReport = new DateTimePicker();
            this.m_chkLost = new CheckBox();
            this.label3 = new Label();
            this.m_txtAppDoct = new ctlEmpTextBox();
            this.m_txtUnmatchedRule = new TextBox();
            this.label8 = new Label();
            this.label4 = new Label();
            this.label7 = new Label();
            this.m_lblBatchNO = new Label();
            this.m_txtReason = new TextBox();
            this.label5 = new Label();
            this.m_txtProcess = new TextBox();
            this.m_txtSummary = new TextBox();
            this.label6 = new Label();
            this.panel1 = new Panel();
            this.m_btnCancel = new ButtonXP();
            this.m_btnSave = new ButtonXP();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1af, 0x170);
            this.panel2.TabIndex = 1;
            this.groupBox1.Controls.Add(this.m_cmdAddBrokenRules);
            this.groupBox1.Controls.Add(this.m_lblSeq);
            this.groupBox1.Controls.Add(this.m_dtpReport);
            this.groupBox1.Controls.Add(this.m_chkLost);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.m_txtAppDoct);
            this.groupBox1.Controls.Add(this.m_txtUnmatchedRule);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.m_lblBatchNO);
            this.groupBox1.Controls.Add(this.m_txtReason);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_txtProcess);
            this.groupBox1.Controls.Add(this.m_txtSummary);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1af, 0x170);
            this.groupBox1.TabIndex = 0x16;
            this.groupBox1.TabStop = false;
            this.m_cmdAddBrokenRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdAddBrokenRules.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_cmdAddBrokenRules.DefaultScheme = true;
            this.m_cmdAddBrokenRules.DialogResult = 0;
            this.m_cmdAddBrokenRules.Hint = "";
            this.m_cmdAddBrokenRules.Location = new Point(0x128, 0x2e);
            this.m_cmdAddBrokenRules.Name = "m_cmdAddBrokenRules";
            this.m_cmdAddBrokenRules.Scheme = 0;
            this.m_cmdAddBrokenRules.Size = new Size(0x7f, 0x1d);
            this.m_cmdAddBrokenRules.TabIndex = 7;
            this.m_cmdAddBrokenRules.Text = "复用分析结果";
            this.m_cmdAddBrokenRules.Click += new EventHandler(this.m_cmdAddBrokenRules_Click);
            this.m_lblSeq.AutoSize = true;
            this.m_lblSeq.Location = new Point(40, 0x16);
            this.m_lblSeq.Name = "m_lblSeq";
            this.m_lblSeq.Size = new Size(0x54, 14);
            this.m_lblSeq.TabIndex = 0;
            this.m_lblSeq.Text = "质控批序号:";
            this.m_dtpReport.CustomFormat = "yyyy-MM-dd";
            this.m_dtpReport.Format = DateTimePickerFormat.Custom;
            this.m_dtpReport.Location = new Point(0x121, 0x148);
            this.m_dtpReport.Name = "m_dtpReport";
            this.m_dtpReport.Size = new Size(0x65, 0x17);
            this.m_dtpReport.TabIndex = 6;
            this.m_chkLost.AutoSize = true;
            this.m_chkLost.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_chkLost.Location = new Point(0x33, 0x36);
            this.m_chkLost.Name = "m_chkLost";
            this.m_chkLost.Size = new Size(0x59, 0x12);
            this.m_chkLost.TabIndex = 0;
            this.m_chkLost.Text = "失    控:";
            this.m_chkLost.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(11, 0x57);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x70, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "违反的质控规则:";
            this.m_txtAppDoct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            //this.m_txtAppDoct.EnableAutoValidation = 1;
            //this.m_txtAppDoct.EnableEnterKeyValidate = 1;
            //this.m_txtAppDoct.EnableEscapeKeyUndo = 1;
            //this.m_txtAppDoct.EnableLastValidValue = 1;
            //this.m_txtAppDoct.ErrorProvider = null;
            //this.m_txtAppDoct.ErrorProviderMessage = "Invalid value";
            this.m_txtAppDoct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtAppDoct.ForceFormatText = 1;
            this.m_txtAppDoct.ForeColor = SystemColors.WindowText;
            this.m_txtAppDoct.Location = new Point(0x121, 0x12e);
            this.m_txtAppDoct.m_intShowOtherEmp = 0;
            this.m_txtAppDoct.m_StrDeptID = "*";
            this.m_txtAppDoct.m_StrEmployeeID = null;
            this.m_txtAppDoct.m_StrEmployeeName = null;
            this.m_txtAppDoct.MaxLength = 20;
            this.m_txtAppDoct.Name = "m_txtAppDoct";
            this.m_txtAppDoct.Size = new Size(0x65, 0x17);
            this.m_txtAppDoct.TabIndex = 5;
            this.m_txtUnmatchedRule.Location = new Point(0x7d, 0x4e);
            this.m_txtUnmatchedRule.MaxLength = 50;
            this.m_txtUnmatchedRule.Multiline = true;
            this.m_txtUnmatchedRule.Name = "m_txtUnmatchedRule";
            this.m_txtUnmatchedRule.Size = new Size(0x128, 0x25);
            this.m_txtUnmatchedRule.TabIndex = 1;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xdb, 0x14c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(70, 14);
            this.label8.TabIndex = 0x11;
            this.label8.Text = "报告日期:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x35, 0x87);
            this.label4.Name = "label4";
            this.label4.Size = new Size(70, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "失控原因:";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xe8, 0x134);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x38, 14);
            this.label7.TabIndex = 15;
            this.label7.Text = "报告人:";
            this.m_lblBatchNO.AutoSize = true;
            this.m_lblBatchNO.ForeColor = Color.Blue;
            this.m_lblBatchNO.Location = new Point(0x7e, 0x17);
            this.m_lblBatchNO.Name = "m_lblBatchNO";
            this.m_lblBatchNO.Size = new Size(0, 14);
            this.m_lblBatchNO.TabIndex = 0x12;
            this.m_txtReason.Location = new Point(0x7d, 0x7b);
            this.m_txtReason.MaxLength = 250;
            this.m_txtReason.Multiline = true;
            this.m_txtReason.Name = "m_txtReason";
            this.m_txtReason.Size = new Size(0x128, 0x30);
            this.m_txtReason.TabIndex = 2;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x35, 0xbf);
            this.label5.Name = "label5";
            this.label5.Size = new Size(70, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "失控处理:";
            this.m_txtProcess.Location = new Point(0x7d, 0xb5);
            this.m_txtProcess.MaxLength = 250;
            this.m_txtProcess.Multiline = true;
            this.m_txtProcess.Name = "m_txtProcess";
            this.m_txtProcess.Size = new Size(0x128, 0x2e);
            this.m_txtProcess.TabIndex = 3;
            this.m_txtSummary.Location = new Point(0x7d, 0xed);
            this.m_txtSummary.MaxLength = 250;
            this.m_txtSummary.Multiline = true;
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new Size(0x128, 0x2e);
            this.m_txtSummary.TabIndex = 4;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x51, 0xf7);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2a, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "备注:";
            this.panel1.Controls.Add(this.m_btnCancel);
            this.panel1.Controls.Add(this.m_btnSave);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1af, 0x40);
            this.panel1.TabIndex = 2;
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_btnCancel.DefaultScheme = true;
            this.m_btnCancel.DialogResult = 0;
            this.m_btnCancel.Hint = "";
            this.m_btnCancel.Location = new Point(0x13b, 12);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Scheme = 0;
            this.m_btnCancel.Size = new Size(0x67, 0x25);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "取消(ESC)";
            this.m_btnCancel.Click += new EventHandler(this.m_btnCancel_Click);
            this.m_btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnSave.BackColor = Color.FromArgb(0, 0xec, 0xe9, 0xd8);
            this.m_btnSave.DefaultScheme = true;
            this.m_btnSave.DialogResult = 0;
            this.m_btnSave.Hint = "";
            this.m_btnSave.Location = new Point(0xcf, 12);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Scheme = 0;
            this.m_btnSave.Size = new Size(0x67, 0x25);
            this.m_btnSave.TabIndex = 0;
            this.m_btnSave.Text = "确定";
            this.m_btnSave.Click += new EventHandler(this.m_btnSave_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x1af, 0x1b0);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new Font("宋体", 10.5f);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQCReport";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "质控报告";
            this.Load += new EventHandler(this.frmQCReport_Load);
            this.FormClosed += new FormClosedEventHandler(this.frmQCReport_FormClosed);
            this.KeyDown += new KeyEventHandler(this.frm_KeyDown);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            long num = 0L;
            if (this.m_blnNew)
            {
                if (this.m_blIsDate)
                {
                    clsLisQCReportVO QCReportVO = new clsLisQCReportVO();
                    this.m_mthBindControlValueToObj(QCReportVO);
                    QCReportVO.m_intSeq = -2147483648;
                    QCReportVO.m_intQCBatchSeq = this.m_intQCBatchSeq;
                    QCReportVO.m_enmStatus = enmQCStatus.Natrural;
                    QCReportVO.m_intReportStats = 0;
                    num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCReport(QCReportVO, out QCReportVO.m_intSeq);
                    if (num > 0L)
                    {
                        this.m_objReport = QCReportVO;
                        this.m_blnNew = false;
                    }
                }
                else
                {
                    clsLisQCReportVO QCReportVO = new clsLisQCReportVO();
                    this.m_mthBindControlValueToObj(QCReportVO);
                    QCReportVO.m_intSeq = -2147483648;
                    QCReportVO.m_intQCBatchSeq = this.m_intQCBatchSeq;
                    QCReportVO.m_enmStatus = enmQCStatus.Natrural;
                    QCReportVO.m_intReportStats = 1;
                    num = (new weCare.Proxy.ProxyLis02()).Service.m_lngInsertQCReport(QCReportVO, out QCReportVO.m_intSeq);
                    if (num > 0L)
                    {
                        this.m_objReport = QCReportVO;
                        this.m_blnNew = false;
                    }
                }
            }
            else
            {
                clsLisQCReportVO QCReportVO = new clsLisQCReportVO();
                this.m_objReport.m_mthCopyTo(QCReportVO);
                this.m_mthBindControlValueToObj(QCReportVO);
                QCReportVO.m_intReportStats = this.m_objReport.m_intReportStats;
                num = (new weCare.Proxy.ProxyLis02()).Service.m_lngUpdateQCReport(QCReportVO);
                if (num > 0L)
                {
                    QCReportVO.m_mthCopyTo(this.m_objReport);
                }
            }
            if (num <= 0L)
            {
                clsCommonDialog.m_mthShowDBError();
            }
            else
            {
                this.m_blnOK = true;
                this.Close();
            }
        }

        private void m_cmdAddBrokenRules_Click(object sender, EventArgs e)
        {
            if (this.m_strBrokenRules != null)
            {
                this.m_txtUnmatchedRule.Text = this.m_strBrokenRules;
            }
        }

        private void m_mthBindControlValueToObj(clsLisQCReportVO objReport)
        {
            try
            {
                objReport.m_enmQCControlStatus = !this.m_chkLost.Checked ? enmQCControlStatus.Control : enmQCControlStatus.UnControl;
            }
            catch { }
            objReport.m_strReportorId = m_txtAppDoct.m_StrEmployeeID;
            objReport.m_strUnmatchedRule = m_txtUnmatchedRule.Text;
            objReport.m_strReason = m_txtReason.Text;
            objReport.m_strProcess = m_txtProcess.Text;
            objReport.m_strSummary = m_txtSummary.Text;
            objReport.m_dtReport = DateTime.Parse(m_dtpReport.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            objReport.m_strReportorName = m_txtAppDoct.m_StrEmployeeName;
        }

        private void m_mthControlsDisplayVOValue(clsLisQCReportVO objReport)
        {
            if (objReport != null)
            {
                m_lblBatchNO.Text = DBAssist.ToString(objReport.m_intQCBatchSeq);
                this.m_chkLost.Checked = objReport.m_enmQCControlStatus == enmQCControlStatus.Control ? false : true;
                m_txtAppDoct.m_StrEmployeeID = objReport.m_strReportorId;
                m_txtUnmatchedRule.Text = objReport.m_strUnmatchedRule;
                m_txtReason.Text = objReport.m_strReason;
                m_txtProcess.Text = objReport.m_strProcess;
                m_txtSummary.Text = objReport.m_strSummary;
                m_dtpReport.Value = objReport.m_dtReport;
            }
        }

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            if (p_eumKeyCode == Keys.Escape)
            {
                if (this.m_btnCancel.Enabled && this.m_btnCancel.Visible)
                {
                    this.m_btnCancel_Click(this.m_btnCancel, null);
                }
            }
        }

        // Properties
        public string BrokenRules
        {
            set
            {
                this.m_strBrokenRules = value;
            }
        }

        public clsLisQCReportVO Report
        {
            get
            {
                return this.m_objReport;
            }
        }
    }
}
