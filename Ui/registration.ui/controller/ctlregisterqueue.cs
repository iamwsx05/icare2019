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
    /// 挂号接诊队列
    /// </summary>
    public class ctlRegisterQueue : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterQueue Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterQueue)child;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 班次数据源
        /// </summary>
        List<EntityDicShift> DataSourceShift { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region lue

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

            #region 班次

            DataTable dt = null;
            DataSourceShift = new List<EntityDicShift>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                dt = proxy.Service.SelectFullTable(new EntityDicShift());
                DataSourceShift.AddRange(EntityTools.ConvertToEntityList<EntityDicShift>(dt));
                if (DataSourceShift != null && DataSourceShift.Count > 0)
                {
                    Viewer.cboShift.Properties.Items.Clear();
                    Viewer.cboShift.Properties.Items.Add("");
                    foreach (EntityDicShift item in DataSourceShift)
                    {
                        Viewer.cboShift.Properties.Items.Add(item.shiftName);
                    }
                }
            }
            #endregion

            #endregion
        }
        #endregion

        #region SetRowCellStyle
        /// <summary>
        /// SetRowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string statusName = Viewer.gvNo.GetRowCellValue(e.RowHandle, EntityRegisterQueue.Columns.statusName).ToString();
            switch (statusName)
            {
                case "未报到":
                    e.Appearance.ForeColor = Color.FromArgb(62, 22, 250);
                    break;
                case "已报到":
                case "候诊中":
                    e.Appearance.ForeColor = Color.FromArgb(192, 0, 0);
                    break;
                case "已接诊":
                    e.Appearance.ForeColor = Color.Green;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Search
        /// <summary>
        /// Search
        /// </summary>
        internal void Search()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                EntityRegQueryParm queryVo = new EntityRegQueryParm();
                queryVo.startDate = Viewer.dteStart.Text;
                if (string.IsNullOrEmpty(queryVo.startDate))
                {
                    DialogBox.Msg("请选择挂号日期。");
                    Viewer.dteStart.Focus();
                    return;
                }
                queryVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                if (string.IsNullOrEmpty(queryVo.doctCode))
                {
                    DialogBox.Msg("请选择挂号日期。");
                    Viewer.dteStart.Focus();
                    return;
                }
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    List<EntityRegisterQueue> data = proxy.Service.GetRegisterQueue(queryVo);
                    string shiftName = Viewer.cboShift.Text.Trim();
                    if (shiftName == string.Empty)
                        Viewer.gcNo.DataSource = data;
                    else
                        Viewer.gcNo.DataSource = data.FindAll(t => t.shiftName == shiftName);
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
