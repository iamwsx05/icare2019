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
    public partial class frmMedQuery : com.digitalwave.GUI_Base.frmMDI_Child_Base	//GUI_Base.dll
    {
        public frmMedQuery()
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

        private void m_DlgResult_m_evtReturnVal(object sender, com.digitalwave.controls.clsEvtReturnVal e)
        {
            this.m_txtFindPharm.Text = e.ReturnVo.strMEDICINENAME_VCHR;
            this.m_txtFindPharm.Tag = e.ReturnVo.strMEDICINEID_CHR;
            this.m_DlgResult.Visible = false;
            this.m_cmdFind.Focus();
        }

        private void m_cmdFind_Click(object sender, EventArgs e)
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
            clsPublicParm publicClass = new clsPublicParm();
            listView1.Items.Clear();
            DataTable dt = new DataTable();
            clsDomainControlStorageQuery domain = new clsDomainControlStorageQuery();
            bool isMed;
            string FindID;
            if (radioButton1.Checked)
            {
                if (m_txtFindPharm.Text == "")
                {
                    publicClass.m_mthShowWarning(m_txtFindPharm, "请输入查询条件！");
                    return;
                }
                isMed = true;
                FindID = (string)m_txtFindPharm.Tag;
            }
            else
            {
                if (m_txtVen.txtValuse == "")
                {
                    publicClass.m_mthShowWarning(m_txtVen, "请输入查询条件！");
                    return;
                }
                isMed = false;
                FindID = (string)m_txtVen.Tag;
            }
            domain.m_lngOutInReMark(FindID, list, comboBox1.SelectedIndex.ToString(), out dt, isMed);

            if (dt != null && dt.Rows.Count > 0)
            {
                double inMoney = 0;
                double SaleMoney = 0;
                double totalNuber = 0;
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "1")
                    {
                        totalNuber += double.Parse(dt.Rows[i1]["qty_dec"].ToString());
                        SaleMoney += double.Parse(dt.Rows[i1]["salemoney"].ToString());
                        inMoney += double.Parse(dt.Rows[i1]["money"].ToString());
                    }
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "2")
                    {
                        totalNuber -= double.Parse(dt.Rows[i1]["qty_dec"].ToString());
                        SaleMoney -= double.Parse(dt.Rows[i1]["salemoney"].ToString());
                        inMoney -= double.Parse(dt.Rows[i1]["money"].ToString());
                    }
                }
                DataRow row = dt.NewRow();
                row["medicinename_vchr"] = "合计：";
                row["qty_dec"] = totalNuber;
                row["money"] = inMoney;
                row["AIMUNIT_INT"] = 5;
                row["salemoney"] = SaleMoney;
                dt.Rows.Add(row);
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    ListViewItem Item = new ListViewItem(dt.Rows[i1]["medicinename_vchr"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["medspec_vchr"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["lotno_vchr"].ToString());
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "1")
                    {
                        Item.SubItems.Add("入库");
                    }
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "2")
                    {
                        Item.SubItems.Add("出库");
                    }
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "3")
                    {
                        Item.SubItems.Add("退库");
                    }
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "4")
                    {
                        Item.SubItems.Add("退货");
                    }
                    if (dt.Rows[i1]["SIGN_INT"].ToString() == "")
                    {
                        Item.SubItems.Add("");
                    }
                    Item.SubItems.Add(dt.Rows[i1]["docid_vchr"].ToString().Trim());
                    Item.SubItems.Add(dt.Rows[i1]["inord_dat"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["unitid_chr"].ToString().Trim());
                    Item.SubItems.Add(dt.Rows[i1]["qty_dec"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["BUYUNITPRICE_MNY"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["money"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["saleunitprice_mny"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["salemoney"].ToString());
                    if (dt.Rows[i1]["AIMUNIT_INT"].ToString() == "1")
                    {
                        Item.SubItems.Add("是");
                    }
                    else if (dt.Rows[i1]["AIMUNIT_INT"].ToString() != "5")
                    {
                        Item.SubItems.Add("否");
                    }
                    Item.SubItems.Add(dt.Rows[i1]["vendorname_vchr"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["syslotno_chr"].ToString());
                    Item.SubItems.Add(dt.Rows[i1]["aduitdate_dat"].ToString());


                    listView1.Items.Add(Item);
                }

            }
            else
            {
                publicClass.m_mthShowWarning(m_txtFindPharm, "没有符合条件的数据！");
            }
        }
        clsPeriod_VO[] objPriodItems = null;
        private void frmMedQuery_Load(object sender, EventArgs e)
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
            DataTable p_dtbResult = new DataTable();
            clsDomainControlStorageQuery domain = new clsDomainControlStorageQuery();
            domain.m_lngGetAllVendor(out p_dtbResult);
            m_txtVen.m_GetDataTable = p_dtbResult;
        }

        private void m_cmbEsc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label4.Text = "查找药品";
                m_txtVen.Visible = false;
                m_txtFindPharm.Visible = true;
                m_txtFindPharm.Focus();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label4.Text = "供应商";
                m_txtVen.Visible = true;
                m_txtFindPharm.Visible = false;
                m_txtVen.Focus();
            }
        }

        private void m_txtVen_Load(object sender, EventArgs e)
        {

        }
    }
}