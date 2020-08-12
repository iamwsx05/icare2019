using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.RIS
{
    public partial class frmCardiogramReportrptNew : Form
    {
        public frmCardiogramReportrptNew()
        {
            InitializeComponent();
        }

        weCare.Proxy.ProxyRIS proxy
        {
            get
            {
                return new weCare.Proxy.ProxyRIS();
            }
        }


        void Init()
        {
            this.dteBegin.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.dteEnd.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string beginDate = this.dteBegin.Text;
            string endDate = this.dteEnd.Text;
            string search = string.Empty;//this.txtSearch.Text;
            string deptName = this.ucDept.DeptName;
            DataTable dtbResult = null;
            if (tabCardiogram.SelectedTabPageIndex == 0)
            {
                this.gcReport1.DataSource = GetCardiogramdbt(beginDate, endDate, deptName, search, out dtbResult);
                this.gcReport1.RefreshDataSource();
            }
            else
            {
                this.gcReport2.DataSource = GetDnmCardiogramdbt(beginDate, endDate, deptName, search, out dtbResult);
                this.gcReport2.RefreshDataSource();
            }
        }

        /// <summary>
        /// 动态心电图
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptName"></param>
        /// <param name="strDiagnoses"></param>
        /// <param name="dtbRsult"></param>
        internal List<EntityReport> GetDnmCardiogramdbt(string beginDate, string endDate, string deptName, string strDiagnoses, out DataTable dtbRsult)
        {
            List<EntityReport> data = null;
            dtbRsult = null;

            long lngRes = proxy.Service.m_lngGetDnmCardiogramdbt(beginDate, endDate, deptName, strDiagnoses, out dtbRsult);

            if (lngRes < 0)
                return null;

            if (dtbRsult != null && dtbRsult.Rows.Count > 0)
            {
                data = new List<EntityReport>();
                foreach (DataRow dr in dtbRsult.Rows)
                {
                    EntityReport vo = new EntityReport();
                    vo.reportDate = Function.Datetime(dr["report_dat"]).ToString("yyyy-MM-dd HH:mm:ss");
                    vo.reportNo = dr["report_no_chr"].ToString();
                    vo.patientName = dr["patient_name_vchr"].ToString();
                    vo.sex = dr["sex_chr"].ToString();
                    vo.age = dr["age_flt"].ToString();
                    vo.inpatientNo = dr["inpatient_no_chr"].ToString();
                    vo.paitentNo = dr["patient_no_chr"].ToString();
                    vo.clinicalDiagnose = dr["clinical_diagnose_vchr"].ToString();
                    vo.deptName = dr["summary1_vchr"].ToString();
                    vo.summary2 = dr["summary2_vchr"].ToString();
                    vo.specialFlag = dr["specialflag_int"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }

        /// <summary>
        /// 心电图
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptName"></param>
        /// <param name="strDiagnoses"></param>
        /// <param name="dtbRsult"></param>
        /// <returns></returns>
        internal List<EntityReport> GetCardiogramdbt(string beginDate, string endDate, string deptName, string strDiagnoses, out DataTable dtbRsult)
        {
            List<EntityReport> data = null;
            dtbRsult = null;

            long lngRes = proxy.Service.m_lngGetCardiogramdbt(beginDate, endDate, deptName, strDiagnoses, out dtbRsult);

            if (lngRes < 0)
                return null;

            if(dtbRsult!= null && dtbRsult.Rows.Count > 0)
            {
                data = new List<EntityReport>();
                foreach (DataRow dr in dtbRsult.Rows)
                {
                    EntityReport vo = new EntityReport();
                    vo.reportDate =  Function.Datetime(dr["report_dat"]).ToString("yyyy-MM-dd HH:mm:ss");
                    vo.reportNo = dr["report_no_chr"].ToString();
                    vo.patientName = dr["patient_name_vchr"].ToString();
                    vo.sex = dr["sex_chr"].ToString();
                    vo.age = dr["age_flt"].ToString();
                    vo.inpatientNo = dr["inpatient_no_chr"].ToString();
                    vo.paitentNo = dr["patient_no_chr"].ToString();
                    vo.clinicalDiagnose = dr["clinical_diagnose_vchr"].ToString();
                    vo.deptName = dr["summary1_vchr"].ToString();
                    vo.summary2 = dr["summary2_vchr"].ToString();
                    vo.specialFlag = dr["specialflag_int"].ToString();
                    data.Add(vo);
                }
            }

            return data;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvReport1);
        }

        private void frmCardiogramReportrptNew_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void gvReport1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        private void gvReport2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
    }

    public class EntityReport
    {
        public string reportDate { get; set; }
        public string reportNo { get; set; }
        public string patientName { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public string inpatientNo { get; set; }
        public string paitentNo { get; set; }
        public string clinicalDiagnose { get; set; }
        public string deptName { get; set; }
        public string summary2 { get; set; }
        public string specialFlag { get; set; }
    }

}
