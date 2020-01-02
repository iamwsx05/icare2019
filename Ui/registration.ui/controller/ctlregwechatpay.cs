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
    /// ctlRegWeChatPay
    /// </summary>
    public class ctlRegWeChatPay : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegWeChatPay Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegWeChatPay)child;
        }
        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            #region dic
            DataTable dt = null;
            List<EntityCodeReg> DataSourceCodeReg = new List<EntityCodeReg>();
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                dt = proxy.Service.SelectFullTable(new EntityCodeReg());
                DataSourceCodeReg.AddRange(EntityTools.ConvertToEntityList<EntityCodeReg>(dt));
                if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0)
                {
                    for (int i = DataSourceCodeReg.Count - 1; i >= 0; i--)
                    {
                        if (DataSourceCodeReg[i].status == 0) DataSourceCodeReg.RemoveAt(i);
                    }
                }
            }
            #endregion

            #region lue

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
            if (DataSourceCodeReg != null && DataSourceCodeReg.Count > 0) Viewer.lueRegType.Properties.DataSource = DataSourceCodeReg.ToArray();
            Viewer.lueRegType.Properties.SetSize();
            #endregion

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
            int status = Function.Int(Viewer.gvNo.GetRowCellValue(e.RowHandle, EntityOpRegBooking.Columns.status));
            int feeStatus = Function.Int(Viewer.gvNo.GetRowCellValue(e.RowHandle, EntityOpRegBooking.Columns.feeStatus));
            if (feeStatus == 0)
            {
                if (status >= 0)
                    e.Appearance.ForeColor = Color.Blue;
                else
                    e.Appearance.ForeColor = Color.Black;
            }
            else if (feeStatus == -1)
            {
                e.Appearance.ForeColor = Color.FromArgb(192, 0, 0); //Color.Blue;
            }
            else if (feeStatus == 1)
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 176, 80);  //Color.Crimson;
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

                queryVo.dateType = Viewer.cboDateType.SelectedIndex;
                queryVo.startDate = Viewer.dteStart.Text;
                queryVo.endDate = Viewer.dteEnd.Text;
                if ((!string.IsNullOrEmpty(queryVo.startDate) && !string.IsNullOrEmpty(queryVo.endDate)) &&
                    (Convert.ToDateTime(queryVo.startDate) > Convert.ToDateTime(queryVo.endDate)))
                {
                    DialogBox.Msg("查询开始日期不能大于结束日期,请重新输入。");
                    Viewer.dteStart.Focus();
                    return;
                }
                queryVo.deptCode = Viewer.lueDept.Properties.DBValue;
                queryVo.doctCode = Viewer.lueDoct.Properties.DBValue;
                queryVo.regCode = Viewer.lueRegType.Properties.DBValue;
                queryVo.cardNo = Viewer.txtCardNo.Text.Trim();
                if (!string.IsNullOrEmpty(queryVo.cardNo)) queryVo.cardNo += "%";

                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    int num1 = 0;
                    int num2 = 0;
                    int num3 = 0;
                    decimal totalMny = 0;
                    string info = "收费:{0}人 退费:{1}人 未收费:{2}";
                    List<EntityOpRegBooking> data = proxy.Service.GetRegWeChatPay(queryVo);
                    if (data != null)
                    {
                        // 防止身份证号导出变成科学计数法
                        foreach (EntityOpRegBooking item in data)
                        {
                            item.idNo = " " + item.idNo + " ";
                            if (item.feeStatus == 1)
                                num1++;
                            else if (item.feeStatus == -1)
                                num2++;
                            else
                                num3++;
                            if (GlobalHospital.Current == EnumHospitalCode.增城妇幼 || GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                            {
                                // 预约->挂号
                                if (item.status == 0 && item.regType == 0) item.status = 9;
                            }
                        }
                    }
                    Viewer.gcNo.DataSource = data;
                    Viewer.lblNums.Text = string.Format(info, num1.ToString(), num2.ToString(), num3.ToString());
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region FocusedRowChanged
        /// <summary>
        /// FocusedRowChanged
        /// </summary>
        internal void FocusedRowChanged()
        {
        }
        #endregion

        #region 退费
        /// <summary>
        /// 退费
        /// </summary>
        internal void Refund()
        {
            EntityOpRegBooking vo = Viewer.gvNo.GetRow(Viewer.gvNo.FocusedRowHandle) as EntityOpRegBooking;
            if (vo != null)
            {
                if (vo.feeStatus == -1)
                {
                    DialogBox.Msg("已退费。");
                    return;
                }
                else if (vo.feeStatus == 0 || vo.feeStatus == 1)
                {
                    if (DialogBox.Msg("请确认是否退费(退号)？？", MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // 未收费 已预约(挂号)
                        //if (vo.payMode.ToLower().Trim() == "wxpay")
                        //{
                        string response = string.Empty;
                        if (vo.status >= 0)
                        {
                            // 先退号
                            string request = string.Empty;
                            request += "<Request>";
                            request += string.Format("<hisOrdNum>{0}</hisOrdNum>", vo.serNo);
                            request += string.Format("<cancelReason>{0}</cancelReason>", "手工平台取消");
                            request += "</Request>";
                            using (ProxyRegistration proxy = new ProxyRegistration())
                            {
                                string cancelType = string.Empty;
                                if (vo.regType == 0)
                                {
                                    response = proxy.Service.WeChatCancelToday(request);
                                    cancelType = "2";
                                }
                                else if (vo.regType == 2)
                                {
                                    response = proxy.Service.WeChatCancel(request);
                                    cancelType = "1";
                                }
                                else
                                {
                                    DialogBox.Msg("非微信业务数据，不能处理。");
                                    return;
                                }
                                Dictionary<string, string> dicKey = Function.ReadXmlNodes(response, "Response");
                                if (dicKey.ContainsKey("resultCode"))
                                {
                                    if (dicKey["resultCode"] == "0")
                                    {
                                        // 再退费
                                        if (vo.feeStatus == 1)
                                        {
                                            response = proxy.Service.WeChatCancelOrder(vo.oriRegNo, cancelType, "手工平台退费");
                                            if (response == "success")
                                            {
                                                DialogBox.Msg("退费成功！");
                                                Search();
                                            }
                                            else if (response == "-1")
                                            {
                                                DialogBox.Msg("未找到收费记录。");
                                            }
                                            else if (response == "-2")
                                            {
                                                DialogBox.Msg("已退费。");
                                            }
                                            else
                                            {
                                                DialogBox.Msg("退费失败。\r\n" + response);
                                            }
                                        }
                                        else
                                        {
                                            DialogBox.Msg("释放号源成功");
                                            Search();
                                        }
                                    }
                                    else
                                    {
                                        DialogBox.Msg("退号失败。");
                                    }
                                }
                                else
                                {
                                    DialogBox.Msg("退号异常。");
                                }
                            }
                        }
                        else
                        {
                            if (vo.feeStatus == 1)
                            {
                                using (ProxyRegistration proxy = new ProxyRegistration())
                                {
                                    string cancelType = string.Empty;
                                    if (vo.regType == 0)
                                    {
                                        cancelType = "2";
                                    }
                                    else if (vo.regType == 2)
                                    {
                                        cancelType = "1";
                                    }
                                    else
                                    {
                                        DialogBox.Msg("非合理数据。");
                                        return;
                                    }
                                    response = proxy.Service.WeChatCancelOrder(vo.oriRegNo, cancelType, "手工平台退费");
                                    if (response == "success")
                                    {
                                        DialogBox.Msg("退费成功！");
                                        Search();
                                    }
                                    else if (response == "-1")
                                    {
                                        DialogBox.Msg("未找到收费记录。");
                                    }
                                    else if (response == "-2")
                                    {
                                        DialogBox.Msg("已退费。");
                                    }
                                    else
                                    {
                                        DialogBox.Msg("退费失败。\r\n" + response);
                                    }
                                }
                            }
                        }
                        //}
                    }
                }
            }
        }
        #endregion

        #endregion

    }
}
