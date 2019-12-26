namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmStatistics
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_dwStat = new Sybase.DataWindow.DataWindowControl();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.m_txtAppDoct = new com.digitalwave.Utility.ctlEmpTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_cboScheme = new com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox(this.components);
            this.m_cboArea = new com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox();
            this.btnExport = new PinkieControls.ButtonXP();
            this.btnPreview = new PinkieControls.ButtonXP();
            this.btnClose = new PinkieControls.ButtonXP();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnStat = new PinkieControls.ButtonXP();
            this.m_dtStat = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_dwStat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 502);
            this.panel1.TabIndex = 2;
            // 
            // m_dwStat
            // 
            this.m_dwStat.BorderStyle = Sybase.DataWindow.DataWindowBorderStyle.None;
            this.m_dwStat.DataWindowObject = "d_mfz_stat";
            this.m_dwStat.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_dwStat.LibraryList = "pb_new.pbl";
            this.m_dwStat.Location = new System.Drawing.Point(4, 0);
            this.m_dwStat.Name = "m_dwStat";
            this.m_dwStat.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.m_dwStat.Size = new System.Drawing.Size(965, 498);
            this.m_dwStat.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.White;
            this.panelBottom.Controls.Add(this.panel1);
            this.panelBottom.Controls.Add(this.panelTop);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(0, 0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(973, 542);
            this.panelBottom.TabIndex = 1;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Control;
            this.panelTop.Controls.Add(this.m_txtAppDoct);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.m_cboScheme);
            this.panelTop.Controls.Add(this.m_cboArea);
            this.panelTop.Controls.Add(this.btnExport);
            this.panelTop.Controls.Add(this.btnPreview);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.btnPrint);
            this.panelTop.Controls.Add(this.btnStat);
            this.panelTop.Controls.Add(this.m_dtStat);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(973, 40);
            this.panelTop.TabIndex = 1;
            // 
            // m_txtAppDoct
            // 
            this.m_txtAppDoct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            //this.m_txtAppDoct.EnableAutoValidation = true;
            //this.m_txtAppDoct.EnableEnterKeyValidate = true;
            //this.m_txtAppDoct.EnableEscapeKeyUndo = true;
            //this.m_txtAppDoct.EnableLastValidValue = true;
            //this.m_txtAppDoct.ErrorProvider = null;
            //this.m_txtAppDoct.ErrorProviderMessage = "Invalid value";
            this.m_txtAppDoct.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtAppDoct.ForceFormatText = true;
            this.m_txtAppDoct.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_txtAppDoct.Location = new System.Drawing.Point(52, 9);
            this.m_txtAppDoct.m_intShowOtherEmp = 0;
            this.m_txtAppDoct.m_StrDeptID = "*";
            this.m_txtAppDoct.m_StrEmployeeID = null;
            this.m_txtAppDoct.m_StrEmployeeName = null;
            this.m_txtAppDoct.MaxLength = 20;
            this.m_txtAppDoct.Name = "m_txtAppDoct";
            this.m_txtAppDoct.Size = new System.Drawing.Size(79, 23);
            this.m_txtAppDoct.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 15;
            this.label4.Text = "医生：";
            // 
            // m_cboScheme
            // 
            this.m_cboScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboScheme.FormattingEnabled = true;
            this.m_cboScheme.Location = new System.Drawing.Point(320, 12);
            this.m_cboScheme.Name = "m_cboScheme";
            this.m_cboScheme.SchemeID = -2147483648;
            this.m_cboScheme.Size = new System.Drawing.Size(83, 20);
            this.m_cboScheme.TabIndex = 12;
            // 
            // m_cboArea
            // 
            this.m_cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboArea.FormattingEnabled = true;
            this.m_cboArea.Location = new System.Drawing.Point(178, 12);
            this.m_cboArea.m_intAreaID = -2147483648;
            this.m_cboArea.Name = "m_cboArea";
            this.m_cboArea.Size = new System.Drawing.Size(97, 20);
            this.m_cboArea.TabIndex = 11;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.DefaultScheme = true;
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Hint = "";
            this.btnExport.Location = new System.Drawing.Point(820, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnExport.Size = new System.Drawing.Size(75, 32);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "导出(&E)";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.DefaultScheme = true;
            this.btnPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPreview.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreview.Hint = "";
            this.btnPreview.Location = new System.Drawing.Point(745, 4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPreview.Size = new System.Drawing.Size(75, 32);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.Text = "预览(&R)";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(895, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(75, 32);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关闭(&C)";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(670, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "打印(&P)";
            // 
            // btnStat
            // 
            this.btnStat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnStat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStat.DefaultScheme = true;
            this.btnStat.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnStat.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStat.Hint = "";
            this.btnStat.Location = new System.Drawing.Point(595, 4);
            this.btnStat.Name = "btnStat";
            this.btnStat.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnStat.Size = new System.Drawing.Size(75, 32);
            this.btnStat.TabIndex = 6;
            this.btnStat.Text = "统计(&S)";
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // m_dtStat
            // 
            this.m_dtStat.CustomFormat = "yyyy年MM月dd日";
            this.m_dtStat.Font = new System.Drawing.Font("宋体", 11F);
            this.m_dtStat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtStat.Location = new System.Drawing.Point(453, 10);
            this.m_dtStat.Name = "m_dtStat";
            this.m_dtStat.Size = new System.Drawing.Size(134, 24);
            this.m_dtStat.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(414, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "日期：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(281, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "班次：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(137, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "诊区：";
            // 
            // frmStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 542);
            this.Controls.Add(this.panelBottom);
            this.Name = "frmStatistics";
            this.Text = "门诊医生叫号统计查询";
            this.panel1.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlSchemeCombox m_cboScheme;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox m_cboArea;
        internal PinkieControls.ButtonXP btnExport;
        internal PinkieControls.ButtonXP btnPreview;
        internal PinkieControls.ButtonXP btnClose;
        internal PinkieControls.ButtonXP btnPrint;
        internal PinkieControls.ButtonXP btnStat;
        internal System.Windows.Forms.DateTimePicker m_dtStat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Sybase.DataWindow.DataWindowControl m_dwStat;
        internal com.digitalwave.Utility.ctlEmpTextBox m_txtAppDoct;
    }
}