using Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace Common.Controls
{
    public partial class ucDept : UserControl
    {
        public ucDept()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// 值变化事件
        /// </summary>
        public event _HandleDBValueChanged HandleDBValueChanged;

        /// <summary>
        /// 是否显示所属科室
        /// </summary>
        public bool IsShowOwnDept { get; set; }

        /// <summary>
        /// 所选科室对象
        /// </summary>
        public EntityCodeDepartment DeptVo { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName
        {
            get
            {
                return this.lueDept.Text.Trim();
            }
            set
            {
                this.lueDept.Text = value;
            }
        }

        /// <summary>
        /// 字体是否粗体
        /// </summary>
        public void SetFontBlod(bool isBlod)
        {
            if (isBlod)
                this.lueDept.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Bold);
            else
                this.lueDept.Font = new System.Drawing.Font("宋体", 9.5F, System.Drawing.FontStyle.Regular);
        }

        private void ucDept_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            this.SuspendLayout();
            this.lueDept.Properties.PopupWidth = 200;
            this.lueDept.Properties.PopupHeight = 400;
            this.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            this.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.ShowColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.IsUseShowColumn = true;
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 140);
            this.lueDept.Properties.FilterColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName + "|" + EntityCodeDepartment.Columns.pyCode + "|" + EntityCodeDepartment.Columns.wbCode;
            if (GlobalDic.DataSourceDepartment != null)
            {
                if (this.IsShowOwnDept)
                {
                    List<EntityCodeDepartment> data = new System.Collections.Generic.List<EntityCodeDepartment>();
                    foreach (EntityCodeDepartment item in GlobalDic.DataSourceDepartment)
                    {
                        if (GlobalLogin.objLogin.lstDept.Any(t => t.deptCode == item.deptCode))
                        {
                            data.Add(item);
                        }
                    }
                    if (data.Count > 0) this.lueDept.Properties.DataSource = data.ToArray();
                }
                else
                {
                    this.lueDept.Properties.DataSource = GlobalDic.DataSourceDepartment.ToArray();
                }
            }
            this.ResumeLayout();
            this.lueDept.Properties.SetSize();
        }

        public void ResetDept()
        {
            if (GlobalDic.DataSourceDepartment != null)
            {
                if (this.IsShowOwnDept)
                {
                    List<EntityCodeDepartment> data = new System.Collections.Generic.List<EntityCodeDepartment>();
                    foreach (EntityCodeDepartment item in GlobalDic.DataSourceDepartment)
                    {
                        if (GlobalLogin.objLogin.lstDept.Any(t => t.deptCode == item.deptCode))
                        {
                            data.Add(item);
                        }
                    }
                    if (data.Count > 0) this.lueDept.Properties.DataSource = data.ToArray();
                }
                else
                {
                    this.lueDept.Properties.DataSource = GlobalDic.DataSourceDepartment.ToArray();
                }
            }
            this.ResumeLayout();
            this.lueDept.Properties.SetSize();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            if (this.lueDept.Properties.DBRow != null)
            {
                DeptVo = this.lueDept.Properties.DBRow as EntityCodeDepartment;
            }
            else
            {
                DeptVo = null;
            }
            if (HandleDBValueChanged != null)
            {
                HandleDBValueChanged(sender);
            }
        }
    }
}
