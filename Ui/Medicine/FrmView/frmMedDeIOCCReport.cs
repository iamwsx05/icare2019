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
    public partial class frmMedDeIOCCReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmMedDeIOCCReport()
        {
            InitializeComponent();
        }

        private void m_txtFindPharm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Point p = this.m_txtFindPharm.Parent.PointToScreen(this.m_txtFindPharm.Location);
                p.Offset(-m_DlgResult.Width + this.m_txtFindPharm.Width, 10);
                p = this.m_txtFindPharm.FindForm().PointToClient(p);
                p.Y += this.m_txtFindPharm.Height;
                m_DlgResult.Location = p;
                this.m_DlgResult.Visible = true;
                this.m_DlgResult.Focus();
                this.m_DlgResult.strSTORAGEID = "";
                this.m_DlgResult.FindMedmode = 0;
                this.m_DlgResult.m_txtFindMed.Text = m_txtFindPharm.Text;
            }
        }

        private void m_btnQuery_Click(object sender, EventArgs e)
        {
            if ((m_txtFindPharm.Text != "" &&m_txtFindPharm.Tag != null)||comboBox1.Text!="")
            {
                if (int.Parse(this.m_cboSelPeriodBegion.SelectItemValue) > int.Parse(this.m_cboSelPeriodEnd.SelectItemValue))
                {
                    MessageBox.Show("开始财务期不能大于结束财务期！");
                }
                else
                {
                    clsDomainConrol_Medicne domain = new clsDomainConrol_Medicne();
                    System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                    System.Collections.Generic.List<string> UPlist = new System.Collections.Generic.List<string>();
                    for (int i1 = this.m_cboSelPeriodBegion.SelectedIndex; i1 <= this.m_cboSelPeriodEnd.SelectedIndex; i1++)
                    {
                        list.Add(objPriodItems[i1].m_strPeriodID);
                        if (i1 == 0)
                        {
                            UPlist.Add("");
                        }
                        else
                        {
                            UPlist.Add(objPriodItems[i1 - 1].m_strPeriodID);
                        }
                        
                    }
                    DataTable dt = new DataTable();
                    if (radioButton1.Checked)
                    {
                        domain.m_lngGetReportDataOfInAndOutDe(list, UPlist, (string)this.m_txtFindPharm.Tag,0, out dt);
                    }
                    else
                    {
                        domain.m_lngGetReportDataOfInAndOutDe(list, UPlist, (string)this.m_txtFindPharm.Tag,comboBox1.SelectedIndex+1, out dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                        {
                            if (dt.Rows[i1]["groupCol"].ToString() == "1" || dt.Rows[i1]["groupCol"].ToString() == "5")
                            {
                                for (int f2 = 0; f2 < objPriodItems.Length; f2++)
                                {
                                    if (dt.Rows[i1]["period"].ToString() == objPriodItems[f2].m_strPeriodID)
                                    {
                                        if (dt.Rows[i1]["groupCol"].ToString() == "5")
                                        {
                                            dt.Rows[i1]["dat"] = objPriodItems[f2].m_strEndDate;
                                            break;
                                        }
                                        else
                                        {
                                            dt.Rows[i1]["dat"] = objPriodItems[f2].m_strEndDate;
                                            dt.Rows[i1]["period"] = objPriodItems[f2 + 1].m_strPeriodID;
                                            break;
                                        }
                                        
                                    }
                                    
                                }
                              
                            }
                        }
                    }
                    try
                    {
                        this.dw_1.SetRedrawOff();
                        this.dw_1.Retrieve(dt);
                        this.dw_1.Sort();
                        this.dw_1.CalculateGroups();
                        this.dw_1.SetRedrawOn();
                        this.dw_1.Refresh();
                    }
                    catch (Exception ex)
                    {
                        //DWErrorHandler.HandleDWException(ex);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择药品！");
            }
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
        }
        #endregion

        private void frmMedDeIOCCReport_Load(object sender, EventArgs e)
        {
            m_mthGetPeriodList();
        }

        private void btnesc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            dw_1.Print();
        }
        private void m_DlgResult_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
        {
            this.m_txtFindPharm.Text = e.ReturnVo.strMEDICINENAME_VCHR;
            this.m_txtFindPharm.Tag = e.ReturnVo.strMEDICINEID_CHR;
            this.m_DlgResult.Visible = false;
            this.m_btnQuery.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           m_txtFindPharm.Enabled = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = radioButton2.Checked;
        }
    }
}