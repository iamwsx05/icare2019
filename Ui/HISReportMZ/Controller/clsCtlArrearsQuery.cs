using System;
using com.digitalwave.GUI_Base;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS.Reports
{
    class clsCtlArrearsQuery : clsController_Base
    { 
        #region ���ô������
        frmArrearsQuery m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmArrearsQuery)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����ҳ���ѯ�ϸ��µ�����
        /// <summary>
        /// ����ҳ���ѯ�ϸ��µ�����
        /// </summary>
        public void m_mthLoad()
        {
            m_objViewer.dwRpt.LibraryList = Application.StartupPath + "\\pb_Invioce.pbl";
            m_objViewer.dwRpt.DataWindowObject = "d_op_treatfirst";
            string strMouthAgoShort = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().AddMonths(-1).ToShortDateString();
            m_objViewer.datStartDate.Value = DateTime.Parse(strMouthAgoShort + " 00:00:00");
            string strDateNowShort = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToShortDateString();
            m_objViewer.datEndDate.Value = DateTime.Parse(strDateNowShort + " 23:59:59");
            // m_mthQueryArrearsPatient();
            if (m_objViewer.m_strQueryType == "�ɷѲ���")
            {
                this.m_objViewer.dgvPatient.Columns["colinvo"].Visible = true;
            }
        }
        #endregion

        #region ����ѡ����ʱ��β�ѯǷ�Ѳ���
        /// <summary>
        /// ����ѡ����ʱ��β�ѯǷ�Ѳ���
        /// </summary>
        public void m_mthQueryArrearsPatient()
        {
            this.m_objViewer.dgvPatient.Rows.Clear();
            string strStartDate = m_objViewer.datStartDate.Value.ToString();
            string strEndDate = m_objViewer.datEndDate.Value.ToString();
            DataTable dtResult = new DataTable();
            long lngRes = -1;
            if (m_objViewer.m_strQueryType == "0")
            {
                lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngQueryArrearsPatientByDate(strStartDate, strEndDate, out dtResult, false);
            }
            else if (m_objViewer.m_strQueryType == "1")
            {
                lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngQueryPayedPatientByDate(strStartDate, strEndDate, out dtResult);
            }
            else if (m_objViewer.m_strQueryType == "2")
            {
                lngRes = (new weCare.Proxy.ProxyReport()).Service.m_lngQueryArrearsPatientByDate(strStartDate, strEndDate, out dtResult, true);
            }
            if (lngRes > 0 && dtResult.Rows.Count > 0)
            {
                int row = 0;

                foreach (DataRow dr in dtResult.Rows)
                {
                    int i = this.m_objViewer.dgvPatient.Rows.Add();
                    this.m_objViewer.dgvPatient["colname", i].Value = dr["patientname_chr"].ToString();
                    this.m_objViewer.dgvPatient["colcard", i].Value = dr["patientcardid_chr"].ToString();
                    this.m_objViewer.dgvPatient["colsex", i].Value = dr["sex_chr"].ToString();
                    this.m_objViewer.dgvPatient["colrepice", i].Value = dr["outpatrecipeid_chr"].ToString();
                    this.m_objViewer.dgvPatient["colybcard", i].Value = dr["idcard_chr"].ToString();
                    this.m_objViewer.dgvPatient["coldept", i].Value = dr["deptname_chr"].ToString();
                    this.m_objViewer.dgvPatient["coltel", i].Value = dr["homephone_vchr"].ToString();
                    this.m_objViewer.dgvPatient["colfee", i].Value = dr["totalsum_mny"].ToString();
                    this.m_objViewer.dgvPatient["colinvo", i].Value = dr["invoiceno_vchr"].ToString();
                    //����
                    row = this.m_objViewer.dwRpt.InsertRow();
                    this.m_objViewer.dwRpt.SetItemString(row, "name", dr["patientname_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "cardno", dr["patientcardid_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "sex", dr["sex_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "idcardno", dr["idcard_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "recipeno", dr["outpatrecipeid_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "deptid", dr["deptname_chr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "tel", dr["homephone_vchr"].ToString());
                    this.m_objViewer.dwRpt.SetItemString(row, "totalmny", dr["totalsum_mny"].ToString());
                }
                int k = this.m_objViewer.dgvPatient.Rows.Add();
                this.m_objViewer.dgvPatient["colybcard", k].Value = "��������";
                this.m_objViewer.dgvPatient["coldept", k].Value = dtResult.Rows.Count.ToString() + "��";
                this.m_objViewer.dgvPatient["coltel", k].Value = "�ܷ��ã�";
                this.m_objViewer.dgvPatient["colfee", k].Value = dtResult.Compute("sum(totalsum_mny)", "").ToString() + "Ԫ";
                row = this.m_objViewer.dwRpt.InsertRow();
                this.m_objViewer.dwRpt.SetItemString(row, "idcardno", "��������");
                this.m_objViewer.dwRpt.SetItemString(row, "deptid", dtResult.Rows.Count.ToString() + "��");
                this.m_objViewer.dwRpt.SetItemString(row, "tel", "�ܷ��ã�");
                this.m_objViewer.dwRpt.SetItemString(row, "totalmny", dtResult.Compute("sum(totalsum_mny)", "").ToString() + "Ԫ");
                if (m_objViewer.m_strQueryType == "1")
                {
                    this.m_objViewer.dwRpt.Modify("t_title.text='�����ƺ����ɷѱ���'");
                }
                else
                {
                    this.m_objViewer.dwRpt.Modify("t_title.text='�����ƺ����Ƿ�ѱ���'");
                }
                this.m_objViewer.dwRpt.Modify("t_datebegin.text='" + strStartDate + "'");
                this.m_objViewer.dwRpt.Modify("t_dateend.text='" + strEndDate + "'");
                this.m_objViewer.dwRpt.Modify("datawindow.print.preview=yes datawindow.print.preview.rulers=yes");
            }
            else
            {
                int k = this.m_objViewer.dgvPatient.Rows.Add();
                this.m_objViewer.dgvPatient["colybcard", k].Value = "��������";
                this.m_objViewer.dgvPatient["coldept", k].Value = "0��";
                this.m_objViewer.dgvPatient["coltel", k].Value = "�ܷ��ã�";
                this.m_objViewer.dgvPatient["colfee", k].Value = "0Ԫ";
            }
        }
        #endregion
    }
}
