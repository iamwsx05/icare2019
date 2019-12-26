namespace com.digitalwave.iCare.gui.MFZ
{
    partial class ctlDocPatientWaitQueue
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
            this.m_lsvPatientQueue = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.straightLine2 = new Microsoft.Samples.StraightLine();
            this.straightLine1 = new Microsoft.Samples.StraightLine();
            this.straightLine4 = new Microsoft.Samples.StraightLine();
            this.straightLine3 = new Microsoft.Samples.StraightLine();
            this.gradientPanel1 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.m_lblRoomName = new System.Windows.Forms.Label();
            this.m_lblDeptRoomDocName = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lsvPatientQueue
            // 
            this.m_lsvPatientQueue.AllowDrop = true;
            this.m_lsvPatientQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.m_lsvPatientQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvPatientQueue.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_lsvPatientQueue.FullRowSelect = true;
            this.m_lsvPatientQueue.GridLines = true;
            this.m_lsvPatientQueue.HideSelection = false;
            this.m_lsvPatientQueue.Location = new System.Drawing.Point(0, 0);
            this.m_lsvPatientQueue.MultiSelect = false;
            this.m_lsvPatientQueue.Name = "m_lsvPatientQueue";
            this.m_lsvPatientQueue.Size = new System.Drawing.Size(162, 84);
            this.m_lsvPatientQueue.TabIndex = 29;
            this.m_lsvPatientQueue.UseCompatibleStateImageBehavior = false;
            this.m_lsvPatientQueue.View = System.Windows.Forms.View.Details;
            this.m_lsvPatientQueue.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvPatientQueue_DragEnter);
            this.m_lsvPatientQueue.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvPatientQueue_DragDrop);
            this.m_lsvPatientQueue.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvPatientQueue_ItemDrag);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "序号";
            this.columnHeader2.Width = 43;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "患者姓名";
            this.columnHeader3.Width = 107;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gradientPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine2);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine1);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine4);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_lsvPatientQueue);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(162, 115);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 51;
            // 
            // straightLine2
            // 
            this.straightLine2.BackColor = System.Drawing.Color.Transparent;
            this.straightLine2.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine2.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine2.Location = new System.Drawing.Point(0, 1);
            this.straightLine2.Name = "straightLine2";
            this.straightLine2.Size = new System.Drawing.Size(1, 28);
            this.straightLine2.TabIndex = 16;
            this.straightLine2.Text = "straightLine2";
            // 
            // straightLine1
            // 
            this.straightLine1.BackColor = System.Drawing.Color.Transparent;
            this.straightLine1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.straightLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine1.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine1.Location = new System.Drawing.Point(0, 29);
            this.straightLine1.Name = "straightLine1";
            this.straightLine1.Size = new System.Drawing.Size(161, 1);
            this.straightLine1.TabIndex = 15;
            this.straightLine1.Text = "straightLine1";
            // 
            // straightLine4
            // 
            this.straightLine4.BackColor = System.Drawing.Color.Transparent;
            this.straightLine4.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine4.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine4.Location = new System.Drawing.Point(161, 1);
            this.straightLine4.Name = "straightLine4";
            this.straightLine4.Size = new System.Drawing.Size(1, 29);
            this.straightLine4.TabIndex = 14;
            this.straightLine4.Text = "straightLine8";
            // 
            // straightLine3
            // 
            this.straightLine3.BackColor = System.Drawing.Color.Transparent;
            this.straightLine3.Dock = System.Windows.Forms.DockStyle.Top;
            this.straightLine3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine3.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine3.Location = new System.Drawing.Point(0, 0);
            this.straightLine3.Name = "straightLine3";
            this.straightLine3.Size = new System.Drawing.Size(162, 1);
            this.straightLine3.TabIndex = 12;
            this.straightLine3.Text = "straightLine10";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.m_lblRoomName);
            this.gradientPanel1.Controls.Add(this.m_lblDeptRoomDocName);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.Flip = false;
            this.gradientPanel1.FloatingImage = null;
            this.gradientPanel1.GradientAngle = 180;
            this.gradientPanel1.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel1.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel1.HorizontalFillPercent = 100F;
            this.gradientPanel1.imageXOffset = 0;
            this.gradientPanel1.imageYOffset = 0;
            this.gradientPanel1.Location = new System.Drawing.Point(1, 1);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(160, 28);
            this.gradientPanel1.TabIndex = 17;
            this.gradientPanel1.VerticalFillPercent = 100F;
            // 
            // m_lblRoomName
            // 
            this.m_lblRoomName.BackColor = System.Drawing.Color.Transparent;
            this.m_lblRoomName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.m_lblRoomName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_lblRoomName.Location = new System.Drawing.Point(27, 1);
            this.m_lblRoomName.Name = "m_lblRoomName";
            this.m_lblRoomName.Size = new System.Drawing.Size(26, 29);
            this.m_lblRoomName.TabIndex = 51;
            this.m_lblRoomName.Text = "01";
            this.m_lblRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblDeptRoomDocName
            // 
            this.m_lblDeptRoomDocName.AutoEllipsis = true;
            this.m_lblDeptRoomDocName.BackColor = System.Drawing.Color.Transparent;
            this.m_lblDeptRoomDocName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblDeptRoomDocName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblDeptRoomDocName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_lblDeptRoomDocName.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.men_offline;
            this.m_lblDeptRoomDocName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblDeptRoomDocName.Location = new System.Drawing.Point(0, 0);
            this.m_lblDeptRoomDocName.Name = "m_lblDeptRoomDocName";
            this.m_lblDeptRoomDocName.Size = new System.Drawing.Size(160, 28);
            this.m_lblDeptRoomDocName.TabIndex = 50;
            this.m_lblDeptRoomDocName.Text = "                 项目:心内一专家  \r\n                 梁永匡一【50】";
            this.m_lblDeptRoomDocName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // ctlDocPatientWaitQueue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ctlDocPatientWaitQueue";
            this.Size = new System.Drawing.Size(162, 115);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_lsvPatientQueue;
        private System.Windows.Forms.Label m_lblDeptRoomDocName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Samples.StraightLine straightLine3;
        private Microsoft.Samples.StraightLine straightLine4;
        private Microsoft.Samples.StraightLine straightLine2;
        private Microsoft.Samples.StraightLine straightLine1;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label m_lblRoomName;



    }
}
