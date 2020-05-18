namespace com.digitalwave.emr.EMR_SynchronousCase
{
    partial class frmEMR_SynchronousDepartment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMR_SynchronousDepartment));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_lsvIcareDept = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lsvBADept = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_cmdSave = new System.Windows.Forms.Button();
            this.m_cmdExit = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_lsvIcareDept);
            this.groupBox1.Location = new System.Drawing.Point(315, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 392);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "iCare专业组";
            // 
            // m_lsvIcareDept
            // 
            this.m_lsvIcareDept.CheckBoxes = true;
            this.m_lsvIcareDept.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_lsvIcareDept.GridLines = true;
            this.m_lsvIcareDept.HideSelection = false;
            this.m_lsvIcareDept.Location = new System.Drawing.Point(11, 22);
            this.m_lsvIcareDept.MultiSelect = false;
            this.m_lsvIcareDept.Name = "m_lsvIcareDept";
            this.m_lsvIcareDept.Size = new System.Drawing.Size(270, 364);
            this.m_lsvIcareDept.TabIndex = 1;
            this.m_lsvIcareDept.UseCompatibleStateImageBehavior = false;
            this.m_lsvIcareDept.View = System.Windows.Forms.View.Details;
            this.m_lsvIcareDept.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_lsvIcareDept_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "科室名称";
            this.columnHeader1.Width = 250;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_lsvBADept);
            this.groupBox2.Location = new System.Drawing.Point(13, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 392);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "病案系统科室";
            // 
            // m_lsvBADept
            // 
            this.m_lsvBADept.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_lsvBADept.GridLines = true;
            this.m_lsvBADept.HideSelection = false;
            this.m_lsvBADept.Location = new System.Drawing.Point(11, 22);
            this.m_lsvBADept.MultiSelect = false;
            this.m_lsvBADept.Name = "m_lsvBADept";
            this.m_lsvBADept.Size = new System.Drawing.Size(270, 364);
            this.m_lsvBADept.TabIndex = 2;
            this.m_lsvBADept.UseCompatibleStateImageBehavior = false;
            this.m_lsvBADept.View = System.Windows.Forms.View.Details;
            this.m_lsvBADept.SelectedIndexChanged += new System.EventHandler(this.m_lsvBADept_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "科室名称";
            this.columnHeader2.Width = 250;
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Location = new System.Drawing.Point(205, 417);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Size = new System.Drawing.Size(75, 26);
            this.m_cmdSave.TabIndex = 3;
            this.m_cmdSave.Text = "保存(&S)";
            this.m_cmdSave.UseVisualStyleBackColor = true;
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdExit.Location = new System.Drawing.Point(347, 417);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Size = new System.Drawing.Size(75, 26);
            this.m_cmdExit.TabIndex = 3;
            this.m_cmdExit.Text = "退出(&Q)";
            this.m_cmdExit.UseVisualStyleBackColor = true;
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmEMR_SynchronousDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cmdExit;
            this.ClientSize = new System.Drawing.Size(622, 451);
            this.Controls.Add(this.m_cmdExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEMR_SynchronousDepartment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科室同步配置";
            this.Load += new System.EventHandler(this.frmEMR_SynchronousDepartment_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView m_lsvIcareDept;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView m_lsvBADept;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button m_cmdSave;
        private System.Windows.Forms.Button m_cmdExit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}