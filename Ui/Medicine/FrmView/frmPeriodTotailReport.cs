using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Collections;
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmPeriodTotailReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmPeriodTotailReport()
        {
            InitializeComponent();
        }

        private void frmPeriodTotailReport_Load(object sender, EventArgs e)
        {
            m_mthGetPeriodList();
        }
        clsPeriod_VO[] objPriodItems = null;
        #region 获得帐务期列表
        /// <summary>
        /// 获得帐务期列表
        /// </summary>
        private void m_mthGetPeriodList()
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
                    if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(objPriodItems[i1].m_strStartDate)&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
					{
						intSelPeriod = i1;
					}
                }
            }
				if(intSelPeriod!=-1)
				{
                    this.m_cboSelPeriodEnd.SelectedIndex = intSelPeriod;
                    this.m_cboSelPeriodBegion.SelectedIndex = intSelPeriod;
				}
        }
        #endregion

        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.m_cboSelPeriodBegion.SelectItemValue) > int.Parse(this.m_cboSelPeriodEnd.SelectItemValue))
            {
                MessageBox.Show("开始财务期不能大于结束财务期！");
            }
            else
            {
                System.Collections.Generic.List<string> itemlist = new System.Collections.Generic.List<string>();
                clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
                for (int i1 = this.m_cboSelPeriodBegion.SelectedIndex; i1 <=this.m_cboSelPeriodEnd.SelectedIndex; i1++)
                {

                    itemlist.Add(objPriodItems[i1].m_strPeriodID);
                }
                DataTable dt = new DataTable();
                domain.m_lngGetReportDataOfInAndOut(itemlist, objPriodItems[this.m_cboSelPeriodBegion.SelectedIndex-1].m_strPeriodID, out dt);
                if (dt.Rows.Count > 0)
                {
                    dw_1.Reset();
                    int newRow;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        newRow = dw_1.InsertRow();
                        dw_1.SetItemString(newRow, "rowname", dt.Rows[i]["rowname"].ToString());
                        if (dt.Rows[i]["westmedinmoney"] == null || dt.Rows[i]["westmedinmoney"].ToString() == "")
                        {
                            dt.Rows[i]["westmedinmoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "westmedinmoney", decimal.Parse(dt.Rows[i]["westmedinmoney"].ToString()));
                        if (dt.Rows[i]["westmedsalemoney"] == null || dt.Rows[i]["westmedsalemoney"].ToString() == "")
                        {
                            dt.Rows[i]["westmedsalemoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "westmedsalemoney", decimal.Parse(dt.Rows[i]["westmedsalemoney"].ToString()));
                        if (dt.Rows[i]["wcinmoney"]==null||dt.Rows[i]["wcinmoney"].ToString() == "")
                        {
                            dt.Rows[i]["wcinmoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "wcinmoney", decimal.Parse(dt.Rows[i]["wcinmoney"].ToString()));
                        if (dt.Rows[i]["WCsalemoney"] == null || dt.Rows[i]["WCsalemoney"].ToString() == "")
                        {
                            dt.Rows[i]["WCsalemoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "wcsalemoney", decimal.Parse(dt.Rows[i]["WCsalemoney"].ToString()));
                        if (dt.Rows[i]["chinmoney"] == null || dt.Rows[i]["chinmoney"].ToString() == "")
                        {
                            dt.Rows[i]["chinmoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "chinmoney", decimal.Parse(dt.Rows[i]["chinmoney"].ToString()));
                        if (dt.Rows[i]["chsalemoney"] == null || dt.Rows[i]["chsalemoney"].ToString() == "")
                        {
                            dt.Rows[i]["chsalemoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "chsalemoney", decimal.Parse(dt.Rows[i]["chsalemoney"].ToString()));
                        if (dt.Rows[i]["totailinmoney"] == null || dt.Rows[i]["totailinmoney"].ToString() == "")
                        {
                            dt.Rows[i]["totailinmoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "totailinmoney", decimal.Parse(dt.Rows[i]["totailinmoney"].ToString()));
                        if (dt.Rows[i]["totailsalemoney"] == null || dt.Rows[i]["totailsalemoney"].ToString() == "")
                        {
                            dt.Rows[i]["totailsalemoney"] = 0;
                        }
                        dw_1.SetItemDecimal(newRow, "totailsalemoney", decimal.Parse(dt.Rows[i]["totailsalemoney"].ToString()));
                    }

                    dw_1.Modify("t_7.text = '财务期：" + this.m_cboSelPeriodBegion.Text +"到"+this.m_cboSelPeriodEnd.Text+ "'");
                    dw_1.CalculateGroups();
                    dw_1.Refresh();
                }
            }
        }

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            dw_1.Print();
        }

        private void btnesc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cboSelPeriodEnd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void m_cboSelPeriodBegion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}