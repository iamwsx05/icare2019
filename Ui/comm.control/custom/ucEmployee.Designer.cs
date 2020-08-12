namespace Common.Controls
{
    partial class ucEmployee
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
            this.lueEmp = new Common.Controls.LookUpEdit();
            this.lblDoct = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueEmp.Properties)).BeginInit();
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
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "职工:")});
            this.rdoType.Size = new System.Drawing.Size(119, 28);
            this.rdoType.TabIndex = 0;
            // 
            // lueEmp
            // 
            this.lueEmp.CellValueChanged = true;
            this.lueEmp.EditValue = "";
            this.lueEmp.IsButtonFind = false;
            this.lueEmp.Location = new System.Drawing.Point(114, 4);
            this.lueEmp.Name = "lueEmp";
            this.lueEmp.ParentBandedGridView = null;
            this.lueEmp.ParentBindingSource = null;
            this.lueEmp.ParentGridView = null;
            this.lueEmp.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lueEmp.Properties.Appearance.Options.UseFont = true;
            this.lueEmp.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueEmp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueEmp.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueEmp.Properties.DataSource = null;
            this.lueEmp.Properties.DBRow = null;
            this.lueEmp.Properties.DBValue = "";
            this.lueEmp.Properties.DescCode = null;
            this.lueEmp.Properties.DisplayColumn = null;
            this.lueEmp.Properties.DisplayValue = "";
            this.lueEmp.Properties.Essential = false;
            this.lueEmp.Properties.FieldName = null;
            this.lueEmp.Properties.FilterColumn = null;
            this.lueEmp.Properties.ForbidPoput = false;
            this.lueEmp.Properties.HideColumn = null;
            this.lueEmp.Properties.IsAutoPopup = false;
            this.lueEmp.Properties.IsCheckValid = true;
            this.lueEmp.Properties.IsDescField = false;
            this.lueEmp.Properties.IsFreeInput = false;
            this.lueEmp.Properties.IsHideValueColumn = false;
            this.lueEmp.Properties.IsSelectedMoveNextControl = false;
            this.lueEmp.Properties.IsShowColumnHeaders = false;
            this.lueEmp.Properties.IsShowDescInfo = false;
            this.lueEmp.Properties.IsShowRowNo = false;
            this.lueEmp.Properties.IsTab = true;
            this.lueEmp.Properties.IsUseShowColumn = false;
            this.lueEmp.Properties.ParentBandedGridView = null;
            this.lueEmp.Properties.ParentBindingSource = null;
            this.lueEmp.Properties.ParentGridView = null;
            this.lueEmp.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueEmp.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueEmp.Properties.PopupHeight = 0;
            this.lueEmp.Properties.PopupSizeable = false;
            this.lueEmp.Properties.PopupWidth = 0;
            this.lueEmp.Properties.PresentationMode = 0;
            this.lueEmp.Properties.ShowColumn = null;
            this.lueEmp.Properties.ShowPopupCloseButton = false;
            this.lueEmp.Properties.ShowPopupShadow = false;
            this.lueEmp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueEmp.Properties.ValueColumn = null;
            this.lueEmp.Size = new System.Drawing.Size(102, 20);
            this.lueEmp.TabIndex = 1;
            this.lueEmp.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDept_HandleDBValueChanged);
            // 
            // lblDoct
            // 
            this.lblDoct.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDoct.Location = new System.Drawing.Point(7, 4);
            this.lblDoct.Name = "lblDoct";
            this.lblDoct.Size = new System.Drawing.Size(45, 20);
            this.lblDoct.TabIndex = 110;
            this.lblDoct.Text = "医师:";
            this.lblDoct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lueEmp);
            this.Controls.Add(this.rdoType);
            this.Controls.Add(this.lblDoct);
            this.Name = "ucEmployee";
            this.Size = new System.Drawing.Size(219, 26);
            this.Load += new System.EventHandler(this.ucEmployee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueEmp.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.xtraRadioGroup rdoType;
        private Common.Controls.LookUpEdit lueEmp;
        private System.Windows.Forms.Label lblDoct;
    }
}
