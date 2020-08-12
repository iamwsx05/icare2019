namespace Common.Controls.Emr
{
    partial class ctlSignature
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlSignature));
            this.popupMenu = new DevExpress.XtraBars.PopupMenu();
            this.blbiSign = new DevExpress.XtraBars.BarLargeButtonItem();
            this.blbiDel = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lblDoctName = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiSign),
            new DevExpress.XtraBars.LinkPersistInfo(this.blbiDel, true)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.MenuAppearance.AppearanceMenu.Normal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.popupMenu.MenuAppearance.HeaderItemAppearance.Font = new System.Drawing.Font("宋体", 9F);
            this.popupMenu.Name = "popupMenu";
            // 
            // blbiSign
            // 
            this.blbiSign.Caption = "签名";
            this.blbiSign.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiSign.Glyph")));
            this.blbiSign.Id = 0;
            this.blbiSign.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("blbiSign.LargeGlyph")));
            this.blbiSign.Name = "blbiSign";
            this.blbiSign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiSign_ItemClick);
            // 
            // blbiDel
            // 
            this.blbiDel.Caption = "删除";
            this.blbiDel.Glyph = ((System.Drawing.Image)(resources.GetObject("blbiDel.Glyph")));
            this.blbiDel.Id = 1;
            this.blbiDel.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("blbiDel.LargeGlyph")));
            this.blbiDel.Name = "blbiDel";
            this.blbiDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.blbiDel_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.blbiSign,
            this.blbiDel});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(137, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 14);
            this.barDockControlBottom.Size = new System.Drawing.Size(137, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 14);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(137, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 14);
            // 
            // lblDoctName
            // 
            this.lblDoctName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDoctName.Font = new System.Drawing.Font("宋体", 9F);
            this.lblDoctName.Location = new System.Drawing.Point(35, 0);
            this.lblDoctName.Margin = new System.Windows.Forms.Padding(0);
            this.lblDoctName.Name = "lblDoctName";
            this.lblDoctName.Size = new System.Drawing.Size(102, 14);
            this.lblDoctName.TabIndex = 1;
            this.lblDoctName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDoctName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblDoctName_MouseClick);
            this.lblDoctName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblDoctName_MouseDoubleClick);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCaption.Font = new System.Drawing.Font("宋体", 9F);
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(35, 12);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "签名:";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCaption.Enter += new System.EventHandler(this.lblCaption_Enter);
            this.lblCaption.Leave += new System.EventHandler(this.lblCaption_Leave);
            this.lblCaption.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblCaption_MouseClick);
            this.lblCaption.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblCaption_MouseDoubleClick);
            // 
            // ctlSignature
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDoctName);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ctlSignature";
            this.Size = new System.Drawing.Size(137, 14);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ctlSignature_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ctlSignature_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarLargeButtonItem blbiSign;
        private DevExpress.XtraBars.BarLargeButtonItem blbiDel;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.Label lblDoctName;
        private System.Windows.Forms.Label lblCaption;
    }
}
