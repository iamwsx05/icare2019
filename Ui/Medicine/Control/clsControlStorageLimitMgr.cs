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
    /// clsControlStorageLimitMgr 的摘要说明。
    /// </summary>
    public class clsControlStorageLimitMgr : com.digitalwave.GUI_Base.clsController_Base    //gui_base.dll
    {
        public clsControlStorageLimitMgr()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 设置窗体对象
        /// <summary>
        /// 窗体对象
        /// </summary>
        frmStorageLimitMgr m_objViewer;
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmStorageLimitMgr)frmMDI_Child_Base_in;
        }
        #endregion

        #region 变量
        clsDomainControl_MedStoLimit m_objSVC = new clsDomainControl_MedStoLimit();

        /// <summary>
        /// 保存当前所选中的仓库的药品限额数据
        /// </summary>
        DataTable dbtResultArr = new DataTable();
        /// <summary>
        /// 保存药品资料
        /// </summary>
        DataTable dbtMedArr = new DataTable();

        #endregion

        #region 窗体初始化
        public void m_lngResetFrm()
        {
            // m_lngFillTree();
            m_mthInitStorage();
        }


        #endregion

        #region 根据仓库ID显示药品限额设置资料
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

        #region 增加药品限额
        public long m_lngAddNewLimit()
        {
            //DataRow[] drs = dbtResultArr.Select("MEDICINEID_CHR='"+this.m_objViewer.m_txtPharmName.Tag.ToString().Trim()+"' and Storageid_chr='"+this.m_objViewer.m_cmbStorage.SelectedValue.ToString().Trim()+"'");


            for (int i = 0; i < this.m_objViewer.DgrLimit.RowCount; i++)
            {
                if (this.m_objViewer.DgrLimit.m_objGetRow(i)["MEDICINEID_CHR"].ToString().Trim() == this.m_objViewer.m_txtPharmName.Tag.ToString().Trim())
                {
                    MessageBox.Show("该药品的警戒线以存在");
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
                MessageBox.Show("增加出错了");
                lngRes = -1;
            }
            return lngRes;
        }
        #endregion 增加药品限额

        #region 获取仓库信息
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
        #endregion 获取仓库信息

        #region 保存数据
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
                        if (MessageBox.Show("保存出错，是否继续保存后面的数据？", "", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }
            clsPublicParm publiClass = new clsPublicParm();
            publiClass.m_mthShowWarning(this.m_objViewer.DgrLimit, "保存成功!");
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

        #region 删除数据
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

        #region 判断用户输入
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
                MessageBox.Show("上限的数字不可以小于下限的数字", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
