using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
namespace  com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmDifficultyReport ��ժҪ˵����
	/// </summary>
	public class frmDifficultyReport :com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.Panel panel1;
		//internal CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label2;
		internal System.Windows.Forms.DateTimePicker dateTimePicker2;
		internal PinkieControls.ButtonXP btOK;
		internal System.Data.DataSet dataSet1;
		internal System.Data.DataTable dataTable1;
		private System.Data.DataColumn date;
		private System.Data.DataColumn PatientName;
		private System.Data.DataColumn DifficultyNo;
		private System.Data.DataColumn InvoiceNo;
		private System.Data.DataColumn RegisterCost;
		private System.Data.DataColumn CheckSelfPay;
		private System.Data.DataColumn CheckChargeUp;
		private System.Data.DataColumn CureSelfPay;
		private System.Data.DataColumn CureChargeUp;
		private System.Data.DataColumn MedicineSelfPay;
		private System.Data.DataColumn MedicineChargeUp;
		private System.Data.DataColumn SumChargeUp;
		private System.Data.DataColumn Operator;
		internal PinkieControls.ButtonXP btExit;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDifficultyReport()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.btExit = new PinkieControls.ButtonXP();
			this.btOK = new PinkieControls.ButtonXP();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			//this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.dataSet1 = new System.Data.DataSet();
			this.dataTable1 = new System.Data.DataTable();
			this.date = new System.Data.DataColumn();
			this.PatientName = new System.Data.DataColumn();
			this.DifficultyNo = new System.Data.DataColumn();
			this.InvoiceNo = new System.Data.DataColumn();
			this.RegisterCost = new System.Data.DataColumn();
			this.CheckSelfPay = new System.Data.DataColumn();
			this.CheckChargeUp = new System.Data.DataColumn();
			this.CureSelfPay = new System.Data.DataColumn();
			this.CureChargeUp = new System.Data.DataColumn();
			this.MedicineSelfPay = new System.Data.DataColumn();
			this.MedicineChargeUp = new System.Data.DataColumn();
			this.SumChargeUp = new System.Data.DataColumn();
			this.Operator = new System.Data.DataColumn();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.btExit);
			this.panel1.Controls.Add(this.btOK);
			this.panel1.Controls.Add(this.dateTimePicker2);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.dateTimePicker1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(648, 60);
			this.panel1.TabIndex = 0;
			// 
			// btExit
			// 
			this.btExit.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btExit.DefaultScheme = true;
			this.btExit.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btExit.Hint = "";
			this.btExit.Location = new System.Drawing.Point(524, 16);
			this.btExit.Name = "btExit";
			this.btExit.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btExit.Size = new System.Drawing.Size(104, 32);
			this.btExit.TabIndex = 5;
			this.btExit.Text = "�˳�(ESC)";
			this.btExit.Click += new System.EventHandler(this.btExit_Click);
			// 
			// btOK
			// 
			this.btOK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btOK.DefaultScheme = true;
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btOK.Hint = "";
			this.btOK.Location = new System.Drawing.Point(372, 16);
			this.btOK.Name = "btOK";
			this.btOK.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btOK.Size = new System.Drawing.Size(104, 32);
			this.btOK.TabIndex = 4;
			this.btOK.Text = "���ɱ���(&F)";
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(204, 20);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(108, 23);
			this.dateTimePicker2.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(176, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(20, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "��";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(60, 20);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(108, 23);
			this.dateTimePicker1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "��:";
			// 
			// crystalReportViewer1
			// 
			//this.crystalReportViewer1.ActiveViewIndex = -1;
			//this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			//	| System.Windows.Forms.AnchorStyles.Left) 
			//	| System.Windows.Forms.AnchorStyles.Right)));
			//this.crystalReportViewer1.DisplayGroupTree = false;
			//this.crystalReportViewer1.Location = new System.Drawing.Point(8, 76);
			//this.crystalReportViewer1.Name = "crystalReportViewer1";
			//this.crystalReportViewer1.ReportSource = null;
			//this.crystalReportViewer1.Size = new System.Drawing.Size(644, 412);
			//this.crystalReportViewer1.TabIndex = 1;
			// 
			// dataSet1
			// 
			this.dataSet1.DataSetName = "NewDataSet";
			this.dataSet1.Locale = new System.Globalization.CultureInfo("zh-CN");
			this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
																		  this.dataTable1});
			// 
			// dataTable1
			// 
			this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
																			  this.date,
																			  this.PatientName,
																			  this.DifficultyNo,
																			  this.InvoiceNo,
																			  this.RegisterCost,
																			  this.CheckSelfPay,
																			  this.CheckChargeUp,
																			  this.CureSelfPay,
																			  this.CureChargeUp,
																			  this.MedicineSelfPay,
																			  this.MedicineChargeUp,
																			  this.SumChargeUp,
																			  this.Operator});
			this.dataTable1.TableName = "Table1";
			// 
			// date
			// 
			this.date.ColumnName = "date";
			// 
			// PatientName
			// 
			this.PatientName.ColumnName = "PatientName";
			// 
			// DifficultyNo
			// 
			this.DifficultyNo.ColumnName = "DifficultyNo";
			// 
			// InvoiceNo
			// 
			this.InvoiceNo.ColumnName = "InvoiceNo";
			// 
			// RegisterCost
			// 
			this.RegisterCost.ColumnName = "RegisterCost";
			this.RegisterCost.DataType = typeof(System.Decimal);
			// 
			// CheckSelfPay
			// 
			this.CheckSelfPay.ColumnName = "CheckSelfPay";
			this.CheckSelfPay.DataType = typeof(System.Decimal);
			// 
			// CheckChargeUp
			// 
			this.CheckChargeUp.ColumnName = "CheckChargeUp";
			this.CheckChargeUp.DataType = typeof(System.Decimal);
			// 
			// CureSelfPay
			// 
			this.CureSelfPay.ColumnName = "CureSelfPay";
			this.CureSelfPay.DataType = typeof(System.Decimal);
			// 
			// CureChargeUp
			// 
			this.CureChargeUp.ColumnName = "CureChargeUp";
			this.CureChargeUp.DataType = typeof(System.Decimal);
			// 
			// MedicineSelfPay
			// 
			this.MedicineSelfPay.ColumnName = "MedicineSelfPay";
			this.MedicineSelfPay.DataType = typeof(System.Decimal);
			// 
			// MedicineChargeUp
			// 
			this.MedicineChargeUp.ColumnName = "MedicineChargeUp";
			this.MedicineChargeUp.DataType = typeof(System.Decimal);
			// 
			// SumChargeUp
			// 
			this.SumChargeUp.ColumnName = "SumChargeUp";
			this.SumChargeUp.DataType = typeof(System.Decimal);
			// 
			// Operator
			// 
			this.Operator.ColumnName = "Operator";
			// 
			// frmDifficultyReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
			this.ClientSize = new System.Drawing.Size(656, 493);
			//this.Controls.Add(this.crystalReportViewer1);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("����", 10.5F);
			this.KeyPreview = true;
			this.Name = "frmDifficultyReport";
			this.Text = "�������˱���";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDifficultyReport_KeyDown);
			this.Load += new System.EventHandler(this.frmDifficultyReport_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		public override void CreateController()
		{
			this.objController=new com.digitalwave.iCare.gui.HIS.clsCtl_DifficultyReport();
			objController.Set_GUI_Apperance(this);
		}
		private void btOK_Click(object sender, System.EventArgs e)
		{
		((clsCtl_DifficultyReport)this.objController).m_mthShowReport();
		}

		private void frmDifficultyReport_Load(object sender, System.EventArgs e)
		{
			
		}

		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmDifficultyReport_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape)
			{
				if(MessageBox.Show("ȷ���˳���?","iCare",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
					return;
				this.Close();
			}
		}
	}
}
