using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 预约挂号控制类
    /// </summary>
    public class ctlRegisterBEdit : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterBEdit Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterBEdit)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// EditParm
        /// </summary>
        internal EntityBEditParm EditParm { get; set; }

        bool IsInit { get; set; }

        bool IsDong8 = true;

        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            this.IsInit = true;

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
            if (this.EditParm.DataSourceOpDept != null && this.EditParm.DataSourceOpDept.Count > 0) Viewer.lueDept.Properties.DataSource = this.EditParm.DataSourceOpDept.ToArray();
            Viewer.lueDept.Properties.SetSize();
            #endregion

            #region 号别
            Viewer.lueRegType.Properties.PopupWidth = 130;
            Viewer.lueRegType.Properties.PopupHeight = 200;
            Viewer.lueRegType.Properties.ValueColumn = EntityCodeReg.Columns.regCode;
            Viewer.lueRegType.Properties.DisplayColumn = EntityCodeReg.Columns.regName;
            Viewer.lueRegType.Properties.Essential = false;
            Viewer.lueRegType.Properties.IsShowColumnHeaders = true;
            Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regCode, 45);
            Viewer.lueRegType.Properties.ColumnWidth.Add(EntityCodeReg.Columns.regName, 85);
            Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regCode, "编码");
            Viewer.lueRegType.Properties.ColumnHeaders.Add(EntityCodeReg.Columns.regName, "名称");
            Viewer.lueRegType.Properties.ShowColumn = EntityCodeReg.Columns.regCode + "|" + EntityCodeReg.Columns.regName;
            Viewer.lueRegType.Properties.IsUseShowColumn = true;
            if (EditParm.DataSourceCodeReg != null && EditParm.DataSourceCodeReg.Count > 0) Viewer.lueRegType.Properties.DataSource = EditParm.DataSourceCodeReg.ToArray();
            Viewer.lueRegType.Properties.SetSize();
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
            if (this.EditParm.DataSourceOpDoct != null && this.EditParm.DataSourceOpDoct.Count > 0) Viewer.lueDoct.Properties.DataSource = this.EditParm.DataSourceOpDoct.ToArray();
            Viewer.lueDoct.Properties.SetSize();
            #endregion

            #region 专科
            Viewer.lueDeptReg.Properties.PopupWidth = 130;
            Viewer.lueDeptReg.Properties.PopupHeight = 160;
            Viewer.lueDeptReg.Properties.ValueColumn = EntityDicDeptReg.Columns.regCode;
            Viewer.lueDeptReg.Properties.DisplayColumn = EntityDicDeptReg.Columns.regName;
            Viewer.lueDeptReg.Properties.Essential = false;
            Viewer.lueDeptReg.Properties.IsShowColumnHeaders = true;
            Viewer.lueDeptReg.Properties.ColumnWidth.Add(EntityDicDeptReg.Columns.regCode, 45);
            Viewer.lueDeptReg.Properties.ColumnWidth.Add(EntityDicDeptReg.Columns.regName, 85);
            Viewer.lueDeptReg.Properties.ColumnHeaders.Add(EntityDicDeptReg.Columns.regCode, "编码");
            Viewer.lueDeptReg.Properties.ColumnHeaders.Add(EntityDicDeptReg.Columns.regName, "名称");
            Viewer.lueDeptReg.Properties.ShowColumn = EntityDicDeptReg.Columns.regCode + "|" + EntityDicDeptReg.Columns.regName;
            Viewer.lueDeptReg.Properties.IsUseShowColumn = true;
            #endregion

            #region parm.2

            if (IsDong8)
            {
                SetLueDept(this.EditParm.deptCode);
                Viewer.lueDept.Properties.ReadOnly = true;

                SetLueDoct(this.EditParm.doctCode);
                Viewer.lueDoct.Properties.ReadOnly = true;
                Viewer.lueDeptReg.Visible = false;
                Viewer.lueDoct.Visible = true;

                LueFilterRegType();
                this.IsInit = false;

                if (this.EditParm.regDate.IndexOf("星期") > 0)
                {
                    this.EditParm.regDate = this.EditParm.regDate.Substring(0, this.EditParm.regDate.IndexOf("星期")).Trim();
                }
                Viewer.dteRegDate.Text = this.EditParm.regDate;
            }
            else
            {
                if (this.EditParm.regDid > 0)
                {
                    SetLueRegType(this.EditParm.regCode);
                    Viewer.lueRegType.Properties.ReadOnly = true;

                    SetLueDept(this.EditParm.deptCode);
                    Viewer.lueDept.Properties.ReadOnly = true;

                    if (this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == this.EditParm.regCode).regFlag == "1")
                    {
                        SetLueExpert(this.EditParm.deptCode, this.EditParm.doctCode);
                        Viewer.lueDeptReg.Properties.ReadOnly = true;
                        Viewer.lueDeptReg.Visible = true;
                        Viewer.lueDoct.Visible = false;
                    }
                    else if (this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == this.EditParm.regCode).regFlag == "2")
                    {
                        SetLueDoct(this.EditParm.doctCode);
                        Viewer.lueDoct.Properties.ReadOnly = true;
                        Viewer.lueDeptReg.Visible = false;
                        Viewer.lueDoct.Visible = true;
                    }
                    LueFilterRegType();
                    this.IsInit = false;

                    if (this.EditParm.regDate.IndexOf("星期") > 0)
                    {
                        this.EditParm.regDate = this.EditParm.regDate.Substring(0, this.EditParm.regDate.IndexOf("星期")).Trim();
                    }

                    Viewer.dteRegDate.Text = this.EditParm.regDate;
                }
                else
                {
                    Viewer.lueDeptReg.Visible = false;
                    Viewer.lueDoct.Visible = true;
                }
            }
            #endregion

            SetEditValueChangedEvent(Viewer.plBottom);
            SetEditValueChangedEvent(Viewer.gvNo);

            if (GlobalParm.dicSysParameter.ContainsKey(38) && !string.IsNullOrEmpty(GlobalParm.dicSysParameter[38]))
            {
                List<string> lstClsCode = GlobalParm.dicSysParameter[38].Split('|').ToList();
                if (!string.IsNullOrEmpty(GlobalLogin.objLogin.clsCode) && lstClsCode.IndexOf(GlobalLogin.objLogin.clsCode) >= 0)
                {
                    Viewer.chkToday.Visible = true;
                }
                else
                {
                    Viewer.chkToday.Visible = false;
                }
            }
            if (IsDong8) Viewer.chkToday.Visible = false;

            this.IsInit = false;
        }
        #endregion

        #region GetPatInfo
        /// <summary>
        /// GetPatInfo
        /// </summary>
        internal void GetPatInfo()
        {
            string cardNo = Viewer.txtCardNo.Text.Trim();
            if (string.IsNullOrEmpty(cardNo))
            {
                DialogBox.Msg("请输入诊疗卡号。");
                Viewer.txtCardNo.Focus();
                return;
            }
            ProxyRegistration proxy = new ProxyRegistration();
            EntityPatientInfo patVo = proxy.Service.GetPatInfo(cardNo);
            proxy = null;
            if (patVo == null)
            {
                Viewer.txtCardNo.Tag = null;
                Viewer.lblPatName.Text = string.Empty;
                Viewer.lblSex.Text = string.Empty;
                Viewer.lblAge.Text = string.Empty;
                Viewer.lblIdNo.Text = string.Empty;
                Viewer.lblContactAddr.Text = string.Empty;
                Viewer.lblContactTel.Text = string.Empty;
                Viewer.txtTelNo.Text = string.Empty;

                DialogBox.Msg("没有满足卡号的病人记录。");
                Viewer.txtCardNo.Focus();
                Viewer.btnOk.Enabled = false;
            }
            else
            {
                Viewer.txtCardNo.Tag = patVo;
                Viewer.lblPatName.Text = patVo.name;
                if (patVo.sex == "1")
                    Viewer.lblSex.Text = "男";
                else if (patVo.sex == "2" || patVo.sex == "9")
                    Viewer.lblSex.Text = "女";
                else
                    Viewer.lblSex.Text = "未知";
                Viewer.lblAge.Text = CalcAge.GetAge(Function.Datetime(patVo.birth));
                Viewer.lblIdNo.Text = patVo.ID;
                Viewer.lblContactAddr.Text = patVo.addr;
                Viewer.lblContactTel.Text = patVo.tel;
                Viewer.txtTelNo.Text = patVo.tel;
                Viewer.btnOk.Enabled = true;
            }
        }
        #endregion

        #region SetLue

        #region SetLueRegType
        /// <summary>
        /// SetLueRegType
        /// </summary>
        /// <param name="regCode"></param>
        void SetLueRegType(string regCode)
        {
            Viewer.lueRegType.Properties.DBValue = regCode;
            Viewer.lueRegType.Properties.ForbidPoput = true;
            if (this.EditParm.DataSourceCodeReg != null && this.EditParm.DataSourceCodeReg.Count > 0)
            {
                EntityCodeReg vo = null;
                if (this.EditParm.DataSourceCodeReg.Any(t => t.regCode == regCode))
                {
                    vo = this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == regCode);
                    Viewer.lueRegType.Text = vo.regName;
                    Viewer.lblRegFee.Text = (vo.regFee == 0 ? string.Empty : "￥" + vo.regFee.ToString("0.00"));
                    Viewer.lblDoctFee.Text = (vo.doctFee == 0 ? string.Empty : "￥" + vo.doctFee.ToString("0.00"));
                }
                else
                {
                    Viewer.lueRegType.Text = string.Empty;
                    Viewer.lblRegFee.Text = string.Empty;
                    Viewer.lblDoctFee.Text = string.Empty;
                }
            }
            Viewer.lueRegType.Properties.DisplayValue = Viewer.lueRegType.Text;
            Viewer.lueRegType.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueDept
        /// <summary>
        /// SetLueDept
        /// </summary>
        /// <param name="deptCode"></param>
        void SetLueDept(string deptCode)
        {
            Viewer.lueDept.Properties.DBValue = deptCode;
            Viewer.lueDept.Properties.ForbidPoput = true;
            if (this.EditParm.DataSourceOpDept != null && this.EditParm.DataSourceOpDept.Count > 0)
            {
                if (this.EditParm.DataSourceOpDept.Any(t => t.deptCode == deptCode))
                    Viewer.lueDept.Text = (this.EditParm.DataSourceOpDept.FirstOrDefault(t => t.deptCode == deptCode)).deptName;
                else
                    Viewer.lueDept.Text = string.Empty;
            }
            Viewer.lueDept.Properties.DisplayValue = Viewer.lueDept.Text;
            Viewer.lueDept.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueDoct
        /// <summary>
        /// SetLueDoct
        /// </summary>
        /// <param name="doctCode"></param>
        void SetLueDoct(string doctCode)
        {
            Viewer.lueDoct.Properties.DBValue = doctCode;
            Viewer.lueDoct.Properties.ForbidPoput = true;
            if (this.EditParm.DataSourceOpDoct != null && this.EditParm.DataSourceOpDoct.Count > 0)
            {
                if (this.EditParm.DataSourceOpDoct.Any(t => t.operCode.ToUpper() == doctCode.ToUpper()))
                    Viewer.lueDoct.Text = (this.EditParm.DataSourceOpDoct.FirstOrDefault(t => t.operCode.ToUpper() == doctCode.ToUpper())).operName;
                else
                    Viewer.lueDoct.Text = string.Empty;
            }
            Viewer.lueDoct.Properties.DisplayValue = Viewer.lueDoct.Text;
            Viewer.lueDoct.Properties.ForbidPoput = false;
        }
        #endregion

        #region SetLueExpert
        /// <summary>
        /// SetLueExpert
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regCode"></param>
        void SetLueExpert(string deptCode, string regCode)
        {
            Viewer.lueDeptReg.Properties.DBValue = regCode;
            Viewer.lueDeptReg.Properties.ForbidPoput = true;
            if (this.EditParm.DataSourceDeptReg != null && this.EditParm.DataSourceDeptReg.Count > 0)
            {
                if (this.EditParm.DataSourceDeptReg.Any(t => t.deptCode == deptCode && t.regCode == regCode))
                    Viewer.lueDeptReg.Text = (this.EditParm.DataSourceDeptReg.FirstOrDefault(t => t.deptCode == deptCode && t.regCode == regCode)).regName;
                else
                    Viewer.lueDeptReg.Text = string.Empty;
            }
            Viewer.lueDeptReg.Properties.DisplayValue = Viewer.lueDeptReg.Text;
            Viewer.lueDeptReg.Properties.ForbidPoput = false;
        }
        #endregion

        #endregion

        #region LueFilter

        #region LueFilterRegType
        /// <summary>
        /// LueFilterRegType
        /// </summary>
        internal void LueFilterRegType()
        {
            bool isExpert = false;
            string regCode = Viewer.lueRegType.Properties.DBValue;
            if (!string.IsNullOrEmpty(regCode))
            {
                if (this.EditParm.DataSourceCodeReg != null && this.EditParm.DataSourceCodeReg.Exists(t => t.regCode == regCode))
                {
                    EntityCodeReg vo = null;
                    vo = this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == regCode);
                    if (vo.regFlag == "1") isExpert = true;
                    Viewer.lblRegFee.Text = (vo.regFee == 0 ? string.Empty : "￥" + vo.regFee.ToString("0.00"));
                    Viewer.lblDoctFee.Text = (vo.doctFee == 0 ? string.Empty : "￥" + vo.doctFee.ToString("0.00"));
                }
            }
            if (isExpert)
            {
                Viewer.lblDoct.Text = "专科:";
                Viewer.lueDeptReg.Visible = true;
                Viewer.lueDoct.Visible = false;
            }
            else
            {
                Viewer.lblDoct.Text = "医师:";
                Viewer.lueDeptReg.Visible = false;
                Viewer.lueDoct.Visible = true;
            }
        }
        #endregion

        #region LueFilterDoct
        /// <summary>
        /// LueFilterDoct
        /// </summary>
        internal void LueFilterDoct()
        {
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                EntityCodeOperator vo = null;
                List<EntityCodeOperator> tmpData = new List<EntityCodeOperator>();
                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    tmpData = this.EditParm.DataSourceOpDoct.FindAll(t => t.DeptNo == deptCode);
                }
                else
                {
                    //var doct = from var1 in GlobalDic.DataSourceDoctor
                    //           join var2 in GlobalDic.DataSourceEmpDept on var1.operCode equals var2.operCode
                    //           where (var2.deptCode == deptCode)
                    //           select new { var1.operCode, var1.operName, var2.pyCode, var2.wbCode };
                    var doct = from var1 in GlobalDic.DataSourceDoctor
                               join var2 in GlobalDic.DataSourceDefDeptEmployee on var1.operCode equals var2.operCode
                               where (var2.deptCode == deptCode)
                               select new { var1.operCode, var1.operName, var2.pyCode, var2.wbCode };
                    if (doct != null)
                    {
                        foreach (var item in doct)
                        {
                            vo = new EntityCodeOperator();
                            vo.operCode = item.operCode;
                            vo.operName = item.operName;
                            vo.pyCode = item.pyCode;
                            vo.wbCode = item.wbCode;
                            tmpData.Add(vo);
                        }
                    }
                }
                Viewer.lueDoct.Properties.DataSource = tmpData.ToArray();
                Viewer.lueDoct.Properties.SetSize();
            }
        }
        #endregion

        #region LueFilterExpert
        /// <summary>
        /// LueFilterExpert
        /// </summary>
        internal void LueFilterExpert()
        {
            string deptCode = Viewer.lueDept.Properties.DBValue;
            if (!string.IsNullOrEmpty(deptCode))
            {
                List<EntityDicDeptReg> data = this.EditParm.DataSourceDeptReg.FindAll(t => t.deptCode == deptCode);
                if (data != null)
                {
                    Viewer.lueDeptReg.Properties.DataSource = data.ToArray();
                    Viewer.lueDeptReg.Properties.SetSize();
                }
            }
        }
        #endregion


        #endregion

        #region GetRegDayNumber

        bool IsGet { get; set; }

        /// <summary>
        /// GetRegDayNumber
        /// </summary>
        internal void GetRegDayNumber()
        {
            try
            {
                if (IsInit || IsGet) return;
                IsGet = true;
                string regDate = Viewer.dteRegDate.Text;
                string regCode = Viewer.lueRegType.Properties.DBValue;
                string deptCode = Viewer.lueDept.Properties.DBValue;
                string doctCode = string.Empty;
                if (Viewer.lueDoct.Visible)
                    doctCode = Viewer.lueDoct.Properties.DBValue;
                else if (Viewer.lueDeptReg.Visible)
                    doctCode = Viewer.lueDeptReg.Properties.DBValue;
                if (IsDong8 && string.IsNullOrEmpty(regCode)) regCode = "*";
                if (string.IsNullOrEmpty(regDate) || string.IsNullOrEmpty(regCode) ||
                    string.IsNullOrEmpty(doctCode) || string.IsNullOrEmpty(doctCode))
                {
                    return;
                }
                List<EntityOpRegSchedulingDayNumber> dataNumber = null;
                if (IsDong8)
                {
                    Dong8 d8 = new Dong8();
                    dataNumber = d8.GetSchedulingDayNumber(regDate, deptCode, doctCode, this.EditParm.DataSourceOpDept);
                    d8 = null;
                }
                else
                {
                    using (ProxyRegistration proxy = new ProxyRegistration())
                    {
                        dataNumber = proxy.Service.GetRegDayNumber(this.EditParm.regType, regDate, regCode, deptCode, doctCode);
                    }
                }
                Viewer.gcNo.DataSource = dataNumber;
                Viewer.btnOk.Enabled = (dataNumber != null && dataNumber.Count > 0 ? true : false);
            }
            finally
            {
                IsGet = false;
            }
        }
        #endregion

        #region SetCheck
        /// <summary>
        /// 状态检查
        /// </summary>
        bool IsCellValueChanged { get; set; }
        /// <summary>
        /// SetCheck
        /// </summary>
        /// <param name="rowHandle"></param>
        internal void SetCheck(int rowHandle)
        {
            Viewer.gvNo.CloseEditor();
            if (this.IsCellValueChanged) return;
            this.IsCellValueChanged = true;

            if (Viewer.gvNo.GetRowCellValue(rowHandle, EntityOpRegSchedulingDayNumber.Columns.check).ToString() == "1")
            {
                for (int i = 0; i < Viewer.gvNo.RowCount; i++)
                {
                    if (i == rowHandle) continue;
                    Viewer.gvNo.SetRowCellValue(i, EntityOpRegSchedulingDayNumber.Columns.check, 0);
                }
            }
            Viewer.gvNo.CloseEditor();
            this.IsCellValueChanged = false;
        }
        #endregion

        #region 预约挂号
        /// <summary>
        /// 预约挂号
        /// </summary>
        /// <param name="isExit"></param>
        /// <returns></returns>
        internal bool RegBooking(bool isExit)
        {
            Viewer.gvNo.CloseEditor();
            EntityPatientInfo patVo = Viewer.txtCardNo.Tag as EntityPatientInfo;
            if (Viewer.txtCardNo.Text.Trim() == string.Empty || Viewer.txtCardNo.Tag == null ||
                Viewer.gvNo.RowCount == 0 || patVo == null)
            {
                DialogBox.Msg("请输入卡号，并选择号源时间段。");
                return false;
            }
            bool isToday = Viewer.chkToday.Checked;
            string doctName = string.Empty;
            EntityOpRegBooking bookingVo = new EntityOpRegBooking();
            bookingVo.cardNo = patVo.cardNo;
            bookingVo.pid = patVo.pid.ToString();
            bookingVo.patName = patVo.name;
            bookingVo.regDate = Viewer.dteRegDate.Text;
            bookingVo.regCode = Viewer.lueRegType.Properties.DBValue;
            bookingVo.deptCode = Viewer.lueDept.Properties.DBValue;
            if (Viewer.lueDoct.Visible)
            {
                bookingVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                if (string.IsNullOrEmpty(bookingVo.doctCode))
                {
                    DialogBox.Msg("请选择医师。");
                    Viewer.lueDoct.Focus();
                    return false;
                }
                doctName = Viewer.lueDoct.Text;
            }
            else if (Viewer.lueDeptReg.Visible)
            {
                bookingVo.doctCode = Viewer.lueDeptReg.Properties.DBValue;
                if (string.IsNullOrEmpty(bookingVo.doctCode))
                {
                    DialogBox.Msg("请选择专科号。");
                    Viewer.lueDeptReg.Focus();
                    return false;
                }
                doctName = Viewer.lueDeptReg.Text;
            }
            if (string.IsNullOrEmpty(bookingVo.regDate))
            {
                DialogBox.Msg("请选择预约时间。");
                Viewer.dteRegDate.Focus();
                return false;
            }
            if (!isToday && GlobalHospital.Current == EnumHospitalCode.东莞八院)
            {
                if (Convert.ToDateTime(bookingVo.regDate) <= Convert.ToDateTime(Utils.ServerTime().ToShortDateString()))
                {
                    //DialogBox.Msg("预约时间必须是明天及以后，请重新选择预约时间。");
                    //Viewer.dteRegDate.Focus();
                    //return false;
                }
            }
            if (string.IsNullOrEmpty(bookingVo.deptCode))
            {
                DialogBox.Msg("请选择科室。");
                Viewer.lueDept.Focus();
                return false;
            }

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                if (proxy.Service.IsRegBooking(bookingVo))
                {
                    DialogBox.Msg("当前患者：于 " + bookingVo.regDate + "->" + Viewer.lueRegType.Text + "->" + Viewer.lueDept.Text + "->" + doctName + "\r\n\r\n已预约，请重新选择预约条件。");
                    return false;
                }
            }

            if (!isToday && GlobalHospital.Current == EnumHospitalCode.东莞八院)
            {
                // 博爱要求：为了方便管理，下午4点后，只能预约后天及以后的号
                DateTime dtmNow = Utils.ServerTime();
                if (dtmNow > Function.Datetime(dtmNow.ToString("yyyy-MM-dd") + " 16:00:00"))
                {
                    if (bookingVo.regDate == dtmNow.AddDays(1).ToString("yyyy-MM-dd"))
                    {
                        //DialogBox.Msg("当日16：00后，只能预约后天及以后的号源。");
                        //return false;
                    }
                }
            }

            bool isCheck = false;
            for (int i = 0; i < Viewer.gvNo.RowCount; i++)
            {
                if (GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.check) == "1")
                {
                    if (!isToday)
                    {
                        if (Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.surplusNum)) == 0)
                        {
                            DialogBox.Msg("所选时间段号源已用完，请重新选择。");
                            return false;
                        }
                    }
                    bookingVo.regDid = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.regDid));
                    bookingVo.amPm = Function.Int(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.amPm));
                    bookingVo.startTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.startTime);
                    bookingVo.endTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.endTime);
                    bookingVo.numberSerNo = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.numberSerNo));
                    isCheck = true;
                    break;
                }
            }
            if (isCheck == false)
            {
                DialogBox.Msg("请选择号源时间段。");
                return false;
            }
            bookingVo.doctName = doctName;
            bookingVo.regType = this.EditParm.regType;      // 3 电话预约; 4 现场预约
            bookingVo.recorderCode = GlobalLogin.objLogin.EmpNo;
            bookingVo.recordDate = Utils.ServerTime();
            bookingVo.status = 0;
            bookingVo.contactTel = Viewer.txtTelNo.Text.Trim();
            bookingVo.isVirtual = isToday ? 1 : 0;          // 1 虚拟预约(挂号)
            bookingVo.isSendMsg = isToday ? false : true;   // 虚拟预约(挂号)不发短信

            #region ws 预约
            Dong8 d8 = new Dong8();
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            string resultDesc = string.Empty;
            string typno = "900211";
            string userInfo = Function.ReadConfigXml("userInfo");
            string providerId = Function.ReadConfigXml("providerId");
            string orgId = Function.ReadConfigXml("orgId");
            string producerId = d8.ConvertDoctCode(bookingVo.doctCode, "");
            if (string.IsNullOrEmpty(producerId))
            {
                DialogBox.Msg("医师工号与平台转换失败。");
                return false;
            }
            // 诊疗卡号
            string idType = (GlobalHospital.Current == EnumHospitalCode.东莞八院 ? "5418009" : "5418001");
            if (GlobalHospital.Current == EnumHospitalCode.东莞八院) patVo.ID = patVo.cardNo;
            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                if (string.IsNullOrEmpty(patVo.ID) || patVo.ID.Trim() == "")
                {
                    DialogBox.Msg("身份证号不能为空！");
                    return false;
                }

                if (this.Viewer.txtTelNo.Text.Trim() == "")
                {
                    DialogBox.Msg("联系电话不能为空！");
                    Viewer.txtTelNo.Focus();
                    return false;
                }
            }
            if (patVo.sex == "1")
                patVo.sex = "302001";
            else if (patVo.sex == "2")
                patVo.sex = "302003";
            else
                patVo.sex = "302006";

            xmlIn += "<base>" + Environment.NewLine;
            xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
            xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
            xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
            xmlIn += string.Format("<producerId>{0}</producerId>", producerId) + Environment.NewLine;
            xmlIn += string.Format("<customer>{0}</customer>", patVo.name) + Environment.NewLine;
            xmlIn += string.Format("<diagnosisNumber>{0}</diagnosisNumber>", patVo.cardNo) + Environment.NewLine;
            xmlIn += string.Format("<sex>{0}</sex>", patVo.sex) + Environment.NewLine;
            xmlIn += string.Format("<brithday>{0}</brithday>", Function.Datetime(patVo.birth).ToString("yyyy-MM-dd") == "0001-01-01" ? string.Empty : Function.Datetime(patVo.birth).ToString("yyyy-MM-dd")) + Environment.NewLine;
            xmlIn += string.Format("<credentialsType>{0}</credentialsType>", idType) + Environment.NewLine;
            xmlIn += string.Format("<credentialsNum>{0}</credentialsNum>", patVo.ID) + Environment.NewLine;
            xmlIn += string.Format("<phoneNum>{0}</phoneNum>", patVo.tel) + Environment.NewLine;
            xmlIn += string.Format("<workDate>{0}</workDate>", bookingVo.regDate) + Environment.NewLine;
            xmlIn += string.Format("<startTime>{0}</startTime>", bookingVo.startTime) + Environment.NewLine;
            xmlIn += string.Format("<endTime>{0}</endTime>", bookingVo.endTime) + Environment.NewLine;
            xmlIn += "</base>" + Environment.NewLine;
            // log
            Log.Output("功能号: " + typno + Environment.NewLine + xmlIn);
            try
            {
                WebService ws = new WebService();
                // 分配号源
                xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                // log
                Log.Output("功能号: " + typno + Environment.NewLine + xmlOut);
                resultCode = xmlOut.Split('|')[0];
                resultDesc = xmlOut.Split('|')[1];
                xmlOut = xmlOut.Split('|')[2].Replace("<?xml version='1.0' encoding='GBK'?>", "");
                if ((resultCode == "0" || resultCode == "5201") && !string.IsNullOrEmpty(xmlOut))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(xmlOut);
                    XmlNodeList xnl = doc.SelectNodes("/base");
                    XmlNode bookNode = xnl[0].SelectSingleNode("bookSubscribeProducer");
                    bookingVo.psOrdNum = bookNode.SelectSingleNode("subscribeNo").InnerText;

                    #region 锁号

                    typno = "900213";
                    string tranNo = bookingVo.pid + DateTime.Now.ToString("yyyyMMddHHmm");
                    xmlIn = string.Empty;
                    xmlIn += "<base>" + Environment.NewLine;
                    xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
                    xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
                    xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
                    xmlIn += string.Format("<producerId>{0}</producerId>", producerId) + Environment.NewLine;
                    xmlIn += string.Format("<workDate>{0}</workDate>", bookingVo.regDate) + Environment.NewLine;
                    xmlIn += string.Format("<subscribeNo>{0}</subscribeNo>", bookingVo.psOrdNum) + Environment.NewLine;
                    xmlIn += string.Format("<tranNo>{0}</tranNo>", tranNo) + Environment.NewLine;
                    xmlIn += string.Format("<sumAmount>0.00</sumAmount>") + Environment.NewLine;
                    xmlIn += string.Format("<accountType></accountType>") + Environment.NewLine;
                    xmlIn += string.Format("<accountTranNo></accountTranNo>") + Environment.NewLine;
                    xmlIn += string.Format("<socailSecuNum></socailSecuNum>") + Environment.NewLine;
                    xmlIn += string.Format("<socailSecuNo></socailSecuNo>") + Environment.NewLine;
                    xmlIn += string.Format("<socailSecuAmount></socailSecuAmount>") + Environment.NewLine;
                    xmlIn += string.Format("<subscribeType>3</subscribeType>") + Environment.NewLine;
                    xmlIn += "</base>" + Environment.NewLine;
                    Log.Output("功能号: " + typno + Environment.NewLine + xmlIn);
                    int affectRows = 0;
                    bool isSuccess = false;
                    try
                    {
                        xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                        // log
                        Log.Output("功能号: " + typno + Environment.NewLine + xmlOut);
                        resultCode = xmlOut.Split('|')[0];
                        xmlOut = xmlOut.Split('|')[2];
                        xmlOut = xmlOut.Replace("<?xml version='1.0' encoding='GBK'?>", "");
                        if (resultCode == "0")
                        {
                            using (ProxyRegistration proxyReg = new ProxyRegistration())
                            {
                                // 保存预约记录
                                affectRows = proxyReg.Service.RegBookingD8(bookingVo);
                            }
                            // 预约成功
                            isSuccess = affectRows > 0 ? true : false;
                            //if (isSuccess)
                            //{
                            //    typno = "900512";
                            //    xmlIn = string.Empty;
                            //    xmlIn += "<base>" + Environment.NewLine;
                            //    xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
                            //    xmlIn += string.Format("<acceptDate>{0}</acceptDate>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + Environment.NewLine;
                            //    xmlIn += string.Format("<verifCode>{0}</verifCode>", "") + Environment.NewLine;
                            //    xmlIn += "<subscribeNos>" + Environment.NewLine;
                            //    // 预约号
                            //    xmlIn += string.Format("<subscribeNo>{0}</subscribeNo>", bookingVo.psOrdNum) + Environment.NewLine;
                            //    xmlIn += "</subscribeNos>" + Environment.NewLine;
                            //    xmlIn += "</base>" + Environment.NewLine;
                            //    Log.Output("功能号: " + typno + Environment.NewLine + xmlIn);
                            //    xmlOut = ws.medicalInstitutionsSubscribeService(typno, xmlIn, userInfo);
                            //    Log.Output("功能号: " + typno + Environment.NewLine + xmlOut);
                            //}
                        }
                        else
                        {
                            DialogBox.Msg(d8.ResultMessage(resultCode));
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.OutPutException(ex);
                        isSuccess = false;
                    }

                    if (isSuccess == false)
                    {
                        // 取消分配号源
                        typno = "900214";
                        xmlIn = string.Empty;
                        xmlIn += "<base>" + Environment.NewLine;
                        xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
                        xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
                        xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
                        xmlIn += string.Format("<producerId>{0}</producerId>", producerId) + Environment.NewLine;
                        xmlIn += string.Format("<workDate>{0}</workDate>", bookingVo.regDate) + Environment.NewLine;
                        xmlIn += string.Format("<subscribeNo>{0}</subscribeNo>", bookingVo.psOrdNum) + Environment.NewLine;
                        xmlIn += "</base>" + Environment.NewLine;
                        // log
                        Log.Output("功能号: " + typno + Environment.NewLine + xmlIn);
                        try
                        {
                            xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                            // log
                            Log.Output("功能号: " + typno + Environment.NewLine + xmlOut);
                            resultCode = xmlOut.Split('|')[0];
                            resultDesc = xmlOut.Split('|')[1];
                            xmlOut = xmlOut.Split('|')[2].Replace("<?xml version='1.0' encoding='GBK'?>", "");
                            if (resultCode == "0")
                            {

                            }
                            else
                            {
                                DialogBox.Msg(d8.ResultMessage(resultCode));
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionLog.OutPutException(ex);
                        }
                        DialogBox.Msg("预约失败。");
                        return false;
                    }
                    else
                    {
                        Viewer.ValueChanged = false;
                        Viewer.IsBooking = true;
                        DialogBox.Msg("预约成功！");
                        Viewer.Close();
                        return true;
                    }
                    #endregion
                }
                else
                {
                    DialogBox.Msg(d8.ResultMessage(resultCode));
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            finally
            {

            }

            #endregion

            return false;
        }
        #endregion

        #region 预约挂号2
        /// <summary>
        /// 预约挂号2
        /// </summary>
        /// <param name="isExit"></param>
        /// <returns></returns>
        internal bool RegBooking2(bool isExit)
        {
            Viewer.gvNo.CloseEditor();
            EntityPatientInfo patVo = Viewer.txtCardNo.Tag as EntityPatientInfo;
            if (Viewer.txtCardNo.Text.Trim() == string.Empty || Viewer.txtCardNo.Tag == null ||
                Viewer.gvNo.RowCount == 0 || patVo == null)
            {
                DialogBox.Msg("请输入卡号，并选择号源时间段。");
                return false;
            }
            bool isToday = Viewer.chkToday.Checked;
            string doctName = string.Empty;
            EntityOpRegBooking bookingVo = new EntityOpRegBooking();
            bookingVo.cardNo = patVo.cardNo;
            bookingVo.pid = patVo.pid.ToString();
            bookingVo.patName = patVo.name;
            bookingVo.regDate = Viewer.dteRegDate.Text;
            bookingVo.regCode = Viewer.lueRegType.Properties.DBValue;
            bookingVo.deptCode = Viewer.lueDept.Properties.DBValue;
            if (Viewer.lueDoct.Visible)
            {
                bookingVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                if (string.IsNullOrEmpty(bookingVo.doctCode))
                {
                    DialogBox.Msg("请选择医师。");
                    Viewer.lueDoct.Focus();
                    return false;
                }
                doctName = Viewer.lueDoct.Text;
            }
            else if (Viewer.lueDeptReg.Visible)
            {
                bookingVo.doctCode = Viewer.lueDeptReg.Properties.DBValue;
                if (string.IsNullOrEmpty(bookingVo.doctCode))
                {
                    DialogBox.Msg("请选择专科号。");
                    Viewer.lueDeptReg.Focus();
                    return false;
                }
                doctName = Viewer.lueDeptReg.Text;
            }

            if (string.IsNullOrEmpty(bookingVo.regDate))
            {
                DialogBox.Msg("请选择预约时间。");
                Viewer.dteRegDate.Focus();
                return false;
            }
            if (!isToday && GlobalHospital.Current == EnumHospitalCode.东莞八院)
            {
                if (Convert.ToDateTime(bookingVo.regDate) <= Convert.ToDateTime(Utils.ServerTime().ToShortDateString()))
                {
                    //DialogBox.Msg("预约时间必须是明天及以后，请重新选择预约时间。");
                    //Viewer.dteRegDate.Focus();
                    //return false;
                }
            }
            if (string.IsNullOrEmpty(bookingVo.regCode))
            {
                DialogBox.Msg("请指定号别。");
                Viewer.lueRegType.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(bookingVo.deptCode))
            {
                DialogBox.Msg("请选择科室。");
                Viewer.lueDept.Focus();
                return false;
            }

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                if (proxy.Service.IsRegBooking(bookingVo))
                {
                    DialogBox.Msg("当前患者：于 " + bookingVo.regDate + "->" + Viewer.lueRegType.Text + "->" + Viewer.lueDept.Text + "->" + doctName + "\r\n\r\n已预约，请重新选择预约条件。");
                    return false;
                }
            }

            if (!isToday && GlobalHospital.Current == EnumHospitalCode.东莞八院)
            {
                // 博爱要求：为了方便管理，下午4点后，只能预约后天及以后的号
                DateTime dtmNow = Utils.ServerTime();
                if (dtmNow > Function.Datetime(dtmNow.ToString("yyyy-MM-dd") + " 16:00:00"))
                {
                    if (bookingVo.regDate == dtmNow.AddDays(1).ToString("yyyy-MM-dd"))
                    {
                        //DialogBox.Msg("当日16：00后，只能预约后天及以后的号源。");
                        //return false;
                    }
                }
            }

            bool isCheck = false;
            for (int i = 0; i < Viewer.gvNo.RowCount; i++)
            {
                if (GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.check) == "1")
                {
                    if (!isToday)
                    {
                        if (Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.surplusNum)) == 0)
                        {
                            DialogBox.Msg("所选时间段号源已用完，请重新选择。");
                            return false;
                        }
                    }
                    bookingVo.regDid = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.regDid));
                    bookingVo.amPm = Function.Int(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.amPm));
                    bookingVo.startTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.startTime);
                    bookingVo.endTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.endTime);
                    bookingVo.numberSerNo = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegSchedulingDayNumber.Columns.numberSerNo));
                    isCheck = true;
                    break;
                }
            }
            if (isCheck == false)
            {
                DialogBox.Msg("请选择号源时间段。");
                return false;
            }
            bookingVo.doctName = doctName;
            bookingVo.regType = this.EditParm.regType;      // 3 电话预约; 4 现场预约
            bookingVo.recorderCode = GlobalLogin.objLogin.EmpNo;
            bookingVo.recordDate = Utils.ServerTime();
            bookingVo.status = 0;
            bookingVo.contactTel = Viewer.txtTelNo.Text.Trim();
            bookingVo.isVirtual = isToday ? 1 : 0;          // 1 虚拟预约(挂号)
            bookingVo.isSendMsg = isToday ? false : true;   // 虚拟预约(挂号)不发短信

            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                int ret = proxy.Service.RegBooking(bookingVo);
                if (ret > 0)
                {
                    Viewer.ValueChanged = false;
                    Viewer.IsBooking = true;
                    DialogBox.Msg("预约成功！");
                    Viewer.Close();
                    return true;
                }
                else
                {
                    if (ret == -10)
                    {
                        GetRegDayNumber();
                        DialogBox.Msg("预约失败-->号源已预约完。");
                    }
                    else
                    {
                        DialogBox.Msg("预约失败。");
                    }
                }
            }
            return false; ;
        }
        #endregion

        #region ModifyTelNo
        /// <summary>
        /// ModifyTelNo
        /// </summary>
        internal void ModifyTelNo()
        {
            EntityPatientInfo patVo = Viewer.txtCardNo.Tag as EntityPatientInfo;
            if (Viewer.txtCardNo.Text.Trim() == string.Empty || Viewer.txtCardNo.Tag == null || patVo == null)
            {
                return;
            }
            string telNo = Viewer.txtTelNo.Text.Trim();
            if (string.IsNullOrEmpty(telNo))
            {
                DialogBox.Msg("请输入电话号码。");
                return;
            }
            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                if (proxy.Service.UpdatePatientTelNo(patVo.pid, telNo) > 0)
                {
                    DialogBox.Msg("修改电话号码成功！");
                }
                else
                {
                    DialogBox.Msg("修改电话号码失败。");
                }
            }
        }
        #endregion

        #endregion
    }

    #region EntityBEditParm
    /// <summary>
    /// EntityBEditParm
    /// </summary>
    public class EntityBEditParm
    {
        /// <summary>
        /// 医师资料
        /// </summary>
        public List<EntityOpRegSchedulingDoct> DataSourceDoct { get; set; }

        /// <summary>
        /// 号别字典
        /// </summary>
        public List<EntityCodeReg> DataSourceCodeReg { get; set; }

        /// <summary>
        /// 专科号别
        /// </summary>
        public List<EntityDicDeptReg> DataSourceDeptReg { get; set; }

        /// <summary>
        /// 门诊科室
        /// </summary>
        public List<EntityCodeDepartment> DataSourceOpDept { get; set; }

        /// <summary>
        /// 门诊医生
        /// </summary>
        public List<EntityCodeOperator> DataSourceOpDoct { get; set; }

        /**************/

        /// <summary>
        /// 3 电话预约; 4 现场预约
        /// </summary>
        public int regType { get; set; }
        public decimal regDid { get; set; }
        public string regDate { get; set; }
        public string regCode { get; set; }
        public string deptCode { get; set; }
        public string doctCode { get; set; }
    }
    #endregion

}
