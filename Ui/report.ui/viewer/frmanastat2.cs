﻿using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Report.Ui
{
    /// <summary>
    /// 麻醉专业医疗质量控制指标
    /// </summary>
    public partial class frmAnaStat2 : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmAnaStat2()
        {
            InitializeComponent();
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// 报表XR
        /// </summary>
        XtraReport xr = null;

        #endregion

        #region override

        public override void Statistics()
        {
            this.Stat();
        }

        #region Export
        /// <summary>
        /// Export
        /// </summary>
        public override void Export()
        {
            if (xr != null && xr.DataSource != null)
            {
                xr.Name = this.Text;
                uiHelper.Export(xr);
            }
        }
        #endregion

        public override void Preview()
        {
            if (xr != null && xr.DataSource != null)
            {
                xr.PrintDialog();
            }
        }
        #endregion

        #region 方法

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            #region xr
            decimal printId = 5;
            EntitySysReport rptVo = null;
            using (ProxyCommon proxy = new ProxyCommon())
            {
                rptVo = proxy.Service.GetReport(printId);
            }
            xr = new XtraReport();
            if (rptVo != null)
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(rptVo.rptFile, 0, rptVo.rptFile.Length);
                xr.LoadLayout(ms);
            }
            this.ucPrintControl.PrintingSystem = xr.PrintingSystem;
            xr.CreateDocument();
            #endregion
        }
        #endregion

        #region Stat

        public string StartDate
        {
            get
            {
                if (this.dteStartDate.Text.Trim() == string.Empty) return string.Empty;
                if (this.cboStartMonth.Text.Trim() == string.Empty) return string.Empty;
                return this.dteStartDate.Text + "-" + this.cboStartMonth.Text + "-01";
            }
        }

        public string EndDate
        {
            get
            {
                if (this.dteEndDate.Text.Trim() == string.Empty) return string.Empty;
                if (this.cboEndMonth.Text.Trim() == string.Empty) return string.Empty;
                DateTime dtm = Convert.ToDateTime(this.dteEndDate.Text + "-" + this.cboEndMonth.Text + "-01");
                return ((dtm.AddDays(1 - dtm.Day)).AddMonths(1).AddDays(-1)).ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// Stat
        /// </summary>
        void Stat()
        {
            if (Function.Datetime(this.StartDate) > Function.Datetime(this.EndDate))
            {
                DialogBox.Msg("开始时间不能大于结束时间。");
                this.dteStartDate.Focus();
                return;
            }

            try
            {
                uiHelper.BeginLoading(this);
                using (ProxyAnaReport proxy = new ProxyAnaReport())
                {
                    xr.DataSource = proxy.Service.GetAnaStat2(this.StartDate, this.EndDate);
                    XRControl xc; //报表上的组件
                    xc = xr.FindControl("lblDate", true);
                    if (xc != null) (xc as XRLabel).Text = " " + this.StartDate + " ~ " + this.EndDate;
                    xr.CreateDocument();
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion
        
        #endregion

        #region 事件
        
        private void frmAnaStat2_Load(object sender, EventArgs e)
        {
            Init();
        }

        #endregion
    }
}
