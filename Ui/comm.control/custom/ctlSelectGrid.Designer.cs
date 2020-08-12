namespace Common.Controls
{
    partial class ctlSelectGrid
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlSelectGrid));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gpRight = new DevExpress.XtraEditors.GroupControl();
            this.gdRight = new DevExpress.XtraGrid.GridControl();
            this.gvRight = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.txtRight = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.gpLeft = new DevExpress.XtraEditors.GroupControl();
            this.gdLeft = new DevExpress.XtraGrid.GridControl();
            this.gvLeft = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtLeft = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gpRight)).BeginInit();
            this.gpRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpLeft)).BeginInit();
            this.gpLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLeft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.gpRight, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gpLeft, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelControl2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(379, 442);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // gpRight
            // 
            this.gpRight.Controls.Add(this.gdRight);
            this.gpRight.Controls.Add(this.panelControl3);
            this.gpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpRight.Location = new System.Drawing.Point(222, 5);
            this.gpRight.Margin = new System.Windows.Forms.Padding(5);
            this.gpRight.Name = "gpRight";
            this.tableLayoutPanel1.SetRowSpan(this.gpRight, 3);
            this.gpRight.Size = new System.Drawing.Size(152, 432);
            this.gpRight.TabIndex = 5;
            this.gpRight.Text = "所有";
            // 
            // gdRight
            // 
            this.gdRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdRight.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            gridLevelNode2.RelationName = "Level1";
            this.gdRight.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gdRight.Location = new System.Drawing.Point(2, 51);
            this.gdRight.MainView = this.gvRight;
            this.gdRight.Margin = new System.Windows.Forms.Padding(5);
            this.gdRight.Name = "gdRight";
            this.gdRight.Size = new System.Drawing.Size(148, 379);
            this.gdRight.TabIndex = 1;
            this.gdRight.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRight,
            this.gridView2});
            // 
            // gvRight
            // 
            this.gvRight.GridControl = this.gdRight;
            this.gvRight.Name = "gvRight";
            this.gvRight.OptionsCustomization.AllowFilter = false;
            this.gvRight.OptionsCustomization.AllowGroup = false;
            this.gvRight.OptionsMenu.EnableColumnMenu = false;
            this.gvRight.OptionsMenu.EnableFooterMenu = false;
            this.gvRight.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvRight.OptionsView.ShowGroupPanel = false;
            this.gvRight.DoubleClick += new System.EventHandler(this.gvRight_DoubleClick);
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gdRight;
            this.gridView2.Name = "gridView2";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.txtRight);
            this.panelControl3.Controls.Add(this.pictureEdit2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 23);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(148, 28);
            this.panelControl3.TabIndex = 0;
            // 
            // txtRight
            // 
            this.txtRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRight.Location = new System.Drawing.Point(28, 2);
            this.txtRight.Margin = new System.Windows.Forms.Padding(5);
            this.txtRight.Name = "txtRight";
            this.txtRight.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRight.Properties.Appearance.Options.UseFont = true;
            this.txtRight.Size = new System.Drawing.Size(118, 26);
            this.txtRight.TabIndex = 6;
            this.txtRight.TextChanged += new System.EventHandler(this.txtRight_TextChanged);
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(2, 2);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(5);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Size = new System.Drawing.Size(26, 24);
            this.pictureEdit2.TabIndex = 5;
            // 
            // gpLeft
            // 
            this.gpLeft.Controls.Add(this.gdLeft);
            this.gpLeft.Controls.Add(this.panelControl1);
            this.gpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpLeft.Location = new System.Drawing.Point(5, 5);
            this.gpLeft.Margin = new System.Windows.Forms.Padding(5);
            this.gpLeft.Name = "gpLeft";
            this.tableLayoutPanel1.SetRowSpan(this.gpLeft, 3);
            this.gpLeft.Size = new System.Drawing.Size(151, 432);
            this.gpLeft.TabIndex = 4;
            this.gpLeft.Text = "当前";
            // 
            // gdLeft
            // 
            this.gdLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdLeft.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gdLeft.Location = new System.Drawing.Point(2, 51);
            this.gdLeft.MainView = this.gvLeft;
            this.gdLeft.Margin = new System.Windows.Forms.Padding(5);
            this.gdLeft.Name = "gdLeft";
            this.gdLeft.Size = new System.Drawing.Size(147, 379);
            this.gdLeft.TabIndex = 1;
            this.gdLeft.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLeft,
            this.gridView1});
            // 
            // gvLeft
            // 
            this.gvLeft.GridControl = this.gdLeft;
            this.gvLeft.Name = "gvLeft";
            this.gvLeft.OptionsCustomization.AllowFilter = false;
            this.gvLeft.OptionsCustomization.AllowGroup = false;
            this.gvLeft.OptionsMenu.EnableColumnMenu = false;
            this.gvLeft.OptionsMenu.EnableFooterMenu = false;
            this.gvLeft.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvLeft.OptionsSelection.MultiSelect = true;
            this.gvLeft.OptionsView.ShowGroupPanel = false;
            this.gvLeft.DoubleClick += new System.EventHandler(this.gvLeft_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gdLeft;
            this.gridView1.Name = "gridView1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtLeft);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 23);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(147, 28);
            this.panelControl1.TabIndex = 0;
            // 
            // txtLeft
            // 
            this.txtLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLeft.Location = new System.Drawing.Point(28, 2);
            this.txtLeft.Margin = new System.Windows.Forms.Padding(5);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLeft.Properties.Appearance.Options.UseFont = true;
            this.txtLeft.Size = new System.Drawing.Size(117, 26);
            this.txtLeft.TabIndex = 6;
            this.txtLeft.TextChanged += new System.EventHandler(this.txtLeft_TextChanged);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(2, 2);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(26, 24);
            this.pictureEdit1.TabIndex = 5;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClear);
            this.panelControl2.Controls.Add(this.btnRemove);
            this.panelControl2.Controls.Add(this.btnAdd);
            this.panelControl2.Controls.Add(this.btnAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(164, 66);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(50, 181);
            this.panelControl2.TabIndex = 9;
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(0, 136);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(51, 25);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = ">>";
            this.btnClear.ToolTip = "清除所属科室";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnRemove.Appearance.Options.UseFont = true;
            this.btnRemove.Location = new System.Drawing.Point(0, 90);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(51, 25);
            this.btnRemove.TabIndex = 24;
            this.btnRemove.Text = ">";
            this.btnRemove.ToolTip = "将左侧选中科室移除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(0, 44);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(51, 25);
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "<";
            this.btnAdd.ToolTip = "将右侧选中科室添加到所属科室";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAll
            // 
            this.btnAll.Appearance.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnAll.Appearance.Options.UseFont = true;
            this.btnAll.Location = new System.Drawing.Point(0, 0);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(51, 25);
            this.btnAll.TabIndex = 22;
            this.btnAll.Text = "<<";
            this.btnAll.ToolTip = "将所有科室添加到所属科室";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // ctlSelectGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctlSelectGrid";
            this.Size = new System.Drawing.Size(379, 442);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gpRight)).EndInit();
            this.gpRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpLeft)).EndInit();
            this.gpLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLeft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl gpLeft;
        private DevExpress.XtraGrid.GridControl gdLeft;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLeft;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtLeft;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.GroupControl gpRight;
        private DevExpress.XtraGrid.GridControl gdRight;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRight;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.TextEdit txtRight;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnAll;
    }
}
