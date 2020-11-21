using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// �ɹ�
    /// </summary>
    /// 
    #region �ɹ�������� ŷ����ΰ 2004-05-28
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStockMedApplSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsStockMedApplSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �½��ɹ�����¼
        /// <summary>
        /// �½��ɹ�����¼
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStoMedApp"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewStockAppl(clsStockMedApplication_VO p_objStoMedApp)
        {
            long lngRes = 0;
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"INSERT INTO t_opr_stockmedapplication (stockmedapplid_chr,stockmedapplno_vchr,storageid_chr,appldate_dat,applemp_chr,appldept_chr,vendorid_chr,tolmny_mny,pstatus_int,memo_vchr)
							  VALUES('" + p_objStoMedApp.m_strStockMedApplID + "','" + p_objStoMedApp.m_strStockMedApplNo + "','" + p_objStoMedApp.m_objStorage.m_strStroageID + "',to_date('" +
                              p_objStoMedApp.m_strApplDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objStoMedApp.m_objApplEmp.strEmpID + "','" + p_objStoMedApp.m_objApplDept.strDeptID + "','" +
                              p_objStoMedApp.m_objVendor.m_strVendorID + "'," + p_objStoMedApp.m_fltTolMny.ToString() + "," + p_objStoMedApp.m_intPStatus.ToString() + ",'" + p_objStoMedApp.m_strMemo + "' )";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �½��ɹ���ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// �½��ɹ���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStoMedAppDe"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewStockApplDe(clsStockMedApplDetail_VO p_objStoMedAppDe)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"INSERT INTO T_OPR_STOCKMEDAPPLDETAIL (STOCKMEDAPPLDETAILID_CHR,STOCKMEDAPPLID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,PRODCUTORID_CHR,QTY_DEC,UNITPRICE_MNY,DISCOUNT_DEC,TOLMNY_MNY)
							  VALUES('" + p_objStoMedAppDe.m_strStockMedApplDeID + "','" + p_objStoMedAppDe.m_strStockMedApplID + "','" + p_objStoMedAppDe.m_strRowNo + "','" + p_objStoMedAppDe.m_objMedicine.m_strMedicineID +
                              "','" + p_objStoMedAppDe.m_objUnit.m_strUnitID + "','" + p_objStoMedAppDe.m_objProductor.m_strVendorID + "'," + p_objStoMedAppDe.m_fltQty + "," + p_objStoMedAppDe.m_fltUnitPrice + "," +
                              p_objStoMedAppDe.m_fltDiscount + "," + p_objStoMedAppDe.m_fltTolMny + ")";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ͬʱ��Ӽ�¼������ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ͬ����Ӽ�¼����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStoMedApp"></param>
        /// <param name="p_objStoMedAppDeArr"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewStockApplAndDe(clsStockMedApplication_VO p_objStoMedApp, clsStockMedApplDetail_VO[] p_objStoMedAppDeArr)
        {
            long lngRes = 0;
            lngRes = m_lngDoAddNewStockAppl(p_objStoMedApp);

            if (lngRes > 0)
            {
                if (p_objStoMedAppDeArr.Length > 0)
                {
                    for (int i = 0; i < p_objStoMedAppDeArr.Length; i++)
                    {
                        lngRes = m_lngDoAddNewStockApplDe(p_objStoMedAppDeArr[i]);
                        if (lngRes != 1)
                        {
                            break;
                        }
                    }
                }
            }

            return lngRes;
        }
        #endregion

        #region ���²ɹ���¼�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ���²ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStoMedApp"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateStockApplByID(clsStockMedApplication_VO p_objStoMedApp)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"UPDATE t_opr_stockmedapplication
							  SET stockmedapplno_vchr='" + p_objStoMedApp.m_strStockMedApplNo + "',storageid_chr='" +
                              p_objStoMedApp.m_objStorage.m_strStroageID + "',appldate_dat = to_date('" + p_objStoMedApp.m_strApplDate +
                              "','yyyy-mm-dd hh24:mi:ss'),applemp_chr='" + p_objStoMedApp.m_objApplEmp.strEmpID + "',appldept_chr='" +
                              p_objStoMedApp.m_objApplDept.strDeptID + "',vendorid_chr='" + p_objStoMedApp.m_objVendor.m_strVendorID +
                              "',tolmny_mny = " + p_objStoMedApp.m_fltTolMny.ToString() + ",pstatus_int=" + p_objStoMedApp.m_intPStatus.ToString() + ",memo_vchr='" + p_objStoMedApp.m_strMemo + "'" +
                              "   WHERE stockmedapplid_chr = '" + p_objStoMedApp.m_strStockMedApplID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ���²ɹ���ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ���²ɹ���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objStoMedAppDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoUpdateStockApplDeByID(clsStockMedApplDetail_VO p_objStoMedAppDe)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"UPDATE t_opr_stockmedappldetail
							  SET rowno_chr='" + p_objStoMedAppDe.m_strRowNo + "',unitid_chr='" + p_objStoMedAppDe.m_objUnit.m_strUnitID +
                              "',productorid_chr='" + p_objStoMedAppDe.m_objProductor.m_strVendorID + "',qty_dec=" + p_objStoMedAppDe.m_fltQty.ToString() +
                              ",unitprice_mny=" + p_objStoMedAppDe.m_fltUnitPrice.ToString() +
                              ",tolmny_mny=" + p_objStoMedAppDe.m_fltTolMny.ToString() + "  " +
                              "WHERE stockmedappldetailid_chr='" + p_objStoMedAppDe.m_strStockMedApplDeID + "' and medicineid_chr='" + p_objStoMedAppDe.m_objMedicine.m_strMedicineID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ���ɹ���¼�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ɾ���ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStockMedApplID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDeleteStockApplByID(string p_strStockMedApplID)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"UPDATE t_opr_stockmedapplication
							  SET pstatus_int=3
							  WHERE stockmedapplid_chr = '" + p_strStockMedApplID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ���ɹ���ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ɾ���ɹ���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStockMedApplDeID"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDeleteStockApplDeByID(string p_strStockMedApplDeID)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"DELETE t_opr_stockmedappldetail
							  WHERE stockmedappldetailid_chr = '" + p_strStockMedApplDeID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��DataTable���ݴ��ݵ�VO��ȥ

        #region ��DataTable���ݴ��ݵ���¼VO��
        /// <summary>
        /// ��DataTable���ݴ��ݵ���¼VO��
        /// </summary>
        /// <param name="dtbSource">����Դ</param>
        /// <param name="objItems">��¼��VO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStockMedApplication_VO[] objItems)
        {
            objItems = new clsStockMedApplication_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStockMedApplication_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            objItems[i1] = new clsStockMedApplication_VO();
                            objItems[i1].m_strStockMedApplID = dtbSource.Rows[i1]["STOCKMEDAPPLID_CHR"].ToString().Trim();
                            objItems[i1].m_strStockMedApplNo = dtbSource.Rows[i1]["STOCKMEDAPPLNO_VCHR"].ToString().Trim();
                            objItems[i1].m_objStorage = new clsStorage_VO();
                            objItems[i1].m_objStorage.m_strStroageID = dtbSource.Rows[i1]["STORAGEID_CHR"].ToString().Trim();
                            objItems[i1].m_objStorage.m_strStroageName = dtbSource.Rows[i1]["STORAGENAME_VCHR"].ToString().Trim();
                            objItems[i1].m_strApplDate = dtbSource.Rows[i1]["APPLDATE_DAT"].ToString().Trim();

                            objItems[i1].m_objApplEmp = new clsEmployeeVO();
                            if (dtbSource.Rows[i1]["APPLEMP_CHR"].ToString().Trim() != null)
                            {
                                objItems[i1].m_objApplEmp.strEmpID = dtbSource.Rows[i1]["APPLEMP_CHR"].ToString().Trim();
                            }

                            objItems[i1].m_objApplDept = new clsDepartmentVO();
                            if (dtbSource.Rows[i1]["APPLEMP_CHR"].ToString().Trim() != null)
                            {
                                objItems[i1].m_objApplDept.strDeptID = dtbSource.Rows[i1]["APPLDEPT_CHR"].ToString().Trim();
                            }

                            objItems[i1].m_objVendor = new clsVendor_VO();
                            if (dtbSource.Rows[i1]["VENDORID_CHR"].ToString().Trim() != null)
                            {
                                objItems[i1].m_objVendor.m_strVendorID = dtbSource.Rows[i1]["VENDORID_CHR"].ToString().Trim();
                                objItems[i1].m_objVendor.m_strVendorName = dtbSource.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                            }

                            if (dtbSource.Rows[i1]["TOLMNY_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltTolMny = float.Parse(dtbSource.Rows[i1]["TOLMNY_MNY"].ToString().Trim());
                            }

                            if (dtbSource.Rows[i1]["PSTATUS_INT"].ToString().Trim() != null)
                            {
                                objItems[i1].m_intPStatus = int.Parse(dtbSource.Rows[i1]["PSTATUS_INT"].ToString().Trim());
                            }

                            objItems[i1].m_strMemo = dtbSource.Rows[i1]["MEMO_VCHR"].ToString().Trim();
                        }
                    }

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #region ��DataTable���ݴ��ݵ���ϸVO��
        /// <summary>
        /// ��DataTable���ݴ��ݵ���ϸVO��
        /// </summary>
        /// <param name="dtbSource">����Դ</param>
        /// <param name="objItems">��ϸVO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsStockMedApplDetail_VO[] objItems)
        {
            objItems = new clsStockMedApplDetail_VO[0];

            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStockMedApplDetail_VO[intRow];
                        for (int i1 = 0; i1 < intRow; i1++)
                        {
                            objItems[i1] = new clsStockMedApplDetail_VO();
                            objItems[i1].m_objMedicine = new clsMedicine_VO();
                            objItems[i1].m_objProductor = new clsVendor_VO();
                            objItems[i1].m_objUnit = new clsUnit_VO();

                            objItems[i1].m_strStockMedApplDeID = dtbSource.Rows[i1]["STOCKMEDAPPLDETAILID_CHR"].ToString().Trim();
                            objItems[i1].m_strStockMedApplID = dtbSource.Rows[i1]["STOCKMEDAPPLID_CHR"].ToString().Trim();
                            objItems[i1].m_strRowNo = dtbSource.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedicineID = dtbSource.Rows[i1]["MEDICINEID_CHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedicineName = dtbSource.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedSpec = dtbSource.Rows[i1]["MEDSPEC_VCHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i1]["MEDICINEENGNAME_VCHR"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strPYCode = dtbSource.Rows[i1]["pycode_chr"].ToString().Trim();
                            objItems[i1].m_objMedicine.m_strWBCode = dtbSource.Rows[i1]["wbcode_chr"].ToString().Trim();
                            objItems[i1].m_objUnit.m_strUnitID = dtbSource.Rows[i1]["UNITID_CHR"].ToString().Trim();
                            objItems[i1].m_objUnit.m_strUnitName = dtbSource.Rows[i1]["unitname_chr"].ToString().Trim();
                            objItems[i1].m_objProductor.m_strVendorID = dtbSource.Rows[i1]["PRODCUTORID_CHR"].ToString().Trim();
                            objItems[i1].m_objProductor.m_strVendorName = dtbSource.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();

                            if (dtbSource.Rows[i1]["QTY_DEC"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltQty = float.Parse(dtbSource.Rows[i1]["QTY_DEC"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["UNITPRICE_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltUnitPrice = float.Parse(dtbSource.Rows[i1]["UNITPRICE_MNY"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["DISCOUNT_DEC"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltDiscount = float.Parse(dtbSource.Rows[i1]["DISCOUNT_DEC"].ToString().Trim());
                            }
                            if (dtbSource.Rows[i1]["TOLMNY_MNY"].ToString().Trim() != null)
                            {
                                objItems[i1].m_fltTolMny = float.Parse(dtbSource.Rows[i1]["TOLMNY_MNY"].ToString().Trim());
                            }

                        }
                    }

                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
        }
        #endregion

        #endregion

        #region ģ����ѯ�ɹ���¼�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ģ����ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByAny(string p_strSQL, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = @"SELECT *
							  FROM v_opr_stockmedappl
							 " + p_strSQL;
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);



                if (lngRes > 0 && dtbResult != null)
                {
                    CopyDataTableToVO(dtbResult, out p_objResult);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �Բɹ���ID���Ҳɹ���¼�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// �Բɹ���¼��ID��ѯ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ɹ���ID</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByApplID(string p_strID, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE STOCKMEDAPPLID_CHR='" + p_strID + "'";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Բɹ����Ų��Ҳɹ���¼�� ŷ����ΰ 2004-06-17
        /// <summary>
        /// �Բɹ���¼���Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strNo">�ɹ�����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByApplNo(string p_strNo, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE STOCKMEDAPPLNO_VCHR='" + p_strNo + "'";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �Բֿ���Ҳɹ���¼�� ŷ����ΰ 2004-06-17
        /// <summary>
        /// �Բֿ��ѯ������Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ֿ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByStorage(string p_strID, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ��ʱ��β�ѯ�ɹ���¼����ŷ����ΰ��2004-06-17
        /// <summary>
        /// ��ʱ��β�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByDate(string p_strStartDate, string p_strEndDate, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE APPLDATE_DAT >=TO_DATE('" + p_strStartDate + "','yyyy-mm-dd hh24:mi:ss') AND APPLDATE_DAT <= TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region �����벿�Ų�ѯ�ɹ���¼����ŷ����ΰ��2004-06-17
        /// <summary>
        /// �����벿�Ų�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">���Ŵ���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByDept(string p_strID, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE APPLDEPT_CHR = '" + p_strID + "'";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ����Ӧ�̲�ѯ�ɹ���¼����ŷ����ΰ��2004-06-17
        /// <summary>
        /// ����Ӧ�̲�ѯ�ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">��Ӧ�̴���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplByVendor(string p_strID, out clsStockMedApplication_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplication_VO[0];

            string strSQL = "WHERE VENDORID_CHR = '" + p_strID + "'";
            lngRes = m_lngFindStockMedApplByAny(strSQL, out p_objResult);

            return lngRes;
        }
        #endregion

        #region ģ����ѯ�ɹ���ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ģ����ѯ�ɹ���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplDeByAny(string p_strSQL, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;

            p_objResult = new clsStockMedApplDetail_VO[0];

            string strSQL = @"SELECT *
							  FROM v_opr_stockmedappldetail
							 " + p_strSQL;
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);



                if (lngRes > 0 && dtbResult != null)
                {
                    CopyDataTableToVO(dtbResult, out p_objResult);
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }

        #endregion

        #region �Լ�¼��ID���Ҳɹ���ϸ�� ŷ����ΰ 2004-05-28
        /// <summary>
        /// �Լ�¼��ID��ѯ��ϸ��Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ɹ���ID</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplDeByID(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;

            p_objResult = new clsStockMedApplDetail_VO[0];

            string strSQL = "WHERE stockmedapplid_chr='" + p_strID + "'";

            lngRes = m_lngFindStockMedApplDeByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region ��ҩƷ��ѯ��ϸ����Ϣ��ŷ����ΰ��2004-06-17
        /// <summary>
        /// ��ҩƷ��ѯ�ɹ���ϸ����Ϣ
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">ҩƷ����</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStockMedApplDeByMedicine(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;

            p_objResult = new clsStockMedApplDetail_VO[0];

            string strSQL = "WHERE medicineid_chr='" + p_strID + "'";

            lngRes = m_lngFindStockMedApplDeByAny(strSQL, out p_objResult);

            return lngRes;

        }
        #endregion

        #region ������Ĳɹ���¼ID ŷ����ΰ 2004-05-28
        /// <summary>
        /// ��òɹ���¼�������ID
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxStockMedApplId(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("t_opr_stockmedapplication", "stockmedapplid_chr", 10);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ������Ĳɹ���ϸ���� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ��òɹ���ϸ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxStockMedApplDeId(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("t_opr_stockmedappldetail", "stockmedappldetailid_chr", 10);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region �Զ����ɲɹ��� ŷ����ΰ 2004-05-31
        /// <summary>
        /// �Զ����ɲɹ���
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��ʶ</param>
        /// <param name="p_strID">�ⷿID</param>
        /// <param name="p_objResult">�������</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAutoCalcStockMedAppl(string p_strID, out clsStockMedApplDetail_VO[] p_objResult)
        {
            long lngRes = 0;
            p_objResult = new clsStockMedApplDetail_VO[0];

            DataTable dtbResult = new DataTable();
            string strSQL = @"SELECT a.*, b.amount_dec, c.unitname_chr,
									 a.lowlimit_dec - b.amount_dec AS qty, d.medicinename_vchr,
									 d.medspec_vchr, d.productorid_chr, e.lowinprice_mny, e.curinprice_mny,
									 f.vendorname_vchr
								FROM t_bse_storagemedlimit a,
								     t_bse_storagemedicine b,
								     t_aid_unit c,
									 t_bse_medicine d,
									 t_bse_medicineprice e,
									 t_bse_vendor f
							   WHERE a.storageid_chr = b.storageid_chr
							     AND a.medicineid_chr = b.medicineid_chr
								 AND a.medicineid_chr = d.medicineid_chr
								 AND a.medicineid_chr = e.medicineid_chr
								 AND b.amount_dec < a.lowlimit_dec
								 AND d.productorid_chr = f.vendorid_chr(+)
							     AND a.unitid_chr = c.unitid_chr(+) ";
            strSQL += @" AND a.storageid_chr = '" + p_strID.Trim() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);



                if (lngRes > 0 && dtbResult != null)
                {
                    int intRow = dtbResult.Rows.Count;
                    p_objResult = new clsStockMedApplDetail_VO[intRow];
                    for (int i = 0; i < intRow; i++)
                    {
                        p_objResult[i] = new clsStockMedApplDetail_VO();
                        p_objResult[i].m_objMedicine = new clsMedicine_VO();
                        p_objResult[i].m_objUnit = new clsUnit_VO();
                        p_objResult[i].m_objProductor = new clsVendor_VO();
                        int RowNo = i + 1;
                        p_objResult[i].m_strRowNo = RowNo.ToString("0000");
                        p_objResult[i].m_objMedicine.m_strMedicineID = dtbResult.Rows[i]["medicineid_chr"].ToString().Trim();
                        p_objResult[i].m_objMedicine.m_strMedicineName = dtbResult.Rows[i]["medicinename_vchr"].ToString().Trim();
                        p_objResult[i].m_objMedicine.m_strMedSpec = dtbResult.Rows[i]["medspec_vchr"].ToString().Trim();
                        p_objResult[i].m_objUnit.m_strUnitID = dtbResult.Rows[i]["unitid_chr"].ToString().Trim();
                        p_objResult[i].m_objUnit.m_strUnitName = dtbResult.Rows[i]["unitname_chr"].ToString().Trim();
                        p_objResult[i].m_objProductor.m_strVendorID = dtbResult.Rows[i]["productorid_chr"].ToString().Trim();
                        p_objResult[i].m_objProductor.m_strVendorName = dtbResult.Rows[i]["vendorname_vchr"].ToString().Trim();
                        p_objResult[i].m_fltUnitPrice = float.Parse(dtbResult.Rows[i]["curinprice_mny"].ToString().Trim());
                        p_objResult[i].m_fltQty = float.Parse(dtbResult.Rows[i]["qty"].ToString().Trim());
                        p_objResult[i].m_fltDiscount = 1F;
                        p_objResult[i].m_fltTolMny = p_objResult[i].m_fltQty * p_objResult[i].m_fltUnitPrice;
                    }
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion

        #region ������Ĳɹ���¼���� ŷ����ΰ 2004-05-28
        /// <summary>
        /// ��òɹ���¼������󵥺�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMaxStockMedApplNo(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("t_opr_stockmedapplication", "stockmedapplno_vchr", 10);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            return lngRes;
        }
        #endregion


        #region  ��ϵͳ�Ĳ�������  2004-10-8

        #region ��ȡҩƷ��һ������Ĵ�����
        /// <summary>
        /// ��ȡҩƷ��һ������Ĵ�����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="medID"></param>
        /// <param name="VenName"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVen(string medID, out string VenName)
        {
            VenName = "Table1";
            long lngRes = 0;
            string strSQL = @"select c.VENDORNAME_VCHR  from (select Max(b.INORD_DAT) as MaxORD_DAT from t_opr_storageordde a,T_OPR_STORAGEORD b where a.MEDICINEID_CHR='" + medID + "' and a.STORAGEORDID_CHR=b.STORAGEORDID_CHR and b.SIGN_INT=1) a,T_OPR_STORAGEORD b,t_bse_vendor c where b.INORD_DAT=a.MaxORD_DAT and b.VENDORID_CHR=c.VENDORID_CHR";
            try
            {
                DataTable dtbResult = new DataTable();
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
                if (dtbResult.Rows.Count > 0)
                {
                    if (dtbResult.Rows[0]["VENDORNAME_VCHR"].ToString() != "")
                        VenName = dtbResult.Rows[0]["VENDORNAME_VCHR"].ToString();
                    else
                        VenName = "Table1";
                }
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }

        #endregion

        #region ��ȡ���ĵ��ݺ�
        /// <summary>
        /// ��ȡ���ĵ��ݺ�
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strMaxDoc"></param>
        /// <returns></returns>

        [AutoComplete]
        public long m_lngGetMaxDoc(out string p_strMaxDoc, string strdate)
        {
            p_strMaxDoc = null;
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
            string strSQL = @"select max(STOCKMEDAPPLNO_VCHR) as STOCKMEDAPPLNO from T_OPR_STOCKMEDAPPLICATION where STOCKMEDAPPLNO_VCHR like '" + strdate + "%'";
            DataTable dtbResult = new DataTable();
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                p_strMaxDoc = dtbResult.Rows[0]["STOCKMEDAPPLNO"].ToString();
            }
            return lngRes;
        }
        #endregion

        #region �������ҩƷ����
        /// <summary>
        /// �������ҩƷ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbMedicine"></param>
        /// <returns></returns>
        public long m_lngGetAllMedicine(out DataTable dtbMedicine)
        {
            dtbMedicine = null;
            long lngRes = 0;
            string strSQL = @"select a.MEDICINEID_CHR,a.MEDICINENAME_VCHR,a.MEDSPEC_VCHR,a.OPUNIT_CHR,a.PRODUCTORID_CHR,a.UNITPRICE_MNY,a.PYCODE_CHR,a.WBCODE_CHR,b.vendorname_vchr,c.AMOUNT_DEC from t_bse_medicine order by MEDICINEID_CHR";

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbMedicine);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ��ȡ���뵥��Ϣ
        /// <summary>
        ///��ȡ���뵥��Ϣ 
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetApplCation(out DataTable dtbResult, string date, string p_strStorageID)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = "SELECT a.*, b.storagename_vchr, c.vendorname_vchr as VENDORNANE_CHR,d.DEPTNAME_VCHR,e.LASTNAME_VCHR FROM t_opr_stockmedapplication a, t_bse_storage b, t_bse_vendor c,T_BSE_DEPTDESC d,t_bse_employee e WHERE a.storageid_chr = b.storageid_chr AND a.vendorid_chr = c.vendorid_chr(+) and a.APPLDEPT_CHR=d.DEPTID_CHR(+) and a.APPLEMP_CHR=e.EMPID_CHR(+) and a.STORAGEID_CHR='" + p_strStorageID + "' and APPLDATE_DAT between To_date( '" + date + " 00:00:00','yyyy-mm-dd hh24:mi:ss') and To_date( '" + date + " 23:59:59','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region �ɹ����
        /// <summary>
        /// �ɹ����
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_objResultArr"></param>
        /// <param name="p_objResultDeArr"></param>
        /// <param name="blnAuto">�Ƿ�Ҫ�Զ�������ⵥ</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAutoCompleteApp(clsMedStorageOrd_VO p_objResultArr, clsMedStorageOrdDe_VO[] p_objResultDeArr, bool blnAuto)
        {
            long lngRes = 0;
            string strApplID = p_objResultArr.m_strSTORAGEORDID_CHR;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = "";
            if (blnAuto == true)
            {
                DataTable dt = new DataTable();
                strSQL = @"select STORAGEORDTYPEID_CHR from t_aid_storageordtype where STORAGEORDTYPENAME_VCHR='�ɹ����' and SIGN_INT=1";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt.Rows.Count > 0)
                {
                    p_objResultArr.m_strSTORAGEORDTYPEID_CHR = dt.Rows[0]["STORAGEORDTYPEID_CHR"].ToString();
                    p_objResultArr.m_strINORD_DAT = DateTime.Now.ToString();
                }
                else
                {

                }
                DataTable dt1 = new DataTable();
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                strSQL = @"select PERIODID_CHR from T_BSE_PERIOD where STARTDATE_DAT<=to_date('" + date + "','yyyy-MM-dd')  and ENDDATE_DAT>=to_date('" + date + "','yyyy-MM-dd')";
                try
                {
                    lngRes = objHRPSvc.DoGetDataTable(strSQL, ref dt1);

                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
                if (dt1.Rows.Count > 0)
                {
                    p_objResultArr.m_strPERIODID_CHR = dt1.Rows[0]["PERIODID_CHR"].ToString();
                }
                else
                {

                }
                clsStorageOrdSvc ordSvc = new clsStorageOrdSvc();
                string newID;
                ordSvc.m_lngInsertMetStorageOrd(p_objResultArr, p_objResultDeArr, out newID, false, 1);
            }
            strSQL = @"update T_OPR_STOCKMEDAPPLICATION set PSTATUS_INT=2 where STOCKMEDAPPLID_CHR='" + strApplID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }

        #endregion
        #region ��ȡ���벿��
        [AutoComplete]
        public long m_lngGetDept(out clsT_BSE_DEPTDESC_VO[] DEPTDESC_VO)
        {
            DEPTDESC_VO = null;
            long lngRes = 0;
            string strSQL = "SELECT DEPTID_CHR,SHORTNO_CHR,DEPTNAME_VCHR,PYCODE_CHR,WBCODE_CHR from T_BSE_DEPTDESC  where ATTRIBUTEID='0000001' order by SHORTNO_CHR";
            DataTable dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                DEPTDESC_VO = new clsT_BSE_DEPTDESC_VO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    DEPTDESC_VO[i1] = new clsT_BSE_DEPTDESC_VO();
                    DEPTDESC_VO[i1].m_strDEPTID_CHR = dtbResult.Rows[i1]["DEPTID_CHR"].ToString().Trim();
                    DEPTDESC_VO[i1].m_strSHORTNO_CHR = dtbResult.Rows[i1]["SHORTNO_CHR"].ToString().Trim();
                    DEPTDESC_VO[i1].m_strDEPTNAME_VCHR = dtbResult.Rows[i1]["DEPTNAME_VCHR"].ToString().Trim();
                    DEPTDESC_VO[i1].m_strPYCODE_CHR = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                    DEPTDESC_VO[i1].m_strWBCODE_CHR = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                }
            }
            return lngRes;

        }
        #endregion

        #region ��ȡ��Ӧ������
        /// <summary>
        /// ��ȡ��Ӧ������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="VendorVO"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetVendor(out clsVendor_VO[] VendorVO)
        {
            long lngRes = 0;
            VendorVO = null;
            DataTable dtbResult = new DataTable();
            string strSQL = @"select USERCODE_CHR,VENDORNAME_VCHR,VENDORID_CHR,PYCODE_CHR,WBCODE_CHR from t_bse_vendor where VENDORTYPE_INT<>2 and PRODUCTTYPE_INT=1 order by USERCODE_CHR";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                VendorVO = new clsVendor_VO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    VendorVO[i1] = new clsVendor_VO();
                    VendorVO[i1].m_strUSERCODE_CHR = dtbResult.Rows[i1]["USERCODE_CHR"].ToString();
                    VendorVO[i1].m_strVendorID = dtbResult.Rows[i1]["VENDORID_CHR"].ToString();
                    VendorVO[i1].m_strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString();
                    VendorVO[i1].m_strWBCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString();
                    VendorVO[i1].m_strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString();
                }
            }
            return lngRes;
        }
        #endregion

        #region ��ȡԱ������
        public long m_lngGetEmployee(out clsEmployeeVO[] EmployeeVO)
        {
            long lngRes = 0;
            EmployeeVO = null;
            //			//Ȩ����
            //			clsPrivilegeHandleService objPrivilege = new clsPrivilegeHandleService();
            //			//����Ƿ���ʹ��Щ������Ȩ��
            //			lngRes = objPrivilege.m_lngCheckCallPrivilege(p_objPrincipal,"com.digitalwave.iCare.middletier.HIS.clsStorageMedLimitSvc","m_lngGetEmployee");
            //			if(lngRes < 0) //û��ʹ�õ�Ȩ��
            //			{
            //				return -1;
            //			}
            string strSQL = "SELECT EMPID_CHR,EMPNO_CHR,LASTNAME_VCHR,PYCODE_CHR from t_bse_employee where STATUS_INT=1 order by EMPNO_CHR";
            DataTable dtbResult = new DataTable();
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (dtbResult.Rows.Count > 0)
            {
                EmployeeVO = new clsEmployeeVO[dtbResult.Rows.Count];
                for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                {
                    EmployeeVO[i1] = new clsEmployeeVO();
                    EmployeeVO[i1].strEmpID = dtbResult.Rows[i1]["EMPID_CHR"].ToString().Trim();
                    EmployeeVO[i1].strEmpNO = dtbResult.Rows[i1]["EMPNO_CHR"].ToString().Trim();
                    EmployeeVO[i1].strLastName = dtbResult.Rows[i1]["LASTNAME_VCHR"].ToString().Trim();
                    EmployeeVO[i1].strPYCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                }

            }
            return lngRes;

        }


        #endregion

        #region ��ȡ����
        [AutoComplete]
        public long m_lngGetManufacturer(out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = "SELECT VENDORID_CHR,VENDORNAME_VCHR from t_bse_vendor where VENDORTYPE_INT<>1 and PRODUCTTYPE_INT=1";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }
        #endregion

        #region ���ݵ���ID��õ�����ϸ
        [AutoComplete]
        public long m_lngGetApplDeByID(string strID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = null;
            string strSQL = "SELECT a.*,b.MEDICINENAME_VCHR,b.ASSISTCODE_CHR,b.MEDSPEC_VCHR from t_opr_stockmedappldetail a,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.STOCKMEDAPPLID_CHR='" + strID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }


        #endregion

        #region  ������ϸ
        [AutoComplete]
        public long m_lngInsertDe(DataRow newRow)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string p_strNewDeID = "";
            objHRPSvc.m_lngGenerateNewID("t_opr_stockmedappldetail", "STOCKMEDAPPLDETAILID_CHR", out p_strNewDeID);
            string strSQL = @"insert into t_opr_stockmedappldetail(STOCKMEDAPPLDETAILID_CHR,STOCKMEDAPPLID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,PRODCUTORID_CHR,QTY_DEC,UNITPRICE_MNY,DISCOUNT_DEC,TOLMNY_MNY) 
                            values('" + p_strNewDeID + "','" + newRow["STOCKMEDAPPLID_CHR"].ToString() + "','" + newRow["ROWNO_CHR"].ToString() + "','" + newRow["MEDICINEID_CHR"].ToString() +
                            "','" + newRow["UNITID_CHR"].ToString() + "','" + newRow["PRODCUTORID_CHR"].ToString() + "'," + newRow["QTY_DEC"].ToString() + "," + newRow["UNITPRICE_MNY"].ToString() + ",1," + newRow["TOLMNY_MNY"].ToString() + ")";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            strSQL = @"update T_OPR_STOCKMEDAPPLICATION set TOLMNY_MNY=TOLMNY_MNY+" + newRow["TOLMNY_MNY"].ToString();
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;

        }

        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="newRow"></param>
        /// <param name="newTableDe"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSaveData(DataRow newRow, DataTable newTableDe, out string p_strNewID)
        {
            long lngRes = 0;
            p_strNewID = "";
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();

            objHRPSvc.m_lngGenerateNewID("t_opr_stockmedapplication", "STOCKMEDAPPLID_CHR", out p_strNewID);
            string strSQL = @"insert into t_opr_stockmedapplication(STOCKMEDAPPLID_CHR,STOCKMEDAPPLNO_VCHR,STORAGEID_CHR,APPLDATE_DAT,APPLEMP_CHR,APPLDEPT_CHR,VENDORID_CHR,TOLMNY_MNY,PSTATUS_INT,MEMO_VCHR) 
                            values('" + p_strNewID + "','" + newRow["STOCKMEDAPPLNO_VCHR"].ToString() + "','" + newRow["STORAGEID_CHR"].ToString() + "',To_Date('" + newRow["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
                ",'" + newRow["APPLEMP_CHR"].ToString() + "','" + newRow["APPLDEPT_CHR"].ToString() + "','" + newRow["VENDORID_CHR"].ToString() + "','" + newRow["TOLMNY_MNY"].ToString() + "',1,'" + newRow["MEMO_VCHR"].ToString() + "')";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            if (lngRes > 0)
            {
                string p_strNewDeID = "";
                for (int i1 = 0; i1 < newTableDe.Rows.Count; i1++)
                {
                    objHRPSvc.m_lngGenerateNewID("t_opr_stockmedappldetail", "STOCKMEDAPPLDETAILID_CHR", out p_strNewDeID);
                    strSQL = @"insert into t_opr_stockmedappldetail(STOCKMEDAPPLDETAILID_CHR,STOCKMEDAPPLID_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,PRODCUTORID_CHR,QTY_DEC,TOLMNY_MNY,UNITPRICE_MNY) 
                            values('" + p_strNewDeID + "','" + p_strNewID + "','" + newTableDe.Rows[i1]["ROWNO_CHR"].ToString() + "','" + newTableDe.Rows[i1]["MEDICINEID_CHR"].ToString() +
                        "','" + newTableDe.Rows[i1]["UNITID_CHR"].ToString() + "','" + newTableDe.Rows[i1]["PRODCUTORID_CHR"].ToString() + "','" + newTableDe.Rows[i1]["QTY_DEC"].ToString() +
                        "','" + newTableDe.Rows[i1]["TOLMNY_MNY"].ToString() + "','" + newTableDe.Rows[i1]["UNITPRICE_MNY"].ToString() + "')";
                    try
                    {
                        lngRes = objHRPSvc.DoExcute(strSQL);

                    }
                    catch (Exception objEx)
                    {
                        string strTmp = objEx.Message;
                        com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                        bool blnRes = objLogger.LogError(objEx);
                    }
                    if (lngRes != 1)
                        System.EnterpriseServices.ContextUtil.SetAbort();
                }
            }

            return lngRes;
        }
        #endregion

        #region �޸�����
        [AutoComplete]
        public long m_lngModify(DataRow newRow, DataRow newRowDe)
        {
            long lngRes = 0;
            long lngRes1 = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"UPDATE t_opr_stockmedappldetail set MEDICINEID_CHR='" + newRowDe["MEDICINEID_CHR"].ToString() + "',UNITID_CHR='" + newRowDe["UNITID_CHR"].ToString() +
                          "',PRODCUTORID_CHR='" + newRowDe["PRODCUTORID_CHR"].ToString() + "',QTY_DEC=" + newRowDe["QTY_DEC"].ToString() + ",UNITPRICE_MNY=" + newRowDe["UNITPRICE_MNY"].ToString() +
                          ",TOLMNY_MNY=" + newRowDe["TOLMNY_MNY"].ToString() + " where STOCKMEDAPPLDETAILID_CHR='" + newRowDe["STOCKMEDAPPLDETAILID_CHR"].ToString() + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"update t_opr_stockmedapplication set STORAGEID_CHR='" + newRow["STORAGEID_CHR"].ToString() + "',APPLDATE_DAT=To_Date('" + newRow["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),APPLEMP_CHR='" +
                   newRow["APPLEMP_CHR"].ToString() + "',APPLDEPT_CHR='" + newRow["APPLDEPT_CHR"].ToString() + "',VENDORID_CHR='" + newRow["VENDORID_CHR"].ToString() + "',TOLMNY_MNY=" + newRow["TOLMNY_MNY"].ToString() +
                   ",MEMO_VCHR='" + newRow["MEMO_VCHR"].ToString() + "' where STOCKMEDAPPLID_CHR='" + newRow["STOCKMEDAPPLID_CHR"].ToString() + "'";

            try
            {
                lngRes1 = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes > 0 && lngRes1 > 0)
                return 1;
            else
                return 0;

        }
        #endregion

        #region �޸Ĳɹ���

        [AutoComplete]
        public long m_lngModifyData(DataRow newRow)
        {
            long lngRes = 0;

            string strSQL = @"update t_opr_stockmedapplication set STORAGEID_CHR='" + newRow["STORAGEID_CHR"].ToString() + "',APPLDATE_DAT=To_Date('" + newRow["APPLDATE_DAT"].ToString() + "','yyyy-mm-dd hh24:mi:ss'),APPLEMP_CHR='" +
               newRow["APPLEMP_CHR"].ToString() + "',APPLDEPT_CHR='" + newRow["APPLDEPT_CHR"].ToString() + "',VENDORID_CHR='" + newRow["VENDORID_CHR"].ToString() + "',TOLMNY_MNY=" + newRow["TOLMNY_MNY"].ToString() +
               ",MEMO_VCHR='" + newRow["MEMO_VCHR"].ToString() + "' where STOCKMEDAPPLID_CHR='" + newRow["STOCKMEDAPPLID_CHR"].ToString() + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ���ɹ���ϸ��(ͨ��ID��
        /// <summary>
        /// ɾ���ɹ���ϸ��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStockMedApplDeID"></param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDelStockApplDeByID(string p_strStockMedApplDeID)
        {
            long lngRes = 0;

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"DELETE t_opr_stockmedappldetail
							  WHERE stockmedappldetailid_chr = '" + p_strStockMedApplDeID + "'";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.DoExcute(strSQL);


            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion

        #region ɾ���ɹ���¼��
        /// <summary>
        /// ɾ���ɹ���¼��
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="p_strStockMedApplID"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngDoDelStockApplByID(string p_strStockMedApplID)
        {
            long lngRes = 0;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string strSQL = @"DELETE t_opr_stockmedappldetail
							  WHERE STOCKMEDAPPLID_CHR = '" + p_strStockMedApplID + "'";
            try
            {
                lngRes = objHRPSvc.DoExcute(strSQL);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (lngRes > 0)
            {
                strSQL = @"delete t_opr_stockmedapplication
							  WHERE STOCKMEDAPPLID_CHR = '" + p_strStockMedApplID + "'";
                try
                {
                    lngRes = objHRPSvc.DoExcute(strSQL);
                }
                catch (Exception objEx)
                {
                    string strTmp = objEx.Message;
                    com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                    bool blnRes = objLogger.LogError(objEx);
                }
            }

            return lngRes;
        }
        #endregion

        #region ��ȡ���ϲɹ�����������
        [AutoComplete]
        public long m_lngGetData(out DataTable objDataTable)
        {
            long lngRes = 0;
            objDataTable = null;
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"select a.*,b.UNITID_CHR,b.LOWLIMIT_DEC,b.PLANQTY_DEC,c.MEDICINENAME_VCHR,d.STORAGENAME_VCHR from (select sum(CURQTY_DEC) as amount_dec,STORAGEID_CHR,MEDICINEID_CHR  from t_opr_storagemeddetail group by STORAGEID_CHR,MEDICINEID_CHR) a,t_bse_storagemedlimit b,t_bse_medicine c,t_bse_storage d 
                          where a.MEDICINEID_CHR=c.MEDICINEID_CHR and a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.STORAGEID_CHR=b.STORAGEID_CHR and a.STORAGEID_CHR=d.STORAGEID_CHR(+) and a.amount_dec<=b.lowlimit_dec";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref objDataTable);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            return lngRes;
        }
        #endregion
        #endregion
    }
    #endregion
}
