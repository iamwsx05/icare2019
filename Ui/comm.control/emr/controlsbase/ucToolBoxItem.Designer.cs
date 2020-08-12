
namespace Common.Controls.Emr
{
    partial class ucToolBoxItem
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
            this.picCaption = new System.Windows.Forms.PictureBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblBak = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCaption)).BeginInit();
            this.SuspendLayout();
            // 
            // picCaption
            // 
            this.picCaption.BackColor = System.Drawing.Color.Transparent;
            this.picCaption.Location = new System.Drawing.Point(2, 0);
            this.picCaption.Name = "picCaption";
            this.picCaption.Size = new System.Drawing.Size(24, 18);
            this.picCaption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCaption.TabIndex = 1;
            this.picCaption.TabStop = false;
            // 
            // lblCaption
            // 
            this.lblCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCaption.Location = new System.Drawing.Point(28, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(65, 18);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCaption.MouseLeave += new System.EventHandler(this.lblCaption_MouseLeave);
            this.lblCaption.MouseEnter += new System.EventHandler(this.lblCaption_MouseEnter);
            // 
            // lblBak
            // 
            this.lblBak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBak.Location = new System.Drawing.Point(0, 0);
            this.lblBak.Name = "lblBak";
            this.lblBak.Size = new System.Drawing.Size(98, 18);
            this.lblBak.TabIndex = 3;
            // 
            // ucToolBoxItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.picCaption);
            this.Controls.Add(this.lblBak);
            this.Name = "ucToolBoxItem";
            this.Size = new System.Drawing.Size(98, 18);
            ((System.ComponentModel.ISupportInitialize)(this.picCaption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBak;
        public System.Windows.Forms.Label lblCaption;
        public System.Windows.Forms.PictureBox picCaption;
    }
}
