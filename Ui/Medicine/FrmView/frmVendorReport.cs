using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data; 
using com.digitalwave.iCare.common;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmVendorReport 的摘要说明。
	/// </summary>
	public class frmVendorReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
	{
        private System.Windows.Forms.Panel panel1;
        private PinkieControls.ButtonXP m_BtnSearch;
        private com.digitalwave.controls.Control.ctlprintShow ctlprintShow1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private Panel panel2;
        private PinkieControls.ButtonXP m_btnEsc;
        private PinkieControls.ButtonXP m_btnPrint;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private PinkieControls.ButtonXP btnEx;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        internal exComboBox m_cboSelPeriodEnd;
        internal exComboBox m_cboSelPeriodBegion;
        private Label label3;
        private Label label5;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmVendorReport()
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
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.btnEx = new PinkieControls.ButtonXP();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.m_btnPrint = new PinkieControls.ButtonXP();
            this.m_btnEsc = new PinkieControls.ButtonXP();
            this.m_BtnSearch = new PinkieControls.ButtonXP();
            this.ctlprintShow1 = new com.digitalwave.controls.Control.ctlprintShow();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_cboSelPeriodEnd = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.m_cboSelPeriodBegion = new com.digitalwave.iCare.gui.HIS.exComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.m_cboSelPeriodEnd);
            this.panel1.Controls.Add(this.m_cboSelPeriodBegion);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.checkBox7);
            this.panel1.Controls.Add(this.checkBox6);
            this.panel1.Controls.Add(this.btnEx);
            this.panel1.Controls.Add(this.checkBox5);
            this.panel1.Controls.Add(this.checkBox4);
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.m_btnPrint);
            this.panel1.Controls.Add(this.m_btnEsc);
            this.panel1.Controls.Add(this.m_BtnSearch);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 66);
            this.panel1.TabIndex = 0;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(593, 22);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(124, 18);
            this.checkBox7.TabIndex = 66;
            this.checkBox7.Text = "非中标金额统计";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(477, 36);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(110, 18);
            this.checkBox6.TabIndex = 65;
            this.checkBox6.Text = "中标金额统计";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // btnEx
            // 
            this.btnEx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btnEx.DefaultScheme = true;
            this.btnEx.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEx.Hint = "";
            this.btnEx.Location = new System.Drawing.Point(868, 11);
            this.btnEx.Name = "btnEx";
            this.btnEx.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.btnEx.Size = new System.Drawing.Size(61, 42);
            this.btnEx.TabIndex = 64;
            this.btnEx.Text = "导出(&O)";
            this.btnEx.Click += new System.EventHandler(this.btnEx_Click);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(477, 9);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(110, 18);
            this.checkBox5.TabIndex = 63;
            this.checkBox5.Text = "国家限价统计";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(375, 35);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(96, 18);
            this.checkBox4.TabIndex = 62;
            this.checkBox4.Text = "进口药统计";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(375, 8);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(96, 18);
            this.checkBox3.TabIndex = 61;
            this.checkBox3.Text = "中草药统计";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(268, 37);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 18);
            this.checkBox2.TabIndex = 60;
            this.checkBox2.Text = "中成药统计";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(268, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 18);
            this.checkBox1.TabIndex = 59;
            this.checkBox1.Text = "西药统计";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnPrint.DefaultScheme = true;
            this.m_btnPrint.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnPrint.Hint = "";
            this.m_btnPrint.Location = new System.Drawing.Point(801, 11);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnPrint.Size = new System.Drawing.Size(61, 42);
            this.m_btnPrint.TabIndex = 58;
            this.m_btnPrint.Text = "打印(&P)";
            this.m_btnPrint.Click += new System.EventHandler(this.m_btnPrint_Click);
            // 
            // m_btnEsc
            // 
            this.m_btnEsc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_btnEsc.DefaultScheme = true;
            this.m_btnEsc.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_btnEsc.Hint = "";
            this.m_btnEsc.Location = new System.Drawing.Point(935, 11);
            this.m_btnEsc.Name = "m_btnEsc";
            this.m_btnEsc.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_btnEsc.Size = new System.Drawing.Size(61, 42);
            this.m_btnEsc.TabIndex = 57;
            this.m_btnEsc.Text = "退出(&E)";
            this.m_btnEsc.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // m_BtnSearch
            // 
            this.m_BtnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.m_BtnSearch.DefaultScheme = true;
            this.m_BtnSearch.DialogResult = System.Windows.Forms.DialogResult.None;
            this.m_BtnSearch.Hint = "";
            this.m_BtnSearch.Location = new System.Drawing.Point(734, 11);
            this.m_BtnSearch.Name = "m_BtnSearch";
            this.m_BtnSearch.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
            this.m_BtnSearch.Size = new System.Drawing.Size(61, 42);
            this.m_BtnSearch.TabIndex = 56;
            this.m_BtnSearch.Text = "统计(&F)";
            this.m_BtnSearch.Click += new System.EventHandler(this.m_BtnSearch_Click);
            // 
            // ctlprintShow1
            // 
            this.ctlprintShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlprintShow1.Location = new System.Drawing.Point(0, 0);
            this.ctlprintShow1.Name = "ctlprintShow1";
            this.ctlprintShow1.Size = new System.Drawing.Size(1005, 434);
            this.ctlprintShow1.TabIndex = 3;
            this.ctlprintShow1.Zoom = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ctlprintShow1);
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1007, 436);
            this.panel2.TabIndex = 1;
            // 
            // m_cboSelPeriodEnd
            // 
            this.m_cboSelPeriodEnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodEnd.Location = new System.Drawing.Point(78, 35);
            this.m_cboSelPeriodEnd.Name = "m_cboSelPeriodEnd";
            this.m_cboSelPeriodEnd.Size = new System.Drawing.Size(175, 22);
            this.m_cboSelPeriodEnd.TabIndex = 71;
            // 
            // m_cboSelPeriodBegion
            // 
            this.m_cboSelPeriodBegion.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.m_cboSelPeriodBegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cboSelPeriodBegion.Location = new System.Drawing.Point(78, 9);
            this.m_cboSelPeriodBegion.Name = "m_cboSelPeriodBegion";
            this.m_cboSelPeriodBegion.Size = new System.Drawing.Size(175, 22);
            this.m_cboSelPeriodBegion.TabIndex = 70;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 72;
            this.label3.Text = "结束财务期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 69;
            this.label5.Text = "开始财务期";
            // 
            // frmVendorReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(1007, 517);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmVendorReport";
            this.Text = "药库月购进统计表";
            this.Load += new System.EventHandler(this.frmVendorReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
        clsPeriod_VO[] objPriodItems = null;
		private void frmVendorReport_Load(object sender, System.EventArgs e)
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
            ctlprintShow1.setDocument = printDocument1;
		}
		string strIsBreak;
		public void m_mthShowMe(string IsBreak)
		{
			strIsBreak=IsBreak;
            switch (IsBreak)
            {
                case "1":
                    this.Text = "药库入库月购进报表　";
                    break;
                case "2":
                    this.Text = "药库月出库报表　";
                    break;
                case "3":
                    this.Text = "药库月退库报表　";
                    break;
                case "4":
                    this.Text = "药库月退货报表　";
                    break;
                case "5":
                    this.Text = "供应商月统计报表";
                    break;

            }
			this.Show();
		}
        DataTable dtdein = new DataTable();
		private void m_BtnSearch_Click(object sender, System.EventArgs e)
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
            clsDomainConrol_Medicne domain=new clsDomainConrol_Medicne();
            DataTable  dtENAim=new DataTable();
            DataTable  dtENNoAim=new DataTable();
            DataTable  dtCHAim=new DataTable();
            DataTable  dtCHNoAim=new DataTable();
            DataTable dtEHAim=new DataTable();
            DataTable  dtEHNoAim=new DataTable();
            DataTable  dtImportAim=new DataTable();
            DataTable dtImportNoAim = new DataTable();
            domain.m_lngGetReportDataOfMonth(out dtdein, out  dtENAim, out  dtENNoAim, out  dtCHAim, out  dtCHNoAim, out  dtEHAim, out  dtEHNoAim, out  dtImportAim, out  dtImportNoAim, list, int.Parse(strIsBreak));

            #region 根据用户选择构造表
            dtdein.Columns[0].Caption = "药品供应商名称";
            dtdein.Columns.Add("购进金额", typeof(Double));
            dtdein.Columns[dtdein.Columns.Count - 1].Caption = "购进金额";
            if (this.checkBox6.Checked)
            {
                dtdein.Columns.Add("进价中标", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "进价中标";
            }
            if (this.checkBox7.Checked)
            {
                dtdein.Columns.Add("进价非中标", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "进价非中标";
            }
            dtdein.Columns.Add("零售金额", typeof(Double));
            dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零售金额";
            if (this.checkBox6.Checked)
            {
                dtdein.Columns.Add("零价中标", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零价中标";
            }
            if (this.checkBox7.Checked)
            {
                dtdein.Columns.Add("零价非中标", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零价非中标";
            }
            if (checkBox5.Checked)
            {
                dtdein.Columns.Add("限价金额",typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "限价金额";
            }
            if (checkBox1.Checked)
            {
                dtdein.Columns.Add("totalbuymoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "西药";

                #region 西药中标
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("Aimbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 西药非中标
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("buymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                dtdein.Columns.Add("totalsalmoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零售金额";

                #region 西药零价中标
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("Aimsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 西药零价非中标
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("salmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                if (checkBox5.Checked)
                {
                    dtdein.Columns.Add("EnLIMITmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "限价金额";
                }
                #region 西药中标统计
                if (dtENAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtENAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            dtdein.Rows[i1]["totalbuymoney"] = double.Parse(ArrRow[0]["Aimbuymoney"].ToString());
                            dtdein.Rows[i1]["totalsalmoney"] = double.Parse(ArrRow[0]["Aimsalmoney"].ToString());
                            if (this.checkBox6.Checked)
                            {
                                dtdein.Rows[i1]["Aimbuymoney"] = double.Parse(ArrRow[0]["Aimbuymoney"].ToString());
                                dtdein.Rows[i1]["Aimsalmoney"] = double.Parse(ArrRow[0]["Aimsalmoney"].ToString());
                            }
                            if (checkBox5.Checked)
                            {
                                dtdein.Rows[i1]["EnLIMITmoney"] = double.Parse(ArrRow[0]["EnLIMITmoney"].ToString());
                            }
                        }
                    }
                }
                #endregion

                #region 西药非中标统计
                if (dtENNoAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtENNoAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (dtdein.Rows[i1]["totalbuymoney"] != System.DBNull.Value)
                            {
                                if (ArrRow[0]["buymoney"] != null && ArrRow[0]["buymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["totalbuymoney"] = double.Parse(dtdein.Rows[i1]["totalbuymoney"].ToString()) + double.Parse(ArrRow[0]["buymoney"].ToString());
                                    dtdein.Rows[i1]["totalsalmoney"] = double.Parse(dtdein.Rows[i1]["totalsalmoney"].ToString()) + double.Parse(ArrRow[0]["salmoney"].ToString());
                                }
                            }
                            else
                            {
                                if (ArrRow[0]["buymoney"] != null && ArrRow[0]["buymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["totalbuymoney"] = double.Parse(ArrRow[0]["buymoney"].ToString());
                                    dtdein.Rows[i1]["totalsalmoney"] = double.Parse(ArrRow[0]["salmoney"].ToString());
                                }
                            }

                            if (this.checkBox7.Checked)
                            {
                                dtdein.Rows[i1]["buymoney"] = double.Parse(ArrRow[0]["buymoney"].ToString());
                                dtdein.Rows[i1]["salmoney"] = double.Parse(ArrRow[0]["salmoney"].ToString());
                            }
                            if (checkBox5.Checked)
                            {
                                if (dtdein.Rows[i1]["EnLIMITmoney"] != System.DBNull.Value)
                                {
                                    dtdein.Rows[i1]["EnLIMITmoney"] = double.Parse(dtdein.Rows[i1]["EnLIMITmoney"].ToString()) + double.Parse(ArrRow[0]["EnLIMITmoney"].ToString());
                                }
                                else
                                {
                                    dtdein.Rows[i1]["EnLIMITmoney"] = double.Parse(ArrRow[0]["EnLIMITmoney"].ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (checkBox2.Checked)
            {
                dtdein.Columns.Add("totalEhbuymoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中成药";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimEhbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Ehbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                dtdein.Columns.Add("totalEhsalmoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零售金额";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimEhsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Ehsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                if (checkBox5.Checked)
                {
                    dtdein.Columns.Add("EhLIMITmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "限价金额";
                }

                #region 中成药中标统计
                if (dtEHAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtEHAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (ArrRow[0]["AimEhbuymoney"] != null && ArrRow[0]["AimEhbuymoney"].ToString() != "")
                            {
                                dtdein.Rows[i1]["totalEhbuymoney"] = double.Parse(ArrRow[0]["AimEhbuymoney"].ToString());
                                dtdein.Rows[i1]["totalEhsalmoney"] = double.Parse(ArrRow[0]["AimEhsalmoney"].ToString());
                            }

                            if (this.checkBox6.Checked)
                            {
                                if (ArrRow[0]["AimEhbuymoney"] != null && ArrRow[0]["AimEhbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["AimEhbuymoney"] = double.Parse(ArrRow[0]["AimEhbuymoney"].ToString());
                                    dtdein.Rows[i1]["AimEhsalmoney"] = double.Parse(ArrRow[0]["AimEhsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                dtdein.Rows[i1]["EhLIMITmoney"] = double.Parse(ArrRow[0]["EhLIMITmoney"].ToString());
                            }
                        }
                    }
                }
                #endregion

                #region 中成药非中标统计
                if (dtEHNoAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtEHNoAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (dtdein.Rows[i1]["totalEhbuymoney"] != System.DBNull.Value)
                            {
                                dtdein.Rows[i1]["totalEhbuymoney"] = double.Parse(dtdein.Rows[i1]["totalEhbuymoney"].ToString()) + double.Parse(ArrRow[0]["Ehbuymoney"].ToString());
                                dtdein.Rows[i1]["totalEhsalmoney"] = double.Parse(dtdein.Rows[i1]["totalEhsalmoney"].ToString()) + double.Parse(ArrRow[0]["Ehsalmoney"].ToString());
                            }
                            else
                            {
                                if (ArrRow[0]["Ehbuymoney"] != null && ArrRow[0]["Ehbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["totalEhbuymoney"] = double.Parse(ArrRow[0]["Ehbuymoney"].ToString());
                                    dtdein.Rows[i1]["totalEhsalmoney"] = double.Parse(ArrRow[0]["Ehsalmoney"].ToString());
                                }
                            }
                            if (this.checkBox7.Checked)
                            {
                                if (ArrRow[0]["Ehbuymoney"]!= null && ArrRow[0]["Ehbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["Ehbuymoney"] = double.Parse(ArrRow[0]["Ehbuymoney"].ToString());
                                    dtdein.Rows[i1]["Ehsalmoney"] = double.Parse(ArrRow[0]["Ehsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                if (dtdein.Rows[i1]["EhLIMITmoney"] != System.DBNull.Value)
                                    dtdein.Rows[i1]["EhLIMITmoney"] = double.Parse(dtdein.Rows[i1]["EhLIMITmoney"].ToString()) + double.Parse(ArrRow[0]["EhLIMITmoney"].ToString());
                                else
                                {
                                    dtdein.Rows[i1]["EhLIMITmoney"] =double.Parse(ArrRow[0]["EhLIMITmoney"].ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (checkBox3.Checked)
            {
                dtdein.Columns.Add("totalChbuymoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中草药";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimChbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Chbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                dtdein.Columns.Add("totalChsalmoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零售金额";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimChsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Chsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                if (checkBox5.Checked)
                {
                    dtdein.Columns.Add("ChLIMITmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "限价金额";
                }

                #region 中草药中标统计
                if (dtCHAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtCHAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (ArrRow[0]["AimChbuymoney"] != null && ArrRow[0]["AimChbuymoney"].ToString() != "")
                            {
                                dtdein.Rows[i1]["totalChbuymoney"] = double.Parse(ArrRow[0]["AimChbuymoney"].ToString());
                                dtdein.Rows[i1]["totalChsalmoney"] = double.Parse(ArrRow[0]["AimChsalmoney"].ToString());
                            }

                            if (this.checkBox6.Checked)
                            {
                                if (ArrRow[0]["AimChbuymoney"] != null && ArrRow[0]["AimChbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["AimChbuymoney"] = double.Parse(ArrRow[0]["AimChbuymoney"].ToString());
                                    dtdein.Rows[i1]["AimChsalmoney"] = double.Parse(ArrRow[0]["AimChsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                dtdein.Rows[i1]["ChLIMITmoney"] = double.Parse(ArrRow[0]["ChLIMITmoney"].ToString());
                            }
                        }
                    }
                }
                #endregion

                #region 中草药非中标统计
                if (dtCHNoAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtCHNoAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (dtdein.Rows[i1]["totalChbuymoney"] != System.DBNull.Value)
                            {
                                dtdein.Rows[i1]["totalChbuymoney"] = double.Parse(dtdein.Rows[i1]["totalChbuymoney"].ToString()) + double.Parse(ArrRow[0]["Chbuymoney"].ToString());
                                dtdein.Rows[i1]["totalChsalmoney"] = double.Parse(dtdein.Rows[i1]["totalChsalmoney"].ToString()) + double.Parse(ArrRow[0]["Chsalmoney"].ToString());
                            }
                            else
                            {
                                dtdein.Rows[i1]["totalChbuymoney"] = double.Parse(ArrRow[0]["Chbuymoney"].ToString());
                                dtdein.Rows[i1]["totalChsalmoney"] = double.Parse(ArrRow[0]["Chsalmoney"].ToString());
                            }
                            if (this.checkBox7.Checked)
                            {
                                if (ArrRow[0]["Chbuymoney"] != null && ArrRow[0]["Chbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["Chbuymoney"] = double.Parse(ArrRow[0]["Chbuymoney"].ToString());
                                    dtdein.Rows[i1]["Chsalmoney"] = double.Parse(ArrRow[0]["Chsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                if (dtdein.Rows[i1]["ChLIMITmoney"] != System.DBNull.Value)
                                {
                                    dtdein.Rows[i1]["ChLIMITmoney"] = double.Parse(dtdein.Rows[i1]["ChLIMITmoney"].ToString()) + double.Parse(ArrRow[0]["ChLIMITmoney"].ToString());
                                }
                                else
                                {
                                    dtdein.Rows[i1]["ChLIMITmoney"] =double.Parse(ArrRow[0]["ChLIMITmoney"].ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (checkBox4.Checked)
            {
                dtdein.Columns.Add("totalImbuymoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "进口药";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimImbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Imbuymoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                dtdein.Columns.Add("totalImsalmoney", typeof(Double));
                dtdein.Columns[dtdein.Columns.Count - 1].Caption = "零售金额";

                #region 中标统计
                if (this.checkBox6.Checked)
                {
                    dtdein.Columns.Add("AimImsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "中标金额";
                }
                #endregion

                #region 非中标统计
                if (this.checkBox7.Checked)
                {
                    dtdein.Columns.Add("Imsalmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "非中标金额";
                }
                #endregion

                if (checkBox5.Checked)
                {
                    dtdein.Columns.Add("ImLIMITmoney", typeof(Double));
                    dtdein.Columns[dtdein.Columns.Count - 1].Caption = "限价金额";
                }

                #region 中成药中标统计
                if (dtImportAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtImportAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (ArrRow[0]["AimImbuymoney"] != null && ArrRow[0]["AimImbuymoney"].ToString() != "")
                            {
                                dtdein.Rows[i1]["totalImbuymoney"] = double.Parse(ArrRow[0]["AimImbuymoney"].ToString());
                                dtdein.Rows[i1]["totalImsalmoney"] = double.Parse(ArrRow[0]["AimImsalmoney"].ToString());
                            }

                            if (this.checkBox6.Checked)
                            {
                                if (ArrRow[0]["AimImbuymoney"] != null && ArrRow[0]["AimImbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["AimImbuymoney"] = double.Parse(ArrRow[0]["AimImbuymoney"].ToString());
                                    dtdein.Rows[i1]["AimImsalmoney"] = double.Parse(ArrRow[0]["AimImsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                try
                                {
                                    dtdein.Rows[i1]["ImLIMITmoney"] = double.Parse(ArrRow[0]["ImLIMITmoney"].ToString());
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                #endregion

                #region 中成药非中标统计
                if (dtImportNoAim.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
                    {
                        DataRow[] ArrRow = dtImportNoAim.Select("vendorname_vchr='" + dtdein.Rows[i1]["vendorname_vchr"].ToString() + "'");

                        if (ArrRow.Length > 0)
                        {
                            if (dtdein.Rows[i1]["totalImbuymoney"] != System.DBNull.Value)
                            {
                                dtdein.Rows[i1]["totalImbuymoney"] = double.Parse(dtdein.Rows[i1]["totalImbuymoney"].ToString()) + double.Parse(ArrRow[0]["Imbuymoney"].ToString());
                                dtdein.Rows[i1]["totalImsalmoney"] = double.Parse(dtdein.Rows[i1]["totalImsalmoney"].ToString()) + double.Parse(ArrRow[0]["Imsalmoney"].ToString());
                            }
                            else
                            {
                                if (ArrRow[0]["Imbuymoney"] != null && ArrRow[0]["Imbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["totalImbuymoney"] = double.Parse(ArrRow[0]["Imbuymoney"].ToString());
                                    dtdein.Rows[i1]["totalImsalmoney"] = double.Parse(ArrRow[0]["Imsalmoney"].ToString());
                                }
                            }
                            if (this.checkBox7.Checked)
                            {
                                if (ArrRow[0]["Imbuymoney"] != null && ArrRow[0]["Imbuymoney"].ToString() != "")
                                {
                                    dtdein.Rows[i1]["Imbuymoney"] = double.Parse(ArrRow[0]["Imbuymoney"].ToString());
                                    dtdein.Rows[i1]["Imsalmoney"] = double.Parse(ArrRow[0]["Imsalmoney"].ToString());
                                }
                            }
                            if (checkBox5.Checked)
                            {
                                if (dtdein.Rows[i1]["ImLIMITmoney"] != System.DBNull.Value)
                                {
                                    dtdein.Rows[i1]["ImLIMITmoney"] = double.Parse(dtdein.Rows[i1]["ImLIMITmoney"].ToString()) + double.Parse(ArrRow[0]["ImLIMITmoney"].ToString());
                                }
                                else
                                {
                                    dtdein.Rows[i1]["ImLIMITmoney"] = double.Parse(ArrRow[0]["ImLIMITmoney"].ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            #endregion
            for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
            {
                double BuyMoney = 0;
                double salmoney = 0;
                double LIMITmoney = 0;
                double AimTotalBuyMoney = 0;
                double NoAimTotalBuyMoney = 0;
                double AimTotalSalMoney = 0;
                double NoAimTotalSalMoney = 0;
                for (int f2 = 0; f2 < dtdein.Columns.Count; f2++)
                {
                    switch (dtdein.Columns[f2].ColumnName)
                    {
                        #region 
                        case "totalbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                BuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalChbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                BuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalEhbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                BuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalEhsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                salmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalChsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                salmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                salmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;

                        case "totalEhLIMITmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                LIMITmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalChLIMITmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                LIMITmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "totalEnLIMITmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                LIMITmoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        #endregion

                        #region 合计中标金额买入
                        case "AimImbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "Aimbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "AimChbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "AimEhbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;

                        #endregion

                        #region 合计中标金额零售
                        case "AimEhsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "AimChsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;

                        case "Aimsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "AimImsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                AimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;

                        #endregion

                        #region 合计非中标金额买入
                        case "Imbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "buymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "Chbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "Ehbuymoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalBuyMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;


                        #endregion

                        #region 合计非中标金额零售
                        case "Ehsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "Chsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "salmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                        case "Imsalmoney":
                            if (dtdein.Rows[i1][f2].ToString() != "")
                            {
                                NoAimTotalSalMoney += double.Parse(dtdein.Rows[i1][f2].ToString());
                            }
                            break;
                            #endregion
                    }
                }
               dtdein.Rows[i1]["购进金额"] = BuyMoney;
               dtdein.Rows[i1]["零售金额"] = salmoney;
               if (checkBox5.Checked)
                    dtdein.Rows[i1]["限价金额"] = LIMITmoney;
                if (checkBox6.Checked)
                {
                    dtdein.Rows[i1]["进价中标"] = AimTotalBuyMoney;
                    dtdein.Rows[i1]["零价中标"] = AimTotalSalMoney;
                }
                if (checkBox7.Checked)
                {
                    dtdein.Rows[i1]["进价非中标"] = NoAimTotalBuyMoney;
                    dtdein.Rows[i1]["零价非中标"] = NoAimTotalSalMoney;
                }
            }
            double[] arrList = new double[dtdein.Columns.Count - 1];
            for (int i1 = 0; i1 < dtdein.Rows.Count; i1++)
            {
                for (int f2 = 1; f2 < dtdein.Columns.Count; f2++)
                {
                    if(dtdein.Rows[i1][f2].ToString()!="")
                        arrList[f2 - 1] += double.Parse(dtdein.Rows[i1][f2].ToString());
                }
            }
            DataRow row = dtdein.NewRow();
            row["vendorname_vchr"] = "合计：";
            if (arrList.Length > 0)
            {
                for (int i1 = 0; i1 < arrList.Length; i1++)
                {
                    row[i1 + 1] = arrList[i1];
                }
            }
            dtdein.Rows.Add(row);
            ctlprintShow1.setDocument = printDocument1;
        }
        int intCount = 0;
        public void m_mthOutExcel()
        {
            if (dtdein != null && dtdein.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtdein.Columns.Count; i1++)
                {
                    switch (dtdein.Columns[i1].ColumnName)
                    {
                        case "VENDORNAME_VCHR":
                            dtdein.Columns[i1].ColumnName = "供应商名称";
                            break;
                        case "totalbuymoney":
                            dtdein.Columns[i1].ColumnName = "西药";
                            break;
                        case "totalsalmoney":
                            dtdein.Columns[i1].ColumnName = "西药零价";
                            break;
                        case "EnLIMITmoney":
                            dtdein.Columns[i1].ColumnName = "西药限价";
                            break;
                        case "totalEhbuymoney":
                            dtdein.Columns[i1].ColumnName = "中成药";
                            break;
                        case "totalEhsalmoney":
                            dtdein.Columns[i1].ColumnName = "中成药零价";
                            break;
                        case "EhLIMITmoney":
                            dtdein.Columns[i1].ColumnName = "中成药限价";
                            break;
                        case "totalChbuymoney":
                            dtdein.Columns[i1].ColumnName = "中草药";
                            break;
                        case "totalChsalmoney":
                            dtdein.Columns[i1].ColumnName = "中草药零价";
                            break;
                        case "ChLIMITmoney":
                            dtdein.Columns[i1].ColumnName = "中草药限价";
                            break;
                        case "totalImbuymoney":
                            dtdein.Columns[i1].ColumnName = "进口药";
                            break;
                        case "totalImsalmoney":
                            dtdein.Columns[i1].ColumnName = "进口药零价";
                            break;
                        case "ImLIMITmoney":
                            dtdein.Columns[i1].ColumnName = "进口药限价";
                            break;



                        case "Aimbuymoney":
                            dtdein.Columns[i1].ColumnName = "西药中标(进价)";
                            break;
                        case "buymoney":
                            dtdein.Columns[i1].ColumnName = "西药非中标(进价)";
                            break;
                        case "Aimsalmoney":
                            dtdein.Columns[i1].ColumnName = "西药中标(零价)";
                            break;
                        case "salmoney":
                            dtdein.Columns[i1].ColumnName = "西药非中标(零价)";
                            break;


                        case "AimEhbuymoney":
                            dtdein.Columns[i1].ColumnName = "中成药中标(进价)";
                            break;
                        case "Ehbuymoney":
                            dtdein.Columns[i1].ColumnName = "中成药非中标(进价)";
                            break;
                        case "AimEhsalmoney":
                            dtdein.Columns[i1].ColumnName = "中成药中标(零价)";
                            break;
                        case "Ehsalmoney":
                            dtdein.Columns[i1].ColumnName = "中成药非中标(零价)";
                            break;

                        case "AimChbuymoney":
                            dtdein.Columns[i1].ColumnName = "中草药中标(进价)";
                            break;
                        case "Chbuymoney":
                            dtdein.Columns[i1].ColumnName = "中草药非中标(进价)";
                            break;
                        case "AimChsalmoney":
                            dtdein.Columns[i1].ColumnName = "中草药中标(零价)";
                            break;
                        case "Chsalmoney":
                            dtdein.Columns[i1].ColumnName = "中草药非中标(零价)";
                            break;



                        case "AimImbuymoney":
                            dtdein.Columns[i1].ColumnName = "进口药中标(进价)";
                            break;
                        case "Imbuymoney":
                            dtdein.Columns[i1].ColumnName = "进口药非中标(进价)";
                            break;
                        case "AimImsalmoney":
                            dtdein.Columns[i1].ColumnName = "进口药中标(零价)";
                            break;
                        case "Imsalmoney":
                            dtdein.Columns[i1].ColumnName = "进口药非中标(零价)";
                            break;


                    }
                }
                for (int f2 = 0; f2 < dtdein.Rows.Count; f2++)
                {
                    for (int f3 = 0; f3 < dtdein.Columns.Count; f3++)
                    {
                        if (dtdein.Rows[f2][f3] == null || dtdein.Rows[f2][f3].ToString() == "")
                        {
                            dtdein.Rows[f2][f3] = 0;
                        }
                    }
                }
                intCount++;
                DataTable dttemp = new DataTable("Table" + intCount.ToString());
                string str = "";
                for (int i = 0; i < dtdein.Columns.Count; i++)
                {
                    str = dtdein.Columns[i].ColumnName.Replace("(", "");
                    str = str.Replace(")", "");
                    dttemp.Columns.Add(str, dtdein.Columns[i].DataType);
                }
                DataRow dr = null;
                for (int i = 0; i < dtdein.Rows.Count; i++)
                {
                    dr = dttemp.NewRow();
                    for (int i2 = 0; i2 < dtdein.Columns.Count; i2++)
                    {
                        dr[i2] = dtdein.Rows[i][i2];
                    }
                    dttemp.Rows.Add(dr);
                }
                DataSet ds = new DataSet();
                ds.Tables.Clear();
                ds.Tables.Add(dttemp);
                ExcelExporter excel = new ExcelExporter(ds);
                bool b = excel.m_mthExport();
                if (b)
                {
                    MessageBox.Show("导出数据成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("导出数据失败。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                ds.Tables.Clear();
                dttemp = null;
                ds = null;
            }
        }
        float Y = 0;
        int currPage = 0;
        int pageW = 0;
        int pageH = 0;
        /// <summary>
        /// 画笔
        /// </summary>
        Pen PenLine = new Pen(Brushes.Black, 1);
        /// <summary>
        /// 标题字体
        /// </summary>
        Font objFontTitle = new Font("楷体_GB2312", 14, System.Drawing.FontStyle.Bold);
        /// <summary>
        /// 正常字体
        /// </summary>
        Font objFontNormal = new Font("SimSun", 10);
        /// <summary>
        /// 特细字体
        /// </summary>
        Font objFont = new Font("SimSun", 9);
        /// <summary>
        /// 加粗字体
        /// </summary>
        Font objFontBold = new Font("SimSun", 11, System.Drawing.FontStyle.Bold);
        const float rowH = 23F;
        float coumnWith = 0;
        /// <summary>
        /// 记录当前行数
        /// </summary>
        int dtRow = 0;
        float X = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (dtdein.Rows.Count > 0)
            {
            X = 50;
            Y = 50;
            if (currPage == 0)
            {
                pageW = e.PageBounds.Width;
                pageH = e.PageBounds.Height;
                coumnWith = (pageW - 180) / dtdein.Columns.Count;
            }
            m_mthPrintTitle(e);
            X = 50;
            Y += rowH;
            m_mthPrintBoby(e);
            }
        }
        private void m_mthprintEnd(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int page = currPage + 1;
            Y = pageH - 50;
            X = 100;
            e.Graphics.DrawString("制单:", objFontNormal, Brushes.Black, X, Y);
            X += 100;
            e.Graphics.DrawString("审核:", objFontNormal, Brushes.Black, X, Y);
            X += 100;
            e.Graphics.DrawString("复核:", objFontNormal, Brushes.Black, X, Y);
            X = pageW - 120;
            e.Graphics.DrawString("第" + page.ToString()+ "页", objFontNormal, Brushes.Black, X, Y);
        }
        private void m_mthPrintBoby(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawLine(PenLine, X, Y, pageW - 50, Y);
            Font F;
            for (int f2 = dtRow; f2 < dtdein.Rows.Count; f2++)
            {
                if (f2 == dtdein.Rows.Count - 1)
                {
                    F = objFontNormal;
                }
                else
                {
                    F = objFontNormal;
                }
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + rowH);
                for (int f3 = 0; f3 < dtdein.Columns.Count; f3++)
                {
                    float Pwith = coumnWith;
                    if (f3 == 0)
                    {
                        Pwith += 60;
                    }
                    bool isSub;
                    int coum = GetMaxLen(dtdein.Rows[f2][f3].ToString(), Pwith, objFontNormal, out isSub, e);
                    if (isSub)
                    {
                        string str1 = dtdein.Rows[f2][f3].ToString().Substring(0, coum);

                        e.Graphics.DrawString(str1, objFont, Brushes.Black, X, Y);
                        e.Graphics.DrawString(dtdein.Rows[f2][f3].ToString().Replace(str1, ""), objFont, Brushes.Black, X, Y+10);
                    }
                    else
                    {
                        e.Graphics.DrawString(dtdein.Rows[f2][f3].ToString(), F, Brushes.Black, X, Y + 2);
                    }

                    if (f3 == 0)
                    {
                        X += coumnWith + 80;
                    }
                    else if (f3 != dtdein.Columns.Count - 1)
                    {
                        X += coumnWith;
                    }
                    else
                    {
                        X = pageW - 50;
                    }
                    e.Graphics.DrawLine(PenLine, X, Y, X, Y + rowH);
                }
                X = 50;
                Y += rowH;
                e.Graphics.DrawLine(PenLine, X, Y, pageW - 50, Y);
                if (Y + rowH > pageH - 60)
                {
                    dtRow = f2+1;
                    e.HasMorePages = true;
                    currPage++;
                    m_mthprintEnd(e);
                    return;
                }
                if (f2 == dtdein.Rows.Count - 1)
                {
                    dtRow = 0;
                    currPage = 0;
                    m_mthprintEnd(e);
                }
            }
        }
        private void m_mthPrintTitle(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string strTitle="";
            strTitle = this.Text;
            SizeF titlewith = e.Graphics.MeasureString(strTitle, objFontTitle);
            X = (pageW - titlewith.Width) / 2;
            e.Graphics.DrawString(strTitle, objFontTitle, Brushes.Black, X, Y);
            Y += rowH;
            X = 50;
            e.Graphics.DrawLine(PenLine, X, Y, pageW - 50, Y);
            e.Graphics.DrawLine(PenLine, X, Y, X, Y + rowH);
            e.Graphics.DrawString("统计日期:从" + m_cboSelPeriodBegion.Text + "到" + m_cboSelPeriodEnd.Text, objFontNormal, Brushes.Black, X, Y + 2);
            e.Graphics.DrawLine(PenLine, pageW - 50, Y, pageW - 50, Y + rowH);
            Y += rowH;
            e.Graphics.DrawLine(PenLine, X, Y, pageW - 50, Y);
            e.Graphics.DrawLine(PenLine, X, Y, X, Y + rowH);
            for (int i1 = 0; i1 < dtdein.Columns.Count; i1++)
            {
                float Pwith = coumnWith;
                if (i1 == 0)
                {
                    Pwith += 80;
                }
                bool isSub;
                int coum = GetMaxLen(dtdein.Columns[i1].Caption, Pwith, objFontNormal, out isSub, e);
                if (isSub && coum>0)
                {
                    string str1 = dtdein.Columns[i1].Caption.Substring(0, coum);

                    e.Graphics.DrawString(str1, objFont, Brushes.Black, X, Y);
                    e.Graphics.DrawString(dtdein.Columns[i1].Caption.Replace(str1,""), objFont, Brushes.Black, X, Y+10);
                }
                else
                {
                    e.Graphics.DrawString(dtdein.Columns[i1].Caption, objFontNormal, Brushes.Black, X, Y + 2);
                }
                if (i1 == 0)
                {
                    X += coumnWith + 80;
                }
                else if (i1 != dtdein.Columns.Count - 1)
                {
                    X += coumnWith;
                }
                else
                {
                    X = pageW - 50;
                }
                e.Graphics.DrawLine(PenLine, X, Y, X, Y + rowH);
            }
        }
        #region 判断是否需要换行
        /// <summary>
        /// 判断是否需要换行
        /// </summary>
        /// <param name="str">传入要判断的字符串</param>
        /// <param name="maxWidth">最大的长度，如果超过此长度则要换行</param>
        /// <param name="font">字体</param>
        /// <param name="IsSub">是否要换行</param>
        /// <returns></returns>
        private  int GetMaxLen(string str, float maxWidth, Font font, out bool IsSub, System.Drawing.Printing.PrintPageEventArgs e)
        {
            IsSub = false;
            if (str == null)
                return 0;
            int i = 0; ;
            float width = 0;
            SizeF size;
            while ((width < maxWidth) && (i < str.Length))
            {
                i++;
                size = e.Graphics.MeasureString(str.Substring(0, i), font);
                width = size.Width;
                if (width >= maxWidth)
                {
                    IsSub = true;
                }
            }
            return i - 1;
        }
        #endregion
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            m_mthOutExcel();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
        }
    }
}
