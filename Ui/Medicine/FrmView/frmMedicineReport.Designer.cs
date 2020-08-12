namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmMedicineReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedicineReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dwRpt2 = new Sybase.DataWindow.DataWindowControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.buttonXP4 = new PinkieControls.ButtonXP();
            this.buttonXP5 = new PinkieControls.ButtonXP();
            this.buttonXP6 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNums = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dtgItem = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delCurrentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delAllItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMedList = new System.Windows.Forms.Panel();
            this.dtgMedList = new System.Windows.Forms.DataGridView();
            this.itemcode_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemname_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medicineid_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemid_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medspec_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dwRpt1 = new Sybase.DataWindow.DataWindowControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dwRpt3 = new Sybase.DataWindow.DataWindowControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dtgMedicineList = new System.Windows.Forms.DataGridView();
            this.itemno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colitemcode_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colitemname_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmedspec_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opunit_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitprice_mny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productorid_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorname_vchr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmedicineid_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colitemid_chr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgItem)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelMedList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMedList)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMedicineList)).BeginInit();
            this.SuspendLayout();
            // 
            // dwRpt2
            // 
            this.dwRpt2.DataWindowObject = "";
            this.dwRpt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRpt2.LibraryList = "";
            this.dwRpt2.Location = new System.Drawing.Point(3, 3);
            this.dwRpt2.Name = "dwRpt2";
            this.dwRpt2.Size = new System.Drawing.Size(754, 666);
            this.dwRpt2.TabIndex = 0;
            this.dwRpt2.Text = "dwRpt2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonXP2);
            this.panel1.Controls.Add(this.buttonXP3);
            this.panel1.Controls.Add(this.buttonXP4);
            this.panel1.Controls.Add(this.buttonXP5);
            this.panel1.Controls.Add(this.buttonXP6);
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblNums);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1016, 42);
            this.panel1.TabIndex = 0;
            // 
            // buttonXP2
            // 
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(644, 7);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(94, 28);
            this.buttonXP2.TabIndex = 7;
            this.buttonXP2.Text = "药品目录(&M)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // buttonXP3
            // 
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(738, 7);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(60, 28);
            this.buttonXP3.TabIndex = 7;
            this.buttonXP3.Text = "导出(&E)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click);
            // 
            // buttonXP4
            // 
            this.buttonXP4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP4.DefaultScheme = true;
            this.buttonXP4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP4.Hint = "";
            this.buttonXP4.Location = new System.Drawing.Point(798, 7);
            this.buttonXP4.Name = "buttonXP4";
            this.buttonXP4.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP4.Size = new System.Drawing.Size(60, 28);
            this.buttonXP4.TabIndex = 7;
            this.buttonXP4.Text = "打印(&P)";
            this.buttonXP4.Click += new System.EventHandler(this.buttonXP4_Click);
            // 
            // buttonXP5
            // 
            this.buttonXP5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP5.DefaultScheme = true;
            this.buttonXP5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP5.Hint = "";
            this.buttonXP5.Location = new System.Drawing.Point(858, 7);
            this.buttonXP5.Name = "buttonXP5";
            this.buttonXP5.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP5.Size = new System.Drawing.Size(91, 28);
            this.buttonXP5.TabIndex = 7;
            this.buttonXP5.Text = "门诊医生(&D)";
            this.buttonXP5.Click += new System.EventHandler(this.buttonXP5_Click);
            // 
            // buttonXP6
            // 
            this.buttonXP6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP6.DefaultScheme = true;
            this.buttonXP6.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP6.Hint = "";
            this.buttonXP6.Location = new System.Drawing.Point(949, 7);
            this.buttonXP6.Name = "buttonXP6";
            this.buttonXP6.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP6.Size = new System.Drawing.Size(60, 28);
            this.buttonXP6.TabIndex = 7;
            this.buttonXP6.Text = "关闭(&C)";
            this.buttonXP6.Click += new System.EventHandler(this.buttonXP6_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(545, 7);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(60, 28);
            this.buttonXP1.TabIndex = 7;
            this.buttonXP1.Text = "检索(&S)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "到";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(420, 10);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(123, 23);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(276, 10);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(123, 23);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "统计日期：从";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 18);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblNums
            // 
            this.lblNums.AutoSize = true;
            this.lblNums.ForeColor = System.Drawing.Color.Blue;
            this.lblNums.Location = new System.Drawing.Point(138, 14);
            this.lblNums.Name = "lblNums";
            this.lblNums.Size = new System.Drawing.Size(0, 14);
            this.lblNums.TabIndex = 1;
            this.lblNums.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入查询条件";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 699);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panelMedList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 672);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dtgItem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 220);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(244, 452);
            this.panel5.TabIndex = 3;
            // 
            // dtgItem
            // 
            this.dtgItem.AllowUserToAddRows = false;
            this.dtgItem.AllowUserToDeleteRows = false;
            this.dtgItem.AllowUserToResizeRows = false;
            this.dtgItem.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dtgItem.ColumnHeadersHeight = 25;
            this.dtgItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn1});
            this.dtgItem.ContextMenuStrip = this.contextMenuStrip1;
            this.dtgItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgItem.Location = new System.Drawing.Point(0, 0);
            this.dtgItem.Name = "dtgItem";
            this.dtgItem.ReadOnly = true;
            this.dtgItem.RowHeadersVisible = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            this.dtgItem.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgItem.RowTemplate.Height = 23;
            this.dtgItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgItem.Size = new System.Drawing.Size(244, 452);
            this.dtgItem.TabIndex = 1;
            this.dtgItem.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgItem_CellContentDoubleClick);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "itemcode_vchr";
            this.dataGridViewTextBoxColumn3.HeaderText = "代码";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "itemname_vchr";
            this.dataGridViewTextBoxColumn4.HeaderText = "药品名称";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 160;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "medicineid_chr";
            this.dataGridViewTextBoxColumn2.HeaderText = "medicineid_chr";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "itemid_chr";
            this.dataGridViewTextBoxColumn1.HeaderText = "itemid_chr";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delCurrentItem,
            this.delAllItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 48);
            // 
            // delCurrentItem
            // 
            this.delCurrentItem.Image = ((System.Drawing.Image)(resources.GetObject("delCurrentItem.Image")));
            this.delCurrentItem.Name = "delCurrentItem";
            this.delCurrentItem.Size = new System.Drawing.Size(161, 22);
            this.delCurrentItem.Text = "删除当前药品(&D)";
            this.delCurrentItem.Click += new System.EventHandler(this.delCurrentItem_Click);
            // 
            // delAllItem
            // 
            this.delAllItem.Image = ((System.Drawing.Image)(resources.GetObject("delAllItem.Image")));
            this.delAllItem.Name = "delAllItem";
            this.delAllItem.Size = new System.Drawing.Size(161, 22);
            this.delAllItem.Text = "删除所有药品(&A)";
            this.delAllItem.Click += new System.EventHandler(this.delAllItem_Click);
            // 
            // panelMedList
            // 
            this.panelMedList.Controls.Add(this.dtgMedList);
            this.panelMedList.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMedList.Location = new System.Drawing.Point(0, 0);
            this.panelMedList.Name = "panelMedList";
            this.panelMedList.Size = new System.Drawing.Size(244, 220);
            this.panelMedList.TabIndex = 2;
            // 
            // dtgMedList
            // 
            this.dtgMedList.AllowUserToAddRows = false;
            this.dtgMedList.AllowUserToDeleteRows = false;
            this.dtgMedList.AllowUserToResizeRows = false;
            this.dtgMedList.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dtgMedList.ColumnHeadersHeight = 25;
            this.dtgMedList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemcode_vchr,
            this.itemname_vchr,
            this.medicineid_chr,
            this.itemid_chr,
            this.medspec_vchr});
            this.dtgMedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMedList.Location = new System.Drawing.Point(0, 0);
            this.dtgMedList.Name = "dtgMedList";
            this.dtgMedList.ReadOnly = true;
            this.dtgMedList.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.dtgMedList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgMedList.RowTemplate.Height = 23;
            this.dtgMedList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgMedList.Size = new System.Drawing.Size(244, 220);
            this.dtgMedList.TabIndex = 0;
            this.dtgMedList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgMedList_KeyDown);
            this.dtgMedList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgMedList_CellContentDoubleClick);
            this.dtgMedList.Leave += new System.EventHandler(this.dtgMedList_Leave);
            // 
            // itemcode_vchr
            // 
            this.itemcode_vchr.DataPropertyName = "itemcode_vchr";
            this.itemcode_vchr.HeaderText = "代码";
            this.itemcode_vchr.Name = "itemcode_vchr";
            this.itemcode_vchr.ReadOnly = true;
            this.itemcode_vchr.Width = 80;
            // 
            // itemname_vchr
            // 
            this.itemname_vchr.DataPropertyName = "itemname_vchr";
            this.itemname_vchr.HeaderText = "药品名称";
            this.itemname_vchr.Name = "itemname_vchr";
            this.itemname_vchr.ReadOnly = true;
            this.itemname_vchr.Width = 160;
            // 
            // medicineid_chr
            // 
            this.medicineid_chr.DataPropertyName = "medicineid_chr";
            this.medicineid_chr.HeaderText = "medicineid_chr";
            this.medicineid_chr.Name = "medicineid_chr";
            this.medicineid_chr.ReadOnly = true;
            this.medicineid_chr.Visible = false;
            // 
            // itemid_chr
            // 
            this.itemid_chr.DataPropertyName = "itemid_chr";
            this.itemid_chr.HeaderText = "itemid_chr";
            this.itemid_chr.Name = "itemid_chr";
            this.itemid_chr.ReadOnly = true;
            this.itemid_chr.Visible = false;
            this.itemid_chr.Width = 80;
            // 
            // medspec_vchr
            // 
            this.medspec_vchr.DataPropertyName = "medspec_vchr";
            this.medspec_vchr.HeaderText = "药品规格";
            this.medspec_vchr.Name = "medspec_vchr";
            this.medspec_vchr.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtFind);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 27);
            this.panel2.TabIndex = 1;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(2, 2);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(240, 23);
            this.txtFind.TabIndex = 0;
            this.txtFind.Enter += new System.EventHandler(this.txtFind_Enter);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 699);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dwRpt1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 672);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "医生用药信息";
            this.tabPage1.ToolTipText = "医生用药信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dwRpt1
            // 
            this.dwRpt1.DataWindowObject = "";
            this.dwRpt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRpt1.LibraryList = "";
            this.dwRpt1.Location = new System.Drawing.Point(3, 3);
            this.dwRpt1.Name = "dwRpt1";
            this.dwRpt1.Size = new System.Drawing.Size(754, 666);
            this.dwRpt1.TabIndex = 0;
            this.dwRpt1.Text = "dwRpt1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dwRpt2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 672);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "药品库存信息";
            this.tabPage2.ToolTipText = "药品库存信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dwRpt3);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(760, 672);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "出入库明细信息";
            this.tabPage3.ToolTipText = "出入库明细信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dwRpt3
            // 
            this.dwRpt3.DataWindowObject = "";
            this.dwRpt3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwRpt3.LibraryList = "";
            this.dwRpt3.Location = new System.Drawing.Point(0, 0);
            this.dwRpt3.Name = "dwRpt3";
            this.dwRpt3.Size = new System.Drawing.Size(760, 672);
            this.dwRpt3.TabIndex = 0;
            this.dwRpt3.Text = "dwRpt3";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dtgMedicineList);
            this.tabPage4.ImageIndex = 3;
            this.tabPage4.Location = new System.Drawing.Point(4, 23);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(760, 672);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "药品目录";
            this.tabPage4.ToolTipText = "药品目录";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dtgMedicineList
            // 
            this.dtgMedicineList.AllowUserToAddRows = false;
            this.dtgMedicineList.AllowUserToDeleteRows = false;
            this.dtgMedicineList.AllowUserToResizeRows = false;
            this.dtgMedicineList.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dtgMedicineList.ColumnHeadersHeight = 25;
            this.dtgMedicineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemno,
            this.colitemcode_vchr,
            this.colitemname_vchr,
            this.colmedspec_vchr,
            this.opunit_chr,
            this.unitprice_mny,
            this.productorid_chr,
            this.vendorname_vchr,
            this.colmedicineid_chr,
            this.colitemid_chr});
            this.dtgMedicineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMedicineList.Location = new System.Drawing.Point(0, 0);
            this.dtgMedicineList.Name = "dtgMedicineList";
            this.dtgMedicineList.ReadOnly = true;
            this.dtgMedicineList.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Orange;
            this.dtgMedicineList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgMedicineList.RowTemplate.Height = 26;
            this.dtgMedicineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgMedicineList.Size = new System.Drawing.Size(760, 672);
            this.dtgMedicineList.TabIndex = 1;
            this.dtgMedicineList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgMedicineList_CellDoubleClick);
            // 
            // itemno
            // 
            this.itemno.DataPropertyName = "itemno";
            this.itemno.HeaderText = "";
            this.itemno.Name = "itemno";
            this.itemno.ReadOnly = true;
            this.itemno.Width = 40;
            // 
            // colitemcode_vchr
            // 
            this.colitemcode_vchr.DataPropertyName = "itemcode_vchr";
            this.colitemcode_vchr.HeaderText = "药品代码";
            this.colitemcode_vchr.Name = "colitemcode_vchr";
            this.colitemcode_vchr.ReadOnly = true;
            this.colitemcode_vchr.Width = 90;
            // 
            // colitemname_vchr
            // 
            this.colitemname_vchr.DataPropertyName = "itemname_vchr";
            this.colitemname_vchr.HeaderText = "药品名称";
            this.colitemname_vchr.Name = "colitemname_vchr";
            this.colitemname_vchr.ReadOnly = true;
            this.colitemname_vchr.Width = 160;
            // 
            // colmedspec_vchr
            // 
            this.colmedspec_vchr.DataPropertyName = "medspec_vchr";
            this.colmedspec_vchr.HeaderText = "规格";
            this.colmedspec_vchr.Name = "colmedspec_vchr";
            this.colmedspec_vchr.ReadOnly = true;
            // 
            // opunit_chr
            // 
            this.opunit_chr.DataPropertyName = "opunit_chr";
            this.opunit_chr.HeaderText = "单位";
            this.opunit_chr.Name = "opunit_chr";
            this.opunit_chr.ReadOnly = true;
            this.opunit_chr.Width = 70;
            // 
            // unitprice_mny
            // 
            this.unitprice_mny.DataPropertyName = "unitprice_mny";
            this.unitprice_mny.HeaderText = "零售价";
            this.unitprice_mny.Name = "unitprice_mny";
            this.unitprice_mny.ReadOnly = true;
            this.unitprice_mny.Width = 80;
            // 
            // productorid_chr
            // 
            this.productorid_chr.DataPropertyName = "productorid_chr";
            this.productorid_chr.HeaderText = "生产厂家";
            this.productorid_chr.Name = "productorid_chr";
            this.productorid_chr.ReadOnly = true;
            // 
            // vendorname_vchr
            // 
            this.vendorname_vchr.DataPropertyName = "vendorname_vchr";
            this.vendorname_vchr.HeaderText = "供应商";
            this.vendorname_vchr.Name = "vendorname_vchr";
            this.vendorname_vchr.ReadOnly = true;
            // 
            // colmedicineid_chr
            // 
            this.colmedicineid_chr.DataPropertyName = "medicineid_chr";
            this.colmedicineid_chr.HeaderText = "medicineid_chr";
            this.colmedicineid_chr.Name = "colmedicineid_chr";
            this.colmedicineid_chr.ReadOnly = true;
            this.colmedicineid_chr.Visible = false;
            // 
            // colitemid_chr
            // 
            this.colitemid_chr.DataPropertyName = "itemid_chr";
            this.colitemid_chr.HeaderText = "itemid_chr";
            this.colitemid_chr.Name = "colitemid_chr";
            this.colitemid_chr.ReadOnly = true;
            this.colitemid_chr.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ActualSizeHS.png");
            this.imageList1.Images.SetKeyName(1, "book_addressHS.png");
            this.imageList1.Images.SetKeyName(2, "Book_openHS.png");
            this.imageList1.Images.SetKeyName(3, "FindHS.png");
            this.imageList1.Images.SetKeyName(4, "DeleteHS.png");
            this.imageList1.Images.SetKeyName(5, "DeleteFolderHS.png");
            // 
            // frmMedicineReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMedicineReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询系统";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMedicineReport_KeyDown);
            this.Load += new System.EventHandler(this.frmMedicineReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgItem)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelMedList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMedList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMedicineList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNums;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private PinkieControls.ButtonXP buttonXP1;
        private PinkieControls.ButtonXP buttonXP2;
        private PinkieControls.ButtonXP buttonXP3;
        private PinkieControls.ButtonXP buttonXP4;
        private PinkieControls.ButtonXP buttonXP5;
        private PinkieControls.ButtonXP buttonXP6;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dtgMedList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.DataGridView dtgItem;
        private Sybase.DataWindow.DataWindowControl dwRpt1;
        private Sybase.DataWindow.DataWindowControl dwRpt3;
        private System.Windows.Forms.DataGridView dtgMedicineList;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemno;
        private System.Windows.Forms.DataGridViewTextBoxColumn colitemcode_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colitemname_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmedspec_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn opunit_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitprice_mny;
        private System.Windows.Forms.DataGridViewTextBoxColumn productorid_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorname_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmedicineid_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colitemid_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemcode_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemname_vchr;
        private System.Windows.Forms.DataGridViewTextBoxColumn medicineid_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemid_chr;
        private System.Windows.Forms.DataGridViewTextBoxColumn medspec_vchr;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delCurrentItem;
        private System.Windows.Forms.ToolStripMenuItem delAllItem;
        private Sybase.DataWindow.DataWindowControl dwRpt2;
        private System.Windows.Forms.Panel panelMedList;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ImageList imageList1;
    }
}