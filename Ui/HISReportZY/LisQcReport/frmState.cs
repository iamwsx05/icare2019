using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmState : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public frmState()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        void Query()
        {
            string dteStart = this.dteStart.Text;
            string dteEnd = this.dteEnd.Text;
            DataTable dtbResult = null;
            List<EntitySate> data = new List<EntitySate>();
            weCare.Proxy.ProxyReport svc = new weCare.Proxy.ProxyReport();
            long lns =  svc.Service.GetSampleArenaStat(out dtbResult, dteStart, dteEnd);

            if(dtbResult != null && dtbResult.Rows.Count > 0)
            {
                EntitySate vo = null; 
                foreach(DataRow dr in dtbResult.Rows)
                {
                    vo = new EntitySate();
                    vo.application = dr["application"].ToString();
                    vo.applyDate = dr["applyDate"].ToString();
                    vo.acceptDate = dr["acceptDate"].ToString();
                    vo.age = dr["age"].ToString();
                    vo.barcode = dr["barcode"].ToString();
                    vo.pattype = dr["pattype"].ToString();
                    vo.chcekContent = dr["chcekContent"].ToString();
                    vo.name = dr["name"].ToString();
                    vo.cardNo = dr["cardNo"].ToString();
                    if (string.IsNullOrEmpty(vo.cardNo.Trim()))
                        vo.cardNo = dr["patInNo"].ToString();
                    vo.depteName = dr["depteName"].ToString();
                    vo.summary = dr["summary"].ToString();
                    vo.checktor = dr["checktor"].ToString();
                    vo.checkdate = dr["checkdate"].ToString();
                    vo.result = dr["result"].ToString();

                    #region  全部
                    if (rdgSelect.SelectedIndex == 0)
                    {
                        if (data.Exists(r => r.application == vo.application))
                            continue;
                    }
                    #endregion

                    #region  复检
                    if (rdgSelect.SelectedIndex == 1)
                    {
                        if (vo.result.Contains("\\") && string.IsNullOrEmpty(vo.summary.Trim()))
                            continue;
                    }
                    #endregion

                    if (data.Exists(r => r.application == vo.application))
                        continue;
                    data.Add(vo);

                }
            }

            this.gcStat.DataSource = data;
            this.gcStat.RefreshDataSource();
        }

        private void gvStat_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Info.IsRowIndicator)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Info.DisplayText = Convert.ToInt32(e.RowHandle + 1).ToString();
            }
        }

        private void frmState_Load(object sender, EventArgs e)
        {
            this.dteStart.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
            this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvStat);
        }
    }


    public class EntitySate
    {
        public string application { get; set; }
        public string applyDate { get; set; }
        public string acceptDate { get; set; }
        public string age { get; set; }
        public string barcode { get; set; }
        public string pattype { get; set; }
        public string chcekContent { get; set; }
        public string name { get; set; }
        public string cardNo { get; set; }
        public string depteName { get; set; }
        public string summary { get; set; }
        public string checktor { get; set; }
        public string checkdate { get; set; }
        public string result { get; set; }
    }
}
