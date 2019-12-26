using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Itf;
using Registration.Entity;

namespace Registration.Ui
{
    /// <summary>
    /// 预约计划编辑控制类
    /// </summary>
    public class ctlSchedulingWeek : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmSchedulingWeek Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmSchedulingWeek)child;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 数据源
        /// </summary>
        List<EntitySchedulingWeek> DataSourcePlan { get; set; }

        bool isFilter { get; set; }

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            // 数据源
            RefreshData(true);

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
            Viewer.lueDoct.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            Viewer.lueDoct.Properties.IsUseShowColumn = true;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion
        }
        #endregion

        #region PauseRedraw/StartRedraw
        /// <summary>
        /// PauseRedraw
        /// </summary>
        internal void PauseRedraw()
        {
            Function.SuspendLayout(Viewer.gcPlan.Handle);
            Function.SuspendLayout(Viewer.Handle);
        }
        /// <summary>
        /// StartRedraw
        /// </summary>
        internal void StartRedraw()
        {
            Function.ResumeLayout(Viewer.gcPlan.Handle);
            Function.ResumeLayout(Viewer.Handle);
            Viewer.Refresh();
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        internal void RefreshData(bool isRefresh)
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                // 数据源
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    DataSourcePlan = proxy.Service.GetSchedulingWeek();
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
            if (isRefresh) FilterDay(string.Empty);
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            bool isSave = false;
            if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                using (frmSchedulingEdit2 frm = new frmSchedulingEdit2(1, string.Empty, string.Empty, string.Empty, string.Empty))
                {
                    frm.ShowDialog();
                    isSave = frm.IsSave;
                }
            }
            else
            {
                using (frmSchedulingEdit frm = new frmSchedulingEdit(1, 0))
                {
                    frm.ShowDialog();
                    isSave = frm.IsSave;
                }
            }
            if (isSave)
            {
                if (Viewer.lueDept.Text.Trim() != string.Empty || Viewer.lueDoct.Text.Trim() != string.Empty)
                {
                    RefreshData(false);
                    if (Viewer.lueDept.Text.Trim() != string.Empty) FilterDept(Viewer.lueDept.Text);
                    if (Viewer.lueDoct.Text.Trim() != string.Empty) FilterDoct(Viewer.lueDoct.Text);
                }
                else
                {
                    RefreshData(true);
                }
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit
        /// </summary>
        internal void Edit()
        {
            int regWid = Function.Int(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.regWid));
            if (regWid > 0)
            {
                bool isSave = false;
                if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    string deptCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.deptCode);
                    string roomCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.roomCode);
                    string doctCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.doctCode);
                    using (frmSchedulingEdit2 frm = new frmSchedulingEdit2(1, deptCode, roomCode, doctCode, string.Empty))
                    {
                        frm.ShowDialog();
                        isSave = frm.IsSave;
                    }
                }
                else
                {
                    using (frmSchedulingEdit frm = new frmSchedulingEdit(1, regWid))
                    {
                        frm.ShowDialog();
                        isSave = frm.IsSave;
                    }
                }
                if (isSave)
                {
                    if (Viewer.lueDept.Text.Trim() != string.Empty || Viewer.lueDoct.Text.Trim() != string.Empty)
                    {
                        RefreshData(false);
                        if (Viewer.lueDept.Text.Trim() != string.Empty) FilterDept(Viewer.lueDept.Text);
                        if (Viewer.lueDoct.Text.Trim() != string.Empty) FilterDoct(Viewer.lueDoct.Text);
                    }
                    else
                    {
                        RefreshData(true);
                    }
                }
            }
            else
            {
                DialogBox.Msg("请选择记录。");
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal void Delete()
        {
            decimal regWid = Function.Dec(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.regWid));
            if (regWid > 0)
            {
                string dayName = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.dayName);
                string deptName = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.deptName);
                string doctName = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.doctName);
                if (DialogBox.Msg("确定是否删除？\r\n\r\n" + dayName + " -> " + deptName + " -> " + doctName, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.DelScheduling(regWid);
                    proxy = null;
                    if (ret > 0)
                    {
                        Viewer.gvPlan.DeleteRow(Viewer.gvPlan.FocusedRowHandle);
                        DialogBox.Msg("删除排班计划成功！");
                    }
                    else
                    {
                        DialogBox.Msg("删除排班计划失败。");
                    }
                }
            }
            else
            {
                DialogBox.Msg("请选择记录。");
            }
        }
        #endregion

        #region FilterDay
        /// <summary>
        /// FilterDay
        /// </summary>
        /// <param name="dayName"></param>
        internal void FilterDay(string dayName)
        {
            try
            {
                uiHelper.BeginLoading(Viewer);
                if (string.IsNullOrEmpty(dayName))
                {
                    Viewer.gcPlan.DataSource = DataSourcePlan;
                    Viewer.gvPlan.ViewCaption = "全院排班表";
                }
                else
                {
                    Viewer.gcPlan.DataSource = DataSourcePlan.FindAll(t => t.dayName == dayName);
                    Viewer.gvPlan.ViewCaption = "星期" + dayName.Replace("周", "") + "排班表";
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region FilterDept
        /// <summary>
        /// FilterDept
        /// </summary>
        /// <param name="deptName"></param>
        internal void FilterDept(string deptName)
        {
            try
            {
                if (isFilter) return;
                uiHelper.BeginLoading(Viewer);
                isFilter = true;
                Viewer.lueDoct.Text = string.Empty;
                if (string.IsNullOrEmpty(deptName))
                    Viewer.gcPlan.DataSource = DataSourcePlan;
                else
                    Viewer.gcPlan.DataSource = DataSourcePlan.FindAll(t => t.deptName == deptName);
                Viewer.gvPlan.ViewCaption = deptName + "排班表";
            }
            finally
            {
                isFilter = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region FilterDoct
        /// <summary>
        /// FilterDoct
        /// </summary>
        /// <param name="deptName"></param>
        internal void FilterDoct(string doctName)
        {
            try
            {
                if (isFilter) return;
                uiHelper.BeginLoading(Viewer);
                isFilter = true;
                Viewer.lueDept.Text = string.Empty;
                if (string.IsNullOrEmpty(doctName))
                    Viewer.gcPlan.DataSource = DataSourcePlan;
                else
                    Viewer.gcPlan.DataSource = DataSourcePlan.FindAll(t => t.doctName == doctName);
                Viewer.gvPlan.ViewCaption = doctName + "排班表";
            }
            finally
            {
                isFilter = false;
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        internal void Print()
        {
            Viewer.gvPlan.PrintDialog();
        }
        #endregion

        #region RowCellStyle
        /// <summary>
        /// RowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void RowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == Viewer.gvPlan.FocusedColumn && e.RowHandle == Viewer.gvPlan.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.FromArgb(251, 165, 8);
                e.Appearance.BackColor2 = Color.White;
            }
            else
            {
                if (Function.Int(GetFieldValueStr(Viewer.gvPlan, e.RowHandle, EntitySchedulingWeek.Columns.ltFlag)) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(235, 241, 222);
                    e.Appearance.BackColor2 = Color.FromArgb(235, 241, 222);
                }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                    e.Appearance.BackColor2 = Color.Transparent;
                }
            }
            Viewer.gvPlan.Invalidate();
        }
        #endregion

        #region itf.ImportRegPlan

        /// <summary>
        /// EntityTemp
        /// </summary>
        class EntityX
        {
            public string shiftNo { get; set; }
            public string deptCode { get; set; }
            public string doctCode { get; set; }
            public string regCode { get; set; }
            public string roomCode { get; set; }
        }

        /// <summary>
        /// itf.ImportRegPlan
        /// </summary>
        internal void ImportRegPlan()
        {
            DataTable dtSource = null;
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                dtSource = proxy.Service.GetJlRegplan();
            }
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                if (DialogBox.Msg("总记录" + dtSource.Rows.Count.ToString() + ", 是否开始导入?", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Viewer.Cursor = Cursors.WaitCursor;
                        EntityX x = null;
                        List<EntityX> lstX = new List<EntityX>();
                        foreach (DataRow dr in dtSource.Rows)
                        {
                            x = new EntityX();
                            x.shiftNo = dr["shift_no"].ToString();
                            x.deptCode = dr["dept_code"].ToString();
                            x.doctCode = dr["dr_code"].ToString();
                            x.roomCode = dr["room_code"].ToString();
                            x.regCode = dr["reg_code"].ToString();
                            if (!lstX.Any(t => t.shiftNo == x.shiftNo && t.deptCode == x.deptCode && t.doctCode == x.doctCode && t.roomCode == x.roomCode && t.regCode == x.regCode))
                            {
                                lstX.Add(x);
                            }
                        }
                        string shiftCode = string.Empty;
                        DataRow[] drr = null;
                        foreach (EntityX item in lstX)
                        {
                            // 主信息
                            EntityOpRegScheduling mainVo1 = new EntityOpRegScheduling();
                            mainVo1.regWid = 0;
                            mainVo1.weekId = Function.Int(item.shiftNo) - 1;
                            mainVo1.regCode = item.regCode;
                            mainVo1.deptCode = item.deptCode;
                            mainVo1.roomCode = item.roomCode;
                            mainVo1.doctCode = item.doctCode;
                            mainVo1.comment = string.Empty;
                            mainVo1.ltFlag = 0;

                            List<EntityOpRegSchedulingDate> lstRegDate1 = new List<EntityOpRegSchedulingDate>();
                            List<EntityOpRegSchedulingDate> lstRegDate2 = new List<EntityOpRegSchedulingDate>();
                            drr = dtSource.Select("dept_code = '" + item.deptCode + "' and dr_code = '" + item.doctCode + "' and room_code = '" + item.roomCode +
                                                  "' and reg_code = '" + item.regCode + "' and shift_no = '" + item.shiftNo + "'");
                            if (drr != null && drr.Length > 0)
                            {
                                foreach (DataRow dr2 in drr)
                                {
                                    shiftCode = dr2["shift_code"].ToString().Trim();
                                    switch (shiftCode)
                                    {
                                        case "01":  // 上午 07:30     	11:30   
                                            GetRegDateList("07:30", "11:30", 3, 1, 80, 6, ref lstRegDate1);
                                            break;
                                        case "02":  // 中午 11:30     	14:00   
                                            GetRegDateList("11:30", "14:00", 3, 4, 80, 6, ref lstRegDate1);
                                            break;
                                        case "03":  // 下午 14:00     	17:30   
                                            GetRegDateList("14:00", "17:30", 3, 2, 80, 6, ref lstRegDate1);
                                            break;
                                        case "04":  // 夜班 00:00     	07:30  
                                            GetRegDateList("00:00", "07:30", 3, 1, 80, 6, ref lstRegDate1);
                                            break;
                                        case "05":  // 中班 17:30     	24:00  
                                            GetRegDateList("17:30", "23:59", 3, 3, 80, 6, ref lstRegDate1);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            List<EntityOpRegSchedulingNumber> lstRegNumber1 = new List<EntityOpRegSchedulingNumber>();
                            List<EntityOpRegSchedulingNumber> lstRegNumber2 = new List<EntityOpRegSchedulingNumber>();
                            if (lstRegDate1.Count > 0)
                            {
                                EntityOpRegSchedulingNumber numVo = null;
                                foreach (EntityOpRegSchedulingDate item2 in lstRegDate1)
                                {
                                    numVo = new EntityOpRegSchedulingNumber();
                                    numVo.regWid = item2.regWid;
                                    numVo.amPm = item2.amPm;
                                    numVo.startTime = item2.startTime;
                                    numVo.endTime = item2.endTime;
                                    numVo.normalNum = 60;//item2.limitNum;
                                    numVo.weNum = 20;
                                    lstRegNumber1.Add(numVo);
                                }
                            }
                            int ret = 0;
                            decimal regWid = 0;
                            EntityOpRegScheduling mainVo2 = new EntityOpRegScheduling();
                            if (lstRegDate1.Count > 0)
                            {
                                using (ProxyScheduling proxy = new ProxyScheduling())
                                {
                                    ret = proxy.Service.SaveScheduling(mainVo1, lstRegDate1, lstRegNumber1, null, out regWid);
                                }
                                if (ret > 0 && regWid > 0)
                                { }
                                else
                                {
                                    DialogBox.Msg("失败\r\n" + "deptCode:" + mainVo1.deptCode + " doctCode:" + mainVo1.doctCode);
                                }
                            }


                            //if (lstRegDate2.Count > 0)
                            //{
                            //    EntityOpRegSchedulingNumber numVo = null;
                            //    foreach (EntityOpRegSchedulingDate item2 in lstRegDate2)
                            //    {
                            //        numVo = new EntityOpRegSchedulingNumber();
                            //        numVo.regWid = item2.regWid;
                            //        numVo.amPm = item2.amPm;
                            //        numVo.startTime = item2.startTime;
                            //        numVo.endTime = item2.endTime;
                            //        numVo.normalNum = 60;//item2.limitNum;
                            //        numVo.weNum = 20;
                            //        lstRegNumber2.Add(numVo);
                            //    }
                            //}
                            //int ret = 0;
                            //decimal regWid = 0;
                            //EntityOpRegScheduling mainVo2 = new EntityOpRegScheduling();
                            //if (mainVo1.regCode.Trim() != "04")
                            //{
                            //    if(lstRegDate1.Count>0)
                            //    {
                            //        using (ProxyScheduling proxy = new ProxyScheduling())
                            //        {
                            //            ret = proxy.Service.SaveScheduling(mainVo1, lstRegDate1, lstRegNumber1, null, out regWid);
                            //        }
                            //        if (ret > 0 && regWid > 0)
                            //        { }
                            //        else
                            //        {
                            //            DialogBox.Msg("失败\r\n" + "deptCode:" + mainVo1.deptCode + " doctCode:" + mainVo1.doctCode);
                            //        }
                            //    }
                            //    if (lstRegDate2.Count > 0)
                            //    {
                            //        mainVo2.regWid = 0;
                            //        mainVo2.weekId = Function.Int(item.shiftNo) - 1;
                            //        mainVo2.regCode = "04  ";
                            //        mainVo2.deptCode = item.deptCode;
                            //        mainVo2.roomCode = item.roomCode;
                            //        mainVo2.doctCode = item.doctCode;
                            //        mainVo2.comment = string.Empty;
                            //        mainVo2.ltFlag = 0;
                            //        using (ProxyScheduling proxy = new ProxyScheduling())
                            //        {
                            //            ret = proxy.Service.SaveScheduling(mainVo2, lstRegDate2, lstRegNumber2, null, out regWid);
                            //        }
                            //        if (ret > 0 && regWid > 0)
                            //        { }
                            //        else
                            //        {
                            //            DialogBox.Msg("失败\r\n" + "deptCode:" + mainVo2.deptCode + " doctCode:" + mainVo2.doctCode);
                            //        }
                            //    }
                            //}
                            //if (mainVo1.regCode.Trim() == "04" )
                            //{
                            //    if (lstRegDate2.Count > 0)
                            //    {
                            //        using (ProxyScheduling proxy = new ProxyScheduling())
                            //        {
                            //            ret = proxy.Service.SaveScheduling(mainVo1, lstRegDate2, lstRegNumber2, null, out regWid);
                            //        }
                            //        if (ret > 0 && regWid > 0)
                            //        { }
                            //        else
                            //        {
                            //            DialogBox.Msg("失败\r\n" + "deptCode:" + mainVo1.deptCode + " doctCode:" + mainVo1.doctCode);
                            //        }
                            //    }
                            //    if (lstRegDate1.Count > 0)
                            //    {
                            //        mainVo2.regWid = 0;
                            //        mainVo2.weekId = Function.Int(item.shiftNo) - 1;
                            //        mainVo2.regCode = "01  ";
                            //        mainVo2.deptCode = item.deptCode;
                            //        mainVo2.roomCode = item.roomCode;
                            //        mainVo2.doctCode = item.doctCode;
                            //        mainVo2.comment = string.Empty;
                            //        mainVo2.ltFlag = 0;
                            //        using (ProxyScheduling proxy = new ProxyScheduling())
                            //        {
                            //            ret = proxy.Service.SaveScheduling(mainVo2, lstRegDate1, lstRegNumber1, null, out regWid);
                            //        }
                            //        if (ret > 0 && regWid > 0)
                            //        { }
                            //        else
                            //        {
                            //            DialogBox.Msg("失败\r\n" + "deptCode:" + mainVo2.deptCode + " doctCode:" + mainVo2.doctCode);
                            //        }
                            //    }
                            //}
                        }
                        DialogBox.Msg("导入完毕。");
                    }
                    finally
                    {
                        Viewer.Cursor = Cursors.Default;
                    }
                }
            }
        }

        void GetRegDateList(string startTime, string endTime, int typeId, int amPm, int limitNum, decimal freqNum, ref List<EntityOpRegSchedulingDate> lstRegDate)
        {
            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime) &&
                endTime != "00:00" && Convert.ToDateTime(endTime) > Convert.ToDateTime(startTime))
            {
                EntityOpRegSchedulingDate dateVo = new EntityOpRegSchedulingDate();
                dateVo.typeId = typeId;
                dateVo.amPm = amPm;
                dateVo.startTime = startTime;
                dateVo.endTime = endTime;
                dateVo.limitNum = limitNum;
                dateVo.freqNum = freqNum;
                lstRegDate.Add(dateVo);
            }
        }
        #endregion

        #region 复制
        /// <summary>
        /// 复制
        /// </summary>
        internal void Copy()
        {
            int regWid = Function.Int(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.regWid));
            if (regWid > 0)
            {
                string deptCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.deptCode);
                string roomCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.roomCode);
                string doctCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingWeek.Columns.doctCode);
                frmSchedulingCopy frm = new frmSchedulingCopy(deptCode, roomCode, doctCode);
                frm.ShowDialog();
            }
            else
            {
                DialogBox.Msg("请选择记录。");
            }
        }
        #endregion

        #endregion


    }
}
