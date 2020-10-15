using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 选择医生
    /// </summary>
    public partial class frmAidChooseDoct : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region 变量
        /// <summary>
        /// 医生ID串(格式 '0001','0002','0003')
        /// </summary>
        private string doctidarr = "";
        /// <summary>
        /// 医生ID串(格式 '0001','0002','0003')
        /// </summary>
        public string DoctIDArr
        {
            get
            {
                return doctidarr;
            }
        }

        /// <summary>
        /// 医生名称串(格式 张三,李四,王五)
        /// </summary>
        public string DoctNameArr { get; set; }

        /// <summary>
        /// 0 全部; 1 医生; 3 抗菌药会诊专家
        /// </summary>
        public int EmpTypeId { get; set; }

        /// <summary>
        /// 职工Table
        /// </summary>
        DataTable dtEmployee { get; set; }

        /// <summary>
        /// 是否显示本科室员工
        /// </summary>
        public bool IsFilterMyDept { get; set; }

        #endregion

        /// <summary>
        /// 构造

        /// </summary>
        public frmAidChooseDoct()
        {
            InitializeComponent();
        }

        private void frmAidChooseDoct_Load(object sender, EventArgs e)
        {
            long l = 0;
            clsDcl_Charge objCharge = new clsDcl_Charge();
            DataTable dt;
            if (EmpTypeId > 0)
            {
                l = objCharge.m_lngGetEmployee(out dt, EmpTypeId);
            }
            else
            {
                l = objCharge.m_lngGetEmployee(out dt);
            }
            this.dtEmployee = dt;
            if (this.IsFilterMyDept == false)
                this.SetDataSource(dt);
            this.timer.Enabled = true;
        }

        void SetDataSource(DataTable dt)
        {
            this.dtDoct.Rows.Clear();
            if (dt == null || dt.Rows.Count == 0)
                return;

            int n = 0;
            string[] sarr = null;
            foreach (DataRow dr in dt.Rows)
            {
                sarr = new string[5];
                sarr[0] = "F";
                sarr[1] = Convert.ToString(++n);
                sarr[2] = dr["empno_chr"].ToString().Trim();
                sarr[3] = dr["lastname_vchr"].ToString().Trim();
                sarr[4] = weCare.Core.Utils.SpellCodeHelper.GetPyCode(dr["lastname_vchr"].ToString().Trim());

                int row = this.dtDoct.Rows.Add(sarr);
                this.dtDoct.Rows[row].Tag = dr;
            }
        }

        private void frmAidChooseDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string val = this.txtVal.Text.Trim();

                if (val == "")
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < this.dtDoct.Rows.Count; i++)
                    {
                        if (this.dtDoct.Rows[i].Cells["colGh"].Value.ToString().Contains(val) || this.dtDoct.Rows[i].Cells["colXm"].Value.ToString().Contains(val) || this.dtDoct.Rows[i].Cells["colPyCode"].Value.ToString().Contains(val))
                        {
                            this.dtDoct.CurrentCell = this.dtDoct.Rows[i].Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            doctidarr = "";
            DoctNameArr = string.Empty;

            if (this.chkAll.Checked)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                for (int i = 0; i < this.dtDoct.Rows.Count; i++)
                {
                    if (this.dtDoct.Rows[i].Cells["colZt"].Value.ToString().Trim() == "T")
                    {
                        DataRow dr = this.dtDoct.Rows[i].Tag as DataRow;
                        doctidarr += "'" + dr["empid_chr"].ToString() + "',";
                        DoctNameArr += dr["lastname_vchr"].ToString() + ",";
                    }
                }

                if (doctidarr == "")
                {
                    MessageBox.Show("请从列表中选择统计医生。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    doctidarr = doctidarr.Substring(0, doctidarr.Length - 1);
                    DoctNameArr = DoctNameArr.Substring(0, DoctNameArr.Length - 1);
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAll.Checked)
            {
                for (int i = 0; i < this.dtDoct.Rows.Count; i++)
                {
                    this.dtDoct.Rows[i].Cells["colZt"].Value = "T";
                }
            }
            else
            {
                for (int i = 0; i < this.dtDoct.Rows.Count; i++)
                {
                    this.dtDoct.Rows[i].Cells["colZt"].Value = "F";
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.txtVal.Focus();
            this.chkMyDept.Checked = this.IsFilterMyDept;

        }

        private void chkMyDept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMyDept.Checked)
            {
                DataTable dt = this.dtEmployee.Clone();
                DataRow[] drr = this.dtEmployee.Select(string.Format("deptid_chr = '{0}'", string.IsNullOrEmpty(this.LoginInfo.m_strInpatientAreaID) ? this.LoginInfo.m_strDepartmentID : this.LoginInfo.m_strInpatientAreaID));
                if (drr != null && drr.Length > 0)
                {
                    foreach (DataRow dr in drr)
                    {
                        dt.LoadDataRow(dr.ItemArray, true);
                    }
                }
                this.SetDataSource(dt);
            }
            else
            {
                this.SetDataSource(this.dtEmployee);
            }
        }
    }
}