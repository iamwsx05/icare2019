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
    public partial class frmDiagnoseArea : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ���캯��

        public frmDiagnoseArea()
        {
            InitializeComponent();
        }
 
        #endregion

        #region ˽�г�Ա

        bool m_blnNewDiagnoseArea = true; 
        
        #endregion

        #region ��������

        // ����б�
        private void m_mthShowDiagnoseAreaList(clsMFZDiagnoseAreaVO[] p_arrAreas)
        {
            m_lsvDiagnoseArea.BeginUpdate();
            m_lsvDiagnoseArea.Tag = p_arrAreas;
            for (int i = 0; i < p_arrAreas.Length; i++)
            {
                ListViewItem item = new ListViewItem(p_arrAreas[i].m_intDiagnoseAreaID.ToString());
                item.SubItems.Add(p_arrAreas[i].m_strDiagnoseAreaName);
                item.SubItems.Add(p_arrAreas[i].m_strSummary);
                item.Tag = p_arrAreas[i];
                m_lsvDiagnoseArea.Items.Add(item);
            }
            m_lsvDiagnoseArea.EndUpdate();
        }

        // �ؼ���ʾVO
        private void m_mthControlsShowVO(clsMFZDiagnoseAreaVO diagnoseArea)
        {
            m_txtDiagnoseAreaName.Text = diagnoseArea.m_strDiagnoseAreaName;
            m_txtSummary.Text = diagnoseArea.m_strSummary;
        }

        private void m_mthDetailClear()
        {
            m_txtDiagnoseAreaName.Clear();
            m_txtSummary.Clear();
        } 

        #endregion

        #region �¼�ʵ��
        
        private void frmDiagnoseArea_Load(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            //this.m_txtDiagnoseAreaName.Enabled = false;

            //��������
            clsMFZDiagnoseAreaVO[] objDiagnoseAreasArr = null;
            clsTmdDiagnoseAreaSmp.s_object.m_lngFind(out objDiagnoseAreasArr);
            if (objDiagnoseAreasArr == null)
            {
                objDiagnoseAreasArr = new clsMFZDiagnoseAreaVO[0];
            }
            m_lsvDiagnoseArea.Tag = objDiagnoseAreasArr;

            //����б�
            m_mthShowDiagnoseAreaList(objDiagnoseAreasArr);

            Cursor.Current = Cursors.Default;
            this.m_txtDiagnoseAreaName.Focus();
        }

        private void m_cmdNew_Click(object sender, EventArgs e)
        {
            m_txtDiagnoseAreaName.Enabled = true;
            m_txtDiagnoseAreaName.Focus();
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvDiagnoseArea.FocusedItem != null)
            {
                this.m_lsvDiagnoseArea.FocusedItem.Selected = false;
                this.m_lsvDiagnoseArea.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthDetailClear();

            //���ù�꽹��
            //this.m_cmdSave.Focus();

            //����������־
            this.m_blnNewDiagnoseArea = true;
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvDiagnoseArea.FocusedItem == null
             && !this.m_blnNewDiagnoseArea)
                return;
            if (String.IsNullOrEmpty(this.m_txtDiagnoseAreaName.Text))
            {
                MessageBox.Show("�������Ʋ���Ϊ�գ�");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdSave.Enabled = false;

            if (this.m_blnNewDiagnoseArea)
            {//�����ı���
                clsMFZDiagnoseAreaVO objDiagnoseArea = new clsMFZDiagnoseAreaVO();
                objDiagnoseArea.m_strDiagnoseAreaName = this.m_txtDiagnoseAreaName.Text.Trim();
                objDiagnoseArea.m_strSummary = m_txtSummary.Text.Trim();
                long lngRes = clsTmdDiagnoseAreaSmp.s_object.m_lngInsert(objDiagnoseArea);
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewDiagnoseArea = false;
                    //���뵽����
                    clsMFZDiagnoseAreaVO[] objDiagnoseAreas = (clsMFZDiagnoseAreaVO[])this.m_lsvDiagnoseArea.Tag;
                    clsMFZDiagnoseAreaVO[] objDiagnoseAreasNewArr = new clsMFZDiagnoseAreaVO[objDiagnoseAreas.Length + 1];
                    objDiagnoseAreas.CopyTo(objDiagnoseAreasNewArr, 0);
                    objDiagnoseAreasNewArr[objDiagnoseAreasNewArr.Length - 1] = objDiagnoseArea;
                    this.m_lsvDiagnoseArea.Tag = objDiagnoseAreasNewArr;
                    //�������
                    ListViewItem item = new ListViewItem(objDiagnoseArea.m_intDiagnoseAreaID.ToString());

                    item.SubItems.Add(objDiagnoseArea.m_strDiagnoseAreaName);
                    item.SubItems.Add(objDiagnoseArea.m_strSummary);

                    item.Tag = objDiagnoseArea;
                    this.m_lsvDiagnoseArea.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvDiagnoseArea_SelectedIndexChanged(null, null);
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
                this.m_blnNewDiagnoseArea = true;
                this.m_txtDiagnoseAreaName.Focus();
            }
            else
            {//�޸ĵı���
                clsMFZDiagnoseAreaVO objDiagnoseArea = (clsMFZDiagnoseAreaVO)this.m_lsvDiagnoseArea.FocusedItem.Tag;

                clsMFZDiagnoseAreaVO objNewDiagnoseArea = new clsMFZDiagnoseAreaVO();
                objDiagnoseArea.m_mthCopyTo(objNewDiagnoseArea);
                objNewDiagnoseArea.m_strDiagnoseAreaName = this.m_txtDiagnoseAreaName.Text.Trim();
                objNewDiagnoseArea.m_strSummary = this.m_txtSummary.Text.Trim();
                long lngRes = clsTmdDiagnoseAreaSmp.s_object.m_lngUpdate(objNewDiagnoseArea);

                if (lngRes > 0)
                {//�ɹ�
                    objNewDiagnoseArea.m_mthCopyTo(objDiagnoseArea);

                    this.m_lsvDiagnoseArea.FocusedItem.Text = objDiagnoseArea.m_intDiagnoseAreaID.ToString();
                    this.m_lsvDiagnoseArea.FocusedItem.SubItems[1].Text = objDiagnoseArea.m_strDiagnoseAreaName;
                    this.m_lsvDiagnoseArea.FocusedItem.SubItems[2].Text = objDiagnoseArea.m_strSummary;
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
            if (this.m_lsvDiagnoseArea.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdDelete.Enabled = false;

            

            clsMFZDiagnoseAreaVO objDiagnoseArea = (clsMFZDiagnoseAreaVO)this.m_lsvDiagnoseArea.FocusedItem.Tag;
            clsMFZDiagnoseAreaVO objCopy = new clsMFZDiagnoseAreaVO();
            objDiagnoseArea.m_mthCopyTo(objCopy);

            //ɾ������,�ж������µĿ����Ƿ�Ϊ��
            clsMFZRoomVO[] arrRooms=null;
            clsTmdRoomSmp.s_object.m_lngFind(objCopy.m_intDiagnoseAreaID, out arrRooms);
            if (arrRooms.Length > 0)
            {
                this.m_cmdDelete.Enabled = true;
                MessageBox.Show("����ɾ�����ɹ�,�������µ����Ҳ�Ϊ��!");
                return;
            }


            long lngRes = clsTmdDiagnoseAreaSmp.s_object.m_lngDelete(objCopy.m_intDiagnoseAreaID);

            if (lngRes > 0)
            {//�ɹ�
                int intIdx = this.m_lsvDiagnoseArea.FocusedItem.Index;

                this.m_lsvDiagnoseArea.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvDiagnoseArea.Items.Count)
                {
                    this.m_lsvDiagnoseArea.Items[intIdx].Selected = true;
                    this.m_lsvDiagnoseArea.Items[intIdx].Focused = true;
                    this.m_lsvDiagnoseArea_SelectedIndexChanged(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvDiagnoseArea.Items[intIdx - 1].Selected = true;
                    this.m_lsvDiagnoseArea.Items[intIdx - 1].Focused = true;
                    this.m_lsvDiagnoseArea_SelectedIndexChanged(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_lsvDiagnoseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_txtDiagnoseAreaName.Enabled = true;
            if (m_lsvDiagnoseArea.FocusedItem == null)
            {
                return;
            }

            m_blnNewDiagnoseArea = false;
            clsMFZDiagnoseAreaVO diagnoseArea = m_lsvDiagnoseArea.FocusedItem.Tag as clsMFZDiagnoseAreaVO;
            if (diagnoseArea != null)
            {
                m_mthControlsShowVO(diagnoseArea);
                this.m_txtDiagnoseAreaName.SelectAll();
            }
        }

        private void m_lsvDiagnoseArea_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvDiagnoseArea.FocusedItem == null)
            {
                return;
            }

            clsMFZDiagnoseAreaVO area = m_lsvDiagnoseArea.FocusedItem.Tag as clsMFZDiagnoseAreaVO;
            if (area != null)
            {
                frmRoomStation room = new frmRoomStation(area.m_intDiagnoseAreaID);
                room.Show();
            }
        }

        private void m_lsvDiagnoseArea_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            bool isAsc = false;
            ListView lsvTemp = m_lsvDiagnoseArea;
            if (lsvTemp.Sorting == SortOrder.Ascending)
            {
                lsvTemp.Sorting = SortOrder.Descending;
            }
            else 
            {
                lsvTemp.Sorting = SortOrder.Ascending;
                isAsc = true;
            }
            lsvTemp.ListViewItemSorter = new ListViewItemComparer(e.Column, isAsc, lsvTemp);
            lsvTemp.Sort();
        }

        /// <summary>
        /// �������ÿ�ݼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDiagnoseArea_KeyDown(object sender, KeyEventArgs e)
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
        
        #endregion

    }
}