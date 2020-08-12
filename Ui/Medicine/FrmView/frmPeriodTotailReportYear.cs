using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmPeriodTotailReportYear : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmPeriodTotailReportYear()
        {
            InitializeComponent();
        }

        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            if (m_btnSeleYear.Text != "")
            {
                clsPeriod_VO[] objPriodItems = clsPublicParm.s_GetPeriodList();
                System.Collections.Generic.List<string> arrPriod = new System.Collections.Generic.List<string>();
                System.Collections.ArrayList arrPriodmoth = new System.Collections.ArrayList();
                if (objPriodItems.Length > 0)
                {
                    for (int i1 = 0; i1 < objPriodItems.Length; i1++)
                    {
                        if ((DateTime.Parse(objPriodItems[i1].m_strEndDate)).Year == int.Parse(m_btnSeleYear.Text))
                        {
                            arrPriod.Add(objPriodItems[i1].m_strPeriodID);
                            arrPriodmoth.Add((DateTime.Parse(objPriodItems[i1].m_strEndDate)).Month.ToString());
                        }
                    }
                    DataTable dt = new DataTable();
                    clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
                    domain.m_lngGetReportDataOfInAndOutYear(arrPriod, out dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["ROWNAME"] = "合计";
                        for (int f2 = 1; f2 < dt.Columns.Count; f2++)
                        {
                            newRow[f2] = 0;
                        }
                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            dt.Rows[i1]["ROWNAME"] = (string)arrPriodmoth[i1]+"月";
                            for (int f2 = 1; f2 < dt.Columns.Count; f2++)
                            {
                                if (dt.Rows[i1][f2] != DBNull.Value &&dt.Rows[i1][f2].ToString() != "")
                                {
                                    newRow[f2] = decimal.Parse(newRow[f2].ToString()) + decimal.Parse(dt.Rows[i1][f2].ToString());
                                }
                            }
                        }
                        dt.Rows.Add(newRow);
                            dw_1.Reset();
                            int newRowNuber;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                newRowNuber = dw_1.InsertRow();
                                dw_1.SetItemString(newRowNuber, "rowname", dt.Rows[i]["rowname"].ToString());
                                if (dt.Rows[i]["westmedinmoney"] == null || dt.Rows[i]["westmedinmoney"].ToString() == "")
                                {
                                    dt.Rows[i]["westmedinmoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "westmedinmoney", decimal.Parse(dt.Rows[i]["westmedinmoney"].ToString()));
                                if (dt.Rows[i]["westmedsalemoney"] == null || dt.Rows[i]["westmedsalemoney"].ToString() == "")
                                {
                                    dt.Rows[i]["westmedsalemoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "westmedsalemoney", decimal.Parse(dt.Rows[i]["westmedsalemoney"].ToString()));
                                if (dt.Rows[i]["wcinmoney"] == null || dt.Rows[i]["wcinmoney"].ToString() == "")
                                {
                                    dt.Rows[i]["wcinmoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "wcinmoney", decimal.Parse(dt.Rows[i]["wcinmoney"].ToString()));
                                if (dt.Rows[i]["WCsalemoney"] == null || dt.Rows[i]["WCsalemoney"].ToString() == "")
                                {
                                    dt.Rows[i]["WCsalemoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "wcsalemoney", decimal.Parse(dt.Rows[i]["WCsalemoney"].ToString()));
                                if (dt.Rows[i]["chinmoney"] == null || dt.Rows[i]["chinmoney"].ToString() == "")
                                {
                                    dt.Rows[i]["chinmoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "chinmoney", decimal.Parse(dt.Rows[i]["chinmoney"].ToString()));
                                if (dt.Rows[i]["chsalemoney"] == null || dt.Rows[i]["chsalemoney"].ToString() == "")
                                {
                                    dt.Rows[i]["chsalemoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "chsalemoney", decimal.Parse(dt.Rows[i]["chsalemoney"].ToString()));
                                if (dt.Rows[i]["totailinmoney"] == null || dt.Rows[i]["totailinmoney"].ToString() == "")
                                {
                                    dt.Rows[i]["totailinmoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "totailinmoney", decimal.Parse(dt.Rows[i]["totailinmoney"].ToString()));
                                if (dt.Rows[i]["totailsalemoney"] == null || dt.Rows[i]["totailsalemoney"].ToString() == "")
                                {
                                    dt.Rows[i]["totailsalemoney"] = 0;
                                }
                                dw_1.SetItemDecimal(newRowNuber, "totailsalemoney", decimal.Parse(dt.Rows[i]["totailsalemoney"].ToString()));
                            }

                            dw_1.Modify("t_7.text = '统计年份：" + this.m_btnSeleYear.Text + "'");
                            dw_1.CalculateGroups();
                            dw_1.Refresh();
                        }
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
    }
}