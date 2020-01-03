namespace com.digitalwave.iCare.gui.HIS 
{
    partial class frmIVDRISpecification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIVDRISpecification));
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.panel = new System.Windows.Forms.Panel();
            this.chkSyz8 = new System.Windows.Forms.CheckBox();
            this.chkSyz7 = new System.Windows.Forms.CheckBox();
            this.chkSyz6 = new System.Windows.Forms.CheckBox();
            this.chkSyz5 = new System.Windows.Forms.CheckBox();
            this.chkSyz4 = new System.Windows.Forms.CheckBox();
            this.chkSyz3 = new System.Windows.Forms.CheckBox();
            this.chkSyz2 = new System.Windows.Forms.CheckBox();
            this.chkSyz1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkType3 = new System.Windows.Forms.CheckBox();
            this.chkType2 = new System.Windows.Forms.CheckBox();
            this.chkType1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.btnClose = new PinkieControls.ButtonXP();
            this.btnSave = new PinkieControls.ButtonXP();
            this.txtSyz_tsqk = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint2 = new PinkieControls.ButtonXP();
            this.btnClose2 = new PinkieControls.ButtonXP();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.AutoZoom = false;
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Document = this.printDocument;
            this.printPreviewControl.Location = new System.Drawing.Point(3, 40);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(878, 601);
            this.printPreviewControl.TabIndex = 0;
            this.printPreviewControl.Zoom = 1D;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.chkSyz8);
            this.panel.Controls.Add(this.chkSyz7);
            this.panel.Controls.Add(this.chkSyz6);
            this.panel.Controls.Add(this.chkSyz5);
            this.panel.Controls.Add(this.chkSyz4);
            this.panel.Controls.Add(this.chkSyz3);
            this.panel.Controls.Add(this.chkSyz2);
            this.panel.Controls.Add(this.chkSyz1);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.chkType3);
            this.panel.Controls.Add(this.chkType2);
            this.panel.Controls.Add(this.chkType1);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.btnPrint);
            this.panel.Controls.Add(this.btnClose);
            this.panel.Controls.Add(this.btnSave);
            this.panel.Controls.Add(this.txtSyz_tsqk);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(878, 638);
            this.panel.TabIndex = 1;
            // 
            // chkSyz8
            // 
            this.chkSyz8.AutoSize = true;
            this.chkSyz8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz8.Location = new System.Drawing.Point(44, 390);
            this.chkSyz8.Name = "chkSyz8";
            this.chkSyz8.Size = new System.Drawing.Size(285, 18);
            this.chkSyz8.TabIndex = 147;
            this.chkSyz8.Text = "因诊疗需要的特殊情况(需记录具体情况):";
            this.chkSyz8.UseVisualStyleBackColor = true;
            // 
            // chkSyz7
            // 
            this.chkSyz7.AutoSize = true;
            this.chkSyz7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz7.Location = new System.Drawing.Point(44, 348);
            this.chkSyz7.Name = "chkSyz7";
            this.chkSyz7.Size = new System.Drawing.Size(250, 18);
            this.chkSyz7.TabIndex = 146;
            this.chkSyz7.Text = "各种原因所致不适合胃肠道给药者。";
            this.chkSyz7.UseVisualStyleBackColor = true;
            // 
            // chkSyz6
            // 
            this.chkSyz6.AutoSize = true;
            this.chkSyz6.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz6.Location = new System.Drawing.Point(44, 306);
            this.chkSyz6.Name = "chkSyz6";
            this.chkSyz6.Size = new System.Drawing.Size(250, 18);
            this.chkSyz6.TabIndex = 145;
            this.chkSyz6.Text = "经口服或肌注给药治疗无效的疾病。";
            this.chkSyz6.UseVisualStyleBackColor = true;
            // 
            // chkSyz5
            // 
            this.chkSyz5.AutoSize = true;
            this.chkSyz5.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz5.Location = new System.Drawing.Point(44, 264);
            this.chkSyz5.Name = "chkSyz5";
            this.chkSyz5.Size = new System.Drawing.Size(250, 18);
            this.chkSyz5.TabIndex = 144;
            this.chkSyz5.Text = "中重度感染需要静脉给予抗菌药物。";
            this.chkSyz5.UseVisualStyleBackColor = true;
            // 
            // chkSyz4
            // 
            this.chkSyz4.AutoSize = true;
            this.chkSyz4.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz4.Location = new System.Drawing.Point(44, 222);
            this.chkSyz4.Name = "chkSyz4";
            this.chkSyz4.Size = new System.Drawing.Size(460, 18);
            this.chkSyz4.TabIndex = 143;
            this.chkSyz4.Text = "输入药物，以达到解毒、脱水利尿、维持血液渗透压、抗肿瘤等治疗。";
            this.chkSyz4.UseVisualStyleBackColor = true;
            // 
            // chkSyz3
            // 
            this.chkSyz3.AutoSize = true;
            this.chkSyz3.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz3.Location = new System.Drawing.Point(44, 180);
            this.chkSyz3.Name = "chkSyz3";
            this.chkSyz3.Size = new System.Drawing.Size(824, 18);
            this.chkSyz3.TabIndex = 142;
            this.chkSyz3.Text = "补充营养，维持热量，促进组织修复，获得正氦平衡。用于慢性消耗疾病、禁食、不能经口提取食物、管饲不能得到足够营养等。";
            this.chkSyz3.UseVisualStyleBackColor = true;
            // 
            // chkSyz2
            // 
            this.chkSyz2.AutoSize = true;
            this.chkSyz2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz2.Location = new System.Drawing.Point(44, 138);
            this.chkSyz2.Name = "chkSyz2";
            this.chkSyz2.Size = new System.Drawing.Size(810, 18);
            this.chkSyz2.TabIndex = 141;
            this.chkSyz2.Text = "补充水和电解质，以调节或维持酸碱平衡。用于各种原因引起的脱水、严重呕吐、腹泻、大手术后、代谢性或呼吸性酸中毒等。";
            this.chkSyz2.UseVisualStyleBackColor = true;
            // 
            // chkSyz1
            // 
            this.chkSyz1.AutoSize = true;
            this.chkSyz1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.chkSyz1.Location = new System.Drawing.Point(44, 96);
            this.chkSyz1.Name = "chkSyz1";
            this.chkSyz1.Size = new System.Drawing.Size(453, 18);
            this.chkSyz1.TabIndex = 140;
            this.chkSyz1.Text = "补充血容量,改善微循环，维持血压。用于治疗烧伤、失血、休克等。";
            this.chkSyz1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label4.Location = new System.Drawing.Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 14);
            this.label4.TabIndex = 139;
            this.label4.Text = "静脉输液适应症:     ";
            // 
            // chkType3
            // 
            this.chkType3.AutoSize = true;
            this.chkType3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkType3.Location = new System.Drawing.Point(288, 24);
            this.chkType3.Name = "chkType3";
            this.chkType3.Size = new System.Drawing.Size(149, 18);
            this.chkType3.TabIndex = 137;
            this.chkType3.Text = "急诊I、II、III级";
            this.chkType3.UseVisualStyleBackColor = true;
            this.chkType3.CheckedChanged += new System.EventHandler(this.chkType3_CheckedChanged);
            // 
            // chkType2
            // 
            this.chkType2.AutoSize = true;
            this.chkType2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkType2.Location = new System.Drawing.Point(192, 24);
            this.chkType2.Name = "chkType2";
            this.chkType2.Size = new System.Drawing.Size(87, 18);
            this.chkType2.TabIndex = 136;
            this.chkType2.Text = "急诊IV级";
            this.chkType2.UseVisualStyleBackColor = true;
            this.chkType2.CheckedChanged += new System.EventHandler(this.chkType2_CheckedChanged);
            // 
            // chkType1
            // 
            this.chkType1.AutoSize = true;
            this.chkType1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkType1.Location = new System.Drawing.Point(96, 24);
            this.chkType1.Name = "chkType1";
            this.chkType1.Size = new System.Drawing.Size(86, 18);
            this.chkType1.TabIndex = 135;
            this.chkType1.Text = "普通门诊";
            this.chkType1.UseVisualStyleBackColor = true;
            this.chkType1.CheckedChanged += new System.EventHandler(this.chkType1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 134;
            this.label2.Text = "患者类型: ";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Hint = "先诊后结通道";
            this.btnPrint.Location = new System.Drawing.Point(642, 540);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(88, 31);
            this.btnPrint.TabIndex = 133;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.DefaultScheme = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.Hint = "先诊后结通道";
            this.btnClose.Location = new System.Drawing.Point(760, 540);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose.Size = new System.Drawing.Size(88, 31);
            this.btnClose.TabIndex = 132;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSave.DefaultScheme = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSave.Hint = "先诊后结通道";
            this.btnSave.Location = new System.Drawing.Point(524, 540);
            this.btnSave.Name = "btnSave";
            this.btnSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnSave.Size = new System.Drawing.Size(88, 31);
            this.btnSave.TabIndex = 131;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSyz_tsqk
            // 
            this.txtSyz_tsqk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSyz_tsqk.Font = new System.Drawing.Font("宋体", 12F);
            this.txtSyz_tsqk.Location = new System.Drawing.Point(60, 420);
            this.txtSyz_tsqk.Multiline = true;
            this.txtSyz_tsqk.Name = "txtSyz_tsqk";
            this.txtSyz_tsqk.Size = new System.Drawing.Size(788, 92);
            this.txtSyz_tsqk.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10F);
            this.tabControl1.ImageList = this.imageList;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(892, 671);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(884, 644);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = " 基本资料";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.printPreviewControl);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(884, 644);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = " 预览打印";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint2);
            this.panel1.Controls.Add(this.btnClose2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 37);
            this.panel1.TabIndex = 1;
            // 
            // btnPrint2
            // 
            this.btnPrint2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint2.DefaultScheme = true;
            this.btnPrint2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint2.Hint = "先诊后结通道";
            this.btnPrint2.Location = new System.Drawing.Point(680, 2);
            this.btnPrint2.Name = "btnPrint2";
            this.btnPrint2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrint2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint2.Size = new System.Drawing.Size(88, 31);
            this.btnPrint2.TabIndex = 135;
            this.btnPrint2.Text = "打印(&P)";
            this.btnPrint2.Click += new System.EventHandler(this.btnPrint2_Click);
            // 
            // btnClose2
            // 
            this.btnClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose2.DefaultScheme = true;
            this.btnClose2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose2.Hint = "先诊后结通道";
            this.btnClose2.Location = new System.Drawing.Point(782, 2);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClose2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnClose2.Size = new System.Drawing.Size(88, 31);
            this.btnClose2.TabIndex = 134;
            this.btnClose2.Text = "关闭(&C)";
            this.btnClose2.Click += new System.EventHandler(this.btnClose2_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "BOContact2_16x16.png");
            this.imageList.Images.SetKeyName(1, "Print_16x16.png");
            // 
            // frmIVDRISpecification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 671);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmIVDRISpecification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "门诊患者静脉输液情况说明书";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox txtSyz_tsqk;
        internal PinkieControls.ButtonXP btnClose;
        internal PinkieControls.ButtonXP btnSave;
        internal PinkieControls.ButtonXP btnPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panel1;
        internal PinkieControls.ButtonXP btnPrint2;
        internal PinkieControls.ButtonXP btnClose2;
        private System.Windows.Forms.CheckBox chkType3;
        private System.Windows.Forms.CheckBox chkType2;
        private System.Windows.Forms.CheckBox chkType1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSyz8;
        private System.Windows.Forms.CheckBox chkSyz7;
        private System.Windows.Forms.CheckBox chkSyz6;
        private System.Windows.Forms.CheckBox chkSyz5;
        private System.Windows.Forms.CheckBox chkSyz4;
        private System.Windows.Forms.CheckBox chkSyz3;
        private System.Windows.Forms.CheckBox chkSyz2;
        private System.Windows.Forms.CheckBox chkSyz1;
        private System.Windows.Forms.Label label4;
    }
}