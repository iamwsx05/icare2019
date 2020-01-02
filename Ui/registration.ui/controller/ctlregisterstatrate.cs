using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraReports.UI;
using Registration.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// ctlRegisterStatRate
    /// </summary>
    public class ctlRegisterStatRate : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterStatRate Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterStatRate)child;
        }
        #endregion

        #region 属性.变量

        XtraReport xr = null;

        #endregion
        
        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region xr

            EntitySysReport rptVo = null;
            if (GlobalParm.dicSysParameter.ContainsKey(37))
            {
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    rptVo = proxy.Service.GetReport(Function.Dec(GlobalParm.dicSysParameter[37]));
                }
            }
            xr = new XtraReport();
            if (rptVo != null)
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(rptVo.rptFile, 0, rptVo.rptFile.Length);
                xr.LoadLayout(ms);
            }
            Viewer.ucPrintControl.PrintingSystem = xr.PrintingSystem;
            xr.CreateDocument();

            #endregion

            DateTime dtmNow = DateTime.Now;
            Viewer.dteStart.Text = dtmNow.ToString("yyyy-MM-dd");
            Viewer.dteEnd.Text = (dtmNow.AddDays(1 - dtmNow.Day)).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        }
        #endregion

        #region Stat
        /// <summary>
        /// Stat
        /// </summary>
        internal void Stat()
        {
            try
            {
                string startDate = Viewer.dteStart.Text;
                string endDate = Viewer.dteEnd.Text;
                if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    DialogBox.Msg("请选择预约日期。");
                    return;
                }
                if (Function.Datetime(startDate) > Function.Datetime(endDate))
                {
                    DialogBox.Msg("预约开始日期不能大于结束日期。");
                    return;
                }
                
                uiHelper.BeginLoading(Viewer);
                XRControl xc; //报表上的组件
                xc = xr.FindControl("txtDate", true);
                if (xc != null) (xc as XRLabel).Text = " " + startDate + " ~ " + endDate;

                List<EntityBookingRate> dataSource = null;   // 数据源
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    dataSource = proxy.Service.GetBookingRate(startDate, endDate);
                }
                xr.DataSource = dataSource;
                xr.CreateDocument();
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region Export
        /// <summary>
        /// Export
        /// </summary>
        internal void Export()
        {
            if (xr != null && xr.DataSource != null)
            {
                xr.Name = "预约挂号工作量报表";
                uiHelper.Export(xr);
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        internal void Print()
        {
            if (xr != null && xr.DataSource != null)// && (xr.DataSource as List<EntitySchedulingRpt1>).Count > 0)
            {
                xr.PrintDialog();
            }
        }
        #endregion

        #endregion

    }
}
