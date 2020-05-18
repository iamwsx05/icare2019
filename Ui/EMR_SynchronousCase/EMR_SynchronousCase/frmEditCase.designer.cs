namespace com.digitalwave.emr.EMR_SynchronousCase
{
    partial class frmEditCase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditCase));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_tsbNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tslNavigatorCount = new System.Windows.Forms.ToolStripLabel();
            this.m_tstPosition = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbSynchronousCase = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbReGetCaseData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tsbCancle = new System.Windows.Forms.ToolStripButton();
            this.m_pnlContain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.m_pgbCommit = new System.Windows.Forms.ProgressBar();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tsbNext,
            this.toolStripSeparator1,
            this.m_tslNavigatorCount,
            this.m_tstPosition,
            this.toolStripSeparator2,
            this.m_tsbPrevious,
            this.toolStripSeparator3,
            this.m_tsbSave,
            this.toolStripSeparator4,
            this.m_tsbSynchronousCase,
            this.toolStripSeparator5,
            this.m_tsbReGetCaseData,
            this.toolStripSeparator6,
            this.m_tsbCancle});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(852, 37);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // m_tsbNext
            // 
            this.m_tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbNext.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbNext.Image")));
            this.m_tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbNext.Name = "m_tsbNext";
            this.m_tsbNext.Size = new System.Drawing.Size(23, 34);
            this.m_tsbNext.Text = "移到下一页";
            this.m_tsbNext.ToolTipText = "移到下一页";
            this.m_tsbNext.Click += new System.EventHandler(this.m_tsbNext_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tslNavigatorCount
            // 
            this.m_tslNavigatorCount.Name = "m_tslNavigatorCount";
            this.m_tslNavigatorCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.m_tslNavigatorCount.Size = new System.Drawing.Size(30, 34);
            this.m_tslNavigatorCount.Text = "/ {0}";
            this.m_tslNavigatorCount.ToolTipText = "总页数";
            // 
            // m_tstPosition
            // 
            this.m_tstPosition.BackColor = System.Drawing.Color.White;
            this.m_tstPosition.Name = "m_tstPosition";
            this.m_tstPosition.ReadOnly = true;
            this.m_tstPosition.Size = new System.Drawing.Size(40, 37);
            this.m_tstPosition.ToolTipText = "当前位置";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tsbPrevious
            // 
            this.m_tsbPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_tsbPrevious.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbPrevious.Image")));
            this.m_tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbPrevious.Name = "m_tsbPrevious";
            this.m_tsbPrevious.Size = new System.Drawing.Size(23, 34);
            this.m_tsbPrevious.Text = "移到上一页";
            this.m_tsbPrevious.ToolTipText = "移到上一页";
            this.m_tsbPrevious.Click += new System.EventHandler(this.m_tsbPrevious_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tsbSave
            // 
            this.m_tsbSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.m_tsbSave.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbSave.Image")));
            this.m_tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSave.Name = "m_tsbSave";
            this.m_tsbSave.Size = new System.Drawing.Size(57, 34);
            this.m_tsbSave.Text = "保存";
            this.m_tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_tsbSave.ToolTipText = "保存";
            this.m_tsbSave.Click += new System.EventHandler(this.m_tsbSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tsbSynchronousCase
            // 
            this.m_tsbSynchronousCase.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.m_tsbSynchronousCase.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbSynchronousCase.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbSynchronousCase.Image")));
            this.m_tsbSynchronousCase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbSynchronousCase.Name = "m_tsbSynchronousCase";
            this.m_tsbSynchronousCase.Size = new System.Drawing.Size(102, 34);
            this.m_tsbSynchronousCase.Text = "保存并同步";
            this.m_tsbSynchronousCase.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_tsbSynchronousCase.Click += new System.EventHandler(this.m_tsbSynchronousCase_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tsbReGetCaseData
            // 
            this.m_tsbReGetCaseData.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_tsbReGetCaseData.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbReGetCaseData.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbReGetCaseData.Image")));
            this.m_tsbReGetCaseData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbReGetCaseData.Name = "m_tsbReGetCaseData";
            this.m_tsbReGetCaseData.Size = new System.Drawing.Size(117, 34);
            this.m_tsbReGetCaseData.Text = "重取首页数据";
            this.m_tsbReGetCaseData.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_tsbReGetCaseData.Click += new System.EventHandler(this.m_tsbReGetCaseData_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 37);
            // 
            // m_tsbCancle
            // 
            this.m_tsbCancle.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.m_tsbCancle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_tsbCancle.Image = ((System.Drawing.Image)(resources.GetObject("m_tsbCancle.Image")));
            this.m_tsbCancle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tsbCancle.Name = "m_tsbCancle";
            this.m_tsbCancle.Size = new System.Drawing.Size(57, 34);
            this.m_tsbCancle.Text = "关闭";
            this.m_tsbCancle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_tsbCancle.ToolTipText = "取消";
            this.m_tsbCancle.Click += new System.EventHandler(this.m_tsbCancle_Click);
            // 
            // m_pnlContain
            // 
            this.m_pnlContain.BackColor = System.Drawing.SystemColors.Control;
            this.m_pnlContain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pnlContain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlContain.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_pnlContain.Location = new System.Drawing.Point(0, 37);
            this.m_pnlContain.Name = "m_pnlContain";
            this.m_pnlContain.Size = new System.Drawing.Size(852, 529);
            this.m_pnlContain.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "病案资料编辑";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_pgbCommit
            // 
            this.m_pgbCommit.Location = new System.Drawing.Point(116, 6);
            this.m_pgbCommit.Name = "m_pgbCommit";
            this.m_pgbCommit.Size = new System.Drawing.Size(230, 23);
            this.m_pgbCommit.TabIndex = 7;
            this.m_pgbCommit.Visible = false;
            // 
            // frmEditCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(852, 566);
            this.Controls.Add(this.m_pgbCommit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_pnlContain);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEditCase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病案";
            this.Load += new System.EventHandler(this.frmEditCase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_tsbPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_tsbNext;
        private System.Windows.Forms.ToolStripLabel m_tslNavigatorCount;
        private System.Windows.Forms.ToolStripTextBox m_tstPosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton m_tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton m_tsbSynchronousCase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton m_tsbCancle;
        private System.Windows.Forms.Panel m_pnlContain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar m_pgbCommit;
        private System.Windows.Forms.ToolStripButton m_tsbReGetCaseData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    }
}