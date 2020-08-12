namespace Common.Controls.Emr
{
    partial class frmSetSumRowScopes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetSumRowScopes));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtToRowNo = new DevExpress.XtraEditors.TextEdit();
            this.txtFromRowNo = new DevExpress.XtraEditors.TextEdit();
            this.txtDateTime = new DevExpress.XtraEditors.TextEdit();
            this.txtColNo = new DevExpress.XtraEditors.TextEdit();
            this.txtMValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToRowNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromRowNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(294, 164);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(213, 164);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(59, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtToRowNo);
            this.panelControl1.Controls.Add(this.txtFromRowNo);
            this.panelControl1.Controls.Add(this.txtDateTime);
            this.panelControl1.Controls.Add(this.txtColNo);
            this.panelControl1.Controls.Add(this.txtMValue);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.radioGroup);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(373, 195);
            this.panelControl1.TabIndex = 2;
            // 
            // txtToRowNo
            // 
            this.txtToRowNo.EditValue = "";
            this.txtToRowNo.Location = new System.Drawing.Point(245, 67);
            this.txtToRowNo.Name = "txtToRowNo";
            this.txtToRowNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtToRowNo.Properties.Appearance.Options.UseFont = true;
            this.txtToRowNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtToRowNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtToRowNo.Size = new System.Drawing.Size(40, 20);
            this.txtToRowNo.TabIndex = 16;
            this.txtToRowNo.Enter += new System.EventHandler(this.txtToRowNo_Enter);
            // 
            // txtFromRowNo
            // 
            this.txtFromRowNo.EditValue = "";
            this.txtFromRowNo.Location = new System.Drawing.Point(162, 67);
            this.txtFromRowNo.Name = "txtFromRowNo";
            this.txtFromRowNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFromRowNo.Properties.Appearance.Options.UseFont = true;
            this.txtFromRowNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFromRowNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFromRowNo.Size = new System.Drawing.Size(40, 20);
            this.txtFromRowNo.TabIndex = 15;
            this.txtFromRowNo.Enter += new System.EventHandler(this.txtFromRowNo_Enter);
            // 
            // txtDateTime
            // 
            this.txtDateTime.EditValue = "07:00";
            this.txtDateTime.Location = new System.Drawing.Point(282, 21);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDateTime.Properties.Appearance.Options.UseFont = true;
            this.txtDateTime.Properties.Appearance.Options.UseTextOptions = true;
            this.txtDateTime.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtDateTime.Size = new System.Drawing.Size(60, 20);
            this.txtDateTime.TabIndex = 14;
            this.txtDateTime.Enter += new System.EventHandler(this.txtDateTime_Enter);
            // 
            // txtColNo
            // 
            this.txtColNo.EditValue = "2";
            this.txtColNo.Location = new System.Drawing.Point(162, 21);
            this.txtColNo.Name = "txtColNo";
            this.txtColNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtColNo.Properties.Appearance.Options.UseFont = true;
            this.txtColNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtColNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtColNo.Size = new System.Drawing.Size(40, 20);
            this.txtColNo.TabIndex = 13;
            this.txtColNo.Enter += new System.EventHandler(this.txtColNo_Enter);
            // 
            // txtMValue
            // 
            this.txtMValue.EditValue = "";
            this.txtMValue.Location = new System.Drawing.Point(116, 112);
            this.txtMValue.Name = "txtMValue";
            this.txtMValue.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMValue.Properties.Appearance.Options.UseFont = true;
            this.txtMValue.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtMValue.Size = new System.Drawing.Size(86, 20);
            this.txtMValue.TabIndex = 12;
            this.txtMValue.Enter += new System.EventHandler(this.txtMValue_Enter);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl5.Location = new System.Drawing.Point(291, 72);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(13, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "行";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl6.Location = new System.Drawing.Point(232, 25);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(39, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "时间点";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl3.Location = new System.Drawing.Point(206, 72);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "行 到";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl4.Location = new System.Drawing.Point(144, 72);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(13, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "从";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl2.Location = new System.Drawing.Point(206, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(13, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "列";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl1.Location = new System.Drawing.Point(144, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(13, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "第";
            // 
            // radioGroup
            // 
            this.radioGroup.Location = new System.Drawing.Point(4, 4);
            this.radioGroup.Name = "radioGroup";
            this.radioGroup.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.radioGroup.Properties.Appearance.Options.UseFont = true;
            this.radioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "指定列号、时间:"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "指定行号范围:"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "手工录入值:")});
            this.radioGroup.Size = new System.Drawing.Size(364, 148);
            this.radioGroup.TabIndex = 5;
            // 
            // frmSetSumRowScopes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 195);
            this.Controls.Add(this.panelControl1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetSumRowScopes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置求和行范围";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSetSumRowScopes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToRowNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromRowNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtColNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup;
        private DevExpress.XtraEditors.TextEdit txtMValue;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtColNo;
        private DevExpress.XtraEditors.TextEdit txtToRowNo;
        private DevExpress.XtraEditors.TextEdit txtFromRowNo;
        private DevExpress.XtraEditors.TextEdit txtDateTime;
    }
}