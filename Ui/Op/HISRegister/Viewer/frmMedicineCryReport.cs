using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmMedicineCryReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmMedicineCryReport()
        {
            InitializeComponent();
            m_lsv.Items[0].Checked = true;
        }
        private string[] m_strMedStoreIdArr = null;
        private DataTable m_DataTable = null;
        private clsDcl_MedicineSource m_objSvc = new clsDcl_MedicineSource();
        //private CrystalDecisions.CrystalReports.Engine.ReportDocument m_rptRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument ();

        #region ��ʾ����
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_strMedStoreId">ҩ���ɣ�</param>
        public void m_mthShow(string p_strMedStoreId)
        {
            #region ��ʾ����
            m_strMedStoreIdArr = p_strMedStoreId.Trim().Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            if (m_strMedStoreIdArr != null)
            {
                string strWhere = " where ";

                if (m_strMedStoreIdArr.Length > 0)
                {
                    for (int i = 0; i < m_strMedStoreIdArr.Length; i++)
                    {
                        strWhere += " MEDSTOREID_CHR='" + m_strMedStoreIdArr[i] + "' OR ";
                    }
                    strWhere = strWhere.Substring(0, strWhere.Length - 3);
                    long lngRes = m_objSvc.m_lngGetMedStoreData(strWhere, out m_DataTable);
                    if (lngRes > 0)
                    {
                        ListViewItem li = null;
                        for (int j = 0; j < m_DataTable.Rows.Count; j++)
                        {
                            li = new ListViewItem();
                            li.Text = m_DataTable.Rows[j]["MEDSTORENAME_VCHR"].ToString().Trim();
                            li.SubItems.Add(m_DataTable.Rows[j]["MEDSTOREID_CHR"].ToString().Trim());
                            m_lsv.Items.Add(li);
                        }
                    }
                    m_DataTable = null;
                    this.Show();
                    
                }
                else
                {
                    MessageBox.Show("��������ȷ");
                    return;
                }
            }
            else
            {
                MessageBox.Show("��������ȷ");
                return;
            }
            #endregion
        }
        #endregion

        private void frmMedicineCryReport_Load(object sender, EventArgs e)
        {
            m_cboRptSel.SelectedIndex = 0;
            m_cboState.SelectedIndex = 0;
            m_cboAsc.SelectedIndex = 0;
            
            long lngRes =m_objSvc.m_lngGetItemData(out m_DataTable);
            if (lngRes >0)
            {
                m_ctlTBFindItem.m_GetDataTable = m_DataTable;
                m_DataTable.Columns[0].ColumnName = "��Ŀ����";
                m_DataTable.Columns[1].ColumnName = "��  Ŀ  ��  ��";
                m_DataTable.Columns[2].ColumnName = "��Ŀƴ������";
                m_DataTable.Columns[3].ColumnName = "��Ŀ��ʼ���";
                m_DataTable.Columns[4].ColumnName = "ITEMID_CHR";
            }
        }

        private void collapsibleSplitter1_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true ;
            }
        }

        private void m_lsv_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            #region check
            if (e.Item.Text.Trim() == "ȫ��")
            {
                if (m_lsv.Items[0].Checked)
                {
                    for (int i = 0; i < m_lsv.Items.Count; i++)
                    {
                        if (m_lsv.Items[i] != null)
                        {
                            if (i != 0)
                            {
                                m_lsv.Items[i].Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < m_lsv.Items.Count; i++)
                    {
                        if (m_lsv.Items[i] != null)
                            if (i != 0)
                            {
                                m_lsv.Items[i].Checked = false;
                            }
                    }
                }
            }
            #endregion
        }

        private void m_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_btnFind_Click(object sender, EventArgs e)
        {
            if (m_lsv.Items.Count == 1)
            {
                MessageBox.Show("û�����ú�ҩ������,����ϵ����Ա", "����");
                return;
            }

            #region ȡ���������
            int intSelCount = 0;
            string strDep = "";
            int index = 0;
            for (int i = 1; i < m_lsv.Items.Count; i++)
            {
                if (m_lsv.Items[i].Checked )
                {
                    intSelCount++;                    
                }
            }
            string[] strMedStoreIDArr = null;

            if (m_lsv.Items[0].Checked)
            {
                strMedStoreIDArr = new string[m_lsv.Items.Count - 1];
                for (int i = 1; i < m_lsv.Items.Count; i++)
                {
                    strMedStoreIDArr[i - 1] = m_lsv.Items[i ].SubItems[1].Text;
                    strDep += m_lsv.Items[i ].SubItems[0].Text + " ";
                }
            }
            else
            {
                strMedStoreIDArr = new string[intSelCount];
                for (int i = 1; i < m_lsv.Items.Count; i++)
                {
                    if (m_lsv.Items[i].Checked)
                    {
                        strMedStoreIDArr[index++] = m_lsv.Items[i].SubItems[1].Text;
                        strDep += m_lsv.Items[i].SubItems[0].Text + " ";
                    }
                }
            }

            if (strDep == "")
            {
                MessageBox.Show("��ѡ��ͳ��ҩ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //δ��1
            //����2
            //�ѷ�3
            string[] strStatusArr = null;
            if (m_cboState.Text == "ȫ��")
                strStatusArr = new string[] { "1", "2", "3" };
            else if (m_cboState.Text == "δ��")
            {
                strStatusArr = new string[] { "1" };
            }
            else if (m_cboState.Text == "����")
            {
                strStatusArr = new string[] { "2" };
            }
            else if (m_cboState.Text == "�ѷ�")
            {
                strStatusArr = new string[] { "3" };
            }
            #endregion

            #region order by
            string strASC = " ASC ";
            if (m_cboAsc.Text!="����")
            {
                strASC = " DESC ";
            }
            string strOrder = " " + m_strGetColName(m_cbocol.Text) + " " + strASC;
            #endregion 
            string strRPT = "";
            long lngRes = 0;
            switch (m_cboRptSel.SelectedIndex)
            {
                case  0:
                case  2:

                    #region ���ñ���
                    strRPT = "cryMedSendDetails.rpt";
                    //m_rptRpt.Load("Report\\" + strRPT);

                    lngRes = m_objSvc.m_lngGetMedSendItemData(m_dtmStart.Value.ToShortDateString(),
                        m_dtmEnd.Value.ToShortDateString(), strMedStoreIDArr, strStatusArr, strOrder, m_ctlTBFindItem.txtValuse.Trim(), out m_DataTable);
                    //DataRow[] drArr = m_DataTable.Select(" OUTPATRECIPEID_CHR is not null ", strOrder);
                    //m_DataTable.Rows.Clear();
                    //for (int i = 0; i < drArr.Length; i++)
                    //{
                    //    m_DataTable.Rows.Add(drArr[i]);
                    //}
                    if (m_cboRptSel.SelectedIndex == 2)
                    {
                        //TextObject m_sendman = m_rptRpt.ReportDefinition.ReportObjects["m_sendman"] as TextObject;
                        //m_sendman.Text = "������";
                        //TextObject m_setman = m_rptRpt.ReportDefinition.ReportObjects["m_setman"] as TextObject;
                        //m_setman.Text = "������";
                        //TextObject m_title = m_rptRpt.ReportDefinition.ReportObjects["m_title"] as TextObject;
                        //m_title.Text = "�����ۺϲ��Ϸ����嵥(��ϸ)";
                    }
                    else if (m_cboRptSel.SelectedIndex == 0)
                    {
                        //TextObject m_sendman = m_rptRpt.ReportDefinition.ReportObjects["m_sendman"] as TextObject;
                        //m_sendman.Text = "��ҩ��";
                        //TextObject m_setman = m_rptRpt.ReportDefinition.ReportObjects["m_setman"] as TextObject;
                        //m_setman.Text = "��ҩ��";
                        //TextObject m_title = m_rptRpt.ReportDefinition.ReportObjects["m_title"] as TextObject;
                        //m_title.Text = "����ҩ��ҩƷ�����嵥(��ϸ)";
                    }

                    //TextObject m_PrintDate = m_rptRpt.ReportDefinition.ReportObjects["m_PrintDate"] as TextObject;
                    //m_PrintDate.Text = System.DateTime.Now.ToString();
                    //TextObject m_state = m_rptRpt.ReportDefinition.ReportObjects["m_state"] as TextObject;
                    //m_state.Text = m_cboState.Text;
                    //TextObject m_dept = m_rptRpt.ReportDefinition.ReportObjects["m_dept"] as TextObject;
                    //m_dept.Text = strDep;
                    //TextObject m_statis = m_rptRpt.ReportDefinition.ReportObjects["m_statis"] as TextObject;
                    //m_statis.Text = m_dtmStart.Value.ToShortDateString() + " ~ " + m_dtmEnd.Value.ToShortDateString();
                    double dblTotal = 0;
                    string str = "";
                    for (int i = 0; i < m_DataTable.Rows.Count; i++)
                    {
                        str = m_DataTable.Rows[i]["tolprice_mny"].ToString();
                        if (str != "")
                        {
                            dblTotal += double.Parse(str);
                        }
                    }
                    //TextObject m_total = m_rptRpt.ReportDefinition.ReportObjects["m_total"] as TextObject;
                    //m_total.Text = dblTotal.ToString();
                    #endregion

                    if (lngRes > 0)
                    {

                        //m_rptRpt.SetDataSource(m_DataTable);
                        //m_rptRpt.Refresh();
                        //crystalReportViewer1.ReportSource = m_rptRpt;
                    }
                    else
                    {
                        MessageBox.Show("�������ݿ����.");
                    }
                    break;
                case 1:
                case 3:
                    #region ���ñ���
                    //strRPT = "cryMedSum.rpt";
                    //m_rptRpt.Load("Report\\" + strRPT);

                    //lngRes = m_objSvc.m_lngGetMedSendItemData(m_dtmStart.Value.ToShortDateString(),
                    //    m_dtmEnd.Value.ToShortDateString(), strMedStoreIDArr, strStatusArr, strOrder, m_ctlTBFindItem.txtValuse.Trim(), out m_DataTable);

                    //TextObject m_title2 = m_rptRpt.ReportDefinition.ReportObjects["m_title"] as TextObject;
                    //if (m_cboRptSel.SelectedIndex == 1)
                    //{
                    //    m_title2.Text = "����ҩ��ҩƷ�����嵥�����ܣ�";
                    //}
                    //else
                    //{
                    //    m_title2.Text = "�����ۺϲ��Ϸ����嵥�����ܣ�";
                    //}
                   

                    //TextObject m_PrintDate2 = m_rptRpt.ReportDefinition.ReportObjects["m_PrintDate"] as TextObject;
                    //m_PrintDate2.Text = System.DateTime.Now.ToString();
                    //TextObject m_state2 = m_rptRpt.ReportDefinition.ReportObjects["m_state"] as TextObject;
                    //m_state2.Text = m_cboState.Text;
                    //TextObject m_dept2 = m_rptRpt.ReportDefinition.ReportObjects["m_dept"] as TextObject;
                    //m_dept2.Text = strDep;
                    //TextObject m_statis2 = m_rptRpt.ReportDefinition.ReportObjects["m_statis"] as TextObject;
                    //m_statis2.Text = m_dtmStart.Value.ToShortDateString() + " ~ " + m_dtmEnd.Value.ToShortDateString();
                    #endregion

                    if (lngRes>0)
                    {
                        DataTable dtTemp = m_DataTable.Clone();
                        DataRow dr = null;
                        DataRow[] drarr = null;
                        string itemid = "";
                        int indexHas = -1;
                        bool blnHas = false;
                        if (m_DataTable != null)
                        {

                            for (int i = 0; i < m_DataTable.Rows.Count; i++)
                            {
                                blnHas = false;
                                itemid = m_DataTable.Rows[i]["strTemp1"].ToString().Trim();

                                for (int i2 = 0; i2 < dtTemp.Rows.Count; i2++)
                                {
                                    if (itemid == dtTemp.Rows[i2]["strTemp1"].ToString().Trim())
                                    {
                                        blnHas = true;
                                        indexHas = i2;
                                        break;
                                    }
                                }
                                if (blnHas)
                                {
                                   
                                    dtTemp.Rows[indexHas]["qty_dec"] = Math.Ceiling(double.Parse(dtTemp.Rows[indexHas]["qty_dec"].ToString().Trim())) + Math.Ceiling(double.Parse(m_DataTable.Rows[i]["qty_dec"].ToString().Trim()));
                                    dtTemp.Rows[indexHas]["tolprice_mny"] = Convert.ToDouble(dtTemp.Rows[indexHas]["tolprice_mny"].ToString().Trim()) + Convert.ToDouble(m_DataTable.Rows[i]["tolprice_mny"].ToString().Trim());
                                }
                                else
                                {
                                    dr = dtTemp.NewRow();
                                    for (int j = 0; j < dtTemp.Columns.Count; j++)
                                    {
                                        dr[j] = m_DataTable.Rows[i][j];
                                    }
                                    dtTemp.Rows.Add(dr);
                                }                              
                            }
                        }
                        drarr = dtTemp.Select("qty_dec >-100", strOrder);
                        DataTable dtsource = dtTemp.Clone();
                        for (int i3 = 0; i3 < drarr.Length; i3++)
                        {
                            dr = dtsource.NewRow();
                            for (int j = 0; j < dtsource.Columns.Count; j++)
                            {
                                dr[j] = drarr[i3][j];
                            }
                            dtsource.Rows.Add(dr);
                        }
                        //m_rptRpt.SetDataSource(dtsource);
                        //m_rptRpt.Refresh();
                        //crystalReportViewer1.ReportSource = m_rptRpt;
                    }
                    break;
                default:
                    break;
            }

            
        }

        private void m_cboRptSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region col
            m_cbocol.Items.Clear();
            switch (m_cboRptSel.SelectedIndex)
            {
                case 0:
                    m_cbocol.Items.Add("��������");
                    m_cbocol.Items.Add("��������");
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("���");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("��λ");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("���ϼ�");
                    m_cbocol.Items.Add("����ҽ��");
                    m_cbocol.Items.Add("��ҩ��");
                    m_cbocol.Items.Add("��ҩ��");
                    break;
                case 1:
                case 3:
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("���");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("��λ");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("���ϼ�");
                    break;
                case 2:
                    m_cbocol.Items.Add("��������");
                    m_cbocol.Items.Add("��������");
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("��Ŀ����");
                    m_cbocol.Items.Add("���");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("��λ");
                    m_cbocol.Items.Add("����");
                    m_cbocol.Items.Add("���ϼ�");
                    m_cbocol.Items.Add("����ҽ��");
                    m_cbocol.Items.Add("������");
                    m_cbocol.Items.Add("������");
                    break;

                default:
                    break;
            }
            m_cbocol.SelectedIndex =0;
            #endregion
        }
        #region ȡ�������ֶ�
        string m_strGetColName(string p_strCol)
        {
            string str = "";
            switch (p_strCol)
            {
                    case "��������":
                        str = " recorddate_dat ";
                    break;
                    case "��������":
                        str = "patientname";
                        break;
                    case "��Ŀ����": 
                        str = " itemcode_vchr ";

                        break;
                    case "��Ŀ����":
                        str = " itemname_vchr ";
                        break;
                    case "���":
                        str = " itemspec_vchr ";
                        break;
                    case "����":
                        str = " qty_dec ";
                        break;
                    case "��λ":
                        str = " dosageunit_chr ";
                        break;
                    case "����":
                        str = " unitprice_mny ";
                        break;
                    case "���ϼ�":
                        str = " tolprice_mny ";
                        break;
                    case "����ҽ��":
                        str = " lastname_vchr ";
                        break;
                    case "��ҩ��":
                    case "������":
                        str = " treatemp_name ";
                        break;
                    case "��ҩ��":
                    case "������":
                        str = " give_name ";
                        break;
                default:
                    break;
            }
            return str;
        }
        #endregion
    }
}