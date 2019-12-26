using Common.Controls;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;

namespace Registration.Ui
{
    /// <summary>
    /// ctlRegisterStatStopAdmi
    /// </summary>
    public class ctlRegisterStatStopAdmi : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterStatStopAdmi Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterStatStopAdmi)child;
        }
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

            Viewer.dteStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Viewer.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                uiHelper.BeginLoading(Viewer);
                EntityRegQueryParm queryVo = new EntityRegQueryParm();

                queryVo.startDate = Viewer.dteStart.Text;
                queryVo.endDate = Viewer.dteEnd.Text;
                if ((!string.IsNullOrEmpty(queryVo.startDate) && !string.IsNullOrEmpty(queryVo.endDate)) &&
                    (Convert.ToDateTime(queryVo.startDate) > Convert.ToDateTime(queryVo.endDate)))
                {
                    DialogBox.Msg("查询开始日期不能大于结束日期,请重新输入。");
                    Viewer.dteStart.Focus();
                    return;
                }
                if (Viewer.lueDept.Text.Trim() == string.Empty)
                    queryVo.deptCode = string.Empty;
                else
                    queryVo.deptCode = Viewer.lueDept.Properties.DBValue;
                if (Viewer.lueDoct.Text.Trim() == string.Empty)
                    queryVo.doctCode = string.Empty;
                else
                    queryVo.doctCode = Viewer.lueDoct.Properties.DBValue;

                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    Viewer.gcNo.DataSource = proxy.Service.GetRegStopAdmi(queryVo);
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #endregion
    }
}
