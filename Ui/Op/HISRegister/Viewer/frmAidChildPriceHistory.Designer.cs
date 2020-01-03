namespace com.digitalwave.iCare.gui.HIS
{
    partial class frmAidChildPriceHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvHistory = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemChildPricePre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemChildPriceCur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // gvHistory
            // 
            this.gvHistory.AllowUserToAddRows = false;
            this.gvHistory.AllowUserToDeleteRows = false;
            this.gvHistory.AllowUserToResizeRows = false;
            this.gvHistory.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gvHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvHistory.ColumnHeadersHeight = 28;
            this.gvHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colName,
            this.operName,
            this.itemChildPricePre,
            this.itemChildPriceCur});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvHistory.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvHistory.Location = new System.Drawing.Point(0, 0);
            this.gvHistory.MultiSelect = false;
            this.gvHistory.Name = "gvHistory";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvHistory.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gvHistory.RowHeadersVisible = false;
            this.gvHistory.RowTemplate.Height = 23;
            this.gvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvHistory.Size = new System.Drawing.Size(448, 191);
            this.gvHistory.TabIndex = 12;
            // 
            // colNo
            // 
            this.colNo.DataPropertyName = "no";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNo.HeaderText = "NO.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 42;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "frecdate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colName.HeaderText = "调价时间";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colName.Width = 130;
            // 
            // operName
            // 
            this.operName.DataPropertyName = "frecopername";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.operName.DefaultCellStyle = dataGridViewCellStyle4;
            this.operName.HeaderText = "调价人";
            this.operName.Name = "operName";
            this.operName.ReadOnly = true;
            this.operName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.operName.Width = 80;
            // 
            // itemChildPricePre
            // 
            this.itemChildPricePre.DataPropertyName = "fitemchildpricepre";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.itemChildPricePre.DefaultCellStyle = dataGridViewCellStyle5;
            this.itemChildPricePre.HeaderText = "历史价格";
            this.itemChildPricePre.Name = "itemChildPricePre";
            this.itemChildPricePre.ReadOnly = true;
            this.itemChildPricePre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemChildPricePre.Width = 80;
            // 
            // itemChildPriceCur
            // 
            this.itemChildPriceCur.DataPropertyName = "fitemchildpricecur";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.itemChildPriceCur.DefaultCellStyle = dataGridViewCellStyle6;
            this.itemChildPriceCur.HeaderText = "当前价格";
            this.itemChildPriceCur.Name = "itemChildPriceCur";
            this.itemChildPriceCur.ReadOnly = true;
            this.itemChildPriceCur.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemChildPriceCur.Width = 80;
            // 
            // frmAidChildPriceHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 191);
            this.Controls.Add(this.gvHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAidChildPriceHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "儿童价格历史记录";
            this.Load += new System.EventHandler(this.frmAidChildPriceHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView gvHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn operName;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemChildPricePre;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemChildPriceCur;
    }
}