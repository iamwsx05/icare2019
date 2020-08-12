namespace Common.Controls.Emr
{
    partial class ctlThreeItemsControl
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
            this.picContainer = new System.Windows.Forms.PictureBox();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.ttcHint = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picContainer
            // 
            this.picContainer.BackColor = System.Drawing.Color.White;
            this.picContainer.Location = new System.Drawing.Point(0, 0);
            this.picContainer.Name = "picContainer";
            this.picContainer.Size = new System.Drawing.Size(300, 200);
            this.picContainer.TabIndex = 3;
            this.picContainer.TabStop = false;
            this.picContainer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picContainer_MouseClick);
            this.picContainer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picContainer_MouseDoubleClick);
            this.picContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picContainer_MouseMove);
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Controls.Add(this.picContainer);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(617, 588);
            this.xtraScrollableControl1.TabIndex = 4;
            // 
            // ttcHint
            // 
            this.ttcHint.Appearance.ForeColor = System.Drawing.Color.Black;
            this.ttcHint.Appearance.Options.UseForeColor = true;
            this.ttcHint.AutoPopDelay = 3000;
            this.ttcHint.CloseOnClick = DevExpress.Utils.DefaultBoolean.True;
            this.ttcHint.Rounded = true;
            this.ttcHint.ShowBeak = true;
            this.ttcHint.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            // 
            // ctlThreeItemsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraScrollableControl1);
            this.Name = "ctlThreeItemsControl";
            this.Size = new System.Drawing.Size(617, 588);
            ((System.ComponentModel.ISupportInitialize)(this.picContainer)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picContainer;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.Utils.ToolTipController ttcHint;
    }
}
