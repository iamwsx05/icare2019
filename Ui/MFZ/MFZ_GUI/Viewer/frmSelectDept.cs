using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// 班次维护界面
    /// </summary>
    public partial class frmSelectDept : Form
    {

        public frmSelectDept()
        {
            InitializeComponent();
        }

        #region 私有成员

        private List<clsDept> m_lstDepts;

        #endregion

        #region 公开成员

        public clsDept SelectedDept
        {
            get
            {
                return this.m_cboDeptName.SelectedItem as clsDept;
            }
        }
        public List<clsDept> Depts
        {
            set
            {
                m_lstDepts = value;
                if (m_lstDepts.Count==0)
                {
                    MessageBox.Show("当前班次没有任何医生！");
                    return;
                }
                foreach (clsDept dept in m_lstDepts)
                {
                    this.m_cboDeptName.Items.Add(dept);
                }
                this.m_cboDeptName.SelectedIndex = 0;
            }
        }

        #endregion

        #region 事件实现

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;
        }

        private void m_cmdSubmit_Click(object sender, EventArgs e)
        {
            if (m_cboDeptName.SelectedItem == null)
            {
                MessageBox.Show("请选择科室!");
                return;
            }
            this.Hide();
            this.DialogResult = DialogResult.Yes;
        }

        private void frmSelectDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_cmdSubmit_Click(sender, e);
                if (this.m_cmdCancel.Focused)
                {
                    m_cmdCancel_Click(null, null);
                }
            }
        }

        private void frmSelectDept_Load(object sender, EventArgs e)
        {
            m_cmdSubmit.Focus();
        }

        #endregion

    }
}