namespace Registration.Ui
{
    partial class frmRegisterStatRate
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
            this.dteEnd = new DevExpress.XtraEditors.DateEdit();
            this.dteStart = new DevExpress.XtraEditors.DateEdit();
            this.ucPrintControl = new Common.Controls.ucPrintControl();
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).BeginInit();
            this.pcBackGround.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBackGround
            // 
            this.pcBackGround.Controls.Add(this.labelControl7);
            this.pcBackGround.Controls.Add(this.labelControl2);
            this.pcBackGround.Controls.Add(this.dteEnd);
            this.pcBackGround.Controls.Add(this.dteStart);
            this.pcBackGround.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcBackGround.Location = new System.Drawing.Point(0, 0);
            this.pcBackGround.Size = new System.Drawing.Size(813, 35);
            this.pcBackGround.Visible = true;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Location = new System.Drawing.Point(14, 12);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(90, 12);
            this.labelControl7.TabIndex = 79;
            this.labelControl7.Text = "预约(出诊)日期:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(215, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(7, 12);
            this.labelControl2.TabIndex = 78;
            this.labelControl2.Text = "~";
            // 
            // dteEnd
            // 
            this.dteEnd.EditValue = null;
            this.dteEnd.Location = new System.Drawing.Point(229, 7);
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
            this.dteEnd.TabIndex = 77;
            // 
            // dteStart
            // 
            this.dteStart.EditValue = null;
            this.dteStart.Location = new System.Drawing.Point(108, 7);
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
            this.dteStart.TabIndex = 76;
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
            this.ucPrintControl.Size = new System.Drawing.Size(813, 434);
            this.ucPrintControl.TabIndex = 12;
            this.ucPrintControl.ValueChanged = false;
            // 
            // frmRegisterStatRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 469);
            this.Controls.Add(this.ucPrintControl);
            this.Name = "frmRegisterStatRate";
            this.Text = "预约率统计报表";
            this.Load += new System.EventHandler(this.frmRegisterStatRate_Load);
            this.Controls.SetChildIndex(this.pcBackGround, 0);
            this.Controls.SetChildIndex(this.ucPrintControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pcBackGround)).EndInit();
            this.pcBackGround.ResumeLayout(false);
            this.pcBackGround.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStart.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.DateEdit dteEnd;
        internal DevExpress.XtraEditors.DateEdit dteStart;
        internal Common.Controls.ucPrintControl ucPrintControl;
    }
}