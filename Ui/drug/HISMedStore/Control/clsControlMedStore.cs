using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ҩ�����ÿ�����
    /// Create by kong 2004-07-05
    /// </summary>
    public class clsControlMedStore : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public clsControlMedStore()
        {
            m_objManage = new clsDomainControlMedStoreBseInfo();
        }
        #endregion

        #region ����
        public string flage = "Add";
        clsDomainControlMedStoreBseInfo m_objManage = null;
        clsMedStore_VO m_objItem;
        /// <summary>
        /// ��ǰѡ����
        /// </summary>
        private int m_SelRow = 0;
        #endregion

        #region ���ô������
        frmMedStore m_objViewer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmMedStore)frmMDI_Child_Base_in;
        }
        #endregion

        #region �б����

        #region ���б����һ����¼
        /// <summary>
        /// ���б����һ������
        /// </summary>
        /// <param name="objItem">ҩ������</param>
        private void m_mthInsertDetail(clsMedStore_VO objItem)
        {
            if (objItem != null)
            {
                string strMedStoreType = "";
                string strMedicineType = "";
                string m_isUrgency = "";

                if (objItem.m_intMedStoreType == 1)
                {
                    strMedStoreType = "����ҩ��";
                }
                else if (objItem.m_intMedStoreType == 2)
                {
                    strMedStoreType = "סԺҩ��";
                }
                else if (objItem.m_intMedStoreType == 3)
                {
                    strMedStoreType = "ȫԺҩ��";
                }

                if (objItem.m_intMedicneType == 1)
                {
                    strMedicineType = "��ҩ";
                }
                else if (objItem.m_intMedicneType == 2)
                {
                    strMedicineType = "��ҩ";
                }
                else if (objItem.m_intMedicneType == 3)
                    strMedicineType = "����";
                if (objItem.m_intUrgency == 1)//�ж��Ƿ���
                {
                    m_isUrgency = "��";
                }
                else
                    m_isUrgency = "��";
                ListViewItem lsvItem = new ListViewItem(objItem.m_strMedStoreID);
                lsvItem.SubItems.Add(objItem.m_strMedStoreName.Trim());
                lsvItem.SubItems.Add(strMedStoreType);
                lsvItem.SubItems.Add(strMedicineType);
                lsvItem.SubItems.Add(m_isUrgency);//�ж��Ƿ���
                lsvItem.SubItems.Add(objItem.m_strDeptName);
                lsvItem.SubItems.Add(objItem.m_strDeptid);
                //2008.6.2 chongkun.wu+
                lsvItem.SubItems.Add(objItem.m_strMedStoreShortName);
                lsvItem.Tag = objItem;

                this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
            }
        }
        #endregion

        #region �޸��б�����
        /// <summary>
        /// �޸��б�����
        /// </summary>
        /// <param name="objItem">ҩ������</param>
        private void m_mthModifyDetail(clsMedStore_VO objItem)
        {
            string strMedStoreType = "";
            string strMedicineType = "";
            string m_isUrgency = "";
            if (objItem.m_intMedStoreType == 1)
            {
                strMedStoreType = "����ҩ��";
            }
            else if (objItem.m_intMedStoreType == 2)
            {
                strMedStoreType = "סԺҩ��";
            }
            else if (objItem.m_intMedStoreType == 3)
            {
                strMedStoreType = "ȫԺҩ��";
            }

            if (objItem.m_intMedicneType == 1)
            {
                strMedicineType = "��ҩ";
            }
            else if (objItem.m_intMedicneType == 2)
            {
                strMedicineType = "��ҩ";
            }
            else
                strMedicineType = "����";
            if (this.m_objViewer.m_chkUrgency.Checked)
            {
                m_isUrgency = "��";
                //objItem.m_intUrgency==1;
            }
            else
            {
                m_isUrgency = "��";
                //objItem.m_intUrgency==0;
            }


            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[1].Text = objItem.m_strMedStoreName.Trim();
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[2].Text = strMedStoreType;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[3].Text = strMedicineType;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = m_isUrgency;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[5].Text = objItem.m_strDeptName;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[6].Text = objItem.m_strDeptid;
            //2008.6.2 chongkun.wu+
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[7].Text = objItem.m_strMedStoreShortName;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag = objItem;
        }
        #endregion

        #endregion

        #region ����б�����
        /// <summary>
        /// ����б�����
        /// </summary>
        public void m_mthGetDetailList()
        {
            this.m_objViewer.m_lsvDetail.Items.Clear();
            clsMedStore_VO[] objItemArr = new clsMedStore_VO[0];
            long lngRes = 0;

            lngRes = this.m_objManage.m_lngGetMedStoreList(out objItemArr);
            if (lngRes > 0 && objItemArr.Length > 0)
            {
                for (int i1 = 0; i1 < objItemArr.Length; i1++)
                {
                    m_mthInsertDetail(objItemArr[i1]);
                }
                if (this.m_objViewer.m_lsvDetail.Items.Count != 0)
                    this.m_objViewer.m_lsvDetail.Items[0].Selected = true;

            }
        }
        #endregion

        #region ���ô�������
        /// <summary>
        /// ���ô�������
        /// </summary>
        /// <param name="objItem">ҩ������</param>
        public void m_mthSetViewInfo(clsMedStore_VO objItem)
        {
            this.m_objItem = objItem;

            if (this.m_objItem == null)
            {
                m_mthClear();
                if (this.m_objViewer.m_cboMedStoreType.Items.Count > 0)
                {
                    this.m_objViewer.m_cboMedStoreType.SelectedIndex = 0;
                }
                if (this.m_objViewer.m_cboMedicineType.Items.Count > 0)
                {
                    this.m_objViewer.m_cboMedicineType.SelectedIndex = 0;
                }
                this.m_objViewer.m_txtMedStoreName.Focus();

            }
            else
            {
                m_mthClear();
                this.m_objViewer.m_txtMedStoreName.Text = this.m_objItem.m_strMedStoreName.Trim();
                this.m_objViewer.m_cboMedStoreType.SelectedIndex = this.m_objItem.m_intMedStoreType - 1;
                this.m_objViewer.m_cboMedicineType.SelectedIndex = this.m_objItem.m_intMedicneType - 1;
                this.m_objViewer.m_txtDept.Text = this.m_objItem.m_strDeptName;
                this.m_objViewer.m_txtDept.AccessibleName = this.m_objItem.m_strDeptid;
                //2008.6.2 chongkun.wu+
                this.m_objViewer.txtMedStoreShortName.Text = this.m_objItem.m_strMedStoreShortName;

                this.m_objViewer.m_txtMedStoreName.Focus();
            }
        }
        #endregion

        #region �����ʼ��
        /// <summary>
        /// �����ʼ��
        /// </summary>
        public void m_mthInit()
        {
            m_mthGetDetailList();
            m_mthSetViewInfo(null);
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public void m_mthSave()
        {
            if (!m_blnCheckValue())
            {
                return;
            }

            if (this.m_objItem == null)
            {
                m_mthDoAddNew();
                m_mthClear();
            }
            else
            {
                m_mthDoUpdate();
                m_mthClear();
            }
        }
        #endregion

        #region ��ϸ�б�˫��
        /// <summary>
        /// �б�˫���¼�
        /// </summary>
        public void m_mthDetailSel()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
                {
                    clsMedStore_VO objItem = (clsMedStore_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    this.m_SelRow = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
                    if (objItem.m_intUrgency == 1)
                        this.m_objViewer.m_chkUrgency.Checked = true;
                    else
                        this.m_objViewer.m_chkUrgency.Checked = false;
                    m_mthSetViewInfo(objItem);
                }
            }
            this.flage = "Add";
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        private void m_mthDoAddNew()
        {
            clsMedStore_VO objItem = new clsMedStore_VO();
            string strDeID;
            this.m_objManage.m_lngGetMedStoreID(out strDeID);
            objItem.m_strMedStoreID = strDeID;
            objItem.m_strMedStoreName = this.m_objViewer.m_txtMedStoreName.Text.Trim();
            objItem.m_intMedStoreType = this.m_objViewer.m_cboMedStoreType.SelectedIndex + 1;
            objItem.m_intMedicneType = this.m_objViewer.m_cboMedicineType.SelectedIndex + 1;
            objItem.m_strDeptid = this.m_objViewer.m_txtDept.StrItemId.Trim();
            objItem.m_strDeptName = this.m_objViewer.m_txtDept.Text.Trim();
            //2008.6.2 chongkun.wu+
            objItem.m_strMedStoreShortName = this.m_objViewer.txtMedStoreShortName.Text.Trim();
            if (this.m_objViewer.m_chkUrgency.Checked)//�ж��Ƿ���
            {
                objItem.m_intUrgency = 1;
            }
            else
            {
                objItem.m_intUrgency = 0;
            }



            long lngRes = this.m_objManage.m_lngAddNewMedStore(objItem);

            if (lngRes > 0)
            {
                m_mthInsertDetail(objItem);
                this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count - 1].Selected = true;

            }
            else
            {
                MessageBox.Show("���ݱ�������������Ա��ϵ", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region �����޸�
        /// <summary>
        /// �����޸�
        /// </summary>
        private void m_mthDoUpdate()
        {
            this.m_objItem.m_strMedStoreName = this.m_objViewer.m_txtMedStoreName.Text.Trim();
            this.m_objItem.m_intMedStoreType = this.m_objViewer.m_cboMedStoreType.SelectedIndex + 1;
            this.m_objItem.m_intMedicneType = this.m_objViewer.m_cboMedicineType.SelectedIndex + 1;
            this.m_objItem.m_strDeptid = this.m_objViewer.m_txtDept.AccessibleName;
            this.m_objItem.m_strDeptName = this.m_objViewer.m_txtDept.Text;
            //2008.6.2 chongkun.wu+
            this.m_objItem.m_strMedStoreShortName = this.m_objViewer.txtMedStoreShortName.Text;
            if (this.m_objViewer.m_chkUrgency.Checked)//�ж��Ƿ���
            {
                m_objItem.m_intUrgency = 1;
            }
            else
            {
                m_objItem.m_intUrgency = 0;
            }

            long lngRes = this.m_objManage.m_lngUpdMedStoreByID(this.m_objItem);

            if (lngRes > 0)
            {
                m_mthModifyDetail(this.m_objItem);
            }
        }
        #endregion

        #region ����ɾ��
        /// <summary>
        /// ����ɾ��
        /// </summary>
        public void m_mthDoDelete()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
                {
                    clsMedStore_VO objItem = new clsMedStore_VO();
                    objItem = (clsMedStore_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    if (MessageBox.Show("ȷ��Ҫɾ���ü�¼��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                    long lngRes = this.m_objManage.m_lngDeleteMedStoreByID(objItem.m_strMedStoreID.Trim());
                    int index = this.m_objViewer.m_lsvDetail.SelectedIndices[0];
                    if (lngRes > 0)
                    {
                        this.m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
                        //						if(this.m_objViewer.m_lsvDetail.Items.Count>0)
                        //						{
                        //							if(index>0)
                        //								this.m_objViewer.m_lsvDetail.Items[index-1].Selected=true;
                        //							else
                        //								this.m_objViewer.m_lsvDetail.Items[index].Selected=true;
                        //						}
                        this.m_objViewer.m_lsv.Items.Clear();//ɾ���Ű���Ϣ��
                        m_mthClearDeptDutyInfo();

                    }
                }
            }
            else
            {
                MessageBox.Show("��ѡ����ɾ�����", "ϵͳ��ʾ");
                return;
            }

        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckValue()
        {
            bool blnResult = true;

            if (this.m_objViewer.m_txtMedStoreName.Text.Trim() == "")
            {
                this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtMedStoreName);
                blnResult = false;
            }
            if (this.m_objViewer.txtMedStoreShortName.Text.Trim().Replace(" ", "").Length != 3)
            {
                MessageBox.Show("�������Ϊ��λ��д��ĸ��", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.txtMedStoreShortName.Focus();
                blnResult = false;
                return false;
            }
            if (this.m_objViewer.m_txtDept.Text.Trim() == string.Empty)
            {
                MessageBox.Show("����󶨸�ҩ����Ӧ�Ĳ��ţ�", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.m_txtDept.Focus();
                blnResult = false;
                return false;
            }
            if (this.m_objItem == null)
            {
                foreach (ListViewItem lvi in this.m_objViewer.m_lsvDetail.Items)
                {
                    if (((clsMedStore_VO)lvi.Tag).m_strDeptid.Trim() == this.m_objViewer.m_txtDept.StrItemId.Trim() && this.m_objViewer.m_txtDept.StrItemId.Trim() != "")
                    {
                        MessageBox.Show(((clsMedStore_VO)lvi.Tag).m_strMedStoreName + "�Ѷ�Ӧ���ò��ţ� һ������ֻ�ܶ�Ӧһ��ҩ����", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.m_objViewer.m_txtDept.Clear();
                        this.m_objViewer.m_txtDept.Focus();
                        blnResult = false;
                        return false;
                    }
                    if (((clsMedStore_VO)lvi.Tag).m_strMedStoreShortName.Trim() == this.m_objViewer.txtMedStoreShortName.Text)
                    {
                        MessageBox.Show("\"" + m_objViewer.txtMedStoreShortName.Text + "\"�ѱ�ʹ�ã�", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.m_objViewer.txtMedStoreShortName.Clear();
                        this.m_objViewer.txtMedStoreShortName.Focus();
                        blnResult = false;
                        return false;
                    }
                }
            }
            else
            {
                foreach (ListViewItem lvi in this.m_objViewer.m_lsvDetail.Items)
                {
                    if (((clsMedStore_VO)lvi.Tag).m_strDeptid.Trim() == this.m_objViewer.m_txtDept.AccessibleName && ((clsMedStore_VO)lvi.Tag).m_strMedStoreID.Trim() != this.m_objItem.m_strMedStoreID)
                    {
                        MessageBox.Show(((clsMedStore_VO)lvi.Tag).m_strMedStoreName + "�Ѷ�Ӧ���ò��ţ� һ������ֻ�ܶ�Ӧһ��ҩ����", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.m_objViewer.m_txtDept.Clear();
                        this.m_objViewer.m_txtDept.Focus();
                        blnResult = false;
                        return false;
                    }
                    if (((clsMedStore_VO)lvi.Tag).m_strMedStoreShortName.Trim() == this.m_objViewer.txtMedStoreShortName.Text && ((clsMedStore_VO)lvi.Tag).m_strMedStoreID.Trim() != this.m_objItem.m_strMedStoreID)
                    {
                        MessageBox.Show("\"" + m_objViewer.txtMedStoreShortName.Text + "\"�ѱ�ʹ�ã�", "ҩ��������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.m_objViewer.txtMedStoreShortName.Clear();
                        this.m_objViewer.txtMedStoreShortName.Focus();
                        blnResult = false;
                        return false;
                    }
                }
            }
            if (!blnResult)
            {
                this.m_ephHandler.m_mthShowControlsErrorProvider();
                this.m_ephHandler.m_mthClearControl();
            }
            return blnResult;
        }
        #endregion

        #region ��ձ༭������  xgpeng 2006-2-9
        /// <summary>
        /// ����༭������
        /// </summary>
        private void m_mthClear()
        {
            this.m_objViewer.m_txtMedStoreName.Clear();
            this.m_objViewer.m_txtDept.Clear();
            this.m_objViewer.txtMedStoreShortName.Clear();
            this.m_objViewer.m_cboMedStoreType.SelectedIndex = -1;
            this.m_objViewer.m_cboMedicineType.SelectedIndex = -1;
        }
        #endregion

        #region ��ձ༭������(�Ű�) xgpeng 2006-2-9
        /// <summary>
        /// ��ձ༭������(�Ű�)
        /// </summary>
        public void m_mthClearDeptDutyInfo()
        {
            this.m_objViewer.m_txtRemark.Text = "";
            this.m_objViewer.m_chkMorning.Checked = false;
            this.m_objViewer.m_chkNoon.Checked = false;
            this.m_objViewer.m_chkEvening.Checked = false;
            this.m_objViewer.m_txtMedStore.txtValuse = "";
            this.m_objViewer.m_txtMedStore.Tag = "";/////�¼�
            this.m_objViewer.m_cboDate.SelectedIndex = -1;
        }
        #endregion

        #region ����ҩ����Ϣ xgpeng 2006-2-9
        /// <summary>
        /// ����ҩ����Ϣ
        /// </summary>
        public void LoadMedStoreInfo1()
        {
            int i = 4;//�����ֶ�
            DataTable m_dtable = new DataTable();
            if (this.m_objViewer.m_lsvDetail.Items.Count == 0)
                return;
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count == 0)
                return;

            long lngRes = this.m_objManage.m_lngGetMedStoreInfo(out m_dtable);

            if (lngRes > 0 && m_dtable.Rows.Count > 0)
            {
                //				DataTable tempTable = new DataTable();
                //				DataRow tempRow;
                //				tempTable.Columns[i].c.Add("ҩ��ID");
                //				tempTable.Columns.Add("ҩ������");
                //				tempTable.Columns.Add("ҩ�����");

                m_dtable.Columns[0].ColumnName = "ҩ��ID";
                m_dtable.Columns[1].ColumnName = "  ҩ������";
                m_dtable.Columns[2].ColumnName = "ҩ������";


                //					 				for(int i = 0;i<m_dtable.Rows.Count;i++)
                //					 				{
                //					 					tempRow = tempTable.NewRow();
                //					 					tempRow[0]=m_dtable.Rows[i][].m_strEMPNO_CHR;
                //					 					tempRow[1] = DataResultArr[i].m_strLASTNAME_VCHR;
                //					 					tempRow[2] = DataResultArr[i].m_strEMPID_CHR;
                //					 					tempTable.Rows.Add(tempRow);
                //					 				}
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[3].Text.ToString().Trim() == "��ҩ")
                    i = 1;
                else if (this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[3].Text.ToString().Trim() == "��ҩ")
                    i = 2;
                else
                    i = 3;
                int count = m_dtable.Rows.Count;
                for (int i1 = 0; i1 < m_dtable.Rows.Count; i1++)
                {
                    if (Convert.ToInt32(m_dtable.Rows[i1]["ҩ������"].ToString().Trim()) != i ||
                        m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[1].Text.ToString().Trim() == m_dtable.Rows[i1]["  ҩ������"].ToString().Trim())
                    {
                        m_dtable.Rows.RemoveAt(i1);
                        i1--;
                    }


                }
                //if (m_dtable.Rows.Count == 1)
                //    m_dtable.Rows.Clear();
                m_objViewer.m_txtMedStore.m_GetDataTable = m_dtable;
            }
        }
        #endregion
        public void m_mthInitialDept()
        {
            DataTable m_dtDeptDesc = new DataTable();
            this.m_objManage.m_lngGetDeptInfo(out m_dtDeptDesc);
            this.m_objViewer.m_txtDept.m_mthInitDeptData(m_dtDeptDesc);

        }
        #region ����ҩ��ID��ȡҩ���Ű���Ϣ xgpeng 2006-2-9
        /// <summary>
        /// ����ҩ��ID��ȡҩ���Ű���Ϣ
        /// </summary>
        public void m_GetDeptDutyInfo()
        {
            string p_TypeID = "";
            string m_strDate = "";
            clsMedDeptDuty_VO[] m_objResArr;
            if (this.m_objViewer.m_lsvDetail.Items.Count == 0)
                return;
            //			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count==0)
            //			{
            //				//MessageBox.Show("��ѡ���¼","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            //				//MessageBox.Show("��ѡ���¼","��ʾ");
            //				return;
            //			}
            p_TypeID = ((clsMedStore_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag).m_strMedStoreID;
            long lngRes = this.m_objManage.m_lngGetDeptDutyInfo(p_TypeID, out m_objResArr);
            this.m_objViewer.m_lsv.Items.Clear();
            if (lngRes > 0 && m_objResArr.Length > 0)
            {
                ListViewItem ListTemp = null;
                for (int i1 = 0; i1 < m_objResArr.Length; ++i1)
                {
                    m_strDate = m_changeDate(m_objResArr[i1].m_intWeekDay);
                    ListTemp = new ListViewItem(m_strDate);
                    ListTemp.SubItems.Add(m_objResArr[i1].m_strWorkTime.ToString().Trim());
                    ListTemp.SubItems.Add(m_objResArr[i1].m_strObjectDeptName.ToString().Trim());
                    ListTemp.SubItems.Add(m_objResArr[i1].m_strRemark.ToString().Trim());
                    ListTemp.Tag = m_objResArr[i1];
                    this.m_objViewer.m_lsv.Items.Add(ListTemp);
                }
            }
        }
        #endregion

        #region ����ת��������  xgpeng 2006-2-9
        private string m_changeDate(int i2)
        {
            string p_strDate = "";
            switch (i2)
            {
                case 1: p_strDate = "����һ";
                    break;
                case 2: p_strDate = "���ڶ�";
                    break;
                case 3: p_strDate = "������";
                    break;
                case 4: p_strDate = "������";
                    break;
                case 5: p_strDate = "������";
                    break;
                case 6: p_strDate = "������";
                    break;
                case 7: p_strDate = "������";
                    break;
            }
            return p_strDate;
        }
        #endregion

        #region ����ת�������� xgpeng 2006-2-9
        private int m_changeNum(string p_strDate)
        {
            int m_intWeekDay = 0;
            switch (p_strDate)
            {
                case "����һ": m_intWeekDay = 1;
                    break;
                case "���ڶ�": m_intWeekDay = 2;
                    break;
                case "������": m_intWeekDay = 3;
                    break;
                case "������": m_intWeekDay = 4;
                    break;
                case "������": m_intWeekDay = 5;
                    break;
                case "������": m_intWeekDay = 6;
                    break;
                case "������": m_intWeekDay = 7;
                    break;
            }
            return m_intWeekDay;
        }
        #endregion

        #region ����ҩ���Ű���Ϣ  xgpeng 2006-2-9
        public void m_AddDuty()
        {
            this.flage = "Add";
            m_mthClearDeptDutyInfo();
            this.m_objViewer.m_cboDate.Focus();
        }
        #endregion

        #region ���� ����ҩ���Ű���Ϣ  xgpeng 2006-2-9
        /// <summary>
        /// ���� ����ҩ���Ű���Ϣ
        /// </summary>
        public void m_AddDeptDutyInfo()
        {
            string p_strTime = ""; //�Ű�ʱ��
            bool flag = false;
            int m_intSeq;   //��ˮ��
            clsMedDeptDuty_VO p_objDuty = new clsMedDeptDuty_VO();

            p_objDuty.m_intWeekDay = m_changeNum(this.m_objViewer.m_cboDate.Text.Trim());

            flag = m_Judge(out p_strTime);
            if (flag == false)
                return;
            p_objDuty.m_strWorkTime = p_strTime.Trim();
            p_objDuty.m_strDeptID = ((clsMedStore_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag).m_strMedStoreID;
            p_objDuty.m_intTypeID = 1;
            p_objDuty.m_strObjectDeptID = this.m_objViewer.m_txtMedStore.Tag.ToString().Trim();
            p_objDuty.m_strObjectDeptName = this.m_objViewer.m_txtMedStore.txtValuse.Trim();
            p_objDuty.m_strRemark = this.m_objViewer.m_txtRemark.Text.Trim();

            long lngRes = this.m_objManage.m_lngAddDeptDutyInfo(out  m_intSeq, p_objDuty);
            if (lngRes > 0)
            {
                //			    p_objDuty.m_strSeq= m_intSeq;
                //				ListViewItem LsvTemp=new ListViewItem(this.m_objViewer.m_cboDate.Text.Trim());
                //				LsvTemp.SubItems.Add(p_objDuty.m_strWorkTime.ToString().Trim());
                //				LsvTemp.SubItems.Add(p_objDuty.m_strObjectDeptName.ToString().Trim());
                //				LsvTemp.SubItems.Add(p_objDuty.m_strRemark.ToString().Trim());
                //				LsvTemp.Tag=p_objDuty;
                //				this.m_objViewer.m_lsv.Items.Add(LsvTemp);
                m_GetDeptDutyInfo();
                m_mthClearDeptDutyInfo();
            }
        }

        #endregion

        #region �жϿؼ��Ƿ�Ϊ�� �� ƴ��ʱ��  xgpeng 2006-2-9
        private bool m_Judge(out string p_strTime)
        {
            p_strTime = "";
            this.m_objViewer.m_dtpMorng1.Value = Convert.ToDateTime(this.m_objViewer.m_dtpMorng1.Value.ToString("HH:mm"));
            this.m_objViewer.m_dtpMorng2.Value = Convert.ToDateTime(this.m_objViewer.m_dtpMorng2.Value.ToString("HH:mm"));
            this.m_objViewer.m_dtpNoon1.Value = Convert.ToDateTime(this.m_objViewer.m_dtpNoon1.Value.ToString("HH:mm"));
            this.m_objViewer.m_dtpNoon2.Value = Convert.ToDateTime(this.m_objViewer.m_dtpNoon2.Value.ToString("HH:mm"));
            this.m_objViewer.m_dtpEven1.Value = Convert.ToDateTime(this.m_objViewer.m_dtpEven1.Value.ToString("HH:mm"));
            this.m_objViewer.m_dtpEven2.Value = Convert.ToDateTime(this.m_objViewer.m_dtpEven2.Value.ToString("HH:mm"));

            if (this.m_objViewer.m_chkMorning.Checked == true)//ʱ���1
            {
                if (this.m_objViewer.m_chkNoon.Checked == true && Convert.ToDateTime(this.m_objViewer.m_dtpMorng1.Value.ToString("HH:mm")) > Convert.ToDateTime(this.m_objViewer.m_dtpNoon1.Value.ToString("HH:mm")))
                {

                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpNoon1.Focus();
                    return false;
                }
                else if (this.m_objViewer.m_chkEvening.Checked == true && this.m_objViewer.m_dtpMorng1.Value > this.m_objViewer.m_dtpEven1.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }
                if (this.m_objViewer.m_dtpMorng1.Value > this.m_objViewer.m_dtpMorng2.Value)
                {
                    MessageBox.Show("����С��ǰ��ʱ��", "��ʾ");
                    this.m_objViewer.m_dtpMorng2.Focus();
                    return false;
                }
                if (this.m_objViewer.m_chkNoon.Checked == true && this.m_objViewer.m_dtpMorng2.Value > this.m_objViewer.m_dtpNoon1.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpNoon1.Focus();
                    return false;
                }
                else if (this.m_objViewer.m_chkEvening.Checked == true && this.m_objViewer.m_dtpMorng2.Value > this.m_objViewer.m_dtpEven1.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }

                p_strTime = this.m_objViewer.m_dtpMorng1.Value.ToString("HH:mm") + "-" + this.m_objViewer.m_dtpMorng2.Value.ToString("HH:mm");
            }

            if (this.m_objViewer.m_chkNoon.Checked == true) //ʱ���2
            {
                if (this.m_objViewer.m_chkMorning.Checked == true)
                    p_strTime += "|";
                if (this.m_objViewer.m_chkMorning.Checked == true && this.m_objViewer.m_dtpNoon1.Value < this.m_objViewer.m_dtpMorng2.Value)
                {

                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpNoon1.Focus();
                    return false;
                }
                if (this.m_objViewer.m_dtpNoon1.Value > this.m_objViewer.m_dtpNoon2.Value)
                {
                    MessageBox.Show("����С��ǰ��ʱ��", "��ʾ");
                    this.m_objViewer.m_dtpNoon2.Focus();
                    return false;
                }
                else if (this.m_objViewer.m_chkEvening.Checked == true && this.m_objViewer.m_dtpNoon1.Value > this.m_objViewer.m_dtpEven1.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }

                if (this.m_objViewer.m_chkEvening.Checked == true && this.m_objViewer.m_dtpNoon2.Value > this.m_objViewer.m_dtpEven1.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }
                p_strTime += this.m_objViewer.m_dtpNoon1.Value.ToString("HH:mm") + "-" + this.m_objViewer.m_dtpNoon2.Value.ToString("HH:mm");
            }
            if (this.m_objViewer.m_chkEvening.Checked == true)  //ʱ���3
            {
                if (this.m_objViewer.m_chkMorning.Checked == true || this.m_objViewer.m_chkNoon.Checked == true)
                    p_strTime += "|";
                if (this.m_objViewer.m_chkMorning.Checked == true && this.m_objViewer.m_dtpEven1.Value < this.m_objViewer.m_dtpMorng2.Value)
                {

                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }
                else if (this.m_objViewer.m_chkNoon.Checked == true && this.m_objViewer.m_dtpEven1.Value < this.m_objViewer.m_dtpNoon2.Value)
                {
                    MessageBox.Show("���������ʱ���", "��ʾ");
                    this.m_objViewer.m_dtpEven1.Focus();
                    return false;
                }
                if (this.m_objViewer.m_dtpEven1.Value > this.m_objViewer.m_dtpEven2.Value)
                {
                    MessageBox.Show("����С��ǰ��ʱ��", "��ʾ");
                    this.m_objViewer.m_dtpEven2.Focus();
                    return false;
                }
                p_strTime += this.m_objViewer.m_dtpEven1.Value.ToString("HH:mm") + "-" + this.m_objViewer.m_dtpEven1.Value.ToString("HH:mm");
            }

            if (this.m_objViewer.m_cboDate.Text == "")
            {
                MessageBox.Show("��ѡ���Ű�����", "��ʾ");
                this.m_objViewer.m_cboDate.Focus();
                return false;
            }
            if (p_strTime == "")
            {
                MessageBox.Show("��ѡ���Ű�ʱ��", "��ʾ");
                this.m_objViewer.m_chkMorning.Focus();
                return false;
            }
            //if(this.m_objViewer.m_txtMedStore.txtValuse=="")
            //{
            //    MessageBox.Show("ת��ҩ������Ϊ��","��ʾ");
            //    this.m_objViewer.m_txtMedStore.Focus();
            //    return false;
            //}
            if (this.flage == "Add")
            {
                for (int i1 = 0; i1 < this.m_objViewer.m_lsv.Items.Count; ++i1)
                {

                    if (this.m_objViewer.m_cboDate.Text.Trim() == m_objViewer.m_lsv.Items[i1].SubItems[0].Text.Trim())
                    {
                        MessageBox.Show("���ڲ����ظ�", "��ʾ");
                        this.m_objViewer.m_cboDate.Focus();
                        return false;
                    }
                }
            }
            else
            {
                for (int i1 = 0; i1 < this.m_objViewer.m_lsv.Items.Count; ++i1)
                {
                    if (this.m_objViewer.m_cboDate.Text.Trim() == m_objViewer.m_lsv.Items[i1].SubItems[0].Text.Trim() &&
                        this.m_objViewer.m_lsv.SelectedItems[0].Index != i1)
                    {
                        MessageBox.Show("���ڲ����ظ�", "��ʾ");
                        this.m_objViewer.m_cboDate.Focus();
                        return false;
                    }
                }
            }
            return true;


        }
        #endregion

        #region ���� �Ű���Ϣ xgpeng 2006-2-9
        /// <summary>
        /// ���� �Ű���Ϣ
        /// </summary>
        public void m_saveDeptDutyInfo()
        {
            if (this.m_objViewer.m_lsvDetail.Items.Count == 0)
                return;

            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ���Ű�ҩ��", "��ʾ");
                return;
            }
            if (this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[1].Text == this.m_objViewer.m_txtMedStore.txtValuse.Trim())
            {
                MessageBox.Show("ת��ҩ������Ϊ�Լ�", "��ʾ");
                m_objViewer.m_txtMedStore.Focus();
                return;
            }
            if (this.flage == "Add")
            {
                m_AddDeptDutyInfo();
            }
            else
                m_UpdateDeptDutyInfo();



        }
        #endregion

        #region ���� �޸��Ű���Ϣ xgpeng 2006-2-9
        /// <summary>
        /// ���� �޸��Ű���Ϣ
        /// </summary>
        public void m_UpdateDeptDutyInfo()
        {
            bool flag = false;
            string p_strTime = "";
            if (this.m_objViewer.m_lsv.Items.Count == 0)
                return;
            if (this.m_objViewer.m_lsv.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�޸ĵļ�¼", "��ʾ");
                return;
            }
            flag = m_Judge(out p_strTime);
            if (flag == false)
                return;
            clsMedDeptDuty_VO p_objWorkDuty = new clsMedDeptDuty_VO();
            p_objWorkDuty = (clsMedDeptDuty_VO)(this.m_objViewer.m_lsv.SelectedItems[0].Tag);
            p_objWorkDuty.m_strWorkTime = p_strTime.Trim();
            p_objWorkDuty.m_intWeekDay = m_changeNum(this.m_objViewer.m_cboDate.Text.Trim());
            //p_objWorkDuty.m_strDeptID=((clsMedStore_VO)this.m_objViewer.m_lsv.SelectedItems[0].Tag).m_strMedStoreID;
            //p_objWorkDuty.m_intTypeID=1;
            p_objWorkDuty.m_strObjectDeptID = this.m_objViewer.m_txtMedStore.Tag.ToString().Trim();
            p_objWorkDuty.m_strObjectDeptName = this.m_objViewer.m_txtMedStore.txtValuse.Trim();
            p_objWorkDuty.m_strRemark = this.m_objViewer.m_txtRemark.Text.Trim();
            long lngRes = this.m_objManage.m_thUpdateDeptDutyInfo(p_objWorkDuty);
            if (lngRes > 0)
            {
                //				this.m_objViewer.m_lsv.SelectedItems[0].SubItems[0].Text=this.m_objViewer.m_cboDate.Text.Trim();
                //				this.m_objViewer.m_lsv.SelectedItems[0].SubItems[1].Text=p_objWorkDuty.m_strWorkTime.ToString().Trim();
                //				this.m_objViewer.m_lsv.SelectedItems[0].SubItems[2].Text=p_objWorkDuty.m_strObjectDeptName.ToString().Trim();
                //				this.m_objViewer.m_lsv.SelectedItems[0].SubItems[3].Text=p_objWorkDuty.m_strRemark.ToString().Trim();
                //				this.m_objViewer.m_lsv.SelectedItems[0].Tag=p_objWorkDuty;
                m_GetDeptDutyInfo();
                this.flage = "Add";
                //this.m_objViewer.m_lsv.SelectedItems[0].Checked=false;
                m_mthClearDeptDutyInfo();
            }

        }

        #region ɾ�� �Ű���Ϣ  xgpeng 2006-2-9
        /// <summary>
        /// ɾ�� �Ű���Ϣ
        /// </summary>
        public void m_DelDeptDutyInfo()
        {
            int p_intID;
            if (this.m_objViewer.m_lsv.Items.Count == 0)
                return;
            if (this.m_objViewer.m_lsv.SelectedItems.Count == 0)
            {
                MessageBox.Show("��ѡ��Ҫ�޸ĵļ�¼", "��ʾ");
                return;
            }
            if (MessageBox.Show("ȷ��Ҫɾ���ü�¼��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            //clsMedDeptDuty_VO p_objWorkDuty=new clsMedDeptDuty_VO();
            p_intID = ((clsMedDeptDuty_VO)m_objViewer.m_lsv.SelectedItems[0].Tag).m_strSeq;
            long lngRes = this.m_objManage.m_thDelDeptDutyInfo(p_intID);
            if (lngRes > 0)
            {
                m_objViewer.m_lsv.SelectedItems[0].Remove();
                m_mthClearDeptDutyInfo(); //��տؼ�����
                this.flage = "Add";
            }
        }
        #endregion


        #endregion

        #region �Ű��б����ı�  xgpeng 2006-2-9
        /// <summary>
        /// �Ű��б����ı� 
        /// </summary>
        public void m_thChangeLsvText()
        {
            string p_strWorkTime = "";
            this.flage = "Update";
            if (this.m_objViewer.m_lsv.Items.Count == 0)
                return;
            if (this.m_objViewer.m_lsv.SelectedItems.Count == 0)
            {
                //MessageBox.Show("��ѡ���¼","��ʾ");
                return;
            }
            this.m_objViewer.m_cboDate.Text = this.m_objViewer.m_lsv.SelectedItems[0].SubItems[0].Text.Trim();
            p_strWorkTime = this.m_objViewer.m_lsv.SelectedItems[0].SubItems[1].Text.Trim();
            m_DischargeWorkTime(p_strWorkTime);
            this.m_objViewer.m_txtMedStore.Tag = ((clsMedDeptDuty_VO)m_objViewer.m_lsv.SelectedItems[0].Tag).m_strObjectDeptID.Trim();
            this.m_objViewer.m_txtMedStore.txtValuse = this.m_objViewer.m_lsv.SelectedItems[0].SubItems[2].Text.Trim();
            this.m_objViewer.m_txtRemark.Text = this.m_objViewer.m_lsv.SelectedItems[0].SubItems[3].Text.Trim();
        }
        #endregion

        #region ���ʱ��  xgpeng 2006-2-9
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <param name="p_strWorkTime"></param>
        private void m_DischargeWorkTime(string p_strWorkTime)
        {
            string _split = "|";
            string[] objstr = p_strWorkTime.ToString().Split(_split.ToCharArray());


            for (int f2 = 0; f2 < objstr.Length; f2++)
            {
                _split = "-";
                string[] objstr1 = objstr[f2].Split(_split.ToCharArray());
                if (objstr1.Length == 2)
                {
                    if (f2 == 2) //������ʱ��
                    {
                        this.m_objViewer.m_chkEvening.Checked = true;
                        this.m_objViewer.m_dtpEven1.Value = Convert.ToDateTime(objstr1[0].ToString().Trim());
                        this.m_objViewer.m_dtpEven2.Value = Convert.ToDateTime(objstr1[1].ToString().Trim());
                        continue;
                    }
                    else
                        this.m_objViewer.m_chkEvening.Checked = false;
                    if (f2 == 1) //�ڶ���ʱ��
                    {
                        this.m_objViewer.m_chkNoon.Checked = true;
                        this.m_objViewer.m_dtpNoon1.Value = Convert.ToDateTime(objstr1[0].ToString().Trim());
                        this.m_objViewer.m_dtpNoon2.Value = Convert.ToDateTime(objstr1[1].ToString().Trim());
                        continue;
                    }
                    else
                        this.m_objViewer.m_chkNoon.Checked = false;

                    if (f2 == 0) //��һ��ʱ��
                    {
                        this.m_objViewer.m_chkMorning.Checked = true;
                        this.m_objViewer.m_dtpMorng1.Value = Convert.ToDateTime(objstr1[0].ToString().Trim());
                        this.m_objViewer.m_dtpMorng2.Value = Convert.ToDateTime(objstr1[1].ToString().Trim());
                        continue;
                    }
                    else
                        this.m_objViewer.m_chkMorning.Checked = false;
                }
            }
        }
        #endregion


    }
}
