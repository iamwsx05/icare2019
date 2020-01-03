namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmInNotice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInNotice));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbiNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbiSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbiDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbiPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbiClose = new System.Windows.Forms.ToolStripButton();
            this.dwNotice = new Sybase.DataWindow.DataWindowControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbiNew,
            this.toolStripSeparator1,
            this.tsbiSave,
            this.toolStripSeparator2,
            this.tsbiDel,
            this.toolStripSeparator3,
            this.tsbiPrint,
            this.toolStripSeparator5,
            this.tsbiClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(700, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbiNew
            // 
            this.tsbiNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbiNew.Image")));
            this.tsbiNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbiNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiNew.Name = "tsbiNew";
            this.tsbiNew.Size = new System.Drawing.Size(68, 36);
            this.tsbiNew.Text = "新建";
            this.tsbiNew.Click += new System.EventHandler(this.tsbiNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbiSave
            // 
            this.tsbiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbiSave.Image")));
            this.tsbiSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbiSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiSave.Name = "tsbiSave";
            this.tsbiSave.Size = new System.Drawing.Size(68, 36);
            this.tsbiSave.Text = "保存";
            this.tsbiSave.Click += new System.EventHandler(this.tsbiSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbiDel
            // 
            this.tsbiDel.Image = ((System.Drawing.Image)(resources.GetObject("tsbiDel.Image")));
            this.tsbiDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbiDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiDel.Name = "tsbiDel";
            this.tsbiDel.Size = new System.Drawing.Size(68, 36);
            this.tsbiDel.Text = "删除";
            this.tsbiDel.Click += new System.EventHandler(this.tsbiDel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbiPrint
            // 
            this.tsbiPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbiPrint.Image")));
            this.tsbiPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbiPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiPrint.Name = "tsbiPrint";
            this.tsbiPrint.Size = new System.Drawing.Size(68, 36);
            this.tsbiPrint.Text = "打印";
            this.tsbiPrint.Click += new System.EventHandler(this.tsbiPrint_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbiClose
            // 
            this.tsbiClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbiClose.Image")));
            this.tsbiClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbiClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbiClose.Name = "tsbiClose";
            this.tsbiClose.Size = new System.Drawing.Size(68, 36);
            this.tsbiClose.Text = "关闭";
            this.tsbiClose.Click += new System.EventHandler(this.tsbiClose_Click);
            // 
            // dwNotice
            // 
            this.dwNotice.DataWindowObject = "";
            this.dwNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dwNotice.LibraryList = "";
            this.dwNotice.Location = new System.Drawing.Point(0, 39);
            this.dwNotice.Name = "dwNotice";
            this.dwNotice.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Vertical;
            this.dwNotice.Size = new System.Drawing.Size(700, 771);
            this.dwNotice.TabIndex = 2;
            this.dwNotice.Text = "dataWindowControl1";
            this.dwNotice.DataWindowKeyDown += new Sybase.DataWindow.DataWindowKeyDownEventHandler(this.dataWindowControl_DataWindowKeyDown);
            this.dwNotice.EditChanged += new Sybase.DataWindow.EditChangedEventHandler(this.dataWindowControl_EditChanged);
            // 
            // frmInNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 810);
            this.Controls.Add(this.dwNotice);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.Name = "frmInNotice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "入院通知书";
            this.Load += new System.EventHandler(this.frmInNotice_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbiNew;
        private System.Windows.Forms.ToolStripButton tsbiSave;
        private System.Windows.Forms.ToolStripButton tsbiDel;
        private System.Windows.Forms.ToolStripButton tsbiPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbiClose;
        private Sybase.DataWindow.DataWindowControl dwNotice;
    }
}