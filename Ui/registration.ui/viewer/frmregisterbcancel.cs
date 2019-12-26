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
    /// 取消预约
    /// </summary>
    public partial class frmRegisterCancel : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterCancel(EntityBEditParm _EditParm)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                ((ctlRegisterBCancel)Controller).EditParm = _EditParm;
                if (_EditParm.regType == 3)
                    this.Text += "(电话)";
                else if (_EditParm.regType == 4)
                    this.Text += "(现场)";
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
            Controller = new ctlRegisterBCancel();
            Controller.SetUI(this);
        }
        #endregion

        #region 属性

        /// <summary>
        /// 已取消预约
        /// </summary>
        public bool IsCancel { get; set; }

        #endregion

        #region 窗体事件

        private void btnOk_Click(object sender, EventArgs e)
        {
            ((ctlRegisterBCancel)Controller).CancelRegBooking();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (GlobalHospital.Current == EnumHospitalCode.东莞茶山)
                {
                    string cardNo = this.txtCardNo.Text.Trim();
                    if (cardNo != null)
                    {
                        this.txtCardNo.Text = cardNo.PadLeft(10, '0');
                    }
                }
                else
                {
                    // 社保卡号自动截断 6214620634003276232=23032202130000010
                    if (this.txtCardNo.Text.Length > 19 && this.txtCardNo.Text.IndexOf("=") > 0)
                    {
                        this.txtCardNo.Text = this.txtCardNo.Text.Substring(0, this.txtCardNo.Text.IndexOf("="));
                    }
                }
                ((ctlRegisterBCancel)Controller).GetPatInfo();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.txtCardNo.Focus();
        }

        private void gvNo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ((ctlRegisterBCancel)Controller).SetCellDisable(e.FocusedRowHandle);
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.gvNo.CloseEditor();
            ((ctlRegisterBCancel)Controller).SetCheck(this.gvNo.FocusedRowHandle);
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlRegisterBCancel)Controller).SetRowCellStyle(e);
        }

        #endregion

    }
}
