using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// ����ά������
    /// </summary>
    public partial class frmDept : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {

        public frmDept()
        {
            InitializeComponent();
        }

        #region ˽�г�Ա

        private clsMFZDeptVO[] m_arrDept;
        bool m_blnNewDept = false; 
        
        #endregion

        #region ��������

        private void m_mthInitList()
        {
            m_lsvDepts.Items.Clear();
            m_lsvDepts.BeginUpdate();
            m_lsvDepts.Tag = m_arrDept;
            for (int i = 0; i < m_arrDept.Length; i++)
            {
                clsMFZDeptVO dept = m_arrDept[i];
                m_lsvDepts.Items.Add(m_ConstructListViewItemByVO(dept));
            }
            m_lsvDepts.EndUpdate();
        }

        private ListViewItem m_ConstructListViewItemByVO(clsMFZDeptVO dept)
        {
            ListViewItem item = new ListViewItem(dept.m_strDeptName);
            item.SubItems.Add(dept.m_strDeptNameShort);
            item.SubItems.Add(dept.m_strSummary);
            item.Tag = dept;
            return item;
        }

        private void m_ConstructListViewItemByVO(ListViewItem item, clsMFZDeptVO dept)
        {
            if (item.SubItems.Count > 0)
            {
                item.Text = dept.m_strDeptName;
                item.SubItems[1].Text = dept.m_strDeptNameShort;
                item.SubItems[3].Text = dept.m_strSummary;
            }
        }

        // �ؼ�չʾVO
        private void m_mthControlShowVO(clsMFZDeptVO p_objDept)
        {
            this.m_txtDeptName.m_StrDeptName = p_objDept.m_strDeptName;
            this.m_txtDeptNameShort.Text = p_objDept.m_strDeptNameShort;
            this.m_txtSammary.Text = p_objDept.m_strSummary;
        }

        // �ؼ�����VO
        private clsMFZDeptVO m_objControlConstructVO()
        {
            clsMFZDeptVO dept = new clsMFZDeptVO();
            dept.m_strDeptID = this.m_txtDeptName.m_StrDeptID;
            dept.m_strDeptName = m_strGetDeptName(dept.m_strDeptID);
            dept.m_strDeptNameShort = this.m_txtDeptNameShort.Text;
            dept.m_strSummary = this.m_txtSammary.Text;
            return dept;
        }

        private void m_mthDetailClear()
        {
            this.m_txtDeptNameShort.Clear();
            this.m_txtSammary.Clear();
            this.m_txtDeptName.Clear();
        }

        /// <summary>
        /// �����Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (this.m_txtDeptName.Text == string.Empty)
            {
                MessageBox.Show("���Ҳ���Ϊ�գ�");
                m_txtDeptName.Focus();
                return false;
            }

            if (m_blnNewDept == true)
            {
                for (int i = 0; i < m_arrDept.Length; i++)
                {
                    if (m_arrDept[i].m_strDeptID == m_txtDeptName.m_StrDeptID)
                    {
                        MessageBox.Show(string.Format("�Ѿ����{0}�ļ���ˣ�",m_txtDeptName.m_StrDeptName));
                        return false;
                    }
                }
            }

            return true;
        }

        private string m_strGetDeptName(string deptID)
        {
            com.digitalwave.Utility.ctlDeptTextBox deptBox = new com.digitalwave.Utility.ctlDeptTextBox();
            deptBox.m_StrDeptID = deptID;
            return deptBox.m_StrDeptName;
        } 

        #endregion

        #region �¼�ʵ��

        private void frmDept_Load(object sender, EventArgs e)
        {
            this.m_txtDeptName.Enabled = false;
            clsTmdDeptSmp.s_object.m_lngFind(out m_arrDept);
            m_mthInitList();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void m_lsvDepts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool isAsc = false;
            if (m_lsvDepts.Sorting == SortOrder.Ascending)
            {
                m_lsvDepts.Sorting = SortOrder.Descending;
            }
            else
            {
                m_lsvDepts.Sorting = SortOrder.Ascending;
                isAsc = true;
            }

            m_lsvDepts.ListViewItemSorter = new ListViewItemComparer(e.Column, isAsc, m_lsvDepts);
            m_lsvDepts.Sort();
        }

        private void frmDept_KeyDown(object sender, KeyEventArgs e)
        {
            base.m_mthSetKeyTab(e);

            if (e.KeyCode == Keys.F3)
            {
                m_cmdNew_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                m_cmdSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F5)
            {
                m_cmdDelete_Click(sender, e);
            }
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            m_txtDeptName.Enabled = true;
            m_txtDeptName.Focus();
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvDepts.FocusedItem != null)
            {
                this.m_lsvDepts.FocusedItem.Selected = false;
                this.m_lsvDepts.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthDetailClear();

            //���ù�꽹��
            //this.m_cmdSave.Focus();

            //����������־
            this.m_blnNewDept = true;
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvDepts.FocusedItem == null
             && !this.m_blnNewDept)
                return;
            if (!IsValid())
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            if (this.m_blnNewDept)
            {//�����ı���
                clsMFZDeptVO objDept = m_objControlConstructVO();
                long lngRes = clsTmdDeptSmp.s_object.m_lngInsert(objDept);
                
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewDept = false;
                    //���뵽����
                    clsMFZDeptVO[] objDepts = (clsMFZDeptVO[])this.m_lsvDepts.Tag;
                    clsMFZDeptVO[] objDeptsNewArr = new clsMFZDeptVO[objDepts.Length + 1];
                    objDepts.CopyTo(objDeptsNewArr, 0);
                    objDeptsNewArr[objDeptsNewArr.Length - 1] = objDept;
                    this.m_lsvDepts.Tag = objDeptsNewArr;

                    //�������
                    ListViewItem item = m_ConstructListViewItemByVO(objDept);
                    this.m_lsvDepts.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvDepts_SelectedIndexChanged(null, null);
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//�޸ĵı���
                clsMFZDeptVO objDept = (clsMFZDeptVO)this.m_lsvDepts.FocusedItem.Tag;

                clsMFZDeptVO objNewDept = new clsMFZDeptVO();
                objDept.m_mthCopyTo(objNewDept);
                objNewDept = m_objControlConstructVO();

                long lngRes = clsTmdDeptSmp.s_object.m_lngUpdate(objNewDept, objDept.m_strDeptID);

                if (lngRes > 0)
                {//�ɹ�
                    objNewDept.m_mthCopyTo(objDept);

                    this.m_lsvDepts.FocusedItem.Text = objDept.m_strDeptName;
                    this.m_lsvDepts.FocusedItem.SubItems[1].Text = objDept.m_strDeptNameShort;
                    this.m_lsvDepts.FocusedItem.SubItems[2].Text = objDept.m_strSummary;
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvDepts.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;
            clsMFZDeptVO objDept = (clsMFZDeptVO)this.m_lsvDepts.FocusedItem.Tag;
            clsMFZDeptVO objCopy = new clsMFZDeptVO();
            objDept.m_mthCopyTo(objCopy);

            long lngRes = clsTmdDeptSmp.s_object.m_lngDelete(objDept.m_strDeptID);

            if (lngRes > 0)
            {//�ɹ�
                int intIdx = this.m_lsvDepts.FocusedItem.Index;

                this.m_lsvDepts.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvDepts.Items.Count)
                {
                    this.m_lsvDepts.Items[intIdx].Selected = true;
                    this.m_lsvDepts.Items[intIdx].Focused = true;
                    this.m_lsvDepts_SelectedIndexChanged(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvDepts.Items[intIdx - 1].Selected = true;
                    this.m_lsvDepts.Items[intIdx - 1].Focused = true;
                    this.m_lsvDepts_SelectedIndexChanged(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvDepts_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_txtDeptName.Enabled = true;
            if (m_lsvDepts.FocusedItem == null)
            {
                return;
            }

            m_blnNewDept = false;
            clsMFZDeptVO Dept = m_lsvDepts.FocusedItem.Tag as clsMFZDeptVO;
            if (Dept != null)
            {
                m_mthControlShowVO(Dept);
            }
        }

        #endregion

    }
}
