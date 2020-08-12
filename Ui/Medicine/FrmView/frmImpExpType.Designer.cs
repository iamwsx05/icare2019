namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmImpExpType
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdDelete = new PinkieControls.ButtonXP();
            this.cmdExit = new PinkieControls.ButtonXP();
            this.cmdCancel = new PinkieControls.ButtonXP();
            this.cmdSave = new PinkieControls.ButtonXP();
            this.radShowDel = new System.Windows.Forms.RadioButton();
            this.radShowAll = new System.Windows.Forms.RadioButton();
            this.cobStorgeflag = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cobFlag = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblcode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmdNew = new PinkieControls.ButtonXP();
            this.lsvTypes = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmdDelete);
            this.panel1.Controls.Add(this.cmdExit);
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.radShowDel);
            this.panel1.Controls.Add(this.radShowAll);
            this.panel1.Controls.Add(this.cobStorgeflag);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cobFlag);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblcode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 455);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 118);
            this.panel1.TabIndex = 0;
            // 
            // cmdDelete
            // 
            this.cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdDelete.DefaultScheme = true;
            this.cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdDelete.Hint = "";
            this.cmdDelete.Location = new System.Drawing.Point(339, 77);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdDelete.Size = new System.Drawing.Size(81, 34);
            this.cmdDelete.TabIndex = 120;
            this.cmdDelete.Text = "删除(&D)";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdExit.DefaultScheme = true;
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Hint = "";
            this.cmdExit.Location = new System.Drawing.Point(441, 77);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdExit.Size = new System.Drawing.Size(81, 34);
            this.cmdExit.TabIndex = 119;
            this.cmdExit.Text = "退出(&E)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdCancel.DefaultScheme = true;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Hint = "";
            this.cmdCancel.Location = new System.Drawing.Point(237, 77);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdCancel.Size = new System.Drawing.Size(81, 34);
            this.cmdCancel.TabIndex = 118;
            this.cmdCancel.Text = "取消(&C)";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdSave.DefaultScheme = true;
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSave.Hint = "";
            this.cmdSave.Location = new System.Drawing.Point(135, 77);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdSave.Size = new System.Drawing.Size(81, 34);
            this.cmdSave.TabIndex = 117;
            this.cmdSave.Text = "保存(&S)";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // radShowDel
            // 
            this.radShowDel.AutoSize = true;
            this.radShowDel.Location = new System.Drawing.Point(412, 48);
            this.radShowDel.Name = "radShowDel";
            this.radShowDel.Size = new System.Drawing.Size(109, 18);
            this.radShowDel.TabIndex = 116;
            this.radShowDel.Text = "显示无效类型";
            this.radShowDel.UseVisualStyleBackColor = true;
            this.radShowDel.CheckedChanged += new System.EventHandler(this.radShowDel_CheckedChanged);
            // 
            // radShowAll
            // 
            this.radShowAll.AutoSize = true;
            this.radShowAll.Checked = true;
            this.radShowAll.Location = new System.Drawing.Point(264, 48);
            this.radShowAll.Name = "radShowAll";
            this.radShowAll.Size = new System.Drawing.Size(109, 18);
            this.radShowAll.TabIndex = 115;
            this.radShowAll.TabStop = true;
            this.radShowAll.Text = "显示有效类型";
            this.radShowAll.UseVisualStyleBackColor = true;
            this.radShowAll.CheckedChanged += new System.EventHandler(this.radShowAll_CheckedChanged);
            // 
            // cobStorgeflag
            // 
            this.cobStorgeflag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cobStorgeflag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobStorgeflag.FormattingEnabled = true;
            this.cobStorgeflag.Items.AddRange(new object[] {
            "0_药库",
            "1_药房",
            "2_药库房共用"});
            this.cobStorgeflag.Location = new System.Drawing.Point(86, 46);
            this.cobStorgeflag.Name = "cobStorgeflag";
            this.cobStorgeflag.Size = new System.Drawing.Size(89, 22);
            this.cobStorgeflag.TabIndex = 114;
            this.cobStorgeflag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobStorgeflag_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 113;
            this.label5.Text = "库房标志：";
            // 
            // cobFlag
            // 
            this.cobFlag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cobFlag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cobFlag.FormattingEnabled = true;
            this.cobFlag.Items.AddRange(new object[] {
            "1_入库",
            "2_出库"});
            this.cobFlag.Location = new System.Drawing.Point(465, 12);
            this.cobFlag.Name = "cobFlag";
            this.cobFlag.Size = new System.Drawing.Size(87, 22);
            this.cobFlag.TabIndex = 112;
            this.cobFlag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cobFlag_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 111;
            this.label4.Text = "类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 110;
            this.label3.Text = "类型名称：";
            // 
            // lblcode
            // 
            this.lblcode.BackColor = System.Drawing.Color.PapayaWhip;
            this.lblcode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblcode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblcode.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblcode.Location = new System.Drawing.Point(86, 14);
            this.lblcode.Name = "lblcode";
            this.lblcode.Size = new System.Drawing.Size(89, 18);
            this.lblcode.TabIndex = 109;
            this.lblcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 108;
            this.label1.Text = "类型编号：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(264, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(124, 23);
            this.txtName.TabIndex = 107;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // cmdNew
            // 
            this.cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.cmdNew.DefaultScheme = true;
            this.cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdNew.Hint = "";
            this.cmdNew.Location = new System.Drawing.Point(33, 77);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.cmdNew.Size = new System.Drawing.Size(81, 34);
            this.cmdNew.TabIndex = 106;
            this.cmdNew.Text = "新建(&N)";
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // lsvTypes
            // 
            this.lsvTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvTypes.FullRowSelect = true;
            this.lsvTypes.GridLines = true;
            this.lsvTypes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvTypes.HideSelection = false;
            this.lsvTypes.Location = new System.Drawing.Point(1, 0);
            this.lsvTypes.MultiSelect = false;
            this.lsvTypes.Name = "lsvTypes";
            this.lsvTypes.Size = new System.Drawing.Size(557, 451);
            this.lsvTypes.TabIndex = 1;
            this.lsvTypes.UseCompatibleStateImageBehavior = false;
            this.lsvTypes.View = System.Windows.Forms.View.Details;
            this.lsvTypes.SelectedIndexChanged += new System.EventHandler(this.lsvTypes_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "类型编号";
            this.columnHeader1.Width = 88;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类型名称";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "库房标志";
            this.columnHeader4.Width = 150;
            // 
            // frmImpExpType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 573);
            this.Controls.Add(this.lsvTypes);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmImpExpType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "药库入库类型";
            this.Load += new System.EventHandler(this.frmImpExpType_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ListView lsvTypes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        internal PinkieControls.ButtonXP cmdNew;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label lblcode;
        private System.Windows.Forms.Label label5;
        internal PinkieControls.ButtonXP cmdExit;
        internal PinkieControls.ButtonXP cmdCancel;
        internal PinkieControls.ButtonXP cmdSave;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.RadioButton radShowDel;
        internal System.Windows.Forms.RadioButton radShowAll;
        internal System.Windows.Forms.ComboBox cobStorgeflag;
        internal System.Windows.Forms.ComboBox cobFlag;
        internal PinkieControls.ButtonXP cmdDelete;
    }
}