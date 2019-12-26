using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
    public class clsController_WorkStatstic : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsController_WorkStatstic()
        {
            m_objManage = new clsDomainController_WorkStatstic();
        }

        #region
        /// <summary>
        /// �����
        /// </summary>
        internal int strQuery;
        private string strEmname;
        private frmWorkStatstic m_objViewer;
        private DateTime dtDateFrom;
        private DateTime dtDateTo;
        DataTable dtbEmp = null;
        DataView drName;
        private clsDomainController_WorkStatstic m_objManage;
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmWorkStatstic)frmMDI_Child_Base_in;
        }
        #endregion
        #region
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void m_mthInti()
        {
            m_objViewer.cboCondition.Items.AddRange(new object[] { "��������", "����ҽ��", "������Ա", "�����" });
            m_mthGetDept();
        }
        #endregion
        #region
        /// <summary>
        /// ��ȡ����ҽ���������
        /// </summary>
        public void m_mthGetEmployee()
        {
            //long lngRes = m_objManage.lngGetEmployee(out dtbEmp);
            //drName = new DataView(dtbEmp);
            //m_objViewer.ddgvName.DataSource = drName;
            //if (lngRes < 0)
            //{
            //    MessageBox.Show("����ʧ��...");
            //}
        }

        public void m_mthSetEmployee()
        {
            string str = m_objViewer.txtCondition.Text.ToString();
            if (str == "" || str == null || str == "'") { return; }
            drName.RowFilter = "pycode_chr like '%" + str + "%' or empno_chr like '%" + str + "%'";
           
        }
        #endregion
        #region
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public void m_mthGetDept()
        {
            DataTable dtbDept = null;
            long lngRes = m_objManage.lngGetDept(out dtbDept);
            m_objViewer.cboDept.Visible = true;
            m_objViewer.cboDept.Enabled = true;
            m_objViewer.cboDept.DataSource = dtbDept;
            m_objViewer.cboDept.DisplayMember = "deptname_vchr";
            m_objViewer.cboDept.ValueMember = "deptid_chr";
            //DataRow drtt=dtbDept.Rows[m_objViewer.cboDept.SelectedIndex];
            //strEmpno =drtt["deptid_chr"].ToString();
            //strEmname = drtt["deptname_vchr"].ToString();
        }
        #endregion
        #region
        /// <summary>
        /// ������ͳ��
        /// </summary>
        public void m_mthGetWorkStatstic(string strEmpno)
        {
            DataTable dtbResult;
            string strCondition = strEmpno;
            int iQueryType = 0;
            if (m_objViewer.m_rdbAppDat.Checked)
            {
                iQueryType = 1;
            }

            if (strQuery == 1 || strQuery == 2) 
            {
                strCondition = strEmpno;
                DateTime dtDateFrom = DateTime.Parse(m_objViewer.m_dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"));
                DateTime dtDateTo = DateTime.Parse(m_objViewer.m_dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"));
                long lngRes = m_objManage.lngGetWorkStatstic(iQueryType, dtDateFrom, dtDateTo, strQuery, strCondition, out dtbResult);
                
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    this.FillDW(dtbResult);
                }
            }
            if (strQuery == 0) 
            {
                strCondition = m_objViewer.cboDept.SelectedValue.ToString();

                DateTime dtDateFrom = DateTime.Parse(m_objViewer.m_dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"));
                DateTime dtDateTo = DateTime.Parse(m_objViewer.m_dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"));
                long lngRes = m_objManage.lngGetWorkStatstic(iQueryType, dtDateFrom, dtDateTo, strQuery, strCondition, out dtbResult);
                if (lngRes > 0 && dtbResult.Rows.Count==0)
                {
                    MessageBox.Show("û�з������������ݣ�");
                    return;
                }
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    this.FillDW(dtbResult);
                }
            }
            if (strQuery == 3)
            {
                strCondition = "0000044";
                dtDateFrom = DateTime.Parse(m_objViewer.m_dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"));
                dtDateTo = DateTime.Parse(m_objViewer.m_dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"));
                long lngRes = m_objManage.lngGetWorkStatstic(iQueryType, dtDateFrom, dtDateTo, strQuery, strCondition, out dtbResult);

                if (lngRes > 0 && dtbResult.Rows.Count == 0)
                {
                    MessageBox.Show("û�з������������ݣ�");
                    return;
                }
                if (lngRes > 0 && dtbResult.Rows.Count > 0)
                {
                    this.FillDW(dtbResult);
                }
            }
        }
        #endregion
        #region
        /// <summary>
        /// ���DW
        /// </summary>
        /// <param name="intall"></param>
        /// <param name="dtRsult"></param>
        public void FillDW(DataTable dtbResult)
        {
            try 
            {
                DataRow dtFr;
                int intall = 0;
                int inttemp;
                int row = 0;
                m_objViewer.dwResult.SetRedrawOff();

                for (int intI = 0; intI < dtbResult.Rows.Count; intI++)
                {
                    row = this.m_objViewer.dwResult.InsertRow(0);
                    dtFr = dtbResult.Rows[intI];
                    m_objViewer.dwResult.SetItemString(row, "���", dtFr["check_category_desc_vchr"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "��Ŀ", dtFr["apply_unit_name_vchr"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "���", dtFr["itemcount"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "�˴�", dtFr["appcount"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "�����", dtFr["totalitem"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "����", dtFr["price_num"].ToString());
                    m_objViewer.dwResult.SetItemString(row, "�ܺ�", dtFr["totalmoney"].ToString());
                    //intall = intall + Convert.ToInt32(dtFr["totalmoney"]);
                    //int.TryParse(dtFr["totalmoney"].ToString(), out inttemp);
                    //intall = intall + inttemp;

                    m_objViewer.dwResult.Modify("t_datefrom.text = '" + m_objViewer.m_dtpFromDate.Value.ToString("yyyy-MM-dd") + "'");
                    m_objViewer.dwResult.Modify("t_dateto.text = '" + m_objViewer.m_dtpToDate.Value.ToString("yyyy-MM-dd") + "'");

                    if (strQuery == 0)
                    {
                        m_objViewer.dwResult.Modify("t_4.Text ='" + "�������ң�" + "'");
                        m_objViewer.dwResult.Modify("t_condition.text = '" + m_objViewer.cboDept.Text + "'");
                    }
                    if (strQuery == 1)
                    {
                        m_objViewer.dwResult.Modify("t_4.Text ='" + "����ҽ����" + "'");
                        m_objViewer.dwResult.Modify("t_condition.text = '" + m_objViewer.txtCondition.Text + "'");
                    }
                    if (strQuery == 2)
                    {
                        m_objViewer.dwResult.Modify("t_4.Text ='" + "������Ա��" + "'");
                        m_objViewer.dwResult.Modify("t_condition.text = '" + m_objViewer.txtCondition.Text + "'");
                    }
                    if (strQuery == 3)
                    {
                        m_objViewer.dwResult.Modify("t_4.Text ='" + "����ƣ�" + "'");
                        m_objViewer.dwResult.Modify("t_condition.text = '�����'");
                    }
                }

                decimal decToatal = 0;
                decimal.TryParse(dtbResult.Compute("Sum(itemcount)", "").ToString(), out decToatal);
                m_objViewer.dwResult.Modify("t_items.text = '" + decToatal.ToString() + "'");
                decimal.TryParse(dtbResult.Compute("Sum(appcount)", "").ToString(), out decToatal);
                m_objViewer.dwResult.Modify("t_count.text = '" + decToatal.ToString() + "'");
                decimal.TryParse(dtbResult.Compute("Sum(totalitem)", "").ToString(), out decToatal);
                m_objViewer.dwResult.Modify("t_totalitems.text = '" + decToatal.ToString() + "'");

                decimal.TryParse(dtbResult.Compute("Sum(totalmoney)", "").ToString(), out decToatal);
                m_objViewer.dwResult.Modify("t_all.text = '" + decToatal.ToString("###,##0.00") + "'");

                m_objViewer.dwResult.SetRedrawOn();
                m_objViewer.dwResult.Refresh();
            }
            catch(Exception objEx)
            {
                MessageBox.Show(objEx.Message);
            }
        }
        #endregion
    }
}
