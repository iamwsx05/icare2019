namespace Common.Controls
{
    partial class ucDepts
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkcDept = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcDept.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 109;
            this.label1.Text = "科室:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkcDept
            // 
            this.chkcDept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkcDept.EditValue = "";
            this.chkcDept.Location = new System.Drawing.Point(47, 1);
            this.chkcDept.MinimumSize = new System.Drawing.Size(0, 20);
            this.chkcDept.Name = "chkcDept";
            this.chkcDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.chkcDept.Properties.Appearance.Options.UseFont = true;
            this.chkcDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcDept.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-1", "未执行"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-2", "无人接听"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-3", "电话不通"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-4", "完成本次任务"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-5", "完成随访"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-6", "拒绝随访"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-7", "电话有误"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-8", "再次入院"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-9", "病故"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-10", "手动结束"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("*-11", "非本人接听")});
            this.chkcDept.Properties.PopupFormSize = new System.Drawing.Size(60, 260);
            this.chkcDept.Size = new System.Drawing.Size(109, 20);
            this.chkcDept.TabIndex = 121;
            // 
            // ucDepts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkcDept);
            this.Controls.Add(this.label1);
            this.Name = "ucDepts";
            this.Size = new System.Drawing.Size(159, 23);
            this.Load += new System.EventHandler(this.ucDepts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkcDept.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal DevExpress.XtraEditors.CheckedComboBoxEdit chkcDept;
    }
}
