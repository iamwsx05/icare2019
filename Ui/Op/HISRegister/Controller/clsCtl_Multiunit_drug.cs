using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.gui.HIS
{
    class clsCtl_Multiunit_drug : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ����
        /// <summary>
        /// �߼���
        /// </summary>
        private clsDcl_Multiunit_drug m_objDomain;

        /// <summary>
        /// �����
        /// </summary>
        private frmMultiunit_drug m_objViewer;

        private DataView dvMedicine;
        private DataView dvMultiUnit;
        private DataTable dtMultiUnit1;
        /// <summary>
        /// ҩƷ���ݱ��ֶ�
        /// </summary>
        private string[] strMedColArr = new string[7] { "itemid_chr", "itemcode_vchr", "itemname_vchr", 
                                                      "itempycode_chr", "itemwbcode_chr", "itemcommname_vchr", 
                                                      "itemengname_vchr" 
                                                    };

        #endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_Multiunit_drug()
        {
            this.m_objDomain = new clsDcl_Multiunit_drug();
            dvMedicine = new DataView();
            //dvAlias = new DataView();
        }
        #endregion
        
        #region ���ô���
        /// <summary>
        /// ���ô���
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmMultiunit_drug)frmMDI_Child_Base_in;
        }
        #endregion

        #region �����ʼ��
        /// <summary>
        /// �����ʼ��
        /// </summary>
        public void m_mthInit()
        {
            m_mthLoadDvMedicine();
        }
        #endregion

        #region ��ʾȫ��ҩƷ
        /// <summary>
        /// ��ʾȫ��ҩƷ
        /// </summary>
        public void m_mthShowAllMedicine()
        {
            this.dvMedicine.RowFilter = "";
            m_mthDgvMedicineDataBind();
            if (this.m_objViewer.dtgMedicineList.Rows.Count > 0)
            {
                this.m_objViewer.dtgMedicineList.Rows[0].Selected = true;
                m_mthShowSeledMedName();
                m_mthLoadDvMultiUnit();
                m_mthDgvMultiUnitDataBind();
                if (this.m_objViewer.dtgMultiUnitList.Rows.Count > 0)
                {
                    this.m_objViewer.dtgMultiUnitList.Rows[0].Selected = true;
                }
                m_mthTxtLoadData();
            }
        }
        #endregion

        #region �����ݿ����ҩƷ��ͼ
        /// <summary>
        /// �����ݿ����ҩƷ��ͼ
        /// </summary>
        public void m_mthLoadDvMedicine()
        {
            DataTable dtMedicine = new DataTable();
            long lngRes = this.m_objDomain.m_lngGetTableMedicineList(out dtMedicine);
            if (lngRes > -1)
            {
                this.dvMedicine = new DataView(dtMedicine);
                this.dvMedicine.Sort = "itemid_chr asc";
                this.dvMedicine.RowFilter = "";
            }
            dtMedicine = null;
            //this.m_mthDgvMedicineDataBind();
        }
        #endregion

        #region ��ʾ��ǰҩƷ����
        /// <summary>
        /// ��ʾѡ���ҩƷ����
        /// </summary>
        public void m_mthShowSeledMedName()
        {
            if (this.m_objViewer.dtgMedicineList.Rows.Count > 0)
            {
                this.m_objViewer.labMedicineName.Text = "ҩƷ���ƣ���" + this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineName"].Value.ToString() + "��";
                this.m_objViewer.lblSpec.Text = "ҩƷ���" + this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["medspec_vchr"].Value.ToString() ;
                this.m_objViewer.lblProductor.Text = "�������ң�" + this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["productorid_chr"].Value.ToString() ;
            }
            else
            {
                this.m_objViewer.labMedicineName.Text = "ҩƷ���ƣ���û�У�";
                this.m_objViewer.lblSpec.Text = "ҩƷ���";
                this.m_objViewer.lblProductor.Text = "�������ң�";
            }
        }
        #endregion

        #region ��ҩƷ�б���ҩƷ��ͼ
        /// <summary>
        /// ��ҩƷ�б���ҩƷ��ͼ
        /// </summary>
        public void m_mthDgvMedicineDataBind()
        {
            m_objViewer.dtgMedicineList.DataSource = this.dvMedicine;
        }
        #endregion

        #region ���ݹؼ��ֲ���ҩƷ
        /// <summary>
        /// ���ݹؼ��ֲ���ҩƷ
        /// </summary>
        public void m_mthQueryMedicine()
        {
            string strKey = this.m_objViewer.txtMKey.Text.Trim();
            string strBy = strMedColArr[this.m_objViewer.cmbMKey.SelectedIndex + 1];

            try
            {
                this.dvMedicine.RowFilter = strBy + " like '" + strKey + "%' ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.m_objViewer, "������Ĳ�ѯ���������Ƿ��ַ������޸Ĳ�ѯ������", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.m_objViewer.txtMKey.Focus();
                return;
            }
            m_mthDgvMedicineDataBind();

            if (this.m_objViewer.dtgMedicineList.Rows.Count > 0)
            {
                this.m_objViewer.dtgMedicineList.Rows[0].Selected = true;
                m_mthLoadDvMultiUnit();
                m_mthDgvMultiUnitDataBind();
                if (this.m_objViewer.dtgMultiUnitList.Rows.Count > 0)
                {
                    this.m_objViewer.dtgMultiUnitList.Rows[0].Selected = true;
                }
            }
            else
            {
                MessageBox.Show("�Ҳ�����ҩƷ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_objViewer.cmdSave.Tag = null;
                this.m_mthClearTXT();
                this.m_objViewer.dtgMultiUnitList.DataSource = this.dtMultiUnit1;
                return;
            }
            //��ά���ı����ر�����Ϣ
            m_mthTxtLoadData();
            //��ʾ��ǰѡ���ҩƷ����
            m_mthShowSeledMedName();
        }
        #endregion

        #region ���ά���е�textbox
        /// <summary>
        /// ���ά���е�textbox
        /// </summary>
        public void m_mthClearTXT()
        {
            this.m_objViewer.txtUnitName.Text = "";
            this.m_objViewer.txtPackage.Text = "";
            this.m_objViewer.ckbCurruseFlag.Checked = false;
            this.m_objViewer.cboStatus.SelectedIndex = 1;
        }
        #endregion

        #region �����ݿ����ѡ����ҩƷ�ĵ�λ��ͼ
        /// <summary>
        /// �����ݿ����ѡ����ҩƷ�ĵ�λ��ͼ
        /// </summary>
        public void m_mthLoadDvMultiUnit()
        {
            if (this.m_objViewer.dtgMedicineList.SelectedRows.Count < 1)
            {
                return;
            }

            DataTable dtMultiUnit = new DataTable();
            string p_strMedId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            long lngRes = this.m_objDomain.m_lngGetTableMultiUnitList(p_strMedId, out dtMultiUnit);
            if (lngRes > -1)
            {
                this.dtMultiUnit1 = dtMultiUnit.Clone();
                this.dvMultiUnit = new DataView(dtMultiUnit);
                this.dvMultiUnit.Sort = "itemid_chr asc";
                this.dvMultiUnit.RowFilter = "";
                m_mthDgvMultiUnitDataBind();
            }
            dtMultiUnit = null;
        }
        #endregion

        #region �󶨱����б��뵥λ��ͼ
        /// <summary>
        /// �󶨱����б��뵥λ��ͼ
        /// </summary>
        public void m_mthDgvMultiUnitDataBind()
        {
            m_objViewer.cmdNew.Enabled = true;
            m_objViewer.cmdCancel.Enabled = true;
            m_objViewer.dtgMultiUnitList.DataSource = dvMultiUnit;
            if (m_objViewer.dtgMultiUnitList.SelectedRows.Count > 0)
            {
                m_objViewer.dtgMultiUnitList_RowEnter(m_objViewer.dtgMultiUnitList, null);
            }
        }
        #endregion

        #region ά���ı�����ѡ����λ��Ϣ
        /// <summary>
        /// ά���ı�����ѡ���ĵ�λ��Ϣ
        /// </summary>
        public void m_mthTxtLoadData()
        {
            //�б�û�е�λ�����ɱ�����ɾ��
            if (this.m_objViewer.dtgMultiUnitList.Rows.Count < 1)
            {
                //���ά���е�textbox
                m_mthClearTXT();
                return;
                
            }
            //�ѵ�ǰѡ�еĵ�λ���ص�txt��
            DataGridViewRow dgvrSelectedRow = this.m_objViewer.dtgMultiUnitList.SelectedRows[0];
            this.m_objViewer.txtUnitName.Text = dgvrSelectedRow.Cells["ColumnUnitName"].Value.ToString();
            this.m_objViewer.txtPackage.Text = dgvrSelectedRow.Cells["ColumnPackageNum"].Value.ToString();
            if (dgvrSelectedRow.Cells[3].Value.ToString() == "��")
            {
                m_objViewer.ckbCurruseFlag.Checked = true;
            }
            else 
            {
                m_objViewer.ckbCurruseFlag.Checked = false;
            }
            //this.m_objViewer.cboStatus.SelectedIndex = int.Parse(dgvrSelectedRow.Cells["Status"].Value.ToString());
            if (dgvrSelectedRow.Cells["Status"].Value.ToString() == "����")
            {
                this.m_objViewer.cboStatus.SelectedIndex = 1;
            }
            else
            {
                this.m_objViewer.cboStatus.SelectedIndex = 0;
            }
            clsMultiunit_drug_VO objTmp = new clsMultiunit_drug_VO();
            objTmp.m_strItemId = dgvrSelectedRow.Cells["ColumnItemID"].Value.ToString();
            objTmp.m_strUnit = dgvrSelectedRow.Cells["ColumnUnitName"].Value.ToString();
            objTmp.m_intPackage = int.Parse(dgvrSelectedRow.Cells["ColumnPackageNum"].Value.ToString());
            if (dgvrSelectedRow.Cells[3].Value.ToString() == "��")
            {
                objTmp.m_intCurruseFlag_Int = 1;
            }
            else
            {
                objTmp.m_intCurruseFlag_Int = 0;
            }
            if (dgvrSelectedRow.Cells["Status"].Value.ToString() == "����")
            {
                objTmp.m_intStauts = 1;
            }
            else
            {
                objTmp.m_intStauts = 0;
            }

            this.m_objViewer.cmdSave.Tag = objTmp;
            
        }
        #endregion

        #region ɾ����λ
        /// <summary>
        /// ɾ����λ
        /// </summary>
        /// <returns></returns>
        public bool m_blnDeleteMultiUnit()
        {
            if (this.m_objViewer.dtgMultiUnitList.Rows.Count <= 0)
            {
                return false;
            }

            //ʵ����VO��ס���ֶ��и�ֵ
            clsMultiunit_drug_VO objVO = m_objGetVOFromText();

            //�����ݿ���ɾ����λ
            long lngRes = this.m_objDomain.m_lngDeleteMultiUnit(objVO);
            //��������ݿ���ɾ�������ɹ�
            if (lngRes > 0)
            {
                
                MessageBox.Show("��λɾ���ɹ�!", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("��λɾ��ʧ��!", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        #endregion

        #region  ʵ����clsMultiunit_drug_VO����
        /// <summary>
        /// ʵ����clsMultiunit_drug_VO����
        /// </summary>
        /// <returns>clsMultiunit_drug_VO����</returns>
        private clsMultiunit_drug_VO m_objGetVOFromText()
        {
            clsMultiunit_drug_VO objVO = new clsMultiunit_drug_VO();
            objVO.m_strItemId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            objVO.m_strUnit = this.m_objViewer.txtUnitName.Text.ToString().Trim();
            objVO.m_intPackage = Convert.ToInt16(this.m_objViewer.txtPackage.Text.ToString().Trim());
            if (m_objViewer.ckbCurruseFlag.Checked)
            {
                
                objVO.m_intCurruseFlag_Int = 1;
            }
            else 
            {
                objVO.m_intCurruseFlag_Int = 0;
            }
            objVO.m_intStauts = this.m_objViewer.cboStatus.SelectedIndex;
            return objVO;
        }
        #endregion

        #region ���ӵ�λ
        /// <summary>
        /// ���ӵ�λ
        /// </summary>
        /// <returns></returns>
        public bool m_blnAddMultiUnit()
        {
            //ʵ����VO��ס���ֶ��и�ֵ
            clsMultiunit_drug_VO objVO = m_objGetVOFromText();
            string strSeledMedId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            if (this.m_objDomain.m_blnQueryByIndex(strSeledMedId, objVO.m_strUnit, objVO.m_intPackage, objVO.m_intCurruseFlag_Int))
            {
                MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.txtUnitName.Focus();
                m_objViewer.txtUnitName.SelectAll();
                return false;
            }

            //�����ݿ������ӱ���
            long lngRes = this.m_objDomain.m_lngAddMultiUnit(objVO);
            //��������ݿ������ӱ����ɹ�
            if (lngRes > 0)
            {
                //����ͼ����������ӵ�����

                this.m_mthLoadDvMultiUnit();
                this.m_mthDgvMultiUnitDataBind();
                this.m_objViewer.dtgMultiUnitList.Rows[this.m_objViewer.dtgMultiUnitList.Rows.Count - 1].Selected = true;
                m_mthTxtLoadData();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region �ж��Ƿ�Ϊ����
        /// <summary>
        /// �ж��Ƿ�Ϊ����
        /// </summary>
        /// <returns></returns>
        public bool m_blnIsNum()
        {
            string strS = m_objViewer.txtPackage.Text.ToString().Trim();
            if (strS == null || strS.Length == 0)
            {
                return true;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(strS);
            foreach (byte c1 in bytestr)
            {
                if (c1 < 48 || c1 > 57)
                {
                    return true;
                }
            }
            return false; 

        }
        #endregion

        #region ���浥λ
        /// <summary>
        /// ���浥λ
        /// </summary>
        /// <returns></returns>
        public bool m_blnUpdateMultiUnit()
        {
            //���µ�����
            string strMedicineId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            string strUnitName = this.m_objViewer.dtgMultiUnitList.SelectedRows[0].Cells["ColumnUnitName"].Value.ToString();
            int intPackAge = Convert.ToInt16(this.m_objViewer.dtgMultiUnitList.SelectedRows[0].Cells["ColumnPackageNum"].Value.ToString());
            int intCurruseFlag = -1;
            if (this.m_objViewer.dtgMultiUnitList.SelectedRows[0].Cells["ColumnCurruseFlag"].Value.ToString() == "��")
            {
                intCurruseFlag = 1;
            }
            else
            {
                intCurruseFlag = 0;
            }
            int intCurruseFlag_Int = intCurruseFlag;
            //ʵ����VO��ס���ֶ��и�ֵ
            clsMultiunit_drug_VO objVO = m_objGetVOFromText();

            //�жϵ�λ�Ƿ��Ѿ�����
            //if (this.m_objDomain.m_blnQueryByIndex(strMedicineId, objVO.m_strUnit, objVO.m_intPackage))
            //{
            //    MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    m_objViewer.txtPackage.Focus();
            //    m_objViewer.txtPackage.SelectAll();
            //    return false;
            //}
            //else
            //{
            //    if (this.m_objDomain.m_blnQueryByIndex(strMedicineId, objVO.m_strUnit, objVO.m_intPackage, objVO.m_intCurruseFlag_Int))
            //    {
            //        MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        m_objViewer.txtPackage.Focus();
            //        m_objViewer.txtPackage.SelectAll();
            //        return false;
            //    }
            //    else
            //    {
            //        //�����ݿ��и��µ�λ
            //        long lngRes = this.m_objDomain.m_lngUpdateMultiUnit(objVO, strMedicineId, strUnitName, intPackAge, intCurruseFlag_Int);
            //        if (lngRes > 0)
            //        {

            //            return true;
            //        }
            //        else
            //        {

            //            return false;
            //        }
            //    }
            //}

            DataGridViewRow dgvr;
            int intCount=this.m_objViewer.dtgMultiUnitList.Rows.Count;
            int intCurRow=this.m_objViewer.dtgMultiUnitList.SelectedRows[0].Cells[0].RowIndex;
            if (intCurRow != 0)
            {
                for (int i1 = 0; i1 < intCount && i1 != intCurRow; i1++)
                {
                    dgvr = this.m_objViewer.dtgMultiUnitList.Rows[i1];
                    if (objVO.m_strItemId == dgvr.Cells[0].Value.ToString() && objVO.m_strUnit == dgvr.Cells[1].Value.ToString() && objVO.m_intPackage == int.Parse(dgvr.Cells[2].Value.ToString()))
                    {
                        MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_objViewer.txtPackage.Focus();
                        m_objViewer.txtPackage.SelectAll();
                        return false;
                    }
                }
            }
            else
            {
                for (int i1 = 1; i1 < intCount; i1++)
                {
                    dgvr = this.m_objViewer.dtgMultiUnitList.Rows[i1];
                    if (objVO.m_strItemId == dgvr.Cells[0].Value.ToString() && objVO.m_strUnit == dgvr.Cells[1].Value.ToString() && objVO.m_intPackage == int.Parse(dgvr.Cells[2].Value.ToString()))
                    {
                        MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_objViewer.txtPackage.Focus();
                        m_objViewer.txtPackage.SelectAll();
                        return false;
                    }
                }
            }

            long lngRes = this.m_objDomain.m_lngUpdateMultiUnit(objVO, strMedicineId, strUnitName, intPackAge, intCurruseFlag_Int, this.m_objViewer.cboStatus.SelectedIndex);
            if (lngRes > 0)
            {

                return true;
            }
            else
            {

                return false;
            } 

            //if (this.m_objDomain.m_blnQueryByIndex(strMedicineId, objVO.m_strUnit, objVO.m_intPackage, objVO.m_intCurruseFlag_Int,this.m_objViewer.cboStatus.SelectedIndex))
            //{
            //    MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    m_objViewer.txtPackage.Focus();
            //    m_objViewer.txtPackage.SelectAll();
            //    return false;
            //} 
                //�����ݿ��и��µ�λ
        }
        #endregion

        #region �����е�λ��Ϊ�ǵ�ǰ��λ
        /// <summary>
        /// �����е�λ��Ϊ�ǵ�ǰ��λ
        /// </summary>
        /// <param name="p_strMedicineId"></param>
        /// <returns></returns>
        public long m_lngSetAllCurruseFlag_0ByItemId()
        {
            string strSeledMedId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            string strUnitName=m_objViewer.txtUnitName.Text.ToString().Trim();
            int intF = Convert.ToInt32(m_objViewer.txtPackage.Text.ToString());
            long lngRes = this.m_objDomain.m_lngSetAllCurruseFlag_0ByItemId(strSeledMedId);
            return lngRes;
            //if (m_objDomain.m_blnQueryByIndex(strSeledMedId, strUnitName, intF))
            //{
            //    MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    m_objViewer.txtPackage.Focus();
            //    m_objViewer.txtPackage.SelectAll();
            //    return -1;
            //}
            //else
            //{
            //    long lngRes = this.m_objDomain.m_lngSetAllCurruseFlag_0ByItemId(strSeledMedId);
            //    return lngRes;
            //}
        }
        #endregion

        public bool m_BlnQueryByIndex()
        {
            long lngRes = -1;
            string strSeledMedId = this.m_objViewer.dtgMedicineList.SelectedRows[0].Cells["ColumnMedicineId"].Value.ToString();
            string strUnitName = m_objViewer.txtUnitName.Text.ToString().Trim();
            int intF = Convert.ToInt32(m_objViewer.txtPackage.Text.ToString());
            if (m_objDomain.m_blnQueryByIndex(strSeledMedId, strUnitName, intF))
            {
                MessageBox.Show("��ǰҩƷ��λ���Ѵ��ڴ˵�λ��\n�����������λ��", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.txtPackage.Focus();
                m_objViewer.txtPackage.SelectAll();
                return false;
            }
            return true;
        }

    }
}
