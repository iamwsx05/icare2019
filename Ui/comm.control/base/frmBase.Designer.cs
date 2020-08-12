namespace Common.Controls
{
    partial class frmBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBase));
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.marqueeProgressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // marqueeProgressBarControl
            // 
            this.marqueeProgressBarControl.EditValue = "请稍候...";
            this.marqueeProgressBarControl.Location = new System.Drawing.Point(-216, 4);
            this.marqueeProgressBarControl.Margin = new System.Windows.Forms.Padding(0);
            this.marqueeProgressBarControl.Name = "marqueeProgressBarControl";
            this.marqueeProgressBarControl.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marqueeProgressBarControl.Properties.ShowTitle = true;
            this.marqueeProgressBarControl.Size = new System.Drawing.Size(195, 25);
            this.marqueeProgressBarControl.TabIndex = 1;
            this.marqueeProgressBarControl.Visible = false;
            // 
            // frmBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 354);
            this.Controls.Add(this.marqueeProgressBarControl);
            this.Font = new System.Drawing.Font("宋体", 9.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标题";
            this.Activated += new System.EventHandler(this.frmBase_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBase_FormClosing);
            this.Load += new System.EventHandler(this.frmBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        public DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl;

    }
}