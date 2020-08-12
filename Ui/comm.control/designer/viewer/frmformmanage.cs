using Common.Controls;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    /// <summary>
    /// 表单设计管理
    /// </summary>
    public partial class frmFormManage : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmFormManage()
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
            Controller = new ctlFormManage();
            Controller.SetUI(this);
        }

        public override void New()
        {
            ((ctlFormManage)Controller).New();
        }

        public override void Delete()
        {
            ((ctlFormManage)Controller).Delete();
        }

        public override void Save()
        {
            ((ctlFormManage)Controller).Save();
        }

        public override void Design()
        {
            ((ctlFormManage)Controller).Design();
        }

        public override void RefreshData()
        {
            ((ctlFormManage)Controller).RefreshData();
        }

        #endregion

        #region 事件

        private void frmFormManage_Load(object sender, EventArgs e)
        {
            ((ctlFormManage)Controller).Init();
        }

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
                ((ctlFormManage)Controller).FindIndex = 0;
                ((ctlFormManage)Controller).FindForm(this.txtFind.Text.Trim());
            }
        }

        #endregion

        #region checkbox

        void SetCheckStat(DevExpress.XtraEditors.CheckEdit chk1, DevExpress.XtraEditors.CheckEdit chk2)
        {
            if (chk1.Checked)
            {
                chk2.Checked = false;
                chk2.ForeColor = System.Drawing.Color.FromArgb(30, 57, 91);
                chk1.ForeColor = System.Drawing.Color.Crimson;
            }
            else
            {
                chk1.ForeColor = System.Drawing.Color.FromArgb(30, 57, 91);
            }
        }

        private void chkShowFormStatus1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkShowFormStatus1, chkShowFormStatus2);
        }

        private void chkShowFormStatus2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkShowFormStatus2, chkShowFormStatus1);
        }

        private void chkExpert1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkExpert1, chkExpert2);
        }

        private void chkExpert2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkExpert2, chkExpert1);
        }

        private void chkMultPage1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkMultPage1, chkMultPage2);
        }

        private void chkMultPage2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkMultPage2, chkMultPage1);
        }

        private void chkRef1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkRef1, chkRef2);
        }

        private void chkRef2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkRef2, chkRef1);
        }

        private void chkBanding1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkBanding1, chkBanding2);
        }

        private void chkBanding2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkBanding2, chkBanding1);
        }

        private void chkHeadOfDeptCheck1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkHeadOfDeptCheck1, chkHeadOfDeptCheck2);
        }

        private void chkHeadOfDeptCheck2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkHeadOfDeptCheck2, chkHeadOfDeptCheck1);
        }

        private void chkSuperDoctCheck1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkSuperDoctCheck1, chkSuperDoctCheck2);
        }

        private void chkSuperDoctCheck2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkSuperDoctCheck2, chkSuperDoctCheck1);
        }

        private void chkMissPageCheck1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkMissPageCheck1, chkMissPageCheck2);
        }

        private void chkMissPageCheck2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStat(chkMissPageCheck2, chkMissPageCheck1);
        }

        #endregion

        private void btnAddDept_Click(object sender, EventArgs e)
        {
            ((ctlFormManage)Controller).NewFormDept();
        }

        private void btnDelDept_Click(object sender, EventArgs e)
        {
            ((ctlFormManage)Controller).DelFormDept();
        }

        #endregion

    }
}
