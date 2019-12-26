using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ControlLibrary;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ÿ���嵥UI��
    /// </summary>
    public partial class frmRptEveryDayBill : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ÿ���嵥�Զ��屨��ID
        /// </summary>
        private string RepID = "";
        /// <summary>
        /// ���ҷ�Χ
        /// </summary>
        private string Scope = "";
        /// <summary>
        /// ����ҵ����
        /// </summary>
        private clsCtl_Report objReport;
        /// <summary>
        /// ��¼��������
        /// </summary>
        private Form frmMark = null;
        /// <summary>
        /// ��Ժ�Ǽ�ID
        /// </summary>
        private string RegID = "";
        /// <summary>
        /// ��Ժ�Ǽ�ID
        /// </summary>
        public string RegisterID
        {
            set
            {
                RegID = value;
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string bedname = "";
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string BedName
        {
            set
            {
                bedname = value;
                this.txtBedID.Text = value;
            }
        }
        /// <summary>
        /// ��Ŀ����ʹ������ 0 �����շѱ��� 1 ��Ŀ����
        /// </summary>
        private int ItemCodeType = 1;

        /// <summary>
        /// ��������
        /// </summary>
        private int intDiffCostOn = 0;

        /// <summary>
        /// ����
        /// </summary>
        public frmRptEveryDayBill()
        {
            InitializeComponent();
            objReport = new clsCtl_Report();
        }

        /// <summary>
        /// �ҽ����
        /// </summary>
        /// <param name="ReportID">ÿ���嵥�Զ��屨��ID Ĭ�ϣ�0003</param>
        public void m_mthShow(string ReportID)
        {
            if (ReportID.Trim() != "")
            {
                RepID = ReportID.Trim();
                if (!Microsoft.VisualBasic.Information.IsNumeric(RepID.Substring(RepID.Length - 1, 1)))
                {
                    Scope = RepID.Substring(RepID.Length - 1, 1);
                    RepID = RepID.Substring(0, RepID.Length - 1);
                }
            }
            else
            {
                RepID = "0003";
            }

            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRptEveryDayBill_Load(object sender, EventArgs e)
        {
            // �����б�
            clsColumns_VO[] columArr = new clsColumns_VO[]{ new clsColumns_VO("���","code_vchr",HorizontalAlignment.Left,50),
                                                            new clsColumns_VO("ƴ����","pycode_chr",HorizontalAlignment.Left,60),
                                                            new clsColumns_VO("��������","deptname_vchr",HorizontalAlignment.Left,130)
                                                          };

            if (Scope == "%")
            {
                this.m_txtAREAID.m_strSQL = @" select '00' as deptid_chr,
                                                      'ȫԺ' as deptname_vchr,
                                                      'qy' as pycode_chr, 
                                                      '00' as code_vchr
                                                 from dual             
                                                
                                                union all

                                                select a.deptid_chr, a.deptname_vchr, a.pycode_chr, a.code_vchr
                                                  from t_bse_deptdesc a
                                                 where a.status_int = 1 
                                                   and a.attributeid = '0000003'";
            }
            else
            {
                this.m_txtAREAID.m_strSQL = @"select a.deptid_chr, a.deptname_vchr, a.pycode_chr, a.code_vchr
                                              from t_bse_deptdesc a,
                                                   t_bse_deptemp b 
                                             where a.deptid_chr = b.deptid_chr 
                                               and a.status_int = 1 
                                               and a.attributeid = '0000003' 
                                               and b.empid_chr = '" + this.LoginInfo.m_strEmpID + @"'    
                                          order by a.code_vchr";
            }

            this.m_txtAREAID.m_mthInitListView(columArr);
            this.m_txtAREAID.Value = this.LoginInfo.m_strInpatientAreaID;
            this.m_txtAREAID.Text = this.LoginInfo.m_strInpatientAreaName;
            this.lsvBed.Tag = objReport.m_dtGetBed(this.LoginInfo.m_strInpatientAreaID);
            this.m_txtAREAID.SelectAll();

            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            this.dteRq.Value = DateTime.Now.Subtract(ts);

            this.rdoArea.Checked = true;
            this.lsvBed.Enabled = false;
            //Ĭ�ϰ����� 2008-02-21 yunjie.xie �޸�
            //if (clsPublic.m_strGetSysparm("1000") == "001")
            //{
            //    this.cboType.SelectedIndex = 0;
            //    this.cboType.Enabled = false;
            //}
            //else
            //{
            //    this.cboType.SelectedIndex = 1;
            //}
            this.cboType.SelectedIndex = 1;

            //��ȡ��Ŀ����ʹ������
            ItemCodeType = clsPublic.m_intGetSysParm("9008");
            intDiffCostOn = clsPublic.m_intGetSysParm("9002");
            if (intDiffCostOn == 1)
                this.dwRep.DataWindowObject = "d_everydaybill_diff";
            else
                this.dwRep.DataWindowObject = "d_everydaybill";
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            int Type = 0;
            string ID = "";

            if (this.rdoArea.Checked)
            {
                if (this.m_txtAREAID.Value != null)
                {
                    ID = this.m_txtAREAID.Value.ToString().Trim();
                }

                if (ID == "")
                {
                    MessageBox.Show("��ѡ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Type = 1;
            }
            else if (this.rdoBed.Checked)
            {
                foreach (ListViewItem item in this.lsvBed.Items)
                {
                    if (item.Checked == true)
                    {
                        DataRow dr = item.Tag as DataRow;
                        ID += "'" + dr["registerid_chr"].ToString() + "',";
                    }
                }
                if (ID == "")
                {
                    MessageBox.Show("��ѡ���ˣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ID = ID.TrimEnd(',');
                Type = 4;
            }
            else if (this.rdoZyh.Checked)
            {
                ID = this.txtZyh.Text.Trim();

                if (ID == "")
                {
                    MessageBox.Show("������סԺ�ţ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Type = 3;
            }

            string BillDate = this.dteRq.Value.ToString("yyyy-MM-dd");

            clsPublic.PlayAvi("findFILE.avi", "���������嵥��Ϣ�����Ժ�...");
            if (this.cboType.SelectedIndex == 0)
            {
                this.objReport.m_mthRptEveryDayBillEntry2(ID, BillDate, Type, ItemCodeType, this.dwRep);
            }
            else if (this.cboType.SelectedIndex == 1)
            {
                this.objReport.m_mthRptEveryDayBill(RepID, ID, BillDate, Type, this.dwRep);
            }
            else if (this.cboType.SelectedIndex == 2)
            {
                //(RegID, 999, null, CurrAreaID, CurrBeginDate, CurrEndDate, out dtSource);
                this.objReport.m_mthRptEveryDayBillCate(ID, BillDate, Type, ItemCodeType, this.dwRep);
            }
            clsPublic.CloseAvi();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.dwRep.PrintProperties.Preview = !this.dwRep.PrintProperties.Preview;
            this.dwRep.PrintProperties.ShowPreviewRulers = this.dwRep.PrintProperties.Preview;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsPublic.ChoosePrintDialog(this.dwRep, true);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dwRep.RowCount > 0)
            {
                clsPublic.ExportDataWindow(this.dwRep, null);
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dwRep.LibraryList = clsPublic.PBLPath;
            this.dwRep.Reset();

            if (this.cboType.SelectedIndex == 0)
            {
                if (intDiffCostOn == 1)
                    this.dwRep.DataWindowObject = "d_bih_everydaybill_entry2_diff";
                else
                    this.dwRep.DataWindowObject = "d_bih_everydaybill_entry2";
            }
            else if (this.cboType.SelectedIndex == 1)
            {
                if (intDiffCostOn == 1)
                    this.dwRep.DataWindowObject = "d_everydaybill_diff";
                else
                    this.dwRep.DataWindowObject = "d_everydaybill";
            }
            else if (this.cboType.SelectedIndex == 2)
            {
                if (intDiffCostOn == 1)
                    this.dwRep.DataWindowObject = "d_bih_everydaybill_Cate_diff";
                else
                    this.dwRep.DataWindowObject = "d_bih_everydaybill_Cate";
            }

            this.dwRep.InsertRow(0);
            this.dwRep.Modify("t_title.text = '" + this.objReport.HospitalName + this.dwRep.Describe("t_title.text") + "'");
            this.dwRep.PrintProperties.Preview = false;
            this.dwRep.Refresh();
        }

        private void txtBedID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.m_txtAREAID.Value == null)
                {
                    MessageBox.Show("����ѡ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string areaid = this.m_txtAREAID.Value.ToString().Trim();
                    frmAidFind fAid = new frmAidFind(this, 3, areaid, this.objReport.objDeptIDArr);
                    fAid.StartPosition = FormStartPosition.Manual;

                    frmMark = fAid;

                    Point p1 = new Point(this.txtBedID.Location.X - 98, this.txtBedID.Location.Y + this.txtBedID.Height - 143);
                    Point p2 = this.txtBedID.PointToScreen(p1);
                    fAid.Location = new Point(p2.X, p2.Y);
                    fAid.Width = fAid.Width - 30;
                    fAid.Show();
                }
            }
        }

        private void txtBedID_Leave(object sender, EventArgs e)
        {
            if (frmMark != null)
            {
                frmMark.Close();
                frmMark = null;
            }
        }

        private void txtBedID_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBedID.Text.Trim() != bedname)
            {
                RegID = "";
            }
        }

        private void m_txtAREAID_TextChanged(object sender, EventArgs e)
        {
            this.txtBedID.Text = "";
            this.RegID = "";
        }

        private void rdoArea_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoArea.Checked)
            {
                this.txtBedID.Enabled = false;
                this.txtZyh.Enabled = false;

                this.txtBedID.Text = "";
                this.txtBedID.Tag = "";
                this.txtZyh.Text = "";

                this.m_txtAREAID.Focus();
            }
        }

        private void rdoBed_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoBed.Checked)
            {
                this.txtBedID.Enabled = true;
                this.txtZyh.Enabled = false;

                this.txtBedID.Focus();
                this.txtZyh.Text = "";
                this.lsvBed.Enabled = true;
                this.lsvBed.Items.Clear();
                if (this.lsvBed.Tag != null)
                {
                    DataTable dtReslut = this.lsvBed.Tag as DataTable;
                    if (dtReslut.Rows.Count == 0)
                    {
                        return;
                    }
                    this.lsvBed.BeginUpdate();
                    DataRow dr;
                    for (int i1 = 0; i1 < dtReslut.Rows.Count; i1++)
                    {
                        dr = dtReslut.Rows[i1];
                        ListViewItem item = new ListViewItem();

                        item.Text = "";
                        item.SubItems.Add(dr["code_chr"].ToString());
                        item.SubItems.Add(dr["lastname_vchr"].ToString());
                        item.Checked = false;
                        item.Tag = dr;
                        this.lsvBed.Items.Add(item);
                    }
                    this.lsvBed.EndUpdate();
                    this.lsvBed.Focus();
                }
            }
            else
            {
                this.lsvBed.Items.Clear();
                this.lsvBed.Enabled = false;
            }
        }

        private void rdoZyh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoZyh.Checked)
            {
                this.txtBedID.Enabled = false;
                this.txtZyh.Enabled = true;

                this.txtBedID.Text = "";
                this.txtBedID.Tag = "";
                this.txtZyh.Focus();
            }
        }

        private void m_txtAREAID_ItemSelectedOK(object s, EventArgs e)
        {
            DataTable dtReslut = this.objReport.m_dtGetBed(this.m_txtAREAID.Value);

            this.lsvBed.Tag = dtReslut;
        }
    }
}