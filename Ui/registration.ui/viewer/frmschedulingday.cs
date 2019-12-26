using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Registration.Ui
{
    /// <summary>
    /// 日排班
    /// </summary>
    public partial class frmSchedulingDay : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmSchedulingDay()
        {
            InitializeComponent();
        }
        #endregion

        #region Override

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlSchedulingDay();
            Controller.SetUI(this);
        }
        #endregion

        #region PauseRedraw/StartRedraw
        /// <summary>
        /// PauseRedraw
        /// </summary>
        public override void PauseRedraw()
        {
            ((ctlSchedulingDay)Controller).PauseRedraw();
        }
        /// <summary>
        /// StartRedraw
        /// </summary>
        public override void StartRedraw()
        {
            ((ctlSchedulingDay)Controller).StartRedraw();
        }
        #endregion

        #region Func

        public override void LoadData()
        {
            ((ctlSchedulingDay)Controller).Import();
        }            

        public override void New()
        {
            ((ctlSchedulingDay)Controller).New();
        }

        public override void Edit()
        {
            ((ctlSchedulingDay)Controller).Edit();
        }

        public override void Confirm()
        {
            pmConfirm.ShowPopup(Control.MousePosition);
        }

        public override void Cancel()
        {
            pmUnConfirm.ShowPopup(Control.MousePosition);
        }

        public override void RefreshData()
        {
            ((ctlSchedulingDay)Controller).RefreshData();
        }

        public override void Export()
        {
            uiHelper.ExportToXls(this.gvPlan);
        }
        
        public override void Preview()
        {
            gvPlan.PrintDialog();
        }

        public override void Put()
        {
            ((ctlSchedulingDay)Controller).GdaSynRegSchedule();
        }

        #endregion

        #endregion

        #region 事件

        private void frmSchedulingDay_Load(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).Init();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingDay)Controller).Load();
        }

        private void lueDept_HandleResetDBValue(object sender)
        {
            ((ctlSchedulingDay)Controller).Load();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.lueDept.Properties.DBValue = string.Empty;
            this.lueDept.Text = string.Empty;
            ((ctlSchedulingDay)Controller).Load();
        }

        private void dteRegDate_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).Load();
        }

        private void dteRegDateEnd_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).Load();
        }

        private void gvPlan_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlSchedulingDay)Controller).RowCellStyle(e);
        }

        private void gvPlan_DoubleClick(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).Edit();
        }

        private void blbiConfirm1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingDay)Controller).Confirm(1);
        }

        private void blbiConfirm2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingDay)Controller).Confirm(2);
        }

        private void blbiUnConfirm1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingDay)Controller).UnConfirm(1);
        }

        private void blbiUnConfirm2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingDay)Controller).UnConfirm(2);
        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingDay)Controller).FilterDoct();
        }

        private void lueDoct_HandleResetDBValue(object sender)
        {
            ((ctlSchedulingDay)Controller).FilterDoct();
        }

        private void lueDoct_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lueDoct.Text.Trim() == string.Empty)
                ((ctlSchedulingDay)Controller).FilterDoct();
        }

        private void cboRegType_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).FilterDoct();
        }

        private void cboShift_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlSchedulingDay)Controller).FilterDoct();
        }

        private void dteRegDateStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((ctlSchedulingDay)Controller).Load();
            }
        }

        private void dteRegDateEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((ctlSchedulingDay)Controller).Load();
            }
        }
        
        #endregion

              
    }
}
