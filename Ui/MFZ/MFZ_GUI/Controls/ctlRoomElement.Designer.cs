namespace com.digitalwave.iCare.gui.MFZ
{
    partial class ctlRoomElement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gradientPanel1 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.m_lblRoom = new System.Windows.Forms.Label();
            this.m_lblAdd = new System.Windows.Forms.Label();
            this.m_lblRoomName = new System.Windows.Forms.Label();
            this.straightLine7 = new Microsoft.Samples.StraightLine();
            this.straightLine8 = new Microsoft.Samples.StraightLine();
            this.straightLine9 = new Microsoft.Samples.StraightLine();
            this.straightLine10 = new Microsoft.Samples.StraightLine();
            this.m_lsvRoom = new System.Windows.Forms.ListView();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 81);
            this.panel1.TabIndex = 37;
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
            this.splitContainer1.Panel1.Controls.Add(this.straightLine7);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine8);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine9);
            this.splitContainer1.Panel1.Controls.Add(this.straightLine10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_lsvRoom);
            this.splitContainer1.Size = new System.Drawing.Size(155, 81);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 38;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.m_lblRoom);
            this.gradientPanel1.Controls.Add(this.m_lblAdd);
            this.gradientPanel1.Controls.Add(this.m_lblRoomName);
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
            this.gradientPanel1.Size = new System.Drawing.Size(153, 26);
            this.gradientPanel1.TabIndex = 15;
            this.gradientPanel1.VerticalFillPercent = 100F;
            // 
            // m_lblRoom
            // 
            this.m_lblRoom.BackColor = System.Drawing.Color.Transparent;
            this.m_lblRoom.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_lblRoom.Location = new System.Drawing.Point(28, 6);
            this.m_lblRoom.Name = "m_lblRoom";
            this.m_lblRoom.Size = new System.Drawing.Size(24, 23);
            this.m_lblRoom.TabIndex = 13;
            this.m_lblRoom.Text = "01";
            this.m_lblRoom.Visible = false;
            // 
            // m_lblAdd
            // 
            this.m_lblAdd.BackColor = System.Drawing.Color.Transparent;
            this.m_lblAdd.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.add;
            this.m_lblAdd.Location = new System.Drawing.Point(124, 3);
            this.m_lblAdd.Name = "m_lblAdd";
            this.m_lblAdd.Size = new System.Drawing.Size(26, 22);
            this.m_lblAdd.TabIndex = 12;
            this.m_lblAdd.Click += new System.EventHandler(this.m_lblAdd_Click);
            // 
            // m_lblRoomName
            // 
            this.m_lblRoomName.AutoEllipsis = true;
            this.m_lblRoomName.BackColor = System.Drawing.Color.Transparent;
            this.m_lblRoomName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblRoomName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblRoomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(70)))), ((int)(((byte)(96)))));
            this.m_lblRoomName.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.roomBlueLine;
            this.m_lblRoomName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblRoomName.Location = new System.Drawing.Point(0, 0);
            this.m_lblRoomName.Name = "m_lblRoomName";
            this.m_lblRoomName.Size = new System.Drawing.Size(153, 26);
            this.m_lblRoomName.TabIndex = 11;
            this.m_lblRoomName.Text = "                诊室:内一\r\n                科室:内科\r\n\r\n\r\n\r\n        ";
            this.m_lblRoomName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // straightLine7
            // 
            this.straightLine7.BackColor = System.Drawing.Color.Transparent;
            this.straightLine7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.straightLine7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine7.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine7.Location = new System.Drawing.Point(1, 27);
            this.straightLine7.Name = "straightLine7";
            this.straightLine7.Size = new System.Drawing.Size(153, 1);
            this.straightLine7.TabIndex = 14;
            this.straightLine7.Text = "straightLine7";
            // 
            // straightLine8
            // 
            this.straightLine8.BackColor = System.Drawing.Color.Transparent;
            this.straightLine8.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine8.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine8.Location = new System.Drawing.Point(154, 1);
            this.straightLine8.Name = "straightLine8";
            this.straightLine8.Size = new System.Drawing.Size(1, 27);
            this.straightLine8.TabIndex = 13;
            this.straightLine8.Text = "straightLine8";
            // 
            // straightLine9
            // 
            this.straightLine9.BackColor = System.Drawing.Color.Transparent;
            this.straightLine9.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine9.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine9.Location = new System.Drawing.Point(0, 1);
            this.straightLine9.Name = "straightLine9";
            this.straightLine9.Size = new System.Drawing.Size(1, 27);
            this.straightLine9.TabIndex = 12;
            this.straightLine9.Text = "straightLine9";
            // 
            // straightLine10
            // 
            this.straightLine10.BackColor = System.Drawing.Color.Transparent;
            this.straightLine10.Dock = System.Windows.Forms.DockStyle.Top;
            this.straightLine10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine10.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine10.Location = new System.Drawing.Point(0, 0);
            this.straightLine10.Name = "straightLine10";
            this.straightLine10.Size = new System.Drawing.Size(155, 1);
            this.straightLine10.TabIndex = 11;
            this.straightLine10.Text = "straightLine10";
            // 
            // m_lsvRoom
            // 
            this.m_lsvRoom.AllowDrop = true;
            this.m_lsvRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_lsvRoom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader16});
            this.m_lsvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvRoom.Font = new System.Drawing.Font("宋体", 10.5F);
            this.m_lsvRoom.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.m_lsvRoom.FullRowSelect = true;
            this.m_lsvRoom.GridLines = true;
            this.m_lsvRoom.HideSelection = false;
            this.m_lsvRoom.Location = new System.Drawing.Point(0, 0);
            this.m_lsvRoom.MultiSelect = false;
            this.m_lsvRoom.Name = "m_lsvRoom";
            this.m_lsvRoom.Size = new System.Drawing.Size(155, 52);
            this.m_lsvRoom.TabIndex = 26;
            this.m_lsvRoom.UseCompatibleStateImageBehavior = false;
            this.m_lsvRoom.View = System.Windows.Forms.View.Details;
            this.m_lsvRoom.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvRoom_DragEnter);
            this.m_lsvRoom.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvRoom_DragDrop);
            this.m_lsvRoom.DoubleClick += new System.EventHandler(this.m_lsvRoom_DoubleClick);
            this.m_lsvRoom.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvRoom_ItemDrag);
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "工作站";
            this.columnHeader15.Width = 68;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "医生";
            this.columnHeader16.Width = 72;
            // 
            // ctlRoomElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ctlRoomElement";
            this.Size = new System.Drawing.Size(155, 81);
            this.ParentChanged += new System.EventHandler(this.ctlRoomElement_ParentChanged);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView m_lsvRoom;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel1;
        private Microsoft.Samples.StraightLine straightLine7;
        private Microsoft.Samples.StraightLine straightLine8;
        private Microsoft.Samples.StraightLine straightLine9;
        private Microsoft.Samples.StraightLine straightLine10;
        private System.Windows.Forms.Label m_lblRoomName;
        private System.Windows.Forms.Label m_lblAdd;
        private System.Windows.Forms.Label m_lblRoom;
    }
}
