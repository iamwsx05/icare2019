namespace Registration.Ui
{
    partial class frmRegisterStat3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.rdoType = new DevExpress.XtraEditors.RadioGroup();
            this.ucPrintControl = new Common.Controls.ucPrintControl();
            this.lueDept = new Common.Controls.LookUpEdit();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.lueDoct = new Common.Controls.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.lueDoct);
            this.pcBackGround.Controls.Add(this.labelControl3);
            this.pcBackGround.Controls.Add(this.lueDept);
            this.pcBackGround.Controls.Add(this.dteEnd);
            this.pcBackGround.Controls.Add(this.dteStart);
            this.pcBackGround.Controls.Add(this.rdoType);
            this.pcBackGround.Controls.Add(this.labelControl7);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(1017, 35);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(11, 12);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(54, 12);
            this.labelControl7.TabIndex = 59;
            this.labelControl7.Text = "预约日期:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(180, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(7, 12);
            this.labelControl2.TabIndex = 58;
            this.labelControl2.Text = "~";
            // 
            // rdoType
            // 
            this.rdoType.Location = new System.Drawing.Point(324, 4);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoType.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.Appearance.Options.UseFont = true;
            this.rdoType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全院"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "科室：")});
            this.rdoType.Size = new System.Drawing.Size(120, 28);
            this.rdoType.TabIndex = 60;
            this.rdoType.SelectedIndexChanged += new System.EventHandler(this.rdoType_SelectedIndexChanged);
            // 
            // ucPrintControl
            // 
            this.ucPrintControl.Caption = null;
            this.ucPrintControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPrintControl.IsDockFill = true;
            this.ucPrintControl.IsReloadDictionary = false;
            this.ucPrintControl.IsSave = false;
            this.ucPrintControl.Location = new System.Drawing.Point(0, 35);
            this.ucPrintControl.Name = "ucPrintControl";
            this.ucPrintControl.PrintingSystem = null;
            this.ucPrintControl.ShowStatusBar = false;
            this.ucPrintControl.ShowToolBar = true;
            this.ucPrintControl.Size = new System.Drawing.Size(1017, 567);
            this.ucPrintControl.TabIndex = 11;
            this.ucPrintControl.ValueChanged = false;
            // 
            // lueDept
            // 
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.IsButtonFind = false;
            this.lueDept.Location = new System.Drawing.Point(441, 7);
            this.lueDept.Name = "lueDept";
            this.lueDept.ParentBandedGridView = null;
            this.lueDept.ParentBindingSource = null;
            this.lueDept.ParentGridView = null;
            this.lueDept.Properties.Appearance.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lueDept.Size = new System.Drawing.Size(135, 22);
            this.lueDept.TabIndex = 72;
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(196, 7);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteEnd.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteEnd.Properties.Appearance.Options.UseFont = true;
            this.dteEnd.Properties.Appearance.Options.UseForeColor = true;
            this.dteEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteEnd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteEnd.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteEnd.Size = new System.Drawing.Size(100, 22);
            this.dteEnd.TabIndex = 71;
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(72, 7);
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.dteStart.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.dteStart.Properties.Appearance.Options.UseFont = true;
            this.dteStart.Properties.Appearance.Options.UseForeColor = true;
            this.dteStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStart.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dteStart.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dteStart.Size = new System.Drawing.Size(100, 22);
            this.dteStart.TabIndex = 70;
            // 
            // lueDoct
            // 
            this.lueDoct.CellValueChanged = true;
            this.lueDoct.EditValue = "";
            this.lueDoct.IsButtonFind = false;
            this.lueDoct.Location = new System.Drawing.Point(640, 7);
            this.lueDoct.Name = "lueDoct";
            this.lueDoct.ParentBandedGridView = null;
            this.lueDoct.ParentBindingSource = null;
            this.lueDoct.ParentGridView = null;
            this.lueDoct.Properties.Appearance.Font = new System.Drawing.Font("黑体", 12F);
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
            this.lueDoct.Size = new System.Drawing.Size(135, 22);
            this.lueDoct.TabIndex = 74;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(603, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 12);
            this.labelControl3.TabIndex = 73;
            this.labelControl3.Text = "医师：";
            // 
            // frmRegisterStat3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 602);
            this.Controls.Add(this.ucPrintControl);
            this.Name = "frmRegisterStat3";
            this.Text = "预约清单";
            this.Load += new System.EventHandler(this.frmRegisterStat3_Load);
            this.Controls.SetChildIndex(this.marqueeProgressBarControl, 0);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.ucPrintControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.RadioGroup rdoType;
        internal Common.Controls.LookUpEdit lueDept;
        internal DevExpress.XtraEditors.DateEdit dteEnd;
        internal DevExpress.XtraEditors.DateEdit dteStart;
        internal Common.Controls.ucPrintControl ucPrintControl;
        internal Common.Controls.LookUpEdit lueDoct;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}