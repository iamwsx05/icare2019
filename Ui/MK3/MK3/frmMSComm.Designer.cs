﻿namespace MK3
{
    partial class frmMSComm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMSComm));
            this.axMSComm = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm)).BeginInit();
            this.SuspendLayout();
            // 
            // axMSComm
            // 
            this.axMSComm.Enabled = true;
            this.axMSComm.Location = new System.Drawing.Point(0, 0);
            this.axMSComm.Name = "axMSComm";
            this.axMSComm.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm.OcxState")));
            this.axMSComm.Size = new System.Drawing.Size(38, 38);
            this.axMSComm.TabIndex = 0;
            // 
            // frmMSComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(116, 49);
            this.ControlBox = false;
            this.Controls.Add(this.axMSComm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMSComm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ms comm";
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxMSCommLib.AxMSComm axMSComm;


    }
}