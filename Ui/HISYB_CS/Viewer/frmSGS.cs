using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using GSSB;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 省工伤社保
    /// </summary>
    public partial class frmSGS : Form
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmSGS()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                //Common.Entity.GlobalLogin.SkinName = "Visual Studio 2013 Blue";
                Common.Entity.GlobalLogin.SkinMaskColorValue = "ff05fff9|255|5|255|249";
                this.defaultLookAndFeel.LookAndFeel.SkinName = Common.Entity.GlobalLogin.SkinName;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Common.Entity.GlobalLogin.SkinName);
                this.defaultLookAndFeel.LookAndFeel.SkinMaskColor = Common.Entity.GlobalLogin.SkinMaskColor;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(Common.Entity.GlobalLogin.SkinMaskColor, Common.Entity.GlobalLogin.SkinMaskColor2);
            }
        }
        public frmSGS(int _pageNo)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                //Common.Entity.GlobalLogin.SkinName = "Visual Studio 2013 Blue";
                Common.Entity.GlobalLogin.SkinMaskColorValue = "ff05fff9|255|5|255|249";
                this.defaultLookAndFeel.LookAndFeel.SkinName = Common.Entity.GlobalLogin.SkinName;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Common.Entity.GlobalLogin.SkinName);
                this.defaultLookAndFeel.LookAndFeel.SkinMaskColor = Common.Entity.GlobalLogin.SkinMaskColor;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(Common.Entity.GlobalLogin.SkinMaskColor, Common.Entity.GlobalLogin.SkinMaskColor2);
            }
            pageNo = _pageNo;
        }
        #endregion

        #region var/property
        public int pageNo { get; set; }
        /// <summary>
        /// 登记修改 1 
        /// </summary>
        public string RegisterId { get; set; }

        public DataTable DataSourceArea { get; set; }
        public DataTable DataSourceDoct { get; set; }
        public DataTable DataSourceICD10 { get; set; }

        List<EntityDict> DataSourceRYFS { get; set; }
        List<EntityDict> DataSourceRYQK { get; set; }
        List<EntityDict> DataSourceXX { get; set; }
        List<EntityDict> DataSourceCYZG { get; set; }

        /// <summary>
        /// 卡信息
        /// </summary>
        List<EntityFK> DataSourceCard { get; set; }
        /// <summary>
        /// 登记人员工号
        /// </summary>
        public string LoginOperCode { get; set; }
        /// <summary>
        /// 登记人员姓名
        /// </summary>
        public string loginOperName { get; set; }

        /// <summary>
        /// 病人行记录
        /// </summary>
        DataRow drPat { get; set; }

        /// <summary>
        /// 省工伤优惠金额
        /// </summary>
        public decimal SGSTotalSum { get; set; }

        /// <summary>
        /// 省工伤优惠金额
        /// </summary>
        public decimal SGSAccSum { get; set; }

        #endregion

        #region method

        #region Msg
        /// <summary>
        /// Msg
        /// </summary>
        /// <param name="info"></param>
        void Msg(string info)
        {
            Common.Controls.DialogBox.Msg(info);
            //MessageBox.Show(info, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region GetRequest
        /// <summary>
        /// GetRequest
        /// </summary>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        string GetRequest(Dictionary<string, string> dicKey)
        {
            StringBuilder request = new StringBuilder();
            request.AppendLine("<request>");
            foreach (KeyValuePair<string, string> key in dicKey)
            {
                request.AppendLine(string.Format("<{0}>{1}</{0}>", key.Key, key.Value));
            }
            request.AppendLine("</request>");
            return request.ToString();
        }
        #endregion

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            #region enable
            //this.blbiInReg.Enabled = false;
            //this.blbiModifyInReg.Enabled = false;
            //this.blbiCancelInReg.Enabled = false;
            //this.blbiItemUpload.Enabled = false;
            //this.blbiCancelItemUpload.Enabled = false;
            //this.blbiTrial.Enabled = false;
            //this.blbiOutReg.Enabled = false;
            //this.blbiCancelOutReg.Enabled = false;
            //this.blbiCharge.Enabled = false;
            //this.blbiCancelCharge.Enabled = false;
            //this.blbiComplete.Enabled = false;
            this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.blbiComplete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            #endregion

            DataTable dt = null;
            using (weCare.Proxy.ProxyIP proxy = new weCare.Proxy.ProxyIP())
            {
                dt = proxy.Service.GetSGSPatInfo(this.RegisterId);
            }
            DataRow dr = dt.Rows[0];
            this.dtmZyrq1.Text = Convert.ToDateTime(dr["indate"]).ToString("yyyy-MM-dd");
            this.txtZyh1.Text = dr["ipno"].ToString();
            this.txtXm1.Text = dr["patname"].ToString();
            this.txtSfzh1.Text = dr["idcardno"].ToString();
            if (dr["inareacode"] != DBNull.Value && dr["inareacode"].ToString().Trim() != "")
            {
                this.lueBqmc1.SetDisplayText<EntityDict>(dr["inareaname"].ToString());
            }
            this.txtCh1.Text = dr["inbedcode"].ToString();
            if (dr["indoctno"] != DBNull.Value && dr["indoctno"].ToString().Trim() != "")
            {
                this.lueZzys1.SetDisplayText<EntityDict>(dr["indoctname"].ToString());
            }
            if (dr["indiagicd10code"] != DBNull.Value && dr["indiagicd10code"].ToString().Trim() != "")
            {
                this.lueIcd1.SetDisplayText<EntityDict>(dr["indiagicd10name"].ToString());
            }
            if (dr["outdiagicd10code"] != DBNull.Value && dr["outdiagicd10code"].ToString().Trim() != "")
            {
                this.lueIcd2.SetDisplayText<EntityDict>(dr["outdiagicd10name"].ToString());
            }
            this.txtZyrq3.Text = Convert.ToDateTime(dr["indate"]).ToString("yyyy-MM-dd");
            this.txtZyh3.Text = dr["ipno"].ToString();
            this.txtXm3.Text = dr["patname"].ToString();
            this.txtSfzh3.Text = dr["idcardno"].ToString();
            this.txtJydjh3.Text = dr["registerno"].ToString();
            this.txtCybq3.Text = dr["outareaname"].ToString();
            this.txtCh3.Text = dr["outbedcode"].ToString();
            this.txtZzys3.Text = dr["outdoctname"].ToString();
            if (dr["inway"] != DBNull.Value && dr["inway"].ToString().Trim() != "")
            {
                this.lueRyfs3.SetDisplayText<EntityDict>(this.DataSourceRYFS.FirstOrDefault(p => p.Id == dr["inway"].ToString()).Name);
            }
            if (dr["insituation"] != DBNull.Value && dr["insituation"].ToString().Trim() != "")
            {
                this.lueRyqk3.SetDisplayText<EntityDict>(this.DataSourceRYQK.FirstOrDefault(p => p.Id == dr["insituation"].ToString()).Name);
            }
            if (dr["blood"] != DBNull.Value && dr["blood"].ToString().Trim() != "")
            {
                this.lueXx3.SetDisplayText<EntityDict>(this.DataSourceXX.FirstOrDefault(p => p.Id == dr["blood"].ToString()).Name);
            }
            if (dr["outcome"] != DBNull.Value && dr["outcome"].ToString().Trim() != "")
            {
                this.lueCyzg3.SetDisplayText<EntityDict>(this.DataSourceCYZG.FirstOrDefault(p => p.Id == dr["outcome"].ToString()).Name);
            }
            if (dr["gsbxzf"] != DBNull.Value && Function.Dec(dr["gsbxzf"]) > 0)
            {
                this.txtGsAcctSum2.Text = dr["gsbxzf"].ToString();
            }
            this.dtmCyrq3.Text = dr["outdate"] == DBNull.Value ? "" : Convert.ToDateTime(dr["outdate"]).ToString("yyyy-MM-dd");
            this.txtQjcs3.Text = dr["rescuetime"].ToString();
            this.txtQjcgcs3.Text = dr["rescuetimesuce"].ToString();
            this.txtCyzd3.Text = dr["outdiag"].ToString();
            this.txtCysm3.Text = dr["outdesc"].ToString();
            dt.Columns["registerno"].ReadOnly = false;
            dt.Columns["isCharge"].ReadOnly = false;
            dt.Columns["registerno"].MaxLength = 200;
            this.drPat = dr;

            if (pageNo == 1)
            {
                // 已入院登记
                if (dr["registerno"] != DBNull.Value && dr["registerno"].ToString().Trim() != "")
                {
                    this.txtDnh1.Text = dr["COMPUTERNO"].ToString();
                    this.txtShbzhm1.Text = dr["SOCIALNO"].ToString();
                    this.txtRylb1.Text = dr["rylb"].ToString();
                    this.txtCbdbm1.Text = dr["CBDBM"].ToString();
                    this.txtXmcb1.Text = dr["name"].ToString();
                    this.txtXb1.Text = dr["sex"].ToString();
                    this.txtCsrq1.Text = dr["birthday"].ToString();
                    this.txtGspzh1.Text = dr["WORKNO"].ToString();
                    this.txtLxdh1.Text = dr["lxdh"].ToString();
                    this.txtDwbm1.Text = dr["dwbm"].ToString();
                    this.txtDwmc1.Text = dr["dwmc"].ToString();
                    this.txtKsrq1.Text = dr["begindate"].ToString();
                    this.txtJsrq1.Text = dr["enddate"].ToString();
                    this.txtGrjjzt1.Text = dr["grjjzt"].ToString();
                    this.txtRegisterno.Text = dr["registerno"].ToString();


                    this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.xtraTabControl.TabPages.Remove(xtraTabPage3);
                    this.xtraTabControl.TabPages.Remove(xtraTabPage2);
                }
                else
                {
                    this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.xtraTabControl.TabPages.Remove(xtraTabPage3);
                    this.xtraTabControl.TabPages.Remove(xtraTabPage2);
                }
            }
            else
            {
                this.xtraTabControl.TabPages.Remove(xtraTabPage1);
                using (weCare.Proxy.ProxyYB proxy = new weCare.Proxy.ProxyYB())
                {
                    string minDate = string.Empty;
                    string maxDate = string.Empty;
                    proxy.Service.m_lngGetZYFYSJ(this.RegisterId, out maxDate, out minDate);
                    this.dteFeeStart.Text = Convert.ToDateTime(minDate).ToString("yyyy-MM-dd");
                    this.dteFeeEnd.Text = Convert.ToDateTime(maxDate).ToString("yyyy-MM-dd");
                }

                if (dr["isCharge"] != DBNull.Value && dr["isCharge"].ToString() == "1")
                {
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                // 已作出院登记
                else if (dr["cydj"] != DBNull.Value && dr["cydj"].ToString() == "1")
                {
                    this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    // 已上传收费项目
                    if (dr["isUp"] != DBNull.Value && Convert.ToInt32(dr["isUp"].ToString()) == 1)
                    {
                        QueryFeeItems();
                        this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                }
            }
        }
        #endregion

        #region InitLue
        /// <summary>
        /// InitLue
        /// </summary>
        void InitLue()
        {
            #region 病区
            List<EntityDict> lstBq = new List<EntityDict>();
            if (this.DataSourceArea != null && this.DataSourceArea.Rows.Count > 0)
            {
                foreach (DataRow dr in this.DataSourceArea.Rows)
                {
                    lstBq.Add(new EntityDict() { Id = dr["code_vchr"].ToString(), Name = dr["deptname_vchr"].ToString(), PyCode = dr["pycode_chr"] == DBNull.Value ? "" : dr["pycode_chr"].ToString().ToUpper() });
                }
            }
            this.lueBqmc1.Properties.PopupWidth = 220;
            this.lueBqmc1.Properties.PopupHeight = 350;
            this.lueBqmc1.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueBqmc1.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueBqmc1.Properties.Essential = false;
            this.lueBqmc1.Properties.IsShowColumnHeaders = true;
            this.lueBqmc1.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 70);
            this.lueBqmc1.Properties.ColumnWidth.Add(EntityDict.Columns.Name, 150);
            this.lueBqmc1.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "编码");
            this.lueBqmc1.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "名称");
            this.lueBqmc1.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueBqmc1.Properties.IsUseShowColumn = true;
            this.lueBqmc1.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.lueBqmc1.Properties.DataSource = lstBq.ToArray();
            this.lueBqmc1.Properties.SetSize();
            #endregion

            #region 医师
            List<EntityDict> lstYs = new List<EntityDict>();
            if (this.DataSourceDoct != null && this.DataSourceDoct.Rows.Count > 0)
            {
                foreach (DataRow dr in this.DataSourceDoct.Rows)
                {
                    lstYs.Add(new EntityDict() { Id = dr["empno_chr"].ToString(), Name = dr["doctorname"].ToString(), PyCode = dr["pycode_chr"] == DBNull.Value ? "" : dr["pycode_chr"].ToString().ToUpper() });
                }
            }
            this.lueZzys1.Properties.PopupWidth = 150;
            this.lueZzys1.Properties.PopupHeight = 350;
            this.lueZzys1.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueZzys1.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueZzys1.Properties.Essential = false;
            this.lueZzys1.Properties.IsShowColumnHeaders = true;
            this.lueZzys1.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 60);
            this.lueZzys1.Properties.ColumnWidth.Add(EntityDict.Columns.Name, 90);
            this.lueZzys1.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "工号");
            this.lueZzys1.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "姓名");
            this.lueZzys1.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueZzys1.Properties.IsUseShowColumn = true;
            this.lueZzys1.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.lueZzys1.Properties.DataSource = lstYs.ToArray();
            this.lueZzys1.Properties.SetSize();
            #endregion

            #region ICD10
            List<EntityDict> lstIcd10 = new List<EntityDict>();
            if (this.DataSourceICD10 != null && this.DataSourceICD10.Rows.Count > 0)
            {
                foreach (DataRow dr in this.DataSourceICD10.Rows)
                {
                    lstIcd10.Add(new EntityDict() { Id = dr["code"].ToString(), Name = dr["name"].ToString(), PyCode = dr["py"] == DBNull.Value ? "" : dr["py"].ToString().ToUpper() });
                }
            }
            this.lueIcd1.Properties.PopupWidth = this.lueIcd1.Width;
            this.lueIcd1.Properties.PopupHeight = 350;
            this.lueIcd1.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueIcd1.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueIcd1.Properties.Essential = false;
            this.lueIcd1.Properties.IsShowColumnHeaders = true;
            this.lueIcd1.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 100);
            this.lueIcd1.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueIcd1.Width - 100);
            this.lueIcd1.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "ICD编码");
            this.lueIcd1.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "ICD名称");
            this.lueIcd1.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueIcd1.Properties.IsUseShowColumn = true;
            this.lueIcd1.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.lueIcd1.Properties.DataSource = lstIcd10.ToArray();
            this.lueIcd1.Properties.SetSize();

            this.lueIcd2.Properties.PopupWidth = this.lueIcd2.Width;
            this.lueIcd2.Properties.PopupHeight = 350;
            this.lueIcd2.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueIcd2.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueIcd2.Properties.Essential = false;
            this.lueIcd2.Properties.IsShowColumnHeaders = true;
            this.lueIcd2.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 100);
            this.lueIcd2.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueIcd2.Width - 100);
            this.lueIcd2.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "ICD编码");
            this.lueIcd2.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "ICD名称");
            this.lueIcd2.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueIcd2.Properties.IsUseShowColumn = true;
            this.lueIcd2.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.lueIcd2.Properties.DataSource = lstIcd10.ToArray();
            this.lueIcd2.Properties.SetSize();
            #endregion

            #region 入院方式
            this.lueRyfs3.Properties.PopupWidth = this.lueRyfs3.Width;
            this.lueRyfs3.Properties.PopupHeight = 90;
            this.lueRyfs3.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueRyfs3.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueRyfs3.Properties.Essential = false;
            this.lueRyfs3.Properties.IsShowColumnHeaders = true;
            this.lueRyfs3.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 40);
            this.lueRyfs3.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueRyfs3.Width - 40);
            this.lueRyfs3.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "编码");
            this.lueRyfs3.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "名称");
            this.lueRyfs3.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueRyfs3.Properties.IsUseShowColumn = true;
            this.lueRyfs3.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.DataSourceRYFS = (new GSSB.Dictionary()).GetRYFS();
            this.lueRyfs3.Properties.DataSource = this.DataSourceRYFS.ToArray();
            this.lueRyfs3.Properties.SetSize();
            #endregion

            #region 入院情况
            this.lueRyqk3.Properties.PopupWidth = this.lueRyqk3.Width;
            this.lueRyqk3.Properties.PopupHeight = 120;
            this.lueRyqk3.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueRyqk3.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueRyqk3.Properties.Essential = false;
            this.lueRyqk3.Properties.IsShowColumnHeaders = true;
            this.lueRyqk3.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 40);
            this.lueRyqk3.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueRyqk3.Width - 40);
            this.lueRyqk3.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "编码");
            this.lueRyqk3.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "名称");
            this.lueRyqk3.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueRyqk3.Properties.IsUseShowColumn = true;
            this.lueRyqk3.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.DataSourceRYQK = (new GSSB.Dictionary()).GetRYQK();
            this.lueRyqk3.Properties.DataSource = this.DataSourceRYQK.ToArray();
            this.lueRyqk3.Properties.SetSize();
            #endregion

            #region 血型
            this.lueXx3.Properties.PopupWidth = this.lueXx3.Width;
            this.lueXx3.Properties.PopupHeight = 200;
            this.lueXx3.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueXx3.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueXx3.Properties.Essential = false;
            this.lueXx3.Properties.IsShowColumnHeaders = true;
            this.lueXx3.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 40);
            this.lueXx3.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueXx3.Width - 40);
            this.lueXx3.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "编码");
            this.lueXx3.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "名称");
            this.lueXx3.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueXx3.Properties.IsUseShowColumn = true;
            this.lueXx3.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.DataSourceXX = (new GSSB.Dictionary()).GetXX();
            this.lueXx3.Properties.DataSource = this.DataSourceXX.ToArray();
            this.lueXx3.Properties.SetSize();
            #endregion

            #region 出院转归
            this.lueCyzg3.Properties.PopupWidth = this.lueCyzg3.Width;
            this.lueCyzg3.Properties.PopupHeight = 200;
            this.lueCyzg3.Properties.ValueColumn = EntityDict.Columns.Id;
            this.lueCyzg3.Properties.DisplayColumn = EntityDict.Columns.Name;
            this.lueCyzg3.Properties.Essential = false;
            this.lueCyzg3.Properties.IsShowColumnHeaders = true;
            this.lueCyzg3.Properties.ColumnWidth.Add(EntityDict.Columns.Id, 50);
            this.lueCyzg3.Properties.ColumnWidth.Add(EntityDict.Columns.Name, this.lueCyzg3.Width - 50);
            this.lueCyzg3.Properties.ColumnHeaders.Add(EntityDict.Columns.Id, "编码");
            this.lueCyzg3.Properties.ColumnHeaders.Add(EntityDict.Columns.Name, "名称");
            this.lueCyzg3.Properties.ShowColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name;
            this.lueCyzg3.Properties.IsUseShowColumn = true;
            this.lueCyzg3.Properties.FilterColumn = EntityDict.Columns.Id + "|" + EntityDict.Columns.Name + "|" + EntityDict.Columns.PyCode;
            this.DataSourceCYZG = (new GSSB.Dictionary()).GetCYZG();
            this.lueCyzg3.Properties.DataSource = this.DataSourceCYZG.ToArray();
            this.lueCyzg3.Properties.SetSize();
            #endregion

        }
        #endregion

        #region 读卡
        /// <summary>
        /// ReadCard
        /// </summary>
        void ReadCard()
        {
            try
            {
                #region 暂时屏蔽
                /*
                string message = string.Empty;
                //message = GSSB.Message.ReadCardBase();
                message = " 1 | 639900 | 111111198101011110 | X00000019 | 639900D15600000500BF7C7A48FB4966 | 张三 | 00814E43238697159900BF7C7A | 1.00 | 20101001 | 20201001 | 410100813475 | 终端设备号 | 持卡就诊登记许可号 |";
                string[] data = message.Split('|');
                int len = data.Length;

                // 错误代码(“1”)、发卡地区行政区划代码（卡识别码前6位）、社会保障号码、卡号、卡识别码、姓名、卡复位信息（仅取历史字节）、规范版本、发卡日期、卡有效期、终端机编号、终端设备号、持卡就诊登记许可号。各数据项之间以“|”分割，且最后一个数据项以“|”结尾
                EntityFK vo = null;
                DataSourceCard = new List<EntityFK>();
                for (int i = 0; i < len - 1; i++)
                {
                    vo = new EntityFK();
                    vo.FId = i.ToString();
                    vo.FValue = data[i] == null ? "" : data[i].Trim();
                    switch (i)
                    {
                        case 0:
                            vo.FName = "返回状态";
                            vo.FValue = string.IsNullOrEmpty(vo.FValue) ? "就绪..." : ((vo.FValue == "1" ? "成功" : "失败"));
                            break;
                        case 1:
                            vo.FName = "发卡地区";
                            break;
                        case 2:
                            vo.FName = "社会保障号";
                            break;
                        case 3:
                            vo.FName = "卡号";
                            break;
                        case 4:
                            vo.FName = "卡识别码";
                            break;
                        case 5:
                            vo.FName = "姓名";
                            break;
                        case 6:
                            vo.FName = "卡复位信息";
                            break;
                        case 7:
                            vo.FName = "规范版本";
                            break;
                        case 8:
                            vo.FName = "发卡日期";
                            break;
                        case 9:
                            vo.FName = "卡有效期";
                            break;
                        case 10:
                            vo.FName = "终端机编号";
                            break;
                        case 11:
                            vo.FName = "终端设备号";
                            break;
                        case 12:
                            vo.FName = "登记许可号";
                            break;
                        case 13:
                            break;
                        case 14:
                            break;
                        default:
                            break;
                    }
                    DataSourceCard.Add(vo);
                } */
                #endregion

                frmPopupTestPats frm = new frmPopupTestPats();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataSourceCard = new List<EntityFK>();
                    DataSourceCard.Add(new EntityFK() { FId = "A", FName = "电脑号", FValue = frm.Dnh });
                }
                else
                {
                    return;
                }

                if (DataSourceCard != null && DataSourceCard.Any(p => p.FName == "电脑号"))   //"社会保障号"))
                {
                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    dicKey.Add("socialNo", DataSourceCard.FirstOrDefault(p => p.FName == "电脑号").FValue);       //"社会保障号").FValue);
                    dicKey.Add("inDate", this.dtmZyrq1.DateTime.ToString("yyyyMMdd"));
                    string error = string.Empty;
                    DataTable dtResult = null;
                    if (GSSB.Message.Access("biz120001", GetRequest(dicKey), out dtResult, out error))
                    {
                        if (dtResult != null && dtResult.Rows.Count > 0)
                        {
                            DataRow dr = dtResult.Rows[0];
                            this.txtDnh1.Text = dr["aac001"].ToString();
                            this.txtShbzhm1.Text = dr["aac002"].ToString();
                            this.txtRylb1.Text = dr["bka004"].ToString();
                            this.txtCbdbm1.Text = dr["baa027"].ToString();
                            this.txtXmcb1.Text = dr["aac003"].ToString();
                            this.txtXb1.Text = dr["aac004"].ToString() == "2" ? "女" : "男";
                            this.txtCsrq1.Text = dr["aac006"].ToString();
                            this.txtGspzh1.Text = dr["bka042"].ToString();
                            this.txtLxdh1.Text = dr["aae005"].ToString();
                            this.txtDwbm1.Text = dr["aab001"].ToString();
                            this.txtDwmc1.Text = dr["bka008"].ToString();
                            this.txtKsrq1.Text = dr["aae030"].ToString();
                            this.txtJsrq1.Text = dr["aae031"].ToString();
                            if (dr["bka888"].ToString() == "0")
                                this.txtGrjjzt1.Text = "正常";
                            else if (dr["bka888"].ToString() == "1")
                                this.txtGrjjzt1.Text = "冻结";
                            else if (dr["bka888"].ToString() == "9")
                                this.txtGrjjzt1.Text = "未参保";
                            else
                                this.txtGrjjzt1.Text = "未参保";
                        }
                        else
                        {
                            this.Msg("读卡失败.");
                        }
                    }
                    else
                    {
                        this.Msg(error);
                    }
                }
                else
                {
                    this.Msg("读卡失败.");
                }
            }
            catch (Exception ex)
            {

                this.Msg(ex.Message);
            }
        }
        #endregion

        #region 保存入院登记
        /// <summary>
        /// 保存入院登记
        /// </summary>
        void SaveInRegister()
        {
            if (this.txtShbzhm1.Text == "")
            {
                this.Msg("请读社保卡。");
                return;
            }
            if (this.txtGrjjzt1.Text != "正常")
            {
                this.Msg("个人账户不正常。");
                return;
            }
            if (this.txtXmcb1.Text.Trim() != this.txtXm1.Text.Trim())
            {
                this.Msg("参保姓名与HIS姓名不一致。");
                return;
            }
            string areaCode = (this.lueBqmc1.Properties.DBRow as EntityDict).Id;
            if (string.IsNullOrEmpty(areaCode))
            {
                this.Msg("请选择病区。");
                return;
            }
            if (this.lueIcd1.Text.Trim() == "")
            {
                this.Msg("请选择入院主要诊断(ICD)。");
                return;
            }
            if (this.lueZzys1.Text.Trim() == "")
            {
                this.Msg("请选择主治医师。");
                return;
            }
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 电脑号
            dicKey.Add("computerno", this.txtDnh1.Text);
            // 住院时间 格式：yyyyMMdd
            dicKey.Add("inDate", this.dtmZyrq1.DateTime.ToString("yyyyMMdd"));
            // 登记人员工号
            dicKey.Add("operCode", this.LoginOperCode);
            // 登记人姓名
            dicKey.Add("operName", this.loginOperName);
            // 病区编码
            dicKey.Add("areaCode", areaCode);
            // 病区名称
            dicKey.Add("areaName", (this.lueBqmc1.Properties.DBRow as EntityDict).Name);
            // 就诊科室
            dicKey.Add("deptCode", areaCode);
            // 就诊科室名称
            dicKey.Add("deptName", (this.lueBqmc1.Properties.DBRow as EntityDict).Name);
            // 诊断 疾病ICD编码
            dicKey.Add("icdCode", (this.lueIcd1.Properties.DBRow as EntityDict).Id);
            // 住院号
            dicKey.Add("ipNo", this.txtZyh1.Text.Trim());
            // 床位号
            dicKey.Add("bedNo", this.txtCh1.Text.Trim());
            // 医师编码
            dicKey.Add("doctCode", (this.lueZzys1.Properties.DBRow as EntityDict).Id);

            string error = string.Empty;
            DataTable dtResult = null;
            if (GSSB.Message.Access("biz120103", GetRequest(dicKey), out dtResult, out error))
            {
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    EntitySGSRegister vo = new EntitySGSRegister();
                    vo.RegisterNo = dtResult.Rows[0]["aaz218"].ToString();
                    vo.RegisterId = drPat["registerid"].ToString();
                    vo.ComputerNo = this.txtDnh1.Text;
                    vo.SocialNo = this.txtShbzhm1.Text;
                    vo.WorkNo = this.txtGspzh1.Text;
                    vo.InAreaCode = dicKey["areaCode"];
                    vo.InAreaName = dicKey["areaName"];
                    vo.InBedId = drPat["inbedid"].ToString();
                    vo.InBedCode = dicKey["bedNo"];
                    vo.InDoctNo = dicKey["doctCode"];
                    vo.InDoctName = (this.lueZzys1.Properties.DBRow as EntityDict).Name;
                    vo.InDiagIcd10Code = dicKey["icdCode"];
                    vo.InDiagIcd10Name = (this.lueIcd1.Properties.DBRow as EntityDict).Name;

                    vo.BeginDate = this.txtKsrq1.Text;
                    vo.EndDate = this.txtJsrq1.Text;
                    vo.Name = this.txtXmcb1.Text;
                    vo.Sex = this.txtXb1.Text;
                    vo.Birthday = this.txtCsrq1.Text;
                    vo.Dwbm = this.txtDwbm1.Text;
                    vo.Dwmc = this.txtDwmc1.Text;
                    vo.Grjjzt = this.txtGrjjzt1.Text;
                    vo.Rylb = this.txtRylb1.Text;
                    vo.Cbdbm = this.txtCbdbm1.Text;
                    vo.Lxdh = this.txtLxdh1.Text;

                    if ((new weCare.Proxy.ProxyIP()).Service.SaveSGSRegister(vo) > 0)
                    {
                        this.Msg("入院资料保存成功！");
                        this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.txtRegisterno.Text = vo.RegisterNo;
                        this.drPat["registerno"] = vo.RegisterNo;
                        Init();
                    }
                    else
                    {
                        this.Msg("平台成功，医院系统失败。请联系信息科。");
                    }
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 修改入院登记
        /// <summary>
        /// 修改入院登记
        /// </summary>
        void UpdateInRegister()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }
            string areaCode = (this.lueBqmc1.Properties.DBRow as EntityDict).Id;
            if (string.IsNullOrEmpty(areaCode))
            {
                this.Msg("请选择病区。");
                return;
            }
            if (this.lueZzys1.Text.Trim() == "")
            {
                this.Msg("请选择主治医师。");
                return;
            }
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号 
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());
            // 病区编码
            dicKey.Add("areaCode", areaCode);
            // 病区名称
            dicKey.Add("areaName", (this.lueBqmc1.Properties.DBRow as EntityDict).Name);
            // 就诊科室
            dicKey.Add("deptCode", areaCode);
            // 就诊科室名称
            dicKey.Add("deptName", (this.lueBqmc1.Properties.DBRow as EntityDict).Name);
            // 住院号
            dicKey.Add("ipNo", this.txtZyh1.Text.Trim());
            // 入院床位号
            dicKey.Add("bedNo", this.txtCh1.Text.Trim());
            // 医师编号
            dicKey.Add("doctCode", (this.lueZzys1.Properties.DBRow as EntityDict).Id);

            string error = string.Empty;
            DataTable dtResult = null;
            if (GSSB.Message.Access("biz120104", GetRequest(dicKey), out dtResult, out error))
            {
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("inareacode", dicKey["areaCode"]);
                dicField.Add("inareaname", dicKey["areaName"]);
                dicField.Add("inbedcode", dicKey["bedNo"]);
                dicField.Add("indoctno", dicKey["doctCode"]);
                dicField.Add("indoctname", (this.lueZzys1.Properties.DBRow as EntityDict).Name);
                if ((new weCare.Proxy.ProxyIP()).Service.UpdateSGSRegister(this.drPat["registerid"].ToString(), dicField) > 0)
                {
                    this.Msg("修改入院资料成功！");
                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 取消入院登记
        /// <summary>
        /// 取消入院登记
        /// </summary>
        void CancelInRegister()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }

            DataTable dtResult = null;
            if (QueryRegInfo(0, out dtResult) == false)
            {
                return;
            }

            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());
            //dicKey.Add("begindate",)

            string error = string.Empty;
            if (GSSB.Message.Access("biz120109", GetRequest(dicKey), out dtResult, out error))
            {
                if ((new weCare.Proxy.ProxyIP()).Service.DeleteSGSRegister(this.drPat["registerid"].ToString()) > 0)
                {
                    //this.Msg("取消入院登记成功！");
                    MessageBox.Show("取消入院登记成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    Init();
                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 入院登记后取业务信息
        /// <summary>
        /// 结算标识 1已结算0未结算
        /// </summary>
        /// <param name="isCharge"></param>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        bool QueryRegInfo(int isCharge, out DataTable dtResult)
        {
            dtResult = null;
            string error = string.Empty;
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 入院登记后取业务信息
            dicKey.Add("computerno", this.drPat["computerno"].ToString());                                          // 社保号
            dicKey.Add("isCharge", this.drPat["isCharge"].ToString());                                                        // 结算标识 1已结算0未结算
            dicKey.Add("beginDate", this.drPat["beginDate"].ToString());             // 开始时间 格式：yyyyMMdd
            dicKey.Add("endDate", this.drPat["endDate"].ToString());                // 结束时间 格式：yyyyMMdd
            if (GSSB.Message.Access("bizh120102", GetRequest(dicKey), out dtResult, out error))
            {
                // 返回数据集
                return true;
            }
            else
            {
                this.Msg(error);
                return false;
            }
        }
        #endregion

        #region 查询收费项目
        /// <summary>
        /// 查询收费项目
        /// </summary>
        void QueryFeeItems()
        {
            if (this.dteFeeStart.Text.Trim() == "" || this.dteFeeEnd.Text.Trim() == "")
            {
                this.Msg("请选择费用时间段。");
                this.dteFeeStart.Focus();
                return;
            }
            string startDate = this.dteFeeStart.Text;
            string endDate = this.dteFeeEnd.Text;
            if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
            {
                this.Msg("费用开始日期不能大于结束日期。");
                this.dteFeeStart.Focus();
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                List<EntitySGSFeeItem> data = (new weCare.Proxy.ProxyIP()).Service.GetSGSFeeItems(this.RegisterId, startDate, endDate, null);
                decimal upSum = 0;
                decimal totalSum = 0;
                foreach (EntitySGSFeeItem item in data)
                {
                    if (item.upStatus == "上传")
                    {
                        upSum += item.total;
                    }
                    totalSum += item.total;
                }
                this.gcItems.DataSource = data;
                this.txtTotalMoney2.Text = totalSum.ToString("0.00");
                this.txtUpMoney2.Text = upSum.ToString("0.00");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region 收费项目上传
        /// <summary>
        /// 收费项目上传
        /// </summary>
        void ChargeItemsUpLoad()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }

            if (this.gvItems.RowCount == 0)
            {
                this.Msg("请先检索费用明细。");
                return;
            }
            List<EntitySGSFeeItem> data = this.gcItems.DataSource as List<EntitySGSFeeItem>;
            for (int i = data.Count - 1; i >= 0; i--)
            {
                if (data[i].upStatus.Trim() != "")
                    data.RemoveAt(i);
            }
            if (data.Count == 0)
            {
                this.Msg("费用明细都已经上传。");
                return;
            }

            // 由于上传限制每组不超过300，暂定每组传250条， 拆分
            int x = 0;
            List<string> lstPChargeId = new List<string>();
            Dictionary<int, List<EntitySGSFeeItem>> dicArr = new Dictionary<int, List<EntitySGSFeeItem>>();
            for (int i = 0; i < data.Count; i++)
            {
                x = i / 250;
                if (dicArr.ContainsKey(x))
                {
                    dicArr[x].Add(data[i]);
                }
                else
                {
                    dicArr.Add(x, new List<EntitySGSFeeItem>() { data[i] });
                }
                lstPChargeId.Add(data[i].pChargeId);
            }

            DataTable dtResult = null;
            if (QueryRegInfo(0, out dtResult) == false)
            {
                return;
            }

            string error = string.Empty;
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());
            bool isPass = true;
            foreach (KeyValuePair<int, List<EntitySGSFeeItem>> item in dicArr)
            {
                error = string.Empty;
                if (GSSB.Message.Access120002(GetRequest(dicKey), item.Value, out error) == false)
                {
                    this.Msg(error);
                    isPass = false;
                }
            }

            if (isPass)
            {
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("isuploaditems", "1");
                weCare.Proxy.ProxyIP proxy = new weCare.Proxy.ProxyIP();
                if (proxy.Service.UpdateSGSRegister(this.drPat["registerid"].ToString(), dicField) > 0)
                {
                    if (proxy.Service.SaveSGSUpLoadPChargeId(this.drPat["registerid"].ToString(), lstPChargeId) > 0)
                    {
                        this.Msg("费用上传成功！");
                        this.QueryFeeItems();

                        this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        this.Msg("平台成功，医院系统失败。请联系信息科。");
                    }
                }
            }
        }
        #endregion

        #region 取消收费项目上传
        /// <summary>
        /// 取消收费项目上传
        /// </summary>
        void CancelChargeItemsUpload()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());

            string error = string.Empty;
            DataTable dtResult = null;
            if (GSSB.Message.Access("biz120004", GetRequest(dicKey), out dtResult, out error))
            {
                if ((new weCare.Proxy.ProxyIP()).Service.DeleteSGSUpLoadPChargeId(this.drPat["registerid"].ToString()) > 0)
                {
                    this.Msg("删除住院业务费用明细成功！");
                    this.QueryFeeItems();

                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiComplete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 试算费用
        /// <summary>
        /// 试算费用
        /// </summary>
        void TrialFee()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());

            string error = string.Empty;
            DataTable dtResult = null;
            if (GSSB.Message.Access("biz120003", GetRequest(dicKey), out dtResult, out error))
            {
                this.SGSTotalSum = weCare.Core.Utils.Function.Dec(dtResult.Rows[0]["akc264"]);
                this.SGSAccSum = weCare.Core.Utils.Function.Dec(dtResult.Rows[0]["bka832"]);
                this.txtGsAcctSum2.Text = this.SGSAccSum.ToString("0.00");
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 保存出院登记
        /// <summary>
        /// 保存出院登记
        /// </summary>
        void SaveOutRegister()
        {
            if (this.dtmCyrq3.Text == "")
            {
                this.Msg("出院日期不能为空。");
                this.dtmCyrq3.Focus();
                return;
            }
            string bloodType = string.Empty;
            if (this.lueXx3.Text.Trim() != "" && this.lueXx3.Properties.DBRow != null)
            {
                bloodType = (this.lueXx3.Properties.DBRow as EntityDict).Id;
            }
            string inWay = string.Empty;
            if (this.lueRyfs3.Text.Trim() != "" && this.lueRyfs3.Properties.DBRow != null)
            {
                inWay = (this.lueRyfs3.Properties.DBRow as EntityDict).Id;
            }
            else
            {
                this.Msg("入院方式不能为空。");
                this.lueRyfs3.Focus();
                return;
            }
            string inStatus = string.Empty;
            if (this.lueRyqk3.Text.Trim() != "" && this.lueRyqk3.Properties.DBRow != null)
            {
                inStatus = (this.lueRyqk3.Properties.DBRow as EntityDict).Id;
            }
            else
            {
                this.Msg("入院情况不能为空。");
                this.lueRyqk3.Focus();
                return;
            }
            string zgqk = string.Empty;
            if (this.lueCyzg3.Text.Trim() != "" && this.lueCyzg3.Properties.DBRow != null)
            {
                zgqk = (this.lueCyzg3.Properties.DBRow as EntityDict).Id;
            }
            else
            {
                this.Msg("出院转归不能为空。");
                this.lueCyzg3.Focus();
                return;
            }
            //if (this.txtCyzd3.Text.Trim() == "")
            //{
            //    this.Msg("出院诊断不能为空。");
            //    this.txtCyzd3.Focus();
            //    return;
            //}
            if (this.lueIcd2.Text.Trim() == "")
            {
                this.Msg("请选择出院主要诊断(ICD)。");
                return;
            }

            DataTable dtResult = null;
            if (QueryRegInfo(0, out dtResult) == false)
            {
                return;
            }

            // 就医登记号
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());
            dicKey.Add("outDate", Convert.ToDateTime(this.dtmCyrq3.Text).ToString("yyyyMMdd"));                  // 出院日期 格式：yyyyMMdd
            dicKey.Add("operCode", this.LoginOperCode);                                                          // 登记人员工号
            dicKey.Add("operName", this.loginOperName);                                                          // 登记人姓名
            dicKey.Add("bloodType", bloodType);                                 // 血型
            dicKey.Add("inWay", inWay);                                         // 入院方式
            dicKey.Add("inStatus", inStatus);                                   // 入院情况
            dicKey.Add("zgqk", zgqk);                                           // 出院转归情况
            dicKey.Add("qjcs", weCare.Core.Utils.Function.Int(this.txtQjcs3.Text).ToString());                   // 抢救次数
            dicKey.Add("qjcgcs", weCare.Core.Utils.Function.Int(this.txtQjcgcs3.Text).ToString());               // 抢救成功次数
            dicKey.Add("outDiag", this.txtCyzd3.Text.Trim());                  // 出院诊断
            dicKey.Add("outDesc", this.txtCysm3.Text.Trim());                  // 出院说明
            dicKey.Add("outdiagicd10Code", (this.lueIcd2.Properties.DBRow as EntityDict).Id);                  // 出院icd
            dicKey.Add("outdiagicd10Name", (this.lueIcd2.Properties.DBRow as EntityDict).Name);                  // 出院icd
            string error = string.Empty;
            if (GSSB.Message.Access("biz120105", GetRequest(dicKey), out dtResult, out error))
            {
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("outbedid", this.drPat["outbedid"].ToString());
                dicField.Add("outbedcode", this.drPat["outbedcode"].ToString());
                dicField.Add("inway", dicKey["inWay"]);
                dicField.Add("insituation", dicKey["inStatus"]);
                dicField.Add("blood", dicKey["bloodType"]);
                dicField.Add("outcome", dicKey["zgqk"]);
                dicField.Add("outdate", dicKey["outDate"]);
                dicField.Add("rescuetime", dicKey["qjcs"]);
                dicField.Add("rescuetimesuce", dicKey["qjcgcs"]);
                dicField.Add("outdiag", dicKey["outDiag"]);
                dicField.Add("outdesc", dicKey["outDesc"]);
                dicField.Add("cydj", "1");
                dicField.Add("outdiagicd10Code", dicKey["outdiagicd10Code"]);
                dicField.Add("outdiagicd10Name", dicKey["outdiagicd10Name"]);
                if ((new weCare.Proxy.ProxyIP()).Service.UpdateSGSRegister(this.drPat["registerid"].ToString(), dicField) > 0)
                {
                    MessageBox.Show("出院登记成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 取消出院登记
        /// <summary>
        /// 取消出院登记
        /// </summary>
        void CancelOutRegister()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }

            DataTable dtResult = null;
            if (QueryRegInfo(0, out dtResult) == false)
            {
                return;
            }

            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());

            string error = string.Empty;
            if (GSSB.Message.Access("biz120108", GetRequest(dicKey), out dtResult, out error))
            {
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("outbedid", "");
                dicField.Add("outbedcode", "");
                dicField.Add("inway", "");
                dicField.Add("insituation", "");
                dicField.Add("blood", "");
                dicField.Add("outcome", "");
                dicField.Add("outdate", "");
                dicField.Add("rescuetime", "0");
                dicField.Add("rescuetimesuce", "0");
                dicField.Add("outdiag", "");
                dicField.Add("outdesc", "");
                dicField.Add("cydj", "");
                dicField.Add("outdiagicd10Code", "");
                dicField.Add("outdiagicd10Name", "");
                if ((new weCare.Proxy.ProxyIP()).Service.UpdateSGSRegister(this.drPat["registerid"].ToString(), dicField) > 0)
                {
                    //this.Msg("取消出院登记成功！");
                    MessageBox.Show("取消出院登记成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    //this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    Init();
                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 出院结算
        /// <summary>
        /// 出院结算
        /// </summary>
        void Charge()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }

            DataTable dtResult = null;
            if (QueryRegInfo(0, out dtResult) == false)
            {
                return;
            }

            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());
            dicKey.Add("operCode", this.LoginOperCode);         // 完成人工号
            dicKey.Add("operName", this.loginOperName);         // 完成人

            string error = string.Empty;
            if (GSSB.Message.Access("biz120106", GetRequest(dicKey), out dtResult, out error))
            {
                EntitySGSChargeZy vo = new EntitySGSChargeZy();
                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    vo.JYDJH = dtResult.Rows[0]["aaz218"].ToString();
                    vo.registerId = drPat["registerid"].ToString();
                    vo.YYBH = dtResult.Rows[0]["akb020"].ToString();
                    vo.YLZFY = Function.Dec(dtResult.Rows[0]["akc264"]);
                    vo.GRZF = Function.Dec(dtResult.Rows[0]["bka831"]);
                    vo.GSBXZF = Function.Dec(dtResult.Rows[0]["bka832"]);
                    vo.QZFFY = Function.Dec(dtResult.Rows[0]["bka825"]);
                    vo.BFZFFY = Function.Dec(dtResult.Rows[0]["bka826"]);
                    vo.QFXFY = Function.Dec(dtResult.Rows[0]["aka151"]);
                    vo.CGFDFYGRZF = Function.Dec(dtResult.Rows[0]["bka838"]);
                    vo.GRXJZF = Function.Dec(dtResult.Rows[0]["akb067"]);
                    vo.GRZHZF = Function.Dec(dtResult.Rows[0]["akb066"]);
                    vo.MZJZJZF = Function.Dec(dtResult.Rows[0]["bka821"]);
                    vo.QTZF = Function.Dec(dtResult.Rows[0]["bka839"]);
                    vo.GSBXTCJJZF = Function.Dec(dtResult.Rows[0]["ake039"]);
                    vo.GWYYLBZJJZF = Function.Dec(dtResult.Rows[0]["ake035"]);
                    vo.JYBCGSBXJJZF = Function.Dec(dtResult.Rows[0]["ake026"]);
                    vo.DEYLFYBZJJZF = Function.Dec(dtResult.Rows[0]["ake029"]);
                    vo.DWZF = Function.Dec(dtResult.Rows[0]["ake029"]);
                    vo.YYTF = Function.Dec(dtResult.Rows[0]["bka842"]);
                    vo.QTJJZF = Function.Dec(dtResult.Rows[0]["bka840"]);
                    vo.chargeStatus = "1";
                }

                if ((new weCare.Proxy.ProxyIP()).Service.SaveSGSChargeZy(vo) > 0)
                {
                    drPat["isCharge"] = 1;
                    this.SGSTotalSum = weCare.Core.Utils.Function.Dec(dtResult.Rows[0]["akc264"]);
                    this.SGSAccSum = weCare.Core.Utils.Function.Dec(dtResult.Rows[0]["bka832"]);
                    this.txtGsAcctSum2.Text = this.SGSAccSum.ToString("0.00");
                    MessageBox.Show("出院结算成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.blbiInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiModifyInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelInReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiComplete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #region 取消出院结算
        /// <summary>
        /// 取消出院结算
        /// </summary>
        void CancelCharge()
        {
            if (this.drPat["registerno"] == DBNull.Value || this.drPat["registerno"].ToString().Trim() == "")
            {
                this.Msg("就医登记号不能为空。");
                return;
            }

            DataTable dtResult = null;
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            if (QueryRegInfo(1, out dtResult) == false)
            {
                return;
            }


            // 就医登记号
            dicKey.Add("jydjh", this.drPat["registerno"].ToString());

            string error = string.Empty;
            if (GSSB.Message.Access("bizh120107", GetRequest(dicKey), out dtResult, out error))
            {
                Dictionary<string, string> dicField = new Dictionary<string, string>();
                dicField.Add("ischarge", "0");
                //if ((new weCare.Proxy.ProxyIP()).Service.UpdateSGSRegister(this.drPat["registerid"].ToString(), dicField) > 0)
                if ((new weCare.Proxy.ProxyIP()).Service.DeleteSGSChargeZy(this.drPat["registerid"].ToString()) > 0)
                {
                    this.txtGsAcctSum2.Text = "";
                    this.Msg("取消出院结算成功！");

                    this.blbiItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelItemUpload.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiTrial.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelOutReg.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.blbiCancelCharge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    this.Msg("平台成功，医院系统失败。请联系信息科。");
                }
            }
            else
            {
                this.Msg(error);
            }
        }
        #endregion

        #endregion

        #region event

        private void frmSGS_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.InitLue();
                this.Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void frmSGS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void blbiReadCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReadCard();
        }

        private void blbiInReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveInRegister();
        }

        private void blbiModifyInReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInRegister();
        }

        private void blbiCancelInReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CancelInRegister();
        }

        private void blbiItemUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChargeItemsUpLoad();
        }

        private void blbiCancelItemUpload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CancelChargeItemsUpload();
        }

        private void blbiTrial_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TrialFee();
        }

        private void blbiOutReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveOutRegister();
        }

        private void blbiCancelOutReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CancelOutRegister();
        }

        private void blbiCharge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Charge();
        }

        private void blbiCancelCharge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CancelCharge();
        }

        private void blbiComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnQueryFee_Click(object sender, EventArgs e)
        {
            QueryFeeItems();
        }

        #endregion

    }
}
