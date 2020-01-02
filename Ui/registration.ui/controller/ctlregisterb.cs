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
using System.Xml;

namespace Registration.Ui
{
    public class ctlRegisterB : BaseController
    {
        #region Override

        /// <summary>
        /// UI.Viewer
        /// </summary>
        private frmRegisterB Viewer = null;

        /// <summary>
        /// SetUI
        /// </summary>
        /// <param name="child"></param>
        public override void SetUI(frmBase child)
        {
            base.SetUI(child);
            Viewer = (frmRegisterB)child;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// 数据源
        /// </summary>
        List<EntitySchedulingBList> DataSource { get; set; }

        /// <summary>
        /// 医师资料
        /// </summary>
        List<EntityOpRegSchedulingDoct> DataSourceDoct { get; set; }

        /// <summary>
        /// 号别字典
        /// </summary>
        List<EntityCodeReg> DataSourceCodeReg { get; set; }

        /// <summary>
        /// 专科号别
        /// </summary>
        List<EntityDicDeptReg> DataSourceDeptReg { get; set; }

        /// <summary>
        /// 初始化中
        /// </summary>
        bool IsInit { get; set; }

        /// <summary>
        /// 东8
        /// </summary>
        internal bool IsDong8 = true;

        List<EntityCodeDepartment> DataSourceOpDept { get; set; }

        List<EntityCodeOperator> DataSourceOpDoct { get; set; }

        #endregion

        #region 方法

        #region GetStr
        /// <summary>
        /// GetStr
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        string GetStr(string val)
        {
            int len = 45;//40;
            bool b = false;
            string str = string.Empty;
            do
            {
                if (val.Length > len)
                {
                    str += val.Substring(0, len) + "\r\n\r\n";
                    val = val.Substring(len);
                    b = true;
                }
                else
                {
                    str += val;
                    b = false;
                }
            } while (b);

            return str + "\r\n\r\n";
        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            try
            {
                IsInit = true;
                uiHelper.BeginLoading(Viewer);
                DateTime startDate = DateTime.Now;
                DateTime endDate = (startDate.AddDays(1 - startDate.Day)).AddMonths(1).AddDays(-1);
                Viewer.dteStart.Text = startDate.ToString("yyyy-MM-dd");
                Viewer.dteEnd.Text = endDate.ToString("yyyy-MM-dd");

                #region 科室

                List<EntityCodeDepartment> DgPlatformDept = null;
                List<EntityCodeOperator> DgPlatformDoct = null;
                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    using (ProxyRegistration proxy = new ProxyRegistration())
                    {
                        DgPlatformDept = proxy.Service.GetDgPlatformDept();
                        DgPlatformDoct = proxy.Service.GetDgPlatformDoct();
                    }
                }
                DataSourceOpDept = (DgPlatformDept != null && DgPlatformDept.Count > 0) ? DgPlatformDept : GlobalDic.DataSourceDepartment;
                DataSourceOpDoct = (DgPlatformDoct != null && DgPlatformDoct.Count > 0) ? DgPlatformDoct : GlobalDic.DataSourceDoctor;

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
                if (DataSourceOpDept != null && DataSourceOpDept.Count > 0)
                {
                    Viewer.lueDept.Properties.DataSource = DataSourceOpDept.ToArray();
                }
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
                FilterDoct(string.Empty);
                Viewer.lueDoct.Properties.SetSize();
                #endregion

                InitDic();
                IsInit = false;
                LoadData();

                if (Viewer.regType == "3")
                {
                    Viewer.lblRegTypeName.Text = "电话预约";
                }
                else if (Viewer.regType == "4")
                {
                    // 医师看本人
                    if (this.DataSourceOpDoct != null && this.DataSourceOpDoct.Any(t => t.operCode.ToUpper() == GlobalLogin.objLogin.EmpNo.ToUpper()))
                    {
                        Viewer.lblRegTypeName.Text = "诊间预约";

                        #region 不显示科室、医师列表

                        int diff = Viewer.lblDate1.Location.X - Viewer.lblDept.Location.X;
                        Viewer.lblDept.Visible = false;
                        Viewer.lueDept.Visible = false;
                        Viewer.lblDoct.Visible = false;
                        Viewer.lueDoct.Visible = false;

                        Viewer.lblDate1.Location = new Point(Viewer.lblDate1.Location.X - diff, Viewer.lblDate1.Location.Y);
                        Viewer.lblDate2.Location = new Point(Viewer.lblDate2.Location.X - diff, Viewer.lblDate2.Location.Y);
                        Viewer.dteStart.Location = new Point(Viewer.dteStart.Location.X - diff, Viewer.dteStart.Location.Y);
                        Viewer.dteEnd.Location = new Point(Viewer.dteEnd.Location.X - diff, Viewer.dteEnd.Location.Y);
                        Viewer.lblSort.Location = new Point(Viewer.lblSort.Location.X - diff, Viewer.lblSort.Location.Y);
                        Viewer.cboSort.Location = new Point(Viewer.cboSort.Location.X - diff, Viewer.cboSort.Location.Y);

                        #endregion
                    }
                    else
                    {
                        Viewer.lblRegTypeName.Text = "现场预约";
                    }
                }
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region InitDic
        /// <summary>
        /// InitDic
        /// </summary>
        void InitDic()
        {
            DataTable dt = null;
            DataSourceCodeReg = new List<EntityCodeReg>();
            DataSourceDeptReg = new List<EntityDicDeptReg>();
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
                dt = proxy.Service.SelectFullTable(new EntityDicDeptReg());
                DataSourceDeptReg.AddRange(EntityTools.ConvertToEntityList<EntityDicDeptReg>(dt));
            }
            using (ProxyScheduling proxy = new ProxyScheduling())
            {
                DataSourceDoct = proxy.Service.GetDoctPlusInfo();
            }
        }
        #endregion

        #region FilterDoct
        /// <summary>
        /// FilterDoct
        /// </summary>
        /// <param name="deptCode"></param>
        void FilterDoct(string deptCode)
        {
            if (this.DataSourceOpDoct != null && this.DataSourceOpDoct.Count > 0)
            {
                if (string.IsNullOrEmpty(deptCode))
                {
                    Viewer.lueDoct.Properties.DataSource = this.DataSourceOpDoct.ToArray();
                }
                else
                {
                    List<EntityCodeOperator> data = this.DataSourceOpDoct.FindAll(t => t.DeptNo == deptCode);
                    Viewer.lueDoct.Properties.DataSource = data.ToArray();
                }
                Viewer.lueDoct.Properties.SetSize();
            }
        }
        #endregion

        #region RefreshData
        /// <summary>
        /// RefreshData
        /// </summary>
        internal void RefreshData()
        {
            InitDic();
            LoadData();
        }
        #endregion

        #region LoadData
        /// <summary>
        /// LoadData
        /// </summary>
        internal void LoadData()
        {
            if (IsInit) return;
            string deptCode = (Viewer.lueDept.Text.Trim() == string.Empty ? string.Empty : Viewer.lueDept.Properties.DBValue);
            FilterDoct(deptCode);

            string startDate = Viewer.dteStart.Text;
            string endDate = Viewer.dteEnd.Text;
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
            {
                DialogBox.Msg("起止日期不能为空。");
                Viewer.dteStart.Focus();
                return;
            }
            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
            {
                DialogBox.Msg("开始日期不能大于结束日期。");
                Viewer.dteStart.Focus();
                return;
            }

            try
            {
                uiHelper.BeginLoading(Viewer);
                Dong8 d8 = new Dong8();
                if (IsDong8)
                {
                    // 医师看本人
                    if (this.DataSourceOpDoct != null && this.DataSourceOpDoct.Any(t => t.operCode.ToUpper() == GlobalLogin.objLogin.EmpNo.ToUpper()))
                    {
                        DataSource = d8.GetSchedulingBList(startDate, endDate, deptCode, new List<string>() { GlobalLogin.objLogin.EmpNo }, this.DataSourceOpDept);
                    }
                    else
                    {
                        string doctCode = (Viewer.lueDoct.Text.Trim() == string.Empty ? string.Empty : Viewer.lueDoct.Properties.DBValue);
                        if (doctCode != string.Empty)
                        {
                            DataSource = d8.GetSchedulingBList(startDate, endDate, deptCode, new List<string>() { doctCode }, this.DataSourceOpDept);
                        }
                    }
                }
                else
                {
                    using (ProxyRegistration proxy = new ProxyRegistration())
                    {
                        // 医师看本人
                        if (this.DataSourceOpDoct != null && this.DataSourceOpDoct.Any(t => t.operCode.ToUpper() == GlobalLogin.objLogin.EmpNo.ToUpper()))
                        {
                            DataSource = proxy.Service.GetSchedulingBList(startDate, endDate, deptCode, GlobalLogin.objLogin.EmpNo, 2);
                        }
                        else
                        {
                            string doctCode = (Viewer.lueDoct.Text.Trim() == string.Empty ? string.Empty : Viewer.lueDoct.Properties.DBValue);
                            DataSource = proxy.Service.GetSchedulingBList(startDate, endDate, deptCode, doctCode, 2);
                        }
                    }
                }
                if (DataSource != null && DataSource.Count > 0)
                {
                    string rankName = string.Empty;
                    EntityOpRegSchedulingDoct doctVo = null;
                    foreach (EntitySchedulingBList item in DataSource)
                    {
                        item.regDate += "   " + Function.CaculateWeekDay(item.regDate);
                        if (IsDong8)
                        {
                            if (DataSourceDoct.Exists(t => t.doctCode.ToUpper() == item.doctCode.ToUpper()))
                            {
                                if (GlobalDic.DataSourceEmployee.Exists(t => t.operCode.ToUpper() == item.doctCode.ToUpper()))
                                {
                                    rankName = GlobalDic.DataSourceEmployee.FirstOrDefault(t => t.operCode.ToUpper() == item.doctCode.ToUpper()).TechnicalLevelName;
                                }

                                doctVo = DataSourceDoct.FirstOrDefault(t => t.doctCode == item.doctCode.ToUpper());
                                item.doctName = item.deptName + " " + /*DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode).regName +*/ "\r\n\r\n" + doctVo.doctName + " " + rankName;
                                item.doctIntroduce = (!string.IsNullOrEmpty(doctVo.doctIntroduce) ? GetStr(doctVo.doctIntroduce) : doctVo.doctIntroduce);
                                item.doctSkill = (!string.IsNullOrEmpty(doctVo.doctSkill) ? GetStr(doctVo.doctSkill) : doctVo.doctSkill);
                                item.doctImage = Function.ConvertByteToImage(doctVo.doctPhoto);

                                if (string.IsNullOrEmpty(item.deptCode) && DataSourceOpDept.Any(t => t.deptName == item.deptName))
                                {
                                    item.deptCode = DataSourceOpDept.FirstOrDefault(t => t.deptName == item.deptName).deptCode;
                                }
                            }
                        }
                        else
                        {
                            if (DataSourceCodeReg.Exists(t => t.regCode == item.regCode && t.regFlag == "2"))
                            {
                                if (DataSourceDoct.Exists(t => t.doctCode.ToUpper() == item.doctCode.ToUpper()))
                                {
                                    if (GlobalDic.DataSourceEmployee.Exists(t => t.operCode.ToUpper() == item.doctCode.ToUpper()))
                                    {
                                        rankName = GlobalDic.DataSourceEmployee.FirstOrDefault(t => t.operCode.ToUpper() == item.doctCode.ToUpper()).TechnicalLevelName;
                                    }

                                    doctVo = DataSourceDoct.FirstOrDefault(t => t.doctCode == item.doctCode);
                                    item.doctName = item.deptName + " " + DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode).regName + "\r\n\r\n" + doctVo.doctName + " " + rankName;
                                    item.doctIntroduce = (!string.IsNullOrEmpty(doctVo.doctIntroduce) ? GetStr(doctVo.doctIntroduce) : doctVo.doctIntroduce);
                                    item.doctSkill = (!string.IsNullOrEmpty(doctVo.doctSkill) ? GetStr(doctVo.doctSkill) : doctVo.doctSkill);
                                    item.doctImage = Function.ConvertByteToImage(doctVo.doctPhoto);
                                }
                            }
                            else
                            {
                                item.doctName = item.deptName + "  " + DataSourceCodeReg.FirstOrDefault(t => t.regCode == item.regCode).regName;
                            }
                        }
                    }
                }
                Viewer.gcPlan.DataSource = DataSource;
                Viewer.gvPlan.ExpandAllGroups();
            }
            finally
            {
                uiHelper.CloseLoading(Viewer);
            }
        }
        #endregion

        #region BookingReg
        /// <summary>
        /// BookingReg
        /// </summary>
        internal void BookingReg(int type)
        {
            EntityBEditParm editParm = new EntityBEditParm();
            editParm.DataSourceCodeReg = this.DataSourceCodeReg;
            editParm.DataSourceDeptReg = this.DataSourceDeptReg;
            editParm.DataSourceDoct = this.DataSourceDoct;
            editParm.DataSourceOpDept = this.DataSourceOpDept;
            editParm.DataSourceOpDoct = this.DataSourceOpDoct;
            if (type == 2)
            {
                editParm.regDid = Function.Dec(GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingBList.Columns.regDid));
                editParm.regCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingBList.Columns.regCode);
                editParm.regDate = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingBList.Columns.regDate);
                editParm.deptCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingBList.Columns.deptCode);
                editParm.doctCode = GetFieldValueStr(Viewer.gvPlan, Viewer.gvPlan.FocusedRowHandle, EntitySchedulingBList.Columns.doctCode);
            }
            editParm.regType = Function.Int(Viewer.regType);
            using (frmRegisterBEdit frm = new frmRegisterBEdit(editParm))
            {
                frm.ShowDialog();
                if (type == 2 && frm.IsBooking)
                {
                    LoadData();
                }
            }
        }
        #endregion

        #region CancelReg
        /// <summary>
        /// CancelReg
        /// </summary>
        internal void CancelReg()
        {
            EntityBEditParm editParm = new EntityBEditParm();
            editParm.DataSourceCodeReg = this.DataSourceCodeReg;
            editParm.DataSourceDeptReg = this.DataSourceDeptReg;
            editParm.DataSourceOpDept = this.DataSourceOpDept;
            editParm.DataSourceOpDoct = this.DataSourceOpDoct;
            editParm.regType = Function.Int(Viewer.regType);
            using (frmRegisterCancel frm = new frmRegisterCancel(editParm))
            {
                frm.ShowDialog();
                if (frm.IsCancel)
                {
                    LoadData();
                }
            }
        }
        #endregion

        #region Query
        /// <summary>
        /// Query
        /// </summary>
        internal void Query()
        {
            using (frmRegisterBQuery frm = new frmRegisterBQuery())
            {
                frm.ShowDialog();
            }
        }
        #endregion

        #region FilterRegCode
        /// <summary>
        /// FilterRegCode
        /// </summary>
        /// <param name="type">1 普通； 2 专家； 3 专科； 4 特需 </param>
        internal void FilterRegCode(int type)
        {
            string regCode = string.Empty;
            if (type == 1)
                regCode = GlobalParm.dicSysParameter[31];
            else if (type == 2)
                regCode = GlobalParm.dicSysParameter[32];
            else if (type == 3)
                regCode = GlobalParm.dicSysParameter[33];
            else if (type == 4)
                regCode = GlobalParm.dicSysParameter[34];

            try
            {
                uiHelper.BeginLoading(Viewer);
                if (DataSource != null)
                {
                    Viewer.gcPlan.DataSource = DataSource.FindAll(t => t.regCode == regCode);
                    Viewer.gvPlan.ExpandAllGroups();
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

    #region Dong 8
    /// <summary>
    /// 东莞八院.类
    /// </summary>
    public class Dong8
    {
        #region ConvertDoctCode
        /// <summary>
        /// ConvertDoctCode
        /// </summary>
        /// <param name="doctCode">HIS.doctCode</param>
        /// <param name="platId">plat.doctId</param>
        /// <returns></returns>
        public string ConvertDoctCode(string doctCode, string platId)
        {
            string value = string.Empty;
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            string typno = "900931";
            string userInfo = Function.ReadConfigXml("userInfo");
            string orgId = Function.ReadConfigXml("orgId");

            xmlIn += "<base>" + Environment.NewLine;
            xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
            xmlIn += "<departmentId></departmentId>" + Environment.NewLine;
            xmlIn += string.Format("<id>{0}</id>", platId) + Environment.NewLine;
            xmlIn += string.Format("<empId>{0}</empId>", doctCode) + Environment.NewLine;
            xmlIn += "</base>" + Environment.NewLine;
            try
            {
                WebService ws = new WebService();
                xmlOut = ws.smsService(typno, xmlIn, userInfo);
                resultCode = xmlOut.Split('|')[0];
                xmlOut = xmlOut.Split('|')[2];
                if (resultCode == "0" && !string.IsNullOrEmpty(xmlOut))
                {
                    string id = string.Empty;
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(xmlOut);
                    System.Xml.XmlElement element = null;
                    if (!string.IsNullOrEmpty(doctCode))
                    {
                        element = doc["base"]["users"]["user"]["empId"];
                        if (element != null)
                        {
                            id = element.InnerText.Trim();
                            if (id == doctCode)
                            {
                                element = doc["base"]["users"]["user"]["id"];
                                value = element.InnerText.Trim();
                                return value;
                            }
                        }
                    }
                    else
                    {
                        element = doc["base"]["users"]["user"]["id"];
                        if (element != null)
                        {
                            id = element.InnerText.Trim();
                            if (id == platId)
                            {
                                element = doc["base"]["users"]["user"]["empId"];
                                value = element.InnerText.Trim();
                                return value;
                            }
                        }
                    }

                    //XmlNodeList xnl = doc.SelectNodes("/users");
                    //XmlNodeList nodeList = xnl[0].SelectNodes("user");
                    //if (!string.IsNullOrEmpty(doctCode))
                    //{
                    //    for (int i = 0; i < nodeList.Count; i++)
                    //    {
                    //        id = nodeList[i].SelectSingleNode("empId").InnerText;
                    //        if (id == doctCode)
                    //        {
                    //            value = nodeList[i].SelectSingleNode("id").InnerText;
                    //            break;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    for (int i = 0; i < nodeList.Count; i++)
                    //    {
                    //        id = nodeList[i].SelectSingleNode("id").InnerText;
                    //        if (id == platId)
                    //        {
                    //            value = nodeList[i].SelectSingleNode("empId").InnerText;
                    //            break;
                    //        }
                    //    }
                    //}
                    doc = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            return value;
        }
        #endregion

        #region ConvertDeptCode
        /// <summary>
        /// ConvertDeptCode
        /// </summary>
        /// <param name="deptCode">HIS.deptCode</param>
        /// <param name="platId">plat.deptId</param>
        /// <returns></returns>
        public string ConvertDeptCode(string deptCode, string platId)
        {
            string value = string.Empty;
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            string typno = "900921";
            string userInfo = Function.ReadConfigXml("userInfo");
            string orgId = Function.ReadConfigXml("orgId");

            xmlIn += "<base>" + Environment.NewLine;
            xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
            xmlIn += string.Format("<departmentId>{0}</departmentId>", platId) + Environment.NewLine;
            xmlIn += string.Format("<hisId>{0}</hisId>", deptCode) + Environment.NewLine;
            xmlIn += "</base>" + Environment.NewLine;

            try
            {
                WebService ws = new WebService();
                xmlOut = ws.smsService(typno, xmlIn, userInfo);
                resultCode = xmlOut.Split('|')[0];
                xmlOut = xmlOut.Split('|')[2];
                if (resultCode == "0" && !string.IsNullOrEmpty(xmlOut))
                {
                    string deptId = string.Empty;
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(xmlOut);

                    XmlNodeList xnl = doc.SelectNodes("/base");
                    XmlNodeList linkNOdes = xnl[0].SelectNodes("departments");
                    XmlNodeList linkNodes = linkNOdes[0].SelectNodes("department");
                    if (!string.IsNullOrEmpty(deptCode))
                    {
                        for (int i = 0; i < linkNodes.Count; i++)
                        {
                            deptId = linkNodes[i].SelectSingleNode("localId").InnerText;
                            if (deptId == deptCode)
                            {
                                value = linkNOdes[i].SelectSingleNode("id").InnerText;
                                break;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(platId))
                    {
                        for (int i = 0; i < linkNodes.Count; i++)
                        {
                            deptId = linkNodes[i].SelectSingleNode("id").InnerText;
                            if (deptId == platId)
                            {
                                if (linkNOdes[i].SelectSingleNode("localId") != null)
                                {
                                    value = linkNOdes[i].SelectSingleNode("localId").InnerText;
                                    break;
                                }

                            }
                        }
                    }
                    doc = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            return value;
        }
        #endregion

        #region GetDoctList
        /// <summary>
        /// GetDoctList
        /// </summary>
        /// <returns></returns>
        public List<string> GetDoctList()
        {
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            List<string> lstDoct = new List<string>();

            string typno = "900111";
            string userInfo = Function.ReadConfigXml("userInfo");
            string providerId = Function.ReadConfigXml("providerId");
            string orgId = Function.ReadConfigXml("orgId");

            xmlIn += "<base>" + Environment.NewLine;
            xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
            xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
            xmlIn += string.Format("<orgId>{0}</orgId>", orgId) + Environment.NewLine;
            xmlIn += "</base>" + Environment.NewLine;
            try
            {
                WebService ws = new WebService();
                xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                resultCode = xmlOut.Split('|')[0];
                xmlOut = xmlOut.Split('|')[2];
                if (resultCode == "0" && !string.IsNullOrEmpty(xmlOut))
                {
                    string doctId = string.Empty;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlOut);
                    System.Xml.XmlNodeList xnl = doc.SelectNodes("/base");
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        XmlNodeList nodeList = xnl[i].SelectNodes("subscribeProducerResult");
                        foreach (XmlNode linkNode in nodeList)
                        {
                            foreach (XmlNode xn2 in linkNode.ChildNodes)
                            {
                                if (xn2.Name == "producerId")
                                {
                                    doctId = xn2.InnerText;
                                    if (!string.IsNullOrEmpty(doctId) && lstDoct.IndexOf(doctId) < 0)
                                    {
                                        lstDoct.Add(doctId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            return lstDoct;
        }
        #endregion

        #region GetSchedulingBList
        /// <summary>
        /// GetSchedulingBList
        /// </summary>
        /// <param name="lstDoctId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<EntitySchedulingBList> GetSchedulingBList(string startDate, string endDate, string deptCode, List<string> lstDoctId, List<EntityCodeDepartment> DataSourceOpDept)
        {
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            string typno = "900131";
            string userInfo = Function.ReadConfigXml("userInfo");
            string providerId = Function.ReadConfigXml("providerId");
            List<EntitySchedulingBList> data = new List<EntitySchedulingBList>();
            bool isReq = true;
            if (lstDoctId == null || lstDoctId.Count == 0 || (lstDoctId.Count == 1 && lstDoctId[0] == string.Empty))
            {
                lstDoctId = GetDoctList();
                isReq = false;
            }
            string doctId = string.Empty;
            string doctCode = string.Empty;
            string deptId = string.Empty;
            DateTime dtmNow = DateTime.Now;
            foreach (string did in lstDoctId)
            {
                if (isReq)
                {
                    doctCode = did;
                    doctId = ConvertDoctCode(did, "");
                }
                else
                {
                    doctId = did;
                    doctCode = ConvertDoctCode("", did);
                }
                xmlIn = string.Empty;
                xmlIn += "<base>" + Environment.NewLine;
                xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
                xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
                xmlIn += string.Format("<producerId>{0}</producerId>", doctId) + Environment.NewLine;
                xmlIn += string.Format("<workStartDate>{0}</workStartDate>", startDate) + Environment.NewLine;
                xmlIn += string.Format("<workEndDate>{0}</workEndDate>", endDate) + Environment.NewLine;
                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    xmlIn += string.Format("<orgId>{0}</orgId>", "4419001030020000000") + Environment.NewLine;
                }
                if (!string.IsNullOrEmpty(deptCode))
                {
                    if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                    {
                        if (DataSourceOpDept.Any(t => t.deptCode == deptCode))
                        {
                            deptId = DataSourceOpDept.FirstOrDefault(t => t.deptCode == deptCode).nhDeptCode;
                        }
                    }
                    else
                    {
                        deptId = ConvertDeptCode(deptCode, "");
                    }
                    if (!string.IsNullOrEmpty(deptId))
                    {
                        xmlIn += string.Format("<departmentId>{0}</departmentId>", deptId) + Environment.NewLine;
                    }
                }
                xmlIn += "</base>" + Environment.NewLine;

                Log.Output(xmlIn);
                try
                {
                    WebService ws = new WebService();
                    xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                    xmlIn = string.Empty;
                    // log
                    Log.Output(xmlOut);

                    resultCode = xmlOut.Split('|')[0];
                    xmlOut = xmlOut.Split('|')[2];
                    if (resultCode == "0" && !string.IsNullOrEmpty(xmlOut))
                    {
                        string doctName = string.Empty;
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.LoadXml(xmlOut);
                        XmlNodeList nodeList = doc.SelectNodes("/base");
                        try
                        {
                            for (int i = 0; i < nodeList.Count; i++)
                            {
                                XmlNodeList linkNodes = nodeList[i].SelectNodes("subscribeProducerTime");
                                if (linkNodes != null)
                                {
                                    for (int j = 0; j < linkNodes.Count; j++)
                                    {
                                        XmlNode xn3 = linkNodes[i].SelectSingleNode("rows");
                                        XmlNodeList lstXn4 = xn3.SelectNodes("subscribeProducerTimeRsInfo");
                                        doctName = linkNodes[i].SelectSingleNode("producerName").InnerText;
                                        EntitySchedulingBList vo = null;
                                        for (int k = 0; k < lstXn4.Count; k++)
                                        {
                                            vo = new EntitySchedulingBList();
                                            vo.regDid = 0;
                                            vo.regDate = lstXn4[k].SelectSingleNode("workDate").InnerText;
                                            vo.regCode = string.Empty;
                                            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                                            {
                                                if (DataSourceOpDept.Any(t => t.nhDeptCode == lstXn4[k].SelectSingleNode("departmentId").InnerText))
                                                {
                                                    vo.deptCode = DataSourceOpDept.FirstOrDefault(t => t.nhDeptCode == lstXn4[k].SelectSingleNode("departmentId").InnerText).deptCode;
                                                }
                                            }
                                            else
                                            {
                                                vo.deptCode = (string.IsNullOrEmpty(deptCode) ? ConvertDeptCode("", lstXn4[k].SelectSingleNode("departmentId").InnerText) : deptCode);
                                            }
                                            vo.deptName = lstXn4[k].SelectSingleNode("departmentName").InnerText;
                                            vo.doctCode = doctCode;
                                            vo.doctName = doctName;
                                            vo.doctIntroduce = string.Empty;
                                            vo.doctImage = null;
                                            vo.usedNums = string.Empty;
                                            vo.amPm = lstXn4[k].SelectSingleNode("shift").InnerText;
                                            if (vo.amPm == "1") vo.amPm = "上午";
                                            else if (vo.amPm == "2") vo.amPm = "下午";
                                            vo.surplusNums = lstXn4[k].SelectSingleNode("residueNum").InnerText;
                                            vo.booking = string.Empty;
                                            vo.regFee = lstXn4[k].SelectSingleNode("feePrice").InnerText;
                                            vo.status = lstXn4[k].SelectSingleNode("subscribeFlag").InnerText;
                                            vo.startTime = lstXn4[k].SelectSingleNode("startTime").InnerText;
                                            vo.endTime = lstXn4[k].SelectSingleNode("endTime").InnerText;
                                            vo.sortNo = 0;
                                            if (data.Any(t => t.regDate == vo.regDate && t.doctCode == vo.doctCode && t.amPm == vo.amPm))
                                            {
                                                data.FirstOrDefault(t => t.regDate == vo.regDate && t.doctCode == vo.doctCode && t.amPm == vo.amPm).surplusNums = Convert.ToString(Function.Dec(data.FirstOrDefault(t => t.regDate == vo.regDate && t.doctCode == vo.doctCode && t.amPm == vo.amPm).surplusNums) + Function.Dec(vo.surplusNums));
                                            }
                                            else
                                            {
                                                data.Add(vo);
                                            }
                                        }
                                    }
                                }
                            }
                            foreach (EntitySchedulingBList item in data)
                            {
                                item.surplusNums = item.amPm + " 剩余 " + item.surplusNums;
                            }
                        }
                        catch (Exception e)
                        {
                            ExceptionLog.OutPutException(e);
                        }
                        finally
                        {
                            doc = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog.OutPutException(ex);
                }
            }
            return data;
        }
        #endregion

        #region GetSchedulingDayNumber
        /// <summary>
        /// GetSchedulingDayNumber
        /// </summary>
        /// <param name="regDate"></param>
        /// <param name="deptCode"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public List<EntityOpRegSchedulingDayNumber> GetSchedulingDayNumber(string regDate, string deptCode, string doctCode, List<EntityCodeDepartment> DataSourceOpDept)
        {
            string xmlIn = string.Empty;
            string xmlOut = string.Empty;
            string resultCode = string.Empty;
            string typno = "900131";
            string userInfo = Function.ReadConfigXml("userInfo");
            string providerId = Function.ReadConfigXml("providerId");
            List<EntityOpRegSchedulingDayNumber> data = new List<EntityOpRegSchedulingDayNumber>();
            string deptId = string.Empty;
            DateTime dtmNow = DateTime.Now;
            string doctId = ConvertDoctCode(doctCode, "");
            string startDate = regDate;
            string endDate = regDate;

            xmlIn += "<base>" + Environment.NewLine;
            xmlIn += string.Format("<producerType>1</producerType>") + Environment.NewLine;
            xmlIn += string.Format("<providerId>{0}</providerId>", providerId) + Environment.NewLine;
            xmlIn += string.Format("<producerId>{0}</producerId>", doctId) + Environment.NewLine;
            xmlIn += string.Format("<workStartDate>{0}</workStartDate>", startDate) + Environment.NewLine;
            xmlIn += string.Format("<workEndDate>{0}</workEndDate>", endDate) + Environment.NewLine;
            if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
            {
                xmlIn += string.Format("<orgId>{0}</orgId>", "4419001030020000000") + Environment.NewLine;
            }
            if (!string.IsNullOrEmpty(deptCode))
            {
                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    if (DataSourceOpDept.Any(t => t.deptCode == deptCode))
                    {
                        deptId = DataSourceOpDept.FirstOrDefault(t => t.deptCode == deptCode).nhDeptCode;
                    }
                }
                else
                {
                    deptId = ConvertDeptCode(deptCode, "");
                }
                if (!string.IsNullOrEmpty(deptId))
                {
                    xmlIn += string.Format("<departmentId>{0}</departmentId>", deptId) + Environment.NewLine;
                }
            }
            xmlIn += "</base>" + Environment.NewLine;
            Log.Output(xmlIn);
            try
            {
                WebService ws = new WebService();
                xmlOut = ws.subscribService(typno, xmlIn, userInfo);
                xmlIn = string.Empty;
                // log
                Log.Output(xmlOut);

                resultCode = xmlOut.Split('|')[0];
                xmlOut = xmlOut.Split('|')[2];
                if (resultCode == "0" && !string.IsNullOrEmpty(xmlOut))
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(xmlOut);
                    XmlNodeList nodeList = doc.SelectNodes("/base");
                    try
                    {
                        for (int i = 0; i < nodeList.Count; i++)
                        {
                            XmlNodeList linkNodes = nodeList[i].SelectNodes("subscribeProducerTime");
                            if (linkNodes != null)
                            {
                                for (int j = 0; j < linkNodes.Count; j++)
                                {
                                    XmlNode xn3 = linkNodes[i].SelectSingleNode("rows");
                                    XmlNodeList lstXn4 = xn3.SelectNodes("subscribeProducerTimeRsInfo");
                                    EntityOpRegSchedulingDayNumber vo = null;
                                    for (int k = 0; k < lstXn4.Count; k++)
                                    {
                                        vo = new EntityOpRegSchedulingDayNumber();
                                        vo.numberSerNo = 0;
                                        vo.regDid = 0;
                                        vo.amPm = 0;
                                        vo.startTime = lstXn4[k].SelectSingleNode("startTime").InnerText;
                                        vo.endTime = lstXn4[k].SelectSingleNode("endTime").InnerText;
                                        vo.limitNum = "0";
                                        if (Function.Dec(lstXn4[k].SelectSingleNode("residueNum").InnerText) > 0)
                                            vo.surplusNum = lstXn4[k].SelectSingleNode("residueNum").InnerText;
                                        data.Add(vo);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ExceptionLog.OutPutException(e);
                    }
                    finally
                    {
                        doc = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }

            return data;
        }
        #endregion

        #region ResultMessage
        /// <summary>
        /// ResultMessage
        /// </summary>
        /// <param name="resultCode"></param>
        /// <returns></returns>
        public string ResultMessage(string resultCode)
        {
            Dictionary<string, string> dicMsg = new Dictionary<string, string>();
            dicMsg.Add("-10", "重复数据！");
            dicMsg.Add("3101", "基础数据错误，检查ORG_SUBSCRIBE_PROVIDER表");
            dicMsg.Add("4101", "该热门医生今日的额度已经用完！");
            dicMsg.Add("4102", "该预约渠道今日的额度已经用完！");
            dicMsg.Add("4103", "该热门医生今日的预约被限制！");
            dicMsg.Add("4104", "该预约渠道今日的预约被限制！");
            dicMsg.Add("4201", "该用户今天已经预约过3个预约，今日不能再预约！");
            dicMsg.Add("4202", "该用户今天已经预约过这个预约，今日不能重复约这个预约！");
            dicMsg.Add("4203", "该用户今天已经预约过这个时段的预约，今日不能再预约这个时段的预约！");
            dicMsg.Add("5101", "该时间段的预约服务已经用完！");
            dicMsg.Add("5102", "没有找到该预约服务！");
            dicMsg.Add("5103", "预约服务条数错误！");
            dicMsg.Add("5201", "已经分配预约但没有锁定预约！");
            dicMsg.Add("5204", "已经取消预约！");
            dicMsg.Add("5205", "该预约没有锁号不能进行退号");
            if (dicMsg.ContainsKey(resultCode))
                return dicMsg[resultCode];
            else
                return "未知错误";
        }
        #endregion

    }
    #endregion
}
