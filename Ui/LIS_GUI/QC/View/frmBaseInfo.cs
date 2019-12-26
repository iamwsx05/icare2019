using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
    public partial class frmBaseInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region Contruct
        public frmBaseInfo()
        {
            InitializeComponent();
        }
        #endregion

        #region һ������

        #region ��ݼ�����

        private void m_mthShortCutKey(Keys p_eumKeyCode)
        {
            //if (m_tabControl.SelectedIndex == 2)
            //{
            //    if (p_eumKeyCode == Keys.F4)
            //    {
            //        m_cmdCDelete_Click(this.m_cmdCMDelete, EventArgs.Empty);
            //    }
            //}
        }

        #endregion

        private void frmReportQuery_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            m_mthShortCutKey(e.KeyCode);
            base.m_mthSetKeyTab(e);
        }

        #endregion

        #region FormLod
        private void frmBaseInfo_Load(object sender, EventArgs e)
        {
            this.m_mthLoadWorkGroup();
        }
        #endregion

        #region TabpageSelected
        private void m_tabControl_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    if(this.m_lsvWorkGroup.Tag == null)
                        this.m_mthLoadWorkGroup();
                    break;
                case 1:
                    if (this.m_lsvCheckMethod.Tag == null)
                        this.m_mthLoadCheckMethod();
                    break;
                case 2:
                    if (this.m_lsvConcentration.Tag == null)
                        this.m_mthLoadConcentration();
                    break;
                case 3:
                    if (this.m_lsvVendor.Tag == null)
                        this.m_mthLoadVendor();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region CheckMethod

        bool m_blnNewCheckMethod = false;

        private void m_mthLoadCheckMethod()
        {
            Cursor.Current = Cursors.WaitCursor;

            //��������
            clsLisCheckMethodVO[] objMethodsArr = null;
            clsTmdCheckMethodSmp.s_object.m_lngFind(out objMethodsArr);
            if (objMethodsArr == null) 
            {
                objMethodsArr=new clsLisCheckMethodVO[0];
            }
            m_lsvCheckMethod.Tag = objMethodsArr;

            //����б�
            m_mthShowCheckMethodList(objMethodsArr);

            Cursor.Current = Cursors.Default;
        }

        private void m_mthShowCheckMethodList(clsLisCheckMethodVO[] objMethodsArr)
        {
            this.m_lsvCheckMethod.BeginUpdate();//��ʼ�����б�
            this.m_lsvCheckMethod.Items.Clear();
            if (objMethodsArr != null)
            {
                foreach (clsLisCheckMethodVO method in objMethodsArr)
                {
                    ListViewItem item = new ListViewItem(method.m_strName);
                    item.SubItems.Add(method.m_strPycode);
                    item.SubItems.Add(method.m_strWbcode);
                    item.Tag = method;
                    this.m_lsvCheckMethod.Items.Add(item);
                }
            }
            //����״̬��־
            this.m_blnNewCheckMethod = false;
            //�����ϸ
            m_mthCMDetailClear();

            this.m_lsvCheckMethod.EndUpdate();//���������б�
        }

        private void m_lsvCheckMethod_Click(object sender, EventArgs e)
        {
            if (this.m_lsvCheckMethod.FocusedItem == null)
                return;
            //���״̬��־
            this.m_blnNewCheckMethod = false;

            clsLisCheckMethodVO objCheckMethod = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;

            this.m_txtCMName.Text = objCheckMethod.m_strName;
            this.m_txtCMPYCode.Text = objCheckMethod.m_strPycode;
            this.m_txtCMWBCode.Text = objCheckMethod.m_strWbcode;
        }

        //�����ϸ
        private void m_mthCMDetailClear()
        {
            this.m_txtCMName.Clear();
            this.m_txtCMPYCode.Clear();
            this.m_txtCMWBCode.Clear();
        }

        private void m_cmdCMNew_Click(object sender, EventArgs e)
        {
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvCheckMethod.FocusedItem != null)
            {
                this.m_lsvCheckMethod.FocusedItem.Selected = false;
                this.m_lsvCheckMethod.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthCMDetailClear();

            //���ù�꽹��
            this.m_txtCMName.Focus();

            //����������־
            this.m_blnNewCheckMethod = true;
        }

        private void m_cmdCMSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvCheckMethod.FocusedItem == null
              && !this.m_blnNewCheckMethod)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCMSave.Enabled = false;

            if (this.m_blnNewCheckMethod)
            {//�����ı���
                clsLisCheckMethodVO objMethod = new clsLisCheckMethodVO();
                objMethod.m_strName = this.m_txtCMName.Text.Trim();
                objMethod.m_strPycode = this.m_txtCMPYCode.Text.Trim();
                objMethod.m_strWbcode = this.m_txtCMWBCode.Text.Trim();

                long lngRes = clsTmdCheckMethodSmp.s_object.m_lngInsert(objMethod);
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewCheckMethod = false;
                    //���뵽����
                    clsLisCheckMethodVO[] objMethods = (clsLisCheckMethodVO[])this.m_lsvCheckMethod.Tag;
                    clsLisCheckMethodVO[] objMethodsNewArr = new clsLisCheckMethodVO[objMethods.Length + 1];
                    objMethods.CopyTo(objMethodsNewArr, 0);
                    objMethodsNewArr[objMethodsNewArr.Length - 1] = objMethod;
                    this.m_lsvCheckMethod.Tag = objMethodsNewArr;
                    //�������
                    ListViewItem item = new ListViewItem(objMethod.m_strName);

                    item.SubItems.Add(objMethod.m_strPycode);
                    item.SubItems.Add(objMethod.m_strWbcode);

                    item.Tag = objMethod;
                    this.m_lsvCheckMethod.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvCheckMethod_Click(null, null);

                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//�޸ĵı���
                clsLisCheckMethodVO objMethod = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;

                clsLisCheckMethodVO objNewMethod = new clsLisCheckMethodVO();
                objMethod.m_mthCopyTo(objNewMethod);
                objNewMethod.m_strName = this.m_txtCMName.Text.Trim();
                objNewMethod.m_strPycode = this.m_txtCMPYCode.Text.Trim();
                objNewMethod.m_strWbcode = this.m_txtCMWBCode.Text.Trim();

                long lngRes = clsTmdCheckMethodSmp.s_object.m_lngUpdate(objNewMethod);

                if (lngRes > 0)
                {//�ɹ�
                    objNewMethod.m_mthCopyTo(objMethod);

                    this.m_lsvCheckMethod.FocusedItem.Text = objMethod.m_strName;
                    this.m_lsvCheckMethod.FocusedItem.SubItems[1].Text = objMethod.m_strPycode;
                    this.m_lsvCheckMethod.FocusedItem.SubItems[2].Text = objMethod.m_strWbcode;
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdCMSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdCMDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvCheckMethod.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCMDelete.Enabled = false;
            clsLisCheckMethodVO objMethod = (clsLisCheckMethodVO)this.m_lsvCheckMethod.FocusedItem.Tag;
            clsLisCheckMethodVO objCopy = new clsLisCheckMethodVO();
            objMethod.m_mthCopyTo(objCopy);

            long lngRes = clsTmdCheckMethodSmp.s_object.m_lngDelete(objCopy.m_intSeq);

            if (lngRes > 0)
            {//�ɹ�
                int intIdx = this.m_lsvCheckMethod.FocusedItem.Index;

                this.m_lsvCheckMethod.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvCheckMethod.Items.Count)
                {
                    this.m_lsvCheckMethod.Items[intIdx].Selected = true;
                    this.m_lsvCheckMethod.Items[intIdx].Focused = true;
                    this.m_lsvCheckMethod_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvCheckMethod.Items[intIdx - 1].Selected = true;
                    this.m_lsvCheckMethod.Items[intIdx - 1].Focused = true;
                    this.m_lsvCheckMethod_Click(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }
            this.m_cmdCMDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region WorkGroup
        bool m_blnNewWorkGroup = false;

        //�������ݺ�����б�
        private void m_mthLoadWorkGroup()
        {
            Cursor.Current = Cursors.WaitCursor;

            //��������
            clsLisWorkGroupVO[] objGroupArr = null;
            clsTmdWorkGroupSmp.s_object.m_lngFind(out objGroupArr);
            if (objGroupArr == null) 
            {
                objGroupArr=new clsLisWorkGroupVO[0];
            }
            m_lsvWorkGroup.Tag = objGroupArr;

            //����б�
            m_mthShowWorkGroupList(objGroupArr, this.m_chkWGShowDeleted.Checked);

            Cursor.Current = Cursors.Default;
        }

        //����б�
        private void m_mthShowWorkGroupList(clsLisWorkGroupVO[] objGroupArr, bool p_blnDeleted)
        {
            this.m_lsvWorkGroup.BeginUpdate();//��ʼ�����б�
            this.m_lsvWorkGroup.Items.Clear();
            if (objGroupArr != null)
            {
                foreach (clsLisWorkGroupVO group in objGroupArr)
                {
                    //������������Ҫ������
                    if ((p_blnDeleted && (group.m_enmStatus == enmQCStatus.Delete))
                        || (!p_blnDeleted && (group.m_enmStatus == enmQCStatus.Natrural)))
                    {
                        ListViewItem item = new ListViewItem(group.m_strName);
                        item.SubItems.Add(group.m_strSummary);
                        item.Tag = group;

                        this.m_lsvWorkGroup.Items.Add(item);
                    }
                }
            }
            //����״̬��־
            this.m_blnNewWorkGroup = false;
            //�����ϸ
            m_mthWGDetailClear();

            this.m_lsvWorkGroup.EndUpdate();//���������б�
        }
        //�б�ѡ������
        private void m_lsvWorkGroup_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null)
                return;
            //���״̬��־
            this.m_blnNewWorkGroup = false;

            clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;

            this.m_txtWGName.Text = objWorkGroup.m_strName;
            this.m_txtWGSummary.Text = objWorkGroup.m_strSummary;            
        }

        //�����
        private void m_chkWGShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_chkWGShowDeleted.Enabled = false;

            //Ϊ�б����ѡ�����������
            this.m_mthShowWorkGroupList((clsLisWorkGroupVO[])this.m_lsvWorkGroup.Tag, this.m_chkWGShowDeleted.Checked);

            //����������ÿؼ�״̬
            this.m_cmdWGCancelDelete.Visible = this.m_chkWGShowDeleted.Checked;
            this.m_cmdWGNew.Visible = !this.m_chkWGShowDeleted.Checked;
            this.m_cmdWGSave.Visible = !this.m_chkWGShowDeleted.Checked;
            this.m_cmdWGDelete.Visible = !this.m_chkWGShowDeleted.Checked;
            this.m_gpbWorkGroup.Enabled = !this.m_chkWGShowDeleted.Checked;

            m_chkWGShowDeleted.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        //�ָ�
        private void m_cmdWGCancelDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null
                || this.m_lsvWorkGroup.FocusedItem.Tag == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            m_cmdWGCancelDelete.Enabled = false;

            clsLisWorkGroupVO objGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;
            clsLisWorkGroupVO objCopy = new clsLisWorkGroupVO();
            objGroup.m_mthCopyTo(objCopy);//��������һ������
            objCopy.m_enmStatus = enmQCStatus.Natrural;

            //���µ����ݿ�
            long lngRes = clsTmdWorkGroupSmp.s_object.m_lngUpdate(objCopy);

            if (lngRes > 0)
            {//���³ɹ�
                objGroup.m_enmStatus = enmQCStatus.Natrural;
                int intIdx = this.m_lsvWorkGroup.FocusedItem.Index;

                this.m_lsvWorkGroup.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvWorkGroup.Items.Count)
                {
                    this.m_lsvWorkGroup.Items[intIdx].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvWorkGroup.Items[intIdx - 1].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx - 1].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
            }
            else
            {//����ʧ��
                clsCommonDialog.m_mthShowDBError();
            }

            m_cmdWGCancelDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        //����
        private void m_cmdWGNew_Click(object sender, EventArgs e)
        {
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvWorkGroup.FocusedItem != null)
            {
                this.m_lsvWorkGroup.FocusedItem.Selected = false;
                this.m_lsvWorkGroup.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthWGDetailClear();

            //���ù�꽹��
            this.m_txtWGName.Focus();

            //����������־
            this.m_blnNewWorkGroup = true;
        }

        //�����ϸ
        private void m_mthWGDetailClear()
        {
            this.m_txtWGName.Clear();
            this.m_txtWGSummary.Clear();
        }

        //����
        private void m_cmdWGSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null
                && !this.m_blnNewWorkGroup)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGSave.Enabled = false;

            if (this.m_blnNewWorkGroup)
            {//�����ı���
                clsLisWorkGroupVO objGroup = new clsLisWorkGroupVO();
                objGroup.m_enmStatus = enmQCStatus.Natrural;
                objGroup.m_strName = this.m_txtWGName.Text.Trim();
                objGroup.m_strSummary = this.m_txtWGSummary.Text;

                long lngRes = clsTmdWorkGroupSmp.s_object.m_lngInsert(objGroup);
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewWorkGroup = false;
                    //���뵽����
                    clsLisWorkGroupVO[] objGroupArr = (clsLisWorkGroupVO[])this.m_lsvWorkGroup.Tag;
                    clsLisWorkGroupVO[] objGroupNewArr = new clsLisWorkGroupVO[objGroupArr.Length + 1];
                    objGroupArr.CopyTo(objGroupNewArr, 0);
                    objGroupNewArr[objGroupNewArr.Length - 1] = objGroup;
                    this.m_lsvWorkGroup.Tag = objGroupNewArr;
                    //�������
                    ListViewItem item = new ListViewItem(objGroup.m_strName);
                    item.SubItems.Add(objGroup.m_strSummary);
                    item.Tag = objGroup;
                    this.m_lsvWorkGroup.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);

                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//�޸ĵı���
                clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;

                clsLisWorkGroupVO objGroup = new clsLisWorkGroupVO();
                objWorkGroup.m_mthCopyTo(objGroup);
                objGroup.m_strName = this.m_txtWGName.Text.Trim();
                objGroup.m_strSummary = this.m_txtWGSummary.Text;

                long lngRes = clsTmdWorkGroupSmp.s_object.m_lngUpdate(objGroup);

                if (lngRes > 0)
                {//�ɹ�
                    objGroup.m_mthCopyTo(objWorkGroup);
                    this.m_lsvWorkGroup.FocusedItem.Text = objWorkGroup.m_strName;
                    this.m_lsvWorkGroup.FocusedItem.SubItems[1].Text = objWorkGroup.m_strSummary;
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdWGSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdWGDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvWorkGroup.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGDelete.Enabled = false;

            clsLisWorkGroupVO objWorkGroup = (clsLisWorkGroupVO)this.m_lsvWorkGroup.FocusedItem.Tag;
            clsLisWorkGroupVO objCopy = new clsLisWorkGroupVO();
            objWorkGroup.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Delete;
            
            long lngRes = clsTmdWorkGroupSmp.s_object.m_lngUpdate(objCopy);

            if (lngRes > 0)
            {//�ɹ�
                objWorkGroup.m_enmStatus = enmQCStatus.Delete;
                int intIdx = this.m_lsvWorkGroup.FocusedItem.Index;

                this.m_lsvWorkGroup.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvWorkGroup.Items.Count)
                {
                    this.m_lsvWorkGroup.Items[intIdx].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvWorkGroup.Items[intIdx - 1].Selected = true;
                    this.m_lsvWorkGroup.Items[intIdx - 1].Focused = true;
                    this.m_lsvWorkGroup_Click(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdWGDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Concentration

        bool m_blnNewConcentration=false;

        //�������ݺ�����б�
        private void m_mthLoadConcentration()
        {
            Cursor.Current = Cursors.WaitCursor;

            //��������
            clsLisConcentrationVO[] objconcentrationArr = null;
            clsTmdConcentrationSmp.s_object.m_lngFind(out objconcentrationArr);
            if (objconcentrationArr == null) 
            {
                objconcentrationArr=new clsLisConcentrationVO[0];
            }
            m_lsvConcentration.Tag = objconcentrationArr;

            //����б�
            m_mthShowConcentrationList(objconcentrationArr, this.m_chkCShowDeleted.Checked);

            Cursor.Current = Cursors.Default;
        }

        //����б�
        private void m_mthShowConcentrationList(clsLisConcentrationVO[] objconcentrationArr, bool p_blnDeleted)
        {
            this.m_lsvConcentration.BeginUpdate();//��ʼ�����б�
            this.m_lsvConcentration.Items.Clear();
            if (objconcentrationArr != null)
            {
                foreach (clsLisConcentrationVO concentration in objconcentrationArr)
                {
                    //������������Ҫ������
                    if ((p_blnDeleted && (concentration.m_enmStatus == enmQCStatus.Delete))
                        || (!p_blnDeleted && (concentration.m_enmStatus == enmQCStatus.Natrural)))
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text=concentration.m_intSeq.ToString();
                        item.SubItems.Add(concentration.m_strConcentration);
                        item.Tag = concentration;

                        this.m_lsvConcentration.Items.Add(item);
                    }
                }
            }
            //����״̬��־
            this.m_blnNewConcentration = false;
            //�����ϸ
            m_mthCDetailClear();

            this.m_lsvConcentration.EndUpdate();//���������б�
        }
        //�б�ѡ������
        private void m_lsvConcentration_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null)
                return;
            //���״̬��־
            this.m_blnNewConcentration = false;

            clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;

            this.m_txtConcentration.Text = objConcentration.m_strConcentration;
        }
       
        //�����ϸ
        private void m_mthCDetailClear()
        {
            this.m_txtConcentration.Clear();
        }

        private void m_chkCShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            m_chkCShowDeleted.Enabled = false;

            //Ϊ�б����ѡ�����������
            this.m_mthShowConcentrationList((clsLisConcentrationVO[])this.m_lsvConcentration.Tag, this.m_chkCShowDeleted.Checked);

            //����������ÿؼ�״̬
            this.m_cmdCCancelDelete.Visible = this.m_chkCShowDeleted.Checked;
            
            this.m_cmdCNew.Visible = !this.m_chkCShowDeleted.Checked;
            this.m_cmdCSave.Visible = !this.m_chkCShowDeleted.Checked;
            this.m_cmdCDelete.Visible = !this.m_chkCShowDeleted.Checked;
            this.m_txtConcentration.Enabled = !this.m_chkCShowDeleted.Checked;

            m_chkCShowDeleted.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdCCancelDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null
               || this.m_lsvConcentration.FocusedItem.Tag == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            m_cmdCCancelDelete.Enabled = false;

            clsLisConcentrationVO objconcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;
            clsLisConcentrationVO objCopy = new clsLisConcentrationVO();
            objconcentration.m_mthCopyTo(objCopy);//��������һ������
            objCopy.m_enmStatus = enmQCStatus.Natrural;

            //���µ����ݿ�
            long lngRes = clsTmdConcentrationSmp.s_object.m_lngUpdate(objCopy);

            if (lngRes > 0)
            {//���³ɹ�
                objconcentration.m_enmStatus = enmQCStatus.Natrural;
                int intIdx = this.m_lsvConcentration.FocusedItem.Index;

                this.m_lsvConcentration.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvConcentration.Items.Count)
                {
                    this.m_lsvConcentration.Items[intIdx].Selected = true;
                    this.m_lsvConcentration.Items[intIdx].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvConcentration.Items[intIdx - 1].Selected = true;
                    this.m_lsvConcentration.Items[intIdx - 1].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
            }
            else
            {//����ʧ��
                clsCommonDialog.m_mthShowDBError();
            }

            m_cmdCCancelDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdCNew_Click(object sender, EventArgs e)
        {
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvConcentration.FocusedItem != null)
            {
                this.m_lsvConcentration.FocusedItem.Selected = false;
                this.m_lsvConcentration.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthCDetailClear();

            //���ù�꽹��
            this.m_txtConcentration.Focus();

            //����������־
            this.m_blnNewConcentration = true;
        }

        private void m_cmdCSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null
               && !this.m_blnNewConcentration)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdCSave.Enabled = false;

            if (this.m_blnNewConcentration)
            {//�����ı���
                clsLisConcentrationVO objConentration = new clsLisConcentrationVO();
                objConentration.m_enmStatus = enmQCStatus.Natrural;
                objConentration.m_strConcentration = this.m_txtConcentration.Text.Trim();

                long lngRes = clsTmdConcentrationSmp.s_object.m_lngInsert(objConentration);
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewConcentration = false;
                    //���뵽����
                    clsLisConcentrationVO[] objconcentrationArr = (clsLisConcentrationVO[])this.m_lsvConcentration.Tag;
                    clsLisConcentrationVO[] objconcentrationNewArr = new clsLisConcentrationVO[objconcentrationArr.Length + 1];
                    objconcentrationArr.CopyTo(objconcentrationNewArr, 0);
                    objconcentrationNewArr[objconcentrationNewArr.Length - 1] = objConentration;
                    this.m_lsvConcentration.Tag = objconcentrationNewArr;

                    //�������
                    ListViewItem item = new ListViewItem(objConentration.m_strConcentration);
                    item.Text = objConentration.m_intSeq.ToString();
                    item.SubItems.Add(objConentration.m_strConcentration);
                    item.Tag = objConentration;
                    this.m_lsvConcentration.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//�޸ĵı���
                clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;

                clsLisConcentrationVO objNewConcentration = new clsLisConcentrationVO();
                objConcentration.m_mthCopyTo(objNewConcentration);
                objNewConcentration.m_strConcentration = this.m_txtConcentration.Text.Trim();

                long lngRes = clsTmdConcentrationSmp.s_object.m_lngUpdate(objNewConcentration);

                if (lngRes > 0)
                {//�ɹ�
                    objNewConcentration.m_mthCopyTo(objConcentration);
                    this.m_lsvConcentration.FocusedItem.Text = objConcentration.m_intSeq.ToString();
                    this.m_lsvConcentration.FocusedItem.SubItems[1].Text = objConcentration.m_strConcentration;
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdCSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdCDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvConcentration.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdWGDelete.Enabled = false;

            clsLisConcentrationVO objConcentration = (clsLisConcentrationVO)this.m_lsvConcentration.FocusedItem.Tag;
            clsLisConcentrationVO objCopy = new clsLisConcentrationVO();
            objConcentration.m_mthCopyTo(objCopy);
            objCopy.m_enmStatus = enmQCStatus.Delete;

            long lngRes = clsTmdConcentrationSmp.s_object.m_lngUpdate(objCopy);

            if (lngRes > 0)
            {//�ɹ�
                objConcentration.m_enmStatus = enmQCStatus.Delete;
                int intIdx = this.m_lsvConcentration.FocusedItem.Index;

                this.m_lsvConcentration.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvConcentration.Items.Count)
                {
                    this.m_lsvConcentration.Items[intIdx].Selected = true;
                    this.m_lsvConcentration.Items[intIdx].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvConcentration.Items[intIdx - 1].Selected = true;
                    this.m_lsvConcentration.Items[intIdx - 1].Focused = true;
                    this.m_lsvConcentration_Click(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdCDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Vendor

        bool m_blnNewVendor = false;

        private void m_mthLoadVendor()
        {
            Cursor.Current = Cursors.WaitCursor;

            //��������
            clsLisVendorVO[] objVendorArr = null;
            clsTmdVendorSmp.s_object.m_lngFind(out objVendorArr);
            if (objVendorArr == null)
            {
                objVendorArr = new clsLisVendorVO[0];
            }
            m_lsvVendor.Tag = objVendorArr;

            //����б�
            m_mthShowVendorList(objVendorArr);

            Cursor.Current = Cursors.Default;
        }

        private void m_mthShowVendorList(clsLisVendorVO[] objVendorArr)
        {
            this.m_lsvVendor.BeginUpdate();//��ʼ�����б�
            this.m_lsvVendor.Items.Clear();
            if (objVendorArr != null)
            {
                foreach (clsLisVendorVO vendor in objVendorArr)
                {
                    ListViewItem item = new ListViewItem(vendor.m_strVendor);
                    item.SubItems.Add(vendor.m_strId);
                    item.SubItems.Add(vendor.m_strPycode);
                    item.SubItems.Add(vendor.m_strWbcode);
                    item.Tag = vendor;
                    this.m_lsvVendor.Items.Add(item);
                }
            }
            //����״̬��־
            this.m_blnNewVendor = false;
            //�����ϸ
            m_mthVDDetailClear();

            this.m_lsvVendor.EndUpdate();//���������б�
        }

        private void m_lsvVendor_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null)
                return;
            //���״̬��־
            this.m_blnNewVendor = false;

            clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;

            this.m_txtVDName.Text = objVendor.m_strVendor;
            this.m_txtVendorCode.Text = objVendor.m_strId;
            this.m_txtVDPYCode.Text = objVendor.m_strPycode;
            this.m_txtVDWBCode.Text = objVendor.m_strWbcode;
        }

        //�����ϸ
        private void m_mthVDDetailClear()
        {
            this.m_txtVDName.Clear();
            this.m_txtVDPYCode.Clear();
            this.m_txtVDWBCode.Clear();
            this.m_txtVendorCode.Clear();
        }

        private void m_cmdVDDelete_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdVDDelete.Enabled = false;

            clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;
            clsLisVendorVO objCopy = new clsLisVendorVO();
            objVendor.m_mthCopyTo(objCopy);

            long lngRes = clsTmdCheckMethodSmp.s_object.m_lngDelete(objCopy.m_intSeq);

            if (lngRes > 0)
            {//�ɹ�
                int intIdx = this.m_lsvVendor.FocusedItem.Index;

                this.m_lsvVendor.FocusedItem.Remove();

                //�����µľ��н���� ListView ��
                if (intIdx < this.m_lsvVendor.Items.Count)
                {
                    this.m_lsvVendor.Items[intIdx].Selected = true;
                    this.m_lsvVendor.Items[intIdx].Focused = true;
                    this.m_lsvVendor_Click(null, null);
                }
                else if (intIdx - 1 >= 0)
                {
                    this.m_lsvVendor.Items[intIdx - 1].Selected = true;
                    this.m_lsvVendor.Items[intIdx - 1].Focused = true;
                    this.m_lsvVendor_Click(null, null);
                }
            }
            else
            {//ʧ��
                clsCommonDialog.m_mthShowDBError();
            }

            this.m_cmdVDDelete.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void m_cmdVDNew_Click(object sender, EventArgs e)
        {
            //ʹ��ǰListView���н������ʧȥ����
            if (this.m_lsvVendor.FocusedItem != null)
            {
                this.m_lsvVendor.FocusedItem.Selected = false;
                this.m_lsvVendor.FocusedItem.Focused = false;
            }

            //�����ϸ
            m_mthVDDetailClear();

            //���ù�꽹��
            this.m_txtVDName.Focus();

            //����������־
            this.m_blnNewVendor = true;
        }

        private void m_cmdVDSave_Click(object sender, EventArgs e)
        {
            if (this.m_lsvVendor.FocusedItem == null
            && !this.m_blnNewVendor)
                return;
            Cursor.Current = Cursors.WaitCursor;
            this.m_cmdVDSave.Enabled = false;

            if (this.m_blnNewVendor)
            {//�����ı���
                clsLisVendorVO objVendor = new clsLisVendorVO();

                objVendor.m_strVendor = this.m_txtVDName.Text.Trim();
                objVendor.m_strId = this.m_txtVendorCode.Text.Trim();
                objVendor.m_strPycode = this.m_txtVDPYCode.Text.Trim();
                objVendor.m_strWbcode = this.m_txtVDWBCode.Text.Trim();

                long lngRes = clsTmdVendorSmp.s_object.m_lngInsert(objVendor);
                if (lngRes > 0)
                {//�ɹ�
                    //����״̬��־
                    this.m_blnNewVendor = false;
                    //���뵽����
                    clsLisVendorVO[] objVendorArr = (clsLisVendorVO[])this.m_lsvVendor.Tag;
                    clsLisVendorVO[] objVendorNewArr = new clsLisVendorVO[objVendorArr.Length + 1];
                    objVendorArr.CopyTo(objVendorNewArr, 0);
                    objVendorNewArr[objVendorNewArr.Length - 1] = objVendor;
                    this.m_lsvVendor.Tag = objVendorNewArr;
                    //�������
                    ListViewItem item = new ListViewItem(objVendor.m_strVendor);
                    item.SubItems.Add(objVendor.m_strId);
                    item.SubItems.Add(objVendor.m_strPycode);
                    item.SubItems.Add(objVendor.m_strWbcode);

                    item.Tag = objVendor;
                    this.m_lsvVendor.Items.Add(item);
                    item.Selected = true;
                    item.Focused = true;
                    this.m_lsvVendor_Click(null, null);

                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            else
            {//�޸ĵı���
                clsLisVendorVO objVendor = (clsLisVendorVO)this.m_lsvVendor.FocusedItem.Tag;

                clsLisVendorVO objNewVendor = new clsLisVendorVO();
                objVendor.m_mthCopyTo(objNewVendor);
                objNewVendor.m_strVendor = this.m_txtVDName.Text.Trim();
                objNewVendor.m_strId = this.m_txtVendorCode.Text.Trim();
                objNewVendor.m_strPycode = this.m_txtVDPYCode.Text.Trim();
                objNewVendor.m_strWbcode = this.m_txtVDWBCode.Text.Trim();

                long lngRes = clsTmdVendorSmp.s_object.m_lngUpdate(objNewVendor);

                if (lngRes > 0)
                {//�ɹ�
                    objNewVendor.m_mthCopyTo(objVendor);

                    this.m_lsvVendor.FocusedItem.Text = objVendor.m_strVendor;
                    this.m_lsvVendor.FocusedItem.SubItems[1].Text = objVendor.m_strId;
                    this.m_lsvVendor.FocusedItem.SubItems[2].Text = objVendor.m_strPycode;
                    this.m_lsvVendor.FocusedItem.SubItems[3].Text = objVendor.m_strWbcode;
                }
                else
                {//ʧ��
                    clsCommonDialog.m_mthShowDBError();
                }
            }
            this.m_cmdVDSave.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Close
        private void btnExit0_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }

    
}