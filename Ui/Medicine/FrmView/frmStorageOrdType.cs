using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 单据类型窗口
	/// Create by kong 2004-06-06
	/// </summary>
	public class frmStorageOrdType : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
	{
		#region 窗体代码
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
		internal System.Windows.Forms.ComboBox m_cboSign;
		internal System.Windows.Forms.ComboBox m_cboDeptType;
		private System.Windows.Forms.GroupBox groupBox2;
		internal PinkieControls.ButtonXP m_cmdNew;
		internal PinkieControls.ButtonXP m_cmdModify;
		internal PinkieControls.ButtonXP m_cmdSave;
        internal PinkieControls.ButtonXP m_cmdDelete;
		internal PinkieControls.ButtonXP m_cmdClose;
		internal System.Windows.Forms.ListView m_lsvDetail;
		private System.Windows.Forms.ColumnHeader clhStorageOrdTypeName;
		private System.Windows.Forms.ColumnHeader clhSign;
        private System.Windows.Forms.ColumnHeader clhDeptType;
		internal System.Windows.Forms.TextBox m_txtStorageOrdTypeID;
		private System.Windows.Forms.ColumnHeader ordTypeID;
        internal TextBox textBox1;
        private Label label1;
        internal CheckBox checkBox1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        internal TextBox m_txtStorageOrdTypeName;
        private Label label5;
        internal CheckBox checkBox2;
        private ColumnHeader columnHeader3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region 构造函数
		public frmStorageOrdType()
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.m_lsvDetail = new System.Windows.Forms.ListView();
            this.ordTypeID = new System.Windows.Forms.ColumnHeader();
            this.clhStorageOrdTypeName = new System.Windows.Forms.ColumnHeader();
            this.clhSign = new System.Windows.Forms.ColumnHeader();
            this.clhDeptType = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.m_cmdClose = new PinkieControls.ButtonXP();
            this.m_cmdDelete = new PinkieControls.ButtonXP();
            this.m_cmdSave = new PinkieControls.ButtonXP();
            this.m_cmdModify = new PinkieControls.ButtonXP();
            this.m_cmdNew = new PinkieControls.ButtonXP();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_txtStorageOrdTypeName = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtStorageOrdTypeID = new System.Windows.Forms.TextBox();
            this.m_cboDeptType = new System.Windows.Forms.ComboBox();
            this.m_cboSign = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lsvDetail
            // 
            this.m_lsvDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lsvDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ordTypeID,
            this.clhStorageOrdTypeName,
            this.clhSign,
            this.clhDeptType,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_lsvDetail.FullRowSelect = true;
            this.m_lsvDetail.GridLines = true;
            this.m_lsvDetail.Location = new System.Drawing.Point(304, 8);
            this.m_lsvDetail.Name = "m_lsvDetail";
            this.m_lsvDetail.Size = new System.Drawing.Size(611, 600);
            this.m_lsvDetail.TabIndex = 2;
            this.m_lsvDetail.UseCompatibleStateImageBehavior = false;
            this.m_lsvDetail.View = System.Windows.Forms.View.Details;
            this.m_lsvDetail.DoubleClick += new System.EventHandler(this.m_lsvDetail_DoubleClick);
            // 
            // ordTypeID
            // 
            this.ordTypeID.Text = "单据ID";
            // 
            // clhStorageOrdTypeName
            // 
            this.clhStorageOrdTypeName.Text = "单据类型名称 ";
            this.clhStorageOrdTypeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhStorageOrdTypeName.Width = 200;
            // 
            // clhSign
            // 
            this.clhSign.Text = "单据类别";
            this.clhSign.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhSign.Width = 97;
            // 
            // clhDeptType
            // 
            this.clhDeptType.Text = "院内外标识";
            this.clhDeptType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clhDeptType.Width = 85;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "单据号标识";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "药房申请";
            this.columnHeader2.Width = 82;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "药房退药";
            this.columnHeader3.Width = 82;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.m_cmdClose);
            this.groupBox2.Controls.Add(this.m_cmdDelete);
            this.groupBox2.Controls.Add(this.m_cmdSave);
            this.groupBox2.Controls.Add(this.m_cmdModify);
            this.groupBox2.Controls.Add(this.m_cmdNew);
            this.groupBox2.Location = new System.Drawing.Point(0, 318);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 290);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // m_cmdClose
            // 
            this.m_cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdClose.DefaultScheme = true;
            this.m_cmdClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdClose.Hint = "";
            this.m_cmdClose.Location = new System.Drawing.Point(48, 238);
            this.m_cmdClose.Name = "m_cmdClose";
            this.m_cmdClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdClose.Size = new System.Drawing.Size(184, 40);
            this.m_cmdClose.TabIndex = 4;
            this.m_cmdClose.Text = "退出(&E)";
            this.m_cmdClose.Click += new System.EventHandler(this.m_cmdClose_Click);
            // 
            // m_cmdDelete
            // 
            this.m_cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdDelete.DefaultScheme = true;
            this.m_cmdDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdDelete.Hint = "";
            this.m_cmdDelete.Location = new System.Drawing.Point(48, 187);
            this.m_cmdDelete.Name = "m_cmdDelete";
            this.m_cmdDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdDelete.Size = new System.Drawing.Size(184, 40);
            this.m_cmdDelete.TabIndex = 3;
            this.m_cmdDelete.Text = "删除(&D)";
            this.m_cmdDelete.Click += new System.EventHandler(this.m_cmdDelete_Click);
            // 
            // m_cmdSave
            // 
            this.m_cmdSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdSave.DefaultScheme = true;
            this.m_cmdSave.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdSave.Hint = "";
            this.m_cmdSave.Location = new System.Drawing.Point(48, 34);
            this.m_cmdSave.Name = "m_cmdSave";
            this.m_cmdSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdSave.Size = new System.Drawing.Size(184, 40);
            this.m_cmdSave.TabIndex = 0;
            this.m_cmdSave.Text = "保存(&S)";
            this.m_cmdSave.Click += new System.EventHandler(this.m_cmdSave_Click);
            // 
            // m_cmdModify
            // 
            this.m_cmdModify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdModify.DefaultScheme = true;
            this.m_cmdModify.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdModify.Hint = "";
            this.m_cmdModify.Location = new System.Drawing.Point(48, 136);
            this.m_cmdModify.Name = "m_cmdModify";
            this.m_cmdModify.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdModify.Size = new System.Drawing.Size(184, 40);
            this.m_cmdModify.TabIndex = 2;
            this.m_cmdModify.Text = "修改(&R)";
            this.m_cmdModify.Click += new System.EventHandler(this.m_cmdModify_Click);
            // 
            // m_cmdNew
            // 
            this.m_cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_cmdNew.DefaultScheme = true;
            this.m_cmdNew.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_cmdNew.Hint = "";
            this.m_cmdNew.Location = new System.Drawing.Point(48, 85);
            this.m_cmdNew.Name = "m_cmdNew";
            this.m_cmdNew.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_cmdNew.Size = new System.Drawing.Size(184, 40);
            this.m_cmdNew.TabIndex = 1;
            this.m_cmdNew.Text = "新增(&A)";
            this.m_cmdNew.Click += new System.EventHandler(this.m_cmdNew_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.m_txtStorageOrdTypeName);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_txtStorageOrdTypeID);
            this.groupBox1.Controls.Add(this.m_cboDeptType);
            this.groupBox1.Controls.Add(this.m_cboSign);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 283);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(110, 259);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(152, 18);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "药房退药品使用单据";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 188);
            this.textBox1.MaxLength = 2;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(38, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.DarkOrange;
            this.label5.Location = new System.Drawing.Point(159, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "[两位英文字母]";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_txtStorageOrdTypeName
            // 
            this.m_txtStorageOrdTypeName.Location = new System.Drawing.Point(110, 41);
            this.m_txtStorageOrdTypeName.MaxLength = 30;
            this.m_txtStorageOrdTypeName.Name = "m_txtStorageOrdTypeName";
            this.m_txtStorageOrdTypeName.Size = new System.Drawing.Size(160, 23);
            this.m_txtStorageOrdTypeName.TabIndex = 0;
            this.m_txtStorageOrdTypeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtStorageOrdTypeName_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(110, 227);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(166, 18);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "药房申请药品使用单据";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "单据号标识";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // m_txtStorageOrdTypeID
            // 
            this.m_txtStorageOrdTypeID.Location = new System.Drawing.Point(110, 2);
            this.m_txtStorageOrdTypeID.Name = "m_txtStorageOrdTypeID";
            this.m_txtStorageOrdTypeID.Size = new System.Drawing.Size(160, 23);
            this.m_txtStorageOrdTypeID.TabIndex = 8;
            this.m_txtStorageOrdTypeID.Visible = false;
            // 
            // m_cboDeptType
            // 
            this.m_cboDeptType.Items.AddRange(new object[] {
            "院外",
            "院内"});
            this.m_cboDeptType.Location = new System.Drawing.Point(110, 139);
            this.m_cboDeptType.Name = "m_cboDeptType";
            this.m_cboDeptType.Size = new System.Drawing.Size(160, 22);
            this.m_cboDeptType.TabIndex = 2;
            this.m_cboDeptType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboDeptType_KeyDown);
            // 
            // m_cboSign
            // 
            this.m_cboSign.Items.AddRange(new object[] {
            "入库单据",
            "出库单据",
            "盘点单据",
            "调价单据"});
            this.m_cboSign.Location = new System.Drawing.Point(110, 90);
            this.m_cboSign.Name = "m_cboSign";
            this.m_cboSign.Size = new System.Drawing.Size(160, 22);
            this.m_cboSign.TabIndex = 1;
            this.m_cboSign.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_cboSign_KeyDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "院内外标识";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "单 据 类 别";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "单据类型名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmStorageOrdType
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(912, 621);
            this.Controls.Add(this.m_lsvDetail);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmStorageOrdType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据类型维护";
            this.Load += new System.EventHandler(this.frmStorageOrdType_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
	
		#region 控件事件
		public override void CreateController()
		{
			// TODO:  添加 frmStorageOrdType.CreateController 实现
			this.objController = new clsControlStorageOrdType();
			this.objController.Set_GUI_Apperance(this);
		}

		private void frmStorageOrdType_Load(object sender, System.EventArgs e)
		{
			base.m_mthSetFormControlCanBeNull(this);
            this.m_mthSetEnter2Tab(new System.Windows.Forms.Control[] { });
			((clsControlStorageOrdType)this.objController).m_mthGetStorageOrdTypeList();
		}

		private void m_cmdClear_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthClear();
		}

		private void m_cmdSave_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthDoSave();
		}

		private void m_cmdNew_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthNew();
		}

		private void m_cmdModify_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthModify();
		}

		private void m_cmdDelete_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthDoDelete();
		}

		private void m_cmdRefersh_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthGetStorageOrdTypeList();
		}

		private void m_cmdPrint_Click(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthPrint();
		}

		private void m_cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void m_lsvDetail_DoubleClick(object sender, System.EventArgs e)
		{
			((clsControlStorageOrdType)this.objController).m_mthModify();
		}
		#endregion

		private void groupBox2_Enter(object sender, System.EventArgs e)
		{
		
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void m_txtStorageOrdTypeName_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboDeptType_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void m_cboSign_KeyDown(object sender, KeyEventArgs e)
        {
            this.m_mthSetKeyTab(e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
        }
	}
}
