namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmRoomStation
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_lsvRooms = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtSummary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtDeptName = new com.digitalwave.Utility.ctlDeptTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtRoomName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ctlCboDiagnoseArea = new com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_lsvRooms);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 124);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 293);
            this.panel2.TabIndex = 12;
            // 
            // m_lsvRooms
            // 
            this.m_lsvRooms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.m_lsvRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvRooms.FullRowSelect = true;
            this.m_lsvRooms.GridLines = true;
            this.m_lsvRooms.HideSelection = false;
            this.m_lsvRooms.Location = new System.Drawing.Point(0, 0);
            this.m_lsvRooms.MultiSelect = false;
            this.m_lsvRooms.Name = "m_lsvRooms";
            this.m_lsvRooms.Size = new System.Drawing.Size(826, 293);
            this.m_lsvRooms.TabIndex = 0;
            this.m_lsvRooms.UseCompatibleStateImageBehavior = false;
            this.m_lsvRooms.View = System.Windows.Forms.View.Details;
            this.m_lsvRooms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_lsvRooms_MouseDoubleClick);
            this.m_lsvRooms.SelectedIndexChanged += new System.EventHandler(this.m_lsvRoom_Click);
            this.m_lsvRooms.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.m_lsvRooms_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "诊区名称";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "科室名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 189;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "诊室名称";
            this.columnHeader3.Width = 234;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_cmdDelete);
            this.panel1.Controls.Add(this.m_cmdSave);
            this.panel1.Controls.Add(this.m_cmdNew);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 82);
            this.panel1.TabIndex = 1;
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(695, 22);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(100, 38);
            this.m_cmdDelete.TabIndex = 2;
            this.m_cmdDelete.Text = "删除(F5)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(589, 22);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(100, 38);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保存(F4)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(474, 22);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(100, 38);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增(F3)";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtSummary);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_txtDeptName);
            this.groupBox2.Controls.Add(this.ctlCboDiagnoseArea);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.m_txtRoomName);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(826, 124);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // m_txtSummary
            // 
            this.m_txtSummary.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtSummary.Location = new System.Drawing.Point(102, 93);
            this.m_txtSummary.MaxLength = 25;
            this.m_txtSummary.Name = "m_txtSummary";
            this.m_txtSummary.Size = new System.Drawing.Size(663, 23);
            this.m_txtSummary.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "备  注";
            // 
            // m_txtDeptName
            // 
            //this.m_txtDeptName.EnableAutoValidation = true;
            //this.m_txtDeptName.EnableEnterKeyValidate = true;
            //this.m_txtDeptName.EnableEscapeKeyUndo = true;
            //this.m_txtDeptName.EnableLastValidValue = true;
            //this.m_txtDeptName.ErrorProvider = null;
            //this.m_txtDeptName.ErrorProviderMessage = "Invalid value";
            this.m_txtDeptName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //this.m_txtDeptName.ForceFormatText = true;
            this.m_txtDeptName.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.m_txtDeptName.Location = new System.Drawing.Point(103, 68);
            this.m_txtDeptName.m_StrDeptID = null;
            this.m_txtDeptName.m_StrDeptName = null;
            this.m_txtDeptName.MaxLength = 50;
            this.m_txtDeptName.Name = "m_txtDeptName";
            this.m_txtDeptName.SetDepartment = com.digitalwave.Utility.ctlDeptTextBox.eDeptArea.All;
            this.m_txtDeptName.Size = new System.Drawing.Size(663, 23);
            this.m_txtDeptName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "默认科室";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "诊区名称";
            // 
            // m_txtRoomName
            // 
            this.m_txtRoomName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.m_txtRoomName.Location = new System.Drawing.Point(103, 20);
            this.m_txtRoomName.MaxLength = 25;
            this.m_txtRoomName.Name = "m_txtRoomName";
            this.m_txtRoomName.Size = new System.Drawing.Size(663, 23);
            this.m_txtRoomName.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 0;
            this.label9.Text = "诊室名称";
            // 
            // ctlCboDiagnoseArea
            // 
            this.ctlCboDiagnoseArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCboDiagnoseArea.FormattingEnabled = true;
            this.ctlCboDiagnoseArea.Location = new System.Drawing.Point(103, 44);
            this.ctlCboDiagnoseArea.m_intAreaID = -2147483648;
            this.ctlCboDiagnoseArea.Name = "ctlCboDiagnoseArea";
            this.ctlCboDiagnoseArea.Size = new System.Drawing.Size(663, 22);
            this.ctlCboDiagnoseArea.TabIndex = 1;
            this.ctlCboDiagnoseArea.SelectedIndexChanged += new System.EventHandler(this.ctlCboDiagnoseArea_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "备 注";
            this.columnHeader4.Width = 247;
            // 
            // frmRoomStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 499);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmRoomStation";
            this.Text = "诊室及工作站维护";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRoomStation_KeyDown);
            this.Load += new System.EventHandler(this.frmRoomStation_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSave;
        private PinkieControls.ButtonXP m_cmdNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox m_txtRoomName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView m_lsvRooms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox ctlCboDiagnoseArea;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private com.digitalwave.Utility.ctlDeptTextBox m_txtDeptName;
        private System.Windows.Forms.TextBox m_txtSummary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}