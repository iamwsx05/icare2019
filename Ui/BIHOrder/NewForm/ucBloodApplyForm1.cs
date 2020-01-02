using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.BIHOrder
{
    /// <summary>
    /// 普通用血申请
    /// </summary>
    public partial class ucBloodApplyForm1 : UserControl
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public ucBloodApplyForm1()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion

        #region 变量.属性

        /// <summary>
        /// InitXmlValue
        /// </summary>
        string InitXmlValue { get; set; }

        /// <summary>
        /// IsValueChange
        /// </summary>
        public bool IsValueChange { get; set; }

        #endregion

        #region Method

        #region GetContent
        /// <summary>
        /// GetContent
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            string fieldName = string.Empty;
            string fieldVal = string.Empty;
            StringBuilder info = new StringBuilder();
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.LabelControl)
                {
                    fieldName = ((DevExpress.XtraEditors.LabelControl)ctrl).AccessibleName;
                    fieldVal = ((DevExpress.XtraEditors.LabelControl)ctrl).Text;
                }
                else if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.TextEdit)ctrl).Properties.AccessibleName;
                    fieldVal = ((DevExpress.XtraEditors.TextEdit)ctrl).Text.Trim();
                }
                else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.AccessibleName;
                    fieldVal = ((DevExpress.XtraEditors.CheckEdit)ctrl).Checked ? "1" : "0";
                }
                else if (ctrl is DevExpress.XtraEditors.DateEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.DateEdit)ctrl).Properties.AccessibleName;
                    fieldVal = ((DevExpress.XtraEditors.DateEdit)ctrl).Text;
                }
                else if (ctrl is System.Windows.Forms.RichTextBox)
                {
                    fieldName = ((System.Windows.Forms.RichTextBox)ctrl).AccessibleName;
                    fieldVal = ((System.Windows.Forms.RichTextBox)ctrl).Text.Trim();
                }
                if (!string.IsNullOrEmpty(fieldName))
                {
                    info.AppendLine(string.Format("<{0}>{1}</{2}>", fieldName, fieldVal, fieldName));
                }
            }
            return "<appData>" + Environment.NewLine + info.ToString() + Environment.NewLine + "</appData>";
        }
        #endregion

        #region SetContent
        /// <summary>
        /// SetContent
        /// </summary>
        /// <param name="dicVal"></param>
        public void SetContent(Dictionary<string, string> dicVal)
        {
            string fieldName = string.Empty;
            string fieldVal = string.Empty;
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.LabelControl)
                {
                    fieldName = ((DevExpress.XtraEditors.LabelControl)ctrl).AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        ((DevExpress.XtraEditors.LabelControl)ctrl).Text = dicVal.ContainsKey(fieldName) ? dicVal[fieldName] : "";
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.TextEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.TextEdit)ctrl).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        ((DevExpress.XtraEditors.TextEdit)ctrl).Text = dicVal.ContainsKey(fieldName) ? dicVal[fieldName] : "";
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        ((DevExpress.XtraEditors.CheckEdit)ctrl).Checked = dicVal.ContainsKey(fieldName) ? (dicVal[fieldName] == "1" ? true : false) : false;
                    }
                }
                else if (ctrl is DevExpress.XtraEditors.DateEdit)
                {
                    fieldName = ((DevExpress.XtraEditors.DateEdit)ctrl).Properties.AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        ((DevExpress.XtraEditors.DateEdit)ctrl).Text = dicVal.ContainsKey(fieldName) ? dicVal[fieldName] : "";
                    }
                }
                else if (ctrl is System.Windows.Forms.RichTextBox)
                {
                    fieldName = ((System.Windows.Forms.RichTextBox)ctrl).AccessibleName;
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        ((System.Windows.Forms.RichTextBox)ctrl).Text = dicVal.ContainsKey(fieldName) ? dicVal[fieldName] : "";
                    }
                }
            }
            InitXmlValue = this.GetContent();
        }
        #endregion

        #region SetEditValueChangedEvent
        /// <summary>
        /// SetEditValueChangedEvent
        /// </summary>
        /// <param name="ctrl"></param>
        public void SetEditValueChangedEvent(System.Windows.Forms.Control ctrl)
        {
            if (ctrl is DevExpress.XtraEditors.CheckEdit)
            {
                ((DevExpress.XtraEditors.CheckEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.TextEdit)
            {
                ((DevExpress.XtraEditors.TextEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.MemoEdit)
            {
                ((DevExpress.XtraEditors.MemoEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.ComboBoxEdit)
            {
                ((DevExpress.XtraEditors.ComboBoxEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.DateEdit)
            {
                ((DevExpress.XtraEditors.DateEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.TimeEdit)
            {
                ((DevExpress.XtraEditors.TimeEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is DevExpress.XtraEditors.RadioGroup)
            {
                ((DevExpress.XtraEditors.RadioGroup)ctrl).SelectedIndexChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is Common.Controls.LookUpEdit)
            {
                ((Common.Controls.LookUpEdit)ctrl).EditValueChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else if (ctrl is System.Windows.Forms.RichTextBox)
            {
                ((System.Windows.Forms.RichTextBox)ctrl).TextChanged += new System.EventHandler(ctrl_EditValueChanged);
            }
            else
            {
                if (ctrl.HasChildren)
                {
                    foreach (System.Windows.Forms.Control ctrl2 in ctrl.Controls)
                    {
                        SetEditValueChangedEvent(ctrl2);
                    }
                }
            }
        } 

        void ctrl_EditValueChanged(object sender, EventArgs e)
        {
            this.IsValueChange = true;
        }  
        #endregion

        #endregion

        #region Event

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBloodApplyForm1_Load(object sender, EventArgs e)
        {
            this.SetEditValueChangedEvent(this);
        }

        #endregion

        #region OnPaint
        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.CheckEdit)
                {
                    if (((DevExpress.XtraEditors.CheckEdit)ctrl).Checked)
                    {
                        ((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.Appearance.ForeColor = Color.Blue;
                    }
                    else
                    {
                        ((DevExpress.XtraEditors.CheckEdit)ctrl).Properties.Appearance.ForeColor = Color.Black;
                    }
                }
            }
            base.OnPaint(e);
        }
        #endregion

        #region CheckBox

        #region chkSxzltys

        private void chkSxzltys_yq_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxzltys_yq.Checked)
            {
                this.chkSxzltys_wq.Checked = false;
            }
        }

        private void chkSxzltys_wq_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxzltys_wq.Checked)
            {
                this.chkSxzltys_yq.Checked = false;
            }
        }
        #endregion

        #region chkSqlx_jzyx

        private void chkSqlx_jzyx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSqlx_jzyx.Checked)
            {
                this.chkSqlx_cgyx.Checked = false;
                this.chkSqlx_bx.Checked = false;
            }
        }

        private void chkSqlx_cgyx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSqlx_cgyx.Checked)
            {
                this.chkSqlx_jzyx.Checked = false;
                this.chkSqlx_bx.Checked = false;
            }
        }

        private void chkSqlx_bx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSqlx_bx.Checked)
            {
                this.chkSqlx_jzyx.Checked = false;
                this.chkSqlx_cgyx.Checked = false;
            }
        }

        #endregion

        #region chkSxs

        private void chkSxs_w_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxs_w.Checked)
            {
                this.chkSxs_y.Checked = false;
            }
        }

        private void chkSxs_y_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxs_y.Checked)
            {
                this.chkSxs_w.Checked = false;
            }
        }


        #endregion

        #region chkSd

        private void chkSd_bs_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSd_bs.Checked)
            {
                this.chkSd_wb.Checked = false;
            }
        }

        private void chkSd_wb_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSd_wb.Checked)
            {
                this.chkSd_bs.Checked = false;
            }
        }
        #endregion

        #region chkSxmd

        private void chkSxmd_tgxynl_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxmd_tgxynl.Checked)
            {
                this.chkSxmd_jznxgn.Checked = false;
                this.chkSxmd_qt.Checked = false;
                this.txtSxmd_qt.Enabled = false;
            }
        }

        private void chkSxmd_jznxgn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxmd_jznxgn.Checked)
            {
                this.chkSxmd_tgxynl.Checked = false;
                this.chkSxmd_qt.Checked = false;
                this.txtSxmd_qt.Enabled = false;
            }
        }

        private void chkSxmd_qt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSxmd_qt.Checked)
            {
                this.chkSxmd_tgxynl.Checked = false;
                this.chkSxmd_jznxgn.Checked = false;
                this.txtSxmd_qt.Enabled = true;
            }
        }
        #endregion

        #endregion

        #endregion

    }
}
