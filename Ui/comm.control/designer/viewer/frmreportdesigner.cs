using Common.Controls;
using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 报表设计器
    /// </summary>
    public partial class frmReportDesigner : frmBaseMdi
    {
        #region 构造

        //public frmReportDesigner()
        //{
        //    InitializeComponent();
        //}

        public frmReportDesigner(EntitySysReport _reportVo)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                ((ctlReportDesigner)Controller).rptTypeId = 1;
                ((ctlReportDesigner)Controller).reportVo = _reportVo;
            }
        }

        public frmReportDesigner(EntityEmrPrintTemplate _reportVo)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                ((ctlReportDesigner)Controller).rptTypeId = 2;
                ((ctlReportDesigner)Controller).templateVo = _reportVo;
            }
        }
        #endregion

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlReportDesigner();
            Controller.SetUI(this);
        }
        #endregion

        #region 事件

        private void frmReportDesigner_Load(object sender, EventArgs e)
        {
            ((ctlReportDesigner)Controller).Init();
        }

        private void frmReportDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((ctlReportDesigner)Controller).FormClosing();
        }

        private void btnImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlReportDesigner)Controller).Import();
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlReportDesigner)Controller).Export();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlReportDesigner)Controller).New();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlReportDesigner)Controller).Save();
        }

        #endregion

    }
}
