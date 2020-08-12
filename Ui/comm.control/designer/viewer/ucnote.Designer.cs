namespace Common.Controls
{
    partial class ucNote
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNote));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnReply = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.colorEdit = new DevExpress.XtraEditors.ColorEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fontBig = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fontEdit = new DevExpress.XtraEditors.FontEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnU = new DevExpress.XtraEditors.SimpleButton();
            this.btnI = new DevExpress.XtraEditors.SimpleButton();
            this.btnB = new DevExpress.XtraEditors.SimpleButton();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblReason = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontBig.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblReason);
            this.panelControl1.Controls.Add(this.btnReply);
            this.panelControl1.Controls.Add(this.panel3);
            this.panelControl1.Controls.Add(this.panel2);
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Controls.Add(this.btnU);
            this.panelControl1.Controls.Add(this.btnI);
            this.panelControl1.Controls.Add(this.btnB);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(703, 22);
            this.panelControl1.TabIndex = 0;
            // 
            // btnReply
            // 
            this.btnReply.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReply.Appearance.Options.UseFont = true;
            this.btnReply.Image = ((System.Drawing.Image)(resources.GetObject("btnReply.Image")));
            this.btnReply.Location = new System.Drawing.Point(515, 1);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(79, 20);
            this.btnReply.TabIndex = 18;
            this.btnReply.Text = "回复 &R";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.colorEdit);
            this.panel3.Controls.Add(this.labelControl3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(324, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(184, 18);
            this.panel3.TabIndex = 5;
            // 
            // colorEdit
            // 
            this.colorEdit.EditValue = System.Drawing.Color.Empty;
            this.colorEdit.Location = new System.Drawing.Point(36, 0);
            this.colorEdit.Name = "colorEdit";
            this.colorEdit.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.colorEdit.Properties.Appearance.Options.UseFont = true;
            this.colorEdit.Properties.AppearanceFocused.Font = new System.Drawing.Font("宋体", 9F);
            this.colorEdit.Properties.AppearanceFocused.Options.UseFont = true;
            this.colorEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorEdit.Size = new System.Drawing.Size(140, 18);
            this.colorEdit.TabIndex = 1;
            this.colorEdit.EditValueChanged += new System.EventHandler(this.colorEdit_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(5, 3);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 12);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "颜色:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fontBig);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(200, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 18);
            this.panel2.TabIndex = 4;
            // 
            // fontBig
            // 
            this.fontBig.EditValue = "12";
            this.fontBig.Location = new System.Drawing.Point(36, 0);
            this.fontBig.Name = "fontBig";
            this.fontBig.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontBig.Properties.Appearance.Options.UseFont = true;
            this.fontBig.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fontBig.Properties.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
            this.fontBig.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.fontBig.Size = new System.Drawing.Size(85, 18);
            this.fontBig.TabIndex = 1;
            this.fontBig.SelectedIndexChanged += new System.EventHandler(this.fontBig_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(5, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(30, 12);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "大小:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fontEdit);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(80, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 18);
            this.panel1.TabIndex = 3;
            // 
            // fontEdit
            // 
            this.fontEdit.EditValue = "宋体";
            this.fontEdit.Location = new System.Drawing.Point(35, 0);
            this.fontEdit.Name = "fontEdit";
            this.fontEdit.Properties.Appearance.Font = new System.Drawing.Font("宋体", 9F);
            this.fontEdit.Properties.Appearance.Options.UseFont = true;
            this.fontEdit.Properties.AppearanceFocused.Font = new System.Drawing.Font("宋体", 9F);
            this.fontEdit.Properties.AppearanceFocused.Options.UseFont = true;
            this.fontEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fontEdit.Size = new System.Drawing.Size(85, 18);
            this.fontEdit.TabIndex = 1;
            this.fontEdit.SelectedIndexChanged += new System.EventHandler(this.fontEdit_SelectedIndexChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "字体:";
            // 
            // btnU
            // 
            this.btnU.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU.Appearance.Options.UseFont = true;
            this.btnU.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnU.Image = ((System.Drawing.Image)(resources.GetObject("btnU.Image")));
            this.btnU.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnU.Location = new System.Drawing.Point(54, 2);
            this.btnU.Name = "btnU";
            this.btnU.Size = new System.Drawing.Size(26, 18);
            this.btnU.TabIndex = 2;
            this.btnU.Click += new System.EventHandler(this.btnU_Click);
            // 
            // btnI
            // 
            this.btnI.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnI.Appearance.Options.UseFont = true;
            this.btnI.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnI.Image = ((System.Drawing.Image)(resources.GetObject("btnI.Image")));
            this.btnI.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnI.Location = new System.Drawing.Point(28, 2);
            this.btnI.Name = "btnI";
            this.btnI.Size = new System.Drawing.Size(26, 18);
            this.btnI.TabIndex = 1;
            this.btnI.Click += new System.EventHandler(this.btnI_Click);
            // 
            // btnB
            // 
            this.btnB.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.Appearance.Options.UseFont = true;
            this.btnB.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnB.Image = ((System.Drawing.Image)(resources.GetObject("btnB.Image")));
            this.btnB.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnB.Location = new System.Drawing.Point(2, 2);
            this.btnB.Name = "btnB";
            this.btnB.Size = new System.Drawing.Size(26, 18);
            this.btnB.TabIndex = 0;
            this.btnB.Click += new System.EventHandler(this.btnB_Click);
            // 
            // rtbNote
            // 
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNote.Font = new System.Drawing.Font("宋体", 12F);
            this.rtbNote.Location = new System.Drawing.Point(2, 2);
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.Size = new System.Drawing.Size(699, 183);
            this.rtbNote.TabIndex = 1;
            this.rtbNote.Text = "";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.rtbNote);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 22);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(703, 187);
            this.panelControl2.TabIndex = 2;
            // 
            // lblReason
            // 
            this.lblReason.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.lblReason.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.lblReason.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblReason.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblReason.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblReason.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblReason.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblReason.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblReason.Location = new System.Drawing.Point(616, 2);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(85, 18);
            this.lblReason.TabIndex = 121;
            this.lblReason.Text = "咨询问题";
            // 
            // ucNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucNote";
            this.Size = new System.Drawing.Size(703, 209);
            this.Load += new System.EventHandler(this.ucNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorEdit.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontBig.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.FontEdit fontEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnI;
        private DevExpress.XtraEditors.SimpleButton btnB;
        private System.Windows.Forms.RichTextBox rtbNote;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnU;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.ColorEdit colorEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit fontBig;
        public DevExpress.XtraEditors.SimpleButton btnReply;
        internal DevExpress.XtraEditors.LabelControl lblReason;
    }
}
