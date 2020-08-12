namespace Common.Controls
{
    partial class frmPrintDocumentSimple
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintDocumentSimple));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnFirstPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnPageUp = new DevExpress.XtraBars.BarButtonItem();
            this.txtPageIndex = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtPageNum = new DevExpress.XtraBars.BarButtonItem();
            this.btnPageDown = new DevExpress.XtraBars.BarButtonItem();
            this.btnLastPage = new DevExpress.XtraBars.BarButtonItem();
            this.btnContinue = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this.btnScale = new DevExpress.XtraBars.BarStaticItem();
            this.labScale = new DevExpress.XtraBars.BarStaticItem();
            this.bbiPrinter = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.printControl = new Common.Controls.ucPrintPreviewControl();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.barScale = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.vScrollBar = new DevExpress.XtraEditors.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barScale.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.vScrollBar);
            this.pcBackGround.Controls.Add(this.printControl);
            this.pcBackGround.Location = new System.Drawing.Point(0, 24);
            this.pcBackGround.Size = new System.Drawing.Size(1155, 664);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnFirstPage,
            this.btnPageUp,
            this.txtPageIndex,
            this.txtPageNum,
            this.btnPageDown,
            this.btnLastPage,
            this.btnContinue,
            this.btnPrint,
            this.btnExport,
            this.btnScale,
            this.labScale,
            this.bbiClose,
            this.bbiPrinter});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 15;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            // 
            // bar2
            // 
            this.bar2.BarAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.bar2.BarAppearance.Normal.Options.UseFont = true;
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnFirstPage),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPageUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.txtPageIndex, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.txtPageNum, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPageDown, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLastPage),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnContinue, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnPrint, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnScale, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.labScale),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiPrinter, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExport, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.bbiClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.AllowRename = true;
            this.bar2.OptionsBar.DisableClose = true;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Caption = "首页";
            this.btnFirstPage.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFirstPage.Glyph")));
            this.btnFirstPage.Id = 0;
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFirstPage_ItemClick);
            // 
            // btnPageUp
            // 
            this.btnPageUp.Caption = "上一页";
            this.btnPageUp.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPageUp.Glyph")));
            this.btnPageUp.Id = 1;
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPageUp_ItemClick);
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Edit = this.repositoryItemTextEdit1;
            this.txtPageIndex.EditValue = "0";
            this.txtPageIndex.Id = 2;
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.EditValueChanged += new System.EventHandler(this.txtPageIndex_EditValueChanged);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // txtPageNum
            // 
            this.txtPageNum.Caption = "/ 0 页";
            this.txtPageNum.Id = 3;
            this.txtPageNum.ItemAppearance.Disabled.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPageNum.ItemAppearance.Disabled.Options.UseFont = true;
            this.txtPageNum.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.txtPageNum.ItemAppearance.Hovered.Options.UseFont = true;
            this.txtPageNum.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.txtPageNum.ItemAppearance.Normal.Options.UseFont = true;
            this.txtPageNum.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.txtPageNum.ItemAppearance.Pressed.Options.UseFont = true;
            this.txtPageNum.Name = "txtPageNum";
            // 
            // btnPageDown
            // 
            this.btnPageDown.Caption = "下一页";
            this.btnPageDown.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPageDown.Glyph")));
            this.btnPageDown.Id = 4;
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPageDown_ItemClick);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Caption = "尾页";
            this.btnLastPage.Glyph = ((System.Drawing.Image)(resources.GetObject("btnLastPage.Glyph")));
            this.btnLastPage.Id = 5;
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLastPage_ItemClick);
            // 
            // btnContinue
            // 
            this.btnContinue.Caption = "续打";
            this.btnContinue.Glyph = ((System.Drawing.Image)(resources.GetObject("btnContinue.Glyph")));
            this.btnContinue.Id = 6;
            this.btnContinue.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnContinue.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnContinue.ItemAppearance.Normal.Options.UseFont = true;
            this.btnContinue.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnContinue.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnContinue.ItemInMenuAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnContinue.ItemInMenuAppearance.Hovered.Options.UseFont = true;
            this.btnContinue.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnContinue.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnContinue.ItemInMenuAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnContinue.ItemInMenuAppearance.Pressed.Options.UseFont = true;
            this.btnContinue.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnContinue.LargeGlyph")));
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnContinue_ItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Caption = "打印";
            this.btnPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("btnPrint.Glyph")));
            this.btnPrint.Id = 8;
            this.btnPrint.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnPrint.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemAppearance.Normal.Options.UseFont = true;
            this.btnPrint.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnPrint.ItemInMenuAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemInMenuAppearance.Hovered.Options.UseFont = true;
            this.btnPrint.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnPrint.ItemInMenuAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnPrint.ItemInMenuAppearance.Pressed.Options.UseFont = true;
            this.btnPrint.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnPrint.LargeGlyph")));
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrint_ItemClick);
            // 
            // btnScale
            // 
            this.btnScale.Caption = "缩放                          ";
            this.btnScale.Glyph = ((System.Drawing.Image)(resources.GetObject("btnScale.Glyph")));
            this.btnScale.Id = 10;
            this.btnScale.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnScale.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemAppearance.Normal.Options.UseFont = true;
            this.btnScale.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnScale.ItemInMenuAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemInMenuAppearance.Hovered.Options.UseFont = true;
            this.btnScale.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnScale.ItemInMenuAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnScale.ItemInMenuAppearance.Pressed.Options.UseFont = true;
            this.btnScale.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnScale.LargeGlyph")));
            this.btnScale.Name = "btnScale";
            this.btnScale.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // labScale
            // 
            this.labScale.Caption = "  100%";
            this.labScale.Id = 11;
            this.labScale.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labScale.ItemAppearance.Hovered.Options.UseFont = true;
            this.labScale.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labScale.ItemAppearance.Normal.Options.UseFont = true;
            this.labScale.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labScale.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.labScale.Name = "labScale";
            this.labScale.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // bbiPrinter
            // 
            this.bbiPrinter.Caption = "打印设置";
            this.bbiPrinter.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiPrinter.Glyph")));
            this.bbiPrinter.Id = 14;
            this.bbiPrinter.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiPrinter.ItemAppearance.Hovered.Options.UseFont = true;
            this.bbiPrinter.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiPrinter.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiPrinter.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiPrinter.ItemAppearance.Pressed.Options.UseFont = true;
            this.bbiPrinter.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiPrinter.LargeGlyph")));
            this.bbiPrinter.Name = "bbiPrinter";
            this.bbiPrinter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiPrinter_ItemClick);
            // 
            // btnExport
            // 
            this.btnExport.Caption = "导出";
            this.btnExport.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExport.Glyph")));
            this.btnExport.Id = 9;
            this.btnExport.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnExport.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemAppearance.Normal.Options.UseFont = true;
            this.btnExport.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnExport.ItemInMenuAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemInMenuAppearance.Hovered.Options.UseFont = true;
            this.btnExport.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnExport.ItemInMenuAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.btnExport.ItemInMenuAppearance.Pressed.Options.UseFont = true;
            this.btnExport.Name = "btnExport";
            this.btnExport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "关闭";
            this.bbiClose.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiClose.Glyph")));
            this.bbiClose.Id = 13;
            this.bbiClose.ItemAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bbiClose.ItemAppearance.Hovered.Options.UseFont = true;
            this.bbiClose.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiClose.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiClose.ItemAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiClose.ItemAppearance.Pressed.Options.UseFont = true;
            this.bbiClose.ItemInMenuAppearance.Hovered.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiClose.ItemInMenuAppearance.Hovered.Options.UseFont = true;
            this.bbiClose.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiClose.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.bbiClose.ItemInMenuAppearance.Pressed.Font = new System.Drawing.Font("宋体", 9F);
            this.bbiClose.ItemInMenuAppearance.Pressed.Options.UseFont = true;
            this.bbiClose.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiClose.LargeGlyph")));
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1155, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 688);
            this.barDockControlBottom.Size = new System.Drawing.Size(1155, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 664);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1155, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 664);
            // 
            // printControl
            // 
            this.printControl.AutoZoom = false;
            this.printControl.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.printControl.Columns = 1;
            this.printControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printControl.Document = this.printDocument;
            this.printControl.ForeColor = System.Drawing.Color.White;
            this.printControl.Location = new System.Drawing.Point(2, 2);
            this.printControl.Name = "printControl";
            this.printControl.Rows = 1;
            this.printControl.Size = new System.Drawing.Size(1151, 660);
            this.printControl.StartPage = 0;
            this.printControl.TabIndex = 0;
            this.printControl.UseAntiAlias = false;
            this.printControl.Zoom = 1D;
            this.printControl.OnSetPage += new System.EventHandler(this.printControl_OnSetPage);
            this.printControl.OnScroll += new System.EventHandler(this.printControl_OnScroll);
            this.printControl.SizeChanged += new System.EventHandler(this.printControl_SizeChanged);
            // 
            // printDocument
            // 
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            // 
            // barScale
            // 
            this.barScale.EditValue = 90;
            this.barScale.Location = new System.Drawing.Point(444, 3);
            this.barScale.Name = "barScale";
            this.barScale.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barScale.Properties.LargeChange = 20;
            this.barScale.Properties.Maximum = 160;
            this.barScale.Properties.Middle = 5;
            this.barScale.Properties.Minimum = 10;
            this.barScale.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.barScale.Properties.SmallChange = 5;
            this.barScale.Size = new System.Drawing.Size(143, 17);
            this.barScale.TabIndex = 5;
            this.barScale.Value = 90;
            this.barScale.EditValueChanged += new System.EventHandler(this.barScale_EditValueChanged);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(1136, 2);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 660);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // frmPrintDocumentSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 688);
            this.Controls.Add(this.barScale);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmPrintDocumentSimple";
            this.Text = "打印预览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrintDocumentSimple_Load);
            this.Controls.SetChildIndex(this.barDockControlTop, 0);
            this.Controls.SetChildIndex(this.barDockControlBottom, 0);
            this.Controls.SetChildIndex(this.barDockControlRight, 0);
            this.Controls.SetChildIndex(this.barDockControlLeft, 0);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.barScale, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barScale.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnFirstPage;
        private DevExpress.XtraBars.BarButtonItem btnPageUp;
        private DevExpress.XtraBars.BarEditItem txtPageIndex;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem txtPageNum;
        private DevExpress.XtraBars.BarButtonItem btnPageDown;
        private DevExpress.XtraBars.BarButtonItem btnLastPage;
        private DevExpress.XtraBars.BarButtonItem btnContinue;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.BarButtonItem btnExport;
        private DevExpress.XtraBars.BarStaticItem btnScale;
        private Common.Controls.ucPrintPreviewControl printControl;
        private System.Windows.Forms.Timer timer;
        private DevExpress.XtraEditors.ZoomTrackBarControl barScale;
        private DevExpress.XtraEditors.VScrollBar vScrollBar;
        internal System.Drawing.Printing.PrintDocument printDocument;
        private DevExpress.XtraBars.BarStaticItem labScale;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraBars.BarButtonItem bbiPrinter;
    }
}