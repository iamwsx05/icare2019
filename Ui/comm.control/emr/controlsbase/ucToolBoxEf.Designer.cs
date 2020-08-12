
namespace Common.Controls.Emr
{
    partial class ucToolBoxEf
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucToolBoxEf));
            this.pcBak = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.flpTop = new Common.Controls.ctlFlowLayoutPanel();
            this.navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.fltBottom = new Common.Controls.ctlFlowLayoutPanel();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.imageList = new System.Windows.Forms.ImageList();
            ((System.ComponentModel.ISupportInitialize)(this.pcBak)).BeginInit();
            this.pcBak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            this.navBarControl.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcBak
            // 
            this.pcBak.Controls.Add(this.navBarControl);
            this.pcBak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcBak.Location = new System.Drawing.Point(0, 0);
            this.pcBak.Name = "pcBak";
            this.pcBak.Size = new System.Drawing.Size(130, 768);
            this.pcBak.TabIndex = 0;
            // 
            // navBarControl
            // 
            this.navBarControl.ActiveGroup = this.navBarGroup1;
            this.navBarControl.Appearance.Background.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.navBarControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.navBarControl.ContentButtonHint = null;
            this.navBarControl.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl.Controls.Add(this.navBarGroupControlContainer2);
            this.navBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2});
            this.navBarControl.Location = new System.Drawing.Point(2, 2);
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.OptionsNavPane.ExpandedWidth = 171;
            this.navBarControl.OptionsNavPane.ShowExpandButton = false;
            this.navBarControl.OptionsNavPane.ShowOverflowButton = false;
            this.navBarControl.OptionsNavPane.ShowOverflowPanel = false;
            this.navBarControl.OptionsNavPane.ShowSplitter = false;
            this.navBarControl.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
            this.navBarControl.Size = new System.Drawing.Size(126, 764);
            this.navBarControl.SkinExplorerBarViewScrollStyle = DevExpress.XtraNavBar.SkinExplorerBarViewScrollStyle.ScrollBar;
            this.navBarControl.TabIndex = 0;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "编辑控件";
            this.navBarGroup1.ControlContainer = this.navBarGroupControlContainer1;
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupClientHeight = 320;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.panelControl1);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(118, 316);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.flpTop);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(118, 316);
            this.panelControl1.TabIndex = 0;
            // 
            // flpTop
            // 
            this.flpTop.AutoScroll = true;
            this.flpTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTop.Location = new System.Drawing.Point(2, 2);
            this.flpTop.Name = "flpTop";
            this.flpTop.Size = new System.Drawing.Size(114, 312);
            this.flpTop.TabIndex = 0;
            // 
            // navBarGroupControlContainer2
            // 
            this.navBarGroupControlContainer2.Controls.Add(this.panelControl2);
            this.navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
            this.navBarGroupControlContainer2.Size = new System.Drawing.Size(118, 377);
            this.navBarGroupControlContainer2.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.fltBottom);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(118, 377);
            this.panelControl2.TabIndex = 1;
            // 
            // fltBottom
            // 
            this.fltBottom.AutoScroll = true;
            this.fltBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fltBottom.Location = new System.Drawing.Point(2, 2);
            this.fltBottom.Margin = new System.Windows.Forms.Padding(0);
            this.fltBottom.Name = "fltBottom";
            this.fltBottom.Padding = new System.Windows.Forms.Padding(1);
            this.fltBottom.Size = new System.Drawing.Size(114, 373);
            this.fltBottom.TabIndex = 0;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "宏元素";
            this.navBarGroup2.ControlContainer = this.navBarGroupControlContainer2;
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.GroupClientHeight = 381;
            this.navBarGroup2.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "指针.png");
            this.imageList.Images.SetKeyName(1, "水平线.png");
            this.imageList.Images.SetKeyName(2, "垂直线.png");
            this.imageList.Images.SetKeyName(3, "标签.png");
            this.imageList.Images.SetKeyName(4, "勾选框.png");
            this.imageList.Images.SetKeyName(5, "文本框.png");
            this.imageList.Images.SetKeyName(6, "多行编辑器.png");
            this.imageList.Images.SetKeyName(7, "AlignObjectsTopHS.png");
            this.imageList.Images.SetKeyName(8, "AlignObjectsBottomHS.png");
            this.imageList.Images.SetKeyName(9, "AlignObjectsLeftHS.png");
            this.imageList.Images.SetKeyName(10, "AlignObjectsRightHS.png");
            this.imageList.Images.SetKeyName(11, "AlignObjectsCenteredVerticalHS.png");
            this.imageList.Images.SetKeyName(12, "AlignObjectsCenteredHorizontalHS.png");
            this.imageList.Images.SetKeyName(13, "BringToFrontHS.png");
            this.imageList.Images.SetKeyName(14, "SendToBackHS.png");
            this.imageList.Images.SetKeyName(15, "TabOrder.bmp");
            this.imageList.Images.SetKeyName(16, "HMakeEqual.bmp");
            this.imageList.Images.SetKeyName(17, "VMakeEqual.bmp");
            this.imageList.Images.SetKeyName(18, "HVMakeEqual.bmp");
            this.imageList.Images.SetKeyName(19, "HSpaceMakeEqual.bmp");
            this.imageList.Images.SetKeyName(20, "VSpaceMakeEqual.bmp");
            // 
            // ucToolBoxEf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcBak);
            this.Name = "ucToolBoxEf";
            this.Size = new System.Drawing.Size(130, 768);
            this.Load += new System.EventHandler(this.ucToolBoxEf_Load);
            this.Resize += new System.EventHandler(this.ucToolBoxEf_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pcBak)).EndInit();
            this.pcBak.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            this.navBarControl.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.navBarGroupControlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcBak;
        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer2;
        private System.Windows.Forms.ImageList imageList;
        private ctlFlowLayoutPanel flpTop;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private ctlFlowLayoutPanel fltBottom;
        internal DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        internal DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
    }
}
