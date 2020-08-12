namespace Common.Controls
{
    partial class ucDept
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
            this.lueDept = new Common.Controls.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lueDept
            // 
            this.lueDept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.IsButtonFind = false;
            this.lueDept.Location = new System.Drawing.Point(40, 0);
            this.lueDept.Name = "lueDept";
            this.lueDept.ParentBandedGridView = null;
            this.lueDept.ParentBindingSource = null;
            this.lueDept.ParentGridView = null;
            this.lueDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueDept.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDept.Properties.Appearance.Options.UseFont = true;
            this.lueDept.Properties.Appearance.Options.UseForeColor = true;
            this.lueDept.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDept.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDept.Properties.DataSource = null;
            this.lueDept.Properties.DBRow = null;
            this.lueDept.Properties.DBValue = "";
            this.lueDept.Properties.DescCode = null;
            this.lueDept.Properties.DisplayColumn = null;
            this.lueDept.Properties.DisplayValue = "";
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.FieldName = null;
            this.lueDept.Properties.FilterColumn = null;
            this.lueDept.Properties.ForbidPoput = false;
            this.lueDept.Properties.HideColumn = null;
            this.lueDept.Properties.IsAutoPopup = false;
            this.lueDept.Properties.IsCheckValid = true;
            this.lueDept.Properties.IsDescField = false;
            this.lueDept.Properties.IsFreeInput = false;
            this.lueDept.Properties.IsHideValueColumn = false;
            this.lueDept.Properties.IsSelectedMoveNextControl = false;
            this.lueDept.Properties.IsShowColumnHeaders = false;
            this.lueDept.Properties.IsShowDescInfo = false;
            this.lueDept.Properties.IsShowRowNo = false;
            this.lueDept.Properties.IsTab = true;
            this.lueDept.Properties.IsUseShowColumn = false;
            this.lueDept.Properties.ParentBandedGridView = null;
            this.lueDept.Properties.ParentBindingSource = null;
            this.lueDept.Properties.ParentGridView = null;
            this.lueDept.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDept.Properties.PopupHeight = 0;
            this.lueDept.Properties.PopupSizeable = false;
            this.lueDept.Properties.PopupWidth = 0;
            this.lueDept.Properties.PresentationMode = 0;
            this.lueDept.Properties.ShowColumn = null;
            this.lueDept.Properties.ShowPopupCloseButton = false;
            this.lueDept.Properties.ShowPopupShadow = false;
            this.lueDept.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDept.Properties.ValueColumn = null;
            this.lueDept.Size = new System.Drawing.Size(108, 20);
            this.lueDept.TabIndex = 107;
            this.lueDept.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDept_HandleDBValueChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 108;
            this.label1.Text = "科室:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lueDept);
            this.Controls.Add(this.label1);
            this.Name = "ucDept";
            this.Size = new System.Drawing.Size(159, 23);
            this.Load += new System.EventHandler(this.ucDept_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal LookUpEdit lueDept;
        private System.Windows.Forms.Label label1;
    }
}
