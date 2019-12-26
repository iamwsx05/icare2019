namespace com.digitalwave.iCare.gui.MFZ
{
    partial class frmPatientManager
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_lsvComShareWaitQueue = new System.Windows.Forms.ListView();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.gradientPanelCommom = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.straightLine4 = new Microsoft.Samples.StraightLine();
            this.straightLine3 = new Microsoft.Samples.StraightLine();
            this.straightLine2 = new Microsoft.Samples.StraightLine();
            this.straightLine1 = new Microsoft.Samples.StraightLine();
            this.m_lblQueueCommon = new System.Windows.Forms.Label();
            this.m_lsvExpShareWaitQueue = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.gradientPanel1 = new com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel();
            this.straightLine5 = new Microsoft.Samples.StraightLine();
            this.m_lblQueueExp = new System.Windows.Forms.Label();
            this.straightLine6 = new Microsoft.Samples.StraightLine();
            this.straightLine7 = new Microsoft.Samples.StraightLine();
            this.straightLine8 = new Microsoft.Samples.StraightLine();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_lsvCalledQueue = new System.Windows.Forms.ListView();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnVoiceSet = new PinkieControls.ButtonXP();
            this.m_cmdCurrentScreen = new PinkieControls.ButtonXP();
            this.m_cmdSend = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_txtCard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctlCboDiagnoseArea = new com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gradientPanelCommom.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_flpMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 746);
            this.splitContainer1.SplitterDistance = 793;
            this.splitContainer1.TabIndex = 46;
            // 
            // m_flpMain
            // 
            this.m_flpMain.AutoScroll = true;
            this.m_flpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_flpMain.Location = new System.Drawing.Point(0, 0);
            this.m_flpMain.Name = "m_flpMain";
            this.m_flpMain.Size = new System.Drawing.Size(793, 746);
            this.m_flpMain.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 746);
            this.panel1.TabIndex = 46;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 196);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(231, 550);
            this.tabControl1.TabIndex = 36;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(223, 522);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "等候队列";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_lsvComShareWaitQueue);
            this.splitContainer2.Panel1.Controls.Add(this.gradientPanelCommom);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_lsvExpShareWaitQueue);
            this.splitContainer2.Panel2.Controls.Add(this.gradientPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(223, 522);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 48;
            // 
            // m_lsvComShareWaitQueue
            // 
            this.m_lsvComShareWaitQueue.AllowDrop = true;
            this.m_lsvComShareWaitQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader19,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader8});
            this.m_lsvComShareWaitQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvComShareWaitQueue.FullRowSelect = true;
            this.m_lsvComShareWaitQueue.GridLines = true;
            this.m_lsvComShareWaitQueue.Location = new System.Drawing.Point(0, 26);
            this.m_lsvComShareWaitQueue.MultiSelect = false;
            this.m_lsvComShareWaitQueue.Name = "m_lsvComShareWaitQueue";
            this.m_lsvComShareWaitQueue.Size = new System.Drawing.Size(223, 232);
            this.m_lsvComShareWaitQueue.TabIndex = 46;
            this.m_lsvComShareWaitQueue.UseCompatibleStateImageBehavior = false;
            this.m_lsvComShareWaitQueue.View = System.Windows.Forms.View.Details;
            this.m_lsvComShareWaitQueue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_lsvComShareWaitQueue_MouseClick);
            this.m_lsvComShareWaitQueue.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvComShareWaitQueue_DragDrop);
            this.m_lsvComShareWaitQueue.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvComShareWaitQueue_DragEnter);
            this.m_lsvComShareWaitQueue.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvComShareWaitQueue_ItemDrag);
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "姓 名";
            this.columnHeader19.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "就诊科室";
            this.columnHeader2.Width = 96;
            // 
            // gradientPanelCommom
            // 
            this.gradientPanelCommom.Controls.Add(this.straightLine4);
            this.gradientPanelCommom.Controls.Add(this.straightLine3);
            this.gradientPanelCommom.Controls.Add(this.straightLine2);
            this.gradientPanelCommom.Controls.Add(this.straightLine1);
            this.gradientPanelCommom.Controls.Add(this.m_lblQueueCommon);
            this.gradientPanelCommom.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanelCommom.Flip = false;
            this.gradientPanelCommom.FloatingImage = null;
            this.gradientPanelCommom.GradientAngle = 180;
            this.gradientPanelCommom.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanelCommom.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanelCommom.HorizontalFillPercent = 100F;
            this.gradientPanelCommom.imageXOffset = 0;
            this.gradientPanelCommom.imageYOffset = 0;
            this.gradientPanelCommom.Location = new System.Drawing.Point(0, 0);
            this.gradientPanelCommom.Name = "gradientPanelCommom";
            this.gradientPanelCommom.Size = new System.Drawing.Size(223, 26);
            this.gradientPanelCommom.TabIndex = 52;
            this.gradientPanelCommom.VerticalFillPercent = 100F;
            // 
            // straightLine4
            // 
            this.straightLine4.BackColor = System.Drawing.Color.Transparent;
            this.straightLine4.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine4.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine4.Location = new System.Drawing.Point(0, 1);
            this.straightLine4.Name = "straightLine4";
            this.straightLine4.Size = new System.Drawing.Size(1, 24);
            this.straightLine4.TabIndex = 3;
            this.straightLine4.Text = "straightLine4";
            // 
            // straightLine3
            // 
            this.straightLine3.BackColor = System.Drawing.Color.Transparent;
            this.straightLine3.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine3.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine3.Location = new System.Drawing.Point(222, 1);
            this.straightLine3.Name = "straightLine3";
            this.straightLine3.Size = new System.Drawing.Size(1, 24);
            this.straightLine3.TabIndex = 2;
            this.straightLine3.Text = "straightLine3";
            // 
            // straightLine2
            // 
            this.straightLine2.BackColor = System.Drawing.Color.Transparent;
            this.straightLine2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.straightLine2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine2.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine2.Location = new System.Drawing.Point(0, 25);
            this.straightLine2.Name = "straightLine2";
            this.straightLine2.Size = new System.Drawing.Size(223, 1);
            this.straightLine2.TabIndex = 1;
            this.straightLine2.Text = "straightLine2";
            // 
            // straightLine1
            // 
            this.straightLine1.BackColor = System.Drawing.Color.Transparent;
            this.straightLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.straightLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine1.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine1.Location = new System.Drawing.Point(0, 0);
            this.straightLine1.Name = "straightLine1";
            this.straightLine1.Size = new System.Drawing.Size(223, 1);
            this.straightLine1.TabIndex = 0;
            this.straightLine1.Text = "straightLine1";
            // 
            // m_lblQueueCommon
            // 
            this.m_lblQueueCommon.AutoEllipsis = true;
            this.m_lblQueueCommon.BackColor = System.Drawing.Color.Transparent;
            this.m_lblQueueCommon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblQueueCommon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblQueueCommon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(70)))), ((int)(((byte)(96)))));
            this.m_lblQueueCommon.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimize;
            this.m_lblQueueCommon.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lblQueueCommon.Location = new System.Drawing.Point(0, 0);
            this.m_lblQueueCommon.Name = "m_lblQueueCommon";
            this.m_lblQueueCommon.Size = new System.Drawing.Size(223, 26);
            this.m_lblQueueCommon.TabIndex = 51;
            this.m_lblQueueCommon.Text = " 普通队列";
            this.m_lblQueueCommon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblQueueCommon.Click += new System.EventHandler(this.m_lblQueueCommon_Click);
            // 
            // m_lsvExpShareWaitQueue
            // 
            this.m_lsvExpShareWaitQueue.AllowDrop = true;
            this.m_lsvExpShareWaitQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6});
            this.m_lsvExpShareWaitQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvExpShareWaitQueue.FullRowSelect = true;
            this.m_lsvExpShareWaitQueue.GridLines = true;
            this.m_lsvExpShareWaitQueue.Location = new System.Drawing.Point(0, 26);
            this.m_lsvExpShareWaitQueue.MultiSelect = false;
            this.m_lsvExpShareWaitQueue.Name = "m_lsvExpShareWaitQueue";
            this.m_lsvExpShareWaitQueue.Size = new System.Drawing.Size(223, 234);
            this.m_lsvExpShareWaitQueue.TabIndex = 46;
            this.m_lsvExpShareWaitQueue.UseCompatibleStateImageBehavior = false;
            this.m_lsvExpShareWaitQueue.View = System.Windows.Forms.View.Details;
            this.m_lsvExpShareWaitQueue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_lsvExpShareWaitQueue_MouseClick);
            this.m_lsvExpShareWaitQueue.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvExpShareWaitQueue_DragDrop);
            this.m_lsvExpShareWaitQueue.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvExpShareWaitQueue_DragEnter);
            this.m_lsvExpShareWaitQueue.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvExpShareWaitQueue_ItemDrag);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "姓 名";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "就诊科室";
            this.columnHeader3.Width = 80;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.straightLine5);
            this.gradientPanel1.Controls.Add(this.m_lblQueueExp);
            this.gradientPanel1.Controls.Add(this.straightLine6);
            this.gradientPanel1.Controls.Add(this.straightLine7);
            this.gradientPanel1.Controls.Add(this.straightLine8);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.Flip = false;
            this.gradientPanel1.FloatingImage = null;
            this.gradientPanel1.GradientAngle = 180;
            this.gradientPanel1.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel1.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(215)))), ((int)(((byte)(242)))));
            this.gradientPanel1.HorizontalFillPercent = 100F;
            this.gradientPanel1.imageXOffset = 0;
            this.gradientPanel1.imageYOffset = 0;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(223, 26);
            this.gradientPanel1.TabIndex = 54;
            this.gradientPanel1.VerticalFillPercent = 100F;
            // 
            // straightLine5
            // 
            this.straightLine5.BackColor = System.Drawing.Color.Transparent;
            this.straightLine5.Dock = System.Windows.Forms.DockStyle.Left;
            this.straightLine5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine5.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine5.Location = new System.Drawing.Point(0, 1);
            this.straightLine5.Name = "straightLine5";
            this.straightLine5.Size = new System.Drawing.Size(1, 24);
            this.straightLine5.TabIndex = 3;
            this.straightLine5.Text = "straightLine5";
            // 
            // m_lblQueueExp
            // 
            this.m_lblQueueExp.AutoEllipsis = true;
            this.m_lblQueueExp.BackColor = System.Drawing.Color.Transparent;
            this.m_lblQueueExp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblQueueExp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblQueueExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(70)))), ((int)(((byte)(96)))));
            this.m_lblQueueExp.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimize;
            this.m_lblQueueExp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_lblQueueExp.Location = new System.Drawing.Point(0, 1);
            this.m_lblQueueExp.Name = "m_lblQueueExp";
            this.m_lblQueueExp.Size = new System.Drawing.Size(222, 24);
            this.m_lblQueueExp.TabIndex = 51;
            this.m_lblQueueExp.Text = "预约专家队列";
            this.m_lblQueueExp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_lblQueueExp.Click += new System.EventHandler(this.m_lblQueueExp_Click);
            // 
            // straightLine6
            // 
            this.straightLine6.BackColor = System.Drawing.Color.Transparent;
            this.straightLine6.Dock = System.Windows.Forms.DockStyle.Right;
            this.straightLine6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine6.LineType = Microsoft.Samples.StraightLineTypes.Vertical;
            this.straightLine6.Location = new System.Drawing.Point(222, 1);
            this.straightLine6.Name = "straightLine6";
            this.straightLine6.Size = new System.Drawing.Size(1, 24);
            this.straightLine6.TabIndex = 2;
            this.straightLine6.Text = "straightLine6";
            // 
            // straightLine7
            // 
            this.straightLine7.BackColor = System.Drawing.Color.Transparent;
            this.straightLine7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.straightLine7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine7.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine7.Location = new System.Drawing.Point(0, 25);
            this.straightLine7.Name = "straightLine7";
            this.straightLine7.Size = new System.Drawing.Size(223, 1);
            this.straightLine7.TabIndex = 1;
            this.straightLine7.Text = "straightLine7";
            // 
            // straightLine8
            // 
            this.straightLine8.BackColor = System.Drawing.Color.Transparent;
            this.straightLine8.Dock = System.Windows.Forms.DockStyle.Top;
            this.straightLine8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(128)))), ((int)(((byte)(210)))));
            this.straightLine8.LineType = Microsoft.Samples.StraightLineTypes.Horizontal;
            this.straightLine8.Location = new System.Drawing.Point(0, 0);
            this.straightLine8.Name = "straightLine8";
            this.straightLine8.Size = new System.Drawing.Size(223, 1);
            this.straightLine8.TabIndex = 0;
            this.straightLine8.Text = "straightLine8";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_lsvCalledQueue);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(223, 522);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "叫过队列";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_lsvCalledQueue
            // 
            this.m_lsvCalledQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader4});
            this.m_lsvCalledQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvCalledQueue.FullRowSelect = true;
            this.m_lsvCalledQueue.GridLines = true;
            this.m_lsvCalledQueue.Location = new System.Drawing.Point(3, 3);
            this.m_lsvCalledQueue.MultiSelect = false;
            this.m_lsvCalledQueue.Name = "m_lsvCalledQueue";
            this.m_lsvCalledQueue.Size = new System.Drawing.Size(217, 516);
            this.m_lsvCalledQueue.TabIndex = 32;
            this.m_lsvCalledQueue.UseCompatibleStateImageBehavior = false;
            this.m_lsvCalledQueue.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "姓 名";
            this.columnHeader18.Width = 117;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "就诊科室";
            this.columnHeader4.Width = 94;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnVoiceSet);
            this.groupBox3.Controls.Add(this.m_cmdCurrentScreen);
            this.groupBox3.Controls.Add(this.m_cmdSend);
            this.groupBox3.Controls.Add(this.m_cmdDelete);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 84);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            // 
            // btnVoiceSet
            // 
            this.btnVoiceSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoiceSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnVoiceSet.DefaultScheme = true;
            this.btnVoiceSet.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnVoiceSet.Hint = "";
            this.btnVoiceSet.Location = new System.Drawing.Point(127, 50);
            this.btnVoiceSet.Name = "btnVoiceSet";
            this.btnVoiceSet.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnVoiceSet.Size = new System.Drawing.Size(90, 30);
            this.btnVoiceSet.TabIndex = 57;
            this.btnVoiceSet.Text = "语音设置";
            this.btnVoiceSet.Click += new System.EventHandler(this.btnVoiceSet_Click);
            // 
            // m_cmdCurrentScreen
            // 
            this.m_cmdCurrentScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCurrentScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdCurrentScreen.DefaultScheme = true;
            this.m_cmdCurrentScreen.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdCurrentScreen.Hint = "";
            this.m_cmdCurrentScreen.Location = new System.Drawing.Point(13, 50);
            this.m_cmdCurrentScreen.Name = "m_cmdCurrentScreen";
            this.m_cmdCurrentScreen.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdCurrentScreen.Size = new System.Drawing.Size(75, 30);
            this.m_cmdCurrentScreen.TabIndex = 56;
            this.m_cmdCurrentScreen.Text = "屏幕(F8)";
            this.m_cmdCurrentScreen.Click += new System.EventHandler(this.m_cmdCurrentScreen_Click);
            // 
            // m_cmdSend
            // 
            this.m_cmdSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdSend.DefaultScheme = true;
            this.m_cmdSend.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdSend.Hint = "";
            this.m_cmdSend.Location = new System.Drawing.Point(127, 14);
            this.m_cmdSend.Name = "m_cmdSend";
            this.m_cmdSend.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSend.Size = new System.Drawing.Size(90, 30);
            this.m_cmdSend.TabIndex = 55;
            this.m_cmdSend.Text = "自定义(F2)";
            this.m_cmdSend.Click += new System.EventHandler(this.m_cmdSend_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(13, 14);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(75, 30);
            this.m_cmdDelete.TabIndex = 53;
            this.m_cmdDelete.Text = "删除(F5)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.m_txtCard);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 55);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "患者卡号";
            // 
            // m_txtCard
            // 
            this.m_txtCard.Location = new System.Drawing.Point(67, 22);
            this.m_txtCard.Name = "m_txtCard";
            this.m_txtCard.Size = new System.Drawing.Size(150, 23);
            this.m_txtCard.TabIndex = 0;
            this.m_txtCard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_txtCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtCard_KeyDown);
            this.m_txtCard.Enter += new System.EventHandler(this.m_txtCard_Enter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(-4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "〔F4选中)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctlCboDiagnoseArea);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 57);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "所属诊区";
            // 
            // ctlCboDiagnoseArea
            // 
            this.ctlCboDiagnoseArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlCboDiagnoseArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctlCboDiagnoseArea.FormattingEnabled = true;
            this.ctlCboDiagnoseArea.Location = new System.Drawing.Point(13, 21);
            this.ctlCboDiagnoseArea.m_intAreaID = -2147483648;
            this.ctlCboDiagnoseArea.Name = "ctlCboDiagnoseArea";
            this.ctlCboDiagnoseArea.Size = new System.Drawing.Size(204, 22);
            this.ctlCboDiagnoseArea.TabIndex = 10;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "医生";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "预约时间";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 0;
            // 
            // frmPatientManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 746);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.KeyPreview = true;
            this.Name = "frmPatientManager";
            this.Text = "分诊管理界面";
            this.Load += new System.EventHandler(this.frmPatientManager_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPatientManager_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPatientManager_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gradientPanelCommom.ResumeLayout(false);
            this.gradientPanel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel m_flpMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView m_lsvExpShareWaitQueue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView m_lsvComShareWaitQueue;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView m_lsvCalledQueue;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private com.digitalwave.iCare.gui.MFZ.Controls.ctlDiagnoseAreaCombox ctlCboDiagnoseArea;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label m_lblQueueCommon;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanelCommom;
        private Microsoft.Samples.StraightLine straightLine4;
        private Microsoft.Samples.StraightLine straightLine3;
        private Microsoft.Samples.StraightLine straightLine2;
        private Microsoft.Samples.StraightLine straightLine1;
        private com.digitalwave.iCare.gui.MFZ.Controls.GradientPanel gradientPanel1;
        private Microsoft.Samples.StraightLine straightLine5;
        private System.Windows.Forms.Label m_lblQueueExp;
        private Microsoft.Samples.StraightLine straightLine6;
        private Microsoft.Samples.StraightLine straightLine7;
        private Microsoft.Samples.StraightLine straightLine8;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox m_txtCard;
        private System.Windows.Forms.GroupBox groupBox3;
        private PinkieControls.ButtonXP m_cmdDelete;
        private PinkieControls.ButtonXP m_cmdSend;
        private PinkieControls.ButtonXP m_cmdCurrentScreen;
        private System.Windows.Forms.Label label1;
        private PinkieControls.ButtonXP btnVoiceSet;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;



    }
}