using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing; 
using weCare.Core.Entity;  
using System.ComponentModel; 
using System.Windows.Forms; 
using com.digitalwave.GUI_Base;
using PinkieControls;

namespace com.digitalwave.iCare.gui.LIS
{
    public class frmBaseInfo : frmMDI_Child_Base
    {
        // Fields
        private ButtonXP btnExit0;
        private ButtonXP btnExit1;
        private ButtonXP btnExit2;
        private ButtonXP btnExit3;
        private ButtonXP buttonXP10;
        private ButtonXP buttonXP11;
        private ButtonXP buttonXP8;
        private ButtonXP buttonXP9;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private GroupBox groupBox3;
        private Label label1;
        private Label label10;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListView listView3;
        private bool m_blnNewCheckMethod;
        private bool m_blnNewConcentration;
        private bool m_blnNewVendor;
        private bool m_blnNewWorkGroup;
        private CheckBox m_chkCShowDeleted;
        private CheckBox m_chkWGShowDeleted;
        private ButtonXP m_cmdCCancelDelete;
        private ButtonXP m_cmdCDelete;
        private ButtonXP m_cmdCMDelete;
        private ButtonXP m_cmdCMNew;
        private ButtonXP m_cmdCMSave;
        private ButtonXP m_cmdCNew;
        private ButtonXP m_cmdCSave;
        private ButtonXP m_cmdVDDelete;
        private ButtonXP m_cmdVDNew;
        private ButtonXP m_cmdVDSave;
        private ButtonXP m_cmdWGCancelDelete;
        private ButtonXP m_cmdWGDelete;
        private ButtonXP m_cmdWGNew;
        private ButtonXP m_cmdWGSave;
        private GroupBox m_gpbCheckMethod;
        private GroupBox m_gpbConcentration;
        private GroupBox m_gpbVendor;
        private GroupBox m_gpbWorkGroup;
        private ListView m_lsvCheckMethod;
        private ListView m_lsvConcentration;
        private ListView m_lsvVendor;
        private ListView m_lsvWorkGroup;
       // private clsDcl_BaseInfo m_objDomain;
        private TabControl m_tabControl;
        private TabPage m_tbpCheckMethod;
        private TabPage m_tbpConcentration;
        private TabPage m_tbpWorkGroup;
        private TextBox m_txtCMName;
        private TextBox m_txtCMPYCode;
        private TextBox m_txtCMWBCode;
        private TextBox m_txtConcentration;
        private TextBox m_txtVDName;
        private TextBox m_txtVDPYCode;
        private TextBox m_txtVDWBCode;
        private TextBox m_txtVendorCode;
        private TextBox m_txtWGName;
        private TextBox m_txtWGSummary;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private TabPage tabVendor;

        // Methods
        public frmBaseInfo()
        {
           // this.m_objDomain = new clsDcl_BaseInfo();
            this.m_blnNewCheckMethod = false;
            this.m_blnNewWorkGroup = false;
            this.m_blnNewConcentration = false;
            this.m_blnNewVendor = false;
            this.components = null; 
            this.InitializeComponent(); 
        }

        private void btnExit0_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnExit3_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void frmBaseInfo_Load(object sender, EventArgs e)
        {
            this.m_mthLoadWorkGroup(); 
        }

        private void frmReportQuery_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthShortCutKey(e.KeyCode);
            base.m_mthSetKeyTab(e);
            return;
        }

        private void InitializeComponent()
        {
            ColumnHeader[] headerArray;
            this.listView3 = new ListView();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.panel5 = new Panel();
            this.groupBox3 = new GroupBox();
            this.panel6 = new Panel();
            this.buttonXP8 = new ButtonXP();
            this.buttonXP9 = new ButtonXP();
            this.buttonXP10 = new ButtonXP();
            this.buttonXP11 = new ButtonXP();
            this.m_tabControl = new TabControl();
            this.m_tbpWorkGroup = new TabPage();
            this.m_lsvWorkGroup = new ListView();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.panel3 = new Panel();
            this.m_chkWGShowDeleted = new CheckBox();
            this.m_cmdWGSave = new ButtonXP();
            this.m_cmdWGNew = new ButtonXP();
            this.m_cmdWGCancelDelete = new ButtonXP();
            this.m_cmdWGDelete = new ButtonXP();
            this.panel4 = new Panel();
            this.m_gpbWorkGroup = new GroupBox();
            this.m_txtWGSummary = new TextBox();
            this.label2 = new Label();
            this.m_txtWGName = new TextBox();
            this.label1 = new Label();
            this.m_tbpCheckMethod = new TabPage();
            this.m_lsvCheckMethod = new ListView();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.panel7 = new Panel();
            this.m_cmdCMDelete = new ButtonXP();
            this.m_cmdCMSave = new ButtonXP();
            this.m_cmdCMNew = new ButtonXP();
            this.panel8 = new Panel();
            this.m_gpbCheckMethod = new GroupBox();
            this.m_txtCMWBCode = new TextBox();
            this.label5 = new Label();
            this.m_txtCMPYCode = new TextBox();
            this.label4 = new Label();
            this.m_txtCMName = new TextBox();
            this.label3 = new Label();
            this.m_tbpConcentration = new TabPage();
            this.m_lsvConcentration = new ListView();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.panel9 = new Panel();
            this.m_cmdCDelete = new ButtonXP();
            this.m_chkCShowDeleted = new CheckBox();
            this.m_cmdCSave = new ButtonXP();
            this.m_cmdCNew = new ButtonXP();
            this.m_cmdCCancelDelete = new ButtonXP();
            this.panel10 = new Panel();
            this.m_gpbConcentration = new GroupBox();
            this.m_txtConcentration = new TextBox();
            this.label6 = new Label();
            this.tabVendor = new TabPage();
            this.m_lsvVendor = new ListView();
            this.columnHeader16 = new ColumnHeader();
            this.columnHeader17 = new ColumnHeader();
            this.columnHeader18 = new ColumnHeader();
            this.columnHeader1 = new ColumnHeader();
            this.panel11 = new Panel();
            this.m_cmdVDDelete = new ButtonXP();
            this.m_cmdVDSave = new ButtonXP();
            this.m_cmdVDNew = new ButtonXP();
            this.panel12 = new Panel();
            this.m_gpbVendor = new GroupBox();
            this.m_txtVendorCode = new TextBox();
            this.label10 = new Label();
            this.m_txtVDWBCode = new TextBox();
            this.label7 = new Label();
            this.m_txtVDPYCode = new TextBox();
            this.label8 = new Label();
            this.m_txtVDName = new TextBox();
            this.label9 = new Label();
            this.btnExit0 = new ButtonXP();
            this.btnExit1 = new ButtonXP();
            this.btnExit2 = new ButtonXP();
            this.btnExit3 = new ButtonXP();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.m_tbpWorkGroup.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.m_gpbWorkGroup.SuspendLayout();
            this.m_tbpCheckMethod.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.m_gpbCheckMethod.SuspendLayout();
            this.m_tbpConcentration.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.m_gpbConcentration.SuspendLayout();
            this.tabVendor.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.m_gpbVendor.SuspendLayout();
            base.SuspendLayout();
            this.listView3.Columns.AddRange(new ColumnHeader[] { this.columnHeader7, this.columnHeader8, this.columnHeader9 });
            this.listView3.Dock = System.Windows.Forms.DockStyle.Fill; 
            this.listView3.GridLines = true;
            this.listView3.Location = new Point(3, 0x59);
            this.listView3.Name = "listView3";
            this.listView3.Size = new Size(0x2ac, 0xd3);
            this.listView3.TabIndex = 2;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.columnHeader8.Width = 0xc0;
            this.columnHeader9.Width = 0x321;
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x2ac, 0x56);
            this.panel5.TabIndex = 1;
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x2ac, 0x56);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.panel6.Controls.Add(this.buttonXP8);
            this.panel6.Controls.Add(this.buttonXP9);
            this.panel6.Controls.Add(this.buttonXP10);
            this.panel6.Controls.Add(this.buttonXP11);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new Point(3, 300);
            this.panel6.Name = "panel6";
            this.panel6.Size = new Size(0x2ac, 0x44);
            this.panel6.TabIndex = 0;
            this.buttonXP8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP8.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.buttonXP8.DefaultScheme = true;
            this.buttonXP8.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP8.Hint = "";
            this.buttonXP8.Location = new Point(0x23e, 0x10);
            this.buttonXP8.Name = "buttonXP8";
            this.buttonXP8.Scheme = 0;
            this.buttonXP8.Size = new Size(100, 0x21);
            this.buttonXP8.TabIndex = 12;
            this.buttonXP8.Text = "取消";
            this.buttonXP9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP9.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.buttonXP9.DefaultScheme = true;
            this.buttonXP9.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP9.Hint = "";
            this.buttonXP9.Location = new Point(0x1d2, 0x10);
            this.buttonXP9.Name = "buttonXP9";
            this.buttonXP9.Scheme = 0;
            this.buttonXP9.Size = new Size(100, 0x21);
            this.buttonXP9.TabIndex = 11;
            this.buttonXP9.Text = "取消";
            this.buttonXP10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP10.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.buttonXP10.DefaultScheme = true;
            this.buttonXP10.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP10.Hint = "";
            this.buttonXP10.Location = new Point(0x166, 0x10);
            this.buttonXP10.Name = "buttonXP10";
            this.buttonXP10.Scheme = 0;
            this.buttonXP10.Size = new Size(100, 0x21);
            this.buttonXP10.TabIndex = 10;
            this.buttonXP10.Text = "取消";
            this.buttonXP11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP11.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.buttonXP11.DefaultScheme = true;
            this.buttonXP11.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP11.Hint = "";
            this.buttonXP11.Location = new Point(250, 0x10);
            this.buttonXP11.Name = "buttonXP11";
            this.buttonXP11.Scheme = 0;
            this.buttonXP11.Size = new Size(100, 0x21);
            this.buttonXP11.TabIndex = 9;
            this.buttonXP11.Text = "取消";
            this.m_tabControl.Controls.Add(this.m_tbpWorkGroup);
            this.m_tabControl.Controls.Add(this.m_tbpCheckMethod);
            this.m_tabControl.Controls.Add(this.m_tbpConcentration);
            this.m_tabControl.Controls.Add(this.tabVendor);
            this.m_tabControl.Dock = DockStyle.Fill;
            this.m_tabControl.Location = new Point(0, 0);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new Size(0x337, 0x1f3);
            this.m_tabControl.TabIndex = 0;
            this.m_tabControl.Selected += new TabControlEventHandler(this.m_tabControl_Selected);
            this.m_tbpWorkGroup.Controls.Add(this.m_lsvWorkGroup);
            this.m_tbpWorkGroup.Controls.Add(this.panel3);
            this.m_tbpWorkGroup.Controls.Add(this.panel4);
            this.m_tbpWorkGroup.Location = new Point(4, 0x17);
            this.m_tbpWorkGroup.Name = "m_tbpWorkGroup";
            this.m_tbpWorkGroup.Padding = new Padding(3);
            this.m_tbpWorkGroup.Size = new Size(0x32f, 0x1d8);
            this.m_tbpWorkGroup.TabIndex = 1;
            this.m_tbpWorkGroup.Text = "工作组";
            this.m_tbpWorkGroup.UseVisualStyleBackColor = true;
            this.m_lsvWorkGroup.Columns.AddRange(new ColumnHeader[] { this.columnHeader4, this.columnHeader5 });
            this.m_lsvWorkGroup.Dock = DockStyle.Fill;
            this.m_lsvWorkGroup.FullRowSelect = true;
            this.m_lsvWorkGroup.GridLines = true;
            this.m_lsvWorkGroup.HideSelection = false;
            this.m_lsvWorkGroup.Location = new Point(3, 0x65);
            this.m_lsvWorkGroup.MultiSelect = false;
            this.m_lsvWorkGroup.Name = "m_lsvWorkGroup";
            this.m_lsvWorkGroup.Size = new Size(0x329, 0x12a);
            this.m_lsvWorkGroup.TabIndex = 5;
            this.m_lsvWorkGroup.UseCompatibleStateImageBehavior = false;
            this.m_lsvWorkGroup.View = View.Details;
            this.m_lsvWorkGroup.Click += new EventHandler(this.m_lsvWorkGroup_Click);
            this.columnHeader4.Text = "工作组名称";
            this.columnHeader4.Width = 0xc6;
            this.columnHeader5.Text = "备注";
            this.columnHeader5.Width = 0x210;
            this.panel3.Controls.Add(this.btnExit0);
            this.panel3.Controls.Add(this.m_chkWGShowDeleted);
            this.panel3.Controls.Add(this.m_cmdWGSave);
            this.panel3.Controls.Add(this.m_cmdWGNew);
            this.panel3.Controls.Add(this.m_cmdWGCancelDelete);
            this.panel3.Controls.Add(this.m_cmdWGDelete);
            this.panel3.Dock = DockStyle.Bottom;
            this.panel3.Location = new Point(3, 0x18f);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x329, 70);
            this.panel3.TabIndex = 4;
            this.m_chkWGShowDeleted.AutoSize = true;
            this.m_chkWGShowDeleted.Location = new Point(0x16, 20);
            this.m_chkWGShowDeleted.Name = "m_chkWGShowDeleted";
            this.m_chkWGShowDeleted.Size = new Size(110, 0x12);
            this.m_chkWGShowDeleted.TabIndex = 0;
            this.m_chkWGShowDeleted.Text = "显示已删除项";
            this.m_chkWGShowDeleted.UseVisualStyleBackColor = true;
            this.m_chkWGShowDeleted.CheckedChanged += new EventHandler(this.m_chkWGShowDeleted_CheckedChanged);
            this.m_cmdWGSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdWGSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdWGSave.DefaultScheme = true;
            this.m_cmdWGSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdWGSave.Hint = "";
            this.m_cmdWGSave.Location = new Point(0x20b, 15);
            this.m_cmdWGSave.Name = "m_cmdWGSave";
            this.m_cmdWGSave.Scheme = 0;
            this.m_cmdWGSave.Size = new Size(0x56, 0x21);
            this.m_cmdWGSave.TabIndex = 3;
            this.m_cmdWGSave.Text = "保存";
            this.m_cmdWGSave.Click += new EventHandler(this.m_cmdWGSave_Click);
            this.m_cmdWGNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdWGNew.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdWGNew.DefaultScheme = true;
            this.m_cmdWGNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdWGNew.Hint = "";
            this.m_cmdWGNew.Location = new Point(430, 15);
            this.m_cmdWGNew.Name = "m_cmdWGNew";
            this.m_cmdWGNew.Scheme = 0;
            this.m_cmdWGNew.Size = new Size(0x56, 0x21);
            this.m_cmdWGNew.TabIndex = 2;
            this.m_cmdWGNew.Text = "新增";
            this.m_cmdWGNew.Click += new EventHandler(this.m_cmdWGNew_Click);
            this.m_cmdWGCancelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdWGCancelDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdWGCancelDelete.DefaultScheme = true;
            this.m_cmdWGCancelDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdWGCancelDelete.Hint = "";
            this.m_cmdWGCancelDelete.Location = new Point(0x151, 15);
            this.m_cmdWGCancelDelete.Name = "m_cmdWGCancelDelete";
            this.m_cmdWGCancelDelete.Scheme = 0;
            this.m_cmdWGCancelDelete.Size = new Size(0x56, 0x21);
            this.m_cmdWGCancelDelete.TabIndex = 1;
            this.m_cmdWGCancelDelete.Text = "取消删除";
            this.m_cmdWGCancelDelete.Visible = false;
            this.m_cmdWGCancelDelete.Click += new EventHandler(this.m_cmdWGCancelDelete_Click);
            this.m_cmdWGDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdWGDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdWGDelete.DefaultScheme = true;
            this.m_cmdWGDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdWGDelete.Hint = "";
            this.m_cmdWGDelete.Location = new Point(0x267, 15);
            this.m_cmdWGDelete.Name = "m_cmdWGDelete";
            this.m_cmdWGDelete.Scheme = 0;
            this.m_cmdWGDelete.Size = new Size(0x56, 0x21);
            this.m_cmdWGDelete.TabIndex = 4;
            this.m_cmdWGDelete.Text = "删除";
            this.m_cmdWGDelete.Click += new EventHandler(this.m_cmdWGDelete_Click);
            this.panel4.Controls.Add(this.m_gpbWorkGroup);
            this.panel4.Dock = DockStyle.Top;
            this.panel4.Location = new Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x329, 0x62);
            this.panel4.TabIndex = 3;
            this.m_gpbWorkGroup.Controls.Add(this.m_txtWGSummary);
            this.m_gpbWorkGroup.Controls.Add(this.label2);
            this.m_gpbWorkGroup.Controls.Add(this.m_txtWGName);
            this.m_gpbWorkGroup.Controls.Add(this.label1);
            this.m_gpbWorkGroup.Dock = DockStyle.Fill;
            this.m_gpbWorkGroup.Location = new Point(0, 0);
            this.m_gpbWorkGroup.Name = "m_gpbWorkGroup";
            this.m_gpbWorkGroup.Size = new Size(0x329, 0x62);
            this.m_gpbWorkGroup.TabIndex = 0;
            this.m_gpbWorkGroup.TabStop = false;
            this.m_txtWGSummary.ImeMode = ImeMode.On;
            this.m_txtWGSummary.Location = new Point(0x66, 0x31);
            this.m_txtWGSummary.MaxLength = 100;
            this.m_txtWGSummary.Name = "m_txtWGSummary";
            this.m_txtWGSummary.Size = new Size(0x138, 0x17);
            this.m_txtWGSummary.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x3d, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "备注";
            this.m_txtWGName.ImeMode = ImeMode.On;
            this.m_txtWGName.Location = new Point(0x66, 0x19);
            this.m_txtWGName.MaxLength = 0x19;
            this.m_txtWGName.Name = "m_txtWGName";
            this.m_txtWGName.Size = new Size(0x138, 0x17);
            this.m_txtWGName.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "工作组名称";
            this.m_tbpCheckMethod.Controls.Add(this.m_lsvCheckMethod);
            this.m_tbpCheckMethod.Controls.Add(this.panel7);
            this.m_tbpCheckMethod.Controls.Add(this.panel8);
            this.m_tbpCheckMethod.Location = new Point(4, 0x17);
            this.m_tbpCheckMethod.Name = "m_tbpCheckMethod";
            this.m_tbpCheckMethod.Padding = new Padding(3);
            this.m_tbpCheckMethod.Size = new Size(0x32f, 0x1d8);
            this.m_tbpCheckMethod.TabIndex = 2;
            this.m_tbpCheckMethod.Text = "检测方法";
            this.m_tbpCheckMethod.UseVisualStyleBackColor = true;
            this.m_lsvCheckMethod.Columns.AddRange(new ColumnHeader[] { this.columnHeader10, this.columnHeader11, this.columnHeader12 });
            this.m_lsvCheckMethod.Dock = DockStyle.Fill;
            this.m_lsvCheckMethod.FullRowSelect = true;
            this.m_lsvCheckMethod.GridLines = true;
            this.m_lsvCheckMethod.HideSelection = false;
            this.m_lsvCheckMethod.Location = new Point(3, 0x65);
            this.m_lsvCheckMethod.MultiSelect = false;
            this.m_lsvCheckMethod.Name = "m_lsvCheckMethod";
            this.m_lsvCheckMethod.Size = new Size(0x329, 0x12a);
            this.m_lsvCheckMethod.TabIndex = 5;
            this.m_lsvCheckMethod.UseCompatibleStateImageBehavior = false;
            this.m_lsvCheckMethod.View = View.Details;
            this.m_lsvCheckMethod.Click += new EventHandler(this.m_lsvCheckMethod_Click);
            this.columnHeader10.Text = "检测方法名称";
            this.columnHeader10.Width = 0xd8;
            this.columnHeader11.Text = "拼音码";
            this.columnHeader11.Width = 0x88;
            this.columnHeader12.Text = "五笔码";
            this.columnHeader12.Width = 0x26a;
            this.panel7.Controls.Add(this.btnExit1);
            this.panel7.Controls.Add(this.m_cmdCMDelete);
            this.panel7.Controls.Add(this.m_cmdCMSave);
            this.panel7.Controls.Add(this.m_cmdCMNew);
            this.panel7.Dock = DockStyle.Bottom;
            this.panel7.Location = new Point(3, 0x18f);
            this.panel7.Name = "panel7";
            this.panel7.Size = new Size(0x329, 70);
            this.panel7.TabIndex = 4;
            this.m_cmdCMDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCMDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCMDelete.DefaultScheme = true;
            this.m_cmdCMDelete.DialogResult = DialogResult.Cancel;
            this.m_cmdCMDelete.Hint = "";
            this.m_cmdCMDelete.Location = new Point(0x266, 15);
            this.m_cmdCMDelete.Name = "m_cmdCMDelete";
            this.m_cmdCMDelete.Scheme = 0;
            this.m_cmdCMDelete.Size = new Size(0x56, 0x21);
            this.m_cmdCMDelete.TabIndex = 14;
            this.m_cmdCMDelete.Text = "删除";
            this.m_cmdCMDelete.Click += new EventHandler(this.m_cmdCMDelete_Click);
            this.m_cmdCMSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCMSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCMSave.DefaultScheme = true;
            this.m_cmdCMSave.DialogResult = DialogResult.Cancel;
            this.m_cmdCMSave.Hint = "";
            this.m_cmdCMSave.Location = new Point(0x20a, 15);
            this.m_cmdCMSave.Name = "m_cmdCMSave";
            this.m_cmdCMSave.Scheme = 0;
            this.m_cmdCMSave.Size = new Size(0x56, 0x21);
            this.m_cmdCMSave.TabIndex = 12;
            this.m_cmdCMSave.Text = "保存";
            this.m_cmdCMSave.Click += new EventHandler(this.m_cmdCMSave_Click);
            this.m_cmdCMNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCMNew.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCMNew.DefaultScheme = true;
            this.m_cmdCMNew.DialogResult = DialogResult.Cancel;
            this.m_cmdCMNew.Hint = "";
            this.m_cmdCMNew.Location = new Point(430, 15);
            this.m_cmdCMNew.Name = "m_cmdCMNew";
            this.m_cmdCMNew.Scheme = 0;
            this.m_cmdCMNew.Size = new Size(0x56, 0x21);
            this.m_cmdCMNew.TabIndex = 11;
            this.m_cmdCMNew.Text = "新增";
            this.m_cmdCMNew.Click += new EventHandler(this.m_cmdCMNew_Click);
            this.panel8.Controls.Add(this.m_gpbCheckMethod);
            this.panel8.Dock = DockStyle.Top;
            this.panel8.Location = new Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new Size(0x329, 0x62);
            this.panel8.TabIndex = 3;
            this.m_gpbCheckMethod.Controls.Add(this.m_txtCMWBCode);
            this.m_gpbCheckMethod.Controls.Add(this.label5);
            this.m_gpbCheckMethod.Controls.Add(this.m_txtCMPYCode);
            this.m_gpbCheckMethod.Controls.Add(this.label4);
            this.m_gpbCheckMethod.Controls.Add(this.m_txtCMName);
            this.m_gpbCheckMethod.Controls.Add(this.label3);
            this.m_gpbCheckMethod.Dock = DockStyle.Fill;
            this.m_gpbCheckMethod.Location = new Point(0, 0);
            this.m_gpbCheckMethod.Name = "m_gpbCheckMethod";
            this.m_gpbCheckMethod.Size = new Size(0x329, 0x62);
            this.m_gpbCheckMethod.TabIndex = 0;
            this.m_gpbCheckMethod.TabStop = false;
            this.m_txtCMWBCode.ImeMode = ImeMode.Off;
            this.m_txtCMWBCode.Location = new Point(0x59, 0x41);
            this.m_txtCMWBCode.MaxLength = 10;
            this.m_txtCMWBCode.Name = "m_txtCMWBCode";
            this.m_txtCMWBCode.Size = new Size(0x138, 0x17);
            this.m_txtCMWBCode.TabIndex = 2;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x20, 0x44);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x31, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "五笔码";
            this.m_txtCMPYCode.ImeMode = ImeMode.Off;
            this.m_txtCMPYCode.Location = new Point(0x59, 0x29);
            this.m_txtCMPYCode.MaxLength = 10;
            this.m_txtCMPYCode.Name = "m_txtCMPYCode";
            this.m_txtCMPYCode.Size = new Size(0x138, 0x17);
            this.m_txtCMPYCode.TabIndex = 1;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x20, 0x2c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x31, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "拼音码";
            this.m_txtCMName.ImeMode = ImeMode.On;
            this.m_txtCMName.Location = new Point(0x59, 0x11);
            this.m_txtCMName.MaxLength = 0x19;
            this.m_txtCMName.Name = "m_txtCMName";
            this.m_txtCMName.Size = new Size(0x138, 0x17);
            this.m_txtCMName.TabIndex = 0;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3f, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "方法名称";
            this.m_tbpConcentration.Controls.Add(this.m_lsvConcentration);
            this.m_tbpConcentration.Controls.Add(this.panel9);
            this.m_tbpConcentration.Controls.Add(this.panel10);
            this.m_tbpConcentration.Location = new Point(4, 0x17);
            this.m_tbpConcentration.Name = "m_tbpConcentration";
            this.m_tbpConcentration.Padding = new Padding(3);
            this.m_tbpConcentration.Size = new Size(0x32f, 0x1d8);
            this.m_tbpConcentration.TabIndex = 3;
            this.m_tbpConcentration.Text = "浓度";
            this.m_tbpConcentration.UseVisualStyleBackColor = true;
            this.m_lsvConcentration.Columns.AddRange(new ColumnHeader[] { this.columnHeader13, this.columnHeader14 });
            this.m_lsvConcentration.Dock = DockStyle.Fill;
            this.m_lsvConcentration.FullRowSelect = true;
            this.m_lsvConcentration.GridLines = true;
            this.m_lsvConcentration.HideSelection = false;
            this.m_lsvConcentration.Location = new Point(3, 0x65);
            this.m_lsvConcentration.MultiSelect = false;
            this.m_lsvConcentration.Name = "m_lsvConcentration";
            this.m_lsvConcentration.Size = new Size(0x329, 0x12a);
            this.m_lsvConcentration.TabIndex = 5;
            this.m_lsvConcentration.UseCompatibleStateImageBehavior = false;
            this.m_lsvConcentration.View = View.Details;
            this.m_lsvConcentration.Click += new EventHandler(this.m_lsvConcentration_Click);
            this.columnHeader13.Text = "浓度序号";
            this.columnHeader13.Width = 0x4c;
            this.columnHeader14.Text = "浓度描述";
            this.columnHeader14.Width = 390;
            this.panel9.Controls.Add(this.btnExit2);
            this.panel9.Controls.Add(this.m_cmdCDelete);
            this.panel9.Controls.Add(this.m_chkCShowDeleted);
            this.panel9.Controls.Add(this.m_cmdCSave);
            this.panel9.Controls.Add(this.m_cmdCNew);
            this.panel9.Controls.Add(this.m_cmdCCancelDelete);
            this.panel9.Dock = DockStyle.Bottom;
            this.panel9.Location = new Point(3, 0x18f);
            this.panel9.Name = "panel9";
            this.panel9.Size = new Size(0x329, 70);
            this.panel9.TabIndex = 4;
            this.m_cmdCDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCDelete.DefaultScheme = true;
            this.m_cmdCDelete.DialogResult = DialogResult.Cancel;
            this.m_cmdCDelete.Hint = "";
            this.m_cmdCDelete.Location = new Point(0x267, 15);
            this.m_cmdCDelete.Name = "m_cmdCDelete";
            this.m_cmdCDelete.Scheme = 0;
            this.m_cmdCDelete.Size = new Size(0x56, 0x21);
            this.m_cmdCDelete.TabIndex = 4;
            this.m_cmdCDelete.Text = "删除";
            this.m_cmdCDelete.Click += new EventHandler(this.m_cmdCDelete_Click);
            this.m_chkCShowDeleted.AutoSize = true;
            this.m_chkCShowDeleted.Location = new Point(0x16, 0x15);
            this.m_chkCShowDeleted.Name = "m_chkCShowDeleted";
            this.m_chkCShowDeleted.Size = new Size(110, 0x12);
            this.m_chkCShowDeleted.TabIndex = 0;
            this.m_chkCShowDeleted.Text = "显示已删除项";
            this.m_chkCShowDeleted.UseVisualStyleBackColor = true;
            this.m_chkCShowDeleted.CheckedChanged += new EventHandler(this.m_chkCShowDeleted_CheckedChanged);
            this.m_cmdCSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCSave.DefaultScheme = true;
            this.m_cmdCSave.DialogResult = DialogResult.Cancel;
            this.m_cmdCSave.Hint = "";
            this.m_cmdCSave.Location = new Point(0x20b, 15);
            this.m_cmdCSave.Name = "m_cmdCSave";
            this.m_cmdCSave.Scheme = 0;
            this.m_cmdCSave.Size = new Size(0x56, 0x21);
            this.m_cmdCSave.TabIndex = 3;
            this.m_cmdCSave.Text = "保存";
            this.m_cmdCSave.Click += new EventHandler(this.m_cmdCSave_Click);
            this.m_cmdCNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCNew.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCNew.DefaultScheme = true;
            this.m_cmdCNew.DialogResult = DialogResult.Cancel;
            this.m_cmdCNew.Hint = "";
            this.m_cmdCNew.Location = new Point(430, 15);
            this.m_cmdCNew.Name = "m_cmdCNew";
            this.m_cmdCNew.Scheme = 0;
            this.m_cmdCNew.Size = new Size(0x56, 0x21);
            this.m_cmdCNew.TabIndex = 2;
            this.m_cmdCNew.Text = "新增";
            this.m_cmdCNew.Click += new EventHandler(this.m_cmdCNew_Click);
            this.m_cmdCCancelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdCCancelDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdCCancelDelete.DefaultScheme = true;
            this.m_cmdCCancelDelete.DialogResult = DialogResult.Cancel;
            this.m_cmdCCancelDelete.Hint = "";
            this.m_cmdCCancelDelete.Location = new Point(0x151, 15);
            this.m_cmdCCancelDelete.Name = "m_cmdCCancelDelete";
            this.m_cmdCCancelDelete.Scheme = 0;
            this.m_cmdCCancelDelete.Size = new Size(0x56, 0x21);
            this.m_cmdCCancelDelete.TabIndex = 1;
            this.m_cmdCCancelDelete.Text = "取消删除";
            this.m_cmdCCancelDelete.Visible = false;
            this.m_cmdCCancelDelete.Click += new EventHandler(this.m_cmdCCancelDelete_Click);
            this.panel10.Controls.Add(this.m_gpbConcentration);
            this.panel10.Dock = DockStyle.Top;
            this.panel10.Location = new Point(3, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new Size(0x329, 0x62);
            this.panel10.TabIndex = 3;
            this.m_gpbConcentration.Controls.Add(this.m_txtConcentration);
            this.m_gpbConcentration.Controls.Add(this.label6);
            this.m_gpbConcentration.Dock = DockStyle.Fill;
            this.m_gpbConcentration.Location = new Point(0, 0);
            this.m_gpbConcentration.Name = "m_gpbConcentration";
            this.m_gpbConcentration.Size = new Size(0x329, 0x62);
            this.m_gpbConcentration.TabIndex = 0;
            this.m_gpbConcentration.TabStop = false;
            this.m_txtConcentration.ImeMode = ImeMode.On;
            this.m_txtConcentration.Location = new Point(0x58, 30);
            this.m_txtConcentration.MaxLength = 10;
            this.m_txtConcentration.Name = "m_txtConcentration";
            this.m_txtConcentration.Size = new Size(0x138, 0x17);
            this.m_txtConcentration.TabIndex = 5;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x13, 0x21);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3f, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "浓度描述";
            this.tabVendor.Controls.Add(this.m_lsvVendor);
            this.tabVendor.Controls.Add(this.panel11);
            this.tabVendor.Controls.Add(this.panel12);
            this.tabVendor.Location = new Point(4, 0x17);
            this.tabVendor.Name = "tabVendor";
            this.tabVendor.Padding = new Padding(3);
            this.tabVendor.Size = new Size(0x32f, 0x1d8);
            this.tabVendor.TabIndex = 4;
            this.tabVendor.Text = "厂商、来源";
            this.tabVendor.UseVisualStyleBackColor = true;
            this.m_lsvVendor.Columns.AddRange(new ColumnHeader[] { this.columnHeader16, this.columnHeader17, this.columnHeader18, this.columnHeader1 });
            this.m_lsvVendor.Dock = DockStyle.Fill;
            this.m_lsvVendor.FullRowSelect = true;
            this.m_lsvVendor.GridLines = true;
            this.m_lsvVendor.HideSelection = false;
            this.m_lsvVendor.Location = new Point(3, 0x65);
            this.m_lsvVendor.MultiSelect = false ;
            this.m_lsvVendor.Name = "m_lsvVendor";
            this.m_lsvVendor.Size = new Size(0x329, 0x12a);
            this.m_lsvVendor.TabIndex = 5;
            this.m_lsvVendor.UseCompatibleStateImageBehavior = false;
            this.m_lsvVendor.View = View.Details;
            this.m_lsvVendor.Click += new EventHandler(this.m_lsvVendor_Click);
            this.columnHeader16.Text = "厂商、来源";
            this.columnHeader16.Width = 250;
            this.columnHeader17.Text = "编号";
            this.columnHeader17.Width = 0x77;
            this.columnHeader18.Text = "拼音码";
            this.columnHeader18.Width = 0x66;
            this.columnHeader1.Text = "五笔码";
            this.columnHeader1.Width = 0x133;
            this.panel11.Controls.Add(this.btnExit3);
            this.panel11.Controls.Add(this.m_cmdVDDelete);
            this.panel11.Controls.Add(this.m_cmdVDSave);
            this.panel11.Controls.Add(this.m_cmdVDNew);
            this.panel11.Dock = DockStyle.Bottom;
            this.panel11.Location = new Point(3, 0x18f);
            this.panel11.Name = "panel11";
            this.panel11.Size = new Size(0x329, 70);
            this.panel11.TabIndex = 4;
            this.m_cmdVDDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdVDDelete.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdVDDelete.DefaultScheme = true;
            this.m_cmdVDDelete.DialogResult = DialogResult.Cancel;
            this.m_cmdVDDelete.Hint = "";
            this.m_cmdVDDelete.Location = new Point(0x26a, 15);
            this.m_cmdVDDelete.Name = "m_cmdVDDelete";
            this.m_cmdVDDelete.Scheme = 0;
            this.m_cmdVDDelete.Size = new Size(0x56, 0x21);
            this.m_cmdVDDelete.TabIndex = 13;
            this.m_cmdVDDelete.Text = "删除";
            this.m_cmdVDDelete.Click += new EventHandler(this.m_cmdVDDelete_Click);
            this.m_cmdVDSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdVDSave.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdVDSave.DefaultScheme = true;
            this.m_cmdVDSave.DialogResult = DialogResult.Cancel;
            this.m_cmdVDSave.Hint = "";
            this.m_cmdVDSave.Location = new Point(0x20e, 15);
            this.m_cmdVDSave.Name = "m_cmdVDSave";
            this.m_cmdVDSave.Scheme = 0;
            this.m_cmdVDSave.Size = new Size(0x56, 0x21);
            this.m_cmdVDSave.TabIndex = 12;
            this.m_cmdVDSave.Text = "保存";
            this.m_cmdVDSave.Click += new EventHandler(this.m_cmdVDSave_Click);
            this.m_cmdVDNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmdVDNew.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.m_cmdVDNew.DefaultScheme = true;
            this.m_cmdVDNew.DialogResult = DialogResult.Cancel;
            this.m_cmdVDNew.Hint = "";
            this.m_cmdVDNew.Location = new Point(0x1b1, 15);
            this.m_cmdVDNew.Name = "m_cmdVDNew";
            this.m_cmdVDNew.Scheme = 0;
            this.m_cmdVDNew.Size = new Size(0x56, 0x21);
            this.m_cmdVDNew.TabIndex = 11;
            this.m_cmdVDNew.Text = "新增";
            this.m_cmdVDNew.Click += new EventHandler(this.m_cmdVDNew_Click);
            this.panel12.Controls.Add(this.m_gpbVendor);
            this.panel12.Dock = DockStyle.Top;
            this.panel12.Location = new Point(3, 3);
            this.panel12.Name = "panel12";
            this.panel12.Size = new Size(0x329, 0x62);
            this.panel12.TabIndex = 3;
            this.m_gpbVendor.Controls.Add(this.m_txtVendorCode);
            this.m_gpbVendor.Controls.Add(this.label10);
            this.m_gpbVendor.Controls.Add(this.m_txtVDWBCode);
            this.m_gpbVendor.Controls.Add(this.label7);
            this.m_gpbVendor.Controls.Add(this.m_txtVDPYCode);
            this.m_gpbVendor.Controls.Add(this.label8);
            this.m_gpbVendor.Controls.Add(this.m_txtVDName);
            this.m_gpbVendor.Controls.Add(this.label9);
            this.m_gpbVendor.Dock = DockStyle.Fill;
            this.m_gpbVendor.Location = new Point(0, 0);
            this.m_gpbVendor.Name = "m_gpbVendor";
            this.m_gpbVendor.Size = new Size(0x329, 0x62);
            this.m_gpbVendor.TabIndex = 0;
            this.m_gpbVendor.TabStop = false;
            this.m_txtVendorCode.ImeMode = ImeMode.Off;
            this.m_txtVendorCode.Location = new Point(0x61, 0x2e);
            this.m_txtVendorCode.MaxLength = 10;
            this.m_txtVendorCode.Name = "m_txtVendorCode";
            this.m_txtVendorCode.Size = new Size(0xde, 0x17);
            this.m_txtVendorCode.TabIndex = 10;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x3a, 0x31);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 14);
            this.label10.TabIndex = 14;
            this.label10.Text = "编号";
            this.m_txtVDWBCode.ImeMode = ImeMode.Off;
            this.m_txtVDWBCode.Location = new Point(0x193, 0x2e);
            this.m_txtVDWBCode.MaxLength = 10;
            this.m_txtVDWBCode.Name = "m_txtVDWBCode";
            this.m_txtVDWBCode.Size = new Size(0x98, 0x17);
            this.m_txtVDWBCode.TabIndex = 12;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x15a, 0x31);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x31, 14);
            this.label7.TabIndex = 12;
            this.label7.Text = "五笔码";
            this.m_txtVDPYCode.ImeMode = ImeMode.Off;
            this.m_txtVDPYCode.Location = new Point(0x193, 0x16);
            this.m_txtVDPYCode.MaxLength = 10;
            this.m_txtVDPYCode.Name = "m_txtVDPYCode";
            this.m_txtVDPYCode.Size = new Size(0x98, 0x17);
            this.m_txtVDPYCode.TabIndex = 11;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x15a, 0x19);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x31, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "拼音码";
            this.m_txtVDName.ImeMode = ImeMode.On;
            this.m_txtVDName.Location = new Point(0x61, 0x16);
            this.m_txtVDName.MaxLength = 50;
            this.m_txtVDName.Name = "m_txtVDName";
            this.m_txtVDName.Size = new Size(0xde, 0x17);
            this.m_txtVDName.TabIndex = 9;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x10, 0x19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4d, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "厂商、来源";
            this.btnExit0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit0.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.btnExit0.DefaultScheme = true;
            this.btnExit0.DialogResult = 0;
            this.btnExit0.Hint = "";
            this.btnExit0.Location = new Point(0x2c4, 15);
            this.btnExit0.Name = "btnExit0";
            this.btnExit0.Scheme = 0;
            this.btnExit0.Size = new Size(0x56, 0x21);
            this.btnExit0.TabIndex = 5;
            this.btnExit0.Text = "关闭(&C)";
            this.btnExit0.Click += new EventHandler(this.btnExit0_Click);
            this.btnExit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit1.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.btnExit1.DefaultScheme = true;
            this.btnExit1.DialogResult = 0;
            this.btnExit1.Hint = "";
            this.btnExit1.Location = new Point(0x2c2, 15);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Scheme = 0;
            this.btnExit1.Size = new Size(0x56, 0x21);
            this.btnExit1.TabIndex = 15;
            this.btnExit1.Text = "关闭(&C)";
            this.btnExit1.Click += new EventHandler(this.btnExit1_Click);
            this.btnExit2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit2.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.btnExit2.DefaultScheme = true;
            this.btnExit2.DialogResult = 0;
            this.btnExit2.Hint = "";
            this.btnExit2.Location = new Point(0x2c4, 15);
            this.btnExit2.Name = "btnExit2";
            this.btnExit2.Scheme = 0;
            this.btnExit2.Size = new Size(0x56, 0x21);
            this.btnExit2.TabIndex = 0x10;
            this.btnExit2.Text = "关闭(&C)";
            this.btnExit2.Click += new EventHandler(this.btnExit2_Click);
            this.btnExit3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit3.BackColor = Color.FromArgb(0, 0xd4, 0xd0, 200);
            this.btnExit3.DefaultScheme = true;
            this.btnExit3.DialogResult = 0;
            this.btnExit3.Hint = "";
            this.btnExit3.Location = new Point(0x2c5, 15);
            this.btnExit3.Name = "btnExit3";
            this.btnExit3.Scheme = 0;
            this.btnExit3.Size = new Size(0x56, 0x21);
            this.btnExit3.TabIndex = 0x11;
            this.btnExit3.Text = "关闭(&C)";
            this.btnExit3.Click += new EventHandler(this.btnExit3_Click);
            this.AutoScaleDimensions = new SizeF(7f, 14f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(0x337, 0x1f3);
            this.Controls.Add(this.m_tabControl);
            this.Font = new Font("宋体", 10.5f);
            this.KeyPreview = true;
            this.Name = "frmBaseInfo";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "基本资料维护";
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += new KeyEventHandler(this.frmReportQuery_KeyDown);
            this.Load += new EventHandler(this.frmBaseInfo_Load);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.m_tbpWorkGroup.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.m_gpbWorkGroup.ResumeLayout(false);
            this.m_gpbWorkGroup.PerformLayout();
            this.m_tbpCheckMethod.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.m_gpbCheckMethod.ResumeLayout(false);
            this.m_gpbCheckMethod.PerformLayout();
            this.m_tbpConcentration.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.m_gpbConcentration.ResumeLayout(false);
            this.m_gpbConcentration.PerformLayout();
            this.tabVendor.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.m_gpbVendor.ResumeLayout(false);
            this.m_gpbVendor.PerformLayout();
            this.ResumeLayout(false); 
        }

        private void m_chkCShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.m_chkCShowDeleted.Enabled = false;
            this.m_mthShowConcentrationList((clsLisConcentrationVO[])this.m_lsvConcentration.Tag, this.m_chkCShowDeleted.Checked);
            this.m_cmdCCancelDelete.Visible = this.m_chkCShowDeleted.Checked;
            this.m_cmdCNew.Visible = !this.m_chkCShowDeleted.Checked  ;
            this.m_cmdCSave.Visible = !this.m_chkCShowDeleted.Checked  ;
            this.m_cmdCDelete.Visible = !this.m_chkCShowDeleted.Checked ;
            this.m_txtConcentration.Enabled = !this.m_chkCShowDeleted.Checked  ;
            this.m_chkCShowDeleted.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_chkWGShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.m_chkWGShowDeleted.Enabled = false;
            this.m_mthShowWorkGroupList((clsLisWorkGroupVO[])this.m_lsvWorkGroup.Tag, this.m_chkWGShowDeleted.Checked);
            this.m_cmdWGCancelDelete.Visible = this.m_chkWGShowDeleted.Checked;
            this.m_cmdWGNew.Visible = !this.m_chkWGShowDeleted.Checked  ;
            this.m_cmdWGSave.Visible = !this.m_chkWGShowDeleted.Checked  ;
            this.m_cmdWGDelete.Visible = !this.m_chkWGShowDeleted.Checked  ;
            this.m_gpbWorkGroup.Enabled = !this.m_chkWGShowDeleted.Checked ;
            this.m_chkWGShowDeleted.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdCCancelDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null
               || this.m_lsvConcentration.FocusedItem.Tag == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            m_cmdCCancelDelete.Enabled = false;

            clsLisConcentrationVO objconcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;
            clsLisConcentrationVO objCopy = new clsLisConcentrationVO();
            objconcentration.m_mthCopyTo(objCopy);//拷贝到另一个对象
            objCopy.m_enmStatus = enmQCStatus.Natrural;

            //更新到数据库
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateConcentration(objCopy);

            if (lngRes > 0)
            {//更新成功
                objconcentration.m_enmStatus = enmQCStatus.Natrural;
                int intIdx = this.m_lsvConcentration.FocusedItem.Index;

                this.m_lsvConcentration.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvConcentration.Items.Count)
                {
                    this.m_lsvConcentration.Items[intIdx].Selected = true;
                    this.m_lsvConcentration.Items[intIdx].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvConcentration.Items[intIdx - 1].Selected = true;
                    this.m_lsvConcentration.Items[intIdx - 1].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
            }
            else
            {//更新失败
                clsCommonDialog.m_mthShowDBError();
            }

            m_cmdCCancelDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdCDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGDelete.Enabled = false;

            clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;
            clsLisConcentrationVO objCopy = new clsLisConcentrationVO();
            objConcentration.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Delete;

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateConcentration(objCopy);

            if (lngRes > 0)
            {//成功
                objConcentration.m_enmStatus = enmQCStatus.Delete;
                int intIdx = this.m_lsvConcentration.FocusedItem.Index;

                this.m_lsvConcentration.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvConcentration.Items.Count)
                {
                    this.m_lsvConcentration.Items[intIdx].Selected = true;
                    this.m_lsvConcentration.Items[intIdx].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvConcentration.Items[intIdx - 1].Selected = true;
                    this.m_lsvConcentration.Items[intIdx - 1].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdCDelete.Enabled = true;
            Cursor.Current = Cursors.Default;




            clsLisConcentrationVO nvo;
            clsLisConcentrationVO nvo2;
            long num;
            int num2;
            bool flag;
            if (this.m_lsvConcentration.FocusedItem == null)  
            {
                goto Label_001D;
            }
            goto Label_016F;
        Label_001D:
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGDelete.Enabled = false;
            nvo = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;
            nvo2 = new clsLisConcentrationVO();
            nvo.m_mthCopyTo(nvo2);
            nvo2.m_enmStatus = 0;
           long rec =  (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateConcentration(nvo2);
            if(rec<=0)
            {
                goto Label_014F;
            }
            else
            {
                nvo.m_enmStatus = 0;
                num2 = this.m_lsvConcentration.FocusedItem.Index;
                this.m_lsvConcentration.FocusedItem.Remove();
                if (num2 < this.m_lsvConcentration.Items.Count)
                {
                    goto Label_0101;
                }
                this.m_lsvConcentration.Items[num2].Selected = true;
                this.m_lsvConcentration.Items[num2].Focused = true;
                this.m_lsvConcentration_Click(null, null);
                goto Label_014C;
            }          
        Label_0101:
            if ((num2 - 1) < 0)  
            {
                goto Label_014C;
            }
            this.m_lsvConcentration.Items[num2 - 1].Selected = true;
            this.m_lsvConcentration.Items[num2 - 1].Focused = true;
            this.m_lsvConcentration_Click(null, null);
        Label_014C:
            goto Label_0157;
        Label_014F:
            clsCommonDialog.m_mthShowDBError();
        Label_0157:
            this.m_cmdCDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        Label_016F:
            return;
        }

        private void m_cmdCMDelete_Click(object sender, EventArgs e)
        {
            clsLisCheckMethodVO dvo;
            clsLisCheckMethodVO dvo2;
            long num;
            int num2;
            bool flag;
            if (this.m_lsvCheckMethod.FocusedItem != null)  
            {
                goto Label_001D;
            }
            goto Label_0166;
        Label_001D:
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCMDelete.Enabled = false;
            dvo = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;
            dvo2 = new clsLisCheckMethodVO();
            dvo.m_mthCopyTo(dvo2);
            if ((new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteCheckMethod(dvo2.m_intSeq) <= 0L)  
            {
                goto Label_0146;
            }
            num2 = this.m_lsvCheckMethod.FocusedItem.Index;
            this.m_lsvCheckMethod.FocusedItem.Remove();
            if (num2 >= this.m_lsvCheckMethod.Items.Count)  
            {
                goto Label_00F8;
            }
            this.m_lsvCheckMethod.Items[num2].Selected = true;
            this.m_lsvCheckMethod.Items[num2].Focused = true;
            this.m_lsvCheckMethod_Click(null, null);
            goto Label_0143;
        Label_00F8:
            if ((num2 - 1) < 0) 
            {
                goto Label_0143;
            }
            this.m_lsvCheckMethod.Items[num2 - 1].Selected = true;
            this.m_lsvCheckMethod.Items[num2 - 1].Focused = true;
            this.m_lsvCheckMethod_Click(null, null);
        Label_0143:
            goto Label_014E;
        Label_0146:
            clsCommonDialog.m_mthShowDBError();
        Label_014E:
            this.m_cmdCMDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        Label_0166:
            return;
        }

        private void m_cmdCMNew_Click(object sender, EventArgs e)
        {
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvCheckMethod.FocusedItem != null)
            {
                this.m_lsvCheckMethod.FocusedItem.Selected = false;
                this.m_lsvCheckMethod.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthCMDetailClear();

            //设置光标焦点
            this.m_txtCMName.Focus();

            //设置新增标志
            this.m_blnNewCheckMethod = true;
        }

        private void m_cmdCMSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvCheckMethod.FocusedItem == null
             && !this.m_blnNewCheckMethod)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCMSave.Enabled = false;

            if (this.m_blnNewCheckMethod)
            {//新增的保存
                clsLisCheckMethodVO objMethod = new clsLisCheckMethodVO();
                objMethod.m_strName = this.m_txtCMName.Text.Trim();
                objMethod.m_strPycode = this.m_txtCMPYCode.Text.Trim();
                objMethod.m_strWbcode = this.m_txtCMWBCode.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertCheckMethod(objMethod, out objMethod.m_intSeq);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewCheckMethod = false;
                    //加入到集合
                    clsLisCheckMethodVO[] objMethods = (clsLisCheckMethodVO[])this.m_lsvCheckMethod.Tag;
                    clsLisCheckMethodVO[] objMethodsNewArr = new clsLisCheckMethodVO[objMethods.Length + 1];
                    objMethods.CopyTo(objMethodsNewArr, 0);
                    objMethodsNewArr[objMethodsNewArr.Length - 1] = objMethod;
                    this.m_lsvCheckMethod.Tag = objMethodsNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objMethod.m_strName);

                    item.SubItems.Add(objMethod.m_strPycode);
                    item.SubItems.Add(objMethod.m_strWbcode);

                    item.Tag = objMethod;
                    this.m_lsvCheckMethod.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvCheckMethod_Click(null, null);

                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//修改的保存
                clsLisCheckMethodVO objMethod = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;

                clsLisCheckMethodVO objNewMethod = new clsLisCheckMethodVO();
                objMethod.m_mthCopyTo(objNewMethod);
                objNewMethod.m_strName = this.m_txtCMName.Text.Trim();
                objNewMethod.m_strPycode = this.m_txtCMPYCode.Text.Trim();
                objNewMethod.m_strWbcode = this.m_txtCMWBCode.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateCheckMethod(objNewMethod);

                if (lngRes > 0)
                {//成功
                    objNewMethod.m_mthCopyTo(objMethod);

                    this.m_lsvCheckMethod.FocusedItem.Text = objMethod.m_strName;
                    this.m_lsvCheckMethod.FocusedItem.SubItems[1].Text = objMethod.m_strPycode;
                    this.m_lsvCheckMethod.FocusedItem.SubItems[2].Text = objMethod.m_strWbcode;
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdCMSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdCNew_Click(object sender, EventArgs e)
        {
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvConcentration.FocusedItem != null)
            {
                this.m_lsvConcentration.FocusedItem.Selected = false;
                this.m_lsvConcentration.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthCDetailClear();

            //设置光标焦点
            this.m_txtConcentration.Focus();

            //设置新增标志
            this.m_blnNewConcentration = true;
        }

        private void m_cmdCSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null
              && !this.m_blnNewConcentration)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCSave.Enabled = false;

            if (this.m_blnNewConcentration)
            {//新增的保存
                clsLisConcentrationVO objConentration = new clsLisConcentrationVO();
                objConentration.m_enmStatus = enmQCStatus.Natrural;
                objConentration.m_strConcentration = this.m_txtConcentration.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertConcentration(objConentration, out objConentration.m_intSeq);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewConcentration = false;
                    //加入到集合
                    clsLisConcentrationVO[] objconcentrationArr = (clsLisConcentrationVO[])this.m_lsvConcentration.Tag;
                    clsLisConcentrationVO[] objconcentrationNewArr = new clsLisConcentrationVO[objconcentrationArr.Length + 1];
                    objconcentrationArr.CopyTo(objconcentrationNewArr, 0);
                    objconcentrationNewArr[objconcentrationNewArr.Length - 1] = objConentration;
                    this.m_lsvConcentration.Tag = objconcentrationNewArr;

                    //添加新项
                    ListViewItem item = new ListViewItem(objConentration.m_strConcentration);
                    item.Text = objConentration.m_intSeq.ToString();
                    item.SubItems.Add(objConentration.m_strConcentration);
                    item.Tag = objConentration;
                    this.m_lsvConcentration.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//修改的保存
                clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;

                clsLisConcentrationVO objNewConcentration = new clsLisConcentrationVO();
                objConcentration.m_mthCopyTo(objNewConcentration);
                objNewConcentration.m_strConcentration = this.m_txtConcentration.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateConcentration(objNewConcentration);

                if (lngRes > 0)
                {//成功
                    objNewConcentration.m_mthCopyTo(objConcentration);
                    this.m_lsvConcentration.FocusedItem.Text = objConcentration.m_intSeq.ToString();
                    this.m_lsvConcentration.FocusedItem.SubItems[1].Text = objConcentration.m_strConcentration;
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdCSave.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdVDDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdVDDelete.Enabled = false;

            clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;
            clsLisVendorVO objCopy = new clsLisVendorVO();
            objVendor.m_mthCopyTo(objCopy);

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngDeleteCheckMethod(objCopy.m_intSeq);

            if (lngRes > 0)
            {//成功
                int intIdx = this.m_lsvVendor.FocusedItem.Index;

                this.m_lsvVendor.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvVendor.Items.Count)
                {
                    this.m_lsvVendor.Items[intIdx].Selected = true;
                    this.m_lsvVendor.Items[intIdx].Focused = true;
                    this.m_lsvVendor_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvVendor.Items[intIdx - 1].Selected = true;
                    this.m_lsvVendor.Items[intIdx - 1].Focused = true;
                    this.m_lsvVendor_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdVDDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdVDNew_Click(object sender, EventArgs e)
        {
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvVendor.FocusedItem != null)
            {
                this.m_lsvVendor.FocusedItem.Selected = false;
                this.m_lsvVendor.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthVDDetailClear();

            //设置光标焦点
            this.m_txtVDName.Focus();

            //设置新增标志
            this.m_blnNewVendor = true;
        }

        private void m_cmdVDSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null
            && !this.m_blnNewVendor)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdVDSave.Enabled = false;

            if (this.m_blnNewVendor)
            {//新增的保存
                clsLisVendorVO objVendor = new clsLisVendorVO();

                objVendor.m_strVendor = this.m_txtVDName.Text.Trim();
                objVendor.m_strId = this.m_txtVendorCode.Text.Trim();
                objVendor.m_strPycode = this.m_txtVDPYCode.Text.Trim();
                objVendor.m_strWbcode = this.m_txtVDWBCode.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertVendor(objVendor, out objVendor.m_intSeq);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewVendor = false;
                    //加入到集合
                    clsLisVendorVO[] objVendorArr = (clsLisVendorVO[])this.m_lsvVendor.Tag;
                    clsLisVendorVO[] objVendorNewArr = new clsLisVendorVO[objVendorArr.Length + 1];
                    objVendorArr.CopyTo(objVendorNewArr, 0);
                    objVendorNewArr[objVendorNewArr.Length - 1] = objVendor;
                    this.m_lsvVendor.Tag = objVendorNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objVendor.m_strVendor);
                    item.SubItems.Add(objVendor.m_strId);
                    item.SubItems.Add(objVendor.m_strPycode);
                    item.SubItems.Add(objVendor.m_strWbcode);

                    item.Tag = objVendor;
                    this.m_lsvVendor.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvVendor_Click(null, null);

                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//修改的保存
                clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;

                clsLisVendorVO objNewVendor = new clsLisVendorVO();
                objVendor.m_mthCopyTo(objNewVendor);
                objNewVendor.m_strVendor = this.m_txtVDName.Text.Trim();
                objNewVendor.m_strId = this.m_txtVendorCode.Text.Trim();
                objNewVendor.m_strPycode = this.m_txtVDPYCode.Text.Trim();
                objNewVendor.m_strWbcode = this.m_txtVDWBCode.Text.Trim();

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateVendor(objNewVendor);

                if (lngRes > 0)
                {//成功
                    objNewVendor.m_mthCopyTo(objVendor);

                    this.m_lsvVendor.FocusedItem.Text = objVendor.m_strVendor;
                    this.m_lsvVendor.FocusedItem.SubItems[1].Text = objVendor.m_strId;
                    this.m_lsvVendor.FocusedItem.SubItems[2].Text = objVendor.m_strPycode;
                    this.m_lsvVendor.FocusedItem.SubItems[3].Text = objVendor.m_strWbcode;
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdVDSave.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdWGCancelDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null
                || this.m_lsvWorkGroup.FocusedItem.Tag == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            m_cmdWGCancelDelete.Enabled = false;

            clsLisWorkGroupVO objGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;
            clsLisWorkGroupVO objCopy = new clsLisWorkGroupVO();
            objGroup.m_mthCopyTo(objCopy);//拷贝到另一个对象
            objCopy.m_enmStatus = enmQCStatus.Natrural;

            //更新到数据库
            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateWorkGruop(objCopy);

            if (lngRes > 0)
            {//更新成功
                objGroup.m_enmStatus = enmQCStatus.Natrural;
                int intIdx = this.m_lsvWorkGroup.FocusedItem.Index;

                this.m_lsvWorkGroup.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvWorkGroup.Items.Count)
                {
                    this.m_lsvWorkGroup.Items[intIdx].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvWorkGroup.Items[intIdx - 1].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx - 1].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
            }
            else
            {//更新失败
                clsCommonDialog.m_mthShowDBError();
            }

            m_cmdWGCancelDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdWGDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGDelete.Enabled = false;

            clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;
            clsLisWorkGroupVO objCopy = new clsLisWorkGroupVO();
            objWorkGroup.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Delete;

            long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateWorkGruop(objCopy);

            if (lngRes > 0)
            {//成功
                objWorkGroup.m_enmStatus = enmQCStatus.Delete;
                int intIdx = this.m_lsvWorkGroup.FocusedItem.Index;

                this.m_lsvWorkGroup.FocusedItem.Remove();

                //设置新的具有焦点的 ListView 项
                if (intIdx < this.m_lsvWorkGroup.Items.Count)
                {
                    this.m_lsvWorkGroup.Items[intIdx].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvWorkGroup.Items[intIdx - 1].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx - 1].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
            }
            else
            {//失败
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdWGDelete.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_cmdWGNew_Click(object sender, EventArgs e)
        {
            //使当前ListView具有焦点的行失去焦点
            if (this.m_lsvWorkGroup.FocusedItem != null)
            {
                this.m_lsvWorkGroup.FocusedItem.Selected = false;
                this.m_lsvWorkGroup.FocusedItem.Focused = false;
            }

            //清空明细
            m_mthWGDetailClear();

            //设置光标焦点
            this.m_txtWGName.Focus();

            //设置新增标志
            this.m_blnNewWorkGroup = true;
        }

        private void m_cmdWGSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null
               && !this.m_blnNewWorkGroup)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGSave.Enabled = false;

            if (this.m_blnNewWorkGroup)
            {//新增的保存
                clsLisWorkGroupVO objGroup = new clsLisWorkGroupVO();
                objGroup.m_enmStatus = enmQCStatus.Natrural;
                objGroup.m_strName = this.m_txtWGName.Text.Trim();
                objGroup.m_strSummary = this.m_txtWGSummary.Text;

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngInsertWorkGroup(objGroup, out objGroup.m_intSeq);
                if (lngRes > 0)
                {//成功
                    //更新状态标志
                    this.m_blnNewWorkGroup = false;
                    //加入到集合
                    clsLisWorkGroupVO[] objGroupArr = (clsLisWorkGroupVO[])this.m_lsvWorkGroup.Tag;
                    clsLisWorkGroupVO[] objGroupNewArr = new clsLisWorkGroupVO[objGroupArr.Length + 1];
                    objGroupArr.CopyTo(objGroupNewArr, 0);
                    objGroupNewArr[objGroupNewArr.Length - 1] = objGroup;
                    this.m_lsvWorkGroup.Tag = objGroupNewArr;
                    //添加新项
                    ListViewItem item = new ListViewItem(objGroup.m_strName);
                    item.SubItems.Add(objGroup.m_strSummary);
                    item.Tag = objGroup;
                    this.m_lsvWorkGroup.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);

                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//修改的保存
                clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;

                clsLisWorkGroupVO objGroup = new clsLisWorkGroupVO();
                objWorkGroup.m_mthCopyTo(objGroup);
                objGroup.m_strName = this.m_txtWGName.Text.Trim();
                objGroup.m_strSummary = this.m_txtWGSummary.Text;

                long lngRes = (new weCare.Proxy.ProxyLis03()).Service.m_lngUpdateWorkGruop(objGroup);

                if (lngRes > 0)
                {//成功
                    objGroup.m_mthCopyTo(objWorkGroup);
                    this.m_lsvWorkGroup.FocusedItem.Text = objWorkGroup.m_strName;
                    this.m_lsvWorkGroup.FocusedItem.SubItems[1].Text = objWorkGroup.m_strSummary;
                }
                else
                {//失败
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdWGSave.Enabled = true;
            Cursor.Current = Cursors.Default; 
        }

        private void m_lsvCheckMethod_Click(object sender, EventArgs e)
        {
            if (this.m_lsvCheckMethod.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewCheckMethod = false;

            clsLisCheckMethodVO objCheckMethod = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;

            this.m_txtCMName.Text = objCheckMethod.m_strName;
            this.m_txtCMPYCode.Text = objCheckMethod.m_strPycode;
            this.m_txtCMWBCode.Text = objCheckMethod.m_strWbcode;
        }

        private void m_lsvConcentration_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewConcentration = false;

            clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;

            this.m_txtConcentration.Text = objConcentration.m_strConcentration;
        }

        private void m_lsvVendor_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewVendor = false;

            clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;

            this.m_txtVDName.Text = objVendor.m_strVendor;
            this.m_txtVendorCode.Text = objVendor.m_strId;
            this.m_txtVDPYCode.Text = objVendor.m_strPycode;
            this.m_txtVDWBCode.Text = objVendor.m_strWbcode;
        }

        private void m_lsvWorkGroup_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null)
                return;
            //变更状态标志
            this.m_blnNewWorkGroup = false;

            clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;

            this.m_txtWGName.Text = objWorkGroup.m_strName;
            this.m_txtWGSummary.Text = objWorkGroup.m_strSummary;
        }

        private void m_mthCDetailClear()
        {
            this.m_txtConcentration.Clear(); 
        }

        private void m_mthCMDetailClear()
        {
            this.m_txtCMName.Clear();
            this.m_txtCMPYCode.Clear();
            this.m_txtCMWBCode.Clear(); 
        }

        private void m_mthLoadCheckMethod()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsLisCheckMethodVO[] objMethodsArr = null;
            (new weCare.Proxy.ProxyLis03()).Service.m_lngFindCheckMethod(out objMethodsArr);
            if (objMethodsArr == null)
            {
                objMethodsArr = new clsLisCheckMethodVO[0];
            }
            m_lsvCheckMethod.Tag = objMethodsArr;

            //填充列表
            m_mthShowCheckMethodList(objMethodsArr);

            Cursor.Current = Cursors.Default; 
        }

        private void m_mthLoadConcentration()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsLisConcentrationVO[] objconcentrationArr = null;
            (new weCare.Proxy.ProxyLis03()).Service.m_lngFindConcentration(out objconcentrationArr);
            if (objconcentrationArr == null)
            {
                objconcentrationArr = new clsLisConcentrationVO[0];
            }
            m_lsvConcentration.Tag = objconcentrationArr;

            //填充列表
            m_mthShowConcentrationList(objconcentrationArr, this.m_chkCShowDeleted.Checked);

            Cursor.Current = Cursors.Default; 
        }

        private void m_mthLoadVendor()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsLisVendorVO[] objVendorArr = null;
            (new weCare.Proxy.ProxyLis03()).Service.m_lngFindVendor(out objVendorArr);
            if (objVendorArr == null)
            {
                objVendorArr = new clsLisVendorVO[0];
            }
            m_lsvVendor.Tag = objVendorArr;

            //填充列表
            m_mthShowVendorList(objVendorArr);

            Cursor.Current = Cursors.Default; 
        }

        private void m_mthLoadWorkGroup()
        {
            Cursor.Current = Cursors.WaitCursor;

            //加载数据
            clsLisWorkGroupVO[] objGroupArr = null;
            (new weCare.Proxy.ProxyLis03()).Service.m_lngFindWorkGroup(out objGroupArr);
            if (objGroupArr == null)
            {
                objGroupArr = new clsLisWorkGroupVO[0];
            }
            m_lsvWorkGroup.Tag = objGroupArr;

            //填充列表
            m_mthShowWorkGroupList(objGroupArr, this.m_chkWGShowDeleted.Checked);

            Cursor.Current = Cursors.Default; 
        }

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            return;
        }

        private void m_mthShowCheckMethodList(clsLisCheckMethodVO[] objMethodsArr)
        {
            this.m_lsvCheckMethod.BeginUpdate();//开始更新列表
            this.m_lsvCheckMethod.Items.Clear();
            if (objMethodsArr != null)
            {
                foreach (clsLisCheckMethodVO method in objMethodsArr)
                {
                    ListViewItem item = new ListViewItem(method.m_strName);
                    item.SubItems.Add(method.m_strPycode);
                    item.SubItems.Add(method.m_strWbcode);
                    item.Tag = method;
                    this.m_lsvCheckMethod.Items.Add(item);
                }
            }
            //重置状态标志
            this.m_blnNewCheckMethod = false;
            //清空明细
            m_mthCMDetailClear();

            this.m_lsvCheckMethod.EndUpdate();//结束更新列表
        }

        private void m_mthShowConcentrationList(clsLisConcentrationVO[] objconcentrationArr, bool p_blnDeleted)
        {
            this.m_lsvConcentration.BeginUpdate();//开始更新列表
            this.m_lsvConcentration.Items.Clear();
            if (objconcentrationArr != null)
            {
                foreach (clsLisConcentrationVO concentration in objconcentrationArr)
                {
                    //根据类别过滤需要填充的项
                    if ((p_blnDeleted && (concentration.m_enmStatus == enmQCStatus.Delete))
                        || (!p_blnDeleted && (concentration.m_enmStatus == enmQCStatus.Natrural)))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = concentration.m_intSeq.ToString();
                        item.SubItems.Add(concentration.m_strConcentration);
                        item.Tag = concentration;

                        this.m_lsvConcentration.Items.Add(item);
                    }
                }
            }
            //重置状态标志
            this.m_blnNewConcentration = false;
            //清空明细
            m_mthCDetailClear();

            this.m_lsvConcentration.EndUpdate();//结束更新列表
        }

        private void m_mthShowVendorList(clsLisVendorVO[] objVendorArr)
        {
            this.m_lsvVendor.BeginUpdate();//开始更新列表
            this.m_lsvVendor.Items.Clear();
            if (objVendorArr != null)
            {
                foreach (clsLisVendorVO vendor in objVendorArr)
                {
                    ListViewItem item = new ListViewItem(vendor.m_strVendor);
                    item.SubItems.Add(vendor.m_strId);
                    item.SubItems.Add(vendor.m_strPycode);
                    item.SubItems.Add(vendor.m_strWbcode);
                    item.Tag = vendor;
                    this.m_lsvVendor.Items.Add(item);
                }
            }
            //重置状态标志
            this.m_blnNewVendor = false;
            //清空明细
            m_mthVDDetailClear();

            this.m_lsvVendor.EndUpdate();//结束更新列表
        }

        private void m_mthShowWorkGroupList(clsLisWorkGroupVO[] objGroupArr, bool p_blnDeleted)
        {
            this.m_lsvWorkGroup.BeginUpdate();//开始更新列表
            this.m_lsvWorkGroup.Items.Clear();
            if (objGroupArr != null)
            {
                foreach (clsLisWorkGroupVO group in objGroupArr)
                {
                    //根据类别过滤需要填充的项
                    if ((p_blnDeleted && (group.m_enmStatus == enmQCStatus.Delete))
                        || (!p_blnDeleted && (group.m_enmStatus == enmQCStatus.Natrural)))
                    {
                        ListViewItem item = new ListViewItem(group.m_strName);
                        item.SubItems.Add(group.m_strSummary);
                        item.Tag = group;

                        this.m_lsvWorkGroup.Items.Add(item);
                    }
                }
            }
            //重置状态标志
            this.m_blnNewWorkGroup = false;
            //清空明细
            m_mthWGDetailClear();

            this.m_lsvWorkGroup.EndUpdate();//结束更新列表
        }

        private void m_mthVDDetailClear()
        {
            this.m_txtVDName.Clear();
            this.m_txtVDPYCode.Clear();
            this.m_txtVDWBCode.Clear();
            this.m_txtVendorCode.Clear(); 
        }

        private void m_mthWGDetailClear()
        {
            this.m_txtWGName.Clear();
            this.m_txtWGSummary.Clear(); 
        }

        private void m_tabControl_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    if (this.m_lsvWorkGroup.Tag == null)
                        this.m_mthLoadWorkGroup();
                    break;
                case 1:
                    if (this.m_lsvCheckMethod.Tag == null)
                        this.m_mthLoadCheckMethod();
                    break;
                case 2:
                    if (this.m_lsvConcentration.Tag == null)
                        this.m_mthLoadConcentration();
                    break;
                case 3:
                    if (this.m_lsvVendor.Tag == null)
                        this.m_mthLoadVendor();
                    break;
                default:
                    break;
            }
        }
    } 
}
