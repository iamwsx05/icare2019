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
    /// 预约挂号
    /// </summary>
    public partial class frmRegisterB : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterB()
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
            Controller = new ctlRegisterB();
            Controller.SetUI(this);
        }
        #endregion

        public override void New()
        {
            ((ctlRegisterB)Controller).BookingReg(1);
        }

        public override void Cancel()
        {
            ((ctlRegisterB)Controller).CancelReg();
        }

        public override void Search()
        {
            ((ctlRegisterB)Controller).Query();
        }

        public override void RefreshData()
        {
            ((ctlRegisterB)Controller).RefreshData();
        }

        /// <summary>
        /// 预约类型
        /// </summary>
        internal string _regType = "4";
        /// <summary>
        /// 预约类型
        /// </summary>
        internal string regType
        {
            get { return _regType; }
            set { _regType = value; }
        }
        /// <summary>
        /// 外部接口
        /// </summary>
        /// <param name="_regType"></param>
        public void Show2(string p_regType)
        {
            regType = p_regType;
            this.Show();
            if (regType == "3")
            {
                this.Text += "(电话预约)";
            }
            else if (regType == "4")
            {
                this.Text += "(现场预约)";
            }
        }

        #endregion

        #region 事件

        private void frmRegisterB_Load(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).Init();
        }

        private void frmRegisterB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ((ctlRegisterB)Controller).BookingReg(1);
            }
            else if (e.KeyCode == Keys.F6)
            {
                ((ctlRegisterB)Controller).CancelReg();
            }
            else if (e.KeyCode == Keys.F7)
            {
                ((ctlRegisterB)Controller).Query();
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).BookingReg(2);
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void lueDept_HandleResetDBValue(object sender)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void lueDept_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lueDept.Text == string.Empty) ((ctlRegisterB)Controller).LoadData();
        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void lueDoct_HandleResetDBValue(object sender)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void lueDoct_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lueDoct.Text == string.Empty) ((ctlRegisterB)Controller).LoadData();
        }

        private void dteStart_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void dteEnd_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).LoadData();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).FilterRegCode(1);
        }

        private void btnExpert_Click(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).FilterRegCode(2);
        }

        private void btnDept_Click(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).FilterRegCode(3);
        }

        private void btnSpec_Click(object sender, EventArgs e)
        {
            ((ctlRegisterB)Controller).FilterRegCode(4);
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                uiHelper.BeginLoading(this);
                if (this.cboSort.SelectedIndex == 1)
                    this.regDate.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                else
                    this.regDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }

        #endregion

    }
}
