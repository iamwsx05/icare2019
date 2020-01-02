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
    /// ���ά������
    /// </summary>
    public partial class frmSelectDept : Form
    {

        public frmSelectDept()
        {
            InitializeComponent();
        }

        #region ˽�г�Ա

        private List<clsDept> m_lstDepts;

        #endregion

        #region ������Ա

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
                    MessageBox.Show("��ǰ���û���κ�ҽ����");
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

        #region �¼�ʵ��

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;
        }

        private void m_cmdSubmit_Click(object sender, EventArgs e)
        {
            if (m_cboDeptName.SelectedItem == null)
            {
                MessageBox.Show("��ѡ�����!");
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