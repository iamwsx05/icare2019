namespace com.digitalwave.emr.EMR_SynchronousCase
{
    partial class frmEMR_SynchronousPayType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEMR_SynchronousPayType));
            this.m_cmdExit = new System.Windows.Forms.Button();
            this.m_cmdSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_lsvICarePayType = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_lsvBA_PayType = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdExit.Location = new System.Drawing.Point(347, 416);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Size = new System.Drawing.Size(75, 26);
            this.m_cmdExit.TabIndex = 6;
            this.m_cmdExit.Text = "退出(&Q)";
            this.m_cmdExit.UseVisualStyleBackColor = true;
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.Location = new System.Drawing.Point(205, 416);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Size = new System.Drawing.Size(75, 26);
            this.m_cmdSave.TabIndex = 7;
            this.m_cmdSave.Text = "保存(&S)";
            this.m_cmdSave.UseVisualStyleBackColor = true;
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_lsvICarePayType);
            this.groupBox2.Location = new System.Drawing.Point(321, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 392);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "iCare系统费别";
            // 
            // m_lsvICarePayType
            // 
            this.m_lsvICarePayType.CheckBoxes = true;
            this.m_lsvICarePayType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_lsvICarePayType.GridLines = true;
            this.m_lsvICarePayType.HideSelection = false;
            this.m_lsvICarePayType.Location = new System.Drawing.Point(11, 22);
            this.m_lsvICarePayType.Name = "m_lsvICarePayType";
            this.m_lsvICarePayType.Size = new System.Drawing.Size(270, 364);
            this.m_lsvICarePayType.TabIndex = 2;
            this.m_lsvICarePayType.UseCompatibleStateImageBehavior = false;
            this.m_lsvICarePayType.View = System.Windows.Forms.View.Details;
            this.m_lsvICarePayType.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.m_lsvICarePayType_ItemChecked);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "费别名称";
            this.columnHeader2.Width = 250;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_lsvBA_PayType);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 392);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "病案费别";
            // 
            // m_lsvBA_PayType
            // 
            this.m_lsvBA_PayType.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_lsvBA_PayType.GridLines = true;
            this.m_lsvBA_PayType.HideSelection = false;
            this.m_lsvBA_PayType.Location = new System.Drawing.Point(11, 22);
            this.m_lsvBA_PayType.MultiSelect = false;
            this.m_lsvBA_PayType.Name = "m_lsvBA_PayType";
            this.m_lsvBA_PayType.Size = new System.Drawing.Size(270, 364);
            this.m_lsvBA_PayType.TabIndex = 1;
            this.m_lsvBA_PayType.UseCompatibleStateImageBehavior = false;
            this.m_lsvBA_PayType.View = System.Windows.Forms.View.Details;
            this.m_lsvBA_PayType.SelectedIndexChanged += new System.EventHandler(this.m_lsvBA_PayType_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "费别名称";
            this.columnHeader1.Width = 250;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frmEMR_SynchronousPayType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 451);
            this.Controls.Add(this.m_cmdExit);
            this.Controls.Add(this.m_cmdSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEMR_SynchronousPayType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "费别同步";
            this.Load += new System.EventHandler(this.frmEMR_SynchronousPayType_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_cmdExit;
        private System.Windows.Forms.Button m_cmdSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView m_lsvICarePayType;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView m_lsvBA_PayType;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}