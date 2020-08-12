using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
 
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmChangPriceReport 的摘要说明。
	/// </summary>
	public class frmChangPriceReport  : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
	{
		private System.Windows.Forms.Panel panel2;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Panel panel3;
        private Button button1;
        private Label label3;
        private ComboBox comboBox1;
        private CheckBox checkBox1;
        internal exComboBox m_cboSelPeriodEnd;
        internal exComboBox m_cboSelPeriodBegion;
        private Label label1;
        private Label label5;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmChangPriceReport()
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
            this.panel2 = new System.Windows.Forms.Panel();
            //this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.m_cboSelPeriodEnd = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboSelPeriodBegion = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Location = new System.Drawing.Point(264, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 544);
            this.panel2.TabIndex = 2;
            // 
            // crystalReportViewer1
            // 
            //this.crystalReportViewer1.ActiveViewIndex = -1;
            //this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //this.crystalReportViewer1.DisplayGroupTree = false;
            //this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            //this.crystalReportViewer1.Name = "crystalReportViewer1";
            //this.crystalReportViewer1.SelectionFormula = "";
            //this.crystalReportViewer1.ShowGroupTreeButton = false;
            //this.crystalReportViewer1.ShowRefreshButton = false;
            //this.crystalReportViewer1.ShowTextSearchButton = false;
            //this.crystalReportViewer1.ShowZoomButton = false;
            //this.crystalReportViewer1.Size = new System.Drawing.Size(614, 542);
            //this.crystalReportViewer1.TabIndex = 0;
            //this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            //this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_cboSelPeriodEnd);
            this.panel1.Controls.Add(this.m_cboSelPeriodBegion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 536);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "药品类型：";
            this.label3.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "全部",
            "西药",
            "中草药",
            "中成药"});
            this.comboBox1.Location = new System.Drawing.Point(103, 175);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(116, 22);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Linen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(103, 216);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FloralWhite;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.radioButton2);
            this.panel3.Controls.Add(this.radioButton3);
            this.panel3.Controls.Add(this.radioButton1);
            this.panel3.Location = new System.Drawing.Point(4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 87);
            this.panel3.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(123, 36);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(117, 18);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "显示‘0’盈亏";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 18);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "按调价明细";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 62);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 18);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.Text = "按药品类型";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 18);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "按调价单";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(0, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 274);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(242, 252);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "调价单号";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "调价时间";
            this.columnHeader2.Width = 150;
            // 
            // m_cboSelPeriodEnd
            // 
            this.m_cboSelPeriodEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodEnd.Location = new System.Drawing.Point(36, 131);
            this.m_cboSelPeriodEnd.Name = "m_cboSelPeriodEnd";
            this.m_cboSelPeriodEnd.Size = new System.Drawing.Size(218, 22);
            this.m_cboSelPeriodEnd.TabIndex = 71;
            // 
            // m_cboSelPeriodBegion
            // 
            this.m_cboSelPeriodBegion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodBegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodBegion.Location = new System.Drawing.Point(36, 105);
            this.m_cboSelPeriodBegion.Name = "m_cboSelPeriodBegion";
            this.m_cboSelPeriodBegion.Size = new System.Drawing.Size(218, 22);
            this.m_cboSelPeriodBegion.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 72;
            this.label1.Text = "结束";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 69;
            this.label5.Text = "开始";
            // 
            // frmChangPriceReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(880, 541);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmChangPriceReport";
            this.Text = "调价查询报表";
            this.Load += new System.EventHandler(this.frmChangPriceReport_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		DomainControlMedReport domain=new DomainControlMedReport();
         clsPeriod_VO[] objPriodItems = null;
		private void frmChangPriceReport_Load(object sender, System.EventArgs e)
		{
            objPriodItems = clsPublicParm.s_GetPeriodList();
            string nowdate = clsPublicParm.s_datGetServerDate().Date.ToString();
            int intSelPeriod = -1;
            if (objPriodItems.Length > 0)
            {
                for (int i1 = 0; i1 < objPriodItems.Length; i1++)
                {
                    this.m_cboSelPeriodBegion.Item.Add(objPriodItems[i1].m_strStartDate + " 至 " + objPriodItems[i1].m_strEndDate, objPriodItems[i1].m_strPeriodID);
                    this.m_cboSelPeriodEnd.Item.Add(objPriodItems[i1].m_strStartDate + " 至 " + objPriodItems[i1].m_strEndDate, objPriodItems[i1].m_strPeriodID);
                    if (Convert.ToDateTime(nowdate) >= Convert.ToDateTime(objPriodItems[i1].m_strStartDate) && Convert.ToDateTime(nowdate) <= Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
                    {
                        intSelPeriod = i1;
                    }
                }
            }
            if (intSelPeriod != -1)
            {
                this.m_cboSelPeriodEnd.SelectedIndex = intSelPeriod;
                this.m_cboSelPeriodBegion.SelectedIndex = intSelPeriod;
            }
            comboBox1.SelectedIndex = 0;
		}
		string strFLAG="";
		public void m_mthShowMe(string strFLAG)
		{
			this.strFLAG=strFLAG;
			this.Show();
		}
		#region 填充ListView
		private void m_mthFillList(System.Data.DataTable dt)
		{
			listView1.Items.Clear();
			if(dt.Rows.Count>0)
			{
				for(int i1=0;i1<dt.Rows.Count;i1++)
				{
					ListViewItem newItem=null;
					newItem=new ListViewItem(dt.Rows[i1]["MEDICINEPRICECHGAPPLNO_CHR"].ToString());
					newItem.SubItems.Add(dt.Rows[i1]["ADUITDATE_DAT"].ToString());
					newItem.Tag=dt.Rows[i1];
					listView1.Items.Add(newItem);
				}
			}

		}

		#endregion
		#region 打印数据

		private void m_mthPrintData()
		{
			//if(listView1.SelectedItems.Count>0)
			//{
			//	System.Data.DataTable dtDe=new System.Data.DataTable();
			//	System.Data.DataRow seleRow=(System.Data.DataRow)listView1.SelectedItems[0].Tag;
			//	domain.m_lngGetChangPriceDeOfMonth(seleRow["MEDICINEPRICECHGAPPLID_CHR"].ToString(),strFLAG,out dtDe,this.checkBox1.Checked);
			//	com.digitalwave.iCare.gui.HIS.baotable.changPriceOfMonth Report=new com.digitalwave.iCare.gui.HIS.baotable.changPriceOfMonth();
			//	Report.SetDataSource(dtDe);
			//	((TextObject)Report.ReportDefinition.ReportObjects["Text4"]).Text = seleRow["MEDICINEPRICECHGAPPLNO_CHR"].ToString();
			//	((TextObject)Report.ReportDefinition.ReportObjects["Text6"]).Text =  seleRow["ADUITDATE_DAT"].ToString();
			//	if(strFLAG=="1")
			//	{
			//		((TextObject)Report.ReportDefinition.ReportObjects["Text1"]).Text = "药房调价盈亏统计报表";
			//	}
			//	else
			//	{
			//		((TextObject)Report.ReportDefinition.ReportObjects["Text1"]).Text = "药库调价盈亏统计报表";
			//	}
			//	crystalReportViewer1.ReportSource=Report;
			//}
		}
		#endregion

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_mthPrintData();
		}

		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
		
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.m_cboSelPeriodBegion.SelectItemValue) > int.Parse(this.m_cboSelPeriodEnd.SelectItemValue))
            {
                MessageBox.Show("开始财务期不能大于结束财务期！");
                return;
            }
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            for (int i1 = this.m_cboSelPeriodBegion.SelectedIndex; i1 <= this.m_cboSelPeriodEnd.SelectedIndex; i1++)
            {
                list.Add(objPriodItems[i1].m_strPeriodID);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            if (radioButton1.Checked)
            {
                domain.m_lngGetChangPriceDataOfMonth( list, this.strFLAG, out dt);
                m_mthFillList(dt);
            }
            if (radioButton2.Checked)
            {
                //System.Data.DataTable dtDe = new System.Data.DataTable();
                //domain.m_lngGetChangPriceDe(out dtDe, list, comboBox1.SelectedIndex, this.checkBox1.Checked);
                //com.digitalwave.iCare.gui.HIS.baotable.changPriceOfMonth Report = new com.digitalwave.iCare.gui.HIS.baotable.changPriceOfMonth();
                //Report.SetDataSource(dtDe);
                //((TextObject)Report.ReportDefinition.ReportObjects["Text4"]).Text = "统计日期：" + m_cboSelPeriodBegion.Text + " 到" + m_cboSelPeriodEnd.Text;
                //if (strFLAG == "1")
                //{
                //    ((TextObject)Report.ReportDefinition.ReportObjects["Text1"]).Text = "药房调价盈亏统计报表";
                //}
                //else
                //{
                //    ((TextObject)Report.ReportDefinition.ReportObjects["Text1"]).Text = "药库调价盈亏统计报表";
                //}
                //crystalReportViewer1.ReportSource = Report;
            }
            if (radioButton3.Checked)
            {
                System.Data.DataTable dtSour = new System.Data.DataTable();
                dtSour.Columns.Add("西药");
                dtSour.Columns.Add("中草药");
                dtSour.Columns.Add("中成药");
                dtSour.Columns.Add("合计");
                clsDomainConrolChangPrice objSVC = new clsDomainConrolChangPrice();
                objSVC.m_lngGetChangePriceRpt(list, out dt);
                if (dt.Rows.Count > 0)
                {
                    System.Data.DataRow newRow = dtSour.NewRow();
                    newRow["合计"] = 0;
                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        if (dt.Rows[i1]["MEDICINETYPEID_CHR"].ToString().Trim() == "1")
                        {
                            if (dt.Rows[i1]["balance"].ToString() != "")
                            {
                                newRow["西药"] = dt.Rows[i1]["balance"].ToString();
                                newRow["合计"] = double.Parse(double.Parse(newRow["合计"].ToString()).ToString("0.00")) + double.Parse(double.Parse(newRow["西药"].ToString()).ToString("0.00"));
                            }
                        }
                        if (dt.Rows[i1]["MEDICINETYPEID_CHR"].ToString().Trim() == "2")
                        {
                            if (dt.Rows[i1]["balance"].ToString() != "")
                            {
                                newRow["中草药"] =dt.Rows[i1]["balance"].ToString();
                                newRow["合计"] = double.Parse(double.Parse(newRow["合计"].ToString()).ToString("0.00")) + double.Parse(double.Parse(newRow["中草药"].ToString()).ToString("0.00"));
                            }
                        }
                        if (dt.Rows[i1]["MEDICINETYPEID_CHR"].ToString().Trim() == "3")
                        {
                            if (dt.Rows[i1]["balance"].ToString() != "")
                            {
                                newRow["中成药"] = dt.Rows[i1]["balance"].ToString();
                                newRow["合计"] = double.Parse(double.Parse(newRow["合计"].ToString()).ToString("0.00")) + double.Parse(double.Parse(newRow["中成药"].ToString()).ToString("0.00"));
                            }
                        }
                    }
                    
                    dtSour.Rows.Add(newRow);
                    for (int i2 = 0; i2 < dtSour.Columns.Count; i2++)
                    {
                        if (dtSour.Rows[0][i2].ToString() != "")
                            dtSour.Rows[0][i2] = (double.Parse(dtSour.Rows[0][i2].ToString())).ToString("0.00");
                    }
                }
                //com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRptRpt rpt = new com.digitalwave.iCare.gui.HIS.baotable.ChangePriceRptRpt();
                //((TextObject)rpt.ReportDefinition.ReportObjects["Text3"]).Text = m_cboSelPeriodBegion.Text + "到" + m_cboSelPeriodEnd.Text;
                //rpt.SetDataSource(dtSour);
                //rpt.Refresh();
                //this.crystalReportViewer1.ReportSource = rpt;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Visible = true;
                button1.Location=new Point(87,213);
                label3.Visible = false;
                comboBox1.Visible = false;
            }
            if (radioButton2.Checked)
            {
                groupBox1.Visible = false;
                label3.Visible = true;
                comboBox1.Visible = true;
                button1.Location =new Point(87,243);
            }
            if (radioButton3.Checked)
            {
                label3.Visible = false;
                groupBox1.Visible = false;
                comboBox1.Visible = false;
                button1.Location = new Point(87, 213);
            }
        }
	}
}
