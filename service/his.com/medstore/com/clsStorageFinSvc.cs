using System;
using System.Data;
using com.digitalwave.Utility;//Utility.dll
using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using weCare.Core.Entity;
using System.EnterpriseServices;

namespace com.digitalwave.iCare.middletier.HIS
{
    #region 帐务 欧阳孔伟 2004-06-15
    /// <summary>
    /// 库房帐务，包括药品明细帐务，药品月结帐务，药品总帐，库房月结帐务，库房总帐
    /// </summary>	
    [Transaction(TransactionOption.Required)]
    [ObjectPooling(Enabled = true)]
    public class clsStorageFinSvc : com.digitalwave.iCare.middletier.clsMiddleTierBase  //MiddleTierBase.dll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public clsStorageFinSvc()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 登帐及改帐

        #region 查询登帐单据  欧阳孔伟  2004-06-16
        /// <summary>
        /// 查询登帐单据
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStorageID">仓库代码</param>
        /// <param name="p_strPeriodID">帐务期代码</param>
        /// <param name="p_blnFlag">审核标志，true：未审核，false：已审核</param>
        /// <param name="p_dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngSelectAcct(string p_strStorageID, string p_strPeriodID, bool p_blnFlag, out System.Data.DataTable p_dtbResult)
        {
            long lngRes = 0;
            p_dtbResult = null;
            string strAcct;
            if (p_blnFlag)
            {
                strAcct = "IS NULL";
            }
            else
            {
                strAcct = "IS NOT NULL";
            }

            //检索语句
            string strSQL = @"SELECT *
							  FROM v_opr_selectacctord
							 WHERE period = '" + p_strPeriodID.Trim() + "'";
            strSQL += @"
							   AND STORAGE = '" + p_strStorageID.Trim() + "'";
            strSQL += @"
							   AND acctemp " + strAcct;
            strSQL += @"
							   AND acctemp " + strAcct + " ";
            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref p_dtbResult);


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

        #region 出入库登帐  欧阳孔伟  2004-06-15
        /// <summary>
        /// 出入库登帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objItem">出入库数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAcct(clsStorageOrd_VO p_objItem)
        {
            long lngRes = 0;
            //更新单据记录表
            string strSQL = @"UPDATE t_opr_StorageOrd SET acctemp_chr='" + p_objItem.m_objAcctEmp.strEmpID + "',acctdate_dat=To_date('" + p_objItem.m_strAcctDate + "','yyyy.mm.dd hh24:mi:ss') " +
                " WHERE storageordid_chr='" + p_objItem.m_strStorageOrdID + "' and storageordtypeid_chr='" + p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID + "' and storageid_chr = '" + p_objItem.m_objStorage.m_strStroageID + "'";
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

        #region 出入库登帐后更改帐务
        /// <summary>
        /// 出入登帐后更改帐务
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据号</param>
        /// <param name="p_strTypeID">单据类型号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChgFinAfterOrdAcct(string p_strID, string p_strTypeID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_STORAGEORDACCT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[3];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "ordid";

                objParams[1].objParameter_Value = p_strTypeID;
                objParams[1].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[1].strParameter_Name = "typeid";

                objParams[2].strParameter_Type = clsOracleDbType.strInt32;
                objParams[2].strParameter_Direction = clsDirection.strOutput;
                objParams[2].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                if (objParams[2].objParameter_Value == null ||
                    objParams[2].objParameter_Value.ToString().Trim() == "")
                {
                    p_intFlag = -1;
                }
                else
                {
                    p_intFlag = int.Parse(objParams[2].objParameter_Value.ToString().Trim());
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

        #region 盘点单登帐  欧阳孔伟  2004-06-15
        /// <summary>
        /// 盘点单登帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objItem">盘点单数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAcct(clsStorageCheck_VO p_objItem)
        {
            long lngRes = 0;
            string strSQL = @"UPDATE T_OPR_STORAGECHECK
							SET ACCTEMP_CHR='" + p_objItem.m_objAcctEmp.strEmpID.Trim() + "',ACCTDATE_DAT=TO_DATE('" + p_objItem.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss') " +
                " WHERE STORAGECHECKID_CHR='" + p_objItem.m_strStorageCheckID + "' AND STORAGEID_CHR='" + p_objItem.m_objStorage.m_strStroageID.Trim() +
                "' AND STORAGEORDTYPEID_CHR='" + p_objItem.m_objStorageOrdType.m_strStorageOrdTypeID.Trim() +
                "' AND PERIODID_CHR='" + p_objItem.m_objPeriod.m_strPeriodID.Trim() + "' ";
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

        #region 盘点单登帐后更改帐务
        /// <summary>
        /// 盘点单登帐后更改帐务
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        ///	<param name="p_strID">盘点单号</param>
        /// <param name="p_intFlag">返回标识，1：成功，0：失败，-1：异常</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChgFinAfterCheckAcct(string p_strID, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_STORAGECHECKACCT";
                clsSQLParamDefinitionVO[] objParams = new clsSQLParamDefinitionVO[2];

                for (int i = 0; i < objParams.Length; i++)
                {
                    objParams[i] = new clsSQLParamDefinitionVO();
                }
                objParams[0].objParameter_Value = p_strID;
                objParams[0].strParameter_Type = clsOracleDbType.strVarchar2;
                objParams[0].strParameter_Name = "checkid";

                objParams[1].strParameter_Type = clsOracleDbType.strInt32;
                objParams[1].strParameter_Direction = clsDirection.strOutput;
                objParams[1].strParameter_Name = "flag";


                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new clsHRPTableService();
                lngRes = objHRPSvc.lngExecuteParameterProc(strProcedure, objParams);
                if (objParams[1].objParameter_Value == null ||
                    objParams[1].objParameter_Value.ToString().Trim() == "")
                {
                    p_intFlag = -1;
                }
                else
                {
                    p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());
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

        #region 调价单登帐  欧阳孔伟  2004-06-11
        /// <summary>
        /// 调价单登帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_objItem">调价单数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngAcct(clsMedicinePriceChgAppl_VO p_objItem)
        {
            long lngRes = 0;

            string strSQL = @"UPDATE T_OPR_MEDICINEPRICECHGAPPL
							SET ACCTEMP_CHR='" + p_objItem.m_objAcctEmp.strEmpID + "',ACCTDATE_DAT=TO_DATE('" + p_objItem.m_strAcctDate + "','yyyy-mm-dd hh24:mi:ss') " +
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

        #region 调价登帐后更改帐务
        /// <summary>
        /// 调价登帐后更改帐务
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strNo">调价单号</param>
        /// <param name="p_intFlag">返回标识，1：成功  0：失败  -1：异常</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngChgFinAfterChangePriceAcct(string p_strNo, out int p_intFlag)
        {
            long lngRes = 0;
            p_intFlag = 0;
            try
            {
                string strProcedure = "P_MEDICINECHANGEPRICEACCT";
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

                if (objParams[1].objParameter_Value == null ||
                    objParams[1].objParameter_Value.ToString().Trim() == "")
                {
                    p_intFlag = -1;
                }
                else
                {
                    p_intFlag = int.Parse(objParams[1].objParameter_Value.ToString().Trim());
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

        #endregion

        #region 帐务查询

        #region 将DataTable数据传递到VO

        #region 将DataTable数据传递到库房药品明细帐务VO
        /// <summary>
        /// 将DataTable数据传递到库房药品明细帐务VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据源</param>
        /// <param name="objItems">库房药品明细帐务数据</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageOprOrdFinDe_VO[] objItems)
        {
            objItems = new clsStorageOprOrdFinDe_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageOprOrdFinDe_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItems[i] = new clsStorageOprOrdFinDe_VO();
                            objItems[i].m_objStorage = new clsStorage_VO();
                            objItems[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
                            objItems[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();
                            objItems[i].m_objPeriod = new clsPeriod_VO();
                            objItems[i].m_objPeriod.m_strPeriodID = dtbSource.Rows[i]["periodid_chr"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strStartDate = dtbSource.Rows[i]["startdate_dat"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strEndDate = dtbSource.Rows[i]["enddate_dat"].ToString().Trim();
                            objItems[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["storageordtypename_vchr"].ToString().Trim();
                            objItems[i].m_strStorageOprOrdID = dtbSource.Rows[i]["storageoprordid_chr"].ToString().Trim();
                            objItems[i].m_objMedicine = new clsMedicine_VO();
                            objItems[i].m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["medicineid_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["medicinename_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["medspec_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strPYCode = dtbSource.Rows[i]["pycode_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strWBCode = dtbSource.Rows[i]["wbcode_chr"].ToString().Trim();
                            objItems[i].m_strSyslotNo = dtbSource.Rows[i]["syslotno_chr"].ToString().Trim();
                            objItems[i].m_objUnit = new clsUnit_VO();
                            objItems[i].m_objUnit.m_strUnitID = dtbSource.Rows[i]["unitid_chr"].ToString().Trim();
                            objItems[i].m_objUnit.m_strUnitName = dtbSource.Rows[i]["unitname_chr"].ToString().Trim();
                            objItems[i].m_objOperator = new clsEmployeeVO();
                            objItems[i].m_objOperator.strEmpID = dtbSource.Rows[i]["operatorid_chr"].ToString().Trim();
                            objItems[i].m_strCreateDate = dtbSource.Rows[i]["createdate_dat"].ToString().Trim();

                            string strQty = dtbSource.Rows[i]["qty_dec"].ToString().Trim();
                            string strBorRow = dtbSource.Rows[i]["borrow_mny"].ToString().Trim();
                            string strLoan = dtbSource.Rows[i]["loan_mny"].ToString().Trim();
                            if (strQty == "")
                            {
                                strQty = "0";
                            }
                            if (strBorRow == "")
                            {
                                strBorRow = "0";
                            }
                            if (strLoan == "")
                            {
                                strLoan = "0";
                            }
                            objItems[i].m_decQty = Convert.ToDecimal(strQty);
                            objItems[i].m_decBorrow = Convert.ToDecimal(strBorRow);
                            objItems[i].m_decLoan = Convert.ToDecimal(strLoan);
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

        #region 将DataTable数据传递到库房药品月结帐务VO
        /// <summary>
        /// 将DataTable数据传递到库房药品月结帐务VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据源</param>
        /// <param name="objItems">库房药品月结帐务数据</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageMedMonFin_VO[] objItems)
        {
            objItems = new clsStorageMedMonFin_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageMedMonFin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItems[i] = new clsStorageMedMonFin_VO();
                            objItems[i].m_objStorage = new clsStorage_VO();
                            objItems[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
                            objItems[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();
                            objItems[i].m_objPeriod = new clsPeriod_VO();
                            objItems[i].m_objPeriod.m_strPeriodID = dtbSource.Rows[i]["periodid_chr"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strStartDate = dtbSource.Rows[i]["startdate_dat"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strEndDate = dtbSource.Rows[i]["enddate_dat"].ToString().Trim();
                            objItems[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["storageordtypename_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine = new clsMedicine_VO();
                            objItems[i].m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["medicineid_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["medicinename_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["medspec_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strPYCode = dtbSource.Rows[i]["pycode_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strWBCode = dtbSource.Rows[i]["wbcode_chr"].ToString().Trim();

                            string strBorRow = dtbSource.Rows[i]["borrow_mny"].ToString().Trim();
                            string strLoan = dtbSource.Rows[i]["loan_mny"].ToString().Trim();
                            if (strBorRow == "")
                            {
                                strBorRow = "0";
                            }
                            if (strLoan == "")
                            {
                                strLoan = "0";
                            }
                            objItems[i].m_decBorrow = Convert.ToDecimal(strBorRow);
                            objItems[i].m_dedLoan = Convert.ToDecimal(strLoan);
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

        #region 将DataTable数据传递到库房药品总帐务
        /// <summary>
        /// 将DataTable数据传递到库房药品总帐务VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据源</param>
        /// <param name="objItems">库房药品总帐务务数据</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageMedFin_VO[] objItems)
        {
            objItems = new clsStorageMedFin_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageMedFin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItems[i] = new clsStorageMedFin_VO();
                            objItems[i].m_objStorage = new clsStorage_VO();
                            objItems[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
                            objItems[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["storageordtypename_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine = new clsMedicine_VO();
                            objItems[i].m_objMedicine.m_strMedicineID = dtbSource.Rows[i]["medicineid_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineName = dtbSource.Rows[i]["medicinename_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedSpec = dtbSource.Rows[i]["medspec_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strMedicineEngName = dtbSource.Rows[i]["medicineengname_vchr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strPYCode = dtbSource.Rows[i]["pycode_chr"].ToString().Trim();
                            objItems[i].m_objMedicine.m_strWBCode = dtbSource.Rows[i]["wbcode_chr"].ToString().Trim();

                            string strBorRow = dtbSource.Rows[i]["borrow_mny"].ToString().Trim();
                            string strLoan = dtbSource.Rows[i]["loan_mny"].ToString().Trim();
                            if (strBorRow == "")
                            {
                                strBorRow = "0";
                            }
                            if (strLoan == "")
                            {
                                strLoan = "0";
                            }
                            objItems[i].m_decBorrow = Convert.ToDecimal(strBorRow);
                            objItems[i].m_decLoan = Convert.ToDecimal(strLoan);
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

        #region 将DataTable数据传递到库房月结帐务VO
        /// <summary>
        /// 将DataTable数据传递到库房月结帐务VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据源</param>
        /// <param name="objItems">库房月结帐务数据</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageMonthFin_VO[] objItems)
        {
            objItems = new clsStorageMonthFin_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageMonthFin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItems[i] = new clsStorageMonthFin_VO();
                            objItems[i].m_objStorage = new clsStorage_VO();
                            objItems[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
                            objItems[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();
                            objItems[i].m_objPeriod = new clsPeriod_VO();
                            objItems[i].m_objPeriod.m_strPeriodID = dtbSource.Rows[i]["periodid_chr"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strStartDate = dtbSource.Rows[i]["startdate_dat"].ToString().Trim();
                            objItems[i].m_objPeriod.m_strEndDate = dtbSource.Rows[i]["enddate_dat"].ToString().Trim();
                            objItems[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["storageordtypename_vchr"].ToString().Trim();

                            string strBorRow = dtbSource.Rows[i]["borrow_mny"].ToString().Trim();
                            string strLoan = dtbSource.Rows[i]["loan_mny"].ToString().Trim();
                            if (strBorRow == "")
                            {
                                strBorRow = "0";
                            }
                            if (strLoan == "")
                            {
                                strLoan = "0";
                            }
                            objItems[i].m_decBorrow = Convert.ToDecimal(strBorRow);
                            objItems[i].m_decLoan = Convert.ToDecimal(strLoan);
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

        #region 将DataTable数据传递到库房总帐务
        /// <summary>
        /// 将DataTable数据传递到库房总帐务VO
        /// </summary>
        /// <param name="dtbSource">DataTable数据源</param>
        /// <param name="objItems">库房总帐务数据</param>
        private void CopyDataTableToVO(DataTable dtbSource, out clsStorageFin_VO[] objItems)
        {
            objItems = new clsStorageFin_VO[0];
            try
            {
                if (dtbSource != null)
                {
                    int intRow = dtbSource.Rows.Count;
                    if (intRow > 0)
                    {
                        objItems = new clsStorageFin_VO[intRow];
                        for (int i = 0; i < intRow; i++)
                        {
                            objItems[i] = new clsStorageFin_VO();
                            objItems[i].m_objStorage = new clsStorage_VO();
                            objItems[i].m_objStorage.m_strStroageID = dtbSource.Rows[i]["storageid_chr"].ToString().Trim();
                            objItems[i].m_objStorage.m_strStroageName = dtbSource.Rows[i]["storagename_vchr"].ToString().Trim();

                            objItems[i].m_objStorageOrdType = new clsStorageOrdType_VO();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeID = dtbSource.Rows[i]["storageordtypeid_chr"].ToString().Trim();
                            objItems[i].m_objStorageOrdType.m_strStorageOrdTypeName = dtbSource.Rows[i]["storageordtypename_vchr"].ToString().Trim();

                            string strBorRow = dtbSource.Rows[i]["borrow_mny"].ToString().Trim();
                            string strLoan = dtbSource.Rows[i]["loan_mny"].ToString().Trim();
                            if (strBorRow == "")
                            {
                                strBorRow = "0";
                            }
                            if (strLoan == "")
                            {
                                strLoan = "0";
                            }
                            objItems[i].m_decBorrow = Convert.ToDecimal(strBorRow);
                            objItems[i].m_decLoan = Convert.ToDecimal(strLoan);
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

        #region 库房药品明细帐  欧阳孔伟  2004-06-15

        #region 模糊查询库房药品明细帐
        /// <summary>
        /// 模糊查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByAny(string p_strSQL, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = @"SELECT *
							    FROM v_opr_storageoprordfinde
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region 按仓库查找库房药品明细帐
        /// <summary>
        /// 按仓库查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByStorage(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = " WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageOrdFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按帐务期查找库房药品明细帐
        /// <summary>
        /// 按帐务期查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByPeriod(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = " WHERE periodid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageOrdFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找库房药品明细帐
        /// <summary>
        /// 按单据类型查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByType(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = " WHERE storageordtypeid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageOrdFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按药品查找库房药品明细帐
        /// <summary>
        /// 按药品查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByMedicine(string p_strID, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = " WHERE medicineid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageOrdFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按时间段查找库房药品明细帐
        /// <summary>
        /// 按时间段查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strStartDate">开始时间</param>
        /// <param name="p_strEndDate">结束时间</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageOrdFinByDate(string p_strStartDate, string p_strEndDate, out clsStorageOprOrdFinDe_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageOprOrdFinDe_VO[0];

            string strSQL = " WHERE createdate_dat>=TO_DATE('" + p_strStartDate + "','yyyy-mm-dd hh24:mi:ss') AND createdate_dat<=TO_DATE('" + p_strEndDate + "','yyyy-mm-dd hh24:mi:ss')";
            lngRes = m_lngFindStorageOrdFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #endregion

        #region 库房药品月结帐  欧阳孔伟  2004-06-16

        #region 模糊查询库房药品月结帐
        /// <summary>
        /// 模糊查找库房药品月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedMonFinByAny(string p_strSQL, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            string strSQL = @"SELECT *
								FROM v_bal_storagemedmonfin
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region 按仓库查找库房药品月结帐
        /// <summary>
        /// 按仓库查找库房药品月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedMonFinByStorage(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            string strSQL = " WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按帐务期查找库房药品月结帐
        /// <summary>
        /// 按帐务期查找库房药品月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedMonFinByPeriod(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            string strSQL = " WHERE periodid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找库房药品月结帐
        /// <summary>
        /// 按单据类型查找库房药品月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedMonFinByType(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            string strSQL = " WHERE storageordtypeid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按药品查找库房药品月结帐
        /// <summary>
        /// 按药品查找库房药品明细帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedMonFinByMedicine(string p_strID, out clsStorageMedMonFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedMonFin_VO[0];

            string strSQL = " WHERE medicineid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #endregion

        #region 库房药品总帐  欧阳孔伟  2004-06-16

        #region 模糊查询库房药品总帐
        /// <summary>
        /// 模糊查找库房药品总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedFinByAny(string p_strSQL, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            string strSQL = @"SELECT *
								FROM v_tol_storagemedfin
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region 按仓库查找库房药品月结帐
        /// <summary>
        /// 按仓库查找库房药品总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedFinByStorage(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            string strSQL = " WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找库房药品月结帐
        /// <summary>
        /// 按单据类型查找库房药品总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedFinByType(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            string strSQL = " WHERE storageordtypeid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按药品查找库房药品月结帐
        /// <summary>
        /// 按药品查找库房药品总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">药品代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMedFinByMedicine(string p_strID, out clsStorageMedFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMedFin_VO[0];

            string strSQL = " WHERE medicineid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMedFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #endregion

        #region 库房月结帐  欧阳孔伟  2004-06-16

        #region 模糊查询库房月结帐
        /// <summary>
        /// 模糊查找库房月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMonFinByAny(string p_strSQL, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            string strSQL = @"SELECT *
								FROM v_bal_storagemonthfin
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region 按仓库查找库房月结帐
        /// <summary>
        /// 按仓库查找库房月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMonFinByStorage(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            string strSQL = " WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按帐务期查找库房月结帐
        /// <summary>
        /// 按帐务期查找库房月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">帐务期代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMonFinByPeriod(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];
            string strSQL = " WHERE periodid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找库房月结帐
        /// <summary>
        /// 按单据类型查找库房月结帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageMonFinByType(string p_strID, out clsStorageMonthFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageMonthFin_VO[0];

            string strSQL = " WHERE storageordtypeid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageMonFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #endregion

        #region 库房总帐  欧阳孔伟  2004-06-16

        #region 模糊查询库房总帐
        /// <summary>
        /// 模糊查找库房总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strSQL">SQL语句</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageFinByAny(string p_strSQL, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            string strSQL = @"SELECT *
								FROM v_tol_storagefin
							" + p_strSQL + " ";
            DataTable dtbResult = new DataTable();

            try
            {
                com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref dtbResult);


                if (lngRes > 0)
                {
                    CopyDataTableToVO(dtbResult, out p_objResultArr);
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

        #region 按仓库查找库房总帐
        /// <summary>
        /// 按仓库查找库房总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">仓库代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageFinByStorage(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            string strSQL = " WHERE storageid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #region 按单据类型查找库房总帐
        /// <summary>
        /// 按单据类型查找库房总帐
        /// </summary>
        /// <param name="p_objPrincipal">安全标识</param>
        /// <param name="p_strID">单据类型代码</param>
        /// <param name="p_objResultArr">输出数据</param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngFindStorageFinByType(string p_strID, out clsStorageFin_VO[] p_objResultArr)
        {
            long lngRes = 0;
            p_objResultArr = new clsStorageFin_VO[0];

            string strSQL = " WHERE storageordtypeid_chr='" + p_strID + "'";
            lngRes = m_lngFindStorageFinByAny(strSQL, out p_objResultArr);

            return lngRes;

        }
        #endregion

        #endregion


        #endregion


        #region 药库系统的登账模块的方法（新系统）2004-11-23

        #region 获得出入库及调价未登账的数据
        /// <summary>
        /// 获得出入库及调价未登账的数据
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="MedStorageArr"></param>
        /// <param name="MedStorageChangArr"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetMedStorageUnAcct(out DataTable MedStorageArr, out DataTable MedStorageChangArr)
        {
            long lngRes = 0;
            MedStorageArr = new DataTable();
            MedStorageChangArr = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL = @"SELECT b.sign_int,
                 (CASE b.sign_int
                     WHEN 1
                        THEN '入库'
                     WHEN 2
                        THEN '出库'
                  END
                 ) AS flag,
                 a.storageordid_chr as id, a.storageordtypeid_chr as type,
                 b.storageordtypename_vchr as typeName,
                 a.storageid_chr , a.periodid_chr,
                 a.inord_dat, c.lastname_vchr as creatName,a.PSTATUS_INT,
                 d.lastname_vchr as aduitempName, a.acctemp_chr,e.lastname_vchr as acctempName,
                 a.acctdate_dat,a.CREATEDATE_DAT,a.CREATEDATE_DAT,a.TOLMNY_MNY as tolMoney
            FROM t_opr_storageord a,
                 t_aid_storageordtype b, 
                 t_bse_employee c,
                 t_bse_employee d,
                 t_bse_employee e
           WHERE a.pstatus_int <> 1
             AND a.storageordtypeid_chr = b.storageordtypeid_chr
             AND a.creatorid_chr = c.empid_chr(+)
             AND a.aduitemp_chr = d.empid_chr(+)
             AND a.aduitemp_chr = e.empid_chr(+)
             AND a.creatorid_chr IS NOT NULL
             AND a.createdate_dat IS NOT NULL
             AND a.aduitemp_chr IS NOT NULL
             AND a.aduitdate_dat IS NOT NULL";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref MedStorageArr);
            }
            catch (Exception objEx)
            {
                string strTmp = objEx.Message;
                com.digitalwave.Utility.clsLogText objLogger = new clsLogText();
                bool blnRes = objLogger.LogError(objEx);
            }
            strSQL = @"SELECT a.medicinepricechgapplno_chr as id,
                 a.periodid_chr,
                 a.appldate_dat, c.lastname_vchr as creatName,
                 d.lastname_vchr as aduitempName, a.acctemp_chr,e.lastname_vchr as acctempName,
                 a.acctdate_dat,a.PSTATUS_INT,a.CREATEDATE_DAT,a.CREATEDATE_DAT,a.ODDS_MNY as tolMoney
            FROM t_opr_medicinepricechgappl a,
                 t_bse_employee c,
                 t_bse_employee d,
				 t_bse_employee e
           WHERE a.pstatus_int <> 1
             AND a.creatorid_chr = c.empid_chr(+)
             AND a.aduitemp_chr = d.empid_chr(+)
             AND a.aduitemp_chr = e.empid_chr(+)
             AND a.creatorid_chr IS NOT NULL
             AND a.createdate_dat IS NOT NULL
             AND a.aduitemp_chr IS NOT NULL
             AND a.aduitdate_dat IS NOT NULL";
            try
            {
                lngRes = objHRPSvc.lngGetDataTableWithoutParameters(strSQL, ref MedStorageChangArr);
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

        #region 根据单号ID获得单号明细
        /// <summary>
        /// 根据单号ID获得单号明细
        /// </summary>
        /// <param name="p_objPrincipal"></param>
        /// <param name="command">1,出入库ID。2，调价ID</param>
        /// <param name="strID"></param>
        /// <param name="dtbResult"></param>
        /// <returns></returns>
        [AutoComplete]
        public long m_lngGetDeById(int command, string strID, out DataTable dtbResult)
        {
            long lngRes = 0;
            dtbResult = new DataTable();
            com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objHRPSvc = new com.digitalwave.iCare.middletier.HRPService.clsHRPTableService();
            string strSQL;
            if (command == 1)
            {
                strSQL = @"select a.STORAGEORDDEID_CHR,a.MEDICINEID_CHR,a.SYSLOTNO_CHR,a.ORD_DAT,a.ROWNO_CHR,a.UNITID_CHR,a.QTY_DEC,a.USEFULLIFE_DAT,a.BUYTOLPRICE_MNY,a.BUYUNITPRICE_MNY,a.SALEUNITPRICE_MNY,b.MEDICINENAME_VCHR,b.MEDSPEC_VCHR,a. from t_opr_storageordde a,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.STORAGEORDID_CHR='" + strID + "'";
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
            }
            if (command == 2)
            {
                strSQL = @"select a.ROWNO_CHR,a.MEDICINEID_CHR,a.UNITID_CHR,a.CURPRICE_MNY,a.CHANGEPRICE_MNY,a.MEDSPEC_VCHR,a.ODDSDE_MNY,a.QTY_DEC,b.MEDICINENAME_VCHR from t_opr_medicinepricechgapplde a,t_bse_medicine b where a.MEDICINEID_CHR=b.MEDICINEID_CHR and a.MEDICINEPRICECHGAPPLNO_CHR='" + strID + "'";
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
            }

            return lngRes;
        }

        #endregion
         
        #endregion
    }
    #endregion
}
