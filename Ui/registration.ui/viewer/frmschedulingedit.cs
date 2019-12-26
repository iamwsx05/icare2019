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
    /// 排班计划编辑
    /// </summary>
    public partial class frmSchedulingEdit : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="_EditType"></param>
        /// <param name="_RegId"></param>
        public frmSchedulingEdit(int _EditType, int _RegId)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                ((ctlSchedulingEdit)Controller).EditType = _EditType;
                ((ctlSchedulingEdit)Controller).RegId = _RegId;
            }
        }
        #endregion

        #region 变量

        public bool IsNew { get; set; }

        /// <summary>
        /// 是否保存过
        /// </summary>
        public bool IsSave { get; set; }
        #endregion

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlSchedulingEdit();
            Controller.SetUI(this);
        }
        #endregion

        #region 事件

        private void frmSchedulingEdit_Load(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).Init();
        }

        private void frmSchedulingEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.gvNo.CloseEditor();
            if (this.ValueChanged)
            {
                DialogResult res = DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question, true);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ((ctlSchedulingEdit)Controller).Save(true);
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.isCancelExit = true;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void blbiClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).Clear();
        }

        private void blbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).New();
        }

        private void blbiContiue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).Continue();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).Save(false);
        }

        private void blbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).Delete();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void lueRegType_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingEdit)Controller).LueFilterRegType();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            this.lueRoom.Text = string.Empty;
            this.lueDoct.Text = string.Empty;
            this.lueDeptReg.Text = string.Empty;
            this.txtRank.Text = string.Empty;
            ((ctlSchedulingEdit)Controller).LueFilterRoom();
            ((ctlSchedulingEdit)Controller).LueFilterDoct();
            ((ctlSchedulingEdit)Controller).LueFilterExpert();           
        }

        private void lueDeptReg_HandleDBValueChanged(object sender)
        {

        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingEdit)Controller).LueFilterDoctInfo();
            ((ctlSchedulingEdit)Controller).LueFilterRank();
        }
        
        #region clearTIme

        private void btnGam_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(1);
        }

        private void btnGpm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(2);
        }

        private void btnGnm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(3);
        }

        private void btnBam_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(4);
        }

        private void btnBpm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(5);
        }

        private void btnBnm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(6);
        }

        private void btnVam_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(7);
        }

        private void btnVpm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(8);
        }

        private void btnVnm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(9);
        }

        private void btnVmm_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).ClearTime(10);
        }
        #endregion
        
        private void btnAddNum_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).AddNumberRow();
        }

        private void btnDelNum_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit)Controller).DelNumberRow();
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).SetCellStyle(e);
        }

        private void dteRegDate_EditValueChanged(object sender, EventArgs e)
        {
            // 改为用树
            //((ctlSchedulingEdit)Controller).ReLoadRegDay();
        }

        private void tvDate_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            ((ctlSchedulingEdit)Controller).CustomDrawNodeCell(e);
        }

        #endregion


    }
}
