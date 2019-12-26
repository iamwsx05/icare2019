namespace com.digitalwave.iCare.gui.MFZ
{
    partial class ctlLCDRoomElement
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
            this.timer.Stop();
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "第一位", System.Drawing.SystemColors.ActiveCaption, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(255))))), new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "下一位", System.Drawing.SystemColors.ActiveCaption, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(255))))), new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "", System.Drawing.Color.Red, System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(255))))), new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))))}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "第二位",
            "第二位"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "第三位",
            "第三位"}, -1);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.gradientPanel1 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.lblDeptName = new System.Windows.Forms.Label();
            this.gradientPanel2 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.lblDocName = new System.Windows.Forms.Label();
            this.gradientPanel3 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.gradientPanel4 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.lblDocType = new System.Windows.Forms.Label();
            this.gradientPanel5 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblHz = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.gradientPanel6 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            this.gradientPanel3.SuspendLayout();
            this.gradientPanel4.SuspendLayout();
            this.gradientPanel5.SuspendLayout();
            this.gradientPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gradientPanel6, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(273, 268);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gradientPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(269, 91);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.gradientPanel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gradientPanel2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(53, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(216, 91);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tableLayoutPanel4.Controls.Add(this.gradientPanel4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.gradientPanel5, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(2, 95);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(269, 39);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.lblDeptName);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.Flip = false;
            this.gradientPanel1.FloatingImage = null;
            this.gradientPanel1.GradientAngle = 180;
            this.gradientPanel1.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel1.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel1.HorizontalFillPercent = 100F;
            this.gradientPanel1.imageXOffset = 0;
            this.gradientPanel1.imageYOffset = 0;
            this.gradientPanel1.Location = new System.Drawing.Point(2, 2);
            this.gradientPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(212, 34);
            this.gradientPanel1.TabIndex = 1;
            this.gradientPanel1.VerticalFillPercent = 100F;
            // 
            // lblDeptName
            // 
            this.lblDeptName.AutoSize = true;
            this.lblDeptName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeptName.Font = new System.Drawing.Font("宋体", 20.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDeptName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDeptName.Location = new System.Drawing.Point(13, 9);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(0, 28);
            this.lblDeptName.TabIndex = 0;
            this.lblDeptName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.Transparent;
            this.gradientPanel2.Controls.Add(this.lblDocName);
            this.gradientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel2.Flip = false;
            this.gradientPanel2.FloatingImage = null;
            this.gradientPanel2.GradientAngle = 180;
            this.gradientPanel2.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel2.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel2.HorizontalFillPercent = 100F;
            this.gradientPanel2.imageXOffset = 0;
            this.gradientPanel2.imageYOffset = 0;
            this.gradientPanel2.Location = new System.Drawing.Point(2, 38);
            this.gradientPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(212, 51);
            this.gradientPanel2.TabIndex = 2;
            this.gradientPanel2.VerticalFillPercent = 100F;
            // 
            // lblDocName
            // 
            this.lblDocName.AutoSize = true;
            this.lblDocName.Font = new System.Drawing.Font("宋体", 23.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDocName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDocName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDocName.Location = new System.Drawing.Point(12, 17);
            this.lblDocName.Name = "lblDocName";
            this.lblDocName.Size = new System.Drawing.Size(0, 33);
            this.lblDocName.TabIndex = 0;
            this.lblDocName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gradientPanel3
            // 
            this.gradientPanel3.Controls.Add(this.lblRoomName);
            this.gradientPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel3.Flip = false;
            this.gradientPanel3.FloatingImage = null;
            this.gradientPanel3.GradientAngle = 0;
            this.gradientPanel3.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel3.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel3.HorizontalFillPercent = 100F;
            this.gradientPanel3.imageXOffset = 0;
            this.gradientPanel3.imageYOffset = 0;
            this.gradientPanel3.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel3.Name = "gradientPanel3";
            this.gradientPanel3.Size = new System.Drawing.Size(53, 91);
            this.gradientPanel3.TabIndex = 1;
            this.gradientPanel3.VerticalFillPercent = 100F;
            // 
            // lblRoomName
            // 
            this.lblRoomName.Font = new System.Drawing.Font("宋体", 14.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRoomName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblRoomName.Location = new System.Drawing.Point(16, 11);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(0, 20);
            this.lblRoomName.TabIndex = 0;
            this.lblRoomName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientPanel4
            // 
            this.gradientPanel4.Controls.Add(this.lblDocType);
            this.gradientPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel4.Flip = false;
            this.gradientPanel4.FloatingImage = null;
            this.gradientPanel4.GradientAngle = 0;
            this.gradientPanel4.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel4.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel4.HorizontalFillPercent = 100F;
            this.gradientPanel4.imageXOffset = 0;
            this.gradientPanel4.imageYOffset = 0;
            this.gradientPanel4.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel4.Name = "gradientPanel4";
            this.gradientPanel4.Size = new System.Drawing.Size(166, 39);
            this.gradientPanel4.TabIndex = 2;
            this.gradientPanel4.VerticalFillPercent = 100F;
            // 
            // lblDocType
            // 
            this.lblDocType.AutoSize = true;
            this.lblDocType.BackColor = System.Drawing.Color.Transparent;
            this.lblDocType.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDocType.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblDocType.Location = new System.Drawing.Point(15, 9);
            this.lblDocType.Name = "lblDocType";
            this.lblDocType.Size = new System.Drawing.Size(0, 26);
            this.lblDocType.TabIndex = 0;
            this.lblDocType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gradientPanel5
            // 
            this.gradientPanel5.Controls.Add(this.lblCount);
            this.gradientPanel5.Controls.Add(this.lblHz);
            this.gradientPanel5.Controls.Add(this.lblR);
            this.gradientPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel5.Flip = false;
            this.gradientPanel5.FloatingImage = null;
            this.gradientPanel5.GradientAngle = 180;
            this.gradientPanel5.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel5.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel5.HorizontalFillPercent = 100F;
            this.gradientPanel5.imageXOffset = 0;
            this.gradientPanel5.imageYOffset = 0;
            this.gradientPanel5.Location = new System.Drawing.Point(166, 0);
            this.gradientPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel5.Name = "gradientPanel5";
            this.gradientPanel5.Size = new System.Drawing.Size(103, 39);
            this.gradientPanel5.TabIndex = 3;
            this.gradientPanel5.VerticalFillPercent = 100F;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Font = new System.Drawing.Font("Arial", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCount.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCount.Location = new System.Drawing.Point(46, 12);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(28, 18);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "25";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHz
            // 
            this.lblHz.AutoSize = true;
            this.lblHz.BackColor = System.Drawing.Color.Transparent;
            this.lblHz.Font = new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHz.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblHz.Location = new System.Drawing.Point(3, 13);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(44, 18);
            this.lblHz.TabIndex = 0;
            this.lblHz.Text = "候诊";
            this.lblHz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.BackColor = System.Drawing.Color.Transparent;
            this.lblR.Font = new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblR.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblR.Location = new System.Drawing.Point(73, 13);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(26, 18);
            this.lblR.TabIndex = 2;
            this.lblR.Text = "人";
            this.lblR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gradientPanel6
            // 
            this.gradientPanel6.Controls.Add(this.listView1);
            this.gradientPanel6.Controls.Add(this.tableLayoutPanel5);
            this.gradientPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel6.Flip = false;
            this.gradientPanel6.FloatingImage = null;
            this.gradientPanel6.GradientAngle = 90;
            this.gradientPanel6.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.gradientPanel6.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.gradientPanel6.HorizontalFillPercent = 100F;
            this.gradientPanel6.imageXOffset = 0;
            this.gradientPanel6.imageYOffset = 0;
            this.gradientPanel6.Location = new System.Drawing.Point(2, 136);
            this.gradientPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.gradientPanel6.Name = "gradientPanel6";
            this.gradientPanel6.Size = new System.Drawing.Size(269, 130);
            this.gradientPanel6.TabIndex = 2;
            this.gradientPanel6.VerticalFillPercent = 100F;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("宋体", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.UseItemStyleForSubItems = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(0, 36);
            this.listView1.Margin = new System.Windows.Forms.Padding(0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Scrollable = false;
            this.listView1.Size = new System.Drawing.Size(269, 94);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "队列";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "病人姓名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 148;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(269, 36);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(5, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "序号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(117, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "患者姓名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctlLCDRoomElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ctlLCDRoomElement";
            this.Size = new System.Drawing.Size(273, 268);
            this.Load += new System.EventHandler(this.ctlLCDRoomElement_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.gradientPanel2.ResumeLayout(false);
            this.gradientPanel2.PerformLayout();
            this.gradientPanel3.ResumeLayout(false);
            this.gradientPanel4.ResumeLayout(false);
            this.gradientPanel4.PerformLayout();
            this.gradientPanel5.ResumeLayout(false);
            this.gradientPanel5.PerformLayout();
            this.gradientPanel6.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.Label lblDocName;
        private System.Windows.Forms.Label lblDeptName;
        private System.Windows.Forms.Label lblDocType;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblHz;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel1;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel2;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel3;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel4;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel5;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel6;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}
