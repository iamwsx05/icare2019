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
    /// ctlRegisterStat3
    /// </summary>
    public class ctlRegisterStat3 : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterStat3 Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterStat3)child;
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

            #region 医师
            Viewer.lueDoct.Properties.PopupWidth = 155;
            Viewer.lueDoct.Properties.PopupHeight = 250;
            Viewer.lueDoct.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            Viewer.lueDoct.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.Essential = false;
            Viewer.lueDoct.Properties.IsShowColumnHeaders = true;
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 70);
            Viewer.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 85);
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "编码");
            Viewer.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "名称");
            Viewer.lueDoct.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            Viewer.lueDoct.Properties.IsUseShowColumn = true;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #region xr

            EntitySysReport rptVo = null;
            if (GlobalParm.dicSysParameter.ContainsKey(36))
            {
                using (ProxyCommon proxy = new ProxyCommon())
                {
                    rptVo = proxy.Service.GetReport(Function.Dec(GlobalParm.dicSysParameter[36]));
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

                EntityRegQueryParm queryVo = new EntityRegQueryParm();
                queryVo.startDate = startDate;
                queryVo.endDate = endDate;
                queryVo.deptCode = deptCode;
                queryVo.doctCode = Viewer.lueDoct.Properties.DBValue;

                uiHelper.BeginLoading(Viewer);
                //XRControl xc; //报表上的组件
                //xc = xr.FindControl("txtDeptName", true);
                //if (xc != null) (xc as XRLabel).Text = deptName;
                //xc = xr.FindControl("txtDate", true);
                //if (xc != null) (xc as XRLabel).Text = " " + startDate + " ~ " + endDate;

                List<EntityOpRegBooking> dataSource = null;   // 数据源
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    dataSource = proxy.Service.GetRegBookingInfo2(queryVo);
                    if (dataSource != null && dataSource.Count > 0)
                    {
                        dataSource = dataSource.FindAll(t => t.status == 0);
                    }
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
                xr.Name = "预约清单";
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
