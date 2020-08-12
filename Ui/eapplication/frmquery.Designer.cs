namespace weCare.eApp
{
    partial class frmQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery));
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.blbiExport = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiPrint = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.tvApp = new DevExpress.XtraTreeList.TreeList();
            this.imageListCase = new System.Windows.Forms.ImageList(this.components);
            this.xscResult = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanel.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvApp)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.None;
            this.pcBackGround.Location = new System.Drawing.Point(-452, 208);
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
            this.blbiExport,
            this.blbiPrint,
            this.blbiClose});
            this.barManager.MaxItemId = 3;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.blbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // blbiExport
            // 
            this.blbiExport.Caption = " 导出 ";
            this.blbiExport.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiExport.Glyph")));
            this.blbiExport.Id = 0;
            this.blbiExport.Name = "blbiExport";
            this.blbiExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiExport_ItemClick);
            // 
            // blbiPrint
            // 
            this.blbiPrint.Caption = " 打印 ";
            this.blbiPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiPrint.Glyph")));
            this.blbiPrint.Id = 1;
            this.blbiPrint.Name = "blbiPrint";
            this.blbiPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiPrint_ItemClick);
            // 
            // blbiClose
            // 
            this.blbiClose.Caption = " 关闭 ";
            this.blbiClose.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiClose.Glyph")));
            this.blbiClose.Id = 2;
            this.blbiClose.Name = "blbiClose";
            this.blbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1275, 60);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 729);
            this.barDockControlBottom.Size = new System.Drawing.Size(1275, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 60);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 669);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1275, 60);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 669);
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManager;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel
            // 
            this.dockPanel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockPanel.Appearance.Options.UseFont = true;
            this.dockPanel.Controls.Add(this.dockPanel1_Container);
            this.dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel.ID = new System.Guid("0776f2c5-d19b-4241-aa35-80b6fde07537");
            this.dockPanel.Location = new System.Drawing.Point(0, 60);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Options.ShowCloseButton = false;
            this.dockPanel.OriginalSize = new System.Drawing.Size(226, 200);
            this.dockPanel.Size = new System.Drawing.Size(226, 669);
            this.dockPanel.Text = "电子申请单";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.tvApp);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(218, 640);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // tvApp
            // 
            this.tvApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvApp.Location = new System.Drawing.Point(0, 0);
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
            this.tvApp.Size = new System.Drawing.Size(218, 640);
            this.tvApp.StateImageList = this.imageListCase;
            this.tvApp.TabIndex = 29;
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
            // xscResult
            // 
            this.xscResult.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xscResult.Appearance.Options.UseBackColor = true;
            this.xscResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscResult.Location = new System.Drawing.Point(226, 60);
            this.xscResult.Name = "xscResult";
            this.xscResult.Size = new System.Drawing.Size(1049, 669);
            this.xscResult.TabIndex = 9;
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1275, 729);
            this.Controls.Add(this.xscResult);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmQuery";
            this.Text = "电子申请单查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmQuery_Load);
            this.Controls.SetChildIndex(this.barDockControlTop, 0);
            this.Controls.SetChildIndex(this.barDockControlBottom, 0);
            this.Controls.SetChildIndex(this.barDockControlRight, 0);
            this.Controls.SetChildIndex(this.barDockControlLeft, 0);
            this.Controls.SetChildIndex(this.dockPanel, 0);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.xscResult, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanel.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvApp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem blbiExport;
        private DevExpress.XtraBars.BarLargeButtonItem blbiPrint;
        private DevExpress.XtraBars.BarLargeButtonItem blbiClose;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        internal DevExpress.XtraTreeList.TreeList tvApp;
        internal System.Windows.Forms.ImageList imageListCase;
        internal DevExpress.XtraEditors.XtraScrollableControl xscResult;
    }
}