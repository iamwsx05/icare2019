namespace Common.Controls.Emr
{
    partial class ctlPartogramControl
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
            this.xscContainer = new DevExpress.XtraEditors.XtraScrollableControl();
            this.picContainer = new System.Windows.Forms.PictureBox();
            this.ttcMsg = new DevExpress.Utils.ToolTipController(this.components);
            this.xscContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // xscContainer
            // 
            this.xscContainer.Appearance.BackColor = System.Drawing.Color.White;
            this.xscContainer.Appearance.Options.UseBackColor = true;
            this.xscContainer.Controls.Add(this.picContainer);
            this.xscContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xscContainer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xscContainer.Location = new System.Drawing.Point(0, 0);
            this.xscContainer.Name = "xscContainer";
            this.xscContainer.Size = new System.Drawing.Size(588, 518);
            this.ttcMsg.SetSuperTip(this.xscContainer, null);
            this.xscContainer.TabIndex = 0;
            // 
            // picContainer
            // 
            this.picContainer.BackColor = System.Drawing.Color.White;
            this.picContainer.Location = new System.Drawing.Point(0, 0);
            this.picContainer.Name = "picContainer";
            this.picContainer.Size = new System.Drawing.Size(300, 200);
            this.ttcMsg.SetSuperTip(this.picContainer, null);
            this.picContainer.TabIndex = 4;
            this.picContainer.TabStop = false;
            this.picContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picContainer_MouseMove);
            this.picContainer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContainer_MouseDoubleClick);
            // 
            // ttcMsg
            // 
            this.ttcMsg.Appearance.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ttcMsg.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ttcMsg.Appearance.Options.UseFont = true;
            this.ttcMsg.Appearance.Options.UseForeColor = true;
            this.ttcMsg.AppearanceTitle.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ttcMsg.AppearanceTitle.Options.UseFont = true;
            this.ttcMsg.AutoPopDelay = 3000;
            this.ttcMsg.CloseOnClick = DevExpress.Utils.DefaultBoolean.True;
            this.ttcMsg.Rounded = true;
            this.ttcMsg.ShowBeak = true;
            this.ttcMsg.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // ctlConcomitantBirthPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xscContainer);
            this.Name = "ctlConcomitantBirthPic";
            this.Size = new System.Drawing.Size(588, 518);
            this.ttcMsg.SetSuperTip(this, null);
            this.xscContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl xscContainer;
        private System.Windows.Forms.PictureBox picContainer;
        private DevExpress.Utils.ToolTipController ttcMsg;
    }
}
