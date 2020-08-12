using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace weCare.eApp
{
    /// <summary>
    /// 电子申请单.控制台
    /// </summary>
    public partial class frmConsole : frmBasePopup
    {
        #region .ctor
        /// <summary>
        /// .ctor
        /// </summary>
        public frmConsole(string _request)
        {
            InitializeComponent();
            this.request = _request;
        }
        #endregion

        #region 变量.属性

        string request { get; set; }

        ShowPanelForm efPanel { get; set; }

        EntityPatient patVo { get; set; }

        EntityCatalog currCataVo { get; set; }

        List<EntityEafApplication> appDataSource { get; set; }

        bool isLoading { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        string illHistory { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        string clinicDiag { get; set; }

        #endregion

        #region 方法

        #region 本地配置
        /// <summary>
        /// 本地配置
        /// </summary>
        /// <param name="empNo"></param>
        /// <returns></returns>
        List<EntityAppConfig> GetAppConfig(string empNo)
        {
            EntityPC pc = new EntityPC();
            pc.MachineName = Function.LocalHostName();
            pc.IpAddr = Function.LocalIP();
            pc.MacAddr = Function.LocalMac();
            pc.EmpNo = empNo;
            using (ProxyLogin proxy = new ProxyLogin())
            {
                return proxy.Service.GetAppConfig(pc);
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            try
            {
                #region 主题
                // 主题           
                string skinName = Function.ReadLocalSettingValue("Main|skinName", "value");
                if (!string.IsNullOrEmpty(skinName)) GlobalLogin.SkinName = skinName;
                this.defaultLookAndFeel.LookAndFeel.SkinName = GlobalLogin.SkinName;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(GlobalLogin.SkinName);
                GlobalLogin.SkinMaskColorValue = Function.ReadLocalSettingValue("Main|skinMaskColor", "value");
                //if (!string.IsNullOrEmpty(GlobalLogin.SkinMaskColorValue))
                //{
                //    this.defaultLookAndFeel.LookAndFeel.SkinMaskColor = GlobalLogin.SkinMaskColor;
                //    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(GlobalLogin.SkinMaskColor, GlobalLogin.SkinMaskColor2);
                //}
                #endregion

                uiHelper.BeginLoading(this);

                #region data source

                GlobalAppConfig.AppConfig = this.GetAppConfig(string.Empty);

                using (ProxyCommon proxy = new ProxyCommon())
                {
                    GlobalDic.DataSourceCheckPart = new List<EntityCheckPart>();    // = proxy.Service.GetCheckPart();

                    EntityCodeOperator[] data1 = null;
                    data1 = proxy.Service.GetEmployee(1);
                    if (data1 != null && data1.Length > 0)
                        GlobalDic.DataSourceDoctor = data1.ToList();
                    else
                        GlobalDic.DataSourceDoctor = new List<EntityCodeOperator>();
                    data1 = proxy.Service.GetEmployee(2);
                    if (data1 != null && data1.Length > 0)
                        GlobalDic.DataSourceNurse = data1.ToList();
                    else
                        GlobalDic.DataSourceNurse = new List<EntityCodeOperator>();
                    GlobalDic.dicEmpRole = proxy.Service.GetEmpRoleList();
                    GlobalDic.DataSourceDefDeptEmployee = proxy.Service.GetDefDeptEmployee();
                }
                GlobalDic.DataSourceEmployee = null;
                GlobalDic.DataSourceEmployee = new List<EntityCodeOperator>();
                if (GlobalDic.DataSourceDoctor != null)
                {
                    GlobalDic.DataSourceEmployee.AddRange(GlobalDic.DataSourceDoctor);
                }
                if (GlobalDic.DataSourceNurse != null)
                {
                    GlobalDic.DataSourceEmployee.AddRange(GlobalDic.DataSourceNurse);
                }
                #endregion

                #region request
                patVo = new EntityPatient();
                Dictionary<string, string> dicKey = Function.ReadXmlNodes(this.request, "request");
                if (dicKey.ContainsKey("sourceId")) patVo.PatientType = dicKey["sourceId"];
                if (dicKey.ContainsKey("registerId")) patVo.RegisterID = dicKey["registerId"];
                if (dicKey.ContainsKey("patientId")) patVo.PatientID = dicKey["patientId"];
                if (dicKey.ContainsKey("patientName")) patVo.PatientName = dicKey["patientName"];
                if (dicKey.ContainsKey("sex")) patVo.Sex = dicKey["sex"];
                if (dicKey.ContainsKey("birthday")) patVo.Birthday = Convert.ToDateTime(dicKey["birthday"]);
                if (dicKey.ContainsKey("cardNo")) patVo.CardNo = dicKey["cardNo"];
                if (dicKey.ContainsKey("ipNo")) patVo.PatientIpNo = dicKey["ipNo"];
                if (dicKey.ContainsKey("bedNo")) patVo.BedNo = dicKey["bedNo"];
                if (dicKey.ContainsKey("homeTel")) patVo.HomeTel = dicKey["homeTel"];
                if (dicKey.ContainsKey("homeAddr")) patVo.HomeAddr = dicKey["homeAddr"];
                if (dicKey.ContainsKey("marriage")) patVo.Marriage = dicKey["marriage"];
                if (dicKey.ContainsKey("occupation")) patVo.Occupation = dicKey["occupation"];
                if (dicKey.ContainsKey("nativeplace")) patVo.NativePlace = dicKey["nativeplace"];
                if (dicKey.ContainsKey("appDeptId")) patVo.DeptID = dicKey["appDeptId"];
                if (dicKey.ContainsKey("appDeptName")) patVo.DeptName = dicKey["appDeptName"];
                if (dicKey.ContainsKey("appDoctId")) patVo.DoctID = dicKey["appDoctId"];
                if (dicKey.ContainsKey("appDoctName")) patVo.DoctName = dicKey["appDoctName"];
                if (dicKey.ContainsKey("payTypeId")) patVo.FeeCode = dicKey["payTypeId"];
                if (dicKey.ContainsKey("currAreaId")) patVo.AreaID = dicKey["currAreaId"];
                if (dicKey.ContainsKey("currAreaName")) patVo.AreaName = dicKey["currAreaName"];
                if (dicKey.ContainsKey("currBedId")) patVo.BedID = dicKey["currBedId"];
                if (dicKey.ContainsKey("illHistory")) this.illHistory = dicKey["illHistory"];
                if (dicKey.ContainsKey("clinicDiag")) this.clinicDiag = dicKey["clinicDiag"];
                if (dicKey.ContainsKey("ipTimes")) patVo.IpTimes = Convert.ToInt32(dicKey["ipTimes"]);
                if (dicKey.ContainsKey("inDate"))
                {
                    patVo.Indate = Convert.ToDateTime(dicKey["inDate"]);
                    patVo.RegisterDate = Convert.ToDateTime(dicKey["inDate"]);
                }

                // 当前全局病人实体
                GlobalPatient.currPatient = this.patVo;
                // 当前全局登录人
                GlobalLogin.objLogin = new EntityLogin();
                GlobalLogin.objLogin.EmpNo = patVo.DoctID;
                GlobalLogin.objLogin.EmpName = patVo.DoctName;
                GlobalLogin.objLogin.DeptCode = patVo.DeptID;
                #endregion

                // initCatalog
                this.InitCatalog();
                this.Text += "  " + patVo.PatientName;
                //this.TopMost = true; 
                LoadForm((EntityCatalog)this.tvApp.GetDataRecordByNode(this.tvApp.Nodes[0]));
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region LoadForm
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="p"></param>
        void LoadForm(Point p)
        {
            TreeListNode node = this.tvApp.CalcHitInfo(p).Node;
            if (node == null) return;
            LoadForm((EntityCatalog)this.tvApp.GetDataRecordByNode(node));
        }
        /// <summary>
        /// LoadForm
        /// </summary>
        /// <param name="cataVo"></param>
        void LoadForm(EntityCatalog cataVo)
        {
            try
            {
                this.currCataVo = cataVo;
                if (cataVo == null || this.xscResult.Handle == IntPtr.Zero) return;
                if (isLoading) return;
                isLoading = true;
                uiHelper.BeginLoading(this);
                Function.SuspendLayout(this.xscResult.Handle);
                Panel pnl = new Panel();
                pnl.BackColor = Color.FromArgb(196, 200, 205);
                pnl.Height = 30;
                pnl.Dock = DockStyle.Bottom;

                efPanel = new ShowPanelForm(Function.Int(cataVo.FormId));
                efPanel.caseCode = cataVo.Code;
                efPanel.Name = "eaf" + cataVo.Code;
                efPanel.Location = new Point(12, 10);
                if (cataVo.Code.StartsWith("R")) efPanel.IsNoCtorLayout = true;

                GlobalCase.caseInfo = new Core.Entity.EntityCaseInfo();
                GlobalCase.caseInfo.FormId = Function.Int(cataVo.FormId);
                GlobalCase.caseInfo.CaseCode = cataVo.Code;

                this.xscResult.Controls.Clear();
                this.xscResult.BackColor = Color.FromArgb(196, 200, 205);
                this.xscResult.Controls.Add(efPanel);
                this.xscResult.Controls.Add(pnl);
                this.xscResult.Tag = efPanel.FormLayout;

                if (cataVo.Code.StartsWith("C"))
                {
                    this.New();
                }
                else if (cataVo.Code.StartsWith("R"))
                {
                    this.SetApp(Function.Dec(cataVo.Code.Substring(1)));
                    this.blbiSave.Enabled = ((cataVo.appStatus == 1) ? false : true);
                    this.blbiCommit.Enabled = ((cataVo.appStatus == 1) ? false : true);
                    this.blbiDel.Enabled = ((cataVo.appStatus == 1) ? false : true);
                    this.blbiExport.Enabled = true;
                    this.blbiPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                Function.ResumeLayout(this.xscResult.Handle);
                this.xscResult.Refresh();
                uiHelper.CloseLoading(this);
                isLoading = false;
            }
        }
        #endregion

        #region SetApp
        /// <summary>
        /// SetApp
        /// </summary>
        /// <param name="appId"></param>
        void SetApp(decimal appId)
        {
            if (this.appDataSource != null && this.appDataSource.Count > 0 && this.xscResult.Tag != null)
            {
                string layout = this.xscResult.Tag.ToString();
                EntityEafApplication appVo = this.appDataSource.FirstOrDefault(t => t.appId == appId);
                this.efPanel.InitComponent(layout, appVo.appData);
                this.efPanel.Tag = appVo;
            }
        }
        #endregion

        #region New
        /// <summary>
        /// New
        /// </summary>
        void New()
        {
            if (efPanel == null || this.xscResult.Handle == IntPtr.Zero) return;
            if (this.xscResult.Tag == null || string.IsNullOrEmpty(this.xscResult.Tag.ToString())) return;

            string layout = this.xscResult.Tag.ToString();
            string xmlData = string.Empty;
            if (efPanel.IsValueChanged())
            {
                if (DialogBox.Msg("申请单数据已修改，是否保存？", MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Save();
                }
            }
            try
            {
                this.blbiSave.Enabled = true;
                this.blbiCommit.Enabled = false;
                this.blbiDel.Enabled = false;
                this.blbiExport.Enabled = false;
                this.blbiPrint.Enabled = false;
                uiHelper.BeginLoading(this);
                Function.SuspendLayout(this.xscResult.Handle);
                efPanel.InitComponent(layout);
                efPanel.RefreshPatInfo();
                efPanel.SetFieldValue("FM_IllHistory", this.illHistory);
                efPanel.SetFieldValue("FM_ClinicDiag", this.clinicDiag);
                efPanel.SetSignature("FM_AppDoct", GlobalLogin.objLogin.EmpName);
                efPanel.SetFieldValue("FM_AppDate", DateTime.Now.ToString("yyyy-MM-dd"));
                efPanel.Tag = null;
                efPanel.ResetValueChange();
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                Function.ResumeLayout(this.xscResult.Handle);
                this.xscResult.Refresh();
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Save
        /// </summary>
        /// <returns></returns>
        bool Save()
        {
            if (efPanel == null) return true;
            bool isNew = false;
            EntityEafApplication appVo = new EntityEafApplication();
            if (efPanel.Tag != null)
            {
                appVo = efPanel.Tag as EntityEafApplication;
                appVo.appDate = Utils.ServerTime();
                appVo.appData = this.FilterXmlData(efPanel.XmlData());
            }
            else
            {
                appVo.appDate = Utils.ServerTime();
                appVo.sourceId = Function.Dec(patVo.PatientType);
                appVo.patientId = patVo.PatientID;
                appVo.cardNo = patVo.CardNo;
                appVo.registerId = patVo.RegisterID;
                appVo.appData = this.FilterXmlData(efPanel.XmlData());
                if (currCataVo.Code.StartsWith("C"))
                {
                    appVo.classCode = currCataVo.Code.Substring(1);
                }
                else if (currCataVo.Code.StartsWith("R"))
                {
                    appVo.classCode = currCataVo.ParentCode.Substring(1);
                }
                else
                {
                    DialogBox.Msg("检查电子申请单分类不能为空。");
                    return false;
                }
                appVo.appDeptId = patVo.DeptID;
                appVo.appDoctId = patVo.DoctID;
                appVo.status = 0;
                isNew = true;
            }

            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                int appId = 0;
                if (proxy.Service.SaveEaf(appVo, out appId) > 0)
                {
                    appVo.appId = appId;
                    efPanel.Tag = appVo;
                    DialogBox.Msg("保存成功！");

                    if (isNew)
                    {
                        this.InitCatalog();
                        this.FindApp(appId);
                        this.blbiSave.Enabled = true;
                        this.blbiCommit.Enabled = true;
                        this.blbiDel.Enabled = true;
                        this.blbiExport.Enabled = true;
                        this.blbiPrint.Enabled = true;
                    }
                }
                else
                {
                    DialogBox.Msg("保存失败。");
                }
            }
            return true;
        }
        #endregion

        #region RefreshDataSource
        /// <summary>
        /// RefreshDataSource
        /// </summary>
        void RefreshDataSource()
        {
            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                this.appDataSource = proxy.Service.GetEaf(patVo.PatientID);
            }
        }
        #endregion

        #region Commit
        /// <summary>
        /// Commit
        /// </summary>
        /// <returns></returns>
        bool Commit()
        {
            List<string> lstItemName = new List<string>();
            frmItemHint frm = new frmItemHint(this.GetCheckItems(2, out lstItemName));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                EntityEafApplication appVo = this.efPanel.Tag as EntityEafApplication;
                if (lstItemName.Count > 0)
                {
                    EntityCsRecipe recipeVo = new EntityCsRecipe();
                    recipeVo.clinicDiag = this.efPanel.GetItemInfo("FM_ClinicDiag");
                    recipeVo.isEmer = Function.Int(this.efPanel.GetItemInfo("FM_EmerFlag"));
                    appVo.putOperId = patVo.DoctID;
                    appVo.putOperName = patVo.DoctName;
                    appVo.putDate = Utils.ServerTime();
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        int ret = 0;
                        if (patVo.PatientType == "1")           // 门诊
                            ret = proxy.Service.CommitEafOp(this.patVo, appVo, recipeVo, lstItemName);
                        else if (patVo.PatientType == "2")      // 住院
                            ret = proxy.Service.CommitEafIp(this.patVo, appVo, recipeVo, lstItemName);
                        if (ret > 0)
                        {
                            DialogBox.Msg("提交成功！");
                            this.InitCatalog();
                            this.FindApp(appVo.appId);
                            this.blbiSave.Enabled = false;
                            this.blbiCommit.Enabled = false;
                            this.blbiDel.Enabled = false;
                            this.blbiExport.Enabled = true;
                            this.blbiPrint.Enabled = true;
                        }
                        else
                        {
                            DialogBox.Msg("提交失败。");
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region Del
        /// <summary>
        /// Del
        /// </summary>
        /// <returns></returns>
        bool Del()
        {
            if (this.efPanel.Tag == null)
            {
                DialogBox.Msg("请选择需要删除的申请单。");
                return false;
            }
            EntityEafApplication appVo = this.efPanel.Tag as EntityEafApplication;
            if (appVo.status == 1)
            {
                DialogBox.Msg("已提交的申请单不能删除。");
                return false;
            }
            if (DialogBox.Msg("确定删除当前申请单？", MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                using (ProxyFormDesign proxy = new ProxyFormDesign())
                {
                    if (proxy.Service.DelEaf(appVo) > 0)
                    {
                        DialogBox.Msg("删除申请单成功！");
                        this.New();
                        this.InitCatalog();
                        return true;
                    }
                    else
                    {
                        DialogBox.Msg("删除申请单失败。");
                    }
                }
            }
            return false;
        }
        #endregion

        #region Export
        /// <summary>
        /// Export
        /// </summary>
        void Export()
        {
            XtraReport xr = GetXR();
            if (xr != null && xr.DataSource != null)
            {
                uiHelper.Export(xr);
            }
            else
            {
                DialogBox.Msg("请先保存数据。");
            }
        }
        #endregion

        #region Print
        /// <summary>
        /// Print
        /// </summary>
        void Print()
        {
            XtraReport xr = GetXR();
            if (xr != null && xr.DataSource != null)
            {
                ReportPrintTool rpt = new ReportPrintTool(xr);
                rpt.ShowRibbonPreviewDialog();
                //rpt.ShowPreviewDialog();
                //xr.PrintDialog();
                //frmPrintDocumentSimple frm = new frmPrintDocumentSimple(xr);
                //frm.ShowDialog();
            }
            else
            {
                DialogBox.Msg("请先保存数据。");
            }
        }
        #endregion

        #region XR
        /// <summary>
        /// GetXR
        /// </summary>
        /// <returns></returns>
        XtraReport GetXR()
        {
            try
            {
                if (this.currCataVo != null && this.currCataVo.printTemplateId > 0)
                {
                    EntitySysReport rptVo = null;
                    using (ProxyCommon proxy = new ProxyCommon())
                    {
                        rptVo = proxy.Service.GetReport(this.currCataVo.printTemplateId);
                    }
                    if (rptVo != null && rptVo.rptFile != null)
                    {
                        XtraReport xr = new XtraReport();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(rptVo.rptFile, 0, rptVo.rptFile.Length);
                        xr.LoadLayout(ms);
                        xr.DataSource = this.GetXrDataSource();
                        xr.CreateDocument();
                        return xr;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                DialogBox.Msg(ex.Message);
            }
            return null;
        }

        void NoFieldHint(string fieldName)
        {
            DialogBox.Msg(string.Format("不存在节点： {0}", fieldName));
        }

        #region GetXrDataSource
        /// <summary>
        /// GetXrDataSource
        /// </summary>
        /// <returns></returns>
        DataTable GetXrDataSource()
        {
            string formLayout = this.efPanel.FormLayout;
            string xmlData = this.FilterXmlData(this.efPanel.XmlData());
            if (string.IsNullOrEmpty(xmlData)) return null;
            if (this.currCataVo.Code.StartsWith("R") && this.currCataVo.ParentCode.StartsWith("C"))
            {
                string appNo = this.currCataVo.Code.Substring(1);
                string classCode = this.currCataVo.ParentCode.Substring(1);
                Dictionary<string, string> dicXml = Function.ReadXmlNodes(xmlData, "FormData");
                EntityPrint printVo = new EntityPrint();
                printVo.FA = Function.ConvertImageToByte(Properties.Resources.cs, 3);
                // 申请医师.签名  FM_AppDoct
                string doctName = dicXml["FM_AppDoct"];
                Image signImage = null;
                if (!string.IsNullOrEmpty(doctName))
                {
                    byte[] signData = null;
                    using (ProxyFormDesign proxy = new ProxyFormDesign())
                    {
                        signData = proxy.Service.GetEmpSign(doctName);
                    }
                    if (signData != null)
                    {
                        signImage = Function.ConvertByteToImage(signData);
                        signImage = Function.Thumbnail(signImage, 80, 30);
                    }
                }
                if (signImage != null)
                {
                    printVo.FB = Function.ConvertImageToByte(signImage, 3);
                }

                string fieldName = string.Empty;
                if (classCode == "0001")              // 超声检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Allergy";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_BookingDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Symptom";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F12 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F13 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_CheckGoal";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F15 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F16 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Type01";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F31 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Type02";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F32 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);

                    printVo.F14 = this.GetCheckItems(1);
                    #endregion
                }
                else if (classCode == "0002" || classCode == "0003")        // 0002 CT检查申请单; 0003 X线检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Allergy";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_BookingDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Symptom";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F12 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F13 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_CheckGoal";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F15 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F16 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);

                    printVo.F14 = this.GetCheckItems(1);
                    #endregion
                }
                else if (classCode == "0004")        // MRI检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_RisResult";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F13 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);

                    printVo.F12 = this.GetCheckItems(1);
                    #endregion
                }
                else if (classCode == "0005")        // 病理检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientMarriage";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    fieldName = "PatientProfession";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientNativePlace";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientHomeAddr";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F12 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F14 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F15 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_YJZQ";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F16 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_MCYJRQ";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F17 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_YCC";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F18 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_FSSJ";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F19 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_YWZY1";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F20 = Function.Int(dicXml[fieldName]) == 1 ? "有" : "无";
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_WZ";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F21 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_DX";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F22 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_RisResult";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F23 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F24 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_OpsResult";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F25 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_SJCL";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F26 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_TypeCG";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F31 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_TypeBD";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F32 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);

                    printVo.F13 = doctName;
                    printVo.F27 = this.GetCheckItems(1);
                    #endregion
                }
                else if (classCode == "0006" || classCode == "0007" || classCode == "0008")        // 0006 心电图检查申请单; 0007 活动平板心电图检查申请单; 0008 TCD检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Pulse";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_BloodPress";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_RisResult";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F12 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F13 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppGoal";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F14 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F15 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    #endregion
                }
                else if (classCode == "0009")        // 内镜检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    fieldName = "PatientName";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F02 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientSex";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F03 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientAge";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F04 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientTel";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F05 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientDept";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F06 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientBedNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F07 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientOpIpNo";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F08 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientHomeAddr";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F09 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "PatientNativePlace";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F10 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_IllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F11 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Pulse";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F12 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_BloodPress";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F13 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AbdoOps";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F14 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_BigIllHistory";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F15 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_ClinicDiag";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F16 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_AppDate";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F17 = dicXml[fieldName];
                    else
                        this.NoFieldHint(fieldName);

                    fieldName = "FM_PatTypeZf";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F31 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_PatTypeSb";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F32 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal01";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F33 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal02";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F34 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal03";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F35 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal04";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F36 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal05";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F37 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal06";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F38 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);
                    fieldName = "FM_Goal07";
                    if (dicXml.ContainsKey(fieldName))
                        printVo.F39 = Function.Int(dicXml[fieldName]);
                    else
                        this.NoFieldHint(fieldName);

                    #endregion
                }
                return EntityTools.ConvertToDataTable<EntityPrint>(new List<EntityPrint>() { printVo }); ;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region GetCheckItems
        /// <summary>
        /// GetCheckItems
        /// </summary>
        /// <param name="flagId"></param>
        /// <returns></returns>
        string GetCheckItems(int flagId)
        {
            List<string> lstItemName = new List<string>();
            return GetCheckItems(flagId, out lstItemName);
        }
        /// <summary>
        /// GetCheckItems
        /// </summary>
        /// <returns></returns>
        string GetCheckItems(int flagId, out List<string> lstItemName)
        {
            lstItemName = new List<string>();
            int no = 0;
            string checkItems = string.Empty;
            string formLayout = this.efPanel.FormLayout;
            string xmlData = this.FilterXmlData(this.efPanel.XmlData());
            if (string.IsNullOrEmpty(xmlData)) return checkItems;
            Dictionary<string, string> dicXml = Function.ReadXmlNodes(xmlData, "FormData");
            List<EntityFormCtrl> FormCtrls = FormTool.Entities(formLayout);
            foreach (KeyValuePair<string, string> kv in dicXml)
            {
                if (kv.Key.StartsWith("FI_") && Function.Int(kv.Value) == 1)
                {
                    if (FormCtrls.Any(t => t.ItemName == kv.Key))
                    {
                        if (flagId == 1)
                            checkItems += FormCtrls.FirstOrDefault(t => t.ItemName == kv.Key).ItemCaption + ", ";
                        else if (flagId == 2)
                            checkItems += (++no).ToString() + "、" + FormCtrls.FirstOrDefault(t => t.ItemName == kv.Key).ItemCaption + Environment.NewLine;
                        lstItemName.Add(kv.Key);
                    }
                }
            }
            if (flagId == 1 && checkItems != string.Empty)
                checkItems = checkItems.Trim().TrimEnd(',');
            return checkItems;
        }
        #endregion

        #endregion

        #region 加载病历树
        /// <summary>
        /// 加载病历树
        /// </summary>
        void InitCatalog()
        {
            List<EntityEafCatalog> dataSourceCatalog = null;
            using (ProxyEntityFactory proxy = new ProxyEntityFactory())
            {
                EntityEafCatalog vo = new EntityEafCatalog();
                vo.status = 1;
                dataSourceCatalog = EntityTools.ConvertToEntityList<EntityEafCatalog>(proxy.Service.SelectSort(vo, new List<string> { EntityEafCatalog.Columns.status }, new List<string> { EntityEafCatalog.Columns.sortNo }));
            }
            // appDataSource
            this.RefreshDataSource();
            CreateCatalog(dataSourceCatalog, this.appDataSource);
        }
        /// <summary>
        /// CreateCatalog
        /// </summary>
        /// <param name="dataSourceCatalog"></param>
        /// <param name="lstRecord"></param>
        void CreateCatalog(List<EntityEafCatalog> dataSourceCatalog, List<EntityEafApplication> lstRecord)
        {
            // 树结构
            this.tvApp.Columns.Clear();
            uiHelper.SetGridCol(this.tvApp, new string[] { "Name" }, new string[] { "电子申请单" }, new int[] { 230 });
            this.tvApp.Columns["Name"].AppearanceCell.Font = new Font("宋体", 9);
            this.tvApp.KeyFieldName = "Code";
            this.tvApp.ParentFieldName = "ParentCode";
            this.tvApp.ImageIndexFieldName = "ImageIndex";

            this.tvApp.OptionsView.ShowFocusedFrame = false;
            this.tvApp.Appearance.FocusedRow.Options.UseBackColor = true;
            this.tvApp.Appearance.FocusedRow.BackColor = Color.LightGreen;    // Color.LightSkyBlue;
            this.tvApp.Appearance.FocusedRow.BackColor2 = Color.White;
            this.tvApp.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.tvApp.Appearance.HideSelectionRow.BackColor = Color.LightGreen;  // Color.LightSkyBlue;
            this.tvApp.Appearance.HideSelectionRow.BackColor2 = Color.White;

            if (lstRecord == null) lstRecord = new List<EntityEafApplication>();
            EntityCatalog recVo = null;
            List<EntityCatalog> lstRecSource = new List<EntityCatalog>();
            // 主目录
            foreach (EntityEafCatalog catalog in dataSourceCatalog)
            {
                recVo = new EntityCatalog();
                recVo.Id = string.Empty;
                recVo.Code = "C" + catalog.classCode;
                recVo.ParentCode = string.Empty;
                recVo.FormId = catalog.formId;
                recVo.ImageIndex = 2;
                recVo.Name = catalog.className;
                recVo.printTemplateId = catalog.rptId;
                recVo.appStatus = 0;
                lstRecSource.Add(recVo);
            }
            foreach (EntityEafApplication appVo in lstRecord)
            {
                recVo = new EntityCatalog();
                recVo.Id = string.Empty;
                recVo.Code = "R" + appVo.appId.ToString();
                recVo.ParentCode = "C" + appVo.classCode;
                if (dataSourceCatalog.Any(t => t.classCode == appVo.classCode))
                {
                    recVo.FormId = dataSourceCatalog.FirstOrDefault(t => t.classCode == appVo.classCode).formId;
                    recVo.printTemplateId = dataSourceCatalog.FirstOrDefault(t => t.classCode == appVo.classCode).rptId;
                }
                recVo.ImageIndex = 1;
                recVo.Name = appVo.appDate.ToString("yyyy-MM-dd HH:mm");
                recVo.appStatus = (int)appVo.status;
                lstRecSource.Add(recVo);
            }
            this.tvApp.BeginUpdate();
            this.tvApp.DataSource = lstRecSource;
            this.tvApp.MouseClick -= new MouseEventHandler(tvApp_MouseClick);
            this.tvApp.MouseClick += new MouseEventHandler(tvApp_MouseClick);
            this.tvApp.MouseDoubleClick -= new MouseEventHandler(tvApp_MouseDoubleClick);
            this.tvApp.MouseDoubleClick += new MouseEventHandler(tvApp_MouseDoubleClick);
            this.tvApp.CustomDrawNodeCell -= new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(tvApp_CustomDrawNodeCell);
            this.tvApp.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(tvApp_CustomDrawNodeCell);
            this.tvApp.ExpandAll();
            this.tvApp.EndUpdate();
        }
        #endregion

        #region 病历树事件
        /// <summary>
        /// 树操作中
        /// </summary>
        bool isTreeDoing { get; set; }
        /// <summary>
        /// tvApp_MouseClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvApp_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                if (e.Button == MouseButtons.Left)
                {
                    LoadForm(e.Location);
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                isTreeDoing = false;
            }
        }
        /// <summary>
        /// tvApp_MouseDoubleClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvApp_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isTreeDoing) return;
                isTreeDoing = true;
                if (e.Button == MouseButtons.Left)
                {
                    LoadForm(e.Location);
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                isTreeDoing = false;
            }
        }

        /// <summary>
        /// 改变图标的同时,字体颜色需要用代码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tvApp_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node != null)
            {
                EntityCatalog entity = (EntityCatalog)this.tvApp.GetDataRecordByNode(e.Node);
                if (entity.appStatus == 1)
                {
                    e.Appearance.ForeColor = Color.Orange; //Color.Green;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }
        #endregion

        #region FindNode
        /// <summary>
        /// FindNode
        /// </summary>
        void FindNode()
        {
            string val = this.txtFind.Text.Trim();

            if (val == string.Empty) return;

            this.efPanel.FindNode(val);
        }
        #endregion

        #region FindApp
        /// <summary>
        /// FindApp
        /// </summary>
        /// <param name="appId"></param>
        void FindApp(decimal appId)
        {
            EntityCatalog cataVo = null;
            for (int i = 0; i < this.tvApp.AllNodesCount; i++)
            {
                cataVo = (EntityCatalog)this.tvApp.GetDataRecordByNode(this.tvApp.GetNodeByVisibleIndex(i));
                if (cataVo.Code == "R" + appId.ToString())
                {
                    this.tvApp.SetFocusedNode(this.tvApp.GetNodeByVisibleIndex(i));
                    this.currCataVo = cataVo;
                    break;
                }
            }
        }
        #endregion

        #region FilterXmlData
        /// <summary>
        /// FilterXmlData
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        string FilterXmlData(string xmlData)
        {
            System.Text.StringBuilder newXmlData = new System.Text.StringBuilder();
            if (string.IsNullOrEmpty(xmlData)) return string.Empty;
            Dictionary<string, string> dicXml = Function.ReadXmlNodes(xmlData, "FormData");
            newXmlData.AppendLine("<FormData>");
            foreach (KeyValuePair<string, string> kv in dicXml)
            {
                if (kv.Key.StartsWith("FI_"))
                {
                    if (Function.Int(kv.Value) == 1)
                    {
                        newXmlData.AppendLine(string.Format("<{0}>{1}</{2}>", kv.Key, kv.Value, kv.Key));
                    }
                }
                else
                {
                    newXmlData.AppendLine(string.Format("<{0}>{1}</{2}>", kv.Key, kv.Value, kv.Key));
                }
            }
            newXmlData.AppendLine("</FormData>");
            return newXmlData.ToString();
        }
        #endregion

        #endregion

        #region 事件

        private void frmConsole_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void frmConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.xscResult.Handle == IntPtr.Zero)
            {
                e.Cancel = true;
            }
            else
            {
                //if (DialogBox.Msg("是否退出？", MessageBoxIcon.Question) == DialogResult.No)
                //{
                //    e.Cancel = true;
                //}
            }
        }

        private void blbiReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmReport frm = new frmReport(this.patVo.CardNo);
            frm.Show();
        }

        private void blbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.New();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void blbiCommit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Commit();
        }

        private void blbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Del();
        }

        private void blbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Export();
        }

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void blbiExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtFind_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.FindNode();
            }
        }

        #endregion

    }
}
