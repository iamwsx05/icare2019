using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// �÷���Ŀ�༭	����	2005-04-06	
    /// </summary>
    public class clsControlUsaEdit : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        clsDcl_OPCharge m_objManage;
        /// <summary>
        /// ������ID
        /// </summary>
        public string m_strOperatorID;
        #endregion
        #region ���ô������
        com.digitalwave.iCare.gui.HIS.frmUsaEdit m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmUsaEdit)frmMDI_Child_Base_in;
        }
        #endregion
        #region ���캯��
        public clsControlUsaEdit()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            m_objManage = new clsDcl_OPCharge();
        }
        #endregion
        #region ���
        /// <summary>
        /// �������
        /// </summary>
        public void m_ClearInput()
        {
            //��Ŀ����
            m_objViewer.m_txtItem.Text = "";
            m_objViewer.m_txtItem.Tag = null;
            //��������
            m_objViewer.m_cboContinueUseType.SelectedIndex = 0;
            m_objViewer.m_cboContinueUseType.Tag = null;
            //��������
            m_objViewer.m_txtCLINICQTY.Text = "";
            //������������
            m_objViewer.m_cboCLINICTYPE.SelectedIndex = 0;
            //�����ܼ�
            m_objViewer.m_txtCLINICTOTALPRICE.Text = "";
            m_objViewer.m_lblClinicUnit.Text = "";	//�洢���ﵥλ
            m_objViewer.m_lblClinicUnit.Tag = null;	//�洢����������λ
            //סԺ����
            m_objViewer.m_txtBIHQTY.Text = "";
            //סԺ��������
            m_objViewer.m_cboBIHTYPE.SelectedIndex = 0;
            //סԺ�ܼ�
            m_objViewer.m_txtBIHOTALPRICE.Text = "";
            m_objViewer.m_lblBihUnit.Text = "";	//�洢סԺ��λ
            m_objViewer.m_lblBihUnit.Tag = null;	//�洢סԺ������λ
            //��Ŀ����
            m_objViewer.m_txtItemType.Text = "";
            //��Ŀ���
            m_objViewer.m_txtItemSpec.Text = "";
            //����
            m_objViewer.m_txtDOSAGE_DEC.Text = "";		//�洢����+������λ
            m_objViewer.m_txtDOSAGE_DEC.Tag = null;		//�洢����
            m_objViewer.m_lblSaveDosageUnit.Tag = null;	//�洢������λ

            //�۸�
            m_objViewer.m_txtItemPrice.Text = "";
        }
        /// <summary>
        /// ��ձ���
        /// </summary>
        private void ClearVar()
        {
            //��ձ���
            string strUsageID = m_objViewer.m_objBridgeForUsaEdit.m_strUsageID;
            m_objViewer.m_objBridgeForUsaEdit = new clsBridgeForUsaEdit();
            m_objViewer.m_objBridgeForUsaEdit.m_strUsageID = strUsageID;
            //����״̬	{0������(Ĭ��)��1���޸ģ�}
            m_objViewer.m_intOperateState = 0;
            //�������״̬	{0��ֱ�ӹر�(Ĭ��)��1�������ɹ���2������ʧ�ܣ�}
            m_objViewer.m_intResultState = 0;
            //ɾ����ť
            m_objViewer.m_cmdDel.Enabled = false;
        }
        #endregion

        #region �����¼�
        public void m_FromLoad()
        {
            m_ClearInput();
        }

        #endregion
        #region ��ť�¼�
        public void m_Save()
        {
            if (isnoqty == true)
            {
                if (MessageBox.Show(this.m_objViewer.m_txtItem.Text.Trim() + "��ȱҩ��ͣ��ҩ�Ƿ�Ҫ������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (blnSave(false))
                    {
                        m_objViewer.m_intResultState = 1;
                        m_objViewer.Close();
                    }
                }
                else
                {
                    m_objViewer.m_txtItem.Focus();
                }
            }
            else
            {
                if (blnSave(false))
                {
                    m_objViewer.m_intResultState = 1;
                    m_objViewer.Close();
                }
            }

        }
        public void m_SaveAdd()
        {
            if (isnoqty == true)
            {
                if (MessageBox.Show(this.m_objViewer.m_txtItem.Text.Trim() + "��ȱҩ��ͣ��ҩ�Ƿ�Ҫ������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    m_objViewer.m_txtItem.Focus();
                    return;
                }
            }
            if (blnSave(false))
            {
                m_ClearInput();
                ClearVar();
            }
        }
        /// <summary>
        /// �����¼ӵ���ҩ�÷�������Ŀ
        /// </summary>
        public void m_mthCMUsageSaveAdd()
        {
            if (isnoqty == true)
            {
                if (MessageBox.Show(this.m_objViewer.m_txtItem.Text.Trim() + "��ȱҩ��ͣ��ҩ�Ƿ�Ҫ������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    m_objViewer.m_txtItem.Focus();
                    return;
                }
            }
            if (this.m_mthblnSave(false))
            {
                m_ClearInput();
                ClearVar();
            }
        }
        /// <summary>
        /// ������ҩ�÷�������Ŀ
        /// </summary>
        public void m_mthCMUsageSave()
        {
            if (isnoqty == true)
            {
                if (MessageBox.Show(this.m_objViewer.m_txtItem.Text.Trim() + "��ȱҩ��ͣ��ҩ�Ƿ�Ҫ������", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (blnSave(false))
                    {
                        m_objViewer.m_intResultState = 1;
                        m_objViewer.Close();
                    }
                }
                else
                {
                    m_objViewer.m_txtItem.Focus();
                }
            }
            else
            {
                if (m_mthblnSave(false))
                {
                    m_objViewer.m_intResultState = 1;
                    m_objViewer.Close();
                }
            }

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_blnIsAddNew">�Ƿ񱣴�����</param>
        private bool blnSave(bool p_blnIsAddNew)
        {
            //��ȡ����
            clsBridgeForUsaEdit objItem;
            GetObjectFromControl(out objItem);
            //��֤����
            if (!CheckObjectForSave(objItem)) return false;
            //����
            long lngRes = 0;
            string strRecordID = "";
            clsChargeItemUsageGroup_VO objItem1 = new clsChargeItemUsageGroup_VO();
            objItem1 = objItem;
            if (m_objViewer.m_intOperateState != 1)
            {//����
                lngRes = new clsDomainControl_ChargeItem().m_lngDoAddNewChargeItemUsageGroup(out strRecordID, objItem1);
            }
            else
            {//�޸�
                if (objItem1.m_strTOTALPRICE.Trim() != objItem1.m_strItemID.Trim())
                {
                    if (MessageBox.Show("�Ƿ񽫰������÷�����ͬ��Ŀ����ΪЩ��Ŀ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objItem1.m_intFlag = 1;
                    }
                }

                lngRes = new clsDomainControl_ChargeItem().m_lngDoModifyChargeItemUsageGroup(objItem1);
            }
            if (lngRes > 0)
            {
                MessageBox.Show(m_objViewer, "�����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(m_objViewer, "����ʧ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return (lngRes > 0) ? (true) : (false);
        }
        public void m_Del()
        {
            if (m_objViewer.m_intOperateState != 1) return;
            clsChargeItemUsageGroup_VO objItem = new clsChargeItemUsageGroup_VO();
            objItem = m_objViewer.m_objBridgeForUsaEdit;
            if (MessageBox.Show("ȷ��ɾ��������?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            objItem.m_intFlag = 0;
            if (MessageBox.Show("�Ƿ�ɾ������ͬ���Ĵ���Ŀ?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                objItem.m_intFlag = 1;
            }
            long lngRes = new clsDomainControl_ChargeItem().m_lngDelUsageGroupByID(objItem);
            if (lngRes > 0)
            {
                MessageBox.Show(m_objViewer, "�����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                m_objViewer.m_intResultState = 1;
                m_objViewer.Close();
            }
            else
            {
                MessageBox.Show(m_objViewer, "����ʧ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// ɾ����ҩ�÷�������Ŀ
        /// </summary>
        public void m_mthDel()
        {
            if (m_objViewer.m_intOperateState != 1) return;
            clsChargeItemUsageGroup_VO objItem = new clsChargeItemUsageGroup_VO();
            objItem = m_objViewer.m_objBridgeForUsaEdit;
            if (MessageBox.Show("ȷ��ɾ��������?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            objItem.m_intFlag = 0;
            if (MessageBox.Show("�Ƿ�ɾ������ͬ���Ĵ���Ŀ?", "��ʾ��!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                objItem.m_intFlag = 1;
            }
            long lngRes = new clsDomainControl_ChargeItem().m_lngDelCMUsageGroupByID(objItem);
            if (lngRes > 0)
            {
                MessageBox.Show(m_objViewer, "�����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                m_objViewer.m_intResultState = 1;
                m_objViewer.Close();
            }
            else
            {
                MessageBox.Show(m_objViewer, "����ʧ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region �ı��¼�
        public void m_TextItemInitListView(System.Windows.Forms.ListView lvwList)
        {
            lvwList.Width = 540;
            lvwList.HeaderStyle = ColumnHeaderStyle.Clickable;
            //�����ͷ
            lvwList.Columns.Add("����", 60, HorizontalAlignment.Left);
            lvwList.Columns.Add("��Ŀ����", 150, HorizontalAlignment.Left);
            lvwList.Columns.Add("��Ŀ����", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("��Ŀ���", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("����", 70, HorizontalAlignment.Left);
            lvwList.Columns.Add("����", 70, HorizontalAlignment.Right);
        }
        public void m_TextItemFindItem(string strFindCode, System.Windows.Forms.ListView lvwList)
        {
            lvwList.Items.Clear();
            System.Data.DataTable dt = null;
            long longRes = m_objManage.m_mthFindMedicineByID(strFindCode, out dt, false);
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show(m_objViewer, "û����Ҫ����Ŀ,������������������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //����
            ListViewItem lsvItem = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //��Ŀ����
                lsvItem = new ListViewItem(dt.Rows[i]["ITEMCODE_VCHR"].ToString().Trim());
                //��Ŀ����
                lsvItem.SubItems.Add(dt.Rows[i]["ITEMNAME_VCHR"].ToString().Trim());
                //��Ŀ����
                lsvItem.SubItems.Add(strSwitchTypeToName(dt.Rows[i]["ITEMCATID_CHR"].ToString().Trim()));
                //��Ŀ���
                lsvItem.SubItems.Add(dt.Rows[i]["ITEMSPEC_VCHR"].ToString().Trim());
                //����
                lsvItem.SubItems.Add(dt.Rows[i]["DOSAGE_DEC"].ToString().Trim() + dt.Rows[i]["DOSAGEUNIT_CHR"].ToString().Trim());
                //����
                lsvItem.SubItems.Add(dt.Rows[i]["ITEMPRICE_MNY"].ToString().Trim());

                lsvItem.Tag = ((dt.Rows[i]) as DataRow);//dt.Rows[i]["ITEMID_CHR"].ToString().Trim();
                lvwList.Items.Add(lsvItem);
                //try
                //{
                //    if(int.Parse(dt.Rows[i]["NOQTYFLAG_INT"].ToString())==1&&int.Parse(dt.Rows[i]["ITEMSRCTYPE_INT"].ToString())==1)
                //    {
                //        lvwList.Items[lvwList.Items.Count-1].ForeColor=System.Drawing.Color.Red;
                //    }
                //}
                //catch
                //{
                //}
            }
        }
        public void m_TextItemSelectItem(System.Windows.Forms.ListViewItem lviSelected)
        {
            if (lviSelected != null)
            {
                FillTextFromDataRow(lviSelected.Tag as DataRow);
                //������
                //				m_mthCalMoney();
                //����ת��
                SendKeys.SendWait("{Tab}");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ı���	����DataRow
        /// </summary>
        /// <param name="p_dRow"></param>
        public bool isnoqty = true;
        private void FillTextFromDataRow(DataRow p_dRow)
        {
            if (p_dRow == null) return;
            try
            {

                if (this.m_objViewer.CurrentItem != null && this.m_objViewer.CurrentItem.IndexOf(p_dRow["ITEMID_CHR"].ToString().Trim()) > -1)
                {
                    MessageBox.Show("�Բ�����ѡ��Ŀ�Ѿ����ڣ������ظ�ѡ��");
                    return;
                }
                //��Ŀ����
                m_objViewer.m_txtItem.Text = p_dRow["ITEMNAME_VCHR"].ToString().Trim();
                m_objViewer.m_txtItem.Tag = p_dRow["ITEMID_CHR"].ToString().Trim();
                //��Ŀ����
                m_objViewer.m_txtItemType.Text = strSwitchTypeToName(p_dRow["ITEMCATID_CHR"].ToString());
                //��Ŀ���
                m_objViewer.m_txtItemSpec.Text = p_dRow["ITEMSPEC_VCHR"].ToString().Trim();
                //����
                m_objViewer.m_txtDOSAGE_DEC.Text = p_dRow["DOSAGE_DEC"].ToString().Trim() + p_dRow["DOSAGEUNIT_CHR"].ToString().Trim();
                m_objViewer.m_txtDOSAGE_DEC.Tag = p_dRow["DOSAGE_DEC"].ToString().Trim();
                m_objViewer.m_lblSaveDosageUnit.Tag = p_dRow["DOSAGEUNIT_CHR"].ToString().Trim();
                isnoqty = true;//��ʼ��,Ĭ�����Ϊtrue
                //if (p_dRow["noqtyflag_int"].ToString().Trim() == string.Empty && p_dRow["IFSTOP_INT"].ToString().Trim() == string.Empty)
                //{
                //    isnoqty = false;
                //}
                //else
                //{
                //    if (p_dRow["noqtyflag_int"].ToString().Trim() != string.Empty && p_dRow["IFSTOP_INT"].ToString().Trim() != string.Empty)
                //    {
                //        if (int.Parse(p_dRow["noqtyflag_int"].ToString().Trim()) == 0 && int.Parse(p_dRow["IFSTOP_INT"].ToString().Trim()) == 0)
                //        {
                //            isnoqty = false;
                //        }
                //    }
                //    else if (p_dRow["noqtyflag_int"].ToString().Trim() == string.Empty && p_dRow["IFSTOP_INT"].ToString().Trim() != string.Empty)
                //    {
                //        if (int.Parse(p_dRow["IFSTOP_INT"].ToString().Trim()) == 0)
                //        {
                //            isnoqty = false;
                //        }
                //    }
                //    else if (p_dRow["noqtyflag_int"].ToString().Trim() != string.Empty && p_dRow["IFSTOP_INT"].ToString().Trim() == string.Empty)
                //    {
                //        if (int.Parse(p_dRow["noqtyflag_int"].ToString().Trim()) == 0)
                //        {
                //            isnoqty = false;
                //        }
                //    }

                //}
                //if(int.Parse(p_dRow["noqtyflag_int"].ToString())==1||int.Parse(p_dRow["IFSTOP_INT"].ToString())==1)
                //{
                //    isnoqty=true;
                //}
                //else
                //{
                //    isnoqty=false;
                //}
                //�۸�
            }
            catch { }
            try
            {
                if (p_dRow["OPCHARGEFLG_INT"].ToString() == "0")
                {
                    m_objViewer.m_txtItemPrice.Tag = Convert.ToDouble(p_dRow["ITEMPRICE_MNY"].ToString()).ToString("0.0000");
                }
                else
                {
                    double OPPRICE = 0;
                    OPPRICE = Convert.ToDouble(p_dRow["ITEMPRICE_MNY"].ToString()) / Convert.ToDouble(p_dRow["PACKQTY_DEC"].ToString());
                    m_objViewer.m_txtItemPrice.Tag = OPPRICE.ToString("0.0000");
                }

                if (p_dRow["IPCHARGEFLG_INT"].ToString() == "0")
                {
                    m_objViewer.m_txtItemPrice.Text = Convert.ToDouble(p_dRow["ITEMPRICE_MNY"].ToString()).ToString("0.0000");
                }
                else
                {
                    double OPPRICE = 0;
                    OPPRICE = Convert.ToDouble(p_dRow["ITEMPRICE_MNY"].ToString()) / Convert.ToDouble(p_dRow["PACKQTY_DEC"].ToString());
                    m_objViewer.m_txtItemPrice.Text = OPPRICE.ToString("0.0000");
                }
            }
            catch { }

            string strGetBihUnit, strClinicUnit;
            GetUnit(p_dRow, out strClinicUnit, out strGetBihUnit);
            m_objViewer.m_lblClinicUnit.Tag = strClinicUnit;
            m_objViewer.m_lblBihUnit.Tag = strGetBihUnit;
            //���ﵥλ				
            if (m_objViewer.m_cboCLINICTYPE.SelectedIndex == 0)//������λ
                m_objViewer.m_lblClinicUnit.Text = m_objViewer.m_lblClinicUnit.Tag.ToString();
            else
                m_objViewer.m_lblClinicUnit.Text = m_objViewer.m_lblSaveDosageUnit.Tag.ToString();
            //סԺ��λ				
            if (m_objViewer.m_cboBIHTYPE.SelectedIndex == 0)//������λ
                m_objViewer.m_lblBihUnit.Text = m_objViewer.m_lblBihUnit.Tag.ToString();
            else
                m_objViewer.m_lblBihUnit.Text = m_objViewer.m_lblSaveDosageUnit.Tag.ToString();

        }
        /// <summary>
        /// ����ԭ������ĿID
        /// </summary>
        private string strPreItemID = "";
        /// <summary>
        /// ����ı���	�����÷�ά����������
        /// </summary>
        /// <param name="p_objItem">�÷�ά����������</param>
        public void m_FillTextFromObject(clsBridgeForUsaEdit p_objItem)
        {
            if (p_objItem == null || p_objItem.m_strItemID == null || p_objItem.m_strItemID.Trim() == "") return;
            //��Ŀ����
            m_objViewer.m_txtItem.Text = p_objItem.m_strItemName;
            m_objViewer.m_txtItem.Tag = p_objItem.m_strItemID.Trim();
            strPreItemID = p_objItem.m_strItemID.Trim();
            //�������� {0=������;1=ȫ������;2-��������}
            m_objViewer.m_cboContinueUseType.SelectedIndex = p_objItem.m_intCONTINUEUSETYPE_INT;
            //��Ŀ����
            m_objViewer.m_txtItemType.Text = p_objItem.m_strItemType.Trim();
            //��Ŀ���
            m_objViewer.m_txtItemSpec.Text = p_objItem.m_strItemSpec.Trim();
            //����
            m_objViewer.m_txtDOSAGE_DEC.Text = p_objItem.m_dblDOSAGE_DEC.ToString().Trim() + p_objItem.m_strDOSAGEUNIT_CHR.Trim();
            m_objViewer.m_txtDOSAGE_DEC.Tag = p_objItem.m_dblDOSAGE_DEC.ToString().Trim();
            m_objViewer.m_lblSaveDosageUnit.Tag = p_objItem.m_strDOSAGEUNIT_CHR.ToString().Trim();
            //������λ
            m_objViewer.m_lblClinicUnit.Tag = p_objItem.m_strGetClinicUnit.ToString();
            m_objViewer.m_lblBihUnit.Tag = p_objItem.m_strGetBihUnit.ToString();
            //�۸�
            m_objViewer.m_txtItemPrice.Text = p_objItem.m_strItemPrice.Trim();
            //��������
            m_objViewer.m_txtCLINICQTY.Text = p_objItem.m_strUNITPRICE.Trim();
            //������������
            m_objViewer.m_cboCLINICTYPE.SelectedIndex = p_objItem.m_intCLINICTYPE_INT - 1;
            //�����ܼ�
            m_objViewer.m_txtCLINICTOTALPRICE.Text = p_objItem.m_strTOTALPRICE.Trim();
            //סԺ����
            m_objViewer.m_txtBIHQTY.Text = p_objItem.m_dblBIHQTY_DEC.ToString().Trim();
            //סԺ��������
            m_objViewer.m_cboBIHTYPE.SelectedIndex = p_objItem.m_intBIHTYPE_INT - 1;
            //סԺ�ܼ�
            m_objViewer.m_txtBIHOTALPRICE.Text = p_objItem.m_strBihOtalPrice.Trim();
            //���ﵥλ��סԺ��λ
            if (m_objViewer.m_cboCLINICTYPE.SelectedIndex == 0)//������λ
                m_objViewer.m_lblClinicUnit.Text = m_objViewer.m_lblClinicUnit.Tag.ToString();
            else
                m_objViewer.m_lblClinicUnit.Text = m_objViewer.m_lblSaveDosageUnit.Tag.ToString();
            if (m_objViewer.m_cboBIHTYPE.SelectedIndex == 0)//������λ
                m_objViewer.m_lblBihUnit.Text = m_objViewer.m_lblBihUnit.Tag.ToString();
            else
                m_objViewer.m_lblBihUnit.Text = m_objViewer.m_lblSaveDosageUnit.Tag.ToString();
            //סԺִ�п��ұ�־
            m_objViewer.m_cboZyFlag.SelectedIndex = p_objItem.m_intBihExecDeptflag - 1;
            m_objViewer.m_ctfDefDept.txtValuse = p_objItem.m_strBihExecDeptName;
            m_objViewer.m_ctfDefDept.Tag = p_objItem.m_strBihExecDeptID;

        }
        /// <summary>
        /// ת������ {From ��� To ����}
        /// </summary>
        /// <param name="strTypeNo">���</param>
        /// <returns></returns>
        private string strSwitchTypeToName(string strTypeNo)
        {
            string strRet = "��ҩ";
            switch (strTypeNo)
            {
                case "0002":
                    strRet = "��ҩ";
                    break;
                case "0003":
                    strRet = "����";
                    break;
                case "0004":
                    strRet = "����";
                    break;
                case "0005":
                    strRet = "����";
                    break;
                case "0006":
                    strRet = "����";
                    break;
                default:
                    strRet = "��ҩ";
                    break;
            }
            return strRet;
        }
        /// <summary>
        /// ��ȡ����	�����û�����
        /// </summary>
        /// <param name="p_objResult"></param>
        private void GetObjectFromControl(out clsBridgeForUsaEdit p_objResult)
        {
            p_objResult = new clsBridgeForUsaEdit();
            //�÷�ID
            p_objResult.m_strUsageID = m_objViewer.m_objBridgeForUsaEdit.m_strUsageID.Trim();
            //�������� {0=������;1=����}
            p_objResult.m_intCONTINUEUSETYPE_INT = m_objViewer.m_cboContinueUseType.SelectedIndex;
            //��Ŀ����
            p_objResult.m_strItemName = m_objViewer.m_txtItem.Text.Trim();
            if (m_objViewer.m_txtItem.Tag == null) m_objViewer.m_txtItem.Tag = "";
            p_objResult.m_strItemID = m_objViewer.m_txtItem.Tag.ToString().Trim();
            //��������
            p_objResult.m_strUNITPRICE = m_objViewer.m_txtCLINICQTY.Text.Trim();
            //������������
            p_objResult.m_intCLINICTYPE_INT = m_objViewer.m_cboCLINICTYPE.SelectedIndex + 1;
            //�����ܼ�
            //			p_objResult.m_strTOTALPRICE =m_objViewer.m_txtCLINICTOTALPRICE.Text.Trim();
            p_objResult.m_strTOTALPRICE = this.strPreItemID;//���ܼ۱���ԭ������ĿID
            //סԺ����
            try
            {
                p_objResult.m_dblBIHQTY_DEC = Convert.ToDouble(m_objViewer.m_txtBIHQTY.Text.Trim());
            }
            catch { }
            //סԺ��������
            p_objResult.m_intBIHTYPE_INT = m_objViewer.m_cboBIHTYPE.SelectedIndex + 1;
            //סԺ�ܼ�
            p_objResult.m_strBihOtalPrice = m_objViewer.m_txtBIHOTALPRICE.Text.Trim();
            //��Ŀ����
            p_objResult.m_strItemType = m_objViewer.m_txtItemType.Text.Trim();
            //��Ŀ���
            p_objResult.m_strItemSpec = m_objViewer.m_txtItemSpec.Text.Trim();
            //����
            try
            {
                p_objResult.m_dblDOSAGE_DEC = Convert.ToDouble(m_objViewer.m_txtDOSAGE_DEC.Tag.ToString().Trim());
            }
            catch { }
            //������λ
            try
            {
                p_objResult.m_strDOSAGEUNIT_CHR = m_objViewer.m_lblSaveDosageUnit.Tag.ToString().Trim();
            }
            catch { }
            //�۸�
            p_objResult.m_strItemPrice = m_objViewer.m_txtItemPrice.Text.Trim();
            //סԺִ�п��ұ�־
            p_objResult.m_intBihExecDeptflag = m_objViewer.m_cboZyFlag.SelectedIndex + 1;
            if (m_objViewer.m_ctfDefDept.Tag != null)
            {
                p_objResult.m_strBihExecDeptID = m_objViewer.m_ctfDefDept.Tag.ToString();
            }
        }
        /// <summary>
        /// ��֤�����Ƿ���Ա���
        /// </summary>
        /// <param name="p_objItem"></param>
        /// <returns></returns>
        private bool CheckObjectForSave(clsBridgeForUsaEdit p_objItem)
        {
            if (p_objItem == null && p_objItem.m_strItemID != null) return false;
            //��Ŀ������
            if (p_objItem.m_strItemName == null || p_objItem.m_strItemID == null || p_objItem.m_strItemName.Trim() == "" || p_objItem.m_strItemID.Trim() == "")
            {
                MessageBox.Show(m_objViewer, "��Ŀ������!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //���������Ϣ
            double dbl = -1;
            try { dbl = Convert.ToDouble(p_objItem.m_strUNITPRICE); }
            catch { }
            if (dbl < 0)
            {
                MessageBox.Show(m_objViewer, "������������,����Ϊ���ڻ����0����!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_txtCLINICQTY.Focus();
                return false;
            }
            if (p_objItem.m_intCLINICTYPE_INT < 1 || p_objItem.m_intCLINICTYPE_INT > 2)
            {
                MessageBox.Show(m_objViewer, "�����������Ͳ�����!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_cboCLINICTYPE.Focus();
                return false;
            }
            //סԺ�����Ϣ
            if (p_objItem.m_dblBIHQTY_DEC < 0)
            {
                MessageBox.Show(m_objViewer, "סԺ��������,����Ϊ���ڻ����0����!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_txtBIHQTY.Focus();
                return false;
            }
            if (p_objItem.m_intBIHTYPE_INT < 1 || p_objItem.m_intBIHTYPE_INT > 2)
            {
                MessageBox.Show(m_objViewer, "�����������Ͳ�����!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_objViewer.m_cboBIHTYPE.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// ��ȡ�����ܼۼ�סԺ�ܼ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="strITEMID_CHR">��ĿID</param>
        /// <param name="intType">1-�����ܼۣ�2-סԺ�ܼ�</param>
        /// <param name="dblQTY">����</param>
        /// <param name="intNuit">1-��ҩ��λ��2-������λ</param>
        public double m_dblCalMoney(string strITEMID_CHR, int intType, double dblQTY, int intNuit)
        {
            double dblTotailMoney = 0;
            clsDcl_ChargeItem objChargeItem = new clsDcl_ChargeItem();
            objChargeItem.m_lngGetChargeUsageTotailMoney(strITEMID_CHR, intType, dblQTY, intNuit, out dblTotailMoney);
            return dblTotailMoney;
        }
        /// <summary>
        /// ��ȡ������λ
        /// </summary>
        /// <param name="p_dRow"></param>
        /// <param name="p_strClinicUnit">[out ����] ����������λ</param>
        /// <param name="p_strGetBihUnit">[out ����] סԺ������λ</param>
        private void GetUnit(DataRow p_dRow, out string p_strClinicUnit, out string p_strGetBihUnit)
        {
            p_strClinicUnit = "";
            p_strGetBihUnit = "";
            if (p_dRow == null) return;

            int intGetType = 0;
            //���ﵥλ
            intGetType = 0;//�����շѵ�λ 0 ��������λ 1����С��λ
            try { intGetType = Convert.ToInt32(p_dRow["OPCHARGEFLG_INT"].ToString()); }
            catch { }
            try
            {
                if (intGetType == 0)
                {
                    p_strClinicUnit = p_dRow["ITEMOPUNIT_CHR"].ToString().Trim();
                }
                else if (intGetType == 1)
                {
                    p_strClinicUnit = p_dRow["ITEMIPUNIT_CHR"].ToString().Trim();
                }
            }
            catch { }

            //סԺ��λ
            intGetType = 0;//סԺ�շѵ�λ 0 ��������λ 1����С��λ
            try { intGetType = Convert.ToInt32(p_dRow["IPCHARGEFLG_INT"].ToString()); }
            catch { }
            try
            {
                if (intGetType == 0)
                    p_strGetBihUnit = p_dRow["ITEMOPUNIT_CHR"].ToString().Trim();
                else
                    p_strGetBihUnit = p_dRow["ITEMIPUNIT_CHR"].ToString().Trim();
            }
            catch { }
        }
        #endregion
        #region ������ҩ�÷�������Ŀ
        /// <summary>
        /// ������ҩ�÷�������Ŀ
        /// </summary>
        /// <param name="p_blnIsAddNew">�Ƿ񱣴�����</param>
        private bool m_mthblnSave(bool p_blnIsAddNew)
        {
            //��ȡ����
            clsBridgeForUsaEdit objItem;
            GetObjectFromControl(out objItem);
            //��֤����
            if (!CheckObjectForSave(objItem)) return false;
            //����
            long lngRes = 0;
            string strRecordID = "";
            clsChargeItemUsageGroup_VO objItem1 = new clsChargeItemUsageGroup_VO();
            objItem1 = objItem;
            if (m_objViewer.m_intOperateState != 1)
            {//����
                lngRes = new clsDomainControl_ChargeItem().m_lngDoAddNewChargeItemCMUsageGroup(out strRecordID, objItem1);
            }
            else
            {//�޸�
                if (objItem1.m_strTOTALPRICE.Trim() != objItem1.m_strItemID.Trim())
                {
                    if (MessageBox.Show("�Ƿ񽫰������÷�����ͬ��Ŀ����ΪЩ��Ŀ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        objItem1.m_intFlag = 1;
                    }
                }

                lngRes = new clsDomainControl_ChargeItem().m_lngDoModifyChargeItemCMUsageGroup(objItem1);
            }
            if (lngRes > 0)
            {
                MessageBox.Show(m_objViewer, "�����ɹ�!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(m_objViewer, "����ʧ��!", "��ʾ��!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return (lngRes > 0) ? (true) : (false);
        }
        #endregion

        #region ��ȡסԺִ�п���
        /// <summary>
        /// ��ȡסԺִ�п���
        /// yibin.zheng
        /// </summary>
        /// <returns></returns>
        public long m_lngGetZyExecDept()
        {
            //com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc objSvc =
            //               (com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsChargeItemSvc));

            clsBSEUsageType[] arrType;
            long lngRes = (new weCare.Proxy.ProxyOP()).Service.m_lngGetAllDepartment(out arrType);
            if (lngRes > 0 && arrType != null)
            {
                DataTable dtbResult = new DataTable();
                dtbResult.Columns.Add("��  ��");
                dtbResult.Columns.Add("�� �� �� ��");
                dtbResult.Columns.Add("ƴ �� ��");
                dtbResult.Columns.Add("deptid_chr");

                DataRow dr = null;
                for (int i1 = 0; i1 < arrType.Length; i1++)
                {
                    dr = dtbResult.NewRow();
                    dr["��  ��"] = arrType[i1].m_strUserCode;
                    dr["�� �� �� ��"] = arrType[i1].m_strUsageName;
                    dr["ƴ �� ��"] = arrType[i1].m_strPYCODE_VCHR;
                    dr["deptid_chr"] = arrType[i1].m_strUsageID;
                    dtbResult.Rows.Add(dr);
                }
                dtbResult.AcceptChanges();
                this.m_objViewer.m_ctfDefDept.m_GetDataTable = dtbResult;
            }
            return lngRes;
        }
        #endregion
    }
}
