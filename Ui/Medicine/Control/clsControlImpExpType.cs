using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Collections;

namespace com.digitalwave.iCare.gui.HIS
{
    public class clsControlImpExpType : com.digitalwave.GUI_Base.clsController_Base
    {
        private clsDomainControlImpExpType objSvc;

        public clsControlImpExpType()
        {
            objSvc = new clsDomainControlImpExpType();
        }

        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmImpExpType m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmImpExpType)frmMDI_Child_Base_in;
        }
        #endregion

        private ArrayList objImpExp;

        public void m_mthShowData(int SelectIndex)
        {
            clsImpExpType_VO obj = this.m_objViewer.lsvTypes.SelectedItems[0].Tag as clsImpExpType_VO;
            this.m_objViewer.cmdSave.Tag = obj;
            this.m_objViewer.txtName.Text = obj.m_strTypeName;
            this.m_objViewer.lblcode.Text = obj.m_strTypecode;
            this.m_objViewer.cobFlag.SelectedIndex = obj.m_intFlag;
            this.m_objViewer.cobStorgeflag.SelectedIndex = obj.m_intStorgeflag;
        }

        private ListViewItem m_mthCreateItem(clsImpExpType_VO objVO)
        {
            ListViewItem item = new ListViewItem();
            item.Text = objVO.m_strTypecode;
            item.SubItems.Add(objVO.m_strTypeName);

            if (objVO.m_intFlag == 0)
            {
                item.SubItems.Add("���");
            }
            else if (objVO.m_intFlag == 1)
            {
                item.SubItems.Add("����");
            }
            else
            {
                item.SubItems.Add("δ֪");
            }

            //���������ⷿ��־��0-ҩ�⣻1-ҩ�⣻2-ҩ�ⷿ����
            if (objVO.m_intStorgeflag == 0)
            {
                item.SubItems.Add("ҩ��");
            }
            else if (objVO.m_intStorgeflag == 1)
            {
                item.SubItems.Add("ҩ��");
            }
            else if (objVO.m_intStorgeflag == 2)
            {
                item.SubItems.Add("ҩ�ⷿ����");
            }
            else
            {
                item.SubItems.Add("δ֪");
            }
            item.Tag = objVO;
            return item;
        }

        public void m_mthInit()
        {
            this.m_objViewer.lsvTypes.Items.Clear();
            clsImpExpType_VO[] objValues;
            objImpExp = new ArrayList();
            long lngRes = objSvc.m_lngGetAllType(out objValues);

            if (lngRes > 0)
            {
                for (int i1 = 0; objValues != null && i1 < objValues.Length; i1++)
                {
                    this.objImpExp.Add(objValues[i1]);
                    if (objValues[i1].m_intStatus == 0)
                    {
                        continue;
                    }

                    ListViewItem item = this.m_mthCreateItem(objValues[i1]);
                    this.m_objViewer.lsvTypes.Items.Add(item);
                }
            }
        }

        public void m_mthNew()
        {
            this.m_objViewer.txtName.Text = "";
            this.m_objViewer.cobFlag.SelectedIndex = 0;
            this.m_objViewer.cobStorgeflag.SelectedIndex = 0;
            int Maxcode = 0;
            if (objImpExp != null && objImpExp.Count > 0)
            {
                for (int i1 = 0; i1 < objImpExp.Count; i1++)
                {
                    clsImpExpType_VO objTmp = objImpExp[i1] as clsImpExpType_VO;
                    if (Maxcode < int.Parse(objTmp.m_strTypecode))
                    {
                        Maxcode = int.Parse(objTmp.m_strTypecode);
                    }
                }
            }
            this.m_objViewer.lblcode.Text = Convert.ToString(Maxcode + 1);
            this.m_objViewer.cmdSave.Tag = null;
        }

        public void m_mthSave()
        {
            if (this.m_objViewer.txtName.Text.Trim() == "")
            {
                MessageBox.Show(this.m_objViewer, "�������������ơ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.txtName.Focus();
                return;
            }

            if (this.m_objViewer.cobFlag.SelectedIndex == -1)
            {
                MessageBox.Show(this.m_objViewer, "��ѡ�����͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.cobFlag.Focus();
                this.m_objViewer.cobFlag.DroppedDown = true;
                return;
            }

            if (this.m_objViewer.cobStorgeflag.SelectedIndex == -1)
            {
                MessageBox.Show(this.m_objViewer, "��ѡ��ⷿ���͡�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.cobStorgeflag.Focus();
                this.m_objViewer.cobStorgeflag.DroppedDown = true;
                return;
            }

            clsImpExpType_VO objVO = new clsImpExpType_VO();
            objVO.m_strTypecode = this.m_objViewer.lblcode.Text;
            objVO.m_strTypeName = this.m_objViewer.txtName.Text.Trim();
            objVO.m_intFlag = this.m_objViewer.cobFlag.SelectedIndex;
            objVO.m_intStorgeflag = this.m_objViewer.cobStorgeflag.SelectedIndex;
            objVO.m_intStatus = 1;

            if (this.m_objViewer.cmdSave.Tag == null)
            {
                long lngRes = objSvc.m_lngInsertData(objVO);
                if (lngRes > 0)
                {
                    ListViewItem item = this.m_mthCreateItem(objVO);
                    this.m_objViewer.lsvTypes.Items.Add(item);
                    this.objImpExp.Add(objVO);
                    MessageBox.Show(this.m_objViewer, "��ӳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.m_mthNew();
                    this.m_objViewer.lsvTypes.Items[this.m_objViewer.lsvTypes.Items.Count - 1].Selected = true;
                }
            }
            else
            {
                long lngRes = objSvc.m_lngUpdate(objVO, ((clsImpExpType_VO)this.m_objViewer.cmdSave.Tag).m_strTypecode);
                if (lngRes > 0)
                {
                    ListViewItem item = this.m_objViewer.lsvTypes.SelectedItems[0];
                    if (lngRes > 0)
                    {
                        //item = this.m_mthCreateItem(objVO);
                        item.Text = objVO.m_strTypecode;
                        item.SubItems[1].Text = objVO.m_strTypeName;

                        if (objVO.m_intFlag == 0)
                        {
                            item.SubItems[2].Text = "���";
                        }
                        else if (objVO.m_intFlag == 1)
                        {
                            item.SubItems[2].Text = "����";
                        }
                        else
                        {
                            item.SubItems[2].Text = "δ֪";
                        }

                        //���������ⷿ��־��0-ҩ�⣻1-ҩ�⣻2-ҩ�ⷿ����
                        if (objVO.m_intStorgeflag == 0)
                        {
                            item.SubItems[3].Text = "ҩ��";
                        }
                        else if (objVO.m_intStorgeflag == 1)
                        {
                            item.SubItems[3].Text = "ҩ��";
                        }
                        else if (objVO.m_intStorgeflag == 2)
                        {
                            item.SubItems[3].Text = "ҩ�ⷿ����";
                        }
                        else
                        {
                            item.SubItems[3].Text = "δ֪";
                        }
                        item.Tag = objVO;
                        this.m_objViewer.cmdSave.Tag = objVO;
                        if (objImpExp.Contains((clsImpExpType_VO)this.m_objViewer.cmdSave.Tag))
                        {
                            this.m_objViewer.lsvTypes.SelectedItems[0].Tag = objVO;
                            objImpExp.Remove(((clsImpExpType_VO)this.m_objViewer.cmdSave.Tag));
                            objImpExp.Add(objVO);
                        }
                        MessageBox.Show(this.m_objViewer, "�޸ĳɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void m_mthDelete()
        {
            if (this.m_objViewer.cmdSave.Tag == null)
            {
                return;
            }

            clsImpExpType_VO obj = this.m_objViewer.cmdSave.Tag as clsImpExpType_VO;
            int status = 0;
            if (this.m_objViewer.radShowAll.Checked)
            {
                status = 0;
            }
            else if (this.m_objViewer.radShowDel.Checked)
            {
                status = 1;
            }

            long lngRes = objSvc.m_lngUpdateStatus(status, obj.m_strTypecode);
            if (lngRes > 0)
            {
                this.m_objViewer.lsvTypes.SelectedItems[0].Remove();
                for (int i1 = 0; i1 < this.objImpExp.Count; i1++)
                {
                    if (objImpExp.Contains(obj) )
                    {
                        objImpExp.Remove(obj);
                        obj.m_intStatus = status;
                        objImpExp.Add(obj);
                        break;
                    }
                }
                if (status == 1)
                {
                    MessageBox.Show(this.m_objViewer, "��ԭ�ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this.m_objViewer, "ɾ���ɹ���", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void m_mthShow(int Stutas)
        {
            if (this.objImpExp == null)
            {
                return;
            }
            this.m_mthNew();
            if (Stutas == 1)
            {
                this.m_objViewer.lsvTypes.Items.Clear();
                for (int i1 = 0; i1 < objImpExp.Count; i1++)
                {
                    clsImpExpType_VO objTmp = objImpExp[i1] as clsImpExpType_VO;
                    if (objTmp.m_intStatus == 0)
                    {
                        continue;
                    }
                    ListViewItem item = this.m_mthCreateItem(objTmp);
                    this.m_objViewer.lsvTypes.Items.Add(item);
                }
                this.m_objViewer.cmdSave.Enabled = true;
                this.m_objViewer.cmdNew.Enabled = true;
                this.m_objViewer.cmdCancel.Enabled = true;
                this.m_objViewer.cmdDelete.Text = "ɾ��(&D)";
            }
            else if (Stutas == 0)
            {
                this.m_objViewer.lsvTypes.Items.Clear();
                for (int i1 = 0; i1 < objImpExp.Count; i1++)
                {
                    clsImpExpType_VO objTmp = objImpExp[i1] as clsImpExpType_VO;
                    if (objTmp.m_intStatus == 1)
                    {
                        continue;
                    }
                    ListViewItem item = this.m_mthCreateItem(objTmp);
                    this.m_objViewer.lsvTypes.Items.Add(item);
                }
                this.m_objViewer.cmdSave.Enabled = false;
                this.m_objViewer.cmdNew.Enabled = false;
                this.m_objViewer.cmdCancel.Enabled = false;
                this.m_objViewer.cmdDelete.Text = "ȡ��ɾ��";
            }
        }
    }
}
