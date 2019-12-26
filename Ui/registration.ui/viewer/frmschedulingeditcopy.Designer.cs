namespace Registration.Ui
{
    partial class frmSchedulingEditCopy
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboWeek = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chk1 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chk2 = new DevExpress.XtraEditors.CheckEdit();
            this.chk3 = new DevExpress.XtraEditors.CheckEdit();
            this.chk4 = new DevExpress.XtraEditors.CheckEdit();
            this.chk5 = new DevExpress.XtraEditors.CheckEdit();
            this.chk6 = new DevExpress.XtraEditors.CheckEdit();
            this.chk7 = new DevExpress.XtraEditors.CheckEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboWeek.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk7.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.btnCancel);
            this.pcBackGround.Controls.Add(this.btnOk);
            this.pcBackGround.Controls.Add(this.chk7);
            this.pcBackGround.Controls.Add(this.chk6);
            this.pcBackGround.Controls.Add(this.chk5);
            this.pcBackGround.Controls.Add(this.chk4);
            this.pcBackGround.Controls.Add(this.chk3);
            this.pcBackGround.Controls.Add(this.chk2);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Controls.Add(this.chk1);
            this.pcBackGround.Controls.Add(this.cboWeek);
            this.pcBackGround.Controls.Add(this.labelControl1);
            this.pcBackGround.Size = new System.Drawing.Size(246, 326);
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(22, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "从：";
            // 
            // cboWeek
            // 
            this.cboWeek.EnterMoveNextControl = true;
            this.cboWeek.Location = new System.Drawing.Point(48, 12);
            this.cboWeek.MinimumSize = new System.Drawing.Size(0, 22);
            this.cboWeek.Name = "cboWeek";
            this.cboWeek.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboWeek.Properties.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.cboWeek.Properties.Appearance.Options.UseFont = true;
            this.cboWeek.Properties.Appearance.Options.UseForeColor = true;
            this.cboWeek.Properties.Appearance.Options.UseTextOptions = true;
            this.cboWeek.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cboWeek.Properties.AppearanceDisabled.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.cboWeek.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Crimson;
            this.cboWeek.Properties.AppearanceDisabled.Options.UseFont = true;
            this.cboWeek.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.cboWeek.Properties.AppearanceDropDown.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.cboWeek.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Crimson;
            this.cboWeek.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboWeek.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this.cboWeek.Properties.AppearanceFocused.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.cboWeek.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Crimson;
            this.cboWeek.Properties.AppearanceFocused.Options.UseFont = true;
            this.cboWeek.Properties.AppearanceFocused.Options.UseForeColor = true;
            this.cboWeek.Properties.AutoHeight = false;
            this.cboWeek.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboWeek.Properties.DropDownItemHeight = 22;
            this.cboWeek.Properties.Items.AddRange(new object[] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期日"});
            this.cboWeek.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboWeek.Size = new System.Drawing.Size(92, 22);
            this.cboWeek.TabIndex = 1;
            this.cboWeek.SelectedIndexChanged += new System.EventHandler(this.cboWeek_SelectedIndexChanged);
            // 
            // chk1
            // 
            this.chk1.Location = new System.Drawing.Point(46, 49);
            this.chk1.Name = "chk1";
            this.chk1.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk1.Properties.Appearance.Options.UseFont = true;
            this.chk1.Properties.Caption = "星期一";
            this.chk1.Size = new System.Drawing.Size(75, 19);
            this.chk1.TabIndex = 2;
            this.chk1.Tag = "0";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(22, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "到：";
            // 
            // chk2
            // 
            this.chk2.Location = new System.Drawing.Point(46, 88);
            this.chk2.Name = "chk2";
            this.chk2.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk2.Properties.Appearance.Options.UseFont = true;
            this.chk2.Properties.Caption = "星期二";
            this.chk2.Size = new System.Drawing.Size(75, 19);
            this.chk2.TabIndex = 4;
            this.chk2.Tag = "1";
            // 
            // chk3
            // 
            this.chk3.Location = new System.Drawing.Point(46, 127);
            this.chk3.Name = "chk3";
            this.chk3.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk3.Properties.Appearance.Options.UseFont = true;
            this.chk3.Properties.Caption = "星期三";
            this.chk3.Size = new System.Drawing.Size(75, 19);
            this.chk3.TabIndex = 5;
            this.chk3.Tag = "2";
            // 
            // chk4
            // 
            this.chk4.Location = new System.Drawing.Point(46, 166);
            this.chk4.Name = "chk4";
            this.chk4.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk4.Properties.Appearance.Options.UseFont = true;
            this.chk4.Properties.Caption = "星期四";
            this.chk4.Size = new System.Drawing.Size(75, 19);
            this.chk4.TabIndex = 6;
            this.chk4.Tag = "3";
            // 
            // chk5
            // 
            this.chk5.Location = new System.Drawing.Point(46, 205);
            this.chk5.Name = "chk5";
            this.chk5.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk5.Properties.Appearance.Options.UseFont = true;
            this.chk5.Properties.Caption = "星期五";
            this.chk5.Size = new System.Drawing.Size(75, 19);
            this.chk5.TabIndex = 7;
            this.chk5.Tag = "4";
            // 
            // chk6
            // 
            this.chk6.Location = new System.Drawing.Point(46, 244);
            this.chk6.Name = "chk6";
            this.chk6.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk6.Properties.Appearance.Options.UseFont = true;
            this.chk6.Properties.Caption = "星期六";
            this.chk6.Size = new System.Drawing.Size(75, 19);
            this.chk6.TabIndex = 8;
            this.chk6.Tag = "5";
            // 
            // chk7
            // 
            this.chk7.Location = new System.Drawing.Point(46, 283);
            this.chk7.Name = "chk7";
            this.chk7.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk7.Properties.Appearance.Options.UseFont = true;
            this.chk7.Properties.Caption = "星期日";
            this.chk7.Size = new System.Drawing.Size(75, 19);
            this.chk7.TabIndex = 9;
            this.chk7.Tag = "6";
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(164, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定 &O";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(164, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消 &C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSchedulingEditCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 326);
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedulingEditCopy";
            this.Text = "复制";
            this.Load += new System.EventHandler(this.frmSchedulingEditCopy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboWeek.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk7.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.ComboBoxEdit cboWeek;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.CheckEdit chk7;
        private DevExpress.XtraEditors.CheckEdit chk6;
        private DevExpress.XtraEditors.CheckEdit chk5;
        private DevExpress.XtraEditors.CheckEdit chk4;
        private DevExpress.XtraEditors.CheckEdit chk3;
        private DevExpress.XtraEditors.CheckEdit chk2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit chk1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}