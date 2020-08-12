namespace Common.Controls.Emr
{
    partial class ctlTableCase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlTableCase));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsiNewPage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsiAppendRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsiInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTooBar = new System.Windows.Forms.Panel();
            this.txtPageNo = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNext = new Common.Controls.ctlPicbutton();
            this.lblSplit = new System.Windows.Forms.Label();
            this.txtTotalPage = new System.Windows.Forms.Label();
            this.txtCurrentPage = new System.Windows.Forms.Label();
            this.btnLast = new Common.Controls.ctlPicbutton();
            this.btnPrevious = new Common.Controls.ctlPicbutton();
            this.btnFirst = new Common.Controls.ctlPicbutton();
            this.btnDel = new Common.Controls.ctlPicbutton();
            this.btnAdd = new Common.Controls.ctlPicbutton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripSum = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTwoRowsSum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSum = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.pnlTooBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.contextMenuStripSum.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(375, 209);
            this.pnlMain.TabIndex = 3;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiNewPage,
            this.toolStripSeparator1,
            this.tsiAppendRow,
            this.toolStripMenuItem2,
            this.tsiInsertRow});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(117, 82);
            // 
            // tsiNewPage
            // 
            this.tsiNewPage.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tsiNewPage.Image = global::Common.Controls.Properties.Resources.New_16x16;
            this.tsiNewPage.Name = "tsiNewPage";
            this.tsiNewPage.Size = new System.Drawing.Size(116, 22);
            this.tsiNewPage.Text = "新  页";
            this.tsiNewPage.Click += new System.EventHandler(this.tsiNewPage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // tsiAppendRow
            // 
            this.tsiAppendRow.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsiAppendRow.Image = global::Common.Controls.Properties.Resources.AddGroupFooter_16x16;
            this.tsiAppendRow.Name = "tsiAppendRow";
            this.tsiAppendRow.Size = new System.Drawing.Size(116, 22);
            this.tsiAppendRow.Text = "添加行";
            this.tsiAppendRow.Click += new System.EventHandler(this.tsiAppendRow_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 6);
            // 
            // tsiInsertRow
            // 
            this.tsiInsertRow.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsiInsertRow.Image = global::Common.Controls.Properties.Resources.EmptyTableRowSeparator_16x16;
            this.tsiInsertRow.Name = "tsiInsertRow";
            this.tsiInsertRow.Size = new System.Drawing.Size(116, 22);
            this.tsiInsertRow.Text = "插入行";
            this.tsiInsertRow.Click += new System.EventHandler(this.tsiInsertRow_Click);
            // 
            // pnlTooBar
            // 
            this.pnlTooBar.BackColor = System.Drawing.Color.White;
            this.pnlTooBar.Controls.Add(this.txtPageNo);
            this.pnlTooBar.Controls.Add(this.label2);
            this.pnlTooBar.Controls.Add(this.label1);
            this.pnlTooBar.Controls.Add(this.btnNext);
            this.pnlTooBar.Controls.Add(this.lblSplit);
            this.pnlTooBar.Controls.Add(this.txtTotalPage);
            this.pnlTooBar.Controls.Add(this.txtCurrentPage);
            this.pnlTooBar.Controls.Add(this.btnLast);
            this.pnlTooBar.Controls.Add(this.btnPrevious);
            this.pnlTooBar.Controls.Add(this.btnFirst);
            this.pnlTooBar.Controls.Add(this.btnDel);
            this.pnlTooBar.Controls.Add(this.btnAdd);
            this.pnlTooBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTooBar.Location = new System.Drawing.Point(0, 209);
            this.pnlTooBar.Name = "pnlTooBar";
            this.pnlTooBar.Size = new System.Drawing.Size(375, 31);
            this.pnlTooBar.TabIndex = 4;
            this.pnlTooBar.MouseEnter += new System.EventHandler(this.pnlTooBar_MouseEnter);
            // 
            // txtPageNo
            // 
            this.txtPageNo.EditValue = "";
            this.txtPageNo.Location = new System.Drawing.Point(283, 4);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageNo.Properties.Appearance.Options.UseFont = true;
            this.txtPageNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPageNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPageNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPageNo.Properties.Mask.EditMask = "d";
            this.txtPageNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPageNo.Properties.MaxLength = 5;
            this.txtPageNo.Size = new System.Drawing.Size(40, 22);
            this.txtPageNo.TabIndex = 18;
            this.toolTip.SetToolTip(this.txtPageNo, "请输入跳转的页号");
            this.txtPageNo.Enter += new System.EventHandler(this.txtPageNo_Enter);
            this.txtPageNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPageNo_KeyDown);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(323, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "页";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(248, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "转到";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.DefaultImg = null;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.IsResetImg = false;
            this.btnNext.IsShowBorder = false;
            this.btnNext.Location = new System.Drawing.Point(195, 3);
            this.btnNext.LostFocusImg = null;
            this.btnNext.Name = "btnNext";
            this.btnNext.OnFocusImg = null;
            this.btnNext.Size = new System.Drawing.Size(20, 23);
            this.btnNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnNext.TabIndex = 10;
            this.btnNext.TabStop = false;
            this.toolTip.SetToolTip(this.btnNext, "下一页");
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblSplit
            // 
            this.lblSplit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSplit.Location = new System.Drawing.Point(52, 6);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(2, 17);
            this.lblSplit.TabIndex = 14;
            // 
            // txtTotalPage
            // 
            this.txtTotalPage.Font = new System.Drawing.Font("宋体", 10F);
            this.txtTotalPage.Location = new System.Drawing.Point(129, 3);
            this.txtTotalPage.Name = "txtTotalPage";
            this.txtTotalPage.Size = new System.Drawing.Size(70, 23);
            this.txtTotalPage.TabIndex = 13;
            this.txtTotalPage.Text = "/ {0} 页";
            this.txtTotalPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.txtTotalPage, "总页数");
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Font = new System.Drawing.Font("宋体", 10F);
            this.txtCurrentPage.Location = new System.Drawing.Point(99, 3);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(30, 23);
            this.txtCurrentPage.TabIndex = 12;
            this.txtCurrentPage.Text = "0";
            this.txtCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.txtCurrentPage, "当前页号");
            // 
            // btnLast
            // 
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.DefaultImg = null;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.IsResetImg = false;
            this.btnLast.IsShowBorder = false;
            this.btnLast.Location = new System.Drawing.Point(216, 3);
            this.btnLast.LostFocusImg = null;
            this.btnLast.Name = "btnLast";
            this.btnLast.OnFocusImg = null;
            this.btnLast.Size = new System.Drawing.Size(20, 23);
            this.btnLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnLast.TabIndex = 11;
            this.btnLast.TabStop = false;
            this.toolTip.SetToolTip(this.btnLast, "末页");
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.DefaultImg = null;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.IsResetImg = false;
            this.btnPrevious.IsShowBorder = false;
            this.btnPrevious.Location = new System.Drawing.Point(79, 3);
            this.btnPrevious.LostFocusImg = null;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.OnFocusImg = null;
            this.btnPrevious.Size = new System.Drawing.Size(20, 23);
            this.btnPrevious.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPrevious.TabIndex = 9;
            this.btnPrevious.TabStop = false;
            this.toolTip.SetToolTip(this.btnPrevious, "前一页");
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.DefaultImg = null;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.IsResetImg = false;
            this.btnFirst.IsShowBorder = false;
            this.btnFirst.Location = new System.Drawing.Point(58, 3);
            this.btnFirst.LostFocusImg = null;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.OnFocusImg = null;
            this.btnFirst.Size = new System.Drawing.Size(20, 23);
            this.btnFirst.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnFirst.TabIndex = 8;
            this.btnFirst.TabStop = false;
            this.toolTip.SetToolTip(this.btnFirst, "首页");
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnDel
            // 
            this.btnDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDel.DefaultImg = null;
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.IsResetImg = false;
            this.btnDel.IsShowBorder = false;
            this.btnDel.Location = new System.Drawing.Point(27, 3);
            this.btnDel.LostFocusImg = null;
            this.btnDel.Name = "btnDel";
            this.btnDel.OnFocusImg = null;
            this.btnDel.Size = new System.Drawing.Size(20, 23);
            this.btnDel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnDel.TabIndex = 7;
            this.btnDel.TabStop = false;
            this.toolTip.SetToolTip(this.btnDel, "删除当前行");
            this.btnDel.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.DefaultImg = null;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.IsResetImg = false;
            this.btnAdd.IsShowBorder = false;
            this.btnAdd.Location = new System.Drawing.Point(6, 3);
            this.btnAdd.LostFocusImg = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OnFocusImg = null;
            this.btnAdd.Size = new System.Drawing.Size(20, 23);
            this.btnAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnAdd.TabIndex = 6;
            this.btnAdd.TabStop = false;
            this.toolTip.SetToolTip(this.btnAdd, "新行");
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // contextMenuStripSum
            // 
            this.contextMenuStripSum.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTwoRowsSum,
            this.toolStripSeparator2,
            this.tsmiSum});
            this.contextMenuStripSum.Name = "contextMenuStrip";
            this.contextMenuStripSum.Size = new System.Drawing.Size(168, 54);
            this.contextMenuStripSum.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripSum_Opening);
            // 
            // tsmiTwoRowsSum
            // 
            this.tsmiTwoRowsSum.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tsmiTwoRowsSum.Image = global::Common.Controls.Properties.Resources.Pivot_16x16;
            this.tsmiTwoRowsSum.Name = "tsmiTwoRowsSum";
            this.tsmiTwoRowsSum.Size = new System.Drawing.Size(167, 22);
            this.tsmiTwoRowsSum.Text = "上下行累计求和";
            this.tsmiTwoRowsSum.Click += new System.EventHandler(this.tsmiTwoRowsSum_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // tsmiSum
            // 
            this.tsmiSum.Font = new System.Drawing.Font("宋体", 9.5F);
            this.tsmiSum.Image = global::Common.Controls.Properties.Resources.Summary_16x16;
            this.tsmiSum.Name = "tsmiSum";
            this.tsmiSum.Size = new System.Drawing.Size(167, 22);
            this.tsmiSum.Text = "自定义累计求和";
            this.tsmiSum.Click += new System.EventHandler(this.tsmiSum_Click);
            // 
            // ctlTableCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTooBar);
            this.Name = "ctlTableCase";
            this.Size = new System.Drawing.Size(375, 240);
            this.Load += new System.EventHandler(this.ctlTableCase_Load);
            this.Leave += new System.EventHandler(this.ctlTableCase_Leave);
            this.contextMenuStrip.ResumeLayout(false);
            this.pnlTooBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPageNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrevious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            this.contextMenuStripSum.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsiAppendRow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsiInsertRow;
        private System.Windows.Forms.Panel pnlTooBar;
        private System.Windows.Forms.ToolTip toolTip;
        private ctlPicbutton btnAdd;
        private ctlPicbutton btnLast;
        private ctlPicbutton btnNext;
        private ctlPicbutton btnPrevious;
        private ctlPicbutton btnFirst;
        private ctlPicbutton btnDel;
        private System.Windows.Forms.Label txtCurrentPage;
        private System.Windows.Forms.Label txtTotalPage;
        private System.Windows.Forms.Label lblSplit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtPageNo;
        private System.Windows.Forms.ToolStripMenuItem tsiNewPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSum;
        private System.Windows.Forms.ToolStripMenuItem tsmiSum;
        private System.Windows.Forms.ToolStripMenuItem tsmiTwoRowsSum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
