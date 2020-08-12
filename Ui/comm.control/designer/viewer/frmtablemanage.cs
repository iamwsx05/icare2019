using Common.Controls;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    /// <summary>
    /// 表格管理
    /// </summary>
    public partial class frmTableManage : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmTableManage()
        {
            InitializeComponent();
        }
        #endregion

        #region Override
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlTableManage();
            Controller.SetUI(this);
        }

        public override void New()
        {
            ((ctlTableManage)Controller).NewTable();
        }

        public override void Delete()
        {
            ((ctlTableManage)Controller).DelTable();
        }

        public override void Save()
        {
            ((ctlTableManage)Controller).Save(false);
        }

        public override void RefreshData()
        {
            ((ctlTableManage)Controller).Refresh();
        }

        public override void Preview()
        {
            ((ctlTableManage)Controller).Print();
        }

        public override void Export()
        {
            ((ctlTableManage)Controller).Export();
        }

        #endregion

        #region 事件

        #region 查找框

        private void txtFind_Enter(object sender, EventArgs e)
        {
            this.lblFind.Visible = false;
        }

        private void txtFind_Leave(object sender, EventArgs e)
        {
            if (this.txtFind.Text.Trim() == string.Empty)
                this.lblFind.Visible = true;
            else
                this.lblFind.Visible = false;
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((ctlTableManage)Controller).findIndex = 0;
                ((ctlTableManage)Controller).FindTable(this.txtFind.Text.Trim(), true);
            }
        }
        #endregion

        #region Check

        private void chkTableStyle1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTableStyle1.Checked)
            {
                ((ctlTableManage)Controller).SetCheck(1);
            }
        }

        private void chkTableStyle2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTableStyle2.Checked)
            {
                ((ctlTableManage)Controller).SetCheck(2);
            }
        }

        private void chkHeadStyle1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkHeadStyle1.Checked)
            {
                ((ctlTableManage)Controller).SetCheck(3);
            }
        }

        private void chkHeadStyle2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkHeadStyle2.Checked)
            {
                ((ctlTableManage)Controller).SetCheck(4);
            }
        }
        #endregion

        private void frmTableManage_Load(object sender, EventArgs e)
        {
            ((ctlTableManage)Controller).Init();
        }

        private void frmTableManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.gvTable.CloseEditor();
            if (this.ValueChanged)
            {
                DialogResult res = DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question, true);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ((ctlTableManage)Controller).Save(true);
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.isCancelExit = true;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void btnAddTableRow_Click(object sender, EventArgs e)
        {
            ((ctlTableManage)Controller).NewTableRow();
        }

        private void btnDelTableRow_Click(object sender, EventArgs e)
        {
            ((ctlTableManage)Controller).DelTableRow();
        }

        private void rbtnConfig_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ((ctlTableManage)Controller).SetConfig(sender);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            ((ctlTableManage)Controller).ShowHelp();
        }

        #endregion

    }
}
