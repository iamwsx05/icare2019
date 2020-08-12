using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Common.Controls
{
    public partial class ucEmployee : UserControl
    {
        public ucEmployee()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            this.OriWidth = this.Width;
            this.OriLocationX = this.lueEmp.Location.X;
        }

        int OriWidth { get; set; }

        int OriLocationX { get; set; }

        /// <summary>
        /// 是否全职工
        /// </summary>
        [Browsable(false)]
        public bool IsAllEmployee
        {
            get { return this.rdoType.SelectedIndex == 0 ? true : false; }
        }

        bool _IsSingleEmployee = false;

        [Browsable(false)]
        public string TextValue
        {
            get
            {
                return this.lueEmp.Text.Trim();
            }
        }

        /// <summary>
        /// 是否单个医师
        /// </summary>
        [Browsable(true)]
        public bool IsSingleEmployee
        {
            set
            {
                if (value)
                {
                    this.rdoType.Visible = false;
                    this.lblDoct.Visible = true;
                    this.lueEmp.Location = new Point(this.OriLocationX - 65, this.lueEmp.Location.Y);
                    this.Width = this.OriWidth - 65;
                }
                else
                {
                    this.rdoType.Visible = true;
                    this.lblDoct.Visible = false;
                    this.Width = this.OriWidth;
                    this.lueEmp.Location = new Point(this.OriLocationX, this.lueEmp.Location.Y);
                }
                _IsSingleEmployee = value;
            }
            get { return _IsSingleEmployee; }
        }

        /// <summary>
        /// 职工实体
        /// </summary>
        [Browsable(false)]
        public EntityCodeOperator EmpVo { get; set; }

        private List<EntityCodeOperator> DataEmployee { get; set; }

        private void ucEmployee_Load(object sender, System.EventArgs e)
        {
            if (DesignMode) return;
            this.rdoType.SelectedIndex = 1;
            DataEmployee = new List<EntityCodeOperator>();
            foreach (EntityCodeOperator item in GlobalDic.DataSourceEmployee)
            {
                if (!DataEmployee.Exists(t => t.operCode == item.operCode))
                {
                    DataEmployee.Add(item);
                }
            }

            this.lueEmp.Properties.PopupWidth = 200;
            this.lueEmp.Properties.PopupHeight = 400;
            this.lueEmp.Properties.ValueColumn = EntityCodeOperator.Columns.operCode;
            this.lueEmp.Properties.DisplayColumn = EntityCodeOperator.Columns.operName;
            this.lueEmp.Properties.Essential = false;
            this.lueEmp.Properties.ShowColumn = EntityCodeOperator.Columns.operCode + "|" + EntityCodeOperator.Columns.operName;
            this.lueEmp.Properties.IsUseShowColumn = true;
            this.lueEmp.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operCode, 60);
            this.lueEmp.Properties.ColumnWidth.Add(EntityCodeOperator.Columns.operName, 140);
            this.lueEmp.Properties.DataSource = DataEmployee.ToArray();
            this.lueEmp.Properties.SetSize();
        }

        private void lueDept_HandleDBValueChanged(object sender)
        {
            if (this.lueEmp.Properties.DBRow != null)
            {
                EmpVo = this.lueEmp.Properties.DBRow as EntityCodeOperator;
            }
            else
            {
                EmpVo = null;
            }
        }

        public void SelectEmp(EntityCodeOperator vo)
        {
            this.rdoType.SelectedIndex = 1;
            this.lueEmp.Properties.DBValue = vo.operCode;
            this.lueEmp.Properties.DisplayValue = vo.operName;
            this.lueEmp.Text = vo.operName;

            if (vo != null)
            {
                EmpVo = new EntityCodeOperator();
                //EmpVo.Empid = vo.Empid;
                EmpVo.operCode = vo.operCode;
                EmpVo.operName = vo.operName;
            }
            this.Refresh();
        }
    }
}
