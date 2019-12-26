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
    public partial class frmSchedulingEdit2 : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmSchedulingEdit2(int _EditType, string _DeptCode, string _RoomCode, string _DoctCode, string _RegDate)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                ((ctlSchedulingEdit2)Controller).EditType = _EditType;
                ((ctlSchedulingEdit2)Controller).DeptCode = _DeptCode;
                ((ctlSchedulingEdit2)Controller).RoomCode = _RoomCode;
                ((ctlSchedulingEdit2)Controller).DoctCode = _DoctCode;
                ((ctlSchedulingEdit2)Controller).RegDate = _RegDate;
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
            Controller = new ctlSchedulingEdit2();
            Controller.SetUI(this);
        }
        #endregion

        #region 事件

        private void frmSchedulingEdit2_Load(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).Init();
            this.timer.Enabled = true;
        }

        private void frmSchedulingEdit2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.blbiSave.Enabled)
            {
                this.gvDate.CloseEditor();
                this.gvNo.CloseEditor();
                if (this.ValueChanged)
                {
                    DialogResult res = DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question, true);
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        ((ctlSchedulingEdit2)Controller).Save(true);
                    }
                    else if (res == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.isCancelExit = true;
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void blbiClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).Clear();
        }

        private void blbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).New();
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).Save(false);
        }

        private void blbiDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).Delete();
        }

        private void blbiAddNo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).AddNo();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DayCopy();
        }

        private void blbiDoctServcie_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DoctService();
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnDelDateAll_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DelRegDateRowAll();
        }

        private void btnAddDate_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).AddRegDateRow();
        }

        private void btnDelDate_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DelRegDateRow();
        }

        private void btnAddNum_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).AddNumberRow();
        }

        private void btnDelNum_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DelNumberRow();
        }

        private void btnDelNumAll_Click(object sender, EventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).DelNumberRowAll();
        }

        private void tvDate_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).CustomDrawNodeCell(e);
        }

        private void gvDate_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).SetCellStyleDate(e);
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlSchedulingEdit2)Controller).SetCellStyleNumber(e);
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            this.lueRoom.Text = string.Empty;
            this.lueDoct.Text = string.Empty;
            this.txtRank.Text = string.Empty;
            ((ctlSchedulingEdit2)Controller).LueFilterRoom();
            ((ctlSchedulingEdit2)Controller).LueFilterDoct();
        }

        private void lueDoct_HandleDBValueChanged(object sender)
        {
            ((ctlSchedulingEdit2)Controller).LueFilterDoctInfo();
            ((ctlSchedulingEdit2)Controller).LueFilterRank();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.lueDept.Focus();
        }

        #endregion
        
    }
}
