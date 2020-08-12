using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Common.Entity;
using Common.Utils;

namespace Common.Controls
{
    public partial class frmConfirm : frmBasePopup
    {
        public frmConfirm()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// 工号是否只读
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 数字签名ID
        /// </summary>
        public string CaKeyId { get; set; }

        List<string> EmpIdList { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string RoleCode
        {
            set
            {
                EmpIdList = new List<string>();
                if (!string.IsNullOrEmpty(value))
                {
                    EntityDefOperatorRole vo = new EntityDefOperatorRole();
                    vo.roleCode = value;
                    using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                    {
                        DataTable dt = proxy.Service.Select(vo, new System.Collections.Generic.List<string> { EntityDefOperatorRole.Columns.roleCode });
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string operCode = string.Empty;
                            foreach (DataRow dr in dt.Rows)
                            {
                                operCode = dr[EntityDefOperatorRole.Columns.operCode].ToString();
                                if (EmpIdList.IndexOf(operCode) < 0) EmpIdList.Add(operCode);
                            }
                        }
                    }
                }
            }
        }

        string CurrPwd { get; set; }

        private bool Check(string empNo)
        {
            if (empNo == GlobalLogin.objLogin.EmpNo)
            {
                if (txtPwd.Text != GlobalLogin.objLogin.Pwd)
                {
                    DialogBox.Msg("密码错误!");
                    txtPwd.Focus();
                    return false;
                }
            }
            else
            {
                if (txtPwd.Text != CurrPwd)
                {
                    DialogBox.Msg("密码错误!");
                    txtPwd.Focus();
                    return false;
                }
            }

            if (EmpIdList != null)
            {
                if (!EmpIdList.Contains(this.EmpNo))
                {
                    DialogBox.Msg(this.EmpName + "不具有指定角色权限!");
                    txtEmpNo.Focus();
                    return false;
                }
            }

            //if (!clsGlobalLoginInfo.objLoginInfo.blnTraineeFlag && !clsHelper.s_blnLoginerCAVerify(this.m_strCAKeyID))
            //{
            //    return false;
            //}

            return true;
        }

        private void EditValueChanged(string empNo)
        {
            if (!string.IsNullOrEmpty(empNo))
            {
                EntityCodeOperator vo = new EntityCodeOperator();
                vo.disable = "F";
                vo.operCode = empNo;
                using (ProxyEntityFactory proxy = new ProxyEntityFactory())
                {
                    vo = EntityTools.ConvertToEntity<EntityCodeOperator>(proxy.Service.Select(vo, new List<string> { EntityCodeOperator.Columns.disable, EntityCodeOperator.Columns.operCode }));
                    //this.CaKeyId = ?
                    if (empNo != vo.operCode)
                    {
                        this.txtEmpName.Text = string.Empty;
                        this.lblInfo.Visible = true;
                    }
                    else
                    {
                        this.txtEmpName.Text = vo.operName;
                        CurrPwd = vo.pwd;
                        empNo = vo.operCode;
                        EmpName = vo.operName;
                        this.lblInfo.Visible = false;
                    }
                }
            }
        }

        private void frmConfirm_Load(object sender, System.EventArgs e)
        {
            this.txtEmpNo.Properties.ReadOnly = IsReadOnly;
            if (IsReadOnly)
            {
                SendKeys.Send("{Tab}");
            }
            if (string.IsNullOrEmpty(EmpNo)) this.EmpNo = GlobalLogin.objLogin.EmpNo;
            if (string.IsNullOrEmpty(EmpName)) this.EmpName = GlobalLogin.objLogin.EmpName;
            if (string.IsNullOrEmpty(CaKeyId)) this.CaKeyId = GlobalLogin.objLogin.SignKeyID;

            this.txtEmpNo.Text = this.EmpNo;
            this.txtEmpName.Text = this.EmpName;
        }

        private void frmConfirm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmConfirm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.lblInfo.Visible)
                {
                    DialogBox.Msg("工号有误");
                    txtEmpNo.Focus();
                    e.Cancel = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtEmpNo.Text.Trim()))
                {
                    DialogBox.Msg("请输入工号");
                    txtEmpNo.Focus();
                    e.Cancel = true;
                    return;
                }
                if (!Check(txtEmpNo.Text.Trim()))
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void txtEmpNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditValueChanged(txtEmpNo.Text.Trim());
            }
        }

        private void txtEmpNo_Leave(object sender, System.EventArgs e)
        {
            string empNo = txtEmpNo.Text.Trim();
            if (empNo != GlobalLogin.objLogin.EmpNo)
            {
                EditValueChanged(empNo);
            }
        }

        private void txtPwd_Enter(object sender, System.EventArgs e)
        {
            txtPwd.SelectAll();
        }

        private void txtPwd_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
