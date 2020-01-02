namespace Registration.Ui
{
    partial class frmSchedulingImport
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoLtFlag = new DevExpress.XtraEditors.RadioGroup();
            this.lueDoct = new Common.Controls.LookUpEdit();
            this.chkDoct = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.rdoType = new DevExpress.XtraEditors.RadioGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.chkDept = new DevExpress.XtraEditors.CheckEdit();
            this.chkRoom = new DevExpress.XtraEditors.CheckEdit();
            this.lueDept = new Common.Controls.LookUpEdit();
            this.lueRoom = new Common.Controls.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLtFlag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDoct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRoom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnCancel);
            this.pcBackGround.Controls.Add(this.btnOk);
            this.pcBackGround.Controls.Add(this.groupBox1);
            this.pcBackGround.Size = new System.Drawing.Size(236, 445);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lueRoom);
            this.groupBox1.Controls.Add(this.lueDept);
            this.groupBox1.Controls.Add(this.chkRoom);
            this.groupBox1.Controls.Add(this.chkDept);
            this.groupBox1.Controls.Add(this.rdoLtFlag);
            this.groupBox1.Controls.Add(this.lueDoct);
            this.groupBox1.Controls.Add(this.chkDoct);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dteEnd);
            this.groupBox1.Controls.Add(this.dteStart);
            this.groupBox1.Controls.Add(this.rdoType);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox1.Location = new System.Drawing.Point(6, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 391);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间选项";
            // 
            // rdoLtFlag
            // 
            this.rdoLtFlag.Location = new System.Drawing.Point(117, 0);
            this.rdoLtFlag.Name = "rdoLtFlag";
            this.rdoLtFlag.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoLtFlag.Properties.Appearance.Options.UseFont = true;
            this.rdoLtFlag.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "  临时排班"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "  长期排班")});
            this.rdoLtFlag.Properties.LookAndFeel.SkinName = "Office 2013";
            this.rdoLtFlag.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.rdoLtFlag.Size = new System.Drawing.Size(100, 56);
            this.rdoLtFlag.TabIndex = 97;
            // 
            // lueDoct
            // 
            this.lueDoct.CellValueChanged = true;
            this.lueDoct.EditValue = "";
            this.lueDoct.IsButtonFind = false;
            this.lueDoct.Location = new System.Drawing.Point(117, 284);
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
            this.lueDoct.Size = new System.Drawing.Size(100, 22);
            this.lueDoct.TabIndex = 73;
            // 
            // chkDoct
            // 
            this.chkDoct.Location = new System.Drawing.Point(29, 284);
            this.chkDoct.Name = "chkDoct";
            this.chkDoct.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDoct.Properties.Appearance.Options.UseFont = true;
            this.chkDoct.Properties.Caption = " 指定医师：";
            this.chkDoct.Size = new System.Drawing.Size(84, 19);
            this.chkDoct.TabIndex = 72;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(91, 255);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 71;
            this.labelControl2.Text = "到：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(91, 227);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 70;
            this.labelControl1.Text = "从：";
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Enabled = false;
            this.dteEnd.Location = new System.Drawing.Point(117, 251);
            this.dteEnd.Name = "dteEnd";
            this.dteEnd.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
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
            this.dteEnd.TabIndex = 69;
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Enabled = false;
            this.dteStart.Location = new System.Drawing.Point(117, 223);
            this.dteStart.Name = "dteStart";
            this.dteStart.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
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
            this.dteStart.TabIndex = 68;
            // 
            // rdoType
            // 
            this.rdoType.Location = new System.Drawing.Point(12, 20);
            this.rdoType.Name = "rdoType";
            this.rdoType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdoType.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoType.Properties.Appearance.Options.UseFont = true;
            this.rdoType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdoType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "明天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "本周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "下周"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "本月"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "下月"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "指定时间")});
            this.rdoType.Size = new System.Drawing.Size(224, 240);
            this.rdoType.TabIndex = 0;
            this.rdoType.SelectedIndexChanged += new System.EventHandler(this.rdoType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(131, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 61;
            this.btnCancel.Text = "取消 &C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(24, 412);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 24);
            this.btnOk.TabIndex = 60;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkDept
            // 
            this.chkDept.Location = new System.Drawing.Point(29, 320);
            this.chkDept.Name = "chkDept";
            this.chkDept.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDept.Properties.Appearance.Options.UseFont = true;
            this.chkDept.Properties.Caption = " 指定科室：";
            this.chkDept.Size = new System.Drawing.Size(84, 19);
            this.chkDept.TabIndex = 98;
            // 
            // chkRoom
            // 
            this.chkRoom.Location = new System.Drawing.Point(29, 356);
            this.chkRoom.Name = "chkRoom";
            this.chkRoom.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRoom.Properties.Appearance.Options.UseFont = true;
            this.chkRoom.Properties.Caption = " 指定诊间：";
            this.chkRoom.Size = new System.Drawing.Size(84, 19);
            this.chkRoom.TabIndex = 99;
            // 
            // lueDept
            // 
            this.lueDept.CellValueChanged = true;
            this.lueDept.EditValue = "";
            this.lueDept.EnterMoveNextControl = true;
            this.lueDept.IsButtonFind = false;
            this.lueDept.Location = new System.Drawing.Point(117, 320);
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
            this.lueDept.Size = new System.Drawing.Size(100, 20);
            this.lueDept.TabIndex = 100;
            this.lueDept.HandleDBValueChanged += new Common.Controls._HandleDBValueChanged(this.lueDept_HandleDBValueChanged);
            // 
            // lueRoom
            // 
            this.lueRoom.CellValueChanged = true;
            this.lueRoom.EditValue = "";
            this.lueRoom.EnterMoveNextControl = true;
            this.lueRoom.IsButtonFind = false;
            this.lueRoom.Location = new System.Drawing.Point(117, 356);
            this.lueRoom.Name = "lueRoom";
            this.lueRoom.ParentBandedGridView = null;
            this.lueRoom.ParentBindingSource = null;
            this.lueRoom.ParentGridView = null;
            this.lueRoom.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.lueRoom.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lueRoom.Properties.Appearance.Options.UseFont = true;
            this.lueRoom.Properties.Appearance.Options.UseForeColor = true;
            this.lueRoom.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.lueRoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueRoom.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lueRoom.Properties.DataSource = null;
            this.lueRoom.Properties.DBRow = null;
            this.lueRoom.Properties.DBValue = "";
            this.lueRoom.Properties.DescCode = null;
            this.lueRoom.Properties.DisplayColumn = null;
            this.lueRoom.Properties.DisplayValue = "";
            this.lueRoom.Properties.Essential = false;
            this.lueRoom.Properties.FieldName = null;
            this.lueRoom.Properties.FilterColumn = null;
            this.lueRoom.Properties.ForbidPoput = false;
            this.lueRoom.Properties.HideColumn = null;
            this.lueRoom.Properties.IsAutoPopup = false;
            this.lueRoom.Properties.IsCheckValid = true;
            this.lueRoom.Properties.IsDescField = false;
            this.lueRoom.Properties.IsFreeInput = false;
            this.lueRoom.Properties.IsHideValueColumn = false;
            this.lueRoom.Properties.IsSelectedMoveNextControl = false;
            this.lueRoom.Properties.IsShowColumnHeaders = false;
            this.lueRoom.Properties.IsShowDescInfo = false;
            this.lueRoom.Properties.IsShowRowNo = false;
            this.lueRoom.Properties.IsTab = true;
            this.lueRoom.Properties.IsUseShowColumn = false;
            this.lueRoom.Properties.ParentBandedGridView = null;
            this.lueRoom.Properties.ParentBindingSource = null;
            this.lueRoom.Properties.ParentGridView = null;
            this.lueRoom.Properties.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.lueRoom.Properties.PopupFormSize = new System.Drawing.Size(10, 10);
            this.lueRoom.Properties.PopupHeight = 0;
            this.lueRoom.Properties.PopupSizeable = false;
            this.lueRoom.Properties.PopupWidth = 0;
            this.lueRoom.Properties.PresentationMode = 0;
            this.lueRoom.Properties.ShowColumn = null;
            this.lueRoom.Properties.ShowPopupCloseButton = false;
            this.lueRoom.Properties.ShowPopupShadow = false;
            this.lueRoom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueRoom.Properties.ValueColumn = null;
            this.lueRoom.Size = new System.Drawing.Size(100, 20);
            this.lueRoom.TabIndex = 101;
            // 
            // frmSchedulingImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 445);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedulingImport";
            this.Text = "导入排班计划(从明日及以后)";
            this.Load += new System.EventHandler(this.frmSchedulingImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoLtFlag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDoct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDoct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueRoom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.RadioGroup rdoType;
        internal DevExpress.XtraEditors.SimpleButton btnCancel;
        internal DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dteEnd;
        private DevExpress.XtraEditors.DateEdit dteStart;
        private DevExpress.XtraEditors.CheckEdit chkDoct;
        internal Common.Controls.LookUpEdit lueDoct;
        internal DevExpress.XtraEditors.RadioGroup rdoLtFlag;
        private DevExpress.XtraEditors.CheckEdit chkRoom;
        private DevExpress.XtraEditors.CheckEdit chkDept;
        internal Common.Controls.LookUpEdit lueDept;
        internal Common.Controls.LookUpEdit lueRoom;
    }
}