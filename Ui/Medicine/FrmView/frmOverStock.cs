using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmOverStock 的摘要说明。
	/// </summary>
	public class frmOverStock : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		internal com.digitalwave.iCare.gui.HIS.exComboBox m_cobStorage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		internal PinkieControls.ButtonXP bnlfind;
		internal PinkieControls.ButtonXP buttonXP1;
		internal PinkieControls.ButtonXP buttonXP2;
		private System.Windows.Forms.Panel panel2;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmOverStock()
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonXP2 = new PinkieControls.ButtonXP();
            this.buttonXP1 = new PinkieControls.ButtonXP();
            this.bnlfind = new PinkieControls.ButtonXP();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cobStorage = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            //this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.buttonXP2);
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.bnlfind);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.m_cobStorage);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 40);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(416, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 23);
            this.label3.TabIndex = 147;
            this.label3.Text = "月";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this.comboBox1.Location = new System.Drawing.Point(328, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(88, 22);
            this.comboBox1.TabIndex = 146;
            // 
            // buttonXP2
            // 
            this.buttonXP2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP2.DefaultScheme = true;
            this.buttonXP2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXP2.Hint = "";
            this.buttonXP2.Location = new System.Drawing.Point(776, 3);
            this.buttonXP2.Name = "buttonXP2";
            this.buttonXP2.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP2.Size = new System.Drawing.Size(88, 33);
            this.buttonXP2.TabIndex = 145;
            this.buttonXP2.Text = "退出(&ESC)";
            this.buttonXP2.Click += new System.EventHandler(this.buttonXP2_Click);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = true;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Hint = "";
            this.buttonXP1.Location = new System.Drawing.Point(644, 3);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(88, 33);
            this.buttonXP1.TabIndex = 144;
            this.buttonXP1.Text = "打印(&P)";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // bnlfind
            // 
            this.bnlfind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnlfind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.bnlfind.DefaultScheme = true;
            this.bnlfind.DialogResult = System.Windows.Forms.DialogResult.None;
            this.bnlfind.Hint = "";
            this.bnlfind.Location = new System.Drawing.Point(512, 3);
            this.bnlfind.Name = "bnlfind";
            this.bnlfind.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.bnlfind.Size = new System.Drawing.Size(88, 33);
            this.bnlfind.TabIndex = 143;
            this.bnlfind.Text = "统计(&S)";
            this.bnlfind.Click += new System.EventHandler(this.bnlfind_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(224, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "积压期限:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "药库:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_cobStorage
            // 
            this.m_cobStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cobStorage.Location = new System.Drawing.Point(72, 8);
            this.m_cobStorage.Name = "m_cobStorage";
            this.m_cobStorage.Size = new System.Drawing.Size(104, 22);
            this.m_cobStorage.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(904, 344);
            this.panel2.TabIndex = 1;
            // 
            // crystalReportViewer1
            // 
            //this.crystalReportViewer1.ActiveViewIndex = -1;
            //this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crystalReportViewer1.DisplayBackgroundEdge = false;
            //this.crystalReportViewer1.DisplayGroupTree = false;
            //this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.crystalReportViewer1.SelectionFormula = "";
            //this.crystalReportViewer1.Size = new System.Drawing.Size(902, 342);
            //this.crystalReportViewer1.TabIndex = 0;
            //this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // frmOverStock
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.CancelButton = this.buttonXP2;
            this.ClientSize = new System.Drawing.Size(904, 405);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmOverStock";
            this.Load += new System.EventHandler(this.frmOverStock_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void frmOverStock_Load(object sender, System.EventArgs e)
		{
			clsStorage_VO[] p_objStorageArr=new clsStorage_VO[0];
			clsPublicParm.s_lngGetStorageList(out p_objStorageArr);
			if(p_objStorageArr.Length>0)
			{
				for(int i1=0;i1<p_objStorageArr.Length;i1++)
				{
					m_cobStorage.Item.Add(p_objStorageArr[i1].m_strStroageName,p_objStorageArr[i1].m_strStroageID);
				}
			}
			m_cobStorage.SelectedIndex=0;
			comboBox1.SelectedIndex=2;

			switch(intStau)
			{
				case 1:
					this.Text="药品积压统计";
					break;
				case 2:
					this.Text="药品呆滞报表";
					break;
				case 3:
					this.Text="临缺药品报表";
					break;

			}
		}
		int intStau=0;
		public void m_mthShowMe(string str1)
		{
			if(str1!="1")
			{
				label2.Visible=false;
				comboBox1.Visible=false;
				label3.Visible=false;
			
			}
			try
			{
				intStau=int.Parse(str1);
				this.Show();
			}
			catch
			{

			}
		}
		private void bnlfind_Click(object sender, System.EventArgs e)
		{
			
			DomainControlMedReport domain=new DomainControlMedReport();
			System.Data.DataTable dt=new System.Data.DataTable();
			//switch(intStau)
			//{
			//	case 1:
			//		int SpecMonth=comboBox1.SelectedIndex+1;
			//		domain.m_lngGetOverStock(m_cobStorage.SelectItemValue,SpecMonth,1,out dt);
			//		com.digitalwave.iCare.gui.HIS.baotable.OverStockReport Report=new com.digitalwave.iCare.gui.HIS.baotable.OverStockReport();
			//		((TextObject)Report.ReportDefinition.ReportObjects["Text1"]).Text =m_cobStorage.SelectItemText+"药品积压报表";
			//		((TextObject)Report.ReportDefinition.ReportObjects["Text3"]).Text =m_cobStorage.SelectItemText;
			//		((TextObject)Report.ReportDefinition.ReportObjects["Text5"]).Text =SpecMonth.ToString()+"个月";
			//		Report.SetDataSource(dt);
			//		crystalReportViewer1.ReportSource=Report;
			//		break;
			//	case 2:
			//		domain.m_lngGetOverStock(m_cobStorage.SelectItemValue,0,2,out dt);
			//		com.digitalwave.iCare.gui.HIS.baotable.OverStockReport1 Report1=new com.digitalwave.iCare.gui.HIS.baotable.OverStockReport1();
			//		((TextObject)Report1.ReportDefinition.ReportObjects["Text1"]).Text =m_cobStorage.SelectItemText+"药品呆滞报表";
			//		((TextObject)Report1.ReportDefinition.ReportObjects["Text3"]).Text =m_cobStorage.SelectItemText;
			//		Report1.SetDataSource(dt);
			//		crystalReportViewer1.ReportSource=Report1;
			//		break;
			//	case 3:
			//		domain.m_lngGetOverStock(m_cobStorage.SelectItemValue,0,3,out dt);
			//		com.digitalwave.iCare.gui.HIS.baotable.OverStockReport1 Report2=new com.digitalwave.iCare.gui.HIS.baotable.OverStockReport1();
			//		((TextObject)Report2.ReportDefinition.ReportObjects["Text1"]).Text =m_cobStorage.SelectItemText+"临缺药品报表";
			//		((TextObject)Report2.ReportDefinition.ReportObjects["Text3"]).Text =m_cobStorage.SelectItemText;
			//		Report2.SetDataSource(dt);
			//		crystalReportViewer1.ReportSource=Report2;
			//		break;
			//}
			
		}

		private void buttonXP2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonXP1_Click(object sender, System.EventArgs e)
		{
			//crystalReportViewer1.PrintReport();
		}
	}
}
