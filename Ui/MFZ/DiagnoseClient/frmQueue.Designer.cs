namespace DiagnoseClient
{
    partial class frmQueue
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
            this.components = new System.ComponentModel.Container();
            this.lsvQueue = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lsvQueue
            // 
            this.lsvQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvQueue.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lsvQueue.FullRowSelect = true;
            this.lsvQueue.GridLines = true;
            this.lsvQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvQueue.Location = new System.Drawing.Point(2, 2);
            this.lsvQueue.MultiSelect = false;
            this.lsvQueue.Name = "lsvQueue";
            this.lsvQueue.Size = new System.Drawing.Size(96, 478);
            this.lsvQueue.TabIndex = 0;
            this.lsvQueue.UseCompatibleStateImageBehavior = false;
            this.lsvQueue.View = System.Windows.Forms.View.Details;
            this.lsvQueue.ItemActivate += new System.EventHandler(this.lsvQueue_ItemActivate);
            this.lsvQueue.MouseLeave += new System.EventHandler(this.lsvQueue_MouseLeave);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 70;
            // 
            // timerHide
            // 
            this.timerHide.Interval = 1000;
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // frmQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(102, 483);
            this.Controls.Add(this.lsvQueue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQueue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmShareQueue";
            this.MouseLeave += new System.EventHandler(this.frmQueue_MouseLeave);
            this.Load += new System.EventHandler(this.frmShareQueue_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvQueue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timerHide;

    }
}