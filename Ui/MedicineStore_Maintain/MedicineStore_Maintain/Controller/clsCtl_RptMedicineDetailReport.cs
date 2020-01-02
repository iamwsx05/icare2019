using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ControlLibrary;
using Sybase.DataWindow;
using System.Windows.Forms;
using com.digitalwave.iCare.gui.MedicineStore;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public class clsCtl_RptMedicineDetailReport : com.digitalwave.GUI_Base.clsController_Base
    {
        private frmRptMedicineDetailReport m_objViewer;
        private clsDcl_RptDetailReport m_objDomain;
        private ctlQueryMedicintLeastElement m_ctlMedQuery;
        public clsCtl_RptMedicineDetailReport()
        {
            m_objDomain = new clsDcl_RptDetailReport();
        }

        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmRptMedicineDetailReport)frmMDI_Child_Base_in;
        }

        public void m_mthInit()
        {
            this.m_objViewer.dw.LibraryList = clsPublic.PBLPath;
            if (m_objViewer.m_blnIsDS)
            {
                this.m_objViewer.dw.DataWindowObject = "ds_rptmedicinereport";
            }
            else
            {
                this.m_objViewer.dw.DataWindowObject = "ms_rptmedicinereport";
                m_objViewer.m_dgvMedDetail.Columns["patientcardid_chr"].Visible = false;
                m_objViewer.m_dgvMedDetail.Columns["lastname_vchr"].Visible = false;
            }
            this.m_objViewer.dw.InsertRow(0);
        }

        public void m_mthSearch()
        {
            if (Convert.ToString(m_objViewer.m_txtMedicineCode.Text) == string.Empty || string.IsNullOrEmpty(m_objViewer.m_txtMedicineCode.Tag.ToString()))
            {
                MessageBox.Show("����ѡ��ҩƷ��", "ע��...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_objViewer.m_txtMedicineCode.Focus();
                return;
            }
            

            DateTime dtm1, dtm2;
            if (!DateTime.TryParse(m_objViewer.m_dtpSearchBeginDate.Text, out dtm1))
            {
                MessageBox.Show("�������ѯ��ʼʱ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DateTime.TryParse(m_objViewer.m_dtpSearchEndDate.Text, out dtm2))
            {
                MessageBox.Show("�������ѯ����ʱ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                this.m_objViewer.Cursor = Cursors.WaitCursor;
                iCare.gui.HIS.clsPublic.PlayAvi("����ͳ����Ϣ�����Ժ�...");
                DataTable dt;

                long lngRes = m_objDomain.m_lngGetMedicineDetailReport(m_objViewer.m_blnIsDS, m_objViewer.m_blnIsHospital, m_objViewer.m_rbtCombine.Checked, m_objViewer.m_strStorageID, m_objViewer.m_txtMedicineCode.Tag.ToString(), dtm1, dtm2, out dt);
                if (lngRes > 0)
                {
                    this.m_objViewer.m_dgvMedDetail.DataSource = dt;

                    //double dblRetailPrice = 0d;
                    //double dblPriceSum = 0d;
                    //int iRowCount = dt.Rows.Count;
                    //DataRow drTemp = null;
                    //for (int i = 0; i < iRowCount; i++)
                    //{
                    //    drTemp = dt.Rows[i];
                    //    double.TryParse(Convert.ToString(drTemp["sumprice"]), out dblRetailPrice);
                    //    dblPriceSum += dblRetailPrice;
                    //}
                    //this.m_objViewer.m_lblPriceSum.Text = dblPriceSum.ToString("#,##0.0000") + "Ԫ";
                    //drTemp = null;

                    if (dt == null || dt.Rows.Count == 0)
                    {

                        this.m_objViewer.dw.Reset();
                        this.m_objViewer.dw.Refresh();
                        this.m_objViewer.dw.InsertRow(0);
                        iCare.gui.HIS.clsPublic.CloseAvi();
                        MessageBox.Show(this.m_objViewer, "δ���ҵ����ݡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.m_objViewer.Cursor = Cursors.Default;
                        return;
                    }

                    this.m_objViewer.dw.Reset();
                    this.m_objViewer.dw.SetRedrawOff();

                    this.m_objViewer.dw.Modify("t_hospitalname.text='" + this.m_objComInfo.m_strGetHospitalTitle() + "'");
                    this.m_objViewer.dw.Modify("t_start.text='" + dtm1.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    this.m_objViewer.dw.Modify("t_end.text='" + dtm2.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    this.m_objViewer.dw.Modify("t_storagename.text='" + m_objViewer.m_strStorageName + "'");
                    this.m_objViewer.dw.Modify("t_medicinename.text='" + m_objViewer.m_strMedicineName + "'");
                    this.m_objViewer.dw.Modify("t_spec.text='" + this.m_objViewer.m_strMedicineSpec + "'");
                    this.m_objViewer.dw.Modify("t_maker.text = '" + m_objViewer.LoginInfo.m_strEmpName + "'");
                    //this.m_objViewer.dw.Modify("t_summoney.text='" + this.m_objViewer.m_dblSumMoney.ToString("F2") + "'");

                    if (m_objViewer.m_blnIsHospital)
                    {
                        m_objViewer.dw.Modify("t_7.text = 'סԺ��'");
                    }

                    this.m_objViewer.dw.Retrieve(dt);
                    this.m_objViewer.dw.SetRedrawOn();
                    this.m_objViewer.dw.Refresh();

                    if (!dt.Columns.Contains("SortRowNo"))
                    {
                        dt.Columns.Add("SortRowNo", typeof(long));
                    }
                    m_mthAddTotalSumRow(dt);                   
                }
                else
                {
                    iCare.gui.HIS.clsPublic.CloseAvi();
                    MessageBox.Show(this.m_objViewer, "�������ݳ������顣", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.m_objViewer.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.m_objViewer.Cursor = Cursors.Default;
                iCare.gui.HIS.clsPublic.CloseAvi();

            }
        }
        
        internal void m_mthAddTotalSumRow(DataTable p_dtbResult)
        {
            if (p_dtbResult.Rows.Count > 0)
            {
                double dblTempSum = 0d;
                DataRow drAdd = p_dtbResult.NewRow();
                for (int i1 = 0; i1 < p_dtbResult.Columns.Count; i1++)
                {
                    dblTempSum = 0d;
                    if (p_dtbResult.Columns[i1].ColumnName.ToLower() == "direction")
                    {
                        drAdd[i1] = "�ϼ�";
                    }
                    else if (p_dtbResult.Columns[i1].ColumnName.ToLower() == "sumprice")
                    {
                        //drAdd[i1] = m_objViewer.m_dblSumMoney;
                    }
                    else if (p_dtbResult.Columns[i1].ColumnName.ToLower() == "inamount" || p_dtbResult.Columns[i1].ColumnName.ToLower() == "outamount"
    || p_dtbResult.Columns[i1].ColumnName.ToLower() == "insum" || p_dtbResult.Columns[i1].ColumnName.ToLower() == "outsum")
                    {
                        for (int i2 = 0; i2 < p_dtbResult.Rows.Count; i2++)
                        {
                            dblTempSum += Convert.ToDouble(p_dtbResult.Rows[i2][i1]);
                        }
                        drAdd[i1] = dblTempSum;
                    }
                }
                p_dtbResult.Rows.Add(drAdd);
                p_dtbResult.AcceptChanges();
            }
        }

        internal long m_lngGetStoreNameByID(string p_strId, out string p_strName)
        {
            return m_objDomain.m_lngGetStoreNameByID(p_strId, out p_strName);
        }

        internal long m_lngGetDeptIDByDrugID(string m_strDrugID, out string m_strDeptID)
        {
            return m_objDomain.m_lngGetDeptIDByDrugID(m_strDrugID, out m_strDeptID);
        }

        internal long m_mthGetMedBaseInfo(string m_strDeptId, out DataTable m_dtMedicine)
        {
            clsDcl_InstorageDetailReport objInDomain = new clsDcl_InstorageDetailReport();
            return objInDomain.m_lngGetBaseMedicine(m_objViewer.m_blnIsDS,true, string.Empty, m_objViewer.m_strStorageID, out m_dtMedicine);
        }

        public void m_mthShowMedince(string strMedid)
        {
            if (m_ctlMedQuery == null)
            {
                this.m_ctlMedQuery = new ctlQueryMedicintLeastElement(this.m_objViewer.m_dtMedince);
                this.m_objViewer.Controls.Add(m_ctlMedQuery);

                int X = this.m_objViewer.m_dtpSearchBeginDate.Location.X;
                int Y = this.m_objViewer.m_txtMedicineCode.Location.Y + this.m_objViewer.m_txtMedicineCode.Size.Height*2;
                m_ctlMedQuery.Location = new Point(X, Y);

                m_ctlMedQuery.ReturnInfo += new ReturnMedicineInfo(m_ctlRetureInfo);
                m_ctlMedQuery.CancelResult += new MecicineCancelAndReturn(m_ctlQueryMedicint_CancelResult);
                m_ctlMedQuery.RefreshMedicine += new RefreshMedicineInfo(m_ctlMedQuery_RefreshMedicine);
            }
            m_ctlMedQuery.Visible = true;
            m_ctlMedQuery.BringToFront();
            m_ctlMedQuery.Focus();
            m_ctlMedQuery.m_mthSetSearchText(strMedid);
        }

        private void m_ctlMedQuery_RefreshMedicine()
        {
            m_mthGetMedBaseInfo(m_objViewer.m_strStorageID, out m_objViewer.m_dtMedince);
            m_ctlMedQuery.m_dtbMedicineInfo = m_objViewer.m_dtMedince;
        }

        internal void m_ctlQueryMedicint_CancelResult()
        {
            this.m_objViewer.m_txtMedicineCode.Focus();
            m_ctlMedQuery.Visible = false;
        }

        internal void m_ctlRetureInfo(clsMS_MedicintLeastElement_VO objVO)
        {
            if (m_objViewer.m_rbtCombine.Checked)
            {
                m_objViewer.m_txtMedicineCode.Text = objVO.m_strMedicineCode;
                this.m_objViewer.m_txtMedicineCode.Tag = objVO.m_strMedicineCode;
            }
            else
            {
                this.m_objViewer.m_txtMedicineCode.Text = objVO.m_strMedicineName + "\t"+objVO.m_strMedicineSpec+"\t"+objVO.m_strManufacturer;
                this.m_objViewer.m_txtMedicineCode.Tag = objVO.m_strMedicineID;
            }            
            
            m_objViewer.m_strMedicineName = objVO.m_strMedicineName;
            m_objViewer.m_strMedicineSpec = objVO.m_strMedicineSpec;
        }


        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="m_dtStorageName"></param>
        internal void m_mthGetStorageName(out DataTable m_dtStorageName)
        {
            clsDcl_RptInstorageBill objDomain = new clsDcl_RptInstorageBill();
            if (m_objViewer.m_blnIsDS)
            {
                objDomain.m_mthGetStorageName(0, out m_dtStorageName);
            }
            else
            {
                objDomain.m_mthGetStorageName(1, out m_dtStorageName);
            }
        }
        #endregion

        internal long m_lngCheckIsHospital(string p_strDrugStoreID, out bool p_blnIsHospital)
        {
            return m_objDomain.m_lngCheckIsHospital(p_strDrugStoreID, out p_blnIsHospital);
        }
    }
}
