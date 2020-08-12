namespace Common.Controls
{
    partial class ucDoct
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
            this.lueDoct = new Common.Controls.LookUpEdit();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lueDoct
            // 
            this.lueDoct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lueDoct.CellValueChanged = true;
            this.lueDoct.EditValue = "";
            this.lueDoct.IsButtonFind = false;
            this.lueDoct.Location = new System.Drawing.Point(42, 0);
            this.lueDoct.Name = "lueDoct";
            this.lueDoct.ParentBandedGridView = null;
            this.lueDoct.ParentBindingSource = null;
            this.lueDoct.ParentGridView = null;
            this.lueDoct.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueDoct.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueDoct.Properties.Appearance.Options.UseFont = true;
            this.lueDoct.Properties.Appearance.Options.UseForeColor = true;
            this.lueDoct.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueDoct.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDoct.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueDoct.Properties.DataSource = null;
            this.lueDoct.Properties.DBRow = null;
            this.lueDoct.Properties.DBValue = "";
            this.lueDoct.Properties.DescCode = null;
            this.lueDoct.Properties.DisplayColumn = null;
            this.lueDoct.Properties.DisplayValue = "";
            this.lueDoct.Properties.Essential = false;
            this.lueDoct.Properties.FieldName = null;
            this.lueDoct.Properties.FilterColumn = null;
            this.lueDoct.Properties.ForbidPoput = false;
            this.lueDoct.Properties.HideColumn = null;
            this.lueDoct.Properties.IsAutoPopup = false;
            this.lueDoct.Properties.IsCheckValid = true;
            this.lueDoct.Properties.IsDescField = false;
            this.lueDoct.Properties.IsFreeInput = false;
            this.lueDoct.Properties.IsHideValueColumn = false;
            this.lueDoct.Properties.IsSelectedMoveNextControl = false;
            this.lueDoct.Properties.IsShowColumnHeaders = false;
            this.lueDoct.Properties.IsShowDescInfo = false;
            this.lueDoct.Properties.IsShowRowNo = false;
            this.lueDoct.Properties.IsTab = true;
            this.lueDoct.Properties.IsUseShowColumn = false;
            this.lueDoct.Properties.ParentBandedGridView = null;
            this.lueDoct.Properties.ParentBindingSource = null;
            this.lueDoct.Properties.ParentGridView = null;
            this.lueDoct.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueDoct.Properties.PopupHeight = 0;
            this.lueDoct.Properties.PopupSizeable = false;
            this.lueDoct.Properties.PopupWidth = 0;
            this.lueDoct.Properties.PresentationMode = 0;
            this.lueDoct.Properties.ShowColumn = null;
            this.lueDoct.Properties.ShowPopupCloseButton = false;
            this.lueDoct.Properties.ShowPopupShadow = false;
            this.lueDoct.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueDoct.Properties.ValueColumn = null;
            this.lueDoct.Size = new System.Drawing.Size(100, 20);
            this.lueDoct.TabIndex = 112;
            this.lueDoct.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDoct_HandleDBValueChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 22);
            this.label4.TabIndex = 111;
            this.label4.Text = "医师:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDoct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lueDoct);
            this.Controls.Add(this.label4);
            this.Name = "ucDoct";
            this.Size = new System.Drawing.Size(151, 24);
            this.Load += new System.EventHandler(this.ucDoct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal LookUpEdit lueDoct;
        private System.Windows.Forms.Label label4;
    }
}
