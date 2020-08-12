using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlStorageLimitMgr ��ժҪ˵����
    /// </summary>
    public class clsControlStorageLimitMgr : com.digitalwave.GUI_Base.clsController_Base    //gui_base.dll
    {
        public clsControlStorageLimitMgr()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ô������
        /// <summary>
        /// �������
        /// </summary>
        frmStorageLimitMgr m_objViewer;
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmStorageLimitMgr)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        clsDomainControl_MedStoLimit m_objSVC = new clsDomainControl_MedStoLimit();

        /// <summary>
        /// ���浱ǰ��ѡ�еĲֿ��ҩƷ�޶�����
        /// </summary>
        DataTable dbtResultArr = new DataTable();
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        DataTable dbtMedArr = new DataTable();

        #endregion

        #region �����ʼ��
        public void m_lngResetFrm()
        {
            // m_lngFillTree();
            m_mthInitStorage();
        }


        #endregion

        #region ���ݲֿ�ID��ʾҩƷ�޶���������
        public void m_lngShowByStorageID()
        {

            m_objSVC.m_lngGetLimitByStoID(this.m_objViewer.m_cmbStorage.SelectItemValue.ToString(), this.m_objViewer.m_strStorageFlag, out dbtResultArr);
            this.m_objViewer.DgrLimit.m_mthSetDataTable(dbtResultArr);
            if (dbtResultArr.Rows.Count > 0)
            {
                this.m_objViewer.DgrLimit.CurrentCell = new DataGridCell(0, 5);
            }
        }
        #endregion

        #region ����ҩƷ�޶�
        public long m_lngAddNewLimit()
        {
            //DataRow[] drs = dbtResultArr.Select("MEDICINEID_CHR='"+this.m_objViewer.m_txtPharmName.Tag.ToString().Trim()+"' and Storageid_chr='"+this.m_objViewer.m_cmbStorage.SelectedValue.ToString().Trim()+"'");


            for (int i = 0; i < this.m_objViewer.DgrLimit.RowCount; i++)
            {
                if (this.m_objViewer.DgrLimit.m_objGetRow(i)["MEDICINEID_CHR"].ToString().Trim() == this.m_objViewer.m_txtPharmName.Tag.ToString().Trim())
                {
                    MessageBox.Show("��ҩƷ�ľ������Դ���");
                    this.m_objViewer.DgrLimit.CurrentCell = new DataGridCell(i, 0);
                    return -1;
                }
            }

            DataRow dr = dbtResultArr.NewRow();
            dr["Storageid_chr"] = this.m_objViewer.m_cmbStorage.SelectedValue.ToString().Trim();
            dr["Storagename_vchr"] = this.m_objViewer.m_cmbStorage.Text;
            dr["MEDICINEID_CHR"] = this.m_objViewer.m_txtPharmName.Tag;
            dr["Medicinename_vchr"] = this.m_objViewer.m_txtPharmName.Text;
            dr["LOWLIMIT_DEC"] = this.m_objViewer.m_txtMin.Text;
            dr["HIGHLIMIT_DEC"] = this.m_objViewer.m_txtMax.Text;
            dr["PLANQTY_DEC"] = this.m_objViewer.m_txtPlanStock.Text;
            dr["UNITID_CHR"] = this.m_objViewer.m_txtUnit.Text;
            long lngRes = 1;
            try
            {
                DataTable dtRow = dbtResultArr.Clone();
                dtRow.LoadDataRow(dr.ItemArray, true);
                dtRow.AcceptChanges();

                lngRes = this.m_objSVC.m_lngAddLimit(dtRow);
                dbtResultArr.Rows.Add(dr);
                dr.AcceptChanges();
            }
            catch
            {
                MessageBox.Show("���ӳ�����");
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion ����ҩƷ�޶�

        #region ��ȡ�ֿ���Ϣ
        public void m_mthInitStorage()
        {
            DataTable dt = new DataTable();
            this.m_objSVC.m_lngGetStorageList(out dt);
            //for (int i = 0; i < strStorageArr.Length / 3; i++)
            foreach (DataRow dr in dt.Rows)
            {
                this.m_objViewer.m_cmbStorage.Item.Add(dr[1].ToString().Trim(), dr[0].ToString().Trim());
            }
        }
        #endregion ��ȡ�ֿ���Ϣ

        #region ��������
        public void m_lngSave()
        {
            long lngRes = 1;
            for (int i1 = 0; i1 < dbtResultArr.Rows.Count; i1++)
            {
                if (dbtResultArr.Rows[i1].RowState == DataRowState.Modified)
                {
                    DataTable dtRow = dbtResultArr.Clone();
                    dtRow.LoadDataRow(dbtResultArr.Rows[i1].ItemArray, true);
                    dtRow.AcceptChanges();

                    lngRes = this.m_objSVC.m_lngUpLimit(dtRow);

                    if (lngRes < 0)
                    {
                        if (MessageBox.Show("��������Ƿ���������������ݣ�", "", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }
            clsPublicParm publiClass = new clsPublicParm();
            publiClass.m_mthShowWarning(this.m_objViewer.DgrLimit, "����ɹ�!");
            try
            {
                dbtResultArr.AcceptChanges();
            }
            catch (Exception ee)
            {
                string err = ee.Message;
            }
        }
        #endregion

        #region ɾ������
        public void m_lngDel()
        {
            if (dbtResultArr.Rows.Count > 0)
            {
                long lngRes = 0;
                lngRes = this.m_objSVC.m_lngDelLimit(dbtResultArr.Rows[this.m_objViewer.DgrLimit.CurrentCell.RowNumber]["STORAGEID_CHR"].ToString().Trim(), dbtResultArr.Rows[this.m_objViewer.DgrLimit.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim());
                if (lngRes > 0)
                {
                    dbtResultArr.Rows[this.m_objViewer.DgrLimit.CurrentCell.RowNumber].Delete();
                    dbtResultArr.AcceptChanges();
                }

            }
        }
        #endregion

        #region �ж��û�����
        public void m_lngCheckValue()
        {

            if (this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 6].ToString() != "" && this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 5].ToString() == "")
            {
                this.m_objViewer.DgrLimit.CurrentCell = new DataGridCell(this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 5);
                return;
            }
            if (this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 5].ToString() != ""
                && this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 6].ToString() != ""
                && Convert.ToDouble(this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 6].ToString()) < Convert.ToDouble(this.m_objViewer.DgrLimit[this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 5].ToString()))
            {
                MessageBox.Show("���޵����ֲ�����С�����޵�����", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.m_objViewer.DgrLimit.CurrentCell = new DataGridCell(this.m_objViewer.DgrLimit.CurrentCell.RowNumber, 6);
                return;
            }
        }
        #endregion

        public bool m_bHasChange()
        {
            if (dbtResultArr.GetChanges() == null || dbtResultArr.GetChanges().Rows.Count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
