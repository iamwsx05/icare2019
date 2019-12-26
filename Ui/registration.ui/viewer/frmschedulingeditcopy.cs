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
using DevExpress.XtraEditors;

namespace Registration.Ui
{
    public partial class frmSchedulingEditCopy : frmBasePopup
    {
        public frmSchedulingEditCopy(List<string> _lstWeekName)
        {
            InitializeComponent();
            if (!DesignMode)
            { 
                lstWeekName = _lstWeekName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        List<string> lstWeekName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        List<CheckEdit> lstChk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal int SWeekId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        internal List<int> lstWeekId { get; set; }

        private void frmSchedulingEditCopy_Load(object sender, EventArgs e)
        {
            lstWeekId = new List<int>();
            lstChk = new List<CheckEdit>();
            lstChk.Add(this.chk1);
            lstChk.Add(this.chk2);
            lstChk.Add(this.chk3);
            lstChk.Add(this.chk4);
            lstChk.Add(this.chk5);
            lstChk.Add(this.chk6);
            lstChk.Add(this.chk7);
        }

        private void cboWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboWeek.Text == string.Empty) return;
            foreach (CheckEdit chk in lstChk)
            {
                if (Function.GetWeekIndex(this.cboWeek.Text) == Function.Int(chk.Tag.ToString()))
                {
                    chk.Checked = false;
                    chk.Enabled = false;
                }
                else
                {
                    chk.Enabled = true;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.cboWeek.Text == string.Empty)
            {
                this.cboWeek.Focus();
                DialogBox.Msg("请选择复制日期。");
                return;
            }
            SWeekId = Function.GetWeekIndex(this.cboWeek.Text);
            foreach (CheckEdit chk in lstChk)
            {
                if (chk.Checked) lstWeekId.Add(Function.Int(chk.Tag.ToString()));
            }
            if (lstWeekId.Count > 0)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                DialogBox.Msg("请选择复制日期。");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
