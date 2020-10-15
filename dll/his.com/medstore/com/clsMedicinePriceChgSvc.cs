using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    /// <summary>
    /// ���۲���
    /// Create by kong 2004-06-08
    /// </summary>
    /// 
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsMedicinePriceChgSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase    //MiddleTierBase.dll
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsMedicinePriceChgSvc()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #endregion

        #region  �������ۼ�¼��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// �������ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objItem">���ۼ�¼����</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO T_OPR_MEDICINEPRICECHGAPPL (MEDICINEPRICECHGAPPLID_CHR,MEDICINEPRICECHGAPPLNO_CHR,STORAGEORDTYPEID_CHR,STORAGEID_CHR,PERIODID_CHR,APPLDATE_DAT,PSTATUS_INT,MEMO_VCHR,CREATORID_CHR,CREATEDATE_DAT,ADUITEMP_CHR,ADUITDATE_DAT,ACCTEMP_CHR,ACCTDATE_DAT) 
							VALUES ('" + p_objItem.m_strMecicinePriceChgApplID + "','" + p_objItem.m_strMecicinePriceChgApplNo + "','" + p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID + "','" + p_objItem.m_objStorage.m_strStroageID + "','" + p_objItem.m_objPeriod.m_strPeriodID +
                            "',TO_DATE('" + p_objItem.m_strApplDate + "','yyyy-mm-dd hh24:mi:ss'), " + p_objItem.m_intPStatus.ToString().Trim() + ",'" + p_objItem.m_strMemo + "','" + p_objItem.m_objCreateor.strEmpID + "',TO_DATE('" + p_objItem.m_strCreateDate + "','yyyy-mm-dd hh24:mi:ss'),'" +
                            p_objItem.m_objAduitEmp.strEmpID + "',TO_DATE('" + p_objItem.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss'),'" + p_objItem.m_objAcctEmp.strEmpID + "',TO_DATE('" + p_objItem.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss') )";
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

        #region  �޸ĵ��ۼ�¼��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// �޸ĵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objItem">���ۼ�¼����</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoUpdMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE T_OPR_MEDICINEPRICECHGAPPL
							SET APPLDATE_DAT=TO_DATE('" + p_objItem.m_strApplDate + "','yyyy-mm-dd hh24:mi:ss'),PSTATUS_INT=" + p_objItem.m_intPStatus.ToString().Trim() +
                            ",MEMO_VCHR='" + p_objItem.m_strMemo + "' " +
                            " WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_objItem.m_strMecicinePriceChgApplID + "' ";

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

        #region  ɾ�����ۼ�¼��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ɾ�����ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">���ۼ�¼ID</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDeleteMedicinePriceChgAppl(string p_strID)
        {
            long lngRes = 0;
            string strSQL = @"DELETE T_OPR_MEDICINEPRICECHGAPPL
							WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_strID + "' ";

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

        #region  ����������ϸ��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ����������ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objItem">������ϸ����</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAddNewMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"INSERT INTO T_OPR_MEDICINEPRICECHGAPPLDE (MEDICINEPRICECHGAPPLDEID_CHR,MEDICINEPRICECHGAPPLNO_CHR,ROWNO_CHR,MEDICINEID_CHR,UNITID_CHR,QTY_DEC,CURPRICE_MNY,CHANGEPRICE_MNY,SYSLOTNO_CHR) 
							VALUES ('" + p_objItem.m_strMedicinePriceChgApplDeId + "','" + p_objItem.m_strMedicinePriceChgApplNo + "','" + p_objItem.m_strRowNo + "','" +
                            p_objItem.m_objMedicine.m_objMedicine.m_strMedicineID + "','" + p_objItem.m_objUnit.m_strUnitID + "'," + p_objItem.m_fltQty.ToString().Trim() + "," + p_objItem.m_fltCurPrice.ToString().Trim() +
                            "," + p_objItem.m_fltChangePrice + ",'" + p_objItem.m_strSysLotNo + "') ";

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

        #region  �޸ĵ�����ϸ��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// �޸ĵ�����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objItem">������ϸ����</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoUpdMedicinePriceChgApplDe(clsMedicinePriceChgApplDe_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_MEDICINEPRICECHGAPPLDE
							SET ROWNO_CHR='" + p_objItem.m_strRowNo + "',QTY_DEC=" + p_objItem.m_fltQty.ToString().Trim() + ",UNITID_CHR='" +
                            p_objItem.m_objUnit.m_strUnitID + "',CURPRICE_MNY=" + p_objItem.m_fltCurPrice.ToString().Trim() +
                            ",CHANGEPRICE_MNY=" + p_objItem.m_fltChangePrice.ToString().Trim() + " " +
                            " WHERE MEDICINEPRICECHGAPPLDEID_CHR='" + p_objItem.m_strMedicinePriceChgApplDeId + "' ";

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

        #region  ɾ��������ϸ��  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ɾ��������ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">������ϸID</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoDeleteMedicinePriceChgApplDe(string p_strID)
        {
            long lngRes = 0;

            string strSQL = @"DELETE T_OPR_MEDICINEPRICECHGAPPLDE
							WHERE MEDICINEPRICECHGAPPLDEID_CHR='" + p_strID + "' ";

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

        #region ��˵��۵�  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ��˵��۵�
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objItem">���۵�����</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoAduitMedicinePriceChgAppl(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_MEDICINEPRICECHGAPPL
							SET ADUITEMP_CHR='" + p_objItem.m_objAduitEmp.strEmpID + "',ADUITDATE_DAT=TO_DATE('" + p_objItem.m_strAduitDate + "','yyyy-mm-dd hh24:mi:ss') " +
                            " WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_objItem.m_strMecicinePriceChgApplID + "' ";

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

        #region ��˵��۵�����ļ۸�  ŷ����ΰ  2004-06-10
        /// <summary>
        /// ��˵��۵�����ļ۸�
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strNo">���ۼ�¼����</param>
        /// <param name="p_intFlag">�����ʶ��1Ϊ�ɹ���0Ϊ���ĳ�����1Ϊ�����쳣</param>
        /// <returns></returns>
        /// 
        [AutoComplete]
        public long m_lngDoChangePriceAfterAduit(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;

            try
            {
                string strProcedure = "P_MEDICINECHANGEPRICEADUIT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strNo;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "ApplNo";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Name = "Flag";
                objParams[1].strParameter_Direction = clsDirection.strOutput;

                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);

                p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());


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

        #region ��DataTable���ݴ��ݵ����ۼ�¼VO  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ��DataTable���ݴ��ݵ����ۼ�¼VO
        /// </summary>
        /// <param name="dtbSource">DataTable����</param>
        /// <param name="objResultArr">���ۼ�¼VO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsMedicinePriceChgAppl_VO[] objResultArr)
        {
            objResultArr = new clsMedicinePriceChgAppl_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objResultArr = new clsMedicinePriceChgAppl_VO[intRow];
                        for (int i = 0; i < dtbSource.Rows.Count; i++)
                        {
                            objResultArr[i] = new clsMedicinePriceChgAppl_VO();
                            objResultArr[i].m_objAcctEmp = new clsEmployeeVO();
                            objResultArr[i].m_objAduitEmp = new clsEmployeeVO();
                            objResultArr[i].m_objCreateor = new clsEmployeeVO();
                            objResultArr[i].m_objPeriod = new clsPeriod_VO();
                            objResultArr[i].m_objStorage = new clsStorage_VO();
                            objResultArr[i].m_objStorageOrdType = new clsStorageOrdType_VO();

                            objResultArr[i].m_strMecicinePriceChgApplID = dtbSource.Rows[i]["MEDICINEPRICECHGAPPLID_CHR"].ToString().Trim();
                            objResultArr[i].m_strMecicinePriceChgApplNo = dtbSource.Rows[i]["MEDICINEPRICECHGAPPLNO_CHR"].ToString().Trim();
                            objResultArr[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["STORAGEORDTYPEID_CHR"].ToString().Trim();
                            objResultArr[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["STORAGEORDTYPENAME_VCHR"].ToString().Trim();
                            objResultArr[i].m_objStorageOrdType.m_intSign = int.Parse(dtbSource.Rows[i]["SIGN_INT"].ToString().Trim());
                            objResultArr[i].m_objStorageOrdType.m_intDeptType = int.Parse(dtbSource.Rows[i]["DEPTTYPE_INT"].ToString().Trim());
                            objResultArr[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["STORAGEID_CHR"].ToString().Trim();
                            objResultArr[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["STORAGENAME_VCHR"].ToString().Trim();
                            objResultArr[i].m_objPeriod.m_strPeriodID = dtbSource.Rows[i]["PERIODID_CHR"].ToString().Trim();
                            objResultArr[i].m_objPeriod.m_strStartDate = dtbSource.Rows[i]["STARTDATE_DAT"].ToString().Trim();
                            objResultArr[i].m_objPeriod.m_strEndDate = dtbSource.Rows[i]["ENDDATE_DAT"].ToString().Trim();
                            objResultArr[i].m_strApplDate = dtbSource.Rows[i]["APPLDATE_DAT"].ToString().Trim();

                            string strStatus = dtbSource.Rows[i]["PSTATUS_INT"].ToString().Trim();
                            if (strStatus == "" || strStatus == " ")
                            {
                                strStatus = "1";
                            }
                            objResultArr[i].m_intPStatus = int.Parse(strStatus);
                            objResultArr[i].m_strMemo = dtbSource.Rows[i]["MEMO_VCHR"].ToString().Trim();
                            objResultArr[i].m_objCreateor.strEmpID = dtbSource.Rows[i]["CREATORID_CHR"].ToString().Trim();
                            objResultArr[i].m_strCreateDate = dtbSource.Rows[i]["CREATEDATE_DAT"].ToString().Trim();
                            objResultArr[i].m_objAduitEmp.strEmpID = dtbSource.Rows[i]["ADUITEMP_CHR"].ToString().Trim();
                            objResultArr[i].m_strAduitDate = dtbSource.Rows[i]["ADUITDATE_DAT"].ToString().Trim();
                            objResultArr[i].m_objAcctEmp.strEmpID = dtbSource.Rows[i]["ACCTEMP_CHR"].ToString().Trim();
                            objResultArr[i].m_strAcctDate = dtbSource.Rows[i]["ACCTDATE_DAT"].ToString().Trim();

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

        #region ��DataTable���ݴ��ݵ�������ϸVO  ŷ����ΰ  2004-06-08
        /// <summary>
        /// ��DataTable���ݴ��ݵ�������ϸVO
        /// </summary>
        /// <param name="dtbSource">DataTable����</param>
        /// <param name="objResultArr">������ϸVO</param>
        private void CopyDataTableToVO(System.Data.DataTable dtbSource, out clsMedicinePriceChgApplDe_VO[] objResultArr)
        {
            objResultArr = new clsMedicinePriceChgApplDe_VO[dtbSource.Rows.Count];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objResultArr = new clsMedicinePriceChgApplDe_VO[intRow];

                        for (int i = 0; i < dtbSource.Rows.Count; i++)
                        {
                            objResultArr[i] = new clsMedicinePriceChgApplDe_VO();
                            objResultArr[i].m_objMedicine = new clsStorageMedDetail_VO();
                            objResultArr[i].m_objMedicine.m_objMedicine = new clsMedicine_VO();
                            objResultArr[i].m_objUnit = new clsUnit_VO();
                            objResultArr[i].m_objMedicine.m_objProduct = new clsVendor_VO();

                            objResultArr[i].m_strMedicinePriceChgApplDeId = dtbSource.Rows[i]["MEDICINEPRICECHGAPPLDEID_CHR"].ToString().Trim();
                            objResultArr[i].m_strMedicinePriceChgApplNo = dtbSource.Rows[i]["MEDICINEPRICECHGAPPLNO_CHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["MEDICINEID_CHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["MEDICINENAME_VCHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["MEDSPEC_VCHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strPYCode = dtbSource.Rows[i]["PYCODE_CHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strWBCode = dtbSource.Rows[i]["WBCODE_CHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["MEDICINEENGNAME_VCHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objProduct.m_strVendorID = dtbSource.Rows[i]["PRODUCTORID_CHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_objProduct.m_strVendorName = dtbSource.Rows[i]["VENDORNAME_VCHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_strLotNo = dtbSource.Rows[i]["LOTNO_VCHR"].ToString().Trim();
                            objResultArr[i].m_objMedicine.m_strUsefulLife = dtbSource.Rows[i]["USEFULLIFE_DAT"].ToString().Trim();

                            string strBuyPrice = dtbSource.Rows[i]["BUYUNITPRICE_MNY"].ToString().Trim();

                            if (strBuyPrice == "" || strBuyPrice == " ")
                            {
                                strBuyPrice = "0";
                            }
                            objResultArr[i].m_objMedicine.m_fltBuyUnitPrice = float.Parse(strBuyPrice);

                            objResultArr[i].m_strRowNo = dtbSource.Rows[i]["ROWNO_CHR"].ToString().Trim();
                            objResultArr[i].m_objUnit.m_strUnitID = dtbSource.Rows[i]["UNITID_CHR"].ToString().Trim();
                            objResultArr[i].m_objUnit.m_strUnitName = dtbSource.Rows[i]["UNITNAME_CHR"].ToString().Trim();

                            string strQty = dtbSource.Rows[i]["QTY_DEC"].ToString().Trim();
                            if (strQty == "" || strQty == " ")
                            {
                                strQty = "0";
                            }
                            objResultArr[i].m_fltQty = float.Parse(strQty);

                            string strCurPrice = dtbSource.Rows[i]["CURPRICE_MNY"].ToString().Trim();
                            if (strCurPrice == "" || strCurPrice == " ")
                            {
                                strCurPrice = "0";
                            }
                            objResultArr[i].m_fltCurPrice = float.Parse(strCurPrice);

                            string strChangePrice = dtbSource.Rows[i]["CHANGEPRICE_MNY"].ToString().Trim();
                            if (strChangePrice == "" || strChangePrice == " ")
                            {
                                strChangePrice = "0";
                            }
                            objResultArr[i].m_fltChangePrice = float.Parse(strChangePrice);

                            objResultArr[i].m_strSysLotNo = dtbSource.Rows[i]["SYSLOTNO_CHR"].ToString().Trim();

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

        #region ģ�����ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ģ�����ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByAny(string p_strSQL, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = @"SELECT *
								FROM v_opr_medicinepricechgappl
							" + p_strSQL + " ";

            System.Data.DataTable dtbResult = new DataTable();

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

            if (lngRes > 0)
            {
                CopyDataTableToVO(dtbResult, out p_objResultArr);
            }

            return lngRes;
        }
        #endregion

        #region �����ۼ�¼��ID���ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼��ID���ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">���ۼ�¼��ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByID(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE MEDICINEPRICECHGAPPLID_CHR='" + p_strID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �����ۼ�¼���Ų��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼���Ų��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strNo">���ۼ�¼����</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByNo(string p_strNo, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE MEDICINEPRICECHGAPPLNO_CHR='" + p_strNo + "' ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���������Ͳ��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���������Ͳ��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">��������ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByStorageOrdType(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE STORAGEORDTYPEID_CHR='" + p_strID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region  ���ֿ���ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ֿ���ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">�ֿ�ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByStorage(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE STORAGEID_CHR='" + p_strID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region  �������ڲ��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �������ڲ��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">������ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByPeriod(string p_strID, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE PERIODID_CHR='" + p_strID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ������ʱ��β��ҵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ������ʱ��β��ҵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strStartDate">��ʼʱ��</param>
        /// <param name="p_strEndDate">����ʱ��</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplByDate(string p_strStartDate, string p_strEndDate, out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = "WHERE APPLDATE_DAT >= TO_DATE('" + p_strStartDate + "','yyyy-mm-dd hh24:mi:ss') AND APPLDATE_DAT <= TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss') ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �������еĵ��ۼ�¼��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �������еĵ��ۼ�¼��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindAllMedicinePriceChgAppl(out clsMedicinePriceChgAppl_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgAppl_VO[0];

            string strSQL = " ";

            lngRes = m_lngFindMedicinePriceChgApplByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region  ģ�����ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ģ�����ҵ�����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strSQL">SQL���</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplDeByAny(string p_strSQL, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];
            string strSQL = @"SELECT *
								FROM v_opr_medicinepricechgapplde
							 " + p_strSQL + " ";

            System.Data.DataTable dtbResult = new DataTable();

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

            if (lngRes > 0)
            {
                CopyDataTableToVO(dtbResult, out p_objResultArr);
            }


            return lngRes;
        }
        #endregion

        #region ��������ϸ��ID���ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��������ϸ��ID���ҵ�����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strID">������ϸ��ID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplDeByID(string p_strID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];
            string strSQL = "WHERE MEDICINEPRICECHGAPPLDEID_CHR='" + p_strID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region �����ۼ�¼���Ų��ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// �����ۼ�¼���Ų��ҵ�����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strNo">���ۼ�¼����</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplDeByApplNo(string p_strNo, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];
            string strSQL = "WHERE MEDICINEPRICECHGAPPLNO_CHR='" + p_strNo + "' ";

            lngRes = m_lngFindMedicinePriceChgApplDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ��ҩƷ���ҵ�����ϸ��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ��ҩƷ���ҵ�����ϸ��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strMedicineID">ҩƷID</param>
        /// <param name="p_objResultArr">�����Ϣ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngFindMedicinePriceChgApplDeByMedicine(string p_strMedicineID, out clsMedicinePriceChgApplDe_VO[] p_objResultArr)
        {
            long lngRes = 0;

            p_objResultArr = new clsMedicinePriceChgApplDe_VO[0];
            string strSQL = "WHERE MEDICINEID_CHR='" + p_strMedicineID + "' ";

            lngRes = m_lngFindMedicinePriceChgApplDeByAny(strSQL, out p_objResultArr);

            return lngRes;
        }
        #endregion

        #region ���ҵ�ǰ���ĵ��ۼ�¼��ID��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ���ĵ��ۼ�¼��ID��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strResult">���ֵ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngGetApplID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("T_OPR_MEDICINEPRICECHGAPPL", "MEDICINEPRICECHGAPPLID_CHR", 10);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (p_strResult == "" || p_strResult == " ")
            {
                p_strResult = "0000000000";
            }

            long lngID = long.Parse(p_strResult);

            if (lngID < 1)
            {
                lngID = 1;
                p_strResult = lngID.ToString("0000000000");
            }

            return lngRes;

        }
        #endregion

        #region ���ҵ�ǰ�����������ĵ��ۼ�¼����  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ�������Ͷ�Ӧ���ĵ��ۼ�¼����
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strStorageOrdTypeID">��������</param>
        /// <param name="p_strResult">���ֵ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngGetApplNo(string p_strStorageOrdTypeID, out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;
            string strSQL = @"SELECT max(MEDICINEPRICECHGAPPLNO_CHR) as maxid
							  FROM T_OPR_MEDICINEPRICECHGAPPL
							  WHERE STORAGEORDTYPEID_CHR ='" + p_strStorageOrdTypeID + "'";
            System.Data.DataTable dtbResult = new System.Data.DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0 && dtbResult != null)
                {
                    if (dtbResult.Rows.Count > 0)
                    {
                        p_strResult = dtbResult.Rows[0]["MAXID"].ToString();

                        if (p_strResult == "" || p_strResult == " ")
                        {
                            p_strResult = "0000000000";
                        }

                        long lngNo = long.Parse(p_strResult);

                        if (lngNo < 1)
                        {
                            lngNo = 1;
                            p_strResult = lngRes.ToString("0000000000");
                        }
                        else
                        {
                            lngRes += 1;
                            p_strResult = lngRes.ToString("0000000000");
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

            return lngRes;

        }
        #endregion

        #region ���ҵ�ǰ���ĵ�����ϸ��ID��  ŷ����ΰ  2004-06-09
        /// <summary>
        /// ���ҵ�ǰ���ĵ�����ϸ��ID��
        /// </summary>
        /// <param name="p_objPrincipal">��ȫ��</param>
        /// <param name="p_strResult">���ֵ</param>
        /// <returns>����ֵ</returns>
        [AutoComplete]
        public long m_lngGetApplDeID(out string p_strResult)
        {
            long lngRes = 0;
            p_strResult = null;
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                p_strResult = objHRPSvc.m_strGetNewID("T_OPR_MEDICINEPRICECHGAPPLDE", "MEDICINEPRICECHGAPPLDEID_CHR", 10);

            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }

            if (p_strResult == "" || p_strResult == " ")
            {
                p_strResult = "0000000000";
            }

            long lngID = long.Parse(p_strResult);

            if (lngID < 1)
            {
                lngID = 1;
                p_strResult = lngID.ToString("0000000000");
            }

            return lngRes;

        }
        #endregion


    }
}
