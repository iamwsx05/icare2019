using Common.Controls;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;
using DevExpress.XtraReports.UI;
using System.Diagnostics;

namespace Registration.Ui
{
    /// <summary>
    /// ctlRegisterStat2
    /// </summary>
    public class ctlRegisterStat2 : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterStat2 Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterStat2)child;
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
            #region 科室
            Viewer.lueDept.Properties.PopupWidth = 160;
            Viewer.lueDept.Properties.PopupHeight = 300;
            Viewer.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            Viewer.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            Viewer.lueDept.Properties.Essential = false;
            Viewer.lueDept.Properties.IsShowColumnHeaders = true;
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            Viewer.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 100);
            Viewer.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptCode, "编码");
            Viewer.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptName, "名称");
            Viewer.lueDept.Properties.ShowColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName;
            Viewer.lueDept.Properties.IsUseShowColumn = true;
            Viewer.lueDept.Properties.FilterColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName + "|" + EntityCodeDepartment.Columns.pyCode + "|" + EntityCodeDepartment.Columns.wbCode;
            if (GlobalDic.DataSourceDepartment != null && GlobalDic.DataSourceDepartment.Count > 0) Viewer.lueDept.Properties.DataSource = GlobalDic.DataSourceDepartment.ToArray();
            Viewer.lueDept.Properties.SetSize();
            #endregion

            #region xr

            EntitySysReport rptVo = null;
            if (GlobalParm.dicSysParameter.ContainsKey(35))
            {
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    rptVo = proxy.Service.GetReport(Function.Dec(GlobalParm.dicSysParameter[35]));
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

            Viewer.dteStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Viewer.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Viewer.lueDept.Properties.ReadOnly = true;
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
                string deptCode = string.Empty;
                string deptName = "全院";
                if (Viewer.rdoType.SelectedIndex == 1)
                {
                    deptCode = Viewer.lueDept.Properties.DBValue;
                    deptName = Viewer.lueDept.Text;
                }

                uiHelper.BeginLoading(Viewer);
                XRControl xc; //报表上的组件
                xc = xr.FindControl("txtDeptName", true);
                if (xc != null) (xc as XRLabel).Text = deptName;
                xc = xr.FindControl("txtDate", true);
                if (xc != null) (xc as XRLabel).Text = " " + startDate + " ~ " + endDate;

                List<EntitySchedulingRpt1> dataSource = null;   // 数据源
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    dataSource = proxy.Service.GetSchedulingRpt1(startDate, endDate, deptCode);
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
