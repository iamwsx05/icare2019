namespace com.digitalwave.iCare.BIHOrder
{
    partial class frmBloodPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBloodPopup));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
            this.ucBloodApplyForm2 = new com.digitalwave.iCare.BIHOrder.ucBloodApplyForm2();
            this.ucBloodApplyForm1 = new com.digitalwave.iCare.BIHOrder.ucBloodApplyForm1();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.xtraScrollableControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.xtraScrollableControl);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(796, 786);
            this.panelControl1.TabIndex = 0;
            // 
            // xtraScrollableControl
            // 
            this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.Silver;
            this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl.Controls.Add(this.ucBloodApplyForm2);
            this.xtraScrollableControl.Controls.Add(this.ucBloodApplyForm1);
            this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl.Location = new System.Drawing.Point(2, 2);
            this.xtraScrollableControl.Name = "xtraScrollableControl";
            this.xtraScrollableControl.Size = new System.Drawing.Size(792, 782);
            this.xtraScrollableControl.TabIndex = 12;
            // 
            // ucBloodApplyForm2
            // 
            this.ucBloodApplyForm2.BackColor = System.Drawing.Color.White;
            this.ucBloodApplyForm2.IsValueChange = false;
            this.ucBloodApplyForm2.Location = new System.Drawing.Point(92, 260);
            this.ucBloodApplyForm2.Name = "ucBloodApplyForm2";
            this.ucBloodApplyForm2.Size = new System.Drawing.Size(720, 996);
            this.ucBloodApplyForm2.TabIndex = 1;
            // 
            // ucBloodApplyForm1
            // 
            this.ucBloodApplyForm1.BackColor = System.Drawing.Color.White;
            this.ucBloodApplyForm1.IsValueChange = false;
            this.ucBloodApplyForm1.Location = new System.Drawing.Point(8, 16);
            this.ucBloodApplyForm1.Name = "ucBloodApplyForm1";
            this.ucBloodApplyForm1.Size = new System.Drawing.Size(720, 1004);
            this.ucBloodApplyForm1.TabIndex = 0;
            // 
            // frmBloodPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 786);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBloodPopup";
            this.Text = "申请单预览";
            this.Load += new System.EventHandler(this.frmBloodPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.xtraScrollableControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
        internal com.digitalwave.iCare.BIHOrder.ucBloodApplyForm2 ucBloodApplyForm2;
        internal com.digitalwave.iCare.BIHOrder.ucBloodApplyForm1 ucBloodApplyForm1;
    }
}