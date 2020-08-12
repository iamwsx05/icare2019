namespace Common.Controls
{
    partial class ucDepartment
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
            this.rdoType = new Common.Controls.xtraRadioGroup();
            this.lueDept = new Common.Controls.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rdoType
            // 
            this.rdoType.Location = new System.Drawing.Point(1, 0);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoType.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.Appearance.Options.UseFont = true;
            this.rdoType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoType.Properties.Columns = 2;
            this.rdoType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全院"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "科室:")});
            this.rdoType.Size = new System.Drawing.Size(119, 28);
            this.rdoType.TabIndex = 0;
            // 
            // lueDept
            // 
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.Location = new System.Drawing.Point(114, 4);
            this.lueDept.Name = "lueDept";
            this.lueDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lueDept.Properties.Appearance.Options.UseFont = true;
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
            this.lueDept.Size = new System.Drawing.Size(127, 21);
            this.lueDept.TabIndex = 1;
            this.lueDept.HandleDBValueChanged += new _HandleDBValueChanged(this.lueDept_HandleDBValueChanged);
            // 
            // ucDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lueDept);
            this.Controls.Add(this.rdoType);
            this.Name = "ucDepartment";
            this.Size = new System.Drawing.Size(244, 26);
            this.Load += new System.EventHandler(this.ucDepartment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.xtraRadioGroup rdoType;
        private Common.Controls.LookUpEdit lueDept;
    }
}
