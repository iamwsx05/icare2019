using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 排班复制控制类
    /// </summary>
    public class ctlSchedulingCopy : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmSchedulingCopy Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmSchedulingCopy)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// DeptCode
        /// </summary>
        internal string DeptCode { get; set; }

        /// <summary>
        /// RoomCode
        /// </summary>
        internal string RoomCode { get; set; }

        /// <summary>
        /// DoctCode
        /// </summary>
        internal string DoctCode { get; set; }

        /// <summary>
        /// 诊室字典
        /// </summary>
        List<EntityDicDeptRoom> DataSourceRoom { get; set; }

        /// <summary>
        /// 班次字典
        /// </summary>
        List<EntityDicShift> DataSourceShift { get; set; }

        /// <summary>
        /// 医师数据源
        /// </summary>
        List<EntityCodeOperator> DataSourceDoct { get; set; }

        /// <summary>
        /// 排班计划主信息
        /// </summary>
        List<EntityOpRegSchedulingDatePlus> lstPlus = null;

        /// <summary>
        /// 排班计划号源
        /// </summary>
        List<EntityOpRegSchedulingNumber> lstRegNumber = null;

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                #region dic
                DataTable dt = null;
                DataSourceRoom = new List<EntityDicDeptRoom>();
                DataSourceShift = new List<EntityDicShift>();
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    dt = proxy.Service.SelectFullTable(new EntityDicDeptRoom());
                    DataSourceRoom.AddRange(EntityTools.ConvertToEntityList<EntityDicDeptRoom>(dt));
                    dt = proxy.Service.SelectFullTable(new EntityDicShift());
                    DataSourceShift.AddRange(EntityTools.ConvertToEntityList<EntityDicShift>(dt));
                }
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
                Viewer.lueDoct.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
                Viewer.lueDoct.Properties.IsUseShowColumn = true;
                if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
                Viewer.lueDoct.Properties.SetSize();
                #endregion
                #region 诊间
                Viewer.lueRoom.Properties.PopupWidth = 260;
                Viewer.lueRoom.Properties.PopupHeight = 380;
                Viewer.lueRoom.Properties.ValueColumn = EntityDicDeptRoom.Columns.roomCode;
                Viewer.lueRoom.Properties.DisplayColumn = EntityDicDeptRoom.Columns.roomName;
                Viewer.lueRoom.Properties.Essential = false;
                Viewer.lueRoom.Properties.IsShowColumnHeaders = true;
                Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomCode, 45);
                Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomName, 110);
                Viewer.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomDesc, 110);
                Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomCode, "编码");
                Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomName, "名称");
                Viewer.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomDesc, "地点");
                Viewer.lueRoom.Properties.ShowColumn = EntityDicDeptRoom.Columns.roomCode + "|" + EntityDicDeptRoom.Columns.roomName + "|" + EntityDicDeptRoom.Columns.roomDesc;
                Viewer.lueRoom.Properties.IsUseShowColumn = true;
                #endregion
                EntityOpRegSchedulingDoct doctVo = null;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    proxy.Service.GetScheduling(this.DeptCode, this.RoomCode, this.DoctCode, out lstPlus, out lstRegNumber, out doctVo);
                }
                if (GlobalDic.DataSourceDepartment != null && GlobalDic.DataSourceDepartment.Count > 0)
                {
                    if (GlobalDic.DataSourceDepartment.Any(t => t.deptCode == this.DeptCode))
                        Viewer.txtDept.Text = (GlobalDic.DataSourceDepartment.FirstOrDefault(t => t.deptCode == this.DeptCode)).deptName;
                }
                if (DataSourceRoom != null && DataSourceRoom.Count > 0)
                {
                    if (DataSourceRoom.Any(t => t.deptCode.Trim() == this.DeptCode.Trim() && t.roomCode.Trim() == this.RoomCode.Trim()))
                        Viewer.txtRoom.Text = (DataSourceRoom.FirstOrDefault(t => t.deptCode.Trim() == this.DeptCode.Trim() && t.roomCode.Trim() == this.RoomCode.Trim())).roomName;
                }
                Viewer.txtDoct.Text = doctVo.doctCode + " " + doctVo.doctName;
                if (GlobalDic.DataSourceEmployee != null && GlobalDic.DataSourceEmployee.Count > 0)
                {
                    if (GlobalDic.DataSourceEmployee.Any(t => t.operCode.Trim() == this.DoctCode.Trim()))
                        Viewer.txtRank.Text = (GlobalDic.DataSourceEmployee.FirstOrDefault(t => t.operCode.Trim() == this.DoctCode.Trim())).TechnicalLevelName;
                }
                #region 时间设定
                List<EntityOpRegSchedulingDatePlus> data = new List<EntityOpRegSchedulingDatePlus>();
                foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
                {
                    if (data.Any(t => t.weekId == item.weekId))
                        continue;
                    else
                    {
                        item.check = 1;
                        data.Add(item);
                    }
                }
                Viewer.gcDate.DataSource = data;
                #endregion

                DataSourceDoct = new List<EntityCodeOperator>();
                Viewer.gcDoct.DataSource = DataSourceDoct;
            }
            finally
            {
                Viewer.ValueChanged = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region LueFilterRoom
        /// <summary>
        /// LueFilterRoom
        /// </summary>
        internal void LueFilterRoom()
        {
            if (!string.IsNullOrEmpty(Viewer.lueDoct.Properties.DBValue))
            {
                EntityCodeOperator doctVo = Viewer.lueDoct.Properties.DBRow as EntityCodeOperator;
                if (!string.IsNullOrEmpty(doctVo.DeptNo))
                {
                    List<EntityDicDeptRoom> data = DataSourceRoom.FindAll(t => t.deptCode == doctVo.DeptNo);
                    if (data != null)
                    {
                        Viewer.lueRoom.Properties.DataSource = data.ToArray();
                        Viewer.lueRoom.Properties.SetSize();
                    }
                }
            }
        }
        #endregion

        #region DoctAdd
        /// <summary>
        /// DoctAdd
        /// </summary>
        internal void DoctAdd()
        {
            if (!string.IsNullOrEmpty(Viewer.lueDoct.Properties.DBValue))
            {
                EntityCodeOperator doctVo = Viewer.lueDoct.Properties.DBRow as EntityCodeOperator;
                doctVo.roomCode = Viewer.lueRoom.Properties.DBValue;
                doctVo.roomName = Viewer.lueRoom.Text;
                if (string.IsNullOrEmpty(doctVo.roomCode) || string.IsNullOrEmpty(doctVo.roomName))
                {
                    DialogBox.Msg("请选择诊间。");
                    return;
                }
                if (DataSourceDoct.Any(t => t.operCode == doctVo.operCode))
                    return;
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    if (proxy.Service.IsExistScheduling(doctVo.DeptNo, doctVo.roomCode, doctVo.operCode))
                        doctVo.isScheduling = "√";
                    else
                        doctVo.isScheduling = "╳";
                }
                DataSourceDoct.Add(doctVo);
                Viewer.gcDoct.RefreshDataSource();
                Viewer.btnCopy.Enabled = true;
            }
            else
            {
                DialogBox.Msg("请选择医师。");
            }
        }
        #endregion

        #region DoctDel
        /// <summary>
        /// DoctDel
        /// </summary>
        internal void DoctDel()
        {
            if (Viewer.gvDoct.FocusedRowHandle >= 0)
            {
                if (DataSourceDoct.Count > Viewer.gvDoct.FocusedRowHandle)
                    DataSourceDoct.RemoveAt(Viewer.gvDoct.FocusedRowHandle);
                Viewer.gcDoct.RefreshDataSource();
                if (DataSourceDoct.Count == 0)
                    Viewer.btnCopy.Enabled = false;
            }
        }
        #endregion

        #region SchedulingCopy
        /// <summary>
        /// SchedulingCopy
        /// </summary>
        internal void SchedulingCopy()
        {
            if (DataSourceDoct.Count == 0) return;
            List<decimal> lstWeekId = new List<decimal>();
            List<EntityOpRegSchedulingDatePlus> data = Viewer.gcDate.DataSource as List<EntityOpRegSchedulingDatePlus>;
            foreach (EntityOpRegSchedulingDatePlus item in data)
            {
                if (item.check == 1)
                {
                    lstWeekId.Add(item.weekId);
                }
            }
            if (lstWeekId.Count == 0)
            {
                DialogBox.Msg("请选择时间(左侧列表)。");
                return;
            }
            List<decimal> lstRegWid = new List<decimal>();
            List<EntityOpRegSchedulingDatePlus> lstDate = new List<EntityOpRegSchedulingDatePlus>();
            List<EntityOpRegSchedulingNumber> lstNumber = new List<EntityOpRegSchedulingNumber>();
            foreach (EntityOpRegSchedulingDatePlus item in lstPlus)
            {
                if (lstWeekId.IndexOf(item.weekId) >= 0)
                {
                    if (lstRegWid.IndexOf(item.regWid) < 0)
                        lstRegWid.Add(item.regWid);
                    lstDate.Add(item);
                }
            }
            foreach (EntityOpRegSchedulingNumber item in lstRegNumber)
            {
                if (lstRegWid.IndexOf(item.regWid) >= 0)
                {
                    lstNumber.Add(item);
                }
            }
            if (lstDate.Count == 0 || lstNumber.Count == 0)
            {
                DialogBox.Msg("数据不完整，不能复制。");
                return;
            }
            if (DialogBox.Msg("是否开始复制？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string info = string.Empty;
                uiHelper.BeginLoading(Viewer);
                // 主信息
                EntityOpRegScheduling mainVo = null;
                // 依次复制
                foreach (EntityCodeOperator item in DataSourceDoct)
                {
                    // 主信息
                    mainVo = new EntityOpRegScheduling();
                    mainVo.deptCode = item.DeptNo;
                    mainVo.roomCode = item.roomCode;
                    mainVo.doctCode = item.operCode;
                    mainVo.isScheduling = (item.isScheduling == "√" ? 1 : 0);
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        try
                        {
                            if (proxy.Service.CopyScheduling(mainVo, lstDate, lstNumber, DataSourceShift) <= 0)
                            {
                                DialogBox.Msg("医师：" + item.operName + " 复制失败。");
                            }
                            else
                            {
                                info += item.operCode + " " + item.operName + "\r\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            DialogBox.Msg(ex.Message);
                        }
                    }
                }
                uiHelper.CloseLoading(Viewer);
                if (info != string.Empty)
                {
                    DialogBox.Msg(info + "\r\n复制成功！");
                }
            }
        }
        #endregion

        #endregion
    }
}
