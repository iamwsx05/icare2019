﻿namespace HISReportMZ.Viewer
{
    partial class frmWorkLoadStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkLoadStatistics));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtm_end = new System.Windows.Forms.DateTimePicker();
            this.dtm_start = new System.Windows.Forms.DateTimePicker();
            this.btnExcel = new PinkieControls.ButtonXP();
            this.btnPreview = new PinkieControls.ButtonXP();
            this.btnClose = new PinkieControls.ButtonXP();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnFind = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.dwRep = new Sybase.DataWindow.DataWindowControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtm_end);
            this.panel1.Controls.Add(this.dtm_start);
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnPreview);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 48);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 65;
            this.label1.Text = "～";
            // 
            // dtm_end
            // 
            this.dtm_end.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtm_end.CustomFormat = "yyyy-MM-dd";
            this.dtm_end.Font = new System.Drawing.Font("宋体", 10F);
            this.dtm_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtm_end.Location = new System.Drawing.Point(243, 13);
            this.dtm_end.Name = "dtm_end";
            this.dtm_end.Size = new System.Drawing.Size(128, 23);
            this.dtm_end.TabIndex = 64;
            // 
            // dtm_start
            // 
            this.dtm_start.CalendarFont = new System.Drawing.Font("宋体", 10F);
            this.dtm_start.CustomFormat = "yyyy-MM-dd";
            this.dtm_start.Font = new System.Drawing.Font("宋体", 10F);
            this.dtm_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtm_start.Location = new System.Drawing.Point(86, 13);
            this.dtm_start.Name = "dtm_start";
            this.dtm_start.Size = new System.Drawing.Size(126, 23);
            this.dtm_start.TabIndex = 63;
            this.dtm_start.Value = new System.DateTime(2019, 11, 4, 0, 0, 0, 0);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnExcel.DefaultScheme = true;
            this.btnExcel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnExcel.Font = new System.Drawing.Font("宋体", 10F);
            this.btnExcel.Hint = "";
            this.btnExcel.Location = new System.Drawing.Point(769, 8);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnExcel.Size = new System.Drawing.Size(103, 32);
            this.btnExcel.TabIndex = 62;
            this.btnExcel.Text = "导出(&D)";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPreview.DefaultScheme = true;
            this.btnPreview.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPreview.Font = new System.Drawing.Font("宋体", 10F);
            this.btnPreview.Hint = "";
            this.btnPreview.Location = new System.Drawing.Point(551, 8);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPreview.Size = new System.Drawing.Size(103, 32);
            this.btnPreview.TabIndex = 61;
            this.btnPreview.Text = "预览(&R)";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Font = new System.Drawing.Font("宋体", 10F);
            this.btnClose.Hint = "";
            this.btnClose.Location = new System.Drawing.Point(878, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(103, 32);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "退出(&E)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Font = new System.Drawing.Font("宋体", 10F);
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(660, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(103, 32);
            this.btnPrint.TabIndex = 59;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnFind.DefaultScheme = true;
            this.btnFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnFind.Font = new System.Drawing.Font("宋体", 10F);
            this.btnFind.Hint = "";
            this.btnFind.Location = new System.Drawing.Point(444, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnFind.Size = new System.Drawing.Size(101, 32);
            this.btnFind.TabIndex = 58;
            this.btnFind.Text = "查询(&F)";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(28, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 66;
            this.label2.Text = "时间段：";
            // 
            // dwRep
            // 
            this.dwRep.DataWindowObject = "";
            this.dwRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRep.LibraryList = "";
            this.dwRep.Location = new System.Drawing.Point(0, 48);
            this.dwRep.Name = "dwRep";
            this.dwRep.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
            this.dwRep.Size = new System.Drawing.Size(1057, 481);
            this.dwRep.TabIndex = 8;
            this.dwRep.Text = "dataWindowControl1";
            // 
            // frmWorkLoadStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 529);
            this.Controls.Add(this.dwRep);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWorkLoadStatistics";
            this.Text = "药房配药工作量统计";
            this.Load += new System.EventHandler(this.frmWorkLoadStatistics_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dtm_end;
        public System.Windows.Forms.DateTimePicker dtm_start;
        public PinkieControls.ButtonXP btnExcel;
        public PinkieControls.ButtonXP btnPreview;
        public PinkieControls.ButtonXP btnClose;
        public PinkieControls.ButtonXP btnPrint;
        public PinkieControls.ButtonXP btnFind;
        private System.Windows.Forms.Label label2;
        public Sybase.DataWindow.DataWindowControl dwRep;
    }
}