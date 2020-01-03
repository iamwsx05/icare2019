namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmAidChooseRegular
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new PinkieControls.ButtonXP();
            this.btnOk = new PinkieControls.ButtonXP();
            this.gvRegulat = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fexpress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fclass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegulat)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 604);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 55);
            this.panel2.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DefaultScheme = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Hint = "";
            this.btnCancel.Location = new System.Drawing.Point(398, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnCancel.Size = new System.Drawing.Size(89, 37);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "放弃(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.DefaultScheme = true;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Hint = "";
            this.btnOk.Location = new System.Drawing.Point(276, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnOk.Size = new System.Drawing.Size(89, 37);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gvRegulat
            // 
            this.gvRegulat.AllowUserToAddRows = false;
            this.gvRegulat.AllowUserToDeleteRows = false;
            this.gvRegulat.AllowUserToResizeRows = false;
            this.gvRegulat.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gvRegulat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvRegulat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvRegulat.ColumnHeadersHeight = 28;
            this.gvRegulat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fno,
            this.fexpress,
            this.frule,
            this.fclass});
            this.gvRegulat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvRegulat.GridColor = System.Drawing.SystemColors.Control;
            this.gvRegulat.Location = new System.Drawing.Point(0, 35);
            this.gvRegulat.MultiSelect = false;
            this.gvRegulat.Name = "gvRegulat";
            this.gvRegulat.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gvRegulat.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvRegulat.RowTemplate.Height = 23;
            this.gvRegulat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvRegulat.Size = new System.Drawing.Size(500, 569);
            this.gvRegulat.TabIndex = 11;
            this.gvRegulat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.gvRegulat_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找规则：";
            // 
            // txtVal
            // 
            this.txtVal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVal.Font = new System.Drawing.Font("宋体", 11F);
            this.txtVal.Location = new System.Drawing.Point(92, 5);
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(373, 24);
            this.txtVal.TabIndex = 2;
            this.txtVal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVal_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtVal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 35);
            this.panel1.TabIndex = 6;
            // 
            // fno
            // 
            this.fno.DataPropertyName = "fno";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fno.DefaultCellStyle = dataGridViewCellStyle2;
            this.fno.HeaderText = "NO.";
            this.fno.Name = "fno";
            this.fno.ReadOnly = true;
            this.fno.Width = 44;
            // 
            // fexpress
            // 
            this.fexpress.DataPropertyName = "fexpress";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.fexpress.DefaultCellStyle = dataGridViewCellStyle3;
            this.fexpress.HeaderText = "表达式";
            this.fexpress.Name = "fexpress";
            this.fexpress.ReadOnly = true;
            this.fexpress.Width = 420;
            // 
            // frule
            // 
            this.frule.DataPropertyName = "frule";
            this.frule.HeaderText = "frule";
            this.frule.Name = "frule";
            this.frule.ReadOnly = true;
            this.frule.Visible = false;
            // 
            // fclass
            // 
            this.fclass.DataPropertyName = "fclass";
            this.fclass.HeaderText = "fclass";
            this.fclass.Name = "fclass";
            this.fclass.ReadOnly = true;
            this.fclass.Visible = false;
            // 
            // frmAidChooseRegular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 659);
            this.Controls.Add(this.gvRegulat);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAidChooseRegular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择费用自动核对规则";
            this.Load += new System.EventHandler(this.frmAidChooseRegular_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAidChooseRegular_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvRegulat)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        internal PinkieControls.ButtonXP btnCancel;
        internal PinkieControls.ButtonXP btnOk;
        internal System.Windows.Forms.DataGridView gvRegulat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fno;
        private System.Windows.Forms.DataGridViewTextBoxColumn fexpress;
        private System.Windows.Forms.DataGridViewTextBoxColumn frule;
        private System.Windows.Forms.DataGridViewTextBoxColumn fclass;
    }
}