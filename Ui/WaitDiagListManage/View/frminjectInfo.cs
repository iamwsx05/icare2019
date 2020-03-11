using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// frminjectInfo 的摘要说明。
    /// </summary>
    public class frminjectInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        internal System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal com.digitalwave.controls.NumTextBox txtCardID;
        internal com.digitalwave.controls.exTextBox txtName;
        internal System.Windows.Forms.TextBox txtSex;
        internal System.Windows.Forms.TextBox txtAge;
        internal PinkieControls.ButtonXP btFind;
        internal System.Windows.Forms.ListView listView_Patient;
        internal System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        internal System.Drawing.Printing.PrintDocument printDocument1;
        internal System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label9;
        internal PinkieControls.ButtonXP buttonXP3;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtDeptName;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox m_txtCarNo;
        internal System.Windows.Forms.TextBox m_patentName;
        private System.Windows.Forms.Label label10;
        internal com.digitalwave.controls.ctlTextBoxFind txtEmp;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.RadioButton m_rdbinject;
        public System.Windows.Forms.RadioButton m_rdbscout;
        public System.Windows.Forms.RadioButton m_rdbbottle;
        public System.Windows.Forms.RadioButton m_rdbcure;
        public System.Windows.Forms.RadioButton m_rdboperation;
        public System.Windows.Forms.RadioButton m_rdbblood;
        internal PinkieControls.ButtonXP m_cmdPrintPrivew;
        internal PinkieControls.ButtonXP m_cmdExit;
        internal System.Windows.Forms.ListView m_lsvSinature;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ContextMenu m_contextMenuAlterName;
        private System.Windows.Forms.MenuItem menuItemAlterName;
        internal PinkieControls.ButtonXP m_cmdSinature;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ContextMenu cmFullAllSelected;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label18;
        internal System.Windows.Forms.TextBox m_txtDoc;
        private System.Windows.Forms.Label label19;
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ContextMenu m_conMenuAllergic;
        private System.Windows.Forms.MenuItem menuItem3;
        public System.Windows.Forms.Label m_lblOUTPATRECIPEID_CHR;
        private System.Windows.Forms.Button button1;
        private MenuItem DelmenuItem;
        public Label m_lblRecordCount;
        public CheckBox m_chkTui;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        internal PinkieControls.ButtonXP btnPrint;
        private ColumnHeader columnHeader19;
        private ColumnHeader columnHeader20;
        public CheckBox m_chkPrint;
        public frmAllergichint m_frmAllergichintShow = new frmAllergichint();
        public frminjectInfo()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #region 公共属性
        public string PatientCardID
        {
            get
            {
                return this.txtCardID.Text;
            }
        }
        public string PatientID
        {
            get
            {
                if (this.txtCardID.Tag != null)
                {
                    return this.txtCardID.Text;
                }
                else
                {
                    return this.txtCardID.Tag.ToString();
                }
            }
        }
        public string PatientSex
        {
            get
            {
                return this.txtSex.Text;
            }
        }
        public string PatientAge
        {
            get
            {
                return this.txtAge.Text;
            }
        }
        public string PatientName
        {
            get
            {
                return this.txtName.Text;
            }
        }
        public string ChargeItemName
        {
            get
            {
                string temp = "";
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    temp += ((int)(i + 1)).ToString() + "、" + this.listView1.Items[i].SubItems[1].Text + "\n";
                }
                return temp;
            }
        }
        public string ChargeItemUnit
        {
            get
            {
                string temp = "";
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    temp += ((int)(i + 1)).ToString() + "、" + this.listView1.Items[i].SubItems[2].Text + "\n";
                }
                return temp;
            }
        }
        public string ChargeItemCount
        {
            get
            {
                string temp = "";
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    temp += ((int)(i + 1)).ToString() + "、" + this.listView1.Items[i].SubItems[3].Text + " " + this.listView1.Items[i].SubItems[2].Text + "\n";
                }
                return temp;
            }
        }
        public string ChargeItemUsage
        {
            get
            {
                string temp = "";
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    temp += ((int)(i + 1)).ToString() + "、" + this.listView1.Items[i].SubItems[4].Text + "\n";
                }
                return temp;
            }
        }
        #endregion
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frminjectInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView_Patient = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.m_conMenuAllergic = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCardID = new com.digitalwave.controls.NumTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.cmFullAllSelected = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.txtName = new com.digitalwave.controls.exTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.btFind = new PinkieControls.ButtonXP();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.buttonXP3 = new PinkieControls.ButtonXP();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDeptName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.m_chkPrint = new System.Windows.Forms.CheckBox();
            this.m_chkTui = new System.Windows.Forms.CheckBox();
            this.txtEmp = new com.digitalwave.controls.ctlTextBoxFind();
            this.m_txtCarNo = new System.Windows.Forms.TextBox();
            this.m_patentName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.m_txtDoc = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_rdbinject = new System.Windows.Forms.RadioButton();
            this.m_rdbscout = new System.Windows.Forms.RadioButton();
            this.m_rdbbottle = new System.Windows.Forms.RadioButton();
            this.m_rdbcure = new System.Windows.Forms.RadioButton();
            this.m_rdboperation = new System.Windows.Forms.RadioButton();
            this.m_rdbblood = new System.Windows.Forms.RadioButton();
            this.m_cmdPrintPrivew = new PinkieControls.ButtonXP();
            this.m_cmdExit = new PinkieControls.ButtonXP();
            this.m_cmdSinature = new PinkieControls.ButtonXP();
            this.m_lsvSinature = new System.Windows.Forms.ListView();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.m_contextMenuAlterName = new System.Windows.Forms.ContextMenu();
            this.menuItemAlterName = new System.Windows.Forms.MenuItem();
            this.DelmenuItem = new System.Windows.Forms.MenuItem();
            this.label17 = new System.Windows.Forms.Label();
            this.m_lblOUTPATRECIPEID_CHR = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.m_lblRecordCount = new System.Windows.Forms.Label();
            this.btnPrint = new PinkieControls.ButtonXP();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.listView_Patient);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(4, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 580);
            this.panel1.TabIndex = 0;
            // 
            // listView_Patient
            // 
            this.listView_Patient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Patient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_Patient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader7});
            this.listView_Patient.ContextMenu = this.m_conMenuAllergic;
            this.listView_Patient.ForeColor = System.Drawing.Color.Green;
            this.listView_Patient.FullRowSelect = true;
            this.listView_Patient.GridLines = true;
            this.listView_Patient.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_Patient.HideSelection = false;
            this.listView_Patient.Location = new System.Drawing.Point(0, 26);
            this.listView_Patient.Name = "listView_Patient";
            this.listView_Patient.Size = new System.Drawing.Size(236, 548);
            this.listView_Patient.TabIndex = 0;
            this.listView_Patient.UseCompatibleStateImageBehavior = false;
            this.listView_Patient.View = System.Windows.Forms.View.Details;
            this.listView_Patient.SelectedIndexChanged += new System.EventHandler(this.listView_Patient_SelectedIndexChanged);
            this.listView_Patient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_Patient_KeyDown);
            this.listView_Patient.Click += new System.EventHandler(this.listView_Patient_Click);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "诊疗卡号";
            this.columnHeader6.Width = 95;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "病人名称";
            this.columnHeader8.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "处方日期";
            this.columnHeader7.Width = 150;
            // 
            // m_conMenuAllergic
            // 
            this.m_conMenuAllergic.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "过敏信息";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.OliveDrab;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(236, 24);
            this.label9.TabIndex = 8;
            this.label9.Text = "病人列表";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(511, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "卡号:";
            // 
            // txtCardID
            // 
            this.txtCardID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCardID.Location = new System.Drawing.Point(520, -28);
            this.txtCardID.MaxLength = 10;
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.SendTabKey = false;
            this.txtCardID.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtCardID.Size = new System.Drawing.Size(128, 26);
            this.txtCardID.TabIndex = 7;
            this.txtCardID.TabStop = false;
            this.txtCardID.Visible = false;
            this.txtCardID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardID_KeyDown);
            this.txtCardID.Leave += new System.EventHandler(this.txtCardID_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "姓名:";
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader2,
            this.columnHeader11,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20});
            this.listView1.ContextMenu = this.cmFullAllSelected;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(587, 508);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "状态";
            this.columnHeader12.Width = 40;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "方号";
            this.columnHeader13.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "项目名称";
            this.columnHeader2.Width = 155;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "规格";
            this.columnHeader11.Width = 110;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "用量";
            this.columnHeader4.Width = 54;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "频率";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 55;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "用法";
            this.columnHeader5.Width = 65;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "天数";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "总数";
            this.columnHeader18.Width = 0;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "详细用法";
            this.columnHeader19.Width = 0;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "诊断";
            this.columnHeader20.Width = 0;
            // 
            // cmFullAllSelected
            // 
            this.cmFullAllSelected.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "全选";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "反选";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(720, -28);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.SendTabKey = false;
            this.txtName.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtName.Size = new System.Drawing.Size(84, 26);
            this.txtName.TabIndex = 8;
            this.txtName.TabStop = false;
            this.txtName.Visible = false;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "性别:";
            // 
            // txtSex
            // 
            this.txtSex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSex.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSex.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtSex.Location = new System.Drawing.Point(52, 14);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(51, 19);
            this.txtSex.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "年龄:";
            // 
            // txtAge
            // 
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAge.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtAge.Location = new System.Drawing.Point(155, 14);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(62, 19);
            this.txtAge.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(-1, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "分类:";
            // 
            // cmbCat
            // 
            this.cmbCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCat.Items.AddRange(new object[] {
            "输液",
            "治疗",
            "手术",
            "输血"});
            this.cmbCat.Location = new System.Drawing.Point(38, 12);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(58, 22);
            this.cmbCat.TabIndex = 0;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            this.cmbCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCat_KeyDown);
            // 
            // btFind
            // 
            this.btFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btFind.DefaultScheme = true;
            this.btFind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btFind.Hint = "";
            this.btFind.Location = new System.Drawing.Point(866, 7);
            this.btFind.Name = "btFind";
            this.btFind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btFind.Size = new System.Drawing.Size(74, 33);
            this.btFind.TabIndex = 6;
            this.btFind.Text = "查询(&F)";
            this.btFind.Click += new System.EventHandler(this.btFind_Click);
            this.btFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCat_KeyDown);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(259, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(120, 23);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCat_KeyDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(117, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 23);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCat_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.listView1);
            this.panel2.Location = new System.Drawing.Point(248, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 512);
            this.panel2.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label7.Location = new System.Drawing.Point(214, 632);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(652, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "说明:  1、绿色为已收费处方.2、浅蓝色为未收费处方.3、红色为已退票处方.4、黑色为签名处方.\r\n";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "用法名称";
            this.columnHeader10.Width = 171;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ID";
            this.columnHeader9.Width = 0;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // buttonXP3
            // 
            this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.buttonXP3.DefaultScheme = true;
            this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP3.Hint = "";
            this.buttonXP3.Location = new System.Drawing.Point(942, 7);
            this.buttonXP3.Name = "buttonXP3";
            this.buttonXP3.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP3.Size = new System.Drawing.Size(76, 33);
            this.buttonXP3.TabIndex = 6;
            this.buttonXP3.Text = "当天(&C)";
            this.buttonXP3.Click += new System.EventHandler(this.buttonXP3_Click_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(449, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 14);
            this.label11.TabIndex = 18;
            this.label11.Text = "床号:";
            // 
            // txtDeptName
            // 
            this.txtDeptName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeptName.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDeptName.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtDeptName.Location = new System.Drawing.Point(271, 14);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.ReadOnly = true;
            this.txtDeptName.Size = new System.Drawing.Size(173, 19);
            this.txtDeptName.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(225, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 14);
            this.label12.TabIndex = 16;
            this.label12.Text = "科室:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.textBox2.Location = new System.Drawing.Point(501, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(32, 19);
            this.textBox2.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(157, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 1);
            this.label13.TabIndex = 20;
            this.label13.Text = "label13";
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Location = new System.Drawing.Point(52, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 1);
            this.label14.TabIndex = 21;
            this.label14.Text = "label14";
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(271, 30);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(159, 1);
            this.label15.TabIndex = 22;
            this.label15.Text = "label15";
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(499, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 1);
            this.label16.TabIndex = 23;
            this.label16.Text = "label16";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.m_chkPrint);
            this.panel5.Controls.Add(this.btFind);
            this.panel5.Controls.Add(this.m_chkTui);
            this.panel5.Controls.Add(this.txtEmp);
            this.panel5.Controls.Add(this.m_patentName);
            this.panel5.Controls.Add(this.m_txtCarNo);
            this.panel5.Controls.Add(this.txtName);
            this.panel5.Controls.Add(this.txtCardID);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.cmbCat);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.dateTimePicker1);
            this.panel5.Controls.Add(this.dateTimePicker2);
            this.panel5.Controls.Add(this.buttonXP3);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1028, 47);
            this.panel5.TabIndex = 0;
            // 
            // m_chkPrint
            // 
            this.m_chkPrint.AutoSize = true;
            this.m_chkPrint.Location = new System.Drawing.Point(762, 25);
            this.m_chkPrint.Name = "m_chkPrint";
            this.m_chkPrint.Size = new System.Drawing.Size(96, 18);
            this.m_chkPrint.TabIndex = 21;
            this.m_chkPrint.Text = "显示已打印";
            this.m_chkPrint.UseVisualStyleBackColor = true;
            // 
            // m_chkTui
            // 
            this.m_chkTui.AutoSize = true;
            this.m_chkTui.Location = new System.Drawing.Point(762, 7);
            this.m_chkTui.Name = "m_chkTui";
            this.m_chkTui.Size = new System.Drawing.Size(82, 18);
            this.m_chkTui.TabIndex = 20;
            this.m_chkTui.Text = "显示退票";
            this.m_chkTui.UseVisualStyleBackColor = true;
            this.m_chkTui.CheckedChanged += new System.EventHandler(this.m_chkTui_CheckedChanged);
            // 
            // txtEmp
            // 
            this.txtEmp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmp.intHeight = 150;
            this.txtEmp.IsEnterShow = true;
            this.txtEmp.isHide = 4;
            this.txtEmp.isTxt = 1;
            this.txtEmp.isUpOrDn = 0;
            this.txtEmp.isValuse = 4;
            this.txtEmp.Location = new System.Drawing.Point(413, 11);
            this.txtEmp.m_IsHaveParent = false;
            this.txtEmp.m_strParentName = "";
            this.txtEmp.Name = "txtEmp";
            this.txtEmp.nextCtl = this.m_txtCarNo;
            this.txtEmp.Size = new System.Drawing.Size(97, 24);
            this.txtEmp.TabIndex = 19;
            this.txtEmp.txtValuse = "";
            this.txtEmp.VsLeftOrRight = 1;
            // 
            // m_txtCarNo
            // 
            this.m_txtCarNo.Location = new System.Drawing.Point(553, 12);
            this.m_txtCarNo.Name = "m_txtCarNo";
            this.m_txtCarNo.Size = new System.Drawing.Size(92, 23);
            this.m_txtCarNo.TabIndex = 4;
            this.m_txtCarNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtCarNo_KeyDown);
            // 
            // m_patentName
            // 
            this.m_patentName.Location = new System.Drawing.Point(689, 12);
            this.m_patentName.Name = "m_patentName";
            this.m_patentName.Size = new System.Drawing.Size(56, 23);
            this.m_patentName.TabIndex = 5;
            this.m_patentName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCat_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(378, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 18;
            this.label10.Text = "科室";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 17;
            this.label6.Text = "到";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "从";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.m_txtDoc);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtAge);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtSex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtDeptName);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(248, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(780, 37);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            // 
            // label19
            // 
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Location = new System.Drawing.Point(619, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 1);
            this.label19.TabIndex = 26;
            this.label19.Text = "label19";
            // 
            // m_txtDoc
            // 
            this.m_txtDoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtDoc.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_txtDoc.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_txtDoc.Location = new System.Drawing.Point(620, 14);
            this.m_txtDoc.Name = "m_txtDoc";
            this.m_txtDoc.ReadOnly = true;
            this.m_txtDoc.Size = new System.Drawing.Size(120, 19);
            this.m_txtDoc.TabIndex = 25;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(545, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 14);
            this.label18.TabIndex = 24;
            this.label18.Text = "就诊医生:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_rdbinject);
            this.panel4.Controls.Add(this.m_rdbscout);
            this.panel4.Controls.Add(this.m_rdbbottle);
            this.panel4.Controls.Add(this.m_rdbcure);
            this.panel4.Controls.Add(this.m_rdboperation);
            this.panel4.Controls.Add(this.m_rdbblood);
            this.panel4.Location = new System.Drawing.Point(248, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(481, 33);
            this.panel4.TabIndex = 25;
            // 
            // m_rdbinject
            // 
            this.m_rdbinject.Location = new System.Drawing.Point(8, 8);
            this.m_rdbinject.Name = "m_rdbinject";
            this.m_rdbinject.Size = new System.Drawing.Size(68, 24);
            this.m_rdbinject.TabIndex = 0;
            this.m_rdbinject.Text = "注射单";
            this.m_rdbinject.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_rdbscout
            // 
            this.m_rdbscout.Location = new System.Drawing.Point(81, 8);
            this.m_rdbscout.Name = "m_rdbscout";
            this.m_rdbscout.Size = new System.Drawing.Size(96, 24);
            this.m_rdbscout.TabIndex = 0;
            this.m_rdbscout.Text = "输液巡视卡";
            this.m_rdbscout.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_rdbbottle
            // 
            this.m_rdbbottle.Location = new System.Drawing.Point(182, 8);
            this.m_rdbbottle.Name = "m_rdbbottle";
            this.m_rdbbottle.Size = new System.Drawing.Size(76, 24);
            this.m_rdbbottle.TabIndex = 0;
            this.m_rdbbottle.Text = "贴瓶单";
            this.m_rdbbottle.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_rdbcure
            // 
            this.m_rdbcure.Location = new System.Drawing.Point(263, 8);
            this.m_rdbcure.Name = "m_rdbcure";
            this.m_rdbcure.Size = new System.Drawing.Size(68, 24);
            this.m_rdbcure.TabIndex = 0;
            this.m_rdbcure.Text = "治疗单";
            this.m_rdbcure.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_rdboperation
            // 
            this.m_rdboperation.Location = new System.Drawing.Point(336, 8);
            this.m_rdboperation.Name = "m_rdboperation";
            this.m_rdboperation.Size = new System.Drawing.Size(68, 24);
            this.m_rdboperation.TabIndex = 0;
            this.m_rdboperation.Text = "手术单";
            this.m_rdboperation.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_rdbblood
            // 
            this.m_rdbblood.Location = new System.Drawing.Point(409, 8);
            this.m_rdbblood.Name = "m_rdbblood";
            this.m_rdbblood.Size = new System.Drawing.Size(68, 24);
            this.m_rdbblood.TabIndex = 0;
            this.m_rdbblood.Text = "输血单";
            this.m_rdbblood.CheckedChanged += new System.EventHandler(this.m_rdbinject_CheckedChanged);
            // 
            // m_cmdPrintPrivew
            // 
            this.m_cmdPrintPrivew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdPrintPrivew.DefaultScheme = true;
            this.m_cmdPrintPrivew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdPrintPrivew.Hint = "";
            this.m_cmdPrintPrivew.Location = new System.Drawing.Point(794, 81);
            this.m_cmdPrintPrivew.Name = "m_cmdPrintPrivew";
            this.m_cmdPrintPrivew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdPrintPrivew.Size = new System.Drawing.Size(73, 32);
            this.m_cmdPrintPrivew.TabIndex = 6;
            this.m_cmdPrintPrivew.Text = "预览(&V)";
            this.m_cmdPrintPrivew.Click += new System.EventHandler(this.m_cmdPrintPrivew_Click);
            // 
            // m_cmdExit
            // 
            this.m_cmdExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdExit.DefaultScheme = true;
            this.m_cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cmdExit.Hint = "";
            this.m_cmdExit.Location = new System.Drawing.Point(940, 81);
            this.m_cmdExit.Name = "m_cmdExit";
            this.m_cmdExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdExit.Size = new System.Drawing.Size(73, 32);
            this.m_cmdExit.TabIndex = 6;
            this.m_cmdExit.Text = "退出(&E)";
            this.m_cmdExit.Click += new System.EventHandler(this.m_cmdExit_Click);
            // 
            // m_cmdSinature
            // 
            this.m_cmdSinature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.m_cmdSinature.DefaultScheme = true;
            this.m_cmdSinature.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSinature.Hint = "";
            this.m_cmdSinature.Location = new System.Drawing.Point(724, 81);
            this.m_cmdSinature.Name = "m_cmdSinature";
            this.m_cmdSinature.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSinature.Size = new System.Drawing.Size(69, 32);
            this.m_cmdSinature.TabIndex = 6;
            this.m_cmdSinature.Text = "签名(&S)";
            this.m_cmdSinature.Click += new System.EventHandler(this.m_cmdSinature_Click);
            // 
            // m_lsvSinature
            // 
            this.m_lsvSinature.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvSinature.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader14});
            this.m_lsvSinature.ContextMenu = this.m_contextMenuAlterName;
            this.m_lsvSinature.ForeColor = System.Drawing.Color.Green;
            this.m_lsvSinature.FullRowSelect = true;
            this.m_lsvSinature.GridLines = true;
            this.m_lsvSinature.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_lsvSinature.HideSelection = false;
            this.m_lsvSinature.Location = new System.Drawing.Point(821, 116);
            this.m_lsvSinature.MultiSelect = false;
            this.m_lsvSinature.Name = "m_lsvSinature";
            this.m_lsvSinature.Size = new System.Drawing.Size(206, 512);
            this.m_lsvSinature.TabIndex = 26;
            this.m_lsvSinature.UseCompatibleStateImageBehavior = false;
            this.m_lsvSinature.View = System.Windows.Forms.View.Details;
            this.m_lsvSinature.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_lsvSinature_MouseDown);
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "签名";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "类型";
            this.columnHeader16.Width = 50;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "时间";
            this.columnHeader14.Width = 150;
            // 
            // m_contextMenuAlterName
            // 
            this.m_contextMenuAlterName.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAlterName,
            this.DelmenuItem});
            // 
            // menuItemAlterName
            // 
            this.menuItemAlterName.Index = 0;
            this.menuItemAlterName.Text = "修改签名";
            this.menuItemAlterName.Click += new System.EventHandler(this.menuItemAlterName_Click);
            // 
            // DelmenuItem
            // 
            this.DelmenuItem.Index = 1;
            this.DelmenuItem.Text = "删除签名";
            this.DelmenuItem.Click += new System.EventHandler(this.DelmenuItem_Click);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label17.Location = new System.Drawing.Point(848, 632);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(168, 16);
            this.label17.TabIndex = 27;
            this.label17.Text = "说明:右键修改签名";
            // 
            // m_lblOUTPATRECIPEID_CHR
            // 
            this.m_lblOUTPATRECIPEID_CHR.AutoSize = true;
            this.m_lblOUTPATRECIPEID_CHR.Location = new System.Drawing.Point(384, 36);
            this.m_lblOUTPATRECIPEID_CHR.Name = "m_lblOUTPATRECIPEID_CHR";
            this.m_lblOUTPATRECIPEID_CHR.Size = new System.Drawing.Size(42, 14);
            this.m_lblOUTPATRECIPEID_CHR.TabIndex = 28;
            this.m_lblOUTPATRECIPEID_CHR.Text = "性别:";
            this.m_lblOUTPATRECIPEID_CHR.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "button1(&G)";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // m_lblRecordCount
            // 
            this.m_lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblRecordCount.BackColor = System.Drawing.SystemColors.Control;
            this.m_lblRecordCount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.m_lblRecordCount.Location = new System.Drawing.Point(42, 630);
            this.m_lblRecordCount.Name = "m_lblRecordCount";
            this.m_lblRecordCount.Size = new System.Drawing.Size(168, 16);
            this.m_lblRecordCount.TabIndex = 30;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnPrint.DefaultScheme = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnPrint.Hint = "";
            this.btnPrint.Location = new System.Drawing.Point(868, 81);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnPrint.Size = new System.Drawing.Size(71, 32);
            this.btnPrint.TabIndex = 31;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frminjectInfo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.CancelButton = this.m_cmdExit;
            this.ClientSize = new System.Drawing.Size(1028, 649);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.m_lblRecordCount);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_lsvSinature);
            this.Controls.Add(this.m_cmdSinature);
            this.Controls.Add(this.m_cmdExit);
            this.Controls.Add(this.m_cmdPrintPrivew);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_lblOUTPATRECIPEID_CHR);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frminjectInfo";
            this.Text = "护士工作站注射治疗查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frminjectInfo_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frminjectInfo_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frminjectInfo_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        public override void CreateController()
        {

            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_injectInfo();
            objController.Set_GUI_Apperance(this);
        }
        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            //				((clsCtl_injectInfo)this.objController).m_mthGetPatinetInfo();
        }
        public string m_strHospitalTitle;
        /// <summary>
        /// 门诊护士工作站的使用模式，0-默认使用格式；1-茶山使用模式（特殊需求）
        /// </summary>
        public string m_strUseMode = "0";
        /// <summary>
        /// 巡视卡格式 0-默认格式；1-伦教格式
        /// </summary>
        public string m_strXskStyle = "0";
        private PrintPreviewControl ppc = null;
        /// <summary>
        /// 11位卡号标志,此值没实际用途
        /// </summary>
        private bool CardNo11Flag = false;

        private void frminjectInfo_Load(object sender, System.EventArgs e)
        {
            this.m_strHospitalTitle = this.objController.m_objComInfo.m_strGetHospitalTitle();
            com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Init(m_strHospitalTitle, this.m_txtCarNo, ref CardNo11Flag); // 初始化卡号控件
            this.cmbCat.SelectedIndex = 0;
            ((clsCtl_injectInfo)this.objController).m_mthGetPatinetInfo(this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            label9.Text = "病人列表(当天)";
            txtCardID.Focus();
            foreach (System.Windows.Forms.Control c in printPreviewDialog1.Controls)
            {
                if (c is PrintPreviewControl)
                {
                    ppc = c as PrintPreviewControl;

                }
            }
            ppc.Zoom = 1;

            this.m_strXskStyle = this.objController.m_objComInfo.m_lonGetModuleInfo("1400");
            this.objController.m_objComInfo.m_lonGetModuleInfo("0041");
            ((clsCtl_injectInfo)this.objController).m_mthFillDeptdesc();
            if (this.listView_Patient.Items.Count > 0)
            {
                listView_Patient.Items[0].Selected = true;
                #region 设置选择第一组方号选择
                if (listView_Patient.SelectedItems.Count > 0 && listView1.Items.Count > 0)
                {
                    listView1.Items[0].Selected = true;
                    listView1.Items[0].Checked = true;
                    ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                    if (m_lsvSinature.Items.Count > 0)
                    {
                        m_lsvSinature.Items[0].Selected = true;
                    }
                }
                #endregion
            }

        }

        private void btExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void txtCardID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //			if(e.KeyCode==Keys.Enter)
            //			{
            //				if(this.txtCardID.Text.Length<10)
            //				{
            //					string strCardID = "";
            //					strCardID = "0000000000"+this.txtCardID.Text;
            //					this.txtCardID.Text = strCardID.Substring(strCardID.Length-10);
            //				}
            ////			((clsCtl_injectInfo)this.objController).m_mthFindFromList(0,this.txtCardID.Text);
            //			}
        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //			if(e.KeyCode==Keys.Enter&&txtName.Text!="")
            //			{
            //				((clsCtl_injectInfo)this.objController).m_mthFindFromList(1,this.txtName.Text);
            //			}
        }

        private void listView2_Leave(object sender, System.EventArgs e)
        {
        }

        private void listView2_DoubleClick(object sender, System.EventArgs e)
        {
        }

        private void listView2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.listView2_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Escape)
            {

            }
        }

        private void cmbCat_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //((clsCtl_injectInfo)this.objController).m_mthGetPatinetInfo(this.dateTimePicker1.Value,this.dateTimePicker2.Value);
            ((clsCtl_injectInfo)this.objController).m_mthSetRiadioButtonCheck(false);
            this.Cursor = Cursors.Default;

            this.btnPrint.Enabled = false;

            switch (cmbCat.SelectedIndex)
            {
                case 0:
                    m_rdbinject.Enabled = true;
                    if (this.m_strHospitalTitle.Contains("茶山"))
                    {
                        this.m_rdbbottle.Checked = true;
                        this.m_strUseMode = "1";
                    }
                    else
                        m_rdbinject.Checked = true;
                    m_rdbscout.Enabled = true;
                    m_rdbbottle.Enabled = true;

                    m_rdbcure.Enabled = false;
                    m_rdboperation.Enabled = false;
                    m_rdbblood.Enabled = false;

                    this.btnPrint.Enabled = true;

                    break;
                case 1:
                    m_rdbinject.Enabled = false;
                    m_rdbscout.Enabled = false;
                    m_rdbbottle.Enabled = false;
                    this.btnPrint.Enabled = true;
                    m_rdbcure.Enabled = true;
                    m_rdboperation.Enabled = false;
                    m_rdbblood.Enabled = false;
                    m_rdbcure.Checked = true;
                    break;
                case 2:
                    m_rdbinject.Enabled = false;
                    m_rdbscout.Enabled = false;
                    m_rdbbottle.Enabled = false;

                    m_rdbcure.Enabled = false;
                    m_rdboperation.Enabled = true;
                    m_rdbblood.Enabled = false;
                    m_rdboperation.Checked = true;
                    break;
                case 3:
                    m_rdbinject.Enabled = false;
                    m_rdbscout.Enabled = false;
                    m_rdbbottle.Enabled = false;

                    m_rdbcure.Enabled = false;
                    m_rdboperation.Enabled = false;
                    m_rdbblood.Enabled = true;
                    m_rdbblood.Checked = true;
                    break;
            }
        }

        private void btFind_Click(object sender, System.EventArgs e)
        {
            m_mthQueryNewMethod();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void m_mthQueryNewMethod()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(this.m_txtCarNo.Text))
                {
                    m_txtCarNo.Text = com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Value(this.m_txtCarNo.Text.Trim());
                    m_txtCarNo.Text = this.m_txtCarNo.Text.Trim().PadLeft(10, '0');
                }
                ((clsCtl_injectInfo)this.objController).m_mthGetPatinetInfo(this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                label9.Text = "病人列表(" + this.dateTimePicker1.Value.ToString("MM.dd") + "-" + this.dateTimePicker2.Value.ToString("MM.dd") + ")";
                if (this.listView_Patient.Items.Count > 0)
                    listView_Patient.Items[0].Selected = true;

                #region 设置选择第一组方号选择
                if (listView_Patient.SelectedItems.Count > 0 && listView1.Items.Count > 0)
                {
                    listView1.Items[0].Selected = true;
                    listView1.Items[0].Checked = true;
                    ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                    if (m_lsvSinature.Items.Count > 0)
                    {
                        m_lsvSinature.Items[0].Selected = true;
                    }
                }
                #endregion
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void listView_Usage_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void listView_Patient_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.listView_Patient.SelectedItems.Count == 1)
            {
                m_lsvSinature.Items.Clear();
                ((clsCtl_injectInfo)this.objController).m_mthFillDataToTextBox((DataRow)this.listView_Patient.SelectedItems[0].Tag);
                ((clsCtl_injectInfo)this.objController).m_mthFindData(this.listView_Patient.SelectedItems[0]);

            }
            #region 设置选择第一组方号选择
            if (listView_Patient.SelectedItems.Count > 0 && listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Checked = true;
                ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                if (m_lsvSinature.Items.Count > 0)
                {
                    m_lsvSinature.Items[0].Selected = true;
                }
            }
            #endregion
        }

        private void listView_Usage_DoubleClick(object sender, System.EventArgs e)
        {
            btFind_Click(null, null);
        }

        private void btPrint_Click(object sender, System.EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void buttonXP3_Click(object sender, System.EventArgs e)
        {

        }

        private void buttonXP2_Click(object sender, System.EventArgs e)
        {

            if (cmbCat.SelectedIndex == 1)
            {
                ((clsCtl_injectInfo)this.objController).m_mthPrintData(2);
            }
            else
            {
                MessageBox.Show("请先选择相应的分类!", "Icare");
            }
        }

        private void buttonXP1_Click(object sender, System.EventArgs e)
        {
            if (cmbCat.SelectedIndex == 0)
            {
                ((clsCtl_injectInfo)this.objController).m_mthPrintData(1);
            }
            else
            {
                MessageBox.Show("请先选择相应的分类!", "Icare");
            }
        }

        private void buttonXP5_Click(object sender, System.EventArgs e)
        {
            if (cmbCat.SelectedIndex == 3)
            {
                ((clsCtl_injectInfo)this.objController).m_mthPrintData(4);
            }
            else
            {
                MessageBox.Show("请先选择相应的分类!", "Icare");
            }
        }

        private void buttonXP4_Click(object sender, System.EventArgs e)
        {
            if (cmbCat.SelectedIndex == 2)
            {
                ((clsCtl_injectInfo)this.objController).m_mthPrintData(3);
            }
            else
            {
                MessageBox.Show("请先选择相应的分类!", "Icare");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthPrint(e);
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthGetData(false);
        }

        private void buttonXP3_Click_1(object sender, System.EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = DateTime.Now;
            ((clsCtl_injectInfo)this.objController).m_mthGetPatinetInfo(DateTime.Now, DateTime.Now);
            label9.Text = "病人列表(当天)";
        }

        private void buttonXP6_Click(object sender, System.EventArgs e)
        {
            if (listView_Patient.Items.Count > 0 && listView_Patient.SelectedItems.Count > 0)
            {
                DataRow seleRow = (DataRow)listView_Patient.SelectedItems[0].Tag;
                com.digitalwave.iCare.middletier.HI.clsInjectPrint injectPrint = new com.digitalwave.iCare.middletier.HI.clsInjectPrint(seleRow["OUTPATRECIPEID_CHR"].ToString(), this.objController.m_objComInfo.m_strGetHospitalTitle());
                injectPrint.m_mthPrint();
            }
        }

        private void txtCardID_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCardID.Text))
            {
                txtCardID.Text = com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Value(txtCardID.Text.Trim());
                txtCardID.Text = txtCardID.Text.Trim().PadLeft(10, '0');
            }
        }

        private void m_txtCarNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.m_txtCarNo.Text))
                {
                    m_txtCarNo.Text = com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Value(m_txtCarNo.Text.Trim());
                    m_txtCarNo.Text = m_txtCarNo.Text.Trim().PadLeft(10, '0');
                }
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbCat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void m_cmdExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private bool m_blnRuning = true;
        private void listView1_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (m_blnRuning == true)
            {
                m_blnRuning = false;
                if (e.CurrentValue.ToString() == "Unchecked")
                {
                    ((clsCtl_injectInfo)this.objController).m_mthSelectListViewCheckBox(this.listView1, e.Index, true);
                }
                else
                {
                    ((clsCtl_injectInfo)this.objController).m_mthSelectListViewCheckBox(this.listView1, e.Index, false);

                }
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    if (i != e.Index)
                        this.listView1.Items[i].Selected = false;
                    else
                        this.listView1.Items[i].Selected = true;
                }
                m_blnRuning = true;
            }

        }

        private void m_cmdPrintPrivew_Click(object sender, System.EventArgs e)
        {
            if (this.listView_Patient.SelectedItems.Count > 0)
                ((clsCtl_injectInfo)this.objController).m_mthPrintPrivew(true, this.listView_Patient.SelectedItems[0]);
        }

        private void menuItemAlterName_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthAlterName(this.m_lsvSinature);
        }

        private void m_cmdSinature_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthSinature(listView1);
        }

        private void listView1_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
        }

        private void m_rdbinject_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.listView_Patient.SelectedItems.Count > 0)
            {
                ((clsCtl_injectInfo)this.objController).m_mthFindData(this.listView_Patient.SelectedItems[0]);
                ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                #region 设置选择第一组方号选择
                if (listView1.Items.Count > 0)
                {
                    listView1.Items[0].Selected = true;
                    listView1.Items[0].Checked = true;
                    ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                    if (m_lsvSinature.Items.Count > 0)
                    {
                        m_lsvSinature.Items[0].Selected = true;
                    }
                }
                else
                {
                    m_lsvSinature.Items.Clear();
                }
                #endregion
            }

        }

        private void m_lsvSinature_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_intListViewAllSelected(listView1, true);
            //((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1,this.m_lsvSinature,true);	

        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_intListViewAllSelected(listView1, false);
            this.m_lsvSinature.Items.Clear();
        }

        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            if (this.m_frmAllergichintShow != null)
            {
                this.m_frmAllergichintShow.Hide();
            }
            ((clsCtl_injectInfo)this.objController).m_mthAllergicManage(listView_Patient);
            ((clsCtl_injectInfo)this.objController).m_mthGetAllergic();


        }

        private void listView_Patient_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthGetAllergic();
            for (int i = 0; i < this.listView_Patient.Items.Count; i++)
            {
                DataRow dr = (DataRow)listView_Patient.Items[i].Tag;
                if (dr["PSTAUTS_INT"].ToString().Trim() != "2")
                {
                    if (dr["PSTAUTS_INT"].ToString().Trim() != "-2")
                    {
                        listView_Patient.Items[i].ForeColor = Color.CadetBlue;
                    }
                    else
                    {
                        listView_Patient.Items[i].ForeColor = Color.Red;
                    }
                }
            }
            #region 设置选择第一组方号选择
            if (listView_Patient.SelectedItems.Count > 0 && listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Checked = true;
                ((clsCtl_injectInfo)this.objController).m_mthQueryOPERATORID_CHRAndName(listView1, this.m_lsvSinature, false);
                if (m_lsvSinature.Items.Count > 0)
                {
                    m_lsvSinature.Items[0].Selected = true;
                }
            }
            #endregion
        }

        private void frminjectInfo_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.m_frmAllergichintShow != null)
            {
                this.m_frmAllergichintShow.Close();
            }
        }

        private void frminjectInfo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.G)
                {
                    ((clsCtl_injectInfo)this.objController).m_mthGetAllergic();
                }
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthGetAllergic();
        }

        private void button1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void listView_Patient_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void DelmenuItem_Click(object sender, EventArgs e)
        {
            ((clsCtl_injectInfo)this.objController).m_mthDeleteName(this.m_lsvSinature);
        }

        private void m_chkTui_CheckedChanged(object sender, EventArgs e)
        {
            //m_mthQueryNewMethod();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.listView_Patient.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "请先选择病人！", "iCare提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            //for (int i = 0; i < this.listView_Patient.SelectedItems.Count; i++)
            //{
            //    m_lsvSinature.Items.Clear();
            //    ((clsCtl_injectInfo)this.objController).m_mthFillDataToTextBox((DataRow)this.listView_Patient.SelectedItems[i].Tag);
            //    ((clsCtl_injectInfo)this.objController).m_mthFindData(this.listView_Patient.SelectedItems[i]);
            //    ((clsCtl_injectInfo)this.objController).m_mthPrintPrivew(false, this.listView_Patient.SelectedItems[i]);
            //    ((clsCtl_injectInfo)this.objController).m_mthUpdatePrintFlag(this.listView_Patient.SelectedItems[i]);
            //}
            foreach (ListViewItem lvi in this.listView_Patient.SelectedItems)
            {
                m_lsvSinature.Items.Clear();
                ((clsCtl_injectInfo)this.objController).m_mthFillDataToTextBox((DataRow)lvi.Tag);
                ((clsCtl_injectInfo)this.objController).m_mthFindData(lvi);
                ((clsCtl_injectInfo)this.objController).m_mthPrintPrivew(false, lvi);
                ((clsCtl_injectInfo)this.objController).m_mthUpdatePrintFlag(lvi);
            }
        }
    }
}
