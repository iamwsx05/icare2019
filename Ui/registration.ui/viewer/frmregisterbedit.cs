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
    public partial class frmRegisterBEdit : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterBEdit(EntityBEditParm _editParm)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                ((ctlRegisterBEdit)Controller).EditParm = _editParm;
                if (_editParm.regType == 3)
                {
                    this.Text += "(电话)";
                    this.colNumName.Caption = "电话剩余号";
                }
                else if (_editParm.regType == 4)
                {
                    this.Text += "(现场)";
                    this.colNumName.Caption = "现场剩余号";
                }
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
            Controller = new ctlRegisterBEdit();
            Controller.SetUI(this);
        }
        #endregion

        #region 属性

        /// <summary>
        /// 是否预约成功
        /// </summary>
        public bool IsBooking { get; set; }

        #endregion

        #region 窗体事件

        private void frmRegisterBEdit_Load(object sender, EventArgs e)
        {
            ((ctlRegisterBEdit)Controller).Init();
        }

        private void frmRegisterBEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.gvNo.CloseEditor();
            if (this.ValueChanged)
            {
                DialogResult res = DialogBox.Msg("是否进行预约挂号？", MessageBoxIcon.Question, true);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ((ctlRegisterBEdit)Controller).RegBooking(true);
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.isCancelExit = true;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.txtCardNo.Focus();
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
                ((ctlRegisterBEdit)Controller).GetPatInfo();
            }
        }

        private void lueRegType_HandleDBValueChanged(object sender)
        {
            ((ctlRegisterBEdit)Controller).LueFilterRegType();
            ((ctlRegisterBEdit)Controller).GetRegDayNumber();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            this.lueDoct.Text = string.Empty;
            this.lueDeptReg.Text = string.Empty;
            ((ctlRegisterBEdit)Controller).LueFilterDoct();
            ((ctlRegisterBEdit)Controller).LueFilterExpert();
            ((ctlRegisterBEdit)Controller).GetRegDayNumber();
        }

        private void dteRegDate_EditValueChanged(object sender, EventArgs e)
        {
            ((ctlRegisterBEdit)Controller).GetRegDayNumber();
        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            ((ctlRegisterBEdit)Controller).GetRegDayNumber();
        }

        private void lueDeptReg_HandleDBValueChanged(object sender)
        {
            ((ctlRegisterBEdit)Controller).GetRegDayNumber();
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.gvNo.CloseEditor();
            ((ctlRegisterBEdit)Controller).SetCheck(this.gvNo.FocusedRowHandle);
        }

        private void btnModifyTelNo_Click(object sender, EventArgs e)
        {
            ((ctlRegisterBEdit)Controller).ModifyTelNo();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ((ctlRegisterBEdit)Controller).RegBooking(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
