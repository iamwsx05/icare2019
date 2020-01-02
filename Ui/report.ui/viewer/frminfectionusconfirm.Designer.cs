namespace Report.Ui
{
    partial class frmInfectionusConfirm
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
            this.btnDenial = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnDenial
            // 
            this.btnDenial.Location = new System.Drawing.Point(200, 184);
            this.btnDenial.Name = "btnDenial";
            this.btnDenial.Size = new System.Drawing.Size(112, 40);
            this.btnDenial.TabIndex = 3;
            this.btnDenial.Text = "否";
            this.btnDenial.Click += new System.EventHandler(this.btnDenial_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(24, 184);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(112, 40);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "是";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(40, 88);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(271, 19);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "审核通过请点‘是’，未通过请点‘否’！";
            // 
            // frmInfectionusConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 243);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnDenial);
            this.Controls.Add(this.btnConfirm);
            this.MaximizeBox = false;
            this.Name = "frmInfectionusConfirm";
            this.Text = "感染病例审核";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDenial;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}