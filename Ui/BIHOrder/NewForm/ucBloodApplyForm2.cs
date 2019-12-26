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
    /// 自体输血
    /// </summary>
    public partial class ucBloodApplyForm2 : UserControl
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public ucBloodApplyForm2()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion

        #region 属性.变量

        /// <summary>
        /// InitXmlValue
        /// </summary>
        string InitXmlValue { get; set; }

        /// <summary>
        /// IsValueChange
        /// </summary>
        public bool IsValueChange { get; set; }

        #endregion

        #region 方法

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
            this.InitXmlValue = this.GetContent();
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

        #region 事件

        #region Load
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBloodApplyForm2_Load(object sender, EventArgs e)
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

        private void chkYbqk_lh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkYbqk_lh.Checked)
            {
                this.chkYbqk_yb.Checked = false;
                this.chkYbqk_c.Checked = false;
            }
        }

        private void chkYbqk_yb_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkYbqk_yb.Checked)
            {
                this.chkYbqk_lh.Checked = false;
                this.chkYbqk_c.Checked = false;
            }
        }

        private void chkYbqk_c_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkYbqk_c.Checked)
            {
                this.chkYbqk_lh.Checked = false;
                this.chkYbqk_yb.Checked = false;
            }
        }

        private void chkTys_yes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTys_yes.Checked)
            {
                this.chkTys_no.Checked = false;
            }
        }

        private void chkTys_no_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTys_no.Checked)
            {
                this.chkTys_yes.Checked = false;
            }
        }
        #endregion

        #endregion

    }
}
