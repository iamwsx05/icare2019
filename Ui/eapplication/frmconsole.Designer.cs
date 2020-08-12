namespace weCare.eApp
{
    partial class frmConsole
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsole));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.blbiReport = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiNew = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiCommit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiDel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiExport = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiPrint = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiExit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanelLeft = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tvApp = new DevExpress.XtraTreeList.TreeList();
            this.imageListCase = new System.Windows.Forms.ImageList(this.components);
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.xscResult = new DevExpress.XtraEditors.XtraScrollableControl();
            this.splitterControl = new DevExpress.XtraEditors.SplitterControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFind = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanelLeft.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcBackGround.Location = new System.Drawing.Point(226, 640);
            this.pcBackGround.Size = new System.Drawing.Size(748, 46);
            this.pcBackGround.Visible = false;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // barManager
            // 
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.DockManager = this.dockManager;
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.blbiNew,
            this.blbiSave,
            this.blbiDel,
            this.blbiExport,
            this.blbiPrint,
            this.blbiExit,
            this.blbiCommit,
            this.blbiReport});
            this.barManager.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarAppearance.Disabled.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar1.BarAppearance.Disabled.Options.UseFont = true;
            this.bar1.BarAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.bar1.BarAppearance.Hovered.Options.UseFont = true;
            this.bar1.BarAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.bar1.BarAppearance.Normal.Options.UseFont = true;
            this.bar1.BarAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.bar1.BarAppearance.Pressed.Options.UseFont = true;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiReport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiNew, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiSave, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiCommit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiDel, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiExit, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // blbiReport
            // 
            this.blbiReport.Caption = "报告结果";
            this.blbiReport.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiReport.Glyph")));
            this.blbiReport.Id = 7;
            this.blbiReport.Name = "blbiReport";
            this.blbiReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiReport_ItemClick);
            // 
            // blbiNew
            // 
            this.blbiNew.Caption = " 新建 ";
            this.blbiNew.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiNew.Glyph")));
            this.blbiNew.Id = 0;
            this.blbiNew.Name = "blbiNew";
            this.blbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiNew_ItemClick);
            // 
            // blbiSave
            // 
            this.blbiSave.Caption = " 保存 ";
            this.blbiSave.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiSave.Glyph")));
            this.blbiSave.Id = 1;
            this.blbiSave.Name = "blbiSave";
            this.blbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiSave_ItemClick);
            // 
            // blbiCommit
            // 
            this.blbiCommit.Caption = " 提交 ";
            this.blbiCommit.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiCommit.Glyph")));
            this.blbiCommit.Id = 6;
            this.blbiCommit.Name = "blbiCommit";
            this.blbiCommit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiCommit_ItemClick);
            // 
            // blbiDel
            // 
            this.blbiDel.Caption = " 删除 ";
            this.blbiDel.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiDel.Glyph")));
            this.blbiDel.Id = 2;
            this.blbiDel.Name = "blbiDel";
            this.blbiDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiDel_ItemClick);
            // 
            // blbiExport
            // 
            this.blbiExport.Caption = " 导出 ";
            this.blbiExport.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiExport.Glyph")));
            this.blbiExport.Id = 3;
            this.blbiExport.Name = "blbiExport";
            this.blbiExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiExport_ItemClick);
            // 
            // blbiPrint
            // 
            this.blbiPrint.Caption = " 打印 ";
            this.blbiPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiPrint.Glyph")));
            this.blbiPrint.Id = 4;
            this.blbiPrint.Name = "blbiPrint";
            this.blbiPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiPrint_ItemClick);
            // 
            // blbiExit
            // 
            this.blbiExit.Caption = " 关闭 ";
            this.blbiExit.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiExit.Glyph")));
            this.blbiExit.Id = 5;
            this.blbiExit.Name = "blbiExit";
            this.blbiExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(974, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 686);
            this.barDockControlBottom.Size = new System.Drawing.Size(974, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 626);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(974, 60);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 626);
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManager;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelLeft});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanelLeft
            // 
            this.dockPanelLeft.Controls.Add(this.dockPanel1_Container);
            this.dockPanelLeft.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanelLeft.ID = new System.Guid("4dcdef73-c99a-4924-b772-152c117d68d1");
            this.dockPanelLeft.Location = new System.Drawing.Point(0, 60);
            this.dockPanelLeft.Name = "dockPanelLeft";
            this.dockPanelLeft.Options.ShowCloseButton = false;
            this.dockPanelLeft.OriginalSize = new System.Drawing.Size(226, 200);
            this.dockPanelLeft.Size = new System.Drawing.Size(226, 626);
            this.dockPanelLeft.Text = "电子申请单";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.tvApp);
            this.dockPanel1_Container.Controls.Add(this.panelControl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(218, 597);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // tvApp
            // 
            this.tvApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvApp.Location = new System.Drawing.Point(0, 36);
            this.tvApp.Margin = new System.Windows.Forms.Padding(0);
            this.tvApp.Name = "tvApp";
            this.tvApp.OptionsBehavior.AllowExpandOnDblClick = false;
            this.tvApp.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.tvApp.OptionsSelection.UseIndicatorForSelection = true;
            this.tvApp.OptionsView.ShowColumns = false;
            this.tvApp.OptionsView.ShowHorzLines = false;
            this.tvApp.OptionsView.ShowIndicator = false;
            this.tvApp.OptionsView.ShowVertLines = false;
            this.tvApp.RowHeight = 22;
            this.tvApp.SelectImageList = this.imageListCase;
            this.tvApp.Size = new System.Drawing.Size(218, 561);
            this.tvApp.TabIndex = 28;
            // 
            // imageListCase
            // 
            this.imageListCase.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCase.ImageStream")));
            this.imageListCase.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCase.Images.SetKeyName(0, "r1.png");
            this.imageListCase.Images.SetKeyName(1, "r2.png");
            this.imageListCase.Images.SetKeyName(2, "r3.png");
            this.imageListCase.Images.SetKeyName(3, "orange.png");
            this.imageListCase.Images.SetKeyName(4, "green.png");
            // 
            // panelControl
            // 
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(218, 36);
            this.panelControl.TabIndex = 0;
            this.panelControl.Visible = false;
            // 
            // xscResult
            // 
            this.xscResult.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xscResult.Appearance.Options.UseBackColor = true;
            this.xscResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscResult.Location = new System.Drawing.Point(226, 88);
            this.xscResult.Name = "xscResult";
            this.xscResult.Size = new System.Drawing.Size(748, 548);
            this.xscResult.TabIndex = 8;
            // 
            // splitterControl
            // 
            this.splitterControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl.Location = new System.Drawing.Point(226, 636);
            this.splitterControl.MaximumSize = new System.Drawing.Size(0, 4);
            this.splitterControl.Name = "splitterControl";
            this.splitterControl.Size = new System.Drawing.Size(748, 4);
            this.splitterControl.TabIndex = 9;
            this.splitterControl.TabStop = false;
            this.splitterControl.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(8, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "加收费用：";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtFind);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Controls.Add(this.lblFind);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(226, 60);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(748, 28);
            this.panelControl1.TabIndex = 15;
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(111, 3);
            this.txtFind.MenuManager = this.barManager;
            this.txtFind.Name = "txtFind";
            this.txtFind.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFind.Properties.Appearance.Options.UseFont = true;
            this.txtFind.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Size = new System.Drawing.Size(369, 22);
            this.txtFind.TabIndex = 3;
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblFind
            // 
            this.lblFind.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFind.Location = new System.Drawing.Point(27, 8);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(84, 12);
            this.lblFind.TabIndex = 0;
            this.lblFind.Text = "查找诊疗项目：";
            // 
            // frmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(974, 686);
            this.Controls.Add(this.xscResult);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl);
            this.Controls.Add(this.dockPanelLeft);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmConsole";
            this.Text = "电子申请单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConsole_FormClosing);
            this.Load += new System.EventHandler(this.frmConsole_Load);
            this.Controls.SetChildIndex(this.barDockControlTop, 0);
            this.Controls.SetChildIndex(this.barDockControlBottom, 0);
            this.Controls.SetChildIndex(this.barDockControlRight, 0);
            this.Controls.SetChildIndex(this.barDockControlLeft, 0);
            this.Controls.SetChildIndex(this.dockPanelLeft, 0);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.splitterControl, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            this.Controls.SetChildIndex(this.xscResult, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanelLeft.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiNew;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiSave;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiDel;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiExport;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiPrint;
        internal DevExpress.XtraBars.BarLargeButtonItem blbiExit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelLeft;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PanelControl panelControl;
        internal DevExpress.XtraEditors.XtraScrollableControl xscResult;
        private DevExpress.XtraEditors.SplitterControl splitterControl;
        internal DevExpress.XtraTreeList.TreeList tvApp;
        internal System.Windows.Forms.ImageList imageListCase;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblFind;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.TextEdit txtFind;
        private DevExpress.XtraBars.BarLargeButtonItem blbiCommit;
        private DevExpress.XtraBars.BarLargeButtonItem blbiReport;
    }
}