namespace com.digitalwave.iCare.gui.MFZ.Controls
{
    partial class ctlDoctorList
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("0");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("1");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_lsvDoctors = new System.Windows.Forms.ListView();
            this.columnHeader49 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader50 = new System.Windows.Forms.ColumnHeader();
            this.deptname = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_lsvDoctors);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 467);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "医生列表";
            // 
            // m_lsvDoctors
            // 
            this.m_lsvDoctors.AllowDrop = true;
            this.m_lsvDoctors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader49,
            this.columnHeader50,
            this.deptname});
            this.m_lsvDoctors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsvDoctors.FullRowSelect = true;
            this.m_lsvDoctors.GridLines = true;
            this.m_lsvDoctors.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.m_lsvDoctors.Location = new System.Drawing.Point(3, 17);
            this.m_lsvDoctors.MultiSelect = false;
            this.m_lsvDoctors.Name = "m_lsvDoctors";
            this.m_lsvDoctors.Size = new System.Drawing.Size(169, 447);
            this.m_lsvDoctors.TabIndex = 27;
            this.m_lsvDoctors.UseCompatibleStateImageBehavior = false;
            this.m_lsvDoctors.View = System.Windows.Forms.View.Details;
            this.m_lsvDoctors.DragEnter += new System.Windows.Forms.DragEventHandler(this.m_lsvDoctors_DragEnter);
            this.m_lsvDoctors.DragDrop += new System.Windows.Forms.DragEventHandler(this.m_lsvDoctors_DragDrop);
            this.m_lsvDoctors.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_lsvDoctors_ItemDrag);
            // 
            // columnHeader49
            // 
            this.columnHeader49.Text = "姓 名";
            this.columnHeader49.Width = 64;
            // 
            // columnHeader50
            // 
            this.columnHeader50.Text = "医生类型";
            this.columnHeader50.Width = 70;
            // 
            // deptname
            // 
            this.deptname.Text = "科室";
            this.deptname.Width = 150;
            // 
            // ctlDoctorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctlDoctorList";
            this.Size = new System.Drawing.Size(175, 467);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView m_lsvDoctors;
        private System.Windows.Forms.ColumnHeader columnHeader49;
        private System.Windows.Forms.ColumnHeader columnHeader50;
        private System.Windows.Forms.ColumnHeader deptname;

    }
}
