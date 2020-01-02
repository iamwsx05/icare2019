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
    /// 周排班
    /// </summary>
    public partial class frmSchedulingWeek : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmSchedulingWeek()
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
            Controller = new ctlSchedulingWeek();
            Controller.SetUI(this);
        }
        #endregion

        #region PauseRedraw/StartRedraw
        /// <summary>
        /// PauseRedraw
        /// </summary>
        public override void PauseRedraw()
        {
            ((ctlSchedulingWeek)Controller).PauseRedraw();
        }
        /// <summary>
        /// StartRedraw
        /// </summary>
        public override void StartRedraw()
        {
            ((ctlSchedulingWeek)Controller).StartRedraw();
        }
        #endregion

        #region Func

        public override void New()
        {
            ((ctlSchedulingWeek)Controller).New();
        }

        public override void Edit()
        {
            ((ctlSchedulingWeek)Controller).Edit();
        }

        public override void Delete()
        {
            ((ctlSchedulingWeek)Controller).Delete();
        }

        public override void Export()
        {
            uiHelper.ExportToXls(this.gvPlan);
        }

        public override void Preview()
        {
            ((ctlSchedulingWeek)Controller).Print();
        }

        public override void RefreshData()
        {
            ((ctlSchedulingWeek)Controller).RefreshData(true);
        }

        public override void Copy()
        {
            ((ctlSchedulingWeek)Controller).Copy();
        }

        #endregion

        #endregion

        #region 事件

        private void frmSchedulingWeek_Load(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).Init();
        }

        private void gvPlan_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlSchedulingWeek)Controller).RowCellStyle(e);
        }

        private void gvPlan_DoubleClick(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).Edit();
        }

        #region btnW
        private void btnW1_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周一");
        }

        private void btnW2_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周二");
        }

        private void btnW3_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周三");
        }

        private void btnW4_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周四");
        }

        private void btnW5_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周五");
        }

        private void btnW6_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周六");
        }

        private void btnW7_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay("周日");
        }

        private void btnW8_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingWeek)Controller).FilterDay(string.Empty);
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingWeek)Controller).FilterDept(this.lueDept.Text);
        }

        private void lueDeptReg_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingWeek)Controller).FilterDoct(this.lueDoct.Text);
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            //((ctlSchedulingWeek)Controller).ImportRegPlan();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //frmRegisterBEdit frm = new frmRegisterBEdit(new EntityBEditParm());
            //frm.ShowDialog();
            if (GlobalLogin.objLogin.EmpNo == "00")
            {
                frmTest frm = new frmTest();
                frm.ShowDialog();
            }
        }

        #endregion

    }
}
