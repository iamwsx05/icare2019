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
    /// 电子申请单.查询
    /// </summary>
    public partial class frmQuery : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmQuery(string _request)
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

                #endregion

                #region request
                patVo = new EntityPatient();
                Dictionary<string, string> dicKey = Function.ReadXmlNodes(this.request, "request");
                if (dicKey.ContainsKey("patientId")) patVo.PatientID = dicKey["patientId"];

                // 当前全局病人实体
                GlobalPatient.currPatient = this.patVo;
                // 当前全局登录人
                GlobalLogin.objLogin = new EntityLogin();

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
                if (cataVo.Code.StartsWith("R"))
                    this.SetApp(Function.Dec(cataVo.Code.Substring(1)));
                this.currCataVo = cataVo;
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

        #region GetXrDataSource
        /// <summary>
        /// GetXrDataSource
        /// </summary>
        /// <returns></returns>
        DataTable GetXrDataSource()
        {
            string formLayout = this.efPanel.FormLayout;
            string xmlData = this.efPanel.XmlData();
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

                if (classCode == "0001")              // 超声检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientOpIpNo"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["FM_Allergy"];
                    printVo.F09 = dicXml["PatientTel"];
                    printVo.F10 = dicXml["FM_BookingDate"];
                    printVo.F11 = dicXml["FM_Symptom"];
                    printVo.F12 = dicXml["FM_IllHistory"];
                    printVo.F13 = dicXml["FM_ClinicDiag"];
                    printVo.F14 = this.GetCheckItems(1);
                    printVo.F15 = dicXml["FM_CheckGoal"];
                    printVo.F16 = dicXml["FM_AppDate"];
                    printVo.F31 = Function.Int(dicXml["FM_Type01"]);
                    printVo.F32 = Function.Int(dicXml["FM_Type02"]);
                    #endregion
                }
                else if (classCode == "0002" || classCode == "0003")        // 0002 CT检查申请单; 0003 X线检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientOpIpNo"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["FM_Allergy"];
                    printVo.F09 = dicXml["PatientTel"];
                    printVo.F10 = dicXml["FM_BookingDate"];
                    printVo.F11 = dicXml["FM_Symptom"];
                    printVo.F12 = dicXml["FM_IllHistory"];
                    printVo.F13 = dicXml["FM_ClinicDiag"];
                    printVo.F14 = this.GetCheckItems(1);
                    printVo.F15 = dicXml["FM_CheckGoal"];
                    printVo.F16 = dicXml["FM_AppDate"];
                    #endregion
                }
                else if (classCode == "0004")        // MRI检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientTel"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["PatientOpIpNo"];
                    printVo.F09 = dicXml["FM_IllHistory"];
                    printVo.F10 = dicXml["FM_RisResult"];
                    printVo.F11 = dicXml["FM_ClinicDiag"];
                    printVo.F12 = this.GetCheckItems(1);
                    printVo.F13 = dicXml["FM_AppDate"];
                    #endregion
                }
                else if (classCode == "0005")        // 病理检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientTel"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["PatientOpIpNo"];
                    printVo.F09 = dicXml["PatientMarriage"];
                    printVo.F10 = dicXml["PatientProfession"];
                    printVo.F11 = dicXml["PatientNativePlace"];
                    printVo.F12 = dicXml["PatientHomeAddr"];
                    printVo.F13 = doctName;
                    printVo.F14 = dicXml["FM_AppDate"];
                    printVo.F15 = dicXml["FM_IllHistory"];
                    printVo.F16 = dicXml["FM_YJZQ"];
                    printVo.F17 = dicXml["FM_MCYJRQ"];
                    printVo.F18 = dicXml["FM_YCC"];
                    printVo.F19 = dicXml["FM_FSSJ"];
                    printVo.F20 = Function.Int(dicXml["FM_YWZY1"]) == 1 ? "有" : "无";
                    printVo.F21 = dicXml["FM_WZ"];
                    printVo.F22 = dicXml["FM_DX"];
                    printVo.F23 = dicXml["FM_RisResult"];
                    printVo.F24 = dicXml["FM_ClinicDiag"];
                    printVo.F25 = dicXml["FM_OpsResult"];
                    printVo.F26 = dicXml["FM_SJCL"];
                    printVo.F27 = this.GetCheckItems(1);
                    printVo.F31 = Function.Int(dicXml["FM_TypeCG"]);
                    printVo.F32 = Function.Int(dicXml["FM_TypeBD"]);
                    #endregion
                }
                else if (classCode == "0006" || classCode == "0007" || classCode == "0008")        // 0006 心电图检查申请单; 0007 活动平板心电图检查申请单; 0008 TCD检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientTel"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["PatientOpIpNo"];
                    printVo.F09 = dicXml["FM_IllHistory"];
                    printVo.F10 = dicXml["FM_Pulse"];
                    printVo.F11 = dicXml["FM_BloodPress"];
                    printVo.F12 = dicXml["FM_RisResult"];
                    printVo.F13 = dicXml["FM_ClinicDiag"];
                    printVo.F14 = dicXml["FM_AppGoal"];
                    printVo.F15 = dicXml["FM_AppDate"];
                    #endregion
                }
                else if (classCode == "0009")        // 内镜检查申请单
                {
                    #region set value
                    printVo.F01 = appNo;
                    printVo.F02 = dicXml["PatientName"];
                    printVo.F03 = dicXml["PatientSex"];
                    printVo.F04 = dicXml["PatientAge"];
                    printVo.F05 = dicXml["PatientTel"];
                    printVo.F06 = dicXml["PatientDept"];
                    printVo.F07 = dicXml["PatientBedNo"];
                    printVo.F08 = dicXml["PatientOpIpNo"];
                    printVo.F09 = dicXml["PatientHomeAddr"];
                    printVo.F10 = dicXml["PatientNativePlace"];
                    printVo.F11 = dicXml["FM_IllHistory"];
                    printVo.F12 = dicXml["FM_Pulse"];
                    printVo.F13 = dicXml["FM_BloodPress"];
                    printVo.F14 = dicXml["FM_AbdoOps"];
                    printVo.F15 = dicXml["FM_BigIllHistory"];
                    printVo.F16 = dicXml["FM_ClinicDiag"];
                    printVo.F17 = dicXml["FM_AppDate"];
                    printVo.F31 = Function.Int(dicXml["FM_PatTypeZf"]);
                    printVo.F32 = Function.Int(dicXml["FM_PatTypeSb"]);
                    printVo.F33 = Function.Int(dicXml["FM_Goal01"]);
                    printVo.F34 = Function.Int(dicXml["FM_Goal02"]);
                    printVo.F35 = Function.Int(dicXml["FM_Goal03"]);
                    printVo.F36 = Function.Int(dicXml["FM_Goal04"]);
                    printVo.F37 = Function.Int(dicXml["FM_Goal05"]);
                    printVo.F38 = Function.Int(dicXml["FM_Goal06"]);
                    printVo.F39 = Function.Int(dicXml["FM_Goal07"]);
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
            string xmlData = this.efPanel.XmlData();
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
            }
        }
        #endregion

        #endregion

        #region 事件

        private void frmQuery_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Export();
        }

        private void blbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Print();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
