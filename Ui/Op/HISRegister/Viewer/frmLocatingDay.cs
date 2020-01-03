using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmLocatingDay ��ժҪ˵����
	/// </summary>
	public class frmLocatingDay : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ListView listView2;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Label label3;
		private PinkieControls.ButtonXP btnEsc;
		private PinkieControls.ButtonXP btnReset;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmLocatingDay()
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnReset = new PinkieControls.ButtonXP();
			this.btnEsc = new PinkieControls.ButtonXP();
			this.label3 = new System.Windows.Forms.Label();
			this.listView2 = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader8});
			this.listView1.Enabled = false;
			this.listView1.FullRowSelect = true;
			this.listView1.Location = new System.Drawing.Point(8, 40);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(520, 40);
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "�ֽ�ϼ�";
			this.columnHeader1.Width = 80;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "ˢ�����";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "֧Ʊ���";
			this.columnHeader3.Width = 80;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "ҽ�����ʽ��";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "���Ѽ��ʽ��";
			this.columnHeader5.Width = 100;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "�������";
			this.columnHeader8.Width = 80;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("����", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.OrangeRed;
			this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label2.Location = new System.Drawing.Point(120, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(408, 24);
			this.label2.TabIndex = 1;
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 32);
			this.label1.TabIndex = 0;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.btnReset);
			this.panel1.Controls.Add(this.btnEsc);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.listView2);
			this.panel1.Controls.Add(this.listView1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(536, 237);
			this.panel1.TabIndex = 1;
			// 
			// btnReset
			// 
			this.btnReset.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnReset.DefaultScheme = true;
			this.btnReset.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnReset.Hint = "";
			this.btnReset.Location = new System.Drawing.Point(368, 120);
			this.btnReset.Name = "btnReset";
			this.btnReset.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnReset.Size = new System.Drawing.Size(128, 40);
			this.btnReset.TabIndex = 7;
			this.btnReset.Text = "ˢ�£�F5��";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnEsc
			// 
			this.btnEsc.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnEsc.DefaultScheme = true;
			this.btnEsc.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnEsc.Hint = "";
			this.btnEsc.Location = new System.Drawing.Point(368, 184);
			this.btnEsc.Name = "btnEsc";
			this.btnEsc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.btnEsc.Size = new System.Drawing.Size(128, 40);
			this.btnEsc.TabIndex = 6;
			this.btnEsc.Text = "�˳���ESC��";
			this.btnEsc.Click += new System.EventHandler(this.btnEsc_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 24);
			this.label3.TabIndex = 4;
			this.label3.Text = "��Ʊ�ţ�";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listView2
			// 
			this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader7});
			this.listView2.FullRowSelect = true;
			this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView2.Location = new System.Drawing.Point(7, 112);
			this.listView2.MultiSelect = false;
			this.listView2.Name = "listView2";
			this.listView2.Size = new System.Drawing.Size(329, 120);
			this.listView2.TabIndex = 3;
			this.listView2.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "��ʼ��Ʊ��";
			this.columnHeader6.Width = 130;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "������Ʊ��";
			this.columnHeader7.Width = 130;
			// 
			// frmLocatingDay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(536, 237);
			this.Controls.Add(this.panel1);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "frmLocatingDay";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�շѸ���";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLocatingDay_KeyDown);
			this.Load += new System.EventHandler(this.frmLocatingDay_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region ����
		clsDomainControl_Register Domain=new clsDomainControl_Register();
		string checkDate;//���������ʱ��
		private DataTable dtCheckOut=new DataTable();//���淵�ص�����
		private DataTable dtStatistics;//����ͳ�Ʊ�

		#endregion

		private void frmLocatingDay_Load(object sender, System.EventArgs e)
		{
			//com.digitalwave.iCare.middletier.HIS.clsHisBase  HisBase=new com.digitalwave.iCare.middletier.HIS.clsHisBase();
			checkDate= (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToShortDateString();
			label1.Text=this.LoginInfo.m_strEmpName+"�ϼƽ��:";
			m_GetAndFillData();
		
		}

		#region ��ȡ����
		private void m_GetAndFillData()
		{
			Domain.m_lngGetOneDayData(this.LoginInfo.m_strEmpID,checkDate,out dtCheckOut);
			#region ����һ��ͳ�Ʊ�
			dtStatistics=new DataTable();
			dtStatistics.Columns.Add("ʵ�ս��ϼ�");
			dtStatistics.Columns.Add("ʵ���ֽ�ϼ�");
			dtStatistics.Columns.Add("ˢ�����ϼ�");
			dtStatistics.Columns.Add("֧Ʊ���ϼ�");
			dtStatistics.Columns.Add("ҽ�����˽��");
			dtStatistics.Columns.Add("���Ѽ��˽��");
			dtStatistics.Columns.Add("�Է��Ͻɽ��");
			dtStatistics.Columns.Add("��Ʊ���");
			dtStatistics.Columns.Add("��Ʊ���ϼ�");
			dtStatistics.Columns.Add("�ָ����ϼ�");
			dtStatistics.Columns.Add("�������ϼ�");
			#endregion

			#region ͳ������
			DataRow StatisticsRow=dtStatistics.NewRow();
			StatisticsRow["ʵ�ս��ϼ�"]=0.00;
			StatisticsRow["ʵ���ֽ�ϼ�"]=0.00;
			StatisticsRow["ˢ�����ϼ�"]=0.00;
			StatisticsRow["֧Ʊ���ϼ�"]=0.00;
			StatisticsRow["ҽ�����˽��"]=0.00;
			StatisticsRow["���Ѽ��˽��"]=0.00;
			StatisticsRow["�Է��Ͻɽ��"]=0.00;

			StatisticsRow["��Ʊ���"]=0.00;
			StatisticsRow["��Ʊ���ϼ�"]=0.00;
			StatisticsRow["�ָ����ϼ�"]=0.00;
			StatisticsRow["�������ϼ�"]=0.00;
			if(dtCheckOut.Rows.Count>0)
			{
				for(int i1=0;i1<dtCheckOut.Rows.Count;i1++)
				{
					if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="1")//ͳ�ƿ�Ʊ��,��Ʊ���
					{
						StatisticsRow["��Ʊ���"]=Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString())+Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
					}

					if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="2")//��Ʊ��,��Ʊ���ϼ�,���е���Ʊ��
					{
						StatisticsRow["��Ʊ���ϼ�"]=Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString())-Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
					}

					if(dtCheckOut.Rows[i1]["STATUS_INT"].ToString().Trim()=="3")//�ָ�Ʊ��,�ָ����ϼ�
					{
						StatisticsRow["�ָ����ϼ�"]=Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString())+Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
					}
					if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()=="0")//ͳ���ֽ�ϼ�
					{
						StatisticsRow["ʵ���ֽ�ϼ�"]=Convert.ToDouble(StatisticsRow["ʵ���ֽ�ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
					}
					if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()=="1")//ˢ���ϼ�
					{
						StatisticsRow["ˢ�����ϼ�"]=Convert.ToDouble(StatisticsRow["ˢ�����ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
					}

					if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()=="2")//֧Ʊ
					{
						StatisticsRow["֧Ʊ���ϼ�"]=Convert.ToDouble(StatisticsRow["֧Ʊ���ϼ�"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["SBSUM_MNY"].ToString());
					}

					if(dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()!="1"&&dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()!="2"&&dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()!="3"&&dtCheckOut.Rows[i1]["PAYTYPE_INT"].ToString().Trim()!="0")//ͳ���������
					{
						StatisticsRow["�������ϼ�"]=Convert.ToDouble(StatisticsRow["�������ϼ�"].ToString())+Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
					}

				
					if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()=="2")//ҽ�����˽��˴�
					{
						StatisticsRow["ҽ�����˽��"]=Convert.ToDouble(StatisticsRow["ҽ�����˽��"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
					}
					if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()=="1")//���Ѽ��˽��˴�
					{
						StatisticsRow["���Ѽ��˽��"]=Convert.ToDouble(StatisticsRow["���Ѽ��˽��"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["ACCTSUM_MNY"].ToString());
					}
					if(dtCheckOut.Rows[i1]["INTERNALFLAG_INT"].ToString().Trim()=="0")//�Է��Ͻɽ��˴�
					{
						StatisticsRow["�Է��Ͻɽ��"]=Convert.ToDouble(StatisticsRow["�Է��Ͻɽ��"].ToString().Trim())+Convert.ToDouble(dtCheckOut.Rows[i1]["TOTALSUM_MNY"].ToString());
					}
				}
			}
			Double AvailabilityMoney=Convert.ToDouble(StatisticsRow["��Ʊ���"].ToString().Trim())-Convert.ToDouble(StatisticsRow["��Ʊ���ϼ�"].ToString().Trim())+Convert.ToDouble(StatisticsRow["�ָ����ϼ�"].ToString().Trim());
			StatisticsRow["ʵ�ս��ϼ�"]=AvailabilityMoney.ToString();
			#endregion
			listView1.Items.Clear();
			string strMoney=clsMain.CurrencyToString(Math.Abs(float.Parse(StatisticsRow["ʵ�ս��ϼ�"].ToString())));
			label2.Text=strMoney+"(��"+StatisticsRow["ʵ�ս��ϼ�"].ToString()+")";
			ListViewItem addItem=new ListViewItem("��"+StatisticsRow["ʵ���ֽ�ϼ�"].ToString());
			addItem.SubItems.Add("��"+StatisticsRow["ˢ�����ϼ�"].ToString());
			addItem.SubItems.Add("��"+StatisticsRow["֧Ʊ���ϼ�"].ToString());
			addItem.SubItems.Add("��"+StatisticsRow["ҽ�����˽��"].ToString());
			addItem.SubItems.Add("��"+StatisticsRow["���Ѽ��˽��"].ToString());
			addItem.SubItems.Add("��"+StatisticsRow["�������ϼ�"].ToString());
			listView1.Items.Add(addItem);
			ArrayList arrList=new ArrayList();
			clsMain.m_Detach(dtCheckOut,"INVOICENO_VCHR",out arrList);
			ListViewItem newItem=null;
			string temsun="";
			listView2.Items.Clear();
			if(arrList.Count>0)
			{
				temsun=arrList[0].ToString();
				for(int i1=0;i1<arrList.Count;i1++)
				{
					if(arrList[i1].ToString()==",")
					{
						newItem=new ListViewItem(temsun);
						newItem.SubItems.Add(arrList[i1-1].ToString());
						listView2.Items.Add(newItem);
						temsun=arrList[i1+1].ToString();
					}

				}
				newItem=new ListViewItem(temsun);
				newItem.SubItems.Add(arrList[arrList.Count-1].ToString());
				listView2.Items.Add(newItem);
			}

		}


		#endregion

		private void btnEsc_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		    frmLocatingDay_Load(null,null);
		}

		private void frmLocatingDay_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F5)
				frmLocatingDay_Load(null,null);
			if(e.KeyCode==Keys.Escape)
				this.Close();

		}
	}
}
