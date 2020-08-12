using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Utils;
using weCare.Core.Entity;
using Common.Entity;

namespace Common.Controls
{
    public partial class frmSignature : frmBasePopup
    {
        public frmSignature()
        {
            InitializeComponent(); 
        }

        public frmSignature(List<int> _FilterRoleID)
            : this()
        {
            FilterRoleID = _FilterRoleID;
        }

        private List<int> FilterRoleID { get; set; }

        public EntityCodeOperator EmpVO { get; set; }

        private void Filter()
        {
            List<EntityCodeOperator> lstEmp = new List<EntityCodeOperator>();
            if (this.chkAll.Checked)
            {
                // MER
                //lstEmp.AddRange(GlobalDic.DataSourceDoctor);
                //lstEmp.AddRange(GlobalDic.DataSourceNurse);
                // 其他
                lstEmp.AddRange(GlobalDic.DataSourceEmployee);
            }
            else
            {
                // EMR
                //lstEmp.AddRange(GlobalDic.DataSourceDoctor.FindAll(t => t.DeptNo == GlobalLogin.objLogin.DeptCode));
                //lstEmp.AddRange(GlobalDic.DataSourceNurse.FindAll(t => t.DeptNo == GlobalLogin.objLogin.DeptCode));
                // 其他
                lstEmp.AddRange(GlobalDic.DataSourceEmployee.FindAll(t => t.DeptNo == GlobalLogin.objLogin.DeptCode));
            }

            if (FilterRoleID != null && FilterRoleID.Count > 0)
            {
                bool blnExits = false;
                for (int i = lstEmp.Count - 1; i >= 0; i--)
                {
                    blnExits = false;
                    foreach (int roleid in FilterRoleID)
                    {
                        if (lstEmp[i].RoleID.IndexOf(roleid.ToString()) >= 0)
                        {
                            blnExits = true;
                            break;
                        }
                    }
                    if (!blnExits)
                    {
                        lstEmp.RemoveAt(i);
                    }
                }
            }

            string val = Convert.ToString(this.txtFind.EditValue);
            if (!string.IsNullOrEmpty(val))
            {
                gcItem.DataSource = lstEmp.FindAll(t => t.operCode.Contains(val) || t.operName.Contains(val) || t.pyCode.Contains(val) || t.wbCode.Contains(val));
            }
            else
            {
                gcItem.DataSource = lstEmp;
            }
        }

        private void SelectRow()
        {
            if (gvItem.FocusedRowHandle < 0) return;
            EmpVO = gvItem.GetFocusedRow() as EntityCodeOperator;
            if (EmpVO != null)
            {
                //this.Hide();
                Point p = this.Location;
                this.Location = new Point(-500, -500);
                frmSignaturePwd frm = new frmSignaturePwd(EmpVO, p);
                this.DialogResult = frm.ShowDialog();
            }
        }

        private void frmSignature_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            Filter();
        }

        private void frmSignature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvItem_DoubleClick(object sender, EventArgs e)
        {
            SelectRow();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFind_EditValueChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectRow();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
