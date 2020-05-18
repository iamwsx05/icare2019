namespace com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl
{
    partial class ctlOperationAndInfant
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlOperationAndInfant));
            this.m_dgwOperation = new System.Windows.Forms.DataGridView();
            this.clmOpDate = new System.Windows.Forms.DataGridViewTextBoxColumn();//com.digitalwave.controls.MedicineStoreControls.DataGridViewCalendarColumn();
            this.clmOpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOpLevel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm1stAssistant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clm2ndAssistant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCutLevel = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColOpzheqi = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmAnaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAnaDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmOpCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.m_dgwInfant = new System.Windows.Forms.DataGridView();
            this.clmNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSex = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmLaborResult = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmInfantWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmInfantResult = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmInfantBreath = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.clmRescueTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRescueSuccTimes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.m_cmdDeleteOP = new System.Windows.Forms.Button();
            this.m_cmdAddOP = new System.Windows.Forms.Button();
            this.m_cmdDeleteInfant = new System.Windows.Forms.Button();
            this.m_cmdAddInfant = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwInfant)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dgwOperation
            // 
            this.m_dgwOperation.AllowUserToAddRows = false;
            this.m_dgwOperation.AllowUserToDeleteRows = false;
            this.m_dgwOperation.AllowUserToResizeRows = false;
            this.m_dgwOperation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgwOperation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmOpDate,
            this.clmOpName,
            this.ColOpLevel,
            this.clmOperator,
            this.clm1stAssistant,
            this.clm2ndAssistant,
            this.clmCutLevel,
            this.ColOpzheqi,
            this.clmAnaName,
            this.clmAnaDoctor,
            this.clmOpCode});
            this.m_dgwOperation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.m_dgwOperation.Location = new System.Drawing.Point(4, 31);
            this.m_dgwOperation.Name = "m_dgwOperation";
            this.m_dgwOperation.RowHeadersVisible = false;
            this.m_dgwOperation.RowHeadersWidth = 20;
            this.m_dgwOperation.RowTemplate.Height = 23;
            this.m_dgwOperation.Size = new System.Drawing.Size(843, 230);
            this.m_dgwOperation.TabIndex = 0;
            this.m_dgwOperation.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.m_dgwOperation_EditingControlShowing);
            // 
            // clmOpDate
            // 
            this.clmOpDate.DataPropertyName = "opdate";
            dataGridViewCellStyle1.Format = "D";
            this.clmOpDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmOpDate.HeaderText = "手术及操作日期";
            this.clmOpDate.Name = "clmOpDate";
            this.clmOpDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmOpDate.Width = 120;
            // 
            // clmOpName
            // 
            this.clmOpName.DataPropertyName = "opname";
            this.clmOpName.HeaderText = "手术及操作名称";
            this.clmOpName.Name = "clmOpName";
            this.clmOpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmOpName.Width = 120;
            // 
            // ColOpLevel
            // 
            this.ColOpLevel.DataPropertyName = "operationlevel";
            this.ColOpLevel.HeaderText = "手术等级";
            this.ColOpLevel.Name = "ColOpLevel";
            this.ColOpLevel.Width = 80;
            // 
            // clmOperator
            // 
            this.clmOperator.DataPropertyName = "opdoctor";
            this.clmOperator.HeaderText = "术者";
            this.clmOperator.Name = "clmOperator";
            this.clmOperator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmOperator.Width = 80;
            // 
            // clm1stAssistant
            // 
            this.clm1stAssistant.DataPropertyName = "firstassist";
            this.clm1stAssistant.HeaderText = "Ⅰ助";
            this.clm1stAssistant.Name = "clm1stAssistant";
            this.clm1stAssistant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clm1stAssistant.Width = 80;
            // 
            // clm2ndAssistant
            // 
            this.clm2ndAssistant.DataPropertyName = "secondassist";
            this.clm2ndAssistant.HeaderText = "Ⅱ助";
            this.clm2ndAssistant.Name = "clm2ndAssistant";
            this.clm2ndAssistant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clm2ndAssistant.Width = 80;
            // 
            // clmCutLevel
            // 
            this.clmCutLevel.DataPropertyName = "cutlevel";
            this.clmCutLevel.HeaderText = "切口/愈合";
            this.clmCutLevel.Items.AddRange(new object[] {
                "0",
            "Ⅰ/甲",
            "Ⅰ/乙",
            "Ⅰ/丙",
                "Ⅰ/其他",
            "Ⅱ/甲",
            "Ⅱ/乙",
            "Ⅱ/丙",
                "Ⅱ/其他",
            "Ⅲ/甲",
            "Ⅲ/乙",
            "Ⅲ/丙","Ⅲ/其他"});
            this.clmCutLevel.Name = "clmCutLevel";
            this.clmCutLevel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmCutLevel.Width = 80;
            // 
            // ColOpzheqi
            // 
            this.ColOpzheqi.DataPropertyName = "operationelective";
            this.ColOpzheqi.HeaderText = "择期手术";
            this.ColOpzheqi.Name = "ColOpzheqi";
            this.ColOpzheqi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColOpzheqi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColOpzheqi.Width = 60;
            // 
            // clmAnaName
            // 
            this.clmAnaName.DataPropertyName = "Ananame";
            this.clmAnaName.HeaderText = "麻醉方式";
            this.clmAnaName.Name = "clmAnaName";
            this.clmAnaName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmAnaName.Width = 80;
            // 
            // clmAnaDoctor
            // 
            this.clmAnaDoctor.DataPropertyName = "anadoctor";
            this.clmAnaDoctor.HeaderText = "麻醉医师";
            this.clmAnaDoctor.Name = "clmAnaDoctor";
            this.clmAnaDoctor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmOpCode
            // 
            this.clmOpCode.DataPropertyName = "opcode";
            this.clmOpCode.HeaderText = "手术及操作编码";
            this.clmOpCode.Name = "clmOpCode";
            this.clmOpCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmOpCode.Width = 90;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "产科分娩婴儿记录表:";
            // 
            // m_dgwInfant
            // 
            this.m_dgwInfant.AllowUserToAddRows = false;
            this.m_dgwInfant.AllowUserToDeleteRows = false;
            this.m_dgwInfant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgwInfant.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmNO,
            this.clmSex,
            this.clmLaborResult,
            this.clmInfantWeight,
            this.clmInfantResult,
            this.clmInfantBreath,
            this.clmRescueTimes,
            this.clmRescueSuccTimes});
            this.m_dgwInfant.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.m_dgwInfant.Location = new System.Drawing.Point(4, 289);
            this.m_dgwInfant.Name = "m_dgwInfant";
            this.m_dgwInfant.RowHeadersVisible = false;
            this.m_dgwInfant.RowHeadersWidth = 20;
            this.m_dgwInfant.RowTemplate.Height = 23;
            this.m_dgwInfant.Size = new System.Drawing.Size(843, 230);
            this.m_dgwInfant.TabIndex = 2;
            this.m_dgwInfant.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.m_dgwInfant_EditingControlShowing);
            // 
            // clmNO
            // 
            this.clmNO.DataPropertyName = "SEQid";
            this.clmNO.HeaderText = "序号";
            this.clmNO.Name = "clmNO";
            this.clmNO.ReadOnly = true;
            this.clmNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmNO.Width = 50;
            // 
            // clmSex
            // 
            this.clmSex.DataPropertyName = "sex";
            this.clmSex.HeaderText = "性别";
            this.clmSex.Name = "clmSex";
            this.clmSex.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmSex.Width = 50;
            // 
            // clmLaborResult
            // 
            this.clmLaborResult.DataPropertyName = "LaborResult";
            this.clmLaborResult.HeaderText = "分娩结果";
            this.clmLaborResult.Name = "clmLaborResult";
            this.clmLaborResult.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmLaborResult.Width = 90;
            // 
            // clmInfantWeight
            // 
            this.clmInfantWeight.DataPropertyName = "InfantWeight";
            this.clmInfantWeight.HeaderText = "婴儿体重";
            this.clmInfantWeight.Name = "clmInfantWeight";
            this.clmInfantWeight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmInfantResult
            // 
            this.clmInfantResult.DataPropertyName = "InfantResult";
            this.clmInfantResult.HeaderText = "婴儿转归";
            this.clmInfantResult.Name = "clmInfantResult";
            this.clmInfantResult.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmInfantResult.Width = 90;
            // 
            // clmInfantBreath
            // 
            this.clmInfantBreath.DataPropertyName = "InfantBreath";
            this.clmInfantBreath.HeaderText = "呼吸";
            this.clmInfantBreath.Name = "clmInfantBreath";
            this.clmInfantBreath.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmInfantBreath.Width = 90;
            // 
            // clmRescueTimes
            // 
            this.clmRescueTimes.DataPropertyName = "RescueTimes";
            this.clmRescueTimes.HeaderText = "抢救次数";
            this.clmRescueTimes.Name = "clmRescueTimes";
            this.clmRescueTimes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmRescueSuccTimes
            // 
            this.clmRescueSuccTimes.DataPropertyName = "RescueSuccTimes";
            this.clmRescueSuccTimes.HeaderText = "成功次数";
            this.clmRescueSuccTimes.Name = "clmRescueSuccTimes";
            this.clmRescueSuccTimes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "手术记录表:";
            // 
            // m_cmdDeleteOP
            // 
            this.m_cmdDeleteOP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_cmdDeleteOP.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdDeleteOP.Image")));
            this.m_cmdDeleteOP.Location = new System.Drawing.Point(126, 3);
            this.m_cmdDeleteOP.Name = "m_cmdDeleteOP";
            this.m_cmdDeleteOP.Size = new System.Drawing.Size(25, 25);
            this.m_cmdDeleteOP.TabIndex = 30037;
            this.m_cmdDeleteOP.UseVisualStyleBackColor = true;
            this.m_cmdDeleteOP.Click += new System.EventHandler(this.m_cmdDeleteOP_Click);
            // 
            // m_cmdAddOP
            // 
            this.m_cmdAddOP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_cmdAddOP.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdAddOP.Image")));
            this.m_cmdAddOP.Location = new System.Drawing.Point(95, 3);
            this.m_cmdAddOP.Name = "m_cmdAddOP";
            this.m_cmdAddOP.Size = new System.Drawing.Size(25, 25);
            this.m_cmdAddOP.TabIndex = 30036;
            this.m_cmdAddOP.UseVisualStyleBackColor = true;
            this.m_cmdAddOP.Click += new System.EventHandler(this.m_cmdAddOP_Click);
            // 
            // m_cmdDeleteInfant
            // 
            this.m_cmdDeleteInfant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_cmdDeleteInfant.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdDeleteInfant.Image")));
            this.m_cmdDeleteInfant.Location = new System.Drawing.Point(181, 263);
            this.m_cmdDeleteInfant.Name = "m_cmdDeleteInfant";
            this.m_cmdDeleteInfant.Size = new System.Drawing.Size(25, 25);
            this.m_cmdDeleteInfant.TabIndex = 30039;
            this.m_cmdDeleteInfant.UseVisualStyleBackColor = true;
            this.m_cmdDeleteInfant.Click += new System.EventHandler(this.m_cmdDeleteInfant_Click);
            // 
            // m_cmdAddInfant
            // 
            this.m_cmdAddInfant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_cmdAddInfant.Image = ((System.Drawing.Image)(resources.GetObject("m_cmdAddInfant.Image")));
            this.m_cmdAddInfant.Location = new System.Drawing.Point(150, 263);
            this.m_cmdAddInfant.Name = "m_cmdAddInfant";
            this.m_cmdAddInfant.Size = new System.Drawing.Size(25, 25);
            this.m_cmdAddInfant.TabIndex = 30038;
            this.m_cmdAddInfant.UseVisualStyleBackColor = true;
            this.m_cmdAddInfant.Click += new System.EventHandler(this.m_cmdAddInfant_Click);
            // 
            // ctlOperationAndInfant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_cmdDeleteInfant);
            this.Controls.Add(this.m_cmdAddInfant);
            this.Controls.Add(this.m_cmdDeleteOP);
            this.Controls.Add(this.m_cmdAddOP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_dgwInfant);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_dgwOperation);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Name = "ctlOperationAndInfant";
            this.Size = new System.Drawing.Size(850, 528);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgwInfant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView m_dgwOperation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView m_dgwInfant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_cmdDeleteOP;
        private System.Windows.Forms.Button m_cmdAddOP;
        private System.Windows.Forms.Button m_cmdDeleteInfant;
        private System.Windows.Forms.Button m_cmdAddInfant;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNO;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmSex;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmLaborResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmInfantWeight;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmInfantResult;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmInfantBreath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRescueTimes;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRescueSuccTimes;
        private /*com.digitalwave.controls.MedicineStoreControls.DataGridViewCalendarColumn*/System.Windows.Forms.DataGridViewTextBoxColumn clmOpDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOpName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColOpLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm1stAssistant;
        private System.Windows.Forms.DataGridViewTextBoxColumn clm2ndAssistant;
        private System.Windows.Forms.DataGridViewComboBoxColumn clmCutLevel;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColOpzheqi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAnaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAnaDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmOpCode;
    }
}
