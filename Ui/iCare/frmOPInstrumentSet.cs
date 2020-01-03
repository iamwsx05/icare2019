using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Text.RegularExpressions;

namespace iCare
{
    /// <summary>
    /// ������е��������Ŀά��
    /// </summary>
    public partial class frmOPInstrumentSet : iCare.iCareBaseForm.frmBaseForm
    {
        #region ȫ�ֱ���
        /// <summary>
        /// �����ֵ����Ŀ
        /// </summary>
        clsEMR_OPInstrument_Dict[] m_objAllItems = null;
        /// <summary>
        /// �������ֵ����Ŀ
        /// </summary>
        clsEMR_OPInstrument_Dict[] m_objActiveItems = null;
        /// <summary>
        /// ��ǰ���޸���Ŀ
        /// </summary>
        clsEMR_OPInstrument_Dict m_objCurrentItem = null;
        /// <summary>
        /// �����ֵ����Ŀ
        /// </summary>
        DataTable m_dtbAllItems = new DataTable();
        /// <summary>
        /// �������ֵ����Ŀ
        /// </summary>
        DataTable m_dtbActiveItems = new DataTable();
        #endregion

        #region ���캯��
        public frmOPInstrumentSet()
        {
            InitializeComponent();

            m_dtbAllItems.Columns.Add("Name");
            m_dtbAllItems.Columns.Add("Index");

            m_dtbActiveItems.Columns.Add("OrderID");
            m_dtbActiveItems.Columns.Add("Name");
        } 
        #endregion

        #region �¼�
        #region ͣ����Ŀ
        private void m_miDeActiveItem_Click(object sender, EventArgs e)
        {
            if (m_lsvActiveItem.SelectedItems.Count <= 0)
                return;

            long lngRes = 0;
            clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
            ListViewItem[] selectedItem = new ListViewItem[m_lsvActiveItem.SelectedItems.Count];
            for (int i = 0; i < m_lsvActiveItem.SelectedItems.Count; i++)
            {
                clsEMR_OPInstrument_Dict objDict = m_lsvActiveItem.SelectedItems[i].Tag as clsEMR_OPInstrument_Dict;
                if (objDict == null)
                    continue;
                lngRes = objDomain.m_lngDeActiveItemFromDict(objDict.m_intOPInstrumentID);

                if (lngRes <= 0)
                {
                    clsPublicFunction.ShowInformationMessageBox("�޸�ʧ��");
                    return;
                }
                selectedItem[i] = m_lsvActiveItem.SelectedItems[i];
            }

            for (int i = 0; i < selectedItem.Length; i++)
            {
                m_lsvActiveItem.Items.Remove(selectedItem[i]);
            }

            for (int i = 0; i < m_lsvActiveItem.Items.Count; i++)
            {
                clsEMR_OPInstrument_Dict objDict = m_lsvActiveItem.Items[i].Tag as clsEMR_OPInstrument_Dict;
                objDict.m_intOrderID = i + 1;
                m_lsvActiveItem.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
            m_mthLoadAllItems();

            m_cmdSaveModify_Click(null, null);
        } 
        #endregion

        #region ������Ŀ
        private void m_miActiveItem_Click(object sender, EventArgs e)
        {
            if (m_lsvDict.SelectedItems.Count <= 0)
                return;

            long lngRes = 0;
            clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
            for (int i = 0; i < m_lsvDict.SelectedItems.Count; i++)
            {
                int intIdx = m_lsvDict.SelectedItems[i].Index;
                clsEMR_OPInstrument_Dict objDict = m_lsvDict.SelectedItems[i].Tag as clsEMR_OPInstrument_Dict;
                if (objDict == null)
                    continue;
                lngRes = objDomain.m_lngActiveItemFromDict(objDict.m_intOPInstrumentID);

                if (lngRes <= 0)
                {
                    clsPublicFunction.ShowInformationMessageBox("�޸�ʧ��");
                    return;
                }

                ListViewItem ActiveItem = new ListViewItem((m_lsvActiveItem.Items.Count + 1).ToString());
                ActiveItem.SubItems.Add(objDict.m_strOPInstrumentName);
                ActiveItem.Tag = objDict;
                m_lsvActiveItem.Items.Add(ActiveItem);
            }

            m_mthLoadAllItems();
            m_cmdSaveModify_Click(null, null);
        } 
        #endregion

        #region Load����
        private void frmOPInstrumentSet_Load(object sender, EventArgs e)
        {
            m_mthLoadAllItems();
            m_mthLoadActiveItems();
        } 
        #endregion

        #region �����ֵ����Ŀ
        private void m_cmdSaveItem_Click(object sender, EventArgs e)
        {
            if (m_txtItem.Text.Trim() == string.Empty)
            {
                clsPublicFunction.ShowInformationMessageBox("��������������е���������ƣ�");
                return;
            }
            clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
            long lngRes = 0;

            int intInstrumentID = -1;
            lngRes = objDomain.m_lngCheckSameItemID(m_txtItem.Text.Trim(), out intInstrumentID);

            if (m_objCurrentItem == null)
            {
                if (intInstrumentID > 0)
                {
                    clsPublicFunction.ShowInformationMessageBox("�Ѵ��ڸ���Ŀ��");
                    return;
                }
                lngRes = objDomain.m_lngAddNewToDict(m_txtItem.Text.Trim());
            }
            else
            {
                if (intInstrumentID > 0 && intInstrumentID != m_objCurrentItem.m_intOPInstrumentID)
                {
                    clsPublicFunction.ShowInformationMessageBox("�Ѵ��ڸ���Ŀ��");
                    return;
                }
                lngRes = objDomain.m_lngModifyToDisc(m_objCurrentItem.m_intOPInstrumentID, m_txtItem.Text.Trim());
            }

            if (lngRes > 0)
            {
                m_mthLoadAllItems();

                int intIdx = m_intGetAllItemsRowNum(m_txtItem.Text.Trim());
                if (intIdx >= 0)
                {
                    m_lsvDict.Items[intIdx].EnsureVisible();
                }
                m_objCurrentItem = null;
                m_txtItem.Text = string.Empty;
                m_cmdSaveItem.Text = "������Ŀ";
            }
        } 
        #endregion

        #region ����������Ŀ˳��
        private void m_cmdSaveModify_Click(object sender, EventArgs e)
        {
            if (m_lsvActiveItem.Items.Count <= 0)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
                clsEMR_OPInstrument_Dict objDict = null;
                long lngRes = 0;
                for (int i = 0; i < m_lsvActiveItem.Items.Count; i++)
                {
                    objDict = m_lsvActiveItem.Items[i].Tag as clsEMR_OPInstrument_Dict;
                    if (objDict != null)
                    {
                        if (objDict.m_intOrderID == i)
                            continue;
                        lngRes = objDomain.m_lngUpdateOrderID(objDict.m_intOPInstrumentID, i);
                    }
                }
                if (lngRes > 0)
                {
                    m_mthLoadActiveItems();
                    clsPublicFunction.ShowInformationMessageBox("�޸ĳɹ���");
                }
                else
                {
                    clsPublicFunction.ShowInformationMessageBox("�޸�ʧ�ܣ�");
                }
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox("�������´���\r\n" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region ѡ�����������Ŀ�����ƶ�
        private void m_cmdUp_Click(object sender, EventArgs e)
        {
            if (m_lsvActiveItem.SelectedItems.Count <= 0)
                return;

            ListViewItem[] lsiArr = new ListViewItem[m_lsvActiveItem.SelectedItems.Count];
            for (int i = 0; i < m_lsvActiveItem.SelectedItems.Count; i++)
            {
                int idx = m_lsvActiveItem.SelectedItems[i].Index;
                lsiArr[i] = m_lsvActiveItem.Items[idx];
            }

            for (int i = 0; i < lsiArr.Length; i++)
            {
                int idx = lsiArr[i].Index;
                if (idx > 0)
                {
                    ListViewItem itemLast = m_lsvActiveItem.Items[idx - 1];
                    ListViewItem itemCurrent = lsiArr[i].Clone() as ListViewItem;
                    m_lsvActiveItem.Items[idx - 1] = itemCurrent;
                    m_lsvActiveItem.Items[idx] = itemLast;
                    m_lsvActiveItem.Focus();
                    m_lsvActiveItem.Items[idx - 1].Selected = true;
                    m_lsvActiveItem.Items[idx - 1].EnsureVisible();
                }
            }

            m_mthReSetAcitveItemOrderID();
        } 
        #endregion

        #region ѡ�����������Ŀ�����ƶ�
        private void m_cmdDown_Click(object sender, EventArgs e)
        {
            if (m_lsvActiveItem.SelectedItems.Count <= 0)
                return;

            ListViewItem[] lsiArr = new ListViewItem[m_lsvActiveItem.SelectedItems.Count];
            for (int i = 0; i < m_lsvActiveItem.SelectedItems.Count; i++)
            {
                int idx = m_lsvActiveItem.SelectedItems[i].Index;
                lsiArr[i] = m_lsvActiveItem.Items[idx];
            }

            for (int i = lsiArr.Length - 1; i >= 0; i--)
            {
                int idx = lsiArr[i].Index;
                if (idx < m_lsvActiveItem.Items.Count - 1)
                {
                    ListViewItem itemNext = m_lsvActiveItem.Items[idx + 1];
                    ListViewItem itemCurrent = lsiArr[i].Clone() as ListViewItem;
                    m_lsvActiveItem.Items[idx + 1] = itemCurrent;
                    m_lsvActiveItem.Items[idx] = itemNext;
                    m_lsvActiveItem.Focus();
                    m_lsvActiveItem.Items[idx + 1].Selected = true;
                    m_lsvActiveItem.Items[idx + 1].EnsureVisible();
                }
            }

            m_mthReSetAcitveItemOrderID();
        } 
        #endregion

        #region ͨ����Ŀ��������������Ŀ
        private void m_txtSearchDict_TextChanged(object sender, EventArgs e)
        {
            int intIdx = m_intGetAllItemsRowNum(m_txtSearchDict.Text.Trim());
            if (intIdx >= 0)
            {
                m_lsvDict.Focus();
                m_lsvDict.Items[intIdx].EnsureVisible();
                m_lsvDict.Items[intIdx].Selected = true;
            }
        } 
        #endregion

        #region ͨ����Ŀ����������������Ŀ
        private void m_txtSearchActiveItem_TextChanged(object sender, EventArgs e)
        {
            int intIdx = m_intGetActiveItemsRowNum(m_txtSearchActiveItem.Text.Trim());
            if (intIdx >= 0)
            {
                m_lsvActiveItem.Focus();
                m_lsvActiveItem.Items[intIdx].EnsureVisible();
                m_lsvActiveItem.Items[intIdx].Selected = true;
            }
        } 
        #endregion

        #region ˫��ѡ����Ŀ���޸�
        private void m_lsvDict_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvDict.Items.Count <= 0 || m_lsvDict.SelectedItems.Count != 1)
                return;

            m_objCurrentItem = m_lsvDict.SelectedItems[0].Tag as clsEMR_OPInstrument_Dict;
            if (m_objCurrentItem != null)
            {
                m_cmdSaveItem.Text = "�޸���Ŀ";
                m_txtItem.Text = m_objCurrentItem.m_strOPInstrumentName;
            }
        } 
        #endregion

        #region �����ֵ����Ŀ
        private void m_txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_cmdSaveItem_Click(sender, null);
            }
        } 
        #endregion

        #region �����ƶ�ѡ�����������Ŀ��ָ��λ��
        private void m_txtSpecifyLineNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_txtSpecifyLineNum.Text.Trim() == string.Empty)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                string RegexText = @"^[1-9]\d*|0$";
                if (!Regex.IsMatch(m_txtSpecifyLineNum.Text.Trim(), RegexText))
                {
                    clsPublicFunction.ShowInformationMessageBox("������Ǹ�������");
                    return;
                }

                int intSpecifyIdx = Convert.ToInt32(m_txtSpecifyLineNum.Text.Trim());

                if (intSpecifyIdx > 0)
                {
                    intSpecifyIdx -= 1;
                }

                ListViewItem[] lsiArr = new ListViewItem[m_lsvActiveItem.SelectedItems.Count];
                int[] idxArr = new int[m_lsvActiveItem.SelectedItems.Count];
                for (int i = 0; i < m_lsvActiveItem.SelectedItems.Count; i++)
                {
                    int idx = m_lsvActiveItem.SelectedItems[i].Index;
                    lsiArr[i] = m_lsvActiveItem.Items[idx];
                    idxArr[i] = idx;
                }

                for (int i = 0; i < lsiArr.Length; i++)
                {
                    if (idxArr[i] == intSpecifyIdx)
                        continue;

                    m_lsvActiveItem.Items.RemoveAt(idxArr[i]);
                    m_lsvActiveItem.Items.Insert(intSpecifyIdx + i, lsiArr[i]);
                    m_lsvActiveItem.Items[intSpecifyIdx + i].EnsureVisible();
                }

                m_mthReSetAcitveItemOrderID();
            }
        }  
        #endregion
        #endregion

        #region ����
        #region ��ѯ����ʾ�ֵ��������Ŀ
        /// <summary>
        /// ��ѯ����ʾ�ֵ��������Ŀ
        /// </summary>
        private void m_mthLoadAllItems()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_lsvDict.Items.Clear();
                clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
                long lngRes = objDomain.m_lngGetAllItemsFromDict(out m_objAllItems);

                if (m_objAllItems == null || m_objAllItems.Length <= 0)
                    return;

                m_lsvDict.BeginUpdate();
                m_dtbAllItems.BeginLoadData();
                for (int i = 0; i < m_objAllItems.Length; i++)
                {
                    ListViewItem item = new ListViewItem(m_objAllItems[i].m_strOPInstrumentName);
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add(m_strGetActiveDesc(m_objAllItems[i].m_intStatus),
                        m_clrGetActiveDesc(m_objAllItems[i].m_intStatus), m_lsvDict.BackColor, m_lsvDict.Font);
                    item.Tag = m_objAllItems[i];
                    m_lsvDict.Items.Add(item);
                    object[] objRow = new object[] { m_objAllItems[i].m_strOPInstrumentName, i};
                    m_dtbAllItems.LoadDataRow(objRow, true);
                }
                m_lsvDict.EndUpdate();
                m_dtbAllItems.EndLoadData();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox("�������´���\r\n" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region ��ȡʹ��״��˵��
        /// <summary>
        /// ��ȡʹ��״��˵��
        /// </summary>
        /// <param name="p_intStatus">״̬</param>
        /// <returns></returns>
        private string m_strGetActiveDesc(int p_intStatus)
        {
            switch (p_intStatus)
            {
                case 0:
                    return "������";
                case 1:
                    return "��ͣ��";
                default:
                    return "��ͣ��";
            }
        } 
        #endregion

        #region ��ȡʹ��״����ɫ��ʾ
        /// <summary>
        /// ��ȡʹ��״����ɫ��ʾ
        /// </summary>
        /// <param name="p_intStatus">״̬</param>
        /// <returns></returns>
        private Color m_clrGetActiveDesc(int p_intStatus)
        {
            switch (p_intStatus)
            {
                case 0:
                    return Color.Blue;
                case 1:
                    return Color.Red;
                default:
                    return Color.Red;
            }
        } 
        #endregion

        #region ��ѯ����ʾ�ֵ����������Ŀ
        /// <summary>
        /// ��ѯ����ʾ�ֵ����������Ŀ
        /// </summary>
        private void m_mthLoadActiveItems()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_lsvActiveItem.Items.Clear();

                clsEMR_OPInstrumentDomain objDomain = new clsEMR_OPInstrumentDomain();
                long lngRes = objDomain.m_lngGetActiveItemsFromDict(out m_objActiveItems);

                if (m_objActiveItems == null || m_objActiveItems.Length <= 0)
                    return;

                m_lsvActiveItem.BeginUpdate();
                m_dtbActiveItems.BeginLoadData();
                for (int i = 0; i < m_objActiveItems.Length; i++)
                {
                    int intOrder = m_objActiveItems[i].m_intOrderID == -1 ? i + 1 : m_objActiveItems[i].m_intOrderID + 1;
                    ListViewItem item = new ListViewItem(intOrder.ToString());
                    item.SubItems.Add(m_objActiveItems[i].m_strOPInstrumentName);
                    item.Tag = m_objActiveItems[i];
                    m_lsvActiveItem.Items.Add(item);
                    object[] objItem = new object[] { m_objActiveItems[i].m_intOrderID, m_objActiveItems[i].m_strOPInstrumentName };
                    m_dtbActiveItems.LoadDataRow(objItem,true);
                }
                m_lsvActiveItem.EndUpdate();
                m_dtbActiveItems.EndLoadData();
            }
            catch (Exception ex)
            {
                clsPublicFunction.ShowInformationMessageBox("�������´���\r\n" + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region �����趨��������Ŀ˳��
        /// <summary>
        /// �����趨��������Ŀ˳��
        /// </summary>
        private void m_mthReSetAcitveItemOrderID()
        {
            for (int i = 0; i < m_lsvActiveItem.Items.Count; i++)
            {
                m_lsvActiveItem.Items[i].SubItems[0].Text = (i + 1).ToString();
                clsEMR_OPInstrument_Dict objDict = m_lsvActiveItem.Items[i].Tag as clsEMR_OPInstrument_Dict;
                if (objDict != null)
                {
                    objDict.m_intOrderID = i + 1;
                }
            }
        }  
        #endregion

        #region ������Ŀ���Ʒ����ֵ��������Ŀ�к�
        /// <summary>
        /// ������Ŀ���Ʒ����ֵ��������Ŀ�к�
        /// </summary>
        /// <param name="p_strInstrumentName">��Ŀ����</param>
        /// <returns></returns>
        private int m_intGetAllItemsRowNum(string p_strInstrumentName)
        {
            if (m_dtbAllItems == null || m_dtbAllItems.Rows.Count <= 0 || p_strInstrumentName == null)
                return -1;

            int intIdex = -1;
            DataView dtvItems = m_dtbAllItems.DefaultView;

            dtvItems.RowFilter = @"Name like '" + p_strInstrumentName.Trim() + "%'";

            if (dtvItems != null)
            {
                intIdex = Convert.ToInt32(dtvItems[0][1]);
            }
            return intIdex;
        } 
        #endregion

        #region ������Ŀ���Ʒ�����������Ŀ�к�
        /// <summary>
        /// ������Ŀ���Ʒ�����������Ŀ�к�
        /// </summary>
        /// <param name="p_strInstrumentName">��Ŀ����</param>
        /// <returns></returns>
        private int m_intGetActiveItemsRowNum(string p_strInstrumentName)
        {
            if (m_dtbActiveItems == null || m_dtbActiveItems.Rows.Count <= 0 || p_strInstrumentName == null)
                return -1;

            int intIdex = -1;
            DataView dtvItems = m_dtbActiveItems.DefaultView;

            dtvItems.RowFilter = @"Name like '" + p_strInstrumentName.Trim() + "%'";

            if (dtvItems != null)
            {
                intIdex = Convert.ToInt32(dtvItems[0][0]);
            }
            return intIdex;
        } 
        #endregion
        #endregion
    }
}