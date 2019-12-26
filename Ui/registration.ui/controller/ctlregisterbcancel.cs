using Common.Controls;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 取消挂号控制类
    /// </summary>
    public class ctlRegisterBCancel : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterCancel Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterCancel)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// 参数
        /// </summary>
        internal EntityBEditParm EditParm { get; set; }

        /// <summary>
        /// 东8
        /// </summary>
        internal bool IsDong8 = true;
        
        #endregion

        #region 方法

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

                Viewer.gcNo.DataSource = null;

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

                using (ProxyRegistration proxy1 = new ProxyRegistration())
                {
                    EntityCodeReg regVo = null;
                    List<EntityOpRegBooking> data = proxy1.Service.GetRegBookingByCardNo(patVo.cardNo);
                    EntityOpRegBooking item = null;
                    for (int i = data.Count - 1; i >= 0; i--)
                    {
                        item = data[i];
                        if (item.regType != this.EditParm.regType)
                        {
                            data.RemoveAt(i);
                            continue;
                        }
                        if (this.EditParm.DataSourceOpDept.Exists(t => t.deptCode == item.deptCode))
                        {
                            item.deptName = this.EditParm.DataSourceOpDept.FirstOrDefault(t => t.deptCode == item.deptCode).deptName;
                        }
                        if (this.EditParm.DataSourceCodeReg.Exists(t => t.regCode == item.regCode))
                        {
                            regVo = this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode);
                            item.regCodeName = regVo.regName;
                            item.regFee = (regVo.regFee == 0 ? string.Empty : "￥" + regVo.regFee.ToString("0.00"));
                            item.doctFee = (regVo.doctFee == 0 ? string.Empty : "￥" + regVo.doctFee.ToString("0.00"));
                            if (this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode).regFlag == "1")
                            {
                                if (this.EditParm.DataSourceDeptReg.Exists(t => t.regCode == item.doctCode))
                                {
                                    item.doctName = this.EditParm.DataSourceDeptReg.FirstOrDefault(t => t.deptCode == item.deptCode && t.regCode == item.doctCode).regName;
                                }
                            }
                            else if (this.EditParm.DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode).regFlag == "2")
                            {
                                if (GlobalDic.DataSourceEmployee.Exists(t => t.operCode.ToUpper() == item.doctCode.ToUpper()))
                                {
                                    item.doctName = GlobalDic.DataSourceEmployee.FirstOrDefault(t => t.operCode.ToUpper() == item.doctCode.ToUpper()).operName;
                                }
                            }
                        }
                    }

                    Viewer.gcNo.DataSource = data;
                    if (data != null && data.Count > 0) Viewer.btnOk.Enabled = true;
                }
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

        #region SetRowCellStyle
        /// <summary>
        /// SetRowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int status = Function.Int(GetFieldValueStr(Viewer.gvNo, e.RowHandle, EntityOpRegBooking.Columns.status));
            if (status == -2)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            else if (status == -1)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else if (status == 1)
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Crimson;
            }
        }
        #endregion

        #region SetCellDisable
        /// <summary>
        /// SetCellDisable
        /// </summary>
        /// <param name="rowHandle"></param>
        internal void SetCellDisable(int rowHandle)
        {
            int status = Function.Int(GetFieldValueStr(Viewer.gvNo, rowHandle, EntityOpRegBooking.Columns.status));
            if (status == 0)
            {
                Viewer.colCheck.OptionsColumn.AllowEdit = true;
            }
            else
            {
                Viewer.colCheck.OptionsColumn.AllowEdit = false;
            }
        }
        #endregion

        #region CancelRegBooking
        /// <summary>
        /// CancelRegBooking
        /// </summary>
        internal void CancelRegBooking()
        {
            Viewer.gvNo.CloseEditor();
            EntityPatientInfo patVo = Viewer.txtCardNo.Tag as EntityPatientInfo;
            if (Viewer.txtCardNo.Text.Trim() == string.Empty || Viewer.txtCardNo.Tag == null || patVo == null)
            {
                DialogBox.Msg("请输入卡号.");
                return;
            }

            EntityOpRegBooking bookingVo = new EntityOpRegBooking();
            bool isCheck = false;
            for (int i = 0; i < Viewer.gvNo.RowCount; i++)
            {
                if (GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.check) == "1")
                {
                    if (Convert.ToDateTime(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.regDate)) <= Convert.ToDateTime(Utils.ServerTime().ToShortDateString()))
                    {
                        //DialogBox.Msg("取消的预约记录必须是明天及以后，请重新选择。");
                        //Viewer.gvNo.SetRowCellValue(i, EntityOpRegBooking.Columns.check, 0);
                        //Viewer.gvNo.Focus();
                        //return;
                    }
                    bookingVo.serNo = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.serNo));
                    bookingVo.regDid = Function.Dec(GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.regDid));
                    bookingVo.startTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.startTime);
                    bookingVo.endTime = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.endTime);
                    bookingVo.rowHandle = i;
                    bookingVo.regType = this.EditParm.regType;
                    bookingVo.doctName = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.doctName);
                    bookingVo.doctCode = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.doctCode);
                    bookingVo.psOrdNum = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.psOrdNum);
                    bookingVo.regDate = GetFieldValueStr(Viewer.gvNo, i, EntityOpRegBooking.Columns.regDate);
                    isCheck = true;
                    break;
                }
            }
            if (isCheck == false)
            {
                DialogBox.Msg("请选择取消预约的记录。");
                return;
            }

            if (DialogBox.Msg("请确定是否取消预约？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bookingVo.cancelReason = Viewer.txtReason.Text.Trim();
                bookingVo.cancelOperCode = GlobalLogin.objLogin.EmpNo;
                bookingVo.cancelDate = Utils.ServerTime();
                bookingVo.status = -1;
                bookingVo.patName = patVo.name;
                bookingVo.contactTel = patVo.tel;

                if (IsDong8)
                {
                    Dong8 d8 = new Dong8();
                    string xmlIn = string.Empty;
                    string xmlOut = string.Empty;
                    string resultCode = string.Empty;
                    string resultDesc = string.Empty;
                    string typno = "900214";
                    string userInfo = Function.ReadConfigXml("userInfo");
                    string providerId = Function.ReadConfigXml("providerId");
                    string orgId = Function.ReadConfigXml("orgId");
                    string producerId = d8.ConvertDoctCode(bookingVo.doctCode, "");

                    xmlIn += "<base>" + Environment.NewLine;
                    xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
                    xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
                    xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
                    xmlIn += string.Format("<producerId>{0}</producerId>", producerId) + Environment.NewLine;
                    xmlIn += string.Format("<workDate>{0}</workDate>", bookingVo.regDate) + Environment.NewLine;
                    xmlIn += string.Format("<subscribeNo>{0}</subscribeNo>", bookingVo.psOrdNum) + Environment.NewLine;
                    xmlIn += "</base>" + Environment.NewLine;
                    // log
                    Log.Output(xmlIn);
                    try
                    {
                        WebService ws = new WebService();
                        xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                        // log
                        Log.Output(xmlOut);
                        resultCode = xmlOut.Split('|')[0];
                        resultDesc = xmlOut.Split('|')[1];
                        xmlOut = xmlOut.Split('|')[2].Replace("<?xml version='1.0' encoding='GBK'?>", "");
                        if (resultCode == "0")
                        {
                            using (ProxyRegistration proxy = new ProxyRegistration())
                            {
                                if (proxy.Service.CancelRegBookingD8(bookingVo) > 0)
                                {
                                    Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.check, 0);
                                    Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.status, -1);
                                    Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.statusName, "取消");
                                    DialogBox.Msg("取消预约成功！");
                                    Viewer.IsCancel = true;
                                }
                                else
                                {
                                    DialogBox.Msg("取消预约失败。");
                                }
                            }
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
                }
                else
                {
                    using (ProxyRegistration proxy = new ProxyRegistration())
                    {
                        if (proxy.Service.CancelRegBooking(bookingVo) > 0)
                        {
                            Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.check, 0);
                            Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.status, -1);
                            Viewer.gvNo.SetRowCellValue(bookingVo.rowHandle, EntityOpRegBooking.Columns.statusName, "取消");
                            DialogBox.Msg("取消预约成功！");
                            Viewer.IsCancel = true;
                        }
                        else
                        {
                            DialogBox.Msg("取消预约失败。");
                        }
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}
