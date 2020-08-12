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

namespace Common.Controls
{
    /// <summary>
    /// 表单管理UI
    /// </summary>
    public partial class frmFormList : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmFormList()
        {
            InitializeComponent(); 
        }
        #endregion

        #region override

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlFormList();
            Controller.SetUI(this);
        }
        #endregion

        public override void New()
        {
            ((ctlFormList)Controller).New();
        }

        public override void Save()
        {
            ((ctlFormList)Controller).Save(false);
        }

        public override void Delete()
        {
            ((ctlFormList)Controller).Delete();
        }

        public override void Design()
        {
            ((ctlFormList)Controller).Design();
        }

        public override void RefreshData()
        {
            ((ctlFormList)Controller).LoadDataSource();
        }

        #region 外部接口

        /// <summary>
        /// 是否调用模式
        /// </summary>
        public bool IsLoadModel { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public string FormType { get; set; }
        /// <summary>
        /// 外部接口
        /// </summary>
        /// <param name="_regType"></param>
        public void Show2(string _formType)
        {
            FormType = _formType;
            this.Show();
        }
        
        #endregion

        #endregion

        #region 事件

        private void frmCaseTemplate_Load(object sender, EventArgs e)
        {
            ((ctlFormList)Controller).Init();
        }

        private void frmCaseTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ValueChanged)
            {
                DialogResult res = DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question, true);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ((ctlFormList)Controller).Save(true);
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.isCancelExit = true;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void rdoStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoStatus.SelectedIndex == 1)
                rdoStatus.ForeColor = Color.Green;
            else
                rdoStatus.ForeColor = Color.Red;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.txtFormCode.Focus();
        }
        
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
                ((ctlFormList)Controller).FindIndex = 0;
                ((ctlFormList)Controller).FindForm(this.txtFind.Text.Trim(), false);
            }
        }

        #endregion

    }
}
