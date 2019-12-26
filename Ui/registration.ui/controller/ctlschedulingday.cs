using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;

namespace Registration.Ui
{
    /// <summary>
    /// 日排班控制类
    /// </summary>
    public class ctlSchedulingDay : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmSchedulingDay Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmSchedulingDay)child;
        }
        #endregion

        #region 变量.属性

        bool isInit { get; set; }

        List<EntitySchedulingDay> dataSource { get; set; }

        List<EntityCodeReg> DataSourceCodeReg { get; set; }

        List<EntityDicShift> DataSourceShift { get; set; }

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
            Viewer.lueDoct.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            Viewer.lueDoct.Properties.IsUseShowColumn = true;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) Viewer.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #region 诊别

            DataTable dt = null;
            DataSourceCodeReg = new List<EntityCodeReg>();
            DataSourceShift = new List<EntityDicShift>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                dt = proxy.Service.SelectFullTable(new EntityCodeReg());
                DataSourceCodeReg.AddRange(EntityTools.ConvertToEntityList<EntityCodeReg>(dt));
                if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
                {
                    Viewer.cboRegType.Properties.Items.Clear();
                    Viewer.cboRegType.Properties.Items.Add("");
                    foreach (EntityCodeReg item in DataSourceCodeReg)
                    {
                        Viewer.cboRegType.Properties.Items.Add(item.regName);
                    }
                }
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

            this.isInit = true;
            Viewer.dteRegDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.isInit = false;
            Viewer.dteRegDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                string maxRegDate = proxy.Service.GetMaxRegDate();
                if (string.IsNullOrEmpty(maxRegDate))
                    Viewer.lblToDate.Text = "无排班信息..";
                else
                    Viewer.lblToDate.Text = "最新排班至" + maxRegDate;// +") 红字：停诊 绿字：已审核";
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        internal void New()
        {
            if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                using (frmSchedulingEdit2 frm = new frmSchedulingEdit2(2, string.Empty, string.Empty, string.Empty, string.Empty))
                {
                    frm.IsNew = true;
                    frm.ShowDialog();
                    if (frm.IsSave)
                    {
                        RefreshData();
                    }
                }
            }
            else
            {
                using (frmSchedulingEdit frm = new frmSchedulingEdit(2, 0))
                {
                    frm.IsNew = true;
                    frm.ShowDialog();
                    if (frm.IsSave)
                    {
                        RefreshData();
                    }
                }
            }
        }
        #endregion

        #region Import
        /// <summary>
        /// Import
        /// </summary>
        internal void Import()
        {
            using (frmSchedulingImport frm = new frmSchedulingImport(1))
            {
                frm.ShowDialog();
                if (frm.IsImport) Load();
            }
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

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        internal void Load()
        {
            if (isInit) return;
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (Viewer.dteRegDateStart.Text == string.Empty || Viewer.dteRegDateEnd.Text == string.Empty)
            {
                DialogBox.Msg("请选择时间。");
                Viewer.dteRegDateStart.Focus();
                return;
            }
            string regDateStart = Viewer.dteRegDateStart.DateTime.ToString("yyyy-MM-dd");
            string regDateEnd = Viewer.dteRegDateEnd.DateTime.ToString("yyyy-MM-dd");
            if (Function.Datetime(regDateStart) > Function.Datetime(regDateEnd))
            {
                DialogBox.Msg("开始时间不能大于结束时间，请重新选择。");
                Viewer.dteRegDateStart.Focus();
                return;
            }
            try
            {
                uiHelper.BeginLoading(Viewer);
                ProxyScheduling proxy = new ProxyScheduling();
                dataSource = proxy.Service.GetSchedulingDay(regDateStart, regDateEnd, deptCode);
                proxy = null;
                if (string.IsNullOrEmpty(Viewer.lueDoct.Text.Trim()))
                    Viewer.gcPlan.DataSource = dataSource;
                else
                    FilterDoct();
                Viewer.gvPlan.ViewCaption = (regDateStart == regDateEnd ? regDateStart : regDateStart + "至" + regDateEnd) + "日排班表";
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region FilterDoct
        /// <summary>
        /// FilterDoct
        /// </summary>
        internal void FilterDoct()
        {
            try
            {
                if (dataSource == null) return;
                uiHelper.BeginLoading(Viewer);
                bool isRegType = false;
                string doctName = Viewer.lueDoct.Text.Trim();
                string regCode = Viewer.cboRegType.Text;
                string shiftName = Viewer.cboShift.Text;
                if (regCode != string.Empty && DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
                {
                    if (DataSourceCodeReg.Any(t => t.regName == regCode))
                    {
                        regCode = DataSourceCodeReg.FirstOrDefault(t => t.regName == regCode).regCode;
                        isRegType = true;
                    }
                }
                if (string.IsNullOrEmpty(doctName))
                {
                    if (isRegType)
                    {
                        if (string.IsNullOrEmpty(shiftName))
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.regCode == regCode);
                        else
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.regCode == regCode && t.amPm == shiftName);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(shiftName))
                            Viewer.gcPlan.DataSource = dataSource;
                        else
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.amPm == shiftName);
                    }
                }
                else
                {
                    if (isRegType)
                    {
                        if (string.IsNullOrEmpty(shiftName))
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.doctName == doctName && t.regCode == regCode);
                        else
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.doctName == doctName && t.regCode == regCode && t.amPm == shiftName);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(shiftName))
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.doctName == doctName);
                        else
                            Viewer.gcPlan.DataSource = dataSource.FindAll(t => t.doctName == doctName && t.amPm == shiftName);
                    }
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit
        /// </summary>
        internal void Edit()
        {
            int regDid = Function.Int(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.regDid));
            if (regDid > 0)
            {
                if (!string.IsNullOrEmpty(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.auditorCode)))
                {
                    if (Viewer.Tag != null)
                    {
                        try
                        {
                            List<EntitySysModule> lstVo = Viewer.Tag as List<EntitySysModule>;
                            if (lstVo != null && lstVo.Count > 0)
                            {
                                bool isHave = false;
                                foreach (EntitySysModule item in lstVo)
                                {
                                    if (!string.IsNullOrEmpty(item.FuncCode) && item.OperName.Trim().ToLower() == "confirm") isHave = true;
                                }
                                if (!isHave)
                                {
                                    DialogBox.Msg("排班记录已审核，只能拥有【审核】权限的工作人员可以再编辑。");
                                    return;
                                }
                            }
                        }
                        catch { }
                    }
                }
                if (GlobalHospital.Current == EnumHospitalCode.顺德乐从 || GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    string deptCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.deptCode);
                    string roomCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.roomCode);
                    string doctCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.doctCode);
                    string regDate = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingDay.Columns.regDate);
                    using (frmSchedulingEdit2 frm = new frmSchedulingEdit2(2, deptCode, roomCode, doctCode, regDate))
                    {
                        frm.IsNew = true;
                        frm.ShowDialog();
                        if (frm.IsSave)
                        {
                            RefreshData();
                        }
                    }
                }
                else
                {
                    using (frmSchedulingEdit frm = new frmSchedulingEdit(2, regDid))
                    {
                        frm.ShowDialog();
                        if (frm.IsSave)
                        {
                            RefreshData();
                        }
                    }
                }
            }
            else
            {
                DialogBox.Msg("请选择记录。");
            }
        }
        #endregion

        #region Confirm
        /// <summary>
        /// Confirm
        /// </summary>
        internal void Confirm(int typeId)
        {
            if (typeId == 1)
            {
                List<EntitySchedulingDay> data = Viewer.gcPlan.DataSource as List<EntitySchedulingDay>;
                if (data == null || data.Count == 0)
                {
                    DialogBox.Msg("无排班数据。");
                    return;
                }
                List<decimal> lstRegDid = new List<decimal>();
                foreach (EntitySchedulingDay item in data)
                {
                    if (!string.IsNullOrEmpty(item.auditorCode)) continue;
                    if (lstRegDid.IndexOf(item.regDid) < 0) lstRegDid.Add(item.regDid);
                }
                if (lstRegDid.Count == 0)
                {
                    DialogBox.Msg("当前排班数据已全部审核。");
                    return;
                }
                if (DialogBox.Msg("是否审核当前排班信息？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    int ret = 0;
                    DataTable dtQueue = null;
                    bool isGenQueue = (GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从) ? true : false;
                    try
                    {
                        Viewer.Cursor = Cursors.WaitCursor;
                        Viewer.Cursor = Cursors.WaitCursor;
                        ProxyScheduling proxy = new ProxyScheduling();
                        ret = proxy.Service.ConfirmSchedulingDay(lstRegDid, GlobalLogin.objLogin.EmpNo, Utils.ServerTime(), isGenQueue, out dtQueue);
                        proxy = null;
                    }
                    finally
                    {
                        Viewer.Cursor = Cursors.Default;
                        uiHelper.CloseLoading(Viewer);
                    }
                    if (ret > 0)
                    {
                        //DialogBox.Msg(isGenQueue ? "现在开始生成接诊队列，大约需要几分钟。" : "排班数据审核成功！");
                        #region 生成接诊队列
                        if (isGenQueue && dtQueue != null)
                        {
                            try
                            {
                                Viewer.Cursor = Cursors.WaitCursor;
                                uiHelper.BeginLoading(Viewer);
                                int sortNo = 0;
                                int limitNum = 0;
                                decimal freqNum = 0;
                                string shiftCode = string.Empty;
                                string shiftEndTime = string.Empty;
                                string amPm = string.Empty;
                                int isBk = 0;
                                DateTime? dtmStart = null;
                                DataRow[] drr = null;
                                EntityOpRegQueue queueVo = null;
                                EntityClDuty dutyVo = null;
                                List<EntityOpRegQueue> lstQueue = null;
                                List<EntityClDuty> lstDuty = null;
                                List<decimal> lstSuccess = new List<decimal>();
                                foreach (decimal regDid in lstRegDid)
                                {
                                    drr = dtQueue.Select(EntityOpRegSchedulingDay.Columns.regDid + "=" + regDid);
                                    if (drr != null && drr.Length > 0)
                                    {
                                        sortNo = 0;
                                        lstQueue = new List<EntityOpRegQueue>();
                                        lstDuty = new List<EntityClDuty>();
                                        foreach (DataRow dr in drr)
                                        {
                                            // amPm: 1 am; 2 pm; 3 nm; 4 mm(中午) ...
                                            shiftCode = string.Empty;
                                            amPm = dr[EntityOpRegSchedulingDayNumber.Columns.amPm].ToString();
                                            shiftCode = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).hisId;
                                            shiftEndTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).endTime;
                                            isBk = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).isBk;
                                            limitNum = Function.Int(dr[EntityOpRegSchedulingDayDate.Columns.limitNum]);
                                            if (isBk == 1)  // 可预约
                                            {
                                                if (limitNum <= 0) continue;
                                                freqNum = Function.Dec(dr[EntityOpRegSchedulingDayDate.Columns.freqNum]);
                                                if (freqNum == 0) freqNum = 6; // 默认： 6分钟/人
                                                dtmStart = Function.Datetime(dr[EntityOpRegSchedulingDayDate.Columns.startTime]);
                                                sortNo = 0; // 按班次生成
                                                for (int k = 0; k < limitNum; k++)
                                                {
                                                    queueVo = new EntityOpRegQueue();
                                                    queueVo.regDid = regDid;
                                                    queueVo.regDate = dr[EntityOpRegSchedulingDay.Columns.regDate].ToString();
                                                    if (dtmStart.Value.AddMinutes(Convert.ToDouble(k * freqNum)) > Function.Datetime(shiftEndTime))
                                                        queueVo.planTime = shiftEndTime;
                                                    else
                                                        queueVo.planTime = dtmStart.Value.AddMinutes(Convert.ToDouble(k * freqNum)).ToString("HH:mm");
                                                    queueVo.deptCode = dr[EntityOpRegSchedulingDay.Columns.deptCode].ToString();
                                                    queueVo.roomCode = dr[EntityOpRegSchedulingDay.Columns.roomCode].ToString();
                                                    queueVo.doctCode = dr[EntityOpRegSchedulingDay.Columns.doctCode].ToString();
                                                    queueVo.ltFlag = Function.Dec(dr[EntityOpRegSchedulingDay.Columns.ltFlag].ToString());
                                                    queueVo.typeId = null;
                                                    queueVo.regTime = null;
                                                    queueVo.regNo = null;
                                                    queueVo.queueNo = ++sortNo;
                                                    queueVo.pid = null;
                                                    queueVo.bookingId = null;
                                                    queueVo.isPlus = 0;
                                                    queueVo.status = 0;
                                                    queueVo.amPm = Function.Int(amPm);
                                                    lstQueue.Add(queueVo);
                                                }
                                            }
                                            //
                                            dutyVo = new EntityClDuty();
                                            dutyVo.DUTY_DATE = dr[EntityOpRegSchedulingDay.Columns.regDate].ToString().Replace("-", ".");
                                            dutyVo.DR_CODE = dr[EntityOpRegSchedulingDay.Columns.doctCode].ToString();
                                            dutyVo.SHIFT_CODE = shiftCode;
                                            dutyVo.DIAG_CODE = dr[EntityCodeReg.Columns.typeCode].ToString();
                                            dutyVo.REG_CODE = dr[EntityCodeReg.Columns.regCode].ToString();
                                            dutyVo.DEPT_CODE = dr[EntityOpRegSchedulingDay.Columns.deptCode].ToString();
                                            dutyVo.ROOM_CODE = dr[EntityOpRegSchedulingDay.Columns.roomCode].ToString();
                                            dutyVo.LIMIT_FLAG = "T";
                                            dutyVo.LIMIT_NUM = limitNum;
                                            dutyVo.ADD_NUM = 0;
                                            dutyVo.BOOK_NUM = 0;
                                            dutyVo.REG_NUM = 0;
                                            dutyVo.DIAG_NUM = 0;
                                            dutyVo.Inline = Function.Int(dr[EntityOpRegSchedulingDay.Columns.status]) == 1 ? "T" : "F"; // 出、停诊
                                            dutyVo.regDid = regDid;
                                            lstDuty.Add(dutyVo);
                                            if (lstSuccess.IndexOf(regDid) < 0) lstSuccess.Add(regDid);
                                        }
                                        if (lstQueue.Count > 0 || lstDuty.Count > 0)
                                        {
                                            using (ProxyScheduling proxy2 = new ProxyScheduling())
                                            {
                                                try
                                                {
                                                    if (proxy2.Service.GenerateQueue(lstQueue, lstDuty) < 0)
                                                    {
                                                        proxy2.Service.UnConfirmSchedulingDay(lstSuccess, true);
                                                        DialogBox.Msg("审核失败。");
                                                        return;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    proxy2.Service.UnConfirmSchedulingDay(lstSuccess, true);
                                                    DialogBox.Msg(ex.Message);
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                                DialogBox.Msg("生成接诊队列成功！");
                            }
                            catch (Exception ex)
                            {
                                DialogBox.Msg(ex.Message);
                            }
                            finally
                            {
                                Viewer.Cursor = Cursors.Default;
                                uiHelper.CloseLoading(Viewer);
                            }
                        }
                        #endregion
                        Load();
                    }
                    else
                    {
                        DialogBox.Msg("审核失败。");
                    }
                }
            }
            else if (typeId == 2)
            {
                using (frmSchedulingImport frm = new frmSchedulingImport(2))
                {
                    frm.ShowDialog();
                    if (frm.IsConfirm) Load();
                }
            }
        }
        #endregion

        #region UnConfirm
        /// <summary>
        /// 反确认
        /// </summary>
        internal void UnConfirm(int typeId)
        {
            if (typeId == 1)
            {
                List<EntitySchedulingDay> data = Viewer.gcPlan.DataSource as List<EntitySchedulingDay>;
                if (data == null || data.Count == 0)
                {
                    DialogBox.Msg("无排班数据。");
                    return;
                }
                List<decimal> lstRegDid = new List<decimal>();
                foreach (EntitySchedulingDay item in data)
                {
                    if (string.IsNullOrEmpty(item.auditorCode)) continue;
                    if (lstRegDid.IndexOf(item.regDid) < 0) lstRegDid.Add(item.regDid);
                }
                if (lstRegDid.Count == 0)
                {
                    DialogBox.Msg("无审核数据。");
                    return;
                }
                else
                {
                    using (ProxyScheduling proxy = new ProxyScheduling())
                    {
                        if (GlobalHospital.Current == EnumHospitalCode.东莞八院)
                        {
                            if (proxy.Service.CheckSchedulingDayIsReg(lstRegDid))
                            {
                                DialogBox.Msg("所选数据已经被预约挂号，不能反审核。");
                                return;
                            }
                        }
                        else
                        {
                            if (proxy.Service.CheckRegQueueIsUsed(lstRegDid))
                            {
                                DialogBox.Msg("所选数据已经被挂号，不能反审核。");
                                return;
                            }
                        }
                    }
                }
                if (DialogBox.Msg("是否反审核当前已审核的排班信息？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    ProxyScheduling proxy = new ProxyScheduling();
                    int ret = proxy.Service.UnConfirmSchedulingDay(lstRegDid, ((GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从) ? true : false));
                    proxy = null;
                    if (ret > 0)
                    {
                        DialogBox.Msg("已审核排班数据反审核成功！");
                        Load();
                    }
                    else
                    {
                        DialogBox.Msg("反审核失败。");
                    }
                }
            }
            else if (typeId == 2)
            {
                using (frmSchedulingImport frm = new frmSchedulingImport(3))
                {
                    frm.ShowDialog();
                    if (frm.IsUnConfirm) Load();
                }
            }
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        internal void RefreshData()
        {
            Load();
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
                if (Function.Int(GetFieldValueStr(Viewer.gvPlan, e.RowHandle, EntitySchedulingDay.Columns.ltFlag)) == 1)
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

            if (!string.IsNullOrEmpty(GetFieldValueStr(Viewer.gvPlan, e.RowHandle, EntitySchedulingDay.Columns.auditorCode)))
            {
                e.Appearance.ForeColor = Color.Green;
            }
            if (GetFieldValueStr(Viewer.gvPlan, e.RowHandle, EntitySchedulingDay.Columns.status) == "停诊")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            Viewer.gvPlan.Invalidate();
        }
        #endregion

        #region GdaSynRegSchedule
        /// <summary>
        /// GDA.同步排班信息
        /// </summary>
        internal void GdaSynRegSchedule()
        {
            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                proxy.Service.GdaSynRegSchedule();
            }
        }
        #endregion

        #endregion

    }
}
