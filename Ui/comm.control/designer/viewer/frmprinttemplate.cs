using Common.Controls;
using Common.Controls.Emr;
using Common.Entity;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    /// <summary>
    /// 打印模板管理
    /// </summary>
    public partial class frmPrintTemplate : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmPrintTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region override
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlPrintTemplate();
            Controller.SetUI(this);
        }

        public override void New()
        {
            ((ctlPrintTemplate)Controller).New();
        }

        public override void Delete()
        {
            ((ctlPrintTemplate)Controller).Delete();
        }

        public override void Save()
        {
            ((ctlPrintTemplate)Controller).Save(false);
        }

        public override void RefreshData()
        {
            ((ctlPrintTemplate)Controller).LoadDataSource(false);
        }

        public override void Design()
        {
            ((ctlPrintTemplate)Controller).Design();
        }

        public override void Preview()
        {
            ((ctlPrintTemplate)Controller).Print();
        }

        #endregion

        #region 事件

        private void frmPrintTemplate_Load(object sender, EventArgs e)
        {
            ((ctlPrintTemplate)Controller).Init();
        }

        private void frmPrintTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ValueChanged)
            {
                DialogResult res = DialogBox.Msg("是否保存数据？", MessageBoxIcon.Question, true);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    ((ctlPrintTemplate)Controller).Save(true);
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.isCancelExit = true;
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void chkTemplateStyle1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.chkTemplateStyle1.Checked)
            {
                this.chkTemplateStyle2.Checked = false;
                this.chkTemplateStyle3.Checked = false;
            }
        }

        private void chkTemplateStyle2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTemplateStyle2.Checked)
            {
                this.chkTemplateStyle1.Checked = false;
                this.chkTemplateStyle3.Checked = false;
            }
        }

        private void chkTemplateStyle3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTemplateStyle3.Checked)
            {
                this.chkTemplateStyle1.Checked = false;
                this.chkTemplateStyle2.Checked = false;
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ((ctlPrintTemplate)Controller).LoadFields();
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
                ((ctlPrintTemplate)Controller).findIndex = 0;
                ((ctlPrintTemplate)Controller).FindTemplate(this.txtFind.Text.Trim(), false);
            }
        }
        
        #endregion
     
    }
}
