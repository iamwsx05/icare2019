using Common.Entity;
using Common.Utils;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Common.Controls.Emr
{
    public partial class frmTableBatchSign : System.Windows.Forms.Form
    {
        public frmTableBatchSign()
        {
            InitializeComponent();
        }

        internal List<string> SignColNameIn { get; set; }

        internal string SignColNameOut { get; set; }

        internal List<EntitySignature> SignEmpInfo { get; set; }

        internal int FromRowNo { get; set; }
        internal int ToRowNo { get; set; }

        internal int MaxRowNo { get; set; }

        private void frmTableBatchSign_Load(object sender, EventArgs e)
        {
            if (SignColNameIn != null)
            {
                cboSignColName.Properties.Items.AddRange(SignColNameIn.ToArray());
                cboSignColName.SelectedIndex = 0;
                //if (SignColNameIn.Count == 1)
                //    cboSignColName.Enabled = false;
            }
            this.txtFromRowNo.Focus();
        }

        private void frmTableBatchSign_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int intMinRowNo = 0;
            string strMinRowNo = this.txtFromRowNo.Text.Trim();
            if (strMinRowNo == string.Empty)
            {
                DialogBox.Msg("请输入起始行号.");
                txtFromRowNo.Focus();
                return;
            }
            try
            {
                intMinRowNo = int.Parse(strMinRowNo);
                if (intMinRowNo < 0)
                {
                    DialogBox.Msg("起始行号不能小于0，请重新输入.");
                    txtFromRowNo.Focus();
                    return;
                }
            }
            catch
            {
                DialogBox.Msg("起始行号输入不正确，请重新输入.");
                txtFromRowNo.Focus();
                return;
            }
            int intMaxRowNo = 0;
            string strMaxRowNo = this.txtToRowNo.Text.Trim();
            if (strMaxRowNo == string.Empty)
            {
                DialogBox.Msg("请输入结束行号.");
                txtToRowNo.Focus();
                return;
            }
            try
            {
                intMaxRowNo = int.Parse(strMaxRowNo);
                if (intMaxRowNo < intMinRowNo)
                {
                    DialogBox.Msg("结束行号不能小于开始行号，请重新输入.");
                    txtToRowNo.Focus();
                    return;
                }
                if (intMaxRowNo > MaxRowNo)
                {
                    DialogBox.Msg("结束行号不能允许的最大行号 " + MaxRowNo.ToString() + "，请重新输入.");
                    txtToRowNo.Focus();
                    return;
                }
            }
            catch
            {
                DialogBox.Msg("结束行号输入不正确，请重新输入.");
                txtToRowNo.Focus();
                return;
            }

            if (ctlSignature.m_lstNoSaveSignature != null && ctlSignature.m_lstNoSaveSignature.Count > 0)
            {
                FromRowNo = intMinRowNo;
                ToRowNo = intMaxRowNo;
                SignColNameOut = cboSignColName.Text;
                SignEmpInfo = ctlSignature.m_lstNoSaveSignature;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (ctlSignature.IsAutoSignature == 1)
                {
                    EntitySignature dcSign = new EntitySignature();
                    dcSign.empId = GlobalLogin.objLogin.EmpNo;
                    dcSign.empName = GlobalLogin.objLogin.EmpName;
                    dcSign.signDate = DateTime.Now;
                    dcSign.recordDate = DateTime.Now;
                    dcSign.techLevelCode = GlobalLogin.objLogin.TechLevelCode;
                    dcSign.techLevelName = GlobalLogin.objLogin.TechLevelName;
                    dcSign.registerId = GlobalPatient.currPatient.RegisterID;
                    dcSign.caseCode = GlobalCase.caseInfo.CaseCode;
                    dcSign.objectID = ctlSignature.ItemName;
                    ctlSignature.AddSignEmp(dcSign);
                }
                else
                {
                    DialogBox.Msg("请签名.");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
