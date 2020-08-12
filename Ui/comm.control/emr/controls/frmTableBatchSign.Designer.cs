namespace Common.Controls.Emr
{
    partial class frmTableBatchSign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTableBatchSign));
            this.txtToRowNo = new DevExpress.XtraEditors.TextEdit();
            this.txtFromRowNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboSignColName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ctlSignature = new Common.Controls.Emr.ctlSignature();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtToRowNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromRowNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSignColName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtToRowNo
            // 
            this.txtToRowNo.EditValue = "";
            this.txtToRowNo.Location = new System.Drawing.Point(161, 20);
            this.txtToRowNo.Name = "txtToRowNo";
            this.txtToRowNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtToRowNo.Properties.Appearance.Options.UseFont = true;
            this.txtToRowNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtToRowNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtToRowNo.Size = new System.Drawing.Size(46, 20);
            this.txtToRowNo.TabIndex = 2;
            // 
            // txtFromRowNo
            // 
            this.txtFromRowNo.EditValue = "";
            this.txtFromRowNo.Location = new System.Drawing.Point(77, 20);
            this.txtFromRowNo.Name = "txtFromRowNo";
            this.txtFromRowNo.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFromRowNo.Properties.Appearance.Options.UseFont = true;
            this.txtFromRowNo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtFromRowNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtFromRowNo.Size = new System.Drawing.Size(46, 20);
            this.txtFromRowNo.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl5.Location = new System.Drawing.Point(211, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(13, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "行";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl3.Location = new System.Drawing.Point(125, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "行 到";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 9.5F);
            this.labelControl4.Location = new System.Drawing.Point(61, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(13, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "从";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(251, 180);
            this.panelControl1.TabIndex = 7;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.cboSignColName);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.txtFromRowNo);
            this.panelControl2.Controls.Add(this.txtToRowNo);
            this.panelControl2.Controls.Add(this.ctlSignature);
            this.panelControl2.Location = new System.Drawing.Point(3, 3);
            this.panelControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(243, 144);
            this.panelControl2.TabIndex = 22;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Location = new System.Drawing.Point(25, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(33, 12);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "范围:";
            // 
            // cboSignColName
            // 
            this.cboSignColName.Location = new System.Drawing.Point(71, 62);
            this.cboSignColName.Name = "cboSignColName";
            this.cboSignColName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            this.cboSignColName.Properties.Appearance.Options.UseFont = true;
            this.cboSignColName.Properties.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSignColName.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboSignColName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSignColName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboSignColName.Size = new System.Drawing.Size(146, 20);
            this.cboSignColName.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl6.Location = new System.Drawing.Point(25, 106);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(33, 12);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "签名:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl2.Location = new System.Drawing.Point(25, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 12);
            this.labelControl2.TabIndex = 17;
            this.labelControl2.Text = "列名:";
            // 
            // ctlSignature
            // 
            this.ctlSignature.BackColor = System.Drawing.Color.Transparent;
            this.ctlSignature.Caption = "";
            //this.ctlSignature.DefaultRows = 1;
            this.ctlSignature.EditObject = "";
            this.ctlSignature.Essential = false;
            this.ctlSignature.EssentialGroupNo = null;
            this.ctlSignature.Font = new System.Drawing.Font("宋体", 12F);
            this.ctlSignature.IsAllowSignNull = 0;
            this.ctlSignature.IsAutoSignature = 0;
            this.ctlSignature.ItemCaption = null;
            this.ctlSignature.ItemName = null;
            this.ctlSignature.ItemType = null;
            this.ctlSignature.LineStyle = Common.Controls.CtlLineStyle.Solid;
            this.ctlSignature.Location = new System.Drawing.Point(66, 101);
            this.ctlSignature.MaskType = 0;
            this.ctlSignature.Name = "ctlSignature";
            this.ctlSignature.ParentNode = null;
            this.ctlSignature.PresentationMode = 1;
            this.ctlSignature.Referencetype = true;
            this.ctlSignature.RegisterID = "";
            this.ctlSignature.RunTimeReadOnly = true;
            this.ctlSignature.ShowUnderLine = true;
            this.ctlSignature.Size = new System.Drawing.Size(151, 24);
            this.ctlSignature.TabIndex = 4;
            this.ctlSignature.TableFlag = false;
            this.ctlSignature.TextFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctlSignature.ValueChangedFlag = false;
            this.ctlSignature.Visible4Design = true;
            this.ctlSignature.ZIndex = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Location = new System.Drawing.Point(178, 152);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(108, 152);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(59, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmTableBatchSign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 180);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTableBatchSign";
            this.Text = "批量签名";
            this.Load += new System.EventHandler(this.frmTableBatchSign_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTableBatchSign_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtToRowNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromRowNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSignColName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtToRowNo;
        private DevExpress.XtraEditors.TextEdit txtFromRowNo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cboSignColName;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private ctlSignature ctlSignature;
    }
}