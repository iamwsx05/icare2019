using Common.Controls;
using Common.Entity;
using Common.Utils;
using System;
using System.Xml;
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
    /// ctlWeChatRegAccCheck
    /// </summary>
    public class ctlWeChatRegAccCheck : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmWeChatRegAccCheck Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmWeChatRegAccCheck)child;
        }
        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            using (ProxyRegistration proxy = new ProxyRegistration())
            {
                string maxTime = proxy.Service.GetMaxAccTime();
                if (string.IsNullOrEmpty(maxTime))
                {
                    Viewer.lblDownTime.Text = "微信下载时间: /";
                }
                else
                {
                    Viewer.lblDownTime.Text = "微信下载时间: " + maxTime;
                }
            }
        }
        #endregion

        #region SetRowCellStyle
        /// <summary>
        /// SetRowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int feeStatus = Function.Int(Viewer.gvAcc.GetRowCellValue(e.RowHandle, EntityOpRegBooking.Columns.feeStatus));
            if (feeStatus == 1)
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else if (feeStatus == 2)
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else if (feeStatus == 3)
            {
                e.Appearance.ForeColor = Color.Brown;
            }
            else if (feeStatus == 4)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else if (feeStatus == 5)
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region 下载微信账单
        /// <summary>
        /// 下载微信账单
        /// </summary>
        internal void WeChatDownload()
        {
            string downDateStart = Viewer.dteDownStart.Text;
            string downDateEnd = Viewer.dteDownEnd.Text;
            if (string.IsNullOrEmpty(downDateStart) || string.IsNullOrEmpty(downDateEnd))
            {
                DialogBox.Msg("请输入下载日期。");
                return;
            }
            if (Function.Datetime(downDateStart) > Function.Datetime(downDateEnd))
            {
                DialogBox.Msg("开始日期不能大于结束日期。");
                return;
            }
            if (downDateEnd == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                DialogBox.Msg("结束日期只能小于今天，因为今天的账单还未生成。");
                return;
            }
            string response = string.Empty;
            try
            {
                uiHelper.BeginLoading(Viewer);
                string error = string.Empty;
                string currDate = string.Empty;
                string ipAddr = string.Empty;
                if (GlobalHospital.Current == EnumHospitalCode.顺德乐从)
                {
                    if (GlobalParm.dicSysParameter.ContainsKey(38) && GlobalParm.dicSysParameter[38].Trim() != string.Empty)
                    {
                        ipAddr = GlobalParm.dicSysParameter[38].Trim();
                    }
                }
                XmlDocument document = new XmlDocument();
                DataTable dtSource = null;
                ProxyRegistration proxy = null;
                if (ipAddr == string.Empty)
                    proxy = new ProxyRegistration();
                else
                    proxy = new ProxyRegistration(ipAddr);
                currDate = downDateStart;
                do
                {
                    response = proxy.Service.WeChatDownloadOrderInfo(currDate);
                    if (!string.IsNullOrEmpty(response))
                    {
                        document.LoadXml(response);
                        if (document["response"]["resultCode"].InnerText.Trim() == "0")
                        {
                            DataSet ds = Function.ReadXml(response);
                            DataTable dt = ds.Tables["list"];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dtSource == null) dtSource = dt.Clone();
                                dtSource.Merge(dt);
                                dtSource.AcceptChanges();
                            }
                        }
                        else
                        {
                            error += document["response"]["resultDesc"].InnerText + "\r\n";
                        }
                    }
                    Application.DoEvents();
                    currDate = Function.Datetime(currDate).AddDays(1).ToString("yyyy-MM-dd");
                }
                while (Convert.ToDateTime(currDate) <= Convert.ToDateTime(downDateEnd));

                if (!string.IsNullOrEmpty(error))
                {
                    DialogBox.Msg(error);
                }
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    DateTime dtmNow = Utils.ServerTime();
                    EntityOpWeChatAccount vo = null;
                    List<string> lstDate = new List<string>();
                    List<EntityOpWeChatAccount> lstAcc = new List<EntityOpWeChatAccount>();
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        vo = new EntityOpWeChatAccount();
                        vo.agtOrdNum = dr["agtOrdNum"].ToString();
                        vo.hisOrdNum = Function.Dec(dr["hisOrdNum"]);
                        vo.psOrdNum = dr["psOrdNum"].ToString();
                        vo.orderType = dr["orderType"].ToString();
                        vo.payMode = dr["payMode"].ToString();
                        vo.payAmt = Function.Dec(dr["payAmt"]) / 100;
                        vo.payTime = dr["payTime"].ToString();
                        vo.oldAgtOrdNum = dr["oldAgtOrdNum"].ToString();
                        vo.oldPsOrdNum = dr["oldPsOrdNum"].ToString();
                        if (!string.IsNullOrEmpty(dr["oldHisOrdNum"].ToString()))
                            vo.oldHisOrdNum = Function.Dec(dr["oldHisOrdNum"].ToString());
                        vo.feeType = dr["feeType"].ToString();
                        vo.cardNo = dr["cardNo"].ToString();
                        vo.patName = dr["patName"].ToString();
                        vo.payType = dr["payType"].ToString();
                        vo.recorder = GlobalLogin.objLogin.EmpNo;
                        vo.recordDate = dtmNow;
                        vo.accDate = vo.payTime.Substring(0, 10);
                        if (lstDate.IndexOf(vo.accDate) < 0) lstDate.Add(vo.accDate);
                        lstAcc.Add(vo);
                        Application.DoEvents();
                    }
                    using (ProxyRegistration proxy2 = new ProxyRegistration())
                    {
                        if (proxy2.Service.SaveWeChatAcc(lstAcc, lstDate) > 0)
                        {
                            DialogBox.Msg("下载成功！");
                        }
                        else
                        {
                            DialogBox.Msg("下载失败.");
                        }
                    }
                }
                else
                {
                    DialogBox.Msg("无数据下载.");
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region 对账
        /// <summary>
        /// 对账
        /// </summary>
        internal void AccountCheck()
        {
            string accDateStart = Viewer.dteDownStart.Text;
            string accDateEnd = Viewer.dteDownEnd.Text;
            if (string.IsNullOrEmpty(accDateStart) || string.IsNullOrEmpty(accDateEnd))
            {
                DialogBox.Msg("请输入对账日期。");
                return;
            }
            if (Function.Datetime(accDateStart) > Function.Datetime(accDateEnd))
            {
                DialogBox.Msg("开始日期不能大于结束日期。");
                return;
            }

            List<EntityOpRegBooking> hisData = new List<EntityOpRegBooking>();
            List<EntityOpRegBooking> accData = new List<EntityOpRegBooking>();
            try
            {
                uiHelper.BeginLoading(Viewer);
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    hisData = proxy.Service.GetRegWeChatPay(accDateStart, accDateEnd, 1);
                    accData = proxy.Service.GetRegWeChatAcc(accDateStart, accDateEnd, 1);
                }
                EntityOpRegBooking vo = null;
                List<EntityOpRegBooking> dataSource = new List<EntityOpRegBooking>();

                #region 1.微信有(收、退) -- HIS无
                var data1 = accData.Where(x => !hisData.Select(y => y.serNo).Contains(x.serNo)).ToList();
                foreach (var item in data1)
                {
                    vo = new EntityOpRegBooking();
                    vo.exception = "微信" + (item.feeStatus == 1 ? "收费" : "退费") + " HIS没有收费记录";
                    vo.accDate = item.payTime != string.Empty && item.payTime.Length >= 10 ? item.payTime.Substring(0, 10) : string.Empty;
                    vo.cardNo = item.cardNo;
                    vo.patName = item.patName;
                    vo.sex = item.sex;
                    vo.age = item.age;
                    vo.contactTel = item.contactTel;
                    vo.regCodeName = item.regCodeName;
                    vo.deptName = item.deptName;
                    vo.doctName = item.doctName;
                    vo.payMode = item.payMode;
                    vo.payAmt = item.payAmt;
                    if (item.feeStatus == 1)
                    {
                        vo.payTime = item.payTime;
                        vo.feeStatus = 1;
                    }
                    else if (item.feeStatus == 2)
                    {
                        vo.refundTime = item.payTime;
                        vo.feeStatus = 2;
                    }
                    vo.status = item.status;
                    vo.agtOrdNum = item.agtOrdNum;
                    dataSource.Add(vo);
                }
                #endregion

                #region 2.微信收 -- HIS退; 微信退 -- HIS收

                List<EntityOpRegBooking> hisData1 = hisData.FindAll(t => t.feeStatus == 1);
                List<EntityOpRegBooking> hisData2 = hisData.FindAll(t => t.feeStatus == 2);
                List<EntityOpRegBooking> accData1 = accData.FindAll(t => t.feeStatus == 1);
                List<EntityOpRegBooking> accData2 = accData.FindAll(t => t.feeStatus == 2);
                foreach (EntityOpRegBooking item1 in hisData1)
                {
                    item1.accDate = item1.payTime != string.Empty && item1.payTime.Length >= 10 ? item1.payTime.Substring(0, 10) : string.Empty;
                    if (hisData2.Any(t => t.serNo == item1.serNo))
                    {
                        if (accData1.Any(t => t.serNo == item1.serNo) && !accData2.Any(t => t.serNo == item1.serNo))
                        {
                            item1.exception = "微信收费 HIS退费";
                            item1.feeStatus = 3;
                            item1.refundTime = hisData2.FirstOrDefault(t => t.serNo == item1.serNo).payTime;
                            dataSource.Add(item1);
                        }
                    }
                    else
                    {
                        if (accData2.Any(t => t.serNo == item1.serNo))
                        {
                            item1.exception = "微信退费 HIS收费";
                            item1.feeStatus = 4;
                            dataSource.Add(item1);
                        }
                    }
                }

                #region bak
                //var data2 = from m in accData2
                //            from n in hisData1
                //            from l in hisData2
                //            where m.serNo == n.serNo && m.serNo != l.serNo
                //            select new
                //            {
                //                m.payTime,
                //                accStatus = m.status,
                //                n.serNo,
                //                n.cardNo,
                //                n.psOrdNum,
                //                n.payMode,
                //                n.payAmt,
                //                n.agtOrdNum,
                //                hisStatus = n.status,
                //                n.patName,
                //                n.sex,
                //                n.age,
                //                n.idNo,
                //                n.contactTel,
                //                n.regCodeName,
                //                n.deptName,
                //                n.doctName,
                //                n.oriRegNo
                //            };


                //var data2 = from m in accData
                //            from n in hisData
                //            where m.serNo == n.serNo && m.agtOrdNum == n.agtOrdNum.Replace(".", "") && m.status != n.status
                //            select new
                //            {
                //                m.payTime,
                //                accStatus = m.status,
                //                n.serNo,
                //                n.cardNo,
                //                n.psOrdNum,
                //                n.payMode,
                //                n.payAmt,
                //                n.agtOrdNum,
                //                hisStatus = n.status,
                //                n.patName,
                //                n.sex,
                //                n.age,
                //                n.idNo,
                //                n.contactTel,
                //                n.regCodeName,
                //                n.deptName,
                //                n.doctName,
                //                n.oriRegNo
                //            };
                //foreach (var item in data2)
                //{
                //    vo = new EntityOpRegBooking();
                //    if (item.accStatus == 1 && item.hisStatus == 2)
                //    {
                //        vo.exception = "微信收费 HIS退费";
                //        vo.status = 3;
                //    }
                //    else if (item.accStatus == 2 && item.hisStatus == 1)
                //    {
                //        vo.exception = "微信退费 HIS收费";
                //        vo.status = 4;
                //    }
                //    else
                //    {
                //        string xx = "";
                //    }
                //    if (dataSource.Any(t => t.serNo == item.serNo && t.agtOrdNum == item.agtOrdNum)) continue;
                //    vo.accDate = item.payTime != string.Empty && item.payTime.Length >= 10 ? item.payTime.Substring(0, 10) : string.Empty;
                //    vo.serNo = item.serNo;
                //    vo.cardNo = item.cardNo;
                //    vo.patName = item.patName;
                //    vo.sex = item.sex;
                //    vo.age = item.age;
                //    vo.contactTel = item.contactTel;
                //    vo.regCodeName = item.regCodeName;
                //    vo.deptName = item.deptName;
                //    vo.doctName = item.doctName;
                //    vo.payMode = item.payMode;
                //    vo.payAmt = item.payAmt;
                //    if (item.accStatus == 1)
                //        vo.payTime = item.payTime;
                //    else if (item.accStatus == 2)
                //        vo.refundTime = item.payTime;
                //    vo.agtOrdNum = item.agtOrdNum;
                //    vo.oriRegNo = item.oriRegNo;
                //    dataSource.Add(vo);
                //}
                #endregion

                #endregion

                #region 3.微信无 -- HIS有 (未下载账单)
                var data3 = hisData.Where(x => !accData.Select(y => y.serNo).Contains(x.serNo)).ToList();
                foreach (var item in data3)
                {
                    vo = new EntityOpRegBooking();
                    vo.exception = "微信无下载 HIS有缴费记录";
                    vo.accDate = item.payTime != string.Empty && item.payTime.Length >= 10 ? item.payTime.Substring(0, 10) : string.Empty;
                    vo.cardNo = item.cardNo;
                    vo.patName = item.patName;
                    vo.sex = item.sex;
                    vo.age = item.age;
                    vo.contactTel = item.contactTel;
                    vo.regCodeName = item.regCodeName;
                    vo.deptName = item.deptName;
                    vo.doctName = item.doctName;
                    vo.payMode = item.payMode;
                    vo.payAmt = item.payAmt;
                    if (item.feeStatus == 1)
                        vo.payTime = item.payTime;
                    else if (item.feeStatus == 2)
                        vo.refundTime = item.payTime;
                    vo.agtOrdNum = item.agtOrdNum;
                    vo.oriRegNo = item.oriRegNo;
                    vo.feeStatus = 5;
                    vo.status = item.status;
                    vo.invoNo = item.invoNo;
                    dataSource.Add(vo);
                }
                #endregion

                if (dataSource.Count > 0)
                    dataSource.Sort();
                else
                    DialogBox.Msg("无异常数据...");
                Viewer.gcAcc.DataSource = dataSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region 退费
        /// <summary>
        /// 退费
        /// </summary>
        internal void Refund()
        {
            List<int> lstRowNo = new List<int>();
            for (int i = 0; i < Viewer.gvAcc.RowCount; i++)
            {
                if (Viewer.gvAcc.IsRowSelected(i)) lstRowNo.Add(i);
            }
            if (lstRowNo.Count == 0)
            {
                DialogBox.Msg("请选择记录。");
                return;
            }
            if (DialogBox.Msg("请确认是否退费？？", MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool isSucc = false;
                string info = string.Empty;
                EntityOpRegBooking vo = null;
                try
                {
                    uiHelper.BeginLoading(Viewer);
                    foreach (int rowNo in lstRowNo)
                    {
                        vo = Viewer.gvAcc.GetRow(rowNo) as EntityOpRegBooking;
                        if (vo != null)
                        {
                            if (vo.feeStatus == 1 || vo.feeStatus == 2 || vo.feeStatus == 3)
                            {
                                continue;
                            }
                            if (!string.IsNullOrEmpty(vo.refundTime))
                            {
                                continue;
                            }

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
                                    Dictionary<string, string> dicKey = Function.ReadXmlNodes(response, "Response");
                                    if (dicKey.ContainsKey("resultCode"))
                                    {
                                        if (dicKey["resultCode"] == "0")
                                        {
                                            // 再退费
                                            response = proxy.Service.WeChatCancelOrder(vo.oriRegNo, cancelType, "手工平台退费");
                                            if (response == "success")
                                            {
                                                isSucc = true;
                                            }
                                            else if (response == "-1")
                                            {
                                                info += vo.cardNo + " " + vo.patName + " :" + "未找到收费记录。\r\n";
                                            }
                                            else if (response == "-2")
                                            {
                                                info += vo.cardNo + " " + vo.patName + " :" + "已退费。\r\n";
                                            }
                                            else
                                            {
                                                info += vo.cardNo + " " + vo.patName + " :" + "退费失败。\r\n";
                                            }
                                        }
                                        else
                                        {
                                            info += vo.cardNo + " " + vo.patName + " :" + "退号失败。\r\n";
                                        }
                                    }
                                    else
                                    {
                                        info += vo.cardNo + " " + vo.patName + " :" + "退号异常。\r\n";
                                    }
                                }
                            }
                            else
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
                                    response = proxy.Service.WeChatCancelOrder(vo.oriRegNo, cancelType, "手工平台退费");
                                    if (response == "success")
                                    {
                                        isSucc = true;
                                    }
                                    else if (response == "-1")
                                    {
                                        info += vo.cardNo + " " + vo.patName + " :" + "未找到收费记录。\r\n";
                                    }
                                    else if (response == "-2")
                                    {
                                        info += vo.cardNo + " " + vo.patName + " :" + "已退费。\r\n";
                                    }
                                    else
                                    {
                                        info += vo.cardNo + " " + vo.patName + " :" + "退费失败。\r\n" + response + "\r\n";
                                    }
                                }
                            }
                        }
                        Application.DoEvents();
                    }
                    if (!string.IsNullOrEmpty(info)) DialogBox.Msg(info);
                    if (isSucc)
                    {
                        DialogBox.Msg("退费成功!");
                        this.AccountCheck();
                    }
                }
                finally
                {
                    uiHelper.CloseLoading(Viewer);
                }
            }
        }
        #endregion

        #region 汇总
        /// <summary>
        /// 汇总
        /// </summary>
        internal void AccSum()
        {
            string statDateStart = Viewer.dteDownStart.Text;
            string statDateEnd = Viewer.dteDownEnd.Text;
            if (string.IsNullOrEmpty(statDateStart) || string.IsNullOrEmpty(statDateEnd))
            {
                DialogBox.Msg("请输入下载日期。");
                return;
            }
            if (Function.Datetime(statDateStart) > Function.Datetime(statDateEnd))
            {
                DialogBox.Msg("开始日期不能大于结束日期。");
                return;
            }
            List<EntityOpRegBooking> hisData = new List<EntityOpRegBooking>();
            List<EntityOpRegBooking> accData = new List<EntityOpRegBooking>();
            List<EntityRegAcc> dataSource = new List<EntityRegAcc>();
            try
            {
                uiHelper.BeginLoading(Viewer);
                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    hisData = proxy.Service.GetRegWeChatPay(statDateStart, statDateEnd, 2);
                    accData = proxy.Service.GetRegWeChatAcc(statDateStart, statDateEnd, 2);
                }
                EntityRegAcc vo = null;
                // HIS
                foreach (EntityOpRegBooking item in hisData)
                {
                    vo = new EntityRegAcc();
                    vo.accDate = item.payTime.Substring(0, 10);
                    vo.hisMny = Function.Dec(item.payAmt);
                    if (dataSource.Any(t => t.accDate == vo.accDate))
                    {
                        dataSource.FirstOrDefault(t => t.accDate == vo.accDate).hisMny += Math.Abs(vo.hisMny) * (item.feeStatus == 2 ? -1 : 1); ;
                    }
                    else
                    {
                        vo.typeName = "收入";
                        dataSource.Add(vo);
                    }
                }
                // WeChat
                foreach (EntityOpRegBooking item in accData)
                {
                    vo = new EntityRegAcc();
                    vo.accDate = item.payTime.Substring(0, 10);
                    vo.weChatMny = Function.Dec(item.payAmt);
                    if (dataSource.Any(t => t.accDate == vo.accDate))
                    {
                        dataSource.FirstOrDefault(t => t.accDate == vo.accDate).weChatMny += Math.Abs(vo.weChatMny) * (item.feeStatus == 2 ? -1 : 1); ;
                    }
                    else
                    {
                        vo.typeName = "收入";
                        dataSource.Add(vo);
                    }
                }
                // 差值
                decimal totalWeChat = 0;
                decimal totalHis = 0;
                foreach (EntityRegAcc item in dataSource)
                {
                    if (item.typeName == "收入")
                    {
                        totalWeChat += item.weChatMny;
                        totalHis += item.hisMny;
                    }
                    item.diffMny = item.hisMny - item.weChatMny;
                }
                vo = new EntityRegAcc();
                vo.accDate = "合计";
                vo.hisMny = totalHis;
                vo.weChatMny = totalWeChat;
                vo.diffMny = totalHis - totalWeChat;
                dataSource.Add(vo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }

            using (frmWeChatRegAccSum frm = new frmWeChatRegAccSum(dataSource))
            {
                frm.ShowDialog();
            }
        }
        #endregion

        #endregion
    }
}
