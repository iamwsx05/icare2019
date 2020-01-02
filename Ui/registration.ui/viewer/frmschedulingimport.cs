using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Common.Utils;
using Registration.Entity;

namespace Registration.Ui
{
    public partial class frmSchedulingImport : frmBasePopup
    {
        /// <summary>
        /// 构造
        /// </summary>
        public frmSchedulingImport(int _typeId)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.typeId = _typeId; 
            }
        }

        /// <summary>
        /// 1. 导入； 2.审核； 3.反审核
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// 是否导入
        /// </summary>
        public bool IsImport { get; set; }
        /// <summary>
        /// 是否审核
        /// </summary>
        public bool IsConfirm { get; set; }
        /// <summary>
        /// 是否反审核
        /// </summary>
        public bool IsUnConfirm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime dtmNow { get; set; }

        /// <summary>
        /// 诊室字典
        /// </summary>
        List<EntityDicDeptRoom> DataSourceRoom { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        List<EntityDicShift> DataSourceShift { get; set; }

        private void frmSchedulingImport_Load(object sender, EventArgs e)
        {
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

            this.rdoLtFlag.Visible = (GlobalHospital.Current == EnumHospitalCode.东莞八院 ? true : false);
            dtmNow = Utils.ServerTime();
            if (this.typeId == 1)
                this.Text = "导入排班计划(从明日及以后)";
            else if (this.typeId == 2)
                this.Text = "*审核*";
            else if (this.typeId == 3)
                this.Text = "***反审核***";

            #region 医师
            this.lueDoct.Properties.PopupWidth = 155;
            this.lueDoct.Properties.PopupHeight = 250;
            this.lueDoct.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueDoct.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueDoct.Properties.Essential = false;
            this.lueDoct.Properties.IsShowColumnHeaders = true;
            this.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 70);
            this.lueDoct.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 85);
            this.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operCode, "编码");
            this.lueDoct.Properties.ColumnHeaders.Add(EntityCodeOperator.Columns.operName, "名称");
            this.lueDoct.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueDoct.Properties.IsUseShowColumn = true;
            this.lueDoct.Properties.FilterColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName + "|" + EntityCodeOperator.Columns.pyCode + "|" + EntityCodeOperator.Columns.wbCode;
            if (GlobalDic.DataSourceDoctor != null && GlobalDic.DataSourceDoctor.Count > 0) this.lueDoct.Properties.DataSource = GlobalDic.DataSourceDoctor.ToArray();
            this.lueDoct.Properties.SetSize();
            #endregion

            #region 科室
            this.lueDept.Properties.PopupWidth = 160;
            this.lueDept.Properties.PopupHeight = 300;
            this.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            this.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.IsShowColumnHeaders = true;
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 100);
            this.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptCode, "编码");
            this.lueDept.Properties.ColumnHeaders.Add(EntityCodeDepartment.Columns.deptName, "名称");
            this.lueDept.Properties.ShowColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.IsUseShowColumn = true;
            this.lueDept.Properties.FilterColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName + "|" + EntityCodeDepartment.Columns.pyCode + "|" + EntityCodeDepartment.Columns.wbCode;
            if (GlobalDic.DataSourceDepartment != null && GlobalDic.DataSourceDepartment.Count > 0)
            {
                List<EntityCodeDepartment> dataDept = GlobalDic.DataSourceDepartment.FindAll(t => t.type == "1");
                if (dataDept != null && dataDept.Count > 0) this.lueDept.Properties.DataSource = dataDept.ToArray();
            }
            this.lueDept.Properties.SetSize();
            #endregion

            #region 诊间
            this.lueRoom.Properties.PopupWidth = 250;
            this.lueRoom.Properties.PopupHeight = 380;
            this.lueRoom.Properties.ValueColumn = EntityDicDeptRoom.Columns.roomCode;
            this.lueRoom.Properties.DisplayColumn = EntityDicDeptRoom.Columns.roomName;
            this.lueRoom.Properties.Essential = false;
            this.lueRoom.Properties.IsShowColumnHeaders = true;
            this.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomCode, 45);
            this.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomName, 110);
            this.lueRoom.Properties.ColumnWidth.Add(EntityDicDeptRoom.Columns.roomDesc, 110);
            this.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomCode, "编码");
            this.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomName, "名称");
            this.lueRoom.Properties.ColumnHeaders.Add(EntityDicDeptRoom.Columns.roomDesc, "地点");
            this.lueRoom.Properties.ShowColumn = EntityDicDeptRoom.Columns.roomCode + "|" + EntityDicDeptRoom.Columns.roomName + "|" + EntityDicDeptRoom.Columns.roomDesc;
            this.lueRoom.Properties.IsUseShowColumn = true;
            #endregion
        }

        #region GetDate
        /// <summary>
        /// GetDate
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        bool GetDate(ref DateTime startDate, ref DateTime endDate)
        {
            switch (this.rdoType.SelectedIndex)
            {
                case 0:
                    startDate = dtmNow.AddDays(1);
                    endDate = startDate;
                    break;
                case 1:
                    startDate = dtmNow.AddDays(1);
                    endDate = (dtmNow.AddDays(1 - Convert.ToInt32(dtmNow.DayOfWeek.ToString("d")))).AddDays(6);
                    break;
                case 2:
                    startDate = (dtmNow.AddDays(1 - Convert.ToInt32(dtmNow.DayOfWeek.ToString("d")))).AddDays(7);
                    endDate = (dtmNow.AddDays(1 - Convert.ToInt32(dtmNow.DayOfWeek.ToString("d")))).AddDays(13);
                    break;
                case 3:
                    startDate = dtmNow.AddDays(1);
                    endDate = (dtmNow.AddDays(1 - dtmNow.Day)).AddMonths(1).AddDays(-1);
                    break;
                case 4:
                    startDate = (dtmNow.AddDays(1 - dtmNow.Day)).AddMonths(1);
                    endDate = (((dtmNow.AddDays(1 - dtmNow.Day)).AddMonths(1)).AddDays(-1)).AddMonths(1);
                    break;
                case 5:
                    if (this.dteStart.EditValue == null || this.dteEnd.EditValue == null)
                    {
                        DialogBox.Msg("请输入自定义起止时间。");
                        this.dteStart.Focus();
                        return false;
                    }
                    if (this.dteStart.DateTime.Date > this.dteEnd.DateTime.Date)
                    {
                        DialogBox.Msg("开始时间不能大于结束时间。");
                        this.dteStart.Focus();
                        return false;
                    }
                    //if (this.dteStart.DateTime.Date < dtmNow.AddDays(1).Date)
                    //{
                    //    DialogBox.Msg("开始时间不能小于结束时间。(" + dtmNow.AddDays(1).Date.ToString("yyyy-MM-dd") + ")");
                    //    this.dteStart.Focus();
                    //    return false;
                    //}
                    startDate = this.dteStart.DateTime;
                    endDate = this.dteEnd.DateTime;
                    break;
                default:
                    break;
            }
            return true;
        }
        #endregion

        #region GetDoct
        /// <summary>
        /// GetDoct
        /// </summary>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        bool GetDoct(ref string doctCode, ref string doctName)
        {
            if (this.chkDoct.Checked)
            {
                doctCode = this.lueDoct.Properties.DBValue;
                doctName = this.lueDoct.Text.Trim();
                if (string.IsNullOrEmpty(doctCode))
                {
                    this.lueDoct.Focus();
                    DialogBox.Msg("勾选了医师，则需指定具体医师。");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        #endregion

        #region GetDept
        /// <summary>
        /// GetDept
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        bool GetDept(ref string deptCode, ref string deptName)
        {
            if (this.chkDept.Checked)
            {
                deptCode = this.lueDept.Properties.DBValue;
                deptName = this.lueDept.Text.Trim();
                if (string.IsNullOrEmpty(deptCode))
                {
                    this.lueDept.Focus();
                    DialogBox.Msg("勾选了科室，则需指定具体科室。");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        #endregion

        #region GetRoom
        /// <summary>
        /// GetRoom
        /// </summary>
        /// <param name="roomCode"></param>
        /// <returns></returns>
        bool GetRoom(ref string roomCode, ref string roomName)
        {
            if (this.chkRoom.Checked)
            {
                roomCode = this.lueRoom.Properties.DBValue;
                roomName = this.lueRoom.Text.Trim();
                if (string.IsNullOrEmpty(roomCode))
                {
                    this.lueRoom.Focus();
                    DialogBox.Msg("勾选了诊间，则需指定具体诊间。");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        #endregion

        #region Import
        /// <summary>
        /// Import
        /// </summary>
        void Import()
        {
            EntityImport vo = null;
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string doctCode = string.Empty;
            string doctName = string.Empty;
            string deptCode = string.Empty;
            string deptName = string.Empty;
            string roomCode = string.Empty;
            string roomName = string.Empty;
            List<EntityImport> lstImport = new List<EntityImport>();
            if (GetDate(ref startDate, ref endDate) == false) return;
            if (GetDoct(ref doctCode, ref doctName) == false) return;
            if (GetDept(ref deptCode, ref deptName) == false) return;
            if (GetRoom(ref roomCode, ref roomName) == false) return;
            string doctInfo = (string.IsNullOrEmpty(doctName) ? string.Empty : "\r\n\r\n医师: " + doctName);
            string deptInfo = (string.IsNullOrEmpty(deptName) ? string.Empty : "\r\n\r\n科室: " + deptName);
            string roomInfo = (string.IsNullOrEmpty(roomName) ? string.Empty : "\r\n\r\n诊间: " + roomName);
            do
            {
                vo = new EntityImport();
                vo.regDate = startDate.ToString("yyyy-MM-dd");
                vo.weekId = uiHelper.NumOfWeek(startDate);
                vo.doctCode = doctCode;
                vo.deptCode = deptCode;
                vo.roomCode = roomCode;
                lstImport.Add(vo);
                startDate = startDate.AddDays(1);

            } while (startDate.Date <= endDate.Date);

            Dictionary<string, string> dicParm = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(doctCode)) dicParm.Add("doctCode", doctCode);
            if (!string.IsNullOrEmpty(deptCode)) dicParm.Add("deptCode", deptCode);
            if (!string.IsNullOrEmpty(roomCode)) dicParm.Add("roomCode", roomCode);
            if (lstImport.Count > 0)
            {
                using (ProxyScheduling proxy = new ProxyScheduling())
                {
                    if (proxy.Service.CheckSchedulingDayIsConfirm(lstImport[0].regDate, lstImport[lstImport.Count - 1].regDate, dicParm))
                    {
                        DialogBox.Msg("所选导入时间段排班记录已审核，不能再次导入。\r\n\r\n如需再次导入请先反审排班记录。" + doctInfo);
                        return;
                    }
                }

                int ltFlag = this.rdoLtFlag.SelectedIndex;
                string strFlag = ltFlag == 1 ? "【长期排班】" : "【临时排班】";
                if (DialogBox.Msg(strFlag + "\r\n\r\n确定是否导入？ " + doctInfo + deptInfo + roomInfo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    uiHelper.BeginLoading(this);
                    try
                    {
                        using (ProxyScheduling proxy = new ProxyScheduling())
                        {
                            if (proxy.Service.ImportScheduling(lstImport, ltFlag) > 0)
                            {
                                this.IsImport = true;
                                DialogBox.Msg("导入成功！");
                            }
                            else
                            {
                                DialogBox.Msg("导入失败。");
                            }
                        }
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        uiHelper.CloseLoading(this);
                    }
                }
            }
        }
        #endregion

        #region Confirm
        /// <summary>
        /// Confirm
        /// </summary>
        void Confirm()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string doctCode = string.Empty;
            string doctName = string.Empty;
            string deptCode = string.Empty;
            string deptName = string.Empty;
            string roomCode = string.Empty;
            string roomName = string.Empty;
            if (GetDate(ref startDate, ref endDate) == false) return;
            if (GetDoct(ref doctCode, ref doctName) == false) return;
            if (GetDept(ref deptCode, ref deptName) == false) return;
            if (GetRoom(ref roomCode, ref roomName) == false) return;
            string doctInfo = (string.IsNullOrEmpty(doctName) ? string.Empty : "\r\n\r\n医师: " + doctName);
            string deptInfo = (string.IsNullOrEmpty(deptName) ? string.Empty : "\r\n\r\n科室: " + deptName);
            string roomInfo = (string.IsNullOrEmpty(roomName) ? string.Empty : "\r\n\r\n诊间: " + roomName);
            int ltFlag = this.rdoLtFlag.SelectedIndex;
            string strFlag = ltFlag == 1 ? "【长期排班】" : "【临时排班】";
            if (DialogBox.Msg(strFlag + "\r\n\r\n是否审核:" + startDate.ToString("yyyy-MM-dd") + "至" + endDate.ToString("yyyy-MM-dd") + "时间段的排班信息？" + doctInfo + deptInfo + roomInfo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int ret = 0;
                DataTable dtQueue = null;
                bool isGenQueue = (GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从) ? true : false;
                try
                {
                    Dictionary<string, string> dicParm = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(doctCode)) dicParm.Add("doctCode", doctCode);
                    if (!string.IsNullOrEmpty(deptCode)) dicParm.Add("deptCode", deptCode);
                    if (!string.IsNullOrEmpty(roomCode)) dicParm.Add("roomCode", roomCode);

                    this.Cursor = Cursors.WaitCursor;
                    uiHelper.BeginLoading(this);
                    ProxyScheduling proxy = new ProxyScheduling();
                    ret = proxy.Service.ConfirmSchedulingDay(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), dicParm, GlobalLogin.objLogin.EmpNo, Utils.ServerTime(), ltFlag, isGenQueue, out dtQueue);
                    proxy = null;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    uiHelper.CloseLoading(this);
                }
                if (ret > 0)
                {
                    #region 生成接诊队列
                    decimal regDid = 0;
                    this.IsConfirm = true;
                    DialogBox.Msg(isGenQueue ? "排班数据审核成功！\r\n\r\n现在开始生成接诊队列，大约需要几分钟。" : "排班数据审核成功！");
                    if (isGenQueue && dtQueue != null)
                    {
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            uiHelper.BeginLoading(this);
                            List<decimal> lstRegDid = new List<decimal>();
                            foreach (DataRow dr in dtQueue.Rows)
                            {
                                regDid = Function.Dec(dr[EntityOpRegSchedulingDay.Columns.regDid]);
                                if (lstRegDid.IndexOf(regDid) < 0) lstRegDid.Add(regDid);
                            }
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
                            foreach (decimal item in lstRegDid)
                            {
                                regDid = item;
                                drr = dtQueue.Select(EntityOpRegSchedulingDay.Columns.regDid + "=" + regDid);
                                if (drr != null && drr.Length > 0)
                                {
                                    sortNo = 0;
                                    lstQueue = new List<EntityOpRegQueue>();
                                    lstDuty = new List<EntityClDuty>();
                                    foreach (DataRow dr in drr)
                                    {
                                        // amPm: 1 am; 2 pm; 3 nm; 4 mm(中午)
                                        shiftCode = string.Empty;
                                        amPm = dr[EntityOpRegSchedulingDayNumber.Columns.amPm].ToString();
                                        shiftCode = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).hisId;
                                        shiftEndTime = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).endTime;
                                        isBk = DataSourceShift.FirstOrDefault(t => t.shiftCode == Function.Int(amPm)).isBk;
                                        limitNum = Function.Int(dr[EntityOpRegSchedulingDayDate.Columns.limitNum]);
                                        if (isBk == 1)
                                        {
                                            if (limitNum <= 0) continue;
                                            freqNum = Function.Dec(dr[EntityOpRegSchedulingDayDate.Columns.freqNum]);
                                            if (freqNum == 0) freqNum = 6; // 默认： 6分钟/人
                                            dtmStart = Function.Datetime(dr[EntityOpRegSchedulingDayDate.Columns.startTime]);
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
                                    }
                                    if (lstQueue.Count > 0 || lstDuty.Count > 0)
                                    {
                                        using (ProxyScheduling proxy2 = new ProxyScheduling())
                                        {
                                            try
                                            {
                                                if (lstSuccess.IndexOf(regDid) < 0) lstSuccess.Add(regDid);
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
                        finally
                        {
                            this.Cursor = Cursors.Default;
                            uiHelper.CloseLoading(this);
                        }
                    }
                    #endregion
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (ret == 0)
                {
                    DialogBox.Msg("无排班数据需要审核。");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    DialogBox.Msg("审核失败。");
                }
            }
        }
        #endregion

        #region UnConfirm
        /// <summary>
        /// UnConfirm
        /// </summary>
        void UnConfirm()
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            string doctCode = string.Empty;
            string doctName = string.Empty;
            string deptCode = string.Empty;
            string deptName = string.Empty;
            string roomCode = string.Empty;
            string roomName = string.Empty;
            if (GetDate(ref startDate, ref endDate) == false) return;
            if (GetDoct(ref doctCode, ref doctName) == false) return;
            if (GetDept(ref deptCode, ref deptName) == false) return;
            if (GetRoom(ref roomCode, ref roomName) == false) return;
            string doctInfo = (string.IsNullOrEmpty(doctName) ? string.Empty : "\r\n\r\n医师: " + doctName);
            string deptInfo = (string.IsNullOrEmpty(deptName) ? string.Empty : "\r\n\r\n科室: " + deptName);
            string roomInfo = (string.IsNullOrEmpty(roomName) ? string.Empty : "\r\n\r\n诊间: " + roomName);

            int ltFlag = this.rdoLtFlag.SelectedIndex;
            string strFlag = ltFlag == 1 ? "【长期排班】" : "【临时排班】";
            if (DialogBox.Msg(strFlag + "\r\n\r\n是否反审核:" + startDate.ToString("yyyy-MM-dd") + "至" + endDate.ToString("yyyy-MM-dd") + "时间段的已审核排班记录？" + doctInfo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Dictionary<string, string> dicParm = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(doctCode)) dicParm.Add("doctCode", doctCode);
                if (!string.IsNullOrEmpty(deptCode)) dicParm.Add("deptCode", deptCode);
                if (!string.IsNullOrEmpty(roomCode)) dicParm.Add("roomCode", roomCode);

                ProxyScheduling proxy = new ProxyScheduling();
                int ret = proxy.Service.UnConfirmSchedulingDay(startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), dicParm, ltFlag, ((GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从) ? true : false));
                proxy = null;
                if (ret > 0)
                {
                    this.IsUnConfirm = true;
                    DialogBox.Msg("反审核排班数据成功！");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (ret == 0)
                {
                    DialogBox.Msg("无排班数据需要反审核。");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else if (ret == -99)
                {
                    DialogBox.Msg("所选时间段排班数据已经被预约挂号，不能反审核。");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    DialogBox.Msg("审核失败。");
                }
            }
        }
        #endregion

        #region LueFilterRoom
        /// <summary>
        /// LueFilterRoom
        /// </summary>
        internal void LueFilterRoom()
        {
            string deptCode = this.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                List<EntityDicDeptRoom> data = DataSourceRoom.FindAll(t => t.deptCode == deptCode);
                if (data != null)
                {
                    this.lueRoom.Properties.DataSource = data.ToArray();
                    this.lueRoom.Properties.SetSize();
                }
            }
        }
        #endregion

        private void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoType.SelectedIndex == 5)
            {
                this.dteStart.Enabled = true;
                this.dteEnd.Enabled = true;
                this.dteStart.Text = dtmNow.ToString("yyyy-MM-dd");
            }
            else
            {
                this.dteStart.Enabled = false;
                this.dteEnd.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.typeId == 1)
                Import();
            else if (this.typeId == 2)
                Confirm();
            else if (this.typeId == 3)
                UnConfirm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            this.lueRoom.Text = string.Empty;
            this.LueFilterRoom();
        }
    }
}
