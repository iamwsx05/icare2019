﻿namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmRptDoctorPerformance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRptDoctorPerformance));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDoct = new System.Windows.Forms.Button();
            this.cboDoct = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPreview = new PinkieControls.ButtonXP();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnSelect = new PinkieControls.ButtonXP();
            this.btnExport = new PinkieControls.ButtonXP();
            this.dteRq2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new PinkieControls.ButtonXP();
            this.dteRq1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dwRep = new Sybase.DataWindow.DataWindowControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnDoct);
            this.panel1.Controls.Add(this.cboDoct);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnPreview);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.dteRq2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.dteRq1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 40);
            this.panel1.TabIndex = 0;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("宋体", 11.5F);
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "实收",
            "业务"});
            this.cboType.Location = new System.Drawing.Point(566, 10);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(70, 23);
            this.cboType.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(526, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 17;
            this.label4.Text = "类型：";
            // 
            // btnDoct
            // 
            this.btnDoct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDoct.Location = new System.Drawing.Point(501, 9);
            this.btnDoct.Name = "btnDoct";
            this.btnDoct.Size = new System.Drawing.Size(20, 23);
            this.btnDoct.TabIndex = 16;
            this.btnDoct.Text = "…";
            this.btnDoct.UseVisualStyleBackColor = true;
            this.btnDoct.Click += new System.EventHandler(this.btnDoct_Click);
            // 
            // cboDoct
            // 
            this.cboDoct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoct.Font = new System.Drawing.Font("宋体", 11.5F);
            this.cboDoct.FormattingEnabled = true;
            this.cboDoct.Items.AddRange(new object[] {
            "全部",
            "选择》"});
            this.cboDoct.Location = new System.Drawing.Point(431, 9);
            this.cboDoct.Name = "cboDoct";
            this.cboDoct.Size = new System.Drawing.Size(70, 23);
            this.cboDoct.TabIndex = 15;
            this.cboDoct.SelectedIndexChanged += new System.EventHandler(this.cboDoct_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(390, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "医生：";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.DefaultScheme = true;
            this.btnPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPreview.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreview.Hint = "";
            this.btnPreview.Location = new System.Drawing.Point(716, 5);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPreview.Size = new System.Drawing.Size(63, 32);
            this.btnPreview.TabIndex = 13;
            this.btnPreview.Text = "预览(&V)";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(856, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(63, 32);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.DefaultScheme = true;
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Hint = "";
            this.btnSelect.Location = new System.Drawing.Point(646, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSelect.Size = new System.Drawing.Size(63, 32);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "检索(&S)";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.DefaultScheme = true;
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Hint = "";
            this.btnExport.Location = new System.Drawing.Point(786, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnExport.Size = new System.Drawing.Size(63, 32);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dteRq2
            // 
            this.dteRq2.CustomFormat = "yyyy年MM月dd日";
            this.dteRq2.Font = new System.Drawing.Font("宋体", 11F);
            this.dteRq2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteRq2.Location = new System.Drawing.Point(253, 9);
            this.dteRq2.Name = "dteRq2";
            this.dteRq2.Size = new System.Drawing.Size(133, 24);
            this.dteRq2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(232, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "到";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(926, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(63, 32);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dteRq1
            // 
            this.dteRq1.CustomFormat = "yyyy年MM月dd日";
            this.dteRq1.Font = new System.Drawing.Font("宋体", 11F);
            this.dteRq1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteRq1.Location = new System.Drawing.Point(96, 9);
            this.dteRq1.Name = "dteRq1";
            this.dteRq1.Size = new System.Drawing.Size(133, 24);
            this.dteRq1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "统计日期：从";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dwRep);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 571);
            this.panel2.TabIndex = 3;
            // 
            // dwRep
            // 
            this.dwRep.BorderStyle = Sybase.DataWindow.DataWindowBorderStyle.None;
            this.dwRep.DataWindowObject = "";
            this.dwRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRep.LibraryList = "";
            this.dwRep.Location = new System.Drawing.Point(0, 0);
            this.dwRep.Name = "dwRep";
            this.dwRep.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwRep.Size = new System.Drawing.Size(988, 567);
            this.dwRep.TabIndex = 0;
            this.dwRep.Text = "dataWindowControl1";
            // 
            // frmRptDoctorPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmRptDoctorPerformance";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "住院医生绩效统计报表";
            this.Load += new System.EventHandler(this.frmRptDoctorPerformance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal PinkieControls.ButtonXP btnClose;
        internal System.Windows.Forms.DateTimePicker dteRq1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.DateTimePicker dteRq2;
        internal PinkieControls.ButtonXP btnPrint;
        internal PinkieControls.ButtonXP btnSelect;
        internal PinkieControls.ButtonXP btnExport;
        internal PinkieControls.ButtonXP btnPreview;
        private System.Windows.Forms.Panel panel2;
        internal Sybase.DataWindow.DataWindowControl dwRep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDoct;
        private System.Windows.Forms.Button btnDoct;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label4;
    }
}