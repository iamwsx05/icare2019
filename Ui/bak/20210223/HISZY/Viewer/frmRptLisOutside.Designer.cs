﻿namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmRptLisOutside
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnSelect = new PinkieControls.ButtonXP();
            this.btnExport = new PinkieControls.ButtonXP();
            this.dteRq2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new PinkieControls.ButtonXP();
            this.dteRq1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dwRep = new Sybase.DataWindow.DataWindowControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboType);
            this.panel1.Controls.Add(this.label3);
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
            this.panel1.Size = new System.Drawing.Size(834, 40);
            this.panel1.TabIndex = 7;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "汇总",
            "明细"});
            this.cboType.Location = new System.Drawing.Point(52, 9);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(74, 23);
            this.cboType.TabIndex = 16;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "分类：";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(684, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(70, 32);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelect.DefaultScheme = true;
            this.btnSelect.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelect.Hint = "";
            this.btnSelect.Location = new System.Drawing.Point(499, 5);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSelect.Size = new System.Drawing.Size(70, 32);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "检索(&S)";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.DefaultScheme = true;
            this.btnExport.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExport.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Hint = "";
            this.btnExport.Location = new System.Drawing.Point(610, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnExport.Size = new System.Drawing.Size(70, 32);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dteRq2
            // 
            this.dteRq2.CustomFormat = "yyyy-MM-dd";
            this.dteRq2.Font = new System.Drawing.Font("宋体", 11F);
            this.dteRq2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteRq2.Location = new System.Drawing.Point(373, 9);
            this.dteRq2.Name = "dteRq2";
            this.dteRq2.Size = new System.Drawing.Size(120, 24);
            this.dteRq2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(352, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "到";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(758, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(70, 32);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dteRq1
            // 
            this.dteRq1.CustomFormat = "yyyy-MM-dd";
            this.dteRq1.Font = new System.Drawing.Font("宋体", 11F);
            this.dteRq1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dteRq1.Location = new System.Drawing.Point(228, 9);
            this.dteRq1.Name = "dteRq1";
            this.dteRq1.Size = new System.Drawing.Size(120, 24);
            this.dteRq1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(136, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "检验日期：从";
            // 
            // dwRep
            // 
            this.dwRep.DataWindowObject = "";
            this.dwRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRep.LibraryList = "";
            this.dwRep.Location = new System.Drawing.Point(0, 40);
            this.dwRep.Name = "dwRep";
            this.dwRep.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwRep.Size = new System.Drawing.Size(834, 544);
            this.dwRep.TabIndex = 8;
            this.dwRep.Text = "dataWindowControl1";
            // 
            // frmRptLisOutside
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 584);
            this.Controls.Add(this.dwRep);
            this.Controls.Add(this.panel1);
            this.Name = "frmRptLisOutside";
            this.Text = "检验科外送项目统计";
            this.Load += new System.EventHandler(this.frmRptLisOutside_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label3;
        internal PinkieControls.ButtonXP btnPrint;
        internal PinkieControls.ButtonXP btnSelect;
        internal PinkieControls.ButtonXP btnExport;
        internal System.Windows.Forms.DateTimePicker dteRq2;
        private System.Windows.Forms.Label label2;
        internal PinkieControls.ButtonXP btnClose;
        internal System.Windows.Forms.DateTimePicker dteRq1;
        private System.Windows.Forms.Label label1;
        internal Sybase.DataWindow.DataWindowControl dwRep;
    }
}