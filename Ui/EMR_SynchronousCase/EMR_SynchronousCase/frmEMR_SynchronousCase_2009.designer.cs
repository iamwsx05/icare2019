namespace com.digitalwave.emr.EMR_SynchronousCase
{
    partial class frmEMR_SynchronousCase_2009
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMR_SynchronousCase_2009));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.m_tsSearchToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_lsvDept = new com.digitalwave.Controls.Domain.EmrControls.ctlTSDeptSelected();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbGetData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_lblPatientCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbSynchronouos = new System.Windows.Forms.ToolStripButton();
            this.m_dtpOutEnd = new System.Windows.Forms.DateTimePicker();
            this.m_dtpOutBegin = new System.Windows.Forms.DateTimePicker();
            this.m_chkSelectDept = new System.Windows.Forms.CheckBox();
            this.m_dgwPatient = new System.Windows.Forms.DataGridView();
            this.clmSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmPRN = new System.Windows.Forms.DataGridViewLinkColumn();
            this.clmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOutDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOutDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNativePlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNationnality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOccupation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_pnlProgress = new System.Windows.Forms.Panel();
            this.m_lblCase = new System.Windows.Forms.Label();
            this.m_pbPatient = new System.Windows.Forms.ProgressBar();
            this.m_tsSearchToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwPatient)).BeginInit();
            this.m_pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_tsSearchToolBar
            // 
            this.m_tsSearchToolBar.AutoSize = false;
            this.m_tsSearchToolBar.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_tsSearchToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.m_lsvDept,
            this.toolStripSeparator2,
            this.m_tsbGetData,
            this.toolStripSeparator3,
            this.m_lblPatientCount,
            this.toolStripSeparator4,
            this.m_tsbSynchronouos});
            this.m_tsSearchToolBar.Location = new System.Drawing.Point(0, 0);
            this.m_tsSearchToolBar.Name = "m_tsSearchToolBar";
            this.m_tsSearchToolBar.Size = new System.Drawing.Size(1003, 40);
            this.m_tsSearchToolBar.TabIndex = 0;
            this.m_tsSearchToolBar.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 37);
            this.toolStripLabel1.Text = "出院时间:";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(14, 37);
            this.toolStripLabel2.Text = "~";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // m_lsvDept
            // 
            this.m_lsvDept.Enabled = false;
            this.m_lsvDept.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_lsvDept.m_objTag = null;
            this.m_lsvDept.Name = "m_lsvDept";
            this.m_lsvDept.Size = new System.Drawing.Size(120, 40);
            this.m_lsvDept.显示类型 = com.digitalwave.Controls.Domain.EmrControls.enmDeptAttributeType.全部;
            this.m_lsvDept.ItemSelectedChanged += new com.digitalwave.Controls.ItemSelectedEventHandler(this.m_lsvDept_ItemSelectedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // m_tsbGetData
            // 
            this.m_tsbGetData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.m_tsbGetData.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbGetData.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbGetData.Image")));
            this.m_tsbGetData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbGetData.Name = "m_tsbGetData";
            this.m_tsbGetData.Size = new System.Drawing.Size(87, 37);
            this.m_tsbGetData.Text = "获取数据";
            this.m_tsbGetData.Click += new System.EventHandler(this.m_tsbGetData_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // m_lblPatientCount
            // 
            this.m_lblPatientCount.Image = ((System.Drawing.Image)(resources.GetObject("m_lblPatientCount.Image")));
            this.m_lblPatientCount.Name = "m_lblPatientCount";
            this.m_lblPatientCount.Size = new System.Drawing.Size(142, 37);
            this.m_lblPatientCount.Text = "共查询出病人?人次";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // m_tsbSynchronouos
            // 
            this.m_tsbSynchronouos.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.m_tsbSynchronouos.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbSynchronouos.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbSynchronouos.Image")));
            this.m_tsbSynchronouos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSynchronouos.Name = "m_tsbSynchronouos";
            this.m_tsbSynchronouos.Size = new System.Drawing.Size(73, 37);
            this.m_tsbSynchronouos.Text = "同  步";
            this.m_tsbSynchronouos.Click += new System.EventHandler(this.m_tsbSynchronouos_Click);
            // 
            // m_dtpOutEnd
            // 
            this.m_dtpOutEnd.CustomFormat = "yyyy年MM月dd日";
            this.m_dtpOutEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpOutEnd.Location = new System.Drawing.Point(674, 9);
            this.m_dtpOutEnd.Name = "m_dtpOutEnd";
            this.m_dtpOutEnd.Size = new System.Drawing.Size(121, 23);
            this.m_dtpOutEnd.TabIndex = 0;
            // 
            // m_dtpOutBegin
            // 
            this.m_dtpOutBegin.CustomFormat = "yyyy年MM月dd日";
            this.m_dtpOutBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpOutBegin.Location = new System.Drawing.Point(549, 9);
            this.m_dtpOutBegin.Name = "m_dtpOutBegin";
            this.m_dtpOutBegin.Size = new System.Drawing.Size(121, 23);
            this.m_dtpOutBegin.TabIndex = 0;
            // 
            // m_chkSelectDept
            // 
            this.m_chkSelectDept.BackColor = System.Drawing.Color.Transparent;
            this.m_chkSelectDept.Location = new System.Drawing.Point(799, 7);
            this.m_chkSelectDept.Name = "m_chkSelectDept";
            this.m_chkSelectDept.Size = new System.Drawing.Size(88, 27);
            this.m_chkSelectDept.TabIndex = 1;
            this.m_chkSelectDept.Text = "出院科室";
            this.m_chkSelectDept.UseVisualStyleBackColor = false;
            this.m_chkSelectDept.CheckedChanged += new System.EventHandler(this.m_chkSelectDept_CheckedChanged);
            // 
            // m_dgwPatient
            // 
            this.m_dgwPatient.AllowUserToAddRows = false;
            this.m_dgwPatient.AllowUserToDeleteRows = false;
            this.m_dgwPatient.AllowUserToResizeRows = false;
            //dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.m_dgwPatient.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.m_dgwPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgwPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSelect,
            this.clmPRN,
            this.clmName,
            this.clmTimes,
            this.clmSex,
            this.clmBirth,
            this.clmOutDate,
            this.clmOutDeptName,
            this.clmNativePlace,
            this.clmNationnality,
            this.clmRace,
            this.clmOccupation,
            this.clmInDate,
            this.clmInDeptName});
            this.m_dgwPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dgwPatient.Location = new System.Drawing.Point(0, 40);
            this.m_dgwPatient.Name = "m_dgwPatient";
            this.m_dgwPatient.RowHeadersVisible = false;
            this.m_dgwPatient.RowTemplate.Height = 23;
            this.m_dgwPatient.Size = new System.Drawing.Size(1003, 421);
            this.m_dgwPatient.TabIndex = 2;
            this.m_dgwPatient.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dgwPatient_CellContentClick);
            // 
            // clmSelect
            // 
            this.clmSelect.DataPropertyName = "isselect";
            this.clmSelect.HeaderText = "";
            this.clmSelect.Name = "clmSelect";
            this.clmSelect.ReadOnly = true;
            this.clmSelect.Width = 40;
            // 
            // clmPRN
            // 
            this.clmPRN.DataPropertyName = "fprn";
            this.clmPRN.HeaderText = "住院号";
            this.clmPRN.LinkColor = System.Drawing.Color.DodgerBlue;
            this.clmPRN.Name = "clmPRN";
            this.clmPRN.ReadOnly = true;
            this.clmPRN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmPRN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmPRN.TrackVisitedState = false;
            // 
            // clmName
            // 
            this.clmName.DataPropertyName = "fname";
            this.clmName.HeaderText = "姓名";
            this.clmName.Name = "clmName";
            this.clmName.ReadOnly = true;
            // 
            // clmTimes
            // 
            this.clmTimes.DataPropertyName = "ftimes";
            this.clmTimes.HeaderText = "次数";
            this.clmTimes.Name = "clmTimes";
            this.clmTimes.ReadOnly = true;
            this.clmTimes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmTimes.Width = 50;
            // 
            // clmSex
            // 
            this.clmSex.DataPropertyName = "fsex";
            this.clmSex.HeaderText = "性别";
            this.clmSex.Name = "clmSex";
            this.clmSex.ReadOnly = true;
            this.clmSex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmSex.Width = 50;
            // 
            // clmBirth
            // 
            this.clmBirth.DataPropertyName = "fbirthday";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.clmBirth.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmBirth.HeaderText = "生日";
            this.clmBirth.Name = "clmBirth";
            this.clmBirth.ReadOnly = true;
            this.clmBirth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmOutDate
            // 
            this.clmOutDate.DataPropertyName = "fcydate";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.clmOutDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmOutDate.HeaderText = "出院日期";
            this.clmOutDate.Name = "clmOutDate";
            this.clmOutDate.ReadOnly = true;
            // 
            // clmOutDeptName
            // 
            this.clmOutDeptName.DataPropertyName = "fcydept";
            this.clmOutDeptName.HeaderText = "出院科别";
            this.clmOutDeptName.Name = "clmOutDeptName";
            this.clmOutDeptName.ReadOnly = true;
            // 
            // clmNativePlace
            // 
            this.clmNativePlace.DataPropertyName = "fbirthplace";
            this.clmNativePlace.HeaderText = "出生地";
            this.clmNativePlace.Name = "clmNativePlace";
            this.clmNativePlace.ReadOnly = true;
            this.clmNativePlace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmNationnality
            // 
            this.clmNationnality.DataPropertyName = "fcountry";
            this.clmNationnality.HeaderText = "国籍";
            this.clmNationnality.Name = "clmNationnality";
            this.clmNationnality.ReadOnly = true;
            this.clmNationnality.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmRace
            // 
            this.clmRace.DataPropertyName = "fnationality";
            this.clmRace.HeaderText = "民族";
            this.clmRace.Name = "clmRace";
            this.clmRace.ReadOnly = true;
            this.clmRace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmOccupation
            // 
            this.clmOccupation.DataPropertyName = "fjob";
            this.clmOccupation.HeaderText = "职业";
            this.clmOccupation.Name = "clmOccupation";
            this.clmOccupation.ReadOnly = true;
            this.clmOccupation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmInDate
            // 
            this.clmInDate.DataPropertyName = "frydate";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.clmInDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmInDate.HeaderText = "入院日期";
            this.clmInDate.Name = "clmInDate";
            this.clmInDate.ReadOnly = true;
            this.clmInDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmInDeptName
            // 
            this.clmInDeptName.DataPropertyName = "frydept";
            this.clmInDeptName.HeaderText = "入院科别";
            this.clmInDeptName.Name = "clmInDeptName";
            this.clmInDeptName.ReadOnly = true;
            this.clmInDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // m_pnlProgress
            // 
            this.m_pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlProgress.Controls.Add(this.m_lblCase);
            this.m_pnlProgress.Controls.Add(this.m_pbPatient);
            this.m_pnlProgress.Location = new System.Drawing.Point(285, 201);
            this.m_pnlProgress.Name = "m_pnlProgress";
            this.m_pnlProgress.Size = new System.Drawing.Size(470, 74);
            this.m_pnlProgress.TabIndex = 3;
            this.m_pnlProgress.Visible = false;
            // 
            // m_lblCase
            // 
            this.m_lblCase.AutoSize = true;
            this.m_lblCase.Location = new System.Drawing.Point(7, 6);
            this.m_lblCase.Name = "m_lblCase";
            this.m_lblCase.Size = new System.Drawing.Size(98, 14);
            this.m_lblCase.TabIndex = 1;
            this.m_lblCase.Text = "当前同步病案:";
            // 
            // m_pbPatient
            // 
            this.m_pbPatient.Location = new System.Drawing.Point(6, 29);
            this.m_pbPatient.Name = "m_pbPatient";
            this.m_pbPatient.Size = new System.Drawing.Size(447, 23);
            this.m_pbPatient.TabIndex = 0;
            // 
            // frmEMR_SynchronousCase_2009
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 461);
            this.Controls.Add(this.m_pnlProgress);
            this.Controls.Add(this.m_dgwPatient);
            this.Controls.Add(this.m_chkSelectDept);
            this.Controls.Add(this.m_dtpOutEnd);
            this.Controls.Add(this.m_dtpOutBegin);
            this.Controls.Add(this.m_tsSearchToolBar);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEMR_SynchronousCase_2009";
            this.Text = "病案同步";
            this.Load += new System.EventHandler(this.frmEMR_SynchronousCase_2009_Load);
            this.m_tsSearchToolBar.ResumeLayout(false);
            this.m_tsSearchToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwPatient)).EndInit();
            this.m_pnlProgress.ResumeLayout(false);
            this.m_pnlProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip m_tsSearchToolBar;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DateTimePicker m_dtpOutBegin;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DateTimePicker m_dtpOutEnd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private com.digitalwave.Controls.Domain.EmrControls.ctlTSDeptSelected m_lsvDept;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox m_chkSelectDept;
        private System.Windows.Forms.ToolStripButton m_tsbGetData;
        private System.Windows.Forms.ToolStripLabel m_lblPatientCount;
        private System.Windows.Forms.DataGridView m_dgwPatient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton m_tsbSynchronouos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSelect;
        private System.Windows.Forms.DataGridViewLinkColumn clmPRN;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOutDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOutDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNativePlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNationnality;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRace;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOccupation;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInDeptName;
        private System.Windows.Forms.Panel m_pnlProgress;
        private System.Windows.Forms.ProgressBar m_pbPatient;
        private System.Windows.Forms.Label m_lblCase;
    }
}