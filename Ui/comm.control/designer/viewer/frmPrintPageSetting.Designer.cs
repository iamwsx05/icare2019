namespace Common.Controls
{
    partial class frmPrintPageSetting
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
            this.xtraLabelControl5 = new Common.Controls.xtraLabelControl();
            this.cboPrinterList = new Common.Controls.xtraComboBoxEdit();
            this.xtraLabelControl6 = new Common.Controls.xtraLabelControl();
            this.radioGroupCheck = new Common.Controls.xtraRadioGroup();
            this.txtPageScope = new Common.Controls.xtraTextEdit();
            this.xtraLabelControl7 = new Common.Controls.xtraLabelControl();
            this.spePageCopies = new Common.Controls.xtraSpinEdit();
            this.xtraLabelControl8 = new Common.Controls.xtraLabelControl();
            this.speStartIndex = new Common.Controls.xtraSpinEdit();
            this.btnOK = new Common.Controls.xtraSimpleButton();
            this.btnCancel = new Common.Controls.xtraSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinterList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spePageCopies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speStartIndex.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnCancel);
            this.pcBackGround.Controls.Add(this.btnOK);
            this.pcBackGround.Controls.Add(this.speStartIndex);
            this.pcBackGround.Controls.Add(this.spePageCopies);
            this.pcBackGround.Controls.Add(this.txtPageScope);
            this.pcBackGround.Controls.Add(this.radioGroupCheck);
            this.pcBackGround.Controls.Add(this.cboPrinterList);
            this.pcBackGround.Controls.Add(this.xtraLabelControl8);
            this.pcBackGround.Controls.Add(this.xtraLabelControl7);
            this.pcBackGround.Controls.Add(this.xtraLabelControl6);
            this.pcBackGround.Controls.Add(this.xtraLabelControl5);
            this.pcBackGround.Size = new System.Drawing.Size(330, 265);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // xtraLabelControl5
            // 
            this.xtraLabelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl5.Location = new System.Drawing.Point(23, 18);
            this.xtraLabelControl5.Name = "xtraLabelControl5";
            this.xtraLabelControl5.Size = new System.Drawing.Size(59, 13);
            this.xtraLabelControl5.TabIndex = 0;
            this.xtraLabelControl5.Text = "打印机名:";
            // 
            // cboPrinterList
            // 
            this.cboPrinterList.Location = new System.Drawing.Point(89, 16);
            this.cboPrinterList.Name = "cboPrinterList";
            this.cboPrinterList.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.cboPrinterList.Properties.Appearance.Options.UseFont = true;
            this.cboPrinterList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPrinterList.Size = new System.Drawing.Size(218, 20);
            this.cboPrinterList.TabIndex = 1;
            // 
            // xtraLabelControl6
            // 
            this.xtraLabelControl6.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl6.Location = new System.Drawing.Point(23, 49);
            this.xtraLabelControl6.Name = "xtraLabelControl6";
            this.xtraLabelControl6.Size = new System.Drawing.Size(59, 13);
            this.xtraLabelControl6.TabIndex = 0;
            this.xtraLabelControl6.Text = "打印范围:";
            // 
            // radioGroupCheck
            // 
            this.radioGroupCheck.Location = new System.Drawing.Point(24, 69);
            this.radioGroupCheck.Name = "radioGroupCheck";
            this.radioGroupCheck.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.radioGroupCheck.Properties.Appearance.Options.UseFont = true;
            this.radioGroupCheck.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全部页"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "奇数页"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "偶数页"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "指定页范围")});
            this.radioGroupCheck.Size = new System.Drawing.Size(282, 111);
            this.radioGroupCheck.TabIndex = 2;
            this.radioGroupCheck.SelectedIndexChanged += new System.EventHandler(this.radioGroupCheck_SelectedIndexChanged);
            // 
            // txtPageScope
            // 
            this.txtPageScope.Location = new System.Drawing.Point(120, 152);
            this.txtPageScope.Name = "txtPageScope";
            this.txtPageScope.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.txtPageScope.Properties.Appearance.Options.UseFont = true;
            this.txtPageScope.Size = new System.Drawing.Size(170, 20);
            this.txtPageScope.TabIndex = 3;
            // 
            // xtraLabelControl7
            // 
            this.xtraLabelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl7.Location = new System.Drawing.Point(24, 199);
            this.xtraLabelControl7.Name = "xtraLabelControl7";
            this.xtraLabelControl7.Size = new System.Drawing.Size(59, 13);
            this.xtraLabelControl7.TabIndex = 0;
            this.xtraLabelControl7.Text = "打印份数:";
            // 
            // spePageCopies
            // 
            this.spePageCopies.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spePageCopies.Location = new System.Drawing.Point(88, 196);
            this.spePageCopies.Name = "spePageCopies";
            this.spePageCopies.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.spePageCopies.Properties.Appearance.Options.UseFont = true;
            this.spePageCopies.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spePageCopies.Properties.IsFloatValue = false;
            this.spePageCopies.Properties.Mask.EditMask = "d";
            this.spePageCopies.Properties.MaxLength = 4;
            this.spePageCopies.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spePageCopies.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spePageCopies.Size = new System.Drawing.Size(60, 20);
            this.spePageCopies.TabIndex = 4;
            // 
            // xtraLabelControl8
            // 
            this.xtraLabelControl8.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.xtraLabelControl8.Location = new System.Drawing.Point(182, 199);
            this.xtraLabelControl8.Name = "xtraLabelControl8";
            this.xtraLabelControl8.Size = new System.Drawing.Size(59, 13);
            this.xtraLabelControl8.TabIndex = 0;
            this.xtraLabelControl8.Text = "起始页码:";
            // 
            // speStartIndex
            // 
            this.speStartIndex.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speStartIndex.Location = new System.Drawing.Point(246, 196);
            this.speStartIndex.Name = "speStartIndex";
            this.speStartIndex.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.speStartIndex.Properties.Appearance.Options.UseFont = true;
            this.speStartIndex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.speStartIndex.Properties.IsFloatValue = false;
            this.speStartIndex.Properties.Mask.EditMask = "d";
            this.speStartIndex.Properties.MaxLength = 4;
            this.speStartIndex.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.speStartIndex.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speStartIndex.Size = new System.Drawing.Size(60, 20);
            this.speStartIndex.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(69, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 21);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(182, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPrintPageSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(330, 265);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintPageSetting";
            this.Text = "页面设置";
            this.Load += new System.EventHandler(this.frmPrintPageSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPrinterList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPageScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spePageCopies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speStartIndex.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Controls.xtraComboBoxEdit cboPrinterList;
        private Common.Controls.xtraLabelControl xtraLabelControl5;
        private Common.Controls.xtraTextEdit txtPageScope;
        private Common.Controls.xtraRadioGroup radioGroupCheck;
        private Common.Controls.xtraLabelControl xtraLabelControl6;
        private Common.Controls.xtraSimpleButton btnCancel;
        private Common.Controls.xtraSimpleButton btnOK;
        private Common.Controls.xtraSpinEdit speStartIndex;
        private Common.Controls.xtraSpinEdit spePageCopies;
        private Common.Controls.xtraLabelControl xtraLabelControl8;
        private Common.Controls.xtraLabelControl xtraLabelControl7;
    }
}