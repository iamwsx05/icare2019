using Common.Entity;
using System;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Common.Controls
{
    public partial class ucDepts : UserControl
    {
        public ucDepts()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        public string DeptCode()
        {
            string deptCode = string.Empty;
            string deptName = string.Empty;
            int num = 0;
            if (this.chkcDept.Properties.Items.Count > 0)
            {
                for (int i = 0; i < this.chkcDept.Properties.Items.Count; i++)
                {
                    if (this.chkcDept.Properties.Items[i].CheckState == System.Windows.Forms.CheckState.Checked)
                    {
                        deptName = this.chkcDept.Properties.Items[i].Value.ToString();
                        if (!string.IsNullOrEmpty(deptName))
                        {
                            if (GlobalDic.DataSourceDepartment.Any(t => t.deptName == deptName))
                            {
                                num++;
                                deptCode += "'" + GlobalDic.DataSourceDepartment.FirstOrDefault(t => t.deptName == deptName).deptCode + "',";
                            }
                        }
                    }
                }
                if (deptCode != string.Empty)
                {
                    if (num == 1)
                        deptCode = deptCode.Replace("'", "").Replace(",", "");
                    else
                        deptCode = deptCode.TrimEnd(',');

                }
            }
            return deptCode;
        }

        private void ucDepts_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.chkcDept.Properties.Items.Clear();
            this.SuspendLayout();
            if (GlobalDic.DataSourceDepartment != null)
            {
                GlobalDic.DataSourceDepartment.Sort();
                foreach (EntityCodeDepartment item in GlobalDic.DataSourceDepartment)
                {
                    this.chkcDept.Properties.Items.Add(item.deptName);
                }
            }
            this.ResumeLayout();
        }
    }
}
