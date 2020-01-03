namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmRecipeBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecipeBrowser));
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lstRecipe = new DevExpress.XtraEditors.ListBoxControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.myPrintPreViewControl1 = new com.digitalwave.controls.Control.MyPrintPreViewControl();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstRecipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
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
            // dockPanel1
            // 
            this.dockPanel1.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.dockPanel1.Appearance.Options.UseFont = true;
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.Font = new System.Drawing.Font("宋体", 9.5F);
            this.dockPanel1.ID = new System.Guid("ac4ae55c-e386-491f-a1cd-0f158090f1c6");
            this.dockPanel1.Location = new System.Drawing.Point(604, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(243, 200);
            this.dockPanel1.Size = new System.Drawing.Size(243, 697);
            this.dockPanel1.Text = "处方列表";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.lstRecipe);
            this.dockPanel1_Container.Controls.Add(this.panelControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 25);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(234, 668);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // lstRecipe
            // 
            this.lstRecipe.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lstRecipe.Appearance.ForeColor = System.Drawing.Color.DeepPink;
            this.lstRecipe.Appearance.Options.UseFont = true;
            this.lstRecipe.Appearance.Options.UseForeColor = true;
            this.lstRecipe.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRecipe.ItemHeight = 28;
            this.lstRecipe.Location = new System.Drawing.Point(0, 48);
            this.lstRecipe.Name = "lstRecipe";
            this.lstRecipe.Size = new System.Drawing.Size(234, 620);
            this.lstRecipe.TabIndex = 1;
            this.lstRecipe.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRecipe_MouseDoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOpen);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(234, 48);
            this.panelControl1.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOpen.Appearance.Options.UseFont = true;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.Location = new System.Drawing.Point(4, 6);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(228, 36);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "打开文件 &O";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // printDocument
            // 
            this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // myPrintPreViewControl1
            // 
            this.myPrintPreViewControl1.BlnCustomFlag = false;
            this.myPrintPreViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPrintPreViewControl1.Document = this.printDocument;
            this.myPrintPreViewControl1.Location = new System.Drawing.Point(0, 0);
            this.myPrintPreViewControl1.Name = "myPrintPreViewControl1";
            this.myPrintPreViewControl1.ReportName = "";
            this.myPrintPreViewControl1.ShowPannel = true;
            this.myPrintPreViewControl1.ShowPrintButton = true;
            this.myPrintPreViewControl1.Size = new System.Drawing.Size(604, 697);
            this.myPrintPreViewControl1.strCheckName = "";
            this.myPrintPreViewControl1.strDeptName = "";
            this.myPrintPreViewControl1.TabIndex = 1;
            this.myPrintPreViewControl1.Zoom = 1D;
            // 
            // frmRecipeBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 697);
            this.Controls.Add(this.myPrintPreViewControl1);
            this.Controls.Add(this.dockPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecipeBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处方浏览";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRecipeBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstRecipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container; 
        private DevExpress.XtraEditors.ListBoxControl lstRecipe;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOpen;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ImageList imageList1;
        private controls.Control.MyPrintPreViewControl myPrintPreViewControl1;
    }
}