using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace weCare.eApp
{
    /// <summary>
    /// 检查报告单
    /// </summary>
    public partial class frmReport : frmBasePopup
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmReport(string _cardNo)
        {
            InitializeComponent();
            this.CardNo = _cardNo;
            this.fpnlBL.Visible = false;
            this.webBrowser.Visible = false;
        }
        #endregion

        #region 变量.属性

        string CardNo { get; set; }

        List<EntityCatalog> appDataSource { get; set; }

        bool isLoading { get; set; }

        #endregion

        #region 方法

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
                #endregion

                uiHelper.BeginLoading(this);
                // initCatalog
                this.InitCatalog();
                LoadReport((EntityCatalog)this.tvApp.GetDataRecordByNode(this.tvApp.Nodes[0]));
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

        #region 加载报告单
        /// <summary>
        /// 加载报告单
        /// </summary>
        void InitCatalog()
        {
            List<EntityEafCatalog> dataSourceCatalog = new List<EntityEafCatalog>();
            dataSourceCatalog.Add(new EntityEafCatalog() { classCode = "pacs", className = "PACS报告" });
            dataSourceCatalog.Add(new EntityEafCatalog() { classCode = "bl", className = "病理报告" });
            // appDataSource
            this.RefreshDataSource();
            CreateCatalog(dataSourceCatalog, this.appDataSource);
        }
        /// <summary>
        /// CreateCatalog
        /// </summary>
        /// <param name="dataSourceCatalog"></param>
        /// <param name="lstRecord"></param>
        void CreateCatalog(List<EntityEafCatalog> dataSourceCatalog, List<EntityCatalog> lstRecord)
        {
            // 树结构
            this.tvApp.Columns.Clear();
            uiHelper.SetGridCol(this.tvApp, new string[] { "Name" }, new string[] { "检查报告单" }, new int[] { 230 });
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

            EntityCatalog recVo = null;
            List<EntityCatalog> lstRecSource = new List<EntityCatalog>();
            // 主目录
            foreach (EntityEafCatalog catalog in dataSourceCatalog)
            {
                recVo = new EntityCatalog();
                recVo.Id = string.Empty;
                recVo.Code = catalog.classCode;
                recVo.ParentCode = string.Empty;
                recVo.FormId = catalog.formId;
                recVo.ImageIndex = 2;
                recVo.Name = catalog.className;
                recVo.printTemplateId = catalog.rptId;
                recVo.appStatus = 0;
                lstRecSource.Add(recVo);
            }
            if (lstRecord != null && lstRecord.Count > 0)
                lstRecSource.AddRange(lstRecord);

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

        #region 报告单树事件
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
                    LoadReport(e.Location);
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
                    LoadReport(e.Location);
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

        #region LoadReport
        /// <summary>
        /// LoadReport
        /// </summary>
        /// <param name="p"></param>
        void LoadReport(Point p)
        {
            TreeListNode node = this.tvApp.CalcHitInfo(p).Node;
            if (node == null) return;
            LoadReport((EntityCatalog)this.tvApp.GetDataRecordByNode(node));
        }
        /// <summary>
        /// LoadReport
        /// </summary>
        /// <param name="cataVo"></param>
        void LoadReport(EntityCatalog cataVo)
        {
            try
            {
                if (cataVo == null || this.xscResult.Handle == IntPtr.Zero) return;
                if (isLoading) return;
                isLoading = true;
                uiHelper.BeginLoading(this);
                if (cataVo.Code.StartsWith("R"))
                {
                    if (cataVo.ParentCode == "pacs")
                    {
                        this.webBrowser.Visible = true;
                        this.webBrowser.Dock = DockStyle.Fill;
                        this.fpnlBL.Visible = false;
                        if (string.IsNullOrEmpty(cataVo.rptInfo))
                            this.webBrowser.Navigate(string.Empty);
                        else
                            this.webBrowser.Navigate(cataVo.rptInfo);
                        this.webBrowser.Tag = cataVo.Code.Substring(1);
                    }
                    else if (cataVo.ParentCode == "bl")
                    {
                        if (cataVo.rptImage != null)
                        {
                            this.webBrowser.Visible = false;
                            this.fpnlBL.Dock = DockStyle.Fill;
                            this.fpnlBL.Visible = true;
                            this.picBL.Image = cataVo.rptImage;
                            this.picBL.Width = cataVo.rptImage.Width;
                            this.picBL.Height = cataVo.rptImage.Height;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
            }
            finally
            {
                uiHelper.CloseLoading(this);
                isLoading = false;
            }
        }
        #endregion

        #region RefreshDataSource
        /// <summary>
        /// RefreshDataSource
        /// </summary>
        void RefreshDataSource()
        {
            this.appDataSource = new List<EntityCatalog>();
            EntityCatalog recVo = null;
            DataTable dt = null;
            using (ProxyFormDesign proxy = new ProxyFormDesign())
            {
                dt = proxy.Service.GetNewPacsView(this.CardNo);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        recVo = new EntityCatalog();
                        recVo.Id = string.Empty;
                        recVo.Code = "R" + dr["exid"].ToString();
                        recVo.ParentCode = "pacs";
                        if (dr["bgsj"] == DBNull.Value)
                            recVo.Name = dr["jczt"].ToString();
                        else
                            recVo.Name = Convert.ToDateTime(dr["bgsj"]).ToString("yyyy-MM-dd HH:mm");
                        recVo.rptInfo = dr["bgdz"].ToString();
                        recVo.appStatus = 1;
                        recVo.ImageIndex = 1;
                        this.appDataSource.Add(recVo);
                    }
                }
                string blAppId = proxy.Service.GetNewBLAppId(this.CardNo);
                if (!string.IsNullOrEmpty(blAppId))
                {
                    dt = proxy.Service.GetNewBLView(blAppId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            recVo = new EntityCatalog();
                            recVo.Id = string.Empty;
                            recVo.Code = "R" + dr["applyid"].ToString();
                            recVo.ParentCode = "bl";
                            recVo.Name = Convert.ToDateTime(dr["examtime"]).ToString("yyyy-MM-dd HH:mm");
                            if (dr["ReportImage"] != DBNull.Value)
                            {
                                System.IO.MemoryStream ms = new System.IO.MemoryStream((byte[])dr["ReportImage"]);
                                recVo.rptImage = System.Drawing.Image.FromStream(ms);
                            }
                            recVo.appStatus = 1;
                            recVo.ImageIndex = 1;
                            this.appDataSource.Add(recVo);
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 事件

        private void frmReport_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void blbiPacsImage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.webBrowser.Visible && this.webBrowser.Tag != null)
            {
                System.Diagnostics.Process pro = new System.Diagnostics.Process();
                pro.StartInfo.FileName = Application.StartupPath + @"\PACSReport\ClinicalAccessTo.exe";
                pro.StartInfo.Arguments = this.webBrowser.Tag.ToString();
                pro.Start();
            }
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
