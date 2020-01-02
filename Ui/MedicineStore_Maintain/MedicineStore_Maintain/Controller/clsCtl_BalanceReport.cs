
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
    /// <summary>
    /// �̵���˱�
    /// </summary>
    public class clsCtl_BalanceReport : com.digitalwave.GUI_Base.clsController_Base
    {
        private frmBalanceReport m_objViewer;
        private clsDcl_BalanceReport m_objDomain;
        private DataView dtvMedType;
        private ctlQueryMedicintLeastElement m_ctlMedQuery;
        private DataTable dt;
        DataTable dtbResult = new DataTable();//���ݿⷵ�صĽ����
        DataTable dtbTrueResult = null;
        private string m_strCurrentSelectedPeriod = string.Empty;//��ǰѡ��������
        private DataTable m_dtbRawResult;//����˴ν�����粻�ı������ڣ��´β�ѯֱ��ȡ���ֵ
        private DateTime m_dtStartDate;
        private DateTime m_dtEndDate;
        private DataTable m_dtbRecipeDetail = new DataTable();//������ϸ
        private string m_strType = string.Empty;

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public clsCtl_BalanceReport()
        {
            m_objDomain = new clsDcl_BalanceReport();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmBalanceReport)frmMDI_Child_Base_in;
        }
        #endregion


        internal void m_mthInit()
        {
            clsDcl_OutStorageDetailReport objTmp = new clsDcl_OutStorageDetailReport();
            DataTable dtRoom;
            DataTable dtRoomToid = new DataTable();
            DataTable dtVonder;
            DataTable dtMedType;
            long lngRes = objTmp.m_lngGetExptypeAndVendor(m_objViewer.m_blnForDrugStore, out dtRoom, out dtVonder, out dtMedType);
            this.dtvMedType = new DataView(dtMedType);
            clsColumns_VO[] column2 = new clsColumns_VO[] { new clsColumns_VO("�������", "medicinetypename_vchr", HorizontalAlignment.Left, 145) };
            this.m_objViewer.txtTypecode.m_mthInitListView(column2);
            this.m_objViewer.txtTypecode.m_dtbDataSourse = null;

            if (m_objViewer.m_strRoomidArr.Length > 0)
            {
                if (dtRoom.Rows.Count > 0)
                {
                    dtRoomToid = dtRoom.Clone();
                    DataRow dr = null;
                    int iRowCount = dtRoom.Rows.Count;
                    int iLength = m_objViewer.m_strRoomidArr.Length;
                    dtRoomToid.BeginLoadData();

                    for (int i = 0; i < iLength; i++)
                    {
                        for (int j = 0; j < iRowCount; j++)
                        {
                            dr = dtRoom.Rows[j];
                            if (m_objViewer.m_strRoomidArr[i].ToString().Trim() == dr["medicineroomid"].ToString().Trim())
                            {
                                dtRoomToid.LoadDataRow(dr.ItemArray, true);
                            }
                        }
                    }
                    dtRoomToid.EndLoadData();
                    dtRoomToid.AcceptChanges();
                }
            }

            clsColumns_VO[] column3 = new clsColumns_VO[] { new clsColumns_VO("�ⷿ����", "medicineroomname", HorizontalAlignment.Left, 145) };
            this.m_objViewer.txtStoreroom.m_mthInitListView(column3);
            if (m_objViewer.m_strRoomidArr.Length > 0)
            {
                this.m_objViewer.txtStoreroom.m_dtbDataSourse = dtRoomToid;
            }
            else
            {
                this.m_objViewer.txtStoreroom.m_dtbDataSourse = dtRoom;
            }
            this.m_objViewer.txtStoreroom.m_mthFillData();
            if (m_objViewer.m_strRoomidArr.Length > 0)
            {
                DataRow drRoom = null;
                for (int iRow = 0; iRow < dtRoom.Rows.Count; iRow++)
                {
                    drRoom = dtRoom.Rows[iRow];
                    if (m_objViewer.m_strRoomidArr[0].ToString().Trim() == drRoom["medicineroomid"].ToString().Trim())
                    {
                        this.m_objViewer.txtStoreroom.Text = drRoom["medicineroomname"].ToString().Trim();
                        this.m_objViewer.txtStoreroom.Value = drRoom["medicineroomid"].ToString().Trim();
                        break;
                    }
                }
            }
            else
            {
                this.m_objViewer.txtStoreroom.Text = dtRoom.Rows[0]["medicineroomname"].ToString().Trim();
                this.m_objViewer.txtStoreroom.Value = dtRoom.Rows[0]["medicineroomid"].ToString().Trim();
            }
            m_objDomain.m_lngGetBillType(m_objViewer.m_blnForDrugStore, out m_strType);
        }

        internal void m_mthGetMedicine()
        {
            string strMedid = string.Empty;
            dt = new DataTable();
            clsDcl_InstorageDetailReport objTemp = new clsDcl_InstorageDetailReport();
            long lngRes = objTemp.m_lngGetBaseMedicine(m_objViewer.m_blnForDrugStore,true,this .m_objViewer .m_strLastStorageID, this.m_objViewer.txtStoreroom.Value.ToString(), out dt);
        }

        public void m_mthFillMedType()
        {
            this.m_objViewer.txtTypecode.m_listView.Items.Clear();
            if (string.IsNullOrEmpty(this.m_objViewer.txtStoreroom.Value))
            {
                return;
            }
            this.dtvMedType.RowFilter = "medicineroomid='" + this.m_objViewer.txtStoreroom.Value.ToString() + "'";

            DataTable dtValue = dtvMedType.ToTable();
            DataRow drTmp = dtValue.NewRow();
            drTmp["medicinetypeid_chr"] = "";
            drTmp["medicinetypename_vchr"] = "ȫ��";
            drTmp["medicineroomid"] = "-1";
            dtValue.BeginLoadData();
            dtValue.Rows.Add(drTmp);
            dtValue.EndLoadData();

            this.m_objViewer.txtTypecode.m_dtbDataSourse = dtValue;
            this.m_objViewer.txtTypecode.m_mthFillData();
        }

        public void m_mthShowMedince(string strMedid)
        {
            if (string.IsNullOrEmpty(this.m_objViewer.txtStoreroom.Value))
            {
                MessageBox.Show(this.m_objViewer, "����ѡ��ⷿ��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (m_ctlMedQuery == null)
            {
                this.m_objViewer.m_dtMedince = dt;
                this.m_ctlMedQuery = new ctlQueryMedicintLeastElement(dt);
                this.m_objViewer.Controls.Add(m_ctlMedQuery);

                int X = this.m_objViewer.m_txtMedicineCode.Location.X - 140;
                int Y = this.m_objViewer.m_txtMedicineCode.Location.Y + this.m_objViewer.m_txtMedicineCode.Size.Height+48;
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

        internal void m_ctlMedQuery_RefreshMedicine()
        {
            m_mthGetMedicine();
            this.m_objViewer.m_dtMedince = dt;
            m_ctlMedQuery.m_dtbMedicineInfo = dt;
        }

        internal void m_ctlQueryMedicint_CancelResult()
        {
            this.m_objViewer.m_txtMedicineCode.Focus();
            m_ctlMedQuery.Visible = false;
        }

        internal void m_ctlRetureInfo(clsMS_MedicintLeastElement_VO objVO)
        {
            this.m_objViewer.m_txtMedicineCode.Text = objVO.m_strMedicineName;
            this.m_objViewer.m_txtMedicineCode.Tag = objVO.m_strMedicineID;
        }

        internal void m_mthSearch()
        {
            #region ����
            //if (Convert.ToString(this.m_objViewer.txtStoreroom.Value) == string.Empty)
            //{
            //    MessageBox.Show("����ѡ��ⷿ��", "����...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    this.m_objViewer.txtStoreroom.Focus();
            //    return;
            //}
            //if ((Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text)) > (Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text)))
            //{
            //    MessageBox.Show("��ʼ���ڱ���С�ڽ������ڣ�", "����...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    m_objViewer.m_dtpSearchBeginDate.Focus();
            //    return;
            //}
            //m_mthGetAccount();            
            //long lngRes = m_objDomain.m_lngGetBalance(m_objViewer.txtStoreroom.Value, out dtbResult);
            //if (lngRes > 0)
            //{
            //    m_mthFilterResult();
            //}
            //m_objViewer.m_dgvBalance.DataSource = dtbResult;
            //m_objViewer.m_dgvBalance.Refresh();
            #endregion            

            if (m_objViewer.m_blnForDrugStore)
            {
                if (m_objViewer.m_txtAccountID.Text == m_strCurrentSelectedPeriod)
                {
                    m_mthFilterResultDetailForDrugStore(m_dtbRawResult, m_dtbRecipeDetail);
                }
                else
                {
                    m_dtStartDate = m_objViewer.AccouVO.m_dtmSTARTTIME_DAT;
                    m_dtEndDate = m_objViewer.AccouVO.m_dtmTRANSFERTIME_DAT;
                    long lngRes = m_objDomain.m_lngGetBalanceDetailForDrugStore(m_objViewer.txtStoreroom.Value,m_objViewer.m_txtAccountID.Text,m_objViewer.m_strLastStorageID,m_dtStartDate,m_dtEndDate, out dtbResult);
                    if (lngRes > 0)
                    {
                        m_objDomain.m_lngGetRecipeDetail(m_objViewer.txtStoreroom.Value, m_dtStartDate, m_dtEndDate, out m_dtbRecipeDetail);
                        m_dtbRecipeDetail.PrimaryKey = new DataColumn[] { m_dtbRecipeDetail.Columns["medicineid_chr"] };
                        m_strCurrentSelectedPeriod = m_objViewer.m_txtAccountID.Text;
                        m_dtbRawResult = dtbResult.Copy();
                        m_mthFilterResultDetailForDrugStore(m_dtbRawResult, m_dtbRecipeDetail);
                    }
                }
                if (dtbTrueResult == null || dtbTrueResult.Rows.Count == 0)
                {
                    MessageBox.Show("û�в�ѯ������", "ע��...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                m_objViewer.m_dgvBalance.DataSource = dtbTrueResult;
                m_objViewer.m_dgvBalance.Refresh();
            }
            else
            {
                if (m_objViewer.m_txtAccountID.Text == m_strCurrentSelectedPeriod)
                {
                    m_mthFilterResultDetail(m_dtbRawResult);
                }
                else
                {
                    long lngRes = m_objDomain.m_lngGetBalanceDetail(m_objViewer.txtStoreroom.Value, out dtbResult);
                    if (lngRes > 0)
                    {
                        m_dtStartDate = m_objViewer.AccouVO.m_dtmSTARTTIME_DAT;
                        m_dtEndDate = m_objViewer.AccouVO.m_dtmTRANSFERTIME_DAT;
                        m_strCurrentSelectedPeriod = m_objViewer.m_txtAccountID.Text;
                        m_dtbRawResult = dtbResult.Copy();
                        m_mthFilterResultDetail(m_dtbRawResult);
                    }
                }
                if (dtbTrueResult == null || dtbTrueResult.Rows.Count == 0)
                {
                    MessageBox.Show("û�в�ѯ������", "ע��...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                m_objViewer.m_dgvBalance.DataSource = dtbTrueResult;
                m_objViewer.m_dgvBalance.Refresh();

            }
        }
        /*  type_int 1��⡢2���⡢3��ʼ����0���� */
        //ʵ����ĩ�� Ӧ��ȥ ������ 20080625
        private void m_mthFilterResultDetailForDrugStore(DataTable p_dtResult, DataTable p_dtbRecipeDetail)
        {
            string str = "";
            int i = 0;
            try
            {
               
                if (p_dtResult == null)
                {
                    return;
                }
                if (dtbTrueResult == null || !(dtbTrueResult.IsInitialized))
                {
                    m_mthInitialDataTable();
                }

                DataView dvTemp = p_dtResult.DefaultView;
                dvTemp.RowFilter = string.Empty;
                if (m_objViewer.m_txtMedicineCode.Text.Trim() != string.Empty && Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) != string.Empty)
                {
                    dvTemp.RowFilter = "medicineid_chr = '" + Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) + "'";
                }
                if (!string.IsNullOrEmpty(m_objViewer.txtTypecode.Value) && m_objViewer.txtTypecode.Value != "-1")
                {
                    if (dvTemp.RowFilter == string.Empty)
                        dvTemp.RowFilter = "medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
                    else
                        dvTemp.RowFilter += " and medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
                }
                p_dtResult = dvTemp.ToTable();

                if (p_dtResult.Rows.Count > 0)
                {
                    string p_strMedicineId = string.Empty;
                    dtbTrueResult.Clear();

                    double p_dblRetailPrice = 0d;//���۵���
                    double p_dblOPAmount = 0d;//��������
                    double p_dblIPAmount = 0d;//��С����
                    double p_dblPackQty = 0d;//��װ��
                    double p_dblNewPrice = 0d;//���ۺ�۸�
                    double p_dblSum = 0d;//���
                    double p_dblEndOPAmount = 0d;//��ĩ����
                    double p_dblEndIPAmount = 0d;//��ĩ����
                    double p_dblEndRetailPrice = 0d;//��ĩ����
                    double p_dblEndSum = 0d;//��ĩ���


                    string strProcessType = string.Empty;//��������
                    DataRow p_drRow = null;
                    DataRow p_drNew = null;
                    DataRow drRecipe = null;
                    if (m_objViewer.m_txtAccountID.Text != "δ��ת")//��ѯ�ѽ�ת�����ڵ�����
                    {
                        if (string.IsNullOrEmpty(m_objViewer.m_strLastStorageID))//��һ��������Ϊ�գ�����ת�ĵ�һ��
                        {
                            for (int i1 = 0; i1 < p_dtResult.Rows.Count; i1++)
                            {
                                p_drRow = p_dtResult.Rows[i1];
                                str = p_drRow["medicineid_chr"].ToString();
                                i = i1;
                                if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                                {
                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    //����������
                                    if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text))
                                        || ((Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate))))
                                    {
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                        p_drNew["putamount"] = 0;
                                        p_drNew["putsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;

                                        drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                        if (drRecipe != null)
                                        {
                                            p_drNew["recipeamount"] = drRecipe["amount_int"];
                                            p_drNew["recipesum"] = drRecipe["sum_int"];
                                        }
                                        else
                                        {
                                            p_drNew["recipeamount"] = 0;
                                            p_drNew["recipesum"] = 0;
                                        }

                                        p_drNew["realendamount"] = 0;
                                        p_drNew["realendsum"] = 0;
                                        p_drNew["realendamountdiff"] = 0 - Convert.ToDouble(p_drNew["recipeamount"]); ;
                                        p_drNew["realendsumdiff"] = 0 - Convert.ToDouble(p_drNew["recipesum"]);
                                        p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                        p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                        p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                        p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                        dtbTrueResult.Rows.Add(p_drNew);
                                        continue;
                                    }

                                    if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                    {
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                    }
                                    else if (Convert.ToInt16(p_drRow["isend_int"]) == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {

                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                        }
                                        //����
                                        if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = p_dblOPAmount;
                                            p_drNew["adjustsum"] = p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 3)//�ڳ���
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = p_dblOPAmount;
                                            p_drNew["startsum"] = p_dblSum;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                        {
                                            p_drNew["inamount"] = p_dblOPAmount;
                                            p_drNew["insum"] = p_dblSum;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = p_dblOPAmount;
                                            p_drNew["outsum"] = p_dblSum;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }                                        
                                    }
                                    else if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                    {                                        
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��          
                                            if (p_dblPackQty == 0)
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                            }
                                            double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                            if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                            {
                                                
                                            }
                                            else//��С��λ
                                            {
                                                p_dblEndOPAmount = p_dblEndIPAmount;
                                            }
                                            p_drNew["endamount"] = p_dblEndOPAmount;
                                            p_drNew["endsum"] = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        }
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                    }

                                    drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                    if (drRecipe != null)
                                    {
                                        p_drNew["recipeamount"] = drRecipe["amount_int"];
                                        p_drNew["recipesum"] = drRecipe["sum_int"];
                                    }
                                    else
                                    {
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                    }
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;

                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]) - Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]) - Convert.ToDouble(p_drNew["recipesum"]);

                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                }
                                else
                                {
                                    //������¼                                
                                    if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text)
                                    {
                                        continue;
                                    }

                                    if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                    {
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                            if (p_dblPackQty == 0)
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                            }
                                            double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                            if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                            {                                                
                                            }
                                            else//��С��λ
                                            {
                                                p_dblEndOPAmount = p_dblEndIPAmount;
                                            }
                                            p_drNew["endamount"] = Convert.ToDouble(p_drNew["endamount"]) + p_dblEndOPAmount;
                                            p_drNew["endsum"] = Convert.ToDouble(p_drNew["endsum"]) + p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        }
                                    }
                                    if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate)
                                    {
                                        continue;
                                    }       

                                    if (Convert.ToInt16(p_drRow["isend_int"]) == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {

                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                        }
                                        //����
                                        if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                        {
                                            p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblOPAmount;
                                            p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 3)
                                        {
                                            p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblOPAmount;
                                            p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblSum;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                        {
                                            p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblOPAmount;
                                            p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblSum;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                        {
                                            p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblOPAmount;
                                            p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblSum;
                                        }
                                    }
                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);
                                }
                            }
                        }
                        else//��һ�ڲ�Ϊ��
                        {
                            for (int i1 = 0; i1 < p_dtResult.Rows.Count; i1++)
                            {
                                p_drRow = p_dtResult.Rows[i1];
                                str = p_drRow["medicineid_chr"].ToString();
                                i = i1;
                                if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                                {
                                    //����������
                                    if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text))
                                        || ((Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate))))
                                    {
                                        p_dblOPAmount = 0;
                                        p_dblRetailPrice = 0;
                                        p_dblPackQty = 0d;//��װ��
                                        p_dblNewPrice = 0d;//���ۺ�۸�

                                        //ȡ���ڳ���
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                        {
                                            if (Convert.ToString(p_drRow["isend_int"]) != "" && p_drRow["isend_int"].ToString() == "1")
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                                if (p_dblPackQty == 0)
                                                {
                                                    double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                }
                                                if (p_dblPackQty == 0)
                                                {
                                                    double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                }
                                                double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                                if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                                {
                                                    
                                                }
                                                else//��С��λ
                                                {
                                                    p_dblEndOPAmount = p_dblEndIPAmount;                                                   
                                                }
                                            }
                                        }
                                        p_drNew = dtbTrueResult.NewRow();
                                        p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                        p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                        p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                        p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                        p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;

                                        drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                        if (drRecipe != null)
                                        {
                                            p_drNew["recipeamount"] = drRecipe["amount_int"];
                                            p_drNew["recipesum"] = drRecipe["sum_int"];
                                        }
                                        else
                                        {
                                            p_drNew["recipeamount"] = 0;
                                            p_drNew["recipesum"] = 0;
                                        }

                                        p_drNew["putamount"] = 0;
                                        p_drNew["putsum"] = 0;
                                        p_drNew["startamount"] = p_dblEndOPAmount;
                                        if (p_dblPackQty == 0 || p_dblEndIPAmount == 0 || p_dblEndRetailPrice == 0)
                                        {
                                            p_drNew["startsum"] = 0;
                                        }
                                        else
                                        {
                                            p_drNew["startsum"] = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        }
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        p_drNew["realendamount"] = 0;
                                        p_drNew["realendsum"] = 0;
                                        p_drNew["realendamountdiff"] = 0 - Convert.ToDouble(p_drNew["recipeamount"]);
                                        p_drNew["realendsumdiff"] = 0 - Convert.ToDouble(p_drNew["recipesum"]);
                                        p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                        p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                        p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                        p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                        dtbTrueResult.Rows.Add(p_drNew);
                                        continue;
                                    }

                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];                                    
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    //ȡ��������ĩ����Ϊ�����ڳ���
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                    {
                                        if (Convert.ToString(p_drRow["isend_int"]) != "" && p_drRow["isend_int"].ToString() == "1")
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                            if (p_dblPackQty == 0)
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                            }
                                            double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                            if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                            {

                                            }
                                            else//��С��λ
                                            {
                                                p_dblEndOPAmount = p_dblEndIPAmount;
                                            }
                                        }
                                        p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                    }
                                    p_drNew["startamount"] = p_dblEndOPAmount;
                                    if (p_dblPackQty == 0 || p_dblEndIPAmount == 0 || p_dblEndRetailPrice == 0)
                                    {
                                        p_drNew["startsum"] = 0;
                                    }
                                    else
                                    {
                                        p_drNew["startsum"] = p_dblEndSum;
                                    }
                                    if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                    {
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                    }
                                    else if (Convert.ToInt16(p_drRow["isend_int"]) == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {
                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                        }
                                        //����
                                        if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = p_dblOPAmount;
                                            p_drNew["adjustsum"] = p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 3)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                        {
                                            p_drNew["inamount"] = p_dblOPAmount;
                                            p_drNew["insum"] = p_dblSum;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = p_dblOPAmount;
                                            p_drNew["outsum"] = p_dblSum;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["endamount"] = 0;
                                            p_drNew["endsum"] = 0;
                                        }                                        
                                    }
                                    else if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                    {
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                        {
                                            if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                                if (p_dblPackQty == 0)
                                                {
                                                    double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                }
                                                double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                            }
                                            else//��С��λ
                                            {
                                                p_dblEndOPAmount = p_dblEndIPAmount;                                               
                                            }
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["endamount"] = p_dblEndOPAmount;
                                            p_drNew["endsum"] = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        }
                                    }

                                    drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                    if (drRecipe != null)
                                    {
                                        p_drNew["recipeamount"] = drRecipe["amount_int"];
                                        p_drNew["recipesum"] = drRecipe["sum_int"];
                                    }
                                    else
                                    {
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                    }
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;

                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);

                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                }
                                else
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text)
                                    {
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                        {
                                            if (Convert.ToString(p_drRow["isend_int"]) != "" && p_drRow["isend_int"].ToString() == "1")//ȡ���ڳ���
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblIPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                                if (p_dblPackQty == 0)
                                                {
                                                    double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                }
                                                double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                                if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                                {
                                                    
                                                }
                                                else//��С��λ
                                                {
                                                    p_dblEndOPAmount = p_dblIPAmount;
                                                }
                                                p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblEndOPAmount;
                                                p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                            }
                                            p_dblOPAmount = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                            p_dblRetailPrice = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);

                                            p_drNew["realendamount"] = p_dblOPAmount;// Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                            p_drNew["realendsum"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                            p_dblOPAmount = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                            p_dblRetailPrice = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);
                                            p_drNew["realendamountdiff"] = p_dblOPAmount;//Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                            p_drNew["realendsumdiff"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                                        }
                                        continue;
                                    }

                                    if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate))
                                    {
                                        continue;
                                    }

                                    if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                    {
                                    }
                                    else if (Convert.ToInt16(p_drRow["isend_int"]) == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {
                                            
                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                        }

                                        //����
                                        if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                        {
                                            p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblOPAmount;
                                            p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                        {
                                            p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblOPAmount;
                                            p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                        {
                                            p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblOPAmount;
                                            p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        }                                           
                                    }
                                    else if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                    {
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                            double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                            if (p_dblPackQty == 0)
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                            }
                                            double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                            if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                            {
                                                
                                            }
                                            else//��С��λ
                                            {
                                                p_dblEndOPAmount = p_dblEndIPAmount;
                                            }
                                            p_drNew["endamount"] = Convert.ToDouble(p_drNew["endamount"]) + p_dblEndOPAmount;
                                            p_drNew["endsum"] = Convert.ToDouble(p_drNew["endsum"]) + p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        }
                                    }

                                    p_dblOPAmount = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_dblRetailPrice = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);

                                    p_drNew["realendamount"] = p_dblOPAmount;// Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_dblOPAmount = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_dblRetailPrice = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);
                                    p_drNew["realendamountdiff"] = p_dblOPAmount;//Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                    p_drNew["realendsumdiff"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                                }
                            }
                        }
                    }
                    else//��ѯδ��ת������
                    {
                        if (string.IsNullOrEmpty(m_objViewer.m_strLastStorageID))//��һ��������Ϊ�գ���δ��ת���ǵ�һ��
                        {
                            for (int i1 = 0; i1 < p_dtResult.Rows.Count; i1++)
                            {
                                p_drRow = p_dtResult.Rows[i1];
                                str = p_drRow["medicineid_chr"].ToString();
                                i = i1;
                                if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                                {
                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];                                    
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                    {
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                    }
                                    else
                                    {
                                        //��ĩ��Ϊ��ǰ���
                                        double.TryParse(Convert.ToString(p_drRow["oprealgross_int"]), out p_dblEndOPAmount);//��ĩ����
                                        double.TryParse(Convert.ToString(p_drRow["iprealgross_int"]), out p_dblEndIPAmount);//��ĩ����
                                        double.TryParse(Convert.ToString(p_drRow["oprealprice"]), out p_dblEndRetailPrice);//����

                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {
                                            p_drNew["endamount"] = p_dblEndOPAmount;
                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                            p_drNew["endamount"] = p_dblEndIPAmount;
                                        }
                                        p_drNew["endsum"] = p_dblEndSum;

                                        //����
                                        if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = p_dblOPAmount;
                                            p_drNew["adjustsum"] = p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                        }                                        
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                        {
                                            p_drNew["inamount"] = p_dblOPAmount;
                                            p_drNew["insum"] = p_dblSum;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = p_dblOPAmount;
                                            p_drNew["outsum"] = p_dblSum;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;
                                        }
                                        else if (Convert.ToInt16(p_drRow["type_int"]) == 3)//ȡ���ڳ���
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = p_dblOPAmount;
                                            p_drNew["startsum"] = p_dblSum;
                                        }
                                    }

                                    drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                    if (drRecipe != null)
                                    {
                                        p_drNew["recipeamount"] = drRecipe["amount_int"];
                                        p_drNew["recipesum"] = drRecipe["sum_int"];
                                    }
                                    else
                                    {
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                    }
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;

                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);

                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                }
                                else
                                {
                                    double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                    double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                    double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                    if (p_dblPackQty == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                    }
                                    double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                    double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                    p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                    if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                    {                                        
                                    }
                                    else//��С��λ
                                    {                                       
                                        p_dblOPAmount = p_dblIPAmount;                                      
                                    }                                   

                                    //����
                                    if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                    {
                                        p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblOPAmount;
                                        p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                    }
                                    else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                    {
                                        p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblOPAmount;
                                        p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblSum;
                                    }
                                    else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                    {
                                        p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblOPAmount;
                                        p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblSum;
                                    }
                                    else if (Convert.ToInt16(p_drRow["type_int"]) == 3)
                                    {
                                        p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblOPAmount;
                                        p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblSum;
                                    }

                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);
                                }
                            }
                        }
                        else//���н�ת��¼
                        {
                            for (int i1 = 0; i1 < p_dtResult.Rows.Count; i1++)
                            {
                                p_drRow = p_dtResult.Rows[i1];
                                str = p_drRow["medicineid_chr"].ToString();
                                i = i1;
                                if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                                {
                                    //���������ڻ�δ��ת�ļ�¼��
                                    if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_strLastStorageID))
                                        || ((Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate)))
                                    {
                                        p_drNew = dtbTrueResult.NewRow();
                                        p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                        p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                        p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                        p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                        p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                        p_drNew["putamount"] = 0;
                                        p_drNew["putsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        //ȡ����ĩ�� = �����
                                        double.TryParse(Convert.ToString(p_drRow["oprealgross_int"]), out p_dblEndOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["iprealgross_int"]), out p_dblEndIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["oprealprice"]), out p_dblEndRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {
                                            p_drNew["endamount"] = p_dblEndOPAmount;                                           
                                        }
                                        else//��С��λ
                                        {
                                            p_drNew["endamount"] = p_dblEndIPAmount;
                                        }
                                        p_drNew["endsum"] = p_dblEndSum;

                                        drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                        if (drRecipe != null)
                                        {
                                            p_drNew["recipeamount"] = drRecipe["amount_int"];
                                            p_drNew["recipesum"] = drRecipe["sum_int"];
                                        }
                                        else
                                        {
                                            p_drNew["recipeamount"] = 0;
                                            p_drNew["recipesum"] = 0;
                                        }

                                        //p_drNew["realendamount"] = 0;
                                        //p_drNew["realendsum"] = 0;
                                        //p_drNew["realendamountdiff"] = 0;
                                        //p_drNew["realendsumdiff"] = 0;

                                        p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                        p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                        p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                        p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);

                                        p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                        p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                        p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                        p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                        dtbTrueResult.Rows.Add(p_drNew);
                                        continue;
                                    }

                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    double.TryParse(Convert.ToString(p_drRow["oprealgross_int"]), out p_dblEndOPAmount);//��ĩ����
                                    double.TryParse(Convert.ToString(p_drRow["iprealgross_int"]), out p_dblEndIPAmount);//��ĩ����
                                    double.TryParse(Convert.ToString(p_drRow["oprealprice"]), out p_dblEndRetailPrice);//����
                                    double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                    if (p_dblPackQty == 0)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                    }
                                    p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;

                                    double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                    double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                    double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                    double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                    
                                    p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty; 
                                    if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                    {                                        
                                        p_drNew["endamount"] = p_dblEndOPAmount;   
                                    }
                                    else//��С��λ
                                    {
                                        p_dblOPAmount = p_dblIPAmount;                                        
                                        p_drNew["endamount"] = p_dblEndIPAmount;
                                        
                                    }
                                    p_drNew["endsum"] = p_dblEndSum;                                    

                                    if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                    {
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;                                        
                                    }
                                    else
                                    {
                                        if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtStartDate)
                                        {
                                            //����
                                            if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                            {
                                                p_drNew["inamount"] = 0;
                                                p_drNew["insum"] = 0;
                                                p_drNew["outamount"] = 0;
                                                p_drNew["outsum"] = 0;
                                                p_drNew["adjustamount"] = p_dblOPAmount;
                                                p_drNew["adjustsum"] = p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                                p_drNew["startamount"] = 0;
                                                p_drNew["startsum"] = 0;                                                
                                            }
                                            else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                            {
                                                p_drNew["inamount"] = p_dblOPAmount;
                                                p_drNew["insum"] = p_dblSum;
                                                p_drNew["outamount"] = 0;
                                                p_drNew["outsum"] = 0;
                                                p_drNew["adjustamount"] = 0;
                                                p_drNew["adjustsum"] = 0;
                                                p_drNew["startamount"] = 0;
                                                p_drNew["startsum"] = 0;
                                            }
                                            else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                            {
                                                p_drNew["inamount"] = 0;
                                                p_drNew["insum"] = 0;
                                                p_drNew["outamount"] = p_dblOPAmount;
                                                p_drNew["outsum"] = p_dblSum;
                                                p_drNew["adjustamount"] = 0;
                                                p_drNew["adjustsum"] = 0;
                                                p_drNew["startamount"] = 0;
                                                p_drNew["startsum"] = 0;
                                            }
                                            else if (Convert.ToInt16(p_drRow["type_int"]) == 3)//�ڳ���
                                            {
                                                //ȡ���ڳ���
                                                if (p_drRow["isend_int"].ToString() == "1")
                                                {
                                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                                    {
                                                        double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                                        double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                                        if (p_dblPackQty == 0)
                                                        {
                                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                        }
                                                        double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                                        p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                                        {
                                                        }
                                                        else//��С��λ
                                                        {
                                                            p_dblEndOPAmount = p_dblEndIPAmount;
                                                        }

                                                        p_drNew["startamount"] = p_dblEndOPAmount;
                                                        p_drNew["startsum"] = p_dblEndSum;
                                                        p_drNew["inamount"] = 0;
                                                        p_drNew["insum"] = 0;
                                                        p_drNew["outamount"] = 0;
                                                        p_drNew["outsum"] = 0;
                                                        p_drNew["adjustamount"] = 0;
                                                        p_drNew["adjustsum"] = 0;
                                                    }
                                                    else
                                                    {
                                                        p_drNew["inamount"] = 0;
                                                        p_drNew["insum"] = 0;
                                                        p_drNew["outamount"] = 0;
                                                        p_drNew["outsum"] = 0;
                                                        p_drNew["adjustamount"] = 0;
                                                        p_drNew["adjustsum"] = 0;
                                                        p_drNew["startamount"] = 0;
                                                        p_drNew["startsum"] = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    p_drNew["inamount"] = 0;
                                                    p_drNew["insum"] = 0;
                                                    p_drNew["outamount"] = 0;
                                                    p_drNew["outsum"] = 0;
                                                    p_drNew["adjustamount"] = 0;
                                                    p_drNew["adjustsum"] = 0;
                                                    p_drNew["startamount"] = 0;
                                                    p_drNew["startsum"] = 0;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            p_drNew["inamount"] = 0;
                                            p_drNew["insum"] = 0;
                                            p_drNew["outamount"] = 0;
                                            p_drNew["outsum"] = 0;
                                            p_drNew["adjustamount"] = 0;
                                            p_drNew["adjustsum"] = 0;
                                            p_drNew["startamount"] = 0;
                                            p_drNew["startsum"] = 0;                                       
                                        }
                                    }

                                    
                                    
                                    drRecipe = p_dtbRecipeDetail.Rows.Find(p_drRow["medicineid_chr"]);
                                    if (drRecipe != null)
                                    {
                                        p_drNew["recipeamount"] = drRecipe["amount_int"];
                                        p_drNew["recipesum"] = drRecipe["sum_int"];
                                    }
                                    else
                                    {
                                        p_drNew["recipeamount"] = 0;
                                        p_drNew["recipesum"] = 0;
                                    }
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;

                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);

                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                }
                                else
                                {
                                    //���������ڻ�δ��ת�ļ�¼                                
                                    if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_strLastStorageID)
                                    {
                                        continue;
                                    }
                                    
                                    //ȡ���ڳ���
                                    if (p_drRow["isend_int"].ToString() == "1")
                                    {
                                        if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                        {
                                            if (Convert.ToString(p_drRow["isend_int"]) != "" && p_drRow["isend_int"].ToString() == "1")
                                            {
                                                double.TryParse(Convert.ToString(p_drRow["endopamount_int"]), out p_dblEndOPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["endipamount_int"]), out p_dblEndIPAmount);//����
                                                double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                                if (p_dblPackQty == 0)
                                                {
                                                    double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                                }
                                                double.TryParse(Convert.ToString(p_drRow["endopretailprice_int"]), out p_dblEndRetailPrice);//����
                                                p_dblEndSum = p_dblEndIPAmount * p_dblEndRetailPrice / p_dblPackQty;
                                                if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                                {
                                                }
                                                else//��С��λ
                                                {
                                                    p_dblEndOPAmount = p_dblEndIPAmount;
                                                }
                                            }
                                            p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblEndOPAmount;
                                            p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblEndSum;
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate)
                                        {
                                            continue;
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opamount_int"]), out p_dblOPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["ipamount_int"]), out p_dblIPAmount);//����
                                        double.TryParse(Convert.ToString(p_drRow["packqty_dec"]), out p_dblPackQty);//��װ��
                                        if (p_dblPackQty == 0)
                                        {
                                            double.TryParse(Convert.ToString(p_drRow["bsepackty"]), out p_dblPackQty);//��װ��
                                        }
                                        double.TryParse(Convert.ToString(p_drRow["opretailprice_int"]), out p_dblRetailPrice);//����
                                        double.TryParse(Convert.ToString(p_drRow["opnewretailprice_int"]), out p_dblNewPrice);
                                        p_dblSum = p_dblIPAmount * p_dblRetailPrice / p_dblPackQty;
                                        if (Convert.ToInt16(p_drRow["opchargeflg_int"]) == 0)//������λ
                                        {
                                        }
                                        else//��С��λ
                                        {
                                            p_dblOPAmount = p_dblIPAmount;
                                        }
                                        if (Convert.ToString(p_drRow["isend_int"]) == "")//û����ϸ��¼��ֻ�л�����
                                        {
                                        }
                                        else
                                        {
                                            if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtStartDate)
                                            {
                                                //����
                                                if (Convert.ToInt16(p_drRow["type_int"]) == 0)
                                                {
                                                    p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblOPAmount;
                                                    p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblIPAmount * (p_dblNewPrice - p_dblRetailPrice) / p_dblPackQty;
                                                }
                                                else if (Convert.ToInt16(p_drRow["type_int"]) == 1)
                                                {
                                                    p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblOPAmount;
                                                    p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblSum;
                                                }
                                                else if (Convert.ToInt16(p_drRow["type_int"]) == 2)
                                                {
                                                    p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblOPAmount;
                                                    p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblSum;
                                                }
                                            }
                                        }
                                    }
                                    p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                    p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                    p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"])- Convert.ToDouble(p_drNew["recipeamount"]);
                                    p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"])- Convert.ToDouble(p_drNew["recipesum"]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    dtbTrueResult.Clear();
                }
                if (!dtbTrueResult.Columns.Contains("SortRowNo"))
                {
                    dtbTrueResult.Columns.Add("SortRowNo", typeof(long));
                }
                m_mthAddTotalSumRow(dtbTrueResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,str + "     " +i.ToString());
            }

        }

        /*20080410 ��ˮ���ṩ
    ���������
һ�������ڣ���ת��
1����ʼʱ���������ڵĿ�ʼʱ�䣬����ʱ���������ڵĽ���ʱ��
2������ǵ�һ�ڣ���ô�ڳ����Ϳ���ʼ����ֵ�����ݺ���7������Ƿǵ�һ�ڣ�  �ڳ�������һ����δ��
2����⣬���ݺŵھ�λ������1
3�����⣬���ݺŵھ�λ������2
4�����ۣ����ݺŵھ�λ������8��
5����δ����ȡ��ˮ�ʱ���ڵ���ĩ��
6��ʵ����δ���������ݹ�ʽ�������Ҳ�����ڳ�+���-����
    ʵ����δ�����ݹ�ʽ�������Ҳ�����ڳ�+���-����+����ӯ����� 

ע�⣬��ͬID��ͬ���ŵ�������������Ҳ���ǲ���������
����ע��״̬����Ч��״̬��Ҫѡ�񣬽��ȡ��λ

������ǰ�ڣ�û�н�ת��
1����ʼʱ�����ϸ������ڵĽ���ʱ���һ�룬����ʱ���ǵ�ǰʱ��(����ǵ�һ�ڣ��ǿ�ʼʱ����������еġ������ڿ�ʼʱ�䡱------�������ʰ���)
2������ǵ�һ�ڣ��ڳ������ǿ���ʼ���ģ����ݺ���7������ǵ�һ�ڣ��ڳ�������һ����δ
5����δ�����õ�ǰ�����*/ 
        /// <summary>
        /// �����������˼����ѯ���
        /// </summary>
        private void m_mthFilterResultDetail(DataTable p_dtbResult)
        {
            if (p_dtbResult == null)
            {
                return;
            }
            if (dtbTrueResult == null)
            {
                m_mthInitialDataTable();
            }

            DataView dvTemp = p_dtbResult.DefaultView;
            dvTemp.RowFilter = string.Empty;
            if (m_objViewer.m_txtMedicineCode.Text.Trim() != string.Empty && Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) != string.Empty)
            {
                dvTemp.RowFilter = "medicineid_chr = '" + Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) + "'";
            }
            if (!string.IsNullOrEmpty(m_objViewer.txtTypecode.Value) && m_objViewer.txtTypecode.Value != "-1")
            {
                if (dvTemp.RowFilter == string.Empty)
                    dvTemp.RowFilter = "medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
                else
                    dvTemp.RowFilter += " and medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
            }
            dvTemp.Sort = "medicinename_vchr,medicineid_chr,accountid_chr";
            p_dtbResult = dvTemp.ToTable();
            DataRow p_drRow = null;
            DataRow p_drNew = null;
            if (p_dtbResult.Rows.Count > 0)
            {
                string p_strMedicineId = string.Empty;
                dtbTrueResult.Clear();
             
                double p_dblRetailPrice = 0d;//���۵���
                double p_dblAmount = 0d;//����
                double p_dblCommon = 0d;//����
                double p_dblStartAmount = 0d;
                double p_dblStartSum = 0d;
                int intProcessType = 0;//��������

                if (m_objViewer.m_txtAccountID.Text != "δ��ת")//��ѯ�ѽ�ת�����ڵ�����
                {                    
                    if (string.IsNullOrEmpty(m_objViewer.m_strLastStorageID))//��һ��������Ϊ�գ�����ת�ĵ�һ��
                    {
                        for (int i1 = 0; i1 < p_dtbResult.Rows.Count; i1++)
                        {
                            p_drRow = p_dtbResult.Rows[i1];
                            
                            if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                            {
                                //���������ڻ�δ��ת�ļ�¼��
                                if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text))
                                    || ((Convert.ToString(p_drRow["operatedate_dat"])!=string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate) && m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"])) != 7))
                                {
                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    p_drNew["inamount"] = 0;
                                    p_drNew["insum"] = 0;
                                    p_drNew["outamount"] = 0;
                                    p_drNew["outsum"] = 0;
                                    p_drNew["adjustamount"] = 0;
                                    p_drNew["adjustsum"] = 0;
                                    p_drNew["recipeamount"] = 0;
                                    p_drNew["recipesum"] = 0;
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;
                                    p_drNew["startamount"] = 0;
                                    p_drNew["startsum"] = 0;
                                    p_drNew["endamount"] = 0;
                                    p_drNew["endsum"] = 0;
                                    p_drNew["realendamount"] = 0;
                                    p_drNew["realendsum"] = 0;
                                    p_drNew["realendamountdiff"] = 0;
                                    p_drNew["realendsumdiff"] = 0;
                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                    continue;
                                }

                                p_drNew = dtbTrueResult.NewRow();
                                p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch(intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = p_dblAmount;
                                        p_drNew["insum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 2://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = p_dblAmount;
                                        p_drNew["outsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 4://ȡ���ڳ���
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                         p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblAmount;
                                        p_drNew["startsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 5://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = p_dblAmount*(p_dblCommon -p_dblRetailPrice) ;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    default:
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                }

                                if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["endamount"] = p_dblAmount;
                                        p_drNew["endsum"] = p_dblAmount * p_dblRetailPrice;
                                    }
                                }

                                p_drNew["recipeamount"] = 0;
                                p_drNew["recipesum"] = 0;
                                p_drNew["putamount"] = 0;
                                p_drNew["putsum"] = 0;

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);

                                p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                dtbTrueResult.Rows.Add(p_drNew);
                            }
                            else
                            {
                                //���������ڻ�δ��ת�ļ�¼                                
                                if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text)
                                {
                                    continue;
                                }

                                if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["endamount"] = Convert.ToDouble(p_drNew["endamount"]) +p_dblAmount;
                                        p_drNew["endsum"] = Convert.ToDouble(p_drNew["endsum"]) + p_dblAmount * p_dblRetailPrice;  
                                    }
                                }
                                if(Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate)
                                {
                                    continue;
                                }

                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblAmount;
                                        p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblAmount * p_dblRetailPrice;                                       
                                        break;
                                    case 2://����                                        
                                        p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblAmount;
                                        p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblAmount * p_dblRetailPrice;                                       
                                        break;
                                    case 4://ȡ���ڳ���                                       
                                        p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblAmount;
                                        p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 5://����                                        
                                        p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        break;
                                    default:                                       
                                        break;
                                }

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                            }
                        }           
                    }
                    else//��һ�ڲ�Ϊ��
                    {
                        for (int i1 = 0; i1 < p_dtbResult.Rows.Count; i1++)
                        {
                            p_drRow = p_dtbResult.Rows[i1];

                            if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                            {
                                //����������
                                if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text))
                                    || ((Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate))))
                                {
                                    p_dblAmount = 0;
                                    p_dblRetailPrice = 0;
                                    p_dblStartAmount = 0d;
                                    p_dblStartSum = 0d;
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                    {
                                        if (p_drRow["isend_int"].ToString() == "1")//ȡ���ڳ���
                                        {                                            
                                            double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                            double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);                                            
                                        }
                                    }
                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    p_drNew["inamount"] = 0;
                                    p_drNew["insum"] = 0;
                                    p_drNew["outamount"] = 0;
                                    p_drNew["outsum"] = 0;
                                    p_drNew["adjustamount"] = 0;
                                    p_drNew["adjustsum"] = 0;
                                    p_drNew["recipeamount"] = 0;
                                    p_drNew["recipesum"] = 0;
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;
                                    p_drNew["startamount"] = p_dblAmount;
                                    p_drNew["startsum"] = p_dblAmount * p_dblRetailPrice;
                                    p_drNew["endamount"] = 0;
                                    p_drNew["endsum"] = 0;
                                    p_drNew["realendamount"] = 0;
                                    p_drNew["realendsum"] = 0;
                                    p_drNew["realendamountdiff"] = 0;
                                    p_drNew["realendsumdiff"] = 0;
                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                    continue;
                                }
                                if (Convert.ToString(p_drRow["accountid_chr"]) == "")//�ų������ڴ��������ڷ����ļ�¼
                                {

                                }

                                double.TryParse(Convert.ToString(p_drRow["startamount"]), out p_dblStartAmount);
                                double.TryParse(Convert.ToString(p_drRow["startsum"]), out p_dblStartSum);

                                p_drNew = dtbTrueResult.NewRow();
                                p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = p_dblAmount;
                                        p_drNew["insum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblStartAmount;
                                        p_drNew["startsum"] = p_dblStartSum;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 2://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = p_dblAmount;
                                        p_drNew["outsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblStartAmount;
                                        p_drNew["startsum"] = p_dblStartSum;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 4://ȡ���ڳ���
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblStartAmount;
                                        p_drNew["startsum"] = p_dblStartSum;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 5://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        p_drNew["startamount"] = p_dblStartAmount;
                                        p_drNew["startsum"] = p_dblStartSum;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    default:
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblStartAmount;
                                        p_drNew["startsum"] = p_dblStartSum;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                }

                                if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["endamount"] = p_dblAmount;
                                        p_drNew["endsum"] = p_dblAmount * p_dblRetailPrice;
                                    }
                                }

                                p_drNew["recipeamount"] = 0;
                                p_drNew["recipesum"] = 0;
                                p_drNew["putamount"] = 0;
                                p_drNew["putsum"] = 0;

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);

                                p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                dtbTrueResult.Rows.Add(p_drNew);
                            }
                            else
                            {
                                //���������ڻ�δ��ת�ļ�¼                                
                                if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_txtAccountID.Text)
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                    {
                                        if (p_drRow["isend_int"].ToString() == "1")//ȡ���ڳ���
                                        {                                            
                                            double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                            double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                            p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"])+ p_dblAmount;
                                            p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                        }
                                        p_dblAmount = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                        p_dblRetailPrice = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);

                                        p_drNew["realendamount"] = p_dblAmount;// Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                        p_drNew["realendsum"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                        p_dblAmount = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                        p_dblRetailPrice = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                                        p_drNew["realendamountdiff"] = p_dblAmount;//Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                        p_drNew["realendsumdiff"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                                    }
                                    continue;
                                }

                                if (p_drRow["isend_int"].ToString() == "1")//ȡ����ĩ��
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_txtAccountID.Text)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["endamount"] = Convert.ToDouble(p_drNew["endamount"]) + p_dblAmount;
                                        p_drNew["endsum"] = Convert.ToDouble(p_drNew["endsum"]) + p_dblAmount * p_dblRetailPrice;
                                    }
                                }

                                if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_dtEndDate))
                                {
                                    continue;
                                }

                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblAmount;
                                        p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 2://����                                        
                                        p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblAmount;
                                        p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 4://ȡ���ڳ���                                       
                                        //p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblAmount;
                                        //p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 5://����                                        
                                        p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        break;
                                    default:
                                        break;
                                }

                                p_dblAmount = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_dblRetailPrice = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);

                                p_drNew["realendamount"] = p_dblAmount;// Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_dblAmount = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_dblRetailPrice = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                                p_drNew["realendamountdiff"] = p_dblAmount;//Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = p_dblRetailPrice;// Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                            }
                        }      
                    }
                }
                else//��ѯδ��ת������
                {
                    if (string.IsNullOrEmpty(m_objViewer.m_strLastStorageID))//��һ��������Ϊ�գ���δ��ת���ǵ�һ��
                    {
                        for (int i1 = 0; i1 < p_dtbResult.Rows.Count; i1++)
                        {
                            p_drRow = p_dtbResult.Rows[i1];

                            if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                            {
                                p_drNew = dtbTrueResult.NewRow();
                                p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = p_dblAmount;
                                        p_drNew["insum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 2://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = p_dblAmount;
                                        p_drNew["outsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 4://ȡ���ڳ���
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = p_dblAmount;
                                        p_drNew["startsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 5://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    default:
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                }

                                //��ĩ��Ϊ��ǰ���
                                double.TryParse(Convert.ToString(p_drRow["realamount"]), out p_dblAmount);
                                double.TryParse(Convert.ToString(p_drRow["realsum"]), out p_dblRetailPrice);
                                p_drNew["endamount"] = p_dblAmount;
                                p_drNew["endsum"] = p_dblRetailPrice;                                    

                                p_drNew["recipeamount"] = 0;
                                p_drNew["recipesum"] = 0;
                                p_drNew["putamount"] = 0;
                                p_drNew["putsum"] = 0;

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);

                                p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                dtbTrueResult.Rows.Add(p_drNew);
                            }
                            else
                            {
                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblAmount;
                                        p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 2://����                                        
                                        p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblAmount;
                                        p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 4://ȡ���ڳ���                                       
                                        p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblAmount;
                                        p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 5://����                                        
                                        p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        break;
                                    default:
                                        break;
                                }

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                            }
                        }
                    }

                    else//���н�ת��¼
                    {
                        for (int i1 = 0; i1 < p_dtbResult.Rows.Count; i1++)
                        {
                            p_drRow = p_dtbResult.Rows[i1];

                            if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                            {
                                //���������ڻ�δ��ת�ļ�¼��
                                if (((Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_strLastStorageID))
                                    || ((Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate)))
                                {
                                    p_drNew = dtbTrueResult.NewRow();
                                    p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                    p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                    p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                    p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                    p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                    p_drNew["inamount"] = 0;
                                    p_drNew["insum"] = 0;
                                    p_drNew["outamount"] = 0;
                                    p_drNew["outsum"] = 0;
                                    p_drNew["adjustamount"] = 0;
                                    p_drNew["adjustsum"] = 0;
                                    p_drNew["recipeamount"] = 0;
                                    p_drNew["recipesum"] = 0;
                                    p_drNew["putamount"] = 0;
                                    p_drNew["putsum"] = 0;
                                    p_drNew["startamount"] = 0;
                                    p_drNew["startsum"] = 0;
                                    p_drNew["endamount"] = 0;
                                    p_drNew["endsum"] = 0;
                                    p_drNew["realendamount"] = 0;
                                    p_drNew["realendsum"] = 0;
                                    p_drNew["realendamountdiff"] = 0;
                                    p_drNew["realendsumdiff"] = 0;
                                    p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                    p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                    p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                    p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                    dtbTrueResult.Rows.Add(p_drNew);
                                    continue;
                                }

                                p_drNew = dtbTrueResult.NewRow();
                                p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                                p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                                p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                                p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                                p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = p_dblAmount;
                                        p_drNew["insum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 2://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = p_dblAmount;
                                        p_drNew["outsum"] = p_dblAmount * p_dblRetailPrice;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 4://ȡ���ڳ���
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    case 5://����
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                    default:
                                        p_drNew["inamount"] = 0;
                                        p_drNew["insum"] = 0;
                                        p_drNew["outamount"] = 0;
                                        p_drNew["outsum"] = 0;
                                        p_drNew["adjustamount"] = 0;
                                        p_drNew["adjustsum"] = 0;
                                        p_drNew["startamount"] = 0;
                                        p_drNew["startsum"] = 0;
                                        p_drNew["endamount"] = 0;
                                        p_drNew["endsum"] = 0;
                                        break;
                                }

                                //ȡ���ڳ���
                                if (p_drRow["isend_int"].ToString() == "1")
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["startamount"] = p_dblAmount;
                                        p_drNew["startsum"] = p_dblAmount * p_dblRetailPrice;
                                    }
                                }
                                //ȡ����ĩ�� = �����
                                double.TryParse(Convert.ToString(p_drRow["realamount"]), out p_dblAmount);
                                double.TryParse(Convert.ToString(p_drRow["realsum"]), out p_dblRetailPrice);
                                p_drNew["endamount"] = p_dblAmount;
                                p_drNew["endsum"] = p_dblRetailPrice;                                    

                                p_drNew["recipeamount"] = 0;
                                p_drNew["recipesum"] = 0;
                                p_drNew["putamount"] = 0;
                                p_drNew["putsum"] = 0;

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);

                                p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                                p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                                p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                                p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                                dtbTrueResult.Rows.Add(p_drNew);
                            }
                            else
                            {
                                //���������ڻ�δ��ת�ļ�¼                                
                                if (Convert.ToString(p_drRow["accountid_chr"]) != "" && Convert.ToString(p_drRow["accountid_chr"]) != m_objViewer.m_strLastStorageID)                                    
                                {
                                    continue;
                                }

                                //ȡ���ڳ���
                                if (p_drRow["isend_int"].ToString() == "1")
                                {
                                    if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strLastStorageID)
                                    {
                                        double.TryParse(Convert.ToString(p_drRow["endamount_int"]), out p_dblAmount);
                                        double.TryParse(Convert.ToString(p_drRow["endretailprice_int"]), out p_dblRetailPrice);
                                        p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblAmount;
                                        p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                    }
                                }
                                //ȡ����ĩ�� = �����
                                double.TryParse(Convert.ToString(p_drRow["realamount"]), out p_dblAmount);
                                double.TryParse(Convert.ToString(p_drRow["realsum"]), out p_dblRetailPrice);
                                p_drNew["endamount"] = p_dblAmount;
                                p_drNew["endsum"] = p_dblRetailPrice;   

                                if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty && Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_dtStartDate)
                                {
                                    continue;
                                }

                                double.TryParse(Convert.ToString(p_drRow["amount_int"]), out p_dblAmount);//����
                                double.TryParse(Convert.ToString(p_drRow["retailprice_int"]), out p_dblRetailPrice);//����

                                intProcessType = m_intGetProcessType(Convert.ToString(p_drRow["chittyid_vchr"]));
                                switch (intProcessType)
                                {
                                    case 1://���
                                        p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblAmount;
                                        p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 2://����                                        
                                        p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblAmount;
                                        p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 4://ȡ���ڳ���                                       
                                        //p_drNew["startamount"] = Convert.ToDouble(p_drNew["startamount"]) + p_dblAmount;
                                        //p_drNew["startsum"] = Convert.ToDouble(p_drNew["startsum"]) + p_dblAmount * p_dblRetailPrice;
                                        break;
                                    case 5://����                                        
                                        p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblAmount;
                                        double.TryParse(Convert.ToString(p_drRow["newretailprice_int"]), out p_dblCommon);
                                        p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblAmount * (p_dblCommon - p_dblRetailPrice);
                                        break;
                                    default:
                                        break;
                                }

                                p_drNew["realendamount"] = Convert.ToDouble(p_drNew["startamount"]) + Convert.ToDouble(p_drNew["inamount"]) - Convert.ToDouble(p_drNew["outamount"]);
                                p_drNew["realendsum"] = Convert.ToDouble(p_drNew["startsum"]) + Convert.ToDouble(p_drNew["insum"]) - Convert.ToDouble(p_drNew["outsum"]) + Convert.ToDouble(p_drNew["adjustsum"]);
                                p_drNew["realendamountdiff"] = Convert.ToDouble(p_drNew["realendamount"]) - Convert.ToDouble(p_drNew["endamount"]);
                                p_drNew["realendsumdiff"] = Convert.ToDouble(p_drNew["realendsum"]) - Convert.ToDouble(p_drNew["endsum"]);
                            }
                        }    
                    }
                }   
            }
            if (!dtbTrueResult.Columns.Contains("SortRowNo"))
            {
                dtbTrueResult.Columns.Add("SortRowNo", typeof(long));
            }
            m_mthAddTotalSumRow(dtbTrueResult);
        }

        internal void m_mthAddTotalSumRow(DataTable p_dtbTarget)
        {
            if (p_dtbTarget.Rows.Count > 0)
            {
                double dblTempSum = 0d;
                DataRow drAdd = p_dtbTarget.NewRow();
                for (int i1 = 0; i1 < p_dtbTarget.Columns.Count; i1++)
                {
                    dblTempSum = 0d;
                    if (p_dtbTarget.Columns[i1].ColumnName == "medicinename_vchr" || p_dtbTarget.Columns[i1].ColumnName == "medicineid_chr")//medicineid_chr��Ϊɾ������ʱ��λ
                    {
                        drAdd[i1] = "�ϼ�";
                    }
                    else if (p_dtbTarget.Columns[i1].DataType == typeof(double))
                    {
                        for (int i2 = 0; i2 < p_dtbTarget.Rows.Count; i2++)
                        {
                            dblTempSum += Convert.ToDouble(p_dtbTarget.Rows[i2][i1]);
                        }
                        drAdd[i1] = dblTempSum;
                    }
                }
                p_dtbTarget.Rows.Add(drAdd);
                p_dtbTarget.AcceptChanges();
            }  
        }

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="p_strChitty">���ݺ�</param>
        /// <returns></returns>
        private int m_intGetProcessType(string p_strChitty)
        {
            int intType = 0;
            if (string.IsNullOrEmpty(p_strChitty))
            {
                return -1;
            }

            try
            {
                //��⣻���⣻�̵㣻ҩ����ʼ��������
                char chrPro = p_strChitty[9];
                //m_objDomain.m_lngGetBillType(false,out strType);
                if (chrPro.ToString() == m_strType.Split(';')[0])
                {
                    intType = 1;//���
                }
                else if (chrPro.ToString() == m_strType.Split(';')[1])
                {
                    intType = 2;//����
                }
                else if (chrPro.ToString() == m_strType.Split(';')[2])
                {
                    intType = 3;//�̵�
                }
                else if (chrPro.ToString() == m_strType.Split(';')[3])
                {
                    intType = 4;//ҩ���ʼ��
                }
                else if (chrPro.ToString() == m_strType.Split(';')[4])
                {
                    intType = 5;//����
                }
            }
            catch 
            {
                
            }
            return intType;
        }

        /// <summary>
        /// ��ȡ�������ͣ�ҩ����
        /// </summary>
        /// <param name="p_strChitty">���ݺ�</param>
        /// <returns></returns>
        private string m_strGetProcessTypeForDrugStore(string p_strChitty)
        {
            string strType = string.Empty;
            string m_strType = string.Empty;
            if (string.IsNullOrEmpty(p_strChitty))
            {
                return strType;
            }

            try
            {
                //��⣻���⣻�̵㣻���죻ҩ����ʼ��������
                if (p_strChitty.Length < 10)
                    return strType;
                //m_objDomain.m_lngGetBillType(true,out m_strType); 
                char chrPro = p_strChitty[9];
                if (chrPro.ToString() == m_strType.Split(';')[0])
                {
                    strType = "01";//���
                }
                else if (chrPro.ToString() == m_strType.Split(';')[1])
                {
                    strType = "02";//����
                }
                else if (chrPro.ToString() == m_strType.Split(';')[2])
                {
                    strType = "03";//�̵�
                }
                else if (chrPro.ToString() == m_strType.Split(';')[3])
                {
                    strType = "04";//����
                }
                else if (chrPro.ToString() == m_strType.Split(';')[4])
                {
                    strType = "05";//ҩ����ʼ��
                }
                else if (chrPro.ToString() == m_strType.Split(';')[5])
                {
                    strType = "06";//����
                }
            }
            catch
            {

            }
            return strType;
        }
        
        #endregion
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void m_mthInitialDataTable()
        {
            dtbTrueResult = new DataTable();
            DataColumn[] dcColArr = new DataColumn[] { new DataColumn("medicineid_chr", typeof(string)), new DataColumn("medicinename_vchr", typeof(string)), new DataColumn("medspec_vchr", typeof(string)), 
                new DataColumn("opunit_chr", typeof(string)),new DataColumn("productorid_chr", typeof(string)),
            new DataColumn("realendamountdiff", typeof(double)),new DataColumn("realendsumdiff", typeof(double)),new DataColumn("inamount", typeof(double)),new DataColumn("insum", typeof(double)),
            new DataColumn("outamount", typeof(double)),new DataColumn("outsum", typeof(double)),new DataColumn("adjustamount", typeof(double)),new DataColumn("adjustsum", typeof(double)),
            new DataColumn("recipeamount", typeof(double)),new DataColumn("recipesum", typeof(double)),new DataColumn("startamount", typeof(double)),new DataColumn("startsum", typeof(double)),
            new DataColumn("endamount", typeof(double)),new DataColumn("endsum", typeof(double)),new DataColumn("realendamount", typeof(double)),new DataColumn("realendsum", typeof(double)),
            new DataColumn("putamount", typeof(double)),new DataColumn("putsum", typeof(double)),new DataColumn("assistcode_chr", typeof(string)),new DataColumn("wbcode_chr", typeof(string)),
            new DataColumn("pycode_chr", typeof(string)),};
            dtbTrueResult.Columns.AddRange(dcColArr);
            dtbTrueResult.PrimaryKey = new DataColumn[] { dtbTrueResult.Columns["medicineid_chr"] };
        }

        #region ����
        /// <summary>
        /// �����������˼����ѯ���
        /// </summary>
        private void m_mthFilterResult()
        {
           if(dtbResult == null)
           {
               return;
           }
           DataTable dtbTemp = dtbResult.Copy();
           DataView dvTemp = dtbResult.DefaultView;
           if (Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) != string.Empty)
           {
               dvTemp.RowFilter = "medicineid_chr = '" + Convert.ToString(m_objViewer.m_txtMedicineCode.Tag) + "'";
           }
           if (!string.IsNullOrEmpty(m_objViewer.txtTypecode.Value) && m_objViewer.txtTypecode.Value != "-1")
           {
               if(dvTemp.RowFilter == string.Empty)
                dvTemp.RowFilter = "medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
               else
                dvTemp.RowFilter += "and medicinetypeid_chr = '" + m_objViewer.txtTypecode.Value + "'";
           }
           dtbResult = dvTemp.ToTable();
           DataRow p_drRow = null;
           DataRow p_drNew = null;
           if (dtbResult.Rows.Count > 0)
           {
               string p_strMedicineId = string.Empty;
               dtbTemp.Clear();
               double p_dblStartAmount = 0d;
               double p_dblStartSum = 0d;
               double p_dblEndAmount = 0d;
               double p_dblEndSum = 0d;
               double p_dblCommon = 0d;//����
               for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
               {
                  
                   p_drRow = dtbResult.Rows[i1];                   
                   if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                   {                       
                       if (p_drRow["isend_int"].ToString() == "1")//ȡ���ڳ�������ĩ��
                       {
                           if (Convert.ToString(p_drRow["accountid_chr"]) == m_objViewer.m_strBackAccount
                               && m_objViewer.m_strBackAccount == m_objViewer.m_strForeAccount && m_objViewer.m_strBackAccount != string.Empty)
                           {
                                double.TryParse(Convert.ToString(p_drRow["startamount"]),out p_dblStartAmount);
                                double.TryParse(Convert.ToString(p_drRow["startsum"]), out p_dblStartSum);
                                double.TryParse(Convert.ToString(p_drRow["endamount"]), out p_dblEndAmount);
                                double.TryParse(Convert.ToString(p_drRow["endsum"]), out p_dblEndSum);
                                p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                           }
                           else
                           {
                               continue;
                           }
                       }
                       else
                       {
                           //�������ڱ����ڲ�ѯ��Χ��
                           if (Convert.ToString(p_drRow["operatedate_dat"]) != string.Empty)
                           {
                               //if (Convert.ToDateTime(p_drRow["operatedate_dat"]) < m_objViewer.m_dtpSearchBeginDate.Value
                               //    || Convert.ToDateTime(p_drRow["operatedate_dat"]) > m_objViewer.m_dtpSearchEndDate.Value)
                               //{
                               //    continue;
                               //}
                           }
                           if (p_strMedicineId != p_drRow["medicineid_chr"].ToString())
                           {
                               p_dblStartAmount = 0d;
                               p_dblStartSum = 0d;
                               p_dblEndAmount = 0d;
                               p_dblEndSum = 0d;
                           }
                       }
                       
                       p_drNew = dtbTemp.NewRow();
                       p_drNew["medicineid_chr"] = p_drRow["medicineid_chr"];
                       p_drNew["medicinename_vchr"] = p_drRow["medicinename_vchr"];
                       p_drNew["medspec_vchr"] = p_drRow["medspec_vchr"];
                       p_drNew["opunit_chr"] = p_drRow["opunit_chr"];
                       p_drNew["productorid_chr"] = p_drRow["productorid_chr"];
                       double.TryParse(Convert.ToString(p_drRow["realendamount"]),out p_dblCommon);
                       p_drNew["realendamountdiff"] = p_dblCommon - p_dblEndAmount;
                       double.TryParse(Convert.ToString(p_drRow["realendsum"]), out p_dblCommon);
                       p_drNew["realendsumdiff"] = p_dblCommon - p_dblEndSum;
                       double.TryParse(Convert.ToString(p_drRow["inamount"]), out p_dblCommon);
                       p_drNew["inamount"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["insum"]), out p_dblCommon);
                       p_drNew["insum"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["outamount"]), out p_dblCommon);
                       p_drNew["outamount"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["outsum"]), out p_dblCommon);
                       p_drNew["outsum"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["adjustamount"]), out p_dblCommon);
                       p_drNew["adjustamount"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["adjustsum"]), out p_dblCommon);
                       p_drNew["adjustsum"] = p_dblCommon;
                       p_drNew["recipeamount"] = p_drRow["recipeamount"];
                       p_drNew["recipesum"] = p_drRow["recipesum"];
                       p_drNew["startamount"] = p_dblStartAmount;
                       p_drNew["startsum"] = p_dblStartSum;
                       p_drNew["endamount"] = p_dblEndAmount;
                       p_drNew["endsum"] = p_dblEndSum;
                       double.TryParse(Convert.ToString(p_drRow["realendamount"]), out p_dblCommon);
                       p_drNew["realendamount"] = p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["realendsum"]), out p_dblCommon);
                       p_drNew["realendsum"] = p_dblCommon;
                       p_drNew["putamount"] = p_drRow["putamount"];
                       p_drNew["putsum"] = p_drRow["putsum"];
                       p_drNew["type_int"] = p_drRow["type_int"];
                       p_drNew["isend_int"] = p_drRow["isend_int"];
                       p_drNew["state_int"] = p_drRow["state_int"];
                       p_drNew["accountid_chr"] = p_drRow["accountid_chr"];
                       p_drNew["medicinetypeid_chr"] = p_drRow["medicinetypeid_chr"];
                       p_drNew["operatedate_dat"] = p_drRow["operatedate_dat"];
                       p_drNew["assistcode_chr"] = p_drRow["assistcode_chr"];
                       p_drNew["wbcode_chr"] = p_drRow["wbcode_chr"];
                       p_drNew["pycode_chr"] = p_drRow["pycode_chr"];
                       p_strMedicineId = p_drRow["medicineid_chr"].ToString();
                       dtbTemp.Rows.Add(p_drNew);
                   }
                   else
                   {
                       double.TryParse(Convert.ToString(p_drRow["realendamount"]), out p_dblCommon);
                       p_drNew["realendamountdiff"] = p_dblCommon - p_dblEndAmount;
                       double.TryParse(Convert.ToString(p_drRow["realendsum"]), out p_dblCommon);
                       p_drNew["realendsumdiff"] = p_dblCommon - p_dblEndSum;
                       double.TryParse(Convert.ToString(p_drRow["inamount"]), out p_dblCommon);
                       p_drNew["inamount"] = Convert.ToDouble(p_drNew["inamount"]) + p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["insum"]), out p_dblCommon);
                       p_drNew["insum"] = Convert.ToDouble(p_drNew["insum"]) + p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["outamount"]), out p_dblCommon);
                       p_drNew["outamount"] = Convert.ToDouble(p_drNew["outamount"]) + p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["outsum"]), out p_dblCommon);
                       p_drNew["outsum"] = Convert.ToDouble(p_drNew["outsum"]) + p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["adjustamount"]), out p_dblCommon);
                       p_drNew["adjustamount"] = Convert.ToDouble(p_drNew["adjustamount"]) + p_dblCommon;
                       double.TryParse(Convert.ToString(p_drRow["adjustsum"]), out p_dblCommon);
                       p_drNew["adjustsum"] = Convert.ToDouble(p_drNew["adjustsum"]) + p_dblCommon;
                   }
                   
               }
               dtbResult = dtbTemp.Copy();
               dtbResult.Columns.Remove("type_int");
               dtbResult.Columns.Remove("isend_int");
               dtbResult.Columns.Remove("state_int");
               dtbResult.Columns.Remove("accountid_chr");
               dtbResult.Columns.Remove("medicinetypeid_chr");
               dtbResult.Columns.Remove("operatedate_dat");
           }
        }

        //#region ��ȡʱ���Ӧ��������
        ///// <summary>
        ///// ��ȡʱ���Ӧ��������
        ///// </summary>
        //internal void m_mthGetAccount()
        //{
        //    string m_strAccountName = string.Empty;
        //    m_objDomain.m_lngGetAccount(m_objViewer.txtStoreroom.Value, DateTime.Parse(Convert.ToDateTime(m_objViewer.m_dtpSearchBeginDate.Text).ToString("yyyy-MM-dd HH:mm:ss")), out m_strAccountName);
        //    m_objViewer.m_strForeAccount = m_strAccountName;
        //    m_objDomain.m_lngGetAccount(m_objViewer.txtStoreroom.Value, DateTime.Parse(Convert.ToDateTime(m_objViewer.m_dtpSearchEndDate.Text).ToString("yyyy-MM-dd HH:mm:ss")), out m_strAccountName);
        //    m_objViewer.m_strBackAccount = m_strAccountName;
        //}
        //#endregion
        #endregion

        #region �����񵼳����ݵ�Excel
        /// <summary>
        /// �����񵼳����ݵ�Excel
        /// </summary>
        internal void m_mthExportToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel�ļ�(*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "����Excel�ļ���";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            try
            {
                //����б���
                for (int iOr = 0; iOr < m_objViewer.m_dgvBalance.ColumnCount; iOr++)
                {
                    if (m_objViewer.m_dgvBalance.Columns[iOr].Visible == false) continue;
                    if (iOr > 0)
                    {
                        str += "\t";
                    }
                    str += m_objViewer.m_dgvBalance.Columns[iOr].HeaderText.Replace("\n", "");
                }
                sw.WriteLine(str);
                //������ı�
                StringBuilder objStrBuilder = null;
                for (int iOr = 0; iOr < m_objViewer.m_dgvBalance.Rows.Count; iOr++)
                {
                    objStrBuilder = new StringBuilder();
                    for (int jOr = 0; jOr < m_objViewer.m_dgvBalance.Columns.Count; jOr++)
                    {
                        if (m_objViewer.m_dgvBalance.Columns[jOr].Visible == false) continue;
                        if (jOr > 0)
                        {
                            objStrBuilder.Append("\t");
                        }
                        objStrBuilder.Append(m_objViewer.m_dgvBalance.Rows[iOr].Cells[jOr].Value.ToString());
                    }
                    sw.WriteLine(objStrBuilder);
                }
                MessageBox.Show("�����ɹ���", "�̵���˱�", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
        #endregion

        internal void m_mthPrint()
        {

            DataTable p_dtPrint = (m_objViewer.m_dgvBalance.DataSource as DataTable).Copy();
            if (p_dtPrint.Columns.Contains("productorid_chr"))
            {
                p_dtPrint.Columns.Remove("productorid_chr");
            }
            if (p_dtPrint.Columns.Contains("SortRowNo"))
            {
                p_dtPrint.Columns.Remove("SortRowNo");
            }
            if (Convert.ToString(p_dtPrint.Rows[p_dtPrint.Rows.Count - 1]["medicineid_chr"]) == "�ϼ�")
            {
                p_dtPrint.Rows.Remove(p_dtPrint.Rows[p_dtPrint.Rows.Count - 1]);
            }
            DataStore ds = new DataStore();
            ds.LibraryList = clsPublic.PBLPath;
            ds.DataWindowObject = "ms_balancereport";
            ds.Reset();
            ds.Modify("t_title.text='" + this.m_objViewer.objController.m_objComInfo.m_strGetHospitalTitle()+ "'");
            ds.Modify("storagename.text='" + m_objViewer.txtStoreroom.Text + "'");
            ds.Modify("date.text='" + m_objViewer.m_lblAccountTime.Text + "'");
            ds.Retrieve(p_dtPrint);
            com.digitalwave.iCare.gui.HIS.clsPublic.PrintDialog(ds);
        }

        internal long m_lngGetLastBalanceTime(string p_strStorageID,out DateTime dtBegin)
        {
            long lngRes = 0;
            lngRes = m_objDomain.m_lngGetLastBalanceTime(p_strStorageID,out dtBegin);
            return lngRes;
        }

        internal long m_lngGetSysParm(string p_strID, out string strBegin)
        {
            return  m_objDomain.m_lngGetSysParm(p_strID, out strBegin);
        }

        #region ��ȡ������ID�б�
        /// <summary>
        /// ��ȡ������ID�б�
        /// </summary>
        /// <param name="p_objAccArr">�����ڽ�ת</param>
        internal void m_mthGetAccountIDList(out clsMS_AccountPeriodVO[] p_objAccArr)
        {
            clsDcl_TotalAccountReport objDomain = new clsDcl_TotalAccountReport();
            long lngRes = objDomain.m_lngGetAccountPeriod(m_objViewer.txtStoreroom.Value, out p_objAccArr);
        }

        /// <summary>
        /// �����������б�������
        /// </summary>
        /// <param name="p_objAccArr">�����ڽ�ת</param>
        internal void m_mthSetAccountPeriodToList(clsMS_AccountPeriodVO[] p_objAccArr)
        {
            m_objViewer.m_lsvAccountIDList.Items.Clear();
            if (p_objAccArr == null || p_objAccArr.Length == 0)
            {
                clsMS_AccountPeriodVO objNow = new clsMS_AccountPeriodVO();
                string strDate;
                long lngRes = m_objDomain.m_lngGetSysParm("5001", out strDate);
                objNow.m_dtmSTARTTIME_DAT = Convert.ToDateTime(strDate);
                objNow.m_dtmENDTIME_DAT = clsPublic.SysDateTimeNow;
                objNow.m_dtmTRANSFERTIME_DAT = clsPublic.SysDateTimeNow;
                objNow.m_strACCOUNTID_CHR = "δ��ת";

                StringBuilder stbID = new StringBuilder(30);
                stbID.Append(objNow.m_strACCOUNTID_CHR);
                stbID.Append("  ");
                stbID.Append(objNow.m_dtmSTARTTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                stbID.Append("��");
                stbID.Append(objNow.m_dtmENDTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                ListViewItem lsiNow = new ListViewItem(stbID.ToString());
                lsiNow.Tag = objNow;
                stbID = null;
                m_objViewer.m_lsvAccountIDList.Items.Add(lsiNow);
                return;
            }


            try
            {
                m_objViewer.m_lsvAccountIDList.Items.Clear();
                m_objViewer.m_lsvAccountIDList.BeginUpdate();
                ListViewItem[] lsiItems = new ListViewItem[p_objAccArr.Length];
                for (int iItem = 0; iItem < p_objAccArr.Length; iItem++)
                {
                    StringBuilder stbID = new StringBuilder(30);
                    stbID.Append(p_objAccArr[iItem].m_strACCOUNTID_CHR);
                    stbID.Append("  ");
                    stbID.Append(p_objAccArr[iItem].m_dtmSTARTTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                    stbID.Append("��");
                    stbID.Append(p_objAccArr[iItem].m_dtmENDTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                    lsiItems[iItem] = new ListViewItem(stbID.ToString());
                    lsiItems[iItem].Tag = p_objAccArr[iItem];
                    stbID = null;
                }
                m_objViewer.m_lsvAccountIDList.Items.AddRange(lsiItems);

                //���δ��תѡ��
                if (clsPublic.SysDateTimeNow > p_objAccArr[p_objAccArr.Length - 1].m_dtmENDTIME_DAT)
                {
                    clsMS_AccountPeriodVO objNow = new clsMS_AccountPeriodVO();
                    objNow.m_dtmSTARTTIME_DAT = Convert.ToDateTime(p_objAccArr[p_objAccArr.Length - 1].m_dtmENDTIME_DAT.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss"));
                    objNow.m_dtmENDTIME_DAT = clsPublic.SysDateTimeNow;
                    objNow.m_dtmTRANSFERTIME_DAT = clsPublic.SysDateTimeNow;
                    objNow.m_strACCOUNTID_CHR = "δ��ת";

                    StringBuilder stbID = new StringBuilder(30);
                    stbID.Append(objNow.m_strACCOUNTID_CHR);
                    stbID.Append("  ");
                    stbID.Append(objNow.m_dtmSTARTTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                    stbID.Append("��");
                    stbID.Append(objNow.m_dtmENDTIME_DAT.ToString("yyyy��MM��dd�� HH:mm:ss"));
                    ListViewItem lsiNow = new ListViewItem(stbID.ToString());
                    lsiNow.Tag = objNow;
                    stbID = null;
                    m_objViewer.m_lsvAccountIDList.Items.Add(lsiNow);
                }
            }
            catch (Exception objEx)
            {
                com.digitalwave.Utility.clsLogText objLogger = new com.digitalwave.Utility.clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            finally
            {
                m_objViewer.m_lsvAccountIDList.EndUpdate();
            }
        }
        #endregion

        internal long m_lngGetRoomid(out DataTable dtTemp)
        {
            return m_objDomain.m_lngGetRoomid(out dtTemp);
        }

        internal void m_mthGetAccountIDListForDrugStore(out clsMS_AccountPeriodVO[] p_objAccArr)
        {
            m_objDomain.m_mthGetAccountIDListForDrugStore(m_objViewer.txtStoreroom.Value, out p_objAccArr);
        }
       internal DataRow drDelete = null;
        internal void m_mthDeleteTotalSumRow()
        {
            drDelete = dtbTrueResult.Rows.Find("�ϼ�");
            if (drDelete != null)
            {
                dtbTrueResult.Rows.Remove(drDelete);
                dtbTrueResult.AcceptChanges();
                //m_mthAddTotalSumRow(dtbTrueResult);
                //m_objViewer.m_dgvBalance.Refresh();
            }
        }
    }
}