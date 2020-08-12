using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class ucDepartment : UserControl
    {
        public ucDepartment()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            this.rdoType.SelectedIndex = 0;
            this.lueDept.Properties.DBValue = string.Empty;
            this.lueDept.Properties.DBRow = null;
            this.lueDept.Text = string.Empty;
        }

        /// <summary>
        /// SelectDept
        /// </summary>
        public void SelectDept(EntityDeptInfo vo)
        {
            this.rdoType.SelectedIndex = 1;
            this.lueDept.Properties.DBValue = vo.DeptID.ToString();
            this.lueDept.Properties.DisplayValue = vo.DeptName;
            this.lueDept.Text = vo.DeptName;

            if (DeptVo == null)
            {
                DeptVo = new EntityCodeDepartment();
                //DeptVo.Deptid = vo.DeptID;
                DeptVo.deptCode = vo.DeptCode;
                DeptVo.deptName = vo.DeptName;
            }
            this.Refresh();
        }

        /// <summary>
        /// 是否全院
        /// </summary>
        [Browsable(false)]
        public bool IsAllDept
        {
            get { return this.rdoType.SelectedIndex == 0 ? true : false; }
        }

        /// <summary>
        /// 科室实体
        /// </summary>
        [Browsable(false)]
        public EntityCodeDepartment DeptVo { get; set; }

        private void ucDepartment_Load(object sender, System.EventArgs e)
        {
            if (DesignMode) return;
            this.rdoType.SelectedIndex = 0;

            this.lueDept.Properties.PopupWidth = 200;
            this.lueDept.Properties.PopupHeight = 400;
            this.lueDept.Properties.ValueColumn = EntityCodeDepartment.Columns.deptCode;
            this.lueDept.Properties.DisplayColumn = EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.Essential = false;
            this.lueDept.Properties.ShowColumn = EntityCodeDepartment.Columns.deptCode + "|" + EntityCodeDepartment.Columns.deptName;
            this.lueDept.Properties.IsUseShowColumn = true;
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptCode, 60);
            this.lueDept.Properties.ColumnWidth.Add(EntityCodeDepartment.Columns.deptName, 140);
            this.lueDept.Properties.DataSource = GlobalDic.DataSourceDepartment.ToArray();
            this.lueDept.Properties.SetSize();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            if (this.lueDept.Properties.DBRow != null)
            {
                DeptVo = this.lueDept.Properties.DBRow as EntityCodeDepartment;
            }
        }
    }
}
