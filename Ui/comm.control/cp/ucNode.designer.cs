namespace Common.Controls
{
    partial class ucNode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNode));
            this.lblMark = new DevExpress.XtraEditors.LabelControl();
            this.picCaption = new System.Windows.Forms.PictureBox();
            this.lblDay = new DevExpress.XtraEditors.LabelControl();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.picStatus2 = new System.Windows.Forms.PictureBox();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.blbiAdjust = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiReturn = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barLargeButtonItem1 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem2 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.bbiRight = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem3 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItem4 = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiNewRecipe = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiNewItem = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiDelRecipe = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiDelItem = new DevExpress.XtraBars.BarLargeButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.picCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMark
            // 
            this.lblMark.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.lblMark.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.lblMark.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMark.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.lblMark.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMark.Location = new System.Drawing.Point(0, 92);
            this.lblMark.Name = "lblMark";
            this.lblMark.Size = new System.Drawing.Size(64, 16);
            this.lblMark.TabIndex = 3;
            this.lblMark.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucNode_MouseClick);
            this.lblMark.MouseEnter += new System.EventHandler(this.lblInfo_MouseEnter);
            this.lblMark.MouseLeave += new System.EventHandler(this.lblInfo_MouseLeave);
            // 
            // picCaption
            // 
            this.picCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picCaption.Image = ((System.Drawing.Image)(resources.GetObject("picCaption.Image")));
            this.picCaption.Location = new System.Drawing.Point(0, 14);
            this.picCaption.Name = "picCaption";
            this.picCaption.Size = new System.Drawing.Size(64, 64);
            this.picCaption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCaption.TabIndex = 0;
            this.picCaption.TabStop = false;
            this.picCaption.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucNode_MouseClick);
            this.picCaption.MouseEnter += new System.EventHandler(this.picCaption_MouseEnter);
            this.picCaption.MouseLeave += new System.EventHandler(this.picCaption_MouseLeave);
            // 
            // lblDay
            // 
            this.lblDay.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.lblDay.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(57)))), ((int)(((byte)(91)))));
            this.lblDay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDay.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblDay.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDay.Location = new System.Drawing.Point(0, 80);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(64, 12);
            this.lblDay.TabIndex = 2;
            // 
            // picStatus
            // 
            this.picStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picStatus.Location = new System.Drawing.Point(0, 80);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(64, 0);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picStatus.TabIndex = 4;
            this.picStatus.TabStop = false;
            // 
            // picStatus2
            // 
            this.picStatus2.Location = new System.Drawing.Point(46, 1);
            this.picStatus2.Name = "picStatus2";
            this.picStatus2.Size = new System.Drawing.Size(16, 16);
            this.picStatus2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picStatus2.TabIndex = 5;
            this.picStatus2.TabStop = false;
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiAdjust),
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiReturn, true)});
            this.popupMenu.Manager = this.barManager;
            this.popupMenu.Name = "popupMenu";
            // 
            // blbiAdjust
            // 
            this.blbiAdjust.Caption = "调整节点";
            this.blbiAdjust.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiAdjust.Glyph")));
            this.blbiAdjust.Id = 9;
            this.blbiAdjust.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiAdjust.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiAdjust.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("blbiAdjust.LargeGlyph")));
            this.blbiAdjust.Name = "blbiAdjust";
            this.blbiAdjust.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiAdjust_ItemClick);
            // 
            // blbiReturn
            // 
            this.blbiReturn.Caption = "回退节点";
            this.blbiReturn.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiReturn.Glyph")));
            this.blbiReturn.Id = 10;
            this.blbiReturn.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiReturn.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiReturn.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("blbiReturn.LargeGlyph")));
            this.blbiReturn.Name = "blbiReturn";
            this.blbiReturn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiReturn_ItemClick);
            // 
            // barManager
            // 
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItem1,
            this.barLargeButtonItem2,
            this.bbiRight,
            this.barLargeButtonItem3,
            this.barLargeButtonItem4,
            this.blbiNewRecipe,
            this.blbiNewItem,
            this.blbiDelRecipe,
            this.blbiDelItem,
            this.blbiAdjust,
            this.blbiReturn});
            this.barManager.MaxItemId = 11;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(64, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 108);
            this.barDockControlBottom.Size = new System.Drawing.Size(64, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 108);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(64, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 108);
            // 
            // barLargeButtonItem1
            // 
            this.barLargeButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barLargeButtonItem1.Caption = "导出";
            this.barLargeButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItem1.Glyph")));
            this.barLargeButtonItem1.Id = 0;
            this.barLargeButtonItem1.Name = "barLargeButtonItem1";
            // 
            // barLargeButtonItem2
            // 
            this.barLargeButtonItem2.Caption = "barLargeButtonItem2";
            this.barLargeButtonItem2.Id = 1;
            this.barLargeButtonItem2.Name = "barLargeButtonItem2";
            // 
            // bbiRight
            // 
            this.bbiRight.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiRight.Caption = "barLargeButtonItem3";
            this.bbiRight.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRight.Glyph")));
            this.bbiRight.Id = 2;
            this.bbiRight.Name = "bbiRight";
            // 
            // barLargeButtonItem3
            // 
            this.barLargeButtonItem3.Caption = "aaaaaaa";
            this.barLargeButtonItem3.Id = 3;
            this.barLargeButtonItem3.Name = "barLargeButtonItem3";
            // 
            // barLargeButtonItem4
            // 
            this.barLargeButtonItem4.Caption = "bbbbbbbbbbb";
            this.barLargeButtonItem4.Id = 4;
            this.barLargeButtonItem4.Name = "barLargeButtonItem4";
            // 
            // blbiNewRecipe
            // 
            this.blbiNewRecipe.Caption = "新建处方";
            this.blbiNewRecipe.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiNewRecipe.Glyph")));
            this.blbiNewRecipe.Id = 5;
            this.blbiNewRecipe.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiNewRecipe.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiNewRecipe.Name = "blbiNewRecipe";
            // 
            // blbiNewItem
            // 
            this.blbiNewItem.Caption = "新建草药明细";
            this.blbiNewItem.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiNewItem.Glyph")));
            this.blbiNewItem.Id = 6;
            this.blbiNewItem.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiNewItem.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiNewItem.Name = "blbiNewItem";
            // 
            // blbiDelRecipe
            // 
            this.blbiDelRecipe.Caption = "删除处方";
            this.blbiDelRecipe.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiDelRecipe.Glyph")));
            this.blbiDelRecipe.Id = 7;
            this.blbiDelRecipe.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiDelRecipe.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiDelRecipe.Name = "blbiDelRecipe";
            // 
            // blbiDelItem
            // 
            this.blbiDelItem.Caption = "删除草药明细";
            this.blbiDelItem.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiDelItem.Glyph")));
            this.blbiDelItem.Id = 8;
            this.blbiDelItem.ItemAppearance.Normal.Font = new System.Drawing.Font("宋体", 9.5F);
            this.blbiDelItem.ItemAppearance.Normal.Options.UseFont = true;
            this.blbiDelItem.Name = "blbiDelItem";
            // 
            // ucNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.picStatus2);
            this.Controls.Add(this.picStatus);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblMark);
            this.Controls.Add(this.picCaption);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucNode";
            this.Size = new System.Drawing.Size(64, 108);
            this.SizeChanged += new System.EventHandler(this.ucNode_SizeChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucNode_MouseClick);
            this.MouseEnter += new System.EventHandler(this.ucNode_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ucNode_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox picCaption;
        public DevExpress.XtraEditors.LabelControl lblMark;
        public DevExpress.XtraEditors.LabelControl lblDay;
        public System.Windows.Forms.PictureBox picStatus;
        public System.Windows.Forms.PictureBox picStatus2;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarLargeButtonItem blbiAdjust;
        private DevExpress.XtraBars.BarLargeButtonItem blbiDelRecipe;
        private DevExpress.XtraBars.BarLargeButtonItem blbiDelItem;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem1;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem2;
        private DevExpress.XtraBars.BarLargeButtonItem bbiRight;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem3;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItem4;
        private DevExpress.XtraBars.BarLargeButtonItem blbiNewRecipe;
        private DevExpress.XtraBars.BarLargeButtonItem blbiNewItem;
        private DevExpress.XtraBars.BarLargeButtonItem blbiReturn;

    }
}
