using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// 月末结算
	/// </summary>
	public class frmMonthBalance : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		internal PinkieControls.ButtonXP m_cmdConfirm;
		internal PinkieControls.ButtonXP m_cmdCancel;
		internal System.Windows.Forms.TextBox m_txtPeriod;
		protected internal System.Windows.Forms.TextBox m_txtStorage;
		internal System.Windows.Forms.TextBox m_txtLoan;
		internal System.Windows.Forms.TextBox m_txtBorrow;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMonthBalance()
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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.m_txtBorrow = new System.Windows.Forms.TextBox();
			this.m_txtLoan = new System.Windows.Forms.TextBox();
			this.m_txtStorage = new System.Windows.Forms.TextBox();
			this.m_txtPeriod = new System.Windows.Forms.TextBox();
			this.m_cmdCancel = new PinkieControls.ButtonXP();
			this.m_cmdConfirm = new PinkieControls.ButtonXP();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.m_txtBorrow);
			this.groupBox1.Controls.Add(this.m_txtLoan);
			this.groupBox1.Controls.Add(this.m_txtStorage);
			this.groupBox1.Controls.Add(this.m_txtPeriod);
			this.groupBox1.Controls.Add(this.m_cmdCancel);
			this.groupBox1.Controls.Add(this.m_cmdConfirm);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(656, 104);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// m_txtBorrow
			// 
			//this.m_txtBorrow.EnableAutoValidation = false;
			//this.m_txtBorrow.EnableEnterKeyValidate = true;
			//this.m_txtBorrow.EnableEscapeKeyUndo = true;
			//this.m_txtBorrow.EnableLastValidValue = true;
			//this.m_txtBorrow.ErrorProvider = null;
			//this.m_txtBorrow.ErrorProviderMessage = "Invalid value";
			//this.m_txtBorrow.ForceFormatText = true;
			this.m_txtBorrow.Location = new System.Drawing.Point(80, 66);
			this.m_txtBorrow.Name = "m_txtBorrow";
			//this.m_txtBorrow.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtBorrow.Size = new System.Drawing.Size(104, 23);
			this.m_txtBorrow.TabIndex = 9;
			this.m_txtBorrow.Text = "";
			this.m_txtBorrow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_txtLoan
			// 
			//this.m_txtLoan.EnableAutoValidation = false;
			//this.m_txtLoan.EnableEnterKeyValidate = true;
			//this.m_txtLoan.EnableEscapeKeyUndo = true;
			//this.m_txtLoan.EnableLastValidValue = true;
			//this.m_txtLoan.ErrorProvider = null;
			//this.m_txtLoan.ErrorProviderMessage = "Invalid value";
			//this.m_txtLoan.ForceFormatText = true;
			this.m_txtLoan.Location = new System.Drawing.Point(328, 66);
			this.m_txtLoan.Name = "m_txtLoan";
			//this.m_txtLoan.NumericCharStyle = ((SourceLibrary.Windows.Forms.NumericCharStyle)((SourceLibrary.Windows.Forms.NumericCharStyle.DecimalSeparator | SourceLibrary.Windows.Forms.NumericCharStyle.NegativeSymbol)));
			this.m_txtLoan.TabIndex = 8;
			this.m_txtLoan.Text = "";
			this.m_txtLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// m_txtStorage
			// 
			//this.m_txtStorage.EnableAutoValidation = false;
			//this.m_txtStorage.EnableEnterKeyValidate = true;
			//this.m_txtStorage.EnableEscapeKeyUndo = true;
			//this.m_txtStorage.EnableLastValidValue = true;
			//this.m_txtStorage.ErrorProvider = null;
			//this.m_txtStorage.ErrorProviderMessage = "Invalid value";
			//this.m_txtStorage.ForceFormatText = true;
			this.m_txtStorage.Location = new System.Drawing.Point(80, 16);
			this.m_txtStorage.Name = "m_txtStorage";
			this.m_txtStorage.TabIndex = 7;
			this.m_txtStorage.Text = "";
			// 
			// m_txtPeriod
			// 
			//this.m_txtPeriod.EnableAutoValidation = false;
			//this.m_txtPeriod.EnableEnterKeyValidate = true;
			//this.m_txtPeriod.EnableEscapeKeyUndo = true;
			//this.m_txtPeriod.EnableLastValidValue = true;
			//this.m_txtPeriod.ErrorProvider = null;
			//this.m_txtPeriod.ErrorProviderMessage = "Invalid value";
			//this.m_txtPeriod.ForceFormatText = true;
			this.m_txtPeriod.Location = new System.Drawing.Point(328, 16);
			this.m_txtPeriod.Name = "m_txtPeriod";
			this.m_txtPeriod.TabIndex = 6;
			this.m_txtPeriod.Text = "";
			// 
			// m_cmdCancel
			// 
			this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdCancel.DefaultScheme = true;
			this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdCancel.Hint = "";
			this.m_cmdCancel.Location = new System.Drawing.Point(528, 64);
			this.m_cmdCancel.Name = "m_cmdCancel";
			this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdCancel.Size = new System.Drawing.Size(112, 32);
			this.m_cmdCancel.TabIndex = 5;
			this.m_cmdCancel.Text = "取消(&C)";
			// 
			// m_cmdConfirm
			// 
			this.m_cmdConfirm.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdConfirm.DefaultScheme = true;
			this.m_cmdConfirm.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdConfirm.Hint = "";
			this.m_cmdConfirm.Location = new System.Drawing.Point(528, 24);
			this.m_cmdConfirm.Name = "m_cmdConfirm";
			this.m_cmdConfirm.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdConfirm.Size = new System.Drawing.Size(112, 32);
			this.m_cmdConfirm.TabIndex = 4;
			this.m_cmdConfirm.Text = "确认(&S)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(256, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 19);
			this.label4.TabIndex = 3;
			this.label4.Text = "贷方金额";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 19);
			this.label3.TabIndex = 2;
			this.label3.Text = "借方金额";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(271, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 19);
			this.label2.TabIndex = 1;
			this.label2.Text = "帐务期";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "库房";
			// 
			// frmMonthBalance
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(656, 525);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.Name = "frmMonthBalance";
			this.Text = "月末结算";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
